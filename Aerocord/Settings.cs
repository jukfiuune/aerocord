﻿using System;
using System.Windows.Forms;
using System.IO;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class Settings : WindowsFormsAero.AeroForm
    {
        private const string TokenFileName = "aerocord_config.txt";
        private static string LCUVer = SysInfo.GetVersionString();
        private static int MajorVersion = Int32.Parse(LCUVer.Split('.')[0]);
        private static int MinorVersion = Int32.Parse(LCUVer.Split('.')[1]);
        private static int BuildNumber = Int32.Parse(LCUVer.Split('.')[2]);

        private bool DarkMode;
        private string RenderMode;

        public Padding GlassMarginsLight = new Padding(115, 15, 15, 73);

        public Settings(bool darkmodee, string rendermodee)
        {
            InitializeComponent();
            DarkMode = darkmodee;
            RenderMode = rendermodee;
            GlassMargins = new Padding(115, 15, 15, 73);
            if (DarkMode)
            {
                GlassMargins = new Padding(-1, -1, -1, -1);
                _ = new DarkModeCS(this, _DarkMode: true);
                PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 1);
            }
            switch (RenderMode)
            {
                case "Aero":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 1);
                    break;
                case "Mica":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;
                case "Acrylic":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;
                case "Mica Alt":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    break;
            }
            if (BuildNumber < 22000)
            {
                rendermode.Items.Remove("Mica");
                rendermode.Items.Remove("Mica Alt");
                if (BuildNumber < 16299)
                {
                    rendermode.Items.Remove("Acrylic");
                    if (MajorVersion < 10)
                    {
                        colormode.Items.Remove("System");
                        colormode.Items.Remove("Dark");
                    }
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(rendermode.SelectedItem.ToString()) || String.IsNullOrWhiteSpace(colormode.SelectedItem.ToString())) throw new ArgumentException("Modes cannot be unselected!");
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string filePath = Path.Combine(homeDirectory, TokenFileName);

                if (File.Exists(filePath))
                {
                    string AccessToken = "";
                    foreach (string line in File.ReadLines(filePath))
                    {
                        if (line.Contains("token="))
                        {
                            AccessToken = line.Replace("token=", "");
                        }
                    }

                    File.WriteAllText(filePath, "token=" + AccessToken + "\nrendermode=" + rendermode.SelectedItem + "\ncolormode=" + colormode.SelectedItem);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void colormode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (colormode.SelectedItem.ToString())
            {
                case "System":
                    DarkMode = !Convert.ToBoolean(Int32.Parse(MajorVersion != 10 ? "1" : SysInfo.GetRegistryValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "1").ToString()));
                    break;
                case "Light":
                    DarkMode = false;
                    break;
                case "Dark":
                    DarkMode = true;
                    break;
            }
            if (DarkMode)
            {
                GlassMargins = new Padding(-1, -1, -1, -1);
                _ = new DarkModeCS(this, _DarkMode: true);
                PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 1);
            }
            else
            {
                GlassMargins = new Padding(115, 15, 15, 73);
                _ = new DarkModeCS(this, _DarkMode: false);
                PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 0);
            }
        }

        private void rendermode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rendermode.SelectedItem.ToString())
            {
                case "Aero":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 1);
                    break;
                case "Mica":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;
                case "Acrylic":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;
                case "Mica Alt":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    break;
            }
        }
    }
}
