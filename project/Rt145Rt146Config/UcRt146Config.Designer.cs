using System.Windows.Forms;

namespace Rt145Rt146Config
{
    partial class UcRt146Config
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
            this.bReadAll = new System.Windows.Forms.Button();
            this.bStoreIntoFlash = new System.Windows.Forms.Button();
            this.bDeviceReset = new System.Windows.Forms.Button();
            this.tpRt145Config = new System.Windows.Forms.TabControl();
            this.tpRt145Global = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbEqPeakCh3 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbEqPeakCh2 = new System.Windows.Forms.ComboBox();
            this.label142 = new System.Windows.Forms.Label();
            this.cbEqPeakCh1 = new System.Windows.Forms.ComboBox();
            this.label143 = new System.Windows.Forms.Label();
            this.cbEqPeakCh0 = new System.Windows.Forms.ComboBox();
            this.label144 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbFaultClearCh3 = new System.Windows.Forms.CheckBox();
            this.cbFaultClearCh1 = new System.Windows.Forms.CheckBox();
            this.cbFaultClearCh0 = new System.Windows.Forms.CheckBox();
            this.cbFaultClearCh2 = new System.Windows.Forms.CheckBox();
            this.cbFaultMaskCh3 = new System.Windows.Forms.CheckBox();
            this.cbFaultMaskCh1 = new System.Windows.Forms.CheckBox();
            this.cbFaultMaskCh0 = new System.Windows.Forms.CheckBox();
            this.cbFaultMaskCh2 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.cbSwitchBistVdet = new System.Windows.Forms.ComboBox();
            this.cbSwitchBistVref = new System.Windows.Forms.ComboBox();
            this.cbCdrLoopBandWidth = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label59 = new System.Windows.Forms.Label();
            this.cbMonitorClockEnableCh3 = new System.Windows.Forms.CheckBox();
            this.cbMonitorClockEnableCh1 = new System.Windows.Forms.CheckBox();
            this.cbMonitorClockEnableCh0 = new System.Windows.Forms.CheckBox();
            this.cbMonitorClockEnableCh2 = new System.Windows.Forms.CheckBox();
            this.label43 = new System.Windows.Forms.Label();
            this.cbAutoTuneUnlockTh = new System.Windows.Forms.ComboBox();
            this.cbAutoTuneLockTh = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.cbSelectImon = new System.Windows.Forms.ComboBox();
            this.cbModeImon = new System.Windows.Forms.ComboBox();
            this.cbSelectVdiop = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.cbIntrptPolarity = new System.Windows.Forms.ComboBox();
            this.cbIntrptType = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.cbLolModeCh3 = new System.Windows.Forms.CheckBox();
            this.cbLolModeCh1 = new System.Windows.Forms.CheckBox();
            this.cbLolModeCh0 = new System.Windows.Forms.CheckBox();
            this.cbLolModeCh2 = new System.Windows.Forms.CheckBox();
            this.cbLosModeCh3 = new System.Windows.Forms.CheckBox();
            this.cbLosModeCh1 = new System.Windows.Forms.CheckBox();
            this.cbLosModeCh0 = new System.Windows.Forms.CheckBox();
            this.cbLosModeCh2 = new System.Windows.Forms.CheckBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.cbLolClearCh3 = new System.Windows.Forms.CheckBox();
            this.cbLolClearCh1 = new System.Windows.Forms.CheckBox();
            this.cbLolClearCh0 = new System.Windows.Forms.CheckBox();
            this.cbLolClearCh2 = new System.Windows.Forms.CheckBox();
            this.cbLosClearCh3 = new System.Windows.Forms.CheckBox();
            this.cbLosClearCh1 = new System.Windows.Forms.CheckBox();
            this.cbLosClearCh0 = new System.Windows.Forms.CheckBox();
            this.cbLosClearCh2 = new System.Windows.Forms.CheckBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cbDeEmphasisCh3 = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cbDeEmphasisCh2 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbDeEmphasisCh1 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbDeEmphasisCh0 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.cbCrossPointCh3 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbCrossPointCh2 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbCrossPointCh1 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbCrossPointCh0 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cbMuteModeCh3 = new System.Windows.Forms.CheckBox();
            this.cbMuteModeCh1 = new System.Windows.Forms.CheckBox();
            this.cbMuteModeCh0 = new System.Windows.Forms.CheckBox();
            this.cbMuteModeCh2 = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbFaultModeCh3 = new System.Windows.Forms.CheckBox();
            this.cbFaultModeCh1 = new System.Windows.Forms.CheckBox();
            this.cbFaultModeCh0 = new System.Windows.Forms.CheckBox();
            this.cbFaultModeCh2 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLosHysteresisCh3 = new System.Windows.Forms.CheckBox();
            this.cbLosHysteresisCh1 = new System.Windows.Forms.CheckBox();
            this.cbLosHysteresisCh0 = new System.Windows.Forms.CheckBox();
            this.cbLosHysteresisCh2 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbLosThresholdCh3 = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.cbLosThresholdCh2 = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.cbLosThresholdCh1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLosThresholdCh0 = new System.Windows.Forms.ComboBox();
            this.lLosThresholdL0 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbLolMaskCh3 = new System.Windows.Forms.CheckBox();
            this.cbLolMaskCh1 = new System.Windows.Forms.CheckBox();
            this.cbLolMaskCh0 = new System.Windows.Forms.CheckBox();
            this.cbLolMaskCh2 = new System.Windows.Forms.CheckBox();
            this.cbLosMaskCh3 = new System.Windows.Forms.CheckBox();
            this.cbLosMaskCh1 = new System.Windows.Forms.CheckBox();
            this.cbLosMaskCh0 = new System.Windows.Forms.CheckBox();
            this.cbLosMaskCh2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTxPowerControlCh3 = new System.Windows.Forms.CheckBox();
            this.cbTxPowerControlCh1 = new System.Windows.Forms.CheckBox();
            this.cbTxPowerControlCh0 = new System.Windows.Forms.CheckBox();
            this.cbTxPowerControlCh2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTxCdrControlCh3 = new System.Windows.Forms.CheckBox();
            this.cbTxCdrControlCh1 = new System.Windows.Forms.CheckBox();
            this.cbTxCdrControlCh0 = new System.Windows.Forms.CheckBox();
            this.cbTxCdrControlCh2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbLOL = new System.Windows.Forms.GroupBox();
            this.cbInvertPolarityCH3 = new System.Windows.Forms.CheckBox();
            this.cbInvertPolarityCH1 = new System.Windows.Forms.CheckBox();
            this.cbInvertPolarityCH0 = new System.Windows.Forms.CheckBox();
            this.cbInvertPolarityCH2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpRt145Bychannel = new System.Windows.Forms.TabPage();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.cbVcoMsbSelecCh3 = new System.Windows.Forms.ComboBox();
            this.cbAutoBypassResetCh3 = new System.Windows.Forms.ComboBox();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.cbVcoMsbSelecCh2 = new System.Windows.Forms.ComboBox();
            this.cbAutoBypassResetCh2 = new System.Windows.Forms.ComboBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.cbVcoMsbSelecCh1 = new System.Windows.Forms.ComboBox();
            this.cbAutoBypassResetCh1 = new System.Windows.Forms.ComboBox();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.cbVcoMsbSelecCh0 = new System.Windows.Forms.ComboBox();
            this.cbAutoBypassResetCh0 = new System.Windows.Forms.ComboBox();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.tpRt145Control = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label97 = new System.Windows.Forms.Label();
            this.cbBurninEnCh3 = new System.Windows.Forms.CheckBox();
            this.cbBurninEnCh2 = new System.Windows.Forms.CheckBox();
            this.cbBurninEnCh1 = new System.Windows.Forms.CheckBox();
            this.label98 = new System.Windows.Forms.Label();
            this.cbBurninEnCh0 = new System.Windows.Forms.CheckBox();
            this.label99 = new System.Windows.Forms.Label();
            this.cbBurninCurrentCh3 = new System.Windows.Forms.ComboBox();
            this.cbBurninCurrentCh2 = new System.Windows.Forms.ComboBox();
            this.cbBurninCurrentCh1 = new System.Windows.Forms.ComboBox();
            this.cbBurninCurrentCh0 = new System.Windows.Forms.ComboBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label66 = new System.Windows.Forms.Label();
            this.cbModulationPowerDownCh3 = new System.Windows.Forms.CheckBox();
            this.cbModulationPowerDownCh2 = new System.Windows.Forms.CheckBox();
            this.cbModulationPowerDownCh1 = new System.Windows.Forms.CheckBox();
            this.label71 = new System.Windows.Forms.Label();
            this.cbModulationPowerDownCh0 = new System.Windows.Forms.CheckBox();
            this.label94 = new System.Windows.Forms.Label();
            this.cbModulationCh3 = new System.Windows.Forms.ComboBox();
            this.cbModulationCh2 = new System.Windows.Forms.ComboBox();
            this.cbModulationCh1 = new System.Windows.Forms.ComboBox();
            this.cbModulationCh0 = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label86 = new System.Windows.Forms.Label();
            this.cbIbiasPowerDownCh3 = new System.Windows.Forms.CheckBox();
            this.cbIbiasPowerDownCh2 = new System.Windows.Forms.CheckBox();
            this.cbIbiasPowerDownCh1 = new System.Windows.Forms.CheckBox();
            this.label85 = new System.Windows.Forms.Label();
            this.cbIbiasPowerDownCh0 = new System.Windows.Forms.CheckBox();
            this.label82 = new System.Windows.Forms.Label();
            this.cbIbiasCurrentCh3 = new System.Windows.Forms.ComboBox();
            this.cbIbiasCurrentCh2 = new System.Windows.Forms.ComboBox();
            this.cbIbiasCurrentCh1 = new System.Windows.Forms.ComboBox();
            this.cbIbiasCurrentCh0 = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbDeemphasisEnCh3 = new System.Windows.Forms.CheckBox();
            this.cbDeemphasisEnCh1 = new System.Windows.Forms.CheckBox();
            this.cbDeemphasisEnCh0 = new System.Windows.Forms.CheckBox();
            this.cbDeemphasisEnCh2 = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDdmiChannelSelect = new System.Windows.Forms.ComboBox();
            this.cbDdmiAdcPowerControl = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.label88 = new System.Windows.Forms.Label();
            this.cbAutoTuneClockSpeed = new System.Windows.Forms.ComboBox();
            this.label89 = new System.Windows.Forms.Label();
            this.cbSampleHoldClockSpeed = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.cbClockLosEnable = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.cbClockAdcEnable = new System.Windows.Forms.ComboBox();
            this.label92 = new System.Windows.Forms.Label();
            this.cbClockAutoTuneEnable = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.cbRingOscPwd = new System.Windows.Forms.ComboBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.cbAeqPwdCh3 = new System.Windows.Forms.CheckBox();
            this.cbAeqPwdCh1 = new System.Windows.Forms.CheckBox();
            this.cbAeqPwdCh0 = new System.Windows.Forms.CheckBox();
            this.cbAeqPwdCh2 = new System.Windows.Forms.CheckBox();
            this.label87 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.cbCdrPwdCh3 = new System.Windows.Forms.CheckBox();
            this.cbCdrPwdCh1 = new System.Windows.Forms.CheckBox();
            this.cbCdrPwdCh0 = new System.Windows.Forms.CheckBox();
            this.cbCdrPwdCh2 = new System.Windows.Forms.CheckBox();
            this.label80 = new System.Windows.Forms.Label();
            this.lCh0SahPwd = new System.Windows.Forms.Label();
            this.cbCh0SahPwd = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.cbIgnoreGlobalPwd = new System.Windows.Forms.ComboBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label76 = new System.Windows.Forms.Label();
            this.cbDdmiReset = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.cbEnableVf = new System.Windows.Forms.ComboBox();
            this.label78 = new System.Windows.Forms.Label();
            this.cbSourceIdoPwd = new System.Windows.Forms.ComboBox();
            this.label79 = new System.Windows.Forms.Label();
            this.cbTemperaturePwd = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.cbDdmiPwd = new System.Windows.Forms.ComboBox();
            this.label75 = new System.Windows.Forms.Label();
            this.cbTemperatureSlope = new System.Windows.Forms.ComboBox();
            this.label74 = new System.Windows.Forms.Label();
            this.cbTemperatureOffset = new System.Windows.Forms.ComboBox();
            this.label73 = new System.Windows.Forms.Label();
            this.cbIdoModeSelect = new System.Windows.Forms.ComboBox();
            this.label72 = new System.Windows.Forms.Label();
            this.cbDdmiIdoRefSource = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.cbRssiLevelSelect = new System.Windows.Forms.ComboBox();
            this.label68 = new System.Windows.Forms.Label();
            this.cbRssiMode = new System.Windows.Forms.ComboBox();
            this.label67 = new System.Windows.Forms.Label();
            this.cbRssiAgcClockSpeed = new System.Windows.Forms.ComboBox();
            this.tpRt145Customer = new System.Windows.Forms.TabPage();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.cbEqualizationR1Ch3 = new System.Windows.Forms.ComboBox();
            this.cbEqualizationR0Ch3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization10dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization9dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualizationR1Ch2 = new System.Windows.Forms.ComboBox();
            this.cbEqualizationR0Ch2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization10dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization9dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualizationR1Ch1 = new System.Windows.Forms.ComboBox();
            this.cbEqualizationR0Ch1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization10dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization9dbCh1 = new System.Windows.Forms.ComboBox();
            this.label115 = new System.Windows.Forms.Label();
            this.cbEqualizationR1Ch0 = new System.Windows.Forms.ComboBox();
            this.label116 = new System.Windows.Forms.Label();
            this.cbEqualizationR0Ch0 = new System.Windows.Forms.ComboBox();
            this.label117 = new System.Windows.Forms.Label();
            this.cbEqualization10dbCh0 = new System.Windows.Forms.ComboBox();
            this.label118 = new System.Windows.Forms.Label();
            this.cbEqualization9dbCh0 = new System.Windows.Forms.ComboBox();
            this.label114 = new System.Windows.Forms.Label();
            this.cbEqualization8dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization7dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization6dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization5dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization4dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization3dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization2dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization1dbCh3 = new System.Windows.Forms.ComboBox();
            this.cbEqualization0dbCh3 = new System.Windows.Forms.ComboBox();
            this.label113 = new System.Windows.Forms.Label();
            this.cbEqualization8dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization7dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization6dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization5dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization4dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization3dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization2dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization1dbCh2 = new System.Windows.Forms.ComboBox();
            this.cbEqualization0dbCh2 = new System.Windows.Forms.ComboBox();
            this.label112 = new System.Windows.Forms.Label();
            this.cbEqualization8dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization7dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization6dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization5dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization4dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization3dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization2dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization1dbCh1 = new System.Windows.Forms.ComboBox();
            this.cbEqualization0dbCh1 = new System.Windows.Forms.ComboBox();
            this.label111 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.cbEqualization8dbCh0 = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.cbEqualization7dbCh0 = new System.Windows.Forms.ComboBox();
            this.label104 = new System.Windows.Forms.Label();
            this.cbEqualization6dbCh0 = new System.Windows.Forms.ComboBox();
            this.label105 = new System.Windows.Forms.Label();
            this.cbEqualization5dbCh0 = new System.Windows.Forms.ComboBox();
            this.label106 = new System.Windows.Forms.Label();
            this.cbEqualization4dbCh0 = new System.Windows.Forms.ComboBox();
            this.label107 = new System.Windows.Forms.Label();
            this.cbEqualization3dbCh0 = new System.Windows.Forms.ComboBox();
            this.label108 = new System.Windows.Forms.Label();
            this.cbEqualization2dbCh0 = new System.Windows.Forms.ComboBox();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.cbEqualization1dbCh0 = new System.Windows.Forms.ComboBox();
            this.cbEqualization0dbCh0 = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tbLOSCh0 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.tbLOSCh1 = new System.Windows.Forms.TextBox();
            this.tbLOSCh2 = new System.Windows.Forms.TextBox();
            this.tbLOSCh3 = new System.Windows.Forms.TextBox();
            this.tbLOLCh0 = new System.Windows.Forms.TextBox();
            this.tbLOLCh1 = new System.Windows.Forms.TextBox();
            this.tbLOLCh2 = new System.Windows.Forms.TextBox();
            this.tbLOLCh3 = new System.Windows.Forms.TextBox();
            this.tbLOSorLOLCh0 = new System.Windows.Forms.TextBox();
            this.tbLOSorLOLCh1 = new System.Windows.Forms.TextBox();
            this.tbLOSorLOLCh2 = new System.Windows.Forms.TextBox();
            this.tbLOSorLOLCh3 = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.tbFaultCh3 = new System.Windows.Forms.TextBox();
            this.tbFaultCh2 = new System.Windows.Forms.TextBox();
            this.tbFaultCh1 = new System.Windows.Forms.TextBox();
            this.tbFaultCh0 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tbChipId = new System.Windows.Forms.TextBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.tbNlatLOLCh3 = new System.Windows.Forms.TextBox();
            this.tbNlatLOLCh2 = new System.Windows.Forms.TextBox();
            this.tbNlatLOLCh1 = new System.Windows.Forms.TextBox();
            this.tbNlatLOLCh0 = new System.Windows.Forms.TextBox();
            this.tbNlatLOSCh3 = new System.Windows.Forms.TextBox();
            this.tbNlatLOSCh2 = new System.Windows.Forms.TextBox();
            this.tbNlatLOSCh1 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.tbNlatLOSCh0 = new System.Windows.Forms.TextBox();
            this.label131 = new System.Windows.Forms.Label();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.cbAgcRssi = new System.Windows.Forms.ComboBox();
            this.label132 = new System.Windows.Forms.Label();
            this.cbAdcOut = new System.Windows.Forms.ComboBox();
            this.label133 = new System.Windows.Forms.Label();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.cbNlatFaultSTCh3 = new System.Windows.Forms.ComboBox();
            this.cbNlatFaultSTCh2 = new System.Windows.Forms.ComboBox();
            this.cbNlatFaultSTCh1 = new System.Windows.Forms.ComboBox();
            this.cbNlatFaultSTCh0 = new System.Windows.Forms.ComboBox();
            this.tbNlatFalutCh3 = new System.Windows.Forms.TextBox();
            this.tbNlatFalutCh2 = new System.Windows.Forms.TextBox();
            this.tbNlatFalutCh1 = new System.Windows.Forms.TextBox();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.tbNlatFalutCh0 = new System.Windows.Forms.TextBox();
            this.label141 = new System.Windows.Forms.Label();
            this.tpRt145Config.SuspendLayout();
            this.tpRt145Global.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbLOL.SuspendLayout();
            this.tpRt145Bychannel.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tpRt145Control.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tpRt145Customer.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.SuspendLayout();
            // 
            // bReadAll
            // 
            this.bReadAll.BackColor = System.Drawing.Color.SkyBlue;
            this.bReadAll.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bReadAll.Location = new System.Drawing.Point(262, 3);
            this.bReadAll.Name = "bReadAll";
            this.bReadAll.Size = new System.Drawing.Size(75, 27);
            this.bReadAll.TabIndex = 7;
            this.bReadAll.Text = "Read All";
            this.bReadAll.UseVisualStyleBackColor = false;
            this.bReadAll.Click += new System.EventHandler(this.bReadAll_Click);
            // 
            // bStoreIntoFlash
            // 
            this.bStoreIntoFlash.BackColor = System.Drawing.Color.SkyBlue;
            this.bStoreIntoFlash.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bStoreIntoFlash.Location = new System.Drawing.Point(128, 3);
            this.bStoreIntoFlash.Name = "bStoreIntoFlash";
            this.bStoreIntoFlash.Size = new System.Drawing.Size(128, 27);
            this.bStoreIntoFlash.TabIndex = 6;
            this.bStoreIntoFlash.Text = "Store Into Flash";
            this.bStoreIntoFlash.UseVisualStyleBackColor = false;
            this.bStoreIntoFlash.Click += new System.EventHandler(this.bStoreIntoFlash_Click);
            // 
            // bDeviceReset
            // 
            this.bDeviceReset.BackColor = System.Drawing.Color.SkyBlue;
            this.bDeviceReset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bDeviceReset.Location = new System.Drawing.Point(3, 3);
            this.bDeviceReset.Name = "bDeviceReset";
            this.bDeviceReset.Size = new System.Drawing.Size(119, 27);
            this.bDeviceReset.TabIndex = 5;
            this.bDeviceReset.Text = "Device Reset";
            this.bDeviceReset.UseVisualStyleBackColor = false;
            this.bDeviceReset.Click += new System.EventHandler(this.bDeviceReset_Click);
            // 
            // tpRt145Config
            // 
            this.tpRt145Config.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tpRt145Config.Controls.Add(this.tpRt145Global);
            this.tpRt145Config.Controls.Add(this.tpRt145Bychannel);
            this.tpRt145Config.Controls.Add(this.tpRt145Control);
            this.tpRt145Config.Controls.Add(this.tpRt145Customer);
            this.tpRt145Config.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tpRt145Config.Location = new System.Drawing.Point(3, 36);
            this.tpRt145Config.Name = "tpRt145Config";
            this.tpRt145Config.SelectedIndex = 0;
            this.tpRt145Config.Size = new System.Drawing.Size(940, 560);
            this.tpRt145Config.TabIndex = 8;
            // 
            // tpRt145Global
            // 
            this.tpRt145Global.Controls.Add(this.groupBox5);
            this.tpRt145Global.Controls.Add(this.groupBox3);
            this.tpRt145Global.Controls.Add(this.groupBox21);
            this.tpRt145Global.Controls.Add(this.groupBox20);
            this.tpRt145Global.Controls.Add(this.groupBox19);
            this.tpRt145Global.Controls.Add(this.groupBox17);
            this.tpRt145Global.Controls.Add(this.groupBox16);
            this.tpRt145Global.Controls.Add(this.groupBox12);
            this.tpRt145Global.Controls.Add(this.groupBox10);
            this.tpRt145Global.Controls.Add(this.groupBox9);
            this.tpRt145Global.Controls.Add(this.groupBox6);
            this.tpRt145Global.Controls.Add(this.groupBox4);
            this.tpRt145Global.Controls.Add(this.groupBox2);
            this.tpRt145Global.Controls.Add(this.groupBox1);
            this.tpRt145Global.Controls.Add(this.gbLOL);
            this.tpRt145Global.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tpRt145Global.Location = new System.Drawing.Point(4, 30);
            this.tpRt145Global.Name = "tpRt145Global";
            this.tpRt145Global.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt145Global.Size = new System.Drawing.Size(932, 526);
            this.tpRt145Global.TabIndex = 0;
            this.tpRt145Global.Text = "Global";
            this.tpRt145Global.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbEqPeakCh3);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.cbEqPeakCh2);
            this.groupBox5.Controls.Add(this.label142);
            this.groupBox5.Controls.Add(this.cbEqPeakCh1);
            this.groupBox5.Controls.Add(this.label143);
            this.groupBox5.Controls.Add(this.cbEqPeakCh0);
            this.groupBox5.Controls.Add(this.label144);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox5.Location = new System.Drawing.Point(465, 375);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 150);
            this.groupBox5.TabIndex = 57;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "R20/21_EQ peak control";
            // 
            // cbEqPeakCh3
            // 
            this.cbEqPeakCh3.FormattingEnabled = true;
            this.cbEqPeakCh3.Location = new System.Drawing.Point(76, 118);
            this.cbEqPeakCh3.Name = "cbEqPeakCh3";
            this.cbEqPeakCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqPeakCh3.TabIndex = 23;
            this.cbEqPeakCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqPeakCh3_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(17, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 18);
            this.label9.TabIndex = 22;
            this.label9.Text = "Ch3：";
            // 
            // cbEqPeakCh2
            // 
            this.cbEqPeakCh2.FormattingEnabled = true;
            this.cbEqPeakCh2.Location = new System.Drawing.Point(76, 87);
            this.cbEqPeakCh2.Name = "cbEqPeakCh2";
            this.cbEqPeakCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqPeakCh2.TabIndex = 21;
            this.cbEqPeakCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqPeakCh2_SelectedIndexChanged);
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.BackColor = System.Drawing.Color.Transparent;
            this.label142.Location = new System.Drawing.Point(17, 90);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(53, 18);
            this.label142.TabIndex = 20;
            this.label142.Text = "Ch2：";
            // 
            // cbEqPeakCh1
            // 
            this.cbEqPeakCh1.FormattingEnabled = true;
            this.cbEqPeakCh1.Location = new System.Drawing.Point(76, 56);
            this.cbEqPeakCh1.Name = "cbEqPeakCh1";
            this.cbEqPeakCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqPeakCh1.TabIndex = 19;
            this.cbEqPeakCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqPeakCh1_SelectedIndexChanged);
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.BackColor = System.Drawing.Color.Transparent;
            this.label143.Location = new System.Drawing.Point(17, 59);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(53, 18);
            this.label143.TabIndex = 18;
            this.label143.Text = "Ch1：";
            // 
            // cbEqPeakCh0
            // 
            this.cbEqPeakCh0.FormattingEnabled = true;
            this.cbEqPeakCh0.Location = new System.Drawing.Point(76, 25);
            this.cbEqPeakCh0.Name = "cbEqPeakCh0";
            this.cbEqPeakCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqPeakCh0.TabIndex = 17;
            this.cbEqPeakCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqPeakCh0_SelectedIndexChanged);
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.BackColor = System.Drawing.Color.Transparent;
            this.label144.Location = new System.Drawing.Point(17, 28);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(53, 18);
            this.label144.TabIndex = 16;
            this.label144.Text = "Ch0：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbFaultClearCh3);
            this.groupBox3.Controls.Add(this.cbFaultClearCh1);
            this.groupBox3.Controls.Add(this.cbFaultClearCh0);
            this.groupBox3.Controls.Add(this.cbFaultClearCh2);
            this.groupBox3.Controls.Add(this.cbFaultMaskCh3);
            this.groupBox3.Controls.Add(this.cbFaultMaskCh1);
            this.groupBox3.Controls.Add(this.cbFaultMaskCh0);
            this.groupBox3.Controls.Add(this.cbFaultMaskCh2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(465, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 103);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "R16_Fault control";
            // 
            // cbFaultClearCh3
            // 
            this.cbFaultClearCh3.AutoSize = true;
            this.cbFaultClearCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultClearCh3.Location = new System.Drawing.Point(171, 63);
            this.cbFaultClearCh3.Name = "cbFaultClearCh3";
            this.cbFaultClearCh3.Size = new System.Drawing.Size(56, 22);
            this.cbFaultClearCh3.TabIndex = 47;
            this.cbFaultClearCh3.Text = "Ch3";
            this.cbFaultClearCh3.UseVisualStyleBackColor = true;
            this.cbFaultClearCh3.CheckedChanged += new System.EventHandler(this.cbFaultClearCh3_CheckedChanged);
            // 
            // cbFaultClearCh1
            // 
            this.cbFaultClearCh1.AutoSize = true;
            this.cbFaultClearCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultClearCh1.Location = new System.Drawing.Point(59, 63);
            this.cbFaultClearCh1.Name = "cbFaultClearCh1";
            this.cbFaultClearCh1.Size = new System.Drawing.Size(56, 22);
            this.cbFaultClearCh1.TabIndex = 45;
            this.cbFaultClearCh1.Text = "Ch1";
            this.cbFaultClearCh1.UseVisualStyleBackColor = true;
            this.cbFaultClearCh1.CheckedChanged += new System.EventHandler(this.cbFaultClearCh1_CheckedChanged);
            // 
            // cbFaultClearCh0
            // 
            this.cbFaultClearCh0.AutoSize = true;
            this.cbFaultClearCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultClearCh0.Location = new System.Drawing.Point(3, 63);
            this.cbFaultClearCh0.Name = "cbFaultClearCh0";
            this.cbFaultClearCh0.Size = new System.Drawing.Size(56, 22);
            this.cbFaultClearCh0.TabIndex = 44;
            this.cbFaultClearCh0.Text = "Ch0";
            this.cbFaultClearCh0.UseVisualStyleBackColor = true;
            this.cbFaultClearCh0.CheckedChanged += new System.EventHandler(this.cbFaultClearCh0_CheckedChanged);
            // 
            // cbFaultClearCh2
            // 
            this.cbFaultClearCh2.AutoSize = true;
            this.cbFaultClearCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultClearCh2.Location = new System.Drawing.Point(115, 63);
            this.cbFaultClearCh2.Name = "cbFaultClearCh2";
            this.cbFaultClearCh2.Size = new System.Drawing.Size(56, 22);
            this.cbFaultClearCh2.TabIndex = 46;
            this.cbFaultClearCh2.Text = "Ch2";
            this.cbFaultClearCh2.UseVisualStyleBackColor = true;
            this.cbFaultClearCh2.CheckedChanged += new System.EventHandler(this.cbFaultClearCh2_CheckedChanged);
            // 
            // cbFaultMaskCh3
            // 
            this.cbFaultMaskCh3.AutoSize = true;
            this.cbFaultMaskCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultMaskCh3.Location = new System.Drawing.Point(171, 23);
            this.cbFaultMaskCh3.Name = "cbFaultMaskCh3";
            this.cbFaultMaskCh3.Size = new System.Drawing.Size(56, 22);
            this.cbFaultMaskCh3.TabIndex = 43;
            this.cbFaultMaskCh3.Text = "Ch3";
            this.cbFaultMaskCh3.UseVisualStyleBackColor = true;
            this.cbFaultMaskCh3.CheckedChanged += new System.EventHandler(this.cbFaultMaskCh3_CheckedChanged);
            // 
            // cbFaultMaskCh1
            // 
            this.cbFaultMaskCh1.AutoSize = true;
            this.cbFaultMaskCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultMaskCh1.Location = new System.Drawing.Point(59, 23);
            this.cbFaultMaskCh1.Name = "cbFaultMaskCh1";
            this.cbFaultMaskCh1.Size = new System.Drawing.Size(56, 22);
            this.cbFaultMaskCh1.TabIndex = 41;
            this.cbFaultMaskCh1.Text = "Ch1";
            this.cbFaultMaskCh1.UseVisualStyleBackColor = true;
            this.cbFaultMaskCh1.CheckedChanged += new System.EventHandler(this.cbFaultMaskCh1_CheckedChanged);
            // 
            // cbFaultMaskCh0
            // 
            this.cbFaultMaskCh0.AutoSize = true;
            this.cbFaultMaskCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultMaskCh0.Location = new System.Drawing.Point(3, 23);
            this.cbFaultMaskCh0.Name = "cbFaultMaskCh0";
            this.cbFaultMaskCh0.Size = new System.Drawing.Size(56, 22);
            this.cbFaultMaskCh0.TabIndex = 40;
            this.cbFaultMaskCh0.Text = "Ch0";
            this.cbFaultMaskCh0.UseVisualStyleBackColor = true;
            this.cbFaultMaskCh0.CheckedChanged += new System.EventHandler(this.cbFaultMaskCh0_CheckedChanged);
            // 
            // cbFaultMaskCh2
            // 
            this.cbFaultMaskCh2.AutoSize = true;
            this.cbFaultMaskCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultMaskCh2.Location = new System.Drawing.Point(115, 23);
            this.cbFaultMaskCh2.Name = "cbFaultMaskCh2";
            this.cbFaultMaskCh2.Size = new System.Drawing.Size(56, 22);
            this.cbFaultMaskCh2.TabIndex = 42;
            this.cbFaultMaskCh2.Text = "Ch2";
            this.cbFaultMaskCh2.UseVisualStyleBackColor = true;
            this.cbFaultMaskCh2.CheckedChanged += new System.EventHandler(this.cbFaultMaskCh2_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(3, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "False / True：normal / clear";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label54.Location = new System.Drawing.Point(3, 43);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(163, 15);
            this.label54.TabIndex = 22;
            this.label54.Text = "False / True：normal / mask";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.cbSwitchBistVdet);
            this.groupBox21.Controls.Add(this.cbSwitchBistVref);
            this.groupBox21.Controls.Add(this.cbCdrLoopBandWidth);
            this.groupBox21.Controls.Add(this.label63);
            this.groupBox21.Controls.Add(this.label60);
            this.groupBox21.Controls.Add(this.label62);
            this.groupBox21.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox21.Location = new System.Drawing.Point(5, 337);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(220, 115);
            this.groupBox21.TabIndex = 53;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "R10_CDR control";
            // 
            // cbSwitchBistVdet
            // 
            this.cbSwitchBistVdet.FormattingEnabled = true;
            this.cbSwitchBistVdet.Location = new System.Drawing.Point(115, 82);
            this.cbSwitchBistVdet.Name = "cbSwitchBistVdet";
            this.cbSwitchBistVdet.Size = new System.Drawing.Size(100, 26);
            this.cbSwitchBistVdet.TabIndex = 27;
            this.cbSwitchBistVdet.SelectedIndexChanged += new System.EventHandler(this.cbSwitchBistVdet_SelectedIndexChanged);
            // 
            // cbSwitchBistVref
            // 
            this.cbSwitchBistVref.FormattingEnabled = true;
            this.cbSwitchBistVref.Location = new System.Drawing.Point(115, 50);
            this.cbSwitchBistVref.Name = "cbSwitchBistVref";
            this.cbSwitchBistVref.Size = new System.Drawing.Size(100, 26);
            this.cbSwitchBistVref.TabIndex = 24;
            this.cbSwitchBistVref.SelectedIndexChanged += new System.EventHandler(this.cbSwitchBistVref_SelectedIndexChanged);
            // 
            // cbCdrLoopBandWidth
            // 
            this.cbCdrLoopBandWidth.FormattingEnabled = true;
            this.cbCdrLoopBandWidth.Location = new System.Drawing.Point(115, 18);
            this.cbCdrLoopBandWidth.Name = "cbCdrLoopBandWidth";
            this.cbCdrLoopBandWidth.Size = new System.Drawing.Size(100, 26);
            this.cbCdrLoopBandWidth.TabIndex = 23;
            this.cbCdrLoopBandWidth.SelectedIndexChanged += new System.EventHandler(this.cbCdrLoopBandWidth_SelectedIndexChanged);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(3, 22);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(126, 18);
            this.label63.TabIndex = 25;
            this.label63.Text = "CDR loop BW.：";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Location = new System.Drawing.Point(3, 87);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(145, 18);
            this.label60.TabIndex = 28;
            this.label60.Text = "Switch BIST Vdet：";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Location = new System.Drawing.Point(3, 54);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(140, 18);
            this.label62.TabIndex = 26;
            this.label62.Text = "Switch BIST Vref：";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label59);
            this.groupBox20.Controls.Add(this.cbMonitorClockEnableCh3);
            this.groupBox20.Controls.Add(this.cbMonitorClockEnableCh1);
            this.groupBox20.Controls.Add(this.cbMonitorClockEnableCh0);
            this.groupBox20.Controls.Add(this.cbMonitorClockEnableCh2);
            this.groupBox20.Controls.Add(this.label43);
            this.groupBox20.Controls.Add(this.cbAutoTuneUnlockTh);
            this.groupBox20.Controls.Add(this.cbAutoTuneLockTh);
            this.groupBox20.Controls.Add(this.label57);
            this.groupBox20.Controls.Add(this.label58);
            this.groupBox20.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox20.Location = new System.Drawing.Point(5, 184);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(220, 147);
            this.groupBox20.TabIndex = 52;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "R9_Monitors-2";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Location = new System.Drawing.Point(0, 87);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(167, 18);
            this.label59.TabIndex = 37;
            this.label59.Text = "Monitor clock enable：";
            // 
            // cbMonitorClockEnableCh3
            // 
            this.cbMonitorClockEnableCh3.AutoSize = true;
            this.cbMonitorClockEnableCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMonitorClockEnableCh3.Location = new System.Drawing.Point(171, 107);
            this.cbMonitorClockEnableCh3.Name = "cbMonitorClockEnableCh3";
            this.cbMonitorClockEnableCh3.Size = new System.Drawing.Size(56, 22);
            this.cbMonitorClockEnableCh3.TabIndex = 36;
            this.cbMonitorClockEnableCh3.Text = "Ch3";
            this.cbMonitorClockEnableCh3.UseVisualStyleBackColor = true;
            this.cbMonitorClockEnableCh3.CheckedChanged += new System.EventHandler(this.cbMonitorClockEnableCh3_CheckedChanged);
            // 
            // cbMonitorClockEnableCh1
            // 
            this.cbMonitorClockEnableCh1.AutoSize = true;
            this.cbMonitorClockEnableCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMonitorClockEnableCh1.Location = new System.Drawing.Point(59, 107);
            this.cbMonitorClockEnableCh1.Name = "cbMonitorClockEnableCh1";
            this.cbMonitorClockEnableCh1.Size = new System.Drawing.Size(56, 22);
            this.cbMonitorClockEnableCh1.TabIndex = 34;
            this.cbMonitorClockEnableCh1.Text = "Ch1";
            this.cbMonitorClockEnableCh1.UseVisualStyleBackColor = true;
            this.cbMonitorClockEnableCh1.CheckedChanged += new System.EventHandler(this.cbMonitorClockEnableCh1_CheckedChanged);
            // 
            // cbMonitorClockEnableCh0
            // 
            this.cbMonitorClockEnableCh0.AutoSize = true;
            this.cbMonitorClockEnableCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMonitorClockEnableCh0.Location = new System.Drawing.Point(3, 107);
            this.cbMonitorClockEnableCh0.Name = "cbMonitorClockEnableCh0";
            this.cbMonitorClockEnableCh0.Size = new System.Drawing.Size(56, 22);
            this.cbMonitorClockEnableCh0.TabIndex = 33;
            this.cbMonitorClockEnableCh0.Text = "Ch0";
            this.cbMonitorClockEnableCh0.UseVisualStyleBackColor = true;
            this.cbMonitorClockEnableCh0.CheckedChanged += new System.EventHandler(this.cbMonitorClockEnableCh0_CheckedChanged);
            // 
            // cbMonitorClockEnableCh2
            // 
            this.cbMonitorClockEnableCh2.AutoSize = true;
            this.cbMonitorClockEnableCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMonitorClockEnableCh2.Location = new System.Drawing.Point(115, 107);
            this.cbMonitorClockEnableCh2.Name = "cbMonitorClockEnableCh2";
            this.cbMonitorClockEnableCh2.Size = new System.Drawing.Size(56, 22);
            this.cbMonitorClockEnableCh2.TabIndex = 35;
            this.cbMonitorClockEnableCh2.Text = "Ch2";
            this.cbMonitorClockEnableCh2.UseVisualStyleBackColor = true;
            this.cbMonitorClockEnableCh2.CheckedChanged += new System.EventHandler(this.cbMonitorClockEnableCh2_CheckedChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label43.Location = new System.Drawing.Point(3, 127);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(120, 15);
            this.label43.TabIndex = 32;
            this.label43.Text = "False / True：off / on";
            // 
            // cbAutoTuneUnlockTh
            // 
            this.cbAutoTuneUnlockTh.FormattingEnabled = true;
            this.cbAutoTuneUnlockTh.Location = new System.Drawing.Point(115, 50);
            this.cbAutoTuneUnlockTh.Name = "cbAutoTuneUnlockTh";
            this.cbAutoTuneUnlockTh.Size = new System.Drawing.Size(100, 26);
            this.cbAutoTuneUnlockTh.TabIndex = 24;
            this.cbAutoTuneUnlockTh.SelectedIndexChanged += new System.EventHandler(this.cbAutoTuneUnlockTh_SelectedIndexChanged);
            // 
            // cbAutoTuneLockTh
            // 
            this.cbAutoTuneLockTh.FormattingEnabled = true;
            this.cbAutoTuneLockTh.Location = new System.Drawing.Point(115, 18);
            this.cbAutoTuneLockTh.Name = "cbAutoTuneLockTh";
            this.cbAutoTuneLockTh.Size = new System.Drawing.Size(100, 26);
            this.cbAutoTuneLockTh.TabIndex = 23;
            this.cbAutoTuneLockTh.SelectedIndexChanged += new System.EventHandler(this.cbAutoTuneLockTh_SelectedIndexChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Location = new System.Drawing.Point(3, 54);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(153, 18);
            this.label57.TabIndex = 26;
            this.label57.Text = "AutoTune unlock th：";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Location = new System.Drawing.Point(3, 22);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(137, 18);
            this.label58.TabIndex = 25;
            this.label58.Text = "AutoTune lock th：";
            // 
            // groupBox19
            // 
            this.groupBox19.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox19.Controls.Add(this.label56);
            this.groupBox19.Controls.Add(this.label55);
            this.groupBox19.Controls.Add(this.cbSelectImon);
            this.groupBox19.Controls.Add(this.cbModeImon);
            this.groupBox19.Controls.Add(this.cbSelectVdiop);
            this.groupBox19.Controls.Add(this.label45);
            this.groupBox19.Controls.Add(this.label42);
            this.groupBox19.Controls.Add(this.cbIntrptPolarity);
            this.groupBox19.Controls.Add(this.cbIntrptType);
            this.groupBox19.Controls.Add(this.label44);
            this.groupBox19.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox19.Location = new System.Drawing.Point(5, 5);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(220, 179);
            this.groupBox19.TabIndex = 51;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "R8_Monitors-1";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Location = new System.Drawing.Point(3, 150);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(111, 18);
            this.label56.TabIndex = 29;
            this.label56.Text = "Select IMON：";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Location = new System.Drawing.Point(3, 119);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(107, 18);
            this.label55.TabIndex = 28;
            this.label55.Text = "Mode IMON：";
            // 
            // cbSelectImon
            // 
            this.cbSelectImon.FormattingEnabled = true;
            this.cbSelectImon.Location = new System.Drawing.Point(115, 147);
            this.cbSelectImon.Name = "cbSelectImon";
            this.cbSelectImon.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSelectImon.Size = new System.Drawing.Size(100, 26);
            this.cbSelectImon.TabIndex = 27;
            this.cbSelectImon.SelectedIndexChanged += new System.EventHandler(this.cbSelectImon_SelectedIndexChanged);
            // 
            // cbModeImon
            // 
            this.cbModeImon.FormattingEnabled = true;
            this.cbModeImon.Location = new System.Drawing.Point(115, 115);
            this.cbModeImon.Name = "cbModeImon";
            this.cbModeImon.Size = new System.Drawing.Size(100, 26);
            this.cbModeImon.TabIndex = 26;
            this.cbModeImon.SelectedIndexChanged += new System.EventHandler(this.cbModeImon_SelectedIndexChanged);
            // 
            // cbSelectVdiop
            // 
            this.cbSelectVdiop.FormattingEnabled = true;
            this.cbSelectVdiop.Location = new System.Drawing.Point(115, 83);
            this.cbSelectVdiop.Name = "cbSelectVdiop";
            this.cbSelectVdiop.Size = new System.Drawing.Size(100, 26);
            this.cbSelectVdiop.TabIndex = 24;
            this.cbSelectVdiop.SelectedIndexChanged += new System.EventHandler(this.cbSelectVdiop_SelectedIndexChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Location = new System.Drawing.Point(3, 88);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(121, 18);
            this.label45.TabIndex = 22;
            this.label45.Text = "Select VDIOP：";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Location = new System.Drawing.Point(3, 23);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(111, 18);
            this.label42.TabIndex = 20;
            this.label42.Text = "INTRPT tpye：";
            // 
            // cbIntrptPolarity
            // 
            this.cbIntrptPolarity.FormattingEnabled = true;
            this.cbIntrptPolarity.Location = new System.Drawing.Point(115, 51);
            this.cbIntrptPolarity.Name = "cbIntrptPolarity";
            this.cbIntrptPolarity.Size = new System.Drawing.Size(100, 26);
            this.cbIntrptPolarity.TabIndex = 19;
            this.cbIntrptPolarity.SelectedIndexChanged += new System.EventHandler(this.cbIntrptPolarity_SelectedIndexChanged);
            // 
            // cbIntrptType
            // 
            this.cbIntrptType.FormattingEnabled = true;
            this.cbIntrptType.Location = new System.Drawing.Point(115, 19);
            this.cbIntrptType.Name = "cbIntrptType";
            this.cbIntrptType.Size = new System.Drawing.Size(100, 26);
            this.cbIntrptType.TabIndex = 18;
            this.cbIntrptType.SelectedIndexChanged += new System.EventHandler(this.cbIntrptType_SelectedIndexChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Location = new System.Drawing.Point(3, 55);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(132, 18);
            this.label44.TabIndex = 21;
            this.label44.Text = "INTRPT polarity：";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.cbLolModeCh3);
            this.groupBox17.Controls.Add(this.cbLolModeCh1);
            this.groupBox17.Controls.Add(this.cbLolModeCh0);
            this.groupBox17.Controls.Add(this.cbLolModeCh2);
            this.groupBox17.Controls.Add(this.cbLosModeCh3);
            this.groupBox17.Controls.Add(this.cbLosModeCh1);
            this.groupBox17.Controls.Add(this.cbLosModeCh0);
            this.groupBox17.Controls.Add(this.cbLosModeCh2);
            this.groupBox17.Controls.Add(this.label40);
            this.groupBox17.Controls.Add(this.label41);
            this.groupBox17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox17.Location = new System.Drawing.Point(230, 417);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(230, 108);
            this.groupBox17.TabIndex = 50;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "R15_LOS / LOL mode control";
            // 
            // cbLolModeCh3
            // 
            this.cbLolModeCh3.AutoSize = true;
            this.cbLolModeCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolModeCh3.Location = new System.Drawing.Point(171, 68);
            this.cbLolModeCh3.Name = "cbLolModeCh3";
            this.cbLolModeCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLolModeCh3.TabIndex = 47;
            this.cbLolModeCh3.Text = "Ch3";
            this.cbLolModeCh3.UseVisualStyleBackColor = true;
            this.cbLolModeCh3.CheckedChanged += new System.EventHandler(this.cbLolModeCh3_CheckedChanged);
            // 
            // cbLolModeCh1
            // 
            this.cbLolModeCh1.AutoSize = true;
            this.cbLolModeCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolModeCh1.Location = new System.Drawing.Point(59, 68);
            this.cbLolModeCh1.Name = "cbLolModeCh1";
            this.cbLolModeCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLolModeCh1.TabIndex = 45;
            this.cbLolModeCh1.Text = "Ch1";
            this.cbLolModeCh1.UseVisualStyleBackColor = true;
            this.cbLolModeCh1.CheckedChanged += new System.EventHandler(this.cbLolModeCh1_CheckedChanged);
            // 
            // cbLolModeCh0
            // 
            this.cbLolModeCh0.AutoSize = true;
            this.cbLolModeCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolModeCh0.Location = new System.Drawing.Point(3, 68);
            this.cbLolModeCh0.Name = "cbLolModeCh0";
            this.cbLolModeCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLolModeCh0.TabIndex = 44;
            this.cbLolModeCh0.Text = "Ch0";
            this.cbLolModeCh0.UseVisualStyleBackColor = true;
            this.cbLolModeCh0.CheckedChanged += new System.EventHandler(this.cbLolModeCh0_CheckedChanged);
            // 
            // cbLolModeCh2
            // 
            this.cbLolModeCh2.AutoSize = true;
            this.cbLolModeCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolModeCh2.Location = new System.Drawing.Point(115, 68);
            this.cbLolModeCh2.Name = "cbLolModeCh2";
            this.cbLolModeCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLolModeCh2.TabIndex = 46;
            this.cbLolModeCh2.Text = "Ch2";
            this.cbLolModeCh2.UseVisualStyleBackColor = true;
            this.cbLolModeCh2.CheckedChanged += new System.EventHandler(this.cbLolModeCh2_CheckedChanged);
            // 
            // cbLosModeCh3
            // 
            this.cbLosModeCh3.AutoSize = true;
            this.cbLosModeCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosModeCh3.Location = new System.Drawing.Point(171, 23);
            this.cbLosModeCh3.Name = "cbLosModeCh3";
            this.cbLosModeCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLosModeCh3.TabIndex = 43;
            this.cbLosModeCh3.Text = "Ch3";
            this.cbLosModeCh3.UseVisualStyleBackColor = true;
            this.cbLosModeCh3.CheckedChanged += new System.EventHandler(this.cbLosModeCh3_CheckedChanged);
            // 
            // cbLosModeCh1
            // 
            this.cbLosModeCh1.AutoSize = true;
            this.cbLosModeCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosModeCh1.Location = new System.Drawing.Point(59, 23);
            this.cbLosModeCh1.Name = "cbLosModeCh1";
            this.cbLosModeCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLosModeCh1.TabIndex = 41;
            this.cbLosModeCh1.Text = "Ch1";
            this.cbLosModeCh1.UseVisualStyleBackColor = true;
            this.cbLosModeCh1.CheckedChanged += new System.EventHandler(this.cbLosModeCh1_CheckedChanged);
            // 
            // cbLosModeCh0
            // 
            this.cbLosModeCh0.AutoSize = true;
            this.cbLosModeCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosModeCh0.Location = new System.Drawing.Point(3, 23);
            this.cbLosModeCh0.Name = "cbLosModeCh0";
            this.cbLosModeCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLosModeCh0.TabIndex = 40;
            this.cbLosModeCh0.Text = "Ch0";
            this.cbLosModeCh0.UseVisualStyleBackColor = true;
            this.cbLosModeCh0.CheckedChanged += new System.EventHandler(this.cbLosModeCh0_CheckedChanged);
            // 
            // cbLosModeCh2
            // 
            this.cbLosModeCh2.AutoSize = true;
            this.cbLosModeCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosModeCh2.Location = new System.Drawing.Point(115, 23);
            this.cbLosModeCh2.Name = "cbLosModeCh2";
            this.cbLosModeCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLosModeCh2.TabIndex = 42;
            this.cbLosModeCh2.Text = "Ch2";
            this.cbLosModeCh2.UseVisualStyleBackColor = true;
            this.cbLosModeCh2.CheckedChanged += new System.EventHandler(this.cbLosModeCh2_CheckedChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label40.Location = new System.Drawing.Point(3, 88);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(196, 15);
            this.label40.TabIndex = 27;
            this.label40.Text = "False / True：LOL latch / non-latch";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label41.Location = new System.Drawing.Point(3, 43);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(197, 15);
            this.label41.TabIndex = 22;
            this.label41.Text = "False / True：LOS latch / non-latch";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.cbLolClearCh3);
            this.groupBox16.Controls.Add(this.cbLolClearCh1);
            this.groupBox16.Controls.Add(this.cbLolClearCh0);
            this.groupBox16.Controls.Add(this.cbLolClearCh2);
            this.groupBox16.Controls.Add(this.cbLosClearCh3);
            this.groupBox16.Controls.Add(this.cbLosClearCh1);
            this.groupBox16.Controls.Add(this.cbLosClearCh0);
            this.groupBox16.Controls.Add(this.cbLosClearCh2);
            this.groupBox16.Controls.Add(this.label38);
            this.groupBox16.Controls.Add(this.label39);
            this.groupBox16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox16.Location = new System.Drawing.Point(230, 304);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(231, 108);
            this.groupBox16.TabIndex = 49;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "R14_LOS / LOL clear control";
            // 
            // cbLolClearCh3
            // 
            this.cbLolClearCh3.AutoSize = true;
            this.cbLolClearCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolClearCh3.Location = new System.Drawing.Point(171, 68);
            this.cbLolClearCh3.Name = "cbLolClearCh3";
            this.cbLolClearCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLolClearCh3.TabIndex = 47;
            this.cbLolClearCh3.Text = "Ch3";
            this.cbLolClearCh3.UseVisualStyleBackColor = true;
            this.cbLolClearCh3.CheckedChanged += new System.EventHandler(this.cbLolClearCh3_CheckedChanged);
            // 
            // cbLolClearCh1
            // 
            this.cbLolClearCh1.AutoSize = true;
            this.cbLolClearCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolClearCh1.Location = new System.Drawing.Point(59, 68);
            this.cbLolClearCh1.Name = "cbLolClearCh1";
            this.cbLolClearCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLolClearCh1.TabIndex = 45;
            this.cbLolClearCh1.Text = "Ch1";
            this.cbLolClearCh1.UseVisualStyleBackColor = true;
            this.cbLolClearCh1.CheckedChanged += new System.EventHandler(this.cbLolClearCh1_CheckedChanged);
            // 
            // cbLolClearCh0
            // 
            this.cbLolClearCh0.AutoSize = true;
            this.cbLolClearCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolClearCh0.Location = new System.Drawing.Point(3, 68);
            this.cbLolClearCh0.Name = "cbLolClearCh0";
            this.cbLolClearCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLolClearCh0.TabIndex = 44;
            this.cbLolClearCh0.Text = "Ch0";
            this.cbLolClearCh0.UseVisualStyleBackColor = true;
            this.cbLolClearCh0.CheckedChanged += new System.EventHandler(this.cbLolClearCh0_CheckedChanged);
            // 
            // cbLolClearCh2
            // 
            this.cbLolClearCh2.AutoSize = true;
            this.cbLolClearCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolClearCh2.Location = new System.Drawing.Point(115, 68);
            this.cbLolClearCh2.Name = "cbLolClearCh2";
            this.cbLolClearCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLolClearCh2.TabIndex = 46;
            this.cbLolClearCh2.Text = "Ch2";
            this.cbLolClearCh2.UseVisualStyleBackColor = true;
            this.cbLolClearCh2.CheckedChanged += new System.EventHandler(this.cbLolClearCh2_CheckedChanged);
            // 
            // cbLosClearCh3
            // 
            this.cbLosClearCh3.AutoSize = true;
            this.cbLosClearCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosClearCh3.Location = new System.Drawing.Point(171, 23);
            this.cbLosClearCh3.Name = "cbLosClearCh3";
            this.cbLosClearCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLosClearCh3.TabIndex = 43;
            this.cbLosClearCh3.Text = "Ch3";
            this.cbLosClearCh3.UseVisualStyleBackColor = true;
            this.cbLosClearCh3.CheckedChanged += new System.EventHandler(this.cbLosClearCh3_CheckedChanged);
            // 
            // cbLosClearCh1
            // 
            this.cbLosClearCh1.AutoSize = true;
            this.cbLosClearCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosClearCh1.Location = new System.Drawing.Point(59, 23);
            this.cbLosClearCh1.Name = "cbLosClearCh1";
            this.cbLosClearCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLosClearCh1.TabIndex = 41;
            this.cbLosClearCh1.Text = "Ch1";
            this.cbLosClearCh1.UseVisualStyleBackColor = true;
            this.cbLosClearCh1.CheckedChanged += new System.EventHandler(this.cbLosClearCh1_CheckedChanged);
            // 
            // cbLosClearCh0
            // 
            this.cbLosClearCh0.AutoSize = true;
            this.cbLosClearCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosClearCh0.Location = new System.Drawing.Point(3, 23);
            this.cbLosClearCh0.Name = "cbLosClearCh0";
            this.cbLosClearCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLosClearCh0.TabIndex = 40;
            this.cbLosClearCh0.Text = "Ch0";
            this.cbLosClearCh0.UseVisualStyleBackColor = true;
            this.cbLosClearCh0.CheckedChanged += new System.EventHandler(this.cbLosClearCh0_CheckedChanged);
            // 
            // cbLosClearCh2
            // 
            this.cbLosClearCh2.AutoSize = true;
            this.cbLosClearCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosClearCh2.Location = new System.Drawing.Point(115, 23);
            this.cbLosClearCh2.Name = "cbLosClearCh2";
            this.cbLosClearCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLosClearCh2.TabIndex = 42;
            this.cbLosClearCh2.Text = "Ch2";
            this.cbLosClearCh2.UseVisualStyleBackColor = true;
            this.cbLosClearCh2.CheckedChanged += new System.EventHandler(this.cbLosClearCh2_CheckedChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label38.Location = new System.Drawing.Point(3, 88);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(185, 15);
            this.label38.TabIndex = 27;
            this.label38.Text = "False / True：LOL normal / clear";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label39.Location = new System.Drawing.Point(3, 43);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(186, 15);
            this.label39.TabIndex = 22;
            this.label39.Text = "False / True：LOS normal / clear";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cbDeEmphasisCh3);
            this.groupBox12.Controls.Add(this.label23);
            this.groupBox12.Controls.Add(this.cbDeEmphasisCh2);
            this.groupBox12.Controls.Add(this.label24);
            this.groupBox12.Controls.Add(this.cbDeEmphasisCh1);
            this.groupBox12.Controls.Add(this.label21);
            this.groupBox12.Controls.Add(this.cbDeEmphasisCh0);
            this.groupBox12.Controls.Add(this.label22);
            this.groupBox12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox12.Location = new System.Drawing.Point(700, 227);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(230, 150);
            this.groupBox12.TabIndex = 47;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "R26/27_De-emphasis control";
            // 
            // cbDeEmphasisCh3
            // 
            this.cbDeEmphasisCh3.FormattingEnabled = true;
            this.cbDeEmphasisCh3.Location = new System.Drawing.Point(76, 118);
            this.cbDeEmphasisCh3.Name = "cbDeEmphasisCh3";
            this.cbDeEmphasisCh3.Size = new System.Drawing.Size(88, 26);
            this.cbDeEmphasisCh3.TabIndex = 23;
            this.cbDeEmphasisCh3.SelectedIndexChanged += new System.EventHandler(this.cbDeEmphasisCh3_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Location = new System.Drawing.Point(17, 121);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 18);
            this.label23.TabIndex = 22;
            this.label23.Text = "Ch3：";
            // 
            // cbDeEmphasisCh2
            // 
            this.cbDeEmphasisCh2.FormattingEnabled = true;
            this.cbDeEmphasisCh2.Location = new System.Drawing.Point(76, 87);
            this.cbDeEmphasisCh2.Name = "cbDeEmphasisCh2";
            this.cbDeEmphasisCh2.Size = new System.Drawing.Size(88, 26);
            this.cbDeEmphasisCh2.TabIndex = 21;
            this.cbDeEmphasisCh2.SelectedIndexChanged += new System.EventHandler(this.cbDeEmphasisCh2_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Location = new System.Drawing.Point(17, 90);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 18);
            this.label24.TabIndex = 20;
            this.label24.Text = "Ch2：";
            // 
            // cbDeEmphasisCh1
            // 
            this.cbDeEmphasisCh1.FormattingEnabled = true;
            this.cbDeEmphasisCh1.Location = new System.Drawing.Point(76, 56);
            this.cbDeEmphasisCh1.Name = "cbDeEmphasisCh1";
            this.cbDeEmphasisCh1.Size = new System.Drawing.Size(88, 26);
            this.cbDeEmphasisCh1.TabIndex = 19;
            this.cbDeEmphasisCh1.SelectedIndexChanged += new System.EventHandler(this.cbDeEmphasisCh1_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(17, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 18);
            this.label21.TabIndex = 18;
            this.label21.Text = "Ch1：";
            // 
            // cbDeEmphasisCh0
            // 
            this.cbDeEmphasisCh0.FormattingEnabled = true;
            this.cbDeEmphasisCh0.Location = new System.Drawing.Point(76, 25);
            this.cbDeEmphasisCh0.Name = "cbDeEmphasisCh0";
            this.cbDeEmphasisCh0.Size = new System.Drawing.Size(88, 26);
            this.cbDeEmphasisCh0.TabIndex = 17;
            this.cbDeEmphasisCh0.SelectedIndexChanged += new System.EventHandler(this.cbDeEmphasisCh0_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Location = new System.Drawing.Point(17, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 18);
            this.label22.TabIndex = 16;
            this.label22.Text = "Ch0：";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.cbCrossPointCh3);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.cbCrossPointCh2);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.cbCrossPointCh1);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.cbCrossPointCh0);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox10.Location = new System.Drawing.Point(700, 77);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(230, 150);
            this.groupBox10.TabIndex = 45;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "R24/25_Cross point control";
            // 
            // cbCrossPointCh3
            // 
            this.cbCrossPointCh3.FormattingEnabled = true;
            this.cbCrossPointCh3.Location = new System.Drawing.Point(76, 118);
            this.cbCrossPointCh3.Name = "cbCrossPointCh3";
            this.cbCrossPointCh3.Size = new System.Drawing.Size(88, 26);
            this.cbCrossPointCh3.TabIndex = 23;
            this.cbCrossPointCh3.SelectedIndexChanged += new System.EventHandler(this.cbCrossPointCh3_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(17, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 22;
            this.label19.Text = "Ch3：";
            // 
            // cbCrossPointCh2
            // 
            this.cbCrossPointCh2.FormattingEnabled = true;
            this.cbCrossPointCh2.Location = new System.Drawing.Point(76, 87);
            this.cbCrossPointCh2.Name = "cbCrossPointCh2";
            this.cbCrossPointCh2.Size = new System.Drawing.Size(88, 26);
            this.cbCrossPointCh2.TabIndex = 21;
            this.cbCrossPointCh2.SelectedIndexChanged += new System.EventHandler(this.cbCrossPointCh2_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(17, 90);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 18);
            this.label20.TabIndex = 20;
            this.label20.Text = "Ch2：";
            // 
            // cbCrossPointCh1
            // 
            this.cbCrossPointCh1.FormattingEnabled = true;
            this.cbCrossPointCh1.Location = new System.Drawing.Point(76, 56);
            this.cbCrossPointCh1.Name = "cbCrossPointCh1";
            this.cbCrossPointCh1.Size = new System.Drawing.Size(88, 26);
            this.cbCrossPointCh1.TabIndex = 19;
            this.cbCrossPointCh1.SelectedIndexChanged += new System.EventHandler(this.cbCrossPointCh1_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(17, 59);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 18);
            this.label17.TabIndex = 18;
            this.label17.Text = "Ch1：";
            // 
            // cbCrossPointCh0
            // 
            this.cbCrossPointCh0.FormattingEnabled = true;
            this.cbCrossPointCh0.Location = new System.Drawing.Point(76, 25);
            this.cbCrossPointCh0.Name = "cbCrossPointCh0";
            this.cbCrossPointCh0.Size = new System.Drawing.Size(88, 26);
            this.cbCrossPointCh0.TabIndex = 17;
            this.cbCrossPointCh0.SelectedIndexChanged += new System.EventHandler(this.cbCrossPointCh0_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Location = new System.Drawing.Point(17, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 18);
            this.label18.TabIndex = 16;
            this.label18.Text = "Ch0：";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cbMuteModeCh3);
            this.groupBox9.Controls.Add(this.cbMuteModeCh1);
            this.groupBox9.Controls.Add(this.cbMuteModeCh0);
            this.groupBox9.Controls.Add(this.cbMuteModeCh2);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox9.Location = new System.Drawing.Point(700, 5);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(230, 66);
            this.groupBox9.TabIndex = 44;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "R23_Mute control";
            // 
            // cbMuteModeCh3
            // 
            this.cbMuteModeCh3.AutoSize = true;
            this.cbMuteModeCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMuteModeCh3.Location = new System.Drawing.Point(170, 24);
            this.cbMuteModeCh3.Name = "cbMuteModeCh3";
            this.cbMuteModeCh3.Size = new System.Drawing.Size(56, 22);
            this.cbMuteModeCh3.TabIndex = 43;
            this.cbMuteModeCh3.Text = "Ch3";
            this.cbMuteModeCh3.UseVisualStyleBackColor = true;
            this.cbMuteModeCh3.CheckedChanged += new System.EventHandler(this.cbMuteModeCh3_CheckedChanged);
            // 
            // cbMuteModeCh1
            // 
            this.cbMuteModeCh1.AutoSize = true;
            this.cbMuteModeCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMuteModeCh1.Location = new System.Drawing.Point(58, 24);
            this.cbMuteModeCh1.Name = "cbMuteModeCh1";
            this.cbMuteModeCh1.Size = new System.Drawing.Size(56, 22);
            this.cbMuteModeCh1.TabIndex = 41;
            this.cbMuteModeCh1.Text = "Ch1";
            this.cbMuteModeCh1.UseVisualStyleBackColor = true;
            this.cbMuteModeCh1.CheckedChanged += new System.EventHandler(this.cbMuteModeCh1_CheckedChanged);
            // 
            // cbMuteModeCh0
            // 
            this.cbMuteModeCh0.AutoSize = true;
            this.cbMuteModeCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMuteModeCh0.Location = new System.Drawing.Point(2, 24);
            this.cbMuteModeCh0.Name = "cbMuteModeCh0";
            this.cbMuteModeCh0.Size = new System.Drawing.Size(56, 22);
            this.cbMuteModeCh0.TabIndex = 40;
            this.cbMuteModeCh0.Text = "Ch0";
            this.cbMuteModeCh0.UseVisualStyleBackColor = true;
            this.cbMuteModeCh0.CheckedChanged += new System.EventHandler(this.cbMuteModeCh0_CheckedChanged);
            // 
            // cbMuteModeCh2
            // 
            this.cbMuteModeCh2.AutoSize = true;
            this.cbMuteModeCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbMuteModeCh2.Location = new System.Drawing.Point(114, 24);
            this.cbMuteModeCh2.Name = "cbMuteModeCh2";
            this.cbMuteModeCh2.Size = new System.Drawing.Size(56, 22);
            this.cbMuteModeCh2.TabIndex = 42;
            this.cbMuteModeCh2.Text = "Ch2";
            this.cbMuteModeCh2.UseVisualStyleBackColor = true;
            this.cbMuteModeCh2.CheckedChanged += new System.EventHandler(this.cbMuteModeCh2_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(2, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(159, 15);
            this.label15.TabIndex = 27;
            this.label15.Text = "False / True：auto / manual";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbFaultModeCh3);
            this.groupBox6.Controls.Add(this.cbFaultModeCh1);
            this.groupBox6.Controls.Add(this.cbFaultModeCh0);
            this.groupBox6.Controls.Add(this.cbFaultModeCh2);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.cbLosHysteresisCh3);
            this.groupBox6.Controls.Add(this.cbLosHysteresisCh1);
            this.groupBox6.Controls.Add(this.cbLosHysteresisCh0);
            this.groupBox6.Controls.Add(this.cbLosHysteresisCh2);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox6.Location = new System.Drawing.Point(465, 267);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(230, 101);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "R19_LOS hysteresis / Fault mode";
            // 
            // cbFaultModeCh3
            // 
            this.cbFaultModeCh3.AutoSize = true;
            this.cbFaultModeCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultModeCh3.Location = new System.Drawing.Point(171, 61);
            this.cbFaultModeCh3.Name = "cbFaultModeCh3";
            this.cbFaultModeCh3.Size = new System.Drawing.Size(56, 22);
            this.cbFaultModeCh3.TabIndex = 52;
            this.cbFaultModeCh3.Text = "Ch3";
            this.cbFaultModeCh3.UseVisualStyleBackColor = true;
            this.cbFaultModeCh3.CheckedChanged += new System.EventHandler(this.cbFaultModeCh3_CheckedChanged);
            // 
            // cbFaultModeCh1
            // 
            this.cbFaultModeCh1.AutoSize = true;
            this.cbFaultModeCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultModeCh1.Location = new System.Drawing.Point(59, 61);
            this.cbFaultModeCh1.Name = "cbFaultModeCh1";
            this.cbFaultModeCh1.Size = new System.Drawing.Size(56, 22);
            this.cbFaultModeCh1.TabIndex = 50;
            this.cbFaultModeCh1.Text = "Ch1";
            this.cbFaultModeCh1.UseVisualStyleBackColor = true;
            this.cbFaultModeCh1.CheckedChanged += new System.EventHandler(this.cbFaultModeCh1_CheckedChanged);
            // 
            // cbFaultModeCh0
            // 
            this.cbFaultModeCh0.AutoSize = true;
            this.cbFaultModeCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultModeCh0.Location = new System.Drawing.Point(3, 61);
            this.cbFaultModeCh0.Name = "cbFaultModeCh0";
            this.cbFaultModeCh0.Size = new System.Drawing.Size(56, 22);
            this.cbFaultModeCh0.TabIndex = 49;
            this.cbFaultModeCh0.Text = "Ch0";
            this.cbFaultModeCh0.UseVisualStyleBackColor = true;
            this.cbFaultModeCh0.CheckedChanged += new System.EventHandler(this.cbFaultModeCh0_CheckedChanged);
            // 
            // cbFaultModeCh2
            // 
            this.cbFaultModeCh2.AutoSize = true;
            this.cbFaultModeCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbFaultModeCh2.Location = new System.Drawing.Point(115, 61);
            this.cbFaultModeCh2.Name = "cbFaultModeCh2";
            this.cbFaultModeCh2.Size = new System.Drawing.Size(56, 22);
            this.cbFaultModeCh2.TabIndex = 51;
            this.cbFaultModeCh2.Text = "Ch2";
            this.cbFaultModeCh2.UseVisualStyleBackColor = true;
            this.cbFaultModeCh2.CheckedChanged += new System.EventHandler(this.cbFaultModeCh2_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(3, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 15);
            this.label8.TabIndex = 48;
            this.label8.Text = "False / True：latch / non-latch";
            // 
            // cbLosHysteresisCh3
            // 
            this.cbLosHysteresisCh3.AutoSize = true;
            this.cbLosHysteresisCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosHysteresisCh3.Location = new System.Drawing.Point(171, 23);
            this.cbLosHysteresisCh3.Name = "cbLosHysteresisCh3";
            this.cbLosHysteresisCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLosHysteresisCh3.TabIndex = 30;
            this.cbLosHysteresisCh3.Text = "Ch3";
            this.cbLosHysteresisCh3.UseVisualStyleBackColor = true;
            this.cbLosHysteresisCh3.CheckedChanged += new System.EventHandler(this.cbLosHysteresisCh3_CheckedChanged);
            // 
            // cbLosHysteresisCh1
            // 
            this.cbLosHysteresisCh1.AutoSize = true;
            this.cbLosHysteresisCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosHysteresisCh1.Location = new System.Drawing.Point(59, 23);
            this.cbLosHysteresisCh1.Name = "cbLosHysteresisCh1";
            this.cbLosHysteresisCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLosHysteresisCh1.TabIndex = 28;
            this.cbLosHysteresisCh1.Text = "Ch1";
            this.cbLosHysteresisCh1.UseVisualStyleBackColor = true;
            this.cbLosHysteresisCh1.CheckedChanged += new System.EventHandler(this.cbLosHysteresisCh1_CheckedChanged);
            // 
            // cbLosHysteresisCh0
            // 
            this.cbLosHysteresisCh0.AutoSize = true;
            this.cbLosHysteresisCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosHysteresisCh0.Location = new System.Drawing.Point(3, 23);
            this.cbLosHysteresisCh0.Name = "cbLosHysteresisCh0";
            this.cbLosHysteresisCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLosHysteresisCh0.TabIndex = 27;
            this.cbLosHysteresisCh0.Text = "Ch0";
            this.cbLosHysteresisCh0.UseVisualStyleBackColor = true;
            this.cbLosHysteresisCh0.CheckedChanged += new System.EventHandler(this.cbLosHysteresisCh0_CheckedChanged);
            // 
            // cbLosHysteresisCh2
            // 
            this.cbLosHysteresisCh2.AutoSize = true;
            this.cbLosHysteresisCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosHysteresisCh2.Location = new System.Drawing.Point(115, 23);
            this.cbLosHysteresisCh2.Name = "cbLosHysteresisCh2";
            this.cbLosHysteresisCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLosHysteresisCh2.TabIndex = 29;
            this.cbLosHysteresisCh2.Text = "Ch2";
            this.cbLosHysteresisCh2.UseVisualStyleBackColor = true;
            this.cbLosHysteresisCh2.CheckedChanged += new System.EventHandler(this.cbLosHysteresisCh2_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(3, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(157, 15);
            this.label10.TabIndex = 22;
            this.label10.Text = "False / True：1.5dB / 2.5dB";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbLosThresholdCh3);
            this.groupBox4.Controls.Add(this.label61);
            this.groupBox4.Controls.Add(this.cbLosThresholdCh2);
            this.groupBox4.Controls.Add(this.label64);
            this.groupBox4.Controls.Add(this.cbLosThresholdCh1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cbLosThresholdCh0);
            this.groupBox4.Controls.Add(this.lLosThresholdL0);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox4.Location = new System.Drawing.Point(465, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 150);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "R17/18_LOS threshold control";
            // 
            // cbLosThresholdCh3
            // 
            this.cbLosThresholdCh3.FormattingEnabled = true;
            this.cbLosThresholdCh3.Location = new System.Drawing.Point(76, 118);
            this.cbLosThresholdCh3.Name = "cbLosThresholdCh3";
            this.cbLosThresholdCh3.Size = new System.Drawing.Size(88, 26);
            this.cbLosThresholdCh3.TabIndex = 23;
            this.cbLosThresholdCh3.SelectedIndexChanged += new System.EventHandler(this.cbLosThresholdCh3_SelectedIndexChanged);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Location = new System.Drawing.Point(17, 121);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(53, 18);
            this.label61.TabIndex = 22;
            this.label61.Text = "Ch3：";
            // 
            // cbLosThresholdCh2
            // 
            this.cbLosThresholdCh2.DisplayMember = "1000";
            this.cbLosThresholdCh2.FormattingEnabled = true;
            this.cbLosThresholdCh2.Location = new System.Drawing.Point(76, 87);
            this.cbLosThresholdCh2.Name = "cbLosThresholdCh2";
            this.cbLosThresholdCh2.Size = new System.Drawing.Size(88, 26);
            this.cbLosThresholdCh2.TabIndex = 21;
            this.cbLosThresholdCh2.SelectedIndexChanged += new System.EventHandler(this.cbLosThresholdCh2_SelectedIndexChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Location = new System.Drawing.Point(17, 90);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(53, 18);
            this.label64.TabIndex = 20;
            this.label64.Text = "Ch2：";
            // 
            // cbLosThresholdCh1
            // 
            this.cbLosThresholdCh1.DisplayMember = "1000";
            this.cbLosThresholdCh1.FormattingEnabled = true;
            this.cbLosThresholdCh1.Location = new System.Drawing.Point(76, 56);
            this.cbLosThresholdCh1.Name = "cbLosThresholdCh1";
            this.cbLosThresholdCh1.Size = new System.Drawing.Size(88, 26);
            this.cbLosThresholdCh1.TabIndex = 19;
            this.cbLosThresholdCh1.SelectedIndexChanged += new System.EventHandler(this.cbLosThresholdCh1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(17, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 18;
            this.label7.Text = "Ch1：";
            // 
            // cbLosThresholdCh0
            // 
            this.cbLosThresholdCh0.DisplayMember = "1100";
            this.cbLosThresholdCh0.FormattingEnabled = true;
            this.cbLosThresholdCh0.Location = new System.Drawing.Point(76, 25);
            this.cbLosThresholdCh0.Name = "cbLosThresholdCh0";
            this.cbLosThresholdCh0.Size = new System.Drawing.Size(88, 26);
            this.cbLosThresholdCh0.TabIndex = 17;
            this.cbLosThresholdCh0.SelectedIndexChanged += new System.EventHandler(this.cbLosThresholdCh0_SelectedIndexChanged);
            // 
            // lLosThresholdL0
            // 
            this.lLosThresholdL0.AutoSize = true;
            this.lLosThresholdL0.BackColor = System.Drawing.Color.Transparent;
            this.lLosThresholdL0.Location = new System.Drawing.Point(17, 28);
            this.lLosThresholdL0.Name = "lLosThresholdL0";
            this.lLosThresholdL0.Size = new System.Drawing.Size(53, 18);
            this.lLosThresholdL0.TabIndex = 16;
            this.lLosThresholdL0.Text = "Ch0：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbLolMaskCh3);
            this.groupBox2.Controls.Add(this.cbLolMaskCh1);
            this.groupBox2.Controls.Add(this.cbLolMaskCh0);
            this.groupBox2.Controls.Add(this.cbLolMaskCh2);
            this.groupBox2.Controls.Add(this.cbLosMaskCh3);
            this.groupBox2.Controls.Add(this.cbLosMaskCh1);
            this.groupBox2.Controls.Add(this.cbLosMaskCh0);
            this.groupBox2.Controls.Add(this.cbLosMaskCh2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(230, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 108);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "R13_LOS / LOL mask control";
            // 
            // cbLolMaskCh3
            // 
            this.cbLolMaskCh3.AutoSize = true;
            this.cbLolMaskCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolMaskCh3.Location = new System.Drawing.Point(171, 68);
            this.cbLolMaskCh3.Name = "cbLolMaskCh3";
            this.cbLolMaskCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLolMaskCh3.TabIndex = 39;
            this.cbLolMaskCh3.Text = "Ch3";
            this.cbLolMaskCh3.UseVisualStyleBackColor = true;
            this.cbLolMaskCh3.CheckedChanged += new System.EventHandler(this.cbLolMaskCh3_CheckedChanged);
            // 
            // cbLolMaskCh1
            // 
            this.cbLolMaskCh1.AutoSize = true;
            this.cbLolMaskCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolMaskCh1.Location = new System.Drawing.Point(59, 68);
            this.cbLolMaskCh1.Name = "cbLolMaskCh1";
            this.cbLolMaskCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLolMaskCh1.TabIndex = 37;
            this.cbLolMaskCh1.Text = "Ch1";
            this.cbLolMaskCh1.UseVisualStyleBackColor = true;
            this.cbLolMaskCh1.CheckedChanged += new System.EventHandler(this.cbLolMaskCh1_CheckedChanged);
            // 
            // cbLolMaskCh0
            // 
            this.cbLolMaskCh0.AutoSize = true;
            this.cbLolMaskCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolMaskCh0.Location = new System.Drawing.Point(3, 68);
            this.cbLolMaskCh0.Name = "cbLolMaskCh0";
            this.cbLolMaskCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLolMaskCh0.TabIndex = 36;
            this.cbLolMaskCh0.Text = "Ch0";
            this.cbLolMaskCh0.UseVisualStyleBackColor = true;
            this.cbLolMaskCh0.CheckedChanged += new System.EventHandler(this.cbLolMaskCh0_CheckedChanged);
            // 
            // cbLolMaskCh2
            // 
            this.cbLolMaskCh2.AutoSize = true;
            this.cbLolMaskCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLolMaskCh2.Location = new System.Drawing.Point(115, 68);
            this.cbLolMaskCh2.Name = "cbLolMaskCh2";
            this.cbLolMaskCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLolMaskCh2.TabIndex = 38;
            this.cbLolMaskCh2.Text = "Ch2";
            this.cbLolMaskCh2.UseVisualStyleBackColor = true;
            this.cbLolMaskCh2.CheckedChanged += new System.EventHandler(this.cbLolMaskCh2_CheckedChanged);
            // 
            // cbLosMaskCh3
            // 
            this.cbLosMaskCh3.AutoSize = true;
            this.cbLosMaskCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosMaskCh3.Location = new System.Drawing.Point(171, 23);
            this.cbLosMaskCh3.Name = "cbLosMaskCh3";
            this.cbLosMaskCh3.Size = new System.Drawing.Size(56, 22);
            this.cbLosMaskCh3.TabIndex = 35;
            this.cbLosMaskCh3.Text = "Ch3";
            this.cbLosMaskCh3.UseVisualStyleBackColor = true;
            this.cbLosMaskCh3.CheckedChanged += new System.EventHandler(this.cbLosMaskCh3_CheckedChanged);
            // 
            // cbLosMaskCh1
            // 
            this.cbLosMaskCh1.AutoSize = true;
            this.cbLosMaskCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosMaskCh1.Location = new System.Drawing.Point(59, 23);
            this.cbLosMaskCh1.Name = "cbLosMaskCh1";
            this.cbLosMaskCh1.Size = new System.Drawing.Size(56, 22);
            this.cbLosMaskCh1.TabIndex = 33;
            this.cbLosMaskCh1.Text = "Ch1";
            this.cbLosMaskCh1.UseVisualStyleBackColor = true;
            this.cbLosMaskCh1.CheckedChanged += new System.EventHandler(this.cbLosMaskCh1_CheckedChanged);
            // 
            // cbLosMaskCh0
            // 
            this.cbLosMaskCh0.AutoSize = true;
            this.cbLosMaskCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosMaskCh0.Location = new System.Drawing.Point(3, 23);
            this.cbLosMaskCh0.Name = "cbLosMaskCh0";
            this.cbLosMaskCh0.Size = new System.Drawing.Size(56, 22);
            this.cbLosMaskCh0.TabIndex = 32;
            this.cbLosMaskCh0.Text = "Ch0";
            this.cbLosMaskCh0.UseVisualStyleBackColor = true;
            this.cbLosMaskCh0.CheckedChanged += new System.EventHandler(this.cbLosMaskCh0_CheckedChanged);
            // 
            // cbLosMaskCh2
            // 
            this.cbLosMaskCh2.AutoSize = true;
            this.cbLosMaskCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLosMaskCh2.Location = new System.Drawing.Point(115, 23);
            this.cbLosMaskCh2.Name = "cbLosMaskCh2";
            this.cbLosMaskCh2.Size = new System.Drawing.Size(56, 22);
            this.cbLosMaskCh2.TabIndex = 34;
            this.cbLosMaskCh2.Text = "Ch2";
            this.cbLosMaskCh2.UseVisualStyleBackColor = true;
            this.cbLosMaskCh2.CheckedChanged += new System.EventHandler(this.cbLosMaskCh2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(3, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 15);
            this.label5.TabIndex = 27;
            this.label5.Text = "False / True：LOL normal / mask";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "False / True：LOS normal / mask";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTxPowerControlCh3);
            this.groupBox1.Controls.Add(this.cbTxPowerControlCh1);
            this.groupBox1.Controls.Add(this.cbTxPowerControlCh0);
            this.groupBox1.Controls.Add(this.cbTxPowerControlCh2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbTxCdrControlCh3);
            this.groupBox1.Controls.Add(this.cbTxCdrControlCh1);
            this.groupBox1.Controls.Add(this.cbTxCdrControlCh0);
            this.groupBox1.Controls.Add(this.cbTxCdrControlCh2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(230, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 108);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "R12_Power and CDR control";
            // 
            // cbTxPowerControlCh3
            // 
            this.cbTxPowerControlCh3.AutoSize = true;
            this.cbTxPowerControlCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxPowerControlCh3.Location = new System.Drawing.Point(171, 23);
            this.cbTxPowerControlCh3.Name = "cbTxPowerControlCh3";
            this.cbTxPowerControlCh3.Size = new System.Drawing.Size(56, 22);
            this.cbTxPowerControlCh3.TabIndex = 31;
            this.cbTxPowerControlCh3.Text = "Ch3";
            this.cbTxPowerControlCh3.UseVisualStyleBackColor = true;
            this.cbTxPowerControlCh3.CheckedChanged += new System.EventHandler(this.cbTxPowerControlCh3_CheckedChanged);
            // 
            // cbTxPowerControlCh1
            // 
            this.cbTxPowerControlCh1.AutoSize = true;
            this.cbTxPowerControlCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxPowerControlCh1.Location = new System.Drawing.Point(59, 23);
            this.cbTxPowerControlCh1.Name = "cbTxPowerControlCh1";
            this.cbTxPowerControlCh1.Size = new System.Drawing.Size(56, 22);
            this.cbTxPowerControlCh1.TabIndex = 29;
            this.cbTxPowerControlCh1.Text = "Ch1";
            this.cbTxPowerControlCh1.UseVisualStyleBackColor = true;
            this.cbTxPowerControlCh1.CheckedChanged += new System.EventHandler(this.cbTxPowerControlCh1_CheckedChanged);
            // 
            // cbTxPowerControlCh0
            // 
            this.cbTxPowerControlCh0.AutoSize = true;
            this.cbTxPowerControlCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxPowerControlCh0.Location = new System.Drawing.Point(3, 23);
            this.cbTxPowerControlCh0.Name = "cbTxPowerControlCh0";
            this.cbTxPowerControlCh0.Size = new System.Drawing.Size(56, 22);
            this.cbTxPowerControlCh0.TabIndex = 28;
            this.cbTxPowerControlCh0.Text = "Ch0";
            this.cbTxPowerControlCh0.UseVisualStyleBackColor = true;
            this.cbTxPowerControlCh0.CheckedChanged += new System.EventHandler(this.cbTxPowerControlCh0_CheckedChanged);
            // 
            // cbTxPowerControlCh2
            // 
            this.cbTxPowerControlCh2.AutoSize = true;
            this.cbTxPowerControlCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxPowerControlCh2.Location = new System.Drawing.Point(115, 23);
            this.cbTxPowerControlCh2.Name = "cbTxPowerControlCh2";
            this.cbTxPowerControlCh2.Size = new System.Drawing.Size(56, 22);
            this.cbTxPowerControlCh2.TabIndex = 30;
            this.cbTxPowerControlCh2.Text = "Ch2";
            this.cbTxPowerControlCh2.UseVisualStyleBackColor = true;
            this.cbTxPowerControlCh2.CheckedChanged += new System.EventHandler(this.cbTxPowerControlCh2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "False / True：cdr / bypass";
            // 
            // cbTxCdrControlCh3
            // 
            this.cbTxCdrControlCh3.AutoSize = true;
            this.cbTxCdrControlCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxCdrControlCh3.Location = new System.Drawing.Point(171, 68);
            this.cbTxCdrControlCh3.Name = "cbTxCdrControlCh3";
            this.cbTxCdrControlCh3.Size = new System.Drawing.Size(56, 22);
            this.cbTxCdrControlCh3.TabIndex = 26;
            this.cbTxCdrControlCh3.Text = "Ch3";
            this.cbTxCdrControlCh3.UseVisualStyleBackColor = true;
            this.cbTxCdrControlCh3.CheckedChanged += new System.EventHandler(this.cbTxCdrControlCh3_CheckedChanged);
            // 
            // cbTxCdrControlCh1
            // 
            this.cbTxCdrControlCh1.AutoSize = true;
            this.cbTxCdrControlCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxCdrControlCh1.Location = new System.Drawing.Point(59, 68);
            this.cbTxCdrControlCh1.Name = "cbTxCdrControlCh1";
            this.cbTxCdrControlCh1.Size = new System.Drawing.Size(56, 22);
            this.cbTxCdrControlCh1.TabIndex = 24;
            this.cbTxCdrControlCh1.Text = "Ch1";
            this.cbTxCdrControlCh1.UseVisualStyleBackColor = true;
            this.cbTxCdrControlCh1.CheckedChanged += new System.EventHandler(this.cbTxCdrControlCh1_CheckedChanged);
            // 
            // cbTxCdrControlCh0
            // 
            this.cbTxCdrControlCh0.AutoSize = true;
            this.cbTxCdrControlCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxCdrControlCh0.Location = new System.Drawing.Point(3, 68);
            this.cbTxCdrControlCh0.Name = "cbTxCdrControlCh0";
            this.cbTxCdrControlCh0.Size = new System.Drawing.Size(56, 22);
            this.cbTxCdrControlCh0.TabIndex = 23;
            this.cbTxCdrControlCh0.Text = "Ch0";
            this.cbTxCdrControlCh0.UseVisualStyleBackColor = true;
            this.cbTxCdrControlCh0.CheckedChanged += new System.EventHandler(this.cbTxCdrControlCh0_CheckedChanged);
            // 
            // cbTxCdrControlCh2
            // 
            this.cbTxCdrControlCh2.AutoSize = true;
            this.cbTxCdrControlCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTxCdrControlCh2.Location = new System.Drawing.Point(115, 68);
            this.cbTxCdrControlCh2.Name = "cbTxCdrControlCh2";
            this.cbTxCdrControlCh2.Size = new System.Drawing.Size(56, 22);
            this.cbTxCdrControlCh2.TabIndex = 25;
            this.cbTxCdrControlCh2.Text = "Ch2";
            this.cbTxCdrControlCh2.UseVisualStyleBackColor = true;
            this.cbTxCdrControlCh2.CheckedChanged += new System.EventHandler(this.cbTxCdrControlCh2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "False / True：Power no / off";
            // 
            // gbLOL
            // 
            this.gbLOL.Controls.Add(this.cbInvertPolarityCH3);
            this.gbLOL.Controls.Add(this.cbInvertPolarityCH1);
            this.gbLOL.Controls.Add(this.cbInvertPolarityCH0);
            this.gbLOL.Controls.Add(this.cbInvertPolarityCH2);
            this.gbLOL.Controls.Add(this.label1);
            this.gbLOL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gbLOL.Location = new System.Drawing.Point(230, 5);
            this.gbLOL.Name = "gbLOL";
            this.gbLOL.Size = new System.Drawing.Size(230, 66);
            this.gbLOL.TabIndex = 30;
            this.gbLOL.TabStop = false;
            this.gbLOL.Text = "R11_Polarity control";
            // 
            // cbInvertPolarityCH3
            // 
            this.cbInvertPolarityCH3.AutoSize = true;
            this.cbInvertPolarityCH3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbInvertPolarityCH3.Location = new System.Drawing.Point(171, 23);
            this.cbInvertPolarityCH3.Name = "cbInvertPolarityCH3";
            this.cbInvertPolarityCH3.Size = new System.Drawing.Size(56, 22);
            this.cbInvertPolarityCH3.TabIndex = 35;
            this.cbInvertPolarityCH3.Text = "Ch3";
            this.cbInvertPolarityCH3.UseVisualStyleBackColor = true;
            this.cbInvertPolarityCH3.CheckedChanged += new System.EventHandler(this.cbInvertPolarityCH3_CheckedChanged);
            // 
            // cbInvertPolarityCH1
            // 
            this.cbInvertPolarityCH1.AutoSize = true;
            this.cbInvertPolarityCH1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbInvertPolarityCH1.Location = new System.Drawing.Point(59, 23);
            this.cbInvertPolarityCH1.Name = "cbInvertPolarityCH1";
            this.cbInvertPolarityCH1.Size = new System.Drawing.Size(56, 22);
            this.cbInvertPolarityCH1.TabIndex = 33;
            this.cbInvertPolarityCH1.Text = "Ch1";
            this.cbInvertPolarityCH1.UseVisualStyleBackColor = true;
            this.cbInvertPolarityCH1.CheckedChanged += new System.EventHandler(this.cbInvertPolarityCH1_CheckedChanged);
            // 
            // cbInvertPolarityCH0
            // 
            this.cbInvertPolarityCH0.AutoSize = true;
            this.cbInvertPolarityCH0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbInvertPolarityCH0.Location = new System.Drawing.Point(3, 23);
            this.cbInvertPolarityCH0.Name = "cbInvertPolarityCH0";
            this.cbInvertPolarityCH0.Size = new System.Drawing.Size(56, 22);
            this.cbInvertPolarityCH0.TabIndex = 32;
            this.cbInvertPolarityCH0.Text = "Ch0";
            this.cbInvertPolarityCH0.UseVisualStyleBackColor = true;
            this.cbInvertPolarityCH0.CheckedChanged += new System.EventHandler(this.cbInvertPolarityCH0_CheckedChanged);
            // 
            // cbInvertPolarityCH2
            // 
            this.cbInvertPolarityCH2.AutoSize = true;
            this.cbInvertPolarityCH2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbInvertPolarityCH2.Location = new System.Drawing.Point(115, 23);
            this.cbInvertPolarityCH2.Name = "cbInvertPolarityCH2";
            this.cbInvertPolarityCH2.Size = new System.Drawing.Size(56, 22);
            this.cbInvertPolarityCH2.TabIndex = 34;
            this.cbInvertPolarityCH2.Text = "Ch2";
            this.cbInvertPolarityCH2.UseVisualStyleBackColor = true;
            this.cbInvertPolarityCH2.CheckedChanged += new System.EventHandler(this.cbInvertPolarityCH2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "False / True：normal / Invert";
            // 
            // tpRt145Bychannel
            // 
            this.tpRt145Bychannel.Controls.Add(this.groupBox28);
            this.tpRt145Bychannel.Controls.Add(this.groupBox27);
            this.tpRt145Bychannel.Controls.Add(this.groupBox26);
            this.tpRt145Bychannel.Controls.Add(this.groupBox25);
            this.tpRt145Bychannel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tpRt145Bychannel.Location = new System.Drawing.Point(4, 30);
            this.tpRt145Bychannel.Name = "tpRt145Bychannel";
            this.tpRt145Bychannel.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt145Bychannel.Size = new System.Drawing.Size(932, 526);
            this.tpRt145Bychannel.TabIndex = 1;
            this.tpRt145Bychannel.Text = "Bychannel";
            this.tpRt145Bychannel.UseVisualStyleBackColor = true;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.cbVcoMsbSelecCh3);
            this.groupBox28.Controls.Add(this.cbAutoBypassResetCh3);
            this.groupBox28.Controls.Add(this.label145);
            this.groupBox28.Controls.Add(this.label146);
            this.groupBox28.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox28.Location = new System.Drawing.Point(8, 278);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(240, 85);
            this.groupBox28.TabIndex = 55;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "R159/165_Channel3";
            // 
            // cbVcoMsbSelecCh3
            // 
            this.cbVcoMsbSelecCh3.FormattingEnabled = true;
            this.cbVcoMsbSelecCh3.Location = new System.Drawing.Point(145, 51);
            this.cbVcoMsbSelecCh3.Name = "cbVcoMsbSelecCh3";
            this.cbVcoMsbSelecCh3.Size = new System.Drawing.Size(88, 26);
            this.cbVcoMsbSelecCh3.TabIndex = 33;
            this.cbVcoMsbSelecCh3.SelectedIndexChanged += new System.EventHandler(this.cbVcoMsbSelecCh3_SelectedIndexChanged);
            // 
            // cbAutoBypassResetCh3
            // 
            this.cbAutoBypassResetCh3.FormattingEnabled = true;
            this.cbAutoBypassResetCh3.Location = new System.Drawing.Point(145, 20);
            this.cbAutoBypassResetCh3.Name = "cbAutoBypassResetCh3";
            this.cbAutoBypassResetCh3.Size = new System.Drawing.Size(88, 26);
            this.cbAutoBypassResetCh3.TabIndex = 32;
            this.cbAutoBypassResetCh3.SelectedIndexChanged += new System.EventHandler(this.cbAutoBypassResetCh3_SelectedIndexChanged);
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.BackColor = System.Drawing.Color.Transparent;
            this.label145.Location = new System.Drawing.Point(3, 55);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(164, 18);
            this.label145.TabIndex = 22;
            this.label145.Text = "VCO MSB selection：";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.BackColor = System.Drawing.Color.Transparent;
            this.label146.Location = new System.Drawing.Point(3, 24);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(178, 18);
            this.label146.TabIndex = 21;
            this.label146.Text = "Auto bypass soft reset：";
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.cbVcoMsbSelecCh2);
            this.groupBox27.Controls.Add(this.cbAutoBypassResetCh2);
            this.groupBox27.Controls.Add(this.label102);
            this.groupBox27.Controls.Add(this.label103);
            this.groupBox27.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox27.Location = new System.Drawing.Point(8, 187);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(240, 85);
            this.groupBox27.TabIndex = 54;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "R127/133_Channel2";
            // 
            // cbVcoMsbSelecCh2
            // 
            this.cbVcoMsbSelecCh2.FormattingEnabled = true;
            this.cbVcoMsbSelecCh2.Location = new System.Drawing.Point(145, 51);
            this.cbVcoMsbSelecCh2.Name = "cbVcoMsbSelecCh2";
            this.cbVcoMsbSelecCh2.Size = new System.Drawing.Size(88, 26);
            this.cbVcoMsbSelecCh2.TabIndex = 33;
            this.cbVcoMsbSelecCh2.SelectedIndexChanged += new System.EventHandler(this.cbVcoMsbSelecCh2_SelectedIndexChanged);
            // 
            // cbAutoBypassResetCh2
            // 
            this.cbAutoBypassResetCh2.FormattingEnabled = true;
            this.cbAutoBypassResetCh2.Location = new System.Drawing.Point(145, 20);
            this.cbAutoBypassResetCh2.Name = "cbAutoBypassResetCh2";
            this.cbAutoBypassResetCh2.Size = new System.Drawing.Size(88, 26);
            this.cbAutoBypassResetCh2.TabIndex = 32;
            this.cbAutoBypassResetCh2.SelectedIndexChanged += new System.EventHandler(this.cbAutoBypassResetCh2_SelectedIndexChanged);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.BackColor = System.Drawing.Color.Transparent;
            this.label102.Location = new System.Drawing.Point(3, 55);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(164, 18);
            this.label102.TabIndex = 22;
            this.label102.Text = "VCO MSB selection：";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.BackColor = System.Drawing.Color.Transparent;
            this.label103.Location = new System.Drawing.Point(3, 24);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(178, 18);
            this.label103.TabIndex = 21;
            this.label103.Text = "Auto bypass soft reset：";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.cbVcoMsbSelecCh1);
            this.groupBox26.Controls.Add(this.cbAutoBypassResetCh1);
            this.groupBox26.Controls.Add(this.label100);
            this.groupBox26.Controls.Add(this.label101);
            this.groupBox26.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox26.Location = new System.Drawing.Point(8, 96);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(240, 85);
            this.groupBox26.TabIndex = 53;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "R95/101_Channel1";
            // 
            // cbVcoMsbSelecCh1
            // 
            this.cbVcoMsbSelecCh1.FormattingEnabled = true;
            this.cbVcoMsbSelecCh1.Location = new System.Drawing.Point(145, 51);
            this.cbVcoMsbSelecCh1.Name = "cbVcoMsbSelecCh1";
            this.cbVcoMsbSelecCh1.Size = new System.Drawing.Size(88, 26);
            this.cbVcoMsbSelecCh1.TabIndex = 33;
            this.cbVcoMsbSelecCh1.SelectedIndexChanged += new System.EventHandler(this.cbVcoMsbSelecCh1_SelectedIndexChanged);
            // 
            // cbAutoBypassResetCh1
            // 
            this.cbAutoBypassResetCh1.FormattingEnabled = true;
            this.cbAutoBypassResetCh1.Location = new System.Drawing.Point(145, 20);
            this.cbAutoBypassResetCh1.Name = "cbAutoBypassResetCh1";
            this.cbAutoBypassResetCh1.Size = new System.Drawing.Size(88, 26);
            this.cbAutoBypassResetCh1.TabIndex = 32;
            this.cbAutoBypassResetCh1.SelectedIndexChanged += new System.EventHandler(this.cbAutoBypassResetCh1_SelectedIndexChanged);
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.BackColor = System.Drawing.Color.Transparent;
            this.label100.Location = new System.Drawing.Point(3, 55);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(164, 18);
            this.label100.TabIndex = 22;
            this.label100.Text = "VCO MSB selection：";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.BackColor = System.Drawing.Color.Transparent;
            this.label101.Location = new System.Drawing.Point(3, 24);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(178, 18);
            this.label101.TabIndex = 21;
            this.label101.Text = "Auto bypass soft reset：";
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.cbVcoMsbSelecCh0);
            this.groupBox25.Controls.Add(this.cbAutoBypassResetCh0);
            this.groupBox25.Controls.Add(this.label95);
            this.groupBox25.Controls.Add(this.label96);
            this.groupBox25.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox25.Location = new System.Drawing.Point(5, 5);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(240, 85);
            this.groupBox25.TabIndex = 52;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "R63/69_Channel0";
            // 
            // cbVcoMsbSelecCh0
            // 
            this.cbVcoMsbSelecCh0.FormattingEnabled = true;
            this.cbVcoMsbSelecCh0.Location = new System.Drawing.Point(145, 51);
            this.cbVcoMsbSelecCh0.Name = "cbVcoMsbSelecCh0";
            this.cbVcoMsbSelecCh0.Size = new System.Drawing.Size(88, 26);
            this.cbVcoMsbSelecCh0.TabIndex = 33;
            this.cbVcoMsbSelecCh0.SelectedIndexChanged += new System.EventHandler(this.cbVcoMsbSelecCh0_SelectedIndexChanged);
            // 
            // cbAutoBypassResetCh0
            // 
            this.cbAutoBypassResetCh0.FormattingEnabled = true;
            this.cbAutoBypassResetCh0.Location = new System.Drawing.Point(145, 20);
            this.cbAutoBypassResetCh0.Name = "cbAutoBypassResetCh0";
            this.cbAutoBypassResetCh0.Size = new System.Drawing.Size(88, 26);
            this.cbAutoBypassResetCh0.TabIndex = 32;
            this.cbAutoBypassResetCh0.SelectedIndexChanged += new System.EventHandler(this.cbAutoBypassResetCh0_SelectedIndexChanged);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.BackColor = System.Drawing.Color.Transparent;
            this.label95.Location = new System.Drawing.Point(3, 55);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(164, 18);
            this.label95.TabIndex = 22;
            this.label95.Text = "VCO MSB selection：";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.BackColor = System.Drawing.Color.Transparent;
            this.label96.Location = new System.Drawing.Point(3, 24);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(178, 18);
            this.label96.TabIndex = 21;
            this.label96.Text = "Auto bypass soft reset：";
            // 
            // tpRt145Control
            // 
            this.tpRt145Control.Controls.Add(this.groupBox23);
            this.tpRt145Control.Controls.Add(this.groupBox13);
            this.tpRt145Control.Controls.Add(this.groupBox8);
            this.tpRt145Control.Controls.Add(this.groupBox7);
            this.tpRt145Control.Controls.Add(this.groupBox24);
            this.tpRt145Control.Controls.Add(this.groupBox22);
            this.tpRt145Control.Controls.Add(this.groupBox11);
            this.tpRt145Control.Location = new System.Drawing.Point(4, 30);
            this.tpRt145Control.Name = "tpRt145Control";
            this.tpRt145Control.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt145Control.Size = new System.Drawing.Size(932, 526);
            this.tpRt145Control.TabIndex = 5;
            this.tpRt145Control.Text = "DDMI & Power control";
            this.tpRt145Control.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.label97);
            this.groupBox23.Controls.Add(this.cbBurninEnCh3);
            this.groupBox23.Controls.Add(this.cbBurninEnCh2);
            this.groupBox23.Controls.Add(this.cbBurninEnCh1);
            this.groupBox23.Controls.Add(this.label98);
            this.groupBox23.Controls.Add(this.cbBurninEnCh0);
            this.groupBox23.Controls.Add(this.label99);
            this.groupBox23.Controls.Add(this.cbBurninCurrentCh3);
            this.groupBox23.Controls.Add(this.cbBurninCurrentCh2);
            this.groupBox23.Controls.Add(this.cbBurninCurrentCh1);
            this.groupBox23.Controls.Add(this.cbBurninCurrentCh0);
            this.groupBox23.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox23.Location = new System.Drawing.Point(625, 357);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(300, 170);
            this.groupBox23.TabIndex = 64;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "R43~46_VCSEL burn-in mode control";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label97.Location = new System.Drawing.Point(14, 150);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(120, 15);
            this.label97.TabIndex = 49;
            this.label97.Text = "False / True：off / on";
            // 
            // cbBurninEnCh3
            // 
            this.cbBurninEnCh3.AutoSize = true;
            this.cbBurninEnCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbBurninEnCh3.Location = new System.Drawing.Point(17, 127);
            this.cbBurninEnCh3.Name = "cbBurninEnCh3";
            this.cbBurninEnCh3.Size = new System.Drawing.Size(113, 22);
            this.cbBurninEnCh3.TabIndex = 48;
            this.cbBurninEnCh3.Text = "Channel 3：";
            this.cbBurninEnCh3.UseVisualStyleBackColor = true;
            this.cbBurninEnCh3.CheckedChanged += new System.EventHandler(this.cbBurninEnCh3_CheckedChanged);
            // 
            // cbBurninEnCh2
            // 
            this.cbBurninEnCh2.AutoSize = true;
            this.cbBurninEnCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbBurninEnCh2.Location = new System.Drawing.Point(17, 99);
            this.cbBurninEnCh2.Name = "cbBurninEnCh2";
            this.cbBurninEnCh2.Size = new System.Drawing.Size(113, 22);
            this.cbBurninEnCh2.TabIndex = 47;
            this.cbBurninEnCh2.Text = "Channel 2：";
            this.cbBurninEnCh2.UseVisualStyleBackColor = true;
            this.cbBurninEnCh2.CheckedChanged += new System.EventHandler(this.cbBurninEnCh2_CheckedChanged);
            // 
            // cbBurninEnCh1
            // 
            this.cbBurninEnCh1.AutoSize = true;
            this.cbBurninEnCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbBurninEnCh1.Location = new System.Drawing.Point(17, 71);
            this.cbBurninEnCh1.Name = "cbBurninEnCh1";
            this.cbBurninEnCh1.Size = new System.Drawing.Size(113, 22);
            this.cbBurninEnCh1.TabIndex = 46;
            this.cbBurninEnCh1.Text = "Channel 1：";
            this.cbBurninEnCh1.UseVisualStyleBackColor = true;
            this.cbBurninEnCh1.CheckedChanged += new System.EventHandler(this.cbBurninEnCh1_CheckedChanged);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.BackColor = System.Drawing.Color.Transparent;
            this.label98.Location = new System.Drawing.Point(11, 22);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(123, 18);
            this.label98.TabIndex = 45;
            this.label98.Text = "Burn-In enable：";
            // 
            // cbBurninEnCh0
            // 
            this.cbBurninEnCh0.AutoSize = true;
            this.cbBurninEnCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbBurninEnCh0.Location = new System.Drawing.Point(17, 43);
            this.cbBurninEnCh0.Name = "cbBurninEnCh0";
            this.cbBurninEnCh0.Size = new System.Drawing.Size(113, 22);
            this.cbBurninEnCh0.TabIndex = 44;
            this.cbBurninEnCh0.Text = "Channel 0：";
            this.cbBurninEnCh0.UseVisualStyleBackColor = true;
            this.cbBurninEnCh0.CheckedChanged += new System.EventHandler(this.cbBurninEnCh0_CheckedChanged);
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.BackColor = System.Drawing.Color.Transparent;
            this.label99.Location = new System.Drawing.Point(140, 22);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(123, 18);
            this.label99.TabIndex = 34;
            this.label99.Text = "Burn-In current：";
            // 
            // cbBurninCurrentCh3
            // 
            this.cbBurninCurrentCh3.FormattingEnabled = true;
            this.cbBurninCurrentCh3.Location = new System.Drawing.Point(140, 126);
            this.cbBurninCurrentCh3.Name = "cbBurninCurrentCh3";
            this.cbBurninCurrentCh3.Size = new System.Drawing.Size(130, 26);
            this.cbBurninCurrentCh3.TabIndex = 32;
            this.cbBurninCurrentCh3.SelectedIndexChanged += new System.EventHandler(this.cbBurninCurrentCh3_SelectedIndexChanged);
            // 
            // cbBurninCurrentCh2
            // 
            this.cbBurninCurrentCh2.FormattingEnabled = true;
            this.cbBurninCurrentCh2.Location = new System.Drawing.Point(140, 98);
            this.cbBurninCurrentCh2.Name = "cbBurninCurrentCh2";
            this.cbBurninCurrentCh2.Size = new System.Drawing.Size(130, 26);
            this.cbBurninCurrentCh2.TabIndex = 30;
            this.cbBurninCurrentCh2.SelectedIndexChanged += new System.EventHandler(this.cbBurninCurrentCh2_SelectedIndexChanged);
            // 
            // cbBurninCurrentCh1
            // 
            this.cbBurninCurrentCh1.FormattingEnabled = true;
            this.cbBurninCurrentCh1.Location = new System.Drawing.Point(140, 70);
            this.cbBurninCurrentCh1.Name = "cbBurninCurrentCh1";
            this.cbBurninCurrentCh1.Size = new System.Drawing.Size(130, 26);
            this.cbBurninCurrentCh1.TabIndex = 28;
            this.cbBurninCurrentCh1.SelectedIndexChanged += new System.EventHandler(this.cbBurninCurrentCh1_SelectedIndexChanged);
            // 
            // cbBurninCurrentCh0
            // 
            this.cbBurninCurrentCh0.FormattingEnabled = true;
            this.cbBurninCurrentCh0.Location = new System.Drawing.Point(140, 42);
            this.cbBurninCurrentCh0.Name = "cbBurninCurrentCh0";
            this.cbBurninCurrentCh0.Size = new System.Drawing.Size(130, 26);
            this.cbBurninCurrentCh0.TabIndex = 19;
            this.cbBurninCurrentCh0.SelectedIndexChanged += new System.EventHandler(this.cbBurninCurrentCh0_SelectedIndexChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label66);
            this.groupBox13.Controls.Add(this.cbModulationPowerDownCh3);
            this.groupBox13.Controls.Add(this.cbModulationPowerDownCh2);
            this.groupBox13.Controls.Add(this.cbModulationPowerDownCh1);
            this.groupBox13.Controls.Add(this.label71);
            this.groupBox13.Controls.Add(this.cbModulationPowerDownCh0);
            this.groupBox13.Controls.Add(this.label94);
            this.groupBox13.Controls.Add(this.cbModulationCh3);
            this.groupBox13.Controls.Add(this.cbModulationCh2);
            this.groupBox13.Controls.Add(this.cbModulationCh1);
            this.groupBox13.Controls.Add(this.cbModulationCh0);
            this.groupBox13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox13.Location = new System.Drawing.Point(625, 181);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(300, 170);
            this.groupBox13.TabIndex = 63;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "R39~42_VCSEL modulation";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label66.Location = new System.Drawing.Point(14, 150);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(120, 15);
            this.label66.TabIndex = 49;
            this.label66.Text = "False / True：on / off";
            // 
            // cbModulationPowerDownCh3
            // 
            this.cbModulationPowerDownCh3.AutoSize = true;
            this.cbModulationPowerDownCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbModulationPowerDownCh3.Location = new System.Drawing.Point(17, 127);
            this.cbModulationPowerDownCh3.Name = "cbModulationPowerDownCh3";
            this.cbModulationPowerDownCh3.Size = new System.Drawing.Size(113, 22);
            this.cbModulationPowerDownCh3.TabIndex = 48;
            this.cbModulationPowerDownCh3.Text = "Channel 3：";
            this.cbModulationPowerDownCh3.UseVisualStyleBackColor = true;
            this.cbModulationPowerDownCh3.CheckedChanged += new System.EventHandler(this.cbModulationPowerDownCh3_CheckedChanged);
            // 
            // cbModulationPowerDownCh2
            // 
            this.cbModulationPowerDownCh2.AutoSize = true;
            this.cbModulationPowerDownCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbModulationPowerDownCh2.Location = new System.Drawing.Point(17, 99);
            this.cbModulationPowerDownCh2.Name = "cbModulationPowerDownCh2";
            this.cbModulationPowerDownCh2.Size = new System.Drawing.Size(113, 22);
            this.cbModulationPowerDownCh2.TabIndex = 47;
            this.cbModulationPowerDownCh2.Text = "Channel 2：";
            this.cbModulationPowerDownCh2.UseVisualStyleBackColor = true;
            this.cbModulationPowerDownCh2.CheckedChanged += new System.EventHandler(this.cbModulationPowerDownCh2_CheckedChanged);
            // 
            // cbModulationPowerDownCh1
            // 
            this.cbModulationPowerDownCh1.AutoSize = true;
            this.cbModulationPowerDownCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbModulationPowerDownCh1.Location = new System.Drawing.Point(17, 71);
            this.cbModulationPowerDownCh1.Name = "cbModulationPowerDownCh1";
            this.cbModulationPowerDownCh1.Size = new System.Drawing.Size(113, 22);
            this.cbModulationPowerDownCh1.TabIndex = 46;
            this.cbModulationPowerDownCh1.Text = "Channel 1：";
            this.cbModulationPowerDownCh1.UseVisualStyleBackColor = true;
            this.cbModulationPowerDownCh1.CheckedChanged += new System.EventHandler(this.cbModulationPowerDownCh1_CheckedChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Location = new System.Drawing.Point(11, 22);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(188, 18);
            this.label71.TabIndex = 45;
            this.label71.Text = "Modulation power down：";
            // 
            // cbModulationPowerDownCh0
            // 
            this.cbModulationPowerDownCh0.AutoSize = true;
            this.cbModulationPowerDownCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbModulationPowerDownCh0.Location = new System.Drawing.Point(17, 43);
            this.cbModulationPowerDownCh0.Name = "cbModulationPowerDownCh0";
            this.cbModulationPowerDownCh0.Size = new System.Drawing.Size(113, 22);
            this.cbModulationPowerDownCh0.TabIndex = 44;
            this.cbModulationPowerDownCh0.Text = "Channel 0：";
            this.cbModulationPowerDownCh0.UseVisualStyleBackColor = true;
            this.cbModulationPowerDownCh0.CheckedChanged += new System.EventHandler(this.cbModulationPowerDownCh0_CheckedChanged);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.BackColor = System.Drawing.Color.Transparent;
            this.label94.Location = new System.Drawing.Point(140, 22);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(151, 18);
            this.label94.TabIndex = 34;
            this.label94.Text = "Modulation current：";
            // 
            // cbModulationCh3
            // 
            this.cbModulationCh3.FormattingEnabled = true;
            this.cbModulationCh3.Location = new System.Drawing.Point(140, 126);
            this.cbModulationCh3.Name = "cbModulationCh3";
            this.cbModulationCh3.Size = new System.Drawing.Size(130, 26);
            this.cbModulationCh3.TabIndex = 32;
            this.cbModulationCh3.SelectedIndexChanged += new System.EventHandler(this.cbModulationCh3_SelectedIndexChanged);
            // 
            // cbModulationCh2
            // 
            this.cbModulationCh2.FormattingEnabled = true;
            this.cbModulationCh2.Location = new System.Drawing.Point(140, 98);
            this.cbModulationCh2.Name = "cbModulationCh2";
            this.cbModulationCh2.Size = new System.Drawing.Size(130, 26);
            this.cbModulationCh2.TabIndex = 30;
            this.cbModulationCh2.SelectedIndexChanged += new System.EventHandler(this.cbModulationCh2_SelectedIndexChanged);
            // 
            // cbModulationCh1
            // 
            this.cbModulationCh1.FormattingEnabled = true;
            this.cbModulationCh1.Location = new System.Drawing.Point(140, 70);
            this.cbModulationCh1.Name = "cbModulationCh1";
            this.cbModulationCh1.Size = new System.Drawing.Size(130, 26);
            this.cbModulationCh1.TabIndex = 28;
            this.cbModulationCh1.SelectedIndexChanged += new System.EventHandler(this.cbModulationCh1_SelectedIndexChanged);
            // 
            // cbModulationCh0
            // 
            this.cbModulationCh0.FormattingEnabled = true;
            this.cbModulationCh0.Location = new System.Drawing.Point(140, 42);
            this.cbModulationCh0.Name = "cbModulationCh0";
            this.cbModulationCh0.Size = new System.Drawing.Size(130, 26);
            this.cbModulationCh0.TabIndex = 19;
            this.cbModulationCh0.SelectedIndexChanged += new System.EventHandler(this.cbModulationCh0_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label86);
            this.groupBox8.Controls.Add(this.cbIbiasPowerDownCh3);
            this.groupBox8.Controls.Add(this.cbIbiasPowerDownCh2);
            this.groupBox8.Controls.Add(this.cbIbiasPowerDownCh1);
            this.groupBox8.Controls.Add(this.label85);
            this.groupBox8.Controls.Add(this.cbIbiasPowerDownCh0);
            this.groupBox8.Controls.Add(this.label82);
            this.groupBox8.Controls.Add(this.cbIbiasCurrentCh3);
            this.groupBox8.Controls.Add(this.cbIbiasCurrentCh2);
            this.groupBox8.Controls.Add(this.cbIbiasCurrentCh1);
            this.groupBox8.Controls.Add(this.cbIbiasCurrentCh0);
            this.groupBox8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox8.Location = new System.Drawing.Point(625, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(300, 170);
            this.groupBox8.TabIndex = 62;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "R35~38_VCSEL iBias";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label86.Location = new System.Drawing.Point(14, 150);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(120, 15);
            this.label86.TabIndex = 49;
            this.label86.Text = "False / True：on / off";
            // 
            // cbIbiasPowerDownCh3
            // 
            this.cbIbiasPowerDownCh3.AutoSize = true;
            this.cbIbiasPowerDownCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbIbiasPowerDownCh3.Location = new System.Drawing.Point(17, 127);
            this.cbIbiasPowerDownCh3.Name = "cbIbiasPowerDownCh3";
            this.cbIbiasPowerDownCh3.Size = new System.Drawing.Size(113, 22);
            this.cbIbiasPowerDownCh3.TabIndex = 48;
            this.cbIbiasPowerDownCh3.Text = "Channel 3：";
            this.cbIbiasPowerDownCh3.UseVisualStyleBackColor = true;
            this.cbIbiasPowerDownCh3.CheckedChanged += new System.EventHandler(this.cbIbiasPowerDownCh3_CheckedChanged);
            // 
            // cbIbiasPowerDownCh2
            // 
            this.cbIbiasPowerDownCh2.AutoSize = true;
            this.cbIbiasPowerDownCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbIbiasPowerDownCh2.Location = new System.Drawing.Point(17, 99);
            this.cbIbiasPowerDownCh2.Name = "cbIbiasPowerDownCh2";
            this.cbIbiasPowerDownCh2.Size = new System.Drawing.Size(113, 22);
            this.cbIbiasPowerDownCh2.TabIndex = 47;
            this.cbIbiasPowerDownCh2.Text = "Channel 2：";
            this.cbIbiasPowerDownCh2.UseVisualStyleBackColor = true;
            this.cbIbiasPowerDownCh2.CheckedChanged += new System.EventHandler(this.cbIbiasPowerDownCh2_CheckedChanged);
            // 
            // cbIbiasPowerDownCh1
            // 
            this.cbIbiasPowerDownCh1.AutoSize = true;
            this.cbIbiasPowerDownCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbIbiasPowerDownCh1.Location = new System.Drawing.Point(17, 71);
            this.cbIbiasPowerDownCh1.Name = "cbIbiasPowerDownCh1";
            this.cbIbiasPowerDownCh1.Size = new System.Drawing.Size(113, 22);
            this.cbIbiasPowerDownCh1.TabIndex = 46;
            this.cbIbiasPowerDownCh1.Text = "Channel 1：";
            this.cbIbiasPowerDownCh1.UseVisualStyleBackColor = true;
            this.cbIbiasPowerDownCh1.CheckedChanged += new System.EventHandler(this.cbIbiasPowerDownCh1_CheckedChanged);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.BackColor = System.Drawing.Color.Transparent;
            this.label85.Location = new System.Drawing.Point(11, 22);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(148, 18);
            this.label85.TabIndex = 45;
            this.label85.Text = "iBias power down：";
            // 
            // cbIbiasPowerDownCh0
            // 
            this.cbIbiasPowerDownCh0.AutoSize = true;
            this.cbIbiasPowerDownCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbIbiasPowerDownCh0.Location = new System.Drawing.Point(17, 43);
            this.cbIbiasPowerDownCh0.Name = "cbIbiasPowerDownCh0";
            this.cbIbiasPowerDownCh0.Size = new System.Drawing.Size(113, 22);
            this.cbIbiasPowerDownCh0.TabIndex = 44;
            this.cbIbiasPowerDownCh0.Text = "Channel 0：";
            this.cbIbiasPowerDownCh0.UseVisualStyleBackColor = true;
            this.cbIbiasPowerDownCh0.CheckedChanged += new System.EventHandler(this.cbIbiasPowerDownCh0_CheckedChanged);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.BackColor = System.Drawing.Color.Transparent;
            this.label82.Location = new System.Drawing.Point(140, 22);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(111, 18);
            this.label82.TabIndex = 34;
            this.label82.Text = "iBias current：";
            // 
            // cbIbiasCurrentCh3
            // 
            this.cbIbiasCurrentCh3.FormattingEnabled = true;
            this.cbIbiasCurrentCh3.Location = new System.Drawing.Point(140, 126);
            this.cbIbiasCurrentCh3.Name = "cbIbiasCurrentCh3";
            this.cbIbiasCurrentCh3.Size = new System.Drawing.Size(130, 26);
            this.cbIbiasCurrentCh3.TabIndex = 32;
            this.cbIbiasCurrentCh3.SelectedIndexChanged += new System.EventHandler(this.cbIbiasCurrentCh3_SelectedIndexChanged);
            // 
            // cbIbiasCurrentCh2
            // 
            this.cbIbiasCurrentCh2.FormattingEnabled = true;
            this.cbIbiasCurrentCh2.Location = new System.Drawing.Point(140, 98);
            this.cbIbiasCurrentCh2.Name = "cbIbiasCurrentCh2";
            this.cbIbiasCurrentCh2.Size = new System.Drawing.Size(130, 26);
            this.cbIbiasCurrentCh2.TabIndex = 30;
            this.cbIbiasCurrentCh2.SelectedIndexChanged += new System.EventHandler(this.cbIbiasCurrentCh2_SelectedIndexChanged);
            // 
            // cbIbiasCurrentCh1
            // 
            this.cbIbiasCurrentCh1.FormattingEnabled = true;
            this.cbIbiasCurrentCh1.Location = new System.Drawing.Point(140, 70);
            this.cbIbiasCurrentCh1.Name = "cbIbiasCurrentCh1";
            this.cbIbiasCurrentCh1.Size = new System.Drawing.Size(130, 26);
            this.cbIbiasCurrentCh1.TabIndex = 28;
            this.cbIbiasCurrentCh1.SelectedIndexChanged += new System.EventHandler(this.cbIbiasCurrentCh1_SelectedIndexChanged);
            // 
            // cbIbiasCurrentCh0
            // 
            this.cbIbiasCurrentCh0.FormattingEnabled = true;
            this.cbIbiasCurrentCh0.Location = new System.Drawing.Point(140, 42);
            this.cbIbiasCurrentCh0.Name = "cbIbiasCurrentCh0";
            this.cbIbiasCurrentCh0.Size = new System.Drawing.Size(130, 26);
            this.cbIbiasCurrentCh0.TabIndex = 19;
            this.cbIbiasCurrentCh0.SelectedIndexChanged += new System.EventHandler(this.cbIbiasCurrentCh0_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.cbDeemphasisEnCh3);
            this.groupBox7.Controls.Add(this.cbDeemphasisEnCh1);
            this.groupBox7.Controls.Add(this.cbDeemphasisEnCh0);
            this.groupBox7.Controls.Add(this.cbDeemphasisEnCh2);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.cbDdmiChannelSelect);
            this.groupBox7.Controls.Add(this.cbDdmiAdcPowerControl);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox7.Location = new System.Drawing.Point(5, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(300, 141);
            this.groupBox7.TabIndex = 61;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "R28_ADC / De-emphasis control";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(3, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 18);
            this.label11.TabIndex = 37;
            this.label11.Text = "De-emphasis enable：";
            // 
            // cbDeemphasisEnCh3
            // 
            this.cbDeemphasisEnCh3.AutoSize = true;
            this.cbDeemphasisEnCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbDeemphasisEnCh3.Location = new System.Drawing.Point(174, 103);
            this.cbDeemphasisEnCh3.Name = "cbDeemphasisEnCh3";
            this.cbDeemphasisEnCh3.Size = new System.Drawing.Size(56, 22);
            this.cbDeemphasisEnCh3.TabIndex = 36;
            this.cbDeemphasisEnCh3.Text = "Ch3";
            this.cbDeemphasisEnCh3.UseVisualStyleBackColor = true;
            this.cbDeemphasisEnCh3.CheckedChanged += new System.EventHandler(this.cbDeemphasisEnCh3_CheckedChanged);
            // 
            // cbDeemphasisEnCh1
            // 
            this.cbDeemphasisEnCh1.AutoSize = true;
            this.cbDeemphasisEnCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbDeemphasisEnCh1.Location = new System.Drawing.Point(62, 103);
            this.cbDeemphasisEnCh1.Name = "cbDeemphasisEnCh1";
            this.cbDeemphasisEnCh1.Size = new System.Drawing.Size(56, 22);
            this.cbDeemphasisEnCh1.TabIndex = 34;
            this.cbDeemphasisEnCh1.Text = "Ch1";
            this.cbDeemphasisEnCh1.UseVisualStyleBackColor = true;
            this.cbDeemphasisEnCh1.CheckedChanged += new System.EventHandler(this.cbDeemphasisEnCh1_CheckedChanged);
            // 
            // cbDeemphasisEnCh0
            // 
            this.cbDeemphasisEnCh0.AutoSize = true;
            this.cbDeemphasisEnCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbDeemphasisEnCh0.Location = new System.Drawing.Point(6, 103);
            this.cbDeemphasisEnCh0.Name = "cbDeemphasisEnCh0";
            this.cbDeemphasisEnCh0.Size = new System.Drawing.Size(56, 22);
            this.cbDeemphasisEnCh0.TabIndex = 33;
            this.cbDeemphasisEnCh0.Text = "Ch0";
            this.cbDeemphasisEnCh0.UseVisualStyleBackColor = true;
            this.cbDeemphasisEnCh0.CheckedChanged += new System.EventHandler(this.cbDeemphasisEnCh0_CheckedChanged);
            // 
            // cbDeemphasisEnCh2
            // 
            this.cbDeemphasisEnCh2.AutoSize = true;
            this.cbDeemphasisEnCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbDeemphasisEnCh2.Location = new System.Drawing.Point(118, 103);
            this.cbDeemphasisEnCh2.Name = "cbDeemphasisEnCh2";
            this.cbDeemphasisEnCh2.Size = new System.Drawing.Size(56, 22);
            this.cbDeemphasisEnCh2.TabIndex = 35;
            this.cbDeemphasisEnCh2.Text = "Ch2";
            this.cbDeemphasisEnCh2.UseVisualStyleBackColor = true;
            this.cbDeemphasisEnCh2.CheckedChanged += new System.EventHandler(this.cbDeemphasisEnCh2_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(6, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 15);
            this.label12.TabIndex = 32;
            this.label12.Text = "False / True：off / on";
            // 
            // cbDdmiChannelSelect
            // 
            this.cbDdmiChannelSelect.FormattingEnabled = true;
            this.cbDdmiChannelSelect.Location = new System.Drawing.Point(190, 51);
            this.cbDdmiChannelSelect.Name = "cbDdmiChannelSelect";
            this.cbDdmiChannelSelect.Size = new System.Drawing.Size(105, 26);
            this.cbDdmiChannelSelect.TabIndex = 24;
            this.cbDdmiChannelSelect.SelectedIndexChanged += new System.EventHandler(this.cbDdmiChannelSelect_SelectedIndexChanged);
            // 
            // cbDdmiAdcPowerControl
            // 
            this.cbDdmiAdcPowerControl.FormattingEnabled = true;
            this.cbDdmiAdcPowerControl.Location = new System.Drawing.Point(190, 20);
            this.cbDdmiAdcPowerControl.Name = "cbDdmiAdcPowerControl";
            this.cbDdmiAdcPowerControl.Size = new System.Drawing.Size(105, 26);
            this.cbDdmiAdcPowerControl.TabIndex = 23;
            this.cbDdmiAdcPowerControl.SelectedIndexChanged += new System.EventHandler(this.cbDdmiAdcPowerControl_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(3, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 18);
            this.label13.TabIndex = 26;
            this.label13.Text = "DDMI channel select：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(3, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(191, 18);
            this.label14.TabIndex = 25;
            this.label14.Text = "DDMI adc power control：";
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.label88);
            this.groupBox24.Controls.Add(this.cbAutoTuneClockSpeed);
            this.groupBox24.Controls.Add(this.label89);
            this.groupBox24.Controls.Add(this.cbSampleHoldClockSpeed);
            this.groupBox24.Controls.Add(this.label90);
            this.groupBox24.Controls.Add(this.cbClockLosEnable);
            this.groupBox24.Controls.Add(this.label91);
            this.groupBox24.Controls.Add(this.cbClockAdcEnable);
            this.groupBox24.Controls.Add(this.label92);
            this.groupBox24.Controls.Add(this.cbClockAutoTuneEnable);
            this.groupBox24.Controls.Add(this.label93);
            this.groupBox24.Controls.Add(this.cbRingOscPwd);
            this.groupBox24.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox24.Location = new System.Drawing.Point(315, 225);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(300, 211);
            this.groupBox24.TabIndex = 60;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "R34_Ring oscillator setting";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.BackColor = System.Drawing.Color.Transparent;
            this.label88.Location = new System.Drawing.Point(3, 179);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(178, 18);
            this.label88.TabIndex = 37;
            this.label88.Text = "Auto-tune clock speed：";
            // 
            // cbAutoTuneClockSpeed
            // 
            this.cbAutoTuneClockSpeed.FormattingEnabled = true;
            this.cbAutoTuneClockSpeed.Location = new System.Drawing.Point(200, 175);
            this.cbAutoTuneClockSpeed.Name = "cbAutoTuneClockSpeed";
            this.cbAutoTuneClockSpeed.Size = new System.Drawing.Size(88, 26);
            this.cbAutoTuneClockSpeed.TabIndex = 36;
            this.cbAutoTuneClockSpeed.SelectedIndexChanged += new System.EventHandler(this.cbAutoTuneClockSpeed_SelectedIndexChanged);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.BackColor = System.Drawing.Color.Transparent;
            this.label89.Location = new System.Drawing.Point(3, 148);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(202, 18);
            this.label89.TabIndex = 35;
            this.label89.Text = "Sample Hold clock speed：";
            // 
            // cbSampleHoldClockSpeed
            // 
            this.cbSampleHoldClockSpeed.FormattingEnabled = true;
            this.cbSampleHoldClockSpeed.Location = new System.Drawing.Point(200, 144);
            this.cbSampleHoldClockSpeed.Name = "cbSampleHoldClockSpeed";
            this.cbSampleHoldClockSpeed.Size = new System.Drawing.Size(88, 26);
            this.cbSampleHoldClockSpeed.TabIndex = 34;
            this.cbSampleHoldClockSpeed.SelectedIndexChanged += new System.EventHandler(this.cbSampleHoldClockSpeed_SelectedIndexChanged);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Location = new System.Drawing.Point(3, 117);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(155, 18);
            this.label90.TabIndex = 33;
            this.label90.Text = "Clock LOS enable ：";
            // 
            // cbClockLosEnable
            // 
            this.cbClockLosEnable.FormattingEnabled = true;
            this.cbClockLosEnable.Location = new System.Drawing.Point(200, 113);
            this.cbClockLosEnable.Name = "cbClockLosEnable";
            this.cbClockLosEnable.Size = new System.Drawing.Size(88, 26);
            this.cbClockLosEnable.TabIndex = 32;
            this.cbClockLosEnable.SelectedIndexChanged += new System.EventHandler(this.cbClockLosEnable_SelectedIndexChanged);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.BackColor = System.Drawing.Color.Transparent;
            this.label91.Location = new System.Drawing.Point(3, 86);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(153, 18);
            this.label91.TabIndex = 31;
            this.label91.Text = "Clock ADC enable：";
            // 
            // cbClockAdcEnable
            // 
            this.cbClockAdcEnable.FormattingEnabled = true;
            this.cbClockAdcEnable.Location = new System.Drawing.Point(200, 82);
            this.cbClockAdcEnable.Name = "cbClockAdcEnable";
            this.cbClockAdcEnable.Size = new System.Drawing.Size(88, 26);
            this.cbClockAdcEnable.TabIndex = 30;
            this.cbClockAdcEnable.SelectedIndexChanged += new System.EventHandler(this.cbClockAdcEnable_SelectedIndexChanged);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.BackColor = System.Drawing.Color.Transparent;
            this.label92.Location = new System.Drawing.Point(3, 55);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(184, 18);
            this.label92.TabIndex = 29;
            this.label92.Text = "Clock Auto-tune enable：";
            // 
            // cbClockAutoTuneEnable
            // 
            this.cbClockAutoTuneEnable.FormattingEnabled = true;
            this.cbClockAutoTuneEnable.Location = new System.Drawing.Point(200, 51);
            this.cbClockAutoTuneEnable.Name = "cbClockAutoTuneEnable";
            this.cbClockAutoTuneEnable.Size = new System.Drawing.Size(88, 26);
            this.cbClockAutoTuneEnable.TabIndex = 28;
            this.cbClockAutoTuneEnable.SelectedIndexChanged += new System.EventHandler(this.cbClockAutoTuneEnable_SelectedIndexChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.BackColor = System.Drawing.Color.Transparent;
            this.label93.Location = new System.Drawing.Point(3, 24);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(210, 18);
            this.label93.TabIndex = 21;
            this.label93.Text = "Ring oscillator power down：";
            // 
            // cbRingOscPwd
            // 
            this.cbRingOscPwd.FormattingEnabled = true;
            this.cbRingOscPwd.Location = new System.Drawing.Point(200, 20);
            this.cbRingOscPwd.Name = "cbRingOscPwd";
            this.cbRingOscPwd.Size = new System.Drawing.Size(88, 26);
            this.cbRingOscPwd.TabIndex = 19;
            this.cbRingOscPwd.SelectedIndexChanged += new System.EventHandler(this.cbRingOscPwd_SelectedIndexChanged);
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.cbAeqPwdCh3);
            this.groupBox22.Controls.Add(this.cbAeqPwdCh1);
            this.groupBox22.Controls.Add(this.cbAeqPwdCh0);
            this.groupBox22.Controls.Add(this.cbAeqPwdCh2);
            this.groupBox22.Controls.Add(this.label87);
            this.groupBox22.Controls.Add(this.label16);
            this.groupBox22.Controls.Add(this.label81);
            this.groupBox22.Controls.Add(this.cbCdrPwdCh3);
            this.groupBox22.Controls.Add(this.cbCdrPwdCh1);
            this.groupBox22.Controls.Add(this.cbCdrPwdCh0);
            this.groupBox22.Controls.Add(this.cbCdrPwdCh2);
            this.groupBox22.Controls.Add(this.label80);
            this.groupBox22.Controls.Add(this.lCh0SahPwd);
            this.groupBox22.Controls.Add(this.cbCh0SahPwd);
            this.groupBox22.Controls.Add(this.label84);
            this.groupBox22.Controls.Add(this.cbIgnoreGlobalPwd);
            this.groupBox22.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox22.Location = new System.Drawing.Point(315, 5);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(300, 214);
            this.groupBox22.TabIndex = 58;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "R32/33_Power control";
            // 
            // cbAeqPwdCh3
            // 
            this.cbAeqPwdCh3.AutoSize = true;
            this.cbAeqPwdCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbAeqPwdCh3.Location = new System.Drawing.Point(192, 170);
            this.cbAeqPwdCh3.Name = "cbAeqPwdCh3";
            this.cbAeqPwdCh3.Size = new System.Drawing.Size(56, 22);
            this.cbAeqPwdCh3.TabIndex = 47;
            this.cbAeqPwdCh3.Text = "Ch3";
            this.cbAeqPwdCh3.UseVisualStyleBackColor = true;
            this.cbAeqPwdCh3.CheckedChanged += new System.EventHandler(this.cbAeqPwdCh3_CheckedChanged);
            // 
            // cbAeqPwdCh1
            // 
            this.cbAeqPwdCh1.AutoSize = true;
            this.cbAeqPwdCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbAeqPwdCh1.Location = new System.Drawing.Point(68, 170);
            this.cbAeqPwdCh1.Name = "cbAeqPwdCh1";
            this.cbAeqPwdCh1.Size = new System.Drawing.Size(56, 22);
            this.cbAeqPwdCh1.TabIndex = 45;
            this.cbAeqPwdCh1.Text = "Ch1";
            this.cbAeqPwdCh1.UseVisualStyleBackColor = true;
            this.cbAeqPwdCh1.CheckedChanged += new System.EventHandler(this.cbAeqPwdCh1_CheckedChanged);
            // 
            // cbAeqPwdCh0
            // 
            this.cbAeqPwdCh0.AutoSize = true;
            this.cbAeqPwdCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbAeqPwdCh0.Location = new System.Drawing.Point(6, 170);
            this.cbAeqPwdCh0.Name = "cbAeqPwdCh0";
            this.cbAeqPwdCh0.Size = new System.Drawing.Size(56, 22);
            this.cbAeqPwdCh0.TabIndex = 44;
            this.cbAeqPwdCh0.Text = "Ch0";
            this.cbAeqPwdCh0.UseVisualStyleBackColor = true;
            this.cbAeqPwdCh0.CheckedChanged += new System.EventHandler(this.cbAeqPwdCh0_CheckedChanged);
            // 
            // cbAeqPwdCh2
            // 
            this.cbAeqPwdCh2.AutoSize = true;
            this.cbAeqPwdCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbAeqPwdCh2.Location = new System.Drawing.Point(130, 170);
            this.cbAeqPwdCh2.Name = "cbAeqPwdCh2";
            this.cbAeqPwdCh2.Size = new System.Drawing.Size(56, 22);
            this.cbAeqPwdCh2.TabIndex = 46;
            this.cbAeqPwdCh2.Text = "Ch2";
            this.cbAeqPwdCh2.UseVisualStyleBackColor = true;
            this.cbAeqPwdCh2.CheckedChanged += new System.EventHandler(this.cbAeqPwdCh2_CheckedChanged);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label87.Location = new System.Drawing.Point(6, 190);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(217, 15);
            this.label87.TabIndex = 43;
            this.label87.Text = "False / True：AEQ power down on / off";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(3, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 18);
            this.label16.TabIndex = 42;
            this.label16.Text = "AEQ power down：";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.BackColor = System.Drawing.Color.Transparent;
            this.label81.Location = new System.Drawing.Point(3, 84);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(147, 18);
            this.label81.TabIndex = 41;
            this.label81.Text = "CDR power down：";
            // 
            // cbCdrPwdCh3
            // 
            this.cbCdrPwdCh3.AutoSize = true;
            this.cbCdrPwdCh3.Checked = true;
            this.cbCdrPwdCh3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCdrPwdCh3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCdrPwdCh3.Location = new System.Drawing.Point(192, 104);
            this.cbCdrPwdCh3.Name = "cbCdrPwdCh3";
            this.cbCdrPwdCh3.Size = new System.Drawing.Size(56, 22);
            this.cbCdrPwdCh3.TabIndex = 40;
            this.cbCdrPwdCh3.Text = "Ch3";
            this.cbCdrPwdCh3.UseVisualStyleBackColor = true;
            this.cbCdrPwdCh3.CheckedChanged += new System.EventHandler(this.cbCdrPwdCh3_CheckedChanged);
            // 
            // cbCdrPwdCh1
            // 
            this.cbCdrPwdCh1.AutoSize = true;
            this.cbCdrPwdCh1.Checked = true;
            this.cbCdrPwdCh1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCdrPwdCh1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCdrPwdCh1.Location = new System.Drawing.Point(68, 104);
            this.cbCdrPwdCh1.Name = "cbCdrPwdCh1";
            this.cbCdrPwdCh1.Size = new System.Drawing.Size(56, 22);
            this.cbCdrPwdCh1.TabIndex = 38;
            this.cbCdrPwdCh1.Text = "Ch1";
            this.cbCdrPwdCh1.UseVisualStyleBackColor = true;
            this.cbCdrPwdCh1.CheckedChanged += new System.EventHandler(this.cbCdrPwdCh1_CheckedChanged);
            // 
            // cbCdrPwdCh0
            // 
            this.cbCdrPwdCh0.AutoSize = true;
            this.cbCdrPwdCh0.Checked = true;
            this.cbCdrPwdCh0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCdrPwdCh0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCdrPwdCh0.Location = new System.Drawing.Point(6, 104);
            this.cbCdrPwdCh0.Name = "cbCdrPwdCh0";
            this.cbCdrPwdCh0.Size = new System.Drawing.Size(56, 22);
            this.cbCdrPwdCh0.TabIndex = 37;
            this.cbCdrPwdCh0.Text = "Ch0";
            this.cbCdrPwdCh0.UseVisualStyleBackColor = true;
            this.cbCdrPwdCh0.CheckedChanged += new System.EventHandler(this.cbCdrPwdCh0_CheckedChanged);
            // 
            // cbCdrPwdCh2
            // 
            this.cbCdrPwdCh2.AutoSize = true;
            this.cbCdrPwdCh2.Checked = true;
            this.cbCdrPwdCh2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCdrPwdCh2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCdrPwdCh2.Location = new System.Drawing.Point(130, 104);
            this.cbCdrPwdCh2.Name = "cbCdrPwdCh2";
            this.cbCdrPwdCh2.Size = new System.Drawing.Size(56, 22);
            this.cbCdrPwdCh2.TabIndex = 39;
            this.cbCdrPwdCh2.Text = "Ch2";
            this.cbCdrPwdCh2.UseVisualStyleBackColor = true;
            this.cbCdrPwdCh2.CheckedChanged += new System.EventHandler(this.cbCdrPwdCh2_CheckedChanged);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label80.Location = new System.Drawing.Point(6, 124);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(120, 15);
            this.label80.TabIndex = 36;
            this.label80.Text = "False / True：on / off";
            // 
            // lCh0SahPwd
            // 
            this.lCh0SahPwd.AutoSize = true;
            this.lCh0SahPwd.BackColor = System.Drawing.Color.Transparent;
            this.lCh0SahPwd.Location = new System.Drawing.Point(3, 53);
            this.lCh0SahPwd.Name = "lCh0SahPwd";
            this.lCh0SahPwd.Size = new System.Drawing.Size(178, 18);
            this.lCh0SahPwd.TabIndex = 29;
            this.lCh0SahPwd.Text = "Ch0 SAH power down：";
            // 
            // cbCh0SahPwd
            // 
            this.cbCh0SahPwd.FormattingEnabled = true;
            this.cbCh0SahPwd.Location = new System.Drawing.Point(200, 49);
            this.cbCh0SahPwd.Name = "cbCh0SahPwd";
            this.cbCh0SahPwd.Size = new System.Drawing.Size(88, 26);
            this.cbCh0SahPwd.TabIndex = 28;
            this.cbCh0SahPwd.SelectedIndexChanged += new System.EventHandler(this.cbCh0SahPwd_SelectedIndexChanged);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.BackColor = System.Drawing.Color.Transparent;
            this.label84.Location = new System.Drawing.Point(3, 22);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(201, 18);
            this.label84.TabIndex = 21;
            this.label84.Text = "Ignore global power down：";
            // 
            // cbIgnoreGlobalPwd
            // 
            this.cbIgnoreGlobalPwd.FormattingEnabled = true;
            this.cbIgnoreGlobalPwd.Location = new System.Drawing.Point(200, 18);
            this.cbIgnoreGlobalPwd.Name = "cbIgnoreGlobalPwd";
            this.cbIgnoreGlobalPwd.Size = new System.Drawing.Size(88, 26);
            this.cbIgnoreGlobalPwd.TabIndex = 19;
            this.cbIgnoreGlobalPwd.SelectedIndexChanged += new System.EventHandler(this.cbIgnoreGlobalPwd_SelectedIndexChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label76);
            this.groupBox11.Controls.Add(this.cbDdmiReset);
            this.groupBox11.Controls.Add(this.label77);
            this.groupBox11.Controls.Add(this.cbEnableVf);
            this.groupBox11.Controls.Add(this.label78);
            this.groupBox11.Controls.Add(this.cbSourceIdoPwd);
            this.groupBox11.Controls.Add(this.label79);
            this.groupBox11.Controls.Add(this.cbTemperaturePwd);
            this.groupBox11.Controls.Add(this.label65);
            this.groupBox11.Controls.Add(this.cbDdmiPwd);
            this.groupBox11.Controls.Add(this.label75);
            this.groupBox11.Controls.Add(this.cbTemperatureSlope);
            this.groupBox11.Controls.Add(this.label74);
            this.groupBox11.Controls.Add(this.cbTemperatureOffset);
            this.groupBox11.Controls.Add(this.label73);
            this.groupBox11.Controls.Add(this.cbIdoModeSelect);
            this.groupBox11.Controls.Add(this.label72);
            this.groupBox11.Controls.Add(this.cbDdmiIdoRefSource);
            this.groupBox11.Controls.Add(this.label69);
            this.groupBox11.Controls.Add(this.cbRssiLevelSelect);
            this.groupBox11.Controls.Add(this.label68);
            this.groupBox11.Controls.Add(this.cbRssiMode);
            this.groupBox11.Controls.Add(this.label67);
            this.groupBox11.Controls.Add(this.cbRssiAgcClockSpeed);
            this.groupBox11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox11.Location = new System.Drawing.Point(6, 152);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(300, 368);
            this.groupBox11.TabIndex = 57;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "R29~31_DDMI control";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Location = new System.Drawing.Point(3, 341);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(103, 18);
            this.label76.TabIndex = 45;
            this.label76.Text = "DDMI reset：";
            // 
            // cbDdmiReset
            // 
            this.cbDdmiReset.FormattingEnabled = true;
            this.cbDdmiReset.Location = new System.Drawing.Point(190, 337);
            this.cbDdmiReset.Name = "cbDdmiReset";
            this.cbDdmiReset.Size = new System.Drawing.Size(105, 26);
            this.cbDdmiReset.TabIndex = 44;
            this.cbDdmiReset.SelectedIndexChanged += new System.EventHandler(this.cbDdmiReset_SelectedIndexChanged);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Location = new System.Drawing.Point(3, 312);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(88, 18);
            this.label77.TabIndex = 43;
            this.label77.Text = "Enable vf：";
            // 
            // cbEnableVf
            // 
            this.cbEnableVf.FormattingEnabled = true;
            this.cbEnableVf.Location = new System.Drawing.Point(190, 308);
            this.cbEnableVf.Name = "cbEnableVf";
            this.cbEnableVf.Size = new System.Drawing.Size(105, 26);
            this.cbEnableVf.TabIndex = 42;
            this.cbEnableVf.SelectedIndexChanged += new System.EventHandler(this.cbEnableVf_SelectedIndexChanged);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Location = new System.Drawing.Point(3, 283);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(197, 18);
            this.label78.TabIndex = 41;
            this.label78.Text = "Source ido power control：";
            // 
            // cbSourceIdoPwd
            // 
            this.cbSourceIdoPwd.FormattingEnabled = true;
            this.cbSourceIdoPwd.Location = new System.Drawing.Point(190, 279);
            this.cbSourceIdoPwd.Name = "cbSourceIdoPwd";
            this.cbSourceIdoPwd.Size = new System.Drawing.Size(105, 26);
            this.cbSourceIdoPwd.TabIndex = 40;
            this.cbSourceIdoPwd.SelectedIndexChanged += new System.EventHandler(this.cbSourceIdoPwd_SelectedIndexChanged);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BackColor = System.Drawing.Color.Transparent;
            this.label79.Location = new System.Drawing.Point(3, 254);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(208, 18);
            this.label79.TabIndex = 39;
            this.label79.Text = "Temperature power control：";
            // 
            // cbTemperaturePwd
            // 
            this.cbTemperaturePwd.FormattingEnabled = true;
            this.cbTemperaturePwd.Location = new System.Drawing.Point(190, 250);
            this.cbTemperaturePwd.Name = "cbTemperaturePwd";
            this.cbTemperaturePwd.Size = new System.Drawing.Size(105, 26);
            this.cbTemperaturePwd.TabIndex = 38;
            this.cbTemperaturePwd.SelectedIndexChanged += new System.EventHandler(this.cbTemperaturePwd_SelectedIndexChanged);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Location = new System.Drawing.Point(3, 225);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(161, 18);
            this.label65.TabIndex = 37;
            this.label65.Text = "DDMI power control：";
            // 
            // cbDdmiPwd
            // 
            this.cbDdmiPwd.FormattingEnabled = true;
            this.cbDdmiPwd.Location = new System.Drawing.Point(190, 221);
            this.cbDdmiPwd.Name = "cbDdmiPwd";
            this.cbDdmiPwd.Size = new System.Drawing.Size(105, 26);
            this.cbDdmiPwd.TabIndex = 36;
            this.cbDdmiPwd.SelectedIndexChanged += new System.EventHandler(this.cbDdmiPwd_SelectedIndexChanged);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.BackColor = System.Drawing.Color.Transparent;
            this.label75.Location = new System.Drawing.Point(3, 196);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(153, 18);
            this.label75.TabIndex = 35;
            this.label75.Text = "Temperature slope：";
            // 
            // cbTemperatureSlope
            // 
            this.cbTemperatureSlope.FormattingEnabled = true;
            this.cbTemperatureSlope.Location = new System.Drawing.Point(190, 192);
            this.cbTemperatureSlope.Name = "cbTemperatureSlope";
            this.cbTemperatureSlope.Size = new System.Drawing.Size(105, 26);
            this.cbTemperatureSlope.TabIndex = 34;
            this.cbTemperatureSlope.SelectedIndexChanged += new System.EventHandler(this.cbTemperatureSlope_SelectedIndexChanged);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Location = new System.Drawing.Point(3, 167);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(153, 18);
            this.label74.TabIndex = 33;
            this.label74.Text = "Temperature offset：";
            // 
            // cbTemperatureOffset
            // 
            this.cbTemperatureOffset.FormattingEnabled = true;
            this.cbTemperatureOffset.Location = new System.Drawing.Point(190, 163);
            this.cbTemperatureOffset.Name = "cbTemperatureOffset";
            this.cbTemperatureOffset.Size = new System.Drawing.Size(105, 26);
            this.cbTemperatureOffset.TabIndex = 32;
            this.cbTemperatureOffset.SelectedIndexChanged += new System.EventHandler(this.cbTemperatureOffset_SelectedIndexChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Location = new System.Drawing.Point(3, 138);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(134, 18);
            this.label73.TabIndex = 31;
            this.label73.Text = "ldo mode select：";
            // 
            // cbIdoModeSelect
            // 
            this.cbIdoModeSelect.FormattingEnabled = true;
            this.cbIdoModeSelect.Location = new System.Drawing.Point(190, 134);
            this.cbIdoModeSelect.Name = "cbIdoModeSelect";
            this.cbIdoModeSelect.Size = new System.Drawing.Size(105, 26);
            this.cbIdoModeSelect.TabIndex = 30;
            this.cbIdoModeSelect.SelectedIndexChanged += new System.EventHandler(this.cbIdoModeSelect_SelectedIndexChanged);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Location = new System.Drawing.Point(3, 109);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(166, 18);
            this.label72.TabIndex = 29;
            this.label72.Text = "DDMI ldo ref. source：";
            // 
            // cbDdmiIdoRefSource
            // 
            this.cbDdmiIdoRefSource.FormattingEnabled = true;
            this.cbDdmiIdoRefSource.Location = new System.Drawing.Point(190, 105);
            this.cbDdmiIdoRefSource.Name = "cbDdmiIdoRefSource";
            this.cbDdmiIdoRefSource.Size = new System.Drawing.Size(105, 26);
            this.cbDdmiIdoRefSource.TabIndex = 28;
            this.cbDdmiIdoRefSource.SelectedIndexChanged += new System.EventHandler(this.cbDdmiIdoRefSource_SelectedIndexChanged);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Location = new System.Drawing.Point(3, 80);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(140, 18);
            this.label69.TabIndex = 27;
            this.label69.Text = "RSSI level select：";
            // 
            // cbRssiLevelSelect
            // 
            this.cbRssiLevelSelect.FormattingEnabled = true;
            this.cbRssiLevelSelect.Location = new System.Drawing.Point(190, 76);
            this.cbRssiLevelSelect.Name = "cbRssiLevelSelect";
            this.cbRssiLevelSelect.Size = new System.Drawing.Size(105, 26);
            this.cbRssiLevelSelect.TabIndex = 26;
            this.cbRssiLevelSelect.SelectedIndexChanged += new System.EventHandler(this.cbRssiLevelSelect_SelectedIndexChanged);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Location = new System.Drawing.Point(3, 51);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(104, 18);
            this.label68.TabIndex = 25;
            this.label68.Text = "RSSI mode：";
            // 
            // cbRssiMode
            // 
            this.cbRssiMode.FormattingEnabled = true;
            this.cbRssiMode.Location = new System.Drawing.Point(190, 47);
            this.cbRssiMode.Name = "cbRssiMode";
            this.cbRssiMode.Size = new System.Drawing.Size(105, 26);
            this.cbRssiMode.TabIndex = 24;
            this.cbRssiMode.SelectedIndexChanged += new System.EventHandler(this.cbRssiMode_SelectedIndexChanged);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Location = new System.Drawing.Point(3, 22);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(186, 18);
            this.label67.TabIndex = 23;
            this.label67.Text = "RSSI AGC clock speed：";
            // 
            // cbRssiAgcClockSpeed
            // 
            this.cbRssiAgcClockSpeed.FormattingEnabled = true;
            this.cbRssiAgcClockSpeed.Location = new System.Drawing.Point(190, 18);
            this.cbRssiAgcClockSpeed.Name = "cbRssiAgcClockSpeed";
            this.cbRssiAgcClockSpeed.Size = new System.Drawing.Size(105, 26);
            this.cbRssiAgcClockSpeed.TabIndex = 22;
            this.cbRssiAgcClockSpeed.SelectedIndexChanged += new System.EventHandler(this.cbRssiAgcClockSpeed_SelectedIndexChanged);
            // 
            // tpRt145Customer
            // 
            this.tpRt145Customer.Controls.Add(this.groupBox30);
            this.tpRt145Customer.Location = new System.Drawing.Point(4, 30);
            this.tpRt145Customer.Name = "tpRt145Customer";
            this.tpRt145Customer.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt145Customer.Size = new System.Drawing.Size(932, 526);
            this.tpRt145Customer.TabIndex = 6;
            this.tpRt145Customer.Text = "Customer";
            this.tpRt145Customer.UseVisualStyleBackColor = true;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.cbEqualizationR1Ch3);
            this.groupBox30.Controls.Add(this.cbEqualizationR0Ch3);
            this.groupBox30.Controls.Add(this.cbEqualization10dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization9dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualizationR1Ch2);
            this.groupBox30.Controls.Add(this.cbEqualizationR0Ch2);
            this.groupBox30.Controls.Add(this.cbEqualization10dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization9dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualizationR1Ch1);
            this.groupBox30.Controls.Add(this.cbEqualizationR0Ch1);
            this.groupBox30.Controls.Add(this.cbEqualization10dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization9dbCh1);
            this.groupBox30.Controls.Add(this.label115);
            this.groupBox30.Controls.Add(this.cbEqualizationR1Ch0);
            this.groupBox30.Controls.Add(this.label116);
            this.groupBox30.Controls.Add(this.cbEqualizationR0Ch0);
            this.groupBox30.Controls.Add(this.label117);
            this.groupBox30.Controls.Add(this.cbEqualization10dbCh0);
            this.groupBox30.Controls.Add(this.label118);
            this.groupBox30.Controls.Add(this.cbEqualization9dbCh0);
            this.groupBox30.Controls.Add(this.label114);
            this.groupBox30.Controls.Add(this.cbEqualization8dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization7dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization6dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization5dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization4dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization3dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization2dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization1dbCh3);
            this.groupBox30.Controls.Add(this.cbEqualization0dbCh3);
            this.groupBox30.Controls.Add(this.label113);
            this.groupBox30.Controls.Add(this.cbEqualization8dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization7dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization6dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization5dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization4dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization3dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization2dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization1dbCh2);
            this.groupBox30.Controls.Add(this.cbEqualization0dbCh2);
            this.groupBox30.Controls.Add(this.label112);
            this.groupBox30.Controls.Add(this.cbEqualization8dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization7dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization6dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization5dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization4dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization3dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization2dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization1dbCh1);
            this.groupBox30.Controls.Add(this.cbEqualization0dbCh1);
            this.groupBox30.Controls.Add(this.label111);
            this.groupBox30.Controls.Add(this.label70);
            this.groupBox30.Controls.Add(this.cbEqualization8dbCh0);
            this.groupBox30.Controls.Add(this.label83);
            this.groupBox30.Controls.Add(this.cbEqualization7dbCh0);
            this.groupBox30.Controls.Add(this.label104);
            this.groupBox30.Controls.Add(this.cbEqualization6dbCh0);
            this.groupBox30.Controls.Add(this.label105);
            this.groupBox30.Controls.Add(this.cbEqualization5dbCh0);
            this.groupBox30.Controls.Add(this.label106);
            this.groupBox30.Controls.Add(this.cbEqualization4dbCh0);
            this.groupBox30.Controls.Add(this.label107);
            this.groupBox30.Controls.Add(this.cbEqualization3dbCh0);
            this.groupBox30.Controls.Add(this.label108);
            this.groupBox30.Controls.Add(this.cbEqualization2dbCh0);
            this.groupBox30.Controls.Add(this.label109);
            this.groupBox30.Controls.Add(this.label110);
            this.groupBox30.Controls.Add(this.cbEqualization1dbCh0);
            this.groupBox30.Controls.Add(this.cbEqualization0dbCh0);
            this.groupBox30.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox30.Location = new System.Drawing.Point(5, 5);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(518, 452);
            this.groupBox30.TabIndex = 53;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Equalization (Unit：dB)";
            // 
            // cbEqualizationR1Ch3
            // 
            this.cbEqualizationR1Ch3.FormattingEnabled = true;
            this.cbEqualizationR1Ch3.Location = new System.Drawing.Point(420, 414);
            this.cbEqualizationR1Ch3.Name = "cbEqualizationR1Ch3";
            this.cbEqualizationR1Ch3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR1Ch3.TabIndex = 86;
            this.cbEqualizationR1Ch3.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR1Ch3_SelectedIndexChanged);
            // 
            // cbEqualizationR0Ch3
            // 
            this.cbEqualizationR0Ch3.FormattingEnabled = true;
            this.cbEqualizationR0Ch3.Location = new System.Drawing.Point(420, 383);
            this.cbEqualizationR0Ch3.Name = "cbEqualizationR0Ch3";
            this.cbEqualizationR0Ch3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR0Ch3.TabIndex = 85;
            this.cbEqualizationR0Ch3.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR0Ch3_SelectedIndexChanged);
            // 
            // cbEqualization10dbCh3
            // 
            this.cbEqualization10dbCh3.FormattingEnabled = true;
            this.cbEqualization10dbCh3.Location = new System.Drawing.Point(420, 352);
            this.cbEqualization10dbCh3.Name = "cbEqualization10dbCh3";
            this.cbEqualization10dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization10dbCh3.TabIndex = 84;
            this.cbEqualization10dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization10dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization9dbCh3
            // 
            this.cbEqualization9dbCh3.FormattingEnabled = true;
            this.cbEqualization9dbCh3.Location = new System.Drawing.Point(420, 321);
            this.cbEqualization9dbCh3.Name = "cbEqualization9dbCh3";
            this.cbEqualization9dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization9dbCh3.TabIndex = 83;
            this.cbEqualization9dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization9dbCh3_SelectedIndexChanged);
            // 
            // cbEqualizationR1Ch2
            // 
            this.cbEqualizationR1Ch2.FormattingEnabled = true;
            this.cbEqualizationR1Ch2.Location = new System.Drawing.Point(326, 413);
            this.cbEqualizationR1Ch2.Name = "cbEqualizationR1Ch2";
            this.cbEqualizationR1Ch2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR1Ch2.TabIndex = 82;
            this.cbEqualizationR1Ch2.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR1Ch2_SelectedIndexChanged);
            // 
            // cbEqualizationR0Ch2
            // 
            this.cbEqualizationR0Ch2.FormattingEnabled = true;
            this.cbEqualizationR0Ch2.Location = new System.Drawing.Point(326, 382);
            this.cbEqualizationR0Ch2.Name = "cbEqualizationR0Ch2";
            this.cbEqualizationR0Ch2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR0Ch2.TabIndex = 81;
            this.cbEqualizationR0Ch2.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR0Ch2_SelectedIndexChanged);
            // 
            // cbEqualization10dbCh2
            // 
            this.cbEqualization10dbCh2.FormattingEnabled = true;
            this.cbEqualization10dbCh2.Location = new System.Drawing.Point(326, 351);
            this.cbEqualization10dbCh2.Name = "cbEqualization10dbCh2";
            this.cbEqualization10dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization10dbCh2.TabIndex = 80;
            this.cbEqualization10dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization10dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization9dbCh2
            // 
            this.cbEqualization9dbCh2.FormattingEnabled = true;
            this.cbEqualization9dbCh2.Location = new System.Drawing.Point(326, 320);
            this.cbEqualization9dbCh2.Name = "cbEqualization9dbCh2";
            this.cbEqualization9dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization9dbCh2.TabIndex = 79;
            this.cbEqualization9dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization9dbCh2_SelectedIndexChanged);
            // 
            // cbEqualizationR1Ch1
            // 
            this.cbEqualizationR1Ch1.FormattingEnabled = true;
            this.cbEqualizationR1Ch1.Location = new System.Drawing.Point(232, 413);
            this.cbEqualizationR1Ch1.Name = "cbEqualizationR1Ch1";
            this.cbEqualizationR1Ch1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR1Ch1.TabIndex = 78;
            this.cbEqualizationR1Ch1.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR1Ch1_SelectedIndexChanged);
            // 
            // cbEqualizationR0Ch1
            // 
            this.cbEqualizationR0Ch1.FormattingEnabled = true;
            this.cbEqualizationR0Ch1.Location = new System.Drawing.Point(232, 382);
            this.cbEqualizationR0Ch1.Name = "cbEqualizationR0Ch1";
            this.cbEqualizationR0Ch1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR0Ch1.TabIndex = 77;
            this.cbEqualizationR0Ch1.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR0Ch1_SelectedIndexChanged);
            // 
            // cbEqualization10dbCh1
            // 
            this.cbEqualization10dbCh1.FormattingEnabled = true;
            this.cbEqualization10dbCh1.Location = new System.Drawing.Point(232, 351);
            this.cbEqualization10dbCh1.Name = "cbEqualization10dbCh1";
            this.cbEqualization10dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization10dbCh1.TabIndex = 76;
            this.cbEqualization10dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization10dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization9dbCh1
            // 
            this.cbEqualization9dbCh1.FormattingEnabled = true;
            this.cbEqualization9dbCh1.Location = new System.Drawing.Point(232, 320);
            this.cbEqualization9dbCh1.Name = "cbEqualization9dbCh1";
            this.cbEqualization9dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization9dbCh1.TabIndex = 75;
            this.cbEqualization9dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization9dbCh1_SelectedIndexChanged);
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.BackColor = System.Drawing.Color.Transparent;
            this.label115.Location = new System.Drawing.Point(3, 416);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(133, 18);
            this.label115.TabIndex = 74;
            this.label115.Text = "Equalization R1：";
            // 
            // cbEqualizationR1Ch0
            // 
            this.cbEqualizationR1Ch0.FormattingEnabled = true;
            this.cbEqualizationR1Ch0.Location = new System.Drawing.Point(138, 413);
            this.cbEqualizationR1Ch0.Name = "cbEqualizationR1Ch0";
            this.cbEqualizationR1Ch0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR1Ch0.TabIndex = 73;
            this.cbEqualizationR1Ch0.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR1Ch0_SelectedIndexChanged);
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.BackColor = System.Drawing.Color.Transparent;
            this.label116.Location = new System.Drawing.Point(3, 385);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(133, 18);
            this.label116.TabIndex = 72;
            this.label116.Text = "Equalization R0：";
            // 
            // cbEqualizationR0Ch0
            // 
            this.cbEqualizationR0Ch0.FormattingEnabled = true;
            this.cbEqualizationR0Ch0.Location = new System.Drawing.Point(138, 382);
            this.cbEqualizationR0Ch0.Name = "cbEqualizationR0Ch0";
            this.cbEqualizationR0Ch0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualizationR0Ch0.TabIndex = 71;
            this.cbEqualizationR0Ch0.SelectedIndexChanged += new System.EventHandler(this.cbEqualizationR0Ch0_SelectedIndexChanged);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.BackColor = System.Drawing.Color.Transparent;
            this.label117.Location = new System.Drawing.Point(3, 354);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(151, 18);
            this.label117.TabIndex = 70;
            this.label117.Text = "Equalization 10dB：";
            // 
            // cbEqualization10dbCh0
            // 
            this.cbEqualization10dbCh0.FormattingEnabled = true;
            this.cbEqualization10dbCh0.Location = new System.Drawing.Point(138, 351);
            this.cbEqualization10dbCh0.Name = "cbEqualization10dbCh0";
            this.cbEqualization10dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization10dbCh0.TabIndex = 69;
            this.cbEqualization10dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization10dbCh0_SelectedIndexChanged);
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.BackColor = System.Drawing.Color.Transparent;
            this.label118.Location = new System.Drawing.Point(3, 323);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(142, 18);
            this.label118.TabIndex = 68;
            this.label118.Text = "Equalization 9dB：";
            // 
            // cbEqualization9dbCh0
            // 
            this.cbEqualization9dbCh0.FormattingEnabled = true;
            this.cbEqualization9dbCh0.Location = new System.Drawing.Point(138, 320);
            this.cbEqualization9dbCh0.Name = "cbEqualization9dbCh0";
            this.cbEqualization9dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization9dbCh0.TabIndex = 67;
            this.cbEqualization9dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization9dbCh0_SelectedIndexChanged);
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.BackColor = System.Drawing.Color.Transparent;
            this.label114.Location = new System.Drawing.Point(440, 20);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(53, 18);
            this.label114.TabIndex = 66;
            this.label114.Text = "Ch3：";
            // 
            // cbEqualization8dbCh3
            // 
            this.cbEqualization8dbCh3.FormattingEnabled = true;
            this.cbEqualization8dbCh3.Location = new System.Drawing.Point(420, 289);
            this.cbEqualization8dbCh3.Name = "cbEqualization8dbCh3";
            this.cbEqualization8dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization8dbCh3.TabIndex = 65;
            this.cbEqualization8dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization8dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization7dbCh3
            // 
            this.cbEqualization7dbCh3.FormattingEnabled = true;
            this.cbEqualization7dbCh3.Location = new System.Drawing.Point(420, 258);
            this.cbEqualization7dbCh3.Name = "cbEqualization7dbCh3";
            this.cbEqualization7dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization7dbCh3.TabIndex = 64;
            this.cbEqualization7dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization7dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization6dbCh3
            // 
            this.cbEqualization6dbCh3.FormattingEnabled = true;
            this.cbEqualization6dbCh3.Location = new System.Drawing.Point(420, 227);
            this.cbEqualization6dbCh3.Name = "cbEqualization6dbCh3";
            this.cbEqualization6dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization6dbCh3.TabIndex = 63;
            this.cbEqualization6dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization6dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization5dbCh3
            // 
            this.cbEqualization5dbCh3.FormattingEnabled = true;
            this.cbEqualization5dbCh3.Location = new System.Drawing.Point(420, 196);
            this.cbEqualization5dbCh3.Name = "cbEqualization5dbCh3";
            this.cbEqualization5dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization5dbCh3.TabIndex = 62;
            this.cbEqualization5dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization5dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization4dbCh3
            // 
            this.cbEqualization4dbCh3.FormattingEnabled = true;
            this.cbEqualization4dbCh3.Location = new System.Drawing.Point(420, 165);
            this.cbEqualization4dbCh3.Name = "cbEqualization4dbCh3";
            this.cbEqualization4dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization4dbCh3.TabIndex = 61;
            this.cbEqualization4dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization4dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization3dbCh3
            // 
            this.cbEqualization3dbCh3.FormattingEnabled = true;
            this.cbEqualization3dbCh3.Location = new System.Drawing.Point(420, 134);
            this.cbEqualization3dbCh3.Name = "cbEqualization3dbCh3";
            this.cbEqualization3dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization3dbCh3.TabIndex = 60;
            this.cbEqualization3dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization3dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization2dbCh3
            // 
            this.cbEqualization2dbCh3.FormattingEnabled = true;
            this.cbEqualization2dbCh3.Location = new System.Drawing.Point(420, 103);
            this.cbEqualization2dbCh3.Name = "cbEqualization2dbCh3";
            this.cbEqualization2dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization2dbCh3.TabIndex = 59;
            this.cbEqualization2dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization2dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization1dbCh3
            // 
            this.cbEqualization1dbCh3.FormattingEnabled = true;
            this.cbEqualization1dbCh3.Location = new System.Drawing.Point(420, 72);
            this.cbEqualization1dbCh3.Name = "cbEqualization1dbCh3";
            this.cbEqualization1dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization1dbCh3.TabIndex = 58;
            this.cbEqualization1dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization1dbCh3_SelectedIndexChanged);
            // 
            // cbEqualization0dbCh3
            // 
            this.cbEqualization0dbCh3.FormattingEnabled = true;
            this.cbEqualization0dbCh3.Location = new System.Drawing.Point(420, 41);
            this.cbEqualization0dbCh3.Name = "cbEqualization0dbCh3";
            this.cbEqualization0dbCh3.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization0dbCh3.TabIndex = 57;
            this.cbEqualization0dbCh3.SelectedIndexChanged += new System.EventHandler(this.cbEqualization0dbCh3_SelectedIndexChanged);
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.BackColor = System.Drawing.Color.Transparent;
            this.label113.Location = new System.Drawing.Point(346, 19);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(53, 18);
            this.label113.TabIndex = 56;
            this.label113.Text = "Ch2：";
            // 
            // cbEqualization8dbCh2
            // 
            this.cbEqualization8dbCh2.FormattingEnabled = true;
            this.cbEqualization8dbCh2.Location = new System.Drawing.Point(326, 288);
            this.cbEqualization8dbCh2.Name = "cbEqualization8dbCh2";
            this.cbEqualization8dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization8dbCh2.TabIndex = 55;
            this.cbEqualization8dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization8dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization7dbCh2
            // 
            this.cbEqualization7dbCh2.FormattingEnabled = true;
            this.cbEqualization7dbCh2.Location = new System.Drawing.Point(326, 257);
            this.cbEqualization7dbCh2.Name = "cbEqualization7dbCh2";
            this.cbEqualization7dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization7dbCh2.TabIndex = 54;
            this.cbEqualization7dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization7dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization6dbCh2
            // 
            this.cbEqualization6dbCh2.FormattingEnabled = true;
            this.cbEqualization6dbCh2.Location = new System.Drawing.Point(326, 226);
            this.cbEqualization6dbCh2.Name = "cbEqualization6dbCh2";
            this.cbEqualization6dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization6dbCh2.TabIndex = 53;
            this.cbEqualization6dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization6dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization5dbCh2
            // 
            this.cbEqualization5dbCh2.FormattingEnabled = true;
            this.cbEqualization5dbCh2.Location = new System.Drawing.Point(326, 195);
            this.cbEqualization5dbCh2.Name = "cbEqualization5dbCh2";
            this.cbEqualization5dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization5dbCh2.TabIndex = 52;
            this.cbEqualization5dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization5dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization4dbCh2
            // 
            this.cbEqualization4dbCh2.FormattingEnabled = true;
            this.cbEqualization4dbCh2.Location = new System.Drawing.Point(326, 164);
            this.cbEqualization4dbCh2.Name = "cbEqualization4dbCh2";
            this.cbEqualization4dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization4dbCh2.TabIndex = 51;
            this.cbEqualization4dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization4dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization3dbCh2
            // 
            this.cbEqualization3dbCh2.FormattingEnabled = true;
            this.cbEqualization3dbCh2.Location = new System.Drawing.Point(326, 133);
            this.cbEqualization3dbCh2.Name = "cbEqualization3dbCh2";
            this.cbEqualization3dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization3dbCh2.TabIndex = 50;
            this.cbEqualization3dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization3dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization2dbCh2
            // 
            this.cbEqualization2dbCh2.FormattingEnabled = true;
            this.cbEqualization2dbCh2.Location = new System.Drawing.Point(326, 102);
            this.cbEqualization2dbCh2.Name = "cbEqualization2dbCh2";
            this.cbEqualization2dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization2dbCh2.TabIndex = 49;
            this.cbEqualization2dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization2dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization1dbCh2
            // 
            this.cbEqualization1dbCh2.FormattingEnabled = true;
            this.cbEqualization1dbCh2.Location = new System.Drawing.Point(326, 71);
            this.cbEqualization1dbCh2.Name = "cbEqualization1dbCh2";
            this.cbEqualization1dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization1dbCh2.TabIndex = 48;
            this.cbEqualization1dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization1dbCh2_SelectedIndexChanged);
            // 
            // cbEqualization0dbCh2
            // 
            this.cbEqualization0dbCh2.FormattingEnabled = true;
            this.cbEqualization0dbCh2.Location = new System.Drawing.Point(326, 40);
            this.cbEqualization0dbCh2.Name = "cbEqualization0dbCh2";
            this.cbEqualization0dbCh2.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization0dbCh2.TabIndex = 47;
            this.cbEqualization0dbCh2.SelectedIndexChanged += new System.EventHandler(this.cbEqualization0dbCh2_SelectedIndexChanged);
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.BackColor = System.Drawing.Color.Transparent;
            this.label112.Location = new System.Drawing.Point(252, 19);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(53, 18);
            this.label112.TabIndex = 46;
            this.label112.Text = "Ch1：";
            // 
            // cbEqualization8dbCh1
            // 
            this.cbEqualization8dbCh1.FormattingEnabled = true;
            this.cbEqualization8dbCh1.Location = new System.Drawing.Point(232, 288);
            this.cbEqualization8dbCh1.Name = "cbEqualization8dbCh1";
            this.cbEqualization8dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization8dbCh1.TabIndex = 45;
            this.cbEqualization8dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization8dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization7dbCh1
            // 
            this.cbEqualization7dbCh1.FormattingEnabled = true;
            this.cbEqualization7dbCh1.Location = new System.Drawing.Point(232, 257);
            this.cbEqualization7dbCh1.Name = "cbEqualization7dbCh1";
            this.cbEqualization7dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization7dbCh1.TabIndex = 44;
            this.cbEqualization7dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization7dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization6dbCh1
            // 
            this.cbEqualization6dbCh1.FormattingEnabled = true;
            this.cbEqualization6dbCh1.Location = new System.Drawing.Point(232, 226);
            this.cbEqualization6dbCh1.Name = "cbEqualization6dbCh1";
            this.cbEqualization6dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization6dbCh1.TabIndex = 43;
            this.cbEqualization6dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization6dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization5dbCh1
            // 
            this.cbEqualization5dbCh1.FormattingEnabled = true;
            this.cbEqualization5dbCh1.Location = new System.Drawing.Point(232, 195);
            this.cbEqualization5dbCh1.Name = "cbEqualization5dbCh1";
            this.cbEqualization5dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization5dbCh1.TabIndex = 42;
            this.cbEqualization5dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization5dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization4dbCh1
            // 
            this.cbEqualization4dbCh1.FormattingEnabled = true;
            this.cbEqualization4dbCh1.Location = new System.Drawing.Point(232, 164);
            this.cbEqualization4dbCh1.Name = "cbEqualization4dbCh1";
            this.cbEqualization4dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization4dbCh1.TabIndex = 41;
            this.cbEqualization4dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization4dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization3dbCh1
            // 
            this.cbEqualization3dbCh1.FormattingEnabled = true;
            this.cbEqualization3dbCh1.Location = new System.Drawing.Point(232, 133);
            this.cbEqualization3dbCh1.Name = "cbEqualization3dbCh1";
            this.cbEqualization3dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization3dbCh1.TabIndex = 40;
            this.cbEqualization3dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization3dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization2dbCh1
            // 
            this.cbEqualization2dbCh1.FormattingEnabled = true;
            this.cbEqualization2dbCh1.Location = new System.Drawing.Point(232, 102);
            this.cbEqualization2dbCh1.Name = "cbEqualization2dbCh1";
            this.cbEqualization2dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization2dbCh1.TabIndex = 39;
            this.cbEqualization2dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization2dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization1dbCh1
            // 
            this.cbEqualization1dbCh1.FormattingEnabled = true;
            this.cbEqualization1dbCh1.Location = new System.Drawing.Point(232, 71);
            this.cbEqualization1dbCh1.Name = "cbEqualization1dbCh1";
            this.cbEqualization1dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization1dbCh1.TabIndex = 38;
            this.cbEqualization1dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization1dbCh1_SelectedIndexChanged);
            // 
            // cbEqualization0dbCh1
            // 
            this.cbEqualization0dbCh1.FormattingEnabled = true;
            this.cbEqualization0dbCh1.Location = new System.Drawing.Point(232, 40);
            this.cbEqualization0dbCh1.Name = "cbEqualization0dbCh1";
            this.cbEqualization0dbCh1.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization0dbCh1.TabIndex = 37;
            this.cbEqualization0dbCh1.SelectedIndexChanged += new System.EventHandler(this.cbEqualization0dbCh1_SelectedIndexChanged);
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.BackColor = System.Drawing.Color.Transparent;
            this.label111.Location = new System.Drawing.Point(158, 19);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(53, 18);
            this.label111.TabIndex = 36;
            this.label111.Text = "Ch0：";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.BackColor = System.Drawing.Color.Transparent;
            this.label70.Location = new System.Drawing.Point(3, 291);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(142, 18);
            this.label70.TabIndex = 35;
            this.label70.Text = "Equalization 8dB：";
            // 
            // cbEqualization8dbCh0
            // 
            this.cbEqualization8dbCh0.FormattingEnabled = true;
            this.cbEqualization8dbCh0.Location = new System.Drawing.Point(138, 288);
            this.cbEqualization8dbCh0.Name = "cbEqualization8dbCh0";
            this.cbEqualization8dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization8dbCh0.TabIndex = 34;
            this.cbEqualization8dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization8dbCh0_SelectedIndexChanged);
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.BackColor = System.Drawing.Color.Transparent;
            this.label83.Location = new System.Drawing.Point(3, 260);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(142, 18);
            this.label83.TabIndex = 33;
            this.label83.Text = "Equalization 7dB：";
            // 
            // cbEqualization7dbCh0
            // 
            this.cbEqualization7dbCh0.FormattingEnabled = true;
            this.cbEqualization7dbCh0.Location = new System.Drawing.Point(138, 257);
            this.cbEqualization7dbCh0.Name = "cbEqualization7dbCh0";
            this.cbEqualization7dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization7dbCh0.TabIndex = 32;
            this.cbEqualization7dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization7dbCh0_SelectedIndexChanged);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.BackColor = System.Drawing.Color.Transparent;
            this.label104.Location = new System.Drawing.Point(3, 229);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(142, 18);
            this.label104.TabIndex = 31;
            this.label104.Text = "Equalization 6dB：";
            // 
            // cbEqualization6dbCh0
            // 
            this.cbEqualization6dbCh0.FormattingEnabled = true;
            this.cbEqualization6dbCh0.Location = new System.Drawing.Point(138, 226);
            this.cbEqualization6dbCh0.Name = "cbEqualization6dbCh0";
            this.cbEqualization6dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization6dbCh0.TabIndex = 30;
            this.cbEqualization6dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization6dbCh0_SelectedIndexChanged);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.BackColor = System.Drawing.Color.Transparent;
            this.label105.Location = new System.Drawing.Point(3, 198);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(142, 18);
            this.label105.TabIndex = 29;
            this.label105.Text = "Equalization 5dB：";
            // 
            // cbEqualization5dbCh0
            // 
            this.cbEqualization5dbCh0.FormattingEnabled = true;
            this.cbEqualization5dbCh0.Location = new System.Drawing.Point(138, 195);
            this.cbEqualization5dbCh0.Name = "cbEqualization5dbCh0";
            this.cbEqualization5dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization5dbCh0.TabIndex = 28;
            this.cbEqualization5dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization5dbCh0_SelectedIndexChanged);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.BackColor = System.Drawing.Color.Transparent;
            this.label106.Location = new System.Drawing.Point(3, 167);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(142, 18);
            this.label106.TabIndex = 27;
            this.label106.Text = "Equalization 4dB：";
            // 
            // cbEqualization4dbCh0
            // 
            this.cbEqualization4dbCh0.FormattingEnabled = true;
            this.cbEqualization4dbCh0.Location = new System.Drawing.Point(138, 164);
            this.cbEqualization4dbCh0.Name = "cbEqualization4dbCh0";
            this.cbEqualization4dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization4dbCh0.TabIndex = 26;
            this.cbEqualization4dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization4dbCh0_SelectedIndexChanged);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.BackColor = System.Drawing.Color.Transparent;
            this.label107.Location = new System.Drawing.Point(3, 136);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(142, 18);
            this.label107.TabIndex = 25;
            this.label107.Text = "Equalization 3dB：";
            // 
            // cbEqualization3dbCh0
            // 
            this.cbEqualization3dbCh0.FormattingEnabled = true;
            this.cbEqualization3dbCh0.Location = new System.Drawing.Point(138, 133);
            this.cbEqualization3dbCh0.Name = "cbEqualization3dbCh0";
            this.cbEqualization3dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization3dbCh0.TabIndex = 24;
            this.cbEqualization3dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization3dbCh0_SelectedIndexChanged);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.BackColor = System.Drawing.Color.Transparent;
            this.label108.Location = new System.Drawing.Point(3, 105);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(142, 18);
            this.label108.TabIndex = 23;
            this.label108.Text = "Equalization 2dB：";
            // 
            // cbEqualization2dbCh0
            // 
            this.cbEqualization2dbCh0.FormattingEnabled = true;
            this.cbEqualization2dbCh0.Location = new System.Drawing.Point(138, 102);
            this.cbEqualization2dbCh0.Name = "cbEqualization2dbCh0";
            this.cbEqualization2dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization2dbCh0.TabIndex = 22;
            this.cbEqualization2dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization2dbCh0_SelectedIndexChanged);
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.BackColor = System.Drawing.Color.Transparent;
            this.label109.Location = new System.Drawing.Point(3, 74);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(142, 18);
            this.label109.TabIndex = 21;
            this.label109.Text = "Equalization 1dB：";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.BackColor = System.Drawing.Color.Transparent;
            this.label110.Location = new System.Drawing.Point(3, 43);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(142, 18);
            this.label110.TabIndex = 20;
            this.label110.Text = "Equalization 0dB：";
            // 
            // cbEqualization1dbCh0
            // 
            this.cbEqualization1dbCh0.FormattingEnabled = true;
            this.cbEqualization1dbCh0.Location = new System.Drawing.Point(138, 71);
            this.cbEqualization1dbCh0.Name = "cbEqualization1dbCh0";
            this.cbEqualization1dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization1dbCh0.TabIndex = 19;
            this.cbEqualization1dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization1dbCh0_SelectedIndexChanged);
            // 
            // cbEqualization0dbCh0
            // 
            this.cbEqualization0dbCh0.FormattingEnabled = true;
            this.cbEqualization0dbCh0.Location = new System.Drawing.Point(138, 40);
            this.cbEqualization0dbCh0.Name = "cbEqualization0dbCh0";
            this.cbEqualization0dbCh0.Size = new System.Drawing.Size(88, 26);
            this.cbEqualization0dbCh0.TabIndex = 18;
            this.cbEqualization0dbCh0.SelectedIndexChanged += new System.EventHandler(this.cbEqualization0dbCh0_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.SystemColors.Control;
            this.label26.Location = new System.Drawing.Point(7, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(73, 18);
            this.label26.TabIndex = 0;
            this.label26.Text = "Ch0 LOS";
            // 
            // tbLOSCh0
            // 
            this.tbLOSCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSCh0.Location = new System.Drawing.Point(65, 24);
            this.tbLOSCh0.Name = "tbLOSCh0";
            this.tbLOSCh0.Size = new System.Drawing.Size(18, 18);
            this.tbLOSCh0.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(7, 44);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 18);
            this.label27.TabIndex = 3;
            this.label27.Text = "Ch1 LOS";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(73, 18);
            this.label28.TabIndex = 5;
            this.label28.Text = "Ch2 LOS";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(7, 84);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(73, 18);
            this.label29.TabIndex = 7;
            this.label29.Text = "Ch3 LOS";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(92, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(71, 18);
            this.label33.TabIndex = 9;
            this.label33.Text = "Ch0 LOL";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(92, 44);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(71, 18);
            this.label32.TabIndex = 11;
            this.label32.Text = "Ch1 LOL";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(92, 64);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 18);
            this.label31.TabIndex = 13;
            this.label31.Text = "Ch2 LOL";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(92, 84);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 18);
            this.label30.TabIndex = 15;
            this.label30.Text = "Ch3 LOL";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Location = new System.Drawing.Point(0, 107);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(117, 18);
            this.label37.TabIndex = 17;
            this.label37.Text = "Ch0 LOSorLOL";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(0, 127);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(117, 18);
            this.label36.TabIndex = 19;
            this.label36.Text = "Ch1 LOSorLOL";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(0, 147);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(117, 18);
            this.label35.TabIndex = 21;
            this.label35.Text = "Ch2 LOSorLOL";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(0, 167);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(117, 18);
            this.label34.TabIndex = 23;
            this.label34.Text = "Ch3 LOSorLOL";
            // 
            // tbLOSCh1
            // 
            this.tbLOSCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSCh1.Location = new System.Drawing.Point(65, 44);
            this.tbLOSCh1.Name = "tbLOSCh1";
            this.tbLOSCh1.Size = new System.Drawing.Size(18, 18);
            this.tbLOSCh1.TabIndex = 24;
            // 
            // tbLOSCh2
            // 
            this.tbLOSCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSCh2.Location = new System.Drawing.Point(65, 64);
            this.tbLOSCh2.Name = "tbLOSCh2";
            this.tbLOSCh2.Size = new System.Drawing.Size(18, 18);
            this.tbLOSCh2.TabIndex = 25;
            // 
            // tbLOSCh3
            // 
            this.tbLOSCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSCh3.Location = new System.Drawing.Point(65, 84);
            this.tbLOSCh3.Name = "tbLOSCh3";
            this.tbLOSCh3.Size = new System.Drawing.Size(18, 18);
            this.tbLOSCh3.TabIndex = 26;
            // 
            // tbLOLCh0
            // 
            this.tbLOLCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOLCh0.Location = new System.Drawing.Point(148, 23);
            this.tbLOLCh0.Name = "tbLOLCh0";
            this.tbLOLCh0.Size = new System.Drawing.Size(18, 18);
            this.tbLOLCh0.TabIndex = 27;
            // 
            // tbLOLCh1
            // 
            this.tbLOLCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOLCh1.Location = new System.Drawing.Point(148, 43);
            this.tbLOLCh1.Name = "tbLOLCh1";
            this.tbLOLCh1.Size = new System.Drawing.Size(18, 18);
            this.tbLOLCh1.TabIndex = 28;
            // 
            // tbLOLCh2
            // 
            this.tbLOLCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOLCh2.Location = new System.Drawing.Point(148, 63);
            this.tbLOLCh2.Name = "tbLOLCh2";
            this.tbLOLCh2.Size = new System.Drawing.Size(18, 18);
            this.tbLOLCh2.TabIndex = 29;
            // 
            // tbLOLCh3
            // 
            this.tbLOLCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOLCh3.Location = new System.Drawing.Point(148, 83);
            this.tbLOLCh3.Name = "tbLOLCh3";
            this.tbLOLCh3.Size = new System.Drawing.Size(18, 18);
            this.tbLOLCh3.TabIndex = 30;
            // 
            // tbLOSorLOLCh0
            // 
            this.tbLOSorLOLCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSorLOLCh0.Location = new System.Drawing.Point(91, 107);
            this.tbLOSorLOLCh0.Name = "tbLOSorLOLCh0";
            this.tbLOSorLOLCh0.Size = new System.Drawing.Size(18, 18);
            this.tbLOSorLOLCh0.TabIndex = 31;
            // 
            // tbLOSorLOLCh1
            // 
            this.tbLOSorLOLCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSorLOLCh1.Location = new System.Drawing.Point(91, 127);
            this.tbLOSorLOLCh1.Name = "tbLOSorLOLCh1";
            this.tbLOSorLOLCh1.Size = new System.Drawing.Size(18, 18);
            this.tbLOSorLOLCh1.TabIndex = 32;
            // 
            // tbLOSorLOLCh2
            // 
            this.tbLOSorLOLCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSorLOLCh2.Location = new System.Drawing.Point(91, 147);
            this.tbLOSorLOLCh2.Name = "tbLOSorLOLCh2";
            this.tbLOSorLOLCh2.Size = new System.Drawing.Size(18, 18);
            this.tbLOSorLOLCh2.TabIndex = 33;
            // 
            // tbLOSorLOLCh3
            // 
            this.tbLOSorLOLCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLOSorLOLCh3.Location = new System.Drawing.Point(91, 167);
            this.tbLOSorLOLCh3.Name = "tbLOSorLOLCh3";
            this.tbLOSorLOLCh3.Size = new System.Drawing.Size(18, 18);
            this.tbLOSorLOLCh3.TabIndex = 34;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.tbFaultCh3);
            this.groupBox15.Controls.Add(this.tbFaultCh2);
            this.groupBox15.Controls.Add(this.tbFaultCh1);
            this.groupBox15.Controls.Add(this.tbFaultCh0);
            this.groupBox15.Controls.Add(this.label46);
            this.groupBox15.Controls.Add(this.label47);
            this.groupBox15.Controls.Add(this.label48);
            this.groupBox15.Controls.Add(this.label49);
            this.groupBox15.Controls.Add(this.tbLOSorLOLCh3);
            this.groupBox15.Controls.Add(this.tbLOSorLOLCh2);
            this.groupBox15.Controls.Add(this.tbLOSorLOLCh1);
            this.groupBox15.Controls.Add(this.tbLOSorLOLCh0);
            this.groupBox15.Controls.Add(this.tbLOLCh3);
            this.groupBox15.Controls.Add(this.tbLOLCh2);
            this.groupBox15.Controls.Add(this.tbLOLCh1);
            this.groupBox15.Controls.Add(this.tbLOLCh0);
            this.groupBox15.Controls.Add(this.tbLOSCh3);
            this.groupBox15.Controls.Add(this.tbLOSCh2);
            this.groupBox15.Controls.Add(this.tbLOSCh1);
            this.groupBox15.Controls.Add(this.label34);
            this.groupBox15.Controls.Add(this.label35);
            this.groupBox15.Controls.Add(this.label36);
            this.groupBox15.Controls.Add(this.label37);
            this.groupBox15.Controls.Add(this.label30);
            this.groupBox15.Controls.Add(this.label31);
            this.groupBox15.Controls.Add(this.label32);
            this.groupBox15.Controls.Add(this.label33);
            this.groupBox15.Controls.Add(this.label29);
            this.groupBox15.Controls.Add(this.label28);
            this.groupBox15.Controls.Add(this.label27);
            this.groupBox15.Controls.Add(this.tbLOSCh0);
            this.groupBox15.Controls.Add(this.label26);
            this.groupBox15.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox15.Location = new System.Drawing.Point(945, 76);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(180, 192);
            this.groupBox15.TabIndex = 10;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "R1/R2_Latched status";
            // 
            // tbFaultCh3
            // 
            this.tbFaultCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFaultCh3.Location = new System.Drawing.Point(155, 166);
            this.tbFaultCh3.Name = "tbFaultCh3";
            this.tbFaultCh3.Size = new System.Drawing.Size(18, 18);
            this.tbFaultCh3.TabIndex = 42;
            // 
            // tbFaultCh2
            // 
            this.tbFaultCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFaultCh2.Location = new System.Drawing.Point(155, 146);
            this.tbFaultCh2.Name = "tbFaultCh2";
            this.tbFaultCh2.Size = new System.Drawing.Size(18, 18);
            this.tbFaultCh2.TabIndex = 41;
            // 
            // tbFaultCh1
            // 
            this.tbFaultCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFaultCh1.Location = new System.Drawing.Point(155, 126);
            this.tbFaultCh1.Name = "tbFaultCh1";
            this.tbFaultCh1.Size = new System.Drawing.Size(18, 18);
            this.tbFaultCh1.TabIndex = 40;
            // 
            // tbFaultCh0
            // 
            this.tbFaultCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFaultCh0.Location = new System.Drawing.Point(155, 106);
            this.tbFaultCh0.Name = "tbFaultCh0";
            this.tbFaultCh0.Size = new System.Drawing.Size(18, 18);
            this.tbFaultCh0.TabIndex = 39;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(115, 167);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(51, 18);
            this.label46.TabIndex = 38;
            this.label46.Text = "Fault3";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(115, 147);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(51, 18);
            this.label47.TabIndex = 37;
            this.label47.Text = "Fault2";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(115, 127);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(51, 18);
            this.label48.TabIndex = 36;
            this.label48.Text = "Fault1";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(115, 107);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(51, 18);
            this.label49.TabIndex = 35;
            this.label49.Text = "Fault0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(17, 28);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(23, 18);
            this.label25.TabIndex = 0;
            this.label25.Text = "ID";
            // 
            // tbChipId
            // 
            this.tbChipId.Location = new System.Drawing.Point(46, 25);
            this.tbChipId.Name = "tbChipId";
            this.tbChipId.Size = new System.Drawing.Size(96, 26);
            this.tbChipId.TabIndex = 1;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.tbChipId);
            this.groupBox14.Controls.Add(this.label25);
            this.groupBox14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox14.Location = new System.Drawing.Point(945, 14);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(180, 56);
            this.groupBox14.TabIndex = 9;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "R0_Chip ID";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.tbNlatLOLCh3);
            this.groupBox18.Controls.Add(this.tbNlatLOLCh2);
            this.groupBox18.Controls.Add(this.tbNlatLOLCh1);
            this.groupBox18.Controls.Add(this.tbNlatLOLCh0);
            this.groupBox18.Controls.Add(this.tbNlatLOSCh3);
            this.groupBox18.Controls.Add(this.tbNlatLOSCh2);
            this.groupBox18.Controls.Add(this.tbNlatLOSCh1);
            this.groupBox18.Controls.Add(this.label50);
            this.groupBox18.Controls.Add(this.label51);
            this.groupBox18.Controls.Add(this.label52);
            this.groupBox18.Controls.Add(this.label53);
            this.groupBox18.Controls.Add(this.label128);
            this.groupBox18.Controls.Add(this.label129);
            this.groupBox18.Controls.Add(this.label130);
            this.groupBox18.Controls.Add(this.tbNlatLOSCh0);
            this.groupBox18.Controls.Add(this.label131);
            this.groupBox18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox18.Location = new System.Drawing.Point(945, 274);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(180, 111);
            this.groupBox18.TabIndex = 11;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "R3_Non-latched status";
            // 
            // tbNlatLOLCh3
            // 
            this.tbNlatLOLCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOLCh3.Location = new System.Drawing.Point(148, 83);
            this.tbNlatLOLCh3.Name = "tbNlatLOLCh3";
            this.tbNlatLOLCh3.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOLCh3.TabIndex = 30;
            // 
            // tbNlatLOLCh2
            // 
            this.tbNlatLOLCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOLCh2.Location = new System.Drawing.Point(148, 63);
            this.tbNlatLOLCh2.Name = "tbNlatLOLCh2";
            this.tbNlatLOLCh2.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOLCh2.TabIndex = 29;
            // 
            // tbNlatLOLCh1
            // 
            this.tbNlatLOLCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOLCh1.Location = new System.Drawing.Point(148, 43);
            this.tbNlatLOLCh1.Name = "tbNlatLOLCh1";
            this.tbNlatLOLCh1.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOLCh1.TabIndex = 28;
            // 
            // tbNlatLOLCh0
            // 
            this.tbNlatLOLCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOLCh0.Location = new System.Drawing.Point(148, 23);
            this.tbNlatLOLCh0.Name = "tbNlatLOLCh0";
            this.tbNlatLOLCh0.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOLCh0.TabIndex = 27;
            // 
            // tbNlatLOSCh3
            // 
            this.tbNlatLOSCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOSCh3.Location = new System.Drawing.Point(65, 84);
            this.tbNlatLOSCh3.Name = "tbNlatLOSCh3";
            this.tbNlatLOSCh3.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOSCh3.TabIndex = 26;
            // 
            // tbNlatLOSCh2
            // 
            this.tbNlatLOSCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOSCh2.Location = new System.Drawing.Point(65, 64);
            this.tbNlatLOSCh2.Name = "tbNlatLOSCh2";
            this.tbNlatLOSCh2.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOSCh2.TabIndex = 25;
            // 
            // tbNlatLOSCh1
            // 
            this.tbNlatLOSCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOSCh1.Location = new System.Drawing.Point(65, 44);
            this.tbNlatLOSCh1.Name = "tbNlatLOSCh1";
            this.tbNlatLOSCh1.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOSCh1.TabIndex = 24;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(92, 84);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(71, 18);
            this.label50.TabIndex = 15;
            this.label50.Text = "Ch3 LOL";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(92, 64);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(71, 18);
            this.label51.TabIndex = 13;
            this.label51.Text = "Ch2 LOL";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(92, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(71, 18);
            this.label52.TabIndex = 11;
            this.label52.Text = "Ch1 LOL";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(92, 24);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(71, 18);
            this.label53.TabIndex = 9;
            this.label53.Text = "Ch0 LOL";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(7, 84);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(73, 18);
            this.label128.TabIndex = 7;
            this.label128.Text = "Ch3 LOS";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(7, 64);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(73, 18);
            this.label129.TabIndex = 5;
            this.label129.Text = "Ch2 LOS";
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(7, 44);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(73, 18);
            this.label130.TabIndex = 3;
            this.label130.Text = "Ch1 LOS";
            // 
            // tbNlatLOSCh0
            // 
            this.tbNlatLOSCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatLOSCh0.Location = new System.Drawing.Point(65, 24);
            this.tbNlatLOSCh0.Name = "tbNlatLOSCh0";
            this.tbNlatLOSCh0.Size = new System.Drawing.Size(18, 18);
            this.tbNlatLOSCh0.TabIndex = 2;
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(7, 24);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(73, 18);
            this.label131.TabIndex = 0;
            this.label131.Text = "Ch0 LOS";
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.cbAgcRssi);
            this.groupBox29.Controls.Add(this.label132);
            this.groupBox29.Controls.Add(this.cbAdcOut);
            this.groupBox29.Controls.Add(this.label133);
            this.groupBox29.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox29.Location = new System.Drawing.Point(945, 391);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(180, 84);
            this.groupBox29.TabIndex = 12;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "R4/R5_DDMI report";
            // 
            // cbAgcRssi
            // 
            this.cbAgcRssi.Enabled = false;
            this.cbAgcRssi.FormattingEnabled = true;
            this.cbAgcRssi.Location = new System.Drawing.Point(72, 53);
            this.cbAgcRssi.Name = "cbAgcRssi";
            this.cbAgcRssi.Size = new System.Drawing.Size(108, 26);
            this.cbAgcRssi.TabIndex = 20;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(2, 56);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(99, 18);
            this.label132.TabIndex = 19;
            this.label132.Text = "AGC RSSI：";
            // 
            // cbAdcOut
            // 
            this.cbAdcOut.Enabled = false;
            this.cbAdcOut.FormattingEnabled = true;
            this.cbAdcOut.Location = new System.Drawing.Point(72, 23);
            this.cbAdcOut.Name = "cbAdcOut";
            this.cbAdcOut.Size = new System.Drawing.Size(108, 26);
            this.cbAdcOut.TabIndex = 18;
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(2, 27);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(84, 18);
            this.label133.TabIndex = 1;
            this.label133.Text = "ADC out：";
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.cbNlatFaultSTCh3);
            this.groupBox32.Controls.Add(this.cbNlatFaultSTCh2);
            this.groupBox32.Controls.Add(this.cbNlatFaultSTCh1);
            this.groupBox32.Controls.Add(this.cbNlatFaultSTCh0);
            this.groupBox32.Controls.Add(this.tbNlatFalutCh3);
            this.groupBox32.Controls.Add(this.tbNlatFalutCh2);
            this.groupBox32.Controls.Add(this.tbNlatFalutCh1);
            this.groupBox32.Controls.Add(this.label134);
            this.groupBox32.Controls.Add(this.label135);
            this.groupBox32.Controls.Add(this.label136);
            this.groupBox32.Controls.Add(this.label137);
            this.groupBox32.Controls.Add(this.label138);
            this.groupBox32.Controls.Add(this.label139);
            this.groupBox32.Controls.Add(this.label140);
            this.groupBox32.Controls.Add(this.tbNlatFalutCh0);
            this.groupBox32.Controls.Add(this.label141);
            this.groupBox32.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox32.Location = new System.Drawing.Point(945, 481);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(180, 111);
            this.groupBox32.TabIndex = 13;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "R6_Non-latched fault";
            // 
            // cbNlatFaultSTCh3
            // 
            this.cbNlatFaultSTCh3.Enabled = false;
            this.cbNlatFaultSTCh3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbNlatFaultSTCh3.FormattingEnabled = true;
            this.cbNlatFaultSTCh3.Location = new System.Drawing.Point(126, 81);
            this.cbNlatFaultSTCh3.Name = "cbNlatFaultSTCh3";
            this.cbNlatFaultSTCh3.Size = new System.Drawing.Size(50, 24);
            this.cbNlatFaultSTCh3.TabIndex = 30;
            // 
            // cbNlatFaultSTCh2
            // 
            this.cbNlatFaultSTCh2.Enabled = false;
            this.cbNlatFaultSTCh2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbNlatFaultSTCh2.FormattingEnabled = true;
            this.cbNlatFaultSTCh2.Location = new System.Drawing.Point(126, 61);
            this.cbNlatFaultSTCh2.Name = "cbNlatFaultSTCh2";
            this.cbNlatFaultSTCh2.Size = new System.Drawing.Size(50, 24);
            this.cbNlatFaultSTCh2.TabIndex = 29;
            // 
            // cbNlatFaultSTCh1
            // 
            this.cbNlatFaultSTCh1.Enabled = false;
            this.cbNlatFaultSTCh1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbNlatFaultSTCh1.FormattingEnabled = true;
            this.cbNlatFaultSTCh1.Location = new System.Drawing.Point(126, 41);
            this.cbNlatFaultSTCh1.Name = "cbNlatFaultSTCh1";
            this.cbNlatFaultSTCh1.Size = new System.Drawing.Size(50, 24);
            this.cbNlatFaultSTCh1.TabIndex = 28;
            // 
            // cbNlatFaultSTCh0
            // 
            this.cbNlatFaultSTCh0.Enabled = false;
            this.cbNlatFaultSTCh0.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbNlatFaultSTCh0.FormattingEnabled = true;
            this.cbNlatFaultSTCh0.Location = new System.Drawing.Point(126, 21);
            this.cbNlatFaultSTCh0.Name = "cbNlatFaultSTCh0";
            this.cbNlatFaultSTCh0.Size = new System.Drawing.Size(50, 24);
            this.cbNlatFaultSTCh0.TabIndex = 27;
            // 
            // tbNlatFalutCh3
            // 
            this.tbNlatFalutCh3.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatFalutCh3.Location = new System.Drawing.Point(43, 84);
            this.tbNlatFalutCh3.Name = "tbNlatFalutCh3";
            this.tbNlatFalutCh3.Size = new System.Drawing.Size(18, 18);
            this.tbNlatFalutCh3.TabIndex = 26;
            // 
            // tbNlatFalutCh2
            // 
            this.tbNlatFalutCh2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatFalutCh2.Location = new System.Drawing.Point(43, 64);
            this.tbNlatFalutCh2.Name = "tbNlatFalutCh2";
            this.tbNlatFalutCh2.Size = new System.Drawing.Size(18, 18);
            this.tbNlatFalutCh2.TabIndex = 25;
            // 
            // tbNlatFalutCh1
            // 
            this.tbNlatFalutCh1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatFalutCh1.Location = new System.Drawing.Point(43, 44);
            this.tbNlatFalutCh1.Name = "tbNlatFalutCh1";
            this.tbNlatFalutCh1.Size = new System.Drawing.Size(18, 18);
            this.tbNlatFalutCh1.TabIndex = 24;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(67, 84);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(75, 18);
            this.label134.TabIndex = 15;
            this.label134.Text = "Falut3 ST";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(67, 64);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(75, 18);
            this.label135.TabIndex = 13;
            this.label135.Text = "Falut2 ST";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(67, 44);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(75, 18);
            this.label136.TabIndex = 11;
            this.label136.Text = "Falut1 ST";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(67, 24);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(75, 18);
            this.label137.TabIndex = 9;
            this.label137.Text = "Falut0 ST";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(3, 84);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(51, 18);
            this.label138.TabIndex = 7;
            this.label138.Text = "Falut3";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(3, 64);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(51, 18);
            this.label139.TabIndex = 5;
            this.label139.Text = "Falut2";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(3, 44);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(51, 18);
            this.label140.TabIndex = 3;
            this.label140.Text = "Falut1";
            // 
            // tbNlatFalutCh0
            // 
            this.tbNlatFalutCh0.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNlatFalutCh0.Location = new System.Drawing.Point(43, 24);
            this.tbNlatFalutCh0.Name = "tbNlatFalutCh0";
            this.tbNlatFalutCh0.Size = new System.Drawing.Size(18, 18);
            this.tbNlatFalutCh0.TabIndex = 2;
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(3, 24);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(51, 18);
            this.label141.TabIndex = 0;
            this.label141.Text = "Falut0";
            // 
            // UcRt146Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox32);
            this.Controls.Add(this.groupBox29);
            this.Controls.Add(this.groupBox18);
            this.Controls.Add(this.groupBox15);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.tpRt145Config);
            this.Controls.Add(this.bReadAll);
            this.Controls.Add(this.bStoreIntoFlash);
            this.Controls.Add(this.bDeviceReset);
            this.Name = "UcRt146Config";
            this.Size = new System.Drawing.Size(1130, 620);
            this.tpRt145Config.ResumeLayout(false);
            this.tpRt145Global.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbLOL.ResumeLayout(false);
            this.gbLOL.PerformLayout();
            this.tpRt145Bychannel.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tpRt145Control.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tpRt145Customer.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bReadAll;
        private System.Windows.Forms.Button bStoreIntoFlash;
        private System.Windows.Forms.Button bDeviceReset;
        private System.Windows.Forms.TabControl tpRt145Config;
        private System.Windows.Forms.TabPage tpRt145Global;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.ComboBox cbSwitchBistVdet;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.ComboBox cbSwitchBistVref;
        private System.Windows.Forms.ComboBox cbCdrLoopBandWidth;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.CheckBox cbMonitorClockEnableCh3;
        private System.Windows.Forms.CheckBox cbMonitorClockEnableCh1;
        private System.Windows.Forms.CheckBox cbMonitorClockEnableCh0;
        private System.Windows.Forms.CheckBox cbMonitorClockEnableCh2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox cbAutoTuneUnlockTh;
        private System.Windows.Forms.ComboBox cbAutoTuneLockTh;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ComboBox cbSelectImon;
        private System.Windows.Forms.ComboBox cbModeImon;
        private System.Windows.Forms.ComboBox cbSelectVdiop;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cbIntrptPolarity;
        private System.Windows.Forms.ComboBox cbIntrptType;
        private System.Windows.Forms.TabPage tpRt145Bychannel;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.ComboBox cbVcoMsbSelecCh0;
        private System.Windows.Forms.ComboBox cbAutoBypassResetCh0;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.TabPage tpRt145Control;
        private System.Windows.Forms.TabPage tpRt145Customer;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.ComboBox cbEqualization8dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization7dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization6dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization5dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization4dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization3dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization2dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization1dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization0dbCh3;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.ComboBox cbEqualization8dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization7dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization6dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization5dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization4dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization3dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization2dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization1dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization0dbCh2;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.ComboBox cbEqualization8dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization7dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization6dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization5dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization4dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization3dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization2dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization1dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization0dbCh1;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.ComboBox cbEqualization8dbCh0;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.ComboBox cbEqualization7dbCh0;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.ComboBox cbEqualization6dbCh0;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.ComboBox cbEqualization5dbCh0;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.ComboBox cbEqualization4dbCh0;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.ComboBox cbEqualization3dbCh0;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.ComboBox cbEqualization2dbCh0;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.ComboBox cbEqualization1dbCh0;
        private System.Windows.Forms.ComboBox cbEqualization0dbCh0;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbLOSCh0;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox tbLOSCh1;
        private System.Windows.Forms.TextBox tbLOSCh2;
        private System.Windows.Forms.TextBox tbLOSCh3;
        private System.Windows.Forms.TextBox tbLOLCh0;
        private System.Windows.Forms.TextBox tbLOLCh1;
        private System.Windows.Forms.TextBox tbLOLCh2;
        private System.Windows.Forms.TextBox tbLOLCh3;
        private System.Windows.Forms.TextBox tbLOSorLOLCh0;
        private System.Windows.Forms.TextBox tbLOSorLOLCh1;
        private System.Windows.Forms.TextBox tbLOSorLOLCh2;
        private System.Windows.Forms.TextBox tbLOSorLOLCh3;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbChipId;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox tbFaultCh1;
        private System.Windows.Forms.TextBox tbFaultCh0;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox tbFaultCh3;
        private System.Windows.Forms.TextBox tbFaultCh2;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.TextBox tbNlatLOLCh3;
        private System.Windows.Forms.TextBox tbNlatLOLCh2;
        private System.Windows.Forms.TextBox tbNlatLOLCh1;
        private System.Windows.Forms.TextBox tbNlatLOLCh0;
        private System.Windows.Forms.TextBox tbNlatLOSCh3;
        private System.Windows.Forms.TextBox tbNlatLOSCh2;
        private System.Windows.Forms.TextBox tbNlatLOSCh1;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.TextBox tbNlatLOSCh0;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ComboBox cbDeEmphasisCh3;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cbDeEmphasisCh2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbDeEmphasisCh1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbDeEmphasisCh0;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox cbCrossPointCh3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbCrossPointCh2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbCrossPointCh1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbCrossPointCh0;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.CheckBox cbLolModeCh3;
        private System.Windows.Forms.CheckBox cbLolModeCh1;
        private System.Windows.Forms.CheckBox cbLolModeCh0;
        private System.Windows.Forms.CheckBox cbLolModeCh2;
        private System.Windows.Forms.CheckBox cbLosModeCh3;
        private System.Windows.Forms.CheckBox cbLosModeCh1;
        private System.Windows.Forms.CheckBox cbLosModeCh0;
        private System.Windows.Forms.CheckBox cbLosModeCh2;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.CheckBox cbLolClearCh3;
        private System.Windows.Forms.CheckBox cbLolClearCh1;
        private System.Windows.Forms.CheckBox cbLolClearCh0;
        private System.Windows.Forms.CheckBox cbLolClearCh2;
        private System.Windows.Forms.CheckBox cbLosClearCh3;
        private System.Windows.Forms.CheckBox cbLosClearCh1;
        private System.Windows.Forms.CheckBox cbLosClearCh0;
        private System.Windows.Forms.CheckBox cbLosClearCh2;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox cbMuteModeCh3;
        private System.Windows.Forms.CheckBox cbMuteModeCh1;
        private System.Windows.Forms.CheckBox cbMuteModeCh0;
        private System.Windows.Forms.CheckBox cbMuteModeCh2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbLosHysteresisCh3;
        private System.Windows.Forms.CheckBox cbLosHysteresisCh1;
        private System.Windows.Forms.CheckBox cbLosHysteresisCh0;
        private System.Windows.Forms.CheckBox cbLosHysteresisCh2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbLosThresholdCh3;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.ComboBox cbLosThresholdCh2;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.ComboBox cbLosThresholdCh1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbLosThresholdCh0;
        private System.Windows.Forms.Label lLosThresholdL0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbLolMaskCh3;
        private System.Windows.Forms.CheckBox cbLolMaskCh1;
        private System.Windows.Forms.CheckBox cbLolMaskCh0;
        private System.Windows.Forms.CheckBox cbLolMaskCh2;
        private System.Windows.Forms.CheckBox cbLosMaskCh3;
        private System.Windows.Forms.CheckBox cbLosMaskCh1;
        private System.Windows.Forms.CheckBox cbLosMaskCh0;
        private System.Windows.Forms.CheckBox cbLosMaskCh2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbTxPowerControlCh3;
        private System.Windows.Forms.CheckBox cbTxPowerControlCh1;
        private System.Windows.Forms.CheckBox cbTxPowerControlCh0;
        private System.Windows.Forms.CheckBox cbTxPowerControlCh2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbTxCdrControlCh3;
        private System.Windows.Forms.CheckBox cbTxCdrControlCh1;
        private System.Windows.Forms.CheckBox cbTxCdrControlCh0;
        private System.Windows.Forms.CheckBox cbTxCdrControlCh2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbLOL;
        private System.Windows.Forms.CheckBox cbInvertPolarityCH3;
        private System.Windows.Forms.CheckBox cbInvertPolarityCH1;
        private System.Windows.Forms.CheckBox cbInvertPolarityCH0;
        private System.Windows.Forms.CheckBox cbInvertPolarityCH2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.ComboBox cbAgcRssi;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.ComboBox cbAdcOut;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.TextBox tbNlatFalutCh3;
        private System.Windows.Forms.TextBox tbNlatFalutCh2;
        private System.Windows.Forms.TextBox tbNlatFalutCh1;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.TextBox tbNlatFalutCh0;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbFaultClearCh3;
        private System.Windows.Forms.CheckBox cbFaultClearCh1;
        private System.Windows.Forms.CheckBox cbFaultClearCh0;
        private System.Windows.Forms.CheckBox cbFaultClearCh2;
        private System.Windows.Forms.CheckBox cbFaultMaskCh3;
        private System.Windows.Forms.CheckBox cbFaultMaskCh1;
        private System.Windows.Forms.CheckBox cbFaultMaskCh0;
        private System.Windows.Forms.CheckBox cbFaultMaskCh2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.CheckBox cbFaultModeCh3;
        private System.Windows.Forms.CheckBox cbFaultModeCh1;
        private System.Windows.Forms.CheckBox cbFaultModeCh0;
        private System.Windows.Forms.CheckBox cbFaultModeCh2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.ComboBox cbAutoTuneClockSpeed;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.ComboBox cbSampleHoldClockSpeed;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.ComboBox cbClockLosEnable;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.ComboBox cbClockAdcEnable;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.ComboBox cbClockAutoTuneEnable;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.ComboBox cbRingOscPwd;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.CheckBox cbCdrPwdCh3;
        private System.Windows.Forms.CheckBox cbCdrPwdCh1;
        private System.Windows.Forms.CheckBox cbCdrPwdCh0;
        private System.Windows.Forms.CheckBox cbCdrPwdCh2;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label lCh0SahPwd;
        private System.Windows.Forms.ComboBox cbCh0SahPwd;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.ComboBox cbIgnoreGlobalPwd;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.ComboBox cbDdmiReset;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.ComboBox cbEnableVf;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cbSourceIdoPwd;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.ComboBox cbTemperaturePwd;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.ComboBox cbDdmiPwd;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.ComboBox cbTemperatureSlope;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.ComboBox cbTemperatureOffset;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.ComboBox cbIdoModeSelect;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.ComboBox cbDdmiIdoRefSource;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.ComboBox cbRssiLevelSelect;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cbRssiMode;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ComboBox cbRssiAgcClockSpeed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbEqPeakCh3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbEqPeakCh2;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.ComboBox cbEqPeakCh1;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.ComboBox cbEqPeakCh0;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbDeemphasisEnCh3;
        private System.Windows.Forms.CheckBox cbDeemphasisEnCh1;
        private System.Windows.Forms.CheckBox cbDeemphasisEnCh0;
        private System.Windows.Forms.CheckBox cbDeemphasisEnCh2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbDdmiChannelSelect;
        private System.Windows.Forms.ComboBox cbDdmiAdcPowerControl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbAeqPwdCh3;
        private System.Windows.Forms.CheckBox cbAeqPwdCh1;
        private System.Windows.Forms.CheckBox cbAeqPwdCh0;
        private System.Windows.Forms.CheckBox cbAeqPwdCh2;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cbIbiasCurrentCh3;
        private System.Windows.Forms.ComboBox cbIbiasCurrentCh2;
        private System.Windows.Forms.ComboBox cbIbiasCurrentCh1;
        private System.Windows.Forms.ComboBox cbIbiasCurrentCh0;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.CheckBox cbIbiasPowerDownCh3;
        private System.Windows.Forms.CheckBox cbIbiasPowerDownCh2;
        private System.Windows.Forms.CheckBox cbIbiasPowerDownCh1;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.CheckBox cbIbiasPowerDownCh0;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.CheckBox cbBurninEnCh3;
        private System.Windows.Forms.CheckBox cbBurninEnCh2;
        private System.Windows.Forms.CheckBox cbBurninEnCh1;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.CheckBox cbBurninEnCh0;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.ComboBox cbBurninCurrentCh3;
        private System.Windows.Forms.ComboBox cbBurninCurrentCh2;
        private System.Windows.Forms.ComboBox cbBurninCurrentCh1;
        private System.Windows.Forms.ComboBox cbBurninCurrentCh0;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.CheckBox cbModulationPowerDownCh3;
        private System.Windows.Forms.CheckBox cbModulationPowerDownCh2;
        private System.Windows.Forms.CheckBox cbModulationPowerDownCh1;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.CheckBox cbModulationPowerDownCh0;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.ComboBox cbModulationCh3;
        private System.Windows.Forms.ComboBox cbModulationCh2;
        private System.Windows.Forms.ComboBox cbModulationCh1;
        private System.Windows.Forms.ComboBox cbModulationCh0;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.ComboBox cbVcoMsbSelecCh3;
        private System.Windows.Forms.ComboBox cbAutoBypassResetCh3;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.ComboBox cbVcoMsbSelecCh2;
        private System.Windows.Forms.ComboBox cbAutoBypassResetCh2;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.ComboBox cbVcoMsbSelecCh1;
        private System.Windows.Forms.ComboBox cbAutoBypassResetCh1;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.ComboBox cbEqualizationR1Ch3;
        private System.Windows.Forms.ComboBox cbEqualizationR0Ch3;
        private System.Windows.Forms.ComboBox cbEqualization10dbCh3;
        private System.Windows.Forms.ComboBox cbEqualization9dbCh3;
        private System.Windows.Forms.ComboBox cbEqualizationR1Ch2;
        private System.Windows.Forms.ComboBox cbEqualizationR0Ch2;
        private System.Windows.Forms.ComboBox cbEqualization10dbCh2;
        private System.Windows.Forms.ComboBox cbEqualization9dbCh2;
        private System.Windows.Forms.ComboBox cbEqualizationR1Ch1;
        private System.Windows.Forms.ComboBox cbEqualizationR0Ch1;
        private System.Windows.Forms.ComboBox cbEqualization10dbCh1;
        private System.Windows.Forms.ComboBox cbEqualization9dbCh1;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.ComboBox cbEqualizationR1Ch0;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.ComboBox cbEqualizationR0Ch0;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.ComboBox cbEqualization10dbCh0;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.ComboBox cbEqualization9dbCh0;
        private System.Windows.Forms.ComboBox cbNlatFaultSTCh3;
        private System.Windows.Forms.ComboBox cbNlatFaultSTCh2;
        private System.Windows.Forms.ComboBox cbNlatFaultSTCh1;
        private System.Windows.Forms.ComboBox cbNlatFaultSTCh0;
    }
}
