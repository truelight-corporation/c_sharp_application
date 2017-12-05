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
    public partial class UcGn2149Config : UserControl
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

        public UcGn2149Config()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 4; i++) {
                cbVcoRangeOverride.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbLosHyst.Items.Add(i);
                cbDrvDeemphLevel.Items.Add(i);
            }

            for (i = 0; i < 64; i++) {
                cbLosThres.Items.Add(i);
                cbRssiHighFaultThres.Items.Add(i);
            }

            for (i = 0; i < 128; i++) {
                cbDrvPredrvCmRef.Items.Add(i);
                cbBbStep.Items.Add(i);
            }

            for (i = 0; i < 181; i++) {
                cbDrvMainSwing.Items.Add(i);
            }

            for (i = 0; i < 256; i++) {
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

        private void _ParseAddr003(byte data)
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

        private void _ParseAddr004(byte data)
        {
            cbLosThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr005(byte data)
        {
            cbLosHyst.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr009(byte data)
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

        private void _ParseAddr016(byte data)
        {
            if ((data & 0x01) == 0)
                cbDrvForceMute.Checked = false;
            else
                cbDrvForceMute.Checked = true;
        }

        private void _ParseAddr017(byte data)
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

        private void _ParseAddr018(byte data)
        {
            cbDrvMainSwing.SelectedIndex = data;
        }

        private void _ParseAddr019(byte data)
        {
            if ((data & 0x01) == 0)
                cbDrvInv.Checked = false;
            else
                cbDrvInv.Checked = true;
        }

        private void _ParseAddr01A(byte data)
        {
            cbDrvDeemphLevel.SelectedIndex = data & 0x0F;
        }

        private void _ParseAddr01C(byte data)
        {
            cbDrvPredrvCmRef.SelectedIndex = data & 0x7F;
        }

        private void _ParseAddr01D(byte data)
        {
            if ((data & 0x01) == 0)
                cbRssiHighFaultDetectEn.Checked = false;
            else
                cbRssiHighFaultDetectEn.Checked = true;
        }

        private void _ParseAddr01F(byte data)
        {
            if ((data & 0x01) == 0)
                cbChannelRssiHighAlarm.Checked = false;
            else
                cbChannelRssiHighAlarm.Checked = true;
        }

        private void _ParseAddr022(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdLane.Checked = false;
            else
                cbPdLane.Checked = true;
        }

        private void _ParseAddr023(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdLos.Checked = false;
            else
                cbPdLos.Checked = true;
        }

        private void _ParseAddr026(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdCdr.Checked = false;
            else
                cbPdCdr.Checked = true;
        }

        private void _ParseAddr028(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdDrv.Checked = false;
            else
                cbPdDrv.Checked = true;

            if ((data & 0x02) == 0)
                cbPdDrvDeemphDrv.Checked = false;
            else
                cbPdDrvDeemphDrv.Checked = true;
        }

        private void bReadChannel_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[5];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bReadChannel.Enabled = false;

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0003).Reverse().ToArray(), 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr003(data[0]);
            _ParseAddr004(data[1]);
            _ParseAddr005(data[2]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0009).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr009(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0016).Reverse().ToArray(), 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr016(data[0]);
            _ParseAddr017(data[1]);
            _ParseAddr018(data[2]);
            _ParseAddr019(data[3]);
            _ParseAddr01A(data[4]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x001C).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr01C(data[0]);
            _ParseAddr01D(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x001F).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr01F(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0022).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr022(data[0]);
            _ParseAddr023(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0026).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr026(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0028).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr028(data[0]);

        exit:
            reading = false;
            bReadChannel.Enabled = true;
        }

        private void _ParseAddr100(byte data)
        {
            if ((data & 0x01) == 0)
                cbTiaRssiDiv.Checked = false;
            else
                cbTiaRssiDiv.Checked = true;
        }

        private void _ParseAddr102(byte data)
        {
            if ((data & 0x01) == 0)
                cbFaultPolarity.Checked = false;
            else
                cbFaultPolarity.Checked = true;
        }

        private void _ParseAddr10A(byte data)
        {
            if ((data & 0x01) == 0)
                cbLoslInvert.Checked = false;
            else
                cbLoslInvert.Checked = true;
        }

        private void _ParseAddr116(byte data)
        {
            if ((data & 0x01) == 0)
                cbControlRssiHighAlarm.Checked = false;
            else
                cbControlRssiHighAlarm.Checked = true;
        }

        private void _ParseAddr117(byte data)
        {
            cbRssiHighFaultThres.SelectedIndex = data & 0x3F;
        }

        private void _ParseAddr119(byte data)
        {
            tbTempSensorTrim.Text = (data & 0x7F).ToString();
        }

        private void _ParseAddr11A_11B(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbTemperature.Text = iTmp.ToString();
        }

        private void _ParseAddr11C(byte data)
        {
            tbVcclSupply.Text = data.ToString();
        }

        private void _ParseAddr11D_11E(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp |= (data1 & 0x03) << 7;

            tbVccSupply.Text = iTmp.ToString();
        }

        private void _ParseAddr123(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcParamMonCtrlReset.Checked = false;
            else
                cbAdcParamMonCtrlReset.Checked = true;
        }

        private void _ParseAddr126(byte data)
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

        private void _ParseAddr13A(byte data)
        {
            tbBiasTrim.Text = data.ToString();
        }

        private void _ParseAddr13B(byte data)
        {
            cbBbStep.SelectedIndex = data;
        }

        private void _ParseAddr142(byte data)
        {
            if ((data & 0x01) == 0)
                cbPdAll.Checked = false;
            else
                cbPdAll.Checked = true;
        }

        private void _ParseAddr143(byte data)
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

        private void _ParseAddr146(byte data)
        {
            tbVersion.Text = data.ToString();
        }

        private void _ParseAddr156(byte data)
        {
            cbVcoRangeOverride.SelectedIndex = data & 0x03;
        }

        private void _ParseAddr159(byte data)
        {
            cbVcoRangeOverrideEn.SelectedIndex = data;
        }

        private void _ParseAddr178(byte data)
        {
            if ((data & 0x01) == 0)
                cbTiaToRssi1.Checked = false;
            else
                cbTiaToRssi1.Checked = true;
        }

        private void _ParseAddr179(byte data)
        {
            if ((data & 0x01) == 0)
                cbRxFault.Checked = false;
            else
                cbRxFault.Checked = true;
        }

        private void _ParseAddr17D(byte data)
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

        private void _ParseAddr17F(byte data)
        {
            if ((data & 0x01) == 0)
                cbLosChangeDetect.Checked = false;
            else
                cbLosChangeDetect.Checked = true;
        }

        private void _ParseAddr181(byte data)
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

        private void _ParseAddr182(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolInvert.Checked = false;
            else
                cbLolInvert.Checked = true;
        }

        private void _ParseAddr183(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolChangeDetect.Checked = false;
            else
                cbLolChangeDetect.Checked = true;
        }

        private void _ParseAddr185(byte data)
        {
            if ((data & 0x01) == 0)
                cbLolToLosEn.Checked = false;
            else
                cbLolToLosEn.Checked = true;

            if ((data & 0x02) == 0)
                cbLosToLosEn.Checked = false;
            else
                cbLosToLosEn.Checked = true;
        }

        private void _ParseAddr186(byte data)
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

        private void _ParseAddr187(byte data)
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
            byte[] data = new byte[6];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bReadChannel.Enabled = false;

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr100(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0102).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr102(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x010A).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr10A(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0116).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr116(data[0]);
            _ParseAddr117(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0119).Reverse().ToArray(), 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr119(data[0]);
            _ParseAddr11A_11B(data[1], data[2]);
            _ParseAddr11C(data[3]);
            _ParseAddr11D_11E(data[4], data[5]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0123).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr123(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0126).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr126(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x013A).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr13A(data[0]);
            _ParseAddr13B(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0142).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr142(data[0]);
            _ParseAddr143(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0146).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr146(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0156).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr156(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0159).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr159(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0178).Reverse().ToArray(), 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr178(data[0]);
            _ParseAddr179(data[1]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x017D).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr17D(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x017F).Reverse().ToArray(), 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr17F(data[0]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0181).Reverse().ToArray(), 3, data);
            if (rv != 2)
                goto exit;

            _ParseAddr181(data[0]);
            _ParseAddr182(data[1]);
            _ParseAddr183(data[2]);

            rv = i2cRead16CB(82, BitConverter.GetBytes((ushort)0x0185).Reverse().ToArray(), 3, data);
            if (rv != 2)
                goto exit;

            _ParseAddr185(data[0]);
            _ParseAddr186(data[1]);
            _ParseAddr187(data[2]);

        exit:
            reading = false;
            bReadChannel.Enabled = true;
        }

        private int _WriteAddr003()
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

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0003).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbLosThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0004).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbLosHyst.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0005).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr009()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbOffsetCorrectionPolarityInv.Checked == true)
                data[0] |= 0x01;
            if (cbEqPolarityInvert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0009).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr016()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbDrvForceMute.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0016).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr017()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbDrvMuteOnLos.Checked == true)
                data[0] |= 0x01;
            if (cbDrvPdOnMute.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0017).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbDrvMainSwing.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0018).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr019()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbDrvInv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0019).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbDrvDeemphLevel.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x001A).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbDrvPredrvCmRef.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x001C).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbRssiHighFaultDetectEn.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x001D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr01F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbChannelRssiHighAlarm.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x001D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr022()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdLane.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0022).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr023()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdLos.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0023).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr026()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdCdr.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0026).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr028()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdDrv.Checked == true)
                data[0] |= 0x01;
            if (cbPdDrvDeemphDrv.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0028).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrForceBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbCdrAutoBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbCdrPdOnBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr003() < 0)
                return;
        }

        private void cbLosThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr004() < 0)
                return;
        }

        private void cbLosHyst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr005() < 0)
                return;
        }

        private void cbOffsetCorrectionPolarityInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr009() < 0)
                return;
        }

        private void cbEqPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr009() < 0)
                return;
        }

        private void cbDrvForceMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr016() < 0)
                return;
        }

        private void cbDrvMuteOnLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr017() < 0)
                return;
        }

        private void cbDrvPdOnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr017() < 0)
                return;
        }
        private void cbDrvMainSwing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr018() < 0)
                return;
        }

        private void cbDrvInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr019() < 0)
                return;
        }

        private void cbDrvDeemphLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01A() < 0)
                return;
        }

        private void cbDrvPredrvCmRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01C() < 0)
                return;
        }

        private void cbRssiHighFaultDetectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01D() < 0)
                return;
        }

        private void cbChannelRssiHighAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr01F() < 0)
                return;
        }

        private void cbPdLane_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr022() < 0)
                return;
        }

        private void cbPdLos_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr023() < 0)
                return;
        }

        private void cbPdCdr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr026() < 0)
                return;
        }

        private void cbPdDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr028() < 0)
                return;
        }

        private void cbPdDrvDeemphDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr028() < 0)
                return;
        }

        private int _WriteAddr100()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTiaRssiDiv.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0100).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr102()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbFaultPolarity.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0102).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr10A()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLoslInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x010A).Reverse().ToArray(), 1, data);
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
            bTmp = Convert.ToByte(cbRssiHighFaultThres.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0117).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr123()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAdcParamMonCtrlReset.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0123).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr126()
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

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0126).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr13B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbBbStep.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x013B).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr142()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0142).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr143()
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

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0143).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr156()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcoRangeOverride.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0156).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr159()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbVcoRangeOverrideEn.SelectedItem);

            data[0] |= bTmp;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0159).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr178()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbPdAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0178).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr17D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosSoftAssertEn.Checked == true)
                data[0] |= 0x01;
            if (cbLosSoftAssert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x017D).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr17F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosChangeDetect.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x017F).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr181()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolSoftAssertEn.Checked == true)
                data[0] |= 0x01;
            if (cbLolSoftAssert.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0181).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr182()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolInvert.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0182).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr183()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolChangeDetect.Checked == true)
                data[0] |= 0x01;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0183).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr185()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLolToLosEn.Checked == true)
                data[0] |= 0x01;
            if (cbLosToLosEn.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0185).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr187()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbLosLatch.Checked == true)
                data[0] |= 0x01;
            if (cbLolLatch.Checked == true)
                data[0] |= 0x02;

            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x0187).Reverse().ToArray(), 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTiaRssiDiv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100() < 0)
                return;
        }

        private void cbFaultPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr102() < 0)
                return;
        }

        private void cbLoslInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10A() < 0)
                return;
        }

        private void cbRssiHighFaultThres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr117() < 0)
                return;
        }

        private void cbAdcParamMonCtrlReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr123() < 0)
                return;
        }

        private void cbBbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13B() < 0)
                return;
        }

        private void cbPdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr142() < 0)
                return;
        }

        private void cbPdTempSense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbPdSupplySense_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbPdAdc_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void cbVcoRangeOverride_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr156() < 0)
                return;
        }

        private void cbVcoRangeOverrideEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr159() < 0)
                return;
        }

        private void cbTiaToRssi1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr178() < 0)
                return;
        }

        private void cbLosSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17D() < 0)
                return;
        }

        private void cbLosSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17D() < 0)
                return;
        }

        private void cbLosChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17F() < 0)
                return;
        }

        private void cbLolSoftAssertEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr181() < 0)
                return;
        }

        private void cbLolSoftAssert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr181() < 0)
                return;
        }

        private void cbLolInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr182() < 0)
                return;
        }

        private void cbLolChangeDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr183() < 0)
                return;
        }

        private void cbLolToLosEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr185() < 0)
                return;
        }

        private void cbLosToLosEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr185() < 0)
                return;
        }

        private void cbLosLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr187() < 0)
                return;
        }

        private void cbLolLatch_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr187() < 0)
                return;
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            bReadAll.Enabled = false;
            bReadChannel_Click(sender, e);
            bReadControl_Click(sender, e);
            bReadAll.Enabled = true;
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            int rv;

            bStoreIntoFlash.Enabled = false;
            rv = i2cWrite16CB(82, BitConverter.GetBytes((ushort)0x05AA).Reverse().ToArray(), 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
        }
    }
}
