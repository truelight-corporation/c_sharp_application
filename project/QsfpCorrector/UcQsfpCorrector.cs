using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpCorrector
{
    public partial class UcQsfpCorrector : UserControl
    {
        I2cMaster i2cMaster = new I2cMaster();
        
        public UcQsfpCorrector()
        {
            InitializeComponent();
        }

        private int _ReadTemperature()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float temperature;
            int tmp;

            tbTxTemperature.Text = "";

            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            if (i2cMaster.ReadApi(80, 22, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");

            data = new byte[] { 2, 0 };
            i2cMaster.WriteApi(80, 127, 1, data);
            if (i2cMaster.ReadApi(80, 137, 1, data) != 1)
                goto DeviceNoResponse;

            try {
                Buffer.BlockCopy(data, 0, sData, 0, 1);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (data[0] < 128)
                tmp = data[0];
            else
                tmp = (~data[0]) + 1;
            tbTemperatureOffset.Text = sData[0].ToString();

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            tbTxTemperature.Text = "";
            return -1;
        }

        private int _WriteTemperatureOffset()
        {
            byte[] data = new byte[1];
            sbyte[] tmp = new sbyte[1];

            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            try {
                tmp[0] = Convert.ToSByte(tbTemperatureOffset.Text);
            }
            catch (Exception eTSB) {
                MessageBox.Show("Temperature offset out of range (-128 ~ 127)!!\n" + eTSB.ToString());
                tbTemperatureOffset.Text = "";
                return -1;
            }

            data[0] = 2;
            i2cMaster.WriteApi(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            i2cMaster.WriteApi(80, 137, 1, data);

            cbTOAutoCorrect.Checked = false;

            return 0;
        }

        private int _AutoCorrectTemperatureOffset()
        {
            sbyte[] offset = new sbyte[1];
            float txTemperature, temperature, fOffset;

            if (tbTemperature.Text.Length == 0) {
                MessageBox.Show("Please input temperature!!");
                return -1;
            }

            if (tbTxTemperature.Text.Length == 0) {
                if (_ReadTemperature() < 0)
                    return -1;
            }

            try {
                txTemperature = Convert.ToSingle(tbTxTemperature.Text);
            }
            catch (Exception eTSTxTemperature) {
                MessageBox.Show(eTSTxTemperature.ToString());
                return -1;
            }

            try {
                temperature = Convert.ToSingle(tbTemperature.Text);
            }
            catch (Exception eTSTemperature) {
                MessageBox.Show(eTSTemperature.ToString());
                return -1;
            }

            fOffset = (temperature - txTemperature) * 10;
            if ((fOffset > 127) || (fOffset < -128)) {
                MessageBox.Show("Offset out of range: " + fOffset + " (-128 ~ 127)!!");
                return -1;
            }

            try {
                offset[0] = Convert.ToSByte(fOffset);
            }
            catch (Exception eTSB) {
                MessageBox.Show(eTSB.ToString());
                return -1;
            }

            tbTemperatureOffset.ReadOnly = true;
            tbTemperatureOffset.Text = offset[0].ToString();

            if (_WriteTemperatureOffset() < 0)
                return -1;

            return 0;
        }

        private void _cbTOAutoCorrectCheckedChanged(object sender, EventArgs e)
        {
            if (cbTOAutoCorrect.Checked == true) {
                if (_AutoCorrectTemperatureOffset() < 0) {
                    cbTOAutoCorrect.Checked = false;
                    return;
                }
            }
            else {
                tbTemperatureOffset.ReadOnly = false;
                if (_ReadTemperature() < 0)
                    return;
            }
        }

        private void _bTemperatureReadClick(object sender, EventArgs e)
        {
            if (_ReadTemperature() < 0)
                return;
        }

        private void _bTemperatureWriteClick(object sender, EventArgs e)
        {
            if (_WriteTemperatureOffset() < 0)
                return;
        }

        private int _ReadPowerRate()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbRssi1.Text = tbRssi2.Text = tbRssi3.Text = tbRssi4.Text = "";
            tbRxPowerRate1.Text = tbRxPowerRate2.Text = tbRxPowerRate3.Text = tbRxPowerRate4.Text = "";
            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text = tbRxPower4.Text = "";

            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            if (i2cMaster.ReadApi(80, 108, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();

            if (i2cMaster.ReadApi(80, 110, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi2.Text = tmp.ToString();

            if (i2cMaster.ReadApi(80, 112, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi3.Text = tmp.ToString();

            if (i2cMaster.ReadApi(80, 114, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi4.Text = tmp.ToString();

            if (i2cMaster.ReadApi(80, 34, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower1.Text = power.ToString("#0.0");

            if (i2cMaster.ReadApi(80, 36, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower2.Text = power.ToString("#0.0");

            if (i2cMaster.ReadApi(80, 38, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower3.Text = power.ToString("#0.0");

            if (i2cMaster.ReadApi(80, 40, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower4.Text = power.ToString("#0.0");

            data = new byte[] { 32, 0, 0, 0 };
            i2cMaster.WriteApi(80, 127, 1, data);
            if (i2cMaster.ReadApi(80, 163, 1, data) != 1)
                goto DeviceNoResponse;

            tbRxPowerRateDefault.Text = data[0].ToString();

            if (tbRxPowerRateMax.Text.Length == 0)
                tbRxPowerRateMax.Text = (data[0] + 15).ToString();

            if (tbRxPowerRateMin.Text.Length == 0)
                tbRxPowerRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 2, 0, 0, 0};
            i2cMaster.WriteApi(80, 127, 1, data);
            if (i2cMaster.ReadApi(80, 133, 4, data) != 4)
                goto DeviceNoResponse;

            tbRxPowerRate1.Text = data[0].ToString();
            tbRxPowerRate2.Text = data[1].ToString();
            tbRxPowerRate3.Text = data[2].ToString();
            tbRxPowerRate4.Text = data[3].ToString();

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            tbRssi1.Text = tbRssi2.Text = tbRssi3.Text = tbRssi4.Text = "";
            tbRxPowerRate1.Text = tbRxPowerRate2.Text = tbRxPowerRate3.Text = tbRxPowerRate4.Text = "";
            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text = tbRxPower4.Text = "";
            return -1;
        }

        private void _bRxPowerRateReadClick(object sender, EventArgs e)
        {
            if (_ReadPowerRate() < 0)
                return;
        }

        private int _WritePowerRate()
        {
            byte[] data = new byte[] {2, 0, 0, 0};;

            if ((tbRxPowerRate1.Text.Length == 0) || (tbRxPowerRate2.Text.Length == 0) ||
                (tbRxPowerRate3.Text.Length == 0) || (tbRxPowerRate4.Text.Length == 0)) {
                MessageBox.Show("Please input Rx power rate!!");
                return -1;
            }

            i2cMaster.WriteApi(80, 127, 1, data);

            try {
                data[0] = Convert.ToByte(tbRxPowerRate1.Text);
                data[1] = Convert.ToByte(tbRxPowerRate2.Text);
                data[2] = Convert.ToByte(tbRxPowerRate3.Text);
                data[3] = Convert.ToByte(tbRxPowerRate4.Text);
            }
            catch (Exception eTB) {
                MessageBox.Show("Rx power rate out of range (0 ~ 255)!!\n" + eTB.ToString());
                return -1;
            }

            i2cMaster.WriteApi(80, 133, 4, data);

            return 0;
        }

        private void _bRxPowerRateWriteClick(object sender, EventArgs e)
        {
            if (_WritePowerRate() < 0)
                return;
        }

        private void _cbRPRAutoCorrectCheckedChanged(object sender, EventArgs e)
        {
            float input, rssi;
            int rate, rateMax, rateMin;

            rate = rateMax = rateMin = 0;

            if (cbRPRAutoCorrect.Checked == true) {
                if ((tbRxInputPower1.Text.Length == 0) || (tbRxInputPower2.Text.Length == 0) ||
                    (tbRxInputPower3.Text.Length == 0) || (tbRxInputPower4.Text.Length == 0)) {
                    MessageBox.Show("Input power empty!!");
                    goto CleanChecked;
                }

                if (tbRxPowerRateDefault.Text.Length == 0) {
                    if (_ReadPowerRate() < 0)
                        goto CleanChecked;
                }

                tbRxPowerRate1.ReadOnly = tbRxPowerRate2.ReadOnly =
                    tbRxPowerRate3.ReadOnly = tbRxPowerRate4.ReadOnly = true;

                try {
                    rateMax = Convert.ToInt32(tbRxPowerRateMax.Text);
                    rateMin = Convert.ToInt32(tbRxPowerRateMin.Text);
                }
                catch (Exception eTI) {
                    MessageBox.Show(eTI.ToString());
                    goto CleanChecked;
                }

                try {
                    input = Convert.ToSingle(tbRxInputPower1.Text);
                    rssi = Convert.ToSingle(tbRssi1.Text);
                    rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
                }
                catch (Exception eCT) {
                    MessageBox.Show(eCT.ToString());
                }
                tbRxPowerRate1.Text = rate.ToString();

                if ((rate > rateMax) || (rate < rateMin))
                    MessageBox.Show("Rx1 rate: "+ rate + " out of bound!!");

                try {
                    input = Convert.ToSingle(tbRxInputPower2.Text);
                    rssi = Convert.ToSingle(tbRssi2.Text);
                    rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
                }
                catch (Exception eCT) {
                    MessageBox.Show(eCT.ToString());
                }
                tbRxPowerRate2.Text = rate.ToString();

                if ((rate > rateMax) || (rate < rateMin))
                    MessageBox.Show("Rx2 rate: " + rate + " out of bound!!");

                try {
                    input = Convert.ToSingle(tbRxInputPower3.Text);
                    rssi = Convert.ToSingle(tbRssi3.Text);
                    rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
                }
                catch (Exception eCT) {
                    MessageBox.Show(eCT.ToString());
                }
                tbRxPowerRate3.Text = rate.ToString();

                if ((rate > rateMax) || (rate < rateMin))
                    MessageBox.Show("Rx3 rate: " + rate + " out of bound!!");

                try {
                    input = Convert.ToSingle(tbRxInputPower4.Text);
                    rssi = Convert.ToSingle(tbRssi4.Text);
                    rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
                }
                catch (Exception eCT) {
                    MessageBox.Show(eCT.ToString());
                }
                tbRxPowerRate4.Text = rate.ToString();

                if ((rate > rateMax) || (rate < rateMin))
                    MessageBox.Show("Rx4 rate: " + rate + " out of bound!!");

                if (_WritePowerRate() < 0)
                    goto CleanChecked;

                tbRxPowerRate1.ReadOnly = tbRxPowerRate2.ReadOnly =
                    tbRxPowerRate3.ReadOnly = tbRxPowerRate4.ReadOnly = false;

                cbRPRAutoCorrect.Checked = false;

                

            }
            else {
                tbRxPowerRate1.ReadOnly = tbRxPowerRate2.ReadOnly =
                    tbRxPowerRate3.ReadOnly = tbRxPowerRate4.ReadOnly = false;
            }

            return;

        CleanChecked:
            cbRPRAutoCorrect.Checked = false;
            tbRxPowerRate1.ReadOnly = tbRxPowerRate2.ReadOnly =
                tbRxPowerRate3.ReadOnly = tbRxPowerRate4.ReadOnly = false;
        }
    }
}
