using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;
using Microsoft.Win32;

namespace Aerocord
{
    public partial class Signin : WindowsFormsAero.AeroForm
    {

        private const string TokenFileName = "token.txt";

        public Signin()
        {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GlassMargins = new Padding(11, 220, 11, 29);

            CheckToken();
        }

        private void signinButton_Click(object sender, EventArgs e)
        {
            string tokenEdit = token.Text.Trim();

            if (string.IsNullOrEmpty(tokenEdit))
            {
                MessageBox.Show("Please enter your Discord access token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PerformLogin(tokenEdit, false);
        }

        private void PerformLogin(string accessToken, bool isAutomated)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = accessToken;

                try
                {
                    string userProfileJson = webClient.DownloadString("https://discord.com/api/v9/users/@me");

                    if(!isAutomated) SaveToken(accessToken);

                    SetIEVer();

                    Main mainForm = new Main(accessToken, this);
                    mainForm.Show();
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Failed to login. Please enter a valid token! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveToken(string accessToken)
        {
            try
            {
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string filePath = Path.Combine(homeDirectory, TokenFileName);

                File.WriteAllText(filePath, "token=" + accessToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckToken()
        {
            try
            {
                this.Hide();

                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string filePath = Path.Combine(homeDirectory, TokenFileName);

                if (File.Exists(filePath))
                {
                    foreach (string line in File.ReadLines(filePath))
                    {
                        if (line.Contains("token="))
                        {
                            PerformLogin(line.Replace("token=", ""), true);
                            return;
                        }
                    }
                }
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetIEVer()
        {
            int BrowserVer, RegVal;

            // get the installed IE version
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            // set the appropriate IE version
            if (BrowserVer >= 11)
                RegVal = 11001;
            else if (BrowserVer == 10)
                RegVal = 10001;
            else if (BrowserVer == 9)
                RegVal = 9999;
            else if (BrowserVer == 8)
                RegVal = 8888;
            else
                RegVal = 7000;

            // set the actual key
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree))
                if (Key.GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe") == null)
                    Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gist.github.com/MarvNC/e601f3603df22f36ebd3102c501116c6");
        }
    }
}
