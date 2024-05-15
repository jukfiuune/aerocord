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
            this.channel = new WindowsFormsAero.ThemeLabel();
            this.servername = new WindowsFormsAero.ThemeLabel();
            this.textBox1 = new WindowsFormsAero.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.listView1 = new WindowsFormsAero.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // channel
            // 
            this.channel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.channel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.channel.Location = new System.Drawing.Point(118, 41);
            this.channel.Name = "channel";
            this.channel.Size = new System.Drawing.Size(654, 22);
            this.channel.TabIndex = 5;
            this.channel.Text = "#channel";
            // 
            // servername
            // 
            this.servername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.servername.Location = new System.Drawing.Point(118, 12);
            this.servername.Name = "servername";
            this.servername.Size = new System.Drawing.Size(213, 23);
            this.servername.TabIndex = 4;
            this.servername.Text = "servername";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 326);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(654, 23);
            this.textBox1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::Aerocord.Properties.Resources.defaultpfp;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(13, 119);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(100, 230);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.channel);
            this.Controls.Add(this.servername);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Server";
            this.ShowIcon = false;
            this.Text = "Aerocord - Chat";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private WindowsFormsAero.ThemeLabel channel;
        private WindowsFormsAero.ThemeLabel servername;
        private WindowsFormsAero.TextBox textBox1;
        private System.Windows.Forms.WebBrowser chatBox;
        private WindowsFormsAero.ListView listView1;
    }
}