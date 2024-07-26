﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;
using WebSocketSharp;
using System.Security.Authentication;

namespace Aerocord
{
    public class WebSocketClient
    {
        private bool init = false;

        private Main parentForm;
        private WebSocket webSocket;
        private string accessToken;
        private const SslProtocols Tls12 = (SslProtocols)0x00000C00;
        bool tryingRandomStuffAtThisPoint = false;

        public WebSocketClient(string accessToken, Main parentForm)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //Console.WriteLine($"Using Access Token: {accessToken}");

            this.accessToken = accessToken;
            this.parentForm = parentForm;
            InitializeWebSocket();
        }

        private void InitializeWebSocket()
        {
            webSocket = new WebSocket($"wss://gateway.discord.gg/?v=9&encoding=json");
            webSocket.SslConfiguration.EnabledSslProtocols = Tls12;
            webSocket.OnMessage += (sender, e) => HandleWebSocketMessage(e.Data);
            webSocket.OnError += (sender, e) => HandleWebSocketError(e.Message);
            webSocket.OnClose += (sender, e) => HandleWebSocketClose();
            webSocket.Connect();
            SendIdentifyPayload();
        }

        private void SendIdentifyPayload()
        {
            if (webSocket.ReadyState == WebSocketState.Open)
            {
                var identifyPayload = new
                {
                    op = 2,
                    d = new
                    {
                        token = accessToken,
                        properties = new
                        {
                            os = "windows",
                            browser = "chrome",
                            device = "pc"
                        }
                    }
                };

                try
                {
                    webSocket.Send(Newtonsoft.Json.JsonConvert.SerializeObject(identifyPayload));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending identify payload: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("WebSocket connection is not open. Unable to send identify payload.");
            }
        }

        private void HandleWebSocketMessage(string data)
        {
            Console.WriteLine($"WebSocket Received: {data}");

            var json = JObject.Parse(data);
            int opCode = (int)json["op"];

            switch (opCode)
            {
                case 0:
                    string eventType = (string)json["t"];
                    switch (eventType)
                    {
                        case "READY":
                            if(!init) HandleReadyEvent(json["d"]);
                            break;
                        case "MESSAGE_CREATE":
                            HandleMessageCreateEvent(json["d"]);
                            break;
                        case "PRESENCE_UPDATE":
                            HandlePresenceUpdateEvent(json["d"]);
                            break;
                    }
                    break;
                default:
                    // handle other op codes when needed ig
                    break;
            }
        }

        public class Attachment
        {
            public string URL { get; set; }
            public string Type { get; set; }
        }

        public class Embed
        {
            public string Type { get; set; }
            public string Author { get; set; }
            public string AuthorURL { get; set; }
            public string Title { get; set; }
            public string TitleURL { get; set; }
            public string Description { get; set; }
        }

        private void HandleReadyEvent(JToken data)
        {
            dynamic eventData = data;
            string status = eventData["user_settings"]["status"];
            string customStatus = eventData["user_settings"]["custom_status"]["text"] ?? "";
            dynamic friendData = eventData["relationships"];
            dynamic serverData = eventData["guilds"];

            if(customStatus == "") switch(status)
                {
                    case "online":
                        customStatus = "Online";
                        break;
                    case "dnd":
                        customStatus = "Do Not Disturb";
                        break;
                    case "idle":
                        customStatus = "Idle";
                        break;
                    case "offline":
                        customStatus = "Offline";
                        break;
                    default:
                        customStatus = "Online";
                        break;
                }

            parentForm.friends = friendData;
            parentForm.guilds = serverData;
            parentForm.statusLabel.Text = customStatus;
            parentForm.userStatus = status;
            foreach (var presence in eventData["presences"]) {
                if (!parentForm.friendStatuses.ContainsKey(long.Parse((string)presence["user"]["id"]))) parentForm.friendStatuses.Add(long.Parse((string)presence["user"]["id"]), (string)presence["status"]);
                foreach(var activity in presence["activities"]) if (activity["type"] == 4 && activity["id"] == "custom") { if (!parentForm.friendCustomStatuses.ContainsKey(long.Parse((string)eventData["user"]["id"]))) parentForm.friendCustomStatuses.Add(long.Parse((string)eventData["user"]["id"]), (string)activity["state"]); break; }
            }
            parentForm.PopulateFriendsTab();
            init = true;
        }

        private void HandleMessageCreateEvent(JToken data)
        {
            dynamic eventData = data;
            dynamic attachmentData = eventData["attachments"];
            dynamic embedData = eventData["embeds"];
            string channelId = eventData["channel_id"];
            string author = eventData["author"]["global_name"];
            if (eventData["author"]["global_name"] == null) author = eventData["author"]["username"];
            string content = eventData["content"];
            List<Attachment> attachmentsFormed = new List<Attachment>();
            List<Embed> embedsFormed = new List<Embed>();

            if (parentForm.listDMs.ContainsKey(long.Parse(channelId)))
            {
                DM parentDMForm = parentForm.listDMs[long.Parse(channelId)];
                    if (attachmentData != null)
                    {
                        foreach (var attachment in attachmentData)
                        {
                            attachmentsFormed.Add(new Attachment { URL = attachment.url, Type = attachment.content_type });
                        }
                    }

                    if (embedData != null)
                    {
                        foreach (var embed in embedData)
                        {
                            embedsFormed.Add(new Embed { Type = embed?.type ?? "", Author = embed?.author?.name ?? "", AuthorURL = embed?.author?.url ?? "", Title = embed?.title ?? "", TitleURL = embed?.url ?? "", Description = embed?.description ?? "" });
                        }
                    }
                    switch ((int)eventData["type"].Value)
                    {
                        case 7:
                            // Join message
                            parentDMForm.AddMessage(author, "*Say hi!*", "slid in the server", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true);
                            break;

                        case 19:
                            // Reply
                            bool found = false;
                            foreach (var message in parentDMForm.GetApiResponse($"channels/{parentDMForm.ChatID.ToString()}/messages"))
                            {
                                if (message.id == eventData["message_reference"]["message_id"])
                                {
                                    string replyAuthor = message.author.global_name;
                                    if (replyAuthor == null) replyAuthor = message.author.username;
                                    parentDMForm.AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true, replyAuthor, message.content.Value);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) parentDMForm.AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true, " ", "Unable to load message");
                            break;

                        default:
                            //Normal text or unimplemented
                            parentDMForm.AddMessage(author, content, "said", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true);
                            break;
                    }
            }
            else if (parentForm.listServers.ContainsKey(long.Parse(channelId)))
            {
                if (attachmentData != null)
                {
                    foreach (var attachment in attachmentData)
                    {
                        attachmentsFormed.Add(new Attachment { URL = attachment.url, Type = attachment.content_type });
                    }
                }

                if (embedData != null)
                {
                    foreach (var embed in embedData)
                    {
                        embedsFormed.Add(new Embed { Type = embed?.type ?? "", Author = embed?.author?.name ?? "", AuthorURL = embed?.author?.url ?? "", Title = embed?.title ?? "", TitleURL = embed?.url ?? "", Description = embed?.description ?? "" });
                    }
                }
                Server parentServerForm = parentForm.listServers[long.Parse(channelId)];
                    switch ((int)eventData["type"].Value)
                    {
                        case 7:
                            // Join message
                            parentServerForm.AddMessage(author, "*Say hi!*", "slid in the server", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true);
                            break;

                        case 19:
                            // Reply
                            bool found = false;
                            foreach (var message in parentServerForm.GetApiResponse($"channels/{parentServerForm.ChatID.ToString()}/messages"))
                            {
                                if (message.id == eventData["message_reference"]["message_id"])
                                {
                                    string replyAuthor = message.author.global_name;
                                    if (replyAuthor == null) replyAuthor = message.author.username;
                                    parentServerForm.AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true, replyAuthor, message.content.Value);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) parentServerForm.AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true, " ", "Unable to load message");
                            break;

                        default:
                            //Normal text or unimplemented
                            parentServerForm.AddMessage(author, content, "said", attachmentsFormed.ToArray(), embedsFormed.ToArray(), true, true);
                            break;
                    }
            }
        }

        private void HandlePresenceUpdateEvent(JToken data)
        {
            dynamic eventData = data;
            string status = eventData["status"];
            string custom_status = "";
            foreach (var activity in eventData["activities"]) if(activity["type"] == 4 && activity["id"] == "custom") { custom_status = activity["state"] ?? ""; break; }
            if (parentForm.friendStatuses.ContainsKey(long.Parse((string)eventData["user"]["id"]))) parentForm.friendStatuses[long.Parse((string)eventData["user"]["id"])] = status;
            if (parentForm.friendCustomStatuses.ContainsKey(long.Parse((string)eventData["user"]["id"]))) parentForm.friendCustomStatuses[long.Parse((string)eventData["user"]["id"])] = custom_status; else parentForm.friendCustomStatuses.Add(long.Parse((string)eventData["user"]["id"]), custom_status);
            foreach (DM dm in parentForm.listDMs.Values) if (dm.FriendID == long.Parse((string)eventData["user"]["id"])) dm.ChangeStatus(status, custom_status);
            
        }

        private void HandleWebSocketError(string errorMessage)
        {
            parentForm.Invoke((MethodInvoker)(() =>
            {
                //Console.WriteLine($"WebSocket Error: {errorMessage}");
                // really shitty code on getting the websocket back but works fine, will be patched soon
                InitializeWebSocket();
            }));
        }

        private void HandleWebSocketClose()
        {
            if (!tryingRandomStuffAtThisPoint) try
                {
                    parentForm.Invoke((MethodInvoker)(() =>
                    {
                        //Console.WriteLine("WebSocket connection closed.");
                        // really shitty code on getting the websocket back but works fine, will be patched soon
                        InitializeWebSocket();
                    }));
                }
                catch { }
        }

        public void CloseWebSocket()
        {
            tryingRandomStuffAtThisPoint = true;
            webSocket.Close();
            GC.Collect();
        }
    }
}
