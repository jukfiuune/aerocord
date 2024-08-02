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
            this.rendermodelabel = new WindowsFormsAero.ThemeLabel();
            this.colormodelabel = new WindowsFormsAero.ThemeLabel();
            this.colormode = new WindowsFormsAero.ComboBox();
            this.save = new WindowsFormsAero.Button();
            this.warning = new WindowsFormsAero.ThemeLabel();
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
            "Mica",
            "Mica Alt"});
            this.rendermode.Location = new System.Drawing.Point(116, 15);
            this.rendermode.Name = "rendermode";
            this.rendermode.Size = new System.Drawing.Size(299, 23);
            this.rendermode.TabIndex = 1;
            this.rendermode.SelectedIndexChanged += new System.EventHandler(this.rendermode_SelectedIndexChanged);
            // 
            // rendermodelabel
            // 
            this.rendermodelabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rendermodelabel.Location = new System.Drawing.Point(15, 15);
            this.rendermodelabel.Name = "rendermodelabel";
            this.rendermodelabel.Size = new System.Drawing.Size(95, 19);
            this.rendermodelabel.TabIndex = 1;
            this.rendermodelabel.Text = "Render Mode:";
            // 
            // colormodelabel
            // 
            this.colormodelabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.colormodelabel.Location = new System.Drawing.Point(15, 46);
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
            "System",
            "Light",
            "Dark"});
            this.colormode.Location = new System.Drawing.Point(116, 46);
            this.colormode.Name = "colormode";
            this.colormode.Size = new System.Drawing.Size(299, 23);
            this.colormode.TabIndex = 3;
            this.colormode.SelectedIndexChanged += new System.EventHandler(this.colormode_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(14, 102);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(401, 27);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // warning
            // 
            this.warning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warning.Location = new System.Drawing.Point(49, 81);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(341, 17);
            this.warning.TabIndex = 5;
            this.warning.Text = "Aerocord needs to be restarted for the changes to take place.";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 142);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.save);
            this.Controls.Add(this.colormode);
            this.Controls.Add(this.colormodelabel);
            this.Controls.Add(this.rendermodelabel);
            this.Controls.Add(this.rendermode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WindowsFormsAero.ThemeLabel rendermodelabel;
        private WindowsFormsAero.ThemeLabel colormodelabel;
        private WindowsFormsAero.Button save;
        private WindowsFormsAero.ThemeLabel warning;
        private WindowsFormsAero.ComboBox rendermode;
        private WindowsFormsAero.ComboBox colormode;
    }
}