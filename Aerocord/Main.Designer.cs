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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Nig");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Nig");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Nig");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Nig");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("18+");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("18+");
            this.username = new WindowsFormsAero.ThemeLabel();
            this.description = new WindowsFormsAero.ThemeLabel();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.servers = new WindowsFormsAero.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.serversTab = new System.Windows.Forms.TabPage();
            this.listView1 = new WindowsFormsAero.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.friendsTab.SuspendLayout();
            this.serversTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.username.Location = new System.Drawing.Point(118, 12);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(213, 23);
            this.username.TabIndex = 1;
            this.username.Text = "username";
            // 
            // description
            // 
            this.description.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.description.ForeColor = System.Drawing.SystemColors.ControlText;
            this.description.Location = new System.Drawing.Point(118, 41);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(213, 72);
            this.description.TabIndex = 3;
            this.description.Text = "description";
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Black;
            this.profilepicture.Image = global::Aerocord.Properties.Resources.discord_mark_black;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.Location = new System.Drawing.Point(12, 12);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(100, 100);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            // 
            // servers
            // 
            this.servers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.servers.ForeColor = System.Drawing.SystemColors.WindowText;
            this.servers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.servers.HideSelection = false;
            listViewItem8.StateImageIndex = 0;
            this.servers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.servers.LabelWrap = false;
            this.servers.Location = new System.Drawing.Point(6, 6);
            this.servers.Name = "servers";
            this.servers.Size = new System.Drawing.Size(300, 347);
            this.servers.TabIndex = 0;
            this.servers.UseCompatibleStateImageBehavior = false;
            this.servers.View = System.Windows.Forms.View.Tile;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.friendsTab);
            this.tabControl1.Controls.Add(this.serversTab);
            this.tabControl1.Location = new System.Drawing.Point(13, 119);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(320, 387);
            this.tabControl1.TabIndex = 4;
            // 
            // friendsTab
            // 
            this.friendsTab.Controls.Add(this.servers);
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
            this.serversTab.Controls.Add(this.listView1);
            this.serversTab.Location = new System.Drawing.Point(4, 24);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(312, 359);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem11,
            listViewItem12});
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(300, 347);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 518);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.description);
            this.Controls.Add(this.username);
            this.Controls.Add(this.profilepicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Aerocord";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.friendsTab.ResumeLayout(false);
            this.serversTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private WindowsFormsAero.ThemeLabel username;
        private WindowsFormsAero.ThemeLabel description;
        public WindowsFormsAero.ListView servers;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        public WindowsFormsAero.ListView listView1;
    }
}

