namespace QsfpPlus40gSr4SerialNumberWiter
{
    partial class UcConfigBackup
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbYcSerialNumber = new System.Windows.Forms.TextBox();
            this.lYcSerialNumber = new System.Windows.Forms.Label();
            this.tbNewSerialNumber = new System.Windows.Forms.TextBox();
            this.lXaSerialNumber = new System.Windows.Forms.Label();
            this.tbXaSerialNumber = new System.Windows.Forms.TextBox();
            this.lCustomerSerialNumber = new System.Windows.Forms.Label();
            this.bBackupConfigToFile = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.lTableSavePath = new System.Windows.Forms.Label();
            this.tbTableSavePath = new System.Windows.Forms.TextBox();
            this.tbPassword126 = new System.Windows.Forms.TextBox();
            this.tbPassword125 = new System.Windows.Forms.TextBox();
            this.tbPassword124 = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbPassword123 = new System.Windows.Forms.TextBox();
            this.bRestoreConfigFromFile = new System.Windows.Forms.Button();
            this.cbBackupOrRestoreSelect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbYcSerialNumber
            // 
            this.tbYcSerialNumber.AcceptsReturn = true;
            this.tbYcSerialNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbYcSerialNumber.Location = new System.Drawing.Point(149, 48);
            this.tbYcSerialNumber.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbYcSerialNumber.Name = "tbYcSerialNumber";
            this.tbYcSerialNumber.ReadOnly = true;
            this.tbYcSerialNumber.Size = new System.Drawing.Size(429, 36);
            this.tbYcSerialNumber.TabIndex = 3;
            // 
            // lYcSerialNumber
            // 
            this.lYcSerialNumber.AutoSize = true;
            this.lYcSerialNumber.Location = new System.Drawing.Point(15, 51);
            this.lYcSerialNumber.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lYcSerialNumber.Name = "lYcSerialNumber";
            this.lYcSerialNumber.Size = new System.Drawing.Size(95, 24);
            this.lYcSerialNumber.TabIndex = 2;
            this.lYcSerialNumber.Text = "YC序號:";
            // 
            // tbNewSerialNumber
            // 
            this.tbNewSerialNumber.BackColor = System.Drawing.Color.YellowGreen;
            this.tbNewSerialNumber.Location = new System.Drawing.Point(149, 144);
            this.tbNewSerialNumber.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbNewSerialNumber.Name = "tbNewSerialNumber";
            this.tbNewSerialNumber.Size = new System.Drawing.Size(429, 36);
            this.tbNewSerialNumber.TabIndex = 5;
            // 
            // lXaSerialNumber
            // 
            this.lXaSerialNumber.AutoSize = true;
            this.lXaSerialNumber.Location = new System.Drawing.Point(15, 147);
            this.lXaSerialNumber.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lXaSerialNumber.Name = "lXaSerialNumber";
            this.lXaSerialNumber.Size = new System.Drawing.Size(112, 24);
            this.lXaSerialNumber.TabIndex = 4;
            this.lXaSerialNumber.Text = "寫入序號:";
            // 
            // tbXaSerialNumber
            // 
            this.tbXaSerialNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbXaSerialNumber.Location = new System.Drawing.Point(149, 96);
            this.tbXaSerialNumber.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbXaSerialNumber.Name = "tbXaSerialNumber";
            this.tbXaSerialNumber.ReadOnly = true;
            this.tbXaSerialNumber.Size = new System.Drawing.Size(429, 36);
            this.tbXaSerialNumber.TabIndex = 7;
            // 
            // lCustomerSerialNumber
            // 
            this.lCustomerSerialNumber.AutoSize = true;
            this.lCustomerSerialNumber.Location = new System.Drawing.Point(15, 99);
            this.lCustomerSerialNumber.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lCustomerSerialNumber.Name = "lCustomerSerialNumber";
            this.lCustomerSerialNumber.Size = new System.Drawing.Size(96, 24);
            this.lCustomerSerialNumber.TabIndex = 6;
            this.lCustomerSerialNumber.Text = "XA序號:";
            // 
            // bBackupConfigToFile
            // 
            this.bBackupConfigToFile.Location = new System.Drawing.Point(588, 48);
            this.bBackupConfigToFile.Name = "bBackupConfigToFile";
            this.bBackupConfigToFile.Size = new System.Drawing.Size(124, 60);
            this.bBackupConfigToFile.TabIndex = 8;
            this.bBackupConfigToFile.Text = "備份";
            this.bBackupConfigToFile.UseVisualStyleBackColor = true;
            this.bBackupConfigToFile.Click += new System.EventHandler(this.bBackupConfigToFile_Click);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(496, 7);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(0, 24);
            this.lStatus.TabIndex = 10;
            // 
            // lTableSavePath
            // 
            this.lTableSavePath.AutoSize = true;
            this.lTableSavePath.Location = new System.Drawing.Point(15, 195);
            this.lTableSavePath.Name = "lTableSavePath";
            this.lTableSavePath.Size = new System.Drawing.Size(112, 24);
            this.lTableSavePath.TabIndex = 11;
            this.lTableSavePath.Text = "存檔路徑:";
            this.lTableSavePath.UseMnemonic = false;
            // 
            // tbTableSavePath
            // 
            this.tbTableSavePath.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbTableSavePath.Location = new System.Drawing.Point(149, 192);
            this.tbTableSavePath.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbTableSavePath.Name = "tbTableSavePath";
            this.tbTableSavePath.ReadOnly = true;
            this.tbTableSavePath.Size = new System.Drawing.Size(563, 36);
            this.tbTableSavePath.TabIndex = 12;
            // 
            // tbPassword126
            // 
            this.tbPassword126.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPassword126.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbPassword126.Location = new System.Drawing.Point(347, 3);
            this.tbPassword126.Name = "tbPassword126";
            this.tbPassword126.Size = new System.Drawing.Size(60, 36);
            this.tbPassword126.TabIndex = 49;
            this.tbPassword126.Text = "34";
            this.tbPassword126.UseSystemPasswordChar = true;
            // 
            // tbPassword125
            // 
            this.tbPassword125.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPassword125.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbPassword125.Location = new System.Drawing.Point(279, 3);
            this.tbPassword125.Name = "tbPassword125";
            this.tbPassword125.Size = new System.Drawing.Size(60, 36);
            this.tbPassword125.TabIndex = 48;
            this.tbPassword125.Text = "33";
            this.tbPassword125.UseSystemPasswordChar = true;
            // 
            // tbPassword124
            // 
            this.tbPassword124.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPassword124.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbPassword124.Location = new System.Drawing.Point(215, 3);
            this.tbPassword124.Name = "tbPassword124";
            this.tbPassword124.Size = new System.Drawing.Size(60, 36);
            this.tbPassword124.TabIndex = 47;
            this.tbPassword124.Text = "32";
            this.tbPassword124.UseSystemPasswordChar = true;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lPassword.Location = new System.Drawing.Point(15, 6);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(112, 24);
            this.lPassword.TabIndex = 46;
            this.lPassword.Text = "模組密碼:";
            // 
            // tbPassword123
            // 
            this.tbPassword123.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPassword123.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbPassword123.Location = new System.Drawing.Point(149, 3);
            this.tbPassword123.Name = "tbPassword123";
            this.tbPassword123.Size = new System.Drawing.Size(60, 36);
            this.tbPassword123.TabIndex = 45;
            this.tbPassword123.Text = "33";
            this.tbPassword123.UseSystemPasswordChar = true;
            // 
            // bRestoreConfigFromFile
            // 
            this.bRestoreConfigFromFile.Enabled = false;
            this.bRestoreConfigFromFile.Location = new System.Drawing.Point(588, 120);
            this.bRestoreConfigFromFile.Name = "bRestoreConfigFromFile";
            this.bRestoreConfigFromFile.Size = new System.Drawing.Size(124, 60);
            this.bRestoreConfigFromFile.TabIndex = 94;
            this.bRestoreConfigFromFile.Text = "寫回";
            this.bRestoreConfigFromFile.UseVisualStyleBackColor = true;
            this.bRestoreConfigFromFile.Click += new System.EventHandler(this.bRestoreConfigFromFile_Click);
            // 
            // cbBackupOrRestoreSelect
            // 
            this.cbBackupOrRestoreSelect.AutoSize = true;
            this.cbBackupOrRestoreSelect.Checked = true;
            this.cbBackupOrRestoreSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBackupOrRestoreSelect.Location = new System.Drawing.Point(413, 6);
            this.cbBackupOrRestoreSelect.Name = "cbBackupOrRestoreSelect";
            this.cbBackupOrRestoreSelect.Size = new System.Drawing.Size(77, 28);
            this.cbBackupOrRestoreSelect.TabIndex = 95;
            this.cbBackupOrRestoreSelect.Text = "模式";
            this.cbBackupOrRestoreSelect.UseVisualStyleBackColor = true;
            this.cbBackupOrRestoreSelect.CheckedChanged += new System.EventHandler(this.cbBackupOrRestoreSelect_CheckedChanged);
            // 
            // UcConfigBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBackupOrRestoreSelect);
            this.Controls.Add(this.bRestoreConfigFromFile);
            this.Controls.Add(this.tbYcSerialNumber);
            this.Controls.Add(this.tbPassword126);
            this.Controls.Add(this.tbPassword125);
            this.Controls.Add(this.tbPassword124);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.tbPassword123);
            this.Controls.Add(this.tbTableSavePath);
            this.Controls.Add(this.lTableSavePath);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.bBackupConfigToFile);
            this.Controls.Add(this.tbXaSerialNumber);
            this.Controls.Add(this.lCustomerSerialNumber);
            this.Controls.Add(this.tbNewSerialNumber);
            this.Controls.Add(this.lXaSerialNumber);
            this.Controls.Add(this.lYcSerialNumber);
            this.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "UcConfigBackup";
            this.Size = new System.Drawing.Size(719, 233);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbYcSerialNumber;
        private System.Windows.Forms.Label lYcSerialNumber;
        private System.Windows.Forms.TextBox tbNewSerialNumber;
        private System.Windows.Forms.Label lXaSerialNumber;
        private System.Windows.Forms.TextBox tbXaSerialNumber;
        private System.Windows.Forms.Label lCustomerSerialNumber;
        private System.Windows.Forms.Button bBackupConfigToFile;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label lTableSavePath;
        private System.Windows.Forms.TextBox tbTableSavePath;
        private System.Windows.Forms.TextBox tbPassword126;
        private System.Windows.Forms.TextBox tbPassword125;
        private System.Windows.Forms.TextBox tbPassword124;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tbPassword123;
        private System.Windows.Forms.Button bRestoreConfigFromFile;
        private System.Windows.Forms.CheckBox cbBackupOrRestoreSelect;
    }
}
