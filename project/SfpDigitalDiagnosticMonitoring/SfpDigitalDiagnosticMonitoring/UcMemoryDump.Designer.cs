namespace SfpDigitalDiagnosticMonitoring
{
    partial class UcMemoryDump
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
            if (disposing && (components != null)) {
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
            this.bWrite = new System.Windows.Forms.Button();
            this.bRead = new System.Windows.Forms.Button();
            this.dgvMemory = new System.Windows.Forms.DataGridView();
            this.cbAddrSelect = new System.Windows.Forms.ComboBox();
            this.lAddr = new System.Windows.Forms.Label();
            this.cbPageSelect = new System.Windows.Forms.ComboBox();
            this.lPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).BeginInit();
            this.SuspendLayout();
            // 
            // bWrite
            // 
            this.bWrite.Location = new System.Drawing.Point(774, 100);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(75, 23);
            this.bWrite.TabIndex = 9;
            this.bWrite.Text = "Write";
            this.bWrite.UseVisualStyleBackColor = true;
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(774, 59);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(75, 23);
            this.bRead.TabIndex = 8;
            this.bRead.Text = "Read";
            this.bRead.UseVisualStyleBackColor = true;
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // dgvMemory
            // 
            this.dgvMemory.AllowUserToAddRows = false;
            this.dgvMemory.AllowUserToDeleteRows = false;
            this.dgvMemory.AllowUserToResizeColumns = false;
            this.dgvMemory.AllowUserToResizeRows = false;
            this.dgvMemory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemory.Location = new System.Drawing.Point(3, 3);
            this.dgvMemory.Name = "dgvMemory";
            this.dgvMemory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMemory.RowTemplate.Height = 24;
            this.dgvMemory.Size = new System.Drawing.Size(724, 214);
            this.dgvMemory.TabIndex = 7;
            // 
            // cbAddrSelect
            // 
            this.cbAddrSelect.FormattingEnabled = true;
            this.cbAddrSelect.Location = new System.Drawing.Point(769, 6);
            this.cbAddrSelect.Name = "cbAddrSelect";
            this.cbAddrSelect.Size = new System.Drawing.Size(80, 20);
            this.cbAddrSelect.TabIndex = 6;
            this.cbAddrSelect.SelectedIndexChanged += new System.EventHandler(this.cbAddrSelect_SelectedIndexChanged);
            // 
            // lAddr
            // 
            this.lAddr.AutoSize = true;
            this.lAddr.Location = new System.Drawing.Point(733, 9);
            this.lAddr.Name = "lAddr";
            this.lAddr.Size = new System.Drawing.Size(32, 12);
            this.lAddr.TabIndex = 5;
            this.lAddr.Text = "Addr:";
            // 
            // cbPageSelect
            // 
            this.cbPageSelect.Enabled = false;
            this.cbPageSelect.FormattingEnabled = true;
            this.cbPageSelect.Location = new System.Drawing.Point(769, 32);
            this.cbPageSelect.Name = "cbPageSelect";
            this.cbPageSelect.Size = new System.Drawing.Size(80, 20);
            this.cbPageSelect.TabIndex = 11;
            // 
            // lPage
            // 
            this.lPage.AutoSize = true;
            this.lPage.Location = new System.Drawing.Point(733, 35);
            this.lPage.Name = "lPage";
            this.lPage.Size = new System.Drawing.Size(30, 12);
            this.lPage.TabIndex = 10;
            this.lPage.Text = "Page:";
            // 
            // UcMemoryDump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbPageSelect);
            this.Controls.Add(this.lPage);
            this.Controls.Add(this.bWrite);
            this.Controls.Add(this.bRead);
            this.Controls.Add(this.dgvMemory);
            this.Controls.Add(this.cbAddrSelect);
            this.Controls.Add(this.lAddr);
            this.Name = "UcMemoryDump";
            this.Size = new System.Drawing.Size(855, 385);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bWrite;
        private System.Windows.Forms.Button bRead;
        private System.Windows.Forms.DataGridView dgvMemory;
        private System.Windows.Forms.ComboBox cbAddrSelect;
        private System.Windows.Forms.Label lAddr;
        private System.Windows.Forms.ComboBox cbPageSelect;
        private System.Windows.Forms.Label lPage;
    }
}
