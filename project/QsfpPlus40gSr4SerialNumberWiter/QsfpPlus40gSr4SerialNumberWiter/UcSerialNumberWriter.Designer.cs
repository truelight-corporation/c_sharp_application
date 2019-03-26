namespace QsfpPlus40gSr4SerialNumberWiter
{
    partial class UcSerialNumberWriter
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
            this.bWriteXaSerialNumber = new System.Windows.Forms.Button();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.lStatus = new System.Windows.Forms.Label();
            this.lTableSavePath = new System.Windows.Forms.Label();
            this.tbTableSavePath = new System.Windows.Forms.TextBox();
            this.bSaveTable = new System.Windows.Forms.Button();
            this.tbPassword126 = new System.Windows.Forms.TextBox();
            this.tbPassword125 = new System.Windows.Forms.TextBox();
            this.tbPassword124 = new System.Windows.Forms.TextBox();
            this.l = new System.Windows.Forms.Label();
            this.tbPassword123 = new System.Windows.Forms.TextBox();
            this.bDelRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
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
            this.tbYcSerialNumber.TextChanged += new System.EventHandler(this.tbYcSerialNumber_TextChanged);
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
            this.tbXaSerialNumber.TextChanged += new System.EventHandler(this.tbXaSerialNumber_TextChanged);
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
            // bWriteXaSerialNumber
            // 
            this.bWriteXaSerialNumber.Enabled = false;
            this.bWriteXaSerialNumber.Location = new System.Drawing.Point(588, 96);
            this.bWriteXaSerialNumber.Name = "bWriteXaSerialNumber";
            this.bWriteXaSerialNumber.Size = new System.Drawing.Size(124, 84);
            this.bWriteXaSerialNumber.TabIndex = 8;
            this.bWriteXaSerialNumber.Text = "寫入";
            this.bWriteXaSerialNumber.UseVisualStyleBackColor = true;
            this.bWriteXaSerialNumber.Click += new System.EventHandler(this.bWriteXaSerialNumber_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Location = new System.Drawing.Point(3, 189);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTable.RowTemplate.Height = 24;
            this.dgvTable.Size = new System.Drawing.Size(709, 303);
            this.dgvTable.TabIndex = 9;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(588, 51);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(0, 24);
            this.lStatus.TabIndex = 10;
            // 
            // lTableSavePath
            // 
            this.lTableSavePath.AutoSize = true;
            this.lTableSavePath.Location = new System.Drawing.Point(129, 507);
            this.lTableSavePath.Name = "lTableSavePath";
            this.lTableSavePath.Size = new System.Drawing.Size(112, 24);
            this.lTableSavePath.TabIndex = 11;
            this.lTableSavePath.Text = "存檔路徑:";
            this.lTableSavePath.UseMnemonic = false;
            // 
            // tbTableSavePath
            // 
            this.tbTableSavePath.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbTableSavePath.Location = new System.Drawing.Point(251, 501);
            this.tbTableSavePath.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbTableSavePath.Name = "tbTableSavePath";
            this.tbTableSavePath.ReadOnly = true;
            this.tbTableSavePath.Size = new System.Drawing.Size(327, 36);
            this.tbTableSavePath.TabIndex = 12;
            // 
            // bSaveTable
            // 
            this.bSaveTable.Location = new System.Drawing.Point(588, 501);
            this.bSaveTable.Name = "bSaveTable";
            this.bSaveTable.Size = new System.Drawing.Size(124, 36);
            this.bSaveTable.TabIndex = 13;
            this.bSaveTable.Text = "存檔";
            this.bSaveTable.UseVisualStyleBackColor = true;
            this.bSaveTable.Click += new System.EventHandler(this.bSaveTable_Click);
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
            this.tbPassword126.TextChanged += new System.EventHandler(this.tbPassword126_TextChanged);
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
            this.tbPassword125.TextChanged += new System.EventHandler(this.tbPassword125_TextChanged);
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
            // l
            // 
            this.l.AutoSize = true;
            this.l.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.l.Location = new System.Drawing.Point(15, 6);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(112, 24);
            this.l.TabIndex = 46;
            this.l.Text = "模組密碼:";
            this.l.Click += new System.EventHandler(this.l_Click);
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
            // bDelRecord
            // 
            this.bDelRecord.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bDelRecord.Location = new System.Drawing.Point(3, 501);
            this.bDelRecord.Name = "bDelRecord";
            this.bDelRecord.Size = new System.Drawing.Size(120, 36);
            this.bDelRecord.TabIndex = 93;
            this.bDelRecord.Text = "刪除紀錄";
            this.bDelRecord.UseVisualStyleBackColor = true;
            this.bDelRecord.Click += new System.EventHandler(this.bDelRecord_Click);
            // 
            // UcSerialNumberWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbYcSerialNumber);
            this.Controls.Add(this.bDelRecord);
            this.Controls.Add(this.tbPassword126);
            this.Controls.Add(this.tbPassword125);
            this.Controls.Add(this.tbPassword124);
            this.Controls.Add(this.l);
            this.Controls.Add(this.tbPassword123);
            this.Controls.Add(this.bSaveTable);
            this.Controls.Add(this.tbTableSavePath);
            this.Controls.Add(this.lTableSavePath);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.bWriteXaSerialNumber);
            this.Controls.Add(this.tbXaSerialNumber);
            this.Controls.Add(this.lCustomerSerialNumber);
            this.Controls.Add(this.tbNewSerialNumber);
            this.Controls.Add(this.lXaSerialNumber);
            this.Controls.Add(this.lYcSerialNumber);
            this.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "UcSerialNumberWriter";
            this.Size = new System.Drawing.Size(719, 541);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
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
        private System.Windows.Forms.Button bWriteXaSerialNumber;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label lTableSavePath;
        private System.Windows.Forms.TextBox tbTableSavePath;
        private System.Windows.Forms.Button bSaveTable;
        private System.Windows.Forms.TextBox tbPassword126;
        private System.Windows.Forms.TextBox tbPassword125;
        private System.Windows.Forms.TextBox tbPassword124;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.TextBox tbPassword123;
        private System.Windows.Forms.Button bDelRecord;
    }
}
