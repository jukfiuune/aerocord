namespace Aerocord
{
    partial class UserProfile
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
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.frame = new System.Windows.Forms.PictureBox();
            this.statusLabel = new WindowsFormsAero.ThemeLabel();
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.pronounsLabel = new WindowsFormsAero.ThemeLabel();
            this.bioLabel = new WindowsFormsAero.ThemeLabel();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            this.SuspendLayout();
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(19, 21);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(80, 80);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 12;
            this.profilepicture.TabStop = false;
            // 
            // frame
            // 
            this.frame.BackColor = System.Drawing.Color.Black;
            this.frame.Image = global::Aerocord.Properties.Resources.offline;
            this.frame.Location = new System.Drawing.Point(-1, 6);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(117, 115);
            this.frame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.frame.TabIndex = 13;
            this.frame.TabStop = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI Variable Text Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.statusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusLabel.Location = new System.Drawing.Point(122, 48);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(336, 25);
            this.statusLabel.TabIndex = 15;
            this.statusLabel.Text = "status";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(122, 21);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(336, 27);
            this.usernameLabel.TabIndex = 14;
            this.usernameLabel.Text = "username";
            // 
            // pronounsLabel
            // 
            this.pronounsLabel.Font = new System.Drawing.Font("Segoe UI Variable Text", 11.25F);
            this.pronounsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pronounsLabel.Location = new System.Drawing.Point(122, 76);
            this.pronounsLabel.Name = "pronounsLabel";
            this.pronounsLabel.Size = new System.Drawing.Size(336, 25);
            this.pronounsLabel.TabIndex = 16;
            this.pronounsLabel.Text = "pronouns";
            // 
            // bioLabel
            // 
            this.bioLabel.Font = new System.Drawing.Font("Segoe UI Variable Text", 11.25F);
            this.bioLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bioLabel.Location = new System.Drawing.Point(12, 127);
            this.bioLabel.Name = "bioLabel";
            this.bioLabel.SingleLine = false;
            this.bioLabel.Size = new System.Drawing.Size(446, 268);
            this.bioLabel.TabIndex = 17;
            this.bioLabel.Text = "bio";
            // 
            // UserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 407);
            this.Controls.Add(this.bioLabel);
            this.Controls.Add(this.pronounsLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicture);
            this.Controls.Add(this.frame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserProfile";
            this.ShowIcon = false;
            this.Text = "Aerocord - Profile";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.PictureBox frame;
        private WindowsFormsAero.ThemeLabel statusLabel;
        private WindowsFormsAero.ThemeLabel usernameLabel;
        private WindowsFormsAero.ThemeLabel pronounsLabel;
        private WindowsFormsAero.ThemeLabel bioLabel;
    }
}