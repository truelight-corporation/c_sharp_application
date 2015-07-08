using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QsfpDigitalDiagnosticMonitoring
{
    public partial class UcInformation : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;

        public UcInformation()
        {
            InitializeComponent();
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

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private void _ParserAddr2(byte data)
        {
            if ((data & 0x04) == 0)
                cbFlatMem.Checked = false;
            else
                cbFlatMem.Checked = true;

            if ((data & 0x02) == 0)
                cbIntL.Checked = false;
            else
                cbIntL.Checked = true;
            
            if ((data & 0x01) == 0)
                cbDataNotReady.Checked = false;
            else
                cbDataNotReady.Checked = true;
        }

        private void _ParserAddr86(byte data)
        {
            if ((data & 0x08) == 0)
                cbTx4Disable.Checked = false;
            else
                cbTx4Disable.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3Disable.Checked = false;
            else
                cbTx3Disable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2Disable.Checked = false;
            else
                cbTx2Disable.Checked = true;

            if ((data & 0x01) == 0)
                cbTx1Disable.Checked = false;
            else
                cbTx1Disable.Checked = true;
        }

        private void _ParserAddr93(byte data)
        {
            if ((data & 0x02) == 0)
                cbPowerSet.Checked = false;
            else
                cbPowerSet.Checked = true;
            
            if ((data & 0x01) == 0)
                cbPowerOverride.Checked = false;
            else
                cbPowerOverride.Checked = true;
        }

        private void _ParserAddr141(byte data)
        {
            if ((data & 0x01) == 0)
                cbExtendedRateSelect.Checked = false;
            else
                cbExtendedRateSelect.Checked = true;
        }

        private void _ParserAddr193(byte data)
        {
            if ((data & 0x01) == 0)
                cbRxOutputAmplitudeProgramming.Checked = false;
            else
                cbRxOutputAmplitudeProgramming.Checked = true;
        }

        private void _ParserAddr194(byte data)
        {
            if ((data & 0x08) == 0)
                cbRxSquelchDisableImplemented.Checked = false;
            else
                cbRxSquelchDisableImplemented.Checked = true;
            
            if ((data & 0x04) == 0)
                cbRxOutputDisableCapable.Checked = false;
            else
                cbRxOutputDisableCapable.Checked = true;
            
            if ((data & 0x02) == 0)
                cbTxSquelchDisableImplemented.Checked = false;
            else
                cbTxSquelchDisableImplemented.Checked = true;
            
            if ((data & 0x01) == 0)
                cbTxSquelchImplemented.Checked = false;
            else
                cbTxSquelchImplemented.Checked = true;
        }

        private void _ParserAddr195(byte data)
        {
            if ((data & 0x80) == 0)
                cbMemoryPage02Provided.Checked = false;
            else
                cbMemoryPage02Provided.Checked = true;

            if ((data & 0x40) == 0)
                cbMemoryPage01Provided.Checked = false;
            else
                cbMemoryPage01Provided.Checked = true;

            if ((data & 0x20) == 0)
                cbRateSelectImplemented.Checked = false;
            else
                cbRateSelectImplemented.Checked = true;

            if ((data & 0x10) == 0)
                cbTxDisableImplemented.Checked = false;
            else
                cbTxDisableImplemented.Checked = true;

            if ((data & 0x08) == 0)
                cbTxFaultSignalImplemented.Checked = false;
            else
                cbTxFaultSignalImplemented.Checked = true;

            if ((data & 0x04) == 0)
                cbTxSquelchImplementedToReduceOma.Checked = false;
            else
                cbTxSquelchImplementedToReduceOma.Checked = true;

            if ((data & 0x02) == 0)
                cbTxLossOfSignal.Checked = false;
            else
                cbTxLossOfSignal.Checked = true;
        }

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[64];
            byte[] bATmp = new byte[16];
            byte[] reverseData;
            UInt16 u16Tmp;

            if (i2cReadCB == null)
                return;

            if (i2cReadCB(80, 0, 1, data) != 1)
                return;

            tbIdentifier.Text = "0x" + data[0].ToString("X2");

            if (i2cReadCB(80, 2, 1, data) != 1)
                return;

            _ParserAddr2(data[0]);

            if (i2cReadCB(80, 86, 1, data) != 1)
                return;

            _ParserAddr86(data[0]);

            if (i2cReadCB(80, 93, 1, data) != 1)
                return;

            _ParserAddr93(data[0]);

            if (i2cWriteCB == null)
                return;

            data[0] = 0;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return;

            if (i2cReadCB(80, 128, 64, data) != 64)
                return;

            tbUpPage0Identifier.Text = "0x" + data[0].ToString("X2");
            tbExtIdentifier.Text = "0x" + data[1].ToString("X2");
            tbConnector.Text = "0x" + data[2].ToString("X2");
            tb1040GEthernetCompliance.Text = "0x" + data[3].ToString("X2");
            tbSonetCompliance.Text = "0x" + data[4].ToString("X2");
            tbSasSataCompliance.Text = "0x" + data[5].ToString("X2");
            tbGigabitEthernetCompliant.Text = "0x" + data[6].ToString("X2");
            tbFibreChannelLinkLength.Text = "0x" + data[7].ToString("X2");
            tbTransmitterTechnology.Text = "0x" + data[8].ToString("X2");
            tbFibreChannelTransmissionMedia.Text = "0x" + data[9].ToString("X2");
            tbFibreChannelSpeed.Text = "0x" + data[10].ToString("X2");
            tbEncoding.Text = "0x" + data[11].ToString("X2");
            tbBitRate.Text = "0x" + data[12].ToString("X2");
            _ParserAddr141(data[13]);
            tbStandardSmFiberLength.Text = data[14].ToString();
            tbOm3Length.Text = data[15].ToString();
            tbOm2Length.Text = data[16].ToString();
            tbOm1Length.Text = data[17].ToString();
            tbCableAssemblyLength.Text = data[18].ToString();
            tbDeviceTech.Text = "0x" + data[19].ToString("X2");
            System.Buffer.BlockCopy(data, 20, bATmp, 0, 16);
            tbVendorName.Text = Encoding.Default.GetString(bATmp);
            tbExtendedModuleCodes.Text = "0x" + data[36].ToString("X2");
            tbVendorOui.Text = "0x" + data[37].ToString("X2") +
                data[38].ToString("X2") + data[39].ToString("X2");
            System.Buffer.BlockCopy(data, 40, bATmp, 0, 16);
            tbVendorPn.Text = Encoding.Default.GetString(bATmp);
            bATmp = new byte[2];
            System.Buffer.BlockCopy(data, 56, bATmp, 0, 2);
            tbVendorRev.Text = Encoding.Default.GetString(bATmp);
            System.Buffer.BlockCopy(data, 58, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            u16Tmp = BitConverter.ToUInt16(reverseData, 0);
            tbWavelength.Text = u16Tmp.ToString();
            System.Buffer.BlockCopy(data, 60, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            u16Tmp = BitConverter.ToUInt16(reverseData, 0);
            tbWavelengthTolerance.Text = u16Tmp.ToString();
            tbMaxCaseTemp.Text = data[62].ToString();
            tbCcBase.Text = "0x" + data[63].ToString("X2");

            if (i2cReadCB(80, 193, 30, data) != 30)
                return;

            _ParserAddr193(data[0]);
            _ParserAddr194(data[1]);
            _ParserAddr195(data[2]);
            bATmp = new byte[16];
            System.Buffer.BlockCopy(data, 3, bATmp, 0, 16);
            tbVendorSn.Text = Encoding.Default.GetString(bATmp);
            bATmp = new byte[8];
            System.Buffer.BlockCopy(data, 19, bATmp, 0, 8);
            tbDateCode.Text = Encoding.Default.GetString(bATmp);
            tbDiagnosticMonitoringType.Text = "0x" + data[27].ToString("X2");
            tbEnhancedOptions.Text = "0x" + data[28].ToString("X2");
            tbCcExt.Text = "0x" + data[29].ToString("X2");
        }

        private int _WriteAddr86()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if ((cbTx4Disable.Checked == true))
                data[0] |= 0x08;

            if ((cbTx3Disable.Checked == true))
                data[0] |= 0x04;

            if ((cbTx2Disable.Checked == true))
                data[0] |= 0x02;

            if ((cbTx1Disable.Checked == true))
                data[0] |= 0x01;

            if (i2cWriteCB(80, 86, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr93()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if ((cbPowerSet.Checked == true))
                data[0] |= 0x02;

            if ((cbPowerOverride.Checked == true))
                data[0] |= 0x01;

            if (i2cWriteCB(80, 93, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr119()
        {
            byte[] data;

            if (i2cWriteCB == null)
                return -1;

            if (tbNewPassword.Text.Length == 0)
                return 0;

            if (tbNewPassword.Text.Length != 4) {
                tbNewPassword.Text = "";
                MessageBox.Show("Please input 4 char in new password!!");
                return -1;
            }

            data = Encoding.Default.GetBytes(tbNewPassword.Text);

            if (i2cWriteCB(80, 119, 4, data) < 0)
                return -1;

            tbPassword.Text = tbNewPassword.Text = "";
            MessageBox.Show("Password changed.");        

            return 1;
        }

        private int _WriteAddr123()
        {
            byte[] data;

            if (i2cWriteCB == null)
                return -1;

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (i2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteUpPage2Addr138_188()
        {
            byte[] data = new byte[64];
            byte[] bATmp;
            string sTmp;
            int i;

            if (i2cWriteCB == null)
                return -1;

            data[0] = 2;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Array.Clear(data, 0, 64);
            bATmp = Encoding.ASCII.GetBytes(tbVendorName.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 0, bATmp.Length);

            if (tbVendorOui.Text.StartsWith("0x"))
                sTmp = tbVendorOui.Text.Substring(2);
            else
                sTmp = tbVendorOui.Text;
            for (i = 0; i < sTmp.Length / 2; i++) {
                data[16 + i] = Convert.ToByte(sTmp.Substring(i * 2, 2), 16);
            }

            bATmp = Encoding.ASCII.GetBytes(tbVendorPn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 19, bATmp.Length);
            
            bATmp = Encoding.ASCII.GetBytes(tbVendorSn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 35, bATmp.Length);

            if (i2cWriteCB(80, 138, 51, data) < 0)
                return -1;

            return 0;
        }

        private void _bWrite_Click(object sender, EventArgs e)
        {
            int rv;

            if (tbPassword.Text.Length != 4) {
                tbPassword.Text = "";
                MessageBox.Show("Please input 4 char password before write!!");
                return;
            }

            if (_WriteAddr123() < 0)
                return;

            rv = _WriteAddr119();
            if (rv < 0)
                return;
            else if (rv == 1)
                return;

            if (_SetQsfpMode(0x4D) < 0)
                return;

            if (_WriteAddr86() < 0)
                return;

            if (_WriteAddr93() < 0)
                return;

            if (_WriteUpPage2Addr138_188() < 0)
                return;
        }

        private void _bPasswordReset_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return;

            data[0] = 32;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return;

            data[0] = 0x50;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                return;

            tbPassword.Text = "";
            MessageBox.Show("QSFP+ password reseted");
        }

    }
}
