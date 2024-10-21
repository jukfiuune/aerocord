using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class UserProfile : AutoForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private string AccessToken;
        public long FriendID;

        public UserProfile(DM parentForm, long friendid, string token, bool darkmode, string rendermode)
        {
            InitializeComponent();
            DarkMode = darkmode;
            RenderMode = rendermode;
            if (DarkMode) { _ = new DarkModeCS(this); PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 1); }
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
            AccessToken = token;
            FriendID = friendid;
            SetFriendInfo();
            frame.Image = parentForm.friendFramePic;
            statusLabel.Text = parentForm.status;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = GlassMarginsLight;
            if (DarkMode) GlassMargins = GlassMarginsDark;
        }

        private void SetFriendInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse($"users/{FriendID}/profile");
                string displayname = userProfile.user.global_name ?? userProfile.user.username;
                string bio = Regex.Replace((string)userProfile.user.bio, @"(<.*?>)|(\*\*|\*|__|_|~~|\|\|)", string.Empty);
                usernameLabel.Text = displayname;
                profilepicture.ImageLocation = $"https://cdn.discordapp.com/avatars/{userProfile.user.id}/{userProfile.user.avatar}.png";
                pronounsLabel.Text = userProfile.user_profile.pronouns;
                bioLabel.Text = bio;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        public dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                Console.WriteLine(jsonResponse.ToString());
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        public void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            GC.Collect();
        }
    }
}
