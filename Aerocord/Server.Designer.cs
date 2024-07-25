namespace Aerocord
{
    partial class Server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.channelLabel = new WindowsFormsAero.ThemeLabel();
            this.servernameLabel = new WindowsFormsAero.ThemeLabel();
            this.messageBox = new WindowsFormsAero.TextBox();
            this.serverPicture = new System.Windows.Forms.PictureBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.channelList = new WindowsFormsAero.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.serverPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // channelLabel
            // 
            this.channelLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.channelLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.channelLabel.Location = new System.Drawing.Point(118, 41);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(654, 22);
            this.channelLabel.TabIndex = 5;
            // 
            // servernameLabel
            // 
            this.servernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.servernameLabel.Location = new System.Drawing.Point(118, 12);
            this.servernameLabel.Name = "servernameLabel";
            this.servernameLabel.Size = new System.Drawing.Size(213, 23);
            this.servernameLabel.TabIndex = 4;
            this.servernameLabel.Text = "servername";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(118, 326);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(654, 23);
            this.messageBox.TabIndex = 6;
            // 
            // serverPicture
            // 
            this.serverPicture.BackColor = System.Drawing.Color.Black;
            this.serverPicture.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.serverPicture.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.serverPicture.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.serverPicture.Location = new System.Drawing.Point(12, 12);
            this.serverPicture.Name = "serverPicture";
            this.serverPicture.Size = new System.Drawing.Size(100, 100);
            this.serverPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.serverPicture.TabIndex = 0;
            this.serverPicture.TabStop = false;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(119, 70);
            this.chatBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScriptErrorsSuppressed = true;
            this.chatBox.Size = new System.Drawing.Size(653, 250);
            this.chatBox.TabIndex = 9;
            this.chatBox.Url = new System.Uri("", System.UriKind.Relative);
            this.chatBox.WebBrowserShortcutsEnabled = false;
            // 
            // channelList
            // 
            this.channelList.HideSelection = false;
            this.channelList.Location = new System.Drawing.Point(13, 119);
            this.channelList.Name = "channelList";
            this.channelList.Size = new System.Drawing.Size(100, 230);
            this.channelList.TabIndex = 10;
            this.channelList.UseCompatibleStateImageBehavior = false;
            this.channelList.View = System.Windows.Forms.View.Tile;
            this.channelList.DoubleClick += new System.EventHandler(this.channelList_DoubleClick);
            // 
            // Server
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.channelList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.servernameLabel);
            this.Controls.Add(this.serverPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Server";
            this.ShowIcon = false;
            this.Text = "Aerocord - Chat";
            ((System.ComponentModel.ISupportInitialize)(this.serverPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox serverPicture;
        private WindowsFormsAero.ThemeLabel channelLabel;
        private WindowsFormsAero.ThemeLabel servernameLabel;
        private WindowsFormsAero.TextBox messageBox;
        private System.Windows.Forms.WebBrowser chatBox;
        private WindowsFormsAero.ListView channelList;
    }
}