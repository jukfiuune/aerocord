namespace Aerocord
{
    partial class Main
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
            this.dmsButton = new Aerocord.ServerEntry();
            this.sideBarFlow = new Aerocord.FlowLayoutPanelNoScrollbars();
            this.channelList = new System.Windows.Forms.FlowLayoutPanel();
            this.messageContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.messageSend = new System.Windows.Forms.TextBox();
            this.currentChannel = new System.Windows.Forms.Label();
            this.currentChannelType = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dmsButton)).BeginInit();
            this.sideBarFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentChannelType)).BeginInit();
            this.SuspendLayout();
            // 
            // dmsButton
            // 
            this.dmsButton.BackColor = System.Drawing.Color.Black;
            this.dmsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmsButton.Image = global::Aerocord.Properties.Resources.DMs;
            this.dmsButton.Location = new System.Drawing.Point(10, 12);
            this.dmsButton.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.dmsButton.Name = "dmsButton";
            this.dmsButton.Size = new System.Drawing.Size(42, 42);
            this.dmsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dmsButton.TabIndex = 9;
            this.dmsButton.TabStop = false;
            // 
            // sideBarFlow
            // 
            this.sideBarFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sideBarFlow.AutoScroll = true;
            this.sideBarFlow.BackColor = System.Drawing.Color.Black;
            this.sideBarFlow.Controls.Add(this.dmsButton);
            this.sideBarFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sideBarFlow.Location = new System.Drawing.Point(0, 0);
            this.sideBarFlow.Margin = new System.Windows.Forms.Padding(0);
            this.sideBarFlow.Name = "sideBarFlow";
            this.sideBarFlow.Padding = new System.Windows.Forms.Padding(10, 6, 14, 6);
            this.sideBarFlow.Size = new System.Drawing.Size(66, 562);
            this.sideBarFlow.TabIndex = 0;
            this.sideBarFlow.WrapContents = false;
            // 
            // channelList
            // 
            this.channelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.channelList.AutoScroll = true;
            this.channelList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.channelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.channelList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.channelList.Location = new System.Drawing.Point(78, 12);
            this.channelList.Name = "channelList";
            this.channelList.Size = new System.Drawing.Size(227, 538);
            this.channelList.TabIndex = 1;
            this.channelList.WrapContents = false;
            // 
            // messageContainer
            // 
            this.messageContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageContainer.AutoScroll = true;
            this.messageContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.messageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.messageContainer.Location = new System.Drawing.Point(311, 37);
            this.messageContainer.Name = "messageContainer";
            this.messageContainer.Size = new System.Drawing.Size(461, 467);
            this.messageContainer.TabIndex = 2;
            this.messageContainer.Visible = false;
            this.messageContainer.WrapContents = false;
            // 
            // messageSend
            // 
            this.messageSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageSend.Location = new System.Drawing.Point(311, 510);
            this.messageSend.Multiline = true;
            this.messageSend.Name = "messageSend";
            this.messageSend.Size = new System.Drawing.Size(461, 40);
            this.messageSend.TabIndex = 0;
            this.messageSend.Visible = false;
            // 
            // currentChannel
            // 
            this.currentChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentChannel.AutoEllipsis = true;
            this.currentChannel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.currentChannel.Location = new System.Drawing.Point(336, 10);
            this.currentChannel.Name = "currentChannel";
            this.currentChannel.Size = new System.Drawing.Size(436, 22);
            this.currentChannel.TabIndex = 0;
            this.currentChannel.Text = "None";
            this.currentChannel.Visible = false;
            // 
            // currentChannelType
            // 
            this.currentChannelType.Image = global::Aerocord.Properties.Resources.Chat;
            this.currentChannelType.Location = new System.Drawing.Point(317, 15);
            this.currentChannelType.Name = "currentChannelType";
            this.currentChannelType.Size = new System.Drawing.Size(16, 16);
            this.currentChannelType.TabIndex = 3;
            this.currentChannelType.TabStop = false;
            this.currentChannelType.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.currentChannelType);
            this.Controls.Add(this.currentChannel);
            this.Controls.Add(this.messageSend);
            this.Controls.Add(this.messageContainer);
            this.Controls.Add(this.channelList);
            this.Controls.Add(this.sideBarFlow);
            this.GlassMargins = new System.Windows.Forms.Padding(66, 0, 0, 0);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dmsButton)).EndInit();
            this.sideBarFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentChannelType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServerEntry dmsButton;
        private FlowLayoutPanelNoScrollbars sideBarFlow;
        private System.Windows.Forms.FlowLayoutPanel channelList;
        private System.Windows.Forms.FlowLayoutPanel messageContainer;
        private System.Windows.Forms.TextBox messageSend;
        private System.Windows.Forms.Label currentChannel;
        private System.Windows.Forms.PictureBox currentChannelType;
    }
}

