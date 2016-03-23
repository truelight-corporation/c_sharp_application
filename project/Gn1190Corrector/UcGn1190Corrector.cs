using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gn1190Corrector
{
    public partial class UcGn1190Corrector : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB qsfpI2cReadCB = null;
        private I2cWriteCB qsfpI2cWriteCB = null;

        public UcGn1190Corrector()
        {
            InitializeComponent();
        }

        public int SetQsfpI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            qsfpI2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetQsfpI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            qsfpI2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        private int _PaserLoadFile()
        {

            return 0;
        }

        private void bLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Filter = "txt files (*.txt)|*.txt";
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbFilePath.Text = ofdSelectFile.FileName;
            if (tbFilePath.Text.Length == 0)
                return;
            
            if (_PaserLoadFile() < 0)
                return;

            cbTemperatureCompensation.Enabled = false;
            cbTemperatureCompensation.Checked = false;
        }

        private void bSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();
            StreamWriter swConfig;

            sfdSelectFile.Filter = "txt files (*.txt)|*.txt";
            if (tbFilePath.Text.Length != 0)
                sfdSelectFile.FileName = tbFilePath.Text;

            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbFilePath.Text = sfdSelectFile.FileName;

            swConfig = new StreamWriter(tbFilePath.Text);
        }

        private int _ReadTemperature()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float temperature;
            int tmp;

            tbTxTemperature.Text = "";

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 22, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");

            if (qsfpI2cWriteCB == null)
                return -1;

            data = new byte[] { 2, 0 };
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 137, 1, data) != 1)
                return -1;

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
        }

        private void bTemperatureRead_Click(object sender, EventArgs e)
        {
            if (_ReadTemperature() < 0)
                return;
        }

        private int _WritePassword()
        {
            byte[] data = new byte[4];
            int tmpI;
            if (qsfpI2cWriteCB == null)
                return -1;

            if ((tbPassword123.Text.Length != 2) || (tbPassword124.Text.Length != 2) ||
                (tbPassword125.Text.Length != 2) || (tbPassword126.Text.Length != 2)) {
                MessageBox.Show("Please input 4 hex value password before write!!");
                return -1;
            }
            try {
                tmpI = Convert.ToInt32(tbPassword123.Text, 16);
                data[0] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPassword124.Text, 16);
                data[1] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPassword125.Text, 16);
                data[2] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPassword126.Text, 16);
                data[3] = (byte)tmpI;
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
                return -1;
            }

            if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (qsfpI2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _ClearPassword()
        {
            byte[] data = new byte[4];

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = data[1] = data[2] = data[3] = 0;

            if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteTemperatureOffset()
        {
            byte[] data = new byte[1];
            sbyte[] tmp = new sbyte[1];

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
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
            qsfpI2cWriteCB(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            qsfpI2cWriteCB(80, 137, 1, data);

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bTemperatureWrite_Click(object sender, EventArgs e)
        {
            if (_WriteTemperatureOffset() < 0)
                return;
        }

        private int _ResetTemperatureOffset()
        {
            tbTemperatureOffset.Text = "0";

            if (_WriteTemperatureOffset() < 0)
                return -1;

            cbTemperatureCorrected.Checked = false;

            return 0;
        }

        private void bTemperatureReset_Click(object sender, EventArgs e)
        {
            if (_ResetTemperatureOffset() < 0)
                return;
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

            tbTemperatureOffset.Text = offset[0].ToString();

            if (_WriteTemperatureOffset() < 0)
                return -1;

            cbTemperatureCorrected.Checked = true;

            return 0;
        }

        private void bTemperatureAutoCorrect_Click(object sender, EventArgs e)
        {
            if (_AutoCorrectTemperatureOffset() < 0)
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

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 108, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 110, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi2.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 112, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi3.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 114, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi4.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 34, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower1.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 36, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower2.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 38, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower3.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 40, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower4.Text = power.ToString("#0.0");

            if (qsfpI2cWriteCB == null)
                return -1;

            data = new byte[] { 32, 0, 0, 0 };
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 163, 1, data) != 1)
                return -1;

            tbRxPowerRateDefault.Text = data[0].ToString();

            if (tbRxPowerRateMax.Text.Length == 0)
                tbRxPowerRateMax.Text = (data[0] + 15).ToString();

            if (tbRxPowerRateMin.Text.Length == 0)
                tbRxPowerRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 2, 0, 0, 0 };
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 133, 4, data) != 4)
                return -1;

            tbRxPowerRate1.Text = data[0].ToString();
            tbRxPowerRate2.Text = data[1].ToString();
            tbRxPowerRate3.Text = data[2].ToString();
            tbRxPowerRate4.Text = data[3].ToString();

            return 0;
        }

        private void bRxPowerRateRead_Click(object sender, EventArgs e)
        {
            if (_ReadPowerRate() < 0)
                return;
        }

        private int _WritePowerRate()
        {
            byte[] data = new byte[] { 2, 0, 0, 0 }; ;

            if ((tbRxPowerRate1.Text.Length == 0) || (tbRxPowerRate2.Text.Length == 0) ||
                (tbRxPowerRate3.Text.Length == 0) || (tbRxPowerRate4.Text.Length == 0)) {
                MessageBox.Show("Please input Rx power rate!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            qsfpI2cWriteCB(80, 127, 1, data);

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

            qsfpI2cWriteCB(80, 133, 4, data);

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bRxPowerRateWrite_Click(object sender, EventArgs e)
        {
            if (_WritePowerRate() < 0)
                return;
        }

        private int _ResetRxPowerRate()
        {
            if (tbRxPowerRateDefault.Text.Length == 0) {
                if (_ReadPowerRate() < 0)
                    return -1;
            }

            tbRxPowerRate1.Text = tbRxPowerRate2.Text = tbRxPowerRate3.Text = tbRxPowerRate4.Text =
                tbRxPowerRateDefault.Text;

            if (_WritePowerRate() < 0)
                return -1;

            cbRxPowerRateCorrected.Checked = false;

            return 0;
        }

        private void bRxPowerRateReset_Click(object sender, EventArgs e)
        {
            if (_ResetRxPowerRate() < 0)
                return;
        }

        private int _AutoCorrectRxPowerRate()
        {
            float input, rssi;
            int rate, rateMax, rateMin;

            rate = rateMax = rateMin = 0;

            if ((tbRxInputPower1.Text.Length == 0) || (tbRxInputPower2.Text.Length == 0) ||
                (tbRxInputPower3.Text.Length == 0) || (tbRxInputPower4.Text.Length == 0)) {
                MessageBox.Show("Input power empty!!");
                return -1;
            }

            if (tbRxPowerRateDefault.Text.Length == 0) {
                if (_ReadPowerRate() < 0)
                    return -1;
            }

            try {
                rateMax = Convert.ToInt32(tbRxPowerRateMax.Text);
                rateMin = Convert.ToInt32(tbRxPowerRateMin.Text);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
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
                MessageBox.Show("Rx1 rate: " + rate + " out of bound!!");

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
                return -1;

            cbRxPowerRateCorrected.Checked = true;

            return 0;
        }

        private void bRxPowerRateAutoCorrect_Click(object sender, EventArgs e)
        {
            if (_AutoCorrectRxPowerRate() < 0)
                return;
        }

        private int _ReadAverageCurrentAndModulationCurrentCorrectData()
        {
            byte[] data = new byte[26];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            Single sTmp;
            int iTmp;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 2;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 189, 26, data) != 26)
                return -1;

            Buffer.BlockCopy(data, 0, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModuleAverageCurrentEquationA.Text = sTmp.ToString("#0.00");

            Array.Copy(data, 1, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToUInt16(reverseData, 0)) / 10;
            tbModuleAverageCurrentEquationB.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 3, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbModuleAverageCurrentEquationC.Text = iTmp.ToString();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[5]) * 0.04);
            tbModuleAverageCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[6]) * 0.04);
            tbModuleAverageCurrentMax.Text = sTmp.ToString("#0.00");

            tbModuleModulationCurrentEquationA.Text = (Convert.ToSingle(data[7]) / 100).ToString("#0.00");

            Array.Copy(data, 8, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModuleModulationCurrentEquationB.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModuleModulationCurrentEquationC.Text = iTmp.ToString();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[12]) * 0.04);
            tbModuleModulationCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[13]) * 0.04);
            tbModuleModulationCurrentMax.Text = sTmp.ToString("#0.00");

            Buffer.BlockCopy(data, 14, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModuleAverageCurrentOffset.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 15, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModuleModulationCurrentOffset.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 16, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbVhfCompPropEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 17, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 19, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 1000;
            tbVhfCompPropEquationC.Text = sTmp.ToString("#0.000");

            Buffer.BlockCopy(data, 21, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbVhfCompConstEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 22, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 24, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationC.Text = sTmp.ToString("#0.00");

            return 0;
        }

        private void bAcMcRead_Click(object sender, EventArgs e)
        {
            if (_ReadAverageCurrentAndModulationCurrentCorrectData() < 0)
                return;
        }

        private int _WriteAcMcCorrectData()
        {

            byte[] data = new byte[44];
            byte[] bATmp;
            int i;
            UInt16 u16Tmp;
            Int16 s16Tmp;
            byte bChecksum;
            sbyte[] sBATmp = new sbyte[1];

            if ((tbAverageCurrentEquationA.Text.Length == 0) ||
                (tbAverageCurrentEquationB.Text.Length == 0) ||
                (tbAverageCurrentEquationC.Text.Length == 0) ||
                (tbAverageCurrentMin.Text.Length == 0) ||
                (tbAverageCurrentMax.Text.Length == 0) ||
                (tbModulationCurrentEquationA.Text.Length == 0) ||
                (tbModulationCurrentEquationB.Text.Length == 0) ||
                (tbModulationCurrentEquationC.Text.Length == 0) ||
                (tbModulationCurrentMin.Text.Length == 0) ||
                (tbModulationCurrentMax.Text.Length == 0) ||
                (tbAverageCurrentOffset.Text.Length == 0) ||
                (tbModulationCurrentOffset.Text.Length == 0) ||
                (tbVhfCompPropEquationA.Text.Length == 0) ||
                (tbVhfCompPropEquationB.Text.Length == 0) ||
                (tbVhfCompPropEquationC.Text.Length == 0) ||
                (tbVhfCompConstEquationA.Text.Length == 0) ||
                (tbVhfCompConstEquationB.Text.Length == 0) ||
                (tbVhfCompConstEquationC.Text.Length == 0)) {
                MessageBox.Show("Please input value before write!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 2;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if ((Convert.ToSingle(tbAverageCurrentEquationA.Text) < -1.28) ||
                (Convert.ToSingle(tbAverageCurrentEquationA.Text) > 1.27)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationA.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[0] = (byte)Convert.ToSByte(Convert.ToSingle(tbAverageCurrentEquationA.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationB.Text) < 0) ||
                Convert.ToSingle(tbAverageCurrentEquationB.Text) > 6553.5) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationB.Text +
                    " out of range (0 ~ 6553.5)!!");
                return -1;
            }
            try {
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(tbAverageCurrentEquationB.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[1] = bATmp[1];
            data[2] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationC.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[3] = bATmp[1];
            data[4] = bATmp[0];

            if ((Convert.ToSingle(tbAverageCurrentMin.Text) < 0) ||
                Convert.ToSingle(tbAverageCurrentMin.Text) > 10.2) {
                MessageBox.Show("Average current min: " +
                    tbAverageCurrentMin.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[5] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMin.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentMax.Text) < 0) ||
                Convert.ToSingle(tbAverageCurrentMax.Text) > 10.2) {
                MessageBox.Show("Average current max: " +
                    tbAverageCurrentMax.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[6] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMax.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentEquationA.Text) < 0 ||
                Convert.ToSingle(tbModulationCurrentEquationA.Text) > 2.55) {
                MessageBox.Show("Modualtion current equation A: " +
                    tbModulationCurrentEquationA.Text +
                    " out of range (0 ~ 2.55)!!");
                return -1;
            }
            try {
                data[7] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentEquationA.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentEquationB.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationB.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationA.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationB.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[8] = bATmp[1];
            data[9] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationC.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[10] = bATmp[1];
            data[11] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentMin.Text) < 0) ||
                Convert.ToSingle(tbModulationCurrentMin.Text) > 10.2) {
                MessageBox.Show("Modulation current min: " +
                    tbModulationCurrentMin.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[12] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMin.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbModulationCurrentMax.Text) < 0) ||
                Convert.ToSingle(tbModulationCurrentMax.Text) > 10.2) {
                MessageBox.Show("Modulation current max: " +
                    tbModulationCurrentMax.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[13] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMax.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbAverageCurrentOffset.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffset.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffset.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffset.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 14, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentOffset.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffset.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffset.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffset.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 15, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbVhfCompPropEquationA.Text) < -0.128) ||
                (Convert.ToSingle(tbVhfCompPropEquationA.Text) > 0.127)) {
                MessageBox.Show("VHF Comp Prop equation A: " +
                    tbVhfCompPropEquationA.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }

            try {
                data[16] = (byte)Convert.ToSByte(Convert.ToSingle(tbVhfCompPropEquationA.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompPropEquationB.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompPropEquationB.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Prop equation B: " +
                    tbVhfCompPropEquationB.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationB.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[17] = bATmp[1];
            data[18] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationC.Text) < -32.768 ||
                Convert.ToSingle(tbVhfCompPropEquationC.Text) > 32.767) {
                MessageBox.Show("VHF Comp Prop equation C: " +
                    tbVhfCompPropEquationC.Text +
                    " out of range (-32.768 ~ 32.767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationC.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[19] = bATmp[1];
            data[20] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompConstEquationA.Text) < -0.128) ||
                (Convert.ToSingle(tbVhfCompConstEquationA.Text) > 0.127)) {
                MessageBox.Show("VHF Comp Const equation A: " +
                    tbVhfCompConstEquationA.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }

            try {
                data[21] = (byte)Convert.ToSByte(Convert.ToSingle(tbVhfCompConstEquationA.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompConstEquationB.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompConstEquationB.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Const equation B: " +
                    tbVhfCompConstEquationB.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationB.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[22] = bATmp[1];
            data[23] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationC.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompConstEquationC.Text) > 327.67) {
                MessageBox.Show("VHF Comp Const equation C: " +
                    tbVhfCompConstEquationC.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationC.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[24] = bATmp[1];
            data[25] = bATmp[0];

            if (qsfpI2cWriteCB(80, 189, 26, data) < 0)
                return -1;

            for (i = 0, bChecksum = 1; i < 16; i++)
                bChecksum += data[i];

            data[0] = bChecksum;

            if (qsfpI2cWriteCB(80, 132, 1, data) < 0)
                return -1;


            
            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bAcMcWrite_Click(object sender, EventArgs e)
        {
            if (_WriteAcMcCorrectData() < 0)
                return;
            cbTemperatureCompensation.Checked = true;
            cbTemperatureCompensation.Enabled = true;
        }

        private int _ResetAcMcEquation()
        {
            byte[] data = new byte[] { 2 };

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0;
            if (qsfpI2cWriteCB(80, 191, 1, data) < 0)
                return -1;

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bLutReset_Click(object sender, EventArgs e)
        {
            if (_ResetAcMcEquation() < 0)
                return;
        }

        private int _DisableTemperatureCompensation()
        {
            byte[] data = new byte[1];

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 2;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 132, 1, data) != 1)
                return -1;

            data[0] ^= 0xFF;
            if (qsfpI2cWriteCB(80, 132, 1, data) < 0)
                return -1;

            return 0;
        }

        private void cbTemperatureCompensation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTemperatureCompensation.Checked == false) {
                if (_DisableTemperatureCompensation() < 0)
                    return;
                cbTemperatureCompensation.Enabled = false;
            }
        }
    }
}
