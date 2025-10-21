namespace IntegratedGuiV2
{
    partial class AdminAuthentication
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminAuthentication));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.tbAdminPassword = new System.Windows.Forms.TextBox();
            this.lBackToLogin = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.lAuthentication = new System.Windows.Forms.Label();
            this.bCheck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 15;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(132)))), ((int)(((byte)(92)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            // 
            // tbAdminPassword
            // 
            this.tbAdminPassword.BackColor = System.Drawing.Color.White;
            this.tbAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdminPassword.Font = new System.Drawing.Font("Bodoni MT", 16.2F, System.Drawing.FontStyle.Bold);
            this.tbAdminPassword.Location = new System.Drawing.Point(69, 73);
            this.tbAdminPassword.Multiline = true;
            this.tbAdminPassword.Name = "tbAdminPassword";
            this.tbAdminPassword.PasswordChar = '•';
            this.tbAdminPassword.Size = new System.Drawing.Size(200, 30);
            this.tbAdminPassword.TabIndex = 39;
            this.tbAdminPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbAdminPassword_KeyDown);
            // 
            // lBackToLogin
            // 
            this.lBackToLogin.AutoSize = true;
            this.lBackToLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lBackToLogin.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBackToLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.lBackToLogin.Location = new System.Drawing.Point(73, 156);
            this.lBackToLogin.Name = "lBackToLogin";
            this.lBackToLogin.Size = new System.Drawing.Size(131, 19);
            this.lBackToLogin.TabIndex = 42;
            this.lBackToLogin.Text = "Back to MainForm";
            this.lBackToLogin.Click += new System.EventHandler(this.lBackToLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.lPassword.Location = new System.Drawing.Point(65, 48);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(77, 19);
            this.lPassword.TabIndex = 38;
            this.lPassword.Text = "Passwrod:";
            // 
            // lAuthentication
            // 
            this.lAuthentication.AutoSize = true;
            this.lAuthentication.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAuthentication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(132)))), ((int)(((byte)(92)))));
            this.lAuthentication.Location = new System.Drawing.Point(52, 0);
            this.lAuthentication.Name = "lAuthentication";
            this.lAuthentication.Size = new System.Drawing.Size(184, 32);
            this.lAuthentication.TabIndex = 37;
            this.lAuthentication.Text = "Authentication";
            // 
            // bCheck
            // 
            this.bCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCheck.FlatAppearance.BorderSize = 0;
            this.bCheck.ForeColor = System.Drawing.Color.White;
            this.bCheck.Location = new System.Drawing.Point(69, 108);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(200, 36);
            this.bCheck.TabIndex = 40;
            this.bCheck.Text = "LOGIN";
            this.bCheck.UseVisualStyleBackColor = false;
            this.bCheck.Click += new System.EventHandler(this.bAuthenticate_Click);
            // 
            // AdminAuthentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(280, 178);
            this.Controls.Add(this.tbAdminPassword);
            this.Controls.Add(this.lBackToLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lAuthentication);
            this.Controls.Add(this.bCheck);
            this.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdminAuthentication";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.TextBox tbAdminPassword;
        private System.Windows.Forms.Label lBackToLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lAuthentication;
        private System.Windows.Forms.Button bCheck;
    }
}