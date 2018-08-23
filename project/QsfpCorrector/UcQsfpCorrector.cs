﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpCorrector
{
    public partial class UcQsfpCorrector : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB qsfpI2cReadCB = null;
        private I2cWriteCB qsfpI2cWriteCB = null;
        private bool disableSend = false;

        public UcQsfpCorrector()
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

        private int _WritePassword()
        {
            byte[] data;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (tbPassword.Text.Length != 4) {
                tbPassword.Text = "";
                MessageBox.Show("Please input 4 char password before write!!");
                return -1;
            }

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
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

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] {32};

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (qsfpI2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
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

        private int _ResetTemperatureOffset()
        {
            tbTemperatureOffset.Text = "0";

            if (_WriteTemperatureOffset() < 0)
                return -1;

            cbTemperatureCorrected.Checked = false;

            return 0;
        }

        private void _bTemperatureResetClick(object sender, EventArgs e)
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

        private void _bTemperatureAutoCorrectClick(object sender, EventArgs e)
        {
            if (_AutoCorrectTemperatureOffset() < 0)
                return;
        }

        private int _ReadRxPowerRate()
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

            data[0] = 32;
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 128, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 130, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi2.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 132, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi3.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 134, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi4.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 163, 1, data) != 1)
                return -1;

            tbRxPowerRateDefault.Text = data[0].ToString();

            if (tbRxPowerRateMax.Text.Length == 0)
                tbRxPowerRateMax.Text = (data[0] + 15).ToString();

            if (tbRxPowerRateMin.Text.Length == 0)
                tbRxPowerRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 4, 0, 0, 0};
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 244, 4, data) != 4)
                return -1;

            tbRxPowerRate1.Text = data[0].ToString();
            tbRxPowerRate2.Text = data[1].ToString();
            tbRxPowerRate3.Text = data[2].ToString();
            tbRxPowerRate4.Text = data[3].ToString();

            return 0;
        }

        private void _bRxPowerRateReadClick(object sender, EventArgs e)
        {
            if (_ReadRxPowerRate() < 0)
                return;
        }

        private int _WritePowerRate()
        {
            byte[] data = new byte[] {4, 0, 0, 0};;

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

            qsfpI2cWriteCB(80, 244, 4, data);

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void _bRxPowerRateWriteClick(object sender, EventArgs e)
        {
            if (_WritePowerRate() < 0)
                return;
        }

        private int _ResetRxPowerRate()
        {
            if (tbRxPowerRateDefault.Text.Length == 0) {
                if (_ReadRxPowerRate() < 0)
                    return -1;
            }

            tbRxPowerRate1.Text = tbRxPowerRate2.Text = tbRxPowerRate3.Text = tbRxPowerRate4.Text =
                tbRxPowerRateDefault.Text;

            if (_WritePowerRate() < 0)
                return -1;

            cbRxPowerRateCorrected.Checked = false;

            return 0;
        }

        private void _bRxPowerRateResetClick(object sender, EventArgs e)
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
                if (_ReadRxPowerRate() < 0)
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

        private void _bRxPowerRateAutoCorrectClick(object sender, EventArgs e)
        {
            if (_AutoCorrectRxPowerRate() < 0)
                return;
        }

        private void _bSaveFileClick(object sender, EventArgs e)
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
            swConfig.WriteLine(tbTemperature.Text);
            swConfig.WriteLine(tbRxInputPower1.Text + "," +
                tbRxInputPower2.Text + "," + tbRxInputPower3.Text + "," +
                tbRxInputPower4.Text + "," + tbRxPowerRateMax.Text + "," +
                tbRxPowerRateMin.Text);

            swConfig.WriteLine(tbAverageCurrentMax.Text + "," +
                tbAverageCurrentMin.Text + "," +
                tbAverageCurrentOffset.Text + "," +
                tbAverageCurrentEquationA.Text + "," + 
                tbAverageCurrentEquationB.Text + "," +
                tbAverageCurrentEquationC.Text);

            swConfig.WriteLine(tbModulationCurrentMax.Text + "," +
                tbModulationCurrentMin.Text + "," +
                tbModulationCurrentOffset.Text + "," +
                tbModulationCurrentEquationA.Text + "," +
                tbModulationCurrentEquationB.Text + "," +
                tbModulationCurrentEquationC.Text);

            swConfig.WriteLine(tbDrvtnMod1.Text + "," +
                tbDrvtnMod2.Text + "," +
                tbDrvtnMod3.Text + "," +
                tbDrvtnMod4.Text + "," +
                tbDrvtnMod5.Text + "," +
                tbDrvtnMod6.Text + "," +
                tbDrvtnMod7.Text);

            swConfig.WriteLine(tbDrvtnCst1.Text + "," +
                tbDrvtnCst2.Text + "," +
                tbDrvtnCst3.Text + "," +
                tbDrvtnCst4.Text + "," +
                tbDrvtnCst5.Text + "," +
                tbDrvtnCst6.Text + "," +
                tbDrvtnCst7.Text);

            swConfig.WriteLine(tbEfMod1.Text + "," +
                tbEfMod2.Text + "," +
                tbEfMod3.Text + "," +
                tbEfMod4.Text + "," +
                tbEfMod5.Text + "," +
                tbEfMod6.Text + "," +
                tbEfMod7.Text);

            swConfig.WriteLine(tbEfCst1.Text + "," +
                tbEfCst2.Text + "," +
                tbEfCst3.Text + "," +
                tbEfCst4.Text + "," +
                tbEfCst5.Text + "," +
                tbEfCst6.Text + "," +
                tbEfCst7.Text);

            swConfig.Close();
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
            tbTemperature.Text = sTmp;

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
            tbRxInputPower1.Text = sATmp[0];
            tbRxInputPower2.Text = sATmp[1];
            tbRxInputPower3.Text = sATmp[2];
            tbRxInputPower4.Text = sATmp[3];
            tbRxPowerRateMin.Text = sATmp[4];
            tbRxPowerRateMax.Text = sATmp[5];

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
            tbAverageCurrentMax.Text = sATmp[0];
            tbAverageCurrentMin.Text = sATmp[1];
            tbAverageCurrentOffset.Text = sATmp[2];
            tbAverageCurrentEquationA.Text = sATmp[3];
            tbAverageCurrentEquationB.Text = sATmp[4];
            tbAverageCurrentEquationC.Text = sATmp[5];

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

            tbModulationCurrentMax.Text = sATmp[0];
            tbModulationCurrentMin.Text = sATmp[1];
            tbModulationCurrentOffset.Text = sATmp[2];
            tbModulationCurrentEquationA.Text = sATmp[3];
            tbModulationCurrentEquationB.Text = sATmp[4];
            tbModulationCurrentEquationC.Text = sATmp[5];

            line++;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File without line " + line + " error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 7) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }

            tbDrvtnMod1.Text = sATmp[0];
            tbDrvtnMod2.Text = sATmp[1];
            tbDrvtnMod3.Text = sATmp[2];
            tbDrvtnMod4.Text = sATmp[3];
            tbDrvtnMod5.Text = sATmp[4];
            tbDrvtnMod6.Text = sATmp[5];
            tbDrvtnMod7.Text = sATmp[6];

            line++;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File without line " + line + " error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 7) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }

            tbDrvtnCst1.Text = sATmp[0];
            tbDrvtnCst2.Text = sATmp[1];
            tbDrvtnCst3.Text = sATmp[2];
            tbDrvtnCst4.Text = sATmp[3];
            tbDrvtnCst5.Text = sATmp[4];
            tbDrvtnCst6.Text = sATmp[5];
            tbDrvtnCst7.Text = sATmp[6];

            line++;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File without line " + line + " error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 7) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }

            tbEfMod1.Text = sATmp[0];
            tbEfMod2.Text = sATmp[1];
            tbEfMod3.Text = sATmp[2];
            tbEfMod4.Text = sATmp[3];
            tbEfMod5.Text = sATmp[4];
            tbEfMod6.Text = sATmp[5];
            tbEfMod7.Text = sATmp[6];

            line++;
            sTmp = srConfig.ReadLine();
            if (sTmp == null) {
                MessageBox.Show("File without line " + line + " error!!");
                goto clearup;
            }
            sATmp = sTmp.Split(',');
            if (sATmp.Length != 7) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }

            tbEfCst1.Text = sATmp[0];
            tbEfCst2.Text = sATmp[1];
            tbEfCst3.Text = sATmp[2];
            tbEfCst4.Text = sATmp[3];
            tbEfCst5.Text = sATmp[4];
            tbEfCst6.Text = sATmp[5];
            tbEfCst7.Text = sATmp[6];

            srConfig.Close();

            return 0;

        clearup:
            tbFilePath.Text = tbTemperature.Text = tbRxInputPower1.Text =
                tbRxInputPower2.Text = tbRxInputPower3.Text =
                tbRxInputPower4.Text = tbRxPowerRateMin.Text =
                tbRxPowerRateMax.Text = tbAverageCurrentMax.Text = 
                tbAverageCurrentMin.Text = 
                tbAverageCurrentEquationA.Text =
                tbAverageCurrentEquationB.Text =
                tbAverageCurrentEquationC.Text =
                tbModulationCurrentMax.Text = 
                tbModulationCurrentMin.Text = 
                tbModulationCurrentEquationA.Text = 
                tbModulationCurrentEquationB.Text =
                tbModulationCurrentEquationC.Text = "";
            
            return -1;
        }

        private void _bLoadFileClick(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Filter = "txt files (*.txt)|*.txt";
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbFilePath.Text = ofdSelectFile.FileName;
            if (tbFilePath.Text.Length == 0)
                return;
            disableSend = true;
            if (_PaserLoadFile() < 0) {
                disableSend = false;
                return;
            }
            disableSend = false;
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
                (tbDrvtnMod1.Text.Length == 0) ||
                (tbDrvtnMod2.Text.Length == 0) ||
                (tbDrvtnMod3.Text.Length == 0) ||
                (tbDrvtnMod4.Text.Length == 0) ||
                (tbDrvtnMod5.Text.Length == 0) ||
                (tbDrvtnMod6.Text.Length == 0) ||
                (tbDrvtnMod7.Text.Length == 0) ||
                (tbDrvtnCst1.Text.Length == 0) ||
                (tbDrvtnCst2.Text.Length == 0) ||
                (tbDrvtnCst3.Text.Length == 0) ||
                (tbDrvtnCst4.Text.Length == 0) ||
                (tbDrvtnCst5.Text.Length == 0) ||
                (tbDrvtnCst6.Text.Length == 0) ||
                (tbDrvtnCst7.Text.Length == 0) ||
                (tbEfMod1.Text.Length == 0) ||
                (tbEfMod2.Text.Length == 0) ||
                (tbEfMod3.Text.Length == 0) ||
                (tbEfMod4.Text.Length == 0) ||
                (tbEfMod5.Text.Length == 0) ||
                (tbEfMod6.Text.Length == 0) ||
                (tbEfMod7.Text.Length == 0) ||
                (tbEfCst1.Text.Length == 0) ||
                (tbEfCst2.Text.Length == 0) ||
                (tbEfCst3.Text.Length == 0) ||
                (tbEfCst4.Text.Length == 0) ||
                (tbEfCst5.Text.Length == 0) ||
                (tbEfCst6.Text.Length == 0) ||
                (tbEfCst7.Text.Length == 0)) {
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

            if (Convert.ToSingle(tbDrvtnMod1.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod1.Text) > 255) {
                MessageBox.Show("DRVTNMOD 1: " + tbDrvtnMod1.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[16] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod1.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod2.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod2.Text) > 255) {
                MessageBox.Show("DRVTNMOD 2: " + tbDrvtnMod2.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[17] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod2.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod3.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod3.Text) > 255) {
                MessageBox.Show("DRVTNMOD 3: " + tbDrvtnMod3.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[18] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod3.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod4.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod4.Text) > 255) {
                MessageBox.Show("DRVTNMOD 4: " + tbDrvtnMod4.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[19] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod4.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod5.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod5.Text) > 255) {
                MessageBox.Show("DRVTNMOD 5: " + tbDrvtnMod5.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[20] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod5.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod6.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod6.Text) > 255) {
                MessageBox.Show("DRVTNMOD 6: " + tbDrvtnMod6.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[21] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod6.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnMod7.Text) < 0 ||
                Convert.ToSingle(tbDrvtnMod7.Text) > 255) {
                MessageBox.Show("DRVTNMOD 7: " + tbDrvtnMod7.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[22] = Convert.ToByte(Convert.ToSingle(tbDrvtnMod7.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbDrvtnCst1.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst1.Text) > 255) {
                MessageBox.Show("DRVTNMOD 1: " + tbDrvtnCst1.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[23] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst1.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst2.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst2.Text) > 255) {
                MessageBox.Show("DRVTNMOD 2: " + tbDrvtnCst2.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[24] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst2.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst3.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst3.Text) > 255) {
                MessageBox.Show("DRVTNMOD 3: " + tbDrvtnCst3.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[25] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst3.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst4.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst4.Text) > 255) {
                MessageBox.Show("DRVTNMOD 4: " + tbDrvtnCst4.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[26] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst4.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst5.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst5.Text) > 255) {
                MessageBox.Show("DRVTNMOD 5: " + tbDrvtnCst5.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[27] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst5.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst6.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst6.Text) > 255) {
                MessageBox.Show("DRVTNMOD 6: " + tbDrvtnCst6.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[28] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst6.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbDrvtnCst7.Text) < 0 ||
                Convert.ToSingle(tbDrvtnCst7.Text) > 255) {
                MessageBox.Show("DRVTNMOD 7: " + tbDrvtnCst7.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[29] = Convert.ToByte(Convert.ToSingle(tbDrvtnCst7.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbEfMod1.Text) < 0 ||
                Convert.ToSingle(tbEfMod1.Text) > 255) {
                MessageBox.Show("DRVTNMOD 1: " + tbEfMod1.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[30] = Convert.ToByte(Convert.ToSingle(tbEfMod1.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod2.Text) < 0 ||
                Convert.ToSingle(tbEfMod2.Text) > 255) {
                MessageBox.Show("DRVTNMOD 2: " + tbEfMod2.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[31] = Convert.ToByte(Convert.ToSingle(tbEfMod2.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod3.Text) < 0 ||
                Convert.ToSingle(tbEfMod3.Text) > 255) {
                MessageBox.Show("DRVTNMOD 3: " + tbEfMod3.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[32] = Convert.ToByte(Convert.ToSingle(tbEfMod3.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod4.Text) < 0 ||
                Convert.ToSingle(tbEfMod4.Text) > 255) {
                MessageBox.Show("DRVTNMOD 4: " + tbEfMod4.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[33] = Convert.ToByte(Convert.ToSingle(tbEfMod4.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod5.Text) < 0 ||
                Convert.ToSingle(tbEfMod5.Text) > 255) {
                MessageBox.Show("DRVTNMOD 5: " + tbEfMod5.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[34] = Convert.ToByte(Convert.ToSingle(tbEfMod5.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod6.Text) < 0 ||
                Convert.ToSingle(tbEfMod6.Text) > 255) {
                MessageBox.Show("DRVTNMOD 6: " + tbEfMod6.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[35] = Convert.ToByte(Convert.ToSingle(tbEfMod6.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfMod7.Text) < 0 ||
                Convert.ToSingle(tbEfMod7.Text) > 255) {
                MessageBox.Show("DRVTNMOD 7: " + tbEfMod7.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[36] = Convert.ToByte(Convert.ToSingle(tbEfMod7.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbEfCst1.Text) < 0 ||
                Convert.ToSingle(tbEfCst1.Text) > 255) {
                MessageBox.Show("DRVTNMOD 1: " + tbEfCst1.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[37] = Convert.ToByte(Convert.ToSingle(tbEfCst1.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst2.Text) < 0 ||
                Convert.ToSingle(tbEfCst2.Text) > 255) {
                MessageBox.Show("DRVTNMOD 2: " + tbEfCst2.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[38] = Convert.ToByte(Convert.ToSingle(tbEfCst2.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst3.Text) < 0 ||
                Convert.ToSingle(tbEfCst3.Text) > 255) {
                MessageBox.Show("DRVTNMOD 3: " + tbEfCst3.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[39] = Convert.ToByte(Convert.ToSingle(tbEfCst3.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst4.Text) < 0 ||
                Convert.ToSingle(tbEfCst4.Text) > 255) {
                MessageBox.Show("DRVTNMOD 4: " + tbEfCst4.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[40] = Convert.ToByte(Convert.ToSingle(tbEfCst4.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst5.Text) < 0 ||
                Convert.ToSingle(tbEfCst5.Text) > 255) {
                MessageBox.Show("DRVTNMOD 5: " + tbEfCst5.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[41] = Convert.ToByte(Convert.ToSingle(tbEfCst5.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst6.Text) < 0 ||
                Convert.ToSingle(tbEfCst6.Text) > 255) {
                MessageBox.Show("DRVTNMOD 6: " + tbEfCst6.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[42] = Convert.ToByte(Convert.ToSingle(tbEfCst6.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbEfCst7.Text) < 0 ||
                Convert.ToSingle(tbEfCst7.Text) > 255) {
                MessageBox.Show("DRVTNMOD 7: " + tbEfCst7.Text +
                    " out of range (0 ~ 255)!!");
                return -1;
            }
            try {
                data[43] = Convert.ToByte(Convert.ToSingle(tbEfCst7.Text));
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (qsfpI2cWriteCB(80, 189, 44, data) < 0)
                return -1;

            for (i = 0, bChecksum = 0; i < 44; i++)
                bChecksum += data[i];

            data[0] = bChecksum;

            if (qsfpI2cWriteCB(80, 132, 1, data) < 0)
                return -1;

            disableSend = false;

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private int _AutoCorrectAcMc()
        {
            if (_WriteAcMcCorrectData() < 0)
                return -1;

            return 0;
        }

        private void _bAcMcAutoCorrectClick(object sender, EventArgs e)
        {
            if (_AutoCorrectAcMc() < 0)
                return;
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

        private void _bResetClick(object sender, EventArgs e)
        {
            if (_ResetTemperatureOffset() < 0)
                return;

            if (_ResetRxPowerRate() < 0)
                return;

            if (_ResetTxPowerRate() < 0)
                return;

            if (_ResetAcMcEquation() < 0)
                return;
        }

        private void _bAutoCorrectClick(object sender, EventArgs e)
        {
            if (_AutoCorrectTemperatureOffset() < 0)
                return;

            if (_AutoCorrectRxPowerRate() < 0)
                return;

            if (_AutoCorrectTxPowerRate() < 0)
                return;

            if (_AutoCorrectAcMc() < 0)
                return;
        }

        private int _ReadAverageCurrentAndModulationCurrentCorrectData()
        {
            byte[] data = new byte[16];
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

            if (qsfpI2cReadCB(80, 189, 16, data) != 16)
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

            return 0;
        }
        
        private void _bAcMcReadClick(object sender, EventArgs e)
        {
            if (_ReadAverageCurrentAndModulationCurrentCorrectData() < 0)
                return;
        }

        private void bAcMcWrite_Click(object sender, EventArgs e)
        {
            if (cbTemperatureCompensation.Checked == true) {
                if (_WriteAcMcCorrectData() < 0)
                    return;
            }
            else
                cbTemperatureCompensation.Checked = true;
        }

        private int _DisableTemperatureCompensation()
        {
            byte[] data = new byte[1];

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 194, 1, data) != 16)
                return -1;

            data[0] &= 0xEF;
            if (qsfpI2cWriteCB(80, 194, 1, data) < 0)
                return -1;

            return 0;
        }

        private void cbTemperatureCompensation_CheckedChanged(object sender, EventArgs e)
        {
            if (disableSend == true)
                return;

            if (cbTemperatureCompensation.Checked == true) {
                if (_WriteAcMcCorrectData() < 0)
                    return;
            }
            else {
                if (_DisableTemperatureCompensation() < 0)
                    return;
            }
        }

        private int _ReadTxPowerRate()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbTxRssi1.Text = tbTxRssi2.Text = tbTxRssi3.Text = tbTxRssi4.Text = "";
            tbTxPowerRate1.Text = tbTxPowerRate2.Text = tbTxPowerRate3.Text = tbTxPowerRate4.Text = "";
            tbTxPower1.Text = tbTxPower2.Text = tbTxPower3.Text = tbTxPower4.Text = "";

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 50, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower1.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 52, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower2.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 54, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower3.Text = power.ToString("#0.0");

            if (qsfpI2cReadCB(80, 56, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower4.Text = power.ToString("#0.0");

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 32;
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 136, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi1.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 138, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi2.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 140, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi3.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 142, 2, data) != 2)
                return -1;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi4.Text = tmp.ToString();

            if (qsfpI2cReadCB(80, 162, 1, data) != 1)
                return -1;

            tbTxPowerRateDefault.Text = data[0].ToString();

            if (tbTxPowerRateMax.Text.Length == 0)
                tbTxPowerRateMax.Text = (data[0] + 15).ToString();

            if (tbTxPowerRateMin.Text.Length == 0)
                tbTxPowerRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 4, 0, 0, 0 };
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 248, 4, data) != 4)
                return -1;

            tbTxPowerRate1.Text = data[0].ToString();
            tbTxPowerRate2.Text = data[1].ToString();
            tbTxPowerRate3.Text = data[2].ToString();
            tbTxPowerRate4.Text = data[3].ToString();

            return 0;
        }

        private void bTxPowerRateRead_Click(object sender, EventArgs e)
        {
            if (_ReadTxPowerRate() < 0)
                return;
        }

        private int _WriteTxPowerRate()
        {
            byte[] data = new byte[] { 4, 0, 0, 0 }; ;

            if ((tbTxPowerRate1.Text.Length == 0) || (tbTxPowerRate2.Text.Length == 0) ||
                (tbTxPowerRate3.Text.Length == 0) || (tbTxPowerRate4.Text.Length == 0))
            {
                MessageBox.Show("Please input Tx power rate!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            qsfpI2cWriteCB(80, 127, 1, data);

            try
            {
                data[0] = Convert.ToByte(tbTxPowerRate1.Text);
                data[1] = Convert.ToByte(tbTxPowerRate2.Text);
                data[2] = Convert.ToByte(tbTxPowerRate3.Text);
                data[3] = Convert.ToByte(tbTxPowerRate4.Text);
            }
            catch (Exception eTB)
            {
                MessageBox.Show("Tx power rate out of range (0 ~ 255)!!\n" + eTB.ToString());
                return -1;
            }

            qsfpI2cWriteCB(80, 248, 4, data);

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bTxPowerRateWrite_Click(object sender, EventArgs e)
        {
            if (_WriteTxPowerRate() < 0)
                return;
        }

        private int _ResetTxPowerRate()
        {
            if (tbTxPowerRateDefault.Text.Length == 0)
            {
                if (_ReadTxPowerRate() < 0)
                    return -1;
            }

            tbTxPowerRate1.Text = tbTxPowerRate2.Text = tbTxPowerRate3.Text = tbTxPowerRate4.Text =
                tbTxPowerRateDefault.Text;

            if (_WriteTxPowerRate() < 0)
                return -1;

            cbTxPowerRateCorrected.Checked = false;

            return 0;
        }

        private void bTxPowerRateReset_Click(object sender, EventArgs e)
        {
            if (_ResetTxPowerRate() < 0)
                return;
        }

        private int _AutoCorrectTxPowerRate()
        {
            float input, rssi;
            int rate, rateMax, rateMin;

            rate = rateMax = rateMin = 0;

            if ((tbTxOutputPower1.Text.Length == 0) || (tbTxOutputPower2.Text.Length == 0) ||
                (tbTxOutputPower3.Text.Length == 0) || (tbTxOutputPower4.Text.Length == 0))
            {
                MessageBox.Show("Output power empty!!");
                return -1;
            }

            if (tbTxPowerRateDefault.Text.Length == 0)
            {
                if (_ReadTxPowerRate() < 0)
                    return -1;
            }

            try
            {
                rateMax = Convert.ToInt32(tbTxPowerRateMax.Text);
                rateMin = Convert.ToInt32(tbTxPowerRateMin.Text);
            }
            catch (Exception eTI)
            {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            try
            {
                input = Convert.ToSingle(tbTxOutputPower1.Text);
                rssi = Convert.ToSingle(tbTxRssi1.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
            tbTxPowerRate1.Text = rate.ToString();

            if ((rate > rateMax) || (rate < rateMin))
                MessageBox.Show("Tx1 rate: " + rate + " out of bound!!");

            try
            {
                input = Convert.ToSingle(tbTxOutputPower2.Text);
                rssi = Convert.ToSingle(tbTxRssi2.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
            tbTxPowerRate2.Text = rate.ToString();

            if ((rate > rateMax) || (rate < rateMin))
                MessageBox.Show("Tx2 rate: " + rate + " out of bound!!");

            try
            {
                input = Convert.ToSingle(tbTxOutputPower3.Text);
                rssi = Convert.ToSingle(tbTxRssi3.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
            tbTxPowerRate3.Text = rate.ToString();

            if ((rate > rateMax) || (rate < rateMin))
                MessageBox.Show("Tx3 rate: " + rate + " out of bound!!");

            try
            {
                input = Convert.ToSingle(tbTxOutputPower4.Text);
                rssi = Convert.ToSingle(tbTxRssi4.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
            tbTxPowerRate4.Text = rate.ToString();

            if ((rate > rateMax) || (rate < rateMin))
                MessageBox.Show("Tx4 rate: " + rate + " out of bound!!");

            if (_WriteTxPowerRate() < 0)
                return -1;

            cbTxPowerRateCorrected.Checked = true;

            return 0;
        }

        private void bTxPowerRateAutoCorrect_Click(object sender, EventArgs e)
        {
            if (_AutoCorrectTxPowerRate() < 0)
                return;
        }
    }
}
