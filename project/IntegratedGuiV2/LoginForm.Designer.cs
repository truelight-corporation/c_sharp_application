namespace IntegratedGuiV2
{
    partial class LoginForm
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
            this.lExit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bClear = new System.Windows.Forms.Button();
            this.bLogin = new System.Windows.Forms.Button();
            this.cbShowPasswrod = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.lId = new System.Windows.Forms.Label();
            this.lLogin = new System.Windows.Forms.Label();
            this.cbProducts = new System.Windows.Forms.ComboBox();
            this.lProduct = new System.Windows.Forms.Label();
            this.lBackToMain = new System.Windows.Forms.Label();
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
            // lExit
            // 
            this.lExit.AutoSize = true;
            this.lExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lExit.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.lExit.Location = new System.Drawing.Point(207, 314);
            this.lExit.Name = "lExit";
            this.lExit.Size = new System.Drawing.Size(33, 19);
            this.lExit.TabIndex = 43;
            this.lExit.Text = "Exit";
            this.lExit.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.label2.Location = new System.Drawing.Point(73, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 42;
            this.label2.Text = "Create Account";
            this.label2.Click += new System.EventHandler(this.lCreateAccount_Click);
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.Color.White;
            this.bClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bClear.Location = new System.Drawing.Point(27, 266);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(200, 36);
            this.bClear.TabIndex = 41;
            this.bClear.Text = "CLEAR";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bLogin
            // 
            this.bLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.bLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLogin.FlatAppearance.BorderSize = 0;
            this.bLogin.ForeColor = System.Drawing.Color.White;
            this.bLogin.Location = new System.Drawing.Point(27, 224);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(200, 36);
            this.bLogin.TabIndex = 40;
            this.bLogin.Text = "LOGIN";
            this.bLogin.UseVisualStyleBackColor = false;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // cbShowPasswrod
            // 
            this.cbShowPasswrod.AutoSize = true;
            this.cbShowPasswrod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowPasswrod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShowPasswrod.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPasswrod.ForeColor = System.Drawing.Color.DarkGray;
            this.cbShowPasswrod.Location = new System.Drawing.Point(122, 151);
            this.cbShowPasswrod.Name = "cbShowPasswrod";
            this.cbShowPasswrod.Size = new System.Drawing.Size(105, 17);
            this.cbShowPasswrod.TabIndex = 39;
            this.cbShowPasswrod.Text = "Show Password";
            this.cbShowPasswrod.UseVisualStyleBackColor = true;
            this.cbShowPasswrod.Click += new System.EventHandler(this.cbShowPasswrod_CheckedChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.White;
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.Font = new System.Drawing.Font("Nirmala UI", 14F);
            this.tbPassword.Location = new System.Drawing.Point(27, 117);
            this.tbPassword.Multiline = true;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '•';
            this.tbPassword.Size = new System.Drawing.Size(200, 30);
            this.tbPassword.TabIndex = 38;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbLogin_KeyDown);
            // 
            // tbId
            // 
            this.tbId.BackColor = System.Drawing.Color.White;
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Font = new System.Drawing.Font("Nirmala UI", 14F);
            this.tbId.Location = new System.Drawing.Point(27, 58);
            this.tbId.Multiline = true;
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(200, 30);
            this.tbId.TabIndex = 37;
            this.tbId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbLogin_KeyDown);
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.lPassword.Location = new System.Drawing.Point(23, 91);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(77, 19);
            this.lPassword.TabIndex = 36;
            this.lPassword.Text = "Passwrod:";
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lId.ForeColor = System.Drawing.Color.DarkGray;
            this.lId.Location = new System.Drawing.Point(23, 32);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(27, 19);
            this.lId.TabIndex = 35;
            this.lId.Text = "ID:";
            // 
            // lLogin
            // 
            this.lLogin.AutoSize = true;
            this.lLogin.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(132)))), ((int)(((byte)(92)))));
            this.lLogin.Location = new System.Drawing.Point(82, 0);
            this.lLogin.Name = "lLogin";
            this.lLogin.Size = new System.Drawing.Size(87, 32);
            this.lLogin.TabIndex = 34;
            this.lLogin.Text = "LOGIN";
            // 
            // cbProducts
            // 
            this.cbProducts.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProducts.FormattingEnabled = true;
            this.cbProducts.Items.AddRange(new object[] {
            "Products",
            "SAS4.0",
            "PCIe4.0",
            "QSFP28"});
            this.cbProducts.Location = new System.Drawing.Point(27, 172);
            this.cbProducts.Name = "cbProducts";
            this.cbProducts.Size = new System.Drawing.Size(200, 29);
            this.cbProducts.TabIndex = 54;
            // 
            // lProduct
            // 
            this.lProduct.AutoSize = true;
            this.lProduct.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lProduct.ForeColor = System.Drawing.Color.DarkGray;
            this.lProduct.Location = new System.Drawing.Point(23, 150);
            this.lProduct.Name = "lProduct";
            this.lProduct.Size = new System.Drawing.Size(66, 19);
            this.lProduct.TabIndex = 55;
            this.lProduct.Text = "Product:";
            // 
            // lBackToMain
            // 
            this.lBackToMain.AutoSize = true;
            this.lBackToMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lBackToMain.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBackToMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(183)))), ((int)(((byte)(43)))));
            this.lBackToMain.Location = new System.Drawing.Point(62, 333);
            this.lBackToMain.Name = "lBackToMain";
            this.lBackToMain.Size = new System.Drawing.Size(131, 19);
            this.lBackToMain.TabIndex = 57;
            this.lBackToMain.Text = "Back to MainForm";
            this.lBackToMain.Click += new System.EventHandler(this.lBackToMain_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(250, 350);
            this.Controls.Add(this.lBackToMain);
            this.Controls.Add(this.lProduct);
            this.Controls.Add(this.cbProducts);
            this.Controls.Add(this.lExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.cbShowPasswrod);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lId);
            this.Controls.Add(this.lLogin);
            this.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LoginForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label lExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.CheckBox cbShowPasswrod;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.Label lLogin;
        private System.Windows.Forms.ComboBox cbProducts;
        private System.Windows.Forms.Label lProduct;
        private System.Windows.Forms.Label lBackToMain;
    }
}