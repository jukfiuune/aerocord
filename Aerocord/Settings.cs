using System;
using System.Windows.Forms;
using System.IO;

namespace Aerocord
{
    public partial class Settings : Form
    {
        private const string TokenFileName = "aerocord_config.txt";
        private static string LCUVer = SysInfo.GetVersionString();
        private static int MajorVersion = Int32.Parse(LCUVer.Split('.')[0]);
        private static int MinorVersion = Int32.Parse(LCUVer.Split('.')[1]);
        private static int BuildNumber = Int32.Parse(LCUVer.Split('.')[2]);

        public Settings()
        {
            InitializeComponent();
            if (BuildNumber < 22000)
            {
                rendermode.Items.Remove("Mica");
                if (BuildNumber < 16299)
                {
                    rendermode.Items.Remove("Acrylic");
                    if (MajorVersion < 10)
                    {
                        colormode.Items.Remove("Light");
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
    }
}
