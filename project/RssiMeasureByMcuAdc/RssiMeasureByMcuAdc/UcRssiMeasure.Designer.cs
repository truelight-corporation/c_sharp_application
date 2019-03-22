namespace RssiMeasureByMcuAdc
{
    partial class UcRssiMeasure
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcFunctionSelect = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tbRxCurrent4 = new System.Windows.Forms.TextBox();
            this.lRxCurrent4 = new System.Windows.Forms.Label();
            this.tbRxCurrent3 = new System.Windows.Forms.TextBox();
            this.lRxCurrent3 = new System.Windows.Forms.Label();
            this.tbRxCurrent2 = new System.Windows.Forms.TextBox();
            this.lRxCurrent2 = new System.Windows.Forms.Label();
            this.lClassification = new System.Windows.Forms.Label();
            this.bDelRecord = new System.Windows.Forms.Button();
            this.bSaveFile = new System.Windows.Forms.Button();
            this.tbLogFilePath = new System.Windows.Forms.TextBox();
            this.lLogFilePath = new System.Windows.Forms.Label();
            this.dgvRecord = new System.Windows.Forms.DataGridView();
            this.tbRxCurrent1 = new System.Windows.Forms.TextBox();
            this.lRxCurrent1 = new System.Windows.Forms.Label();
            this.bLog = new System.Windows.Forms.Button();
            this.tbSerialNumber = new System.Windows.Forms.TextBox();
            this.lSnNumber = new System.Windows.Forms.Label();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.lAction = new System.Windows.Forms.Label();
            this.lResult = new System.Windows.Forms.Label();
            this.cbLogMode = new System.Windows.Forms.ComboBox();
            this.lLogMode = new System.Windows.Forms.Label();
            this.bOpenConfigFile = new System.Windows.Forms.Button();
            this.lConfigFile = new System.Windows.Forms.Label();
            this.tbConfigFilePath = new System.Windows.Forms.TextBox();
            this.lOperator = new System.Windows.Forms.Label();
            this.lSubLotNumber = new System.Windows.Forms.Label();
            this.lLotNumber = new System.Windows.Forms.Label();
            this.lLogLable = new System.Windows.Forms.Label();
            this.tbLotNumber = new System.Windows.Forms.TextBox();
            this.tbSubLotNumber = new System.Windows.Forms.TextBox();
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.tbLogLable = new System.Windows.Forms.TextBox();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.bSaveConfig = new System.Windows.Forms.Button();
            this.gbMonitorThreshold = new System.Windows.Forms.GroupBox();
            this.lCurrentThreshold = new System.Windows.Forms.Label();
            this.tbCurrent4Threshold = new System.Windows.Forms.TextBox();
            this.tbCurrent1Threshold = new System.Windows.Forms.TextBox();
            this.tbCurrent2Threshold = new System.Windows.Forms.TextBox();
            this.tbCurrent3Threshold = new System.Windows.Forms.TextBox();
            this.lCh1Threshold = new System.Windows.Forms.Label();
            this.lCh2Threshold = new System.Windows.Forms.Label();
            this.lCh3Threshold = new System.Windows.Forms.Label();
            this.lCh4Threshold = new System.Windows.Forms.Label();
            this.gbI2cConfig = new System.Windows.Forms.GroupBox();
            this.lLightSourceI2cConfig = new System.Windows.Forms.Label();
            this.tbI2cRssiRegisterAddr = new System.Windows.Forms.TextBox();
            this.lBeTestDevAddr = new System.Windows.Forms.Label();
            this.tbI2cRssiRegisterPage = new System.Windows.Forms.TextBox();
            this.tbI2cLightSourseDevAddr = new System.Windows.Forms.TextBox();
            this.tbI2cRssiDevAddr = new System.Windows.Forms.TextBox();
            this.lBeTestedRegisterPage = new System.Windows.Forms.Label();
            this.lRssiI2cConfig = new System.Windows.Forms.Label();
            this.tbI2cLightSourseRegisterPage = new System.Windows.Forms.TextBox();
            this.lBeTestedRegisterAddr = new System.Windows.Forms.Label();
            this.tbI2cLightSourseRegisterAddr = new System.Windows.Forms.TextBox();
            this.lRssiToUaRate = new System.Windows.Forms.Label();
            this.tbRssiToUaRate = new System.Windows.Forms.TextBox();
            this.tcFunctionSelect.SuspendLayout();
            this.tpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.gbStatus.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.gbMonitorThreshold.SuspendLayout();
            this.gbI2cConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcFunctionSelect
            // 
            this.tcFunctionSelect.Controls.Add(this.tpLog);
            this.tcFunctionSelect.Controls.Add(this.tpConfig);
            this.tcFunctionSelect.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tcFunctionSelect.Location = new System.Drawing.Point(3, 3);
            this.tcFunctionSelect.Name = "tcFunctionSelect";
            this.tcFunctionSelect.SelectedIndex = 0;
            this.tcFunctionSelect.Size = new System.Drawing.Size(1103, 625);
            this.tcFunctionSelect.TabIndex = 0;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.tbRxCurrent4);
            this.tpLog.Controls.Add(this.lRxCurrent4);
            this.tpLog.Controls.Add(this.tbRxCurrent3);
            this.tpLog.Controls.Add(this.lRxCurrent3);
            this.tpLog.Controls.Add(this.tbRxCurrent2);
            this.tpLog.Controls.Add(this.lRxCurrent2);
            this.tpLog.Controls.Add(this.lClassification);
            this.tpLog.Controls.Add(this.bDelRecord);
            this.tpLog.Controls.Add(this.bSaveFile);
            this.tpLog.Controls.Add(this.tbLogFilePath);
            this.tpLog.Controls.Add(this.lLogFilePath);
            this.tpLog.Controls.Add(this.dgvRecord);
            this.tpLog.Controls.Add(this.tbRxCurrent1);
            this.tpLog.Controls.Add(this.lRxCurrent1);
            this.tpLog.Controls.Add(this.bLog);
            this.tpLog.Controls.Add(this.tbSerialNumber);
            this.tpLog.Controls.Add(this.lSnNumber);
            this.tpLog.Controls.Add(this.gbStatus);
            this.tpLog.Controls.Add(this.cbLogMode);
            this.tpLog.Controls.Add(this.lLogMode);
            this.tpLog.Controls.Add(this.bOpenConfigFile);
            this.tpLog.Controls.Add(this.lConfigFile);
            this.tpLog.Controls.Add(this.tbConfigFilePath);
            this.tpLog.Controls.Add(this.lOperator);
            this.tpLog.Controls.Add(this.lSubLotNumber);
            this.tpLog.Controls.Add(this.lLotNumber);
            this.tpLog.Controls.Add(this.lLogLable);
            this.tpLog.Controls.Add(this.tbLotNumber);
            this.tpLog.Controls.Add(this.tbSubLotNumber);
            this.tpLog.Controls.Add(this.tbOperator);
            this.tpLog.Controls.Add(this.tbLogLable);
            this.tpLog.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tpLog.Location = new System.Drawing.Point(4, 29);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(1095, 592);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "紀錄";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // tbRxCurrent4
            // 
            this.tbRxCurrent4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRxCurrent4.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRxCurrent4.Location = new System.Drawing.Point(328, 154);
            this.tbRxCurrent4.Name = "tbRxCurrent4";
            this.tbRxCurrent4.ReadOnly = true;
            this.tbRxCurrent4.Size = new System.Drawing.Size(100, 36);
            this.tbRxCurrent4.TabIndex = 127;
            // 
            // lRxCurrent4
            // 
            this.lRxCurrent4.AutoSize = true;
            this.lRxCurrent4.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRxCurrent4.Location = new System.Drawing.Point(355, 127);
            this.lRxCurrent4.Name = "lRxCurrent4";
            this.lRxCurrent4.Size = new System.Drawing.Size(47, 24);
            this.lRxCurrent4.TabIndex = 126;
            this.lRxCurrent4.Text = "Rx4";
            // 
            // tbRxCurrent3
            // 
            this.tbRxCurrent3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRxCurrent3.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRxCurrent3.Location = new System.Drawing.Point(222, 154);
            this.tbRxCurrent3.Name = "tbRxCurrent3";
            this.tbRxCurrent3.ReadOnly = true;
            this.tbRxCurrent3.Size = new System.Drawing.Size(100, 36);
            this.tbRxCurrent3.TabIndex = 125;
            // 
            // lRxCurrent3
            // 
            this.lRxCurrent3.AutoSize = true;
            this.lRxCurrent3.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRxCurrent3.Location = new System.Drawing.Point(249, 127);
            this.lRxCurrent3.Name = "lRxCurrent3";
            this.lRxCurrent3.Size = new System.Drawing.Size(47, 24);
            this.lRxCurrent3.TabIndex = 124;
            this.lRxCurrent3.Text = "Rx3";
            // 
            // tbRxCurrent2
            // 
            this.tbRxCurrent2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRxCurrent2.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRxCurrent2.Location = new System.Drawing.Point(116, 154);
            this.tbRxCurrent2.Name = "tbRxCurrent2";
            this.tbRxCurrent2.ReadOnly = true;
            this.tbRxCurrent2.Size = new System.Drawing.Size(100, 36);
            this.tbRxCurrent2.TabIndex = 123;
            // 
            // lRxCurrent2
            // 
            this.lRxCurrent2.AutoSize = true;
            this.lRxCurrent2.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRxCurrent2.Location = new System.Drawing.Point(143, 127);
            this.lRxCurrent2.Name = "lRxCurrent2";
            this.lRxCurrent2.Size = new System.Drawing.Size(47, 24);
            this.lRxCurrent2.TabIndex = 122;
            this.lRxCurrent2.Text = "Rx2";
            // 
            // lClassification
            // 
            this.lClassification.AutoSize = true;
            this.lClassification.BackColor = System.Drawing.Color.Green;
            this.lClassification.Font = new System.Drawing.Font("PMingLiU", 135.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lClassification.ForeColor = System.Drawing.Color.White;
            this.lClassification.Location = new System.Drawing.Point(886, 7);
            this.lClassification.Name = "lClassification";
            this.lClassification.Size = new System.Drawing.Size(0, 181);
            this.lClassification.TabIndex = 121;
            // 
            // bDelRecord
            // 
            this.bDelRecord.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bDelRecord.Location = new System.Drawing.Point(10, 546);
            this.bDelRecord.Name = "bDelRecord";
            this.bDelRecord.Size = new System.Drawing.Size(120, 40);
            this.bDelRecord.TabIndex = 120;
            this.bDelRecord.Text = "刪除紀錄";
            this.bDelRecord.UseVisualStyleBackColor = true;
            this.bDelRecord.Click += new System.EventHandler(this.bDelRecord_Click);
            // 
            // bSaveFile
            // 
            this.bSaveFile.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bSaveFile.Location = new System.Drawing.Point(996, 546);
            this.bSaveFile.Name = "bSaveFile";
            this.bSaveFile.Size = new System.Drawing.Size(90, 40);
            this.bSaveFile.TabIndex = 119;
            this.bSaveFile.Text = "存檔";
            this.bSaveFile.UseVisualStyleBackColor = true;
            this.bSaveFile.Click += new System.EventHandler(this.bSaveFile_Click);
            // 
            // tbLogFilePath
            // 
            this.tbLogFilePath.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbLogFilePath.Enabled = false;
            this.tbLogFilePath.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbLogFilePath.Location = new System.Drawing.Point(278, 546);
            this.tbLogFilePath.Name = "tbLogFilePath";
            this.tbLogFilePath.Size = new System.Drawing.Size(712, 36);
            this.tbLogFilePath.TabIndex = 118;
            // 
            // lLogFilePath
            // 
            this.lLogFilePath.AutoSize = true;
            this.lLogFilePath.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLogFilePath.Location = new System.Drawing.Point(136, 554);
            this.lLogFilePath.Name = "lLogFilePath";
            this.lLogFilePath.Size = new System.Drawing.Size(136, 24);
            this.lLogFilePath.TabIndex = 117;
            this.lLogFilePath.Text = "紀錄檔路徑:";
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToResizeColumns = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.YellowGreen;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecord.Location = new System.Drawing.Point(10, 199);
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowTemplate.Height = 24;
            this.dgvRecord.Size = new System.Drawing.Size(1076, 341);
            this.dgvRecord.TabIndex = 116;
            // 
            // tbRxCurrent1
            // 
            this.tbRxCurrent1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRxCurrent1.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRxCurrent1.Location = new System.Drawing.Point(10, 154);
            this.tbRxCurrent1.Name = "tbRxCurrent1";
            this.tbRxCurrent1.ReadOnly = true;
            this.tbRxCurrent1.Size = new System.Drawing.Size(100, 36);
            this.tbRxCurrent1.TabIndex = 115;
            // 
            // lRxCurrent1
            // 
            this.lRxCurrent1.AutoSize = true;
            this.lRxCurrent1.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRxCurrent1.Location = new System.Drawing.Point(37, 127);
            this.lRxCurrent1.Name = "lRxCurrent1";
            this.lRxCurrent1.Size = new System.Drawing.Size(47, 24);
            this.lRxCurrent1.TabIndex = 114;
            this.lRxCurrent1.Text = "Rx1";
            // 
            // bLog
            // 
            this.bLog.Enabled = false;
            this.bLog.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bLog.Location = new System.Drawing.Point(735, 6);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(145, 184);
            this.bLog.TabIndex = 111;
            this.bLog.Text = "紀錄";
            this.bLog.UseVisualStyleBackColor = true;
            this.bLog.Click += new System.EventHandler(this.bLog_Click);
            // 
            // tbSerialNumber
            // 
            this.tbSerialNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbSerialNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbSerialNumber.Location = new System.Drawing.Point(276, 88);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.Size = new System.Drawing.Size(206, 36);
            this.tbSerialNumber.TabIndex = 107;
            this.tbSerialNumber.TextChanged += new System.EventHandler(this.tbSerialNumber_TextChanged);
            this.tbSerialNumber.Enter += new System.EventHandler(this.tbSerialNumber_Enter);
            this.tbSerialNumber.Leave += new System.EventHandler(this.tbSerialNumber_Leave);
            // 
            // lSnNumber
            // 
            this.lSnNumber.AutoSize = true;
            this.lSnNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lSnNumber.Location = new System.Drawing.Point(206, 91);
            this.lSnNumber.Name = "lSnNumber";
            this.lSnNumber.Size = new System.Drawing.Size(64, 24);
            this.lSnNumber.TabIndex = 106;
            this.lSnNumber.Text = "序號:";
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.lAction);
            this.gbStatus.Controls.Add(this.lResult);
            this.gbStatus.Location = new System.Drawing.Point(488, 91);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(241, 97);
            this.gbStatus.TabIndex = 105;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "狀態";
            // 
            // lAction
            // 
            this.lAction.AutoSize = true;
            this.lAction.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lAction.Location = new System.Drawing.Point(6, 26);
            this.lAction.Name = "lAction";
            this.lAction.Size = new System.Drawing.Size(0, 12);
            this.lAction.TabIndex = 35;
            // 
            // lResult
            // 
            this.lResult.AutoSize = true;
            this.lResult.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lResult.Location = new System.Drawing.Point(6, 48);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(0, 24);
            this.lResult.TabIndex = 12;
            // 
            // cbLogMode
            // 
            this.cbLogMode.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.cbLogMode.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbLogMode.FormattingEnabled = true;
            this.cbLogMode.Items.AddRange(new object[] {
            "4 Channel",
            "1 Channel"});
            this.cbLogMode.Location = new System.Drawing.Point(558, 6);
            this.cbLogMode.Name = "cbLogMode";
            this.cbLogMode.Size = new System.Drawing.Size(171, 32);
            this.cbLogMode.TabIndex = 103;
            this.cbLogMode.Text = "4 Channel";
            this.cbLogMode.SelectedIndexChanged += new System.EventHandler(this.cbLogMode_SelectedIndexChanged);
            // 
            // lLogMode
            // 
            this.lLogMode.AutoSize = true;
            this.lLogMode.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLogMode.Location = new System.Drawing.Point(488, 9);
            this.lLogMode.Name = "lLogMode";
            this.lLogMode.Size = new System.Drawing.Size(64, 24);
            this.lLogMode.TabIndex = 104;
            this.lLogMode.Text = "模式:";
            // 
            // bOpenConfigFile
            // 
            this.bOpenConfigFile.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bOpenConfigFile.Location = new System.Drawing.Point(312, 6);
            this.bOpenConfigFile.Name = "bOpenConfigFile";
            this.bOpenConfigFile.Size = new System.Drawing.Size(170, 36);
            this.bOpenConfigFile.TabIndex = 102;
            this.bOpenConfigFile.Text = "選擇設定檔";
            this.bOpenConfigFile.UseVisualStyleBackColor = true;
            this.bOpenConfigFile.Click += new System.EventHandler(this.bOpenConfigFile_Click);
            // 
            // lConfigFile
            // 
            this.lConfigFile.AutoSize = true;
            this.lConfigFile.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lConfigFile.Location = new System.Drawing.Point(6, 9);
            this.lConfigFile.Name = "lConfigFile";
            this.lConfigFile.Size = new System.Drawing.Size(88, 24);
            this.lConfigFile.TabIndex = 100;
            this.lConfigFile.Text = "設定檔:";
            // 
            // tbConfigFilePath
            // 
            this.tbConfigFilePath.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbConfigFilePath.Enabled = false;
            this.tbConfigFilePath.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbConfigFilePath.Location = new System.Drawing.Point(100, 6);
            this.tbConfigFilePath.Name = "tbConfigFilePath";
            this.tbConfigFilePath.Size = new System.Drawing.Size(206, 36);
            this.tbConfigFilePath.TabIndex = 101;
            // 
            // lOperator
            // 
            this.lOperator.AutoSize = true;
            this.lOperator.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lOperator.Location = new System.Drawing.Point(6, 91);
            this.lOperator.Name = "lOperator";
            this.lOperator.Size = new System.Drawing.Size(88, 24);
            this.lOperator.TabIndex = 98;
            this.lOperator.Text = "操作員:";
            // 
            // lSubLotNumber
            // 
            this.lSubLotNumber.AutoSize = true;
            this.lSubLotNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lSubLotNumber.Location = new System.Drawing.Point(312, 49);
            this.lSubLotNumber.Name = "lSubLotNumber";
            this.lSubLotNumber.Size = new System.Drawing.Size(64, 24);
            this.lSubLotNumber.TabIndex = 96;
            this.lSubLotNumber.Text = "子批:";
            // 
            // lLotNumber
            // 
            this.lLotNumber.AutoSize = true;
            this.lLotNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLotNumber.Location = new System.Drawing.Point(6, 49);
            this.lLotNumber.Name = "lLotNumber";
            this.lLotNumber.Size = new System.Drawing.Size(88, 24);
            this.lLotNumber.TabIndex = 92;
            this.lLotNumber.Text = "批　號:";
            // 
            // lLogLable
            // 
            this.lLogLable.AutoSize = true;
            this.lLogLable.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLogLable.Location = new System.Drawing.Point(488, 49);
            this.lLogLable.Name = "lLogLable";
            this.lLogLable.Size = new System.Drawing.Size(64, 24);
            this.lLogLable.TabIndex = 94;
            this.lLogLable.Text = "標籤:";
            // 
            // tbLotNumber
            // 
            this.tbLotNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbLotNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbLotNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbLotNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbLotNumber.Location = new System.Drawing.Point(100, 46);
            this.tbLotNumber.Name = "tbLotNumber";
            this.tbLotNumber.Size = new System.Drawing.Size(206, 36);
            this.tbLotNumber.TabIndex = 93;
            this.tbLotNumber.Enter += new System.EventHandler(this.tbLotNumber_Enter);
            this.tbLotNumber.Leave += new System.EventHandler(this.tbLotNumber_Leave);
            // 
            // tbSubLotNumber
            // 
            this.tbSubLotNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbSubLotNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbSubLotNumber.Location = new System.Drawing.Point(382, 46);
            this.tbSubLotNumber.Name = "tbSubLotNumber";
            this.tbSubLotNumber.Size = new System.Drawing.Size(100, 36);
            this.tbSubLotNumber.TabIndex = 97;
            this.tbSubLotNumber.Enter += new System.EventHandler(this.tbSubLotNumber_Enter);
            this.tbSubLotNumber.Leave += new System.EventHandler(this.tbSubLotNumber_Leave);
            // 
            // tbOperator
            // 
            this.tbOperator.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbOperator.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbOperator.Location = new System.Drawing.Point(100, 88);
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.Size = new System.Drawing.Size(100, 36);
            this.tbOperator.TabIndex = 99;
            // 
            // tbLogLable
            // 
            this.tbLogLable.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbLogLable.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbLogLable.Location = new System.Drawing.Point(558, 45);
            this.tbLogLable.Name = "tbLogLable";
            this.tbLogLable.Size = new System.Drawing.Size(171, 36);
            this.tbLogLable.TabIndex = 95;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.lRssiToUaRate);
            this.tpConfig.Controls.Add(this.tbRssiToUaRate);
            this.tpConfig.Controls.Add(this.bSaveConfig);
            this.tpConfig.Controls.Add(this.gbMonitorThreshold);
            this.tpConfig.Controls.Add(this.gbI2cConfig);
            this.tpConfig.Location = new System.Drawing.Point(4, 29);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(1095, 592);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "設定";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // bSaveConfig
            // 
            this.bSaveConfig.Location = new System.Drawing.Point(428, 6);
            this.bSaveConfig.Name = "bSaveConfig";
            this.bSaveConfig.Size = new System.Drawing.Size(120, 36);
            this.bSaveConfig.TabIndex = 85;
            this.bSaveConfig.Text = "儲存設定";
            this.bSaveConfig.UseVisualStyleBackColor = true;
            this.bSaveConfig.Click += new System.EventHandler(this.bSaveConfig_Click);
            // 
            // gbMonitorThreshold
            // 
            this.gbMonitorThreshold.Controls.Add(this.lCurrentThreshold);
            this.gbMonitorThreshold.Controls.Add(this.tbCurrent4Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbCurrent1Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbCurrent2Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbCurrent3Threshold);
            this.gbMonitorThreshold.Controls.Add(this.lCh1Threshold);
            this.gbMonitorThreshold.Controls.Add(this.lCh2Threshold);
            this.gbMonitorThreshold.Controls.Add(this.lCh3Threshold);
            this.gbMonitorThreshold.Controls.Add(this.lCh4Threshold);
            this.gbMonitorThreshold.Location = new System.Drawing.Point(6, 159);
            this.gbMonitorThreshold.Name = "gbMonitorThreshold";
            this.gbMonitorThreshold.Size = new System.Drawing.Size(416, 107);
            this.gbMonitorThreshold.TabIndex = 84;
            this.gbMonitorThreshold.TabStop = false;
            this.gbMonitorThreshold.Text = "量測數值門檻設定";
            // 
            // lCurrentThreshold
            // 
            this.lCurrentThreshold.AutoSize = true;
            this.lCurrentThreshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCurrentThreshold.Location = new System.Drawing.Point(6, 62);
            this.lCurrentThreshold.Name = "lCurrentThreshold";
            this.lCurrentThreshold.Size = new System.Drawing.Size(43, 24);
            this.lCurrentThreshold.TabIndex = 86;
            this.lCurrentThreshold.Text = "uA:";
            // 
            // tbCurrent4Threshold
            // 
            this.tbCurrent4Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbCurrent4Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbCurrent4Threshold.Location = new System.Drawing.Point(253, 62);
            this.tbCurrent4Threshold.Name = "tbCurrent4Threshold";
            this.tbCurrent4Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbCurrent4Threshold.TabIndex = 85;
            this.tbCurrent4Threshold.Text = "10";
            // 
            // tbCurrent1Threshold
            // 
            this.tbCurrent1Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbCurrent1Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbCurrent1Threshold.Location = new System.Drawing.Point(55, 62);
            this.tbCurrent1Threshold.Name = "tbCurrent1Threshold";
            this.tbCurrent1Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbCurrent1Threshold.TabIndex = 82;
            this.tbCurrent1Threshold.Text = "10";
            // 
            // tbCurrent2Threshold
            // 
            this.tbCurrent2Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbCurrent2Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbCurrent2Threshold.Location = new System.Drawing.Point(121, 62);
            this.tbCurrent2Threshold.Name = "tbCurrent2Threshold";
            this.tbCurrent2Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbCurrent2Threshold.TabIndex = 83;
            this.tbCurrent2Threshold.Text = "10";
            // 
            // tbCurrent3Threshold
            // 
            this.tbCurrent3Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbCurrent3Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbCurrent3Threshold.Location = new System.Drawing.Point(187, 62);
            this.tbCurrent3Threshold.Name = "tbCurrent3Threshold";
            this.tbCurrent3Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbCurrent3Threshold.TabIndex = 84;
            this.tbCurrent3Threshold.Text = "10";
            // 
            // lCh1Threshold
            // 
            this.lCh1Threshold.AutoSize = true;
            this.lCh1Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh1Threshold.Location = new System.Drawing.Point(62, 35);
            this.lCh1Threshold.Name = "lCh1Threshold";
            this.lCh1Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh1Threshold.TabIndex = 25;
            this.lCh1Threshold.Text = "Ch1";
            // 
            // lCh2Threshold
            // 
            this.lCh2Threshold.AutoSize = true;
            this.lCh2Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh2Threshold.Location = new System.Drawing.Point(128, 35);
            this.lCh2Threshold.Name = "lCh2Threshold";
            this.lCh2Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh2Threshold.TabIndex = 26;
            this.lCh2Threshold.Text = "Ch2";
            // 
            // lCh3Threshold
            // 
            this.lCh3Threshold.AutoSize = true;
            this.lCh3Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh3Threshold.Location = new System.Drawing.Point(194, 35);
            this.lCh3Threshold.Name = "lCh3Threshold";
            this.lCh3Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh3Threshold.TabIndex = 27;
            this.lCh3Threshold.Text = "Ch3";
            // 
            // lCh4Threshold
            // 
            this.lCh4Threshold.AutoSize = true;
            this.lCh4Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh4Threshold.Location = new System.Drawing.Point(260, 35);
            this.lCh4Threshold.Name = "lCh4Threshold";
            this.lCh4Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh4Threshold.TabIndex = 28;
            this.lCh4Threshold.Text = "Ch4";
            // 
            // gbI2cConfig
            // 
            this.gbI2cConfig.Controls.Add(this.lLightSourceI2cConfig);
            this.gbI2cConfig.Controls.Add(this.tbI2cRssiRegisterAddr);
            this.gbI2cConfig.Controls.Add(this.lBeTestDevAddr);
            this.gbI2cConfig.Controls.Add(this.tbI2cRssiRegisterPage);
            this.gbI2cConfig.Controls.Add(this.tbI2cLightSourseDevAddr);
            this.gbI2cConfig.Controls.Add(this.tbI2cRssiDevAddr);
            this.gbI2cConfig.Controls.Add(this.lBeTestedRegisterPage);
            this.gbI2cConfig.Controls.Add(this.lRssiI2cConfig);
            this.gbI2cConfig.Controls.Add(this.tbI2cLightSourseRegisterPage);
            this.gbI2cConfig.Controls.Add(this.lBeTestedRegisterAddr);
            this.gbI2cConfig.Controls.Add(this.tbI2cLightSourseRegisterAddr);
            this.gbI2cConfig.Location = new System.Drawing.Point(6, 6);
            this.gbI2cConfig.Name = "gbI2cConfig";
            this.gbI2cConfig.Size = new System.Drawing.Size(416, 147);
            this.gbI2cConfig.TabIndex = 82;
            this.gbI2cConfig.TabStop = false;
            this.gbI2cConfig.Text = "I2C 設定";
            // 
            // lLightSourceI2cConfig
            // 
            this.lLightSourceI2cConfig.AutoSize = true;
            this.lLightSourceI2cConfig.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLightSourceI2cConfig.Location = new System.Drawing.Point(6, 62);
            this.lLightSourceI2cConfig.Name = "lLightSourceI2cConfig";
            this.lLightSourceI2cConfig.Size = new System.Drawing.Size(133, 24);
            this.lLightSourceI2cConfig.TabIndex = 19;
            this.lLightSourceI2cConfig.Text = "Light Source:";
            // 
            // tbI2cRssiRegisterAddr
            // 
            this.tbI2cRssiRegisterAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cRssiRegisterAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cRssiRegisterAddr.Location = new System.Drawing.Point(333, 101);
            this.tbI2cRssiRegisterAddr.Name = "tbI2cRssiRegisterAddr";
            this.tbI2cRssiRegisterAddr.Size = new System.Drawing.Size(60, 36);
            this.tbI2cRssiRegisterAddr.TabIndex = 80;
            this.tbI2cRssiRegisterAddr.Text = "120";
            // 
            // lBeTestDevAddr
            // 
            this.lBeTestDevAddr.AutoSize = true;
            this.lBeTestDevAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lBeTestDevAddr.Location = new System.Drawing.Point(126, 32);
            this.lBeTestDevAddr.Name = "lBeTestDevAddr";
            this.lBeTestDevAddr.Size = new System.Drawing.Size(99, 24);
            this.lBeTestDevAddr.TabIndex = 20;
            this.lBeTestDevAddr.Text = "Dev Addr";
            // 
            // tbI2cRssiRegisterPage
            // 
            this.tbI2cRssiRegisterPage.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cRssiRegisterPage.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cRssiRegisterPage.Location = new System.Drawing.Point(239, 101);
            this.tbI2cRssiRegisterPage.Name = "tbI2cRssiRegisterPage";
            this.tbI2cRssiRegisterPage.Size = new System.Drawing.Size(60, 36);
            this.tbI2cRssiRegisterPage.TabIndex = 79;
            this.tbI2cRssiRegisterPage.Text = "0";
            // 
            // tbI2cLightSourseDevAddr
            // 
            this.tbI2cLightSourseDevAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cLightSourseDevAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cLightSourseDevAddr.Location = new System.Drawing.Point(145, 59);
            this.tbI2cLightSourseDevAddr.Name = "tbI2cLightSourseDevAddr";
            this.tbI2cLightSourseDevAddr.Size = new System.Drawing.Size(60, 36);
            this.tbI2cLightSourseDevAddr.TabIndex = 35;
            this.tbI2cLightSourseDevAddr.Text = "80";
            // 
            // tbI2cRssiDevAddr
            // 
            this.tbI2cRssiDevAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cRssiDevAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cRssiDevAddr.Location = new System.Drawing.Point(145, 101);
            this.tbI2cRssiDevAddr.Name = "tbI2cRssiDevAddr";
            this.tbI2cRssiDevAddr.Size = new System.Drawing.Size(60, 36);
            this.tbI2cRssiDevAddr.TabIndex = 78;
            this.tbI2cRssiDevAddr.Text = "81";
            // 
            // lBeTestedRegisterPage
            // 
            this.lBeTestedRegisterPage.AutoSize = true;
            this.lBeTestedRegisterPage.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lBeTestedRegisterPage.Location = new System.Drawing.Point(242, 32);
            this.lBeTestedRegisterPage.Name = "lBeTestedRegisterPage";
            this.lBeTestedRegisterPage.Size = new System.Drawing.Size(54, 24);
            this.lBeTestedRegisterPage.TabIndex = 36;
            this.lBeTestedRegisterPage.Text = "Page";
            // 
            // lRssiI2cConfig
            // 
            this.lRssiI2cConfig.AutoSize = true;
            this.lRssiI2cConfig.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssiI2cConfig.Location = new System.Drawing.Point(6, 104);
            this.lRssiI2cConfig.Name = "lRssiI2cConfig";
            this.lRssiI2cConfig.Size = new System.Drawing.Size(65, 24);
            this.lRssiI2cConfig.TabIndex = 77;
            this.lRssiI2cConfig.Text = "RSSI:";
            // 
            // tbI2cLightSourseRegisterPage
            // 
            this.tbI2cLightSourseRegisterPage.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cLightSourseRegisterPage.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cLightSourseRegisterPage.Location = new System.Drawing.Point(239, 59);
            this.tbI2cLightSourseRegisterPage.Name = "tbI2cLightSourseRegisterPage";
            this.tbI2cLightSourseRegisterPage.Size = new System.Drawing.Size(60, 36);
            this.tbI2cLightSourseRegisterPage.TabIndex = 37;
            this.tbI2cLightSourseRegisterPage.Text = "0";
            // 
            // lBeTestedRegisterAddr
            // 
            this.lBeTestedRegisterAddr.AutoSize = true;
            this.lBeTestedRegisterAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lBeTestedRegisterAddr.Location = new System.Drawing.Point(314, 32);
            this.lBeTestedRegisterAddr.Name = "lBeTestedRegisterAddr";
            this.lBeTestedRegisterAddr.Size = new System.Drawing.Size(98, 24);
            this.lBeTestedRegisterAddr.TabIndex = 38;
            this.lBeTestedRegisterAddr.Text = "Reg Addr";
            // 
            // tbI2cLightSourseRegisterAddr
            // 
            this.tbI2cLightSourseRegisterAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbI2cLightSourseRegisterAddr.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbI2cLightSourseRegisterAddr.Location = new System.Drawing.Point(333, 59);
            this.tbI2cLightSourseRegisterAddr.Name = "tbI2cLightSourseRegisterAddr";
            this.tbI2cLightSourseRegisterAddr.Size = new System.Drawing.Size(60, 36);
            this.tbI2cLightSourseRegisterAddr.TabIndex = 39;
            this.tbI2cLightSourseRegisterAddr.Text = "86";
            // 
            // lRssiToUaRate
            // 
            this.lRssiToUaRate.AutoSize = true;
            this.lRssiToUaRate.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssiToUaRate.Location = new System.Drawing.Point(12, 275);
            this.lRssiToUaRate.Name = "lRssiToUaRate";
            this.lRssiToUaRate.Size = new System.Drawing.Size(168, 24);
            this.lRssiToUaRate.TabIndex = 88;
            this.lRssiToUaRate.Text = "RSSI to uA Rate:";
            // 
            // tbRssiToUaRate
            // 
            this.tbRssiToUaRate.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssiToUaRate.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssiToUaRate.Location = new System.Drawing.Point(186, 272);
            this.tbRssiToUaRate.Name = "tbRssiToUaRate";
            this.tbRssiToUaRate.Size = new System.Drawing.Size(155, 36);
            this.tbRssiToUaRate.TabIndex = 87;
            this.tbRssiToUaRate.Text = "0.3628";
            // 
            // UcRssiMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcFunctionSelect);
            this.Name = "UcRssiMeasure";
            this.Size = new System.Drawing.Size(1109, 631);
            this.tcFunctionSelect.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.tpLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.gbStatus.ResumeLayout(false);
            this.gbStatus.PerformLayout();
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.gbMonitorThreshold.ResumeLayout(false);
            this.gbMonitorThreshold.PerformLayout();
            this.gbI2cConfig.ResumeLayout(false);
            this.gbI2cConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcFunctionSelect;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Button bOpenConfigFile;
        private System.Windows.Forms.Label lConfigFile;
        private System.Windows.Forms.TextBox tbConfigFilePath;
        private System.Windows.Forms.Label lOperator;
        private System.Windows.Forms.Label lSubLotNumber;
        private System.Windows.Forms.Label lLotNumber;
        private System.Windows.Forms.Label lLogLable;
        private System.Windows.Forms.TextBox tbLotNumber;
        private System.Windows.Forms.TextBox tbSubLotNumber;
        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.TextBox tbLogLable;
        private System.Windows.Forms.Label lClassification;
        private System.Windows.Forms.Button bDelRecord;
        private System.Windows.Forms.Button bSaveFile;
        private System.Windows.Forms.TextBox tbLogFilePath;
        private System.Windows.Forms.Label lLogFilePath;
        private System.Windows.Forms.DataGridView dgvRecord;
        private System.Windows.Forms.TextBox tbRxCurrent1;
        private System.Windows.Forms.Label lRxCurrent1;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.TextBox tbSerialNumber;
        private System.Windows.Forms.Label lSnNumber;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Label lAction;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.ComboBox cbLogMode;
        private System.Windows.Forms.Label lLogMode;
        private System.Windows.Forms.TextBox tbRxCurrent4;
        private System.Windows.Forms.Label lRxCurrent4;
        private System.Windows.Forms.TextBox tbRxCurrent3;
        private System.Windows.Forms.Label lRxCurrent3;
        private System.Windows.Forms.TextBox tbRxCurrent2;
        private System.Windows.Forms.Label lRxCurrent2;
        private System.Windows.Forms.Button bSaveConfig;
        private System.Windows.Forms.GroupBox gbMonitorThreshold;
        private System.Windows.Forms.Label lCurrentThreshold;
        private System.Windows.Forms.TextBox tbCurrent4Threshold;
        private System.Windows.Forms.TextBox tbCurrent1Threshold;
        private System.Windows.Forms.TextBox tbCurrent2Threshold;
        private System.Windows.Forms.TextBox tbCurrent3Threshold;
        private System.Windows.Forms.Label lCh1Threshold;
        private System.Windows.Forms.Label lCh2Threshold;
        private System.Windows.Forms.Label lCh3Threshold;
        private System.Windows.Forms.Label lCh4Threshold;
        private System.Windows.Forms.GroupBox gbI2cConfig;
        private System.Windows.Forms.Label lLightSourceI2cConfig;
        private System.Windows.Forms.TextBox tbI2cRssiRegisterAddr;
        private System.Windows.Forms.Label lBeTestDevAddr;
        private System.Windows.Forms.TextBox tbI2cRssiRegisterPage;
        private System.Windows.Forms.TextBox tbI2cLightSourseDevAddr;
        private System.Windows.Forms.TextBox tbI2cRssiDevAddr;
        private System.Windows.Forms.Label lBeTestedRegisterPage;
        private System.Windows.Forms.Label lRssiI2cConfig;
        private System.Windows.Forms.TextBox tbI2cLightSourseRegisterPage;
        private System.Windows.Forms.Label lBeTestedRegisterAddr;
        private System.Windows.Forms.TextBox tbI2cLightSourseRegisterAddr;
        private System.Windows.Forms.Label lRssiToUaRate;
        private System.Windows.Forms.TextBox tbRssiToUaRate;
    }
}
