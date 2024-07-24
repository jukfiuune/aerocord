namespace Aerocord
{
    partial class Settings
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
            this.rendermode = new WindowsFormsAero.ComboBox();
            this.rendermodelabel = new System.Windows.Forms.Label();
            this.colormodelabel = new System.Windows.Forms.Label();
            this.colormode = new WindowsFormsAero.ComboBox();
            this.save = new WindowsFormsAero.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rendermode
            // 
            this.rendermode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rendermode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rendermode.FormattingEnabled = true;
            this.rendermode.Items.AddRange(new object[] {
            "Aero",
            "Acrylic",
            "Mica"});
            this.rendermode.Location = new System.Drawing.Point(114, 13);
            this.rendermode.Name = "rendermode";
            this.rendermode.Size = new System.Drawing.Size(121, 21);
            this.rendermode.TabIndex = 1;
            // 
            // rendermodelabel
            // 
            this.rendermodelabel.AutoSize = true;
            this.rendermodelabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rendermodelabel.Location = new System.Drawing.Point(13, 13);
            this.rendermodelabel.Name = "rendermodelabel";
            this.rendermodelabel.Size = new System.Drawing.Size(95, 19);
            this.rendermodelabel.TabIndex = 1;
            this.rendermodelabel.Text = "Render Mode:";
            // 
            // colormodelabel
            // 
            this.colormodelabel.AutoSize = true;
            this.colormodelabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.colormodelabel.Location = new System.Drawing.Point(13, 40);
            this.colormodelabel.Name = "colormodelabel";
            this.colormodelabel.Size = new System.Drawing.Size(85, 19);
            this.colormodelabel.TabIndex = 2;
            this.colormodelabel.Text = "Color Mode:";
            // 
            // colormode
            // 
            this.colormode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colormode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.colormode.FormattingEnabled = true;
            this.colormode.Items.AddRange(new object[] {
            "Default",
            "Light",
            "Dark"});
            this.colormode.Location = new System.Drawing.Point(114, 40);
            this.colormode.Name = "colormode";
            this.colormode.Size = new System.Drawing.Size(121, 21);
            this.colormode.TabIndex = 3;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(12, 88);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(344, 23);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "reopen Aerocord for the changes to take effect";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.save);
            this.Controls.Add(this.colormode);
            this.Controls.Add(this.colormodelabel);
            this.Controls.Add(this.rendermodelabel);
            this.Controls.Add(this.rendermode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label rendermodelabel;
        private System.Windows.Forms.Label colormodelabel;
        private WindowsFormsAero.Button save;
        private System.Windows.Forms.Label label1;
        private WindowsFormsAero.ComboBox rendermode;
        private WindowsFormsAero.ComboBox colormode;
    }
}