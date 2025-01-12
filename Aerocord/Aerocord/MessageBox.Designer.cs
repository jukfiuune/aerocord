namespace Aerocord
{
    partial class MessageBox
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
            this.name = new System.Windows.Forms.Label();
            this.content = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.name.Location = new System.Drawing.Point(3, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(47, 17);
            this.name.TabIndex = 0;
            this.name.Text = "name";
            // 
            // content
            // 
            this.content.AutoSize = true;
            this.content.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.content.Location = new System.Drawing.Point(3, 19);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(30, 17);
            this.content.TabIndex = 1;
            this.content.Text = "text";
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.content);
            this.Controls.Add(this.name);
            this.Name = "MessageBox";
            this.Size = new System.Drawing.Size(200, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label content;
    }
}
