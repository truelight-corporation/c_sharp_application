using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gn2148Gn2149Config
{
    public partial class UcGn2148Config : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cRead16CB(byte devAddr, byte[] regAddr, byte length, byte[] data);
        public delegate int I2cWrite16CB(byte devAddr, byte[] regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private I2cRead16CB i2cRead16CB = null;
        private I2cWrite16CB i2cWrite16CB = null;
        private bool reading = false;

        public UcGn2148Config()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 2; i++) {
                cbCtleAdaptive4.Items.Add(i);
            }

            for (i = 0; i < 4; i++) {
                cbLosHyst.Items.Add(i);
                cbVcc1HighFaultThres.Items.Add(i);
                cbVcc1LowFaultThres.Items.Add(i);
                cbVcc2HighFaultThres.Items.Add(i);
                cbVcc2LowFaultThres.Items.Add(i);
                cbVcoRangeOverride.Items.Add(i);
            }

            for (i = 0; i < 8; i++) {
                cbVcselIbiasLowFaultThres.Items.Add(i);
                cbCtleAdaptive1.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbLosThres.Items.Add(i);
                cbVcselVfHighFaulThres.Items.Add(i);
                cbVcselVfLowFaulThres.Items.Add(i);
            }

            for (i = 0; i < 32; i++) {
                cbLdCpaCtrl1.Items.Add(i);
                cbVcselIbiasHighFaultThres.Items.Add(i);
                cbCeqBoostOvrd.Items.Add(i);
            }

            for (i = 0; i < 64; i++) {
                cbCtleAdaptive0.Items.Add(i);
                cbExtendedReach1.Items.Add(i);
                cbExtendedReach2.Items.Add(i);
            }

            for (i = 0; i < 128; i++) {
                cbCtleAdaptive2.Items.Add(i);
                cbCtleAdaptive3.Items.Add(i);
                cbBbStep.Items.Add(i);
            }

            for (i = 0; i < 256; i++) {
                cbVcselIbiasSet.Items.Add(i);
                cbVcselImodSet.Items.Add(i);
                cbLdCompGain0.Items.Add(i);
                cbLdCompGain1.Items.Add(i);
                cbExtendedReach0.Items.Add(i);
                cbVcoRangeOverrideEn.Items.Add(i);
            }
        }

        public int SetI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        public int SetI2cRead16CBApi(I2cRead16CB cb)
        {
            if (cb == null)
                return -1;

            i2cRead16CB = new I2cRead16CB(cb);

            return 0;
        }

        public int SetI2cWrite16CBApi(I2cWrite16CB cb)
        {
            if (cb == null)
                return -1;

            i2cWrite16CB = new I2cWrite16CB(cb);

            return 0;
        }

        private void _ParseAddr000(byte data)
        {
            if ((data & 0x01) == 0)
                cbCdrForceBypass.Checked = false;
            else
                cbCdrForceBypass.Checked = true;

            if ((data & 0x02) == 0)
                cbCdrAutoBypass.Checked = false;
            else
                cbCdrAutoBypass.Checked = true;

            if ((data & 0x04) == 0)
                cbCdrPdOnBypass.Checked = false;
            else
                cbCdrPdOnBypass.Checked = true;
        }

        private void _ParseAddr001(byte data)
        {
            cbLosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr002(byte data)
        {
            cbLosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr006(byte data)
        {
            if ((data & 0x01) == 0)
                cbOffsetCorrectionPolarityInv.Checked = false;
            else
                cbOffsetCorrectionPolarityInv.Checked = true;

            if ((data & 0x02) == 0)
                cbEqPolarityInvert.Checked = false;
            else
                cbEqPolarityInvert.Checked = true;
        }

        private void _ParseAddr012(byte data)
        {
            if ((data & 0x01) == 0)
                cbDrvForceMute.Checked = false;
            else
                cbDrvForceMute.Checked = true;
        }

        private void _ParseAddr013(byte data)
        {
            if ((data & 0x01) == 0)
                cbDrvMuteOnLos.Checked = false;
            else
                cbDrvMuteOnLos.Checked = true;

            if ((data & 0x02) == 0)
                cbDrvPdOnMute.Checked = false;
            else
                cbDrvPdOnMute.Checked = true;
        }

        private void _ParseAddr014(byte data)
        {
            cbVcselIbiasSet.SelectedIndex = data;
        }

        private void _ParseAddr015(byte data)
        {
            cbVcselImodSet.SelectedIndex = data;
        }

        private void _ParseAddr025(byte data)
        {
            cbLdCpaCtrl1.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr027(byte data)
        {
            cbLdCompGain0.SelectedIndex = data;
        }

        private void _ParseAddr028(byte data)
        {
            cbLdCompGain1.SelectedIndex = data;
        }

        private void _ParseAddr03E(byte data)
        {
            cbVcselIbiasHighFaultThres.SelectedIndex = data & 0x1F;
            cbVcselIbiasLowFaultThres.SelectedIndex = (data & 0xE0) >> 5;
        }

        private void _ParseAddr03F(byte data)
        {
            cbVcselVfHighFaulThres.SelectedIndex = data & 0x0F;
            cbVcselVfLowFaulThres.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr040(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcselIbiasHighFaultDetectEn.Checked = false;
            else
                cbVcselIbiasHighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbVcselIbiasLowFaultDetectEn.Checked = false;
            else
                cbVcselIbiasLowFaultDetectEn.Checked = true;

            if ((data & 0x04) == 0)
                cbVcselVfHighFaultDetectEn.Checked = false;
            else
                cbVcselVfHighFaultDetectEn.Checked = true;

            if ((data & 0x08) == 0)
                cbVcselVfLowFaultDetectEn.Checked = false;
            else
                cbVcselVfLowFaultDetectEn.Checked = true;

            if ((data & 0x10) == 0)
                cbVcc1HighFaultDetectEn.Checked = false;
            else
                cbVcc1HighFaultDetectEn.Checked = true;

            if ((data & 0x20) == 0)
                cbVcc1LowFaultDetectEn.Checked = false;
            else
                cbVcc1LowFaultDetectEn.Checked = true;

            if ((data & 0x40) == 0)
                cbTxDsblSoft.Checked = false;
            else
                cbTxDsblSoft.Checked = true;
        }

        private void _ParseAddr041(byte data)
        {
            if ((data & 0x01) == 0)
                cbChannelVcselIbiasHighAlarmDisable.Checked = false;
            else
                cbChannelVcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbChannelVcselIbiasLowAlarmDisable.Checked = false;
            else
                cbChannelVcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbChannelVcselVfHighAlarmDisable.Checked = false;
            else
                cbChannelVcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbChannelVcselVfLowAlarmDisable.Checked = false;
            else
                cbChannelVcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbChannelVcc1HighAlarmDisable.Checked = false;
            else
                cbChannelVcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbChannelVcc1LowAlarmDisable.Checked = false;
            else
                cbChannelVcc1LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr042(byte data)
        {
            if ((data & 0x01) == 0)
                cbChannelVcselIbiasHighAlarm.Checked = false;
            else
                cbChannelVcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbChannelVcselIbiasLowAlarm.Checked = false;
            else
                cbChannelVcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbChannelVcselVfHighAlarm.Checked = false;
            else
                cbChannelVcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbChannelVcselVfLowAlarm.Checked = false;
            else
                cbChannelVcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbChannelVcc1HighAlarm.Checked = false;
            else
                cbChannelVcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbChannelVcc1LowAlarm.Checked = false;
            else
                cbChannelVcc1LowAlarm.Checked = true;
        }

        private void _ParseAddr043_044(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbVcc1Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr045(byte data)
        {
            tbVcselIBias.Text = data.ToString();
        }

        private void _ParseAddr046(byte data)
        {
            tbVcselVf.Text = data.ToString();
        }

        private void _ParseAddr047(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdLane.Checked = false;
            else
                cbPdLane.Checked = true;
        }

        private void _ParseAddr048(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdLos.Checked = false;
            else
                cbPdLos.Checked = true;
        }

        private void _ParseAddr04B(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdCdr.Checked = false;
            else
                cbPdCdr.Checked = true;
        }

        private void _ParseAddr04D(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdDrv.Checked = false;
            else
                cbPdDrv.Checked = true;
        }

        private void _ParseAddr04E(byte data)
        {
            if ((data & 0x01) == 0)
                cbLdPdBiasMon.Checked = false;
            else
                cbLdPdBiasMon.Checked = true;

            if ((data & 0x02) == 0)
                cbLdPdVfMon.Checked = false;
            else
                cbLdPdVfMon.Checked = true;

            if ((data & 0x04) == 0)
                cbLdPdVcselComp.Checked = false;
            else
                cbLdPdVcselComp.Checked = true;
        }

        private void _ParseAddr053(byte data)
        {
            cbCtleAdaptive0.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr055(byte data)
        {
            cbCtleAdaptive1.SelectedIndex = data & 0x07;
        }

        private void _ParseAddr05A(byte data)
        {
            cbCtleAdaptive2.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr05B(byte data)
        {
            cbCtleAdaptive3.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr05E(byte data)
        {
            if ((data & 0x01) == 0)
                cbCeqBoostOvrdEn.Checked = false;
            else
                cbCeqBoostOvrdEn.Checked = true;

            cbCeqBoostOvrd.SelectedIndex = (data & 0x3E) >> 1;
        }

        private void _ParseAddr063(byte data)
        {
            cbCtleAdaptive4.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr06C(byte data)
        {
            if ((data & 0x01) == 0)
                cbCtleBoostInit.Checked = false;
            else
                cbCtleBoostInit.Checked = true;
        }
        
        private void bReadChannel_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bReadChannel.Enabled = false;

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr000(data[0]);
            _ParseAddr001(data[1]);
            _ParseAddr002(data[2]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0006).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr006(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0012).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr012(data[0]);
            _ParseAddr013(data[1]);
            _ParseAddr014(data[2]);
            _ParseAddr015(data[3]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0025).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr025(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0027).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr027(data[0]);
            _ParseAddr028(data[1]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x003E).Reverse().ToArray(), 11, data);
            if (rv != 11)
                goto exit;

            _ParseAddr03E(data[0]);
            _ParseAddr03F(data[1]);
            _ParseAddr040(data[2]);
            _ParseAddr041(data[3]);
            _ParseAddr042(data[4]);
            _ParseAddr043_044(data[5], data[6]);
            _ParseAddr045(data[7]);
            _ParseAddr046(data[8]);
            _ParseAddr047(data[9]);
            _ParseAddr048(data[10]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x004B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr04B(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x004D).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr04D(data[0]);
            _ParseAddr04E(data[1]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0053).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr053(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0055).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr055(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x005A).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr05A(data[0]);
            _ParseAddr05B(data[1]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x005E).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr05E(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0063).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr063(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x006C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr06C(data[0]);

        exit:
            reading = false;
            bReadChannel.Enabled = true;
        }

        private void _ParseAddr101(byte data)
        {
            if ((data & 0x01) == 0)
                cbFaultPolarity.Checked = false;
            else
                cbFaultPolarity.Checked = true;
        }

        private void _ParseAddr108(byte data)
        {
            if ((data & 0x01) == 0)
                cbLoslInvert.Checked = false;
            else
                cbLoslInvert.Checked = true;
        }

        private void _ParseAddr117(byte data)
        {
            if ((data & 0x01) == 0)
                cbLdBurnInEn.Checked = false;
            else
                cbLdBurnInEn.Checked = true;
        }

        private void _ParseAddr11C(byte data)
        {
            cbExtendedReach0.SelectedIndex = data;
        }

        private void _ParseAddr126(byte data)
        {
            cbExtendedReach1.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr128(byte data)
        {
            cbExtendedReach2.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr12C(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcc2HighFaultDetectEn.Checked = false;
            else
                cbVcc2HighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbVcc2LowFaultDetectEn.Checked = false;
            else
                cbVcc2LowFaultDetectEn.Checked = true;
        }

        private void _ParseAddr12D(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcselIbiasHighAlarmDisable.Checked = false;
            else
                cbVcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbVcselIbiasLowAlarmDisable.Checked = false;
            else
                cbVcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbVcselVfHighAlarmDisable.Checked = false;
            else
                cbVcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbVcselVfLowAlarmDisable.Checked = false;
            else
                cbVcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbVcc1HighAlarmDisable.Checked = false;
            else
                cbVcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbVcc1LowAlarmDisable.Checked = false;
            else
                cbVcc1LowAlarmDisable.Checked = true;

            if ((data & 0x40) == 0)
                cbVcc2HighAlarmDisable.Checked = false;
            else
                cbVcc2HighAlarmDisable.Checked = true;

            if ((data & 0x80) == 0)
                cbVcc2LowAlarmDisable.Checked = false;
            else
                cbVcc2LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr12E(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxFaultAlarmDisable.Checked = false;
            else
                cbTxFaultAlarmDisable.Checked = true;
        }

        private void _ParseAddr12F(byte data)
        {
            cbVcc1HighFaultThres.SelectedIndex = data & 0x03;
            cbVcc1LowFaultThres.SelectedIndex = (data & 0x0C) >> 2;
        }

        private void _ParseAddr130(byte data)
        {
            cbVcc2HighFaultThres.SelectedIndex = data & 0x03;
            cbVcc2LowFaultThres.SelectedIndex = (data & 0x0C) >> 2;
        }

        private void _ParseAddr132(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcselIbiasHighAlarm.Checked = false;
            else
                cbVcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbVcselIbiasLowAlarm.Checked = false;
            else
                cbVcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbVcselVfHighAlarm.Checked = false;
            else
                cbVcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbVcselVfLowAlarm.Checked = false;
            else
                cbVcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbVcc1HighAlarm.Checked = false;
            else
                cbVcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbVcc1LowAlarm.Checked = false;
            else
                cbVcc1LowAlarm.Checked = true;

            if ((data & 0x40) == 0)
                cbVcc2HighAlarm.Checked = false;
            else
                cbVcc2HighAlarm.Checked = true;

            if ((data & 0x80) == 0)
                cbVcc2LowAlarm.Checked = false;
            else
                cbVcc2LowAlarm.Checked = true;
        }

        private void _ParseAddr133(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxdsblInput.Checked = false;
            else
                cbTxdsblInput.Checked = true;
        }

        private void _ParseAddr134(byte data)
        {
            tbTempSensorTrim.Text = (data & 0x7F).ToString();
        }

        private void _ParseAddr135_136(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp += (data1 & 0x03) * 256;

            tbTemperature.Text = iTmp.ToString();
        }

        private void _ParseAddr137(byte data)
        {
            tbVcclSupply.Text = data.ToString();
        }

        private void _ParseAddr138_139(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp += (data1 & 0x03) * 256;

            tbVccSupply.Text = iTmp.ToString();
        }

        private void _ParseAddr13A_13B(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp += (data1 & 0x03) * 256;

            tbVcc2Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr140(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcParamMonCtrlReset.Checked = false;
            else
                cbAdcParamMonCtrlReset.Checked = true;
        }

        private void _ParseAddr143(byte data)
        {
            if ((data & 0x01) == 0)
                cbTemperatureMonEnable.Checked = false;
            else
                cbTemperatureMonEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbVcclSupplyMonEnable.Checked = false;
            else
                cbVcclSupplyMonEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbVccSupplyMonEnable.Checked = false;
            else
                cbVccSupplyMonEnable.Checked = true;

            if ((data & 0x08) == 0)
                cbVcc2SupplyMonEnable.Checked = false;
            else
                cbVcc2SupplyMonEnable.Checked = true;
        }

        private void _ParseAddr157(byte data)
        {
            tbRppbiasTrim.Text = (data & 0x3F).ToString();
        }

        private void _ParseAddr158(byte data)
        {
            cbBbStep.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr15F(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdAll.Checked = false;
            else
                cbPdAll.Checked = true;
        }

        private void _ParseAddr160(byte data)
        {
            if ((data & 0x01) == 0)
                cbLdPdVfmonStg2.Checked = false;
            else
                cbLdPdVfmonStg2.Checked = true;
        }

        private void _ParseAddr161(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdTempSense.Checked = false;
            else
                cbPdTempSense.Checked = true;

            if ((data & 0x02) == 0)
                cbPdSupplySense.Checked = false;
            else
                cbPdSupplySense.Checked = true;

            if ((data & 0x04) == 0)
                cbPdAdc.Checked = false;
            else
                cbPdAdc.Checked = true;
        }

        private void _ParseAddr164(byte data)
        {
            tbVersion.Text = data.ToString();
        }

        private void _ParseAddr174(byte data)
        {
            cbVcoRangeOverride.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr177(byte data)
        {
            cbVcoRangeOverrideEn.SelectedIndex = data;
        }

        private void _ParseAddr199(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxFault.Checked = false;
            else
                cbTxFault.Checked = true;
        }

        private void _ParseAddr19A(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcc1SupplyMonEnable.Checked = false;
            else
                cbVcc1SupplyMonEnable.Checked = true;
        }

        private void _ParseAddr19B(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcselIbiasMonEnable.Checked = false;
            else
                cbVcselIbiasMonEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbVcselVfMonEnable.Checked = false;
            else
                cbVcselVfMonEnable.Checked = true;
        }

        private void _ParseAddr19F(byte data)
        {
            if ((data & 0x01) == 0)
                cbLosSoftAssertEn.Checked = false;
            else
                cbLosSoftAssertEn.Checked = true;

            if ((data & 0x02) == 0)
                cbLosSoftAssert.Checked = false;
            else
                cbLosSoftAssert.Checked = true;
        }

        private void _ParseAddr1A1(byte data)
        {
            if ((data & 0x01) == 0)
                cbLosChangeDetect.Checked = false;
            else
                cbLosChangeDetect.Checked = true;
        }

        private void _ParseAddr1A3(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolSoftAssertEn.Checked = false;
            else
                cbLolSoftAssertEn.Checked = true;

            if ((data & 0x02) == 0)
                cbLolSoftAssert.Checked = false;
            else
                cbLolSoftAssert.Checked = true;
        }

        private void _ParseAddr1A4(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolInvert.Checked = false;
            else
                cbLolInvert.Checked = true;
        }

        private void _ParseAddr1A5(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolChangeDetect.Checked = false;
            else
                cbLolChangeDetect.Checked = true;
        }

        private void _ParseAddr1A7(byte data)
        {
            if ((data & 0x01) == 0)
                cbLosToLosEn.Checked = false;
            else
                cbLosToLosEn.Checked = true;

            if ((data & 0x02) == 0)
                cbLolToLosEn.Checked = false;
            else
                cbLolToLosEn.Checked = true;
        }

        private void _ParseAddr1A8(byte data)
        {
            if ((data & 0x01) == 0)
                cbLos.Checked = false;
            else
                cbLos.Checked = true;

            if ((data & 0x02) == 0)
                cbLol.Checked = false;
            else
                cbLol.Checked = true;
        }

        private void _ParseAddr1A9(byte data)
        {
            if ((data & 0x01) == 0)
                cbLosLatch.Checked = false;
            else
                cbLosLatch.Checked = true;

            if ((data & 0x02) == 0)
                cbLolLatch.Checked = false;
            else
                cbLolLatch.Checked = true;
        }

        private void bReadControl_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[10];
            int rv;

            if (reading == true)
                return;

            reading = true;

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0101).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr101(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0108).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr108(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr117(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x011C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr11C(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0126).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr126(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0128).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr128(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x012C).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr12C(data[0]);
            _ParseAddr12D(data[1]);
            _ParseAddr12E(data[2]);
            _ParseAddr12F(data[3]);
            _ParseAddr130(data[4]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0132).Reverse().ToArray(), 10, data);
            if (rv != 10)
                goto exit;

            _ParseAddr132(data[0]);
            _ParseAddr133(data[1]);
            _ParseAddr134(data[2]);
            _ParseAddr135_136(data[3], data[4]);
            _ParseAddr137(data[5]);
            _ParseAddr138_139(data[6], data[7]);
            _ParseAddr13A_13B(data[8], data[9]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0140).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr140(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0143).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr143(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0157).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr157(data[0]);
            _ParseAddr158(data[1]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x015F).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr15F(data[0]);
            _ParseAddr160(data[1]);
            _ParseAddr161(data[2]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0164).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr164(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0174).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr174(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0177).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr177(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x0199).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr199(data[0]);
            _ParseAddr19A(data[1]);
            _ParseAddr19B(data[2]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x019F).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr19F(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x01A1).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr1A1(data[0]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x01A3).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr1A3(data[0]);
            _ParseAddr1A4(data[1]);
            _ParseAddr1A5(data[2]);

            rv = i2cRead16CB(86, BitConverter.GetBytes((ushort)0x01A7).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr1A7(data[0]);
            _ParseAddr1A8(data[1]);
            _ParseAddr1A9(data[2]);

        exit:
            reading = false;
            bReadControl.Enabled = true;
        }

        private int _WriteAddr000()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbCdrForceBypass.Checked == true)
                data[0] |= 0x01;
            if (cbCdrAutoBypass.Checked == true)
                data[0] |= 0x02;
            if (cbCdrPdOnBypass.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr001()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0001).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr002()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0002).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr006()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbOffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;
            if (cbEqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0006).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr012()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbDrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0012).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr013()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbDrvMuteOnLos.Checked == true)
                data[0] |= 0x01;
            if (cbDrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0013).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr014()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcselIbiasSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0014).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr015()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcselImodSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0015).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr025()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLdCpaCtrl1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0025).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr027()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLdCompGain0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0027).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr028()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLdCompGain1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0028).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        
        private int _WriteAddr03E ()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcselIbiasHighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbVcselIbiasLowFaultThres.SelectedItem);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x003E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr03F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcselVfHighFaulThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbVcselVfLowFaulThres.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x003F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr040()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcselIbiasHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;
            if (cbVcselIbiasLowFaultDetectEn.Checked == true)
                data[0] |= 0x02;
            if (cbVcselVfHighFaultDetectEn.Checked == true)
                data[0] |= 0x04;
            if (cbVcselVfLowFaultDetectEn.Checked == true)
                data[0] |= 0x08;
            if (cbVcc1HighFaultDetectEn.Checked == true)
                data[0] |= 0x10;
            if (cbVcc1LowFaultDetectEn.Checked == true)
                data[0] |= 0x20;
            if (cbTxDsblSoft.Checked == true)
                data[0] |= 0x40;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0040).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr041()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbChannelVcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;
            if (cbChannelVcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;
            if (cbChannelVcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;
            if (cbChannelVcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;
            if (cbChannelVcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;
            if (cbChannelVcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0041).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr042()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbChannelVcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;
            if (cbChannelVcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;
            if (cbChannelVcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;
            if (cbChannelVcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;
            if (cbChannelVcc1HighAlarm.Checked == true)
                data[0] |= 0x10;
            if (cbChannelVcc1LowAlarm.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0042).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr047()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdLane.Checked == true)
                data[0] |= 0x01;
            
            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0047).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr048()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0048).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdCdr.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x004B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdDrv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x004D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLdPdBiasMon.Checked == true)
                data[0] |= 0x01;
            if (cbLdPdVfMon.Checked == true)
                data[0] |= 0x02;
            if (cbLdPdVcselComp.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x004E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr053()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCtleAdaptive0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0053).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr055()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCtleAdaptive1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0055).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr05A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCtleAdaptive2.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x005A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr05B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCtleAdaptive3.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x005B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr05E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbCeqBoostOvrdEn.Checked == true)
                data[0] |= 0x01;
            bTmp = Convert.ToByte(cbCeqBoostOvrd.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x005E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr063()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCtleAdaptive4.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0063).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr06C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbCtleBoostInit.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x006C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbCdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbCdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbLosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr001() < 0)
                return;
        }

        private void cbLosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr002() < 0)
                return;
        }

        private void cbOffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr006() < 0)
                return;
        }

        private void cbDrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr012() < 0)
                return;
        }

        private void cbDrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr013() < 0)
                return;
        }

        private void cbVcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr014() < 0)
                return;
        }

        private void cbVcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr015() < 0)
                return;
        }

        private void cbLdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr025() < 0)
                return;
        }

        private void cbLdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr027() < 0)
                return;
        }

        private void cbLdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr028() < 0)
                return;
        }

        private void cbVcselIbiasHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03E() < 0)
                return;
        }

        private void cbVcselIbiasLowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03E() < 0)
                return;
        }

        private void cbVcselVfHighFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03F() < 0)
                return;
        }

        private void cbVcselVfLowFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03F() < 0)
                return;
        }

        private void cbVcselIbiasHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbVcselIbiasLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbVcselVfHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbVcselVfLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbVcc1HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbVcc1LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbTxDsblSoft_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr040() < 0)
                return;
        }

        private void cbChannelVcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr041() < 0)
                return;
        }

        private void cbChannelVcselIbiasHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbChannelVcselIbiasLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbChannelVcselVfHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbChannelVcselVfLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbChannelVcc1HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbChannelVcc1LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr042() < 0)
                return;
        }

        private void cbPdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbPdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr048() < 0)
                return;
        }

        private void cbPdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04B() < 0)
                return;
        }

        private void cbPdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04D() < 0)
                return;
        }

        private void cbLdPdBiasMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04E() < 0)
                return;
        }

        private void cbCtleAdaptive0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr053() < 0)
                return;
        }

        private void cbCtleAdaptive1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr055() < 0)
                return;
        }

        private void cbCtleAdaptive2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr05A() < 0)
                return;
        }

        private void cbCtleAdaptive3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr05B() < 0)
                return;
        }

        private void cbCeqBoostOvrdEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr05E() < 0)
                return;
        }

        private void cbCeqBoostOvrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr05E() < 0)
                return;
        }

        private void cbCtleAdaptive4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr063() < 0)
                return;
        }

        private void cbCtleBoostInit_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr06C() < 0)
                return;
        }

        private int _WriteAddr101()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbFaultPolarity.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0101).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr108()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLoslInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0108).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr117()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLdBurnInEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbExtendedReach0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x011C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr126()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbExtendedReach1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0126).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr128()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbExtendedReach2.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0128).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcc2HighFaultDetectEn.Checked == true)
                data[0] |= 0x01;
            if (cbVcc2LowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x012C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;
            if (cbVcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;
            if (cbVcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;
            if (cbVcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;
            if (cbVcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;
            if (cbVcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;
            if (cbVcc2HighAlarmDisable.Checked == true)
                data[0] |= 0x40;
            if (cbVcc2LowAlarmDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x012D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTxFaultAlarmDisable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x012E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcc1HighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbVcc1LowFaultThres.SelectedItem);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x012F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr130()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcc2HighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbVcc2LowFaultThres.SelectedItem);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0130).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr132()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;
            if (cbVcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;
            if (cbVcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;
            if (cbVcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;
            if (cbVcc1HighAlarm.Checked == true)
                data[0] |= 0x10;
            if (cbVcc1LowAlarm.Checked == true)
                data[0] |= 0x20;
            if (cbVcc2HighAlarm.Checked == true)
                data[0] |= 0x40;
            if (cbVcc2LowAlarm.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0132).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr140()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0140).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr143()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTemperatureMonEnable.Checked == true)
                data[0] |= 0x01;
            if (cbVcclSupplyMonEnable.Checked == true)
                data[0] |= 0x02;
            if (cbVccSupplyMonEnable.Checked == true)
                data[0] |= 0x04;
            if (cbVcc2SupplyMonEnable.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0143).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr158()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbBbStep.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0158).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr15F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x015F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr160()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLdPdVfmonStg2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0160).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr161()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdTempSense.Checked == true)
                data[0] |= 0x01;
            if (cbPdSupplySense.Checked == true)
                data[0] |= 0x02;
            if (cbPdAdc.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0161).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr174()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcoRangeOverride.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0174).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr177()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcoRangeOverrideEn.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x0177).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr19A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcc1SupplyMonEnable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x019A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr19B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcselIbiasMonEnable.Checked == true)
                data[0] |= 0x01;
            if (cbVcselVfMonEnable.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x019B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr19F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosSoftAssertEn.Checked == true)
                data[0] |= 0x01;
            if (cbLosSoftAssert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x019F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A1()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosChangeDetect.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A1).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A3()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolSoftAssertEn.Checked == true)
                data[0] |= 0x01;
            if (cbLolSoftAssert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A3).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A4()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A4).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A5()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolChangeDetect.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A5).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A7()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosToLosEn.Checked == true)
                data[0] |= 0x01;
            if (cbLolToLosEn.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A7).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A9()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosLatch.Checked == true)
                data[0] |= 0x01;
            if (cbLolLatch.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x01A9).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbFaultPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr101() < 0)
                return;
        }

        private void cbLoslInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr108() < 0)
                return;
        }

        private void cbLdBurnInEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr117() < 0)
                return;
        }

        private void cbExtendedReach0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11C() < 0)
                return;
        }

        private void cbExtendedReach1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr126() < 0)
                return;
        }

        private void cbExtendedReach2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr128() < 0)
                return;
        }

        private void cbVcc2HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12C() < 0)
                return;
        }

        private void cbVcc2LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12C() < 0)
                return;
        }

        private void cbVcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcc2HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbVcc2LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12D() < 0)
                return;
        }

        private void cbTxFaultAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12E() < 0)
                return;
        }

        private void cbVcc1HighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12F() < 0)
                return;
        }

        private void cbVcc1LowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12F() < 0)
                return;
        }

        private void cbVcc2HighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private void cbVcc2LowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private void cbVcc2HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr132() < 0)
                return;
        }

        private void cbVcc2LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr132() < 0)
                return;
        }

        private void cbAdcParamMonCtrlReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr140() < 0)
                return;
        }

        private void cbTemperatureMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbVcclSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbVccSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbVcc2SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbBbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr158() < 0)
                return;
        }

        private void cbPdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15F() < 0)
                return;
        }

        private void cbLdPdVfmonStg2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr160() < 0)
                return;
        }

        private void cbPdTempSense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr161() < 0)
                return;
        }

        private void cbPdSupplySense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr161() < 0)
                return;
        }

        private void cbPdAdc_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr161() < 0)
                return;
        }

        private void cbVcoRangeOverride_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr174() < 0)
                return;
        }

        private void cbVcoRangeOverrideEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr177() < 0)
                return;
        }

        private void cbVcc1SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19A() < 0)
                return;
        }

        private void cbVcselIbiasMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19B() < 0)
                return;
        }

        private void cbVcselVfMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19B() < 0)
                return;
        }

        private void cbLosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19F() < 0)
                return;
        }

        private void cbLosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19F() < 0)
                return;
        }

        private void cbLosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A1() < 0)
                return;
        }

        private void cbLolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A3() < 0)
                return;
        }

        private void cbLolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A3() < 0)
                return;
        }

        private void cbLolInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A4() < 0)
                return;
        }

        private void cbLolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A5() < 0)
                return;
        }

        private void cbLosToLosEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A7() < 0)
                return;
        }

        private void cbLolToLosEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A7() < 0)
                return;
        }

        private void cbLosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A9() < 0)
                return;
        }

        private void cbLolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A9() < 0)
                return;
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            int rv;

            bStoreIntoFlash.Enabled = false;
            rv = i2cWrite16CB(86, BitConverter.GetBytes((ushort)0x05AA).Reverse().ToArray(), 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
        }
    }
}
