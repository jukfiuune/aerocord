using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class Main : WindowsFormsAero.AeroForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private string AccessToken;
        private Signin signin;
        private string userPFP;
        private bool DarkMode = false;
        private string RenderMode = "Aero";
        public Main(string token, bool darkmode, string rendermode, Signin signinArg)
        {
            InitializeComponent();
            signin = signinArg;
            AccessToken = token;
            DarkMode = darkmode;
            RenderMode = rendermode;
            if (DarkMode && RenderMode != "Aero") _ = new DarkModeCS(this);
            SetUserInfo();
            PopulateFriendsTab();
            PopulateServersTab();
        }

        private void SetUserInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse("users/@me");
                //Console.WriteLine(userProfile);
                string displayname = userProfile.global_name;
                if (userProfile.global_name != null) { displayname = userProfile.global_name; } else { displayname = userProfile.username; }
                string bio = userProfile.bio;
                usernameLabel.Text = displayname;
                descriptionLabel.Text = bio;
                userPFP = $"https://cdn.discordapp.com/avatars/{userProfile.id}/{userProfile.avatar}.png";
                profilepicture.ImageLocation = userPFP;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        private void PopulateFriendsTab()
        {
            try
            {
                dynamic friends = GetApiResponse("users/@me/relationships");
                //Console.WriteLine(friends);
                List<ListViewItem> friendNames = new List<ListViewItem>();
                foreach (var friend in friends)
                {
                    if (friend.type == 1 && friend.user.global_name != null)
                    {
                        friendNames.Add(new ListViewItem((string)friend.user.global_name/*, $"https://cdn.discordapp.com/avatars/{friend.user.id}/{friend.user.avatar}.png"*/));
                    }
                    else if(friend.type == 1 && friend.user.username != null)
                    {
                        friendNames.Add(new ListViewItem((string)friend.user.username/*, $"https://cdn.discordapp.com/avatars/{friend.user.id}/{friend.user.avatar}.png"*/));
                    }
                }
                friendsList.Items.AddRange(friendNames.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve friend list", ex);
            }
        }
        private long GetChatID(string name)
        {
            try
            {
                dynamic channels = GetApiResponse("users/@me/channels");
                foreach (var channel in channels)
                {
                    if (channel.type == 1 && channel.recipients[0].global_name != null)
                    {
                        if ((string)channel.recipients[0].global_name == name)
                        {
                            return (long)channel.id;
                        }
                    }
                    else if (channel.type == 1 && channel.recipients[0].username != null)
                    {
                        if ((string)channel.recipients[0].username == name)
                        {
                            return (long)channel.id;
                        }
                    }
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve friends", ex);
                return -1;
            }
        }
        private long GetFriendID(string name)
        {
            try
            {
                dynamic friends = GetApiResponse("users/@me/relationships");
                foreach (var friend in friends)
                {
                    if (friend.type == 1 && friend.user.global_name != null)
                    {
                        if ((string)friend.user.global_name == name)
                        {
                            return (long)friend.id;
                        }
                    }
                    else if (friend.type == 1 && friend.user.username != null)
                    {
                        if ((string)friend.user.username == name)
                        {
                            return (long)friend.id;
                        }
                    }
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve friends", ex);
                return -1;
            }
        }
        private long GetServerID(string name)
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                foreach (var guild in guilds)
                {
                    if (guild.name.ToString() == name) return (long)guild.id;
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
                return -1;
            }
        }
        private void PopulateServersTab()
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                List<ListViewItem> serverNames = new List<ListViewItem>();
                foreach (var guild in guilds)
                {
                    string guildName = guild.name.ToString();

                    serverNames.Add(new ListViewItem(guildName));
                }
                serversList.Items.AddRange(serverNames.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }

        private dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnShown(EventArgs e)
        { 
            base.OnShown(e);

            GlassMargins = new Padding(12, 118, 12, 12);
            if (DarkMode && RenderMode != "Aero")
            {
                GlassMargins = new Padding(-1, -1, -1, -1);
                PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 1);
            }
            switch (RenderMode)
            {
                case "Mica":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;
                case "Acrylic":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;
            }

            signin.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            signin.Close();
        }

        private void friendsList_DoubleClick(object sender, EventArgs ex)
        {
            if (friendsList.SelectedItems[0].Text != null)
            {
                string selectedFriend = friendsList.SelectedItems[0].Text;
                long chatID = GetChatID(selectedFriend);
                long friendID = GetFriendID(selectedFriend);
                if (chatID >= 0)
                {
                    DM dm = new DM(chatID, friendID, AccessToken, userPFP, DarkMode, RenderMode);
                    dm.Show();
                } else MessageBox.Show("Unable to open this DM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void serversList_DoubleClick(object sender, EventArgs ex)
        {
            if (serversList.SelectedItems[0].Text != null)
            {
                string selectedServer = serversList.SelectedItems[0].Text;
                long serverID = GetServerID(selectedServer);
                if (serverID >= 0)
                {
                    Server server = new Server(serverID, AccessToken, DarkMode, RenderMode);
                    server.Show();
                }
                else MessageBox.Show("Unable to open this Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
