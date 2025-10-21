namespace IntegratedGuiV2
{
    partial class AccountsManagement
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
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.bDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lExit = new System.Windows.Forms.Label();
            this.lBackToLogin = new System.Windows.Forms.Label();
            this.bClear = new System.Windows.Forms.Button();
            this.bRegister = new System.Windows.Forms.Button();
            this.cbPermissions = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.lId = new System.Windows.Forms.Label();
            this.lCreateAccount = new System.Windows.Forms.Label();
            this.lPermissions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            // bDelete
            // 
            this.bDelete.BackColor = System.Drawing.Color.White;
            this.bDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bDelete.Location = new System.Drawing.Point(516, 20);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(67, 36);
            this.bDelete.TabIndex = 59;
            this.bDelete.Text = "DELETE";
            this.bDelete.UseVisualStyleBackColor = false;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(202)))));
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Location = new System.Drawing.Point(230, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(354, 322);
            this.dataGridView1.TabIndex = 58;
            // 
            // lExit
            // 
            this.lExit.AutoSize = true;
            this.lExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lExit.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.lExit.Location = new System.Drawing.Point(186, 367);
            this.lExit.Name = "lExit";
            this.lExit.Size = new System.Drawing.Size(33, 19);
            this.lExit.TabIndex = 57;
            this.lExit.Text = "Exit";
            this.lExit.Visible = false;
            this.lExit.Click += new System.EventHandler(this.lExit_Click);
            // 
            // lBackToLogin
            // 
            this.lBackToLogin.AutoSize = true;
            this.lBackToLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lBackToLogin.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBackToLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.lBackToLogin.Location = new System.Drawing.Point(54, 367);
            this.lBackToLogin.Name = "lBackToLogin";
            this.lBackToLogin.Size = new System.Drawing.Size(131, 19);
            this.lBackToLogin.TabIndex = 56;
            this.lBackToLogin.Text = "Back to MainForm";
            this.lBackToLogin.Click += new System.EventHandler(this.lBackToLogin_Click);
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.Color.White;
            this.bClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bClear.Location = new System.Drawing.Point(17, 274);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(200, 36);
            this.bClear.TabIndex = 55;
            this.bClear.Text = "CLEAR";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bRegister
            // 
            this.bRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRegister.FlatAppearance.BorderSize = 0;
            this.bRegister.ForeColor = System.Drawing.Color.White;
            this.bRegister.Location = new System.Drawing.Point(17, 232);
            this.bRegister.Name = "bRegister";
            this.bRegister.Size = new System.Drawing.Size(200, 36);
            this.bRegister.TabIndex = 54;
            this.bRegister.Text = "REGISTER";
            this.bRegister.UseVisualStyleBackColor = false;
            this.bRegister.Click += new System.EventHandler(this.bRegister_Click);
            // 
            // cbPermissions
            // 
            this.cbPermissions.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPermissions.FormattingEnabled = true;
            this.cbPermissions.Items.AddRange(new object[] {
            "MP Manager",
            "Engineer",
            "Administrator"});
            this.cbPermissions.Location = new System.Drawing.Point(17, 182);
            this.cbPermissions.Name = "cbPermissions";
            this.cbPermissions.Size = new System.Drawing.Size(200, 29);
            this.cbPermissions.TabIndex = 53;
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.White;
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(17, 125);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(200, 32);
            this.tbPassword.TabIndex = 52;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbRegister_KeyDown);
            // 
            // tbId
            // 
            this.tbId.BackColor = System.Drawing.Color.White;
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbId.Location = new System.Drawing.Point(17, 68);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(200, 32);
            this.tbId.TabIndex = 51;
            this.tbId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbRegister_KeyDown);
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.lPassword.Location = new System.Drawing.Point(13, 103);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(77, 19);
            this.lPassword.TabIndex = 50;
            this.lPassword.Text = "Passwrod:";
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lId.ForeColor = System.Drawing.Color.DarkGray;
            this.lId.Location = new System.Drawing.Point(13, 46);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(27, 19);
            this.lId.TabIndex = 49;
            this.lId.Text = "ID:";
            // 
            // lCreateAccount
            // 
            this.lCreateAccount.AutoSize = true;
            this.lCreateAccount.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCreateAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(132)))), ((int)(((byte)(92)))));
            this.lCreateAccount.Location = new System.Drawing.Point(206, 0);
            this.lCreateAccount.Name = "lCreateAccount";
            this.lCreateAccount.Size = new System.Drawing.Size(189, 32);
            this.lCreateAccount.TabIndex = 48;
            this.lCreateAccount.Text = "Create Account";
            // 
            // lPermissions
            // 
            this.lPermissions.AutoSize = true;
            this.lPermissions.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPermissions.ForeColor = System.Drawing.Color.DarkGray;
            this.lPermissions.Location = new System.Drawing.Point(13, 160);
            this.lPermissions.Name = "lPermissions";
            this.lPermissions.Size = new System.Drawing.Size(91, 19);
            this.lPermissions.TabIndex = 60;
            this.lPermissions.Text = "Permissions:";
            // 
            // AccountsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lExit);
            this.Controls.Add(this.lBackToLogin);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bRegister);
            this.Controls.Add(this.cbPermissions);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lId);
            this.Controls.Add(this.lCreateAccount);
            this.Controls.Add(this.lPermissions);
            this.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AccountsManagement";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lExit;
        private System.Windows.Forms.Label lBackToLogin;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bRegister;
        private System.Windows.Forms.ComboBox cbPermissions;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.Label lCreateAccount;
        private System.Windows.Forms.Label lPermissions;
    }
}