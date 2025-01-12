using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Aerocord
{
    public partial class Main : AutoForm
    {
        Loading loading;

        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private string AccessToken;

        private WebSocketClient websocketClient;

        public dynamic friends;
        public dynamic channels;
        public dynamic guilds;
        public dynamic userProfile;
        public Dictionary<string, string> friendStatuses = new Dictionary<string, string>();
        public Dictionary<string, string> friendCustomStatuses = new Dictionary<string, string>();

        public Main()
        {
            InitializeComponent();
            AccessToken = "";
            websocketClient = new WebSocketClient(AccessToken, this);
            dmsButton.Click += (sender, e) => { if(websocketClient.init) PopulateDMs(); };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if(loading == null)
            {
                loading = new Loading(this);
                loading.Show();
            }
        }

        delegate void PopulateDMsCallback();
        public void PopulateDMs()
        {
            if (InvokeRequired)
            {
                PopulateDMsCallback d = new PopulateDMsCallback(PopulateDMs);
                Invoke(d);
            }
            else
            {
                try
                {
                    channelList.Controls.Clear();
                    //Dictionary<string, long> timestamps = new Dictionary<string, long>();

                    foreach (var friend in friends)
                    {
                        if (friend.type == 1)
                        {
                            string username = friend.nickname != null ? friend.nickname : friend.user.global_name != null ? friend.user.global_name : friend.user.username;
                            string friendId = (string)friend.user.id;
                            string chatId = GetChatID(friendId);
                            //string avatarUrl = $"https://cdn.discordapp.com/avatars/{friend.user.id}/{friend.user.avatar}.png";

                            if (chatId == "-1") continue;

                            /*dynamic lastMessage = GetApiResponse($"channels/{chatId}/messages?limit=1");

                            if (lastMessage.Count > 0) timestamps.Add(username, DateTimeOffset.Parse((string)lastMessage[0].timestamp).ToUnixTimeMilliseconds());*/

                            ChannelEntry friendEntry = new ChannelEntry
                            {
                                Channel = username,
                                ChatId = chatId,
                                FriendId = friendId
                            };

                            if (friendStatuses.ContainsKey(friendId))
                            {
                                switch(friendStatuses[friendId])
                                {
                                    case "online":
                                        friendEntry.ChannelType = Properties.Resources.Active;
                                        break;
                                    case "idle":
                                        friendEntry.ChannelType = Properties.Resources.Idle;
                                        break;
                                    case "dnd":
                                        friendEntry.ChannelType = Properties.Resources.Dnd;
                                        break;
                                    case "offline":
                                        friendEntry.ChannelType = Properties.Resources.Offline;
                                        break;
                                    default:
                                        friendEntry.ChannelType = Properties.Resources.Offline;
                                        break;
                                }
                            }
                            else
                            {
                                friendEntry.ChannelType = Properties.Resources.Offline;
                            }

                            //if (friend.user.avatar != null) friendEntry.ProfilePictureUrl = avatarUrl;

                            /*friendEntry.Click += (sender, e) =>
                            {
                                var clickedFriendEntry = (ChannelEntry)sender;
                                string selectedFriend = clickedFriendEntry.Name;
                                string chatID = clickedFriendEntry.ChatId;
                                string friendID = clickedFriendEntry.FriendId;
                                if (chatID.Length > 1)
                                {
                                    listDMs.Add(chatID, new DM(this, chatID, friendID, AccessToken, userPFP, username, DarkMode, RenderMode));
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
                                    MessageBox.Show($"Unable to open this DM ({chatID.ToString()}, {friendId.ToString()})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };*/

                            channelList.Controls.Add(friendEntry);
                        }
                    }
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to retrieve friend list", ex);
                }
            }
        }

        delegate void PopulateChannelsCallback(dynamic channels);
        public void PopulateChannels(dynamic channels)
        {
            if (InvokeRequired)
            {
                PopulateChannelsCallback d = new PopulateChannelsCallback(PopulateChannels);
                Invoke(d, new object[] { channels });
            }
            else
            {
                try
                {
                    channelList.Controls.Clear();

                    foreach (var channel in channels)
                    {
                        switch ((int)channel.type)
                        {
                            case 0:
                                {
                                    Console.WriteLine(channel.name);
                                    ChannelEntry channelEntry = new ChannelEntry
                                    {
                                        Channel = ((string)channel.name).ToLower(),
                                        ChatId = (string)channel.id,
                                        ChannelType = Properties.Resources.Chat
                                    };
                                    channelList.Controls.Add(channelEntry);
                                }
                                break;
                            case 2:
                                {
                                    ChannelEntry channelEntry = new ChannelEntry
                                    {
                                        Channel = ((string)channel.name).ToString(),
                                        ChatId = (string)channel.id,
                                        ChannelType = Properties.Resources.Call
                                    };
                                    channelList.Controls.Add(channelEntry);
                                }
                                break;
                        }
                    }
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to retrieve channel list", ex);
                }
            }
        }

        delegate void PopulateServersCallback();
        public void PopulateServers()
        {
            if (InvokeRequired)
            {
                PopulateServersCallback d = new PopulateServersCallback(PopulateServers);
                Invoke(d);
            }
            else
            {
                try
                {
                    foreach (var guild in guilds)
                    {
                        string guildName = guild.name.ToString();
                        string serverId = (string)guild.id;
                        string iconUrl = $"https://cdn.discordapp.com/icons/{guild.id}/{guild.icon}.png";

                        ServerEntry serverItem = new ServerEntry
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(42, 42),
                            Margin = new Padding(0, 6, 0, 6),
                            ServerId = serverId
                        };

                        if (guild.icon != null) {
                            serverItem.ImageLocation = iconUrl;
                        }
                        else
                        {
                            serverItem.Image = Properties.Resources.Server;
                        }

                        serverItem.Click += (sender, e) =>
                        {
                            var clickedServerItem = (ServerEntry)sender;
                            //string selectedServer = clickedServerItem.ServerName;
                            string serverIdClicked = clickedServerItem.ServerId;

                            if (serverIdClicked.Length > 1)
                            {
                                PopulateChannels(guild["channels"]);
                            }
                            else
                            {
                                MessageBox.Show("Unable to open this Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };
                        sideBarFlow.Controls.Add(serverItem);
                    }
                    this.Show();
                    loading.Hide();
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

        private string GetChatID(string friendId)
        {
            foreach (var channel in channels)
            {
                if (channel.type == 1 && channel.recipients[0].id == friendId.ToString())
                {
                    return (string)channel.id;
                }
            }
            return "-1";
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
