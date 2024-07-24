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
            this.descriptionLabel = new WindowsFormsAero.ThemeLabel();
            this.friendsList = new WindowsFormsAero.ListView();
            this.fsTabs = new System.Windows.Forms.TabControl();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.serversTab = new System.Windows.Forms.TabPage();
            this.serversList = new WindowsFormsAero.ListView();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.fsTabs.SuspendLayout();
            this.friendsTab.SuspendLayout();
            this.serversTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(74, 12);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(213, 23);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionLabel.Location = new System.Drawing.Point(74, 41);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(213, 21);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "description";
            // 
            // friendsList
            // 
            this.friendsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.friendsList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.friendsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.friendsList.HideSelection = false;
            this.friendsList.LabelWrap = false;
            this.friendsList.Location = new System.Drawing.Point(6, 6);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(300, 347);
            this.friendsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.friendsList.TabIndex = 0;
            this.friendsList.UseCompatibleStateImageBehavior = false;
            this.friendsList.View = System.Windows.Forms.View.Tile;
            this.friendsList.DoubleClick += new System.EventHandler(this.friendsList_DoubleClick);
            // 
            // fsTabs
            // 
            this.fsTabs.Controls.Add(this.friendsTab);
            this.fsTabs.Controls.Add(this.serversTab);
            this.fsTabs.Location = new System.Drawing.Point(14, 73);
            this.fsTabs.Name = "fsTabs";
            this.fsTabs.SelectedIndex = 0;
            this.fsTabs.Size = new System.Drawing.Size(320, 387);
            this.fsTabs.TabIndex = 4;
            // 
            // friendsTab
            // 
            this.friendsTab.Controls.Add(this.friendsList);
            this.friendsTab.Location = new System.Drawing.Point(4, 24);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Padding = new System.Windows.Forms.Padding(3);
            this.friendsTab.Size = new System.Drawing.Size(312, 359);
            this.friendsTab.TabIndex = 0;
            this.friendsTab.Text = "Friends";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // serversTab
            // 
            this.serversTab.Controls.Add(this.serversList);
            this.serversTab.Location = new System.Drawing.Point(4, 24);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(312, 359);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // serversList
            // 
            this.serversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serversList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.serversList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.serversList.HideSelection = false;
            this.serversList.LabelWrap = false;
            this.serversList.Location = new System.Drawing.Point(6, 6);
            this.serversList.Name = "serversList";
            this.serversList.Size = new System.Drawing.Size(300, 347);
            this.serversList.TabIndex = 1;
            this.serversList.UseCompatibleStateImageBehavior = false;
            this.serversList.View = System.Windows.Forms.View.Tile;
            this.serversList.DoubleClick += new System.EventHandler(this.serversList_DoubleClick);
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.ErrorImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.InitialImage = global::Aerocord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(12, 12);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(50, 50);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            this.profilepicture.Click += new System.EventHandler(this.profilepicture_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 473);
            this.Controls.Add(this.fsTabs);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Aerocord";
            this.fsTabs.ResumeLayout(false);
            this.friendsTab.ResumeLayout(false);
            this.serversTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private WindowsFormsAero.ThemeLabel usernameLabel;
        private WindowsFormsAero.ThemeLabel descriptionLabel;
        private System.Windows.Forms.TabControl fsTabs;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        private WindowsFormsAero.ListView friendsList;
        private WindowsFormsAero.ListView serversList;
    }
}

