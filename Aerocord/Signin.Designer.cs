namespace Aerocord
{
    partial class Signin
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
            this.username = new WindowsFormsAero.ThemeLabel();
            this.themeLabel1 = new WindowsFormsAero.ThemeLabel();
            this.themeLabel2 = new WindowsFormsAero.ThemeLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.themeLabel3 = new WindowsFormsAero.ThemeLabel();
            this.token = new WindowsFormsAero.TextBox();
            this.signinButton = new WindowsFormsAero.Button();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.username.Location = new System.Drawing.Point(12, 118);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(321, 28);
            this.username.TabIndex = 2;
            this.username.Text = "Sign in";
            this.username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // themeLabel1
            // 
            this.themeLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.themeLabel1.Location = new System.Drawing.Point(12, 152);
            this.themeLabel1.Name = "themeLabel1";
            this.themeLabel1.Size = new System.Drawing.Size(321, 23);
            this.themeLabel1.TabIndex = 3;
            this.themeLabel1.Text = "Sign in to Discord using your token.";
            // 
            // themeLabel2
            // 
            this.themeLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.themeLabel2.Location = new System.Drawing.Point(12, 170);
            this.themeLabel2.Name = "themeLabel2";
            this.themeLabel2.Size = new System.Drawing.Size(257, 23);
            this.themeLabel2.TabIndex = 4;
            this.themeLabel2.Text = "Don\'t know where to find it? Find out";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Black;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(232, 170);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 19);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "here";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // themeLabel3
            // 
            this.themeLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.themeLabel3.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.themeLabel3.Location = new System.Drawing.Point(232, 170);
            this.themeLabel3.Name = "themeLabel3";
            this.themeLabel3.Size = new System.Drawing.Size(38, 23);
            this.themeLabel3.TabIndex = 5;
            this.themeLabel3.Text = "here";
            // 
            // token
            // 
            this.token.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.token.Location = new System.Drawing.Point(12, 221);
            this.token.Name = "token";
            this.token.Size = new System.Drawing.Size(218, 25);
            this.token.TabIndex = 7;
            // 
            // signinButton
            // 
            this.signinButton.Location = new System.Drawing.Point(236, 221);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(97, 25);
            this.signinButton.TabIndex = 8;
            this.signinButton.Text = "Sign in";
            this.signinButton.UseVisualStyleBackColor = true;
            this.signinButton.Click += new System.EventHandler(this.signinButton_Click);
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.Frame_491;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.Location = new System.Drawing.Point(121, 12);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(100, 100);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            // 
            // Signin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 276);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.token);
            this.Controls.Add(this.themeLabel3);
            this.Controls.Add(this.themeLabel2);
            this.Controls.Add(this.themeLabel1);
            this.Controls.Add(this.username);
            this.Controls.Add(this.profilepicture);
            this.Controls.Add(this.linkLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Signin";
            this.ShowIcon = false;
            this.Text = "Aerocord";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private WindowsFormsAero.ThemeLabel username;
        private WindowsFormsAero.ThemeLabel themeLabel1;
        private WindowsFormsAero.ThemeLabel themeLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private WindowsFormsAero.ThemeLabel themeLabel3;
        private WindowsFormsAero.TextBox token;
        private WindowsFormsAero.Button signinButton;
    }
}

