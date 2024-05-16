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
        const string htmlStart = "<!DOCTYPE html><html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} .spoiler {background-color: black; color: black;} .spoiler:hover {background-color: black; color: white;}</style></head><body>";
        string htmlMiddle = "";
        const string htmlEnd = "</body></html>";
        private string AccessToken;
        private long ServerID;
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
        private void AddMessage(string name, string message)
        {
            htmlMiddle += "<br><br><strong>" + name + ": </strong>" + DiscordMDToHtml(message);
            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
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
        private dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        private void LoadMessages(long ChatID)
        {
            try
            {
                dynamic messages = GetApiResponse($"channels/{ChatID}/messages");
                htmlMiddle = "";
                for (int i = messages.Count - 1; i >= 0; i--)
                {
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

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void channelList_DoubleClick(object sender, EventArgs ex)
        {
            if (channelList.SelectedItems[0].Text != null)
            {
                string selectedChannel = channelList.SelectedItems[0].Text;
                long channelID = (long)channelList.SelectedItems[0].Tag;
                if (channelID>=0)
                {
                    channelLabel.Text = selectedChannel;
                    LoadMessages(channelID);
                }
                else MessageBox.Show("Unable to open this channel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(12, 118, 12, 12);
        }
    }
}