namespace Aerocord
{
    partial class ChannelEntry
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
            this.type = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.type)).BeginInit();
            this.SuspendLayout();
            // 
            // type
            // 
            this.type.Image = global::Aerocord.Properties.Resources.Chat;
            this.type.Location = new System.Drawing.Point(4, 4);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(16, 16);
            this.type.TabIndex = 0;
            this.type.TabStop = false;
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(26, 6);
            this.label.MaximumSize = new System.Drawing.Size(171, 13);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(171, 13);
            this.label.TabIndex = 1;
            this.label.Text = "channel";
            // 
            // ChannelEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Controls.Add(this.type);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ChannelEntry";
            this.Size = new System.Drawing.Size(200, 24);
            ((System.ComponentModel.ISupportInitialize)(this.type)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox type;
        private System.Windows.Forms.Label label;
    }
}
