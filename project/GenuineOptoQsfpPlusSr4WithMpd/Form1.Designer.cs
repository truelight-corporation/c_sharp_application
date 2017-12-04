namespace GenuineOptoQsfpPlusSr4WithMpd
{
    partial class fMain
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
            this.tbPassword126 = new System.Windows.Forms.TextBox();
            this.tbPassword125 = new System.Windows.Forms.TextBox();
            this.tbPassword124 = new System.Windows.Forms.TextBox();
            this.tbPassword123 = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.ucMpdAndPdLoger = new GenuineOptoQsfpPlusSr4WithMpd.UcMpdAndPdLoger();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(446, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 36;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tbPassword126
            // 
            this.tbPassword126.Location = new System.Drawing.Point(165, 10);
            this.tbPassword126.Name = "tbPassword126";
            this.tbPassword126.Size = new System.Drawing.Size(25, 22);
            this.tbPassword126.TabIndex = 41;
            this.tbPassword126.Text = "57";
            // 
            // tbPassword125
            // 
            this.tbPassword125.Location = new System.Drawing.Point(134, 10);
            this.tbPassword125.Name = "tbPassword125";
            this.tbPassword125.Size = new System.Drawing.Size(25, 22);
            this.tbPassword125.TabIndex = 40;
            this.tbPassword125.Text = "83";
            // 
            // tbPassword124
            // 
            this.tbPassword124.Location = new System.Drawing.Point(103, 10);
            this.tbPassword124.Name = "tbPassword124";
            this.tbPassword124.Size = new System.Drawing.Size(25, 22);
            this.tbPassword124.TabIndex = 39;
            this.tbPassword124.Text = "82";
            // 
            // tbPassword123
            // 
            this.tbPassword123.Location = new System.Drawing.Point(72, 10);
            this.tbPassword123.Name = "tbPassword123";
            this.tbPassword123.Size = new System.Drawing.Size(25, 22);
            this.tbPassword123.TabIndex = 38;
            this.tbPassword123.Text = "83";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(12, 13);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(54, 12);
            this.lPassword.TabIndex = 37;
            this.lPassword.Text = "Password :";
            // 
            // ucMpdAndPdLoger
            // 
            this.ucMpdAndPdLoger.Location = new System.Drawing.Point(14, 38);
            this.ucMpdAndPdLoger.Name = "ucMpdAndPdLoger";
            this.ucMpdAndPdLoger.Size = new System.Drawing.Size(506, 449);
            this.ucMpdAndPdLoger.TabIndex = 31;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 468);
            this.Controls.Add(this.tbPassword126);
            this.Controls.Add(this.tbPassword125);
            this.Controls.Add(this.tbPassword124);
            this.Controls.Add(this.tbPassword123);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.cbConnected);
            this.Controls.Add(this.ucMpdAndPdLoger);
            this.MaximizeBox = false;
            this.Name = "fMain";
            this.Text = "Genuine-Opto QSFP+ SR4 with MPD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcMpdAndPdLoger ucMpdAndPdLoger;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TextBox tbPassword126;
        private System.Windows.Forms.TextBox tbPassword125;
        private System.Windows.Forms.TextBox tbPassword124;
        private System.Windows.Forms.TextBox tbPassword123;
        private System.Windows.Forms.Label lPassword;
    }
}

