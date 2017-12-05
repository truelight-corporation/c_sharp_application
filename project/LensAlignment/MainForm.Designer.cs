namespace LensAlignment
{
    partial class fLensAlignment
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
            this.cbLightSourceConnected = new System.Windows.Forms.CheckBox();
            this.cbBeAlignmentConnected = new System.Windows.Forms.CheckBox();
            this.cbStartMonitor = new System.Windows.Forms.CheckBox();
            this.lBeAlignmentPassword = new System.Windows.Forms.Label();
            this.tbBeAlignmentPassword123 = new System.Windows.Forms.TextBox();
            this.bLog = new System.Windows.Forms.Button();
            this.bClearLog = new System.Windows.Forms.Button();
            this.lLogLable = new System.Windows.Forms.Label();
            this.tbLogLable = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.dgvRecord = new System.Windows.Forms.DataGridView();
            this.tbBeAlignmentPassword124 = new System.Windows.Forms.TextBox();
            this.tbBeAlignmentPassword125 = new System.Windows.Forms.TextBox();
            this.tbBeAlignmentPassword126 = new System.Windows.Forms.TextBox();
            this.tcFunction = new System.Windows.Forms.TabControl();
            this.tpLensAlignment = new System.Windows.Forms.TabPage();
            this.ucLensAlignment = new LensAlignment.UcLensAlignment();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tpCorrect = new System.Windows.Forms.TabPage();
            this.bLoadCorrectFile = new System.Windows.Forms.Button();
            this.bSaveCorrectFile = new System.Windows.Forms.Button();
            this.tbCorrectFilePath = new System.Windows.Forms.TextBox();
            this.lCorrectFilePath = new System.Windows.Forms.Label();
            this.tbLightSourcePassword126 = new System.Windows.Forms.TextBox();
            this.tbLightSourcePassword125 = new System.Windows.Forms.TextBox();
            this.tbLightSourcePassword124 = new System.Windows.Forms.TextBox();
            this.tbLightSourcePassword123 = new System.Windows.Forms.TextBox();
            this.lLightSourcePassword = new System.Windows.Forms.Label();
            this.gbRxRateCorrect = new System.Windows.Forms.GroupBox();
            this.bAutoCorrect = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bWrite = new System.Windows.Forms.Button();
            this.lRxPowerUnit = new System.Windows.Forms.Label();
            this.lRxRateUnit = new System.Windows.Forms.Label();
            this.lRssiUnit = new System.Windows.Forms.Label();
            this.lInputPowerUnit = new System.Windows.Forms.Label();
            this.bRead = new System.Windows.Forms.Button();
            this.tbTx1RxPower = new System.Windows.Forms.TextBox();
            this.tbTx2RxPower = new System.Windows.Forms.TextBox();
            this.tbTx3RxPower = new System.Windows.Forms.TextBox();
            this.tbTx4RxPower = new System.Windows.Forms.TextBox();
            this.lRxPower = new System.Windows.Forms.Label();
            this.tbRx1Rate = new System.Windows.Forms.TextBox();
            this.tbRx2Rate = new System.Windows.Forms.TextBox();
            this.tbRx3Rate = new System.Windows.Forms.TextBox();
            this.tbRx4Rate = new System.Windows.Forms.TextBox();
            this.lRxRateMin = new System.Windows.Forms.Label();
            this.tbRxRateMin = new System.Windows.Forms.TextBox();
            this.lRxRateMax = new System.Windows.Forms.Label();
            this.tbRxRateMax = new System.Windows.Forms.TextBox();
            this.lRateDefault = new System.Windows.Forms.Label();
            this.tbRxRateDefault = new System.Windows.Forms.TextBox();
            this.lRxRate = new System.Windows.Forms.Label();
            this.tbRx1Rssi = new System.Windows.Forms.TextBox();
            this.tbRx2Rssi = new System.Windows.Forms.TextBox();
            this.tbRx3Rssi = new System.Windows.Forms.TextBox();
            this.tbRx4Rssi = new System.Windows.Forms.TextBox();
            this.lRssi = new System.Windows.Forms.Label();
            this.lRx1InputPower = new System.Windows.Forms.Label();
            this.tbRx1InputPower = new System.Windows.Forms.TextBox();
            this.lRx2InputPower = new System.Windows.Forms.Label();
            this.tbRx2InputPower = new System.Windows.Forms.TextBox();
            this.lRx3InputPower = new System.Windows.Forms.Label();
            this.tbRx3InputPower = new System.Windows.Forms.TextBox();
            this.lRx4InputPower = new System.Windows.Forms.Label();
            this.tbRx4InputPower = new System.Windows.Forms.TextBox();
            this.lRxInputPower = new System.Windows.Forms.Label();
            this.tbSerialNumber = new System.Windows.Forms.TextBox();
            this.lSerialNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.tcFunction.SuspendLayout();
            this.tpLensAlignment.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpCorrect.SuspendLayout();
            this.gbRxRateCorrect.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLightSourceConnected
            // 
            this.cbLightSourceConnected.AutoSize = true;
            this.cbLightSourceConnected.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbLightSourceConnected.Location = new System.Drawing.Point(12, 12);
            this.cbLightSourceConnected.Name = "cbLightSourceConnected";
            this.cbLightSourceConnected.Size = new System.Drawing.Size(137, 16);
            this.cbLightSourceConnected.TabIndex = 0;
            this.cbLightSourceConnected.Text = "Light Source Connected";
            this.cbLightSourceConnected.UseVisualStyleBackColor = true;
            this.cbLightSourceConnected.CheckedChanged += new System.EventHandler(this._cbLightSourceConnected_CheckedChanged);
            // 
            // cbBeAlignmentConnected
            // 
            this.cbBeAlignmentConnected.AutoSize = true;
            this.cbBeAlignmentConnected.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbBeAlignmentConnected.Location = new System.Drawing.Point(155, 12);
            this.cbBeAlignmentConnected.Name = "cbBeAlignmentConnected";
            this.cbBeAlignmentConnected.Size = new System.Drawing.Size(142, 16);
            this.cbBeAlignmentConnected.TabIndex = 1;
            this.cbBeAlignmentConnected.Text = "Be Alignment Connected";
            this.cbBeAlignmentConnected.UseVisualStyleBackColor = true;
            this.cbBeAlignmentConnected.CheckedChanged += new System.EventHandler(this._cbBeAlignmentConnected_CheckedChanged);
            // 
            // cbStartMonitor
            // 
            this.cbStartMonitor.AutoSize = true;
            this.cbStartMonitor.Enabled = false;
            this.cbStartMonitor.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbStartMonitor.Location = new System.Drawing.Point(1021, 12);
            this.cbStartMonitor.Name = "cbStartMonitor";
            this.cbStartMonitor.Size = new System.Drawing.Size(86, 16);
            this.cbStartMonitor.TabIndex = 3;
            this.cbStartMonitor.Text = "Start Monitor";
            this.cbStartMonitor.UseVisualStyleBackColor = true;
            this.cbStartMonitor.CheckedChanged += new System.EventHandler(this._cbStartMonitor_CheckedChanged);
            // 
            // lBeAlignmentPassword
            // 
            this.lBeAlignmentPassword.AutoSize = true;
            this.lBeAlignmentPassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lBeAlignmentPassword.Location = new System.Drawing.Point(303, 13);
            this.lBeAlignmentPassword.Name = "lBeAlignmentPassword";
            this.lBeAlignmentPassword.Size = new System.Drawing.Size(122, 12);
            this.lBeAlignmentPassword.TabIndex = 4;
            this.lBeAlignmentPassword.Text = "Be Alignment Password :";
            // 
            // tbBeAlignmentPassword123
            // 
            this.tbBeAlignmentPassword123.Location = new System.Drawing.Point(431, 10);
            this.tbBeAlignmentPassword123.Name = "tbBeAlignmentPassword123";
            this.tbBeAlignmentPassword123.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword123.TabIndex = 5;
            this.tbBeAlignmentPassword123.Text = "0";
            // 
            // bLog
            // 
            this.bLog.BackColor = System.Drawing.SystemColors.Control;
            this.bLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bLog.Location = new System.Drawing.Point(905, 8);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(50, 23);
            this.bLog.TabIndex = 6;
            this.bLog.Text = "Log";
            this.bLog.UseVisualStyleBackColor = false;
            this.bLog.Click += new System.EventHandler(this._bLog_Click);
            // 
            // bClearLog
            // 
            this.bClearLog.BackColor = System.Drawing.SystemColors.Control;
            this.bClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bClearLog.Location = new System.Drawing.Point(555, 8);
            this.bClearLog.Name = "bClearLog";
            this.bClearLog.Size = new System.Drawing.Size(60, 23);
            this.bClearLog.TabIndex = 7;
            this.bClearLog.Text = "Clear Log";
            this.bClearLog.UseVisualStyleBackColor = false;
            this.bClearLog.Click += new System.EventHandler(this._bClearLog_Click);
            // 
            // lLogLable
            // 
            this.lLogLable.AutoSize = true;
            this.lLogLable.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lLogLable.Location = new System.Drawing.Point(657, 13);
            this.lLogLable.Name = "lLogLable";
            this.lLogLable.Size = new System.Drawing.Size(59, 12);
            this.lLogLable.TabIndex = 8;
            this.lLogLable.Text = "Log Lable :";
            // 
            // tbLogLable
            // 
            this.tbLogLable.Location = new System.Drawing.Point(722, 10);
            this.tbLogLable.Name = "tbLogLable";
            this.tbLogLable.Size = new System.Drawing.Size(70, 22);
            this.tbLogLable.TabIndex = 9;
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.SystemColors.Control;
            this.bSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bSave.Location = new System.Drawing.Point(1211, 8);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(45, 23);
            this.bSave.TabIndex = 10;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this._bSave_Click);
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRecord.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.GridColor = System.Drawing.Color.Silver;
            this.dgvRecord.Location = new System.Drawing.Point(0, 0);
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.RowTemplate.Height = 24;
            this.dgvRecord.Size = new System.Drawing.Size(1244, 611);
            this.dgvRecord.TabIndex = 11;
            // 
            // tbBeAlignmentPassword124
            // 
            this.tbBeAlignmentPassword124.Location = new System.Drawing.Point(462, 10);
            this.tbBeAlignmentPassword124.Name = "tbBeAlignmentPassword124";
            this.tbBeAlignmentPassword124.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword124.TabIndex = 12;
            this.tbBeAlignmentPassword124.Text = "0";
            // 
            // tbBeAlignmentPassword125
            // 
            this.tbBeAlignmentPassword125.Location = new System.Drawing.Point(493, 10);
            this.tbBeAlignmentPassword125.Name = "tbBeAlignmentPassword125";
            this.tbBeAlignmentPassword125.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword125.TabIndex = 13;
            this.tbBeAlignmentPassword125.Text = "0";
            // 
            // tbBeAlignmentPassword126
            // 
            this.tbBeAlignmentPassword126.Location = new System.Drawing.Point(524, 10);
            this.tbBeAlignmentPassword126.Name = "tbBeAlignmentPassword126";
            this.tbBeAlignmentPassword126.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword126.TabIndex = 14;
            this.tbBeAlignmentPassword126.Text = "0";
            // 
            // tcFunction
            // 
            this.tcFunction.Controls.Add(this.tpLensAlignment);
            this.tcFunction.Controls.Add(this.tpLog);
            this.tcFunction.Controls.Add(this.tpCorrect);
            this.tcFunction.Location = new System.Drawing.Point(8, 38);
            this.tcFunction.Name = "tcFunction";
            this.tcFunction.SelectedIndex = 0;
            this.tcFunction.Size = new System.Drawing.Size(1252, 637);
            this.tcFunction.TabIndex = 15;
            // 
            // tpLensAlignment
            // 
            this.tpLensAlignment.BackColor = System.Drawing.Color.Black;
            this.tpLensAlignment.Controls.Add(this.ucLensAlignment);
            this.tpLensAlignment.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.tpLensAlignment.Location = new System.Drawing.Point(4, 22);
            this.tpLensAlignment.Name = "tpLensAlignment";
            this.tpLensAlignment.Padding = new System.Windows.Forms.Padding(3);
            this.tpLensAlignment.Size = new System.Drawing.Size(1244, 611);
            this.tpLensAlignment.TabIndex = 0;
            this.tpLensAlignment.Text = "Lens Alignment";
            // 
            // ucLensAlignment
            // 
            this.ucLensAlignment.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ucLensAlignment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLensAlignment.Location = new System.Drawing.Point(0, 0);
            this.ucLensAlignment.Name = "ucLensAlignment";
            this.ucLensAlignment.Size = new System.Drawing.Size(1244, 611);
            this.ucLensAlignment.TabIndex = 2;
            // 
            // tpLog
            // 
            this.tpLog.BackColor = System.Drawing.SystemColors.Window;
            this.tpLog.Controls.Add(this.dgvRecord);
            this.tpLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tpLog.Location = new System.Drawing.Point(4, 22);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(1244, 611);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "Log";
            // 
            // tpCorrect
            // 
            this.tpCorrect.Controls.Add(this.bLoadCorrectFile);
            this.tpCorrect.Controls.Add(this.bSaveCorrectFile);
            this.tpCorrect.Controls.Add(this.tbCorrectFilePath);
            this.tpCorrect.Controls.Add(this.lCorrectFilePath);
            this.tpCorrect.Controls.Add(this.tbLightSourcePassword126);
            this.tpCorrect.Controls.Add(this.tbLightSourcePassword125);
            this.tpCorrect.Controls.Add(this.tbLightSourcePassword124);
            this.tpCorrect.Controls.Add(this.tbLightSourcePassword123);
            this.tpCorrect.Controls.Add(this.lLightSourcePassword);
            this.tpCorrect.Controls.Add(this.gbRxRateCorrect);
            this.tpCorrect.Location = new System.Drawing.Point(4, 22);
            this.tpCorrect.Name = "tpCorrect";
            this.tpCorrect.Padding = new System.Windows.Forms.Padding(3);
            this.tpCorrect.Size = new System.Drawing.Size(1244, 611);
            this.tpCorrect.TabIndex = 2;
            this.tpCorrect.Text = "Correct";
            this.tpCorrect.UseVisualStyleBackColor = true;
            // 
            // bLoadCorrectFile
            // 
            this.bLoadCorrectFile.Location = new System.Drawing.Point(1163, 6);
            this.bLoadCorrectFile.Name = "bLoadCorrectFile";
            this.bLoadCorrectFile.Size = new System.Drawing.Size(75, 23);
            this.bLoadCorrectFile.TabIndex = 13;
            this.bLoadCorrectFile.Text = "Load File";
            this.bLoadCorrectFile.UseVisualStyleBackColor = true;
            // 
            // bSaveCorrectFile
            // 
            this.bSaveCorrectFile.Location = new System.Drawing.Point(1082, 6);
            this.bSaveCorrectFile.Name = "bSaveCorrectFile";
            this.bSaveCorrectFile.Size = new System.Drawing.Size(75, 23);
            this.bSaveCorrectFile.TabIndex = 12;
            this.bSaveCorrectFile.Text = "Save File";
            this.bSaveCorrectFile.UseVisualStyleBackColor = true;
            // 
            // tbCorrectFilePath
            // 
            this.tbCorrectFilePath.Enabled = false;
            this.tbCorrectFilePath.Location = new System.Drawing.Point(476, 6);
            this.tbCorrectFilePath.Name = "tbCorrectFilePath";
            this.tbCorrectFilePath.Size = new System.Drawing.Size(600, 22);
            this.tbCorrectFilePath.TabIndex = 11;
            // 
            // lCorrectFilePath
            // 
            this.lCorrectFilePath.AutoSize = true;
            this.lCorrectFilePath.Location = new System.Drawing.Point(419, 9);
            this.lCorrectFilePath.Name = "lCorrectFilePath";
            this.lCorrectFilePath.Size = new System.Drawing.Size(51, 12);
            this.lCorrectFilePath.TabIndex = 10;
            this.lCorrectFilePath.Text = "File Path :";
            // 
            // tbLightSourcePassword126
            // 
            this.tbLightSourcePassword126.Location = new System.Drawing.Point(222, 6);
            this.tbLightSourcePassword126.Name = "tbLightSourcePassword126";
            this.tbLightSourcePassword126.PasswordChar = '*';
            this.tbLightSourcePassword126.Size = new System.Drawing.Size(25, 22);
            this.tbLightSourcePassword126.TabIndex = 9;
            this.tbLightSourcePassword126.Text = "34";
            // 
            // tbLightSourcePassword125
            // 
            this.tbLightSourcePassword125.Location = new System.Drawing.Point(191, 6);
            this.tbLightSourcePassword125.Name = "tbLightSourcePassword125";
            this.tbLightSourcePassword125.PasswordChar = '*';
            this.tbLightSourcePassword125.Size = new System.Drawing.Size(25, 22);
            this.tbLightSourcePassword125.TabIndex = 8;
            this.tbLightSourcePassword125.Text = "33";
            // 
            // tbLightSourcePassword124
            // 
            this.tbLightSourcePassword124.Location = new System.Drawing.Point(160, 6);
            this.tbLightSourcePassword124.Name = "tbLightSourcePassword124";
            this.tbLightSourcePassword124.PasswordChar = '*';
            this.tbLightSourcePassword124.Size = new System.Drawing.Size(25, 22);
            this.tbLightSourcePassword124.TabIndex = 7;
            this.tbLightSourcePassword124.Text = "32";
            // 
            // tbLightSourcePassword123
            // 
            this.tbLightSourcePassword123.Location = new System.Drawing.Point(129, 6);
            this.tbLightSourcePassword123.Name = "tbLightSourcePassword123";
            this.tbLightSourcePassword123.PasswordChar = '*';
            this.tbLightSourcePassword123.Size = new System.Drawing.Size(25, 22);
            this.tbLightSourcePassword123.TabIndex = 6;
            this.tbLightSourcePassword123.Text = "33";
            // 
            // lLightSourcePassword
            // 
            this.lLightSourcePassword.AutoSize = true;
            this.lLightSourcePassword.Location = new System.Drawing.Point(6, 9);
            this.lLightSourcePassword.Name = "lLightSourcePassword";
            this.lLightSourcePassword.Size = new System.Drawing.Size(117, 12);
            this.lLightSourcePassword.TabIndex = 2;
            this.lLightSourcePassword.Text = "Light Source Password :";
            // 
            // gbRxRateCorrect
            // 
            this.gbRxRateCorrect.Controls.Add(this.bAutoCorrect);
            this.gbRxRateCorrect.Controls.Add(this.bReset);
            this.gbRxRateCorrect.Controls.Add(this.bWrite);
            this.gbRxRateCorrect.Controls.Add(this.lRxPowerUnit);
            this.gbRxRateCorrect.Controls.Add(this.lRxRateUnit);
            this.gbRxRateCorrect.Controls.Add(this.lRssiUnit);
            this.gbRxRateCorrect.Controls.Add(this.lInputPowerUnit);
            this.gbRxRateCorrect.Controls.Add(this.bRead);
            this.gbRxRateCorrect.Controls.Add(this.tbTx1RxPower);
            this.gbRxRateCorrect.Controls.Add(this.tbTx2RxPower);
            this.gbRxRateCorrect.Controls.Add(this.tbTx3RxPower);
            this.gbRxRateCorrect.Controls.Add(this.tbTx4RxPower);
            this.gbRxRateCorrect.Controls.Add(this.lRxPower);
            this.gbRxRateCorrect.Controls.Add(this.tbRx1Rate);
            this.gbRxRateCorrect.Controls.Add(this.tbRx2Rate);
            this.gbRxRateCorrect.Controls.Add(this.tbRx3Rate);
            this.gbRxRateCorrect.Controls.Add(this.tbRx4Rate);
            this.gbRxRateCorrect.Controls.Add(this.lRxRateMin);
            this.gbRxRateCorrect.Controls.Add(this.tbRxRateMin);
            this.gbRxRateCorrect.Controls.Add(this.lRxRateMax);
            this.gbRxRateCorrect.Controls.Add(this.tbRxRateMax);
            this.gbRxRateCorrect.Controls.Add(this.lRateDefault);
            this.gbRxRateCorrect.Controls.Add(this.tbRxRateDefault);
            this.gbRxRateCorrect.Controls.Add(this.lRxRate);
            this.gbRxRateCorrect.Controls.Add(this.tbRx1Rssi);
            this.gbRxRateCorrect.Controls.Add(this.tbRx2Rssi);
            this.gbRxRateCorrect.Controls.Add(this.tbRx3Rssi);
            this.gbRxRateCorrect.Controls.Add(this.tbRx4Rssi);
            this.gbRxRateCorrect.Controls.Add(this.lRssi);
            this.gbRxRateCorrect.Controls.Add(this.lRx1InputPower);
            this.gbRxRateCorrect.Controls.Add(this.tbRx1InputPower);
            this.gbRxRateCorrect.Controls.Add(this.lRx2InputPower);
            this.gbRxRateCorrect.Controls.Add(this.tbRx2InputPower);
            this.gbRxRateCorrect.Controls.Add(this.lRx3InputPower);
            this.gbRxRateCorrect.Controls.Add(this.tbRx3InputPower);
            this.gbRxRateCorrect.Controls.Add(this.lRx4InputPower);
            this.gbRxRateCorrect.Controls.Add(this.tbRx4InputPower);
            this.gbRxRateCorrect.Controls.Add(this.lRxInputPower);
            this.gbRxRateCorrect.Location = new System.Drawing.Point(6, 34);
            this.gbRxRateCorrect.Name = "gbRxRateCorrect";
            this.gbRxRateCorrect.Size = new System.Drawing.Size(614, 149);
            this.gbRxRateCorrect.TabIndex = 1;
            this.gbRxRateCorrect.TabStop = false;
            this.gbRxRateCorrect.Text = "Light Sourec Rx Rate";
            // 
            // bAutoCorrect
            // 
            this.bAutoCorrect.Location = new System.Drawing.Point(475, 117);
            this.bAutoCorrect.Name = "bAutoCorrect";
            this.bAutoCorrect.Size = new System.Drawing.Size(126, 23);
            this.bAutoCorrect.TabIndex = 37;
            this.bAutoCorrect.Text = "Auto Correct";
            this.bAutoCorrect.UseVisualStyleBackColor = true;
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(475, 88);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(126, 23);
            this.bReset.TabIndex = 36;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            // 
            // bWrite
            // 
            this.bWrite.Location = new System.Drawing.Point(541, 59);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(60, 23);
            this.bWrite.TabIndex = 35;
            this.bWrite.Text = "Write";
            this.bWrite.UseVisualStyleBackColor = true;
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // lRxPowerUnit
            // 
            this.lRxPowerUnit.AutoSize = true;
            this.lRxPowerUnit.Location = new System.Drawing.Point(440, 120);
            this.lRxPowerUnit.Name = "lRxPowerUnit";
            this.lRxPowerUnit.Size = new System.Drawing.Size(22, 12);
            this.lRxPowerUnit.TabIndex = 34;
            this.lRxPowerUnit.Text = "uW";
            // 
            // lRxRateUnit
            // 
            this.lRxRateUnit.AutoSize = true;
            this.lRxRateUnit.Location = new System.Drawing.Point(440, 92);
            this.lRxRateUnit.Name = "lRxRateUnit";
            this.lRxRateUnit.Size = new System.Drawing.Size(26, 12);
            this.lRxRateUnit.TabIndex = 33;
            this.lRxRateUnit.Text = "0.01";
            // 
            // lRssiUnit
            // 
            this.lRssiUnit.AutoSize = true;
            this.lRssiUnit.Location = new System.Drawing.Point(440, 64);
            this.lRssiUnit.Name = "lRssiUnit";
            this.lRssiUnit.Size = new System.Drawing.Size(19, 12);
            this.lRssiUnit.TabIndex = 32;
            this.lRssiUnit.Text = "uA";
            // 
            // lInputPowerUnit
            // 
            this.lInputPowerUnit.AutoSize = true;
            this.lInputPowerUnit.Location = new System.Drawing.Point(440, 36);
            this.lInputPowerUnit.Name = "lInputPowerUnit";
            this.lInputPowerUnit.Size = new System.Drawing.Size(22, 12);
            this.lInputPowerUnit.TabIndex = 31;
            this.lInputPowerUnit.Text = "uW";
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(475, 59);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(60, 23);
            this.bRead.TabIndex = 30;
            this.bRead.Text = "Read";
            this.bRead.UseVisualStyleBackColor = true;
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // tbTx1RxPower
            // 
            this.tbTx1RxPower.Enabled = false;
            this.tbTx1RxPower.Location = new System.Drawing.Point(384, 117);
            this.tbTx1RxPower.Name = "tbTx1RxPower";
            this.tbTx1RxPower.Size = new System.Drawing.Size(50, 22);
            this.tbTx1RxPower.TabIndex = 29;
            // 
            // tbTx2RxPower
            // 
            this.tbTx2RxPower.Enabled = false;
            this.tbTx2RxPower.Location = new System.Drawing.Point(328, 117);
            this.tbTx2RxPower.Name = "tbTx2RxPower";
            this.tbTx2RxPower.Size = new System.Drawing.Size(50, 22);
            this.tbTx2RxPower.TabIndex = 28;
            // 
            // tbTx3RxPower
            // 
            this.tbTx3RxPower.Enabled = false;
            this.tbTx3RxPower.Location = new System.Drawing.Point(272, 117);
            this.tbTx3RxPower.Name = "tbTx3RxPower";
            this.tbTx3RxPower.Size = new System.Drawing.Size(50, 22);
            this.tbTx3RxPower.TabIndex = 27;
            // 
            // tbTx4RxPower
            // 
            this.tbTx4RxPower.Enabled = false;
            this.tbTx4RxPower.Location = new System.Drawing.Point(216, 117);
            this.tbTx4RxPower.Name = "tbTx4RxPower";
            this.tbTx4RxPower.Size = new System.Drawing.Size(50, 22);
            this.tbTx4RxPower.TabIndex = 26;
            // 
            // lRxPower
            // 
            this.lRxPower.AutoSize = true;
            this.lRxPower.Location = new System.Drawing.Point(6, 120);
            this.lRxPower.Name = "lRxPower";
            this.lRxPower.Size = new System.Drawing.Size(40, 12);
            this.lRxPower.TabIndex = 25;
            this.lRxPower.Text = "Power :";
            // 
            // tbRx1Rate
            // 
            this.tbRx1Rate.Location = new System.Drawing.Point(384, 89);
            this.tbRx1Rate.Name = "tbRx1Rate";
            this.tbRx1Rate.Size = new System.Drawing.Size(50, 22);
            this.tbRx1Rate.TabIndex = 24;
            // 
            // tbRx2Rate
            // 
            this.tbRx2Rate.Location = new System.Drawing.Point(328, 89);
            this.tbRx2Rate.Name = "tbRx2Rate";
            this.tbRx2Rate.Size = new System.Drawing.Size(50, 22);
            this.tbRx2Rate.TabIndex = 23;
            // 
            // tbRx3Rate
            // 
            this.tbRx3Rate.Location = new System.Drawing.Point(272, 89);
            this.tbRx3Rate.Name = "tbRx3Rate";
            this.tbRx3Rate.Size = new System.Drawing.Size(50, 22);
            this.tbRx3Rate.TabIndex = 22;
            // 
            // tbRx4Rate
            // 
            this.tbRx4Rate.Location = new System.Drawing.Point(216, 89);
            this.tbRx4Rate.Name = "tbRx4Rate";
            this.tbRx4Rate.Size = new System.Drawing.Size(50, 22);
            this.tbRx4Rate.TabIndex = 21;
            // 
            // lRxRateMin
            // 
            this.lRxRateMin.AutoSize = true;
            this.lRxRateMin.Location = new System.Drawing.Point(173, 18);
            this.lRxRateMin.Name = "lRxRateMin";
            this.lRxRateMin.Size = new System.Drawing.Size(24, 12);
            this.lRxRateMin.TabIndex = 20;
            this.lRxRateMin.Text = "Min";
            // 
            // tbRxRateMin
            // 
            this.tbRxRateMin.Location = new System.Drawing.Point(160, 89);
            this.tbRxRateMin.Name = "tbRxRateMin";
            this.tbRxRateMin.Size = new System.Drawing.Size(50, 22);
            this.tbRxRateMin.TabIndex = 19;
            // 
            // lRxRateMax
            // 
            this.lRxRateMax.AutoSize = true;
            this.lRxRateMax.Location = new System.Drawing.Point(116, 18);
            this.lRxRateMax.Name = "lRxRateMax";
            this.lRxRateMax.Size = new System.Drawing.Size(26, 12);
            this.lRxRateMax.TabIndex = 18;
            this.lRxRateMax.Text = "Max";
            // 
            // tbRxRateMax
            // 
            this.tbRxRateMax.Location = new System.Drawing.Point(104, 89);
            this.tbRxRateMax.Name = "tbRxRateMax";
            this.tbRxRateMax.Size = new System.Drawing.Size(50, 22);
            this.tbRxRateMax.TabIndex = 17;
            // 
            // lRateDefault
            // 
            this.lRateDefault.AutoSize = true;
            this.lRateDefault.Location = new System.Drawing.Point(54, 18);
            this.lRateDefault.Name = "lRateDefault";
            this.lRateDefault.Size = new System.Drawing.Size(39, 12);
            this.lRateDefault.TabIndex = 16;
            this.lRateDefault.Text = "Default";
            // 
            // tbRxRateDefault
            // 
            this.tbRxRateDefault.Enabled = false;
            this.tbRxRateDefault.Location = new System.Drawing.Point(48, 89);
            this.tbRxRateDefault.Name = "tbRxRateDefault";
            this.tbRxRateDefault.Size = new System.Drawing.Size(50, 22);
            this.tbRxRateDefault.TabIndex = 15;
            // 
            // lRxRate
            // 
            this.lRxRate.AutoSize = true;
            this.lRxRate.Location = new System.Drawing.Point(6, 92);
            this.lRxRate.Name = "lRxRate";
            this.lRxRate.Size = new System.Drawing.Size(32, 12);
            this.lRxRate.TabIndex = 14;
            this.lRxRate.Text = "Rate :";
            // 
            // tbRx1Rssi
            // 
            this.tbRx1Rssi.Enabled = false;
            this.tbRx1Rssi.Location = new System.Drawing.Point(384, 61);
            this.tbRx1Rssi.Name = "tbRx1Rssi";
            this.tbRx1Rssi.Size = new System.Drawing.Size(50, 22);
            this.tbRx1Rssi.TabIndex = 13;
            // 
            // tbRx2Rssi
            // 
            this.tbRx2Rssi.Enabled = false;
            this.tbRx2Rssi.Location = new System.Drawing.Point(328, 61);
            this.tbRx2Rssi.Name = "tbRx2Rssi";
            this.tbRx2Rssi.Size = new System.Drawing.Size(50, 22);
            this.tbRx2Rssi.TabIndex = 12;
            // 
            // tbRx3Rssi
            // 
            this.tbRx3Rssi.Enabled = false;
            this.tbRx3Rssi.Location = new System.Drawing.Point(272, 61);
            this.tbRx3Rssi.Name = "tbRx3Rssi";
            this.tbRx3Rssi.Size = new System.Drawing.Size(50, 22);
            this.tbRx3Rssi.TabIndex = 11;
            // 
            // tbRx4Rssi
            // 
            this.tbRx4Rssi.Enabled = false;
            this.tbRx4Rssi.Location = new System.Drawing.Point(216, 61);
            this.tbRx4Rssi.Name = "tbRx4Rssi";
            this.tbRx4Rssi.Size = new System.Drawing.Size(50, 22);
            this.tbRx4Rssi.TabIndex = 10;
            // 
            // lRssi
            // 
            this.lRssi.AutoSize = true;
            this.lRssi.Location = new System.Drawing.Point(6, 64);
            this.lRssi.Name = "lRssi";
            this.lRssi.Size = new System.Drawing.Size(35, 12);
            this.lRssi.TabIndex = 9;
            this.lRssi.Text = "RSSI :";
            // 
            // lRx1InputPower
            // 
            this.lRx1InputPower.AutoSize = true;
            this.lRx1InputPower.Location = new System.Drawing.Point(397, 18);
            this.lRx1InputPower.Name = "lRx1InputPower";
            this.lRx1InputPower.Size = new System.Drawing.Size(25, 12);
            this.lRx1InputPower.TabIndex = 8;
            this.lRx1InputPower.Text = "Rx1";
            // 
            // tbRx1InputPower
            // 
            this.tbRx1InputPower.Location = new System.Drawing.Point(384, 33);
            this.tbRx1InputPower.Name = "tbRx1InputPower";
            this.tbRx1InputPower.Size = new System.Drawing.Size(50, 22);
            this.tbRx1InputPower.TabIndex = 7;
            // 
            // lRx2InputPower
            // 
            this.lRx2InputPower.AutoSize = true;
            this.lRx2InputPower.Location = new System.Drawing.Point(341, 18);
            this.lRx2InputPower.Name = "lRx2InputPower";
            this.lRx2InputPower.Size = new System.Drawing.Size(25, 12);
            this.lRx2InputPower.TabIndex = 6;
            this.lRx2InputPower.Text = "Rx2";
            // 
            // tbRx2InputPower
            // 
            this.tbRx2InputPower.Location = new System.Drawing.Point(328, 33);
            this.tbRx2InputPower.Name = "tbRx2InputPower";
            this.tbRx2InputPower.Size = new System.Drawing.Size(50, 22);
            this.tbRx2InputPower.TabIndex = 5;
            // 
            // lRx3InputPower
            // 
            this.lRx3InputPower.AutoSize = true;
            this.lRx3InputPower.Location = new System.Drawing.Point(285, 18);
            this.lRx3InputPower.Name = "lRx3InputPower";
            this.lRx3InputPower.Size = new System.Drawing.Size(25, 12);
            this.lRx3InputPower.TabIndex = 4;
            this.lRx3InputPower.Text = "Rx3";
            // 
            // tbRx3InputPower
            // 
            this.tbRx3InputPower.Location = new System.Drawing.Point(272, 33);
            this.tbRx3InputPower.Name = "tbRx3InputPower";
            this.tbRx3InputPower.Size = new System.Drawing.Size(50, 22);
            this.tbRx3InputPower.TabIndex = 3;
            // 
            // lRx4InputPower
            // 
            this.lRx4InputPower.AutoSize = true;
            this.lRx4InputPower.Location = new System.Drawing.Point(229, 18);
            this.lRx4InputPower.Name = "lRx4InputPower";
            this.lRx4InputPower.Size = new System.Drawing.Size(25, 12);
            this.lRx4InputPower.TabIndex = 2;
            this.lRx4InputPower.Text = "Rx4";
            // 
            // tbRx4InputPower
            // 
            this.tbRx4InputPower.Location = new System.Drawing.Point(216, 33);
            this.tbRx4InputPower.Name = "tbRx4InputPower";
            this.tbRx4InputPower.Size = new System.Drawing.Size(50, 22);
            this.tbRx4InputPower.TabIndex = 1;
            // 
            // lRxInputPower
            // 
            this.lRxInputPower.AutoSize = true;
            this.lRxInputPower.Location = new System.Drawing.Point(6, 36);
            this.lRxInputPower.Name = "lRxInputPower";
            this.lRxInputPower.Size = new System.Drawing.Size(36, 12);
            this.lRxInputPower.TabIndex = 0;
            this.lRxInputPower.Text = "Input :";
            // 
            // tbSerialNumber
            // 
            this.tbSerialNumber.Location = new System.Drawing.Point(829, 9);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.Size = new System.Drawing.Size(70, 22);
            this.tbSerialNumber.TabIndex = 17;
            // 
            // lSerialNumber
            // 
            this.lSerialNumber.AutoSize = true;
            this.lSerialNumber.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lSerialNumber.Location = new System.Drawing.Point(798, 13);
            this.lSerialNumber.Name = "lSerialNumber";
            this.lSerialNumber.Size = new System.Drawing.Size(25, 12);
            this.lSerialNumber.TabIndex = 16;
            this.lSerialNumber.Text = "SN :";
            // 
            // fLensAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1272, 683);
            this.Controls.Add(this.tbSerialNumber);
            this.Controls.Add(this.lSerialNumber);
            this.Controls.Add(this.tcFunction);
            this.Controls.Add(this.tbBeAlignmentPassword126);
            this.Controls.Add(this.tbBeAlignmentPassword125);
            this.Controls.Add(this.tbBeAlignmentPassword124);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbLogLable);
            this.Controls.Add(this.lLogLable);
            this.Controls.Add(this.bClearLog);
            this.Controls.Add(this.bLog);
            this.Controls.Add(this.tbBeAlignmentPassword123);
            this.Controls.Add(this.lBeAlignmentPassword);
            this.Controls.Add(this.cbStartMonitor);
            this.Controls.Add(this.cbBeAlignmentConnected);
            this.Controls.Add(this.cbLightSourceConnected);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 710);
            this.MinimumSize = new System.Drawing.Size(1280, 710);
            this.Name = "fLensAlignment";
            this.Text = "Lens Alignment";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.tcFunction.ResumeLayout(false);
            this.tpLensAlignment.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.tpCorrect.ResumeLayout(false);
            this.tpCorrect.PerformLayout();
            this.gbRxRateCorrect.ResumeLayout(false);
            this.gbRxRateCorrect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbLightSourceConnected;
        private System.Windows.Forms.CheckBox cbBeAlignmentConnected;
        private UcLensAlignment ucLensAlignment;
        private System.Windows.Forms.CheckBox cbStartMonitor;
        private System.Windows.Forms.Label lBeAlignmentPassword;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword123;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.Button bClearLog;
        private System.Windows.Forms.Label lLogLable;
        private System.Windows.Forms.TextBox tbLogLable;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.DataGridView dgvRecord;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword124;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword125;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword126;
        private System.Windows.Forms.TabControl tcFunction;
        private System.Windows.Forms.TabPage tpLensAlignment;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TabPage tpCorrect;
        private System.Windows.Forms.GroupBox gbRxRateCorrect;
        private System.Windows.Forms.TextBox tbRx1Rssi;
        private System.Windows.Forms.TextBox tbRx2Rssi;
        private System.Windows.Forms.TextBox tbRx3Rssi;
        private System.Windows.Forms.TextBox tbRx4Rssi;
        private System.Windows.Forms.Label lRssi;
        private System.Windows.Forms.Label lRx1InputPower;
        private System.Windows.Forms.TextBox tbRx1InputPower;
        private System.Windows.Forms.Label lRx2InputPower;
        private System.Windows.Forms.TextBox tbRx2InputPower;
        private System.Windows.Forms.Label lRx3InputPower;
        private System.Windows.Forms.TextBox tbRx3InputPower;
        private System.Windows.Forms.Label lRx4InputPower;
        private System.Windows.Forms.TextBox tbRx4InputPower;
        private System.Windows.Forms.Label lRxInputPower;
        private System.Windows.Forms.TextBox tbTx1RxPower;
        private System.Windows.Forms.TextBox tbTx2RxPower;
        private System.Windows.Forms.TextBox tbTx3RxPower;
        private System.Windows.Forms.TextBox tbTx4RxPower;
        private System.Windows.Forms.Label lRxPower;
        private System.Windows.Forms.TextBox tbRx1Rate;
        private System.Windows.Forms.TextBox tbRx2Rate;
        private System.Windows.Forms.TextBox tbRx3Rate;
        private System.Windows.Forms.TextBox tbRx4Rate;
        private System.Windows.Forms.Label lRxRateMin;
        private System.Windows.Forms.TextBox tbRxRateMin;
        private System.Windows.Forms.Label lRxRateMax;
        private System.Windows.Forms.TextBox tbRxRateMax;
        private System.Windows.Forms.Label lRateDefault;
        private System.Windows.Forms.TextBox tbRxRateDefault;
        private System.Windows.Forms.Label lRxRate;
        private System.Windows.Forms.TextBox tbLightSourcePassword126;
        private System.Windows.Forms.TextBox tbLightSourcePassword125;
        private System.Windows.Forms.TextBox tbLightSourcePassword124;
        private System.Windows.Forms.TextBox tbLightSourcePassword123;
        private System.Windows.Forms.Label lLightSourcePassword;
        private System.Windows.Forms.Button bAutoCorrect;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Button bWrite;
        private System.Windows.Forms.Label lRxPowerUnit;
        private System.Windows.Forms.Label lRxRateUnit;
        private System.Windows.Forms.Label lRssiUnit;
        private System.Windows.Forms.Label lInputPowerUnit;
        private System.Windows.Forms.Button bRead;
        private System.Windows.Forms.Button bLoadCorrectFile;
        private System.Windows.Forms.Button bSaveCorrectFile;
        private System.Windows.Forms.TextBox tbCorrectFilePath;
        private System.Windows.Forms.Label lCorrectFilePath;
        private System.Windows.Forms.TextBox tbSerialNumber;
        private System.Windows.Forms.Label lSerialNumber;
    }
}

