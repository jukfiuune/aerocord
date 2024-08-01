using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

namespace Aerocord
{
    public class AutoColorMode : IDisposable
    {
        const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        const string RegistryValueName = "AppsUseLightTheme";
        static WindowsIdentity currentUser = WindowsIdentity.GetCurrent();

        string query = string.Format(
            CultureInfo.InvariantCulture,
            @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
            currentUser.User.Value,
            RegistryKeyPath.Replace(@"\", @"\\"),
            RegistryValueName);

        private ManagementEventWatcher watcher;

        private Main Form;

        public AutoColorMode(Main form)
        {
            Form = form;
            watcher = new ManagementEventWatcher(query);
            watcher.EventArrived +=
                new EventArrivedEventHandler(registryEventHandler);
            watcher.Start();
        }

        public void Dispose()
        {
            this.watcher?.Dispose();
        }

        private void registryEventHandler(object sender, EventArrivedEventArgs e)
        {
            bool DarkMode = !Convert.ToBoolean(Int32.Parse(SysInfo.GetRegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "1").ToString()));
            Form.SetGlobalMode(DarkMode);
        }
    }
}
