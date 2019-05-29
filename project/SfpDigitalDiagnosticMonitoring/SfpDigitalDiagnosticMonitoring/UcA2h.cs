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
    public partial class UcA2h : UserControl
    {
		public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
		public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWritePasswordCB();

		private I2cReadCB i2cReadCB = null;
		private I2cWriteCB i2cWriteCB = null;
        private I2cWritePasswordCB i2cWritePasswordCB = null;

        public UcA2h()
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

        private int _ReadAlarmWarningThreshold()
        {
            byte[] data = new byte[56];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fTmp;
            int tmp;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(81, 0, 56, data) != 56)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbTemperatureHighAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbTemperatureLowAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbTemperatureHighWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbTemperatureLowWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10000;
            tbVccHighAlarmThreshold.Text = fTmp.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 10, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10000;
            tbVccLowAlarmThreshold.Text = fTmp.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10000;
            tbVccHighWarningThreshold.Text = fTmp.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 14, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10000;
            tbVccLowWarningThreshold.Text = fTmp.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 16, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 500;
            tbTxBiasHighAlarmThreshold.Text = fTmp.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 18, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 500;
            tbTxBiasLowAlarmThreshold.Text = fTmp.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 20, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 500;
            tbTxBiasHighWarningThreshold.Text = fTmp.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 22, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 500;
            tbTxBiasLowWarningThreshold.Text = fTmp.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 24, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbTxPowerHighAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 26, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbTxPowerLowAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 28, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbTxPowerHighWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 30, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbTxPowerLowWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 32, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbRxPowerHighAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 34, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbRxPowerLowAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 36, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbRxPowerHighWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 38, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbRxPowerLowWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 40, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbOptionalLaserTempHighAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 42, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbOptionalLaserTempLowAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 44, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbOptionalLaserTempHighWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 46, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbOptionalLaserTempLowWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 48, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbOptionalTecCurrentHighAlarmThreshold.Text = fTmp.ToString("#0.0");
            
            try {
                Buffer.BlockCopy(data, 50, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbOptionalTecCurrentLowAlarmThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 52, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbOptionalTecCurrentHighWarningThreshold.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 54, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbOptionalTecCurrentLowWarningThreshold.Text = fTmp.ToString("#0.0");

            return 0;
        }

        private int _ReadCalibrationConstants()
        {
            byte[] data = new byte[36];
            byte[] bATmp = new byte[4];
            byte[] reverseData;
            float fTmp;
            int tmp;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(81, 56, 36, data) != 36)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToSingle(reverseData, 0);
            tbCalibrationRxPower4.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToSingle(reverseData, 0);
            tbCalibrationRxPower3.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToSingle(reverseData, 0);
            tbCalibrationRxPower2.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToSingle(reverseData, 0);
            tbCalibrationRxPower1.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 16, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToSingle(reverseData, 0);
            tbCalibrationRxPower0.Text = fTmp.ToString();

            fTmp = data[20];
            fTmp += data[21] / 256;
            tbCalibrationTxISlope.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 22, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbCalibrationTxIOffset.Text = tmp.ToString();

            fTmp = data[24];
            fTmp += data[25] / 256;
            tbCalibrationTxPwrSlope.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 26, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbCalibrationTxPwrOffset.Text = tmp.ToString();

            fTmp = data[28];
            fTmp += data[29] / 256;
            tbCalibrationTSlope.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 30, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbCalibrationTOffset.Text = tmp.ToString();

            fTmp = data[32];
            fTmp += data[33] / 256;
            tbCalibrationVSlope.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 34, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbCalibrationVOffset.Text = tmp.ToString();

            return 0;
        }

        private int _ReadRealTimeDiagonstic()
        {
            byte[] data = new byte[14];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fTmp;
            int tmp;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(81, 96, 14, data) != 14)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbTemperature.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10000;
            tbVcc.Text = fTmp.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 500;
            tbTxBias.Text = fTmp.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbTxPower.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbRxPower.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 10, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 256;
            tbOptionalLaserTemp.Text = fTmp.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = tmp;
            fTmp = fTmp / 10;
            tbOptionalTecCurrent.Text = fTmp.ToString("#0.0");

            return 0;
        }

        private int _ReadAlarmWarningFlag()
        {
            byte[] data = new byte[6];

            if (i2cReadCB(81, 112, 6, data) != 6)
                return -1;

            if ((data[0] & 0x01) != 0)
                cbTxPowerLowAlarm.Checked = true;
            else
                cbTxPowerLowAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTxPowerHighAlarm.Checked = true;
            else
                cbTxPowerHighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxBias1LowAlarm.Checked = true;
            else
                cbTxBias1LowAlarm.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTxBiasHighAlarm.Checked = true;
            else
                cbTxBiasHighAlarm.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbVccLowAlarm.Checked = true;
            else
                cbVccLowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbVccHighAlarm.Checked = true;
            else
                cbVccHighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTemperatureLowAlarm.Checked = true;
            else
                cbTemperatureLowAlarm.Checked = false;

            if ((data[0] & 0x80) != 0)
                cbTemperatureHighAlarm.Checked = true;
            else
                cbTemperatureHighAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbOptionalTecCurrentLowAlarm.Checked = true;
            else
                cbOptionalTecCurrentLowAlarm.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbOptionalTecCurrentHighAlarm.Checked = true;
            else
                cbOptionalTecCurrentHighAlarm.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbOptionalLaserTempLowAlarm.Checked = true;
            else
                cbOptionalLaserTempLowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbOptionalLaserTempHighAlarm.Checked = true;
            else
                cbOptionalLaserTempHighAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbRxPowerLowAlarm.Checked = true;
            else
                cbRxPowerLowAlarm.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbRxPowerHighAlarm.Checked = true;
            else
                cbRxPowerHighAlarm.Checked = false;

            tbTxInputEqu.Text = "0x" + data[2].ToString("X2");
            tbRxOutputEmp.Text = "0x" + data[3].ToString("X2");

            if ((data[4] & 0x01) != 0)
                cbTxPowerLowWarning.Checked = true;
            else
                cbTxPowerLowWarning.Checked = false;

            if ((data[4] & 0x02) != 0)
                cbTxPowerHighWarning.Checked = true;
            else
                cbTxPowerHighWarning.Checked = false;

            if ((data[4] & 0x04) != 0)
                cbTxBias1LowWarning.Checked = true;
            else
                cbTxBias1LowWarning.Checked = false;

            if ((data[4] & 0x08) != 0)
                cbTxBiasHighWarning.Checked = true;
            else
                cbTxBiasHighWarning.Checked = false;

            if ((data[4] & 0x10) != 0)
                cbVccLowWarning.Checked = true;
            else
                cbVccLowWarning.Checked = false;

            if ((data[4] & 0x20) != 0)
                cbVccHighWarning.Checked = true;
            else
                cbVccHighWarning.Checked = false;

            if ((data[4] & 0x40) != 0)
                cbTemperatureLowWarning.Checked = true;
            else
                cbTemperatureLowWarning.Checked = false;

            if ((data[4] & 0x80) != 0)
                cbTemperatureHighWarning.Checked = true;
            else
                cbTemperatureHighWarning.Checked = false;

            if ((data[5] & 0x04) != 0)
                cbOptionalTecCurrentLowWarning.Checked = true;
            else
                cbOptionalTecCurrentLowWarning.Checked = false;

            if ((data[5] & 0x08) != 0)
                cbOptionalTecCurrentHighWarning.Checked = true;
            else
                cbOptionalTecCurrentHighWarning.Checked = false;

            if ((data[5] & 0x10) != 0)
                cbOptionalLaserTempLowWarning.Checked = true;
            else
                cbOptionalLaserTempLowWarning.Checked = false;

            if ((data[5] & 0x20) != 0)
                cbOptionalLaserTempHighWarning.Checked = true;
            else
                cbOptionalLaserTempHighWarning.Checked = false;

            if ((data[5] & 0x40) != 0)
                cbRxPowerLowWarning.Checked = true;
            else
                cbRxPowerLowWarning.Checked = false;

            if ((data[5] & 0x80) != 0)
                cbRxPowerHighWarning.Checked = true;
            else
                cbRxPowerHighWarning.Checked = false;

            return 0;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[2];

            if (i2cReadCB == null)
                return;

            bRead.Enabled = false;

            tbTemperature.Text = "";
            tbVcc.Text = "";
            tbTxBias.Text = "";
            tbTxPower.Text = "";
            tbRxPower.Text = "";
            tbOptionalLaserTemp.Text = "";
            tbOptionalTecCurrent.Text = "";

            if (_ReadAlarmWarningThreshold() < 0)
                goto exit;

            if (_ReadCalibrationConstants() < 0)
                goto exit;

            if (i2cReadCB(81, 95, 1, data) != 1)
                goto exit;

            tbChecksum.Text = "0x" + data[0].ToString("X2");

            if (_ReadRealTimeDiagonstic() < 0)
                goto exit;

            if (i2cReadCB(81, 110, 1, data) != 1)
                goto exit;

            if ((data[0] & 0x01) != 0)
                cbDataReadyBarState.Checked = true;
            else
                cbDataReadyBarState.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbRxLosState.Checked = true;
            else
                cbRxLosState.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxFaultState.Checked = true;
            else
                cbTxFaultState.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbSoftRateSelect.Checked = true;
            else
                cbSoftRateSelect.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbRS0State.Checked = true;
            else
                cbRS0State.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbRS1State.Checked = true;
            else
                cbRS1State.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbSoftTxDisable.Checked = true;
            else
                cbSoftTxDisable.Checked = false;

            if ((data[0] & 0x80) != 0)
                cbTxDisableState.Checked = true;
            else
                cbTxDisableState.Checked = false;

            if (_ReadAlarmWarningFlag() < 0)
                goto exit;

            if (i2cReadCB(81, 118, 2, data) != 2)
                goto exit;

            if ((data[0] & 0x01) != 0)
                cbPowerLevelSelect.Checked = true;
            else
                cbPowerLevelSelect.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbPowerLevelOperation.Checked = true;
            else
                cbPowerLevelOperation.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbSoftRs1Select.Checked = true;
            else
                cbSoftRs1Select.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbOptionalRxCdrUnlocked.Checked = true;
            else
                cbOptionalRxCdrUnlocked.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbOptionalTxCdrUnlocked.Checked = true;
            else
                cbOptionalTxCdrUnlocked.Checked = false;

        exit:
            bRead.Enabled = true;
        }

        private int _TemperatureWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float temperature;
            short sTmp;

            try {
                temperature = Convert.ToSingle(tbTemperatureHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 0, 8, data) < 0)
                return -1;

            return 0;

        Error:
            return -1;
        }

        private void bTemperatureWrite_Click(object sender, EventArgs e)
        {
            if (_TemperatureWrite() < 0)
                return;
        }

        private int _VccWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float vcc;
            ushort uSTmp;

            try {
                vcc = Convert.ToSingle(tbVccHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 8, 8, data) < 0)
                return -1;

            return 0;
        }

        private void bVccWrite_Click(object sender, EventArgs e)
        {
            if (_VccWrite() < 0)
                return;
        }

        private int _TxBiasPowerWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float power;
            ushort uSTmp;

            try {
                power = Convert.ToSingle(tbTxBiasHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 16, 8, data) < 0)
                return -1;

            try {
                power = Convert.ToSingle(tbTxPowerHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxPowerLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxPowerHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxPowerLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 24, 8, data) < 0)
                return -1;

            return 0;
        }

        private void bTxBiasWrite_Click(object sender, EventArgs e)
        {
            if (_TxBiasPowerWrite() < 0)
                return;
        }

        private int _RxPowerWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float power;
            ushort uSTmp;

            try {
                power = Convert.ToSingle(tbRxPowerHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 32, 8, data) < 0)
                return -1;

            return 0;
        }

        private void bRxPowerWrite_Click(object sender, EventArgs e)
        {
            if (_RxPowerWrite() < 0)
                return;
        }

        private int _LaserTempTecCurrentWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float fTmp;
            short sTmp;
            ushort usTmp;

            try {
                fTmp = Convert.ToSingle(tbOptionalLaserTempHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }
            fTmp *= 256;
            try {
                sTmp = Convert.ToInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }
            bATmp = BitConverter.GetBytes(sTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalLaserTempLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }
            fTmp *= 256;
            try {
                sTmp = Convert.ToInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }
            bATmp = BitConverter.GetBytes(sTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalLaserTempHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }
            fTmp *= 256;
            try {
                sTmp = Convert.ToInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }
            bATmp = BitConverter.GetBytes(sTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalLaserTempLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }
            fTmp *= 256;
            try {
                sTmp = Convert.ToInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }
            bATmp = BitConverter.GetBytes(sTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 40, 8, data) < 0)
                return -1;

            try {
                fTmp = Convert.ToSingle(tbOptionalTecCurrentHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            fTmp *= 10;
            try {
                usTmp = Convert.ToUInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(usTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalTecCurrentLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            fTmp *= 10;
            try {
                usTmp = Convert.ToUInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(usTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalTecCurrentHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            fTmp *= 10;
            try {
                usTmp = Convert.ToUInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(usTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                fTmp = Convert.ToSingle(tbOptionalTecCurrentLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }
            fTmp *= 10;
            try {
                usTmp = Convert.ToUInt16(fTmp);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(usTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(81, 48, 8, data) < 0)
                return -1;

            return 0;

        Error:
            return -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_LaserTempTecCurrentWrite() < 0)
                return;
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
