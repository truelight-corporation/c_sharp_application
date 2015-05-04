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

#if false
            if (i2cMaster.ReadApi(80, 26, 2, data) != 2)
                goto DeviceNoResponse;
            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRxInputPower1.Text = tmp.ToString();
#endif

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

#if false
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            if (i2cMaster.ReadApi(80, 22, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");
#endif
            return 0;
        }

        private void _bRxPowerRateReadClick(object sender, EventArgs e)
        {
            if (_ReadPowerRate() < 0)
                return;
        }
    }
}
