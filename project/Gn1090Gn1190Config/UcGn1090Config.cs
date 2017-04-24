using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Gn1090Gn1190Config
{
    public partial class UcGn1090Config : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcGn1090Config()
        {
            int i;

            InitializeComponent();

            reading = true;
            for (i = 0; i < 4; i++) {
                cbRssi2Select.Items.Add(i);
                cbRssi1Select.Items.Add(i);
                cbRx1RateSelect.Items.Add(i);
                cbRx2RateSelect.Items.Add(i);
                cbRx3RateSelect.Items.Add(i);
                cbRx4RateSelect.Items.Add(i);
                cbAllRateSelect.Items.Add(i);
            }
            cbRx1RateSelect.SelectedIndex = cbRx2RateSelect.SelectedIndex =
                cbRx3RateSelect.SelectedIndex = cbRx4RateSelect.SelectedIndex =
                cbAllRateSelect.SelectedIndex = 0;

            for (i = 0; i < 8; i++) {
                cbRx1BandwidthCtrl.Items.Add(i);
                cbRx2BandwidthCtrl.Items.Add(i);
                cbRx3BandwidthCtrl.Items.Add(i);
                cbRx4BandwidthCtrl.Items.Add(i);
                cbAllBandwidthCtrl.Items.Add(i);
                cbRx1Delay.Items.Add(i);
                cbRx2Delay.Items.Add(i);
                cbRx3Delay.Items.Add(i);
                cbRx4Delay.Items.Add(i);
                cbAllDelay.Items.Add(i);
                cbRx1Hysteresis.Items.Add(i);
                cbRx2Hysteresis.Items.Add(i);
                cbRx3Hysteresis.Items.Add(i);
                cbRx4Hysteresis.Items.Add(i);
                cbAllHysteresis.Items.Add(i);
            }
            cbRx1BandwidthCtrl.SelectedIndex = cbRx2BandwidthCtrl.SelectedIndex =
                cbRx3BandwidthCtrl.SelectedIndex = cbRx4BandwidthCtrl.SelectedIndex =
                cbAllBandwidthCtrl.SelectedIndex = 0;
            cbRx1Delay.SelectedIndex = cbRx2Delay.SelectedIndex =
                cbRx3Delay.SelectedIndex = cbRx4Delay.SelectedIndex =
                cbAllDelay.SelectedIndex = 0;
            cbRx1Hysteresis.SelectedIndex = cbRx2Hysteresis.SelectedIndex =
                cbRx3Hysteresis.SelectedIndex = cbRx4Hysteresis.SelectedIndex =
                cbAllHysteresis.SelectedIndex = 0;

            for (i = 0; i < 32; i++) {
                cbRx1OutputAmplitudeSwingSelect.Items.Add(i);
                cbRx2OutputAmplitudeSwingSelect.Items.Add(i);
                cbRx3OutputAmplitudeSwingSelect.Items.Add(i);
                cbRx4OutputAmplitudeSwingSelect.Items.Add(i);
                cbAllOutputAmplitudeSwingSelect.Items.Add(i);
                cbRx1Magnitude.Items.Add(i);
                cbRx2Magnitude.Items.Add(i);
                cbRx3Magnitude.Items.Add(i);
                cbRx4Magnitude.Items.Add(i);
                cbAllMagnitude.Items.Add(i);
            }
            cbRx1OutputAmplitudeSwingSelect.SelectedIndex =
                cbRx2OutputAmplitudeSwingSelect.SelectedIndex =
                cbRx3OutputAmplitudeSwingSelect.SelectedIndex =
                cbRx4OutputAmplitudeSwingSelect.SelectedIndex =
                cbAllOutputAmplitudeSwingSelect.SelectedIndex = 0;
            cbRx1Magnitude.SelectedIndex = cbRx2Magnitude.SelectedIndex =
                cbRx3Magnitude.SelectedIndex = cbRx4Magnitude.SelectedIndex =
                cbAllMagnitude.SelectedIndex = 0;

            for (i = 0; i < 256; i++) {
                cbRx1LosThreshold.Items.Add(i);
                cbRx2LosThreshold.Items.Add(i);
                cbRx3LosThreshold.Items.Add(i);
                cbRx4LosThreshold.Items.Add(i);
                cbAllLosThreshold.Items.Add(i);
            }
            cbRx1LosThreshold.SelectedIndex = cbRx2LosThreshold.SelectedIndex =
                cbRx3LosThreshold.SelectedIndex = cbRx4LosThreshold.SelectedIndex =
                cbAllLosThreshold.SelectedIndex = 0;

            reading = false;
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

        private void _ParseAddr0(byte data)
        {
            if ((data & 0x01) == 0)
                cbGlobalRxLosLatched.Checked = false;
            else
                cbGlobalRxLosLatched.Checked = true;

            if ((data & 0x02) == 0)
                cbGlobalRxLos.Checked = false;
            else
                cbGlobalRxLos.Checked = true;
        }

        private void _ParseAddr1(byte data)
        {
            if ((data & 0x01) == 0)
                cbRx1Los.Checked = false;
            else
                cbRx1Los.Checked = true;

            if ((data & 0x02) == 0)
                cbRx2Los.Checked = false;
            else
                cbRx2Los.Checked = true;

            if ((data & 0x04) == 0)
                cbRx3Los.Checked = false;
            else
                cbRx3Los.Checked = true;

            if ((data & 0x08) == 0)
                cbRx4Los.Checked = false;
            else
                cbRx4Los.Checked = true;
        }

        private void _ParseAddr6_7(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= ((data1 & 0x80) >> 7);

            tbTemperature.Text = iTmp.ToString();
        }

        private void _ParseAddr16_17(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbRx1Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr18_19(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbRx2Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr20_21(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbRx3Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr22_23(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbRx4Rssi.Text = iTmp.ToString();
        }

        private void _ParseAddr52(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            if (iTmp != 0x0F)
                cbRssi2Select.SelectedIndex = iTmp;

            if ((data & 0x80) == 0)
                cbRssi2Enable.Checked = false;
            else
                cbRssi2Enable.Checked = true;
        }

        private void _ParseAddr53(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            if (iTmp != 0x0F)
                cbRssi1Select.SelectedIndex = iTmp;

            if ((data & 0x80) == 0)
                cbRssi1Enable.Checked = false;
            else
                cbRssi1Enable.Checked = true;
        }

        private void _ParseAddr54(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDown.Checked = false;
            else
                cbPowerDown.Checked = true;

            if ((data & 0x02) == 0)
                cbSleep.Checked = false;
            else
                cbSleep.Checked = true;

            if ((data & 0x04) == 0)
                cbLosPinPolarity.Checked = false;
            else
                cbLosPinPolarity.Checked = true;

            if ((data & 0x08) == 0)
                cbLosPinPowerDownState.Checked = false;
            else
                cbLosPinPowerDownState.Checked = true;

            if ((data & 0x10) == 0)
                cbLosChannelLatchEnable.Checked = false;
            else
                cbLosChannelLatchEnable.Checked = true;

            if ((data & 0x20) == 0)
                cbCsSquelch.Checked = false;
            else
                cbCsSquelch.Checked = true;

            if ((data & 0x40) == 0)
                cbCsReset.Checked = false;
            else
                cbCsReset.Checked = true;
        }

        private void _ParseAddr55(byte data)
        {
            if ((data & 0x01) == 0)
                cbI2cTestSpeedup.Checked = false;
            else
                cbI2cTestSpeedup.Checked = true;

            if ((data & 0x40) == 0)
                cbI2cTimeoutEnable.Checked = false;
            else
                cbI2cTimeoutEnable.Checked = true;

            if ((data & 0x80) == 0)
                cbI2cAddrPadEnable.Checked = false;
            else
                cbI2cAddrPadEnable.Checked = true;
        }

        private void _ParseAddr59(byte data)
        {
            if ((data & 0x01) == 0)
                cbSoftResetPartial.Checked = false;
            else
                cbSoftResetPartial.Checked = true;

            if ((data & 0x02) == 0)
                cbSoftResetFull.Checked = false;
            else
                cbSoftResetFull.Checked = true;
        }

        private void _ParseAddr60_61(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbPassword0.Text = String.Format("{0:x4}", iTmp);
        }

        private void _ParseAddr62_63(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbPassword1.Text = String.Format("{0:x4}", iTmp);
        }

        private void _ParseAddr64(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx1PowerDown.Checked = false;
            else
                cbRx1PowerDown.Checked = true;

            if ((data & 0x02) == 0)
                cbRx1Sleep.Checked = false;
            else
                cbRx1Sleep.Checked = true;

            iTmp = (data & 0x0C) >> 2;
            cbRx1RateSelect.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx1PolarityInvert.Checked = false;
            else
                cbRx1PolarityInvert.Checked = true;

            if ((data & 0x20) == 0)
                cbRx1LosDisable.Checked = false;
            else
                cbRx1LosDisable.Checked = true;
        }

        private void _ParseAddr65(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx1OutputAmplitudeSwingSelect.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx1BandwidthCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr66(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx1Magnitude.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx1Delay.SelectedIndex = iTmp;
        }

        private void _ParseAddr67(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx1SquelchDisable.Checked = false;
            else
                cbRx1SquelchDisable.Checked = true;

            iTmp = (data & 0x0E) >> 1;
            cbRx1Hysteresis.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx1ForceSquelch.Checked = false;
            else
                cbRx1ForceSquelch.Checked = true;
        }

        private void _ParseAddr68(byte data)
        {
            int iTmp;

            iTmp = data;
            cbRx1LosThreshold.SelectedIndex = iTmp;
        }

        private void _ParseAddr80(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx2PowerDown.Checked = false;
            else
                cbRx2PowerDown.Checked = true;

            if ((data & 0x02) == 0)
                cbRx2Sleep.Checked = false;
            else
                cbRx2Sleep.Checked = true;

            iTmp = (data & 0x0C) >> 2;
            cbRx2RateSelect.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx2PolarityInvert.Checked = false;
            else
                cbRx2PolarityInvert.Checked = true;

            if ((data & 0x20) == 0)
                cbRx2LosDisable.Checked = false;
            else
                cbRx2LosDisable.Checked = true;
        }

        private void _ParseAddr81(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx2OutputAmplitudeSwingSelect.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx2BandwidthCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr82(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx2Magnitude.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx2Delay.SelectedIndex = iTmp;
        }

        private void _ParseAddr83(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx2SquelchDisable.Checked = false;
            else
                cbRx2SquelchDisable.Checked = true;

            iTmp = (data & 0x0E) >> 1;
            cbRx2Hysteresis.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx2ForceSquelch.Checked = false;
            else
                cbRx2ForceSquelch.Checked = true;
        }

        private void _ParseAddr84(byte data)
        {
            int iTmp;

            iTmp = data;
            cbRx2LosThreshold.SelectedIndex = iTmp;
        }

        private void _ParseAddr96(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx3PowerDown.Checked = false;
            else
                cbRx3PowerDown.Checked = true;

            if ((data & 0x02) == 0)
                cbRx3Sleep.Checked = false;
            else
                cbRx3Sleep.Checked = true;

            iTmp = (data & 0x0C) >> 2;
            cbRx3RateSelect.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx3PolarityInvert.Checked = false;
            else
                cbRx3PolarityInvert.Checked = true;

            if ((data & 0x20) == 0)
                cbRx3LosDisable.Checked = false;
            else
                cbRx3LosDisable.Checked = true;
        }

        private void _ParseAddr97(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx3OutputAmplitudeSwingSelect.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx3BandwidthCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr98(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx3Magnitude.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx3Delay.SelectedIndex = iTmp;
        }

        private void _ParseAddr99(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx3SquelchDisable.Checked = false;
            else
                cbRx3SquelchDisable.Checked = true;

            iTmp = (data & 0x0E) >> 1;
            cbRx3Hysteresis.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx3ForceSquelch.Checked = false;
            else
                cbRx3ForceSquelch.Checked = true;
        }

        private void _ParseAddr100(byte data)
        {
            int iTmp;

            iTmp = data;
            cbRx3LosThreshold.SelectedIndex = iTmp;
        }

        private void _ParseAddr112(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx4PowerDown.Checked = false;
            else
                cbRx4PowerDown.Checked = true;

            if ((data & 0x02) == 0)
                cbRx4Sleep.Checked = false;
            else
                cbRx4Sleep.Checked = true;

            iTmp = (data & 0x0C) >> 2;
            cbRx4RateSelect.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx4PolarityInvert.Checked = false;
            else
                cbRx4PolarityInvert.Checked = true;

            if ((data & 0x20) == 0)
                cbRx4LosDisable.Checked = false;
            else
                cbRx4LosDisable.Checked = true;
        }

        private void _ParseAddr113(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx4OutputAmplitudeSwingSelect.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx4BandwidthCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr114(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbRx4Magnitude.SelectedIndex = iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbRx4Delay.SelectedIndex = iTmp;
        }

        private void _ParseAddr115(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbRx4SquelchDisable.Checked = false;
            else
                cbRx4SquelchDisable.Checked = true;

            iTmp = (data & 0x0E) >> 1;
            cbRx4Hysteresis.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbRx4ForceSquelch.Checked = false;
            else
                cbRx4ForceSquelch.Checked = true;
        }

        private void _ParseAddr116(byte data)
        {
            int iTmp;

            iTmp = data;
            cbRx4LosThreshold.SelectedIndex = iTmp;
        }

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[23];
            int rv;

            bRead.Enabled = false;
            reading = true;

            if (i2cReadCB == null)
                goto exit;

            rv = i2cReadCB(92, 0, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr0(data[0]);
            _ParseAddr1(data[1]);

            rv = i2cReadCB(92, 5, 3, data);
            if (rv != 3)
                goto exit;

            tbSupplyVoltage.Text = data[0].ToString();
            _ParseAddr6_7(data[1], data[2]);

            rv = i2cReadCB(92, 16, 8, data);
            if (rv != 8)
                goto exit;

            _ParseAddr16_17(data[0], data[1]);
            _ParseAddr18_19(data[2], data[3]);
            _ParseAddr20_21(data[4], data[5]);
            _ParseAddr22_23(data[6], data[7]);

            rv = i2cReadCB(92, 52, 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr52(data[0]);
            _ParseAddr53(data[1]);
            _ParseAddr54(data[2]);
            _ParseAddr55(data[3]);

            rv = i2cReadCB(92, 59, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr59(data[0]);
            _ParseAddr60_61(data[1], data[2]);
            _ParseAddr62_63(data[3], data[4]);

            rv = i2cReadCB(92, 64, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr64(data[0]);
            _ParseAddr65(data[1]);
            _ParseAddr66(data[2]);
            _ParseAddr67(data[3]);
            _ParseAddr68(data[4]);

            rv = i2cReadCB(92, 80, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr80(data[0]);
            _ParseAddr81(data[1]);
            _ParseAddr82(data[2]);
            _ParseAddr83(data[3]);
            _ParseAddr84(data[4]);

            rv = i2cReadCB(92, 96, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr96(data[0]);
            _ParseAddr97(data[1]);
            _ParseAddr98(data[2]);
            _ParseAddr99(data[3]);
            _ParseAddr100(data[4]);

            rv = i2cReadCB(92, 112, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr112(data[0]);
            _ParseAddr113(data[1]);
            _ParseAddr114(data[2]);
            _ParseAddr115(data[3]);
            _ParseAddr116(data[4]);

        exit:
            reading = false;
            bRead.Enabled = true;
        }

        private int _WriteAddr52()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRssi2Select.SelectedIndex);
            data[0] |= bTmp;

            if (cbRssi2Enable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(92, 52, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRssi2Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbRssi2Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private int _WriteAddr53()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRssi1Select.SelectedIndex);
            data[0] |= bTmp;

            if (cbRssi1Enable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(92, 53, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRssi1Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr53() < 0)
                return;
        }

        private void cbRssi1Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr53() < 0)
                return;
        }

        private int _WriteAddr54()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbSleep.Checked == true)
                data[0] |= 0x02;

            if (cbLosPinPolarity.Checked == true)
                data[0] |= 0x04;

            if (cbLosPinPowerDownState.Checked == true)
                data[0] |= 0x08;

            if (cbLosChannelLatchEnable.Checked == true)
                data[0] |= 0x10;

            if (cbCsSquelch.Checked == true)
                data[0] |= 0x20;

            if (cbCsReset.Checked == true)
                data[0] |= 0x40;

            rv = i2cWriteCB(92, 54, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbSleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbLosPinPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbLosPinPowerDownState_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbLosChannelLatchEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbCsSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbCsReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private int _WriteAddr55()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbI2cTestSpeedup.Checked == true)
                data[0] |= 0x01;

            if (cbI2cTimeoutEnable.Checked == true)
                data[0] |= 0x40;

            if (cbI2cAddrPadEnable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(92, 55, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbI2cTestSpeedup_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbI2cTimeoutEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbI2cAddrPadEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private int _WriteAddr59()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbSoftResetPartial.Checked == true)
                data[0] |= 0x01;

            if (cbSoftResetFull.Checked == true)
                data[0] |= 0x02;

            rv = i2cWriteCB(92, 59, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbSoftResetPartial_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr59() < 0)
                return;
        }

        private void cbSoftResetFull_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr59() < 0)
                return;
        }

        private int _WriteAddr60_61()
        {
            byte[] data = new byte[2];
            int i, rv;

            data[0] = 0;

            if (tbPassword0.Text.Length != 4) {
                MessageBox.Show("Password0 length wrong: " + tbPassword0.Text.Length);
                tbPassword0.Text = "0000";
                return -1;
            }

            for (i = 0; i < 4; i += 2)
                data[i / 2] = Convert.ToByte(tbPassword0.Text.Substring(i, 2), 16);

            rv = i2cWriteCB(92, 60, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void tbPassword0_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr60_61() < 0)
                return;
        }

        private int _WriteAddr62_63()
        {
            byte[] data = new byte[2];
            int i, rv;

            data[0] = 0;

            if (tbPassword1.Text.Length != 4) {
                MessageBox.Show("Password1 length wrong: " + tbPassword1.Text.Length);
                tbPassword1.Text = "0000";
                return -1;
            }

            for (i = 0; i < 4; i += 2)
                data[i / 2] = Convert.ToByte(tbPassword1.Text.Substring(i, 2), 16);

            rv = i2cWriteCB(92, 62, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void tbPassword1_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr62_63() < 0)
                return;
        }

        private int _WriteAddr64()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx1PowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbRx1Sleep.Checked == true)
                data[0] |= 0x02;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx1RateSelect.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbRx1PolarityInvert.Checked == true)
                data[0] |= 0x10;

            if (cbRx1LosDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWriteCB(92, 64, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx1PowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbRx1Sleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbRx1RateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbRx1PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbRx1LosDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private int _WriteAddr65()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx1OutputAmplitudeSwingSelect.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx1BandwidthCtrl.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx1OutputAmplitudeSwingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void cbRx1BandwidthCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private int _WriteAddr66()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx1Magnitude.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx1Delay.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 66, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx1Magnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void cbRx1Delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private int _WriteAddr67()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx1SquelchDisable.Checked == true)
                data[0] |= 0x01;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx1Hysteresis.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbRx1ForceSquelch.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(92, 67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx1SquelchDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void cbRx1Hysteresis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void cbRx1ForceSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private int _WriteAddr68()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx1LosThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 68, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx1LosThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr68() < 0)
                return;
        }

        private int _WriteAddr80()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx2PowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbRx2Sleep.Checked == true)
                data[0] |= 0x02;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx2RateSelect.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbRx2PolarityInvert.Checked == true)
                data[0] |= 0x10;

            if (cbRx2LosDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWriteCB(92, 80, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx2PowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbRx2Sleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbRx2RateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbRx2PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbRx2LosDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private int _WriteAddr81()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx2OutputAmplitudeSwingSelect.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx2BandwidthCtrl.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 81, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx2OutputAmplitudeSwingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void cbRx2BandwidthCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private int _WriteAddr82()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx2Magnitude.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx2Delay.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx2Magnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void cbRx2Delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private int _WriteAddr83()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx2SquelchDisable.Checked == true)
                data[0] |= 0x01;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx2Hysteresis.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbRx2ForceSquelch.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(92, 83, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx2SquelchDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private void cbRx2Hysteresis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private void cbRx2ForceSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private int _WriteAddr84()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx2LosThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 84, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx2LosThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr84() < 0)
                return;
        }

        private int _WriteAddr96()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx3PowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbRx3Sleep.Checked == true)
                data[0] |= 0x02;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx3RateSelect.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbRx3PolarityInvert.Checked == true)
                data[0] |= 0x10;

            if (cbRx3LosDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWriteCB(92, 96, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx3PowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbRx3Sleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbRx3RateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbRx3PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbRx3LosDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private int _WriteAddr97()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx3OutputAmplitudeSwingSelect.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx3BandwidthCtrl.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 97, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx3OutputAmplitudeSwingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void cbRx3BandwidthCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private int _WriteAddr98()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx3Magnitude.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx3Delay.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 98, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx3Magnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void cbRx3Delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private int _WriteAddr99()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx3SquelchDisable.Checked == true)
                data[0] |= 0x01;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx3Hysteresis.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbRx3ForceSquelch.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(92, 99, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx3SquelchDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void cbRx3Hysteresis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void cbRx3ForceSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private int _WriteAddr100()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx3LosThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 100, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx3LosThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private int _WriteAddr112()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx4PowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbRx4Sleep.Checked == true)
                data[0] |= 0x02;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx4RateSelect.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbRx4PolarityInvert.Checked == true)
                data[0] |= 0x10;

            if (cbRx4LosDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWriteCB(92, 112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx4PowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbRx4Sleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbRx4RateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbRx4PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbRx4LosDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private int _WriteAddr113()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx4OutputAmplitudeSwingSelect.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx4BandwidthCtrl.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx4OutputAmplitudeSwingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void cbRx4BandwidthCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private int _WriteAddr114()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx4Magnitude.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx4Delay.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx4Magnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private void cbRx4Delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private int _WriteAddr115()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbRx4SquelchDisable.Checked == true)
                data[0] |= 0x01;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbRx4Hysteresis.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbRx4ForceSquelch.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(92, 115, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx4SquelchDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void cbRx4Hysteresis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void cbRx4ForceSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private int _WriteAddr116()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbRx4LosThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 116, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRx4LosThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr116() < 0)
                return;
        }

        private int _WriteAddr64_80_96_112()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbAllPowerDown.Checked == true)
                data[0] |= 0x01;

            if (cbAllSleep.Checked == true)
                data[0] |= 0x02;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllRateSelect.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbAllPolarityInvert.Checked == true)
                data[0] |= 0x10;

            if (cbAllLosDisable.Checked == true)
                data[0] |= 0x20;

            rv = i2cWriteCB(92, 64, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 80, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 96, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllPowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllSleep_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllRateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllLosDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private int _WriteAddr65_81_97_113()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllOutputAmplitudeSwingSelect.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllBandwidthCtrl.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 65, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 81, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 97, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllOutputAmplitudeSwingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65_81_97_113() < 0)
                return;
        }

        private void cbAllBandwidthCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65_81_97_113() < 0)
                return;
        }

        private int _WriteAddr66_82_98_114()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllMagnitude.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllDelay.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 66, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 82, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 98, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllMagnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66_82_98_114() < 0)
                return;
        }

        private void cbAllDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66_82_98_114() < 0)
                return;
        }

        private int _WriteAddr67_83_99_115()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbAllSquelchDisable.Checked == true)
                data[0] |= 0x01;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllHysteresis.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;
            

            if (cbAllForceSquelch.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(92, 67, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 83, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 99, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 115, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllSquelchDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67_83_99_115() < 0)
                return;
        }

        private void cbAllHysteresis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67_83_99_115() < 0)
                return;
        }

        private void cbAllForceSquelch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67_83_99_115() < 0)
                return;
        }

        private int _WriteAddr68_84_100_116()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllLosThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(92, 68, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 84, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 100, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(92, 116, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllLosThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr68_84_100_116() < 0)
                return;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            byte[] data = {0x55};
            int rv;

            if (reading == true)
                return;

            bStoreIntoFlash.Enabled = false;
            rv = i2cWriteCB(92, 170, 1, data);

            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;

            return;
        }
    }
}
