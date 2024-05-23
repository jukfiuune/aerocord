﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class Server : WindowsFormsAero.AeroForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClientServer websocketClient;
        const string htmlStart = "<!DOCTYPE html><html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} .spoiler {background-color: black; color: black; border-radius: 5px;} .spoiler:hover {background-color: black; color: white; border-radius: 5px;} .ping {background-color: #e6e8fd; color: #5865f3; border-radius: 5px;} img {max-width: 60%; max-height: 60%;}</style></head><body>";
        string htmlMiddle = "";
        const string htmlEnd = "</body></html>";
        private string AccessToken;
        public long ServerID;
        public long ChatID;
        public Server(long serverid, String token)
        {
            InitializeComponent();
            AccessToken = token;
            ServerID = serverid;
            Thread.Sleep(1000);
            PopulateFields();
        }
        private void PopulateFields()
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                foreach (var guild in guilds)
                {
                    if ((long)guild.id == ServerID)
                    {
                        servernameLabel.Text = guild.name.ToString();
                        serverPicture.ImageLocation = $"https://cdn.discordapp.com/icons/{guild.id}/{guild.icon}.png";
                    }
                }
                Thread.Sleep(500);
                dynamic channels = GetApiResponse($"guilds/{ServerID}/channels");
                List<ListViewGroup> categoryNames = new List<ListViewGroup>();
                List<ListViewItem> channelNames = new List<ListViewItem>();
                foreach (var category in channels)
                {
                    if (category.type == 4)
                    {
                        string categoryName = category.name.ToString().ToUpper();
                        ListViewGroup categoryItem = new ListViewGroup(categoryName);
                        categoryItem.Tag = (long)category.id;
                        categoryNames.Add(categoryItem);
                    }
                }
                foreach (var channel in channels)
                {
                    if (channel.type == 0)
                    {
                        string channelName = channel.name.ToString();
                        ListViewItem channelItem = new ListViewItem("#" + channelName.ToLower());
                        for(int i = 0; i < categoryNames.Count; i++)
                        {
                            if (channel.parent_id != null && (long)categoryNames[i].Tag == (long)channel.parent_id)
                            {
                                channelItem.Group = categoryNames[i];
                            }
                        }
                        channelItem.Tag = (long)channel.id;
                        channelNames.Add(channelItem);
                    }
                }
                channelList.Groups.AddRange(categoryNames.ToArray());
                channelList.Items.AddRange(channelNames.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }
        public void AddMessage(string name, string message, WebSocketClientServer.Attachment[] attachments, bool scroll = true)
        {
            htmlMiddle += "<br><strong>" + name + ": </strong><p>" + DiscordMDToHtml(message) + "</p>";
            if (attachments.Length > 0) foreach (var attachment in attachments)
                {
                    if (attachment.Type.Contains("image")) htmlMiddle += "<br><img src=\"" + attachment.URL + "\"></img>";
                }
            chatBox.DocumentText = (htmlStart + htmlMiddle + htmlEnd).ToString();
            if (scroll) Thread.Sleep(100); ScrollToBottom();
        }

        private string DiscordMDToHtml(string md)
        {
            List<string> waitingToClose = new List<string>();
            StringBuilder html = new StringBuilder();
            for (int i = 0; i < md.Length; i++)
            {
                switch (md[i].ToString())
                {
                    case "*":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "*")
                        {
                            if (!waitingToClose.Contains("**")) { html.Append("<strong>".ToCharArray()); waitingToClose.Add("**"); } else { html.Append("</strong>".ToCharArray()); waitingToClose.Remove("**"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("*")) { html.Append("<em>".ToCharArray()); waitingToClose.Add("*"); } else { html.Append("</em>".ToCharArray()); waitingToClose.Remove("*"); }
                        break;

                    case "_":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "_")
                        {
                            if (!waitingToClose.Contains("__")) { html.Append("<u>".ToCharArray()); waitingToClose.Add("__"); } else { html.Append("</u>".ToCharArray()); waitingToClose.Remove("__"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("_")) { html.Append("<em>".ToCharArray()); waitingToClose.Add("_"); } else { html.Append("</em>".ToCharArray()); waitingToClose.Remove("_"); }
                        break;

                    case "~":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "~") { if (!waitingToClose.Contains("~~")) { html.Append("<strike>".ToCharArray()); waitingToClose.Add("~~"); } else { html.Append("</strike>".ToCharArray()); waitingToClose.Remove("~~"); } i += 1; break; }
                        break;

                    case "#":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "#")
                        {
                            if (md.Length > i + 2 && md[i + 2].ToString() == "#")
                            {
                                if (!waitingToClose.Contains("###")) { html.Append("<h3>".ToCharArray()); waitingToClose.Add("###"); }
                                i += 2;
                                break;
                            }
                            if (!waitingToClose.Contains("##")) { html.Append("<h2>".ToCharArray()); waitingToClose.Add("##"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("#")) { html.Append("<h1>".ToCharArray()); waitingToClose.Add("#"); }
                        break;

                    case "|":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "|")
                        {
                            if (!waitingToClose.Contains("||")) { html.Append("<span class=\"spoiler\">".ToCharArray()); waitingToClose.Add("||"); } else { html.Append("</span>".ToCharArray()); waitingToClose.Remove("||"); }
                            i += 1;
                            break;
                        }
                        break;

                    case "<":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "@")
                        {
                            StringBuilder ping = new StringBuilder();
                            if (!waitingToClose.Contains("||")) ping.Append("<span class=\"ping\">".ToCharArray());
                            ping.Append('@');
                            i += 2;
                            StringBuilder uid = new StringBuilder();
                            while (Char.IsNumber(md[i])) { /*Console.WriteLine(md[i]);*/ uid.Append(md[i]); i += 1; }
                            i += 1;
                            if (md[i].ToString() == ">")
                            {
                                ping.Append(GetUsernameById(uid.ToString()).ToCharArray());
                                if (!waitingToClose.Contains("||")) html.Append("</span>".ToCharArray());
                                html.Append(ping);
                            }
                            break;
                        }
                        break;

                    case "\n":
                        if (waitingToClose.Contains("###")) { html.Append("</h3>".ToCharArray()); waitingToClose.Remove("###"); }
                        if (waitingToClose.Contains("##")) { html.Append("</h2>".ToCharArray()); waitingToClose.Remove("##"); }
                        if (waitingToClose.Contains("#")) { html.Append("</h1>".ToCharArray()); waitingToClose.Remove("#"); }
                        html.Append("<br>".ToCharArray());
                        break;

                    default:
                        html.Append(md[i]);
                        break;
                }
            }
            for (int i = 0; i < waitingToClose.Count; i++)
            {
                switch (waitingToClose[i])
                {
                    case "*":
                    case "_":
                        html.Append("</em>".ToCharArray()); waitingToClose.Remove("*");
                        break;

                    case "**":
                        html.Append("</strong>".ToCharArray()); waitingToClose.Remove("**");
                        break;

                    case "__":
                        html.Append("</u>".ToCharArray()); waitingToClose.Remove("__");
                        break;

                    case "~~":
                        html.Append("</strike>".ToCharArray()); waitingToClose.Remove("~~");
                        break;

                    case "||":
                        html.Append("</span>".ToCharArray()); waitingToClose.Remove("||");
                        break;

                    case "###":
                        html.Append("</h3>".ToCharArray()); waitingToClose.Remove("###");
                        break;

                    case "##":
                        html.Append("</h2>".ToCharArray()); waitingToClose.Remove("##");
                        break;

                    case "#":
                        html.Append("</h1>".ToCharArray()); waitingToClose.Remove("#");
                        break;
                }
            }
            return html.ToString();
        }

        public string GetUsernameById(string userId)
        {
            try
            {
                dynamic user = GetApiResponse($"users/{userId}");
                if (user.global_name != null) return user.global_name;
                return user.username;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get username for user ID {userId}: {ex.Message}");
                return "Unknown";
            }
        }

        public dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        private void LoadMessages(long channelID)
        {
            try
            {
                dynamic messages = GetApiResponse($"channels/{channelID.ToString()}/messages");
                htmlMiddle = "";
                for (int i = messages.Count - 1; i >= 0; i--)
                {
                    string author = messages[i].author.global_name;
                    if (author == null) author = messages[i].author.username;
                    string content = messages[i].content;
                    List<WebSocketClientServer.Attachment> attachmentsFormed = new List<WebSocketClientServer.Attachment>();

                    foreach (var attachment in messages[i].attachments)
                    {
                        attachmentsFormed.Add(new WebSocketClientServer.Attachment { URL = attachment.url, Type = attachment.content_type });
                        //Console.WriteLine(attachment.url);
                    }
                    AddMessage(author, content, attachmentsFormed.ToArray(), false);
                }
                Thread.Sleep(200);
                ScrollToBottom();
                return;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve messages", ex);
            }
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("Ligma");
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string message = messageBox.Text.Trim();
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
                        byte[] responseArray = client.UploadData($"{DiscordApiBaseUrl}channels/{ChatID}/messages", "POST", byteArray);

                        string response = Encoding.UTF8.GetString(responseArray);
                    }

                    messageBox.Clear();
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to send message", ex);
                }
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void channelList_DoubleClick(object sender, EventArgs ex)
        {
            if (channelList.SelectedItems[0].Text != null)
            {
                if (websocketClient != null) websocketClient.CloseWebSocket();
                string selectedChannel = channelList.SelectedItems[0].Text;
                long channelID = (long)channelList.SelectedItems[0].Tag;
                if (channelID>=0)
                {
                    channelLabel.Text = selectedChannel;
                    ChatID = channelID;
                    LoadMessages(channelID);
                    websocketClient = new WebSocketClientServer(AccessToken, this);
                }
                else MessageBox.Show("Unable to open this channel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScrollToBottom()
        {
            Application.DoEvents();
            chatBox.Navigate("javascript:window.scroll(0,document.body.scrollHeight);");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(12, 118, 12, 12);
            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            ScrollToBottom();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (websocketClient != null) websocketClient.CloseWebSocket();
        }
    }
}