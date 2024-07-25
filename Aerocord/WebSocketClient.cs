using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;
using WebSocketSharp;
using System.Security.Authentication;

namespace Aerocord
{
    class WebSocketClient
    {
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
                            HandleReadyEvent(json["d"]);
                            break;
                    }
                    break;
                default:
                    // handle other op codes when needed ig
                    break;
            }
        }

        /*private void HandleTypingStartEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            if (channelId == parentForm.CurrentChannelId)
            {
                string userId = (string)jToken["user_id"];
                string username = GetUsernameById(userId);
                string message = $"{username} is typing...";

                parentForm.Invoke((MethodInvoker)(() =>
                {
                    parentForm.typingStatus.Text = message;
                }));
            }
        }

        private void HandleTypingStopEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            if (channelId == parentForm.CurrentChannelId)
            {
                string message = "";

                parentForm.Invoke((MethodInvoker)(() =>
                {
                    parentForm.typingStatus.Text = message;
                }));
            }
        }*/

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
            string customStatus = eventData["user_settings"]["custom_status"] ?? "";
            dynamic friendData = eventData["relationships"];
            dynamic serverData = eventData["guilds"];

            parentForm.friends = friendData;
            parentForm.guilds = serverData;
            parentForm.PopulateFriendsTab();
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
