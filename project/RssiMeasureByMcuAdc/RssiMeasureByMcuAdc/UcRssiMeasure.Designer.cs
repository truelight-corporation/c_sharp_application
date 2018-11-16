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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcFunctionSelect = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tbRssi4 = new System.Windows.Forms.TextBox();
            this.lRssi4 = new System.Windows.Forms.Label();
            this.tbRssi3 = new System.Windows.Forms.TextBox();
            this.lRssi3 = new System.Windows.Forms.Label();
            this.tbRssi2 = new System.Windows.Forms.TextBox();
            this.lRssi2 = new System.Windows.Forms.Label();
            this.lClassification = new System.Windows.Forms.Label();
            this.bDelRecord = new System.Windows.Forms.Button();
            this.bSaveFile = new System.Windows.Forms.Button();
            this.tbLogFilePath = new System.Windows.Forms.TextBox();
            this.lLogFilePath = new System.Windows.Forms.Label();
            this.dgvRecord = new System.Windows.Forms.DataGridView();
            this.tbRssi1 = new System.Windows.Forms.TextBox();
            this.lRssi1 = new System.Windows.Forms.Label();
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
            this.lRssiThreshold = new System.Windows.Forms.Label();
            this.tbRssi4Threshold = new System.Windows.Forms.TextBox();
            this.tbRssi1Threshold = new System.Windows.Forms.TextBox();
            this.tbRssi2Threshold = new System.Windows.Forms.TextBox();
            this.tbRssi3Threshold = new System.Windows.Forms.TextBox();
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
            this.tpLog.Controls.Add(this.tbRssi4);
            this.tpLog.Controls.Add(this.lRssi4);
            this.tpLog.Controls.Add(this.tbRssi3);
            this.tpLog.Controls.Add(this.lRssi3);
            this.tpLog.Controls.Add(this.tbRssi2);
            this.tpLog.Controls.Add(this.lRssi2);
            this.tpLog.Controls.Add(this.lClassification);
            this.tpLog.Controls.Add(this.bDelRecord);
            this.tpLog.Controls.Add(this.bSaveFile);
            this.tpLog.Controls.Add(this.tbLogFilePath);
            this.tpLog.Controls.Add(this.lLogFilePath);
            this.tpLog.Controls.Add(this.dgvRecord);
            this.tpLog.Controls.Add(this.tbRssi1);
            this.tpLog.Controls.Add(this.lRssi1);
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
            // tbRssi4
            // 
            this.tbRssi4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi4.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi4.Location = new System.Drawing.Point(328, 154);
            this.tbRssi4.Name = "tbRssi4";
            this.tbRssi4.ReadOnly = true;
            this.tbRssi4.Size = new System.Drawing.Size(100, 36);
            this.tbRssi4.TabIndex = 127;
            // 
            // lRssi4
            // 
            this.lRssi4.AutoSize = true;
            this.lRssi4.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssi4.Location = new System.Drawing.Point(343, 127);
            this.lRssi4.Name = "lRssi4";
            this.lRssi4.Size = new System.Drawing.Size(70, 24);
            this.lRssi4.TabIndex = 126;
            this.lRssi4.Text = "RSSI4";
            // 
            // tbRssi3
            // 
            this.tbRssi3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi3.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi3.Location = new System.Drawing.Point(222, 154);
            this.tbRssi3.Name = "tbRssi3";
            this.tbRssi3.ReadOnly = true;
            this.tbRssi3.Size = new System.Drawing.Size(100, 36);
            this.tbRssi3.TabIndex = 125;
            // 
            // lRssi3
            // 
            this.lRssi3.AutoSize = true;
            this.lRssi3.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssi3.Location = new System.Drawing.Point(237, 127);
            this.lRssi3.Name = "lRssi3";
            this.lRssi3.Size = new System.Drawing.Size(70, 24);
            this.lRssi3.TabIndex = 124;
            this.lRssi3.Text = "RSSI3";
            // 
            // tbRssi2
            // 
            this.tbRssi2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi2.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi2.Location = new System.Drawing.Point(116, 154);
            this.tbRssi2.Name = "tbRssi2";
            this.tbRssi2.ReadOnly = true;
            this.tbRssi2.Size = new System.Drawing.Size(100, 36);
            this.tbRssi2.TabIndex = 123;
            // 
            // lRssi2
            // 
            this.lRssi2.AutoSize = true;
            this.lRssi2.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssi2.Location = new System.Drawing.Point(131, 127);
            this.lRssi2.Name = "lRssi2";
            this.lRssi2.Size = new System.Drawing.Size(70, 24);
            this.lRssi2.TabIndex = 122;
            this.lRssi2.Text = "RSSI2";
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.YellowGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.Location = new System.Drawing.Point(10, 199);
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowTemplate.Height = 24;
            this.dgvRecord.Size = new System.Drawing.Size(1076, 341);
            this.dgvRecord.TabIndex = 116;
            // 
            // tbRssi1
            // 
            this.tbRssi1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi1.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi1.Location = new System.Drawing.Point(10, 154);
            this.tbRssi1.Name = "tbRssi1";
            this.tbRssi1.ReadOnly = true;
            this.tbRssi1.Size = new System.Drawing.Size(100, 36);
            this.tbRssi1.TabIndex = 115;
            // 
            // lRssi1
            // 
            this.lRssi1.AutoSize = true;
            this.lRssi1.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssi1.Location = new System.Drawing.Point(25, 127);
            this.lRssi1.Name = "lRssi1";
            this.lRssi1.Size = new System.Drawing.Size(70, 24);
            this.lRssi1.TabIndex = 114;
            this.lRssi1.Text = "RSSI1";
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
            this.tbSerialNumber.Location = new System.Drawing.Point(382, 88);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.Size = new System.Drawing.Size(100, 36);
            this.tbSerialNumber.TabIndex = 107;
            this.tbSerialNumber.TextChanged += new System.EventHandler(this.tbSerialNumber_TextChanged);
            this.tbSerialNumber.Enter += new System.EventHandler(this.tbSerialNumber_Enter);
            this.tbSerialNumber.Leave += new System.EventHandler(this.tbSerialNumber_Leave);
            // 
            // lSnNumber
            // 
            this.lSnNumber.AutoSize = true;
            this.lSnNumber.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lSnNumber.Location = new System.Drawing.Point(312, 91);
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
            this.gbMonitorThreshold.Controls.Add(this.lRssiThreshold);
            this.gbMonitorThreshold.Controls.Add(this.tbRssi4Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbRssi1Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbRssi2Threshold);
            this.gbMonitorThreshold.Controls.Add(this.tbRssi3Threshold);
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
            // lRssiThreshold
            // 
            this.lRssiThreshold.AutoSize = true;
            this.lRssiThreshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lRssiThreshold.Location = new System.Drawing.Point(6, 62);
            this.lRssiThreshold.Name = "lRssiThreshold";
            this.lRssiThreshold.Size = new System.Drawing.Size(65, 24);
            this.lRssiThreshold.TabIndex = 86;
            this.lRssiThreshold.Text = "RSSI:";
            // 
            // tbRssi4Threshold
            // 
            this.tbRssi4Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi4Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi4Threshold.Location = new System.Drawing.Point(275, 59);
            this.tbRssi4Threshold.Name = "tbRssi4Threshold";
            this.tbRssi4Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbRssi4Threshold.TabIndex = 85;
            this.tbRssi4Threshold.Text = "10";
            // 
            // tbRssi1Threshold
            // 
            this.tbRssi1Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi1Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi1Threshold.Location = new System.Drawing.Point(77, 59);
            this.tbRssi1Threshold.Name = "tbRssi1Threshold";
            this.tbRssi1Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbRssi1Threshold.TabIndex = 82;
            this.tbRssi1Threshold.Text = "10";
            // 
            // tbRssi2Threshold
            // 
            this.tbRssi2Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi2Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi2Threshold.Location = new System.Drawing.Point(143, 59);
            this.tbRssi2Threshold.Name = "tbRssi2Threshold";
            this.tbRssi2Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbRssi2Threshold.TabIndex = 83;
            this.tbRssi2Threshold.Text = "10";
            // 
            // tbRssi3Threshold
            // 
            this.tbRssi3Threshold.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbRssi3Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbRssi3Threshold.Location = new System.Drawing.Point(209, 59);
            this.tbRssi3Threshold.Name = "tbRssi3Threshold";
            this.tbRssi3Threshold.Size = new System.Drawing.Size(60, 36);
            this.tbRssi3Threshold.TabIndex = 84;
            this.tbRssi3Threshold.Text = "10";
            // 
            // lCh1Threshold
            // 
            this.lCh1Threshold.AutoSize = true;
            this.lCh1Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh1Threshold.Location = new System.Drawing.Point(84, 32);
            this.lCh1Threshold.Name = "lCh1Threshold";
            this.lCh1Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh1Threshold.TabIndex = 25;
            this.lCh1Threshold.Text = "Ch1";
            // 
            // lCh2Threshold
            // 
            this.lCh2Threshold.AutoSize = true;
            this.lCh2Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh2Threshold.Location = new System.Drawing.Point(150, 32);
            this.lCh2Threshold.Name = "lCh2Threshold";
            this.lCh2Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh2Threshold.TabIndex = 26;
            this.lCh2Threshold.Text = "Ch2";
            // 
            // lCh3Threshold
            // 
            this.lCh3Threshold.AutoSize = true;
            this.lCh3Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh3Threshold.Location = new System.Drawing.Point(216, 32);
            this.lCh3Threshold.Name = "lCh3Threshold";
            this.lCh3Threshold.Size = new System.Drawing.Size(47, 24);
            this.lCh3Threshold.TabIndex = 27;
            this.lCh3Threshold.Text = "Ch3";
            // 
            // lCh4Threshold
            // 
            this.lCh4Threshold.AutoSize = true;
            this.lCh4Threshold.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lCh4Threshold.Location = new System.Drawing.Point(282, 32);
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
        private System.Windows.Forms.TextBox tbRssi1;
        private System.Windows.Forms.Label lRssi1;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.TextBox tbSerialNumber;
        private System.Windows.Forms.Label lSnNumber;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Label lAction;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.ComboBox cbLogMode;
        private System.Windows.Forms.Label lLogMode;
        private System.Windows.Forms.TextBox tbRssi4;
        private System.Windows.Forms.Label lRssi4;
        private System.Windows.Forms.TextBox tbRssi3;
        private System.Windows.Forms.Label lRssi3;
        private System.Windows.Forms.TextBox tbRssi2;
        private System.Windows.Forms.Label lRssi2;
        private System.Windows.Forms.Button bSaveConfig;
        private System.Windows.Forms.GroupBox gbMonitorThreshold;
        private System.Windows.Forms.Label lRssiThreshold;
        private System.Windows.Forms.TextBox tbRssi4Threshold;
        private System.Windows.Forms.TextBox tbRssi1Threshold;
        private System.Windows.Forms.TextBox tbRssi2Threshold;
        private System.Windows.Forms.TextBox tbRssi3Threshold;
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
    }
}
