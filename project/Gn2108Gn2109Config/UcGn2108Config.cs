using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gn2108Gn2109Config
{
    public partial class UcGn2108Config : UserControl
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

        public UcGn2108Config()
        {
            int i;

            InitializeComponent();

            cbL0LosThres.Items.Add(10);
            cbL0LosThres.Items.Add(11);
            cbL0LosThres.Items.Add(12);
            cbL0LosThres.Items.Add(13);
            cbL0LosThres.Items.Add(14);
            cbL0LosThres.Items.Add(15);

            cbL1LosThres.Items.Add(10);
            cbL1LosThres.Items.Add(11);
            cbL1LosThres.Items.Add(12);
            cbL1LosThres.Items.Add(13);
            cbL1LosThres.Items.Add(14);
            cbL1LosThres.Items.Add(15);

            cbL2LosThres.Items.Add(10);
            cbL2LosThres.Items.Add(11);
            cbL2LosThres.Items.Add(12);
            cbL2LosThres.Items.Add(13);
            cbL2LosThres.Items.Add(14);
            cbL2LosThres.Items.Add(15);

            cbL3LosThres.Items.Add(10);
            cbL3LosThres.Items.Add(11);
            cbL3LosThres.Items.Add(12);
            cbL3LosThres.Items.Add(13);
            cbL3LosThres.Items.Add(14);
            cbL3LosThres.Items.Add(15);

            cbAdcSrcSel.Items.Add(0);
            cbAdcSrcSel.Items.Add(28);
            cbAdcSrcSel.Items.Add(29);
            cbAdcSrcSel.Items.Add(30);
            cbAdcSrcSel.Items.Add(31);

            for (i = 0; i < 4; i++) {
                cbL0LosHyst.Items.Add(i);
                cbL1LosHyst.Items.Add(i);
                cbL2LosHyst.Items.Add(i);
                cbL3LosHyst.Items.Add(i);
                cbLockdetLockThres.Items.Add(i);
                cbVcc1HighFaultThres.Items.Add(i);
                cbVcc1LowFaultThres.Items.Add(i);
                cbVcc2HighFaultThres.Items.Add(i);
                cbVcc2LowFaultThres.Items.Add(i);
                cbPrbsgenSequenceSel.Items.Add(i);
                cbPrbsgenOutputSel.Items.Add(i);
                cbPrbschkSelPrbs.Items.Add(i);
            }

            for (i = 0; i < 8; i++) {
                cbL0VcselIbiasLowFaultThres.Items.Add(i);
                cbL1VcselIbiasLowFaultThres.Items.Add(i);
                cbL2VcselIbiasLowFaultThres.Items.Add(i);
                cbL3VcselIbiasLowFaultThres.Items.Add(i);
                cbPrbsgenVcoFreq.Items.Add(i);
                if (i < 7)
                    cbAdcResolution.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbL0VcselVfHighFaulThres.Items.Add(i);
                cbL0VcselVfLowFaulThres.Items.Add(i);
                cbL1VcselVfHighFaulThres.Items.Add(i);
                cbL1VcselVfLowFaulThres.Items.Add(i);
                cbL2VcselVfHighFaulThres.Items.Add(i);
                cbL2VcselVfLowFaulThres.Items.Add(i);
                cbL3VcselVfHighFaulThres.Items.Add(i);
                cbL3VcselVfLowFaulThres.Items.Add(i);
                cbPrbsgenCkdivRate.Items.Add(i);
                cbPrbschkTimerClkSel.Items.Add(i);
            }

            for (i = 0; i < 32; i++) {
                cbL0LdCpaCtrl1.Items.Add(i);
                cbL0LdCpaCtrl2.Items.Add(i);
                cbL0VcselIbiasHighFaultThres.Items.Add(i);
                cbL1LdCpaCtrl1.Items.Add(i);
                cbL1LdCpaCtrl2.Items.Add(i);
                cbL1VcselIbiasHighFaultThres.Items.Add(i);
                cbL2LdCpaCtrl1.Items.Add(i);
                cbL2LdCpaCtrl2.Items.Add(i);
                cbL2VcselIbiasHighFaultThres.Items.Add(i);
                cbL3LdCpaCtrl1.Items.Add(i);
                cbL3LdCpaCtrl2.Items.Add(i);
                cbL3VcselIbiasHighFaultThres.Items.Add(i);
                cbLdCompGain0Scale.Items.Add(i);
                cbLdCompGain1Scale.Items.Add(i);
                cbAllLdCpaCtrl1.Items.Add(i);
                cbAllLdCpaCtrl2.Items.Add(i);
            }

            for (i = 0; i < 128; i++) {
                cbL0EqBoost.Items.Add(i);
                cbL0CkgenMclkPhase.Items.Add(i);
                cbL1EqBoost.Items.Add(i);
                cbL1CkgenMclkPhase.Items.Add(i);
                cbL2EqBoost.Items.Add(i);
                cbL2CkgenMclkPhase.Items.Add(i);
                cbL3EqBoost.Items.Add(i);
                cbL3CkgenMclkPhase.Items.Add(i);
                cbAllEqBoost.Items.Add(i);
                cbL0InputEqualization0db.Items.Add(i);
                cbL0InputEqualization1db.Items.Add(i);
                cbL0InputEqualization2db.Items.Add(i);
                cbL0InputEqualization3db.Items.Add(i);
                cbL0InputEqualization4db.Items.Add(i);
                cbL0InputEqualization5db.Items.Add(i);
                cbL0InputEqualization6db.Items.Add(i);
                cbL0InputEqualization7db.Items.Add(i);
                cbL0InputEqualization8db.Items.Add(i);
                cbL0InputEqualization9db.Items.Add(i);
                cbL0InputEqualization10db.Items.Add(i);
                cbL0InputEqualizationReserved0.Items.Add(i);
                cbL0InputEqualizationReserved1.Items.Add(i);
                cbL1InputEqualization0db.Items.Add(i);
                cbL1InputEqualization1db.Items.Add(i);
                cbL1InputEqualization2db.Items.Add(i);
                cbL1InputEqualization3db.Items.Add(i);
                cbL1InputEqualization4db.Items.Add(i);
                cbL1InputEqualization5db.Items.Add(i);
                cbL1InputEqualization6db.Items.Add(i);
                cbL1InputEqualization7db.Items.Add(i);
                cbL1InputEqualization8db.Items.Add(i);
                cbL1InputEqualization9db.Items.Add(i);
                cbL1InputEqualization10db.Items.Add(i);
                cbL1InputEqualizationReserved0.Items.Add(i);
                cbL1InputEqualizationReserved1.Items.Add(i);
                cbL2InputEqualization0db.Items.Add(i);
                cbL2InputEqualization1db.Items.Add(i);
                cbL2InputEqualization2db.Items.Add(i);
                cbL2InputEqualization3db.Items.Add(i);
                cbL2InputEqualization4db.Items.Add(i);
                cbL2InputEqualization5db.Items.Add(i);
                cbL2InputEqualization6db.Items.Add(i);
                cbL2InputEqualization7db.Items.Add(i);
                cbL2InputEqualization8db.Items.Add(i);
                cbL2InputEqualization9db.Items.Add(i);
                cbL2InputEqualization10db.Items.Add(i);
                cbL2InputEqualizationReserved0.Items.Add(i);
                cbL2InputEqualizationReserved1.Items.Add(i);
                cbL3InputEqualization0db.Items.Add(i);
                cbL3InputEqualization1db.Items.Add(i);
                cbL3InputEqualization2db.Items.Add(i);
                cbL3InputEqualization3db.Items.Add(i);
                cbL3InputEqualization4db.Items.Add(i);
                cbL3InputEqualization5db.Items.Add(i);
                cbL3InputEqualization6db.Items.Add(i);
                cbL3InputEqualization7db.Items.Add(i);
                cbL3InputEqualization8db.Items.Add(i);
                cbL3InputEqualization9db.Items.Add(i);
                cbL3InputEqualization10db.Items.Add(i);
                cbL3InputEqualizationReserved0.Items.Add(i);
                cbL3InputEqualizationReserved1.Items.Add(i);
            }

            for (i = 0; i < 256; i++) {
                cbL0VcselIbiasSet.Items.Add(i);
                cbL0VcselImodSet.Items.Add(i);
                cbL0LdCompGain0.Items.Add(i);
                cbL0LdCompGain1.Items.Add(i);
                cbL1VcselIbiasSet.Items.Add(i);
                cbL1VcselImodSet.Items.Add(i);
                cbL1LdCompGain0.Items.Add(i);
                cbL1LdCompGain1.Items.Add(i);
                cbL2VcselIbiasSet.Items.Add(i);
                cbL2VcselImodSet.Items.Add(i);
                cbL2LdCompGain0.Items.Add(i);
                cbL2LdCompGain1.Items.Add(i);
                cbL3VcselIbiasSet.Items.Add(i);
                cbL3VcselImodSet.Items.Add(i);
                cbL3LdCompGain0.Items.Add(i);
                cbL3LdCompGain1.Items.Add(i);
                cbBbStep.Items.Add(i);
                cbAllVcselIbiasSet.Items.Add(i);
                cbAllVcselImodSet.Items.Add(i);
                cbAllLdCompGain0.Items.Add(i);
                cbAllLdCompGain1.Items.Add(i);
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
                cbL0CdrForceBypass.Checked = false;
            else
                cbL0CdrForceBypass.Checked = true;

            if ((data & 0x02) == 0)
                cbL0CdrAutoBypass.Checked = false;
            else
                cbL0CdrAutoBypass.Checked = true;

            if ((data & 0x04) == 0)
                cbL0CdrPdOnBypass.Checked = false;
            else
                cbL0CdrPdOnBypass.Checked = true;
        }

        private void _ParseAddr001(byte data)
        {
            cbL0LosThres.SelectedIndex = cbL0LosThres.FindStringExact((data & 0x0F).ToString());
        }

        private void _ParseAddr002(byte data)
        {
            cbL0LosHyst.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr006(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0OffsetCorrectionPolarityInv.Checked = false;
            else
                cbL0OffsetCorrectionPolarityInv.Checked = true;

            if ((data & 0x02) == 0)
                cbL0EqPolarityInvert.Checked = false;
            else
                cbL0EqPolarityInvert.Checked = true;
        }

        private void _ParseAddr008(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0EqLoopbackEn.Checked = false;
            else
                cbL0EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr009(byte data)
        {
            cbL0EqBoost.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr015(byte data)
        {
            cbL0CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr016(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0DrvForceMute.Checked = false;
            else
                cbL0DrvForceMute.Checked = true;
        }

        private void _ParseAddr017(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0DrvMuteOnLos.Checked = false;
            else
                cbL0DrvMuteOnLos.Checked = true;

            if ((data & 0x02) == 0)
                cbL0DrvPdOnMute.Checked = false;
            else
                cbL0DrvPdOnMute.Checked = true;
        }

        private void _ParseAddr018(byte data)
        {
            cbL0VcselIbiasSet.SelectedIndex = data;
        }

        private void _ParseAddr019(byte data)
        {
            cbL0VcselImodSet.SelectedIndex = data;
        }

        private void _ParseAddr01A(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LdFiltOffReset.Checked = false;
            else
                cbL0LdFiltOffReset.Checked = true;
        }

        private void _ParseAddr020(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LdBiasOffsetReset.Checked = false;
            else
                cbL0LdBiasOffsetReset.Checked = true;
        }

        private void _ParseAddr029(byte data)
        {
            cbL0LdCpaCtrl1.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr02A(byte data)
        {
            cbL0LdCpaCtrl2.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr02B(byte data)
        {
            cbL0LdCompGain0.SelectedIndex = data;
        }

        private void _ParseAddr02C(byte data)
        {
            cbL0LdCompGain1.SelectedIndex = data;
        }

        private void _ParseAddr03C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasHighFaultOffsetReset.Checked = false;
            else
                cbL0VcselIbiasHighFaultOffsetReset.Checked = true;
        }

        private void _ParseAddr043(byte data)
        {
            cbL0VcselIbiasHighFaultThres.SelectedIndex = data & 0x1F;
            cbL0VcselIbiasLowFaultThres.SelectedIndex = (data & 0xE0) >> 5;
        }

        private void _ParseAddr044(byte data)
        {
            cbL0VcselVfHighFaulThres.SelectedIndex = data & 0x0F;
            cbL0VcselVfLowFaulThres.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr045(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasHighFaultDetectEn.Checked = false;
            else
                cbL0VcselIbiasHighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL0VcselIbiasLowFaultDetectEn.Checked = false;
            else
                cbL0VcselIbiasLowFaultDetectEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL0VcselVfHighFaultDetectEn.Checked = false;
            else
                cbL0VcselVfHighFaultDetectEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL0VcselVfLowFaultDetectEn.Checked = false;
            else
                cbL0VcselVfLowFaultDetectEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL0Vcc1HighFaultDetectEn.Checked = false;
            else
                cbL0Vcc1HighFaultDetectEn.Checked = true;

            if ((data & 0x20) == 0)
                cbL0Vcc1LowFaultDetectEn.Checked = false;
            else
                cbL0Vcc1LowFaultDetectEn.Checked = true;
        }

        private void _ParseAddr046(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasHighAlarmDisable.Checked = false;
            else
                cbL0VcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbL0VcselIbiasLowAlarmDisable.Checked = false;
            else
                cbL0VcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbL0VcselVfHighAlarmDisable.Checked = false;
            else
                cbL0VcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbL0VcselVfLowAlarmDisable.Checked = false;
            else
                cbL0VcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbL0Vcc1HighAlarmDisable.Checked = false;
            else
                cbL0Vcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbL0Vcc1LowAlarmDisable.Checked = false;
            else
                cbL0Vcc1LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr047(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasHighAlarm.Checked = false;
            else
                cbL0VcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbL0VcselIbiasLowAlarm.Checked = false;
            else
                cbL0VcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbL0VcselVfHighAlarm.Checked = false;
            else
                cbL0VcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbL0VcselVfLowAlarm.Checked = false;
            else
                cbL0VcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbL0Vcc1HighAlarm.Checked = false;
            else
                cbL0Vcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbL0Vcc1LowAlarm.Checked = false;
            else
                cbL0Vcc1LowAlarm.Checked = true;
        }

        private void _ParseAddr048_049(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL0Vcc1Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr04A(byte data)
        {
            tbL0VcselIBias.Text = data.ToString();
        }

        private void _ParseAddr04B(byte data)
        {
            tbL0VcselVf.Text = data.ToString();
        }

        private void _ParseAddr04C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdLane.Checked = false;
            else
                cbL0PdLane.Checked = true;
        }

        private void _ParseAddr04D(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdLos.Checked = false;
            else
                cbL0PdLos.Checked = true;
        }

        private void _ParseAddr050(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdCdr.Checked = false;
            else
                cbL0PdCdr.Checked = true;

            if ((data & 0x02) == 0)
                cbL0PdPPdrtMclk.Checked = false;
            else
                cbL0PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr052(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdDrv.Checked = false;
            else
                cbL0PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL0PdPrbClk.Checked = false;
            else
                cbL0PdPrbClk.Checked = true;

            if ((data & 0x04) == 0)
                cbL0PdDrvPrbschk.Checked = false;
            else
                cbL0PdDrvPrbschk.Checked = true;
        }

        private void _ParseAddr053(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LdPdBiasMon.Checked = false;
            else
                cbL0LdPdBiasMon.Checked = true;

            if ((data & 0x02) == 0)
                cbL0LdPdVfMon.Checked = false;
            else
                cbL0LdPdVfMon.Checked = true;

            if ((data & 0x04) == 0)
                cbL0LdPdVcselComp.Checked = false;
            else
                cbL0LdPdVcselComp.Checked = true;
        }

        private void bL0Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL0Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr000(data[0]);
            _ParseAddr001(data[1]);
            _ParseAddr002(data[2]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0006).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr006(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0008).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr008(data[0]);
            _ParseAddr009(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0015).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr015(data[0]);
            _ParseAddr016(data[1]);
            _ParseAddr017(data[2]);
            _ParseAddr018(data[3]);
            _ParseAddr019(data[4]);
            _ParseAddr01A(data[5]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0020).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr020(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0029).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr029(data[0]);
            _ParseAddr02A(data[1]);
            _ParseAddr02B(data[2]);
            _ParseAddr02C(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x003C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr03C(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0043).Reverse().ToArray(), 11, data);
            if (rv != 11)
                goto exit;

            _ParseAddr043(data[0]);
            _ParseAddr044(data[1]);
            _ParseAddr045(data[2]);
            _ParseAddr046(data[3]);
            _ParseAddr047(data[4]);
            _ParseAddr048_049(data[5], data[6]);
            _ParseAddr04A(data[7]);
            _ParseAddr04B(data[8]);
            _ParseAddr04C(data[9]);
            _ParseAddr04D(data[10]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0050).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr050(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0052).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr052(data[0]);
            _ParseAddr053(data[1]);

        exit:
            reading = false;
            bL0Read.Enabled = true;
        }

        private void _ParseAddr100(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1CdrForceBypass.Checked = false;
            else
                cbL1CdrForceBypass.Checked = true;

            if ((data & 0x02) == 0)
                cbL1CdrAutoBypass.Checked = false;
            else
                cbL1CdrAutoBypass.Checked = true;

            if ((data & 0x04) == 0)
                cbL1CdrPdOnBypass.Checked = false;
            else
                cbL1CdrPdOnBypass.Checked = true;
        }

        private void _ParseAddr101(byte data)
        {
            cbL1LosThres.SelectedIndex = cbL1LosThres.FindStringExact((data & 0x0F).ToString());
        }

        private void _ParseAddr102(byte data)
        {
            cbL1LosHyst.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr106(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1OffsetCorrectionPolarityInv.Checked = false;
            else
                cbL1OffsetCorrectionPolarityInv.Checked = true;

            if ((data & 0x02) == 0)
                cbL1EqPolarityInvert.Checked = false;
            else
                cbL1EqPolarityInvert.Checked = true;
        }

        private void _ParseAddr108(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1EqLoopbackEn.Checked = false;
            else
                cbL1EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr109(byte data)
        {
            cbL1EqBoost.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr115(byte data)
        {
            cbL1CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr116(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1DrvForceMute.Checked = false;
            else
                cbL1DrvForceMute.Checked = true;
        }

        private void _ParseAddr117(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1DrvMuteOnLos.Checked = false;
            else
                cbL1DrvMuteOnLos.Checked = true;

            if ((data & 0x02) == 0)
                cbL1DrvPdOnMute.Checked = false;
            else
                cbL1DrvPdOnMute.Checked = true;
        }

        private void _ParseAddr118(byte data)
        {
            cbL1VcselIbiasSet.SelectedIndex = data;
        }

        private void _ParseAddr119(byte data)
        {
            cbL1VcselImodSet.SelectedIndex = data;
        }

        private void _ParseAddr11A(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1LdFiltOffReset.Checked = false;
            else
                cbL1LdFiltOffReset.Checked = true;
        }

        private void _ParseAddr120(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1LdBiasOffsetReset.Checked = false;
            else
                cbL1LdBiasOffsetReset.Checked = true;
        }

        private void _ParseAddr129(byte data)
        {
            cbL1LdCpaCtrl1.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr12A(byte data)
        {
            cbL1LdCpaCtrl2.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr12B(byte data)
        {
            cbL1LdCompGain0.SelectedIndex = data;
        }

        private void _ParseAddr12C(byte data)
        {
            cbL1LdCompGain1.SelectedIndex = data;
        }

        private void _ParseAddr13C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1VcselIbiasHighFaultOffsetReset.Checked = false;
            else
                cbL1VcselIbiasHighFaultOffsetReset.Checked = true;
        }

        private void _ParseAddr143(byte data)
        {
            cbL1VcselIbiasHighFaultThres.SelectedIndex = data & 0x1F;
            cbL1VcselIbiasLowFaultThres.SelectedIndex = (data & 0xE0) >> 5;
        }

        private void _ParseAddr144(byte data)
        {
            cbL1VcselVfHighFaulThres.SelectedIndex = data & 0x0F;
            cbL1VcselVfLowFaulThres.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr145(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1VcselIbiasHighFaultDetectEn.Checked = false;
            else
                cbL1VcselIbiasHighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL1VcselIbiasLowFaultDetectEn.Checked = false;
            else
                cbL1VcselIbiasLowFaultDetectEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL1VcselVfHighFaultDetectEn.Checked = false;
            else
                cbL1VcselVfHighFaultDetectEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL1VcselVfLowFaultDetectEn.Checked = false;
            else
                cbL1VcselVfLowFaultDetectEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL1Vcc1HighFaultDetectEn.Checked = false;
            else
                cbL1Vcc1HighFaultDetectEn.Checked = true;

            if ((data & 0x20) == 0)
                cbL1Vcc1LowFaultDetectEn.Checked = false;
            else
                cbL1Vcc1LowFaultDetectEn.Checked = true;
        }

        private void _ParseAddr146(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1VcselIbiasHighAlarmDisable.Checked = false;
            else
                cbL1VcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbL1VcselIbiasLowAlarmDisable.Checked = false;
            else
                cbL1VcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbL1VcselVfHighAlarmDisable.Checked = false;
            else
                cbL1VcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbL1VcselVfLowAlarmDisable.Checked = false;
            else
                cbL1VcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbL1Vcc1HighAlarmDisable.Checked = false;
            else
                cbL1Vcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbL1Vcc1LowAlarmDisable.Checked = false;
            else
                cbL1Vcc1LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr147(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1VcselIbiasHighAlarm.Checked = false;
            else
                cbL1VcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbL1VcselIbiasLowAlarm.Checked = false;
            else
                cbL1VcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbL1VcselVfHighAlarm.Checked = false;
            else
                cbL1VcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbL1VcselVfLowAlarm.Checked = false;
            else
                cbL1VcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbL1Vcc1HighAlarm.Checked = false;
            else
                cbL1Vcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbL1Vcc1LowAlarm.Checked = false;
            else
                cbL1Vcc1LowAlarm.Checked = true;
        }

        private void _ParseAddr148_149(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL1Vcc1Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr14A(byte data)
        {
            tbL1VcselIBias.Text = data.ToString();
        }

        private void _ParseAddr14B(byte data)
        {
            tbL1VcselVf.Text = data.ToString();
        }

        private void _ParseAddr14C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdLane.Checked = false;
            else
                cbL1PdLane.Checked = true;
        }

        private void _ParseAddr14D(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdLos.Checked = false;
            else
                cbL1PdLos.Checked = true;
        }

        private void _ParseAddr150(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdCdr.Checked = false;
            else
                cbL1PdCdr.Checked = true;

            if ((data & 0x02) == 0)
                cbL1PdPPdrtMclk.Checked = false;
            else
                cbL1PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr152(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdDrv.Checked = false;
            else
                cbL1PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL1PdPrbClk.Checked = false;
            else
                cbL1PdPrbClk.Checked = true;

            if ((data & 0x04) == 0)
                cbL1PdDrvPrbschk.Checked = false;
            else
                cbL1PdDrvPrbschk.Checked = true;
        }

        private void _ParseAddr153(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1LdPdBiasMon.Checked = false;
            else
                cbL1LdPdBiasMon.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LdPdVfMon.Checked = false;
            else
                cbL1LdPdVfMon.Checked = true;

            if ((data & 0x04) == 0)
                cbL1LdPdVcselComp.Checked = false;
            else
                cbL1LdPdVcselComp.Checked = true;
        }

        private void bL1Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL1Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr100(data[0]);
            _ParseAddr101(data[1]);
            _ParseAddr102(data[2]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0106).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr106(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0108).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr108(data[0]);
            _ParseAddr109(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0115).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr115(data[0]);
            _ParseAddr116(data[1]);
            _ParseAddr117(data[2]);
            _ParseAddr118(data[3]);
            _ParseAddr119(data[4]);
            _ParseAddr11A(data[5]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0120).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr120(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0129).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr129(data[0]);
            _ParseAddr12A(data[1]);
            _ParseAddr12B(data[2]);
            _ParseAddr12C(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x013C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr13C(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0143).Reverse().ToArray(), 11, data);
            if (rv != 11)
                goto exit;

            _ParseAddr143(data[0]);
            _ParseAddr144(data[1]);
            _ParseAddr145(data[2]);
            _ParseAddr146(data[3]);
            _ParseAddr147(data[4]);
            _ParseAddr148_149(data[5], data[6]);
            _ParseAddr14A(data[7]);
            _ParseAddr14B(data[8]);
            _ParseAddr14C(data[9]);
            _ParseAddr14D(data[10]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0150).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr150(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0152).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr152(data[0]);
            _ParseAddr153(data[1]);

        exit:
            reading = false;
            bL1Read.Enabled = true;
        }

        private void _ParseAddr200(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2CdrForceBypass.Checked = false;
            else
                cbL2CdrForceBypass.Checked = true;

            if ((data & 0x02) == 0)
                cbL2CdrAutoBypass.Checked = false;
            else
                cbL2CdrAutoBypass.Checked = true;

            if ((data & 0x04) == 0)
                cbL2CdrPdOnBypass.Checked = false;
            else
                cbL2CdrPdOnBypass.Checked = true;
        }

        private void _ParseAddr201(byte data)
        {
            cbL2LosThres.SelectedIndex = cbL2LosThres.FindStringExact((data & 0x0F).ToString());
        }

        private void _ParseAddr202(byte data)
        {
            cbL2LosHyst.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr206(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2OffsetCorrectionPolarityInv.Checked = false;
            else
                cbL2OffsetCorrectionPolarityInv.Checked = true;

            if ((data & 0x02) == 0)
                cbL2EqPolarityInvert.Checked = false;
            else
                cbL2EqPolarityInvert.Checked = true;
        }

        private void _ParseAddr208(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2EqLoopbackEn.Checked = false;
            else
                cbL2EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr209(byte data)
        {
            cbL2EqBoost.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr215(byte data)
        {
            cbL2CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr216(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2DrvForceMute.Checked = false;
            else
                cbL2DrvForceMute.Checked = true;
        }

        private void _ParseAddr217(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2DrvMuteOnLos.Checked = false;
            else
                cbL2DrvMuteOnLos.Checked = true;

            if ((data & 0x02) == 0)
                cbL2DrvPdOnMute.Checked = false;
            else
                cbL2DrvPdOnMute.Checked = true;
        }

        private void _ParseAddr218(byte data)
        {
            cbL2VcselIbiasSet.SelectedIndex = data;
        }

        private void _ParseAddr219(byte data)
        {
            cbL2VcselImodSet.SelectedIndex = data;
        }

        private void _ParseAddr21A(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2LdFiltOffReset.Checked = false;
            else
                cbL2LdFiltOffReset.Checked = true;
        }

        private void _ParseAddr220(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2LdBiasOffsetReset.Checked = false;
            else
                cbL2LdBiasOffsetReset.Checked = true;
        }

        private void _ParseAddr229(byte data)
        {
            cbL2LdCpaCtrl1.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr22A(byte data)
        {
            cbL2LdCpaCtrl2.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr22B(byte data)
        {
            cbL2LdCompGain0.SelectedIndex = data;
        }

        private void _ParseAddr22C(byte data)
        {
            cbL2LdCompGain1.SelectedIndex = data;
        }

        private void _ParseAddr23C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2VcselIbiasHighFaultOffsetReset.Checked = false;
            else
                cbL2VcselIbiasHighFaultOffsetReset.Checked = true;
        }

        private void _ParseAddr243(byte data)
        {
            cbL2VcselIbiasHighFaultThres.SelectedIndex = data & 0x1F;
            cbL2VcselIbiasLowFaultThres.SelectedIndex = (data & 0xE0) >> 5;
        }

        private void _ParseAddr244(byte data)
        {
            cbL2VcselVfHighFaulThres.SelectedIndex = data & 0x0F;
            cbL2VcselVfLowFaulThres.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr245(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2VcselIbiasHighFaultDetectEn.Checked = false;
            else
                cbL2VcselIbiasHighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL2VcselIbiasLowFaultDetectEn.Checked = false;
            else
                cbL2VcselIbiasLowFaultDetectEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL2VcselVfHighFaultDetectEn.Checked = false;
            else
                cbL2VcselVfHighFaultDetectEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL2VcselVfLowFaultDetectEn.Checked = false;
            else
                cbL2VcselVfLowFaultDetectEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL2Vcc1HighFaultDetectEn.Checked = false;
            else
                cbL2Vcc1HighFaultDetectEn.Checked = true;

            if ((data & 0x20) == 0)
                cbL2Vcc1LowFaultDetectEn.Checked = false;
            else
                cbL2Vcc1LowFaultDetectEn.Checked = true;
        }

        private void _ParseAddr246(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2VcselIbiasHighAlarmDisable.Checked = false;
            else
                cbL2VcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbL2VcselIbiasLowAlarmDisable.Checked = false;
            else
                cbL2VcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbL2VcselVfHighAlarmDisable.Checked = false;
            else
                cbL2VcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbL2VcselVfLowAlarmDisable.Checked = false;
            else
                cbL2VcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbL2Vcc1HighAlarmDisable.Checked = false;
            else
                cbL2Vcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbL2Vcc1LowAlarmDisable.Checked = false;
            else
                cbL2Vcc1LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr247(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2VcselIbiasHighAlarm.Checked = false;
            else
                cbL2VcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbL2VcselIbiasLowAlarm.Checked = false;
            else
                cbL2VcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbL2VcselVfHighAlarm.Checked = false;
            else
                cbL2VcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbL2VcselVfLowAlarm.Checked = false;
            else
                cbL2VcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbL2Vcc1HighAlarm.Checked = false;
            else
                cbL2Vcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbL2Vcc1LowAlarm.Checked = false;
            else
                cbL2Vcc1LowAlarm.Checked = true;
        }

        private void _ParseAddr248_249(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL2Vcc1Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr24A(byte data)
        {
            tbL2VcselIBias.Text = data.ToString();
        }

        private void _ParseAddr24B(byte data)
        {
            tbL2VcselVf.Text = data.ToString();
        }

        private void _ParseAddr24C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdLane.Checked = false;
            else
                cbL2PdLane.Checked = true;
        }

        private void _ParseAddr24D(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdLos.Checked = false;
            else
                cbL2PdLos.Checked = true;
        }

        private void _ParseAddr250(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdCdr.Checked = false;
            else
                cbL2PdCdr.Checked = true;

            if ((data & 0x02) == 0)
                cbL2PdPPdrtMclk.Checked = false;
            else
                cbL2PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr252(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdDrv.Checked = false;
            else
                cbL2PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL2PdPrbClk.Checked = false;
            else
                cbL2PdPrbClk.Checked = true;

            if ((data & 0x04) == 0)
                cbL2PdDrvPrbschk.Checked = false;
            else
                cbL2PdDrvPrbschk.Checked = true;
        }

        private void _ParseAddr253(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2LdPdBiasMon.Checked = false;
            else
                cbL2LdPdBiasMon.Checked = true;

            if ((data & 0x02) == 0)
                cbL2LdPdVfMon.Checked = false;
            else
                cbL2LdPdVfMon.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LdPdVcselComp.Checked = false;
            else
                cbL2LdPdVcselComp.Checked = true;
        }

        private void bL2Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL2Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0200).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr200(data[0]);
            _ParseAddr201(data[1]);
            _ParseAddr202(data[2]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0206).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr206(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0208).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr208(data[0]);
            _ParseAddr209(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0215).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr215(data[0]);
            _ParseAddr216(data[1]);
            _ParseAddr217(data[2]);
            _ParseAddr218(data[3]);
            _ParseAddr219(data[4]);
            _ParseAddr21A(data[5]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0220).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr220(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0229).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr229(data[0]);
            _ParseAddr22A(data[1]);
            _ParseAddr22B(data[2]);
            _ParseAddr22C(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x023C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr23C(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0243).Reverse().ToArray(), 11, data);
            if (rv != 11)
                goto exit;

            _ParseAddr243(data[0]);
            _ParseAddr244(data[1]);
            _ParseAddr245(data[2]);
            _ParseAddr246(data[3]);
            _ParseAddr247(data[4]);
            _ParseAddr248_249(data[5], data[6]);
            _ParseAddr24A(data[7]);
            _ParseAddr24B(data[8]);
            _ParseAddr24C(data[9]);
            _ParseAddr24D(data[10]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0250).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr250(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0252).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr252(data[0]);
            _ParseAddr253(data[1]);

        exit:
            reading = false;
            bL2Read.Enabled = true;
        }

        private void _ParseAddr300(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3CdrForceBypass.Checked = false;
            else
                cbL3CdrForceBypass.Checked = true;

            if ((data & 0x02) == 0)
                cbL3CdrAutoBypass.Checked = false;
            else
                cbL3CdrAutoBypass.Checked = true;

            if ((data & 0x04) == 0)
                cbL3CdrPdOnBypass.Checked = false;
            else
                cbL3CdrPdOnBypass.Checked = true;
        }

        private void _ParseAddr301(byte data)
        {
            cbL3LosThres.SelectedIndex = cbL3LosThres.FindStringExact((data & 0x0F).ToString());
        }

        private void _ParseAddr302(byte data)
        {
            cbL3LosHyst.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr306(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3OffsetCorrectionPolarityInv.Checked = false;
            else
                cbL3OffsetCorrectionPolarityInv.Checked = true;

            if ((data & 0x02) == 0)
                cbL3EqPolarityInvert.Checked = false;
            else
                cbL3EqPolarityInvert.Checked = true;
        }

        private void _ParseAddr308(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3EqLoopbackEn.Checked = false;
            else
                cbL3EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr309(byte data)
        {
            cbL3EqBoost.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr315(byte data)
        {
            cbL3CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr316(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3DrvForceMute.Checked = false;
            else
                cbL3DrvForceMute.Checked = true;
        }

        private void _ParseAddr317(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3DrvMuteOnLos.Checked = false;
            else
                cbL3DrvMuteOnLos.Checked = true;

            if ((data & 0x02) == 0)
                cbL3DrvPdOnMute.Checked = false;
            else
                cbL3DrvPdOnMute.Checked = true;
        }

        private void _ParseAddr318(byte data)
        {
            cbL3VcselIbiasSet.SelectedIndex = data;
        }

        private void _ParseAddr319(byte data)
        {
            cbL3VcselImodSet.SelectedIndex = data;
        }

        private void _ParseAddr31A(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3LdFiltOffReset.Checked = false;
            else
                cbL3LdFiltOffReset.Checked = true;
        }

        private void _ParseAddr320(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3LdBiasOffsetReset.Checked = false;
            else
                cbL3LdBiasOffsetReset.Checked = true;
        }

        private void _ParseAddr329(byte data)
        {
            cbL3LdCpaCtrl1.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr32A(byte data)
        {
            cbL3LdCpaCtrl2.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr32B(byte data)
        {
            cbL3LdCompGain0.SelectedIndex = data;
        }

        private void _ParseAddr32C(byte data)
        {
            cbL3LdCompGain1.SelectedIndex = data;
        }

        private void _ParseAddr33C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3VcselIbiasHighFaultOffsetReset.Checked = false;
            else
                cbL3VcselIbiasHighFaultOffsetReset.Checked = true;
        }

        private void _ParseAddr343(byte data)
        {
            cbL3VcselIbiasHighFaultThres.SelectedIndex = data & 0x1F;
            cbL3VcselIbiasLowFaultThres.SelectedIndex = (data & 0xE0) >> 5;
        }

        private void _ParseAddr344(byte data)
        {
            cbL3VcselVfHighFaulThres.SelectedIndex = data & 0x0F;
            cbL3VcselVfLowFaulThres.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr345(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3VcselIbiasHighFaultDetectEn.Checked = false;
            else
                cbL3VcselIbiasHighFaultDetectEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL3VcselIbiasLowFaultDetectEn.Checked = false;
            else
                cbL3VcselIbiasLowFaultDetectEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL3VcselVfHighFaultDetectEn.Checked = false;
            else
                cbL3VcselVfHighFaultDetectEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL3VcselVfLowFaultDetectEn.Checked = false;
            else
                cbL3VcselVfLowFaultDetectEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL3Vcc1HighFaultDetectEn.Checked = false;
            else
                cbL3Vcc1HighFaultDetectEn.Checked = true;

            if ((data & 0x20) == 0)
                cbL3Vcc1LowFaultDetectEn.Checked = false;
            else
                cbL3Vcc1LowFaultDetectEn.Checked = true;
        }

        private void _ParseAddr346(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3VcselIbiasHighAlarmDisable.Checked = false;
            else
                cbL3VcselIbiasHighAlarmDisable.Checked = true;

            if ((data & 0x02) == 0)
                cbL3VcselIbiasLowAlarmDisable.Checked = false;
            else
                cbL3VcselIbiasLowAlarmDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbL3VcselVfHighAlarmDisable.Checked = false;
            else
                cbL3VcselVfHighAlarmDisable.Checked = true;

            if ((data & 0x08) == 0)
                cbL3VcselVfLowAlarmDisable.Checked = false;
            else
                cbL3VcselVfLowAlarmDisable.Checked = true;

            if ((data & 0x10) == 0)
                cbL3Vcc1HighAlarmDisable.Checked = false;
            else
                cbL3Vcc1HighAlarmDisable.Checked = true;

            if ((data & 0x20) == 0)
                cbL3Vcc1LowAlarmDisable.Checked = false;
            else
                cbL3Vcc1LowAlarmDisable.Checked = true;
        }

        private void _ParseAddr347(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3VcselIbiasHighAlarm.Checked = false;
            else
                cbL3VcselIbiasHighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbL3VcselIbiasLowAlarm.Checked = false;
            else
                cbL3VcselIbiasLowAlarm.Checked = true;

            if ((data & 0x04) == 0)
                cbL3VcselVfHighAlarm.Checked = false;
            else
                cbL3VcselVfHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbL3VcselVfLowAlarm.Checked = false;
            else
                cbL3VcselVfLowAlarm.Checked = true;

            if ((data & 0x10) == 0)
                cbL3Vcc1HighAlarm.Checked = false;
            else
                cbL3Vcc1HighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbL3Vcc1LowAlarm.Checked = false;
            else
                cbL3Vcc1LowAlarm.Checked = true;
        }

        private void _ParseAddr348_349(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL3Vcc1Supply.Text = iTmp.ToString();
        }

        private void _ParseAddr34A(byte data)
        {
            tbL3VcselIBias.Text = data.ToString();
        }

        private void _ParseAddr34B(byte data)
        {
            tbL3VcselVf.Text = data.ToString();
        }

        private void _ParseAddr34C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdLane.Checked = false;
            else
                cbL3PdLane.Checked = true;
        }

        private void _ParseAddr34D(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdLos.Checked = false;
            else
                cbL3PdLos.Checked = true;
        }

        private void _ParseAddr350(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdCdr.Checked = false;
            else
                cbL3PdCdr.Checked = true;

            if ((data & 0x02) == 0)
                cbL3PdPPdrtMclk.Checked = false;
            else
                cbL3PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr352(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdDrv.Checked = false;
            else
                cbL3PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL3PdPrbClk.Checked = false;
            else
                cbL3PdPrbClk.Checked = true;

            if ((data & 0x04) == 0)
                cbL3PdDrvPrbschk.Checked = false;
            else
                cbL3PdDrvPrbschk.Checked = true;
        }

        private void _ParseAddr353(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3LdPdBiasMon.Checked = false;
            else
                cbL3LdPdBiasMon.Checked = true;

            if ((data & 0x02) == 0)
                cbL3LdPdVfMon.Checked = false;
            else
                cbL3LdPdVfMon.Checked = true;

            if ((data & 0x04) == 0)
                cbL3LdPdVcselComp.Checked = false;
            else
                cbL3LdPdVcselComp.Checked = true;
        }

        private void bL3Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL3Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0300).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr300(data[0]);
            _ParseAddr301(data[1]);
            _ParseAddr302(data[2]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0306).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr306(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0308).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr308(data[0]);
            _ParseAddr309(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0315).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr315(data[0]);
            _ParseAddr316(data[1]);
            _ParseAddr317(data[2]);
            _ParseAddr318(data[3]);
            _ParseAddr319(data[4]);
            _ParseAddr31A(data[5]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0320).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr320(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0329).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr329(data[0]);
            _ParseAddr32A(data[1]);
            _ParseAddr32B(data[2]);
            _ParseAddr32C(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x033C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr33C(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0343).Reverse().ToArray(), 11, data);
            if (rv != 11)
                goto exit;

            _ParseAddr343(data[0]);
            _ParseAddr344(data[1]);
            _ParseAddr345(data[2]);
            _ParseAddr346(data[3]);
            _ParseAddr347(data[4]);
            _ParseAddr348_349(data[5], data[6]);
            _ParseAddr34A(data[7]);
            _ParseAddr34B(data[8]);
            _ParseAddr34C(data[9]);
            _ParseAddr34D(data[10]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0350).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr350(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0352).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr352(data[0]);
            _ParseAddr353(data[1]);

        exit:
            reading = false;
            bL3Read.Enabled = true;
        }

        private void _ParseAddr403(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LosSoftAssertEn.Checked = false;
            else
                cbL0LosSoftAssertEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LosSoftAssertEn.Checked = false;
            else
                cbL1LosSoftAssertEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LosSoftAssertEn.Checked = false;
            else
                cbL2LosSoftAssertEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LosSoftAssertEn.Checked = false;
            else
                cbL3LosSoftAssertEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL0LosSoftAssert.Checked = false;
            else
                cbL0LosSoftAssert.Checked = true;

            if ((data & 0x20) == 0)
                cbL1LosSoftAssert.Checked = false;
            else
                cbL1LosSoftAssert.Checked = true;

            if ((data & 0x40) == 0)
                cbL2LosSoftAssert.Checked = false;
            else
                cbL2LosSoftAssert.Checked = true;

            if ((data & 0x80) == 0)
                cbL3LosSoftAssert.Checked = false;
            else
                cbL3LosSoftAssert.Checked = true;
        }

        private void _ParseAddr405(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LosChangeDetect.Checked = false;
            else
                cbL0LosChangeDetect.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LosChangeDetect.Checked = false;
            else
                cbL1LosChangeDetect.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LosChangeDetect.Checked = false;
            else
                cbL2LosChangeDetect.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LosChangeDetect.Checked = false;
            else
                cbL3LosChangeDetect.Checked = true;
        }

        private void _ParseAddr407(byte data)
        {
            cbLockdetLockThres.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr40B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LolSoftAssertEn.Checked = false;
            else
                cbL0LolSoftAssertEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LolSoftAssertEn.Checked = false;
            else
                cbL1LolSoftAssertEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LolSoftAssertEn.Checked = false;
            else
                cbL2LolSoftAssertEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LolSoftAssertEn.Checked = false;
            else
                cbL3LolSoftAssertEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL0LolSoftAssert.Checked = false;
            else
                cbL0LolSoftAssert.Checked = true;

            if ((data & 0x20) == 0)
                cbL1LolSoftAssert.Checked = false;
            else
                cbL1LolSoftAssert.Checked = true;

            if ((data & 0x40) == 0)
                cbL2LolSoftAssert.Checked = false;
            else
                cbL2LolSoftAssert.Checked = true;

            if ((data & 0x80) == 0)
                cbL3LolSoftAssert.Checked = false;
            else
                cbL3LolSoftAssert.Checked = true;
        }

        private void _ParseAddr40D(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LolChangeDetect.Checked = false;
            else
                cbL0LolChangeDetect.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LolChangeDetect.Checked = false;
            else
                cbL1LolChangeDetect.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LolChangeDetect.Checked = false;
            else
                cbL2LolChangeDetect.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LolChangeDetect.Checked = false;
            else
                cbL3LolChangeDetect.Checked = true;
        }

        private void _ParseAddr40F(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LosToLoslEn.Checked = false;
            else
                cbL0LosToLoslEn.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LosToLoslEn.Checked = false;
            else
                cbL1LosToLoslEn.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LosToLoslEn.Checked = false;
            else
                cbL2LosToLoslEn.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LosToLoslEn.Checked = false;
            else
                cbL3LosToLoslEn.Checked = true;

            if ((data & 0x10) == 0)
                cbL0LolToLoslEn.Checked = false;
            else
                cbL0LolToLoslEn.Checked = true;

            if ((data & 0x20) == 0)
                cbL1LolToLoslEn.Checked = false;
            else
                cbL1LolToLoslEn.Checked = true;

            if ((data & 0x40) == 0)
                cbL2LolToLoslEn.Checked = false;
            else
                cbL2LolToLoslEn.Checked = true;

            if ((data & 0x80) == 0)
                cbL3LolToLoslEn.Checked = false;
            else
                cbL3LolToLoslEn.Checked = true;
        }

        private void _ParseAddr410(byte data)
        {
            if ((data & 0x01) == 0)
                cbLoslInvert.Checked = false;
            else
                cbLoslInvert.Checked = true;
        }

        private void _ParseAddr411(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0Los.Checked = false;
            else
                cbL0Los.Checked = true;

            if ((data & 0x02) == 0)
                cbL1Los.Checked = false;
            else
                cbL1Los.Checked = true;

            if ((data & 0x04) == 0)
                cbL2Los.Checked = false;
            else
                cbL2Los.Checked = true;

            if ((data & 0x08) == 0)
                cbL3Los.Checked = false;
            else
                cbL3Los.Checked = true;

            if ((data & 0x10) == 0)
                cbL0Lol.Checked = false;
            else
                cbL0Lol.Checked = true;

            if ((data & 0x20) == 0)
                cbL1Lol.Checked = false;
            else
                cbL1Lol.Checked = true;

            if ((data & 0x40) == 0)
                cbL2Lol.Checked = false;
            else
                cbL2Lol.Checked = true;

            if ((data & 0x80) == 0)
                cbL3Lol.Checked = false;
            else
                cbL3Lol.Checked = true;
        }

        private void _ParseAddr412(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LosLatch.Checked = false;
            else
                cbL0LosLatch.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LosLatch.Checked = false;
            else
                cbL1LosLatch.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LosLatch.Checked = false;
            else
                cbL2LosLatch.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LosLatch.Checked = false;
            else
                cbL3LosLatch.Checked = true;

            if ((data & 0x10) == 0)
                cbL0LolLatch.Checked = false;
            else
                cbL0LolLatch.Checked = true;

            if ((data & 0x20) == 0)
                cbL1LolLatch.Checked = false;
            else
                cbL1LolLatch.Checked = true;

            if ((data & 0x40) == 0)
                cbL2LolLatch.Checked = false;
            else
                cbL2LolLatch.Checked = true;

            if ((data & 0x80) == 0)
                cbL3LolLatch.Checked = false;
            else
                cbL3LolLatch.Checked = true;
        }

        private void _ParseAddr423(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LdFiltOffAutocalComplete.Checked = false;
            else
                cbL0LdFiltOffAutocalComplete.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LdFiltOffAutocalComplete.Checked = false;
            else
                cbL1LdFiltOffAutocalComplete.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LdFiltOffAutocalComplete.Checked = false;
            else
                cbL2LdFiltOffAutocalComplete.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LdFiltOffAutocalComplete.Checked = false;
            else
                cbL3LdFiltOffAutocalComplete.Checked = true;
        }

        private void _ParseAddr426(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0LdBiasOffsetAutocalComplete.Checked = false;
            else
                cbL0LdBiasOffsetAutocalComplete.Checked = true;

            if ((data & 0x02) == 0)
                cbL1LdBiasOffsetAutocalComplete.Checked = false;
            else
                cbL1LdBiasOffsetAutocalComplete.Checked = true;

            if ((data & 0x04) == 0)
                cbL2LdBiasOffsetAutocalComplete.Checked = false;
            else
                cbL2LdBiasOffsetAutocalComplete.Checked = true;

            if ((data & 0x08) == 0)
                cbL3LdBiasOffsetAutocalComplete.Checked = false;
            else
                cbL3LdBiasOffsetAutocalComplete.Checked = true;
        }

        private void _ParseAddr427(byte data)
        {
            if ((data & 0x01) == 0)
                cbLdBurnInEn.Checked = false;
            else
                cbLdBurnInEn.Checked = true;
        }

        private void _ParseAddr430(byte data)
        {
            cbLdCompGain0Scale.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr431(byte data)
        {
            cbLdCompGain1Scale.SelectedIndex = data & 0x1F;
        }

        private void _ParseAddr43C(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasHighFaultOffsetAutocalComplete.Checked = false;
            else
                cbL0VcselIbiasHighFaultOffsetAutocalComplete.Checked = true;

            if ((data & 0x02) == 0)
                cbL1VcselIbiasHighFaultOffsetAutocalComplete.Checked = false;
            else
                cbL1VcselIbiasHighFaultOffsetAutocalComplete.Checked = true;

            if ((data & 0x04) == 0)
                cbL2VcselIbiasHighFaultOffsetAutocalComplete.Checked = false;
            else
                cbL2VcselIbiasHighFaultOffsetAutocalComplete.Checked = true;

            if ((data & 0x08) == 0)
                cbL3VcselIbiasHighFaultOffsetAutocalComplete.Checked = false;
            else
                cbL3VcselIbiasHighFaultOffsetAutocalComplete.Checked = true;
        }

        private void _ParseAddr43D(byte data)
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

        private void _ParseAddr43E(byte data)
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

        private void _ParseAddr43F(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxFaultAlarmDisable.Checked = false;
            else
                cbTxFaultAlarmDisable.Checked = true;
        }

        private void _ParseAddr440(byte data)
        {
            cbVcc1HighFaultThres.SelectedIndex = data & 0x03;
            cbVcc1LowFaultThres.SelectedIndex = (data & 0x0C) >> 2;
        }

        private void _ParseAddr441(byte data)
        {
            cbVcc2HighFaultThres.SelectedIndex = data & 0x03;
            cbVcc2LowFaultThres.SelectedIndex = (data & 0x0C) >> 2;
        }

        private void _ParseAddr442(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0TxdsblSoft.Checked = false;
            else
                cbL0TxdsblSoft.Checked = true;

            if ((data & 0x02) == 0)
                cbL1TxdsblSoft.Checked = false;
            else
                cbL1TxdsblSoft.Checked = true;

            if ((data & 0x04) == 0)
                cbL2TxdsblSoft.Checked = false;
            else
                cbL2TxdsblSoft.Checked = true;

            if ((data & 0x08) == 0)
                cbL3TxdsblSoft.Checked = false;
            else
                cbL3TxdsblSoft.Checked = true;

            if ((data & 0x10) == 0)
                cbTxFaultPolarity.Checked = false;
            else
                cbTxFaultPolarity.Checked = true;
        }

        private void _ParseAddr444(byte data)
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
                cbTxFault.Checked = false;
            else
                cbTxFault.Checked = true;

            if ((data & 0x80) == 0)
                cbLos.Checked = false;
            else
                cbLos.Checked = true;
        }

        private void _ParseAddr445(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxdsblInput.Checked = false;
            else
                cbTxdsblInput.Checked = true;
        }

        private void _ParseAddr446(byte data)
        {
            if ((data & 0x01) == 0)
                cbVcc2HighAlarm.Checked = false;
            else
                cbVcc2HighAlarm.Checked = true;

            if ((data & 0x02) == 0)
                cbVcc2LowAlarm.Checked = false;
            else
                cbVcc2LowAlarm.Checked = true;
        }

        private void _ParseAddr447(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0TxFault.Checked = false;
            else
                cbL0TxFault.Checked = true;

            if ((data & 0x02) == 0)
                cbL1TxFault.Checked = false;
            else
                cbL1TxFault.Checked = true;

            if ((data & 0x04) == 0)
                cbL2TxFault.Checked = false;
            else
                cbL2TxFault.Checked = true;

            if ((data & 0x08) == 0)
                cbL3TxFault.Checked = false;
            else
                cbL3TxFault.Checked = true;
        }

        private void bControl1Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0403).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr403(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0405).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr405(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0407).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr407(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x040B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr40B(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x040D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr40D(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x040F).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr40F(data[0]);
            _ParseAddr410(data[1]);
            _ParseAddr411(data[2]);
            _ParseAddr412(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0423).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr423(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0426).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr426(data[0]);
            _ParseAddr427(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0430).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr430(data[0]);
            _ParseAddr431(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x043C).Reverse().ToArray(), 7, data);
            if (rv != 7)
                goto exit;

            _ParseAddr43C(data[0]);
            _ParseAddr43D(data[1]);
            _ParseAddr43E(data[2]);
            _ParseAddr43F(data[3]);
            _ParseAddr440(data[4]);
            _ParseAddr441(data[5]);
            _ParseAddr442(data[6]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0444).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr444(data[0]);
            _ParseAddr445(data[1]);
            _ParseAddr446(data[2]);
            _ParseAddr447(data[3]);

        exit:
            reading = false;
        }

        private void _ParseAddr456(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PrbsClkSel.Checked = false;
            else
                cbL0PrbsClkSel.Checked = true;

            if ((data & 0x02) == 0)
                cbL1PrbsClkSel.Checked = false;
            else
                cbL1PrbsClkSel.Checked = true;

            if ((data & 0x04) == 0)
                cbL2PrbsClkSel.Checked = false;
            else
                cbL2PrbsClkSel.Checked = true;

            if ((data & 0x08) == 0)
                cbL3PrbsClkSel.Checked = false;
            else
                cbL3PrbsClkSel.Checked = true;

            if ((data & 0x10) == 0)
                cbL0PrbsDataSel.Checked = false;
            else
                cbL0PrbsDataSel.Checked = true;

            if ((data & 0x20) == 0)
                cbL1PrbsDataSel.Checked = false;
            else
                cbL1PrbsDataSel.Checked = true;

            if ((data & 0x40) == 0)
                cbL2PrbsDataSel.Checked = false;
            else
                cbL2PrbsDataSel.Checked = true;

            if ((data & 0x80) == 0)
                cbL3PrbsDataSel.Checked = false;
            else
                cbL3PrbsDataSel.Checked = true;
        }

        private void _ParseAddr457(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbsgenStart.Checked = false;
            else
                cbPrbsgenStart.Checked = true;
        }

        private void _ParseAddr458(byte data)
        {
            cbPrbsgenSequenceSel.SelectedIndex = data & 0x03;
            cbPrbsgenOutputSel.SelectedIndex = (data & 0x0C) >> 2;
            cbPrbsgenCkdivRate.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr459(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbsgenCkSel.Checked = false;
            else
                cbPrbsgenCkSel.Checked = true;

            if ((data & 0x02) == 0)
                cbPrbsgenCkdivCkSel.Checked = false;
            else
                cbPrbsgenCkdivCkSel.Checked = true;
        }

        private void _ParseAddr45E(byte data)
        {
            cbPrbsgenVcoFreq.SelectedIndex = data & 0x07;
        }

        private void _ParseAddr460(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkInvPrbs.Checked = false;
            else
                cbPrbschkInvPrbs.Checked = true;

            cbPrbschkSelPrbs.SelectedIndex = (data & 0x06) >> 1;
        }

        private void _ParseAddr464(byte data)
        {
            cbPrbschkTimerClkSel.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr465_468(byte data0, byte data1, byte data2, byte data3)
        {
            long tmp;

            tmp = ((((data3 * 256 + data2) * 256) + data1) * 256) + data0;
            tbPrbschkTimeout.Text = tmp.ToString();
        }

        private void _ParseAddr469(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkTimerEnable.Checked = false;
            else
                cbPrbschkTimerEnable.Checked = true;
        }

        private void _ParseAddr46A(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkEnable.Checked = false;
            else
                cbPrbschkEnable.Checked = true;
        }

        private void _ParseAddr46B(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkDone.Checked = false;
            else
                cbPrbschkDone.Checked = true;
        }

        private void _ParseAddr46C_46F(byte data0, byte data1, byte data2, byte data3)
        {
            long tmp;

            tmp = ((((data3 * 256 + data2) * 256) + data1) * 256) + data0;
            tbPrbschkTime.Text = tmp.ToString();
        }

        private void _ParseAddr470_474(byte data0, byte data1, byte data2, byte data3, byte data4)
        {
            long tmp;

            tmp = ((((((data4 * 256 + data3) * 256) + data2) * 256) + data1) * 256) + data0;
            tbPrbschkErrCount.Text = tmp.ToString();
        }

        private void _ParseAddr475(byte data)
        {
            tbTempSensorTrim.Text = (data & 0x7F).ToString();
        }

        private void _ParseAddr476_477(byte data0, byte data1)
        {
            int tmp;

            tmp = (data1 & 0x03) * 256 + data0;
            tbTemperature.Text = tmp.ToString();
        }

        private void _ParseAddr478(byte data)
        {
            tbVcclSupply.Text = data.ToString();
        }

        private void _ParseAddr479_47A(byte data0, byte data1)
        {
            int tmp;

            tmp = (data1 & 0x03) * 256 + data0;
            tbVccSupply.Text = tmp.ToString();
        }

        private void _ParseAddr47B_47C(byte data0, byte data1)
        {
            int tmp;

            tmp = (data1 & 0x03) * 256 + data0;
            tbVcc2Supply.Text = tmp.ToString();
        }

        private void _ParseAddr47E(byte data)
        {
            tbAdcCmCal.Text = (data & 0x1F).ToString();
        }

        private void _ParseAddr480(byte data)
        {
            cbAdcSrcSel.SelectedIndex = cbAdcSrcSel.FindStringExact((data & 0x1F).ToString());
        }

        private void _ParseAddr483(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcParamMonCtrlReset.Checked = false;
            else
                cbAdcParamMonCtrlReset.Checked = true;
        }

        private void _ParseAddr486(byte data)
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

            if ((data & 0x10) == 0)
                cbL0Vcc1SupplyMonEnable.Checked = false;
            else
                cbL0Vcc1SupplyMonEnable.Checked = true;

            if ((data & 0x20) == 0)
                cbL1Vcc1SupplyMonEnable.Checked = false;
            else
                cbL1Vcc1SupplyMonEnable.Checked = true;

            if ((data & 0x40) == 0)
                cbL2Vcc1SupplyMonEnable.Checked = false;
            else
                cbL2Vcc1SupplyMonEnable.Checked = true;

            if ((data & 0x80) == 0)
                cbL3Vcc1SupplyMonEnable.Checked = false;
            else
                cbL3Vcc1SupplyMonEnable.Checked = true;
        }

        private void _ParseAddr487(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0VcselIbiasMonEnable.Checked = false;
            else
                cbL0VcselIbiasMonEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbL1VcselIbiasMonEnable.Checked = false;
            else
                cbL1VcselIbiasMonEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbL2VcselIbiasMonEnable.Checked = false;
            else
                cbL2VcselIbiasMonEnable.Checked = true;

            if ((data & 0x08) == 0)
                cbL3VcselIbiasMonEnable.Checked = false;
            else
                cbL3VcselIbiasMonEnable.Checked = true;

            if ((data & 0x10) == 0)
                cbL0VcselVfMonEnable.Checked = false;
            else
                cbL0VcselVfMonEnable.Checked = true;

            if ((data & 0x20) == 0)
                cbL1VcselVfMonEnable.Checked = false;
            else
                cbL1VcselVfMonEnable.Checked = true;

            if ((data & 0x40) == 0)
                cbL2VcselVfMonEnable.Checked = false;
            else
                cbL2VcselVfMonEnable.Checked = true;

            if ((data & 0x80) == 0)
                cbL3VcselVfMonEnable.Checked = false;
            else
                cbL3VcselVfMonEnable.Checked = true;
        }

        private void _ParseAddr48B(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcHostCtrlReq.Checked = false;
            else
                cbAdcHostCtrlReq.Checked = true;
        }

        private void _ParseAddr48C(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcHostCtrlGnt.Checked = false;
            else
                cbAdcHostCtrlGnt.Checked = true;
        }

        private void _ParseAddr48D(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcReset.Checked = false;
            else
                cbAdcReset.Checked = true;

            if ((data & 0x02) == 0)
                cbAdcAutoConvEn.Checked = false;
            else
                cbAdcAutoConvEn.Checked = true;

            if ((data & 0x04) == 0)
                cbAdcJustLsb.Checked = false;
            else
                cbAdcJustLsb.Checked = true;

            if ((data & 0x08) == 0)
                cbAdcOffMode.Checked = false;
            else
                cbAdcOffMode.Checked = true;

            cbAdcResolution.SelectedIndex = (data & 0x70) >> 4;
        }

        private void _ParseAddr48E_48F(byte data0, byte data1)
        {
            int tmp;

            tmp = data1 * 256 + data0;
            tbAdcOffset.Text = tmp.ToString();
        }

        private void _ParseAddr490(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcStartConv.Checked = false;
            else
                cbAdcStartConv.Checked = true;
        }

        private void _ParseAddr491(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcDone.Checked = false;
            else
                cbAdcDone.Checked = true;
        }

        private void _ParseAddr492_493(byte data0, byte data1)
        {
            int tmp;

            tmp = data1 * 256 + data0;
            tbAdcOut.Text = tmp.ToString();
        }

        private void _ParseAddr49C(byte data)
        {
            tbRppbiasTrim.Text = (data & 0x3F).ToString();
        }

        private void _ParseAddr49D(byte data)
        {
            cbBbStep.SelectedIndex = data;
        }

        private void _ParseAddr4A2(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdAll.Checked = false;
            else
                cbPdAll.Checked = true;
        }

        private void _ParseAddr4A3(byte data)
        {
            if ((data & 0x01) == 0)
                cbLdPdVfmonStg2.Checked = false;
            else
                cbLdPdVfmonStg2.Checked = true;
        }

        private void _ParseAddr4A4(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdPrbsgenDataBuf.Checked = false;
            else
                cbL0PdPrbsgenDataBuf.Checked = true;

            if ((data & 0x02) == 0)
                cbL1PdPrbsgenDataBuf.Checked = false;
            else
                cbL1PdPrbsgenDataBuf.Checked = true;

            if ((data & 0x04) == 0)
                cbL2PdPrbsgenDataBuf.Checked = false;
            else
                cbL2PdPrbsgenDataBuf.Checked = true;

            if ((data & 0x08) == 0)
                cbL3PdPrbsgenDataBuf.Checked = false;
            else
                cbL3PdPrbsgenDataBuf.Checked = true;
        }

        private void _ParseAddr4A5(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdPrbsgen.Checked = false;
            else
                cbPdPrbsgen.Checked = true;

            if ((data & 0x02) == 0)
                cbPdPrbsgenCkdiv.Checked = false;
            else
                cbPdPrbsgenCkdiv.Checked = true;

            if ((data & 0x04) == 0)
                cbPdPrbsgenVco.Checked = false;
            else
                cbPdPrbsgenVco.Checked = true;

            if ((data & 0x08) == 0)
                cbPdPrbsgenAll.Checked = false;
            else
                cbPdPrbsgenAll.Checked = true;
        }

        private void _ParseAddr4A6(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdPrbschk.Checked = false;
            else
                cbPdPrbschk.Checked = true;
        }

        private void _ParseAddr4A7(byte data)
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

        private void _ParseAddr4AA(byte data)
        {
            tbVersion.Text = data.ToString();
        }

        private void bControl2Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[25];
            int rv;

            if (reading == true)
                return;

            reading = true;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0456).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr456(data[0]);
            _ParseAddr457(data[1]);
            _ParseAddr458(data[2]);
            _ParseAddr459(data[3]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x045E).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr45E(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0460).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr460(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0464).Reverse().ToArray(), 25, data);
            if (rv != 25)
                goto exit;

            _ParseAddr464(data[0]);
            _ParseAddr465_468(data[1], data[2], data[3], data[4]);
            _ParseAddr469(data[5]);
            _ParseAddr46A(data[6]);
            _ParseAddr46B(data[7]);
            _ParseAddr46C_46F(data[8], data[9], data[10], data[11]);
            _ParseAddr470_474(data[12], data[13], data[14], data[15], data[16]);
            _ParseAddr475(data[17]);
            _ParseAddr476_477(data[18], data[19]);
            _ParseAddr478(data[20]);
            _ParseAddr479_47A(data[21], data[22]);
            _ParseAddr47B_47C(data[23], data[24]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x047E).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr47E(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0480).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr480(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0483).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr483(data[0]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0486).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr486(data[0]);
            _ParseAddr487(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x048B).Reverse().ToArray(), 9, data);
            if (rv != 9)
                goto exit;

            _ParseAddr48B(data[0]);
            _ParseAddr48C(data[1]);
            _ParseAddr48D(data[2]);
            _ParseAddr48E_48F(data[3], data[4]);
            _ParseAddr490(data[5]);
            _ParseAddr491(data[6]);
            _ParseAddr492_493(data[7], data[8]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x049C).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr49C(data[0]);
            _ParseAddr49D(data[1]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x04A2).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr4A2(data[0]);
            _ParseAddr4A3(data[1]);
            _ParseAddr4A4(data[2]);
            _ParseAddr4A5(data[3]);
            _ParseAddr4A6(data[4]);
            _ParseAddr4A7(data[5]);

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x04AA).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr4AA(data[0]);

        exit:
            reading = false;
        }

        public int WriteAllApi()
        {
            if (_WriteAddr000() < 0) return -1;
            if (_WriteAddr001() < 0) return -1;
            if (_WriteAddr002() < 0) return -1;
            if (_WriteAddr006() < 0) return -1;
            if (_WriteAddr008() < 0) return -1;
            if (_WriteAddr009() < 0) return -1;
            if (_WriteAddr015() < 0) return -1;
            if (_WriteAddr016() < 0) return -1;
            if (_WriteAddr017() < 0) return -1;
            if (_WriteAddr018() < 0) return -1;
            if (_WriteAddr019() < 0) return -1;
            if (_WriteAddr01A() < 0) return -1;
            if (_WriteAddr020() < 0) return -1;
            if (_WriteAddr029() < 0) return -1;
            if (_WriteAddr02A() < 0) return -1;
            if (_WriteAddr02B() < 0) return -1;
            if (_WriteAddr02C() < 0) return -1;
            if (_WriteAddr03C() < 0) return -1;
            if (_WriteAddr043() < 0) return -1;
            if (_WriteAddr044() < 0) return -1;
            if (_WriteAddr045() < 0) return -1;
            if (_WriteAddr046() < 0) return -1;
            if (_WriteAddr047() < 0) return -1;
            if (_WriteAddr04C() < 0) return -1;
            if (_WriteAddr04D() < 0) return -1;
            if (_WriteAddr050() < 0) return -1;
            if (_WriteAddr052() < 0) return -1;
            if (_WriteAddr053() < 0) return -1;
            if (_WriteAddr100() < 0) return -1;
            if (_WriteAddr101() < 0) return -1;
            if (_WriteAddr102() < 0) return -1;
            if (_WriteAddr106() < 0) return -1;
            if (_WriteAddr108() < 0) return -1;
            if (_WriteAddr109() < 0) return -1;
            if (_WriteAddr115() < 0) return -1;
            if (_WriteAddr116() < 0) return -1;
            if (_WriteAddr117() < 0) return -1;
            if (_WriteAddr118() < 0) return -1;
            if (_WriteAddr119() < 0) return -1;
            if (_WriteAddr11A() < 0) return -1;
            if (_WriteAddr120() < 0) return -1;
            if (_WriteAddr129() < 0) return -1;
            if (_WriteAddr12A() < 0) return -1;
            if (_WriteAddr12B() < 0) return -1;
            if (_WriteAddr12C() < 0) return -1;
            if (_WriteAddr13C() < 0) return -1;
            if (_WriteAddr143() < 0) return -1;
            if (_WriteAddr144() < 0) return -1;
            if (_WriteAddr145() < 0) return -1;
            if (_WriteAddr146() < 0) return -1;
            if (_WriteAddr147() < 0) return -1;
            if (_WriteAddr14C() < 0) return -1;
            if (_WriteAddr14D() < 0) return -1;
            if (_WriteAddr150() < 0) return -1;
            if (_WriteAddr152() < 0) return -1;
            if (_WriteAddr153() < 0) return -1;
            if (_WriteAddr200() < 0) return -1;
            if (_WriteAddr201() < 0) return -1;
            if (_WriteAddr202() < 0) return -1;
            if (_WriteAddr206() < 0) return -1;
            if (_WriteAddr208() < 0) return -1;
            if (_WriteAddr209() < 0) return -1;
            if (_WriteAddr215() < 0) return -1;
            if (_WriteAddr216() < 0) return -1;
            if (_WriteAddr217() < 0) return -1;
            if (_WriteAddr218() < 0) return -1;
            if (_WriteAddr219() < 0) return -1;
            if (_WriteAddr21A() < 0) return -1;
            if (_WriteAddr220() < 0) return -1;
            if (_WriteAddr229() < 0) return -1;
            if (_WriteAddr22A() < 0) return -1;
            if (_WriteAddr22B() < 0) return -1;
            if (_WriteAddr22C() < 0) return -1;
            if (_WriteAddr23C() < 0) return -1;
            if (_WriteAddr243() < 0) return -1;
            if (_WriteAddr244() < 0) return -1;
            if (_WriteAddr245() < 0) return -1;
            if (_WriteAddr246() < 0) return -1;
            if (_WriteAddr247() < 0) return -1;
            if (_WriteAddr24C() < 0) return -1;
            if (_WriteAddr24D() < 0) return -1;
            if (_WriteAddr250() < 0) return -1;
            if (_WriteAddr252() < 0) return -1;
            if (_WriteAddr253() < 0) return -1;
            if (_WriteAddr300() < 0) return -1;
            if (_WriteAddr301() < 0) return -1;
            if (_WriteAddr302() < 0) return -1;
            if (_WriteAddr306() < 0) return -1;
            if (_WriteAddr308() < 0) return -1;
            if (_WriteAddr309() < 0) return -1;
            if (_WriteAddr315() < 0) return -1;
            if (_WriteAddr316() < 0) return -1;
            if (_WriteAddr317() < 0) return -1;
            if (_WriteAddr318() < 0) return -1;
            if (_WriteAddr319() < 0) return -1;
            if (_WriteAddr31A() < 0) return -1;
            if (_WriteAddr320() < 0) return -1;
            if (_WriteAddr329() < 0) return -1;
            if (_WriteAddr32A() < 0) return -1;
            if (_WriteAddr32B() < 0) return -1;
            if (_WriteAddr32C() < 0) return -1;
            if (_WriteAddr33C() < 0) return -1;
            if (_WriteAddr343() < 0) return -1;
            if (_WriteAddr344() < 0) return -1;
            if (_WriteAddr345() < 0) return -1;
            if (_WriteAddr346() < 0) return -1;
            if (_WriteAddr347() < 0) return -1;
            if (_WriteAddr34C() < 0) return -1;
            if (_WriteAddr34D() < 0) return -1;
            if (_WriteAddr350() < 0) return -1;
            if (_WriteAddr352() < 0) return -1;
            if (_WriteAddr353() < 0) return -1;
            if (_WriteAddr403() < 0) return -1;
            if (_WriteAddr405() < 0) return -1;
            if (_WriteAddr407() < 0) return -1;
            if (_WriteAddr40B() < 0) return -1;
            if (_WriteAddr40D() < 0) return -1;
            if (_WriteAddr40F() < 0) return -1;
            if (_WriteAddr410() < 0) return -1;
            if (_WriteAddr412() < 0) return -1;
            if (_WriteAddr427() < 0) return -1;
            if (_WriteAddr430() < 0) return -1;
            if (_WriteAddr431() < 0) return -1;
            if (_WriteAddr43D() < 0) return -1;
            if (_WriteAddr43E() < 0) return -1;
            if (_WriteAddr43F() < 0) return -1;
            if (_WriteAddr440() < 0) return -1;
            if (_WriteAddr441() < 0) return -1;
            if (_WriteAddr442() < 0) return -1;
            if (_WriteAddr446() < 0) return -1;
            if (_WriteAddr456() < 0) return -1;
            if (_WriteAddr457() < 0) return -1;
            if (_WriteAddr458() < 0) return -1;
            if (_WriteAddr459() < 0) return -1;
            if (_WriteAddr460() < 0) return -1;
            if (_WriteAddr464() < 0) return -1;
            if (_WriteAddr465_468() < 0) return -1;
            if (_WriteAddr469() < 0) return -1;
            if (_WriteAddr46A() < 0) return -1;
            if (_WriteAddr480() < 0) return -1;
            if (_WriteAddr483() < 0) return -1;
            if (_WriteAddr486() < 0) return -1;
            if (_WriteAddr487() < 0) return -1;
            if (_WriteAddr48B() < 0) return -1;
            if (_WriteAddr48E_48F() < 0) return -1;
            if (_WriteAddr490() < 0) return -1;
            if (_WriteAddr49D() < 0) return -1;
            if (_WriteAddr4A2() < 0) return -1;
            if (_WriteAddr4A3() < 0) return -1;
            if (_WriteAddr4A4() < 0) return -1;
            if (_WriteAddr4A5() < 0) return -1;
            if (_WriteAddr4A6() < 0) return -1;
            if (_WriteAddr4A7() < 0) return -1;
            if (_WriteAddr500() < 0) return -1;
            if (_WriteAddr501() < 0) return -1;
            if (_WriteAddr502() < 0) return -1;
            if (_WriteAddr503() < 0) return -1;
            if (_WriteAddr504() < 0) return -1;
            if (_WriteAddr505() < 0) return -1;
            if (_WriteAddr506() < 0) return -1;
            if (_WriteAddr507() < 0) return -1;
            if (_WriteAddr508() < 0) return -1;
            if (_WriteAddr509() < 0) return -1;
            if (_WriteAddr510() < 0) return -1;
            if (_WriteAddr511() < 0) return -1;
            if (_WriteAddr512() < 0) return -1;
            if (_WriteAddr513() < 0) return -1;
            if (_WriteAddr514() < 0) return -1;
            if (_WriteAddr515() < 0) return -1;
            if (_WriteAddr516() < 0) return -1;
            if (_WriteAddr517() < 0) return -1;
            if (_WriteAddr518() < 0) return -1;
            if (_WriteAddr519() < 0) return -1;
            if (_WriteAddr520() < 0) return -1;
            if (_WriteAddr521() < 0) return -1;
            if (_WriteAddr522() < 0) return -1;
            if (_WriteAddr523() < 0) return -1;
            if (_WriteAddr524() < 0) return -1;
            if (_WriteAddr525() < 0) return -1;
            if (_WriteAddr526() < 0) return -1;
            if (_WriteAddr527() < 0) return -1;
            if (_WriteAddr528() < 0) return -1;
            if (_WriteAddr529() < 0) return -1;
            if (_WriteAddr530() < 0) return -1;
            if (_WriteAddr531() < 0) return -1;
            if (_WriteAddr532() < 0) return -1;
            if (_WriteAddr533() < 0) return -1;
            if (_WriteAddr534() < 0) return -1;
            if (_WriteAddr535() < 0) return -1;
            if (_WriteAddr536() < 0) return -1;
            if (_WriteAddr537() < 0) return -1;
            if (_WriteAddr538() < 0) return -1;
            if (_WriteAddr539() < 0) return -1;
            if (_WriteAddr540() < 0) return -1;
            if (_WriteAddr541() < 0) return -1;
            if (_WriteAddr542() < 0) return -1;
            if (_WriteAddr543() < 0) return -1;
            if (_WriteAddr544() < 0) return -1;
            if (_WriteAddr545() < 0) return -1;
            if (_WriteAddr546() < 0) return -1;
            if (_WriteAddr547() < 0) return -1;
            if (_WriteAddr548() < 0) return -1;
            if (_WriteAddr549() < 0) return -1;
            if (_WriteAddr550() < 0) return -1;
            if (_WriteAddr551() < 0) return -1;

            return 0;
        }

        private int _WriteAddr000()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0CdrForceBypass.Checked == true)
                data[0] |= 0x01;

            if (cbL0CdrAutoBypass.Checked == true)
                data[0] |= 0x02;

            if (cbL0CdrPdOnBypass.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0001).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0002).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr006()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL0EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0006).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr008()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0008).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr009()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0EqBoost.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0009).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0015).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr016()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0016).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr017()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL0DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0017).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr018()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0VcselIbiasSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0018).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr019()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0VcselImodSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0019).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LdFiltOffReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x001A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr020()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LdBiasOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0020).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr029()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LdCpaCtrl1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0029).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr02A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LdCpaCtrl2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x002A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr02B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LdCompGain0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x002B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr02C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LdCompGain1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x002C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr03C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0VcselIbiasHighFaultOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x003C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr043()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0VcselIbiasHighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL0VcselIbiasLowFaultThres.SelectedItem);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0043).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr044()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0VcselVfHighFaulThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL0VcselVfLowFaulThres.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0044).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr045()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0VcselIbiasHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            if (cbL0VcselIbiasLowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            if (cbL0VcselVfHighFaultDetectEn.Checked == true)
                data[0] |= 0x04;

            if (cbL0VcselVfLowFaultDetectEn.Checked == true)
                data[0] |= 0x08;

            if (cbL0Vcc1HighFaultDetectEn.Checked == true)
                data[0] |= 0x10;

            if (cbL0Vcc1LowFaultDetectEn.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0045).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr046()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0VcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;

            if (cbL0VcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;

            if (cbL0VcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;

            if (cbL0VcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;

            if (cbL0Vcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;

            if (cbL0Vcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0046).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr047()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0VcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;

            if (cbL0VcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;

            if (cbL0VcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;

            if (cbL0VcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;

            if (cbL0Vcc1HighAlarm.Checked == true)
                data[0] |= 0x10;

            if (cbL0Vcc1LowAlarm.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0047).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x004C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x004D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr050()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL0PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0050).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr052()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL0PdPrbClk.Checked == true)
                data[0] |= 0x02;

            if (cbL0PdDrvPrbschk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0052).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr053()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LdPdBiasMon.Checked == true)
                data[0] |= 0x01;

            if (cbL0LdPdVfMon.Checked == true)
                data[0] |= 0x02;

            if (cbL0LdPdVcselComp.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0053).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbL0CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbL0CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbL0LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr001() < 0)
                return;
        }

        private void cbL0LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr002() < 0)
                return;
        }

        private void cbL0OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr006() < 0)
                return;
        }

        private void cbL0EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr006() < 0)
                return;
        }

        private void cbL0EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr008() < 0)
                return;
        }

        private void cbL0EqBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr009() < 0)
                return;
        }

        private void cbL0CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr015() < 0)
                return;
        }

        private void cbL0DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr016() < 0)
                return;
        }

        private void cbL0DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr017() < 0)
                return;
        }

        private void cbL0DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr017() < 0)
                return;
        }

        private void cbL0VcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr018() < 0)
                return;
        }

        private void cbL0VcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr019() < 0)
                return;
        }

        private void cbL0LdFiltOffReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01A() < 0)
                return;
        }

        private void cbL0LdBiasOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr020() < 0)
                return;
        }

        private void cbL0LdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr029() < 0)
                return;
        }

        private void cbL0LdCpaCtrl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02A() < 0)
                return;
        }

        private void cbL0LdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02B() < 0)
                return;
        }

        private void cbL0LdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02C() < 0)
                return;
        }

        private void cbL0VcselIbiasHighFaultOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03C() < 0)
                return;
        }

        private void cbL0VcselIbiasHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr043() < 0)
                return;
        }

        private void cbL0VcselIbiasLowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr043() < 0)
                return;
        }

        private void cbL0VcselVfHighFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr044() < 0)
                return;
        }

        private void cbL0VcselVfLowFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr044() < 0)
                return;
        }

        private void cbL0VcselIbiasHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0VcselIbiasLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0VcselVfHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0VcselVfLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0Vcc1HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0Vcc1LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr045() < 0)
                return;
        }

        private void cbL0VcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0VcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0VcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0VcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0Vcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0Vcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr046() < 0)
                return;
        }

        private void cbL0VcselIbiasHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0VcselIbiasLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0VcselVfHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0VcselVfLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0Vcc1HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0Vcc1LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr047() < 0)
                return;
        }

        private void cbL0PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04C() < 0)
                return;
        }

        private void cbL0PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04D() < 0)
                return;
        }

        private void cbL0PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr050() < 0)
                return;
        }

        private void cbL0PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr050() < 0)
                return;
        }

        private void cbL0PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr052() < 0)
                return;
        }

        private void cbL0PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr052() < 0)
                return;
        }

        private void cbL0PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr052() < 0)
                return;
        }

        private void cbL0LdPdBiasMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr053() < 0)
                return;
        }

        private void cbL0LdPdVfMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr053() < 0)
                return;
        }

        private void cbL0LdPdVcselComp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr053() < 0)
                return;
        }

        private int _WriteAddr100()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1CdrForceBypass.Checked == true)
                data[0] |= 0x01;

            if (cbL1CdrAutoBypass.Checked == true)
                data[0] |= 0x02;

            if (cbL1CdrPdOnBypass.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr101()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0101).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr102()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0102).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr106()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL1EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0106).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr108()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0108).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr109()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1EqBoost.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0109).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr115()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0115).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr116()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0116).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr117()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL1DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr118()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1VcselIbiasSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0118).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr119()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1VcselImodSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0119).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1LdFiltOffReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x011A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr120()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1LdBiasOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0120).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr129()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LdCpaCtrl1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0129).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LdCpaCtrl2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x012A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LdCompGain0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x012B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LdCompGain1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x012C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr13C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1VcselIbiasHighFaultOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x013C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr143()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1VcselIbiasHighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL1VcselIbiasLowFaultThres.SelectedItem);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0143).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr144()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1VcselVfHighFaulThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL1VcselVfLowFaulThres.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0144).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr145()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1VcselIbiasHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            if (cbL1VcselIbiasLowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            if (cbL1VcselVfHighFaultDetectEn.Checked == true)
                data[0] |= 0x04;

            if (cbL1VcselVfLowFaultDetectEn.Checked == true)
                data[0] |= 0x08;

            if (cbL1Vcc1HighFaultDetectEn.Checked == true)
                data[0] |= 0x10;

            if (cbL1Vcc1LowFaultDetectEn.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0145).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr146()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1VcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;

            if (cbL1VcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;

            if (cbL1VcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;

            if (cbL1VcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;

            if (cbL1Vcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;

            if (cbL1Vcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0146).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr147()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1VcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;

            if (cbL1VcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;

            if (cbL1VcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;

            if (cbL1VcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;

            if (cbL1Vcc1HighAlarm.Checked == true)
                data[0] |= 0x10;

            if (cbL1Vcc1LowAlarm.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0147).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr14C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x014C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr14D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x014D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr150()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL1PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0150).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr152()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL1PdPrbClk.Checked == true)
                data[0] |= 0x02;

            if (cbL1PdDrvPrbschk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0152).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr153()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1LdPdBiasMon.Checked == true)
                data[0] |= 0x01;

            if (cbL1LdPdVfMon.Checked == true)
                data[0] |= 0x02;

            if (cbL1LdPdVcselComp.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0153).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL1CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private void cbL1CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private void cbL1CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private void cbL1LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr101() < 0)
                return;
        }

        private void cbL1LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr102() < 0)
                return;
        }

        private void cbL1OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr106() < 0)
                return;
        }

        private void cbL1EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr106() < 0)
                return;
        }

        private void cbL1EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr108() < 0)
                return;
        }

        private void cbL1EqBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbL1CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void cbL1DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr116() < 0)
                return;
        }

        private void cbL1DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr117() < 0)
                return;
        }

        private void cbL1DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr117() < 0)
                return;
        }

        private void cbL1VcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr118() < 0)
                return;
        }

        private void cbL1VcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr119() < 0)
                return;
        }

        private void cbL1LdFiltOffReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11A() < 0)
                return;
        }

        private void cbL1LdBiasOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr120() < 0)
                return;
        }

        private void cbL1LdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void cbL1LdCpaCtrl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12A() < 0)
                return;
        }

        private void cbL1LdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12B() < 0)
                return;
        }

        private void cbL1LdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12C() < 0)
                return;
        }

        private void cbL1VcselIbiasHighFaultOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13C() < 0)
                return;
        }

        private void cbL1VcselIbiasHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbL1VcselIbiasLowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbL1VcselVfHighFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr144() < 0)
                return;
        }

        private void cbL1VcselVfLowFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr144() < 0)
                return;
        }

        private void cbL1VcselIbiasHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1VcselIbiasLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1VcselVfHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1VcselVfLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1Vcc1HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1Vcc1LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbL1VcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1VcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1VcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1VcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1Vcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1Vcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void cbL1VcselIbiasHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1VcselIbiasLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1VcselVfHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1VcselVfLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1Vcc1HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1Vcc1LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void cbL1PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14C() < 0)
                return;
        }

        private void cbL1PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14D() < 0)
                return;
        }

        private void cbL1PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr150() < 0)
                return;
        }

        private void cbL1PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr150() < 0)
                return;
        }

        private void cbL1PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr152() < 0)
                return;
        }

        private void cbL1PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr152() < 0)
                return;
        }

        private void cbL1PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr152() < 0)
                return;
        }

        private void cbL1LdPdBiasMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr153() < 0)
                return;
        }

        private void cbL1LdPdVfMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr153() < 0)
                return;
        }

        private void cbL1LdPdVcselComp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr153() < 0)
                return;
        }

        private int _WriteAddr200()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2CdrForceBypass.Checked == true)
                data[0] |= 0x01;

            if (cbL2CdrAutoBypass.Checked == true)
                data[0] |= 0x02;

            if (cbL2CdrPdOnBypass.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0200).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr201()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0201).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr202()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0202).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr206()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL2EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0206).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr208()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0208).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr209()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2EqBoost.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0209).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr215()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0215).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr216()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0216).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr217()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL2DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0217).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr218()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2VcselIbiasSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0218).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr219()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2VcselImodSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0219).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr21A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2LdFiltOffReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x021A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr220()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2LdBiasOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0220).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr229()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LdCpaCtrl1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0229).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LdCpaCtrl2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x022A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LdCompGain0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x022B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LdCompGain1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x022C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr23C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2VcselIbiasHighFaultOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x023C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr243()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2VcselIbiasHighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL2VcselIbiasLowFaultThres.SelectedItem);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0243).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr244()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2VcselVfHighFaulThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL2VcselVfLowFaulThres.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0244).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr245()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2VcselIbiasHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            if (cbL2VcselIbiasLowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            if (cbL2VcselVfHighFaultDetectEn.Checked == true)
                data[0] |= 0x04;

            if (cbL2VcselVfLowFaultDetectEn.Checked == true)
                data[0] |= 0x08;

            if (cbL2Vcc1HighFaultDetectEn.Checked == true)
                data[0] |= 0x10;

            if (cbL2Vcc1LowFaultDetectEn.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0245).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr246()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2VcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;

            if (cbL2VcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;

            if (cbL2VcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;

            if (cbL2VcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;

            if (cbL2Vcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;

            if (cbL2Vcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0246).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr247()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2VcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;

            if (cbL2VcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;

            if (cbL2VcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;

            if (cbL2VcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;

            if (cbL2Vcc1HighAlarm.Checked == true)
                data[0] |= 0x10;

            if (cbL2Vcc1LowAlarm.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0247).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr24C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x024C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr24D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x024D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr250()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL2PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0250).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr252()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL2PdPrbClk.Checked == true)
                data[0] |= 0x02;

            if (cbL2PdDrvPrbschk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0252).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr253()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2LdPdBiasMon.Checked == true)
                data[0] |= 0x01;

            if (cbL2LdPdVfMon.Checked == true)
                data[0] |= 0x02;

            if (cbL2LdPdVcselComp.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0253).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL2CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr200() < 0)
                return;
        }

        private void cbL2CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr200() < 0)
                return;
        }

        private void cbL2CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr200() < 0)
                return;
        }

        private void cbL2LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr201() < 0)
                return;
        }

        private void cbL2LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr202() < 0)
                return;
        }

        private void cbL2OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr206() < 0)
                return;
        }

        private void cbL2EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr206() < 0)
                return;
        }

        private void cbL2EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr208() < 0)
                return;
        }

        private void cbL2EqBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr209() < 0)
                return;
        }

        private void cbL2CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr215() < 0)
                return;
        }

        private void cbL2DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr216() < 0)
                return;
        }

        private void cbL2DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr217() < 0)
                return;
        }

        private void cbL2DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr217() < 0)
                return;
        }

        private void cbL2VcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr218() < 0)
                return;
        }

        private void cbL2VcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr219() < 0)
                return;
        }

        private void cbL2LdFiltOffReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21A() < 0)
                return;
        }

        private void cbL2LdBiasOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr220() < 0)
                return;
        }

        private void cbL2LdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr229() < 0)
                return;
        }

        private void cbL2LdCpaCtrl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22A() < 0)
                return;
        }

        private void cbL2LdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22B() < 0)
                return;
        }

        private void cbL2LdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22C() < 0)
                return;
        }

        private void cbL2VcselIbiasHighFaultOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23C() < 0)
                return;
        }

        private void cbL2VcselIbiasHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr243() < 0)
                return;
        }

        private void cbL2VcselIbiasLowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr243() < 0)
                return;
        }

        private void cbL2VcselVfHighFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr244() < 0)
                return;
        }

        private void cbL2VcselVfLowFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr244() < 0)
                return;
        }

        private void cbL2VcselIbiasHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2VcselIbiasLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2VcselVfHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2VcselVfLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2Vcc1HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2Vcc1LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr245() < 0)
                return;
        }

        private void cbL2VcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2VcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2VcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2VcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2Vcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2Vcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr246() < 0)
                return;
        }

        private void cbL2VcselIbiasHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2VcselIbiasLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2VcselVfHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2VcselVfLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2Vcc1HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2Vcc1LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr247() < 0)
                return;
        }

        private void cbL2PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24C() < 0)
                return;
        }

        private void cbL2PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24D() < 0)
                return;
        }

        private void cbL2PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr250() < 0)
                return;
        }

        private void cbL2PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr250() < 0)
                return;
        }

        private void cbL2PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr252() < 0)
                return;
        }

        private void cbL2PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr252() < 0)
                return;
        }

        private void cbL2PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr252() < 0)
                return;
        }

        private void cbL2LdPdBiasMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr253() < 0)
                return;
        }

        private void cbL2LdPdVfMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr253() < 0)
                return;
        }

        private void cbL2LdPdVcselComp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr253() < 0)
                return;
        }

        private int _WriteAddr300()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3CdrForceBypass.Checked == true)
                data[0] |= 0x01;

            if (cbL3CdrAutoBypass.Checked == true)
                data[0] |= 0x02;

            if (cbL3CdrPdOnBypass.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0300).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr301()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0301).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr302()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0302).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr306()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL3EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0306).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr308()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0308).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr309()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3EqBoost.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0309).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr315()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0315).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr316()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0316).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr317()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL3DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0317).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr318()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3VcselIbiasSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0318).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr319()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3VcselImodSet.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0319).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr31A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3LdFiltOffReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x031A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr320()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3LdBiasOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0320).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr329()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LdCpaCtrl1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0329).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr32A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LdCpaCtrl2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x032A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr32B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LdCompGain0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x032B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr32C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LdCompGain1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x032C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr33C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3VcselIbiasHighFaultOffsetReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x033C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr343()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3VcselIbiasHighFaultThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL3VcselIbiasLowFaultThres.SelectedItem);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0343).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr344()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3VcselVfHighFaulThres.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbL3VcselVfLowFaulThres.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0344).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr345()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3VcselIbiasHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            if (cbL3VcselIbiasLowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            if (cbL3VcselVfHighFaultDetectEn.Checked == true)
                data[0] |= 0x04;

            if (cbL3VcselVfLowFaultDetectEn.Checked == true)
                data[0] |= 0x08;

            if (cbL3Vcc1HighFaultDetectEn.Checked == true)
                data[0] |= 0x10;

            if (cbL3Vcc1LowFaultDetectEn.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0345).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr346()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3VcselIbiasHighAlarmDisable.Checked == true)
                data[0] |= 0x01;

            if (cbL3VcselIbiasLowAlarmDisable.Checked == true)
                data[0] |= 0x02;

            if (cbL3VcselVfHighAlarmDisable.Checked == true)
                data[0] |= 0x04;

            if (cbL3VcselVfLowAlarmDisable.Checked == true)
                data[0] |= 0x08;

            if (cbL3Vcc1HighAlarmDisable.Checked == true)
                data[0] |= 0x10;

            if (cbL3Vcc1LowAlarmDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0346).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr347()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3VcselIbiasHighAlarm.Checked == true)
                data[0] |= 0x01;

            if (cbL3VcselIbiasLowAlarm.Checked == true)
                data[0] |= 0x02;

            if (cbL3VcselVfHighAlarm.Checked == true)
                data[0] |= 0x04;

            if (cbL3VcselVfLowAlarm.Checked == true)
                data[0] |= 0x08;

            if (cbL3Vcc1HighAlarm.Checked == true)
                data[0] |= 0x10;

            if (cbL3Vcc1LowAlarm.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0347).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr34C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x034C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr34D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x034D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr350()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL3PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0350).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr352()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL3PdPrbClk.Checked == true)
                data[0] |= 0x02;

            if (cbL3PdDrvPrbschk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0352).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr353()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3LdPdBiasMon.Checked == true)
                data[0] |= 0x01;

            if (cbL3LdPdVfMon.Checked == true)
                data[0] |= 0x02;

            if (cbL3LdPdVcselComp.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0353).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL3CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr300() < 0)
                return;
        }

        private void cbL3CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr300() < 0)
                return;
        }

        private void cbL3CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr300() < 0)
                return;
        }

        private void cbL3LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr301() < 0)
                return;
        }

        private void cbL3LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr302() < 0)
                return;
        }

        private void cbL3OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr306() < 0)
                return;
        }

        private void cbL3EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr306() < 0)
                return;
        }

        private void cbL3EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr308() < 0)
                return;
        }

        private void cbL3EqBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr309() < 0)
                return;
        }

        private void cbL3CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr315() < 0)
                return;
        }

        private void cbL3DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr316() < 0)
                return;
        }

        private void cbL3DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr317() < 0)
                return;
        }

        private void cbL3DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr317() < 0)
                return;
        }

        private void cbL3VcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr318() < 0)
                return;
        }

        private void cbL3VcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr319() < 0)
                return;
        }

        private void cbL3LdFiltOffReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr31A() < 0)
                return;
        }

        private void cbL3LdBiasOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr320() < 0)
                return;
        }

        private void cbL3LdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr329() < 0)
                return;
        }

        private void cbL3LdCpaCtrl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32A() < 0)
                return;
        }

        private void cbL3LdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32B() < 0)
                return;
        }

        private void cbL3LdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32C() < 0)
                return;
        }

        private void cbL3VcselIbiasHighFaultOffsetReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr33C() < 0)
                return;
        }

        private void cbL3VcselIbiasHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr343() < 0)
                return;
        }

        private void cbL3VcselIbiasLowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr343() < 0)
                return;
        }

        private void cbL3VcselVfHighFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr344() < 0)
                return;
        }

        private void cbL3VcselVfLowFaulThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr344() < 0)
                return;
        }

        private void cbL3VcselIbiasHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3VcselIbiasLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3VcselVfHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3VcselVfLowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3Vcc1HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3Vcc1LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr345() < 0)
                return;
        }

        private void cbL3VcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3VcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3VcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3VcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3Vcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3Vcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr346() < 0)
                return;
        }

        private void cbL3VcselIbiasHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3VcselIbiasLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3VcselVfHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3VcselVfLowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3Vcc1HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3Vcc1LowAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr347() < 0)
                return;
        }

        private void cbL3PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr34C() < 0)
                return;
        }

        private void cbL3PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr34D() < 0)
                return;
        }

        private void cbL3PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr350() < 0)
                return;
        }

        private void cbL3PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr350() < 0)
                return;
        }

        private void cbL3PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr352() < 0)
                return;
        }

        private void cbL3PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr352() < 0)
                return;
        }

        private void cbL3PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr352() < 0)
                return;
        }

        private void cbL3LdPdBiasMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr353() < 0)
                return;
        }

        private void cbL3LdPdVfMon_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr353() < 0)
                return;
        }

        private void cbL3LdPdVcselComp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr353() < 0)
                return;
        }

        private int _WriteAddr403()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LosSoftAssertEn.Checked == true)
                data[0] |= 0x01;

            if (cbL1LosSoftAssertEn.Checked == true)
                data[0] |= 0x02;

            if (cbL2LosSoftAssertEn.Checked == true)
                data[0] |= 0x04;

            if (cbL3LosSoftAssertEn.Checked == true)
                data[0] |= 0x08;

            if (cbL0LosSoftAssert.Checked == true)
                data[0] |= 0x10;

            if (cbL1LosSoftAssert.Checked == true)
                data[0] |= 0x20;

            if (cbL2LosSoftAssert.Checked == true)
                data[0] |= 0x40;

            if (cbL3LosSoftAssert.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0403).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr405()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LosChangeDetect.Checked == true)
                data[0] |= 0x01;

            if (cbL1LosChangeDetect.Checked == true)
                data[0] |= 0x02;

            if (cbL2LosChangeDetect.Checked == true)
                data[0] |= 0x04;

            if (cbL3LosChangeDetect.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0405).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr407()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLockdetLockThres.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0407).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LolSoftAssertEn.Checked == true)
                data[0] |= 0x01;

            if (cbL1LolSoftAssertEn.Checked == true)
                data[0] |= 0x02;

            if (cbL2LolSoftAssertEn.Checked == true)
                data[0] |= 0x04;

            if (cbL3LolSoftAssertEn.Checked == true)
                data[0] |= 0x08;

            if (cbL0LolSoftAssert.Checked == true)
                data[0] |= 0x10;

            if (cbL1LolSoftAssert.Checked == true)
                data[0] |= 0x20;

            if (cbL2LolSoftAssert.Checked == true)
                data[0] |= 0x40;

            if (cbL3LolSoftAssert.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x040B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LolChangeDetect.Checked == true)
                data[0] |= 0x01;

            if (cbL1LolChangeDetect.Checked == true)
                data[0] |= 0x02;

            if (cbL2LolChangeDetect.Checked == true)
                data[0] |= 0x04;

            if (cbL3LolChangeDetect.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x040D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LosToLoslEn.Checked == true)
                data[0] |= 0x01;

            if (cbL1LosToLoslEn.Checked == true)
                data[0] |= 0x02;

            if (cbL2LosToLoslEn.Checked == true)
                data[0] |= 0x04;

            if (cbL3LosToLoslEn.Checked == true)
                data[0] |= 0x08;

            if (cbL0LolToLoslEn.Checked == true)
                data[0] |= 0x10;

            if (cbL1LolToLoslEn.Checked == true)
                data[0] |= 0x20;

            if (cbL2LolToLoslEn.Checked == true)
                data[0] |= 0x40;

            if (cbL3LolToLoslEn.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x040F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr410()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLoslInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0410).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr412()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0LosLatch.Checked == true)
                data[0] |= 0x01;

            if (cbL1LosLatch.Checked == true)
                data[0] |= 0x02;

            if (cbL2LosLatch.Checked == true)
                data[0] |= 0x04;

            if (cbL3LosLatch.Checked == true)
                data[0] |= 0x08;

            if (cbL0LolLatch.Checked == true)
                data[0] |= 0x10;

            if (cbL1LolLatch.Checked == true)
                data[0] |= 0x20;

            if (cbL2LolLatch.Checked == true)
                data[0] |= 0x40;

            if (cbL3LolLatch.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0412).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr427()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLdBurnInEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0427).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr430()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLdCompGain0Scale.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0430).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr431()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLdCompGain1Scale.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0431).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcc2HighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            if (cbVcc2LowFaultDetectEn.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x043D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43E()
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

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x043E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTxFaultAlarmDisable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x043F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr440()
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

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0440).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr441()
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

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0441).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr442()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0TxdsblSoft.Checked == true)
                data[0] |= 0x01;

            if (cbL1TxdsblSoft.Checked == true)
                data[0] |= 0x02;

            if (cbL2TxdsblSoft.Checked == true)
                data[0] |= 0x04;

            if (cbL3TxdsblSoft.Checked == true)
                data[0] |= 0x08;

            if (cbTxFaultPolarity.Checked == true)
                data[0] |= 0x10;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0442).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr446()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbVcc2HighAlarm.Checked == true)
                data[0] |= 0x01;

            if (cbVcc2LowAlarm.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0446).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL1LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL2LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL3LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL0LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL1LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL2LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL3LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr403() < 0)
                return;
        }

        private void cbL0LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr405() < 0)
                return;
        }

        private void cbL1LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr405() < 0)
                return;
        }

        private void cbL2LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr405() < 0)
                return;
        }

        private void cbL3LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr405() < 0)
                return;
        }

        private void cbLockdetLockThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr407() < 0)
                return;
        }

        private void cbL0LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL1LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL2LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL3LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL0LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL1LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL2LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL3LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40B() < 0)
                return;
        }

        private void cbL0LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40D() < 0)
                return;
        }

        private void cbL1LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40D() < 0)
                return;
        }

        private void cbL2LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40D() < 0)
                return;
        }

        private void cbL3LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40D() < 0)
                return;
        }

        private void cbL0LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL1LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL2LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL3LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL0LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL1LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL2LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbL3LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40F() < 0)
                return;
        }

        private void cbLoslInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL0LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL1LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL2LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL3LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL0LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL1LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL2LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL3LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbLdBurnInEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr427() < 0)
                return;
        }

        private void cbLdCompGain0Scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr430() < 0)
                return;
        }

        private void cbLdCompGain1Scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr431() < 0)
                return;
        }

        private void cbVcc2HighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43D() < 0)
                return;
        }

        private void cbVcc2LowFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43D() < 0)
                return;
        }

        private void cbVcselIbiasHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcselIbiasLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcselVfHighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcselVfLowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcc1HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcc1LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcc2HighAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbVcc2LowAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbTxFaultAlarmDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43F() < 0)
                return;
        }

        private void cbVcc1HighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr440() < 0)
                return;
        }

        private void cbVcc1LowFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr440() < 0)
                return;
        }

        private void cbVcc2HighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr441() < 0)
                return;
        }

        private void cbL0TxdsblSoft_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr442() < 0)
                return;
        }

        private void cbL1TxdsblSoft_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr442() < 0)
                return;
        }

        private void cbL2TxdsblSoft_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr442() < 0)
                return;
        }

        private void cbL3TxdsblSoft_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr442() < 0)
                return;
        }

        private void cbTxFaultPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr442() < 0)
                return;
        }

        private void cbVcc2HighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr446() < 0)
                return;
        }

        private int _WriteAddr456()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PrbsClkSel.Checked == true)
                data[0] |= 0x01;

            if (cbL1PrbsClkSel.Checked == true)
                data[0] |= 0x02;

            if (cbL2PrbsClkSel.Checked == true)
                data[0] |= 0x04;

            if (cbL3PrbsClkSel.Checked == true)
                data[0] |= 0x08;

            if (cbL0PrbsDataSel.Checked == true)
                data[0] |= 0x10;

            if (cbL1PrbsDataSel.Checked == true)
                data[0] |= 0x20;

            if (cbL2PrbsDataSel.Checked == true)
                data[0] |= 0x40;

            if (cbL3PrbsDataSel.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0456).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr457()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbsgenStart.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0457).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr458()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPrbsgenSequenceSel.SelectedItem);
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbPrbsgenOutputSel.SelectedItem);
            bTmp <<= 2;
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbPrbsgenCkdivRate.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0458).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr459()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbsgenCkSel.Checked == true)
                data[0] |= 0x01;

            if (cbPrbsgenCkdivCkSel.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0459).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr45E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPrbsgenVcoFreq.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x045E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr460()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbPrbschkInvPrbs.Checked == true)
                data[0] |= 0x01;

            bTmp = Convert.ToByte(cbPrbschkSelPrbs.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;
            
            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0460).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr464()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPrbschkTimerClkSel.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0464).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr465_468()
        {
            byte[] data;
            int rv;

            if (tbPrbschkTimeout.Text.Length == 0)
                return -1;

            data = BitConverter.GetBytes(Convert.ToUInt32(tbPrbschkTimeout.Text));

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0465).Reverse().ToArray(), 4, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr469()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbschkTimerEnable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0469).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr46A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbschkEnable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x046A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr480()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbAdcSrcSel.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0480).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr483()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcParamMonCtrlReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0483).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr486()
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

            if (cbL0Vcc1SupplyMonEnable.Checked == true)
                data[0] |= 0x10;

            if (cbL1Vcc1SupplyMonEnable.Checked == true)
                data[0] |= 0x20;

            if (cbL2Vcc1SupplyMonEnable.Checked == true)
                data[0] |= 0x40;

            if (cbL3Vcc1SupplyMonEnable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0486).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr487()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0VcselIbiasMonEnable.Checked == true)
                data[0] |= 0x01;

            if (cbL1VcselIbiasMonEnable.Checked == true)
                data[0] |= 0x02;

            if (cbL2VcselIbiasMonEnable.Checked == true)
                data[0] |= 0x04;

            if (cbL3VcselIbiasMonEnable.Checked == true)
                data[0] |= 0x08;

            if (cbL0VcselVfMonEnable.Checked == true)
                data[0] |= 0x10;

            if (cbL1VcselVfMonEnable.Checked == true)
                data[0] |= 0x20;

            if (cbL2VcselVfMonEnable.Checked == true)
                data[0] |= 0x40;

            if (cbL3VcselVfMonEnable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0487).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr48B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcHostCtrlReq.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x048B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr48D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbAdcReset.Checked == true)
                data[0] |= 0x01;

            if (cbAdcAutoConvEn.Checked == true)
                data[0] |= 0x02;

            if (cbAdcJustLsb.Checked == true)
                data[0] |= 0x04;

            if (cbAdcOffMode.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbAdcResolution.SelectedItem);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x048D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr48E_48F()
        {
            byte[] data;
            int rv;

            if (tbAdcOffset.Text.Length == 0)
                return -1;

            data = BitConverter.GetBytes(Convert.ToUInt16(tbAdcOffset.Text));

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x046E).Reverse().ToArray(), 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr490()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcStartConv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0490).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr49D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbBbStep.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x049D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A2).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A3()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLdPdVfmonStg2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A3).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbL0PdPrbsgenDataBuf.Checked == true)
                data[0] |= 0x01;

            if (cbL1PdPrbsgenDataBuf.Checked == true)
                data[0] |= 0x02;

            if (cbL2PdPrbsgenDataBuf.Checked == true)
                data[0] |= 0x04;

            if (cbL3PdPrbsgenDataBuf.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A4).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbPdPrbsgen.Checked == true)
                data[0] |= 0x01;

            if (cbPdPrbsgenCkdiv.Checked == true)
                data[0] |= 0x02;

            if (cbPdPrbsgenVco.Checked == true)
                data[0] |= 0x04;

            if (cbPdPrbsgenAll.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A5).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbPdPrbschk.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A6).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbPdTempSense.Checked == true)
                data[0] |= 0x01;

            if (cbPdSupplySense.Checked == true)
                data[0] |= 0x02;

            if (cbPdAdc.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x04A7).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL1PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL2PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL3PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL0PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL1PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL2PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbL3PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr456() < 0)
                return;
        }

        private void cbPrbsgenStart_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr457() < 0)
                return;
        }

        private void cbPrbsgenSequenceSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr458() < 0)
                return;
        }

        private void cbPrbsgenOutputSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr458() < 0)
                return;
        }

        private void cbPrbsgenCkdivRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr458() < 0)
                return;
        }

        private void cbPrbsgenCkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr459() < 0)
                return;
        }

        private void cbPrbsgenCkdivCkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr459() < 0)
                return;
        }

        private void cbPrbsgenVcoFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr45E() < 0)
                return;
        }

        private void cbPrbschkInvPrbs_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr460() < 0)
                return;
        }

        private void cbPrbschkSelPrbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr460() < 0)
                return;
        }

        private void cbPrbschkTimerClkSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr464() < 0)
                return;
        }

        private void tbPrbschkTimeout_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr465_468() < 0)
                return;
        }

        private void cbPrbschkTimerEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr469() < 0)
                return;
        }

        private void cbPrbschkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46A() < 0)
                return;
        }

        private void cbAdcSrcSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr480() < 0)
                return;
        }

        private void cbAdcParamMonCtrlReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr483() < 0)
                return;
        }

        private void cbTemperatureMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbVcclSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbVccSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbVcc2SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbL0Vcc1SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbL1Vcc1SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbL2Vcc1SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbL3Vcc1SupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbL0VcselIbiasMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL1VcselIbiasMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL2VcselIbiasMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL3VcselIbiasMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL0VcselVfMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL1VcselVfMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL2VcselVfMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbL3VcselVfMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbAdcHostCtrlReq_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48B() < 0)
                return;
        }

        private void cbAdcReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48D() < 0)
                return;
        }

        private void cbAdcAutoConvEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48D() < 0)
                return;
        }

        private void cbAdcJustLsb_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48D() < 0)
                return;
        }

        private void cbAdcOffMode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48D() < 0)
                return;
        }

        private void cbAdcResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48D() < 0)
                return;
        }

        private void tbAdcOffset_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48E_48F() < 0)
                return;
        }

        private void cbAdcStartConv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr490() < 0)
                return;
        }

        private void cbBbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr49D() < 0)
                return;
        }

        private void cbPdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A2() < 0)
                return;
        }

        private void cbLdPdVfmonStg2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A3() < 0)
                return;
        }

        private void cbL0PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A4() < 0)
                return;
        }

        private void cbL1PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A4() < 0)
                return;
        }

        private void cbL2PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A4() < 0)
                return;
        }

        private void cbL3PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A4() < 0)
                return;
        }

        private void cbPdPrbsgen_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A5() < 0)
                return;
        }

        private void cbPdPrbsgenCkdiv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A5() < 0)
                return;
        }

        private void cbPdPrbsgenVco_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A5() < 0)
                return;
        }

        private void cbPdPrbsgenAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A5() < 0)
                return;
        }

        private void cbPdPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A6() < 0)
                return;
        }

        private void cbPdTempSense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A7() < 0)
                return;
        }

        private void cbPdSupplySense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A7() < 0)
                return;
        }

        private void cbPdAdc_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A7() < 0)
                return;
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            bReadAll.Enabled = false;
            bL0Read_Click(sender, e);
            bL1Read_Click(sender, e);
            bL2Read_Click(sender, e);
            bL3Read_Click(sender, e);
            bControl1Read_Click(sender, e);
            bControl2Read_Click(sender, e);
            bCustomerRead_Click(sender, e);
            bReadAll.Enabled = true;
        }

        public int ReadAllApi()
        {
            try {
                bL0Read_Click(null, null);
                bL1Read_Click(null, null);
                bL2Read_Click(null, null);
                bL3Read_Click(null, null);
                bControl1Read_Click(null, null);
                bControl2Read_Click(null, null);
                bCustomerRead_Click(null, null);

                return 0;
            }
            catch {
            	return -1;
            }
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1] { 0xA0 };
            int rv;

            bStoreIntoFlash.Enabled = false;
            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0AAA).Reverse().ToArray(), 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
        }

        private void cbAllEqBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0EqBoost.SelectedIndex = cbL1EqBoost.SelectedIndex = 
                cbL2EqBoost.SelectedIndex = cbL3EqBoost.SelectedIndex = 
                cbAllEqBoost.SelectedIndex;
        }

        private void cbAllVcselIbiasSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0VcselIbiasSet.SelectedIndex = cbL1VcselIbiasSet.SelectedIndex = 
                cbL2VcselIbiasSet.SelectedIndex = cbL3VcselIbiasSet.SelectedIndex = 
                cbAllVcselIbiasSet.SelectedIndex;
        }

        private void cbAllVcselImodSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0VcselImodSet.SelectedIndex = cbL1VcselImodSet.SelectedIndex = 
                cbL2VcselImodSet.SelectedIndex = cbL3VcselImodSet.SelectedIndex = 
                cbAllVcselImodSet.SelectedIndex;
        }

        private void cbAllLdCpaCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0LdCpaCtrl1.SelectedIndex = cbL1LdCpaCtrl1.SelectedIndex = 
                cbL2LdCpaCtrl1.SelectedIndex = cbL3LdCpaCtrl1.SelectedIndex = 
                cbAllLdCpaCtrl1.SelectedIndex;
        }

        private void cbAllLdCpaCtrl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0LdCpaCtrl2.SelectedIndex = cbL1LdCpaCtrl2.SelectedIndex = 
                cbL2LdCpaCtrl2.SelectedIndex = cbL3LdCpaCtrl2.SelectedIndex = 
                cbAllLdCpaCtrl2.SelectedIndex;
        }

        private void cbAllLdCompGain0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0LdCompGain0.SelectedIndex = cbL1LdCompGain0.SelectedIndex = 
                cbL2LdCompGain0.SelectedIndex = cbL3LdCompGain0.SelectedIndex = 
                cbAllLdCompGain0.SelectedIndex;
        }

        private void cbAllLdCompGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0LdCompGain1.SelectedIndex = cbL1LdCompGain1.SelectedIndex =
                cbL2LdCompGain1.SelectedIndex = cbL3LdCompGain1.SelectedIndex = 
                cbAllLdCompGain1.SelectedIndex;
        }

        private void _ParseAddr500(byte data)
        {
            cbL0InputEqualization0db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr501(byte data)
        {
            cbL0InputEqualization1db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr502(byte data)
        {
            cbL0InputEqualization2db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr503(byte data)
        {
            cbL0InputEqualization3db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr504(byte data)
        {
            cbL0InputEqualization4db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr505(byte data)
        {
            cbL0InputEqualization5db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr506(byte data)
        {
            cbL0InputEqualization6db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr507(byte data)
        {
            cbL0InputEqualization7db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr508(byte data)
        {
            cbL0InputEqualization8db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr509(byte data)
        {
            cbL0InputEqualization9db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr510(byte data)
        {
            cbL0InputEqualization10db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr511(byte data)
        {
            cbL0InputEqualizationReserved0.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr512(byte data)
        {
            cbL0InputEqualizationReserved1.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr513(byte data)
        {
            cbL1InputEqualization0db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr514(byte data)
        {
            cbL1InputEqualization1db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr515(byte data)
        {
            cbL1InputEqualization2db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr516(byte data)
        {
            cbL1InputEqualization3db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr517(byte data)
        {
            cbL1InputEqualization4db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr518(byte data)
        {
            cbL1InputEqualization5db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr519(byte data)
        {
            cbL1InputEqualization6db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr520(byte data)
        {
            cbL1InputEqualization7db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr521(byte data)
        {
            cbL1InputEqualization8db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr522(byte data)
        {
            cbL1InputEqualization9db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr523(byte data)
        {
            cbL1InputEqualization10db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr524(byte data)
        {
            cbL1InputEqualizationReserved0.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr525(byte data)
        {
            cbL1InputEqualizationReserved1.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr526(byte data)
        {
            cbL2InputEqualization0db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr527(byte data)
        {
            cbL2InputEqualization1db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr528(byte data)
        {
            cbL2InputEqualization2db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr529(byte data)
        {
            cbL2InputEqualization3db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr530(byte data)
        {
            cbL2InputEqualization4db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr531(byte data)
        {
            cbL2InputEqualization5db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr532(byte data)
        {
            cbL2InputEqualization6db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr533(byte data)
        {
            cbL2InputEqualization7db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr534(byte data)
        {
            cbL2InputEqualization8db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr535(byte data)
        {
            cbL2InputEqualization9db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr536(byte data)
        {
            cbL2InputEqualization10db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr537(byte data)
        {
            cbL2InputEqualizationReserved0.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr538(byte data)
        {
            cbL2InputEqualizationReserved1.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr539(byte data)
        {
            cbL3InputEqualization0db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr540(byte data)
        {
            cbL3InputEqualization1db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr541(byte data)
        {
            cbL3InputEqualization2db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr542(byte data)
        {
            cbL3InputEqualization3db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr543(byte data)
        {
            cbL3InputEqualization4db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr544(byte data)
        {
            cbL3InputEqualization5db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr545(byte data)
        {
            cbL3InputEqualization6db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr546(byte data)
        {
            cbL3InputEqualization7db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr547(byte data)
        {
            cbL3InputEqualization8db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr548(byte data)
        {
            cbL3InputEqualization9db.SelectedIndex = data & 0x7F;
        }
   
        private void _ParseAddr549(byte data)
        {
            cbL3InputEqualization10db.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr550(byte data)
        {
            cbL3InputEqualizationReserved0.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr551(byte data)
        {
            cbL3InputEqualizationReserved1.SelectedIndex = data & 0x7F;
        }

        private void bCustomerRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[52];
            int rv;

            if (reading == true)
                return;

            reading = true;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(84, BitConverter.GetBytes((ushort)0x0500).Reverse().ToArray(), 52, data);
            if (rv != 52)
                goto exit;

            _ParseAddr500(data[0]);
            _ParseAddr501(data[1]);
            _ParseAddr502(data[2]);
            _ParseAddr503(data[3]);
            _ParseAddr504(data[4]);
            _ParseAddr505(data[5]);
            _ParseAddr506(data[6]);
            _ParseAddr507(data[7]);
            _ParseAddr508(data[8]);
            _ParseAddr509(data[9]);
            _ParseAddr510(data[10]);
            _ParseAddr511(data[11]);
            _ParseAddr512(data[12]);
            _ParseAddr513(data[13]);
            _ParseAddr514(data[14]);
            _ParseAddr515(data[15]);
            _ParseAddr516(data[16]);
            _ParseAddr517(data[17]);
            _ParseAddr518(data[18]);
            _ParseAddr519(data[19]);
            _ParseAddr520(data[20]);
            _ParseAddr521(data[21]);
            _ParseAddr522(data[22]);
            _ParseAddr523(data[23]);
            _ParseAddr524(data[24]);
            _ParseAddr525(data[25]);
            _ParseAddr526(data[26]);
            _ParseAddr527(data[27]);
            _ParseAddr528(data[28]);
            _ParseAddr529(data[29]);
            _ParseAddr530(data[30]);
            _ParseAddr531(data[31]);
            _ParseAddr532(data[32]);
            _ParseAddr533(data[33]);
            _ParseAddr534(data[34]);
            _ParseAddr535(data[35]);
            _ParseAddr536(data[36]);
            _ParseAddr537(data[37]);
            _ParseAddr538(data[38]);
            _ParseAddr539(data[39]);
            _ParseAddr540(data[40]);
            _ParseAddr541(data[41]);
            _ParseAddr542(data[42]);
            _ParseAddr543(data[43]);
            _ParseAddr544(data[44]);
            _ParseAddr545(data[45]);
            _ParseAddr546(data[46]);
            _ParseAddr547(data[47]);
            _ParseAddr548(data[48]);
            _ParseAddr549(data[49]);
            _ParseAddr550(data[50]);
            _ParseAddr551(data[51]);

            exit:
            reading = false;
        }

        private int _WriteAddr500()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization0db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0500).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr501()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization1db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0501).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr502()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization2db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0502).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr503()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization3db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0503).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr504()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization4db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0504).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr505()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization5db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0505).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr506()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization6db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0506).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr507()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization7db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0507).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr508()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization8db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0508).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr509()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization9db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0509).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr510()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization10db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr511()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualizationReserved0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr512()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualizationReserved1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr500() < 0)
                return;
        }

        private void cbL0InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr501() < 0)
                return;
        }

        private void cbL0InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr502() < 0)
                return;
        }

        private void cbL0InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr503() < 0)
                return;
        }

        private void cbL0InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr504() < 0)
                return;
        }

        private void cbL0InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr505() < 0)
                return;
        }

        private void cbL0InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr506() < 0)
                return;
        }

        private void cbL0InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr507() < 0)
                return;
        }

        private void cbL0InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr508() < 0)
                return;
        }

        private void cbL0InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr509() < 0)
                return;
        }

        private void cbL0InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr510() < 0)
                return;
        }

        private void cbL0InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr511() < 0)
                return;
        }

        private void cbL0InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr512() < 0)
                return;
        }

        private int _WriteAddr513()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization0db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr514()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization1db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr515()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization2db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x050F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr516()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization3db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0510).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr517()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization4db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0511).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr518()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization5db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0512).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr519()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization6db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0513).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr520()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization7db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0514).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr521()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization8db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0515).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr522()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization9db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0516).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr523()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualization10db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0517).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr524()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualizationReserved0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0518).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr525()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1InputEqualizationReserved1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0519).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL1InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr513() < 0)
                return;
        }

        private void cbL1InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr514() < 0)
                return;
        }

        private void cbL1InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr515() < 0)
                return;
        }

        private void cbL1InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr516() < 0)
                return;
        }

        private void cbL1InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr517() < 0)
                return;
        }

        private void cbL1InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr518() < 0)
                return;
        }

        private void cbL1InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr519() < 0)
                return;
        }

        private void cbL1InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr520() < 0)
                return;
        }

        private void cbL1InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr521() < 0)
                return;
        }

        private void cbL1InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr522() < 0)
                return;
        }

        private void cbL1InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr523() < 0)
                return;
        }

        private void cbL1InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr524() < 0)
                return;
        }

        private void cbL1InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr525() < 0)
                return;
        }

        private int _WriteAddr526()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization0db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr527()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization1db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr528()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization2db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr529()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization3db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr530()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization4db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr531()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization5db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x051F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr532()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization6db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0520).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr533()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization7db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0521).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr534()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization8db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0522).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr535()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization9db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0523).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr536()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualization10db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0524).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr537()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualizationReserved0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0525).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr538()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2InputEqualizationReserved1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0526).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL2InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr526() < 0)
                return;
        }

        private void cbL2InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr527() < 0)
                return;
        }

        private void cbL2InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr528() < 0)
                return;
        }

        private void cbL2InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr529() < 0)
                return;
        }

        private void cbL2InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr530() < 0)
                return;
        }

        private void cbL2InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr531() < 0)
                return;
        }

        private void cbL2InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr532() < 0)
                return;
        }

        private void cbL2InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr533() < 0)
                return;
        }

        private void cbL2InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr534() < 0)
                return;
        }

        private void cbL2InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr535() < 0)
                return;
        }

        private void cbL2InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr536() < 0)
                return;
        }

        private void cbL2InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr537() < 0)
                return;
        }

        private void cbL2InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr538() < 0)
                return;
        }

        private int _WriteAddr539()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization0db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0527).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr540()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization1db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0528).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr541()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization2db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0529).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr542()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization3db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr543()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization4db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr544()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization5db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr545()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization6db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr546()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization7db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr547()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization8db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x052F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr548()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization9db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0530).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr549()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualization10db.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0531).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr550()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualizationReserved0.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0532).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr551()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3InputEqualizationReserved1.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(84, BitConverter.GetBytes((ushort)0x0533).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL3InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr539() < 0)
                return;
        }

        private void cbL3InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr540() < 0)
                return;
        }

        private void cbL3InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr541() < 0)
                return;
        }

        private void cbL3InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr542() < 0)
                return;
        }

        private void cbL3InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr543() < 0)
                return;
        }

        private void cbL3InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr544() < 0)
                return;
        }

        private void cbL3InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr545() < 0)
                return;
        }

        private void cbL3InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr546() < 0)
                return;
        }

        private void cbL3InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr547() < 0)
                return;
        }

        private void cbL3InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr548() < 0)
                return;
        }

        private void cbL3InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr549() < 0)
                return;
        }

        private void cbL3InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr550() < 0)
                return;
        }

        private void cbL3InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr551() < 0)
                return;
        }
    }
}
