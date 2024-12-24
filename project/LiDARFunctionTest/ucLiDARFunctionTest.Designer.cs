namespace LiDARFunctionTest
{
    partial class UcLiDARFunctionTest
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
            this.lPid1Proportional = new System.Windows.Forms.Label();
            this.gbTec1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ccTec1PwmDuty = new LiveCharts.WinForms.CartesianChart();
            this.tbPid1TargetTemperature = new System.Windows.Forms.TextBox();
            this.lPid1TargetTemperature = new System.Windows.Forms.Label();
            this.tbPid1Derivative = new System.Windows.Forms.TextBox();
            this.lPid1Derivative = new System.Windows.Forms.Label();
            this.tbPid1Integral = new System.Windows.Forms.TextBox();
            this.lPid1Integral = new System.Windows.Forms.Label();
            this.tbPid1Proportional = new System.Windows.Forms.TextBox();
            this.ccNtc1Temperature = new LiveCharts.WinForms.CartesianChart();
            this.bRead = new System.Windows.Forms.Button();
            this.bWrite = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.gbNtc1 = new System.Windows.Forms.GroupBox();
            this.tbNtc1TemperatureOffset = new System.Windows.Forms.TextBox();
            this.lNtc1TemperatureOffset = new System.Windows.Forms.Label();
            this.tbNtc1Temperature = new System.Windows.Forms.TextBox();
            this.lNtc1Temperature = new System.Windows.Forms.Label();
            this.tbNtc1Beta = new System.Windows.Forms.TextBox();
            this.lNtc1Beta = new System.Windows.Forms.Label();
            this.tbNtc1Resistance = new System.Windows.Forms.TextBox();
            this.lNtc1Resistance = new System.Windows.Forms.Label();
            this.gbNtc2 = new System.Windows.Forms.GroupBox();
            this.ccNtc2Temperature = new LiveCharts.WinForms.CartesianChart();
            this.tbNtc2TemperatureOffset = new System.Windows.Forms.TextBox();
            this.lNtc2TemperatureOffset = new System.Windows.Forms.Label();
            this.tbNtc2Temperature = new System.Windows.Forms.TextBox();
            this.lNtc2Temperature = new System.Windows.Forms.Label();
            this.tbNtc2Beta = new System.Windows.Forms.TextBox();
            this.lNtc2Beta = new System.Windows.Forms.Label();
            this.tbNtc2Resistance = new System.Windows.Forms.TextBox();
            this.lNtc2Resistance = new System.Windows.Forms.Label();
            this.gbTec2 = new System.Windows.Forms.GroupBox();
            this.lTec2PwmDuty = new System.Windows.Forms.Label();
            this.ccTec2PwmDuty = new LiveCharts.WinForms.CartesianChart();
            this.tbPid2TargetTemperature = new System.Windows.Forms.TextBox();
            this.lPid2TargetTemperature = new System.Windows.Forms.Label();
            this.tbPid2Derivative = new System.Windows.Forms.TextBox();
            this.lPid2Derivative = new System.Windows.Forms.Label();
            this.tbPid2Integral = new System.Windows.Forms.TextBox();
            this.lPid2Integral = new System.Windows.Forms.Label();
            this.tbPid2Proportional = new System.Windows.Forms.TextBox();
            this.lPid2Proportional = new System.Windows.Forms.Label();
            this.cbMoniter = new System.Windows.Forms.CheckBox();
            this.gbSignalGenerator = new System.Windows.Forms.GroupBox();
            this.bSignalGeneratorSet = new System.Windows.Forms.Button();
            this.cbSingleGeneratorPhaseShift = new System.Windows.Forms.ComboBox();
            this.lSignalGeneratorPhaseShift = new System.Windows.Forms.Label();
            this.tbSignalGeneratorFrequency = new System.Windows.Forms.TextBox();
            this.lSignalGeneratorFrequence = new System.Windows.Forms.Label();
            this.cbSignalGeneratorMode = new System.Windows.Forms.ComboBox();
            this.lSignalGeneratorMode = new System.Windows.Forms.Label();
            this.cbSignalGeneratorId = new System.Windows.Forms.ComboBox();
            this.lSignalGeneratorId = new System.Windows.Forms.Label();
            this.gbMpdRSSI = new System.Windows.Forms.GroupBox();
            this.tbMpd2Rssi = new System.Windows.Forms.TextBox();
            this.lMpd2Rssi = new System.Windows.Forms.Label();
            this.tbMpd1Rssi = new System.Windows.Forms.TextBox();
            this.lMpd1Rssi = new System.Windows.Forms.Label();
            this.gbFrequenceCounter = new System.Windows.Forms.GroupBox();
            this.tbFrequenceCounter = new System.Windows.Forms.TextBox();
            this.lFrequenceCounter = new System.Windows.Forms.Label();
            this.lAction = new System.Windows.Forms.Label();
            this.gbTec1.SuspendLayout();
            this.gbNtc1.SuspendLayout();
            this.gbNtc2.SuspendLayout();
            this.gbTec2.SuspendLayout();
            this.gbSignalGenerator.SuspendLayout();
            this.gbMpdRSSI.SuspendLayout();
            this.gbFrequenceCounter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lPid1Proportional
            // 
            this.lPid1Proportional.AutoSize = true;
            this.lPid1Proportional.Location = new System.Drawing.Point(6, 24);
            this.lPid1Proportional.Name = "lPid1Proportional";
            this.lPid1Proportional.Size = new System.Drawing.Size(66, 12);
            this.lPid1Proportional.TabIndex = 0;
            this.lPid1Proportional.Text = "Proportional:";
            // 
            // gbTec1
            // 
            this.gbTec1.Controls.Add(this.label1);
            this.gbTec1.Controls.Add(this.ccTec1PwmDuty);
            this.gbTec1.Controls.Add(this.tbPid1TargetTemperature);
            this.gbTec1.Controls.Add(this.lPid1TargetTemperature);
            this.gbTec1.Controls.Add(this.tbPid1Derivative);
            this.gbTec1.Controls.Add(this.lPid1Derivative);
            this.gbTec1.Controls.Add(this.tbPid1Integral);
            this.gbTec1.Controls.Add(this.lPid1Integral);
            this.gbTec1.Controls.Add(this.tbPid1Proportional);
            this.gbTec1.Controls.Add(this.lPid1Proportional);
            this.gbTec1.Location = new System.Drawing.Point(3, 146);
            this.gbTec1.Name = "gbTec1";
            this.gbTec1.Size = new System.Drawing.Size(290, 137);
            this.gbTec1.TabIndex = 1;
            this.gbTec1.TabStop = false;
            this.gbTec1.Text = "TEC1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "PWM Duty";
            // 
            // ccTec1PwmDuty
            // 
            this.ccTec1PwmDuty.Location = new System.Drawing.Point(124, 21);
            this.ccTec1PwmDuty.Name = "ccTec1PwmDuty";
            this.ccTec1PwmDuty.Size = new System.Drawing.Size(162, 110);
            this.ccTec1PwmDuty.TabIndex = 8;
            this.ccTec1PwmDuty.Text = "cartesianChart1";
            // 
            // tbPid1TargetTemperature
            // 
            this.tbPid1TargetTemperature.Location = new System.Drawing.Point(78, 105);
            this.tbPid1TargetTemperature.Name = "tbPid1TargetTemperature";
            this.tbPid1TargetTemperature.Size = new System.Drawing.Size(40, 22);
            this.tbPid1TargetTemperature.TabIndex = 7;
            // 
            // lPid1TargetTemperature
            // 
            this.lPid1TargetTemperature.AutoSize = true;
            this.lPid1TargetTemperature.Location = new System.Drawing.Point(6, 108);
            this.lPid1TargetTemperature.Name = "lPid1TargetTemperature";
            this.lPid1TargetTemperature.Size = new System.Drawing.Size(68, 12);
            this.lPid1TargetTemperature.TabIndex = 6;
            this.lPid1TargetTemperature.Text = "Target Temp:";
            // 
            // tbPid1Derivative
            // 
            this.tbPid1Derivative.Location = new System.Drawing.Point(78, 77);
            this.tbPid1Derivative.Name = "tbPid1Derivative";
            this.tbPid1Derivative.Size = new System.Drawing.Size(40, 22);
            this.tbPid1Derivative.TabIndex = 5;
            // 
            // lPid1Derivative
            // 
            this.lPid1Derivative.AutoSize = true;
            this.lPid1Derivative.Location = new System.Drawing.Point(6, 80);
            this.lPid1Derivative.Name = "lPid1Derivative";
            this.lPid1Derivative.Size = new System.Drawing.Size(56, 12);
            this.lPid1Derivative.TabIndex = 4;
            this.lPid1Derivative.Text = "Derivative:";
            // 
            // tbPid1Integral
            // 
            this.tbPid1Integral.Location = new System.Drawing.Point(78, 49);
            this.tbPid1Integral.Name = "tbPid1Integral";
            this.tbPid1Integral.Size = new System.Drawing.Size(40, 22);
            this.tbPid1Integral.TabIndex = 3;
            // 
            // lPid1Integral
            // 
            this.lPid1Integral.AutoSize = true;
            this.lPid1Integral.Location = new System.Drawing.Point(6, 52);
            this.lPid1Integral.Name = "lPid1Integral";
            this.lPid1Integral.Size = new System.Drawing.Size(44, 12);
            this.lPid1Integral.TabIndex = 2;
            this.lPid1Integral.Text = "Integral:";
            // 
            // tbPid1Proportional
            // 
            this.tbPid1Proportional.Location = new System.Drawing.Point(78, 21);
            this.tbPid1Proportional.Name = "tbPid1Proportional";
            this.tbPid1Proportional.Size = new System.Drawing.Size(40, 22);
            this.tbPid1Proportional.TabIndex = 1;
            // 
            // ccNtc1Temperature
            // 
            this.ccNtc1Temperature.Location = new System.Drawing.Point(122, 11);
            this.ccNtc1Temperature.Name = "ccNtc1Temperature";
            this.ccNtc1Temperature.Size = new System.Drawing.Size(162, 120);
            this.ccNtc1Temperature.TabIndex = 5;
            this.ccNtc1Temperature.Text = "cartesianChart1";
            // 
            // bRead
            // 
            this.bRead.Enabled = false;
            this.bRead.Location = new System.Drawing.Point(613, 202);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(75, 23);
            this.bRead.TabIndex = 2;
            this.bRead.Text = "Read";
            this.bRead.UseVisualStyleBackColor = true;
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // bWrite
            // 
            this.bWrite.Enabled = false;
            this.bWrite.Location = new System.Drawing.Point(613, 231);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(75, 23);
            this.bWrite.TabIndex = 3;
            this.bWrite.Text = "Write";
            this.bWrite.UseVisualStyleBackColor = true;
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // bSave
            // 
            this.bSave.Enabled = false;
            this.bSave.Location = new System.Drawing.Point(613, 260);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 4;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // gbNtc1
            // 
            this.gbNtc1.Controls.Add(this.ccNtc1Temperature);
            this.gbNtc1.Controls.Add(this.tbNtc1TemperatureOffset);
            this.gbNtc1.Controls.Add(this.lNtc1TemperatureOffset);
            this.gbNtc1.Controls.Add(this.tbNtc1Temperature);
            this.gbNtc1.Controls.Add(this.lNtc1Temperature);
            this.gbNtc1.Controls.Add(this.tbNtc1Beta);
            this.gbNtc1.Controls.Add(this.lNtc1Beta);
            this.gbNtc1.Controls.Add(this.tbNtc1Resistance);
            this.gbNtc1.Controls.Add(this.lNtc1Resistance);
            this.gbNtc1.Location = new System.Drawing.Point(299, 146);
            this.gbNtc1.Name = "gbNtc1";
            this.gbNtc1.Size = new System.Drawing.Size(290, 137);
            this.gbNtc1.TabIndex = 5;
            this.gbNtc1.TabStop = false;
            this.gbNtc1.Text = "NTC1";
            // 
            // tbNtc1TemperatureOffset
            // 
            this.tbNtc1TemperatureOffset.Location = new System.Drawing.Point(76, 77);
            this.tbNtc1TemperatureOffset.Name = "tbNtc1TemperatureOffset";
            this.tbNtc1TemperatureOffset.Size = new System.Drawing.Size(40, 22);
            this.tbNtc1TemperatureOffset.TabIndex = 13;
            // 
            // lNtc1TemperatureOffset
            // 
            this.lNtc1TemperatureOffset.AutoSize = true;
            this.lNtc1TemperatureOffset.Location = new System.Drawing.Point(6, 80);
            this.lNtc1TemperatureOffset.Name = "lNtc1TemperatureOffset";
            this.lNtc1TemperatureOffset.Size = new System.Drawing.Size(64, 12);
            this.lNtc1TemperatureOffset.TabIndex = 12;
            this.lNtc1TemperatureOffset.Text = "Temp offset:";
            // 
            // tbNtc1Temperature
            // 
            this.tbNtc1Temperature.Location = new System.Drawing.Point(76, 105);
            this.tbNtc1Temperature.Name = "tbNtc1Temperature";
            this.tbNtc1Temperature.Size = new System.Drawing.Size(40, 22);
            this.tbNtc1Temperature.TabIndex = 11;
            // 
            // lNtc1Temperature
            // 
            this.lNtc1Temperature.AutoSize = true;
            this.lNtc1Temperature.Location = new System.Drawing.Point(6, 108);
            this.lNtc1Temperature.Name = "lNtc1Temperature";
            this.lNtc1Temperature.Size = new System.Drawing.Size(67, 12);
            this.lNtc1Temperature.TabIndex = 10;
            this.lNtc1Temperature.Text = "Temperature:";
            // 
            // tbNtc1Beta
            // 
            this.tbNtc1Beta.Location = new System.Drawing.Point(76, 21);
            this.tbNtc1Beta.Name = "tbNtc1Beta";
            this.tbNtc1Beta.Size = new System.Drawing.Size(40, 22);
            this.tbNtc1Beta.TabIndex = 9;
            // 
            // lNtc1Beta
            // 
            this.lNtc1Beta.AutoSize = true;
            this.lNtc1Beta.Location = new System.Drawing.Point(6, 24);
            this.lNtc1Beta.Name = "lNtc1Beta";
            this.lNtc1Beta.Size = new System.Drawing.Size(29, 12);
            this.lNtc1Beta.TabIndex = 8;
            this.lNtc1Beta.Text = "Beta:";
            // 
            // tbNtc1Resistance
            // 
            this.tbNtc1Resistance.Location = new System.Drawing.Point(76, 49);
            this.tbNtc1Resistance.Name = "tbNtc1Resistance";
            this.tbNtc1Resistance.Size = new System.Drawing.Size(40, 22);
            this.tbNtc1Resistance.TabIndex = 7;
            // 
            // lNtc1Resistance
            // 
            this.lNtc1Resistance.AutoSize = true;
            this.lNtc1Resistance.Location = new System.Drawing.Point(6, 52);
            this.lNtc1Resistance.Name = "lNtc1Resistance";
            this.lNtc1Resistance.Size = new System.Drawing.Size(56, 12);
            this.lNtc1Resistance.TabIndex = 6;
            this.lNtc1Resistance.Text = "Resistance:";
            // 
            // gbNtc2
            // 
            this.gbNtc2.Controls.Add(this.ccNtc2Temperature);
            this.gbNtc2.Controls.Add(this.tbNtc2TemperatureOffset);
            this.gbNtc2.Controls.Add(this.lNtc2TemperatureOffset);
            this.gbNtc2.Controls.Add(this.tbNtc2Temperature);
            this.gbNtc2.Controls.Add(this.lNtc2Temperature);
            this.gbNtc2.Controls.Add(this.tbNtc2Beta);
            this.gbNtc2.Controls.Add(this.lNtc2Beta);
            this.gbNtc2.Controls.Add(this.tbNtc2Resistance);
            this.gbNtc2.Controls.Add(this.lNtc2Resistance);
            this.gbNtc2.Location = new System.Drawing.Point(299, 3);
            this.gbNtc2.Name = "gbNtc2";
            this.gbNtc2.Size = new System.Drawing.Size(290, 137);
            this.gbNtc2.TabIndex = 7;
            this.gbNtc2.TabStop = false;
            this.gbNtc2.Text = "NTC2";
            // 
            // ccNtc2Temperature
            // 
            this.ccNtc2Temperature.Location = new System.Drawing.Point(122, 11);
            this.ccNtc2Temperature.Name = "ccNtc2Temperature";
            this.ccNtc2Temperature.Size = new System.Drawing.Size(162, 120);
            this.ccNtc2Temperature.TabIndex = 5;
            this.ccNtc2Temperature.Text = "cartesianChart1";
            // 
            // tbNtc2TemperatureOffset
            // 
            this.tbNtc2TemperatureOffset.Location = new System.Drawing.Point(76, 77);
            this.tbNtc2TemperatureOffset.Name = "tbNtc2TemperatureOffset";
            this.tbNtc2TemperatureOffset.Size = new System.Drawing.Size(40, 22);
            this.tbNtc2TemperatureOffset.TabIndex = 13;
            // 
            // lNtc2TemperatureOffset
            // 
            this.lNtc2TemperatureOffset.AutoSize = true;
            this.lNtc2TemperatureOffset.Location = new System.Drawing.Point(6, 80);
            this.lNtc2TemperatureOffset.Name = "lNtc2TemperatureOffset";
            this.lNtc2TemperatureOffset.Size = new System.Drawing.Size(64, 12);
            this.lNtc2TemperatureOffset.TabIndex = 12;
            this.lNtc2TemperatureOffset.Text = "Temp offset:";
            // 
            // tbNtc2Temperature
            // 
            this.tbNtc2Temperature.Location = new System.Drawing.Point(76, 105);
            this.tbNtc2Temperature.Name = "tbNtc2Temperature";
            this.tbNtc2Temperature.Size = new System.Drawing.Size(40, 22);
            this.tbNtc2Temperature.TabIndex = 11;
            // 
            // lNtc2Temperature
            // 
            this.lNtc2Temperature.AutoSize = true;
            this.lNtc2Temperature.Location = new System.Drawing.Point(6, 108);
            this.lNtc2Temperature.Name = "lNtc2Temperature";
            this.lNtc2Temperature.Size = new System.Drawing.Size(67, 12);
            this.lNtc2Temperature.TabIndex = 10;
            this.lNtc2Temperature.Text = "Temperature:";
            // 
            // tbNtc2Beta
            // 
            this.tbNtc2Beta.Location = new System.Drawing.Point(76, 21);
            this.tbNtc2Beta.Name = "tbNtc2Beta";
            this.tbNtc2Beta.Size = new System.Drawing.Size(40, 22);
            this.tbNtc2Beta.TabIndex = 9;
            // 
            // lNtc2Beta
            // 
            this.lNtc2Beta.AutoSize = true;
            this.lNtc2Beta.Location = new System.Drawing.Point(6, 24);
            this.lNtc2Beta.Name = "lNtc2Beta";
            this.lNtc2Beta.Size = new System.Drawing.Size(29, 12);
            this.lNtc2Beta.TabIndex = 8;
            this.lNtc2Beta.Text = "Beta:";
            // 
            // tbNtc2Resistance
            // 
            this.tbNtc2Resistance.Location = new System.Drawing.Point(76, 49);
            this.tbNtc2Resistance.Name = "tbNtc2Resistance";
            this.tbNtc2Resistance.Size = new System.Drawing.Size(40, 22);
            this.tbNtc2Resistance.TabIndex = 7;
            // 
            // lNtc2Resistance
            // 
            this.lNtc2Resistance.AutoSize = true;
            this.lNtc2Resistance.Location = new System.Drawing.Point(6, 52);
            this.lNtc2Resistance.Name = "lNtc2Resistance";
            this.lNtc2Resistance.Size = new System.Drawing.Size(56, 12);
            this.lNtc2Resistance.TabIndex = 6;
            this.lNtc2Resistance.Text = "Resistance:";
            // 
            // gbTec2
            // 
            this.gbTec2.Controls.Add(this.lTec2PwmDuty);
            this.gbTec2.Controls.Add(this.ccTec2PwmDuty);
            this.gbTec2.Controls.Add(this.tbPid2TargetTemperature);
            this.gbTec2.Controls.Add(this.lPid2TargetTemperature);
            this.gbTec2.Controls.Add(this.tbPid2Derivative);
            this.gbTec2.Controls.Add(this.lPid2Derivative);
            this.gbTec2.Controls.Add(this.tbPid2Integral);
            this.gbTec2.Controls.Add(this.lPid2Integral);
            this.gbTec2.Controls.Add(this.tbPid2Proportional);
            this.gbTec2.Controls.Add(this.lPid2Proportional);
            this.gbTec2.Location = new System.Drawing.Point(3, 3);
            this.gbTec2.Name = "gbTec2";
            this.gbTec2.Size = new System.Drawing.Size(290, 137);
            this.gbTec2.TabIndex = 6;
            this.gbTec2.TabStop = false;
            this.gbTec2.Text = "TEC2";
            // 
            // lTec2PwmDuty
            // 
            this.lTec2PwmDuty.AutoSize = true;
            this.lTec2PwmDuty.Location = new System.Drawing.Point(176, 11);
            this.lTec2PwmDuty.Name = "lTec2PwmDuty";
            this.lTec2PwmDuty.Size = new System.Drawing.Size(58, 12);
            this.lTec2PwmDuty.TabIndex = 9;
            this.lTec2PwmDuty.Text = "PWM Duty";
            // 
            // ccTec2PwmDuty
            // 
            this.ccTec2PwmDuty.Location = new System.Drawing.Point(124, 21);
            this.ccTec2PwmDuty.Name = "ccTec2PwmDuty";
            this.ccTec2PwmDuty.Size = new System.Drawing.Size(162, 110);
            this.ccTec2PwmDuty.TabIndex = 8;
            this.ccTec2PwmDuty.Text = "cartesianChart1";
            // 
            // tbPid2TargetTemperature
            // 
            this.tbPid2TargetTemperature.Location = new System.Drawing.Point(78, 105);
            this.tbPid2TargetTemperature.Name = "tbPid2TargetTemperature";
            this.tbPid2TargetTemperature.Size = new System.Drawing.Size(40, 22);
            this.tbPid2TargetTemperature.TabIndex = 7;
            // 
            // lPid2TargetTemperature
            // 
            this.lPid2TargetTemperature.AutoSize = true;
            this.lPid2TargetTemperature.Location = new System.Drawing.Point(6, 108);
            this.lPid2TargetTemperature.Name = "lPid2TargetTemperature";
            this.lPid2TargetTemperature.Size = new System.Drawing.Size(68, 12);
            this.lPid2TargetTemperature.TabIndex = 6;
            this.lPid2TargetTemperature.Text = "Target Temp:";
            // 
            // tbPid2Derivative
            // 
            this.tbPid2Derivative.Location = new System.Drawing.Point(78, 77);
            this.tbPid2Derivative.Name = "tbPid2Derivative";
            this.tbPid2Derivative.Size = new System.Drawing.Size(40, 22);
            this.tbPid2Derivative.TabIndex = 5;
            // 
            // lPid2Derivative
            // 
            this.lPid2Derivative.AutoSize = true;
            this.lPid2Derivative.Location = new System.Drawing.Point(6, 80);
            this.lPid2Derivative.Name = "lPid2Derivative";
            this.lPid2Derivative.Size = new System.Drawing.Size(56, 12);
            this.lPid2Derivative.TabIndex = 4;
            this.lPid2Derivative.Text = "Derivative:";
            // 
            // tbPid2Integral
            // 
            this.tbPid2Integral.Location = new System.Drawing.Point(78, 49);
            this.tbPid2Integral.Name = "tbPid2Integral";
            this.tbPid2Integral.Size = new System.Drawing.Size(40, 22);
            this.tbPid2Integral.TabIndex = 3;
            // 
            // lPid2Integral
            // 
            this.lPid2Integral.AutoSize = true;
            this.lPid2Integral.Location = new System.Drawing.Point(6, 52);
            this.lPid2Integral.Name = "lPid2Integral";
            this.lPid2Integral.Size = new System.Drawing.Size(44, 12);
            this.lPid2Integral.TabIndex = 2;
            this.lPid2Integral.Text = "Integral:";
            // 
            // tbPid2Proportional
            // 
            this.tbPid2Proportional.Location = new System.Drawing.Point(78, 21);
            this.tbPid2Proportional.Name = "tbPid2Proportional";
            this.tbPid2Proportional.Size = new System.Drawing.Size(40, 22);
            this.tbPid2Proportional.TabIndex = 1;
            // 
            // lPid2Proportional
            // 
            this.lPid2Proportional.AutoSize = true;
            this.lPid2Proportional.Location = new System.Drawing.Point(6, 24);
            this.lPid2Proportional.Name = "lPid2Proportional";
            this.lPid2Proportional.Size = new System.Drawing.Size(66, 12);
            this.lPid2Proportional.TabIndex = 0;
            this.lPid2Proportional.Text = "Proportional:";
            // 
            // cbMoniter
            // 
            this.cbMoniter.AutoSize = true;
            this.cbMoniter.Enabled = false;
            this.cbMoniter.Location = new System.Drawing.Point(619, 180);
            this.cbMoniter.Name = "cbMoniter";
            this.cbMoniter.Size = new System.Drawing.Size(62, 16);
            this.cbMoniter.TabIndex = 8;
            this.cbMoniter.Text = "Monitor";
            this.cbMoniter.UseVisualStyleBackColor = true;
            this.cbMoniter.CheckedChanged += new System.EventHandler(this.cbMoniter_CheckedChanged);
            // 
            // gbSignalGenerator
            // 
            this.gbSignalGenerator.Controls.Add(this.bSignalGeneratorSet);
            this.gbSignalGenerator.Controls.Add(this.cbSingleGeneratorPhaseShift);
            this.gbSignalGenerator.Controls.Add(this.lSignalGeneratorPhaseShift);
            this.gbSignalGenerator.Controls.Add(this.tbSignalGeneratorFrequency);
            this.gbSignalGenerator.Controls.Add(this.lSignalGeneratorFrequence);
            this.gbSignalGenerator.Controls.Add(this.cbSignalGeneratorMode);
            this.gbSignalGenerator.Controls.Add(this.lSignalGeneratorMode);
            this.gbSignalGenerator.Controls.Add(this.cbSignalGeneratorId);
            this.gbSignalGenerator.Controls.Add(this.lSignalGeneratorId);
            this.gbSignalGenerator.Location = new System.Drawing.Point(3, 289);
            this.gbSignalGenerator.Name = "gbSignalGenerator";
            this.gbSignalGenerator.Size = new System.Drawing.Size(586, 49);
            this.gbSignalGenerator.TabIndex = 9;
            this.gbSignalGenerator.TabStop = false;
            this.gbSignalGenerator.Text = "Signal Generator";
            // 
            // bSignalGeneratorSet
            // 
            this.bSignalGeneratorSet.Enabled = false;
            this.bSignalGeneratorSet.Location = new System.Drawing.Point(505, 19);
            this.bSignalGeneratorSet.Name = "bSignalGeneratorSet";
            this.bSignalGeneratorSet.Size = new System.Drawing.Size(75, 23);
            this.bSignalGeneratorSet.TabIndex = 10;
            this.bSignalGeneratorSet.Text = "Set";
            this.bSignalGeneratorSet.UseVisualStyleBackColor = true;
            this.bSignalGeneratorSet.Click += new System.EventHandler(this.bSignalGeneratorSet_Click);
            // 
            // cbSingleGeneratorPhaseShift
            // 
            this.cbSingleGeneratorPhaseShift.FormattingEnabled = true;
            this.cbSingleGeneratorPhaseShift.Location = new System.Drawing.Point(396, 21);
            this.cbSingleGeneratorPhaseShift.Name = "cbSingleGeneratorPhaseShift";
            this.cbSingleGeneratorPhaseShift.Size = new System.Drawing.Size(90, 20);
            this.cbSingleGeneratorPhaseShift.TabIndex = 7;
            // 
            // lSignalGeneratorPhaseShift
            // 
            this.lSignalGeneratorPhaseShift.AutoSize = true;
            this.lSignalGeneratorPhaseShift.Location = new System.Drawing.Point(333, 24);
            this.lSignalGeneratorPhaseShift.Name = "lSignalGeneratorPhaseShift";
            this.lSignalGeneratorPhaseShift.Size = new System.Drawing.Size(57, 12);
            this.lSignalGeneratorPhaseShift.TabIndex = 6;
            this.lSignalGeneratorPhaseShift.Text = "Phase shift:";
            // 
            // tbSignalGeneratorFrequency
            // 
            this.tbSignalGeneratorFrequency.Location = new System.Drawing.Point(267, 21);
            this.tbSignalGeneratorFrequency.Name = "tbSignalGeneratorFrequency";
            this.tbSignalGeneratorFrequency.Size = new System.Drawing.Size(60, 22);
            this.tbSignalGeneratorFrequency.TabIndex = 5;
            // 
            // lSignalGeneratorFrequence
            // 
            this.lSignalGeneratorFrequence.AutoSize = true;
            this.lSignalGeneratorFrequence.Location = new System.Drawing.Point(205, 24);
            this.lSignalGeneratorFrequence.Name = "lSignalGeneratorFrequence";
            this.lSignalGeneratorFrequence.Size = new System.Drawing.Size(56, 12);
            this.lSignalGeneratorFrequence.TabIndex = 4;
            this.lSignalGeneratorFrequence.Text = "Frequence:";
            // 
            // cbSignalGeneratorMode
            // 
            this.cbSignalGeneratorMode.FormattingEnabled = true;
            this.cbSignalGeneratorMode.Location = new System.Drawing.Point(109, 21);
            this.cbSignalGeneratorMode.Name = "cbSignalGeneratorMode";
            this.cbSignalGeneratorMode.Size = new System.Drawing.Size(90, 20);
            this.cbSignalGeneratorMode.TabIndex = 3;
            // 
            // lSignalGeneratorMode
            // 
            this.lSignalGeneratorMode.AutoSize = true;
            this.lSignalGeneratorMode.Location = new System.Drawing.Point(68, 24);
            this.lSignalGeneratorMode.Name = "lSignalGeneratorMode";
            this.lSignalGeneratorMode.Size = new System.Drawing.Size(35, 12);
            this.lSignalGeneratorMode.TabIndex = 2;
            this.lSignalGeneratorMode.Text = "Mode:";
            // 
            // cbSignalGeneratorId
            // 
            this.cbSignalGeneratorId.FormattingEnabled = true;
            this.cbSignalGeneratorId.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbSignalGeneratorId.Location = new System.Drawing.Point(32, 21);
            this.cbSignalGeneratorId.Name = "cbSignalGeneratorId";
            this.cbSignalGeneratorId.Size = new System.Drawing.Size(30, 20);
            this.cbSignalGeneratorId.TabIndex = 1;
            this.cbSignalGeneratorId.Text = "0";
            this.cbSignalGeneratorId.SelectedIndexChanged += new System.EventHandler(this.cbSignalGeneratorId_SelectedIndexChanged);
            // 
            // lSignalGeneratorId
            // 
            this.lSignalGeneratorId.AutoSize = true;
            this.lSignalGeneratorId.Location = new System.Drawing.Point(6, 24);
            this.lSignalGeneratorId.Name = "lSignalGeneratorId";
            this.lSignalGeneratorId.Size = new System.Drawing.Size(20, 12);
            this.lSignalGeneratorId.TabIndex = 0;
            this.lSignalGeneratorId.Text = "ID:";
            // 
            // gbMpdRSSI
            // 
            this.gbMpdRSSI.Controls.Add(this.tbMpd2Rssi);
            this.gbMpdRSSI.Controls.Add(this.lMpd2Rssi);
            this.gbMpdRSSI.Controls.Add(this.tbMpd1Rssi);
            this.gbMpdRSSI.Controls.Add(this.lMpd1Rssi);
            this.gbMpdRSSI.Location = new System.Drawing.Point(595, 3);
            this.gbMpdRSSI.Name = "gbMpdRSSI";
            this.gbMpdRSSI.Size = new System.Drawing.Size(99, 80);
            this.gbMpdRSSI.TabIndex = 10;
            this.gbMpdRSSI.TabStop = false;
            this.gbMpdRSSI.Text = "MPD RSSI";
            // 
            // tbMpd2Rssi
            // 
            this.tbMpd2Rssi.Location = new System.Drawing.Point(50, 49);
            this.tbMpd2Rssi.Name = "tbMpd2Rssi";
            this.tbMpd2Rssi.Size = new System.Drawing.Size(40, 22);
            this.tbMpd2Rssi.TabIndex = 13;
            // 
            // lMpd2Rssi
            // 
            this.lMpd2Rssi.AutoSize = true;
            this.lMpd2Rssi.Location = new System.Drawing.Point(6, 56);
            this.lMpd2Rssi.Name = "lMpd2Rssi";
            this.lMpd2Rssi.Size = new System.Drawing.Size(38, 12);
            this.lMpd2Rssi.TabIndex = 12;
            this.lMpd2Rssi.Text = "MPD2:";
            // 
            // tbMpd1Rssi
            // 
            this.tbMpd1Rssi.Location = new System.Drawing.Point(50, 21);
            this.tbMpd1Rssi.Name = "tbMpd1Rssi";
            this.tbMpd1Rssi.Size = new System.Drawing.Size(40, 22);
            this.tbMpd1Rssi.TabIndex = 11;
            // 
            // lMpd1Rssi
            // 
            this.lMpd1Rssi.AutoSize = true;
            this.lMpd1Rssi.Location = new System.Drawing.Point(6, 28);
            this.lMpd1Rssi.Name = "lMpd1Rssi";
            this.lMpd1Rssi.Size = new System.Drawing.Size(38, 12);
            this.lMpd1Rssi.TabIndex = 10;
            this.lMpd1Rssi.Text = "MPD1:";
            // 
            // gbFrequenceCounter
            // 
            this.gbFrequenceCounter.Controls.Add(this.tbFrequenceCounter);
            this.gbFrequenceCounter.Controls.Add(this.lFrequenceCounter);
            this.gbFrequenceCounter.Location = new System.Drawing.Point(595, 89);
            this.gbFrequenceCounter.Name = "gbFrequenceCounter";
            this.gbFrequenceCounter.Size = new System.Drawing.Size(99, 51);
            this.gbFrequenceCounter.TabIndex = 11;
            this.gbFrequenceCounter.TabStop = false;
            this.gbFrequenceCounter.Text = "Freq counter";
            // 
            // tbFrequenceCounter
            // 
            this.tbFrequenceCounter.Location = new System.Drawing.Point(41, 21);
            this.tbFrequenceCounter.Name = "tbFrequenceCounter";
            this.tbFrequenceCounter.Size = new System.Drawing.Size(52, 22);
            this.tbFrequenceCounter.TabIndex = 14;
            // 
            // lFrequenceCounter
            // 
            this.lFrequenceCounter.AutoSize = true;
            this.lFrequenceCounter.Location = new System.Drawing.Point(6, 24);
            this.lFrequenceCounter.Name = "lFrequenceCounter";
            this.lFrequenceCounter.Size = new System.Drawing.Size(29, 12);
            this.lFrequenceCounter.TabIndex = 5;
            this.lFrequenceCounter.Text = "Freq:";
            // 
            // lAction
            // 
            this.lAction.AutoSize = true;
            this.lAction.Location = new System.Drawing.Point(625, 165);
            this.lAction.Name = "lAction";
            this.lAction.Size = new System.Drawing.Size(0, 12);
            this.lAction.TabIndex = 12;
            // 
            // UcLiDARFunctionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lAction);
            this.Controls.Add(this.gbFrequenceCounter);
            this.Controls.Add(this.gbMpdRSSI);
            this.Controls.Add(this.gbTec1);
            this.Controls.Add(this.gbNtc1);
            this.Controls.Add(this.gbTec2);
            this.Controls.Add(this.gbSignalGenerator);
            this.Controls.Add(this.gbNtc2);
            this.Controls.Add(this.cbMoniter);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bWrite);
            this.Controls.Add(this.bRead);
            this.Name = "UcLiDARFunctionTest";
            this.Size = new System.Drawing.Size(700, 340);
            this.gbTec1.ResumeLayout(false);
            this.gbTec1.PerformLayout();
            this.gbNtc1.ResumeLayout(false);
            this.gbNtc1.PerformLayout();
            this.gbNtc2.ResumeLayout(false);
            this.gbNtc2.PerformLayout();
            this.gbTec2.ResumeLayout(false);
            this.gbTec2.PerformLayout();
            this.gbSignalGenerator.ResumeLayout(false);
            this.gbSignalGenerator.PerformLayout();
            this.gbMpdRSSI.ResumeLayout(false);
            this.gbMpdRSSI.PerformLayout();
            this.gbFrequenceCounter.ResumeLayout(false);
            this.gbFrequenceCounter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lPid1Proportional;
        private System.Windows.Forms.GroupBox gbTec1;
        private System.Windows.Forms.TextBox tbPid1TargetTemperature;
        private System.Windows.Forms.Label lPid1TargetTemperature;
        private System.Windows.Forms.TextBox tbPid1Derivative;
        private System.Windows.Forms.Label lPid1Derivative;
        private System.Windows.Forms.TextBox tbPid1Integral;
        private System.Windows.Forms.Label lPid1Integral;
        private System.Windows.Forms.TextBox tbPid1Proportional;
        private System.Windows.Forms.Button bRead;
        private System.Windows.Forms.Button bWrite;
        private System.Windows.Forms.Button bSave;
        private LiveCharts.WinForms.CartesianChart ccNtc1Temperature;
        private System.Windows.Forms.GroupBox gbNtc1;
        private System.Windows.Forms.TextBox tbNtc1TemperatureOffset;
        private System.Windows.Forms.Label lNtc1TemperatureOffset;
        private System.Windows.Forms.TextBox tbNtc1Temperature;
        private System.Windows.Forms.Label lNtc1Temperature;
        private System.Windows.Forms.TextBox tbNtc1Beta;
        private System.Windows.Forms.Label lNtc1Beta;
        private System.Windows.Forms.TextBox tbNtc1Resistance;
        private System.Windows.Forms.Label lNtc1Resistance;
        private System.Windows.Forms.GroupBox gbNtc2;
        private LiveCharts.WinForms.CartesianChart ccNtc2Temperature;
        private System.Windows.Forms.TextBox tbNtc2TemperatureOffset;
        private System.Windows.Forms.Label lNtc2TemperatureOffset;
        private System.Windows.Forms.TextBox tbNtc2Temperature;
        private System.Windows.Forms.Label lNtc2Temperature;
        private System.Windows.Forms.TextBox tbNtc2Beta;
        private System.Windows.Forms.Label lNtc2Beta;
        private System.Windows.Forms.TextBox tbNtc2Resistance;
        private System.Windows.Forms.Label lNtc2Resistance;
        private System.Windows.Forms.GroupBox gbTec2;
        private System.Windows.Forms.TextBox tbPid2TargetTemperature;
        private System.Windows.Forms.Label lPid2TargetTemperature;
        private System.Windows.Forms.TextBox tbPid2Derivative;
        private System.Windows.Forms.Label lPid2Derivative;
        private System.Windows.Forms.TextBox tbPid2Integral;
        private System.Windows.Forms.Label lPid2Integral;
        private System.Windows.Forms.TextBox tbPid2Proportional;
        private System.Windows.Forms.Label lPid2Proportional;
        private System.Windows.Forms.CheckBox cbMoniter;
        private System.Windows.Forms.GroupBox gbSignalGenerator;
        private System.Windows.Forms.ComboBox cbSignalGeneratorId;
        private System.Windows.Forms.Label lSignalGeneratorId;
        private System.Windows.Forms.ComboBox cbSignalGeneratorMode;
        private System.Windows.Forms.Label lSignalGeneratorMode;
        private System.Windows.Forms.Button bSignalGeneratorSet;
        private System.Windows.Forms.ComboBox cbSingleGeneratorPhaseShift;
        private System.Windows.Forms.Label lSignalGeneratorPhaseShift;
        private System.Windows.Forms.TextBox tbSignalGeneratorFrequency;
        private System.Windows.Forms.Label lSignalGeneratorFrequence;
        private System.Windows.Forms.GroupBox gbMpdRSSI;
        private System.Windows.Forms.TextBox tbMpd2Rssi;
        private System.Windows.Forms.Label lMpd2Rssi;
        private System.Windows.Forms.TextBox tbMpd1Rssi;
        private System.Windows.Forms.Label lMpd1Rssi;
        private System.Windows.Forms.GroupBox gbFrequenceCounter;
        private System.Windows.Forms.TextBox tbFrequenceCounter;
        private System.Windows.Forms.Label lFrequenceCounter;
        private System.Windows.Forms.Label lAction;
        private LiveCharts.WinForms.CartesianChart ccTec1PwmDuty;
        private LiveCharts.WinForms.CartesianChart ccTec2PwmDuty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lTec2PwmDuty;
    }
}
