using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SfpCorrector
{
    public partial class UcSfpCorrector : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB qsfpI2cReadCB = null;
        private I2cWriteCB qsfpI2cWriteCB = null;

        public UcSfpCorrector()
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
            StreamReader srConfig;
            String[] sATmp;
            String sTmp;
            int line;

            if (tbFilePath.Text.Length == 0)
                return -1;

            srConfig = new StreamReader(tbFilePath.Text);

            if (srConfig == null)
                return -1;

            line = 1;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File empty error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 2) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }
            tbTemperatureOffset.Text = sATmp[0];
            tbTemperatureSlope.Text = sATmp[1];

            line++;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File without line " + line + " error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 6) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }
            tbRxPowerRateMax.Text = sATmp[0];
            tbRxPowerRateMin.Text = sATmp[1];
            tbRxPowerRate1.Text = sATmp[2];

            return 0;

        clearup:
            tbFilePath.Text = tbTemperatureOffset.Text =
                tbTemperatureSlope.Text = "";

            return -1;
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
            swConfig.WriteLine(tbTemperatureOffset.Text + "," +
                tbTemperatureSlope.Text);
            swConfig.WriteLine(tbRxPowerRateMax.Text + "," +
                tbRxPowerRateMin.Text + "," + tbRxPowerRate1.Text);

            swConfig.Close();
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

            data[0] = 82;
            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                return -1;

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

            if (qsfpI2cWriteCB(81, 158, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 82 };

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (qsfpI2cWriteCB(81, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _ReadVoltage()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float voltage;
            int tmp;

            tbTxVoltage.Text = "";

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(81, 98, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            voltage = tmp;
            voltage = voltage / 10000;
            tbTxVoltage.Text = voltage.ToString("#0.0000");

            if (qsfpI2cWriteCB == null)
                return -1;

            data = new byte[] { 81, 0 };
            qsfpI2cWriteCB(81, 127, 1, data);

            Thread.Sleep(1);

            if (qsfpI2cReadCB(81, 242, 1, data) != 1)
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
            tbVoltageOffset.Text = sData[0].ToString();

            return 0;
        }

        private void bVoltageRead_Click(object sender, EventArgs e)
        {
            if (_ReadVoltage() < 0)
                return;
        }

        private int _WriteVoltageCorrector()
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
                tmp[0] = Convert.ToSByte(tbVoltageOffset.Text);
            }
            catch (Exception eTSB) {
                MessageBox.Show("Voltage offset out of range (-128 ~ 127)!!\n" + eTSB.ToString());
                tbVoltageOffset.Text = "";
                return -1;
            }

            data[0] = 81;
            qsfpI2cWriteCB(81, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            qsfpI2cWriteCB(81, 242, 1, data);

            return 0;
        }

        private void bVoltageWrite_Click(object sender, EventArgs e)
        {
            if (_WriteVoltageCorrector() < 0)
                return;
        }

        private int _ResetVoltageOffset()
        {
            tbVoltageOffset.Text = "0";

            if (_WriteVoltageCorrector() < 0)
                return -1;

            cbVoltageCorrected.Checked = false;

            return 0;
        }

        private void bVoltageReset_Click(object sender, EventArgs e)
        {
            if (_ResetVoltageOffset() < 0)
                return;
        }

        private int _AutoCorrectVoltageOffset()
        {
            sbyte[] offset = new sbyte[1];
            float txVoltage, voltage, fOffset;

            if (tbVoltage.Text.Length == 0) {
                MessageBox.Show("Please input voltage!!");
                return -1;
            }

            if (tbTxVoltage.Text.Length == 0) {
                if (_ReadVoltage() < 0)
                    return -1;
            }

            try {
                txVoltage = Convert.ToSingle(tbTxVoltage.Text);
            }
            catch (Exception eTSTxVoltage) {
                MessageBox.Show(eTSTxVoltage.ToString());
                return -1;
            }

            try {
                voltage = Convert.ToSingle(tbVoltage.Text);
            }
            catch (Exception eTSVoltage) {
                MessageBox.Show(eTSVoltage.ToString());
                return -1;
            }

            fOffset = (voltage - txVoltage) * 1000 / 25;
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

            tbVoltageOffset.Text = offset[0].ToString();

            if (_WriteVoltageCorrector() < 0)
                return -1;

            cbVoltageCorrected.Checked = true;

            return 0;
        }

        private void bVoltageAutoCorrect_Click(object sender, EventArgs e)
        {
            if (_AutoCorrectVoltageOffset() < 0)
                return;
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

            if (qsfpI2cReadCB(81, 96, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");

            if (qsfpI2cWriteCB == null)
                return -1;

            data = new byte[] { 81, 0 };
            qsfpI2cWriteCB(81, 127, 1, data);
            Thread.Sleep(1);

            if (qsfpI2cReadCB(81, 243, 1, data) != 1)
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

            if (qsfpI2cReadCB(81, 244, 2, data) != 2)
                return -1;

            tmp = BitConverter.ToInt16(data, 0);
            tbTemperatureSlope.Text = tmp.ToString();

            return 0;
        }

        private void bTemperatureRead_Click(object sender, EventArgs e)
        {
            if (_ReadTemperature() < 0)
                return;
        }

        private int _WriteTemperatureCorrector()
        {
            byte[] data = new byte[2];
            byte[] bATmp;
            sbyte[] tmp = new sbyte[1];
            short tmpS16;

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

            try {
                tmpS16 = Convert.ToInt16(tbTemperatureSlope.Text);
            }
            catch (Exception eTSB) {
                MessageBox.Show("Temperature slope out of range (0 ~ 65536)!!\n" + eTSB.ToString());
                tbTemperatureSlope.Text = "";
                return -1;
            }

            data[0] = 81;
            qsfpI2cWriteCB(81, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            qsfpI2cWriteCB(81, 243, 1, data);

            bATmp = BitConverter.GetBytes(tmpS16);
            data[0] = bATmp[0];
            data[1] = bATmp[1];

            qsfpI2cWriteCB(81, 244, 2, data);

            return 0;
        }

        private void bTemperatureWrite_Click(object sender, EventArgs e)
        {
            if (_WriteTemperatureCorrector() < 0)
                return;
        }

        private int _ResetTemperatureOffset()
        {
            tbTemperatureOffset.Text = "0";

            if (_WriteTemperatureCorrector() < 0)
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

            fOffset = (temperature - txTemperature) * 2;
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

            if (_WriteTemperatureCorrector() < 0)
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

            tbRssi1.Text = "";
            tbRxPowerRate1.Text = "";
            tbRxPower1.Text = "";

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(81, 120, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();

            if (qsfpI2cReadCB(81, 104, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower1.Text = power.ToString("#0.0");

            if (qsfpI2cWriteCB == null)
                return -1;

            data = new byte[] { 82, 0, 0, 0 };
            qsfpI2cWriteCB(81, 127, 1, data);
            Thread.Sleep(1);

            if (qsfpI2cReadCB(81, 163, 1, data) != 1)
                return -1;

            tbRxPowerRateDefault.Text = data[0].ToString();

            if (tbRxPowerRateMax.Text.Length == 0)
                tbRxPowerRateMax.Text = "255";

            if (tbRxPowerRateMin.Text.Length == 0)
                tbRxPowerRateMin.Text = "0";

            data[0] = 81;
            qsfpI2cWriteCB(81, 127, 1, data);
            Thread.Sleep(1);

            if (qsfpI2cReadCB(81, 246, 1, data) != 1)
                return -1;

            tbRxPowerRate1.Text = data[0].ToString();

            return 0;
        }

        private void bRxPowerRateRead_Click(object sender, EventArgs e)
        {
            if (_ReadPowerRate() < 0)
                return;
        }

        private int _WritePowerRate()
        {
            byte[] data = new byte[] {81}; ;

            if (tbRxPowerRate1.Text.Length == 0) {
                MessageBox.Show("Please input Rx power rate!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            qsfpI2cWriteCB(81, 127, 1, data);

            try {
                data[0] = Convert.ToByte(tbRxPowerRate1.Text);
            }
            catch (Exception eTB) {
                MessageBox.Show("Rx power rate out of range (0 ~ 255)!!\n" + eTB.ToString());
                return -1;
            }

            qsfpI2cWriteCB(81, 246, 1, data);

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

            tbRxPowerRate1.Text = tbRxPowerRateDefault.Text;

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

            if (tbRxInputPower1.Text.Length == 0) {
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
                input = Convert.ToSingle(tbRxInputPowerRssi1.Text);
                rssi = Convert.ToSingle(tbRssi1.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRxPowerRate1.Text = rate.ToString();

            if ((rate > rateMax) || (rate < rateMin))
                MessageBox.Show("Rx1 rate: " + rate + " out of bound!!");

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
            byte[] data = new byte[14];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            Single sTmp;
            int iTmp;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 81;
            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                return -1;
            Thread.Sleep(1);

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(81, 128, 14, data) != 14)
                return -1;

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[0]) * 0.04);
            tbAverageCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[1]) * 0.04);
            tbAverageCurrentMax.Text = sTmp.ToString("#0.00");

            Buffer.BlockCopy(data, 2, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationACh1.Text = sTmp.ToString("#0.00");

            Array.Copy(data, 3, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationBCh1.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 5, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationCCh1.Text = iTmp.ToString();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[7]) * 0.04);
            tbModulationCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[8]) * 0.04);
            tbModulationCurrentMax.Text = sTmp.ToString("#0.00");

            Buffer.BlockCopy(data, 9, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationACh1.Text = sTmp.ToString("#0.00");

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationBCh1.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 12, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationCCh1.Text = iTmp.ToString();

            if (qsfpI2cReadCB(81, 252, 1, data) != 1)
                return -1;

            if (data[0] % 2 == 1)
                cbTemperatureCompensation.Checked = true;
            else
                cbTemperatureCompensation.Checked = false;

            return 0;
        }

        private void bAcMcRead_Click(object sender, EventArgs e)
        {
            bAcMcRead.Enabled = false;
            _ReadAverageCurrentAndModulationCurrentCorrectData();
            bAcMcRead.Enabled = true;
        }

        private int _WriteAcMcCorrectData()
        {

            byte[] data = new byte[14];
            byte[] bATmp;
            UInt16 u16Tmp;
            Int16 s16Tmp;
            sbyte[] sBATmp = new sbyte[1];

            if ((tbAverageCurrentMin.Text.Length == 0) ||
                (tbAverageCurrentMax.Text.Length == 0) ||
                (tbAverageCurrentEquationACh1.Text.Length == 0) ||
                (tbAverageCurrentEquationBCh1.Text.Length == 0) ||
                (tbAverageCurrentEquationCCh1.Text.Length == 0) ||
                (tbModulationCurrentMin.Text.Length == 0) ||
                (tbModulationCurrentMax.Text.Length == 0) ||
                (tbModulationCurrentEquationACh1.Text.Length == 0) ||
                (tbModulationCurrentEquationBCh1.Text.Length == 0) ||
                (tbModulationCurrentEquationCCh1.Text.Length == 0)) {
                MessageBox.Show("Please input value before write!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 81;
            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                return -1;

            if ((Convert.ToSingle(tbAverageCurrentMin.Text) < 0) ||
                Convert.ToSingle(tbAverageCurrentMin.Text) > 10.2) {
                MessageBox.Show("Average current min: " +
                    tbAverageCurrentMin.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[0] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMin.Text) / 0.04);
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
                data[1] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMax.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationACh1.Text) < -1.28) ||
                (Convert.ToSingle(tbAverageCurrentEquationACh1.Text) > 1.27)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationACh1.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[2] = (byte)Convert.ToSByte(Convert.ToSingle(tbAverageCurrentEquationACh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationBCh1.Text) < -3276.8) ||
                Convert.ToSingle(tbAverageCurrentEquationBCh1.Text) > 3276.7) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationBCh1.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbAverageCurrentEquationBCh1.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[3] = bATmp[1];
            data[4] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationCCh1.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[5] = bATmp[1];
            data[6] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentMin.Text) < 0) ||
                Convert.ToSingle(tbModulationCurrentMin.Text) > 10.2) {
                MessageBox.Show("Modulation current min: " +
                    tbModulationCurrentMin.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[7] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMin.Text) / 0.04);
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
                data[8] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMax.Text) / 0.04);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbModulationCurrentEquationACh1.Text) < -1.28) ||
                (Convert.ToSingle(tbModulationCurrentEquationACh1.Text) > 1.27)) {
                MessageBox.Show("Modualtion current equation A: " +
                tbModulationCurrentEquationACh1.Text +
                " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[9] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationACh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbModulationCurrentEquationBCh1.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationBCh1.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationACh1.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationBCh1.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[10] = bATmp[1];
            data[11] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationCCh1.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[12] = bATmp[1];
            data[13] = bATmp[0];

            if (qsfpI2cWriteCB(81, 128, 14, data) < 0)
                return -1;

            Thread.Sleep(1);

            if (cbTemperatureCompensation.Checked)
                data[0] = 0x01;
            else
                data[0] = 0x00;

            if (qsfpI2cWriteCB(81, 252, 1, data) < 0)
                return -1;

            return 0;
        }

        private void bAcMcWrite_Click(object sender, EventArgs e)
        {
            bAcMcWrite.Enabled = false;
            _WriteAcMcCorrectData();
            bAcMcWrite.Enabled = true;
        }

        private int _ResetAcMcEquation()
        {
            tbAverageCurrentMin.Text = tbAverageCurrentMax.Text =
                tbAverageCurrentEquationACh1.Text =
                tbAverageCurrentEquationBCh1.Text =
                tbAverageCurrentEquationCCh1.Text =
                tbModulationCurrentMin.Text =
                tbModulationCurrentMax.Text =
                tbModulationCurrentEquationACh1.Text =
                tbModulationCurrentEquationBCh1.Text =
                tbModulationCurrentEquationCCh1.Text = "0";

            return 0;
        }

        private void bLutClear_Click(object sender, EventArgs e)
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

            data[0] = 5;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 252, 1, data) != 1)
                return -1;

            data[0] &= 0xFE;
            if (qsfpI2cWriteCB(80, 252, 1, data) < 0)
                return -1;

            return 0;
        }

        private void cbTemperatureCompensation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTemperatureCompensation.Checked == false) {
                if (_DisableTemperatureCompensation() < 0)
                    return;
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            bVoltageReset_Click(sender, e);
            bTemperatureReset_Click(sender, e);
            bRxPowerRateReset_Click(sender, e);
        }

        private void bAutoCorrect_Click(object sender, EventArgs e)
        {
            bVoltageAutoCorrect_Click(sender, e);
            bTemperatureAutoCorrect_Click(sender, e);
            bRxPowerRateAutoCorrect_Click(sender, e);
        }

        private void tbRxInputPower1_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRxInputPower1.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRxInputPowerRssi1.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRxRssiRateNumerator_TextChanged(object sender, EventArgs e)
        {
            tbRxInputPower1_TextChanged(sender, e);
        }

        private void tbRxRssiRateDenominator_TextChanged(object sender, EventArgs e)
        {
            tbRxInputPower1_TextChanged(sender, e);
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];

            bStoreIntoFlash.Enabled = false;
            if (_WritePassword() < 0)
                goto exit;

            if (_SetQsfpMode(0x4D) < 0)
                goto exit;

            if (qsfpI2cWriteCB == null)
                goto exit;

            data[0] = 82;
            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (qsfpI2cWriteCB(81, 162, 1, data) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            bStoreIntoFlash.Enabled = true;
        }

        private void bLutTemperatureUpdate_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;

            bLutTemperatureUpdate.Enabled = false;

            data[0] = 82;
            if (qsfpI2cWriteCB(81, 127, 1, data) < 0)
                goto exit;

            Thread.Sleep(1);

            if (qsfpI2cReadCB(81, 175, 2, data) != 2)
                goto exit;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbLutTemperature.Text = tmp.ToString("#0");
        exit:
            bLutTemperatureUpdate.Enabled = true;
        }
    }
}
