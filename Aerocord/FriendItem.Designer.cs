namespace Aerocord
{
    partial class FriendItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.profilePictureFriend = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureFriend)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.Black;
            this.usernameLabel.Location = new System.Drawing.Point(40, 7);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(195, 19);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "usernameLabel";
            // 
            // profilePictureFriend
            // 
            this.profilePictureFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.profilePictureFriend.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilePictureFriend.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilePictureFriend.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilePictureFriend.Location = new System.Drawing.Point(4, 3);
            this.profilePictureFriend.Name = "profilePictureFriend";
            this.profilePictureFriend.Size = new System.Drawing.Size(27, 27);
            this.profilePictureFriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePictureFriend.TabIndex = 0;
            this.profilePictureFriend.TabStop = false;
            // 
            // FriendItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilePictureFriend);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "FriendItem";
            this.Size = new System.Drawing.Size(252, 35);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureFriend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilePictureFriend;
        private WindowsFormsAero.ThemeLabel usernameLabel;
    }
}
