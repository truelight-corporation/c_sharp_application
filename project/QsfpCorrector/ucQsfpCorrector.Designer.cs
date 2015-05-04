namespace QsfpCorrector
{
    partial class UcQsfpCorrector
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
            this.tbTxTemperature = new System.Windows.Forms.TextBox();
            this.lTemperatureOffset = new System.Windows.Forms.Label();
            this.lDegC = new System.Windows.Forms.Label();
            this.tbTemperatureOffset = new System.Windows.Forms.TextBox();
            this.bTemperatureRead = new System.Windows.Forms.Button();
            this.bTemperatureWrite = new System.Windows.Forms.Button();
            this.cbTOAutoCorrect = new System.Windows.Forms.CheckBox();
            this.gbTemperature = new System.Windows.Forms.GroupBox();
            this.lTemperatureOffsetDegC = new System.Windows.Forms.Label();
            this.tbTemperature = new System.Windows.Forms.TextBox();
            this.lTemperature = new System.Windows.Forms.Label();
            this.lTxTemperature = new System.Windows.Forms.Label();
            this.gbRxPowerRate = new System.Windows.Forms.GroupBox();
            this.lRxPowerRateDefault = new System.Windows.Forms.Label();
            this.tbRxPowerRateDefault = new System.Windows.Forms.TextBox();
            this.lRxPowerRateUnit = new System.Windows.Forms.Label();
            this.bRxPowerRateWrite = new System.Windows.Forms.Button();
            this.bRxPowerRateRead = new System.Windows.Forms.Button();
            this.cbRPRAutoCorrect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lRssiUA = new System.Windows.Forms.Label();
            this.lInputUW = new System.Windows.Forms.Label();
            this.tbRxPower4 = new System.Windows.Forms.TextBox();
            this.tbRxPowerRate4 = new System.Windows.Forms.TextBox();
            this.tbRssi4 = new System.Windows.Forms.TextBox();
            this.tbRxInputPower4 = new System.Windows.Forms.TextBox();
            this.lRxCh4 = new System.Windows.Forms.Label();
            this.tbRxPower3 = new System.Windows.Forms.TextBox();
            this.tbRxPowerRate3 = new System.Windows.Forms.TextBox();
            this.tbRssi3 = new System.Windows.Forms.TextBox();
            this.tbRxInputPower3 = new System.Windows.Forms.TextBox();
            this.lRxCh3 = new System.Windows.Forms.Label();
            this.tbRxPower2 = new System.Windows.Forms.TextBox();
            this.tbRxPower1 = new System.Windows.Forms.TextBox();
            this.lRxPower1 = new System.Windows.Forms.Label();
            this.tbRxPowerRate2 = new System.Windows.Forms.TextBox();
            this.tbRssi2 = new System.Windows.Forms.TextBox();
            this.tbRxInputPower2 = new System.Windows.Forms.TextBox();
            this.lRxCh2 = new System.Windows.Forms.Label();
            this.tbRxPowerRate1 = new System.Windows.Forms.TextBox();
            this.lRxPowerRate = new System.Windows.Forms.Label();
            this.tbRssi1 = new System.Windows.Forms.TextBox();
            this.lRxRssi = new System.Windows.Forms.Label();
            this.tbRxInputPower1 = new System.Windows.Forms.TextBox();
            this.lInputPower = new System.Windows.Forms.Label();
            this.lRxCh1 = new System.Windows.Forms.Label();
            this.lRxPowerRateMax = new System.Windows.Forms.Label();
            this.lRxPowerRateMin = new System.Windows.Forms.Label();
            this.tbRxPowerRateMax = new System.Windows.Forms.TextBox();
            this.tbRxPowerRateMin = new System.Windows.Forms.TextBox();
            this.gbTemperature.SuspendLayout();
            this.gbRxPowerRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTxTemperature
            // 
            this.tbTxTemperature.Location = new System.Drawing.Point(10, 33);
            this.tbTxTemperature.Name = "tbTxTemperature";
            this.tbTxTemperature.ReadOnly = true;
            this.tbTxTemperature.Size = new System.Drawing.Size(50, 22);
            this.tbTxTemperature.TabIndex = 1;
            // 
            // lTemperatureOffset
            // 
            this.lTemperatureOffset.AutoSize = true;
            this.lTemperatureOffset.Location = new System.Drawing.Point(159, 36);
            this.lTemperatureOffset.Name = "lTemperatureOffset";
            this.lTemperatureOffset.Size = new System.Drawing.Size(101, 12);
            this.lTemperatureOffset.TabIndex = 2;
            this.lTemperatureOffset.Text = "Temperature Offset :";
            // 
            // lDegC
            // 
            this.lDegC.AutoSize = true;
            this.lDegC.Location = new System.Drawing.Point(121, 36);
            this.lDegC.Name = "lDegC";
            this.lDegC.Size = new System.Drawing.Size(32, 12);
            this.lDegC.TabIndex = 3;
            this.lDegC.Text = "DegC";
            // 
            // tbTemperatureOffset
            // 
            this.tbTemperatureOffset.Location = new System.Drawing.Point(266, 33);
            this.tbTemperatureOffset.Name = "tbTemperatureOffset";
            this.tbTemperatureOffset.Size = new System.Drawing.Size(60, 22);
            this.tbTemperatureOffset.TabIndex = 4;
            // 
            // bTemperatureRead
            // 
            this.bTemperatureRead.Location = new System.Drawing.Point(482, 31);
            this.bTemperatureRead.Name = "bTemperatureRead";
            this.bTemperatureRead.Size = new System.Drawing.Size(60, 23);
            this.bTemperatureRead.TabIndex = 5;
            this.bTemperatureRead.Text = "Read";
            this.bTemperatureRead.UseVisualStyleBackColor = true;
            this.bTemperatureRead.Click += new System.EventHandler(this._bTemperatureReadClick);
            // 
            // bTemperatureWrite
            // 
            this.bTemperatureWrite.Location = new System.Drawing.Point(548, 31);
            this.bTemperatureWrite.Name = "bTemperatureWrite";
            this.bTemperatureWrite.Size = new System.Drawing.Size(60, 23);
            this.bTemperatureWrite.TabIndex = 6;
            this.bTemperatureWrite.Text = "Write";
            this.bTemperatureWrite.UseVisualStyleBackColor = true;
            this.bTemperatureWrite.Click += new System.EventHandler(this._bTemperatureWriteClick);
            // 
            // cbTOAutoCorrect
            // 
            this.cbTOAutoCorrect.AutoSize = true;
            this.cbTOAutoCorrect.Location = new System.Drawing.Point(266, 14);
            this.cbTOAutoCorrect.Name = "cbTOAutoCorrect";
            this.cbTOAutoCorrect.Size = new System.Drawing.Size(85, 16);
            this.cbTOAutoCorrect.TabIndex = 7;
            this.cbTOAutoCorrect.Text = "Auto Correct";
            this.cbTOAutoCorrect.UseVisualStyleBackColor = true;
            this.cbTOAutoCorrect.CheckedChanged += new System.EventHandler(this._cbTOAutoCorrectCheckedChanged);
            // 
            // gbTemperature
            // 
            this.gbTemperature.Controls.Add(this.lTemperatureOffsetDegC);
            this.gbTemperature.Controls.Add(this.tbTemperature);
            this.gbTemperature.Controls.Add(this.lTemperature);
            this.gbTemperature.Controls.Add(this.lTxTemperature);
            this.gbTemperature.Controls.Add(this.tbTxTemperature);
            this.gbTemperature.Controls.Add(this.cbTOAutoCorrect);
            this.gbTemperature.Controls.Add(this.bTemperatureWrite);
            this.gbTemperature.Controls.Add(this.lDegC);
            this.gbTemperature.Controls.Add(this.bTemperatureRead);
            this.gbTemperature.Controls.Add(this.lTemperatureOffset);
            this.gbTemperature.Controls.Add(this.tbTemperatureOffset);
            this.gbTemperature.Location = new System.Drawing.Point(3, 3);
            this.gbTemperature.Name = "gbTemperature";
            this.gbTemperature.Size = new System.Drawing.Size(614, 62);
            this.gbTemperature.TabIndex = 8;
            this.gbTemperature.TabStop = false;
            this.gbTemperature.Text = "Temperature";
            // 
            // lTemperatureOffsetDegC
            // 
            this.lTemperatureOffsetDegC.AutoSize = true;
            this.lTemperatureOffsetDegC.Location = new System.Drawing.Point(332, 36);
            this.lTemperatureOffsetDegC.Name = "lTemperatureOffsetDegC";
            this.lTemperatureOffsetDegC.Size = new System.Drawing.Size(50, 12);
            this.lTemperatureOffsetDegC.TabIndex = 11;
            this.lTemperatureOffsetDegC.Text = "0.1 DegC";
            // 
            // tbTemperature
            // 
            this.tbTemperature.Location = new System.Drawing.Point(66, 33);
            this.tbTemperature.Name = "tbTemperature";
            this.tbTemperature.Size = new System.Drawing.Size(50, 22);
            this.tbTemperature.TabIndex = 10;
            // 
            // lTemperature
            // 
            this.lTemperature.AutoSize = true;
            this.lTemperature.Location = new System.Drawing.Point(59, 18);
            this.lTemperature.Name = "lTemperature";
            this.lTemperature.Size = new System.Drawing.Size(64, 12);
            this.lTemperature.TabIndex = 9;
            this.lTemperature.Text = "Temperature";
            // 
            // lTxTemperature
            // 
            this.lTxTemperature.AutoSize = true;
            this.lTxTemperature.Location = new System.Drawing.Point(26, 18);
            this.lTxTemperature.Name = "lTxTemperature";
            this.lTxTemperature.Size = new System.Drawing.Size(18, 12);
            this.lTxTemperature.TabIndex = 8;
            this.lTxTemperature.Text = "Tx";
            // 
            // gbRxPowerRate
            // 
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRateMin);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRateMax);
            this.gbRxPowerRate.Controls.Add(this.lRxPowerRateMin);
            this.gbRxPowerRate.Controls.Add(this.lRxPowerRateMax);
            this.gbRxPowerRate.Controls.Add(this.lRxPowerRateDefault);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRateDefault);
            this.gbRxPowerRate.Controls.Add(this.lRxPowerRateUnit);
            this.gbRxPowerRate.Controls.Add(this.bRxPowerRateWrite);
            this.gbRxPowerRate.Controls.Add(this.bRxPowerRateRead);
            this.gbRxPowerRate.Controls.Add(this.cbRPRAutoCorrect);
            this.gbRxPowerRate.Controls.Add(this.label1);
            this.gbRxPowerRate.Controls.Add(this.lRssiUA);
            this.gbRxPowerRate.Controls.Add(this.lInputUW);
            this.gbRxPowerRate.Controls.Add(this.tbRxPower4);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRate4);
            this.gbRxPowerRate.Controls.Add(this.tbRssi4);
            this.gbRxPowerRate.Controls.Add(this.tbRxInputPower4);
            this.gbRxPowerRate.Controls.Add(this.lRxCh4);
            this.gbRxPowerRate.Controls.Add(this.tbRxPower3);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRate3);
            this.gbRxPowerRate.Controls.Add(this.tbRssi3);
            this.gbRxPowerRate.Controls.Add(this.tbRxInputPower3);
            this.gbRxPowerRate.Controls.Add(this.lRxCh3);
            this.gbRxPowerRate.Controls.Add(this.tbRxPower2);
            this.gbRxPowerRate.Controls.Add(this.tbRxPower1);
            this.gbRxPowerRate.Controls.Add(this.lRxPower1);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRate2);
            this.gbRxPowerRate.Controls.Add(this.tbRssi2);
            this.gbRxPowerRate.Controls.Add(this.tbRxInputPower2);
            this.gbRxPowerRate.Controls.Add(this.lRxCh2);
            this.gbRxPowerRate.Controls.Add(this.tbRxPowerRate1);
            this.gbRxPowerRate.Controls.Add(this.lRxPowerRate);
            this.gbRxPowerRate.Controls.Add(this.tbRssi1);
            this.gbRxPowerRate.Controls.Add(this.lRxRssi);
            this.gbRxPowerRate.Controls.Add(this.tbRxInputPower1);
            this.gbRxPowerRate.Controls.Add(this.lInputPower);
            this.gbRxPowerRate.Controls.Add(this.lRxCh1);
            this.gbRxPowerRate.Location = new System.Drawing.Point(3, 71);
            this.gbRxPowerRate.Name = "gbRxPowerRate";
            this.gbRxPowerRate.Size = new System.Drawing.Size(614, 147);
            this.gbRxPowerRate.TabIndex = 9;
            this.gbRxPowerRate.TabStop = false;
            this.gbRxPowerRate.Text = "Rx Power Rate";
            // 
            // lRxPowerRateDefault
            // 
            this.lRxPowerRateDefault.AutoSize = true;
            this.lRxPowerRateDefault.Location = new System.Drawing.Point(54, 18);
            this.lRxPowerRateDefault.Name = "lRxPowerRateDefault";
            this.lRxPowerRateDefault.Size = new System.Drawing.Size(39, 12);
            this.lRxPowerRateDefault.TabIndex = 29;
            this.lRxPowerRateDefault.Text = "Default";
            // 
            // tbRxPowerRateDefault
            // 
            this.tbRxPowerRateDefault.Location = new System.Drawing.Point(48, 89);
            this.tbRxPowerRateDefault.Name = "tbRxPowerRateDefault";
            this.tbRxPowerRateDefault.ReadOnly = true;
            this.tbRxPowerRateDefault.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRateDefault.TabIndex = 28;
            // 
            // lRxPowerRateUnit
            // 
            this.lRxPowerRateUnit.AutoSize = true;
            this.lRxPowerRateUnit.Location = new System.Drawing.Point(440, 92);
            this.lRxPowerRateUnit.Name = "lRxPowerRateUnit";
            this.lRxPowerRateUnit.Size = new System.Drawing.Size(26, 12);
            this.lRxPowerRateUnit.TabIndex = 27;
            this.lRxPowerRateUnit.Text = "0.01";
            // 
            // bRxPowerRateWrite
            // 
            this.bRxPowerRateWrite.Location = new System.Drawing.Point(548, 115);
            this.bRxPowerRateWrite.Name = "bRxPowerRateWrite";
            this.bRxPowerRateWrite.Size = new System.Drawing.Size(60, 23);
            this.bRxPowerRateWrite.TabIndex = 8;
            this.bRxPowerRateWrite.Text = "Write";
            this.bRxPowerRateWrite.UseVisualStyleBackColor = true;
            this.bRxPowerRateWrite.Click += new System.EventHandler(this._bRxPowerRateWriteClick);
            // 
            // bRxPowerRateRead
            // 
            this.bRxPowerRateRead.Location = new System.Drawing.Point(482, 115);
            this.bRxPowerRateRead.Name = "bRxPowerRateRead";
            this.bRxPowerRateRead.Size = new System.Drawing.Size(60, 23);
            this.bRxPowerRateRead.TabIndex = 8;
            this.bRxPowerRateRead.Text = "Read";
            this.bRxPowerRateRead.UseVisualStyleBackColor = true;
            this.bRxPowerRateRead.Click += new System.EventHandler(this._bRxPowerRateReadClick);
            // 
            // cbRPRAutoCorrect
            // 
            this.cbRPRAutoCorrect.AutoSize = true;
            this.cbRPRAutoCorrect.Location = new System.Drawing.Point(482, 88);
            this.cbRPRAutoCorrect.Name = "cbRPRAutoCorrect";
            this.cbRPRAutoCorrect.Size = new System.Drawing.Size(85, 16);
            this.cbRPRAutoCorrect.TabIndex = 8;
            this.cbRPRAutoCorrect.Text = "Auto Correct";
            this.cbRPRAutoCorrect.UseVisualStyleBackColor = true;
            this.cbRPRAutoCorrect.CheckedChanged += new System.EventHandler(this._cbRPRAutoCorrectCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "uW";
            // 
            // lRssiUA
            // 
            this.lRssiUA.AutoSize = true;
            this.lRssiUA.Location = new System.Drawing.Point(440, 64);
            this.lRssiUA.Name = "lRssiUA";
            this.lRssiUA.Size = new System.Drawing.Size(19, 12);
            this.lRssiUA.TabIndex = 25;
            this.lRssiUA.Text = "uA";
            // 
            // lInputUW
            // 
            this.lInputUW.AutoSize = true;
            this.lInputUW.Location = new System.Drawing.Point(440, 36);
            this.lInputUW.Name = "lInputUW";
            this.lInputUW.Size = new System.Drawing.Size(22, 12);
            this.lInputUW.TabIndex = 24;
            this.lInputUW.Text = "uW";
            // 
            // tbRxPower4
            // 
            this.tbRxPower4.Location = new System.Drawing.Point(384, 117);
            this.tbRxPower4.Name = "tbRxPower4";
            this.tbRxPower4.ReadOnly = true;
            this.tbRxPower4.Size = new System.Drawing.Size(50, 22);
            this.tbRxPower4.TabIndex = 23;
            // 
            // tbRxPowerRate4
            // 
            this.tbRxPowerRate4.Location = new System.Drawing.Point(384, 89);
            this.tbRxPowerRate4.Name = "tbRxPowerRate4";
            this.tbRxPowerRate4.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRate4.TabIndex = 22;
            // 
            // tbRssi4
            // 
            this.tbRssi4.Location = new System.Drawing.Point(384, 61);
            this.tbRssi4.Name = "tbRssi4";
            this.tbRssi4.ReadOnly = true;
            this.tbRssi4.Size = new System.Drawing.Size(50, 22);
            this.tbRssi4.TabIndex = 21;
            // 
            // tbRxInputPower4
            // 
            this.tbRxInputPower4.Location = new System.Drawing.Point(384, 33);
            this.tbRxInputPower4.Name = "tbRxInputPower4";
            this.tbRxInputPower4.Size = new System.Drawing.Size(50, 22);
            this.tbRxInputPower4.TabIndex = 20;
            this.tbRxInputPower4.Text = "500.0";
            // 
            // lRxCh4
            // 
            this.lRxCh4.AutoSize = true;
            this.lRxCh4.Location = new System.Drawing.Point(397, 18);
            this.lRxCh4.Name = "lRxCh4";
            this.lRxCh4.Size = new System.Drawing.Size(25, 12);
            this.lRxCh4.TabIndex = 19;
            this.lRxCh4.Text = "Rx4";
            // 
            // tbRxPower3
            // 
            this.tbRxPower3.Location = new System.Drawing.Point(328, 117);
            this.tbRxPower3.Name = "tbRxPower3";
            this.tbRxPower3.ReadOnly = true;
            this.tbRxPower3.Size = new System.Drawing.Size(50, 22);
            this.tbRxPower3.TabIndex = 18;
            // 
            // tbRxPowerRate3
            // 
            this.tbRxPowerRate3.Location = new System.Drawing.Point(328, 89);
            this.tbRxPowerRate3.Name = "tbRxPowerRate3";
            this.tbRxPowerRate3.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRate3.TabIndex = 17;
            // 
            // tbRssi3
            // 
            this.tbRssi3.Location = new System.Drawing.Point(328, 61);
            this.tbRssi3.Name = "tbRssi3";
            this.tbRssi3.ReadOnly = true;
            this.tbRssi3.Size = new System.Drawing.Size(50, 22);
            this.tbRssi3.TabIndex = 16;
            // 
            // tbRxInputPower3
            // 
            this.tbRxInputPower3.Location = new System.Drawing.Point(328, 33);
            this.tbRxInputPower3.Name = "tbRxInputPower3";
            this.tbRxInputPower3.Size = new System.Drawing.Size(50, 22);
            this.tbRxInputPower3.TabIndex = 15;
            this.tbRxInputPower3.Text = "500.0";
            // 
            // lRxCh3
            // 
            this.lRxCh3.AutoSize = true;
            this.lRxCh3.Location = new System.Drawing.Point(341, 18);
            this.lRxCh3.Name = "lRxCh3";
            this.lRxCh3.Size = new System.Drawing.Size(25, 12);
            this.lRxCh3.TabIndex = 14;
            this.lRxCh3.Text = "Rx3";
            // 
            // tbRxPower2
            // 
            this.tbRxPower2.Location = new System.Drawing.Point(272, 117);
            this.tbRxPower2.Name = "tbRxPower2";
            this.tbRxPower2.ReadOnly = true;
            this.tbRxPower2.Size = new System.Drawing.Size(50, 22);
            this.tbRxPower2.TabIndex = 13;
            // 
            // tbRxPower1
            // 
            this.tbRxPower1.Location = new System.Drawing.Point(216, 117);
            this.tbRxPower1.Name = "tbRxPower1";
            this.tbRxPower1.ReadOnly = true;
            this.tbRxPower1.Size = new System.Drawing.Size(50, 22);
            this.tbRxPower1.TabIndex = 12;
            // 
            // lRxPower1
            // 
            this.lRxPower1.AutoSize = true;
            this.lRxPower1.Location = new System.Drawing.Point(8, 120);
            this.lRxPower1.Name = "lRxPower1";
            this.lRxPower1.Size = new System.Drawing.Size(40, 12);
            this.lRxPower1.TabIndex = 11;
            this.lRxPower1.Text = "Power :";
            // 
            // tbRxPowerRate2
            // 
            this.tbRxPowerRate2.Location = new System.Drawing.Point(272, 89);
            this.tbRxPowerRate2.Name = "tbRxPowerRate2";
            this.tbRxPowerRate2.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRate2.TabIndex = 10;
            // 
            // tbRssi2
            // 
            this.tbRssi2.Location = new System.Drawing.Point(272, 61);
            this.tbRssi2.Name = "tbRssi2";
            this.tbRssi2.ReadOnly = true;
            this.tbRssi2.Size = new System.Drawing.Size(50, 22);
            this.tbRssi2.TabIndex = 9;
            // 
            // tbRxInputPower2
            // 
            this.tbRxInputPower2.Location = new System.Drawing.Point(272, 33);
            this.tbRxInputPower2.Name = "tbRxInputPower2";
            this.tbRxInputPower2.Size = new System.Drawing.Size(50, 22);
            this.tbRxInputPower2.TabIndex = 8;
            this.tbRxInputPower2.Text = "500.0";
            // 
            // lRxCh2
            // 
            this.lRxCh2.AutoSize = true;
            this.lRxCh2.Location = new System.Drawing.Point(285, 18);
            this.lRxCh2.Name = "lRxCh2";
            this.lRxCh2.Size = new System.Drawing.Size(25, 12);
            this.lRxCh2.TabIndex = 7;
            this.lRxCh2.Text = "Rx2";
            // 
            // tbRxPowerRate1
            // 
            this.tbRxPowerRate1.Location = new System.Drawing.Point(216, 89);
            this.tbRxPowerRate1.Name = "tbRxPowerRate1";
            this.tbRxPowerRate1.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRate1.TabIndex = 6;
            // 
            // lRxPowerRate
            // 
            this.lRxPowerRate.AutoSize = true;
            this.lRxPowerRate.Location = new System.Drawing.Point(10, 92);
            this.lRxPowerRate.Name = "lRxPowerRate";
            this.lRxPowerRate.Size = new System.Drawing.Size(32, 12);
            this.lRxPowerRate.TabIndex = 5;
            this.lRxPowerRate.Text = "Rate :";
            // 
            // tbRssi1
            // 
            this.tbRssi1.Location = new System.Drawing.Point(216, 61);
            this.tbRssi1.Name = "tbRssi1";
            this.tbRssi1.ReadOnly = true;
            this.tbRssi1.Size = new System.Drawing.Size(50, 22);
            this.tbRssi1.TabIndex = 4;
            // 
            // lRxRssi
            // 
            this.lRxRssi.AutoSize = true;
            this.lRxRssi.Location = new System.Drawing.Point(10, 64);
            this.lRxRssi.Name = "lRxRssi";
            this.lRxRssi.Size = new System.Drawing.Size(35, 12);
            this.lRxRssi.TabIndex = 3;
            this.lRxRssi.Text = "RSSI :";
            // 
            // tbRxInputPower1
            // 
            this.tbRxInputPower1.Location = new System.Drawing.Point(216, 33);
            this.tbRxInputPower1.Name = "tbRxInputPower1";
            this.tbRxInputPower1.Size = new System.Drawing.Size(50, 22);
            this.tbRxInputPower1.TabIndex = 2;
            this.tbRxInputPower1.Text = "500.0";
            // 
            // lInputPower
            // 
            this.lInputPower.AutoSize = true;
            this.lInputPower.Location = new System.Drawing.Point(10, 36);
            this.lInputPower.Name = "lInputPower";
            this.lInputPower.Size = new System.Drawing.Size(36, 12);
            this.lInputPower.TabIndex = 1;
            this.lInputPower.Text = "Input :";
            // 
            // lRxCh1
            // 
            this.lRxCh1.AutoSize = true;
            this.lRxCh1.Location = new System.Drawing.Point(229, 18);
            this.lRxCh1.Name = "lRxCh1";
            this.lRxCh1.Size = new System.Drawing.Size(25, 12);
            this.lRxCh1.TabIndex = 0;
            this.lRxCh1.Text = "Rx1";
            // 
            // lRxPowerRateMax
            // 
            this.lRxPowerRateMax.AutoSize = true;
            this.lRxPowerRateMax.Location = new System.Drawing.Point(116, 18);
            this.lRxPowerRateMax.Name = "lRxPowerRateMax";
            this.lRxPowerRateMax.Size = new System.Drawing.Size(26, 12);
            this.lRxPowerRateMax.TabIndex = 30;
            this.lRxPowerRateMax.Text = "Max";
            // 
            // lRxPowerRateMin
            // 
            this.lRxPowerRateMin.AutoSize = true;
            this.lRxPowerRateMin.Location = new System.Drawing.Point(173, 18);
            this.lRxPowerRateMin.Name = "lRxPowerRateMin";
            this.lRxPowerRateMin.Size = new System.Drawing.Size(24, 12);
            this.lRxPowerRateMin.TabIndex = 31;
            this.lRxPowerRateMin.Text = "Min";
            // 
            // tbRxPowerRateMax
            // 
            this.tbRxPowerRateMax.Location = new System.Drawing.Point(104, 89);
            this.tbRxPowerRateMax.Name = "tbRxPowerRateMax";
            this.tbRxPowerRateMax.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRateMax.TabIndex = 32;
            // 
            // tbRxPowerRateMin
            // 
            this.tbRxPowerRateMin.Location = new System.Drawing.Point(160, 89);
            this.tbRxPowerRateMin.Name = "tbRxPowerRateMin";
            this.tbRxPowerRateMin.Size = new System.Drawing.Size(50, 22);
            this.tbRxPowerRateMin.TabIndex = 33;
            // 
            // UcQsfpCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbRxPowerRate);
            this.Controls.Add(this.gbTemperature);
            this.Name = "UcQsfpCorrector";
            this.Size = new System.Drawing.Size(620, 440);
            this.gbTemperature.ResumeLayout(false);
            this.gbTemperature.PerformLayout();
            this.gbRxPowerRate.ResumeLayout(false);
            this.gbRxPowerRate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTxTemperature;
        private System.Windows.Forms.Label lTemperatureOffset;
        private System.Windows.Forms.Label lDegC;
        private System.Windows.Forms.TextBox tbTemperatureOffset;
        private System.Windows.Forms.Button bTemperatureRead;
        private System.Windows.Forms.Button bTemperatureWrite;
        private System.Windows.Forms.CheckBox cbTOAutoCorrect;
        private System.Windows.Forms.GroupBox gbTemperature;
        private System.Windows.Forms.GroupBox gbRxPowerRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lRssiUA;
        private System.Windows.Forms.Label lInputUW;
        private System.Windows.Forms.TextBox tbRxPower4;
        private System.Windows.Forms.TextBox tbRxPowerRate4;
        private System.Windows.Forms.TextBox tbRssi4;
        private System.Windows.Forms.TextBox tbRxInputPower4;
        private System.Windows.Forms.Label lRxCh4;
        private System.Windows.Forms.TextBox tbRxPower3;
        private System.Windows.Forms.TextBox tbRxPowerRate3;
        private System.Windows.Forms.TextBox tbRssi3;
        private System.Windows.Forms.TextBox tbRxInputPower3;
        private System.Windows.Forms.Label lRxCh3;
        private System.Windows.Forms.TextBox tbRxPower2;
        private System.Windows.Forms.TextBox tbRxPower1;
        private System.Windows.Forms.Label lRxPower1;
        private System.Windows.Forms.TextBox tbRxPowerRate2;
        private System.Windows.Forms.TextBox tbRssi2;
        private System.Windows.Forms.TextBox tbRxInputPower2;
        private System.Windows.Forms.Label lRxCh2;
        private System.Windows.Forms.TextBox tbRxPowerRate1;
        private System.Windows.Forms.Label lRxPowerRate;
        private System.Windows.Forms.TextBox tbRssi1;
        private System.Windows.Forms.Label lRxRssi;
        private System.Windows.Forms.TextBox tbRxInputPower1;
        private System.Windows.Forms.Label lInputPower;
        private System.Windows.Forms.Label lRxCh1;
        private System.Windows.Forms.Button bRxPowerRateWrite;
        private System.Windows.Forms.Button bRxPowerRateRead;
        private System.Windows.Forms.CheckBox cbRPRAutoCorrect;
        private System.Windows.Forms.TextBox tbTemperature;
        private System.Windows.Forms.Label lTemperature;
        private System.Windows.Forms.Label lTxTemperature;
        private System.Windows.Forms.Label lTemperatureOffsetDegC;
        private System.Windows.Forms.Label lRxPowerRateUnit;
        private System.Windows.Forms.TextBox tbRxPowerRateDefault;
        private System.Windows.Forms.Label lRxPowerRateDefault;
        private System.Windows.Forms.TextBox tbRxPowerRateMin;
        private System.Windows.Forms.TextBox tbRxPowerRateMax;
        private System.Windows.Forms.Label lRxPowerRateMin;
        private System.Windows.Forms.Label lRxPowerRateMax;
    }
}
