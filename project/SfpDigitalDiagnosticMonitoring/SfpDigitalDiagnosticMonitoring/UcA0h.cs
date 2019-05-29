using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SfpDigitalDiagnosticMonitoring
{
    public partial class UcA0h : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private I2cWritePasswordCB i2cWritePasswordCB = null;

        public UcA0h()
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

        public int SetI2cWritePasswordCBApi(I2cWritePasswordCB cb)
        {
            if (cb == null)
                return -1;

            i2cWritePasswordCB = new I2cWritePasswordCB(cb);

            return 0;
        }

        private int _SetSfpMode(byte mode)
        {
            byte[] data = new byte[] { 82 };

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(81, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(81, 164, 1, data) < 0)
                return -1;

            return 0;
        }

		private void _ParserAddr64(byte data)
		{
			if ((data & 0x20) == 0)
				cbHighPowreLevelDeclaration.Checked = false;
			else
				cbHighPowreLevelDeclaration.Checked = true;

			if ((data & 0x10) == 0)
				cbPaging.Checked = false;
			else
				cbPaging.Checked = true;

			if ((data & 0x08) == 0)
				cbRetimerCdr.Checked = false;
			else
				cbRetimerCdr.Checked = true;

			if ((data & 0x04) == 0)
				cbCooledTransceiverDeclaration.Checked = false;
			else
				cbCooledTransceiverDeclaration.Checked = true;

			if ((data & 0x02) == 0)
				cbPowerLevelDeclaration.Checked = false;
			else
				cbPowerLevelDeclaration.Checked = true;

			if ((data & 0x01) == 0)
				cbLinearReceiverOutput.Checked = false;
			else
				cbLinearReceiverOutput.Checked = true;
		}

		private void _ParserAddr65(byte data)
		{
			if ((data & 0x80) == 0)
				cbReceiverDecisionThreshold.Checked = false;
			else
				cbReceiverDecisionThreshold.Checked = true;

			if ((data & 0x40) == 0)
				cbTunableTransmitterTechnology.Checked = false;
			else
				cbTunableTransmitterTechnology.Checked = true;

			if ((data & 0x20) == 0)
				cbRateSelect.Checked = false;
			else
				cbRateSelect.Checked = true;

			if ((data & 0x10) == 0)
				cbTxDisableImplemented.Checked = false;
			else
				cbTxDisableImplemented.Checked = true;

			if ((data & 0x08) == 0)
				cbTxFaultSignalImplemented.Checked = false;
			else
				cbTxFaultSignalImplemented.Checked = true;

			if ((data & 0x04) == 0)
				cbSignalDetect.Checked = false;
			else
				cbSignalDetect.Checked = true;

			if ((data & 0x02) == 0)
				cbRxLos.Checked = false;
			else
				cbRxLos.Checked = true;
		}

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[96];
            byte[] bATmp = new byte[16];
            byte[] reverseData;
            UInt16 u16Tmp;

            bRead.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            if (i2cReadCB(80, 0, 96, data) != 96)
                goto exit;

            tbIdentifier.Text = "0x" + data[0].ToString("X2");
            tbExtIdentifier.Text = "0x" + data[1].ToString("X2");
			tbConnector.Text = "0x" + data[2].ToString("X2");
			tb10gEthernetInfiniband.Text = "0x" + data[3].ToString("X2");
			tbEsconCompliance.Text = "0x" + data[4].ToString("X2");
			tbSonetCompliance.Text = "0x" + data[5].ToString("X2");
			tbEthernetCompliant.Text = "0x" + data[6].ToString("X2");
			tbFibreChannelLinkLength.Text = "0x" + data[7].ToString("X2");
			tbFiberChannelTechnology.Text = "0x" + data[8].ToString("X2");
			tbFibreChannelTransmissionMedia.Text = "0x" + data[9].ToString("X2");
			tbFibreChannelSpeed.Text = "0x" + data[10].ToString("X2");
			tbEncoding.Text = "0x" + data[11].ToString("X2");
			tbBitRate.Text = "0x" + data[12].ToString("X2");
			tbRateIdentifier.Text = "0x" + data[13].ToString("X2");
			tbStandardSmFiberLengthKm.Text = data[14].ToString();
			tbStandardSmFiberLength100M.Text = data[15].ToString();
			tbOm2Length.Text = data[16].ToString();
			tbOm1Length.Text = data[17].ToString();
			tbOm4Length.Text = data[18].ToString();
			tbOm3Length.Text = data[19].ToString();
			System.Buffer.BlockCopy(data, 20, bATmp, 0, 16);
			tbVendorName.Text = Encoding.Default.GetString(bATmp);
            tbExtendedModuleCodes.Text = "0x" + data[36].ToString("X2");
			tbVendorOui.Text = "0x" + data[37].ToString("X2") +
				data[38].ToString("X2") + data[39].ToString("X2");
			System.Buffer.BlockCopy(data, 40, bATmp, 0, 16);
			tbVendorPn.Text = Encoding.Default.GetString(bATmp);
			bATmp = new byte[4];
			System.Buffer.BlockCopy(data, 56, bATmp, 0, 4);
			tbVendorRev.Text = Encoding.Default.GetString(bATmp);
			bATmp = new byte[2];
			System.Buffer.BlockCopy(data, 60, bATmp, 0, 2);
			reverseData = bATmp.Reverse().ToArray();
			u16Tmp = BitConverter.ToUInt16(reverseData, 0);
			tbWavelength.Text = u16Tmp.ToString();

			tbCcBase.Text = "0x" + data[63].ToString("X2");
			_ParserAddr64(data[64]);
			_ParserAddr65(data[65]);
			tbBitRateMax.Text = "0x" + data[66].ToString("X2");
			tbBitRateMin.Text = "0x" + data[67].ToString("X2");
			bATmp = new byte[16];
			System.Buffer.BlockCopy(data, 68, bATmp, 0, 16);
			tbVendorSn.Text = Encoding.Default.GetString(bATmp);
			bATmp = new byte[8];
			System.Buffer.BlockCopy(data, 84, bATmp, 0, 8);
			tbDateCode.Text = Encoding.Default.GetString(bATmp);
			tbDiagnosticMonitoringType.Text = "0x" + data[92].ToString("X2");
			tbEnhancedOptions.Text = "0x" + data[93].ToString("X2");
			tbSff8472.Text = "0x" + data[94].ToString("X2");
			tbCcExt.Text = "0x" + data[95].ToString("X2");

        exit:
            bRead.Enabled = true;
        }

		private void bWrite_Click(object sender, EventArgs e)
		{
			byte[] data = new byte[62];
			byte[] bATmp;
			string sTmp;
			int i, iTmp;
			ushort uSTmp;

            if (i2cWritePasswordCB == null)
                return;

            if (i2cWritePasswordCB() < 0)
                return;

            Thread.Sleep(1000);

			if (i2cWriteCB == null)
				return;

			bWrite.Enabled = false;

			if (_SetSfpMode(0x4D) < 0)
				goto exit;

            Thread.Sleep(1000);

			Array.Clear(data, 0, 62);

			if (tbIdentifier.Text.Length != 4 || tbIdentifier.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Identifier format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbIdentifier.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[0] = Convert.ToByte(iTmp);

			if (tbExtIdentifier.Text.Length != 4 || tbExtIdentifier.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Ext Identifier format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbExtIdentifier.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[1] = Convert.ToByte(iTmp);

			if (tbConnector.Text.Length != 4 || tbConnector.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Connector format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbConnector.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[2] = Convert.ToByte(iTmp);

			if (tb10gEthernetInfiniband.Text.Length != 4 || tb10gEthernetInfiniband.Text.ElementAt(1) != 'x') {
				MessageBox.Show("10G Ethernet, Infiniband format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tb10gEthernetInfiniband.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[3] = Convert.ToByte(iTmp);

			if (tbEsconCompliance.Text.Length != 4 || tbEsconCompliance.Text.ElementAt(1) != 'x') {
				MessageBox.Show("ESCON format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbEsconCompliance.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[4] = Convert.ToByte(iTmp);

			if (tbSonetCompliance.Text.Length != 4 || tbSonetCompliance.Text.ElementAt(1) != 'x') {
				MessageBox.Show("SONET format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbSonetCompliance.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[5] = Convert.ToByte(iTmp);

			if (tbEthernetCompliant.Text.Length != 4 || tbEthernetCompliant.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Ethernet format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbEthernetCompliant.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[6] = Convert.ToByte(iTmp);

			if (tbFibreChannelLinkLength.Text.Length != 4 || tbFibreChannelLinkLength.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Fiber Channel link length format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbFibreChannelLinkLength.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[7] = Convert.ToByte(iTmp);

			if (tbFiberChannelTechnology.Text.Length != 4 || tbFiberChannelTechnology.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Fiber Channel Technology format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbFiberChannelTechnology.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[8] = Convert.ToByte(iTmp);

			if (tbFibreChannelTransmissionMedia.Text.Length != 4 || tbFibreChannelTransmissionMedia.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Fiber Channel transmission media format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbFibreChannelTransmissionMedia.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[9] = Convert.ToByte(iTmp);

			if (tbFibreChannelSpeed.Text.Length != 4 || tbFibreChannelSpeed.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Fiber Channel Speed format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbFibreChannelSpeed.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[10] = Convert.ToByte(iTmp);

			if (tbEncoding.Text.Length != 4 || tbEncoding.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Encoding format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbEncoding.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[11] = Convert.ToByte(iTmp);
            
			if (tbBitRate.Text.Length != 4 || tbBitRate.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Bit Rate format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbBitRate.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[12] = Convert.ToByte(iTmp);

			if (tbRateIdentifier.Text.Length != 4 || tbRateIdentifier.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Rate Identifier format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbRateIdentifier.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[13] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbStandardSmFiberLengthKm.Text);
			data[14] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbStandardSmFiberLength100M.Text);
			data[15] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbOm2Length.Text);
			data[16] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbOm1Length.Text);
			data[17] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbOm4Length.Text);
			data[18] = Convert.ToByte(iTmp);

			iTmp = Int32.Parse(tbOm3Length.Text);
			data[19] = Convert.ToByte(iTmp);

			bATmp = Encoding.ASCII.GetBytes(tbVendorName.Text);
			System.Buffer.BlockCopy(bATmp, 0, data, 20, bATmp.Length > 16 ? 16 : bATmp.Length);

            if (tbExtendedModuleCodes.Text.Length != 4 || tbExtendedModuleCodes.Text.ElementAt(1) != 'x') {
                MessageBox.Show("Extended Compliance Codes format wrong!! Ex:0xXX");
                goto exit;
            }
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
			System.Buffer.BlockCopy(bATmp, 0, data, 56, bATmp.Length > 4 ? 4 : bATmp.Length);

			try {
				iTmp = Int32.Parse(tbWavelength.Text);
			}
			catch (Exception eTS) {
				MessageBox.Show(eTS.ToString());
				goto exit;
			}
			try {
				uSTmp = Convert.ToUInt16(iTmp);
			}
			catch (Exception eTI) {
				MessageBox.Show(eTI.ToString());
				goto exit;
			}
			bATmp = BitConverter.GetBytes(uSTmp);
			data[60] = bATmp[1];
			data[61] = bATmp[0];

			if (i2cWriteCB(80, 0, 62, data) < 0)
				goto exit;
            
			Array.Clear(data, 0, 62);

			if (cbLinearReceiverOutput.Checked == true)
				data[0] |= 0x01;
			if (cbPowerLevelDeclaration.Checked == true)
				data[0] |= 0x02;
			if (cbCooledTransceiverDeclaration.Checked == true)
				data[0] |= 0x04;
			if (cbRetimerCdr.Checked == true)
				data[0] |= 0x08;
			if (cbPaging.Checked == true)
				data[0] |= 0x10;
			if (cbHighPowreLevelDeclaration.Checked == true)
				data[0] |= 0x20;

			if (cbRxLos.Checked == true)
				data[1] |= 0x02;
			if (cbSignalDetect.Checked == true)
				data[1] |= 0x04;
			if (cbTxFaultSignalImplemented.Checked == true)
				data[1] |= 0x08;
			if (cbTxDisableImplemented.Checked == true)
				data[1] |= 0x10;
			if (cbRateSelect.Checked == true)
				data[1] |= 0x20;
			if (cbTunableTransmitterTechnology.Checked == true)
				data[1] |= 0x40;
			if (cbReceiverDecisionThreshold.Checked == true)
				data[1] |= 0x80;

			if (tbBitRateMax.Text.Length != 4 || tbBitRateMax.Text.ElementAt(1) != 'x') {
				MessageBox.Show("BR Max format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbBitRateMax.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[2] = Convert.ToByte(iTmp);

			if (tbBitRateMin.Text.Length != 4 || tbBitRateMin.Text.ElementAt(1) != 'x') {
				MessageBox.Show("BR Min format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbBitRateMin.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[3] = Convert.ToByte(iTmp);

			bATmp = Encoding.ASCII.GetBytes(tbVendorSn.Text);
			System.Buffer.BlockCopy(bATmp, 0, data, 4, bATmp.Length > 16 ? 16 : bATmp.Length);

			bATmp = Encoding.ASCII.GetBytes(tbDateCode.Text);
			System.Buffer.BlockCopy(bATmp, 0, data, 20, bATmp.Length > 8 ? 8 : bATmp.Length);

			if (tbDiagnosticMonitoringType.Text.Length != 4 || tbDiagnosticMonitoringType.Text.ElementAt(1) != 'x') {
				MessageBox.Show("DM Type format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbDiagnosticMonitoringType.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[28] = Convert.ToByte(iTmp);

			if (tbEnhancedOptions.Text.Length != 4 || tbEnhancedOptions.Text.ElementAt(1) != 'x') {
				MessageBox.Show("Enhanced Options format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbEnhancedOptions.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[29] = Convert.ToByte(iTmp);

			if (tbSff8472.Text.Length != 4 || tbSff8472.Text.ElementAt(1) != 'x') {
				MessageBox.Show("SFF-8472 format wrong!! Ex:0xXX");
				goto exit;
			}
			iTmp = Int32.Parse(tbSff8472.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
			data[30] = Convert.ToByte(iTmp);

			if (i2cWriteCB(80, 64, 31, data) < 0)
				goto exit;

		exit:
			bWrite.Enabled = true;
		}

		private void bStoreIntoFlash_Click(object sender, EventArgs e)
		{
            byte[] data = new byte[1];

            if (i2cWritePasswordCB == null)
                return;

            if (i2cWritePasswordCB() < 0)
                return;

            if (i2cWriteCB == null)
                return;

            _SetSfpMode(0x4D);

            bStoreIntoFlash.Enabled = false;

            data[0] = 82;
            if (i2cWriteCB(81, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(81, 162, 1, data) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            _SetSfpMode(0);
            bStoreIntoFlash.Enabled = true;
		}
    }
}
