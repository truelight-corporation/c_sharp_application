﻿using System;
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

        private byte dataStorePage = 2;

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

        private int _ReadVersion()
        {
            byte[] data = new byte[2];

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 32;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB(80, 165, 2, data) != 2)
                return -1;

            if (data[0] > 48) //'0'
                dataStorePage = 4;
            else if (data[1] > 51) //'3'
                dataStorePage = 4;
            
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

            data = new byte[] { dataStorePage, 0 };
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

            if (qsfpI2cReadCB(80, 139, 2, data) != 2)
                return -1;

            tmp = BitConverter.ToUInt16(data, 0);
            tbTemperatureSlope.Text = tmp.ToString();

            return 0;
        }

        private void bTemperatureRead_Click(object sender, EventArgs e)
        {
            if (_ReadVersion() < 0)
                return;

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

        private int _WriteTemperatureCorrector()
        {
            byte[] data = new byte[2];
            byte[] bATmp;
            sbyte[] tmp = new sbyte[1];
            ushort tmpU16;

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
                tmpU16 = Convert.ToUInt16(tbTemperatureSlope.Text);
            }
            catch (Exception eTSB) {
                MessageBox.Show("Temperature slope out of range (0 ~ 65536)!!\n" + eTSB.ToString());
                tbTemperatureSlope.Text = "";
                return -1;
            }

            data[0] = dataStorePage;
            qsfpI2cWriteCB(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            qsfpI2cWriteCB(80, 137, 1, data);

            bATmp = BitConverter.GetBytes(tmpU16);
            data[0] = bATmp[0];
            data[1] = bATmp[1];

            qsfpI2cWriteCB(80, 139, 2, data);

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bTemperatureWrite_Click(object sender, EventArgs e)
        {
            if (_ReadVersion() < 0)
                return;

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
            if (_ReadVersion() < 0)
                return;

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
            if (_ReadVersion() < 0)
                return;

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

            data = new byte[] { dataStorePage, 0, 0, 0 };
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
            if (_ReadVersion() < 0)
                return;

            if (_ReadPowerRate() < 0)
                return;
        }

        private int _WritePowerRate()
        {
            byte[] data = new byte[] { dataStorePage, 0, 0, 0 }; ;

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
            if (_ReadVersion() < 0)
                return;

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
            if (_ReadVersion() < 0)
                return;

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

            try {
                input = Convert.ToSingle(tbRxInputPowerRssi2.Text);
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
                input = Convert.ToSingle(tbRxInputPowerRssi3.Text);
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
                input = Convert.ToSingle(tbRxInputPowerRssi4.Text);
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
            if (_ReadVersion() < 0)
                return;

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

            data[0] = dataStorePage;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 189, 14, data) != 14)
                return -1;

            Buffer.BlockCopy(data, 0, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationA.Text = sTmp.ToString("#0.00");

            Array.Copy(data, 1, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationB.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 3, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationC.Text = iTmp.ToString();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[5]) * 0.04);
            tbAverageCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[6]) * 0.04);
            tbAverageCurrentMax.Text = sTmp.ToString("#0.00");

            Buffer.BlockCopy(data, 7, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationA.Text = sTmp.ToString("#0.00");

            Array.Copy(data, 8, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationB.Text = sTmp.ToString("#0.0");

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationC.Text = iTmp.ToString();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[12]) * 0.04);
            tbModulationCurrentMin.Text = sTmp.ToString("#0.00");

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[13]) * 0.04);
            tbModulationCurrentMax.Text = sTmp.ToString("#0.00");

            if (qsfpI2cReadCB(80, 205, 22, data) != 22)
                return -1;

            Buffer.BlockCopy(data, 0, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbVhfCompPropEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 1, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 3, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 1000;
            tbVhfCompPropEquationC.Text = sTmp.ToString("#0.000");

            Buffer.BlockCopy(data, 5, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbVhfCompConstEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 6, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 8, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationC.Text = sTmp.ToString("#0.00");

            Buffer.BlockCopy(data, 10, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbAverageCurrentOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 11, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbAverageCurrentOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 12, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbAverageCurrentOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 13, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbAverageCurrentOffsetCh4.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 14, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModulationCurrentOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 15, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModulationCurrentOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 16, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModulationCurrentOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 17, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModulationCurrentOffsetCh4.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 18, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompPropOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 19, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompPropOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 20, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompPropOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 21, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompPropOffsetCh4.Text = sTmp.ToString("#0.0");

            if (qsfpI2cReadCB(80, 231, 22, data) != 22)
                return -1;

            Buffer.BlockCopy(data, 0, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompConstOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 1, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompConstOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 2, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompConstOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 3, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbVhfCompConstOffsetCh4.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 4, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbPeakEnEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 5, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakEnEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 7, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 1000;
            tbPeakEnEquationC.Text = sTmp.ToString("#0.000");

            Buffer.BlockCopy(data, 9, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 1000;
            tbPeakLenCtrlEquationA.Text = sTmp.ToString("#0.000");

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakLenCtrlEquationB.Text = sTmp.ToString("#0.0000");

            Array.Copy(data, 12, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 1000;
            tbPeakLenCtrlEquationC.Text = sTmp.ToString("#0.000");

            Buffer.BlockCopy(data, 14, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakEnOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 15, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakEnOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 16, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakEnOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 17, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakEnOffsetCh4.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 18, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakLenCtrlOffsetCh1.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 19, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakLenCtrlOffsetCh2.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 20, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakLenCtrlOffsetCh3.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 21, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbPeakLenCtrlOffsetCh4.Text = sTmp.ToString("#0.0");

            return 0;
        }

        private void bAcMcRead_Click(object sender, EventArgs e)
        {
            if (_ReadVersion() < 0)
                return;

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
                (tbAverageCurrentOffsetCh1.Text.Length == 0) ||
                (tbAverageCurrentOffsetCh2.Text.Length == 0) ||
                (tbAverageCurrentOffsetCh3.Text.Length == 0) ||
                (tbAverageCurrentOffsetCh4.Text.Length == 0) ||
                (tbModulationCurrentOffsetCh1.Text.Length == 0) ||
                (tbModulationCurrentOffsetCh2.Text.Length == 0) ||
                (tbModulationCurrentOffsetCh3.Text.Length == 0) ||
                (tbModulationCurrentOffsetCh4.Text.Length == 0) ||
                (tbVhfCompPropEquationA.Text.Length == 0) ||
                (tbVhfCompPropEquationB.Text.Length == 0) ||
                (tbVhfCompPropEquationC.Text.Length == 0) ||
                (tbVhfCompConstEquationA.Text.Length == 0) ||
                (tbVhfCompConstEquationB.Text.Length == 0) ||
                (tbVhfCompConstEquationC.Text.Length == 0) ||
                (tbVhfCompPropOffsetCh1.Text.Length == 0) ||
                (tbVhfCompPropOffsetCh2.Text.Length == 0) ||
                (tbVhfCompPropOffsetCh3.Text.Length == 0) ||
                (tbVhfCompPropOffsetCh4.Text.Length == 0) ||
                (tbVhfCompConstOffsetCh1.Text.Length == 0) ||
                (tbVhfCompConstOffsetCh2.Text.Length == 0) ||
                (tbVhfCompConstOffsetCh3.Text.Length == 0) ||
                (tbVhfCompConstOffsetCh4.Text.Length == 0) ||
                (tbPeakEnEquationA.Text.Length == 0) ||
                (tbPeakEnEquationB.Text.Length == 0) ||
                (tbPeakEnEquationC.Text.Length == 0) ||
                (tbPeakLenCtrlEquationA.Text.Length == 0) ||
                (tbPeakLenCtrlEquationB.Text.Length == 0) ||
                (tbPeakLenCtrlEquationC.Text.Length == 0) ||
                (tbPeakEnOffsetCh1.Text.Length == 0) ||
                (tbPeakEnOffsetCh2.Text.Length == 0) ||
                (tbPeakEnOffsetCh3.Text.Length == 0) ||
                (tbPeakEnOffsetCh4.Text.Length == 0) ||
                (tbPeakLenCtrlOffsetCh1.Text.Length == 0) ||
                (tbPeakLenCtrlOffsetCh2.Text.Length == 0) ||
                (tbPeakLenCtrlOffsetCh3.Text.Length == 0) ||
                (tbPeakLenCtrlOffsetCh4.Text.Length == 0)) {
                MessageBox.Show("Please input value before write!!");
                return -1;
            }

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = dataStorePage;
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

            if ((Convert.ToSingle(tbAverageCurrentEquationB.Text) < -3276.8) ||
                Convert.ToSingle(tbAverageCurrentEquationB.Text) > 3276.7) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationB.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbAverageCurrentEquationB.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
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

            if ((Convert.ToSingle(tbModulationCurrentEquationA.Text) < -1.28) ||
                (Convert.ToSingle(tbModulationCurrentEquationA.Text) > 1.27)) {
                    MessageBox.Show("Modualtion current equation A: " +
                    tbModulationCurrentEquationA.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[7] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationA.Text) * 100);
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

            data[14] = data[15] = 0;

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

            if (Convert.ToSingle(tbAverageCurrentOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffsetCh1.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 26, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbAverageCurrentOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffsetCh2.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 27, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbAverageCurrentOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffsetCh3.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 28, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbAverageCurrentOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffsetCh4.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 29, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffsetCh1.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 30, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffsetCh2.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 31, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffsetCh3.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 32, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbModulationCurrentOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffsetCh4.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 33, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompPropOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompPropOffsetCh1.Text) > 12.7) {
                MessageBox.Show("VHT comp prop offset: " +
                    tbVhfCompPropOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompPropOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 34, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompPropOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompPropOffsetCh2.Text) > 12.7) {
                MessageBox.Show("VHT comp prop offset: " +
                    tbVhfCompPropOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompPropOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 35, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompPropOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompPropOffsetCh3.Text) > 12.7) {
                MessageBox.Show("VHT comp prop offset: " +
                    tbVhfCompPropOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompPropOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 36, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompPropOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompPropOffsetCh4.Text) > 12.7) {
                MessageBox.Show("VHT comp prop offset: " +
                    tbVhfCompPropOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompPropOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 37, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (qsfpI2cWriteCB(80, 189, 38, data) < 0)
                return -1;

            for (i = 0, bChecksum = 1; i < 16; i++)
                bChecksum += data[i];

            if (Convert.ToSingle(tbVhfCompConstOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompConstOffsetCh1.Text) > 12.7) {
                MessageBox.Show("VHT comp const offset: " +
                    tbVhfCompConstOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompConstOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 0, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompConstOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompConstOffsetCh2.Text) > 12.7) {
                MessageBox.Show("VHT comp const offset: " +
                    tbVhfCompConstOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompConstOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 1, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompConstOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompConstOffsetCh3.Text) > 12.7) {
                MessageBox.Show("VHT comp const offset: " +
                    tbVhfCompConstOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompConstOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 2, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbVhfCompConstOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbVhfCompConstOffsetCh4.Text) > 12.7) {
                MessageBox.Show("VHT comp const offset: " +
                    tbVhfCompConstOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbVhfCompConstOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 3, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbPeakEnEquationA.Text) < -0.128) ||
                (Convert.ToSingle(tbPeakEnEquationA.Text) > 0.127)) {
                MessageBox.Show("Peak En equation A: " +
                    tbPeakEnEquationA.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }

            try {
                data[4] = (byte)Convert.ToSByte(Convert.ToSingle(tbPeakEnEquationA.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakEnEquationB.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakEnEquationB.Text) > 3.2767) {
                MessageBox.Show("Peak En equation B: " +
                    tbPeakEnEquationB.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationB.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[5] = bATmp[1];
            data[6] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationC.Text) < -32.768 ||
                Convert.ToSingle(tbPeakEnEquationC.Text) > 32.767) {
                MessageBox.Show("Peak En equation C: " +
                    tbPeakEnEquationC.Text +
                    " out of range (-32.768 ~ 32.767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationC.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[7] = bATmp[1];
            data[8] = bATmp[0];

            if ((Convert.ToSingle(tbPeakLenCtrlEquationA.Text) < -0.128) ||
                (Convert.ToSingle(tbPeakLenCtrlEquationA.Text) > 0.127)) {
                MessageBox.Show("Peak Len Ctrl equation A: " +
                    tbPeakLenCtrlEquationA.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }

            try {
                data[9] = (byte)Convert.ToSByte(Convert.ToSingle(tbPeakLenCtrlEquationA.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakLenCtrlEquationB.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationB.Text) > 3.2767) {
                MessageBox.Show("Peak Len Ctrl equation B: " +
                    tbPeakLenCtrlEquationB.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationB.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[10] = bATmp[1];
            data[11] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationC.Text) < -32.768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationC.Text) > 32.767) {
                MessageBox.Show("Peak Len Ctrl equation C: " +
                    tbPeakLenCtrlEquationC.Text +
                    " out of range (-32.768 ~ 32.767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationC.Text) * 1000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[12] = bATmp[1];
            data[13] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbPeakEnOffsetCh1.Text) > 12.7) {
                MessageBox.Show("Peak En offset Ch1: " +
                    tbPeakEnOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakEnOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 14, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakEnOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbPeakEnOffsetCh2.Text) > 12.7) {
                MessageBox.Show("Peak En offset Ch2: " +
                    tbPeakEnOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakEnOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 15, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakEnOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbPeakEnOffsetCh3.Text) > 12.7) {
                MessageBox.Show("Peak En offset Ch3: " +
                    tbPeakEnOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakEnOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 16, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakEnOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbPeakEnOffsetCh4.Text) > 12.7) {
                MessageBox.Show("Peak En offset Ch4: " +
                    tbPeakEnOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakEnOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 17, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakLenCtrlOffsetCh1.Text) < -12.8 ||
                Convert.ToSingle(tbPeakLenCtrlOffsetCh1.Text) > 12.7) {
                MessageBox.Show("Peak Len Ctrl offset Ch1: " +
                    tbPeakLenCtrlOffsetCh1.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakLenCtrlOffsetCh1.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 18, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakLenCtrlOffsetCh2.Text) < -12.8 ||
                Convert.ToSingle(tbPeakLenCtrlOffsetCh2.Text) > 12.7) {
                MessageBox.Show("Peak Len Ctrl offset Ch2: " +
                    tbPeakLenCtrlOffsetCh2.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakLenCtrlOffsetCh2.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 19, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakLenCtrlOffsetCh3.Text) < -12.8 ||
                Convert.ToSingle(tbPeakLenCtrlOffsetCh3.Text) > 12.7) {
                MessageBox.Show("Peak Len Ctrl offset Ch3: " +
                    tbPeakLenCtrlOffsetCh3.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakLenCtrlOffsetCh3.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 20, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (Convert.ToSingle(tbPeakLenCtrlOffsetCh4.Text) < -12.8 ||
                Convert.ToSingle(tbPeakLenCtrlOffsetCh4.Text) > 12.7) {
                MessageBox.Show("Peak Len Ctrl offset Ch4: " +
                    tbPeakLenCtrlOffsetCh4.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                return -1;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbPeakLenCtrlOffsetCh4.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 21, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if (qsfpI2cWriteCB(80, 231, 22, data) < 0)
                return -1;

            data[0] = bChecksum;

            if (qsfpI2cWriteCB(80, 132, 1, data) < 0)
                return -1;

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bAcMcWrite_Click(object sender, EventArgs e)
        {
            if (_ReadVersion() < 0)
                return;

            if (_WriteAcMcCorrectData() < 0)
                return;

            cbTemperatureCompensation.Checked = true;
            cbTemperatureCompensation.Enabled = true;
        }

        private int _ResetAcMcEquation()
        {
            byte[] data = new byte[] { dataStorePage };

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB(80, 191, 1, data) != 1)
                return -1;

            data[0] ^= 0xFF;
            if (qsfpI2cWriteCB(80, 191, 1, data) < 0)
                return -1;

            _ClearPassword();
            _SetQsfpMode(0);

            return 0;
        }

        private void bLutReset_Click(object sender, EventArgs e)
        {
            if (_ReadVersion() < 0)
                return;

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

            data[0] = dataStorePage;
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
            if (_ReadVersion() < 0)
                return;

            if (cbTemperatureCompensation.Checked == false) {
                if (_DisableTemperatureCompensation() < 0)
                    return;
                cbTemperatureCompensation.Enabled = false;
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            bTemperatureReset_Click(sender, e);
            bRxPowerRateReset_Click(sender, e);
        }

        private void bAutoCorrect_Click(object sender, EventArgs e)
        {
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

        private void tbRxInputPower2_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRxInputPower2.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRxInputPowerRssi2.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRxInputPower3_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRxInputPower3.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRxInputPowerRssi3.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRxInputPower4_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRxInputPower4.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRxInputPowerRssi4.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRxRssiRateNumerator_TextChanged(object sender, EventArgs e)
        {
            tbRxInputPower1_TextChanged(sender, e);
            tbRxInputPower2_TextChanged(sender, e);
            tbRxInputPower3_TextChanged(sender, e);
            tbRxInputPower4_TextChanged(sender, e);
        }

        private void tbRxRssiRateDenominator_TextChanged(object sender, EventArgs e)
        {
            tbRxInputPower1_TextChanged(sender, e);
            tbRxInputPower2_TextChanged(sender, e);
            tbRxInputPower3_TextChanged(sender, e);
            tbRxInputPower4_TextChanged(sender, e);
        }
    }
}
