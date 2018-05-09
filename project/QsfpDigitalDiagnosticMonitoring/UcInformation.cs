using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
            int i;

            InitializeComponent();

            for (i = 0; i < 4; i++) {
                cbRx4RateSelect.Items.Add(i);
                cbRx3RateSelect.Items.Add(i);
                cbRx2RateSelect.Items.Add(i);
                cbRx1RateSelect.Items.Add(i);
                cbTx4RateSelect.Items.Add(i);
                cbTx3RateSelect.Items.Add(i);
                cbTx2RateSelect.Items.Add(i);
                cbTx1RateSelect.Items.Add(i);
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

        private void _ParserAddr87(byte data)
        {
            cbRx1RateSelect.SelectedIndex = data & 0x03;
            cbRx2RateSelect.SelectedIndex = (data >> 2) & 0x03;
            cbRx3RateSelect.SelectedIndex = (data >> 4) & 0x03;
            cbRx4RateSelect.SelectedIndex = (data >> 6) & 0x03;
        }

        private void _ParserAddr88(byte data)
        {
            cbTx1RateSelect.SelectedIndex = data & 0x03;
            cbTx2RateSelect.SelectedIndex = (data >> 2) & 0x03;
            cbTx3RateSelect.SelectedIndex = (data >> 4) & 0x03;
            cbTx4RateSelect.SelectedIndex = (data >> 6) & 0x03;
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

            bRead.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            if (i2cReadCB(80, 0, 1, data) != 1)
                goto exit;

            tbIdentifier.Text = "0x" + data[0].ToString("X2");

            if (i2cReadCB(80, 2, 1, data) != 1)
                goto exit;

            _ParserAddr2(data[0]);

            if (i2cReadCB(80, 86, 1, data) != 1)
                goto exit;

            _ParserAddr86(data[0]);

            if (i2cReadCB(80, 87, 2, data) != 2)
                goto exit;

            _ParserAddr87(data[0]);
            _ParserAddr88(data[1]);

            if (i2cReadCB(80, 93, 1, data) != 1)
                goto exit;

            _ParserAddr93(data[0]);

            if (i2cWriteCB == null)
                goto exit;

            data[0] = 0;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            if (i2cReadCB(80, 128, 64, data) != 64)
                goto exit;

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

            if (i2cReadCB(80, 192, 32, data) != 32)
                goto exit;

            tbLinkCodes.Text = "0x" + data[0].ToString("X2");
            _ParserAddr193(data[1]);
            _ParserAddr194(data[2]);
            _ParserAddr195(data[3]);
            bATmp = new byte[16];
            System.Buffer.BlockCopy(data, 4, bATmp, 0, 16);
            tbVendorSn.Text = Encoding.Default.GetString(bATmp);
            bATmp = new byte[8];
            System.Buffer.BlockCopy(data, 20, bATmp, 0, 8);
            tbDateCode.Text = Encoding.Default.GetString(bATmp);
            tbDiagnosticMonitoringType.Text = "0x" + data[28].ToString("X2");
            tbEnhancedOptions.Text = "0x" + data[29].ToString("X2");
            tbCcExt.Text = "0x" + data[31].ToString("X2");

        exit:
            bRead.Enabled = true;
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

        private int _WriteAddr87()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbRx1RateSelect.SelectedIndex;
            data[0] |= (byte)(cbRx2RateSelect.SelectedIndex << 2);
            data[0] |= (byte)(cbRx3RateSelect.SelectedIndex << 4);
            data[0] |= (byte)(cbRx4RateSelect.SelectedIndex << 6);

            if (i2cWriteCB(80, 87, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr88()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbTx1RateSelect.SelectedIndex;
            data[0] |= (byte)(cbTx2RateSelect.SelectedIndex << 2);
            data[0] |= (byte)(cbTx3RateSelect.SelectedIndex << 4);
            data[0] |= (byte)(cbTx4RateSelect.SelectedIndex << 6);

            if (i2cWriteCB(80, 88, 1, data) < 0)
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

        public int WritePassword()
        {
            return _WriteAddr123();
        }

        private int _WriteUpPage0()
        {
            byte[] data = new byte[64];
            byte[] bATmp;
            string sTmp;
            int i, iTmp;
            ushort uSTmp;

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Array.Clear(data, 0, 64);
            if (tbUpPage0Identifier.Text.Length != 4 || tbUpPage0Identifier.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbUpPage0Identifier.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[0] = Convert.ToByte(iTmp);

            if (tbExtIdentifier.Text.Length != 4 || tbExtIdentifier.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbExtIdentifier.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[1] = Convert.ToByte(iTmp);

            if (tbConnector.Text.Length != 4 || tbConnector.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbConnector.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[2] = Convert.ToByte(iTmp);

            if (tb1040GEthernetCompliance.Text.Length != 4 || tb1040GEthernetCompliance.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tb1040GEthernetCompliance.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[3] = Convert.ToByte(iTmp);

            if (tbSonetCompliance.Text.Length != 4 || tbSonetCompliance.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbSonetCompliance.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[4] = Convert.ToByte(iTmp);

            if (tbSasSataCompliance.Text.Length != 4 || tbSasSataCompliance.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbSasSataCompliance.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[5] = Convert.ToByte(iTmp);

            if (tbGigabitEthernetCompliant.Text.Length != 4 || tbGigabitEthernetCompliant.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbGigabitEthernetCompliant.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[6] = Convert.ToByte(iTmp);

            if (tbFibreChannelLinkLength.Text.Length != 4 || tbFibreChannelLinkLength.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbFibreChannelLinkLength.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[7] = Convert.ToByte(iTmp);

            if (tbTransmitterTechnology.Text.Length != 4 || tbTransmitterTechnology.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbTransmitterTechnology.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[8] = Convert.ToByte(iTmp);

            if (tbFibreChannelTransmissionMedia.Text.Length != 4 || tbFibreChannelTransmissionMedia.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbFibreChannelTransmissionMedia.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[9] = Convert.ToByte(iTmp);

            if (tbFibreChannelSpeed.Text.Length != 4 || tbFibreChannelSpeed.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbFibreChannelSpeed.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[10] = Convert.ToByte(iTmp);

            if (tbEncoding.Text.Length != 4 || tbEncoding.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbEncoding.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[11] = Convert.ToByte(iTmp);

            if (tbBitRate.Text.Length != 4 || tbBitRate.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbBitRate.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[12] = Convert.ToByte(iTmp);

            if (cbExtendedRateSelect.Checked == true)
                data[13] = 0x01;

            iTmp = Int32.Parse(tbStandardSmFiberLength.Text);
            data[14] = Convert.ToByte(iTmp);

            iTmp = Int32.Parse(tbOm3Length.Text);
            data[15] = Convert.ToByte(iTmp);

            iTmp = Int32.Parse(tbOm2Length.Text);
            data[16] = Convert.ToByte(iTmp);

            iTmp = Int32.Parse(tbOm1Length.Text);
            data[17] = Convert.ToByte(iTmp);

            iTmp = Int32.Parse(tbCableAssemblyLength.Text);
            data[18] = Convert.ToByte(iTmp);

            if (tbDeviceTech.Text.Length != 4 || tbDeviceTech.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbDeviceTech.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[19] = Convert.ToByte(iTmp);

            bATmp = Encoding.ASCII.GetBytes(tbVendorName.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 20, bATmp.Length > 16 ? 16 : bATmp.Length);

            if (tbExtendedModuleCodes.Text.Length != 4 || tbExtendedModuleCodes.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbExtendedModuleCodes.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[36] = Convert.ToByte(iTmp);

            if (tbVendorOui.Text.StartsWith("0x"))
                sTmp = tbVendorOui.Text.Substring(2);
            else
                sTmp = tbVendorOui.Text;
            for (i = 0; i < sTmp.Length / 2; i++) {
                data[37 + i] = Convert.ToByte(sTmp.Substring(i * 2, 2), 16);
            }

            bATmp = Encoding.ASCII.GetBytes(tbVendorPn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 40, bATmp.Length > 16 ? 16 : bATmp.Length);

            bATmp = Encoding.ASCII.GetBytes(tbVendorRev.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 56, bATmp.Length > 2 ? 2 : bATmp.Length);

            try {
                iTmp = Int32.Parse(tbWavelength.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            try {
                uSTmp = Convert.ToUInt16(iTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[58] = bATmp[1];
            data[59] = bATmp[0];

            try {
                iTmp = Int32.Parse(tbWavelengthTolerance.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            try {
                uSTmp = Convert.ToUInt16(iTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[60] = bATmp[1];
            data[61] = bATmp[0];

            try {
                iTmp = Int32.Parse(tbMaxCaseTemp.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(iTmp);
            data[62] = bATmp[0];

            if (i2cWriteCB(80, 128, 63, data) < 0)
                return -1;

            Array.Clear(data, 0, 64);

            if (tbLinkCodes.Text.Length != 4 || tbLinkCodes.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbLinkCodes.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[0] = Convert.ToByte(iTmp);

            if (cbRxOutputAmplitudeProgramming.Checked == true)
                data[1] |= 0x01;

            if (cbRxSquelchDisableImplemented.Checked == true)
                data[2] |= 0x08;
            if (cbRxOutputDisableCapable.Checked == true)
                data[2] |= 0x04;
            if (cbTxSquelchDisableImplemented.Checked == true)
                data[2] |= 0x02;
            if (cbTxSquelchImplemented.Checked == true)
                data[2] |= 0x01;

            if (cbMemoryPage02Provided.Checked == true)
                data[3] |= 0x80;
            if (cbMemoryPage01Provided.Checked == true)
                data[3] |= 0x40;
            if (cbRateSelectImplemented.Checked == true)
                data[3] |= 0x20;
            if (cbTxDisableImplemented.Checked == true)
                data[3] |= 0x10;
            if (cbTxFaultSignalImplemented.Checked == true)
                data[3] |= 0x08;
            if (cbTxSquelchImplementedToReduceOma.Checked == true)
                data[3] |= 0x04;
            if (cbTxLossOfSignal.Checked == true)
                data[3] |= 0x02;

            bATmp = Encoding.ASCII.GetBytes(tbVendorSn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 4, bATmp.Length > 16 ? 16 : bATmp.Length);

            bATmp = Encoding.ASCII.GetBytes(tbDateCode.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 20, bATmp.Length > 8 ? 8 : bATmp.Length);

            if (tbDiagnosticMonitoringType.Text.Length != 4 || tbDiagnosticMonitoringType.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbDiagnosticMonitoringType.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[28] = Convert.ToByte(iTmp);

            if (tbEnhancedOptions.Text.Length != 4 || tbEnhancedOptions.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbEnhancedOptions.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[29] = Convert.ToByte(iTmp);

            if (i2cWriteCB(80, 192, 30, data) < 0)
                return -1;

            return 0;
        }

        private void _bWrite_Click(object sender, EventArgs e)
        {
            int rv;

            bWrite.Enabled = false;

            if (tbPassword.Text.Length != 4) {
                tbPassword.Text = "";
                MessageBox.Show("Please input 4 char password before write!!");
                goto exit;
            }

            if (_WriteAddr123() < 0)
                goto exit;

            rv = _WriteAddr119();
            if (rv < 0)
                goto exit;
            else if (rv == 1)
                goto exit;

            if (_WriteAddr86() < 0)
                goto exit;

            if (_WriteAddr87() < 0)
                goto exit;

            if (_WriteAddr88() < 0)
                goto exit;

            if (_WriteAddr93() < 0)
                goto exit;

            if (_WriteUpPage0() < 0)
                goto exit;

        exit:
            bWrite.Enabled = true;
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

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];

            bStoreIntoFlash.Enabled = false;

            if (_WriteAddr123() < 0)
                goto exit;

            if (_SetQsfpMode(0x4D) < 0)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            data[0] = 32;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            _SetQsfpMode(0);
            bStoreIntoFlash.Enabled = true;
        }

    }
}
