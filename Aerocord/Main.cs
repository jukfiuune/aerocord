using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
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

        private WebSocketClient websocketClient;

        public dynamic friends;
        public dynamic guilds;
        public Dictionary<long, string> friendStatuses = new Dictionary<long, string>();

        public Dictionary<long, DM> listDMs = new Dictionary<long, DM>();
        public Dictionary<long, Server> listServers = new Dictionary<long, Server>();

        public Main(string token, bool darkmode, string rendermode, Signin signinArg)
        {
            InitializeComponent();
            signin = signinArg;
            AccessToken = token;
            DarkMode = darkmode;
            RenderMode = rendermode;
            if (DarkMode)
            {
                _ = new DarkModeCS(this);
            }
            SetUserInfo();
            friendsButton.Click += FriendsButton_Click;
            button2.Click += ServersButton_Click;
            websocketClient = new WebSocketClient(AccessToken, this);
        }

        private void SetUserInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse("users/@me");
                string displayname = userProfile.global_name ?? userProfile.username;
                string bio = userProfile.bio;
                usernameLabel.Text = displayname;
                userPFP = $"https://cdn.discordapp.com/avatars/{userProfile.id}/{userProfile.avatar}.png";
                profilepicture.ImageLocation = userPFP;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        private void FriendsButton_Click(object sender, EventArgs e)
        {
            PopulateFriendsTab();
        }

        private void ServersButton_Click(object sender, EventArgs e)
        {
            PopulateServersTab();
        }

        delegate void PopulateFriendsTabCallback();
        public void PopulateFriendsTab()
        {
            if (InvokeRequired)
            {
                PopulateFriendsTabCallback d = new PopulateFriendsTabCallback(PopulateFriendsTab);
                Invoke(d);
            }
            else
            {
                this.Show();
                try
                {
                    friendserverPanel.Controls.Clear();

                    foreach (var friend in friends)
                    {
                        if (friend.type == 1)
                        {
                            string username = friend.user.global_name ?? friend.user.username;
                            string avatarUrl = $"https://cdn.discordapp.com/avatars/{friend.user.id}/{friend.user.avatar}.png";

                            FriendItem friendItem = new FriendItem
                            {
                                Username = username,
                                ProfilePictureUrl = avatarUrl
                            };

                            if (!DarkMode)
                            {
                                friendItem.LabelColor = System.Drawing.Color.Black;
                            }

                            friendItem.Clicked += (sender, e) =>
                            {
                                var clickedFriendItem = (FriendItem)sender;
                                string selectedFriend = clickedFriendItem.Username;
                                long chatID = GetChatID(selectedFriend);
                                long friendID = GetFriendID(selectedFriend);
                                if (chatID >= 0)
                                {
                                    listDMs.Add(chatID, new DM(chatID, friendID, AccessToken, userPFP, DarkMode, RenderMode));
                                    listDMs[chatID].ChangeStatus(friendStatuses[friendID]);
                                    listDMs[chatID].Show();
                                }
                                else
                                {
                                    MessageBox.Show("Unable to open this DM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };

                            friendserverPanel.Controls.Add(friendItem);
                        }
                    }
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to retrieve friend list", ex);
                }
            }
        }

        delegate void PopulateServersTabCallback();
        public void PopulateServersTab()
        {
            if (InvokeRequired)
            {
                PopulateServersTabCallback d = new PopulateServersTabCallback(PopulateServersTab);
                Invoke(d);
            }
            else
            {
                try
                {
                    friendserverPanel.Controls.Clear();

                    foreach (var guild in guilds)
                    {
                        string guildName = guild.name.ToString();
                        string iconUrl = $"https://cdn.discordapp.com/icons/{guild.id}/{guild.icon}.png";

                        FriendItem serverItem = new FriendItem
                        {
                            ServerName = guildName,
                            ProfilePictureUrl = iconUrl
                        };

                        if (!DarkMode)
                        {
                            serverItem.LabelColor = System.Drawing.Color.Black;
                        }

                        serverItem.Clicked += (sender, e) =>
                        {
                            var clickedServerItem = (FriendItem)sender;
                            string selectedServer = clickedServerItem.ServerName;
                            long serverID = GetServerID(selectedServer);
                            if (serverID >= 0)
                            {
                                listServers.Add(serverID, new Server(serverID, AccessToken, DarkMode, RenderMode));
                                listServers[serverID].Show();
                            }
                            else
                            {
                                MessageBox.Show("Unable to open this Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };
                        friendserverPanel.Controls.Add(serverItem);
                    }
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to retrieve friend list", ex);
                }
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

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(-1, -1, -1, -1);
            if (DarkMode)
            {
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

            signin.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            signin.Close();
        }

        private void profilepicture_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(DarkMode, RenderMode);
            settings.Show();
        }

        private void descriptionLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
