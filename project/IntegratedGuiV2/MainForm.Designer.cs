using System.Windows.Forms;

namespace IntegratedGuiV2
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.bStart = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.lFilePath = new System.Windows.Forms.Label();
            this.lLogin = new System.Windows.Forms.Label();
            this.cProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.cProgressBar2 = new CircularProgressBar.CircularProgressBar();
            this.lCh1Message = new System.Windows.Forms.Label();
            this.lCh2Message = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lCh1EC = new System.Windows.Forms.Label();
            this.lCh2EC = new System.Windows.Forms.Label();
            this.lProduct = new System.Windows.Forms.Label();
            this.lMode = new System.Windows.Forms.Label();
            this.lModeT = new System.Windows.Forms.Label();
            this.lProductT = new System.Windows.Forms.Label();
            this.lPathAP = new System.Windows.Forms.Label();
            this.lApName = new System.Windows.Forms.Label();
            this.cbBypassW = new System.Windows.Forms.CheckBox();
            this.cbI2cConnect = new System.Windows.Forms.CheckBox();
            this.gbOperatorMode = new System.Windows.Forms.GroupBox();
            this.bOpenLogFileFolder = new System.Windows.Forms.Button();
            this.cbLogCheckAfterSn = new System.Windows.Forms.CheckBox();
            this.cbRxPowerNgInterrupt = new System.Windows.Forms.CheckBox();
            this.lRxPowerCriteria = new System.Windows.Forms.Label();
            this.tbRssiCriteria = new System.Windows.Forms.TextBox();
            this.bRenewRssi = new System.Windows.Forms.Button();
            this.gbCodeEditor = new System.Windows.Forms.GroupBox();
            this.dudDd2 = new System.Windows.Forms.DomainUpDown();
            this.dudMm2 = new System.Windows.Forms.DomainUpDown();
            this.lDd2 = new System.Windows.Forms.Label();
            this.lMm2 = new System.Windows.Forms.Label();
            this.cbSnNamingRule = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dudYy = new System.Windows.Forms.DomainUpDown();
            this.dudMm = new System.Windows.Forms.DomainUpDown();
            this.dudDd = new System.Windows.Forms.DomainUpDown();
            this.dudRr = new System.Windows.Forms.DomainUpDown();
            this.dudSsss = new System.Windows.Forms.DomainUpDown();
            this.dudWw = new System.Windows.Forms.DomainUpDown();
            this.dudD = new System.Windows.Forms.DomainUpDown();
            this.dudL = new System.Windows.Forms.DomainUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lYy = new System.Windows.Forms.Label();
            this.lMm = new System.Windows.Forms.Label();
            this.lDd = new System.Windows.Forms.Label();
            this.lRr = new System.Windows.Forms.Label();
            this.lSsss = new System.Windows.Forms.Label();
            this.lWw = new System.Windows.Forms.Label();
            this.lD = new System.Windows.Forms.Label();
            this.lL = new System.Windows.Forms.Label();
            this.lRxPowerCh2_3 = new System.Windows.Forms.Label();
            this.lRxPowerCh2_2 = new System.Windows.Forms.Label();
            this.lRxPowerCh2_1 = new System.Windows.Forms.Label();
            this.lRxPowerCh2_0 = new System.Windows.Forms.Label();
            this.tbRxPowerCh2_3 = new System.Windows.Forms.TextBox();
            this.tbRxPowerCh2_2 = new System.Windows.Forms.TextBox();
            this.tbRxPowerCh2_1 = new System.Windows.Forms.TextBox();
            this.tbRxPowerCh2_0 = new System.Windows.Forms.TextBox();
            this.tbLogFilePath = new System.Windows.Forms.TextBox();
            this.lRxPowerCh1_3 = new System.Windows.Forms.Label();
            this.lLogFilePath = new System.Windows.Forms.Label();
            this.bWriteSnDateCone = new System.Windows.Forms.Button();
            this.lRxPowerCh1_2 = new System.Windows.Forms.Label();
            this.lRxPowerCh1_1 = new System.Windows.Forms.Label();
            this.tbRxPowerCh1_3 = new System.Windows.Forms.TextBox();
            this.tbRxPowerCh1_2 = new System.Windows.Forms.TextBox();
            this.tbRxPowerCh1_1 = new System.Windows.Forms.TextBox();
            this.lVenderSn = new System.Windows.Forms.Label();
            this.tbRxPowerCh1_0 = new System.Windows.Forms.TextBox();
            this.tbVenderSn = new System.Windows.Forms.TextBox();
            this.lRxPowerCh1_0 = new System.Windows.Forms.Label();
            this.lDateCode = new System.Windows.Forms.Label();
            this.tbDateCode = new System.Windows.Forms.TextBox();
            this.cbRelinkCheck = new System.Windows.Forms.CheckBox();
            this.rbLogFileMode = new System.Windows.Forms.RadioButton();
            this.rbOnlySN = new System.Windows.Forms.RadioButton();
            this.rbFullMode = new System.Windows.Forms.RadioButton();
            this.cbRegisterMapView = new System.Windows.Forms.CheckBox();
            this.cbBarcodeMode = new System.Windows.Forms.CheckBox();
            this.bRelinkTest = new System.Windows.Forms.Button();
            this.tbRelinkCount = new System.Windows.Forms.TextBox();
            this.tbIntervalTime = new System.Windows.Forms.TextBox();
            this.bLogFileComparison = new System.Windows.Forms.Button();
            this.bCfgFileComparison = new System.Windows.Forms.Button();
            this.lDataName = new System.Windows.Forms.Label();
            this.lPathData = new System.Windows.Forms.Label();
            this.tbVersionCodeCh1 = new System.Windows.Forms.TextBox();
            this.tbVersionCodeReNewCh1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbVersionCodeReNewCh2 = new System.Windows.Forms.TextBox();
            this.tbVersionCodeCh2 = new System.Windows.Forms.TextBox();
            this.cbSecurityLock = new System.Windows.Forms.CheckBox();
            this.bWriteFromFile = new System.Windows.Forms.Button();
            this.tbOrignalSNCh1 = new System.Windows.Forms.TextBox();
            this.tbReNewSNCh1 = new System.Windows.Forms.TextBox();
            this.lOriginalSN = new System.Windows.Forms.Label();
            this.lReNewSn = new System.Windows.Forms.Label();
            this.tbReNewSNCh2 = new System.Windows.Forms.TextBox();
            this.tbOrignalSNCh2 = new System.Windows.Forms.TextBox();
            this.gbPrompt = new System.Windows.Forms.GroupBox();
            this.lStatus = new System.Windows.Forms.Label();
            this.lReNewTLSN = new System.Windows.Forms.Label();
            this.tbOrignalTLSN = new System.Windows.Forms.TextBox();
            this.lOriginalTLSN = new System.Windows.Forms.Label();
            this.tbReNewTLSN = new System.Windows.Forms.TextBox();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.lBaseTime = new System.Windows.Forms.Label();
            this.lIntervalTime = new System.Windows.Forms.Label();
            this.lCount = new System.Windows.Forms.Label();
            this.tbHideKey = new System.Windows.Forms.TextBox();
            this.gbReWriteSn = new System.Windows.Forms.GroupBox();
            this.tbCustomerSn = new System.Windows.Forms.TextBox();
            this.cbReParameter = new System.Windows.Forms.CheckBox();
            this.bSearchNumber = new System.Windows.Forms.Button();
            this.cbAutoWirte = new System.Windows.Forms.CheckBox();
            this.bWriteTruelightSn = new System.Windows.Forms.Button();
            this.lTruelightSn = new System.Windows.Forms.Label();
            this.lCustomerSn = new System.Windows.Forms.Label();
            this.tbTruelightSn = new System.Windows.Forms.TextBox();
            this.bCheckSerialNumber = new System.Windows.Forms.Button();
            this.bExportRegister = new System.Windows.Forms.Button();
            this.gbTruelightSn = new System.Windows.Forms.GroupBox();
            this.cbDeepCheck = new System.Windows.Forms.CheckBox();
            this.gbRelinkTest = new System.Windows.Forms.GroupBox();
            this.gbOptionsControl = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbCfgCheckAfterFw = new System.Windows.Forms.CheckBox();
            this.bHardwareValidation = new System.Windows.Forms.Button();
            this.bCurrentRegister = new System.Windows.Forms.Button();
            this.gbOperatorMode.SuspendLayout();
            this.gbCodeEditor.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbPrompt.SuspendLayout();
            this.gbReWriteSn.SuspendLayout();
            this.gbTruelightSn.SuspendLayout();
            this.gbRelinkTest.SuspendLayout();
            this.gbOptionsControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 15;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bStart.FlatAppearance.BorderSize = 0;
            this.bStart.Font = new System.Drawing.Font("Nirmala UI", 16F, System.Drawing.FontStyle.Bold);
            this.bStart.ForeColor = System.Drawing.Color.White;
            this.bStart.Location = new System.Drawing.Point(20, 73);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(76, 53);
            this.bStart.TabIndex = 44;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.BackColor = System.Drawing.Color.White;
            this.tbFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFilePath.Font = new System.Drawing.Font("Nirmala UI", 10F);
            this.tbFilePath.ForeColor = System.Drawing.Color.Silver;
            this.tbFilePath.Location = new System.Drawing.Point(20, 47);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(289, 25);
            this.tbFilePath.TabIndex = 43;
            this.tbFilePath.Text = "Please click here, to import the Config file...";
            this.tbFilePath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbFilePath_MouseClick);
            this.tbFilePath.Enter += new System.EventHandler(this.tbFilePath_Leave);
            this.tbFilePath.Leave += new System.EventHandler(this.tbFilePath_Leave);
            // 
            // lFilePath
            // 
            this.lFilePath.AutoSize = true;
            this.lFilePath.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lFilePath.ForeColor = System.Drawing.Color.White;
            this.lFilePath.Location = new System.Drawing.Point(16, 27);
            this.lFilePath.Name = "lFilePath";
            this.lFilePath.Size = new System.Drawing.Size(118, 19);
            this.lFilePath.TabIndex = 42;
            this.lFilePath.Text = "Config File path:";
            // 
            // lLogin
            // 
            this.lLogin.AutoSize = true;
            this.lLogin.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.lLogin.Location = new System.Drawing.Point(15, -4);
            this.lLogin.Name = "lLogin";
            this.lLogin.Size = new System.Drawing.Size(292, 30);
            this.lLogin.TabIndex = 41;
            this.lLogin.Text = "OptiSync Firmware Manager";
            // 
            // cProgressBar1
            // 
            this.cProgressBar1.AnimationFunction = ((WinFormAnimation.AnimationFunctions.Function)(resources.GetObject("cProgressBar1.AnimationFunction")));
            this.cProgressBar1.AnimationSpeed = 500;
            this.cProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.cProgressBar1.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold);
            this.cProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cProgressBar1.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.cProgressBar1.InnerMargin = 2;
            this.cProgressBar1.InnerWidth = -1;
            this.cProgressBar1.Location = new System.Drawing.Point(323, 21);
            this.cProgressBar1.MarqueeAnimationSpeed = 2000;
            this.cProgressBar1.Name = "cProgressBar1";
            this.cProgressBar1.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(26)))), ((int)(((byte)(43)))));
            this.cProgressBar1.OuterMargin = -25;
            this.cProgressBar1.OuterWidth = 26;
            this.cProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cProgressBar1.ProgressWidth = 3;
            this.cProgressBar1.SecondaryFont = new System.Drawing.Font("PMingLiU", 36F);
            this.cProgressBar1.Size = new System.Drawing.Size(100, 100);
            this.cProgressBar1.StartAngle = 270;
            this.cProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.cProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cProgressBar1.SubscriptText = "";
            this.cProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cProgressBar1.SuperscriptText = "";
            this.cProgressBar1.TabIndex = 48;
            this.cProgressBar1.Text = "A";
            this.cProgressBar1.TextMargin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.cProgressBar1.Value = 68;
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Checked = true;
            this.rbSingle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rbSingle.Location = new System.Drawing.Point(102, 76);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(68, 23);
            this.rbSingle.TabIndex = 49;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "Single";
            this.rbSingle.UseVisualStyleBackColor = true;
            this.rbSingle.CheckedChanged += new System.EventHandler(this.raSingle_CheckedChanged);
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rbBoth.Location = new System.Drawing.Point(102, 99);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(89, 23);
            this.rbBoth.TabIndex = 50;
            this.rbBoth.Text = "Both side";
            this.rbBoth.UseVisualStyleBackColor = true;
            this.rbBoth.CheckedChanged += new System.EventHandler(this.rbBoth_CheckedChanged);
            // 
            // cProgressBar2
            // 
            this.cProgressBar2.AnimationFunction = ((WinFormAnimation.AnimationFunctions.Function)(resources.GetObject("cProgressBar2.AnimationFunction")));
            this.cProgressBar2.AnimationSpeed = 500;
            this.cProgressBar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.cProgressBar2.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold);
            this.cProgressBar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cProgressBar2.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.cProgressBar2.InnerMargin = 2;
            this.cProgressBar2.InnerWidth = -1;
            this.cProgressBar2.Location = new System.Drawing.Point(429, 21);
            this.cProgressBar2.MarqueeAnimationSpeed = 2000;
            this.cProgressBar2.Name = "cProgressBar2";
            this.cProgressBar2.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(26)))), ((int)(((byte)(43)))));
            this.cProgressBar2.OuterMargin = -25;
            this.cProgressBar2.OuterWidth = 26;
            this.cProgressBar2.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cProgressBar2.ProgressWidth = 3;
            this.cProgressBar2.SecondaryFont = new System.Drawing.Font("PMingLiU", 36F);
            this.cProgressBar2.Size = new System.Drawing.Size(100, 100);
            this.cProgressBar2.StartAngle = 270;
            this.cProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.cProgressBar2.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cProgressBar2.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cProgressBar2.SubscriptText = "";
            this.cProgressBar2.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cProgressBar2.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cProgressBar2.SuperscriptText = "";
            this.cProgressBar2.TabIndex = 51;
            this.cProgressBar2.Text = "B";
            this.cProgressBar2.TextMargin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.cProgressBar2.Value = 68;
            this.cProgressBar2.Visible = false;
            // 
            // lCh1Message
            // 
            this.lCh1Message.AutoSize = true;
            this.lCh1Message.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lCh1Message.ForeColor = System.Drawing.Color.White;
            this.lCh1Message.Location = new System.Drawing.Point(328, 120);
            this.lCh1Message.Name = "lCh1Message";
            this.lCh1Message.Size = new System.Drawing.Size(14, 12);
            this.lCh1Message.TabIndex = 52;
            this.lCh1Message.Text = "...";
            // 
            // lCh2Message
            // 
            this.lCh2Message.AutoSize = true;
            this.lCh2Message.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lCh2Message.ForeColor = System.Drawing.Color.White;
            this.lCh2Message.Location = new System.Drawing.Point(434, 120);
            this.lCh2Message.Name = "lCh2Message";
            this.lCh2Message.Size = new System.Drawing.Size(14, 12);
            this.lCh2Message.TabIndex = 53;
            this.lCh2Message.Text = "...";
            this.lCh2Message.Visible = false;
            // 
            // lCh1EC
            // 
            this.lCh1EC.AutoSize = true;
            this.lCh1EC.BackColor = System.Drawing.Color.Transparent;
            this.lCh1EC.Font = new System.Drawing.Font("Nirmala UI", 5F, System.Drawing.FontStyle.Bold);
            this.lCh1EC.ForeColor = System.Drawing.Color.White;
            this.lCh1EC.Location = new System.Drawing.Point(397, 19);
            this.lCh1EC.Name = "lCh1EC";
            this.lCh1EC.Size = new System.Drawing.Size(11, 10);
            this.lCh1EC.TabIndex = 54;
            this.lCh1EC.Text = "...";
            this.lCh1EC.Visible = false;
            // 
            // lCh2EC
            // 
            this.lCh2EC.AutoSize = true;
            this.lCh2EC.BackColor = System.Drawing.Color.Transparent;
            this.lCh2EC.Font = new System.Drawing.Font("Nirmala UI", 5F, System.Drawing.FontStyle.Bold);
            this.lCh2EC.ForeColor = System.Drawing.Color.White;
            this.lCh2EC.Location = new System.Drawing.Point(504, 19);
            this.lCh2EC.Name = "lCh2EC";
            this.lCh2EC.Size = new System.Drawing.Size(11, 10);
            this.lCh2EC.TabIndex = 55;
            this.lCh2EC.Text = "...";
            this.lCh2EC.Visible = false;
            // 
            // lProduct
            // 
            this.lProduct.AutoSize = true;
            this.lProduct.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lProduct.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lProduct.Location = new System.Drawing.Point(74, 180);
            this.lProduct.Name = "lProduct";
            this.lProduct.Size = new System.Drawing.Size(12, 13);
            this.lProduct.TabIndex = 56;
            this.lProduct.Text = "_";
            // 
            // lMode
            // 
            this.lMode.AutoSize = true;
            this.lMode.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lMode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lMode.Location = new System.Drawing.Point(65, 195);
            this.lMode.Name = "lMode";
            this.lMode.Size = new System.Drawing.Size(12, 13);
            this.lMode.TabIndex = 57;
            this.lMode.Text = "_";
            // 
            // lModeT
            // 
            this.lModeT.AutoSize = true;
            this.lModeT.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lModeT.ForeColor = System.Drawing.Color.White;
            this.lModeT.Location = new System.Drawing.Point(21, 195);
            this.lModeT.Name = "lModeT";
            this.lModeT.Size = new System.Drawing.Size(41, 13);
            this.lModeT.TabIndex = 59;
            this.lModeT.Text = "Mode:";
            // 
            // lProductT
            // 
            this.lProductT.AutoSize = true;
            this.lProductT.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lProductT.ForeColor = System.Drawing.Color.White;
            this.lProductT.Location = new System.Drawing.Point(21, 180);
            this.lProductT.Name = "lProductT";
            this.lProductT.Size = new System.Drawing.Size(51, 13);
            this.lProductT.TabIndex = 58;
            this.lProductT.Text = "Product:";
            // 
            // lPathAP
            // 
            this.lPathAP.AutoSize = true;
            this.lPathAP.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lPathAP.ForeColor = System.Drawing.Color.White;
            this.lPathAP.Location = new System.Drawing.Point(21, 210);
            this.lPathAP.Name = "lPathAP";
            this.lPathAP.Size = new System.Drawing.Size(51, 13);
            this.lPathAP.TabIndex = 60;
            this.lPathAP.Text = "APROM:";
            // 
            // lApName
            // 
            this.lApName.AutoSize = true;
            this.lApName.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.lApName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lApName.Location = new System.Drawing.Point(76, 212);
            this.lApName.Name = "lApName";
            this.lApName.Size = new System.Drawing.Size(8, 11);
            this.lApName.TabIndex = 61;
            this.lApName.Text = "_";
            // 
            // cbBypassW
            // 
            this.cbBypassW.AutoSize = true;
            this.cbBypassW.Checked = true;
            this.cbBypassW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBypassW.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbBypassW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbBypassW.Location = new System.Drawing.Point(112, 133);
            this.cbBypassW.Name = "cbBypassW";
            this.cbBypassW.Size = new System.Drawing.Size(102, 15);
            this.cbBypassW.TabIndex = 62;
            this.cbBypassW.Text = "BypassIcConfigWrite";
            this.cbBypassW.UseVisualStyleBackColor = true;
            this.cbBypassW.Visible = false;
            // 
            // cbI2cConnect
            // 
            this.cbI2cConnect.AutoSize = true;
            this.cbI2cConnect.Checked = true;
            this.cbI2cConnect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbI2cConnect.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbI2cConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbI2cConnect.Location = new System.Drawing.Point(444, 0);
            this.cbI2cConnect.Name = "cbI2cConnect";
            this.cbI2cConnect.Size = new System.Drawing.Size(65, 15);
            this.cbI2cConnect.TabIndex = 63;
            this.cbI2cConnect.Text = "ChConnect";
            this.cbI2cConnect.UseVisualStyleBackColor = true;
            this.cbI2cConnect.CheckedChanged += new System.EventHandler(this.I2cMasterConnect_CheckedChanged);
            // 
            // gbOperatorMode
            // 
            this.gbOperatorMode.Controls.Add(this.bOpenLogFileFolder);
            this.gbOperatorMode.Controls.Add(this.cbLogCheckAfterSn);
            this.gbOperatorMode.Controls.Add(this.cbRxPowerNgInterrupt);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCriteria);
            this.gbOperatorMode.Controls.Add(this.tbRssiCriteria);
            this.gbOperatorMode.Controls.Add(this.bRenewRssi);
            this.gbOperatorMode.Controls.Add(this.gbCodeEditor);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh2_3);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh2_2);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh2_1);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh2_0);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh2_3);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh2_2);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh2_1);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh2_0);
            this.gbOperatorMode.Controls.Add(this.tbLogFilePath);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh1_3);
            this.gbOperatorMode.Controls.Add(this.lLogFilePath);
            this.gbOperatorMode.Controls.Add(this.bWriteSnDateCone);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh1_2);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh1_1);
            this.gbOperatorMode.Controls.Add(this.cbBypassW);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh1_3);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh1_2);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh1_1);
            this.gbOperatorMode.Controls.Add(this.lVenderSn);
            this.gbOperatorMode.Controls.Add(this.tbRxPowerCh1_0);
            this.gbOperatorMode.Controls.Add(this.tbVenderSn);
            this.gbOperatorMode.Controls.Add(this.lRxPowerCh1_0);
            this.gbOperatorMode.Controls.Add(this.lDateCode);
            this.gbOperatorMode.Controls.Add(this.tbDateCode);
            this.gbOperatorMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbOperatorMode.Location = new System.Drawing.Point(23, 240);
            this.gbOperatorMode.Name = "gbOperatorMode";
            this.gbOperatorMode.Size = new System.Drawing.Size(506, 177);
            this.gbOperatorMode.TabIndex = 70;
            this.gbOperatorMode.TabStop = false;
            this.gbOperatorMode.Text = "Serial number information";
            this.gbOperatorMode.Visible = false;
            // 
            // bOpenLogFileFolder
            // 
            this.bOpenLogFileFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bOpenLogFileFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bOpenLogFileFolder.FlatAppearance.BorderSize = 0;
            this.bOpenLogFileFolder.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOpenLogFileFolder.ForeColor = System.Drawing.Color.White;
            this.bOpenLogFileFolder.Location = new System.Drawing.Point(223, 149);
            this.bOpenLogFileFolder.Name = "bOpenLogFileFolder";
            this.bOpenLogFileFolder.Size = new System.Drawing.Size(15, 20);
            this.bOpenLogFileFolder.TabIndex = 124;
            this.bOpenLogFileFolder.UseVisualStyleBackColor = false;
            this.bOpenLogFileFolder.Click += new System.EventHandler(this.bOpenLogFileFolder_Click);
            // 
            // cbLogCheckAfterSn
            // 
            this.cbLogCheckAfterSn.AutoSize = true;
            this.cbLogCheckAfterSn.Checked = true;
            this.cbLogCheckAfterSn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLogCheckAfterSn.Enabled = false;
            this.cbLogCheckAfterSn.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbLogCheckAfterSn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbLogCheckAfterSn.Location = new System.Drawing.Point(7, 65);
            this.cbLogCheckAfterSn.Name = "cbLogCheckAfterSn";
            this.cbLogCheckAfterSn.Size = new System.Drawing.Size(131, 15);
            this.cbLogCheckAfterSn.TabIndex = 113;
            this.cbLogCheckAfterSn.Text = "LogFileCheck after SN writing";
            this.cbLogCheckAfterSn.UseVisualStyleBackColor = true;
            // 
            // cbRxPowerNgInterrupt
            // 
            this.cbRxPowerNgInterrupt.AutoSize = true;
            this.cbRxPowerNgInterrupt.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbRxPowerNgInterrupt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbRxPowerNgInterrupt.Location = new System.Drawing.Point(199, 49);
            this.cbRxPowerNgInterrupt.Name = "cbRxPowerNgInterrupt";
            this.cbRxPowerNgInterrupt.Size = new System.Drawing.Size(72, 15);
            this.cbRxPowerNgInterrupt.TabIndex = 109;
            this.cbRxPowerNgInterrupt.Text = "NG Interrupt";
            this.cbRxPowerNgInterrupt.UseVisualStyleBackColor = true;
            // 
            // lRxPowerCriteria
            // 
            this.lRxPowerCriteria.AutoSize = true;
            this.lRxPowerCriteria.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCriteria.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCriteria.Location = new System.Drawing.Point(199, 13);
            this.lRxPowerCriteria.Name = "lRxPowerCriteria";
            this.lRxPowerCriteria.Size = new System.Drawing.Size(31, 11);
            this.lRxPowerCriteria.TabIndex = 108;
            this.lRxPowerCriteria.Text = "Criteria";
            // 
            // tbRssiCriteria
            // 
            this.tbRssiCriteria.Font = new System.Drawing.Font("Nirmala UI", 6F);
            this.tbRssiCriteria.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRssiCriteria.Location = new System.Drawing.Point(199, 25);
            this.tbRssiCriteria.Name = "tbRssiCriteria";
            this.tbRssiCriteria.Size = new System.Drawing.Size(32, 18);
            this.tbRssiCriteria.TabIndex = 107;
            this.tbRssiCriteria.Text = "250";
            this.tbRssiCriteria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bRenewRssi
            // 
            this.bRenewRssi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bRenewRssi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRenewRssi.FlatAppearance.BorderSize = 0;
            this.bRenewRssi.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRenewRssi.ForeColor = System.Drawing.Color.White;
            this.bRenewRssi.Location = new System.Drawing.Point(99, 21);
            this.bRenewRssi.Name = "bRenewRssi";
            this.bRenewRssi.Size = new System.Drawing.Size(90, 43);
            this.bRenewRssi.TabIndex = 89;
            this.bRenewRssi.Text = "ReRSSI";
            this.bRenewRssi.UseVisualStyleBackColor = false;
            this.bRenewRssi.Click += new System.EventHandler(this.ReRssi_Click);
            // 
            // gbCodeEditor
            // 
            this.gbCodeEditor.Controls.Add(this.dudDd2);
            this.gbCodeEditor.Controls.Add(this.dudMm2);
            this.gbCodeEditor.Controls.Add(this.lDd2);
            this.gbCodeEditor.Controls.Add(this.lMm2);
            this.gbCodeEditor.Controls.Add(this.cbSnNamingRule);
            this.gbCodeEditor.Controls.Add(this.flowLayoutPanel2);
            this.gbCodeEditor.Controls.Add(this.flowLayoutPanel1);
            this.gbCodeEditor.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbCodeEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbCodeEditor.Location = new System.Drawing.Point(245, 61);
            this.gbCodeEditor.Name = "gbCodeEditor";
            this.gbCodeEditor.Size = new System.Drawing.Size(256, 89);
            this.gbCodeEditor.TabIndex = 93;
            this.gbCodeEditor.TabStop = false;
            this.gbCodeEditor.Text = "Code Editor";
            // 
            // dudDd2
            // 
            this.dudDd2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudDd2.Location = new System.Drawing.Point(211, 21);
            this.dudDd2.Name = "dudDd2";
            this.dudDd2.Size = new System.Drawing.Size(40, 22);
            this.dudDd2.TabIndex = 113;
            this.dudDd2.Visible = false;
            // 
            // dudMm2
            // 
            this.dudMm2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudMm2.Location = new System.Drawing.Point(167, 21);
            this.dudMm2.Name = "dudMm2";
            this.dudMm2.Size = new System.Drawing.Size(40, 22);
            this.dudMm2.TabIndex = 112;
            this.dudMm2.Visible = false;
            // 
            // lDd2
            // 
            this.lDd2.AutoSize = true;
            this.lDd2.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lDd2.Location = new System.Drawing.Point(211, 8);
            this.lDd2.Name = "lDd2";
            this.lDd2.Size = new System.Drawing.Size(23, 13);
            this.lDd2.TabIndex = 111;
            this.lDd2.Text = "DD";
            this.lDd2.Visible = false;
            // 
            // lMm2
            // 
            this.lMm2.AutoSize = true;
            this.lMm2.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lMm2.Location = new System.Drawing.Point(166, 8);
            this.lMm2.Name = "lMm2";
            this.lMm2.Size = new System.Drawing.Size(29, 13);
            this.lMm2.TabIndex = 110;
            this.lMm2.Text = "MM";
            this.lMm2.Visible = false;
            // 
            // cbSnNamingRule
            // 
            this.cbSnNamingRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSnNamingRule.FormattingEnabled = true;
            this.cbSnNamingRule.Location = new System.Drawing.Point(5, 15);
            this.cbSnNamingRule.Name = "cbSnNamingRule";
            this.cbSnNamingRule.Size = new System.Drawing.Size(152, 21);
            this.cbSnNamingRule.TabIndex = 108;
            this.cbSnNamingRule.SelectedIndexChanged += new System.EventHandler(this.cbSnNamingRule_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.dudYy);
            this.flowLayoutPanel2.Controls.Add(this.dudMm);
            this.flowLayoutPanel2.Controls.Add(this.dudDd);
            this.flowLayoutPanel2.Controls.Add(this.dudRr);
            this.flowLayoutPanel2.Controls.Add(this.dudSsss);
            this.flowLayoutPanel2.Controls.Add(this.dudWw);
            this.flowLayoutPanel2.Controls.Add(this.dudD);
            this.flowLayoutPanel2.Controls.Add(this.dudL);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 58);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(250, 28);
            this.flowLayoutPanel2.TabIndex = 107;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // dudYy
            // 
            this.dudYy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudYy.Location = new System.Drawing.Point(3, 3);
            this.dudYy.Name = "dudYy";
            this.dudYy.Size = new System.Drawing.Size(40, 22);
            this.dudYy.TabIndex = 100;
            this.dudYy.TextChanged += new System.EventHandler(this.domainUpDown_TextChanged);
            // 
            // dudMm
            // 
            this.dudMm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudMm.Location = new System.Drawing.Point(49, 3);
            this.dudMm.Name = "dudMm";
            this.dudMm.Size = new System.Drawing.Size(40, 22);
            this.dudMm.TabIndex = 101;
            this.dudMm.TextChanged += new System.EventHandler(this.domainUpDown_TextChanged);
            // 
            // dudDd
            // 
            this.dudDd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudDd.Location = new System.Drawing.Point(95, 3);
            this.dudDd.Name = "dudDd";
            this.dudDd.Size = new System.Drawing.Size(40, 22);
            this.dudDd.TabIndex = 102;
            // 
            // dudRr
            // 
            this.dudRr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudRr.Location = new System.Drawing.Point(141, 3);
            this.dudRr.Name = "dudRr";
            this.dudRr.Size = new System.Drawing.Size(40, 22);
            this.dudRr.TabIndex = 103;
            // 
            // dudSsss
            // 
            this.dudSsss.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudSsss.Location = new System.Drawing.Point(187, 3);
            this.dudSsss.Name = "dudSsss";
            this.dudSsss.Size = new System.Drawing.Size(55, 22);
            this.dudSsss.TabIndex = 104;
            // 
            // dudWw
            // 
            this.dudWw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudWw.Location = new System.Drawing.Point(248, 3);
            this.dudWw.Name = "dudWw";
            this.dudWw.Size = new System.Drawing.Size(40, 22);
            this.dudWw.TabIndex = 105;
            this.dudWw.TextChanged += new System.EventHandler(this.domainUpDown_TextChanged);
            // 
            // dudD
            // 
            this.dudD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudD.Location = new System.Drawing.Point(294, 3);
            this.dudD.Name = "dudD";
            this.dudD.Size = new System.Drawing.Size(40, 22);
            this.dudD.TabIndex = 106;
            // 
            // dudL
            // 
            this.dudL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.dudL.Location = new System.Drawing.Point(340, 3);
            this.dudL.Name = "dudL";
            this.dudL.Size = new System.Drawing.Size(40, 22);
            this.dudL.TabIndex = 107;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lYy);
            this.flowLayoutPanel1.Controls.Add(this.lMm);
            this.flowLayoutPanel1.Controls.Add(this.lDd);
            this.flowLayoutPanel1.Controls.Add(this.lRr);
            this.flowLayoutPanel1.Controls.Add(this.lSsss);
            this.flowLayoutPanel1.Controls.Add(this.lWw);
            this.flowLayoutPanel1.Controls.Add(this.lD);
            this.flowLayoutPanel1.Controls.Add(this.lL);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 45);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(247, 18);
            this.flowLayoutPanel1.TabIndex = 109;
            // 
            // lYy
            // 
            this.lYy.AutoSize = true;
            this.lYy.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lYy.Location = new System.Drawing.Point(3, 0);
            this.lYy.Name = "lYy";
            this.lYy.Size = new System.Drawing.Size(21, 13);
            this.lYy.TabIndex = 104;
            this.lYy.Text = "YY";
            // 
            // lMm
            // 
            this.lMm.AutoSize = true;
            this.lMm.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lMm.Location = new System.Drawing.Point(30, 0);
            this.lMm.Name = "lMm";
            this.lMm.Size = new System.Drawing.Size(29, 13);
            this.lMm.TabIndex = 105;
            this.lMm.Text = "MM";
            // 
            // lDd
            // 
            this.lDd.AutoSize = true;
            this.lDd.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lDd.Location = new System.Drawing.Point(65, 0);
            this.lDd.Name = "lDd";
            this.lDd.Size = new System.Drawing.Size(23, 13);
            this.lDd.TabIndex = 106;
            this.lDd.Text = "DD";
            // 
            // lRr
            // 
            this.lRr.AutoSize = true;
            this.lRr.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lRr.Location = new System.Drawing.Point(94, 0);
            this.lRr.Name = "lRr";
            this.lRr.Size = new System.Drawing.Size(21, 13);
            this.lRr.TabIndex = 107;
            this.lRr.Text = "RR";
            // 
            // lSsss
            // 
            this.lSsss.AutoSize = true;
            this.lSsss.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lSsss.Location = new System.Drawing.Point(121, 0);
            this.lSsss.Name = "lSsss";
            this.lSsss.Size = new System.Drawing.Size(31, 13);
            this.lSsss.TabIndex = 108;
            this.lSsss.Text = "SSSS";
            // 
            // lWw
            // 
            this.lWw.AutoSize = true;
            this.lWw.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lWw.Location = new System.Drawing.Point(158, 0);
            this.lWw.Name = "lWw";
            this.lWw.Size = new System.Drawing.Size(29, 13);
            this.lWw.TabIndex = 109;
            this.lWw.Text = "WW";
            // 
            // lD
            // 
            this.lD.AutoSize = true;
            this.lD.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lD.Location = new System.Drawing.Point(193, 0);
            this.lD.Name = "lD";
            this.lD.Size = new System.Drawing.Size(15, 13);
            this.lD.TabIndex = 110;
            this.lD.Text = "D";
            // 
            // lL
            // 
            this.lL.AutoSize = true;
            this.lL.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lL.Location = new System.Drawing.Point(214, 0);
            this.lL.Name = "lL";
            this.lL.Size = new System.Drawing.Size(13, 13);
            this.lL.TabIndex = 111;
            this.lL.Text = "L";
            // 
            // lRxPowerCh2_3
            // 
            this.lRxPowerCh2_3.AutoSize = true;
            this.lRxPowerCh2_3.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh2_3.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh2_3.Location = new System.Drawing.Point(469, 13);
            this.lRxPowerCh2_3.Name = "lRxPowerCh2_3";
            this.lRxPowerCh2_3.Size = new System.Drawing.Size(29, 11);
            this.lRxPowerCh2_3.TabIndex = 90;
            this.lRxPowerCh2_3.Text = "BRxP4";
            // 
            // lRxPowerCh2_2
            // 
            this.lRxPowerCh2_2.AutoSize = true;
            this.lRxPowerCh2_2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh2_2.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh2_2.Location = new System.Drawing.Point(436, 13);
            this.lRxPowerCh2_2.Name = "lRxPowerCh2_2";
            this.lRxPowerCh2_2.Size = new System.Drawing.Size(29, 11);
            this.lRxPowerCh2_2.TabIndex = 89;
            this.lRxPowerCh2_2.Text = "BRxP3";
            // 
            // lRxPowerCh2_1
            // 
            this.lRxPowerCh2_1.AutoSize = true;
            this.lRxPowerCh2_1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh2_1.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh2_1.Location = new System.Drawing.Point(403, 13);
            this.lRxPowerCh2_1.Name = "lRxPowerCh2_1";
            this.lRxPowerCh2_1.Size = new System.Drawing.Size(29, 11);
            this.lRxPowerCh2_1.TabIndex = 88;
            this.lRxPowerCh2_1.Text = "BRxP2";
            // 
            // lRxPowerCh2_0
            // 
            this.lRxPowerCh2_0.AutoSize = true;
            this.lRxPowerCh2_0.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh2_0.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh2_0.Location = new System.Drawing.Point(370, 13);
            this.lRxPowerCh2_0.Name = "lRxPowerCh2_0";
            this.lRxPowerCh2_0.Size = new System.Drawing.Size(29, 11);
            this.lRxPowerCh2_0.TabIndex = 87;
            this.lRxPowerCh2_0.Text = "BRxP1";
            // 
            // tbRxPowerCh2_3
            // 
            this.tbRxPowerCh2_3.Enabled = false;
            this.tbRxPowerCh2_3.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh2_3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh2_3.Location = new System.Drawing.Point(469, 25);
            this.tbRxPowerCh2_3.Name = "tbRxPowerCh2_3";
            this.tbRxPowerCh2_3.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh2_3.TabIndex = 86;
            // 
            // tbRxPowerCh2_2
            // 
            this.tbRxPowerCh2_2.Enabled = false;
            this.tbRxPowerCh2_2.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh2_2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh2_2.Location = new System.Drawing.Point(436, 25);
            this.tbRxPowerCh2_2.Name = "tbRxPowerCh2_2";
            this.tbRxPowerCh2_2.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh2_2.TabIndex = 85;
            // 
            // tbRxPowerCh2_1
            // 
            this.tbRxPowerCh2_1.Enabled = false;
            this.tbRxPowerCh2_1.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh2_1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh2_1.Location = new System.Drawing.Point(403, 25);
            this.tbRxPowerCh2_1.Name = "tbRxPowerCh2_1";
            this.tbRxPowerCh2_1.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh2_1.TabIndex = 84;
            // 
            // tbRxPowerCh2_0
            // 
            this.tbRxPowerCh2_0.Enabled = false;
            this.tbRxPowerCh2_0.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh2_0.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh2_0.Location = new System.Drawing.Point(370, 25);
            this.tbRxPowerCh2_0.Name = "tbRxPowerCh2_0";
            this.tbRxPowerCh2_0.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh2_0.TabIndex = 83;
            // 
            // tbLogFilePath
            // 
            this.tbLogFilePath.BackColor = System.Drawing.Color.White;
            this.tbLogFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLogFilePath.Font = new System.Drawing.Font("Nirmala UI", 7F);
            this.tbLogFilePath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbLogFilePath.Location = new System.Drawing.Point(15, 149);
            this.tbLogFilePath.Name = "tbLogFilePath";
            this.tbLogFilePath.Size = new System.Drawing.Size(207, 20);
            this.tbLogFilePath.TabIndex = 72;
            this.tbLogFilePath.Text = "Please click here to set the export file path.";
            this.tbLogFilePath.Click += new System.EventHandler(this.tbLogFilePath_Click);
            // 
            // lRxPowerCh1_3
            // 
            this.lRxPowerCh1_3.AutoSize = true;
            this.lRxPowerCh1_3.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh1_3.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh1_3.Location = new System.Drawing.Point(333, 13);
            this.lRxPowerCh1_3.Name = "lRxPowerCh1_3";
            this.lRxPowerCh1_3.Size = new System.Drawing.Size(30, 11);
            this.lRxPowerCh1_3.TabIndex = 75;
            this.lRxPowerCh1_3.Text = "ARxP4";
            // 
            // lLogFilePath
            // 
            this.lLogFilePath.AutoSize = true;
            this.lLogFilePath.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLogFilePath.ForeColor = System.Drawing.Color.White;
            this.lLogFilePath.Location = new System.Drawing.Point(11, 134);
            this.lLogFilePath.Name = "lLogFilePath";
            this.lLogFilePath.Size = new System.Drawing.Size(76, 13);
            this.lLogFilePath.TabIndex = 71;
            this.lLogFilePath.Text = "Log file path:";
            // 
            // bWriteSnDateCone
            // 
            this.bWriteSnDateCone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bWriteSnDateCone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bWriteSnDateCone.FlatAppearance.BorderSize = 0;
            this.bWriteSnDateCone.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold);
            this.bWriteSnDateCone.ForeColor = System.Drawing.Color.White;
            this.bWriteSnDateCone.Location = new System.Drawing.Point(6, 21);
            this.bWriteSnDateCone.Name = "bWriteSnDateCone";
            this.bWriteSnDateCone.Size = new System.Drawing.Size(90, 43);
            this.bWriteSnDateCone.TabIndex = 82;
            this.bWriteSnDateCone.Text = "Write SN";
            this.bWriteSnDateCone.UseVisualStyleBackColor = false;
            this.bWriteSnDateCone.Visible = false;
            this.bWriteSnDateCone.Click += new System.EventHandler(this.bWriteSnDateCode_Click);
            // 
            // lRxPowerCh1_2
            // 
            this.lRxPowerCh1_2.AutoSize = true;
            this.lRxPowerCh1_2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh1_2.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh1_2.Location = new System.Drawing.Point(300, 13);
            this.lRxPowerCh1_2.Name = "lRxPowerCh1_2";
            this.lRxPowerCh1_2.Size = new System.Drawing.Size(30, 11);
            this.lRxPowerCh1_2.TabIndex = 74;
            this.lRxPowerCh1_2.Text = "ARxP3";
            // 
            // lRxPowerCh1_1
            // 
            this.lRxPowerCh1_1.AutoSize = true;
            this.lRxPowerCh1_1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh1_1.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh1_1.Location = new System.Drawing.Point(267, 13);
            this.lRxPowerCh1_1.Name = "lRxPowerCh1_1";
            this.lRxPowerCh1_1.Size = new System.Drawing.Size(30, 11);
            this.lRxPowerCh1_1.TabIndex = 73;
            this.lRxPowerCh1_1.Text = "ARxP2";
            // 
            // tbRxPowerCh1_3
            // 
            this.tbRxPowerCh1_3.Enabled = false;
            this.tbRxPowerCh1_3.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh1_3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh1_3.Location = new System.Drawing.Point(333, 25);
            this.tbRxPowerCh1_3.Name = "tbRxPowerCh1_3";
            this.tbRxPowerCh1_3.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh1_3.TabIndex = 72;
            // 
            // tbRxPowerCh1_2
            // 
            this.tbRxPowerCh1_2.Enabled = false;
            this.tbRxPowerCh1_2.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh1_2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh1_2.Location = new System.Drawing.Point(300, 25);
            this.tbRxPowerCh1_2.Name = "tbRxPowerCh1_2";
            this.tbRxPowerCh1_2.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh1_2.TabIndex = 71;
            // 
            // tbRxPowerCh1_1
            // 
            this.tbRxPowerCh1_1.Enabled = false;
            this.tbRxPowerCh1_1.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh1_1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh1_1.Location = new System.Drawing.Point(267, 25);
            this.tbRxPowerCh1_1.Name = "tbRxPowerCh1_1";
            this.tbRxPowerCh1_1.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh1_1.TabIndex = 70;
            // 
            // lVenderSn
            // 
            this.lVenderSn.AutoSize = true;
            this.lVenderSn.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lVenderSn.ForeColor = System.Drawing.Color.White;
            this.lVenderSn.Location = new System.Drawing.Point(108, 83);
            this.lVenderSn.Name = "lVenderSn";
            this.lVenderSn.Size = new System.Drawing.Size(64, 13);
            this.lVenderSn.TabIndex = 64;
            this.lVenderSn.Text = "Vender SN:";
            // 
            // tbRxPowerCh1_0
            // 
            this.tbRxPowerCh1_0.Enabled = false;
            this.tbRxPowerCh1_0.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRxPowerCh1_0.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbRxPowerCh1_0.Location = new System.Drawing.Point(234, 25);
            this.tbRxPowerCh1_0.Name = "tbRxPowerCh1_0";
            this.tbRxPowerCh1_0.Size = new System.Drawing.Size(32, 23);
            this.tbRxPowerCh1_0.TabIndex = 69;
            // 
            // tbVenderSn
            // 
            this.tbVenderSn.Enabled = false;
            this.tbVenderSn.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbVenderSn.Location = new System.Drawing.Point(110, 100);
            this.tbVenderSn.Name = "tbVenderSn";
            this.tbVenderSn.Size = new System.Drawing.Size(130, 26);
            this.tbVenderSn.TabIndex = 65;
            this.tbVenderSn.Text = "YYMMDDRRSSSS";
            // 
            // lRxPowerCh1_0
            // 
            this.lRxPowerCh1_0.AutoSize = true;
            this.lRxPowerCh1_0.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRxPowerCh1_0.ForeColor = System.Drawing.Color.White;
            this.lRxPowerCh1_0.Location = new System.Drawing.Point(234, 13);
            this.lRxPowerCh1_0.Name = "lRxPowerCh1_0";
            this.lRxPowerCh1_0.Size = new System.Drawing.Size(30, 11);
            this.lRxPowerCh1_0.TabIndex = 68;
            this.lRxPowerCh1_0.Text = "ARxP1";
            // 
            // lDateCode
            // 
            this.lDateCode.AutoSize = true;
            this.lDateCode.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDateCode.ForeColor = System.Drawing.Color.White;
            this.lDateCode.Location = new System.Drawing.Point(11, 83);
            this.lDateCode.Name = "lDateCode";
            this.lDateCode.Size = new System.Drawing.Size(62, 13);
            this.lDateCode.TabIndex = 66;
            this.lDateCode.Text = "Date code:";
            // 
            // tbDateCode
            // 
            this.tbDateCode.Enabled = false;
            this.tbDateCode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbDateCode.Location = new System.Drawing.Point(13, 100);
            this.tbDateCode.Name = "tbDateCode";
            this.tbDateCode.Size = new System.Drawing.Size(90, 26);
            this.tbDateCode.TabIndex = 67;
            this.tbDateCode.Text = "YYMMDD";
            // 
            // cbRelinkCheck
            // 
            this.cbRelinkCheck.AutoSize = true;
            this.cbRelinkCheck.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbRelinkCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbRelinkCheck.Location = new System.Drawing.Point(8, 70);
            this.cbRelinkCheck.Name = "cbRelinkCheck";
            this.cbRelinkCheck.Size = new System.Drawing.Size(64, 15);
            this.cbRelinkCheck.TabIndex = 106;
            this.cbRelinkCheck.Text = "TimeCheck";
            this.cbRelinkCheck.UseVisualStyleBackColor = true;
            this.cbRelinkCheck.Visible = false;
            this.cbRelinkCheck.CheckedChanged += new System.EventHandler(this.cbRelinkCheck_CheckedChanged);
            // 
            // rbLogFileMode
            // 
            this.rbLogFileMode.AutoSize = true;
            this.rbLogFileMode.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.rbLogFileMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.rbLogFileMode.Location = new System.Drawing.Point(107, 43);
            this.rbLogFileMode.Name = "rbLogFileMode";
            this.rbLogFileMode.Size = new System.Drawing.Size(89, 15);
            this.rbLogFileMode.TabIndex = 101;
            this.rbLogFileMode.Text = "LogFileCom mode";
            this.rbLogFileMode.UseVisualStyleBackColor = true;
            this.rbLogFileMode.Visible = false;
            this.rbLogFileMode.CheckedChanged += new System.EventHandler(this.BarcodeMode_CheckedChanged);
            // 
            // rbOnlySN
            // 
            this.rbOnlySN.AutoSize = true;
            this.rbOnlySN.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.rbOnlySN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.rbOnlySN.Location = new System.Drawing.Point(107, 28);
            this.rbOnlySN.Name = "rbOnlySN";
            this.rbOnlySN.Size = new System.Drawing.Size(74, 15);
            this.rbOnlySN.TabIndex = 99;
            this.rbOnlySN.Text = "Only Write SN";
            this.rbOnlySN.UseVisualStyleBackColor = true;
            this.rbOnlySN.Visible = false;
            this.rbOnlySN.CheckedChanged += new System.EventHandler(this.BarcodeMode_CheckedChanged);
            // 
            // rbFullMode
            // 
            this.rbFullMode.AutoSize = true;
            this.rbFullMode.Checked = true;
            this.rbFullMode.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.rbFullMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.rbFullMode.Location = new System.Drawing.Point(107, 14);
            this.rbFullMode.Name = "rbFullMode";
            this.rbFullMode.Size = new System.Drawing.Size(59, 15);
            this.rbFullMode.TabIndex = 98;
            this.rbFullMode.TabStop = true;
            this.rbFullMode.Text = "Full mode";
            this.rbFullMode.UseVisualStyleBackColor = true;
            this.rbFullMode.Visible = false;
            this.rbFullMode.CheckedChanged += new System.EventHandler(this.BarcodeMode_CheckedChanged);
            // 
            // cbRegisterMapView
            // 
            this.cbRegisterMapView.AutoSize = true;
            this.cbRegisterMapView.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbRegisterMapView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbRegisterMapView.Location = new System.Drawing.Point(8, 33);
            this.cbRegisterMapView.Name = "cbRegisterMapView";
            this.cbRegisterMapView.Size = new System.Drawing.Size(90, 15);
            this.cbRegisterMapView.TabIndex = 95;
            this.cbRegisterMapView.Text = "Register map view";
            this.cbRegisterMapView.UseVisualStyleBackColor = true;
            this.cbRegisterMapView.CheckedChanged += new System.EventHandler(this.BarcodeMode_CheckedChanged);
            // 
            // cbBarcodeMode
            // 
            this.cbBarcodeMode.AutoSize = true;
            this.cbBarcodeMode.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbBarcodeMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbBarcodeMode.Location = new System.Drawing.Point(8, 15);
            this.cbBarcodeMode.Name = "cbBarcodeMode";
            this.cbBarcodeMode.Size = new System.Drawing.Size(77, 15);
            this.cbBarcodeMode.TabIndex = 94;
            this.cbBarcodeMode.Text = "Barcode mode";
            this.cbBarcodeMode.UseVisualStyleBackColor = true;
            this.cbBarcodeMode.CheckedChanged += new System.EventHandler(this.cbBarcodeMode_CheckedChanged);
            // 
            // bRelinkTest
            // 
            this.bRelinkTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bRelinkTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRelinkTest.FlatAppearance.BorderSize = 0;
            this.bRelinkTest.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRelinkTest.ForeColor = System.Drawing.Color.White;
            this.bRelinkTest.Location = new System.Drawing.Point(99, 13);
            this.bRelinkTest.Name = "bRelinkTest";
            this.bRelinkTest.Size = new System.Drawing.Size(38, 31);
            this.bRelinkTest.TabIndex = 105;
            this.bRelinkTest.Text = "Test";
            this.bRelinkTest.UseVisualStyleBackColor = false;
            this.bRelinkTest.Click += new System.EventHandler(this.bRelinkTest_Click);
            // 
            // tbRelinkCount
            // 
            this.tbRelinkCount.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRelinkCount.ForeColor = System.Drawing.Color.Black;
            this.tbRelinkCount.Location = new System.Drawing.Point(71, 26);
            this.tbRelinkCount.Name = "tbRelinkCount";
            this.tbRelinkCount.Size = new System.Drawing.Size(20, 18);
            this.tbRelinkCount.TabIndex = 102;
            this.tbRelinkCount.Text = "3";
            // 
            // tbIntervalTime
            // 
            this.tbIntervalTime.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIntervalTime.ForeColor = System.Drawing.Color.Black;
            this.tbIntervalTime.Location = new System.Drawing.Point(36, 26);
            this.tbIntervalTime.Name = "tbIntervalTime";
            this.tbIntervalTime.Size = new System.Drawing.Size(28, 18);
            this.tbIntervalTime.TabIndex = 90;
            this.tbIntervalTime.Text = "1000";
            // 
            // bLogFileComparison
            // 
            this.bLogFileComparison.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bLogFileComparison.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLogFileComparison.FlatAppearance.BorderSize = 0;
            this.bLogFileComparison.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bLogFileComparison.ForeColor = System.Drawing.Color.White;
            this.bLogFileComparison.Location = new System.Drawing.Point(20, 449);
            this.bLogFileComparison.Name = "bLogFileComparison";
            this.bLogFileComparison.Size = new System.Drawing.Size(165, 25);
            this.bLogFileComparison.TabIndex = 97;
            this.bLogFileComparison.Text = "LogFile comparison all page";
            this.bLogFileComparison.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLogFileComparison.UseVisualStyleBackColor = false;
            this.bLogFileComparison.Visible = false;
            this.bLogFileComparison.Click += new System.EventHandler(this.bLogFileComparison_Click);
            // 
            // bCfgFileComparison
            // 
            this.bCfgFileComparison.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bCfgFileComparison.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCfgFileComparison.FlatAppearance.BorderSize = 0;
            this.bCfgFileComparison.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCfgFileComparison.ForeColor = System.Drawing.Color.White;
            this.bCfgFileComparison.Location = new System.Drawing.Point(20, 423);
            this.bCfgFileComparison.Name = "bCfgFileComparison";
            this.bCfgFileComparison.Size = new System.Drawing.Size(165, 25);
            this.bCfgFileComparison.TabIndex = 89;
            this.bCfgFileComparison.Text = "CfgFile comparison 0x00,03";
            this.bCfgFileComparison.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCfgFileComparison.UseVisualStyleBackColor = false;
            this.bCfgFileComparison.Visible = false;
            this.bCfgFileComparison.Click += new System.EventHandler(this.bCfgFileComparison_Click);
            // 
            // lDataName
            // 
            this.lDataName.AutoSize = true;
            this.lDataName.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.lDataName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lDataName.Location = new System.Drawing.Point(91, 227);
            this.lDataName.Name = "lDataName";
            this.lDataName.Size = new System.Drawing.Size(8, 11);
            this.lDataName.TabIndex = 72;
            this.lDataName.Text = "_";
            // 
            // lPathData
            // 
            this.lPathData.AutoSize = true;
            this.lPathData.Font = new System.Drawing.Font("Nirmala UI", 8F, System.Drawing.FontStyle.Bold);
            this.lPathData.ForeColor = System.Drawing.Color.White;
            this.lPathData.Location = new System.Drawing.Point(21, 225);
            this.lPathData.Name = "lPathData";
            this.lPathData.Size = new System.Drawing.Size(64, 13);
            this.lPathData.TabIndex = 71;
            this.lPathData.Text = "DATAROM:";
            // 
            // tbVersionCodeCh1
            // 
            this.tbVersionCodeCh1.Enabled = false;
            this.tbVersionCodeCh1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVersionCodeCh1.ForeColor = System.Drawing.Color.Black;
            this.tbVersionCodeCh1.Location = new System.Drawing.Point(339, 149);
            this.tbVersionCodeCh1.Name = "tbVersionCodeCh1";
            this.tbVersionCodeCh1.Size = new System.Drawing.Size(70, 18);
            this.tbVersionCodeCh1.TabIndex = 73;
            // 
            // tbVersionCodeReNewCh1
            // 
            this.tbVersionCodeReNewCh1.Enabled = false;
            this.tbVersionCodeReNewCh1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVersionCodeReNewCh1.ForeColor = System.Drawing.Color.Black;
            this.tbVersionCodeReNewCh1.Location = new System.Drawing.Point(339, 169);
            this.tbVersionCodeReNewCh1.Name = "tbVersionCodeReNewCh1";
            this.tbVersionCodeReNewCh1.Size = new System.Drawing.Size(70, 18);
            this.tbVersionCodeReNewCh1.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(289, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 75;
            this.label1.Text = "Renew";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(269, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 77;
            this.label2.Text = "FW Version";
            // 
            // tbVersionCodeReNewCh2
            // 
            this.tbVersionCodeReNewCh2.Enabled = false;
            this.tbVersionCodeReNewCh2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVersionCodeReNewCh2.ForeColor = System.Drawing.Color.Black;
            this.tbVersionCodeReNewCh2.Location = new System.Drawing.Point(442, 169);
            this.tbVersionCodeReNewCh2.Name = "tbVersionCodeReNewCh2";
            this.tbVersionCodeReNewCh2.Size = new System.Drawing.Size(70, 18);
            this.tbVersionCodeReNewCh2.TabIndex = 79;
            this.tbVersionCodeReNewCh2.Visible = false;
            // 
            // tbVersionCodeCh2
            // 
            this.tbVersionCodeCh2.Enabled = false;
            this.tbVersionCodeCh2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVersionCodeCh2.ForeColor = System.Drawing.Color.Black;
            this.tbVersionCodeCh2.Location = new System.Drawing.Point(442, 149);
            this.tbVersionCodeCh2.Name = "tbVersionCodeCh2";
            this.tbVersionCodeCh2.Size = new System.Drawing.Size(70, 18);
            this.tbVersionCodeCh2.TabIndex = 78;
            this.tbVersionCodeCh2.Visible = false;
            // 
            // cbSecurityLock
            // 
            this.cbSecurityLock.AutoSize = true;
            this.cbSecurityLock.Checked = true;
            this.cbSecurityLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSecurityLock.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbSecurityLock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbSecurityLock.Location = new System.Drawing.Point(363, 0);
            this.cbSecurityLock.Name = "cbSecurityLock";
            this.cbSecurityLock.Size = new System.Drawing.Size(70, 15);
            this.cbSecurityLock.TabIndex = 80;
            this.cbSecurityLock.Text = "SecurityLock";
            this.cbSecurityLock.UseVisualStyleBackColor = true;
            this.cbSecurityLock.Visible = false;
            // 
            // bWriteFromFile
            // 
            this.bWriteFromFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bWriteFromFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bWriteFromFile.FlatAppearance.BorderSize = 0;
            this.bWriteFromFile.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWriteFromFile.ForeColor = System.Drawing.Color.White;
            this.bWriteFromFile.Location = new System.Drawing.Point(542, 474);
            this.bWriteFromFile.Name = "bWriteFromFile";
            this.bWriteFromFile.Size = new System.Drawing.Size(122, 25);
            this.bWriteFromFile.TabIndex = 82;
            this.bWriteFromFile.Text = "WriteFromFile_SAS3";
            this.bWriteFromFile.UseVisualStyleBackColor = false;
            this.bWriteFromFile.Visible = false;
            this.bWriteFromFile.Click += new System.EventHandler(this.bWriteFromFile_Click);
            // 
            // tbOrignalSNCh1
            // 
            this.tbOrignalSNCh1.BackColor = System.Drawing.Color.LightBlue;
            this.tbOrignalSNCh1.Enabled = false;
            this.tbOrignalSNCh1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrignalSNCh1.ForeColor = System.Drawing.Color.Black;
            this.tbOrignalSNCh1.Location = new System.Drawing.Point(339, 189);
            this.tbOrignalSNCh1.Name = "tbOrignalSNCh1";
            this.tbOrignalSNCh1.Size = new System.Drawing.Size(70, 18);
            this.tbOrignalSNCh1.TabIndex = 83;
            this.tbOrignalSNCh1.Visible = false;
            // 
            // tbReNewSNCh1
            // 
            this.tbReNewSNCh1.BackColor = System.Drawing.Color.LightBlue;
            this.tbReNewSNCh1.Enabled = false;
            this.tbReNewSNCh1.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReNewSNCh1.ForeColor = System.Drawing.Color.Black;
            this.tbReNewSNCh1.Location = new System.Drawing.Point(339, 209);
            this.tbReNewSNCh1.Name = "tbReNewSNCh1";
            this.tbReNewSNCh1.Size = new System.Drawing.Size(70, 18);
            this.tbReNewSNCh1.TabIndex = 84;
            this.tbReNewSNCh1.Visible = false;
            // 
            // lOriginalSN
            // 
            this.lOriginalSN.AutoSize = true;
            this.lOriginalSN.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lOriginalSN.ForeColor = System.Drawing.Color.White;
            this.lOriginalSN.Location = new System.Drawing.Point(260, 191);
            this.lOriginalSN.Name = "lOriginalSN";
            this.lOriginalSN.Size = new System.Drawing.Size(66, 12);
            this.lOriginalSN.TabIndex = 85;
            this.lOriginalSN.Text = "Original CSN";
            this.lOriginalSN.Visible = false;
            // 
            // lReNewSn
            // 
            this.lReNewSn.AutoSize = true;
            this.lReNewSn.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lReNewSn.ForeColor = System.Drawing.Color.White;
            this.lReNewSn.Location = new System.Drawing.Point(286, 211);
            this.lReNewSn.Name = "lReNewSn";
            this.lReNewSn.Size = new System.Drawing.Size(38, 12);
            this.lReNewSn.TabIndex = 86;
            this.lReNewSn.Text = "ReNew";
            this.lReNewSn.Visible = false;
            // 
            // tbReNewSNCh2
            // 
            this.tbReNewSNCh2.BackColor = System.Drawing.Color.LightBlue;
            this.tbReNewSNCh2.Enabled = false;
            this.tbReNewSNCh2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReNewSNCh2.ForeColor = System.Drawing.Color.Black;
            this.tbReNewSNCh2.Location = new System.Drawing.Point(442, 209);
            this.tbReNewSNCh2.Name = "tbReNewSNCh2";
            this.tbReNewSNCh2.Size = new System.Drawing.Size(70, 18);
            this.tbReNewSNCh2.TabIndex = 88;
            this.tbReNewSNCh2.Visible = false;
            // 
            // tbOrignalSNCh2
            // 
            this.tbOrignalSNCh2.BackColor = System.Drawing.Color.LightBlue;
            this.tbOrignalSNCh2.Enabled = false;
            this.tbOrignalSNCh2.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrignalSNCh2.ForeColor = System.Drawing.Color.Black;
            this.tbOrignalSNCh2.Location = new System.Drawing.Point(442, 189);
            this.tbOrignalSNCh2.Name = "tbOrignalSNCh2";
            this.tbOrignalSNCh2.Size = new System.Drawing.Size(70, 18);
            this.tbOrignalSNCh2.TabIndex = 87;
            this.tbOrignalSNCh2.Visible = false;
            // 
            // gbPrompt
            // 
            this.gbPrompt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.gbPrompt.Controls.Add(this.lStatus);
            this.gbPrompt.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbPrompt.Location = new System.Drawing.Point(192, 73);
            this.gbPrompt.Name = "gbPrompt";
            this.gbPrompt.Size = new System.Drawing.Size(128, 73);
            this.gbPrompt.TabIndex = 104;
            this.gbPrompt.TabStop = false;
            this.gbPrompt.Text = "Prompt";
            this.gbPrompt.Visible = false;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lStatus.ForeColor = System.Drawing.Color.Black;
            this.lStatus.Location = new System.Drawing.Point(2, 24);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(30, 28);
            this.lStatus.TabIndex = 99;
            this.lStatus.Text = "...";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lReNewTLSN
            // 
            this.lReNewTLSN.AutoSize = true;
            this.lReNewTLSN.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lReNewTLSN.ForeColor = System.Drawing.Color.White;
            this.lReNewTLSN.Location = new System.Drawing.Point(3, 60);
            this.lReNewTLSN.Name = "lReNewTLSN";
            this.lReNewTLSN.Size = new System.Drawing.Size(70, 12);
            this.lReNewTLSN.TabIndex = 103;
            this.lReNewTLSN.Text = "ReNew TL-SN";
            // 
            // tbOrignalTLSN
            // 
            this.tbOrignalTLSN.Enabled = false;
            this.tbOrignalTLSN.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbOrignalTLSN.ForeColor = System.Drawing.Color.Black;
            this.tbOrignalTLSN.Location = new System.Drawing.Point(5, 32);
            this.tbOrignalTLSN.Name = "tbOrignalTLSN";
            this.tbOrignalTLSN.Size = new System.Drawing.Size(117, 25);
            this.tbOrignalTLSN.TabIndex = 100;
            // 
            // lOriginalTLSN
            // 
            this.lOriginalTLSN.AutoSize = true;
            this.lOriginalTLSN.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lOriginalTLSN.ForeColor = System.Drawing.Color.White;
            this.lOriginalTLSN.Location = new System.Drawing.Point(3, 17);
            this.lOriginalTLSN.Name = "lOriginalTLSN";
            this.lOriginalTLSN.Size = new System.Drawing.Size(75, 12);
            this.lOriginalTLSN.TabIndex = 102;
            this.lOriginalTLSN.Text = "Original TL-SN";
            // 
            // tbReNewTLSN
            // 
            this.tbReNewTLSN.Enabled = false;
            this.tbReNewTLSN.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbReNewTLSN.ForeColor = System.Drawing.Color.Black;
            this.tbReNewTLSN.Location = new System.Drawing.Point(5, 76);
            this.tbReNewTLSN.Name = "tbReNewTLSN";
            this.tbReNewTLSN.Size = new System.Drawing.Size(117, 25);
            this.tbReNewTLSN.TabIndex = 101;
            // 
            // tbStartTime
            // 
            this.tbStartTime.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartTime.ForeColor = System.Drawing.Color.Black;
            this.tbStartTime.Location = new System.Drawing.Point(4, 26);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(28, 18);
            this.tbStartTime.TabIndex = 105;
            this.tbStartTime.Text = "10";
            // 
            // lBaseTime
            // 
            this.lBaseTime.AutoSize = true;
            this.lBaseTime.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.lBaseTime.ForeColor = System.Drawing.Color.White;
            this.lBaseTime.Location = new System.Drawing.Point(4, 13);
            this.lBaseTime.Name = "lBaseTime";
            this.lBaseTime.Size = new System.Drawing.Size(22, 11);
            this.lBaseTime.TabIndex = 106;
            this.lBaseTime.Text = "Base";
            // 
            // lIntervalTime
            // 
            this.lIntervalTime.AutoSize = true;
            this.lIntervalTime.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.lIntervalTime.ForeColor = System.Drawing.Color.White;
            this.lIntervalTime.Location = new System.Drawing.Point(35, 13);
            this.lIntervalTime.Name = "lIntervalTime";
            this.lIntervalTime.Size = new System.Drawing.Size(33, 11);
            this.lIntervalTime.TabIndex = 107;
            this.lIntervalTime.Text = "Interval";
            // 
            // lCount
            // 
            this.lCount.AutoSize = true;
            this.lCount.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.lCount.ForeColor = System.Drawing.Color.White;
            this.lCount.Location = new System.Drawing.Point(70, 13);
            this.lCount.Name = "lCount";
            this.lCount.Size = new System.Drawing.Size(28, 11);
            this.lCount.TabIndex = 108;
            this.lCount.Text = "Count";
            // 
            // tbHideKey
            // 
            this.tbHideKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.tbHideKey.Font = new System.Drawing.Font("Nirmala UI", 3F);
            this.tbHideKey.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbHideKey.Location = new System.Drawing.Point(518, 1);
            this.tbHideKey.Name = "tbHideKey";
            this.tbHideKey.ReadOnly = true;
            this.tbHideKey.Size = new System.Drawing.Size(13, 13);
            this.tbHideKey.TabIndex = 110;
            this.tbHideKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHideKey.Enter += new System.EventHandler(this.tbHideKey_MouseEnter);
            this.tbHideKey.Leave += new System.EventHandler(this.tbHideKey_MouseLeave);
            // 
            // gbReWriteSn
            // 
            this.gbReWriteSn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.gbReWriteSn.Controls.Add(this.tbCustomerSn);
            this.gbReWriteSn.Controls.Add(this.cbReParameter);
            this.gbReWriteSn.Controls.Add(this.bSearchNumber);
            this.gbReWriteSn.Controls.Add(this.cbAutoWirte);
            this.gbReWriteSn.Controls.Add(this.bWriteTruelightSn);
            this.gbReWriteSn.Controls.Add(this.lTruelightSn);
            this.gbReWriteSn.Controls.Add(this.lCustomerSn);
            this.gbReWriteSn.Controls.Add(this.tbTruelightSn);
            this.gbReWriteSn.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbReWriteSn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbReWriteSn.Location = new System.Drawing.Point(541, 1);
            this.gbReWriteSn.Name = "gbReWriteSn";
            this.gbReWriteSn.Size = new System.Drawing.Size(142, 166);
            this.gbReWriteSn.TabIndex = 105;
            this.gbReWriteSn.TabStop = false;
            this.gbReWriteSn.Text = "ReWrite TL-SN";
            // 
            // tbCustomerSn
            // 
            this.tbCustomerSn.Enabled = false;
            this.tbCustomerSn.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbCustomerSn.ForeColor = System.Drawing.Color.Black;
            this.tbCustomerSn.Location = new System.Drawing.Point(5, 29);
            this.tbCustomerSn.Name = "tbCustomerSn";
            this.tbCustomerSn.Size = new System.Drawing.Size(118, 25);
            this.tbCustomerSn.TabIndex = 116;
            // 
            // cbReParameter
            // 
            this.cbReParameter.AutoSize = true;
            this.cbReParameter.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.cbReParameter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbReParameter.Location = new System.Drawing.Point(3, 146);
            this.cbReParameter.Name = "cbReParameter";
            this.cbReParameter.Size = new System.Drawing.Size(126, 16);
            this.cbReParameter.TabIndex = 115;
            this.cbReParameter.Text = "ReParameter_0x80,81";
            this.cbReParameter.UseVisualStyleBackColor = true;
            this.cbReParameter.CheckedChanged += new System.EventHandler(this.cbReParameter_CheckedChanged);
            // 
            // bSearchNumber
            // 
            this.bSearchNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bSearchNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSearchNumber.Enabled = false;
            this.bSearchNumber.FlatAppearance.BorderSize = 0;
            this.bSearchNumber.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSearchNumber.ForeColor = System.Drawing.Color.White;
            this.bSearchNumber.Location = new System.Drawing.Point(68, 98);
            this.bSearchNumber.Name = "bSearchNumber";
            this.bSearchNumber.Size = new System.Drawing.Size(54, 25);
            this.bSearchNumber.TabIndex = 114;
            this.bSearchNumber.Text = "Search";
            this.bSearchNumber.UseVisualStyleBackColor = false;
            this.bSearchNumber.Click += new System.EventHandler(this.bTest_Click);
            // 
            // cbAutoWirte
            // 
            this.cbAutoWirte.AutoSize = true;
            this.cbAutoWirte.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.cbAutoWirte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbAutoWirte.Location = new System.Drawing.Point(3, 126);
            this.cbAutoWirte.Name = "cbAutoWirte";
            this.cbAutoWirte.Size = new System.Drawing.Size(57, 16);
            this.cbAutoWirte.TabIndex = 112;
            this.cbAutoWirte.Text = "AutoW";
            this.cbAutoWirte.UseVisualStyleBackColor = true;
            this.cbAutoWirte.CheckedChanged += new System.EventHandler(this.cbAutoWirte_CheckedChanged);
            // 
            // bWriteTruelightSn
            // 
            this.bWriteTruelightSn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bWriteTruelightSn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bWriteTruelightSn.FlatAppearance.BorderSize = 0;
            this.bWriteTruelightSn.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWriteTruelightSn.ForeColor = System.Drawing.Color.White;
            this.bWriteTruelightSn.Location = new System.Drawing.Point(4, 98);
            this.bWriteTruelightSn.Name = "bWriteTruelightSn";
            this.bWriteTruelightSn.Size = new System.Drawing.Size(54, 25);
            this.bWriteTruelightSn.TabIndex = 106;
            this.bWriteTruelightSn.Text = "Write";
            this.bWriteTruelightSn.UseVisualStyleBackColor = false;
            this.bWriteTruelightSn.Click += new System.EventHandler(this.bWriteTruelightSn_Click);
            // 
            // lTruelightSn
            // 
            this.lTruelightSn.AutoSize = true;
            this.lTruelightSn.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lTruelightSn.ForeColor = System.Drawing.Color.White;
            this.lTruelightSn.Location = new System.Drawing.Point(2, 55);
            this.lTruelightSn.Name = "lTruelightSn";
            this.lTruelightSn.Size = new System.Drawing.Size(64, 12);
            this.lTruelightSn.TabIndex = 103;
            this.lTruelightSn.Text = "Truelight SN";
            // 
            // lCustomerSn
            // 
            this.lCustomerSn.AutoSize = true;
            this.lCustomerSn.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.lCustomerSn.ForeColor = System.Drawing.Color.White;
            this.lCustomerSn.Location = new System.Drawing.Point(4, 14);
            this.lCustomerSn.Name = "lCustomerSn";
            this.lCustomerSn.Size = new System.Drawing.Size(69, 12);
            this.lCustomerSn.TabIndex = 102;
            this.lCustomerSn.Text = "Customer SN:";
            // 
            // tbTruelightSn
            // 
            this.tbTruelightSn.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbTruelightSn.ForeColor = System.Drawing.Color.Black;
            this.tbTruelightSn.Location = new System.Drawing.Point(4, 70);
            this.tbTruelightSn.Name = "tbTruelightSn";
            this.tbTruelightSn.Size = new System.Drawing.Size(118, 25);
            this.tbTruelightSn.TabIndex = 101;
            // 
            // bCheckSerialNumber
            // 
            this.bCheckSerialNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bCheckSerialNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCheckSerialNumber.FlatAppearance.BorderSize = 0;
            this.bCheckSerialNumber.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCheckSerialNumber.ForeColor = System.Drawing.Color.White;
            this.bCheckSerialNumber.Location = new System.Drawing.Point(120, 474);
            this.bCheckSerialNumber.Name = "bCheckSerialNumber";
            this.bCheckSerialNumber.Size = new System.Drawing.Size(82, 25);
            this.bCheckSerialNumber.TabIndex = 113;
            this.bCheckSerialNumber.Text = "SN. Check";
            this.bCheckSerialNumber.UseVisualStyleBackColor = false;
            this.bCheckSerialNumber.Click += new System.EventHandler(this.bInfCheck_Click);
            // 
            // bExportRegister
            // 
            this.bExportRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bExportRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bExportRegister.FlatAppearance.BorderSize = 0;
            this.bExportRegister.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExportRegister.ForeColor = System.Drawing.Color.White;
            this.bExportRegister.Location = new System.Drawing.Point(542, 449);
            this.bExportRegister.Name = "bExportRegister";
            this.bExportRegister.Size = new System.Drawing.Size(123, 25);
            this.bExportRegister.TabIndex = 111;
            this.bExportRegister.Text = "Export Register";
            this.bExportRegister.UseVisualStyleBackColor = false;
            this.bExportRegister.Click += new System.EventHandler(this.bExportRegister_Click);
            // 
            // gbTruelightSn
            // 
            this.gbTruelightSn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.gbTruelightSn.Controls.Add(this.tbOrignalTLSN);
            this.gbTruelightSn.Controls.Add(this.lReNewTLSN);
            this.gbTruelightSn.Controls.Add(this.tbReNewTLSN);
            this.gbTruelightSn.Controls.Add(this.lOriginalTLSN);
            this.gbTruelightSn.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbTruelightSn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbTruelightSn.Location = new System.Drawing.Point(541, 186);
            this.gbTruelightSn.Name = "gbTruelightSn";
            this.gbTruelightSn.Size = new System.Drawing.Size(142, 108);
            this.gbTruelightSn.TabIndex = 114;
            this.gbTruelightSn.TabStop = false;
            this.gbTruelightSn.Text = "DUT Truelight SN";
            // 
            // cbDeepCheck
            // 
            this.cbDeepCheck.AutoSize = true;
            this.cbDeepCheck.Font = new System.Drawing.Font("Nirmala UI", 7F, System.Drawing.FontStyle.Bold);
            this.cbDeepCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbDeepCheck.Location = new System.Drawing.Point(636, 296);
            this.cbDeepCheck.Name = "cbDeepCheck";
            this.cbDeepCheck.Size = new System.Drawing.Size(47, 16);
            this.cbDeepCheck.TabIndex = 117;
            this.cbDeepCheck.Text = "Deep";
            this.cbDeepCheck.UseVisualStyleBackColor = true;
            this.cbDeepCheck.Click += new System.EventHandler(this.cbDeepCheck_Click);
            // 
            // gbRelinkTest
            // 
            this.gbRelinkTest.Controls.Add(this.bRelinkTest);
            this.gbRelinkTest.Controls.Add(this.tbIntervalTime);
            this.gbRelinkTest.Controls.Add(this.tbRelinkCount);
            this.gbRelinkTest.Controls.Add(this.tbStartTime);
            this.gbRelinkTest.Controls.Add(this.lBaseTime);
            this.gbRelinkTest.Controls.Add(this.lIntervalTime);
            this.gbRelinkTest.Controls.Add(this.lCount);
            this.gbRelinkTest.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbRelinkTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbRelinkTest.Location = new System.Drawing.Point(542, 313);
            this.gbRelinkTest.Name = "gbRelinkTest";
            this.gbRelinkTest.Size = new System.Drawing.Size(141, 51);
            this.gbRelinkTest.TabIndex = 119;
            this.gbRelinkTest.TabStop = false;
            this.gbRelinkTest.Text = "RelinkTest";
            this.gbRelinkTest.Visible = false;
            // 
            // gbOptionsControl
            // 
            this.gbOptionsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.gbOptionsControl.Controls.Add(this.cbRelinkCheck);
            this.gbOptionsControl.Controls.Add(this.cbBarcodeMode);
            this.gbOptionsControl.Controls.Add(this.cbRegisterMapView);
            this.gbOptionsControl.Controls.Add(this.rbFullMode);
            this.gbOptionsControl.Controls.Add(this.rbOnlySN);
            this.gbOptionsControl.Controls.Add(this.rbLogFileMode);
            this.gbOptionsControl.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbOptionsControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.gbOptionsControl.Location = new System.Drawing.Point(316, 421);
            this.gbOptionsControl.Name = "gbOptionsControl";
            this.gbOptionsControl.Size = new System.Drawing.Size(213, 91);
            this.gbOptionsControl.TabIndex = 120;
            this.gbOptionsControl.TabStop = false;
            this.gbOptionsControl.Text = "Options control";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(542, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 26);
            this.button1.TabIndex = 121;
            this.button1.Text = "SsssTest";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbCfgCheckAfterFw
            // 
            this.cbCfgCheckAfterFw.AutoSize = true;
            this.cbCfgCheckAfterFw.Checked = true;
            this.cbCfgCheckAfterFw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCfgCheckAfterFw.Enabled = false;
            this.cbCfgCheckAfterFw.Font = new System.Drawing.Font("Nirmala UI", 6F, System.Drawing.FontStyle.Bold);
            this.cbCfgCheckAfterFw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(72)))), ((int)(((byte)(91)))));
            this.cbCfgCheckAfterFw.Location = new System.Drawing.Point(20, 128);
            this.cbCfgCheckAfterFw.Name = "cbCfgCheckAfterFw";
            this.cbCfgCheckAfterFw.Size = new System.Drawing.Size(124, 15);
            this.cbCfgCheckAfterFw.TabIndex = 121;
            this.cbCfgCheckAfterFw.Text = "CfgCheck after FW flashing";
            this.cbCfgCheckAfterFw.UseVisualStyleBackColor = true;
            this.cbCfgCheckAfterFw.Visible = false;
            // 
            // bHardwareValidation
            // 
            this.bHardwareValidation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bHardwareValidation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bHardwareValidation.FlatAppearance.BorderSize = 0;
            this.bHardwareValidation.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bHardwareValidation.ForeColor = System.Drawing.Color.White;
            this.bHardwareValidation.Location = new System.Drawing.Point(186, 449);
            this.bHardwareValidation.Name = "bHardwareValidation";
            this.bHardwareValidation.Size = new System.Drawing.Size(124, 26);
            this.bHardwareValidation.TabIndex = 122;
            this.bHardwareValidation.Text = "HardwareValidation";
            this.bHardwareValidation.UseVisualStyleBackColor = false;
            this.bHardwareValidation.Visible = false;
            this.bHardwareValidation.Click += new System.EventHandler(this.bHardwareValidation_Click);
            // 
            // bCurrentRegister
            // 
            this.bCurrentRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(138)))));
            this.bCurrentRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCurrentRegister.FlatAppearance.BorderSize = 0;
            this.bCurrentRegister.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCurrentRegister.ForeColor = System.Drawing.Color.White;
            this.bCurrentRegister.Location = new System.Drawing.Point(19, 474);
            this.bCurrentRegister.Name = "bCurrentRegister";
            this.bCurrentRegister.Size = new System.Drawing.Size(100, 25);
            this.bCurrentRegister.TabIndex = 123;
            this.bCurrentRegister.Text = "Current register";
            this.bCurrentRegister.UseVisualStyleBackColor = false;
            this.bCurrentRegister.Click += new System.EventHandler(this.bCurrentRegister_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            this.ClientSize = new System.Drawing.Size(684, 511);
            this.Controls.Add(this.bCurrentRegister);
            this.Controls.Add(this.bHardwareValidation);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbCfgCheckAfterFw);
            this.Controls.Add(this.gbOptionsControl);
            this.Controls.Add(this.gbRelinkTest);
            this.Controls.Add(this.bWriteFromFile);
            this.Controls.Add(this.cbDeepCheck);
            this.Controls.Add(this.gbTruelightSn);
            this.Controls.Add(this.bExportRegister);
            this.Controls.Add(this.gbReWriteSn);
            this.Controls.Add(this.bCheckSerialNumber);
            this.Controls.Add(this.gbPrompt);
            this.Controls.Add(this.tbHideKey);
            this.Controls.Add(this.bLogFileComparison);
            this.Controls.Add(this.tbReNewSNCh2);
            this.Controls.Add(this.tbOrignalSNCh2);
            this.Controls.Add(this.lReNewSn);
            this.Controls.Add(this.lOriginalSN);
            this.Controls.Add(this.bCfgFileComparison);
            this.Controls.Add(this.tbReNewSNCh1);
            this.Controls.Add(this.tbOrignalSNCh1);
            this.Controls.Add(this.cbSecurityLock);
            this.Controls.Add(this.tbVersionCodeReNewCh2);
            this.Controls.Add(this.tbVersionCodeCh2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVersionCodeReNewCh1);
            this.Controls.Add(this.tbVersionCodeCh1);
            this.Controls.Add(this.lDataName);
            this.Controls.Add(this.lPathData);
            this.Controls.Add(this.gbOperatorMode);
            this.Controls.Add(this.cbI2cConnect);
            this.Controls.Add(this.lApName);
            this.Controls.Add(this.lPathAP);
            this.Controls.Add(this.lMode);
            this.Controls.Add(this.lProduct);
            this.Controls.Add(this.lCh2EC);
            this.Controls.Add(this.lCh1EC);
            this.Controls.Add(this.lCh2Message);
            this.Controls.Add(this.lCh1Message);
            this.Controls.Add(this.cProgressBar2);
            this.Controls.Add(this.rbBoth);
            this.Controls.Add(this.rbSingle);
            this.Controls.Add(this.cProgressBar1);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.lFilePath);
            this.Controls.Add(this.lLogin);
            this.Controls.Add(this.lProductT);
            this.Controls.Add(this.lModeT);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbOperatorMode.ResumeLayout(false);
            this.gbOperatorMode.PerformLayout();
            this.gbCodeEditor.ResumeLayout(false);
            this.gbCodeEditor.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbPrompt.ResumeLayout(false);
            this.gbPrompt.PerformLayout();
            this.gbReWriteSn.ResumeLayout(false);
            this.gbReWriteSn.PerformLayout();
            this.gbTruelightSn.ResumeLayout(false);
            this.gbTruelightSn.PerformLayout();
            this.gbRelinkTest.ResumeLayout(false);
            this.gbRelinkTest.PerformLayout();
            this.gbOptionsControl.ResumeLayout(false);
            this.gbOptionsControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label lFilePath;
        private System.Windows.Forms.Label lLogin;
        private CircularProgressBar.CircularProgressBar cProgressBar1;
        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.RadioButton rbBoth;
        private CircularProgressBar.CircularProgressBar cProgressBar2;
        private System.Windows.Forms.Label lCh1Message;
        private System.Windows.Forms.Label lCh2Message;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lCh1EC;
        private System.Windows.Forms.Label lCh2EC;
        private System.Windows.Forms.Label lProduct;
        private System.Windows.Forms.Label lMode;
        private System.Windows.Forms.Label lModeT;
        private System.Windows.Forms.Label lProductT;
        private System.Windows.Forms.Label lPathAP;
        private System.Windows.Forms.Label lApName;
        private System.Windows.Forms.CheckBox cbBypassW;
        private System.Windows.Forms.CheckBox cbI2cConnect;
        private System.Windows.Forms.GroupBox gbOperatorMode;
        private System.Windows.Forms.Label lVenderSn;
        private System.Windows.Forms.TextBox tbRxPowerCh1_0;
        private System.Windows.Forms.TextBox tbVenderSn;
        private System.Windows.Forms.Label lRxPowerCh1_0;
        private System.Windows.Forms.Label lDateCode;
        private System.Windows.Forms.TextBox tbDateCode;
        private System.Windows.Forms.Label lRxPowerCh1_3;
        private System.Windows.Forms.Label lRxPowerCh1_2;
        private System.Windows.Forms.Label lRxPowerCh1_1;
        private System.Windows.Forms.TextBox tbRxPowerCh1_3;
        private System.Windows.Forms.TextBox tbRxPowerCh1_2;
        private System.Windows.Forms.TextBox tbRxPowerCh1_1;
        private System.Windows.Forms.TextBox tbLogFilePath;
        private System.Windows.Forms.Label lLogFilePath;
        private System.Windows.Forms.Label lDataName;
        private System.Windows.Forms.Label lPathData;
        private System.Windows.Forms.TextBox tbVersionCodeCh1;
        private System.Windows.Forms.TextBox tbVersionCodeReNewCh1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVersionCodeReNewCh2;
        private System.Windows.Forms.TextBox tbVersionCodeCh2;
        private System.Windows.Forms.CheckBox cbSecurityLock;
        private System.Windows.Forms.Button bWriteFromFile;
        private System.Windows.Forms.Button bWriteSnDateCone;
        private System.Windows.Forms.TextBox tbOrignalSNCh1;
        private System.Windows.Forms.TextBox tbReNewSNCh1;
        private System.Windows.Forms.Label lOriginalSN;
        private System.Windows.Forms.Label lReNewSn;
        private System.Windows.Forms.TextBox tbReNewSNCh2;
        private System.Windows.Forms.TextBox tbOrignalSNCh2;
        private System.Windows.Forms.Button bCfgFileComparison;
        private System.Windows.Forms.TextBox tbRxPowerCh2_3;
        private System.Windows.Forms.TextBox tbRxPowerCh2_2;
        private System.Windows.Forms.TextBox tbRxPowerCh2_1;
        private System.Windows.Forms.TextBox tbRxPowerCh2_0;
        private System.Windows.Forms.Label lRxPowerCh2_3;
        private System.Windows.Forms.Label lRxPowerCh2_2;
        private System.Windows.Forms.Label lRxPowerCh2_1;
        private System.Windows.Forms.Label lRxPowerCh2_0;
        private System.Windows.Forms.GroupBox gbCodeEditor;
        private System.Windows.Forms.CheckBox cbBarcodeMode;
        private System.Windows.Forms.Button bRenewRssi;
        private System.Windows.Forms.CheckBox cbRegisterMapView;
        private System.Windows.Forms.Button bLogFileComparison;
        private System.Windows.Forms.TextBox tbIntervalTime;
        private System.Windows.Forms.RadioButton rbOnlySN;
        private System.Windows.Forms.RadioButton rbFullMode;
        private System.Windows.Forms.RadioButton rbLogFileMode;
        private System.Windows.Forms.GroupBox gbPrompt;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.TextBox tbRelinkCount;
        private System.Windows.Forms.Button bRelinkTest;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.CheckBox cbRelinkCheck;
        private System.Windows.Forms.TextBox tbRssiCriteria;
        private System.Windows.Forms.Label lRxPowerCriteria;
        private System.Windows.Forms.CheckBox cbRxPowerNgInterrupt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.DomainUpDown dudYy;
        private System.Windows.Forms.DomainUpDown dudMm;
        private System.Windows.Forms.DomainUpDown dudDd;
        private System.Windows.Forms.DomainUpDown dudRr;
        private System.Windows.Forms.DomainUpDown dudSsss;
        private ComboBox cbSnNamingRule;
        private DomainUpDown dudWw;
        private DomainUpDown dudD;
        private DomainUpDown dudL;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lMm;
        private Label lDd;
        private Label lRr;
        private Label lSsss;
        private Label lWw;
        private Label lD;
        private Label lL;
        private DomainUpDown dudDd2;
        private DomainUpDown dudMm2;
        private Label lDd2;
        private Label lMm2;
        private Label lYy;
        private Label lBaseTime;
        private Label lIntervalTime;
        private Label lCount;
        private TextBox tbHideKey;
        private Label lReNewTLSN;
        private Label lOriginalTLSN;
        private TextBox tbReNewTLSN;
        private TextBox tbOrignalTLSN;
        private GroupBox gbReWriteSn;
        private Label lTruelightSn;
        private Label lCustomerSn;
        private TextBox tbTruelightSn;
        private CheckBox cbAutoWirte;
        private Button bWriteTruelightSn;
        private Button bCheckSerialNumber;
        private Button bSearchNumber;
        private CheckBox cbReParameter;
        private Button bExportRegister;
        private GroupBox gbTruelightSn;
        private CheckBox cbDeepCheck;
        private TextBox tbCustomerSn;
        private GroupBox gbRelinkTest;
        private GroupBox gbOptionsControl;
        private CheckBox cbLogCheckAfterSn;
        private Button button1;
        private CheckBox cbCfgCheckAfterFw;
        private Button bHardwareValidation;
        private Button bCurrentRegister;
        private Button bOpenLogFileFolder;
    }
}