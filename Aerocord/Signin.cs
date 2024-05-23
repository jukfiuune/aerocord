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
using Newtonsoft.Json.Linq;

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
            GlassMargins = new Padding(82, 220, 11, 11);

            CheckToken();
        }

        private void signinButton_Click(object sender, EventArgs e)
        {
            string email = emailBox.Text.Trim();
            string password = passBox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PerformLoginWithPass(email, password);
        }

        private void PerformLoginWithPass(string email, string password)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var loginPayload = new
                    {
                        email = email,
                        password = password
                    };

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";

                    string response = webClient.UploadString("https://discord.com/api/v9/auth/login", Newtonsoft.Json.JsonConvert.SerializeObject(loginPayload));
                    var json = JObject.Parse(response);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        PerformLogin(accessToken, false);
                    }
                    else if (json["mfa"] != null && (bool)json["mfa"])
                    {
                        string ticket = json["ticket"].ToString();
                        string code = PromptFor2FACode();
                        Perform2FALogin(ticket, code);
                    }
                    else
                    {
                        MessageBox.Show("Failed to login. Please enter a valid token!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Failed to login. Please check your email and password! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PerformLogin(string accessToken, bool isAutomated)
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

        private void Perform2FALogin(string ticket, string code)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var mfaPayload = new
                    {
                        code = code,
                        ticket = ticket,
                        login_source = (string)null,
                        gift_code_sku_id = (string)null
                    };

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";

                    string response = webClient.UploadString("https://discord.com/api/v9/auth/mfa/totp", Newtonsoft.Json.JsonConvert.SerializeObject(mfaPayload));
                    var json = JObject.Parse(response);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        PerformLogin(accessToken, false);
                    }
                    else
                    {
                        MessageBox.Show("2FA failed. Please check your 2FA code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("2FA failed. Please check your 2FA code! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PromptFor2FACode()
        {
            using (var form = new Form())
            using (var label = new Label() { Text = "Enter your 2FA code:" })
            using (var inputBox = new System.Windows.Forms.TextBox())
            using (var buttonOk = new System.Windows.Forms.Button() { Text = "OK", DialogResult = DialogResult.OK })
            {
                ConfigureFormControls(form, label, inputBox, buttonOk);

                return form.ShowDialog() == DialogResult.OK ? inputBox.Text.Trim() : null;
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

        private void ConfigureFormControls(Form form, Label label, System.Windows.Forms.TextBox inputBox, System.Windows.Forms.Button buttonOk)
        {
            form.Text = "2FA Required";
            form.StartPosition = FormStartPosition.CenterParent;
            form.Width = 300;
            form.Height = 150;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            label.SetBounds(10, 10, 280, 20);
            inputBox.SetBounds(10, 40, 260, 20);
            buttonOk.SetBounds(110, 70, 75, 25);

            form.Controls.Add(label);
            form.Controls.Add(inputBox);
            form.Controls.Add(buttonOk);
            inputBox.Select();
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
            Token token = new Token(this);
            token.Show();
            this.Hide();
        }
    }
}
