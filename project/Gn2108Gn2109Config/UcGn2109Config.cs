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
    public partial class UcGn2109Config : UserControl
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

        public UcGn2109Config()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 4; i++) {
                cbLockdetLockThres.Items.Add(i);
                cbPrbsgenSequenceSel.Items.Add(i);
                cbPrbsgenOutputSel.Items.Add(i);
                cbPrbschkSelPrbs.Items.Add(i);
            }

            for (i = 0; i < 8; i++) {
                cbPrbsgenVcoFreq.Items.Add(i);
                cbAdcResolution.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbL0LosHyst.Items.Add(i);
                cbL1LosHyst.Items.Add(i);
                cbL2LosHyst.Items.Add(i);
                cbL3LosHyst.Items.Add(i);
                cbL0DrvDeemphLevel.Items.Add(i);
                cbL1DrvDeemphLevel.Items.Add(i);
                cbL2DrvDeemphLevel.Items.Add(i);
                cbL3DrvDeemphLevel.Items.Add(i);
                cbPrbsgenCkdivRate.Items.Add(i);
                cbPrbschkTimerClkSel.Items.Add(i);
                cbL0OutputEmphasis0Db.Items.Add(i);
                cbL0OutputEmphasis1Db.Items.Add(i);
                cbL0OutputEmphasis2Db.Items.Add(i);
                cbL0OutputEmphasis3Db.Items.Add(i);
                cbL0OutputEmphasis4Db.Items.Add(i);
                cbL0OutputEmphasis5Db.Items.Add(i);
                cbL0OutputEmphasis6Db.Items.Add(i);
                cbL0OutputEmphasis7Db.Items.Add(i);
                cbL0OutputEmphasisReserved0.Items.Add(i);
                cbL1OutputEmphasis0Db.Items.Add(i);
                cbL1OutputEmphasis1Db.Items.Add(i);
                cbL1OutputEmphasis2Db.Items.Add(i);
                cbL1OutputEmphasis3Db.Items.Add(i);
                cbL1OutputEmphasis4Db.Items.Add(i);
                cbL1OutputEmphasis5Db.Items.Add(i);
                cbL1OutputEmphasis6Db.Items.Add(i);
                cbL1OutputEmphasis7Db.Items.Add(i);
                cbL1OutputEmphasisReserved0.Items.Add(i);
                cbL2OutputEmphasis0Db.Items.Add(i);
                cbL2OutputEmphasis1Db.Items.Add(i);
                cbL2OutputEmphasis2Db.Items.Add(i);
                cbL2OutputEmphasis3Db.Items.Add(i);
                cbL2OutputEmphasis4Db.Items.Add(i);
                cbL2OutputEmphasis5Db.Items.Add(i);
                cbL2OutputEmphasis6Db.Items.Add(i);
                cbL2OutputEmphasis7Db.Items.Add(i);
                cbL2OutputEmphasisReserved0.Items.Add(i);
                cbL3OutputEmphasis0Db.Items.Add(i);
                cbL3OutputEmphasis1Db.Items.Add(i);
                cbL3OutputEmphasis2Db.Items.Add(i);
                cbL3OutputEmphasis3Db.Items.Add(i);
                cbL3OutputEmphasis4Db.Items.Add(i);
                cbL3OutputEmphasis5Db.Items.Add(i);
                cbL3OutputEmphasis6Db.Items.Add(i);
                cbL3OutputEmphasis7Db.Items.Add(i);
                cbL3OutputEmphasisReserved0.Items.Add(i);
            }

            for (i = 0; i < 64; i++) {
                cbL0LosThres.Items.Add(i);
                cbL1LosThres.Items.Add(i);
                cbL2LosThres.Items.Add(i);
                cbL3LosThres.Items.Add(i);
                cbAllLosThres.Items.Add(i);
                cbRssiHighFaultThres.Items.Add(i);
            }

            for (i = 0; i < 128; i++) {
                cbL0CkgenMclkPhase.Items.Add(i);
                cbL1CkgenMclkPhase.Items.Add(i);
                cbL2CkgenMclkPhase.Items.Add(i);
                cbL3CkgenMclkPhase.Items.Add(i);
                cbL0DrvPredrvCmRef.Items.Add(i);
                cbL1DrvPredrvCmRef.Items.Add(i);
                cbL2DrvPredrvCmRef.Items.Add(i);
                cbL3DrvPredrvCmRef.Items.Add(i);
            }

            for (i = 0; i < 181; i++) {
                cbL0DrvMainSwing.Items.Add(i);
                cbL1DrvMainSwing.Items.Add(i);
                cbL2DrvMainSwing.Items.Add(i);
                cbL3DrvMainSwing.Items.Add(i);
                cbAllDrvMainSwing.Items.Add(i);
            }

            for (i = 0; i < 256; i++) {
                cbBbStep.Items.Add(i);
                cbL0OutputAmplitude100400.Items.Add(i);
                cbL0OutputAmplitude300600.Items.Add(i);
                cbL0OutputAmplitude400800.Items.Add(i);
                cbL0OutputAmplitude6001200.Items.Add(i);
                cbL0OutputAmplitudeReserved0.Items.Add(i);
                cbL0OutputAmplitudeReserved1.Items.Add(i);
                cbL0OutputAmplitudeReserved2.Items.Add(i);
                cbL0OutputAmplitudeReserved3.Items.Add(i);
                cbL0OutputAmplitudeReserved4.Items.Add(i);
                cbL1OutputAmplitude100400.Items.Add(i);
                cbL1OutputAmplitude300600.Items.Add(i);
                cbL1OutputAmplitude400800.Items.Add(i);
                cbL1OutputAmplitude6001200.Items.Add(i);
                cbL1OutputAmplitudeReserved0.Items.Add(i);
                cbL1OutputAmplitudeReserved1.Items.Add(i);
                cbL1OutputAmplitudeReserved2.Items.Add(i);
                cbL1OutputAmplitudeReserved3.Items.Add(i);
                cbL1OutputAmplitudeReserved4.Items.Add(i);
                cbL2OutputAmplitude100400.Items.Add(i);
                cbL2OutputAmplitude300600.Items.Add(i);
                cbL2OutputAmplitude400800.Items.Add(i);
                cbL2OutputAmplitude6001200.Items.Add(i);
                cbL2OutputAmplitudeReserved0.Items.Add(i);
                cbL2OutputAmplitudeReserved1.Items.Add(i);
                cbL2OutputAmplitudeReserved2.Items.Add(i);
                cbL2OutputAmplitudeReserved3.Items.Add(i);
                cbL2OutputAmplitudeReserved4.Items.Add(i);
                cbL3OutputAmplitude100400.Items.Add(i);
                cbL3OutputAmplitude300600.Items.Add(i);
                cbL3OutputAmplitude400800.Items.Add(i);
                cbL3OutputAmplitude6001200.Items.Add(i);
                cbL3OutputAmplitudeReserved0.Items.Add(i);
                cbL3OutputAmplitudeReserved1.Items.Add(i);
                cbL3OutputAmplitudeReserved2.Items.Add(i);
                cbL3OutputAmplitudeReserved3.Items.Add(i);
                cbL3OutputAmplitudeReserved4.Items.Add(i);
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
                cbL0TiaRateSel.Checked = false;
            else
                cbL0TiaRateSel.Checked = true;
        }

        private void _ParseAddr003(byte data)
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

        private void _ParseAddr004(byte data)
        {
            cbL0LosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr005(byte data)
        {
            cbL0LosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr009(byte data)
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

        private void _ParseAddr00B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0EqLoopbackEn.Checked = false;
            else
                cbL0EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr017(byte data)
        {
            cbL0CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr018(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0DrvForceMute.Checked = false;
            else
                cbL0DrvForceMute.Checked = true;
        }

        private void _ParseAddr019(byte data)
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

        private void _ParseAddr01A(byte data)
        {
            cbL0DrvMainSwing.SelectedIndex = data;
        }

        private void _ParseAddr01B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0DrvDeemphPrecSel.Checked = false;
            else
                cbL0DrvDeemphPrecSel.Checked = true;

            cbL0DrvDeemphLevel.SelectedIndex = (data & 0x1E) >> 1;
        }

        private void _ParseAddr01D(byte data)
        {
            cbL0DrvPredrvCmRef.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr020(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0RssiHighFaultDetectEn.Checked = false;
            else
                cbL0RssiHighFaultDetectEn.Checked = true;
        }

        private void _ParseAddr022(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0RssiHighAlarm.Checked = false;
            else
                cbL0RssiHighAlarm.Checked = true;
        }

        private void _ParseAddr023_024(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL0Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr025(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdLane.Checked = false;
            else
                cbL0PdLane.Checked = true;
        }

        private void _ParseAddr026(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdLos.Checked = false;
            else
                cbL0PdLos.Checked = true;
        }

        private void _ParseAddr029(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdCdr.Checked = false;
            else
                cbL0PdCdr.Checked = true;

            if ((data & 0x04) == 0)
                cbL0PdPPdrtMclk.Checked = false;
            else
                cbL0PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr02A(byte data)
        {
            if ((data & 0x40) == 0)
                cbL0PdCkgenMclk.Checked = false;
            else
                cbL0PdCkgenMclk.Checked = true;
        }

        private void _ParseAddr02B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0PdDrv.Checked = false;
            else
                cbL0PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL0PdDrvDeemphDrv.Checked = false;
            else
                cbL0PdDrvDeemphDrv.Checked = true;

            if ((data & 0x10) == 0)
                cbL0PdPrbClk.Checked = false;
            else
                cbL0PdPrbClk.Checked = true;

            if ((data & 0x20) == 0)
                cbL0PdDrvPrbschk.Checked = false;
            else
                cbL0PdDrvPrbschk.Checked = true;
        }

        private void bL0Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[5];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL0Read.Enabled = false;
            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr000(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0003).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr003(data[0]);
            _ParseAddr004(data[1]);
            _ParseAddr005(data[2]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0009).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr009(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x000B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr00B(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0017).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr017(data[0]);
            _ParseAddr018(data[1]);
            _ParseAddr019(data[2]);
            _ParseAddr01A(data[3]);
            _ParseAddr01B(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x001D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr01D(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0020).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr020(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0022).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr022(data[0]);
            _ParseAddr023_024(data[1], data[2]);
            _ParseAddr025(data[3]);
            _ParseAddr026(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0029).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr029(data[0]);
            _ParseAddr02A(data[1]);
            _ParseAddr02B(data[2]);

        exit:
            reading = false;
            bL0Read.Enabled = true;
        }

        private void _ParseAddr100(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1TiaRateSel.Checked = false;
            else
                cbL1TiaRateSel.Checked = true;
        }

        private void _ParseAddr103(byte data)
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

        private void _ParseAddr104(byte data)
        {
            cbL1LosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr105(byte data)
        {
            cbL1LosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr109(byte data)
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

        private void _ParseAddr10B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1EqLoopbackEn.Checked = false;
            else
                cbL1EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr117(byte data)
        {
            cbL1CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr118(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1DrvForceMute.Checked = false;
            else
                cbL1DrvForceMute.Checked = true;
        }

        private void _ParseAddr119(byte data)
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

        private void _ParseAddr11A(byte data)
        {
            cbL1DrvMainSwing.SelectedIndex = data;
        }

        private void _ParseAddr11B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1DrvDeemphPrecSel.Checked = false;
            else
                cbL1DrvDeemphPrecSel.Checked = true;

            cbL1DrvDeemphLevel.SelectedIndex = (data & 0x1E) >> 1;
        }

        private void _ParseAddr11D(byte data)
        {
            cbL1DrvPredrvCmRef.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr120(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1RssiHighFaultDetectEn.Checked = false;
            else
                cbL1RssiHighFaultDetectEn.Checked = true;
        }

        private void _ParseAddr122(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1RssiHighAlarm.Checked = false;
            else
                cbL1RssiHighAlarm.Checked = true;
        }

        private void _ParseAddr123_124(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL1Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr125(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdLane.Checked = false;
            else
                cbL1PdLane.Checked = true;
        }

        private void _ParseAddr126(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdLos.Checked = false;
            else
                cbL1PdLos.Checked = true;
        }

        private void _ParseAddr129(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdCdr.Checked = false;
            else
                cbL1PdCdr.Checked = true;

            if ((data & 0x04) == 0)
                cbL1PdPPdrtMclk.Checked = false;
            else
                cbL1PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr12A(byte data)
        {
            if ((data & 0x40) == 0)
                cbL1PdCkgenMclk.Checked = false;
            else
                cbL1PdCkgenMclk.Checked = true;
        }

        private void _ParseAddr12B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL1PdDrv.Checked = false;
            else
                cbL1PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL1PdDrvDeemphDrv.Checked = false;
            else
                cbL1PdDrvDeemphDrv.Checked = true;

            if ((data & 0x10) == 0)
                cbL1PdPrbClk.Checked = false;
            else
                cbL1PdPrbClk.Checked = true;

            if ((data & 0x20) == 0)
                cbL1PdDrvPrbschk.Checked = false;
            else
                cbL1PdDrvPrbschk.Checked = true;
        }

        private void bL1Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[5];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL1Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr100(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0103).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr103(data[0]);
            _ParseAddr104(data[1]);
            _ParseAddr105(data[2]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0109).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr109(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x010B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr10B(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr117(data[0]);
            _ParseAddr118(data[1]);
            _ParseAddr119(data[2]);
            _ParseAddr11A(data[3]);
            _ParseAddr11B(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x011D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr11D(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0120).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr120(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0122).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr122(data[0]);
            _ParseAddr123_124(data[1], data[2]);
            _ParseAddr125(data[3]);
            _ParseAddr126(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0129).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr129(data[0]);
            _ParseAddr12A(data[1]);
            _ParseAddr12B(data[2]);

        exit:
            reading = false;
            bL1Read.Enabled = true;
        }

        private void _ParseAddr200(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2TiaRateSel.Checked = false;
            else
                cbL2TiaRateSel.Checked = true;
        }

        private void _ParseAddr203(byte data)
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

        private void _ParseAddr204(byte data)
        {
            cbL2LosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr205(byte data)
        {
            cbL2LosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr209(byte data)
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

        private void _ParseAddr20B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2EqLoopbackEn.Checked = false;
            else
                cbL2EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr217(byte data)
        {
            cbL2CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr218(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2DrvForceMute.Checked = false;
            else
                cbL2DrvForceMute.Checked = true;
        }

        private void _ParseAddr219(byte data)
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

        private void _ParseAddr21A(byte data)
        {
            cbL2DrvMainSwing.SelectedIndex = data;
        }

        private void _ParseAddr21B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2DrvDeemphPrecSel.Checked = false;
            else
                cbL2DrvDeemphPrecSel.Checked = true;

            cbL2DrvDeemphLevel.SelectedIndex = (data & 0x1E) >> 1;
        }

        private void _ParseAddr21D(byte data)
        {
            cbL2DrvPredrvCmRef.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr220(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2RssiHighFaultDetectEn.Checked = false;
            else
                cbL2RssiHighFaultDetectEn.Checked = true;
        }

        private void _ParseAddr222(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2RssiHighAlarm.Checked = false;
            else
                cbL2RssiHighAlarm.Checked = true;
        }

        private void _ParseAddr223_224(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL2Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr225(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdLane.Checked = false;
            else
                cbL2PdLane.Checked = true;
        }

        private void _ParseAddr226(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdLos.Checked = false;
            else
                cbL2PdLos.Checked = true;
        }

        private void _ParseAddr229(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdCdr.Checked = false;
            else
                cbL2PdCdr.Checked = true;

            if ((data & 0x04) == 0)
                cbL2PdPPdrtMclk.Checked = false;
            else
                cbL2PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr22A(byte data)
        {
            if ((data & 0x40) == 0)
                cbL2PdCkgenMclk.Checked = false;
            else
                cbL2PdCkgenMclk.Checked = true;
        }

        private void _ParseAddr22B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL2PdDrv.Checked = false;
            else
                cbL2PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL2PdDrvDeemphDrv.Checked = false;
            else
                cbL2PdDrvDeemphDrv.Checked = true;

            if ((data & 0x10) == 0)
                cbL2PdPrbClk.Checked = false;
            else
                cbL2PdPrbClk.Checked = true;

            if ((data & 0x20) == 0)
                cbL2PdDrvPrbschk.Checked = false;
            else
                cbL2PdDrvPrbschk.Checked = true;
        }

        private void bL2Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[5];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL2Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0200).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr200(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0203).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr203(data[0]);
            _ParseAddr204(data[1]);
            _ParseAddr205(data[2]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0209).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr209(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x020B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr20B(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0217).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr217(data[0]);
            _ParseAddr218(data[1]);
            _ParseAddr219(data[2]);
            _ParseAddr21A(data[3]);
            _ParseAddr21B(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x021D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr21D(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0220).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr220(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0222).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr222(data[0]);
            _ParseAddr223_224(data[1], data[2]);
            _ParseAddr225(data[3]);
            _ParseAddr226(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0229).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr229(data[0]);
            _ParseAddr22A(data[1]);
            _ParseAddr22B(data[2]);

        exit:
            reading = false;
            bL2Read.Enabled = true;
        }

        private void _ParseAddr300(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3TiaRateSel.Checked = false;
            else
                cbL3TiaRateSel.Checked = true;
        }

        private void _ParseAddr303(byte data)
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

        private void _ParseAddr304(byte data)
        {
            cbL3LosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr305(byte data)
        {
            cbL3LosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr309(byte data)
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

        private void _ParseAddr30B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3EqLoopbackEn.Checked = false;
            else
                cbL3EqLoopbackEn.Checked = true;
        }

        private void _ParseAddr317(byte data)
        {
            cbL3CkgenMclkPhase.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr318(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3DrvForceMute.Checked = false;
            else
                cbL3DrvForceMute.Checked = true;
        }

        private void _ParseAddr319(byte data)
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

        private void _ParseAddr31A(byte data)
        {
            cbL3DrvMainSwing.SelectedIndex = data;
        }

        private void _ParseAddr31B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3DrvDeemphPrecSel.Checked = false;
            else
                cbL3DrvDeemphPrecSel.Checked = true;

            cbL3DrvDeemphLevel.SelectedIndex = (data & 0x1E) >> 1;
        }

        private void _ParseAddr31D(byte data)
        {
            cbL3DrvPredrvCmRef.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr320(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3RssiHighFaultDetectEn.Checked = false;
            else
                cbL3RssiHighFaultDetectEn.Checked = true;
        }

        private void _ParseAddr322(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3RssiHighAlarm.Checked = false;
            else
                cbL3RssiHighAlarm.Checked = true;
        }

        private void _ParseAddr323_324(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbL3Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr325(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdLane.Checked = false;
            else
                cbL3PdLane.Checked = true;
        }

        private void _ParseAddr326(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdLos.Checked = false;
            else
                cbL3PdLos.Checked = true;
        }

        private void _ParseAddr329(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdCdr.Checked = false;
            else
                cbL3PdCdr.Checked = true;

            if ((data & 0x04) == 0)
                cbL3PdPPdrtMclk.Checked = false;
            else
                cbL3PdPPdrtMclk.Checked = true;
        }

        private void _ParseAddr32A(byte data)
        {
            if ((data & 0x40) == 0)
                cbL3PdCkgenMclk.Checked = false;
            else
                cbL3PdCkgenMclk.Checked = true;
        }

        private void _ParseAddr32B(byte data)
        {
            if ((data & 0x01) == 0)
                cbL3PdDrv.Checked = false;
            else
                cbL3PdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbL3PdDrvDeemphDrv.Checked = false;
            else
                cbL3PdDrvDeemphDrv.Checked = true;

            if ((data & 0x10) == 0)
                cbL3PdPrbClk.Checked = false;
            else
                cbL3PdPrbClk.Checked = true;

            if ((data & 0x20) == 0)
                cbL3PdDrvPrbschk.Checked = false;
            else
                cbL3PdDrvPrbschk.Checked = true;
        }

        private void bL3Read_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[5];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bL3Read.Enabled = false;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0300).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr300(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0303).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr303(data[0]);
            _ParseAddr304(data[1]);
            _ParseAddr305(data[2]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0309).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr309(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x030B).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr30B(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0317).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr317(data[0]);
            _ParseAddr318(data[1]);
            _ParseAddr319(data[2]);
            _ParseAddr31A(data[3]);
            _ParseAddr31B(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x031D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr31D(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0320).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr320(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0322).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr322(data[0]);
            _ParseAddr323_324(data[1], data[2]);
            _ParseAddr325(data[3]);
            _ParseAddr326(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0329).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr329(data[0]);
            _ParseAddr32A(data[1]);
            _ParseAddr32B(data[2]);

        exit:
            reading = false;
            bL3Read.Enabled = true;
        }

        private void _ParseAddr400(byte data)
        {
            if ((data & 0x01) == 0)
                cbTiaRssiDiv.Checked = false;
            else
                cbTiaRssiDiv.Checked = true;
        }

        private void _ParseAddr406(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0TiaToRssi1.Checked = false;
            else
                cbL0TiaToRssi1.Checked = true;

            if ((data & 0x02) == 0)
                cbL1TiaToRssi1.Checked = false;
            else
                cbL1TiaToRssi1.Checked = true;

            if ((data & 0x04) == 0)
                cbL2TiaToRssi1.Checked = false;
            else
                cbL2TiaToRssi1.Checked = true;

            if ((data & 0x08) == 0)
                cbL3TiaToRssi1.Checked = false;
            else
                cbL3TiaToRssi1.Checked = true;

            if ((data & 0x10) == 0)
                cbL0TiaToRssi2.Checked = false;
            else
                cbL0TiaToRssi2.Checked = true;

            if ((data & 0x20) == 0)
                cbL1TiaToRssi2.Checked = false;
            else
                cbL1TiaToRssi2.Checked = true;

            if ((data & 0x40) == 0)
                cbL2TiaToRssi2.Checked = false;
            else
                cbL2TiaToRssi2.Checked = true;

            if ((data & 0x80) == 0)
                cbL3TiaToRssi2.Checked = false;
            else
                cbL3TiaToRssi2.Checked = true;
        }

        private void _ParseAddr407(byte data)
        {
            if ((data & 0x10) == 0)
                cbLoslSoftReset.Checked = false;
            else
                cbLoslSoftReset.Checked = true;
        }

        private void _ParseAddr408(byte data)
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

        private void _ParseAddr40A(byte data)
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

        private void _ParseAddr40C(byte data)
        {
            cbLockdetLockThres.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr410(byte data)
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

        private void _ParseAddr412(byte data)
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

        private void _ParseAddr414(byte data)
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

        private void _ParseAddr415(byte data)
        {
            if ((data & 0x10) == 0)
                cbLoslInvert.Checked = false;
            else
                cbLoslInvert.Checked = true;
        }

        private void _ParseAddr416(byte data)
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

        private void _ParseAddr417(byte data)
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

        private void _ParseAddr427(byte data)
        {
            cbRssiHighFaultThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr42A(byte data)
        {
            if ((data & 0x10) == 0)
                cbRssiHighAlarm.Checked = false;
            else
                cbRssiHighAlarm.Checked = true;
        }

        private void _ParseAddr43B(byte data)
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

        private void _ParseAddr43C(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbsgenStart.Checked = false;
            else
                cbPrbsgenStart.Checked = true;
        }

        private void _ParseAddr43D(byte data)
        {
            cbPrbsgenSequenceSel.SelectedIndex = data & 0x03;
            cbPrbsgenOutputSel.SelectedIndex = (data & 0x0C) >> 2;
            cbPrbsgenCkdivRate.SelectedIndex = (data & 0xF0) >> 4;
        }

        private void _ParseAddr43E(byte data)
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

        private void _ParseAddr443(byte data)
        {
            cbPrbsgenVcoFreq.SelectedIndex = data & 0x07;
        }

        private void _ParseAddr445(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkInvPrbs.Checked = false;
            else
                cbPrbschkInvPrbs.Checked = true;

            cbPrbschkSelPrbs.SelectedIndex = (data & 0x06) >> 1;
        }

        private void _ParseAddr449(byte data)
        {
            cbPrbschkTimerClkSel.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr44A_44D(byte data0, byte data1, byte data2, byte data3)
        {
            long tmp;

            tmp = ((((data3 * 256 + data2) * 256) + data1) * 256) + data0;
            tbPrbschkTimeout.Text = tmp.ToString();
        }

        private void _ParseAddr44E(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkTimerEnable.Checked = false;
            else
                cbPrbschkTimerEnable.Checked = true;
        }

        private void _ParseAddr44F(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkEnable.Checked = false;
            else
                cbPrbschkEnable.Checked = true;
        }

        private void _ParseAddr450(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbschkDone.Checked = false;
            else
                cbPrbschkDone.Checked = true;
        }

        private void _ParseAddr451_454(byte data0, byte data1, byte data2, byte data3)
        {
            long tmp;

            tmp = ((((data3 * 256 + data2) * 256) + data1) * 256) + data0;
            tbPrbschkTime.Text = tmp.ToString();
        }

        private void _ParseAddr455_459(byte data0, byte data1, byte data2, byte data3, byte data4)
        {
            long tmp;

            tmp = ((((((data4 * 256 + data3) * 256) + data2) * 256) + data1) * 256) + data0;
            tbPrbschkErrCount.Text = tmp.ToString();
        }

        private void _ParseAddr45A(byte data)
        {
            tbTempSensorTrim.Text = (data & 0x7F).ToString();
        }

        private void _ParseAddr45B_45C(byte data0, byte data1)
        {
            int tmp;

            tmp = (data1 & 0x03) * 256 + data0;
            tbTemperature.Text = tmp.ToString();
        }

        private void _ParseAddr45D(byte data)
        {
            tbVcclSupply.Text = data.ToString();
        }

        private void _ParseAddr45E_45F(byte data0, byte data1)
        {
            int tmp;

            tmp = (data1 & 0x03) * 256 + data0;
            tbVccSupply.Text = tmp.ToString();
        }

        private void _ParseAddr461(byte data)
        {
            tbAdcCmCal.Text = (data & 0x1F).ToString();
        }

        private void _ParseAddr463(byte data)
        {
            cbAdcSrcSel.SelectedIndex = cbAdcSrcSel.FindStringExact((data & 0x1F).ToString());
        }

        private void _ParseAddr466(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcParamMonCtrlReset.Checked = false;
            else
                cbAdcParamMonCtrlReset.Checked = true;
        }

        private void _ParseAddr469(byte data)
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
        }

        private void _ParseAddr46A(byte data)
        {
            if ((data & 0x01) == 0)
                cbL0RssiMonEnable.Checked = false;
            else
                cbL0RssiMonEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbL1RssiMonEnable.Checked = false;
            else
                cbL1RssiMonEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbL2RssiMonEnable.Checked = false;
            else
                cbL2RssiMonEnable.Checked = true;

            if ((data & 0x08) == 0)
                cbL3RssiMonEnable.Checked = false;
            else
                cbL3RssiMonEnable.Checked = true;
        }

        private void _ParseAddr46D(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcHostCtrlReq.Checked = false;
            else
                cbAdcHostCtrlReq.Checked = true;
        }

        private void _ParseAddr46E(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcHostCtrlGnt.Checked = false;
            else
                cbAdcHostCtrlGnt.Checked = true;
        }

        private void _ParseAddr46F(byte data)
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

        private void _ParseAddr470_471(byte data0, byte data1)
        {
            int tmp;

            tmp = data1 * 256 + data0;
            tbAdcOffset.Text = tmp.ToString();
        }

        private void _ParseAddr472(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcStartConv.Checked = false;
            else
                cbAdcStartConv.Checked = true;
        }

        private void _ParseAddr473(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcDone.Checked = false;
            else
                cbAdcDone.Checked = true;
        }

        private void _ParseAddr474_475(byte data0, byte data1)
        {
            int tmp;

            tmp = data1 * 256 + data0;
            tbAdcOut.Text = tmp.ToString();
        }

        private void _ParseAddr47F(byte data)
        {
            cbBbStep.SelectedIndex = data;
        }

        private void _ParseAddr483(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdAll.Checked = false;
            else
                cbPdAll.Checked = true;
        }

        private void _ParseAddr484(byte data)
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

        private void _ParseAddr485(byte data)
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

        private void _ParseAddr486(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdPrbschk.Checked = false;
            else
                cbPdPrbschk.Checked = true;
        }

        private void _ParseAddr487(byte data)
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

        private void _ParseAddr48A(byte data)
        {
            tbVersion.Text = data.ToString();
        }

        private void bReadControl_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[23];
            int rv;

            if (reading == true)
                return;

            reading = true;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0400).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr400(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0406).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr406(data[0]);
            _ParseAddr407(data[1]);
            _ParseAddr408(data[2]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x040A).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr40A(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x040C).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr40C(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0410).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr410(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0414).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr414(data[0]);
            _ParseAddr415(data[1]);
            _ParseAddr416(data[2]);
            _ParseAddr417(data[3]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0427).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr427(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x043B).Reverse().ToArray(), 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr43B(data[0]);
            _ParseAddr43C(data[1]);
            _ParseAddr43D(data[2]);
            _ParseAddr43E(data[3]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0443).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr443(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0445).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr445(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0449).Reverse().ToArray(), 23, data);
            if (rv != 23)
                goto exit;

            _ParseAddr449(data[0]);
            _ParseAddr44A_44D(data[1], data[2], data[3], data[4]);
            _ParseAddr44E(data[5]);
            _ParseAddr44F(data[6]);
            _ParseAddr450(data[7]);
            _ParseAddr451_454(data[8], data[9], data[10], data[11]);
            _ParseAddr455_459(data[12], data[13], data[14], data[15], data[16]);
            _ParseAddr45A(data[17]);
            _ParseAddr45B_45C(data[18], data[19]);
            _ParseAddr45D(data[20]);
            _ParseAddr45E_45F(data[21], data[22]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0461).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr461(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0463).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr463(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0466).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr466(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0469).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr469(data[0]);
            _ParseAddr46A(data[1]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x046D).Reverse().ToArray(), 9, data);
            if (rv != 9)
                goto exit;

            _ParseAddr46D(data[0]);
            _ParseAddr46E(data[1]);
            _ParseAddr46F(data[2]);
            _ParseAddr470_471(data[3], data[4]);
            _ParseAddr472(data[5]);
            _ParseAddr473(data[6]);
            _ParseAddr474_475(data[7], data[8]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x047F).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr47F(data[0]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0483).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr483(data[0]);
            _ParseAddr484(data[1]);
            _ParseAddr485(data[2]);
            _ParseAddr486(data[3]);
            _ParseAddr487(data[4]);

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x048A).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr48A(data[0]);

        exit:
            reading = false;
        }

        public int WriteAllApi()
        {
            if (_WriteAddr000() < 0) return -1;
            if (_WriteAddr003() < 0) return -1;
            if (_WriteAddr004() < 0) return -1;
            if (_WriteAddr005() < 0) return -1;
            if (_WriteAddr009() < 0) return -1;
            if (_WriteAddr00B() < 0) return -1;
            if (_WriteAddr017() < 0) return -1;
            if (_WriteAddr018() < 0) return -1;
            if (_WriteAddr019() < 0) return -1;
            if (_WriteAddr01A() < 0) return -1;
            if (_WriteAddr01B() < 0) return -1;
            if (_WriteAddr01D() < 0) return -1;
            if (_WriteAddr020() < 0) return -1;
            if (_WriteAddr022() < 0) return -1;
            if (_WriteAddr025() < 0) return -1;
            if (_WriteAddr026() < 0) return -1;
            if (_WriteAddr029() < 0) return -1;
            if (_WriteAddr02A() < 0) return -1;
            if (_WriteAddr02B() < 0) return -1;
            if (_WriteAddr100() < 0) return -1;
            if (_WriteAddr103() < 0) return -1;
            if (_WriteAddr104() < 0) return -1;
            if (_WriteAddr105() < 0) return -1;
            if (_WriteAddr109() < 0) return -1;
            if (_WriteAddr10B() < 0) return -1;
            if (_WriteAddr117() < 0) return -1;
            if (_WriteAddr118() < 0) return -1;
            if (_WriteAddr119() < 0) return -1;
            if (_WriteAddr11A() < 0) return -1;
            if (_WriteAddr11B() < 0) return -1;
            if (_WriteAddr11D() < 0) return -1;
            if (_WriteAddr120() < 0) return -1;
            if (_WriteAddr122() < 0) return -1;
            if (_WriteAddr125() < 0) return -1;
            if (_WriteAddr126() < 0) return -1;
            if (_WriteAddr129() < 0) return -1;
            if (_WriteAddr12A() < 0) return -1;
            if (_WriteAddr12B() < 0) return -1;
            if (_WriteAddr200() < 0) return -1;
            if (_WriteAddr203() < 0) return -1;
            if (_WriteAddr204() < 0) return -1;
            if (_WriteAddr205() < 0) return -1;
            if (_WriteAddr209() < 0) return -1;
            if (_WriteAddr20B() < 0) return -1;
            if (_WriteAddr217() < 0) return -1;
            if (_WriteAddr218() < 0) return -1;
            if (_WriteAddr219() < 0) return -1;
            if (_WriteAddr21A() < 0) return -1;
            if (_WriteAddr21B() < 0) return -1;
            if (_WriteAddr21D() < 0) return -1;
            if (_WriteAddr220() < 0) return -1;
            if (_WriteAddr222() < 0) return -1;
            if (_WriteAddr225() < 0) return -1;
            if (_WriteAddr226() < 0) return -1;
            if (_WriteAddr229() < 0) return -1;
            if (_WriteAddr22A() < 0) return -1;
            if (_WriteAddr22B() < 0) return -1;
            if (_WriteAddr300() < 0) return -1;
            if (_WriteAddr303() < 0) return -1;
            if (_WriteAddr304() < 0) return -1;
            if (_WriteAddr305() < 0) return -1;
            if (_WriteAddr309() < 0) return -1;
            if (_WriteAddr30B() < 0) return -1;
            if (_WriteAddr317() < 0) return -1;
            if (_WriteAddr318() < 0) return -1;
            if (_WriteAddr319() < 0) return -1;
            if (_WriteAddr31A() < 0) return -1;
            if (_WriteAddr31B() < 0) return -1;
            if (_WriteAddr31D() < 0) return -1;
            if (_WriteAddr320() < 0) return -1;
            if (_WriteAddr322() < 0) return -1;
            if (_WriteAddr325() < 0) return -1;
            if (_WriteAddr326() < 0) return -1;
            if (_WriteAddr329() < 0) return -1;
            if (_WriteAddr32A() < 0) return -1;
            if (_WriteAddr32B() < 0) return -1;
            if (_WriteAddr400() < 0) return -1;
            if (_WriteAddr406() < 0) return -1;
            if (_WriteAddr407() < 0) return -1;
            if (_WriteAddr408() < 0) return -1;
            if (_WriteAddr40A() < 0) return -1;
            if (_WriteAddr40C() < 0) return -1;
            if (_WriteAddr410() < 0) return -1;
            if (_WriteAddr412() < 0) return -1;
            if (_WriteAddr414() < 0) return -1;
            if (_WriteAddr415() < 0) return -1;
            if (_WriteAddr416() < 0) return -1;
            if (_WriteAddr417() < 0) return -1;
            if (_WriteAddr427() < 0) return -1;
            if (_WriteAddr43B() < 0) return -1;
            if (_WriteAddr43C() < 0) return -1;
            if (_WriteAddr43D() < 0) return -1;
            if (_WriteAddr43E() < 0) return -1;
            if (_WriteAddr443() < 0) return -1;
            if (_WriteAddr445() < 0) return -1;
            if (_WriteAddr449() < 0) return -1;
            if (_WriteAddr44A_44D() < 0) return -1;
            if (_WriteAddr44E() < 0) return -1;
            if (_WriteAddr44F() < 0) return -1;
            if (_WriteAddr463() < 0) return -1;
            if (_WriteAddr466() < 0) return -1;
            if (_WriteAddr469() < 0) return -1;
            if (_WriteAddr46A() < 0) return -1;
            if (_WriteAddr46D() < 0) return -1;
            if (_WriteAddr46F() < 0) return -1;
            if (_WriteAddr470_471() < 0) return -1;
            if (_WriteAddr472() < 0) return -1;
            if (_WriteAddr47F() < 0) return -1;
            if (_WriteAddr483() < 0) return -1;
            if (_WriteAddr484() < 0) return -1;
            if (_WriteAddr485() < 0) return -1;
            if (_WriteAddr486() < 0) return -1;
            if (_WriteAddr487() < 0) return -1;
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
            if (_WriteAddr552() < 0) return -1;
            if (_WriteAddr553() < 0) return -1;
            if (_WriteAddr554() < 0) return -1;
            if (_WriteAddr555() < 0) return -1;
            if (_WriteAddr556() < 0) return -1;
            if (_WriteAddr557() < 0) return -1;
            if (_WriteAddr558() < 0) return -1;
            if (_WriteAddr559() < 0) return -1;
            if (_WriteAddr560() < 0) return -1;
            if (_WriteAddr561() < 0) return -1;
            if (_WriteAddr562() < 0) return -1;
            if (_WriteAddr563() < 0) return -1;
            if (_WriteAddr564() < 0) return -1;
            if (_WriteAddr565() < 0) return -1;
            if (_WriteAddr566() < 0) return -1;
            if (_WriteAddr567() < 0) return -1;
            if (_WriteAddr568() < 0) return -1;
            if (_WriteAddr569() < 0) return -1;
            if (_WriteAddr570() < 0) return -1;
            if (_WriteAddr571() < 0) return -1;


            return 0;
        }

        private int _WriteAddr000()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0TiaRateSel.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0000).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr003()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0003).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr004()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0004).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr005()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0005).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr009()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL0EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0009).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr00B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x000B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr017()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0017).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr018()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0018).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr019()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL0DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0019).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0DrvMainSwing.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x001A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbL0DrvDeemphPrecSel.Checked == true)
                data[0] |= 0x01;

            bTmp = Convert.ToByte(cbL0DrvDeemphLevel.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x001B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0DrvPredrvCmRef.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x001D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr020()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0RssiHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0020).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr022()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0RssiHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0022).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr025()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0025).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr026()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0026).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr029()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL0PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0029).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr02A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdCkgenMclk.Checked == true)
                data[0] |= 0x40;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x002A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr02B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL0PdDrvDeemphDrv.Checked == true)
                data[0] |= 0x02;

            if (cbL0PdPrbClk.Checked == true)
                data[0] |= 0x10;

            if (cbL0PdDrvPrbschk.Checked == true)
                data[0] |= 0x20;
            
            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x002B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0TiaRateSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr000() < 0)
                return;
        }

        private void cbL0CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbL0CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbL0CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbL0LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr004() < 0)
                return;
        }

        private void cbL0LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr005() < 0)
                return;
        }

        private void cbL0OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr009() < 0)
                return;
        }

        private void cbL0EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr009() < 0)
                return;
        }

        private void cbL0EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr00B() < 0)
                return;
        }

        private void cbL0CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr017() < 0)
                return;
        }

        private void cbL0DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr018() < 0)
                return;
        }

        private void cbL0DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr019() < 0)
                return;
        }

        private void cbL0DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr019() < 0)
                return;
        }

        private void cbL0DrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01A() < 0)
                return;
        }

        private void cbL0DrvDeemphPrecSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01B() < 0)
                return;
        }

        private void cbL0DrvDeemphLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01B() < 0)
                return;
        }

        private void cbL0DrvPredrvCmRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01D() < 0)
                return;
        }

        private void cbL0RssiHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr020() < 0)
                return;
        }

        private void cbL0RssiHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr022() < 0)
                return;
        }

        private void cbL0PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr025() < 0)
                return;
        }

        private void cbL0PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr026() < 0)
                return;
        }

        private void cbL0PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr029() < 0)
                return;
        }

        private void cbL0PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr029() < 0)
                return;
        }

        private void cbL0PdCkgenMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02A() < 0)
                return;
        }

        private void cbL0PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02B() < 0)
                return;
        }

        private void cbL0PdDrvDeemphDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02B() < 0)
                return;
        }

        private void cbL0PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02B() < 0)
                return;
        }

        private void cbL0PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02B() < 0)
                return;
        }

        private int _WriteAddr100()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1TiaRateSel.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr103()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0103).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr104()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0104).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr105()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0105).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr109()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL1EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0109).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr10B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x010B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr117()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr118()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0118).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr119()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL1DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0119).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1DrvMainSwing.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x011A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbL1DrvDeemphPrecSel.Checked == true)
                data[0] |= 0x01;

            bTmp = Convert.ToByte(cbL1DrvDeemphLevel.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x011B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1DrvPredrvCmRef.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x011D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr120()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1RssiHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0120).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr122()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1RssiHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0122).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr125()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0125).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr126()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0126).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr129()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL1PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0129).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdCkgenMclk.Checked == true)
                data[0] |= 0x40;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x012A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL1PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL1PdDrvDeemphDrv.Checked == true)
                data[0] |= 0x02;

            if (cbL1PdPrbClk.Checked == true)
                data[0] |= 0x10;

            if (cbL1PdDrvPrbschk.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x012B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL1TiaRateSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private void cbL1CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr103() < 0)
                return;
        }

        private void cbL1CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr103() < 0)
                return;
        }

        private void cbL1CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr103() < 0)
                return;
        }

        private void cbL1LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr104() < 0)
                return;
        }

        private void cbL1LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr105() < 0)
                return;
        }

        private void cbL1OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbL1EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbL1EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10B() < 0)
                return;
        }

        private void cbL1CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr117() < 0)
                return;
        }

        private void cbL1DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr118() < 0)
                return;
        }

        private void cbL1DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr119() < 0)
                return;
        }

        private void cbL1DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr119() < 0)
                return;
        }

        private void cbL1DrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11A() < 0)
                return;
        }

        private void cbL1DrvDeemphPrecSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11B() < 0)
                return;
        }

        private void cbL1DrvDeemphLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11B() < 0)
                return;
        }

        private void cbL1DrvPredrvCmRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11D() < 0)
                return;
        }

        private void cbL1RssiHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr120() < 0)
                return;
        }

        private void cbL1RssiHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr122() < 0)
                return;
        }

        private void cbL1PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr125() < 0)
                return;
        }

        private void cbL1PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr126() < 0)
                return;
        }

        private void cbL1PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void cbL1PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void cbL1PdCkgenMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12A() < 0)
                return;
        }

        private void cbL1PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12B() < 0)
                return;
        }

        private void cbL1PdDrvDeemphDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12B() < 0)
                return;
        }

        private void cbL1PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12B() < 0)
                return;
        }

        private void cbL1PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12B() < 0)
                return;
        }

        private int _WriteAddr200()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2TiaRateSel.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0200).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr203()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0203).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr204()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0204).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr205()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0205).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr209()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL2EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0209).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr20B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x020B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr217()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0217).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr218()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0218).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr219()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL2DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0219).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr21A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2DrvMainSwing.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x021A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr21B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbL2DrvDeemphPrecSel.Checked == true)
                data[0] |= 0x01;

            bTmp = Convert.ToByte(cbL2DrvDeemphLevel.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x021B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr21D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2DrvPredrvCmRef.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x021D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr220()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2RssiHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0220).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr222()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2RssiHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0222).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr225()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0225).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr226()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0226).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr229()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL2PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0229).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdCkgenMclk.Checked == true)
                data[0] |= 0x40;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x022A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL2PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL2PdDrvDeemphDrv.Checked == true)
                data[0] |= 0x02;

            if (cbL2PdPrbClk.Checked == true)
                data[0] |= 0x10;

            if (cbL2PdDrvPrbschk.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x022B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL2TiaRateSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr200() < 0)
                return;
        }

        private void cbL2CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr203() < 0)
                return;
        }

        private void cbL2CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr203() < 0)
                return;
        }

        private void cbL2CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr203() < 0)
                return;
        }

        private void cbL2LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr204() < 0)
                return;
        }

        private void cbL2LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr205() < 0)
                return;
        }

        private void cbL2OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr209() < 0)
                return;
        }

        private void cbL2EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr209() < 0)
                return;
        }

        private void cbL2EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20B() < 0)
                return;
        }

        private void cbL2CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr217() < 0)
                return;
        }

        private void cbL2DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr218() < 0)
                return;
        }

        private void cbL2DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr219() < 0)
                return;
        }

        private void cbL2DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr219() < 0)
                return;
        }

        private void cbL2DrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21A() < 0)
                return;
        }

        private void cbL2DrvDeemphPrecSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21B() < 0)
                return;
        }

        private void cbL2DrvDeemphLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21B() < 0)
                return;
        }

        private void cbL2DrvPredrvCmRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21D() < 0)
                return;
        }

        private void cbL2RssiHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr220() < 0)
                return;
        }

        private void cbL2RssiHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr222() < 0)
                return;
        }

        private void cbL2PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr225() < 0)
                return;
        }

        private void cbL2PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr226() < 0)
                return;
        }

        private void cbL2PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr229() < 0)
                return;
        }

        private void cbL2PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr229() < 0)
                return;
        }

        private void cbL2PdCkgenMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22A() < 0)
                return;
        }

        private void cbL2PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22B() < 0)
                return;
        }

        private void cbL2PdDrvDeemphDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22B() < 0)
                return;
        }

        private void cbL2PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22B() < 0)
                return;
        }

        private void cbL2PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22B() < 0)
                return;
        }

        private int _WriteAddr300()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3TiaRateSel.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0300).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr303()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0303).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr304()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0304).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr305()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3LosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0305).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr309()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3OffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;

            if (cbL3EqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0309).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr30B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3EqLoopbackEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x030B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr317()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3CkgenMclkPhase.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0317).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr318()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3DrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0318).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr319()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3DrvMuteOnLos.Checked == true)
                data[0] |= 0x01;

            if (cbL3DrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0319).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr31A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3DrvMainSwing.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x031A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr31B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbL3DrvDeemphPrecSel.Checked == true)
                data[0] |= 0x01;

            bTmp = Convert.ToByte(cbL3DrvDeemphLevel.SelectedItem);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x031B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr31D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3DrvPredrvCmRef.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x031D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr320()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3RssiHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0320).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr322()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3RssiHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0322).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr325()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0325).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr326()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0326).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr329()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdCdr.Checked == true)
                data[0] |= 0x01;

            if (cbL3PdPPdrtMclk.Checked == true)
                data[0] |= 0x04;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0329).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr32A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdCkgenMclk.Checked == true)
                data[0] |= 0x40;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x032A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr32B()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL3PdDrv.Checked == true)
                data[0] |= 0x01;

            if (cbL3PdDrvDeemphDrv.Checked == true)
                data[0] |= 0x02;

            if (cbL3PdPrbClk.Checked == true)
                data[0] |= 0x10;

            if (cbL3PdDrvPrbschk.Checked == true)
                data[0] |= 0x20;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x032B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL3TiaRateSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr300() < 0)
                return;
        }

        private void cbL3CdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr303() < 0)
                return;
        }

        private void cbL3CdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr303() < 0)
                return;
        }

        private void cbL3CdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr303() < 0)
                return;
        }

        private void cbL3LosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr304() < 0)
                return;
        }

        private void cbL3LosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr305() < 0)
                return;
        }

        private void cbL3OffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr309() < 0)
                return;
        }

        private void cbL3EqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr309() < 0)
                return;
        }

        private void cbL3EqLoopbackEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr30B() < 0)
                return;
        }

        private void cbL3CkgenMclkPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr317() < 0)
                return;
        }

        private void cbL3DrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr318() < 0)
                return;
        }

        private void cbL3DrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr319() < 0)
                return;
        }

        private void cbL3DrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr319() < 0)
                return;
        }

        private void cbL3DrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr31A() < 0)
                return;
        }

        private void cbL3DrvDeemphPrecSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr31B() < 0)
                return;
        }

        private void cbL3DrvDeemphLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr31B() < 0)
                return;
        }

        private void cbL3DrvPredrvCmRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr31D() < 0)
                return;
        }

        private void cbL3RssiHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr320() < 0)
                return;
        }

        private void cbL3RssiHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr322() < 0)
                return;
        }

        private void cbL3PdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr325() < 0)
                return;
        }

        private void cbL3PdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr326() < 0)
                return;
        }

        private void cbL3PdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr329() < 0)
                return;
        }

        private void cbL3PdPPdrtMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr329() < 0)
                return;
        }

        private void cbL3PdCkgenMclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32A() < 0)
                return;
        }

        private void cbL3PdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32B() < 0)
                return;
        }

        private void cbL3PdDrvDeemphDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32B() < 0)
                return;
        }

        private void cbL3PdPrbClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32B() < 0)
                return;
        }

        private void cbL3PdDrvPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr32B() < 0)
                return;
        }

        private int _WriteAddr400()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTiaRssiDiv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0400).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr406()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0TiaToRssi1.Checked == true)
                data[0] |= 0x01;

            if (cbL1TiaToRssi1.Checked == true)
                data[0] |= 0x02;

            if (cbL2TiaToRssi1.Checked == true)
                data[0] |= 0x04;

            if (cbL3TiaToRssi1.Checked == true)
                data[0] |= 0x08;

            if (cbL0TiaToRssi2.Checked == true)
                data[0] |= 0x10;

            if (cbL1TiaToRssi2.Checked == true)
                data[0] |= 0x20;

            if (cbL2TiaToRssi2.Checked == true)
                data[0] |= 0x40;

            if (cbL3TiaToRssi2.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0406).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr407()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLoslSoftReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0407).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr408()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0408).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40A()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x040A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbLockdetLockThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x040C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr410()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0410).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr412()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0412).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr414()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0414).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr415()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLoslInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0415).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr416()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0Los.Checked == true)
                data[0] |= 0x01;

            if (cbL1Los.Checked == true)
                data[0] |= 0x02;

            if (cbL2Los.Checked == true)
                data[0] |= 0x04;

            if (cbL3Los.Checked == true)
                data[0] |= 0x08;

            if (cbL0Lol.Checked == true)
                data[0] |= 0x10;

            if (cbL1Lol.Checked == true)
                data[0] |= 0x20;

            if (cbL2Lol.Checked == true)
                data[0] |= 0x40;

            if (cbL3Lol.Checked == true)
                data[0] |= 0x80;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0416).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr417()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0417).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr427()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbRssiHighFaultThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0427).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43B()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x043B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbsgenStart.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x043C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43D()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x043D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbsgenCkSel.Checked == true)
                data[0] |= 0x01;

            if (cbPrbsgenCkdivCkSel.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x043E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr443()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPrbsgenVcoFreq.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0443).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr445()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0445).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr449()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPrbschkTimerClkSel.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0449).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr44A_44D()
        {
            byte[] data;
            int rv;

            if (tbPrbschkTimeout.Text.Length == 0)
                return -1;

            data = BitConverter.GetBytes(Convert.ToUInt32(tbPrbschkTimeout.Text));

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x044A).Reverse().ToArray(), 4, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr44E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbschkTimerEnable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x044E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr44F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPrbschkEnable.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x044F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr463()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbAdcSrcSel.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0463).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr466()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcParamMonCtrlReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0466).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr469()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0469).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr46A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbL0RssiMonEnable.Checked == true)
                data[0] |= 0x01;

            if (cbL1RssiMonEnable.Checked == true)
                data[0] |= 0x02;

            if (cbL2RssiMonEnable.Checked == true)
                data[0] |= 0x04;

            if (cbL3RssiMonEnable.Checked == true)
                data[0] |= 0x08;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x046A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr46D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcHostCtrlReq.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x046D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr46F()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x046F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr470_471()
        {
            byte[] data;
            int rv;

            if (tbAdcOffset.Text.Length == 0)
                return -1;

            data = BitConverter.GetBytes(Convert.ToUInt16(tbAdcOffset.Text));

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0470).Reverse().ToArray(), 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr472()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcStartConv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0472).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr47F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbBbStep.SelectedItem);
            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x047F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr483()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0483).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr484()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0484).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr485()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0485).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr486()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbPdPrbschk.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0486).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr487()
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

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0487).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTiaRssiDiv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr400() < 0)
                return;
        }

        private void cbL0TiaToRssi1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL1TiaToRssi1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL2TiaToRssi1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL3TiaToRssi1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL0TiaToRssi2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL1TiaToRssi2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL2TiaToRssi2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbL3TiaToRssi2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr406() < 0)
                return;
        }

        private void cbLoslSoftReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr407() < 0)
                return;
        }

        private void cbL0LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL1LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL2LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL3LosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL0LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL1LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL2LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL3LosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr408() < 0)
                return;
        }

        private void cbL0LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40A() < 0)
                return;
        }

        private void cbL1LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40A() < 0)
                return;
        }

        private void cbL2LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40A() < 0)
                return;
        }

        private void cbL3LosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40A() < 0)
                return;
        }

        private void cbLockdetLockThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40C() < 0)
                return;
        }

        private void cbL0LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL1LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL2LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL3LolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL0LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL1LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL2LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL3LolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr410() < 0)
                return;
        }

        private void cbL0LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL1LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL2LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL3LolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr412() < 0)
                return;
        }

        private void cbL0LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL1LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL2LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL3LosToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL0LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL1LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL2LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbL3LolToLoslEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr414() < 0)
                return;
        }

        private void cbLoslInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr415() < 0)
                return;
        }

        private void cbL0LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL1LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL2LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL3LosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL0LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL1LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL2LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbL3LolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr417() < 0)
                return;
        }

        private void cbRssiHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr427() < 0)
                return;
        }

        private void cbL0PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL1PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL2PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL3PrbsClkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL0PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL1PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL2PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbL3PrbsDataSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43B() < 0)
                return;
        }

        private void cbPrbsgenStart_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43C() < 0)
                return;
        }

        private void cbPrbsgenSequenceSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43D() < 0)
                return;
        }

        private void cbPrbsgenOutputSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43D() < 0)
                return;
        }

        private void cbPrbsgenCkdivRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43D() < 0)
                return;
        }

        private void cbPrbsgenCkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbPrbsgenCkdivCkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43E() < 0)
                return;
        }

        private void cbPrbsgenVcoFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr443() < 0)
                return;
        }

        private void cbPrbschkInvPrbs_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr445() < 0)
                return;
        }

        private void cbPrbschkSelPrbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr445() < 0)
                return;
        }

        private void cbPrbschkTimerClkSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr449() < 0)
                return;
        }

        private void tbPrbschkTimeout_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr44A_44D() < 0)
                return;
        }

        private void cbPrbschkTimerEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr44E() < 0)
                return;
        }

        private void cbPrbschkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr44F() < 0)
                return;
        }

        private void cbAdcSrcSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr463() < 0)
                return;
        }

        private void cbAdcParamMonCtrlReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr466() < 0)
                return;
        }

        private void cbTemperatureMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr469() < 0)
                return;
        }

        private void cbVcclSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr469() < 0)
                return;
        }

        private void cbVccSupplyMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr469() < 0)
                return;
        }

        private void cbL0RssiMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46A() < 0)
                return;
        }

        private void cbL1RssiMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46A() < 0)
                return;
        }

        private void cbL2RssiMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46A() < 0)
                return;
        }

        private void cbL3RssiMonEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46A() < 0)
                return;
        }

        private void cbAdcHostCtrlReq_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46D() < 0)
                return;
        }

        private void cbAdcReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46F() < 0)
                return;
        }

        private void cbAdcAutoConvEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46F() < 0)
                return;
        }

        private void cbAdcJustLsb_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46F() < 0)
                return;
        }

        private void cbAdcOffMode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46F() < 0)
                return;
        }

        private void cbAdcResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46F() < 0)
                return;
        }

        private void tbAdcOffset_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr470_471() < 0)
                return;
        }

        private void cbAdcStartConv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr472() < 0)
                return;
        }

        private void cbBbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47F() < 0)
                return;
        }

        private void cbPdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr483() < 0)
                return;
        }

        private void cbL0PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr484() < 0)
                return;
        }

        private void cbL1PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr484() < 0)
                return;
        }

        private void cbL2PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr484() < 0)
                return;
        }

        private void cbL3PdPrbsgenDataBuf_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr484() < 0)
                return;
        }

        private void cbPdPrbsgen_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr485() < 0)
                return;
        }

        private void cbPdPrbsgenCkdiv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr485() < 0)
                return;
        }

        private void cbPdPrbsgenVco_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr485() < 0)
                return;
        }

        private void cbPdPrbsgenAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr485() < 0)
                return;
        }

        private void cbPdPrbschk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr486() < 0)
                return;
        }

        private void cbPdTempSense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbPdSupplySense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void cbPdAdc_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr487() < 0)
                return;
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            bReadAll.Enabled = false;
            bL0Read_Click(sender, e);
            bL1Read_Click(sender, e);
            bL2Read_Click(sender, e);
            bL3Read_Click(sender, e);
            bReadControl_Click(sender, e);
            bReadCustomer_Click(sender, e);
            bReadAll.Enabled = true;
        }

        public int ReadAllApi()
        {
            try {
                bL0Read_Click(null, null);
                bL1Read_Click(null, null);
                bL2Read_Click(null, null);
                bL3Read_Click(null, null);
                bReadControl_Click(null, null);
                bReadCustomer_Click(null, null);

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
            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0AAA).Reverse().ToArray(), 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
        }

        private void cbAllLosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0LosThres.SelectedIndex = cbL1LosThres.SelectedIndex =
                cbL2LosThres.SelectedIndex = cbL3LosThres.SelectedIndex = 
                cbAllLosThres.SelectedIndex;
        }

        private void cbAllDrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbL0DrvMainSwing.SelectedIndex = cbL1DrvMainSwing.SelectedIndex = 
                cbL2DrvMainSwing.SelectedIndex = cbL3DrvMainSwing.SelectedIndex = 
                cbAllDrvMainSwing.SelectedIndex;
        }

        private void _ParseAddr500(byte data)
        {
            cbL0OutputAmplitude100400.SelectedIndex = data;
        }

        private void _ParseAddr501(byte data)
        {
            cbL0OutputAmplitude300600.SelectedIndex = data;
        }

        private void _ParseAddr502(byte data)
        {
            cbL0OutputAmplitude400800.SelectedIndex = data;
        }

        private void _ParseAddr503(byte data)
        {
            cbL0OutputAmplitude6001200.SelectedIndex = data;
        }

        private void _ParseAddr504(byte data)
        {
            cbL0OutputAmplitudeReserved0.SelectedIndex = data;
        }

        private void _ParseAddr505(byte data)
        {
            cbL0OutputAmplitudeReserved1.SelectedIndex = data;
        }

        private void _ParseAddr506(byte data)
        {
            cbL0OutputAmplitudeReserved2.SelectedIndex = data;
        }

        private void _ParseAddr507(byte data)
        {
            cbL0OutputAmplitudeReserved3.SelectedIndex = data;
        }

        private void _ParseAddr508(byte data)
        {
            cbL0OutputAmplitudeReserved4.SelectedIndex = data;
        }

        private void _ParseAddr509(byte data)
        {
            cbL0OutputEmphasis0Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr510(byte data)
        {
            cbL0OutputEmphasis1Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr511(byte data)
        {
            cbL0OutputEmphasis2Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr512(byte data)
        {
            cbL0OutputEmphasis3Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr513(byte data)
        {
            cbL0OutputEmphasis4Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr514(byte data)
        {
            cbL0OutputEmphasis5Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr515(byte data)
        {
            cbL0OutputEmphasis6Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr516(byte data)
        {
            cbL0OutputEmphasis7Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr517(byte data)
        {
            cbL0OutputEmphasisReserved0.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr518(byte data)
        {
            cbL1OutputAmplitude100400.SelectedIndex = data;
        }

        private void _ParseAddr519(byte data)
        {
            cbL1OutputAmplitude300600.SelectedIndex = data;
        }

        private void _ParseAddr520(byte data)
        {
            cbL1OutputAmplitude400800.SelectedIndex = data;
        }

        private void _ParseAddr521(byte data)
        {
            cbL1OutputAmplitude6001200.SelectedIndex = data;
        }

        private void _ParseAddr522(byte data)
        {
            cbL1OutputAmplitudeReserved0.SelectedIndex = data;
        }

        private void _ParseAddr523(byte data)
        {
            cbL1OutputAmplitudeReserved1.SelectedIndex = data;
        }

        private void _ParseAddr524(byte data)
        {
            cbL1OutputAmplitudeReserved2.SelectedIndex = data;
        }

        private void _ParseAddr525(byte data)
        {
            cbL1OutputAmplitudeReserved3.SelectedIndex = data;
        }

        private void _ParseAddr526(byte data)
        {
            cbL1OutputAmplitudeReserved4.SelectedIndex = data;
        }

        private void _ParseAddr527(byte data)
        {
            cbL1OutputEmphasis0Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr528(byte data)
        {
            cbL1OutputEmphasis1Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr529(byte data)
        {
            cbL1OutputEmphasis2Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr530(byte data)
        {
            cbL1OutputEmphasis3Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr531(byte data)
        {
            cbL1OutputEmphasis4Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr532(byte data)
        {
            cbL1OutputEmphasis5Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr533(byte data)
        {
            cbL1OutputEmphasis6Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr534(byte data)
        {
            cbL1OutputEmphasis7Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr535(byte data)
        {
            cbL1OutputEmphasisReserved0.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr536(byte data)
        {
            cbL2OutputAmplitude100400.SelectedIndex = data;
        }

        private void _ParseAddr537(byte data)
        {
            cbL2OutputAmplitude300600.SelectedIndex = data;
        }

        private void _ParseAddr538(byte data)
        {
            cbL2OutputAmplitude400800.SelectedIndex = data;
        }

        private void _ParseAddr539(byte data)
        {
            cbL2OutputAmplitude6001200.SelectedIndex = data;
        }

        private void _ParseAddr540(byte data)
        {
            cbL2OutputAmplitudeReserved0.SelectedIndex = data;
        }

        private void _ParseAddr541(byte data)
        {
            cbL2OutputAmplitudeReserved1.SelectedIndex = data;
        }

        private void _ParseAddr542(byte data)
        {
            cbL2OutputAmplitudeReserved2.SelectedIndex = data;
        }

        private void _ParseAddr543(byte data)
        {
            cbL2OutputAmplitudeReserved3.SelectedIndex = data;
        }

        private void _ParseAddr544(byte data)
        {
            cbL2OutputAmplitudeReserved4.SelectedIndex = data;
        }

        private void _ParseAddr545(byte data)
        {
            cbL2OutputEmphasis0Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr546(byte data)
        {
            cbL2OutputEmphasis1Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr547(byte data)
        {
            cbL2OutputEmphasis2Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr548(byte data)
        {
            cbL2OutputEmphasis3Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr549(byte data)
        {
            cbL2OutputEmphasis4Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr550(byte data)
        {
            cbL2OutputEmphasis5Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr551(byte data)
        {
            cbL2OutputEmphasis6Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr552(byte data)
        {
            cbL2OutputEmphasis7Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr553(byte data)
        {
            cbL2OutputEmphasisReserved0.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr554(byte data)
        {
            cbL3OutputAmplitude100400.SelectedIndex = data;
        }

        private void _ParseAddr555(byte data)
        {
            cbL3OutputAmplitude300600.SelectedIndex = data;
        }

        private void _ParseAddr556(byte data)
        {
            cbL3OutputAmplitude400800.SelectedIndex = data;
        }

        private void _ParseAddr557(byte data)
        {
            cbL3OutputAmplitude6001200.SelectedIndex = data;
        }

        private void _ParseAddr558(byte data)
        {
            cbL3OutputAmplitudeReserved0.SelectedIndex = data;
        }

        private void _ParseAddr559(byte data)
        {
            cbL3OutputAmplitudeReserved1.SelectedIndex = data;
        }

        private void _ParseAddr560(byte data)
        {
            cbL3OutputAmplitudeReserved2.SelectedIndex = data;
        }

        private void _ParseAddr561(byte data)
        {
            cbL3OutputAmplitudeReserved3.SelectedIndex = data;
        }

        private void _ParseAddr562(byte data)
        {
            cbL3OutputAmplitudeReserved4.SelectedIndex = data;
        }

        private void _ParseAddr563(byte data)
        {
            cbL3OutputEmphasis0Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr564(byte data)
        {
            cbL3OutputEmphasis1Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr565(byte data)
        {
            cbL3OutputEmphasis2Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr566(byte data)
        {
            cbL3OutputEmphasis3Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr567(byte data)
        {
            cbL3OutputEmphasis4Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr568(byte data)
        {
            cbL3OutputEmphasis5Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr569(byte data)
        {
            cbL3OutputEmphasis6Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr570(byte data)
        {
            cbL3OutputEmphasis7Db.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr571(byte data)
        {
            cbL3OutputEmphasisReserved0.SelectedIndex = data & 0x0F;
        }

        private void bReadCustomer_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[72];
            int rv;

            if (reading == true)
                return;

            reading = true;

            if (i2cRead16CB == null)
                goto exit;

            rv = i2cRead16CB(92, BitConverter.GetBytes((ushort)0x0500).Reverse().ToArray(), 72, data);
            if (rv != 72)
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
            _ParseAddr552(data[52]);
            _ParseAddr553(data[53]);
            _ParseAddr554(data[54]);
            _ParseAddr555(data[55]);
            _ParseAddr556(data[56]);
            _ParseAddr557(data[57]);
            _ParseAddr558(data[58]);
            _ParseAddr559(data[59]);
            _ParseAddr560(data[60]);
            _ParseAddr561(data[61]);
            _ParseAddr562(data[62]);
            _ParseAddr563(data[63]);
            _ParseAddr564(data[64]);
            _ParseAddr565(data[65]);
            _ParseAddr566(data[66]);
            _ParseAddr567(data[67]);
            _ParseAddr568(data[68]);
            _ParseAddr569(data[69]);
            _ParseAddr570(data[70]);
            _ParseAddr571(data[71]);
            
        exit:
            reading = false;
        }

        private int _WriteAddr500()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0OutputAmplitude100400.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0500).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitude300600.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0501).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitude400800.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0502).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitude6001200.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0503).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0504).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0505).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0506).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved3.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0507).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved4.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0508).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis0Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0509).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis1Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050A).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis2Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050B).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis3Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr513()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0OutputEmphasis4Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050D).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis5Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050E).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis6Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x050F).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasis7Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0510).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL0OutputEmphasisReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0511).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr500() < 0)
                return;
        }

        private void cbL0OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr501() < 0)
                return;
        }

        private void cbL0OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr502() < 0)
                return;
        }

        private void cbL0OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr503() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr504() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr505() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr506() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr507() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr508() < 0)
                return;
        }

        private void cbL0OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr509() < 0)
                return;
        }

        private void cbL0OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr510() < 0)
                return;
        }

        private void cbL0OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr511() < 0)
                return;
        }

        private void cbL0OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr512() < 0)
                return;
        }

        private void cbL0OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr513() < 0)
                return;
        }

        private void cbL0OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr514() < 0)
                return;
        }

        private void cbL0OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr515() < 0)
                return;
        }

        private void cbL0OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr516() < 0)
                return;
        }

        private void cbL0OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr517() < 0)
                return;
        }

        private int _WriteAddr518()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1OutputAmplitude100400.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0512).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitude300600.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0513).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitude400800.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0514).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitude6001200.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0515).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0516).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0517).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0518).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved3.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0519).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr526()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved4.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051A).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis0Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051B).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis1Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051C).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis2Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051D).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis3Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051E).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis4Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x051F).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis5Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0520).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis6Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0521).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasis7Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0522).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL1OutputEmphasisReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0523).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL1OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr518() < 0)
                return;
        }

        private void cbL1OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr519() < 0)
                return;
        }

        private void cbL1OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr520() < 0)
                return;
        }

        private void cbL1OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr521() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr522() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr523() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr524() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr525() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr526() < 0)
                return;
        }

        private void cbL1OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr527() < 0)
                return;
        }

        private void cbL1OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr528() < 0)
                return;
        }

        private void cbL1OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr529() < 0)
                return;
        }

        private void cbL1OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr530() < 0)
                return;
        }

        private void cbL1OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr531() < 0)
                return;
        }

        private void cbL1OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr532() < 0)
                return;
        }

        private void cbL1OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr533() < 0)
                return;
        }

        private void cbL1OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr534() < 0)
                return;
        }

        private void cbL1OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr535() < 0)
                return;
        }

        private int _WriteAddr536()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2OutputAmplitude100400.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0524).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitude300600.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0525).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitude400800.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0526).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr539()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2OutputAmplitude6001200.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0527).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0528).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0529).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052A).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved3.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052B).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved4.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052C).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis0Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052D).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis1Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052E).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis2Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x052F).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis3Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0530).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis4Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0531).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis5Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0532).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbL2OutputEmphasis6Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0533).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr552()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2OutputEmphasis7Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0534).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr553()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL2OutputEmphasisReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0535).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL2OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr536() < 0)
                return;
        }

        private void cbL2OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr537() < 0)
                return;
        }

        private void cbL2OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr538() < 0)
                return;
        }

        private void cbL2OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr539() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr540() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr541() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr542() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr543() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr544() < 0)
                return;
        }

        private void cbL2OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr545() < 0)
                return;
        }

        private void cbL2OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr546() < 0)
                return;
        }

        private void cbL2OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr547() < 0)
                return;
        }

        private void cbL2OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr548() < 0)
                return;
        }

        private void cbL2OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr549() < 0)
                return;
        }

        private void cbL2OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr550() < 0)
                return;
        }

        private void cbL2OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr551() < 0)
                return;
        }

        private void cbL2OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr552() < 0)
                return;
        }

        private void cbL2OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr553() < 0)
                return;
        }

        private int _WriteAddr554()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitude100400.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0536).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr555()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitude300600.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0537).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr556()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitude400800.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0538).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr557()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitude6001200.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0539).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr558()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitudeReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr559()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitudeReserved1.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr560()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitudeReserved2.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr561()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitudeReserved3.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr562()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputAmplitudeReserved4.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053E).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr563()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis0Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x053F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr564()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis1Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0540).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr565()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis2Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0541).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr566()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis3Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0542).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr567()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis4Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0543).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr568()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis5Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0544).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr569()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis6Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0545).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr570()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasis7Db.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0546).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr571()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL3OutputEmphasisReserved0.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(92, BitConverter.GetBytes((ushort)0x0547).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL3OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr554() < 0)
                return;
        }

        private void cbL3OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr555() < 0)
                return;
        }

        private void cbL3OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr556() < 0)
                return;
        }

        private void cbL3OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr557() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr558() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr559() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr560() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr561() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr562() < 0)
                return;
        }

        private void cbL3OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr563() < 0)
                return;
        }

        private void cbL3OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr564() < 0)
                return;
        }

        private void cbL3OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr565() < 0)
                return;
        }

        private void cbL3OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr566() < 0)
                return;
        }

        private void cbL3OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr567() < 0)
                return;
        }

        private void cbL3OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr568() < 0)
                return;
        }

        private void cbL3OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr569() < 0)
                return;
        }

        private void cbL3OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr570() < 0)
                return;
        }

        private void cbL3OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr571() < 0)
                return;
        }
    }
}
