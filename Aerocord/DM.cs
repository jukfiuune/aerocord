﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class DM : WindowsFormsAero.AeroForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClientDM websocketClient;
        const string htmlStart = "<!DOCTYPE html><html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} .spoiler {background-color: black; color: black;} .spoiler:hover {background-color: black; color: white;}</style></head><body>";
        string htmlMiddle = "";
        const string htmlEnd = "</body></html>";
        private string AccessToken;
        public long ChatID;
        private long FriendID;
        private string userPFP;
        public DM(long chatid, long friendid, string token, string userpfp)
        {
            InitializeComponent();
            AccessToken = token;
            ChatID = chatid;
            FriendID = friendid;
            userPFP = userpfp;
            SetFriendInfo();
            LoadMessages();
            websocketClient = new WebSocketClientDM(AccessToken, this);
        }

        private void SetFriendInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse($"users/{FriendID}/profile");
                //Console.WriteLine(userProfile);
                string displayname;
                if (userProfile.user.global_name != null) { displayname = userProfile.user.global_name; } else { displayname = userProfile.user.username; }
                string bio = userProfile.user.bio;
                usernameLabel.Text = displayname;
                descriptionLabel.Text = bio;
                profilepicturefriend.ImageLocation = $"https://cdn.discordapp.com/avatars/{userProfile.user.id}/{userProfile.user.avatar}.png";
                profilepicture.ImageLocation = userPFP;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        private void LoadMessages()
        {
            try
            {
                dynamic messages = GetApiResponse($"channels/{ChatID}/messages");
                for (int i = messages.Count-1; i >= 0; i--) { 
                    string author = messages[i].author.username;
                    string content = messages[i].content;
                    AddMessage(author, content);
                }
                return;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve messages", ex);
            }
        }
        public void AddMessage(string name, string message)
        {
            htmlMiddle += "<br><br><strong>" + name + ": </strong>" + DiscordMDToHtml(message);
            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            ScrollToBottom();
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
                            if (!waitingToClose.Contains("**")) { html.Append(new char[] { '<', 's', 't', 'r', 'o', 'n', 'g', '>' }); waitingToClose.Add("**"); } else { html.Append(new char[] { '<', '/', 's', 't', 'r', 'o', 'n', 'g', '>' }); waitingToClose.Remove("**"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("*")) { html.Append(new char[] { '<', 'e', 'm', '>' }); waitingToClose.Add("*"); } else { html.Append(new char[] { '<', '/', 'e', 'm', '>' }); waitingToClose.Remove("*"); }
                        break;

                    case "_":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "_")
                        {
                            if (!waitingToClose.Contains("__")) { html.Append(new char[] { '<', 'u', '>' }); waitingToClose.Add("__"); } else { html.Append(new char[] { '<', '/', 'u', '>' }); waitingToClose.Remove("__"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("_")) { html.Append(new char[] { '<', 'e', 'm', '>' }); waitingToClose.Add("_"); } else { html.Append(new char[] { '<', '/', 'e', 'm', '>' }); waitingToClose.Remove("_"); }
                        break;

                    case "~":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "~") { if (!waitingToClose.Contains("~~")) { html.Append(new char[] { '<', 's', 't', 'r', 'i', 'k', 'e', '>' }); waitingToClose.Add("~~"); } else { html.Append(new char[] { '<', '/', 's', 't', 'r', 'i', 'k', 'e', '>' }); waitingToClose.Remove("~~"); } i += 1; break; }
                        break;

                    case "#":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "#")
                        {
                            if (md.Length > i + 2 && md[i + 2].ToString() == "#")
                            {
                                if (!waitingToClose.Contains("###")) { html.Append(new char[] { '<', 'h', '3', '>' }); waitingToClose.Add("###"); }
                                i += 2;
                                break;
                            }
                            if (!waitingToClose.Contains("##")) { html.Append(new char[] { '<', 'h', '2', '>' }); waitingToClose.Add("##"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("#")) { html.Append(new char[] { '<', 'h', '1', '>' }); waitingToClose.Add("#"); }
                        break;

                    case "|":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "|")
                        {
                            if (!waitingToClose.Contains("||")) { html.Append(new char[] { '<', 's', 'p', 'a', 'n', ' ', 'c', 'l', 'a', 's', 's', '=', '"', 's', 'p', 'o', 'i', 'l', 'e', 'r', '"', '>' }); waitingToClose.Add("||"); } else { html.Append(new char[] { '<', '/', 's', 'p', 'a', 'n', '>' }); waitingToClose.Remove("||"); }
                            i += 1;
                            break;
                        }
                        break;

                    case "\n":
                        if (waitingToClose.Contains("###")) { html.Append(new char[] { '<', '/', 'h', '3', '>' }); waitingToClose.Remove("###"); }
                        if (waitingToClose.Contains("##")) { html.Append(new char[] { '<', '/', 'h', '2', '>' }); waitingToClose.Remove("##"); }
                        if (waitingToClose.Contains("#")) { html.Append(new char[] { '<', '/', 'h', '1', '>' }); waitingToClose.Remove("#"); }
                        html.Append(new char[] { '<', 'b', 'r', '>' });
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
                        html.Append(new char[] { '<', '/', 'e', 'm', '>' }); waitingToClose.Remove("*");
                        break;

                    case "**":
                        html.Append(new char[] { '<', '/', 's', 't', 'r', 'o', 'n', 'g', '>' }); waitingToClose.Remove("**");
                        break;

                    case "__":
                        html.Append(new char[] { '<', '/', 'u', '>' }); waitingToClose.Remove("__");
                        break;

                    case "~~":
                        html.Append(new char[] { '<', '/', 's', 't', 'r', 'i', 'k', 'e', '>' }); waitingToClose.Remove("~~");
                        break;

                    case "||":
                        html.Append(new char[] { '<', '/', 's', 'p', 'a', 'n', '>' }); waitingToClose.Remove("||");
                        break;

                    case "###":
                        html.Append(new char[] { '<', '/', 'h', '3', '>' }); waitingToClose.Remove("###");
                        break;

                    case "##":
                        html.Append(new char[] { '<', '/', 'h', '2', '>' }); waitingToClose.Remove("##");
                        break;

                    case "#":
                        html.Append(new char[] { '<', '/', 'h', '1', '>' }); waitingToClose.Remove("#");
                        break;
                }
            }
            return html.ToString();
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
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
                    string postData = $"{{\"content\": \"{message}\"}}";

                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Authorization] = AccessToken;
                        string response = client.UploadString($"{DiscordApiBaseUrl}channels/{ChatID}/messages", "POST", postData);
                    }

                    messageBox.Clear();
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to send message", ex);
                }
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

        public void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ScrollToBottom()
        {
            Application.DoEvents();
            try
            {
                if (chatBox.Document != null)
                {
                    chatBox.Document.Body.ScrollIntoView(false);
                }
            }
            catch (Exception e)
            {
                chatBox.Navigate("javascript:window.scroll(0,document.body.scrollHeight);");
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(117, 325, 12, 12);
            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            ScrollToBottom();
        }
    }
}