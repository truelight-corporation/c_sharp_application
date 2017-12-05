namespace Gn2148Gn2149Config
{
    partial class FGn2148Gn2149Config
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
            this.tcGn2148Gn2149Config = new System.Windows.Forms.TabControl();
            this.tpGn2148Config = new System.Windows.Forms.TabPage();
            this.tpGn2149Config = new System.Windows.Forms.TabPage();
            this.ucGn2148Config = new Gn2148Gn2149Config.UcGn2148Config();
            this.ucGn2149Config = new Gn2148Gn2149Config.UcGn2149Config();
            this.tcGn2148Gn2149Config.SuspendLayout();
            this.tpGn2148Config.SuspendLayout();
            this.tpGn2149Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(601, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tcGn2148Gn2149Config
            // 
            this.tcGn2148Gn2149Config.Controls.Add(this.tpGn2148Config);
            this.tcGn2148Gn2149Config.Controls.Add(this.tpGn2149Config);
            this.tcGn2148Gn2149Config.Location = new System.Drawing.Point(12, 34);
            this.tcGn2148Gn2149Config.Name = "tcGn2148Gn2149Config";
            this.tcGn2148Gn2149Config.SelectedIndex = 0;
            this.tcGn2148Gn2149Config.Size = new System.Drawing.Size(667, 554);
            this.tcGn2148Gn2149Config.TabIndex = 1;
            // 
            // tpGn2148Config
            // 
            this.tpGn2148Config.Controls.Add(this.ucGn2148Config);
            this.tpGn2148Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn2148Config.Name = "tpGn2148Config";
            this.tpGn2148Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn2148Config.Size = new System.Drawing.Size(659, 528);
            this.tpGn2148Config.TabIndex = 0;
            this.tpGn2148Config.Text = "GN2148";
            this.tpGn2148Config.UseVisualStyleBackColor = true;
            // 
            // tpGn2149Config
            // 
            this.tpGn2149Config.Controls.Add(this.ucGn2149Config);
            this.tpGn2149Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn2149Config.Name = "tpGn2149Config";
            this.tpGn2149Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn2149Config.Size = new System.Drawing.Size(659, 528);
            this.tpGn2149Config.TabIndex = 1;
            this.tpGn2149Config.Text = "GN2149";
            this.tpGn2149Config.UseVisualStyleBackColor = true;
            // 
            // ucGn2148Config
            // 
            this.ucGn2148Config.Location = new System.Drawing.Point(6, 6);
            this.ucGn2148Config.Name = "ucGn2148Config";
            this.ucGn2148Config.Size = new System.Drawing.Size(647, 520);
            this.ucGn2148Config.TabIndex = 0;
            // 
            // ucGn2149Config
            // 
            this.ucGn2149Config.Location = new System.Drawing.Point(6, 6);
            this.ucGn2149Config.Name = "ucGn2149Config";
            this.ucGn2149Config.Size = new System.Drawing.Size(650, 425);
            this.ucGn2149Config.TabIndex = 0;
            // 
            // FGn2148Gn2149Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 591);
            this.Controls.Add(this.tcGn2148Gn2149Config);
            this.Controls.Add(this.cbConnected);
            this.Name = "FGn2148Gn2149Config";
            this.Text = "GN2148&GN2149 Config";
            this.tcGn2148Gn2149Config.ResumeLayout(false);
            this.tpGn2148Config.ResumeLayout(false);
            this.tpGn2149Config.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcGn2148Gn2149Config;
        private System.Windows.Forms.TabPage tpGn2148Config;
        private System.Windows.Forms.TabPage tpGn2149Config;
        private UcGn2149Config ucGn2149Config;
        private UcGn2148Config ucGn2148Config;
    }
}

