namespace AardvarkAdapter
{
    partial class FunctionTestPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lDevAddr = new System.Windows.Forms.Label();
            this.tbDevAddr = new System.Windows.Forms.TextBox();
            this.lRegAddr = new System.Windows.Forms.Label();
            this.tbRegAddr = new System.Windows.Forms.TextBox();
            this.cbConnectState = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSignal = new System.Windows.Forms.TabPage();
            this.cbTriggerRead = new System.Windows.Forms.CheckBox();
            this.tbTriggerDelay = new System.Windows.Forms.TextBox();
            this.lTriggerDelay = new System.Windows.Forms.Label();
            this.bSignalWrite = new System.Windows.Forms.Button();
            this.bSignalRead = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lValue = new System.Windows.Forms.Label();
            this.tpMulti = new System.Windows.Forms.TabPage();
            this.dgvMultiRegister = new System.Windows.Forms.DataGridView();
            this.bWrite = new System.Windows.Forms.Button();
            this.bMultiRead = new System.Windows.Forms.Button();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.lLength = new System.Windows.Forms.Label();
            this.lPort = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.lBitrate = new System.Windows.Forms.Label();
            this.tbBitrate = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tpSignal.SuspendLayout();
            this.tpMulti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiRegister)).BeginInit();
            this.SuspendLayout();
            // 
            // lDevAddr
            // 
            this.lDevAddr.AutoSize = true;
            this.lDevAddr.Location = new System.Drawing.Point(10, 40);
            this.lDevAddr.Name = "lDevAddr";
            this.lDevAddr.Size = new System.Drawing.Size(57, 12);
            this.lDevAddr.TabIndex = 0;
            this.lDevAddr.Text = "Dev Addr :";
            // 
            // tbDevAddr
            // 
            this.tbDevAddr.Location = new System.Drawing.Point(73, 37);
            this.tbDevAddr.Name = "tbDevAddr";
            this.tbDevAddr.Size = new System.Drawing.Size(30, 22);
            this.tbDevAddr.TabIndex = 1;
            this.tbDevAddr.Text = "80";
            // 
            // lRegAddr
            // 
            this.lRegAddr.AutoSize = true;
            this.lRegAddr.Location = new System.Drawing.Point(109, 40);
            this.lRegAddr.Name = "lRegAddr";
            this.lRegAddr.Size = new System.Drawing.Size(57, 12);
            this.lRegAddr.TabIndex = 2;
            this.lRegAddr.Text = "Reg Addr :";
            // 
            // tbRegAddr
            // 
            this.tbRegAddr.Location = new System.Drawing.Point(172, 37);
            this.tbRegAddr.Name = "tbRegAddr";
            this.tbRegAddr.Size = new System.Drawing.Size(30, 22);
            this.tbRegAddr.TabIndex = 3;
            this.tbRegAddr.Text = "0";
            // 
            // cbConnectState
            // 
            this.cbConnectState.AutoSize = true;
            this.cbConnectState.Location = new System.Drawing.Point(166, 9);
            this.cbConnectState.Name = "cbConnectState";
            this.cbConnectState.Size = new System.Drawing.Size(74, 16);
            this.cbConnectState.TabIndex = 4;
            this.cbConnectState.Text = "Connected";
            this.cbConnectState.UseVisualStyleBackColor = true;
            this.cbConnectState.CheckedChanged += new System.EventHandler(this._cbConnectStateCheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSignal);
            this.tabControl1.Controls.Add(this.tpMulti);
            this.tabControl1.Location = new System.Drawing.Point(3, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(237, 215);
            this.tabControl1.TabIndex = 5;
            // 
            // tpSignal
            // 
            this.tpSignal.Controls.Add(this.cbTriggerRead);
            this.tpSignal.Controls.Add(this.tbTriggerDelay);
            this.tpSignal.Controls.Add(this.lTriggerDelay);
            this.tpSignal.Controls.Add(this.bSignalWrite);
            this.tpSignal.Controls.Add(this.bSignalRead);
            this.tpSignal.Controls.Add(this.tbValue);
            this.tpSignal.Controls.Add(this.lValue);
            this.tpSignal.Location = new System.Drawing.Point(4, 22);
            this.tpSignal.Name = "tpSignal";
            this.tpSignal.Padding = new System.Windows.Forms.Padding(3);
            this.tpSignal.Size = new System.Drawing.Size(229, 189);
            this.tpSignal.TabIndex = 0;
            this.tpSignal.Text = "Signal";
            this.tpSignal.UseVisualStyleBackColor = true;
            // 
            // cbTriggerRead
            // 
            this.cbTriggerRead.AutoSize = true;
            this.cbTriggerRead.Location = new System.Drawing.Point(10, 63);
            this.cbTriggerRead.Name = "cbTriggerRead";
            this.cbTriggerRead.Size = new System.Drawing.Size(86, 16);
            this.cbTriggerRead.TabIndex = 6;
            this.cbTriggerRead.Text = "Trigger Read";
            this.cbTriggerRead.UseVisualStyleBackColor = true;
            this.cbTriggerRead.CheckedChanged += new System.EventHandler(this.cbTriggerRead_CheckedChanged);
            // 
            // tbTriggerDelay
            // 
            this.tbTriggerDelay.Location = new System.Drawing.Point(113, 35);
            this.tbTriggerDelay.Name = "tbTriggerDelay";
            this.tbTriggerDelay.Size = new System.Drawing.Size(60, 22);
            this.tbTriggerDelay.TabIndex = 5;
            this.tbTriggerDelay.Text = "20";
            // 
            // lTriggerDelay
            // 
            this.lTriggerDelay.AutoSize = true;
            this.lTriggerDelay.Location = new System.Drawing.Point(10, 38);
            this.lTriggerDelay.Name = "lTriggerDelay";
            this.lTriggerDelay.Size = new System.Drawing.Size(97, 12);
            this.lTriggerDelay.TabIndex = 4;
            this.lTriggerDelay.Text = "Trigger Delay (ms):";
            // 
            // bSignalWrite
            // 
            this.bSignalWrite.Location = new System.Drawing.Point(161, 6);
            this.bSignalWrite.Name = "bSignalWrite";
            this.bSignalWrite.Size = new System.Drawing.Size(65, 23);
            this.bSignalWrite.TabIndex = 3;
            this.bSignalWrite.Text = "Write";
            this.bSignalWrite.UseVisualStyleBackColor = true;
            this.bSignalWrite.Click += new System.EventHandler(this.bSignalWrite_Click);
            // 
            // bSignalRead
            // 
            this.bSignalRead.Location = new System.Drawing.Point(90, 6);
            this.bSignalRead.Name = "bSignalRead";
            this.bSignalRead.Size = new System.Drawing.Size(65, 23);
            this.bSignalRead.TabIndex = 2;
            this.bSignalRead.Text = "Read";
            this.bSignalRead.UseVisualStyleBackColor = true;
            this.bSignalRead.Click += new System.EventHandler(this.bSignalRead_Click);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(54, 6);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(30, 22);
            this.tbValue.TabIndex = 1;
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Location = new System.Drawing.Point(10, 10);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(38, 12);
            this.lValue.TabIndex = 0;
            this.lValue.Text = "Value :";
            // 
            // tpMulti
            // 
            this.tpMulti.Controls.Add(this.dgvMultiRegister);
            this.tpMulti.Controls.Add(this.bWrite);
            this.tpMulti.Controls.Add(this.bMultiRead);
            this.tpMulti.Controls.Add(this.tbLength);
            this.tpMulti.Controls.Add(this.lLength);
            this.tpMulti.Location = new System.Drawing.Point(4, 22);
            this.tpMulti.Name = "tpMulti";
            this.tpMulti.Padding = new System.Windows.Forms.Padding(3);
            this.tpMulti.Size = new System.Drawing.Size(229, 189);
            this.tpMulti.TabIndex = 1;
            this.tpMulti.Text = "Multi";
            this.tpMulti.UseVisualStyleBackColor = true;
            // 
            // dgvMultiRegister
            // 
            this.dgvMultiRegister.AllowUserToAddRows = false;
            this.dgvMultiRegister.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvMultiRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMultiRegister.Location = new System.Drawing.Point(83, 3);
            this.dgvMultiRegister.Name = "dgvMultiRegister";
            this.dgvMultiRegister.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvMultiRegister.RowTemplate.Height = 24;
            this.dgvMultiRegister.Size = new System.Drawing.Size(143, 183);
            this.dgvMultiRegister.TabIndex = 4;
            // 
            // bWrite
            // 
            this.bWrite.Location = new System.Drawing.Point(12, 64);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(65, 23);
            this.bWrite.TabIndex = 3;
            this.bWrite.Text = "Write";
            this.bWrite.UseVisualStyleBackColor = true;
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // bMultiRead
            // 
            this.bMultiRead.Location = new System.Drawing.Point(12, 35);
            this.bMultiRead.Name = "bMultiRead";
            this.bMultiRead.Size = new System.Drawing.Size(65, 23);
            this.bMultiRead.TabIndex = 2;
            this.bMultiRead.Text = "Read";
            this.bMultiRead.UseVisualStyleBackColor = true;
            this.bMultiRead.Click += new System.EventHandler(this._bMultiReadClick);
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(53, 7);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(30, 22);
            this.tbLength.TabIndex = 1;
            this.tbLength.Text = "6";
            // 
            // lLength
            // 
            this.lLength.AutoSize = true;
            this.lLength.Location = new System.Drawing.Point(10, 10);
            this.lLength.Name = "lLength";
            this.lLength.Size = new System.Drawing.Size(44, 12);
            this.lLength.TabIndex = 0;
            this.lLength.Text = "Length :";
            // 
            // lPort
            // 
            this.lPort.AutoSize = true;
            this.lPort.Location = new System.Drawing.Point(10, 10);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(30, 12);
            this.lPort.TabIndex = 6;
            this.lPort.Text = "Port :";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(46, 7);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(30, 22);
            this.tbPort.TabIndex = 7;
            this.tbPort.Text = "0";
            // 
            // lBitrate
            // 
            this.lBitrate.AutoSize = true;
            this.lBitrate.Location = new System.Drawing.Point(82, 10);
            this.lBitrate.Name = "lBitrate";
            this.lBitrate.Size = new System.Drawing.Size(42, 12);
            this.lBitrate.TabIndex = 8;
            this.lBitrate.Text = "Bitrate :";
            // 
            // tbBitrate
            // 
            this.tbBitrate.Location = new System.Drawing.Point(130, 7);
            this.tbBitrate.Name = "tbBitrate";
            this.tbBitrate.Size = new System.Drawing.Size(30, 22);
            this.tbBitrate.TabIndex = 9;
            this.tbBitrate.Text = "100";
            // 
            // FunctionTestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbBitrate);
            this.Controls.Add(this.lBitrate);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.lPort);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbConnectState);
            this.Controls.Add(this.tbRegAddr);
            this.Controls.Add(this.lRegAddr);
            this.Controls.Add(this.tbDevAddr);
            this.Controls.Add(this.lDevAddr);
            this.MaximumSize = new System.Drawing.Size(243, 283);
            this.MinimumSize = new System.Drawing.Size(243, 283);
            this.Name = "FunctionTestPanel";
            this.Size = new System.Drawing.Size(243, 283);
            this.tabControl1.ResumeLayout(false);
            this.tpSignal.ResumeLayout(false);
            this.tpSignal.PerformLayout();
            this.tpMulti.ResumeLayout(false);
            this.tpMulti.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiRegister)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDevAddr;
        private System.Windows.Forms.TextBox tbDevAddr;
        private System.Windows.Forms.Label lRegAddr;
        private System.Windows.Forms.TextBox tbRegAddr;
        private System.Windows.Forms.CheckBox cbConnectState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSignal;
        private System.Windows.Forms.Button bSignalRead;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.TabPage tpMulti;
        private System.Windows.Forms.Button bSignalWrite;
        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label lBitrate;
        private System.Windows.Forms.TextBox tbBitrate;
        private System.Windows.Forms.Button bMultiRead;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label lLength;
        private System.Windows.Forms.DataGridView dgvMultiRegister;
        private System.Windows.Forms.Button bWrite;
        private System.Windows.Forms.CheckBox cbTriggerRead;
        private System.Windows.Forms.TextBox tbTriggerDelay;
        private System.Windows.Forms.Label lTriggerDelay;
    }
}
