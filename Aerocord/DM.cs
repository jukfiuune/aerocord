using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;
using Newtonsoft.Json;

namespace Aerocord
{
    public partial class DM : WindowsFormsAero.AeroForm
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        string htmlStart;
        const string htmlStartLight = "<!DOCTYPE html><html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt; overflow-x: hidden;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} img {width: auto; height: auto; max-width: 60% !important; max-height: 60% !important;} .spoiler {background-color: black; color: black; border-radius: 5px;} .spoiler:hover {background-color: black; color: white; border-radius: 5px;} .ping {background-color: #e6e8fd; color: #5865f3; border-radius: 5px;} .rich {width: 60%; border-style: solid; border-radius: 5px; border-width: 2px; border-color: black; padding: 10px;}</style></head><body>";
        const string htmlStartDark = "<!DOCTYPE html><html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt; overflow-x: hidden;} body {background-color: black;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline; color: white;} img {width: auto; height: auto; max-width: 60% !important; max-height: 60% !important;} .spoiler {background-color: white; color: white; border-radius: 5px;} .spoiler:hover {background-color: white; color: black; border-radius: 5px;} .ping {background-color: #e6e8fd; color: #5865f3; border-radius: 5px;} .rich {width: 60%; border-style: solid; border-radius: 5px; border-width: 2px; border-color: white; padding: 10px;}</style></head><body>";
        string htmlMiddle = "";
        const string htmlEnd = "</body></html>";
        private string AccessToken;
        public long ChatID;
        public long FriendID;
        private string userPFP;
        private string lastMessageAuthor = "";
        private bool DarkMode = false;
        private string RenderMode = "Aero";
        public DM(Main parentForm, long chatid, long friendid, string token, string userpfp, bool darkmode, string rendermode)
        {
            InitializeComponent();
            DarkMode = darkmode;
            RenderMode = rendermode;
            htmlStart = htmlStartLight;
            if (DarkMode) { _ = new DarkModeCS(this); PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 1); htmlStart = htmlStartDark; }
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
            ChatID = chatid;
            FriendID = friendid;
            userPFP = userpfp;
            frame.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(parentForm.userStatus);
            SetFriendInfo();
            LoadMessages();
        }

        private void SetFriendInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse($"users/{FriendID}/profile");
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
                framefriend.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(status);
                if (custom_status == "")
                {
                    switch (status)
                    {
                        case "online":
                            descriptionLabel.Text = "Online";
                            break;
                        case "dnd":
                            descriptionLabel.Text = "Do Not Disturb";
                            break;
                        case "idle":
                            descriptionLabel.Text = "Idle";
                            break;
                        case "offline":
                            descriptionLabel.Text = "Offline";
                            break;
                        default:
                            descriptionLabel.Text = "Online";
                            break;
                    }
                }
                else
                {
                    descriptionLabel.Text = custom_status;
                }
            }
        }

        private void LoadMessages()
        {
            try
            {
                dynamic messages = GetApiResponse($"channels/{ChatID.ToString()}/messages");
                Console.WriteLine(messages);
                htmlMiddle = "";
                for (int i = messages.Count - 1; i >= 0; i--)
                {
                    string author = messages[i].author.global_name;
                    if (author == null) author = messages[i].author.username;
                    string content = messages[i].content;
                    List<WebSocketClient.Attachment> attachmentsFormed = new List<WebSocketClient.Attachment>();
                    List<WebSocketClient.Embed> embedsFormed = new List<WebSocketClient.Embed>();

                    if (messages[i].attachments != null) foreach (var attachment in messages[i].attachments)
                        {
                            attachmentsFormed.Add(new WebSocketClient.Attachment { URL = attachment.url, Type = attachment.content_type });
                            //Console.WriteLine(attachment.url);
                        }
                    if (messages[i].embeds != null) foreach (var embed in messages[i].embeds)
                        {
                            embedsFormed.Add(new WebSocketClient.Embed { Type = embed?.type ?? "", Author = embed?.author?.name ?? "", AuthorURL = embed?.author?.url ?? "", Title = embed?.title ?? "", TitleURL = embed?.url ?? "", Description = embed?.description ?? "" });
                        }
                    switch ((int)messages[i].type.Value)
                    {
                        case 7:
                            // Join message
                            AddMessage(author, "*Say hi!*", "slid in the server", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;

                        case 19:
                            Console.WriteLine("replyyayaya");
                            // Reply
                            bool found = false;
                            foreach (var message in messages)
                            {
                                if (message.id == messages[i].message_reference.message_id)
                                {
                                    string replyAuthor = message.author.global_name;
                                    if (replyAuthor == null) replyAuthor = message.author.username;
                                    AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, replyAuthor, message.content.Value);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, " ", "Unable to load message");
                            break;

                        default:
                            //Normal text or unimplemented
                            AddMessage(author, content, "said", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;
                    }
                }
                chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
                Thread.Sleep(200);
                ScrollToBottom();
                return;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve messages", ex);
            }
        }
        public void AddMessage(string name, string message, string action, WebSocketClient.Attachment[] attachments, WebSocketClient.Embed[] embeds, bool reload = true, bool scroll = true, string replyname = "", string replymessage = "")
        {
            if (name == lastMessageAuthor && action == "said")
            {
                htmlMiddle += "<br><p>" + DiscordMDToHtml(message) + "</p>";
            }
            else if (action == "replied")
            {
                Console.WriteLine("ligmaballzsd");
                htmlMiddle += "<br><em style=\"color: darkgray\">┌ @" + replyname + ": " + DiscordMDToHtml(replymessage) + "</em><br><strong>" + name + " " + action + ":</strong><br><p>" + DiscordMDToHtml(message) + "</p>";
            }
            else
            {
                htmlMiddle += "<br><strong>" + name + " " + action + ":</strong><br><p>" + DiscordMDToHtml(message) + "</p>";
            }
            lastMessageAuthor = name;
            if (attachments.Length > 0) foreach (var attachment in attachments)
                {
                    chatBox.ScriptErrorsSuppressed = true;
                    if (attachment.Type.Contains("image")) htmlMiddle += "<br><img src=\"" + attachment.URL + "\"></img>";
                    if (attachment.Type.Contains("video")) htmlMiddle += "<br><embed src=\"" + attachment.URL + "\" type=\"" + attachment.Type + "\" width=\"60%\" height=\"60%\">";
                }
            if (embeds.Length > 0) foreach (var embed in embeds)
                {
                    if (embed.Type == "rich") htmlMiddle += "<br><div class=\"rich\"><a style=\"color: black\" href=\"" + embed.AuthorURL + "\">" + embed.Author + "</a><br><br><a href=\"" + embed.TitleURL + "\">" + embed.Title + "</a><br><br><p>" + embed.Description + "</p></div>";
                }
            if (reload) chatBox.DocumentText = (htmlStart + htmlMiddle + htmlEnd).ToString();
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
                            if (md[i].ToString() == ">")
                            {
                                ping.Append(GetUsernameById(uid.ToString()).ToCharArray());
                                if (!waitingToClose.Contains("||")) ping.Append("</span>".ToCharArray());
                                html.Append(ping);
                            }
                            GC.Collect();
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
            GC.Collect();
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
        private void ScrollToBottom()
        {
            Application.DoEvents();
            chatBox.Navigate("javascript:window.scroll(0,document.body.scrollHeight);");
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(117, 325, 12, 12);
            if (DarkMode)
            {
                GlassMargins = new Padding(-1, -1, -1, -1);
            }

            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            ScrollToBottom();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            GC.Collect();
        }
    }
}