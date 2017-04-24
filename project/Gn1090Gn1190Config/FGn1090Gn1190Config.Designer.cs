namespace Gn1090Gn1190Config
{
    partial class FGn1090Gn1190Config
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
            if (disposing && (components != null)) {
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
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.tbGn1090Gn1190Config = new System.Windows.Forms.TabControl();
            this.tpGn1090Config = new System.Windows.Forms.TabPage();
            this.tpGn1190Config = new System.Windows.Forms.TabPage();
            this.ucGn1090Config = new Gn1090Gn1190Config.UcGn1090Config();
            this.ucGn1190Config = new Gn1090Gn1190Config.UcGn1190Config();
            this.tbGn1090Gn1190Config.SuspendLayout();
            this.tpGn1090Config.SuspendLayout();
            this.tpGn1190Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(591, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tbGn1090Gn1190Config
            // 
            this.tbGn1090Gn1190Config.Controls.Add(this.tpGn1090Config);
            this.tbGn1090Gn1190Config.Controls.Add(this.tpGn1190Config);
            this.tbGn1090Gn1190Config.Location = new System.Drawing.Point(12, 34);
            this.tbGn1090Gn1190Config.Name = "tbGn1090Gn1190Config";
            this.tbGn1090Gn1190Config.SelectedIndex = 0;
            this.tbGn1090Gn1190Config.Size = new System.Drawing.Size(653, 548);
            this.tbGn1090Gn1190Config.TabIndex = 1;
            // 
            // tpGn1090Config
            // 
            this.tpGn1090Config.Controls.Add(this.ucGn1090Config);
            this.tpGn1090Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn1090Config.Name = "tpGn1090Config";
            this.tpGn1090Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn1090Config.Size = new System.Drawing.Size(645, 522);
            this.tpGn1090Config.TabIndex = 0;
            this.tpGn1090Config.Text = "GN1090";
            this.tpGn1090Config.UseVisualStyleBackColor = true;
            // 
            // tpGn1190Config
            // 
            this.tpGn1190Config.Controls.Add(this.ucGn1190Config);
            this.tpGn1190Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn1190Config.Name = "tpGn1190Config";
            this.tpGn1190Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn1190Config.Size = new System.Drawing.Size(645, 522);
            this.tpGn1190Config.TabIndex = 1;
            this.tpGn1190Config.Text = "GN1190";
            this.tpGn1190Config.UseVisualStyleBackColor = true;
            // 
            // ucGn1090Config
            // 
            this.ucGn1090Config.Location = new System.Drawing.Point(0, 0);
            this.ucGn1090Config.Name = "ucGn1090Config";
            this.ucGn1090Config.Size = new System.Drawing.Size(643, 440);
            this.ucGn1090Config.TabIndex = 0;
            // 
            // ucGn1190Config
            // 
            this.ucGn1190Config.Location = new System.Drawing.Point(0, 0);
            this.ucGn1190Config.Name = "ucGn1190Config";
            this.ucGn1190Config.Size = new System.Drawing.Size(641, 522);
            this.ucGn1190Config.TabIndex = 0;
            // 
            // FGn1090Gn1190Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 584);
            this.Controls.Add(this.tbGn1090Gn1190Config);
            this.Controls.Add(this.cbConnected);
            this.Name = "FGn1090Gn1190Config";
            this.Text = "GN1090&GN1190 Config";
            this.tbGn1090Gn1190Config.ResumeLayout(false);
            this.tpGn1090Config.ResumeLayout(false);
            this.tpGn1190Config.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tbGn1090Gn1190Config;
        private System.Windows.Forms.TabPage tpGn1090Config;
        private System.Windows.Forms.TabPage tpGn1190Config;
        private UcGn1190Config ucGn1190Config;
        private UcGn1090Config ucGn1090Config;
    }
}

