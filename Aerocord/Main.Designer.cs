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
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.statusLabel = new WindowsFormsAero.ThemeLabel();
            this.friendserverPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.friendsButton = new WindowsFormsAero.Button();
            this.button2 = new WindowsFormsAero.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(75, 14);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(213, 23);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI Variable Text Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusLabel.Location = new System.Drawing.Point(75, 37);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(258, 21);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "Set a custom status";
            this.statusLabel.Click += new System.EventHandler(this.descriptionLabel_Click);
            // 
            // friendserverPanel
            // 
            this.friendserverPanel.AutoScroll = true;
            this.friendserverPanel.BackColor = System.Drawing.Color.Black;
            this.friendserverPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.friendserverPanel.Location = new System.Drawing.Point(16, 95);
            this.friendserverPanel.Name = "friendserverPanel";
            this.friendserverPanel.Size = new System.Drawing.Size(317, 366);
            this.friendserverPanel.TabIndex = 4;
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(16, 12);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(50, 50);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            this.profilepicture.Click += new System.EventHandler(this.profilepicture_Click);
            // 
            // friendsButton
            // 
            this.friendsButton.Location = new System.Drawing.Point(16, 68);
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.Size = new System.Drawing.Size(59, 23);
            this.friendsButton.TabIndex = 5;
            this.friendsButton.Text = "Friends";
            this.friendsButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(77, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Servers";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 473);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.friendserverPanel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Aerocord";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private WindowsFormsAero.ThemeLabel usernameLabel;
        private WindowsFormsAero.ThemeLabel statusLabel;
        private System.Windows.Forms.FlowLayoutPanel friendserverPanel;
        private WindowsFormsAero.Button friendsButton;
        private WindowsFormsAero.Button button2;
    }
}

