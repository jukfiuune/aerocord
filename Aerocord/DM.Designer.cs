namespace Aerocord
{
    partial class DM
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
            this.descriptionLabel = new WindowsFormsAero.ThemeLabel();
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.messageBox = new WindowsFormsAero.TextBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.profilepicturefriend = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).BeginInit();
            this.SuspendLayout();
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionLabel.Location = new System.Drawing.Point(118, 41);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(654, 22);
            this.descriptionLabel.TabIndex = 5;
            this.descriptionLabel.Text = "description";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(118, 12);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(213, 23);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "username";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(118, 326);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(654, 23);
            this.messageBox.TabIndex = 6;
            this.messageBox.KeyDown += this.messageBox_KeyDown;
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
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(12, 249);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(100, 100);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 8;
            this.profilepicture.TabStop = false;
            // 
            // profilepicturefriend
            // 
            this.profilepicturefriend.BackColor = System.Drawing.Color.Black;
            this.profilepicturefriend.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Location = new System.Drawing.Point(12, 12);
            this.profilepicturefriend.Name = "profilepicturefriend";
            this.profilepicturefriend.Size = new System.Drawing.Size(100, 100);
            this.profilepicturefriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicturefriend.TabIndex = 0;
            this.profilepicturefriend.TabStop = false;
            // 
            // DM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.profilepicture);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicturefriend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DM";
            this.ShowIcon = false;
            this.Text = "Aerocord - Chat";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicturefriend;
        private WindowsFormsAero.ThemeLabel descriptionLabel;
        private WindowsFormsAero.ThemeLabel usernameLabel;
        private WindowsFormsAero.TextBox messageBox;
        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.WebBrowser chatBox;
    }
}