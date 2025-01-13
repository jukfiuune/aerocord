using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        public string currentChatId = "";

        public Main()
        {
            InitializeComponent();
            AccessToken = "";
            websocketClient = new WebSocketClient(AccessToken, this);
            dmsButton.Click += (sender, e) => { if (websocketClient.init) PopulateDMs(); };
            messageSend.KeyDown += messageSend_KeyDown;
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

        #region Populate Fields
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

                            friendEntry.Click += (sender, e) =>
                            {
                                var clickedFriendEntry = (ChannelEntry)sender;
                                string selectedFriend = clickedFriendEntry.Name;
                                string chatID = clickedFriendEntry.ChatId;
                                string friendID = clickedFriendEntry.FriendId;
                                if (chatID.Length > 1)
                                {
                                    PopulateMessageContainer(clickedFriendEntry);
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show($"Unable to open this DM ({chatID.ToString()}, {friendId.ToString()})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };

                            channelList.Controls.Add(friendEntry);
                        }
                    }
                    foreach (var channel in channels)
                    {
                        if (channel.type == 3)
                        {
                            string name = channel.name != null ? channel.name : channel.recipients.Count >= 2 ? channel.recipients[0].username + ", " + channel.recipients[1].username + "..." : channel.recipients.Count >= 1 ? channel.recipients[0].username : "Empty group";
                            string chatId = (string)channel.id;
                            Image type = Properties.Resources.OfflineG;

                            ChannelEntry groupEntry = new ChannelEntry
                            {
                                Channel = name,
                                ChatId = chatId
                            };

                            foreach (var recipient in channel.recipients) 
                            {
                                string recId = (string)recipient.id;
                                if (friendStatuses.ContainsKey(recId))
                                {
                                    if (friendStatuses[recId] == "online")
                                    {
                                        type = Properties.Resources.ActiveG;
                                        break;
                                    }
                                    else
                                    {
                                        switch (friendStatuses[recId])
                                        {
                                            case "dnd":
                                                type = Properties.Resources.DndG;
                                                break;
                                            case "idle":
                                                if (type != Properties.Resources.DndG) type = Properties.Resources.IdleG;
                                                break;
                                        }
                                    }
                                }
                            }

                            groupEntry.ChannelType = type;

                            groupEntry.Click += (sender, e) =>
                            {
                                var clickedGroupEntry = (ChannelEntry)sender;
                                string selectedFriend = clickedGroupEntry.Name;
                                string chatID = clickedGroupEntry.ChatId;
                                if (chatID.Length > 1)
                                {
                                    PopulateMessageContainer(clickedGroupEntry);
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show($"Unable to open this DM ({chatID.ToString()})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };

                            channelList.Controls.Add(groupEntry);
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
                                    ChannelEntry channelEntry = new ChannelEntry
                                    {
                                        Channel = ((string)channel.name).ToLower(),
                                        ChatId = (string)channel.id,
                                        ChannelType = Properties.Resources.Chat
                                    };

                                    channelEntry.Click += (sender, e) =>
                                    {
                                        var clickedChannelEntry = (ChannelEntry)sender;
                                        string selectedChannel = clickedChannelEntry.Name;
                                        string chatID = clickedChannelEntry.ChatId;
                                        if (chatID.Length > 1)
                                        {
                                            PopulateMessageContainer(clickedChannelEntry);
                                        }
                                        else
                                        {
                                            System.Windows.Forms.MessageBox.Show($"Unable to open this Channel ({chatID.ToString()})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
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

                                    channelEntry.Click += (sender, e) =>
                                    {
                                        var clickedChannelEntry = (ChannelEntry)sender;
                                        string selectedChannel = clickedChannelEntry.Name;
                                        string chatID = clickedChannelEntry.ChatId;
                                        if (chatID.Length > 1)
                                        {
                                            PopulateMessageContainer(clickedChannelEntry);
                                        }
                                        else
                                        {
                                            System.Windows.Forms.MessageBox.Show($"Unable to open this Channel ({chatID.ToString()})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
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
                                System.Windows.Forms.MessageBox.Show("Unable to open this Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        delegate void PopulateMessageContainerCallback(ChannelEntry channel);
        public void PopulateMessageContainer(ChannelEntry channel)
        {
            if (InvokeRequired)
            {
                PopulateMessageContainerCallback d = new PopulateMessageContainerCallback(PopulateMessageContainer);
                Invoke(d, new object[] { channel });
            }
            else
            {
                try
                {
                    messageContainer.Controls.Clear();

                    dynamic messages = GetApiResponse($"channels/{channel.ChatId}/messages?limit=50");

                    for (int i = messages.Count - 1; i >= 0; i--)
                    {
                        string username = messages[i].author.global_name != null ? messages[i].author.global_name : messages[i].author.username;
                        string content = messages[i].content;

                        ChooseMessageBox(username, content, (int)messages[i].type.Value);
                    }

                    if(messageContainer.Controls.Count > 0) ScrollToBottom();

                    currentChatId = channel.ChatId;
                    currentChannel.Text = channel.Channel;
                    currentChannelType.Image = channel.ChannelType;

                    if (!messageContainer.Visible)
                    {
                        currentChannel.Visible = true;
                        currentChannelType.Visible = true;
                        messageSend.Visible = true;
                        messageContainer.Visible = true;
                    }
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to retrieve user profile", ex);
                }
            }
        }

        #endregion

        #region Message Box

        #region Adding Message to Box
        delegate void ChooseMessageBoxCallback(string username, string content, int type);
        public void ChooseMessageBox(string username, string content, int type)
        {
            if(InvokeRequired)
            {
                ChooseMessageBoxCallback d = new ChooseMessageBoxCallback(ChooseMessageBox);
                Invoke(d, new object[] { username, content, type });
            }
            else
            {
                switch (type)
                {
                    case 3:
                        AddMessageBox(username, "Started a call.");
                        break;
                    case 7:
                        AddMessageBox(username, "I just slid in. Say hi!");
                        break;
                    default:
                        AddMessageBox(username, content);
                        break;
                }
            }
        }

        delegate void AddMessageBoxCallback(string username, string content);
        public void AddMessageBox(string username, string content)
        {
            if(InvokeRequired)
            {
                AddMessageBoxCallback d = new AddMessageBoxCallback(AddMessageBox);
                Invoke(d, new object[] { username, content });
            }
            else
            {
                MessageBox messageEntry = new MessageBox
                {
                    Username = username,
                    Content = content,
                    LabelMaximumSize = new Size(messageContainer.Width - 100, 0)
                };

                messageContainer.SizeChanged += (sender, e) => { messageEntry.LabelMaximumSize = new Size(messageContainer.Width - 100, 0); };

                messageContainer.Controls.Add(messageEntry);
            }
        }

        delegate void ScrollToBottomCallback();
        public void ScrollToBottom()
        {
            if (InvokeRequired)
            {
                ScrollToBottomCallback d = new ScrollToBottomCallback(ScrollToBottom);
                Invoke(d);
            }
            else
            {
                if (messageContainer.Controls.Count > 0) messageContainer.ScrollControlIntoView(messageContainer.Controls[messageContainer.Controls.Count - 1]);
            }
        }
        #endregion

        #region Sending Messages to Server

        private void messageSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control && !e.Alt)
            {
                e.SuppressKeyPress = true;
                if (currentChatId != "") SendMessage();
            }
        }

        private void SendMessage()
        {
            string message = messageSend.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var postData = new
                    {
                        content = message
                    };
                    string jsonPostData = JsonConvert.SerializeObject(postData);

                    using (var client = new WebClient())
                    {
                        byte[] byteArray = Encoding.UTF8.GetBytes(jsonPostData);
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Authorization] = AccessToken;
                        byte[] responseArray = client.UploadData($"{DiscordApiBaseUrl}channels/{currentChatId}/messages", "POST", byteArray);

                        string response = Encoding.UTF8.GetString(responseArray);
                    }

                    messageSend.Clear();
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to send message", ex);
                }
            }
        }

        #endregion

        #endregion

        #region Networking

        private dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        #endregion

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
            System.Windows.Forms.MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
