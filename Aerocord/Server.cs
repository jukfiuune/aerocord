﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAero;
using WindowsFormsAero.TaskDialog;

namespace Aerocord
{
    public partial class Server : WindowsFormsAero.AeroForm
    {
        const string htmlStart = "<html><head><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;}</style></head><body>";
        string htmlMiddle = "";
        const string htmlEnd = "</body></html>";
        public Server()
        {
            InitializeComponent();
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
                        html.Append(new char[] { '<', '/', 's', 't', 'r', 'o', 'n', 'g', '>' });
                        break;

                    case "__":
                        html.Append(new char[] { '<', '/', 'u', '>' }); waitingToClose.Remove("__");
                        break;

                    case "~~":
                        html.Append(new char[] { '<', '/', 's', 't', 'r', 'i', 'k', 'e', '>' });
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
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(12, 118, 12, 12);
            AddMessage("Shamestick", "This is a certified __*shampoo*__ classic! Stay tuned for ***AMAZING SHANE MOMENTS***\n\n# NIGGAYOUBEINGLIGMAHAHA");
            AddMessage("JukFiuuFuck", "This is a certified __*shampoo*__ classic! Stay tuned for ***AMAZING SHANE MOMENTS***\n\n#NIGGAYOUBEINGLIGMAHAHA");
        }
    }
}