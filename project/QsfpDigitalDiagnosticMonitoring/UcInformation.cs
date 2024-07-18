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
        public delegate int GetPasswordCB(int length, byte[] data);
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private GetPasswordCB getPasswordCB = null;
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
                cbRxOutputEmphasisType.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbInputEqualizationTx1.Items.Add(i);
                cbInputEqualizationTx2.Items.Add(i);
                cbInputEqualizationTx3.Items.Add(i);
                cbInputEqualizationTx4.Items.Add(i);
                cbRxOutputEmphasisRx1.Items.Add(i);
                cbRxOutputEmphasisRx2.Items.Add(i);
                cbRxOutputEmphasisRx3.Items.Add(i);
                cbRxOutputEmphasisRx4.Items.Add(i);
                cbOutputAmplitudeRx1.Items.Add(i);
                cbOutputAmplitudeRx2.Items.Add(i);
                cbOutputAmplitudeRx3.Items.Add(i);
                cbOutputAmplitudeRx4.Items.Add(i);
            }

            for (i = 0; i < 255; i++) {
                cbApplicationSelectRx1.Items.Add(i);
                cbApplicationSelectRx2.Items.Add(i);
                cbApplicationSelectRx3.Items.Add(i);
                cbApplicationSelectRx4.Items.Add(i);
                cbApplicationSelectTx1.Items.Add(i);
                cbApplicationSelectTx2.Items.Add(i);
                cbApplicationSelectTx3.Items.Add(i);
                cbApplicationSelectTx4.Items.Add(i);
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

        public int SetGetPasswordCBApi(GetPasswordCB cb)
        {
            if (cb == null)
                return -1;

            getPasswordCB = new GetPasswordCB(cb);

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

        private void _ParserAddr89(byte data)
        {
            cbApplicationSelectRx4.SelectedIndex = data;
        }

        private void _ParserAddr90(byte data)
        {
            cbApplicationSelectRx3.SelectedIndex = data;
        }

        private void _ParserAddr91(byte data)
        {
            cbApplicationSelectRx2.SelectedIndex = data;
        }

        private void _ParserAddr92(byte data)
        {
            cbApplicationSelectRx1.SelectedIndex = data;
        }

        private void _ParserAddr93(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerOverride.Checked = false;
            else
                cbPowerOverride.Checked = true;

            if ((data & 0x02) == 0)
                cbPowerSet.Checked = false;
            else
                cbPowerSet.Checked = true;

            if ((data & 0x04) == 0)
                cbHighPowerClassEnable.Checked = false;
            else
                cbHighPowerClassEnable.Checked = true;
        }

        private void _ParserAddr94(byte data)
        {
            cbApplicationSelectTx4.SelectedIndex = data;
        }

        private void _ParserAddr95(byte data)
        {
            cbApplicationSelectTx3.SelectedIndex = data;
        }

        private void _ParserAddr96(byte data)
        {
            cbApplicationSelectTx2.SelectedIndex = data;
        }

        private void _ParserAddr97(byte data)
        {
            cbApplicationSelectTx1.SelectedIndex = data;
        }

        private void _ParserAddr98(byte data)
        {
            if ((data & 0x01) == 0)
                cbCdrOnRx1.Checked = false;
            else
                cbCdrOnRx1.Checked = true;

            if ((data & 0x02) == 0)
                cbCdrOnRx2.Checked = false;
            else
                cbCdrOnRx2.Checked = true;

            if ((data & 0x04) == 0)
                cbCdrOnRx3.Checked = false;
            else
                cbCdrOnRx3.Checked = true;

            if ((data & 0x08) == 0)
                cbCdrOnRx4.Checked = false;
            else
                cbCdrOnRx4.Checked = true;

            if ((data & 0x10) == 0)
                cbCdrOnTx1.Checked = false;
            else
                cbCdrOnTx1.Checked = true;

            if ((data & 0x20) == 0)
                cbCdrOnTx2.Checked = false;
            else
                cbCdrOnTx2.Checked = true;

            if ((data & 0x40) == 0)
                cbCdrOnTx3.Checked = false;
            else
                cbCdrOnTx3.Checked = true;

            if ((data & 0x80) == 0)
                cbCdrOnTx4.Checked = false;
            else
                cbCdrOnTx4.Checked = true;
        }

        private void _ParserAddr110(byte data)
        {

            tbMinOperatingVoltage.Text = "0x" + (data & 0x07).ToString("X2");

            if ((data & 0x08) == 0)
                cbFarSideManaged.Checked = false;
            else
                cbFarSideManaged.Checked = true;

            tbAdvaancedLowPowerMode.Text = "0x" + ((data & 0xF0) >> 4).ToString("X2");
        }

        private void _ParserAddr113(byte data)
        {
            tbNearFarEndImplementation.Text = "0x" + data.ToString("X2");
        }

        private void _ParserAddr141(byte data)
        {
            if ((data & 0x01) == 0)
                cbExtendedRateSelectV1.Checked = false;
            else
                cbExtendedRateSelectV1.Checked = true;

            if ((data & 0x02) == 0)
                cbExtendedRateSelectV2.Checked = false;
            else
                cbExtendedRateSelectV2.Checked = true;
        }

        private void _ParserAddr193(byte data)
        {
            if ((data & 0x01) == 0)
                cbRxOutputAmplitudeProgramming.Checked = false;
            else
                cbRxOutputAmplitudeProgramming.Checked = true;

            if ((data & 0x02) == 0)
                cbRxOutputEmphasisProgramming.Checked = false;
            else
                cbRxOutputEmphasisProgramming.Checked = true;

            if ((data & 0x04) == 0)
                cbTxInputEqualizationProgrammable.Checked = false;
            else
                cbTxInputEqualizationProgrammable.Checked = true;

            if ((data & 0x08) == 0)
                cbTxInputEqualizationAutoAdaptive.Checked = false;
            else
                cbTxInputEqualizationAutoAdaptive.Checked = true;
        }

        private void _ParserAddr194(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxSquelchImplemented.Checked = false;
            else
                cbTxSquelchImplemented.Checked = true;

            if ((data & 0x02) == 0)
                cbTxSquelchDisableImplemented.Checked = false;
            else
                cbTxSquelchDisableImplemented.Checked = true;

            if ((data & 0x04) == 0)
                cbRxOutputDisableCapable.Checked = false;
            else
                cbRxOutputDisableCapable.Checked = true;

            if ((data & 0x08) == 0)
                cbRxSquelchDisableImplemented.Checked = false;
            else
                cbRxSquelchDisableImplemented.Checked = true;

            if ((data & 0x10) == 0)
                cbRxCdrLolFlag.Checked = false;
            else
                cbRxCdrLolFlag.Checked = true;

            if ((data & 0x20) == 0)
                cbTxCdrLolFlag.Checked = false;
            else
                cbTxCdrLolFlag.Checked = true;

            if ((data & 0x40) == 0)
                cbRxCdrOnOffControl.Checked = false;
            else
                cbRxCdrOnOffControl.Checked = true;

            if ((data & 0x80) == 0)
                cbTxCdrOnOffControl.Checked = false;
            else
                cbTxCdrOnOffControl.Checked = true;
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

        private void _ParserPage3Addr224(byte data)
        {
            if ((data & 0x01) == 0)
                cbRxOutputEmphasisMagnitudeRx1.Checked = false;
            else
                cbRxOutputEmphasisMagnitudeRx1.Checked = true;

            if ((data & 0x02) == 0)
                cbRxOutputEmphasisMagnitudeRx2.Checked = false;
            else
                cbRxOutputEmphasisMagnitudeRx2.Checked = true;

            if ((data & 0x04) == 0)
                cbRxOutputEmphasisMagnitudeRx3.Checked = false;
            else
                cbRxOutputEmphasisMagnitudeRx3.Checked = true;

            if ((data & 0x08) == 0)
                cbRxOutputEmphasisMagnitudeRx4.Checked = false;
            else
                cbRxOutputEmphasisMagnitudeRx4.Checked = true;

            if ((data & 0x10) == 0)
                cbInputEqualizationMagnitudeTx1.Checked = false;
            else
                cbInputEqualizationMagnitudeTx1.Checked = true;

            if ((data & 0x20) == 0)
                cbInputEqualizationMagnitudeTx2.Checked = false;
            else
                cbInputEqualizationMagnitudeTx2.Checked = true;

            if ((data & 0x40) == 0)
                cbInputEqualizationMagnitudeTx3.Checked = false;
            else
                cbInputEqualizationMagnitudeTx3.Checked = true;

            if ((data & 0x80) == 0)
                cbInputEqualizationMagnitudeTx4.Checked = false;
            else
                cbInputEqualizationMagnitudeTx4.Checked = true;
        }

        private void _ParserPage3Addr225(byte data)
        {
            if ((data & 0x01) == 0)
                cbOutputAmplitudeSupportRx1.Checked = false;
            else
                cbOutputAmplitudeSupportRx1.Checked = true;

            if ((data & 0x02) == 0)
                cbOutputAmplitudeSupportRx2.Checked = false;
            else
                cbOutputAmplitudeSupportRx2.Checked = true;

            if ((data & 0x04) == 0)
                cbOutputAmplitudeSupportRx3.Checked = false;
            else
                cbOutputAmplitudeSupportRx3.Checked = true;

            if ((data & 0x08) == 0)
                cbOutputAmplitudeSupportRx4.Checked = false;
            else
                cbOutputAmplitudeSupportRx4.Checked = true;

            cbRxOutputEmphasisType.SelectedIndex = (data & 0x30) >> 4;
        }

        private void _ParserPage3Addr234(byte data)
        {
            cbInputEqualizationTx1.SelectedIndex = (data >> 4) & 0x0F;
            cbInputEqualizationTx2.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr235(byte data)
        {
            cbInputEqualizationTx3.SelectedIndex = (data >> 4) & 0x0F;
            cbInputEqualizationTx4.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr236(byte data)
        {
            cbRxOutputEmphasisRx1.SelectedIndex = (data >> 4) & 0x0F;
            cbRxOutputEmphasisRx2.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr237(byte data)
        {
            cbRxOutputEmphasisRx3.SelectedIndex = (data >> 4) & 0x0F;
            cbRxOutputEmphasisRx4.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr238(byte data)
        {
            cbOutputAmplitudeRx1.SelectedIndex = (data >> 4) & 0x0F;
            cbOutputAmplitudeRx2.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr239(byte data)
        {
            cbOutputAmplitudeRx3.SelectedIndex = (data >> 4) & 0x0F;
            cbOutputAmplitudeRx4.SelectedIndex = data & 0x0F;
        }

        private void _ParserPage3Addr240(byte data)
        {
            if ((data & 0x01) == 0)
                cbSqDisableTx1.Checked = false;
            else
                cbSqDisableTx1.Checked = true;

            if ((data & 0x02) == 0)
                cbSqDisableTx2.Checked = false;
            else
                cbSqDisableTx2.Checked = true;

            if ((data & 0x04) == 0)
                cbSqDisableTx3.Checked = false;
            else
                cbSqDisableTx3.Checked = true;

            if ((data & 0x08) == 0)
                cbSqDisableTx4.Checked = false;
            else
                cbSqDisableTx4.Checked = true;

            if ((data & 0x10) == 0)
                cbSqDisableRx1.Checked = false;
            else
                cbSqDisableRx1.Checked = true;

            if ((data & 0x20) == 0)
                cbSqDisableRx2.Checked = false;
            else
                cbSqDisableRx2.Checked = true;

            if ((data & 0x40) == 0)
                cbSqDisableRx3.Checked = false;
            else
                cbSqDisableRx3.Checked = true;

            if ((data & 0x80) == 0)
                cbSqDisableRx4.Checked = false;
            else
                cbSqDisableRx4.Checked = true;
        }

        private void _ParserPage3Addr241(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdaptiveEqualizationControlTx1.Checked = false;
            else
                cbAdaptiveEqualizationControlTx1.Checked = true;

            if ((data & 0x02) == 0)
                cbAdaptiveEqualizationControlTx2.Checked = false;
            else
                cbAdaptiveEqualizationControlTx2.Checked = true;

            if ((data & 0x04) == 0)
                cbAdaptiveEqualizationControlTx3.Checked = false;
            else
                cbAdaptiveEqualizationControlTx3.Checked = true;

            if ((data & 0x08) == 0)
                cbAdaptiveEqualizationControlTx4.Checked = false;
            else
                cbAdaptiveEqualizationControlTx4.Checked = true;

            if ((data & 0x10) == 0)
                cbOutputDisableRx1.Checked = false;
            else
                cbOutputDisableRx1.Checked = true;

            if ((data & 0x20) == 0)
                cbOutputDisableRx2.Checked = false;
            else
                cbOutputDisableRx2.Checked = true;

            if ((data & 0x40) == 0)
                cbOutputDisableRx3.Checked = false;
            else
                cbOutputDisableRx3.Checked = true;

            if ((data & 0x80) == 0)
                cbOutputDisableRx4.Checked = false;
            else
                cbOutputDisableRx4.Checked = true;
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

            if (i2cReadCB(80, 86, 1, data) != 1)
                goto exit;

            _ParserAddr86(data[0]);

            if (i2cReadCB(80, 87, 12, data) != 12)
                goto exit;

            _ParserAddr87(data[0]);
            _ParserAddr88(data[1]);
            _ParserAddr89(data[2]);
            _ParserAddr90(data[3]);
            _ParserAddr91(data[4]);
            _ParserAddr92(data[5]);
            _ParserAddr93(data[6]);
            _ParserAddr94(data[7]);
            _ParserAddr95(data[8]);
            _ParserAddr96(data[9]);
            _ParserAddr97(data[10]);
            _ParserAddr98(data[11]);

            if (i2cReadCB(80, 108, 6, data) != 6)
                goto exit;

            tbPropagationDelay.Text = "0x" + data[0].ToString("X2") +
                data[1].ToString("X2");

            _ParserAddr110(data[2]);

            tbPciExpress.Text = "0x" + data[3].ToString("X2") +
                data[4].ToString("X2");

            _ParserAddr113(data[5]);

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
            tbBRNominal.Text = "0x" + data[30].ToString("X2");
            tbCcExt.Text = "0x" + data[31].ToString("X2");

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            if (i2cReadCB(80, 224, 2, data) != 2)
                goto exit;

            _ParserPage3Addr224(data[0]);
            _ParserPage3Addr225(data[1]);

            if (i2cReadCB(80, 234, 8, data) != 8)
                goto exit;

            _ParserPage3Addr234(data[0]);
            _ParserPage3Addr235(data[1]);
            _ParserPage3Addr236(data[2]);
            _ParserPage3Addr237(data[3]);
            _ParserPage3Addr238(data[4]);
            _ParserPage3Addr239(data[5]);
            _ParserPage3Addr240(data[6]);
            _ParserPage3Addr241(data[7]);

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

        private int _WriteAddr89()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectRx4.SelectedIndex;

            if (i2cWriteCB(80, 89, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr90()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectRx3.SelectedIndex;

            if (i2cWriteCB(80, 90, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr91()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectRx2.SelectedIndex;

            if (i2cWriteCB(80, 91, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr92()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectRx1.SelectedIndex;

            if (i2cWriteCB(80, 92, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr93()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if ((cbHighPowerClassEnable.Checked == true))
                data[0] |= 0x04;

            if ((cbPowerSet.Checked == true))
                data[0] |= 0x02;

            if ((cbPowerOverride.Checked == true))
                data[0] |= 0x01;

            if (i2cWriteCB(80, 93, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr94()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectTx4.SelectedIndex;

            if (i2cWriteCB(80, 94, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr95()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectTx3.SelectedIndex;

            if (i2cWriteCB(80, 95, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr96()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectTx2.SelectedIndex;

            if (i2cWriteCB(80, 96, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr97()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = (byte)cbApplicationSelectTx1.SelectedIndex;

            if (i2cWriteCB(80, 97, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr98()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if ((cbCdrOnTx4.Checked == true))
                data[0] |= 0x80;

            if ((cbCdrOnTx3.Checked == true))
                data[0] |= 0x40;

            if ((cbCdrOnTx2.Checked == true))
                data[0] |= 0x20;

            if ((cbCdrOnTx1.Checked == true))
                data[0] |= 0x10;

            if ((cbCdrOnRx4.Checked == true))
                data[0] |= 0x08;

            if ((cbCdrOnRx3.Checked == true))
                data[0] |= 0x04;

            if ((cbCdrOnRx2.Checked == true))
                data[0] |= 0x02;

            if ((cbCdrOnRx1.Checked == true))
                data[0] |= 0x01;

            if (i2cWriteCB(80, 98, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr108()
        {
            byte[] data = new byte[2];
            string sTmp;
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (tbPropagationDelay.Text.StartsWith("0x"))
                sTmp = tbPropagationDelay.Text.Substring(2);
            else
                sTmp = tbPropagationDelay.Text;
            for (i = 0; i < sTmp.Length / 2; i++)
            {
                data[i] = Convert.ToByte(sTmp.Substring(i * 2, 2), 16);
            }

            if (i2cWriteCB(80, 108, 2, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr111()
        {
            byte[] data = new byte[2];
            string sTmp;
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (tbPciExpress.Text.StartsWith("0x"))
                sTmp = tbPciExpress.Text.Substring(2);
            else
                sTmp = tbPciExpress.Text;
            for (i = 0; i < sTmp.Length / 2; i++) {
                data[i] = Convert.ToByte(sTmp.Substring(i * 2, 2), 16);
            }

            if (i2cWriteCB(80, 111, 2, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr113()
        {
            byte[] data = new byte[1];
            int iTmp;

            if (i2cWriteCB == null)
                return -1;

            if (tbNearFarEndImplementation.Text.Length != 4 || tbNearFarEndImplementation.Text.ElementAt(1) != 'x')
                return -1;

            iTmp = Int32.Parse(tbNearFarEndImplementation.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[0] = Convert.ToByte(iTmp);

            if (i2cWriteCB(80, 113, 1, data) < 0)
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

            tbNewPassword.Text = "";
            MessageBox.Show("Password changed.");        

            return 1;
        }

        private int _WriteAddr123()
        {
            byte[] data = new byte[4];

            if (i2cWriteCB == null)
                return -1;

            if (getPasswordCB == null)
                return -1;

            if (getPasswordCB(4, data) != 4)
                return -1;

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

            if (cbExtendedRateSelectV1.Checked == true)
                data[13] = 0x01;
            if (cbExtendedRateSelectV2.Checked == true)
                data[13] |= 0x02;

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

            for (i = 0; i < 16; i++)
                data[i + 20] = 0x20;
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

            for (i = 0; i < 16; i++)
                data[i + 40] = 0x20;
            bATmp = Encoding.ASCII.GetBytes(tbVendorPn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 40, bATmp.Length > 16 ? 16 : bATmp.Length);

            for (i = 56; i < 2; i++)
                data[i] = 0x20;
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

            data[63] = 0;
            for (i = 0; i < 63; i++)
                data[63] += data[i];

            if (i2cWriteCB(80, 128, 64, data) < 0)
                return -1;

            Array.Clear(data, 0, 64);

            if (tbLinkCodes.Text.Length != 4 || tbLinkCodes.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbLinkCodes.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[0] = Convert.ToByte(iTmp);

            if (cbTxInputEqualizationAutoAdaptive.Checked == true)
                data[1] |= 0x08;
            if (cbTxInputEqualizationProgrammable.Checked == true)
                data[1] |= 0x04;
            if (cbRxOutputEmphasisProgramming.Checked == true)
                data[1] |= 0x02;
            if (cbRxOutputAmplitudeProgramming.Checked == true)
                data[1] |= 0x01;

            if (cbTxCdrOnOffControl.Checked == true)
                data[2] |= 0x80;
            if (cbRxCdrOnOffControl.Checked == true)
                data[2] |= 0x40;
            if (cbTxCdrLolFlag.Checked == true)
                data[2] |= 0x20;
            if (cbRxCdrLolFlag.Checked == true)
                data[2] |= 0x10;
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

            for (i = 0; i < 16; i++)
                data[i + 4] = 0x20;
            bATmp = Encoding.ASCII.GetBytes(tbVendorSn.Text);
            System.Buffer.BlockCopy(bATmp, 0, data, 4, bATmp.Length > 16 ? 16 : bATmp.Length);

            for (i = 0; i < 8; i++)
                data[i + 20] = 0x20;
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

            if (tbBRNominal.Text.Length != 4 || tbBRNominal.Text.ElementAt(1) != 'x')
                return -1;
            iTmp = Int32.Parse(tbBRNominal.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            data[30] = Convert.ToByte(iTmp);

            data[31] = 0;
            for (i = 0; i < 31; i++)
                data[31] += data[i];

            if (i2cWriteCB(80, 192, 32, data) < 0)
                return -1;

            return 0;
        }

        private int _ChangeToUpPage3()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr224()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (cbInputEqualizationMagnitudeTx4.Checked == true)
                data[0] |= 0x80;
            if (cbInputEqualizationMagnitudeTx3.Checked == true)
                data[0] |= 0x40;
            if (cbInputEqualizationMagnitudeTx2.Checked == true)
                data[0] |= 0x20;
            if (cbInputEqualizationMagnitudeTx1.Checked == true)
                data[0] |= 0x10;
            if (cbRxOutputEmphasisMagnitudeRx4.Checked == true)
                data[0] |= 0x08;
            if (cbRxOutputEmphasisMagnitudeRx3.Checked == true)
                data[0] |= 0x04;
            if (cbRxOutputEmphasisMagnitudeRx2.Checked == true)
                data[0] |= 0x02;
            if (cbRxOutputEmphasisMagnitudeRx1.Checked == true)
                data[0] |= 0x01;

            if (i2cWriteCB(80, 224, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr234()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbInputEqualizationTx2.SelectedIndex;
            data[0] |= (byte)(cbInputEqualizationTx1.SelectedIndex << 4);

            if (i2cWriteCB(80, 234, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr235()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbInputEqualizationTx4.SelectedIndex;
            data[0] |= (byte)(cbInputEqualizationTx3.SelectedIndex << 4);

            if (i2cWriteCB(80, 235, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr236()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbRxOutputEmphasisRx2.SelectedIndex;
            data[0] |= (byte)(cbRxOutputEmphasisRx1.SelectedIndex << 4);

            if (i2cWriteCB(80, 236, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr237()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbRxOutputEmphasisRx4.SelectedIndex;
            data[0] |= (byte)(cbRxOutputEmphasisRx3.SelectedIndex << 4);

            if (i2cWriteCB(80, 237, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr238()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbOutputAmplitudeRx2.SelectedIndex;
            data[0] |= (byte)(cbOutputAmplitudeRx1.SelectedIndex << 4);

            if (i2cWriteCB(80, 238, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr239()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            data[0] |= (byte)cbOutputAmplitudeRx4.SelectedIndex;
            data[0] |= (byte)(cbOutputAmplitudeRx3.SelectedIndex << 4);

            if (i2cWriteCB(80, 239, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr240()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (cbSqDisableRx4.Checked == true)
                data[0] |= 0x80;
            if (cbSqDisableRx3.Checked == true)
                data[0] |= 0x40;
            if (cbSqDisableRx2.Checked == true)
                data[0] |= 0x20;
            if (cbSqDisableRx1.Checked == true)
                data[0] |= 0x10;
            if (cbSqDisableTx4.Checked == true)
                data[0] |= 0x08;
            if (cbSqDisableTx3.Checked == true)
                data[0] |= 0x04;
            if (cbSqDisableTx2.Checked == true)
                data[0] |= 0x02;
            if (cbSqDisableTx1.Checked == true)
                data[0] |= 0x01;

            if (i2cWriteCB(80, 240, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WirteUpPage3Addr241()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (cbOutputDisableRx4.Checked == true)
                data[0] |= 0x80;
            if (cbOutputDisableRx3.Checked == true)
                data[0] |= 0x40;
            if (cbOutputDisableRx2.Checked == true)
                data[0] |= 0x20;
            if (cbOutputDisableRx1.Checked == true)
                data[0] |= 0x10;
            if (cbAdaptiveEqualizationControlTx4.Checked == true)
                data[0] |= 0x08;
            if (cbAdaptiveEqualizationControlTx3.Checked == true)
                data[0] |= 0x04;
            if (cbAdaptiveEqualizationControlTx2.Checked == true)
                data[0] |= 0x02;
            if (cbAdaptiveEqualizationControlTx1.Checked == true)
                data[0] |= 0x01;

            if (i2cWriteCB(80, 241, 1, data) < 0)
                return -1;

            return 0;
        }


        private int _WriteUpPage3()
        {
            _ChangeToUpPage3();
            _WirteUpPage3Addr234();
            _WirteUpPage3Addr235();
            _WirteUpPage3Addr236();
            _WirteUpPage3Addr237();
            _WirteUpPage3Addr238();
            _WirteUpPage3Addr239();
            _WirteUpPage3Addr240();
            _WirteUpPage3Addr241();

            return 0;
        }

        private void _bWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;

            if (getPasswordCB == null) {
                MessageBox.Show("getPasswordCB == null Error!!");
                return;
            }

            bWrite.Enabled = false;

            if (getPasswordCB(4, data) != 4)
                goto exit;

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

            if (_WriteAddr89() < 0)
                goto exit;

            if (_WriteAddr90() < 0)
                goto exit;

            if (_WriteAddr91() < 0)
                goto exit;

            if (_WriteAddr92() < 0)
                goto exit;

            if (_WriteAddr93() < 0)
                goto exit;

            if (_WriteAddr94() < 0)
                goto exit;

            if (_WriteAddr95() < 0)
                goto exit;

            if (_WriteAddr96() < 0)
                goto exit;

            if (_WriteAddr97() < 0)
                goto exit;

            if (_WriteAddr98() < 0)
                goto exit;

            if (_WriteAddr108() < 0)
                goto exit;

            if (_WriteAddr111() < 0)
                goto exit;

            if (_WriteAddr113() < 0)
                goto exit;

            if (_WriteUpPage0() < 0)
                goto exit;

            if (_WriteUpPage3() < 0)
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

            /* old version */
            data[0] = 0x32;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                goto exit;

            /* new firmware */
            data[0] = 0xAA;
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

        private void cbPowerOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr93();
        }

        private void cbPowerSet_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr93();
        }

        private void cbHighPowerClassEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr93();
        }

        private void cbTx1Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr86();
        }

        private void cbTx2Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr86();
        }

        private void cbTx3Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr86();
        }

        private void cbTx4Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr86();
        }

        private void cbCdrOnTx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnTx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnTx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnTx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnRx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnRx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnRx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbCdrOnRx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _WriteAddr98();
        }

        private void cbInputEqualizationMagnitudeTx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbInputEqualizationMagnitudeTx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbInputEqualizationMagnitudeTx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbInputEqualizationMagnitudeTx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbRxOutputEmphasisMagnitudeRx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbRxOutputEmphasisMagnitudeRx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbRxOutputEmphasisMagnitudeRx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }

        private void cbRxOutputEmphasisMagnitudeRx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr224();
        }
        
        private void cbInputEqualizationTx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr234();
        }

        private void cbInputEqualizationTx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr234();
        }

        private void cbInputEqualizationTx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr235();
        }

        private void cbInputEqualizationTx4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr235();
        }

        private void cbRxOutputEmphasisRx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr236();
        }

        private void cbRxOutputEmphasisRx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr236();
        }

        private void cbRxOutputEmphasisRx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr237();
        }

        private void cbRxOutputEmphasisRx4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr237();
        }

        private void cbOutputAmplitudeRx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr238();
        }

        private void cbOutputAmplitudeRx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr238();
        }

        private void cbOutputAmplitudeRx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr239();
        }

        private void cbOutputAmplitudeRx4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr239();
        }

        private void cbSqDisableTx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableTx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableTx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableTx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableRx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableRx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableRx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbSqDisableRx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr240();
        }

        private void cbAdaptiveEqualizationControlTx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbAdaptiveEqualizationControlTx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbAdaptiveEqualizationControlTx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbAdaptiveEqualizationControlTx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbOutputDisableRx1_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbOutputDisableRx2_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbOutputDisableRx3_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }

        private void cbOutputDisableRx4_CheckedChanged(object sender, EventArgs e)
        {
            if (bRead.Enabled == false)
                return;

            _ChangeToUpPage3();
            _WirteUpPage3Addr241();
        }
    }
}
