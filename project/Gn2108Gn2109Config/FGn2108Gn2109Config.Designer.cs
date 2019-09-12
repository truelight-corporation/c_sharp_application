namespace Gn2108Gn2109Config
{
    partial class FGn2108Gn2109Config
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
            this.tcGn2108Gn2109Config = new System.Windows.Forms.TabControl();
            this.tpGn2108Config = new System.Windows.Forms.TabPage();
            this.tpGn2109Config = new System.Windows.Forms.TabPage();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.ucGn2108Config = new Gn2108Gn2109Config.UcGn2108Config();
            this.ucGn2109Config = new Gn2108Gn2109Config.UcGn2109Config();
            this.tcGn2108Gn2109Config.SuspendLayout();
            this.tpGn2108Config.SuspendLayout();
            this.tpGn2109Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(706, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tcGn2108Gn2109Config
            // 
            this.tcGn2108Gn2109Config.Controls.Add(this.tpGn2108Config);
            this.tcGn2108Gn2109Config.Controls.Add(this.tpGn2109Config);
            this.tcGn2108Gn2109Config.Location = new System.Drawing.Point(5, 34);
            this.tcGn2108Gn2109Config.Name = "tcGn2108Gn2109Config";
            this.tcGn2108Gn2109Config.SelectedIndex = 0;
            this.tcGn2108Gn2109Config.Size = new System.Drawing.Size(785, 623);
            this.tcGn2108Gn2109Config.TabIndex = 1;
            // 
            // tpGn2108Config
            // 
            this.tpGn2108Config.Controls.Add(this.ucGn2108Config);
            this.tpGn2108Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn2108Config.Name = "tpGn2108Config";
            this.tpGn2108Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn2108Config.Size = new System.Drawing.Size(777, 597);
            this.tpGn2108Config.TabIndex = 0;
            this.tpGn2108Config.Text = "GN2108";
            this.tpGn2108Config.UseVisualStyleBackColor = true;
            // 
            // tpGn2109Config
            // 
            this.tpGn2109Config.Controls.Add(this.ucGn2109Config);
            this.tpGn2109Config.Location = new System.Drawing.Point(4, 22);
            this.tpGn2109Config.Name = "tpGn2109Config";
            this.tpGn2109Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn2109Config.Size = new System.Drawing.Size(777, 597);
            this.tpGn2109Config.TabIndex = 1;
            this.tpGn2109Config.Text = "GN2109";
            this.tpGn2109Config.UseVisualStyleBackColor = true;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(72, 9);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(60, 22);
            this.tbPassword.TabIndex = 69;
            this.tbPassword.Text = "3234";
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(12, 12);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(54, 12);
            this.lPassword.TabIndex = 68;
            this.lPassword.Text = "Password :";
            // 
            // ucGn2108Config
            // 
            this.ucGn2108Config.Location = new System.Drawing.Point(0, 0);
            this.ucGn2108Config.Name = "ucGn2108Config";
            this.ucGn2108Config.Size = new System.Drawing.Size(777, 594);
            this.ucGn2108Config.TabIndex = 0;
            // 
            // ucGn2109Config
            // 
            this.ucGn2109Config.Location = new System.Drawing.Point(0, 0);
            this.ucGn2109Config.Name = "ucGn2109Config";
            this.ucGn2109Config.Size = new System.Drawing.Size(777, 574);
            this.ucGn2109Config.TabIndex = 0;
            // 
            // FGn2108Gn2109Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 661);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.tcGn2108Gn2109Config);
            this.Controls.Add(this.cbConnected);
            this.Name = "FGn2108Gn2109Config";
            this.Text = "GN2108&GN2109 Config_20190912";
            this.tcGn2108Gn2109Config.ResumeLayout(false);
            this.tpGn2108Config.ResumeLayout(false);
            this.tpGn2109Config.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcGn2108Gn2109Config;
        private System.Windows.Forms.TabPage tpGn2108Config;
        private System.Windows.Forms.TabPage tpGn2109Config;
        private UcGn2108Config ucGn2108Config;
        private UcGn2109Config ucGn2109Config;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lPassword;
    }
}

