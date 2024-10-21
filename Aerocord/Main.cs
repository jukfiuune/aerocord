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
    public partial class Main : AutoForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private string AccessToken;
        private Signin signin;
        private string userPFP;
        public string userStatus = "online";

        private WebSocketClient websocketClient;

        public dynamic friends;
        public dynamic channels;
        public dynamic guilds;
        public dynamic userProfile;
        public Dictionary<long, string> friendStatuses = new Dictionary<long, string>();
        public Dictionary<long, string> friendCustomStatuses = new Dictionary<long, string>();

        public Dictionary<long, DM> listDMs = new Dictionary<long, DM>();
        public Dictionary<long, Server> listServers = new Dictionary<long, Server>();

        public Main(string token, bool darkmode, string rendermode, bool autocolormode, Signin signinArg)
        {
            GlassMarginsLight = new Padding(15, 65, 205, 380);
            InitializeComponent();
            signin = signinArg;
            AccessToken = token;
            AutoColorMode = autocolormode;
            DarkMode = darkmode;
            RenderMode = rendermode;
            if (DarkMode)
            {
                _ = new DarkModeCS(this);
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
            friendsButton.Click += FriendsButton_Click;
            button2.Click += ServersButton_Click;
            websocketClient = new WebSocketClient(AccessToken, this);

            if (AutoColorMode) { AutoColorMode autoColorMode = new AutoColorMode(this); }
        }

        delegate void SetUserInfoCallback();
        public void SetUserInfo()
        {
            if (InvokeRequired)
            {
                SetUserInfoCallback d = new SetUserInfoCallback(SetUserInfo);
                Invoke(d);
            }
            else
            {
                string displayname = userProfile.global_name ?? userProfile.username;
                string bio = userProfile.bio;
                usernameLabel.Text = displayname;
                userPFP = $"https://cdn.discordapp.com/avatars/{userProfile.id}/{userProfile.avatar}.png";
                profilepicture.ImageLocation = userPFP;
            }
        }

        delegate void ChangeStatusCallback(string status, string custom_status);
        public void ChangeStatus(string status, string custom_status)
        {
            if (InvokeRequired)
            {
                ChangeStatusCallback d = new ChangeStatusCallback(ChangeStatus);
                Invoke(d, new object[] { status, custom_status });
            }
            else
            {
                frame.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(status);
                if (custom_status == "")
                {
                    switch (status)
                    {
                        case "online":
                            statusLabel.Text = "Online";
                            break;
                        case "dnd":
                            statusLabel.Text = "Do Not Disturb";
                            break;
                        case "idle":
                            statusLabel.Text = "Idle";
                            break;
                        case "offline":
                            statusLabel.Text = "Offline";
                            break;
                        default:
                            statusLabel.Text = "Online";
                            break;
                    }
                }
                else
                {
                    statusLabel.Text = custom_status;
                }
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
                try
                {
                    friendserverPanel.Controls.Clear();

                    foreach (var friend in friends)
                    {
                        if (friend.type == 1)
                        {
                            string username = friend.user.global_name ?? friend.user.username;
                            long friendId = long.Parse((string)friend.user.id);
                            long chatId = GetChatID(friendId);
                            string avatarUrl = $"https://cdn.discordapp.com/avatars/{friend.user.id}/{friend.user.avatar}.png";

                            FriendItem friendItem = new FriendItem
                            {
                                Username = username,
                                ProfilePictureUrl = avatarUrl,
                                ChatId = chatId,
                                FriendId = friendId
                            };

                            if (!DarkMode)
                            {
                                friendItem.LabelColor = System.Drawing.Color.Black;
                            }



                            friendItem.Clicked += (sender, e) =>
                            {
                                var clickedFriendItem = (FriendItem)sender;
                                string selectedFriend = clickedFriendItem.Username;
                                long chatID = clickedFriendItem.ChatId;
                                long friendID = clickedFriendItem.FriendId;
                                if (chatID >= 0)
                                {
                                    listDMs.Add(chatID, new DM(this, chatID, friendID, AccessToken, userPFP, DarkMode, RenderMode));
                                    if (friendCustomStatuses.ContainsKey(friendID) && friendStatuses.ContainsKey(friendID))
                                    {
                                        listDMs[chatID].ChangeStatus(friendStatuses[friendID], friendCustomStatuses[friendID]);
                                    }
                                    else if (friendStatuses.ContainsKey(friendID))
                                    {
                                        listDMs[chatID].ChangeStatus(friendStatuses[friendID], "");
                                    }
                                    else
                                    {
                                        listDMs[chatID].ChangeStatus("offline", "");
                                    }
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
                    this.Show();
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
                        long serverId = long.Parse((string)guild.id);
                        string iconUrl = $"https://cdn.discordapp.com/icons/{guild.id}/{guild.icon}.png";

                        FriendItem serverItem = new FriendItem
                        {
                            ServerName = guildName,
                            ProfilePictureUrl = iconUrl,
                            FriendId = serverId
                        };

                        if (!DarkMode)
                        {
                            serverItem.LabelColor = System.Drawing.Color.Black;
                        }

                        serverItem.Clicked += (sender, e) =>
                        {
                            var clickedServerItem = (FriendItem)sender;
                            string selectedServer = clickedServerItem.ServerName;
                            long serverID = clickedServerItem.FriendId;
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

        private long GetChatID(long friendId)
        {
            foreach (var channel in channels)
            {
                if (channel.type == 1 && channel.recipients[0].id == friendId.ToString())
                {
                    return long.Parse((string)channel.id);
                }
            }
            return -1;
        }

        delegate void SetGlobalModeCallback(bool DarkMode);
        public void SetGlobalMode(bool DarkMode)
        {
            if (InvokeRequired)
            {
                SetGlobalModeCallback d = new SetGlobalModeCallback(SetGlobalMode);
                Invoke(d, new object[] { DarkMode });
            }
            else
            {
                this.DarkMode = DarkMode;
                foreach(var form in Application.OpenForms)
                {
                    if(form.GetType().BaseType == typeof(AutoForm))
                    {
                        AutoForm autoform = (AutoForm)form;
                        if (DarkMode)
                        {
                            autoform.GlassMargins = autoform.GlassMarginsDark;
                        }
                        else
                        {
                            autoform.GlassMargins = autoform.GlassMarginsLight;
                        }
                        _ = new DarkModeCS(autoform, _DarkMode: DarkMode);
                    }
                }
            }

        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(15, 65, 205, 380);
            if(DarkMode) GlassMargins = new Padding(-1, -1, -1, -1);

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
