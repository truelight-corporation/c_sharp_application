using System;
using System.IO;
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
        bool qsfpConnected;
        bool monitorConnected;
        I2cMaster i2cMasterQsfp = new I2cMaster();
        I2cMaster i2cMasterMonitor = new I2cMaster();

        public UcQsfpCorrector()
        {
            qsfpConnected = false;
            monitorConnected = false;
            InitializeComponent();
        }

        private int _QsfpConnect()
        {
            byte[] data = new byte[] {32};

            if (qsfpConnected == true)
                return 0;

            if (i2cMasterQsfp.ConnectApi(100) < 0)
                return -1;

            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto DeviceNoResponse;

            data[0] = 0x4D;

            if (i2cMasterQsfp.WriteApi(80, 164, 1, data) < 0)
                goto DeviceNoResponse;

            qsfpConnected = cbQsfpLinked.Checked = true;

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            cbQsfpLinked.Checked = false;
            return -1;
        }

        private int _QsfpDisconnect()
        {
            byte[] data = new byte[] { 32 };

            if (qsfpConnected == false)
                return -1;

            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto DeviceNoResponse;

            data[0] = 0;

            if (i2cMasterQsfp.WriteApi(80, 164, 1, data) < 0)
                goto DeviceNoResponse;

            if (i2cMasterQsfp.DisconnectApi() < 0)
                return -1;

            qsfpConnected = cbQsfpLinked.Checked = false;

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            qsfpConnected = cbQsfpLinked.Checked = false;
            return -1;
        }

        private void cbQsfpLinked_CheckedChanged(object sender, EventArgs e)
        {
            if (cbQsfpLinked.Checked == false) {
                if (_QsfpDisconnect() < 0)
                    return;
            }
            else {
                if (_QsfpConnect() < 0)
                    return;
            }
        }

        private int _MonitorConnect()
        {
            byte[] data = new byte[] { 2 };

            if (monitorConnected == true)
                return 0;

            if (i2cMasterMonitor.ConnectApi(100) < 0)
                return -1;

            monitorConnected = cbMonitorLinked.Checked = true;

            return 0;
        }


        private int _MonitorDisconnect()
        {
            if (monitorConnected == false)
                return 0;

            if (i2cMasterMonitor.DisconnectApi() < 0)
                return -1;

            monitorConnected = cbMonitorLinked.Checked = false;

            return 0;
        }

        private void _cbMonitorLinkedCheckedChanged(object sender, EventArgs e)
        {
            if (cbMonitorLinked.Checked == false) {
                if (_MonitorDisconnect() < 0)
                    return;
            }
            else {
                if (_MonitorConnect() < 0)
                    return;
            }
        }

        private int _ReadTemperature()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float temperature;
            int tmp;

            tbTxTemperature.Text = "";

            if (_QsfpConnect() < 0)
                return -1;

            if (i2cMasterQsfp.ReadApi(80, 22, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");

            data = new byte[] { 2, 0 };
            i2cMasterQsfp.WriteApi(80, 127, 1, data);
            if (i2cMasterQsfp.ReadApi(80, 137, 1, data) != 1)
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
            _QsfpDisconnect();
            return -1;
        }

        private int _WriteTemperatureOffset()
        {
            byte[] data = new byte[1];
            sbyte[] tmp = new sbyte[1];

            if (_QsfpConnect() < 0)
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
            i2cMasterQsfp.WriteApi(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            i2cMasterQsfp.WriteApi(80, 137, 1, data);

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

        private int _ReadPowerRate()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbRssi1.Text = tbRssi2.Text = tbRssi3.Text = tbRssi4.Text = "";
            tbRxPowerRate1.Text = tbRxPowerRate2.Text = tbRxPowerRate3.Text = tbRxPowerRate4.Text = "";
            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text = tbRxPower4.Text = "";

            if (_QsfpConnect() < 0)
                return -1;

            if (i2cMasterQsfp.ReadApi(80, 108, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();

            if (i2cMasterQsfp.ReadApi(80, 110, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi2.Text = tmp.ToString();

            if (i2cMasterQsfp.ReadApi(80, 112, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi3.Text = tmp.ToString();

            if (i2cMasterQsfp.ReadApi(80, 114, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi4.Text = tmp.ToString();

            if (i2cMasterQsfp.ReadApi(80, 34, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower1.Text = power.ToString("#0.0");

            if (i2cMasterQsfp.ReadApi(80, 36, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower2.Text = power.ToString("#0.0");

            if (i2cMasterQsfp.ReadApi(80, 38, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower3.Text = power.ToString("#0.0");

            if (i2cMasterQsfp.ReadApi(80, 40, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower4.Text = power.ToString("#0.0");

            data = new byte[] { 32, 0, 0, 0 };
            i2cMasterQsfp.WriteApi(80, 127, 1, data);
            if (i2cMasterQsfp.ReadApi(80, 163, 1, data) != 1)
                goto DeviceNoResponse;

            tbRxPowerRateDefault.Text = data[0].ToString();

            if (tbRxPowerRateMax.Text.Length == 0)
                tbRxPowerRateMax.Text = (data[0] + 15).ToString();

            if (tbRxPowerRateMin.Text.Length == 0)
                tbRxPowerRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 2, 0, 0, 0};
            i2cMasterQsfp.WriteApi(80, 127, 1, data);
            if (i2cMasterQsfp.ReadApi(80, 133, 4, data) != 4)
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

            i2cMasterQsfp.WriteApi(80, 127, 1, data);

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

            i2cMasterQsfp.WriteApi(80, 133, 4, data);

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
                tbAverageCurrentEquationA.Text + "," + 
                tbAverageCurrentEquationB.Text + "," +
                tbAverageCurrentEquationC.Text);

            swConfig.WriteLine(tbModulationCurrentMax.Text + "," +
                tbModulationCurrentMin.Text + "," +
                tbModulationCurrentEquationA.Text + "," +
                tbModulationCurrentEquationB.Text + "," +
                tbModulationCurrentEquationC.Text);

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
            tbAverageCurrentEquationA.Text = sATmp[2];
            tbAverageCurrentEquationB.Text = sATmp[3];
            tbAverageCurrentEquationC.Text = sATmp[4];

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
            tbModulationCurrentEquationA.Text = sATmp[2];
            tbModulationCurrentEquationB.Text = sATmp[3];
            tbModulationCurrentEquationC.Text = sATmp[4];

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

            if (_PaserLoadFile() < 0)
                return;
        }

        private int _WriteAcMcCorrectData()
        {

            byte[] data = new byte[16];
            byte[] bATmp;
            int i;
            UInt16 u16Tmp;
            Int16 s16Tmp;
            byte bChecksum;
            sbyte[] sBATmp = new sbyte[1];
            

            data[0] = 2;
            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto device_no_response;

            if ((Convert.ToSingle(tbAverageCurrentEquationA.Text) < 0) ||
                (Convert.ToSingle(tbAverageCurrentEquationA.Text) > 2.55)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationA.Text +
                    " out of range (0 ~ 2.55)!!");
                goto error;
            }

            try {
                data[0] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentEquationA.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            if ((Convert.ToSingle(tbAverageCurrentEquationB.Text) < 0) ||
                Convert.ToSingle(tbAverageCurrentEquationB.Text) > 6553.5) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationB.Text +
                    " out of range (0 ~ 6553.5)!!");
                goto error;
            }
            try {
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(tbAverageCurrentEquationB.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[1] = bATmp[1];
            data[2] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationC.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
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
                goto error;
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
                goto error;
            }

            if (Convert.ToSingle(tbModulationCurrentEquationA.Text) < 0 ||
                Convert.ToSingle(tbModulationCurrentEquationA.Text) > 2.55) {
                MessageBox.Show("Modualtion current equation A: " +
                    tbModulationCurrentEquationA.Text +
                    " out of range (0 ~ 2.55)!!");
                goto error;
            }
            try {
                data[7] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentEquationA.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            if (Convert.ToSingle(tbModulationCurrentEquationB.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationB.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationA.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                goto error;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationB.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[8] = bATmp[1];
            data[9] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationC.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
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
                goto error;
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
                goto error;
            }

            if (Convert.ToSingle(tbAverageCurrentOffset.Text) < -12.8 ||
                Convert.ToSingle(tbAverageCurrentOffset.Text) > 12.7) {
                MessageBox.Show("Average current offset: " +
                    tbAverageCurrentOffset.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                goto error;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbAverageCurrentOffset.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 14, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            if (Convert.ToSingle(tbModulationCurrentOffset.Text) < -12.8 ||
                Convert.ToSingle(tbModulationCurrentOffset.Text) > 12.7) {
                MessageBox.Show("Modulation current offset: " +
                    tbModulationCurrentOffset.Text +
                    " out of range (-12.8 ~ 12.7)!!");
                goto error;
            }
            try {
                sBATmp[0] = Convert.ToSByte(Convert.ToSingle(tbModulationCurrentOffset.Text) * 10);
                Buffer.BlockCopy(sBATmp, 0, data, 15, 1);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                goto error;
            }
            
            if (i2cMasterQsfp.WriteApi(80, 189, 16, data) < 0)
                goto device_no_response;

            for (i = 0, bChecksum = 0; i < 16; i++)
                bChecksum += data[i];

            data[0] = bChecksum;

            if (i2cMasterQsfp.WriteApi(80, 132, 1, data) < 0)
                goto device_no_response;

            return 0;

        device_no_response:
            MessageBox.Show("QSFP+ module no response Error!!");
            _QsfpDisconnect();
        error:
            return -1;
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

            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto device_no_response;

            data[0] = 0;
            if (i2cMasterQsfp.WriteApi(80, 191, 1, data) < 0)
                goto device_no_response;

            return 0;

        device_no_response:
            MessageBox.Show("QSFP+ module no response Error!!");
            _QsfpDisconnect();
            return -1;
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

            if (_ResetAcMcEquation() < 0)
                return;
        }

        private void _bAutoCorrectClick(object sender, EventArgs e)
        {
            if (_AutoCorrectTemperatureOffset() < 0)
                return;

            if (_AutoCorrectRxPowerRate() < 0)
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

            if (_QsfpConnect() < 0)
                return -1;

            data[0] = 2;
            
            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto DeviceNoResponse;

            if (i2cMasterQsfp.ReadApi(80, 189, 16, data) != 16)
                goto DeviceNoResponse;

            tbModuleAverageCurrentEquationA.Text = (Convert.ToSingle(data[0]) / 100).ToString("#0.00");

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
            tbAverageCurrentOffset.Text = sTmp.ToString("#0.0");

            Buffer.BlockCopy(data, 15, sData, 0, 1);
            sTmp = Convert.ToSingle(Convert.ToInt32(sData[0]) * 0.1);
            tbModulationCurrentOffset.Text = sTmp.ToString("#0.0");

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            tbModuleAverageCurrentEquationA.Text =
                tbModuleAverageCurrentEquationB.Text =
                tbModuleAverageCurrentEquationC.Text =
                tbModuleModulationCurrentEquationA.Text =
                tbModuleModulationCurrentEquationB.Text =
                tbModuleModulationCurrentEquationC.Text = "";
            return -1;
        }
        
        private void _bAcMcReadClick(object sender, EventArgs e)
        {
            if (_ReadAverageCurrentAndModulationCurrentCorrectData() < 0)
                return;
        }

        private void bAcMcWrite_Click(object sender, EventArgs e)
        {
            if (_WriteAcMcCorrectData() < 0)
                return;
        }

    }
}
