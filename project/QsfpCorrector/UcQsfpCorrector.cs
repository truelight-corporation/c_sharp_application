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
        private DataTable dtEquation = new DataTable();

        public UcQsfpCorrector()
        {
            qsfpConnected = false;
            monitorConnected = false;
            InitializeComponent();
            dtEquation.Columns.Add("CEDP", typeof(string));
            dtEquation.Columns.Add("ACE's a", typeof(string));
            dtEquation.Columns.Add("ACE's b", typeof(string));
            dtEquation.Columns.Add("ACE's c", typeof(string));
            dtEquation.Columns.Add("MCE's a", typeof(string));
            dtEquation.Columns.Add("MCE's b", typeof(string));
            dtEquation.Columns.Add("MCE's c", typeof(string));
            dgvEquation.DataSource = dtEquation;
        }

        private int _QsfpConnect()
        {
            byte[] data = new byte[] {2};

            if (qsfpConnected == true)
                return 0;

            if (i2cMasterQsfp.ConnectApi(100) < 0)
                return -1;

            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto DeviceNoResponse;

            data[0] = 0x4D;

            if (i2cMasterQsfp.WriteApi(80, 191, 1, data) < 0)
                goto DeviceNoResponse;

            qsfpConnected = cbQsfpLinked.Checked = true;

            return 0;

        DeviceNoResponse:
            MessageBox.Show("QSFP+ no reponse!!");
            return -1;
        }

        private int _QsfpDisconnect()
        {
            if (qsfpConnected == false)
                return -1;

            if (i2cMasterQsfp.DisconnectApi() < 0)
                return -1;

            qsfpConnected = cbQsfpLinked.Checked = false;

            return 0;
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
            swConfig.WriteLine(tbRxInputPower1.Text + ", " +
                tbRxInputPower2.Text + ", " + tbRxInputPower3.Text + ", " +
                tbRxInputPower4.Text + ", " + tbRxPowerRateMax.Text + ", " +
                tbRxPowerRateMin.Text);
            swConfig.WriteLine(tbTxPower.Text + ", " + tbAverageCurrentMin.Text
                + ", " + tbAverageCurrentMax.Text + ", " +
                tbModulationCurrentMin.Text + ", " +
                tbModulationCurrentMax.Text);

            foreach (DataRow row in dtEquation.Rows) {
                swConfig.WriteLine(Convert.ToString(row.ItemArray[0]) + ", " +
                    Convert.ToString(row.ItemArray[1]) + ", " +
                    Convert.ToString(row.ItemArray[2]) + ", " +
                    Convert.ToString(row.ItemArray[3]) + ", " +
                    Convert.ToString(row.ItemArray[4]) + ", " +
                    Convert.ToString(row.ItemArray[5]) + ", " +
                    Convert.ToString(row.ItemArray[6]));
            }

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
            if (sATmp.Length != 5) {
                MessageBox.Show("Line " + line + " context paser error");
                goto clearup;
            }
            tbTxPower.Text = sATmp[0];
            tbAverageCurrentMax.Text = sATmp[1];
            tbAverageCurrentMin.Text = sATmp[2];
            tbModulationCurrentMax.Text = sATmp[3];
            tbModulationCurrentMin.Text = sATmp[4];

            line++;
            sTmp = srConfig.ReadLine();
            while ((sTmp != null) && (sTmp.Length != 0)) {
                sATmp = sTmp.Split(',');
                if (sATmp.Length != 7) {
                    MessageBox.Show("Line " + line + " context paser error");
                    goto clearup;
                }
                dtEquation.Rows.Add(sATmp);

                line++;
                sTmp = srConfig.ReadLine();
            }

            return 0;

        clearup:
            tbFilePath.Text = tbTemperature.Text = tbRxInputPower1.Text =
                tbRxInputPower2.Text = tbRxInputPower3.Text =
                tbRxInputPower4.Text = tbRxPowerRateMin.Text = 
                tbRxPowerRateMax.Text = tbAverageCurrentMax.Text = 
                tbAverageCurrentMin.Text = tbModulationCurrentMax.Text =
                tbModulationCurrentMin.Text = "";

            dtEquation.Clear();
            
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

        private int _EquationCellCheck()
        {
            string sTmp;
            float fTmp;
            int rIndex, cIndex, iTmp;

            foreach (DataRow row in dtEquation.Rows) {
                rIndex = dtEquation.Rows.IndexOf(row);

                sTmp = Convert.ToString(row.ItemArray[0]);
                if (sTmp.Length != 0) {
                    cIndex = 0;
                    try {
                        iTmp = Convert.ToInt32(sTmp);
                        if ((iTmp < 80) || (iTmp > 120)) {
                            MessageBox.Show((rIndex + 1) + " row's CEDP data:" + sTmp + " out of range (80 ~ 120)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, iTmp.ToString());
                    }
                    catch (Exception eTB) {
                        MessageBox.Show((rIndex + 1) + " row's CEDP data not valid\n" + eTB.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[1]);
                if (sTmp.Length != 0) {
                    cIndex = 1;
                    try {
                        fTmp = Convert.ToSingle(sTmp);
                        if ((fTmp < 0) || (fTmp > 2.55)) {
                            MessageBox.Show((rIndex + 1) + " rows' ACE's a:" + sTmp + " out of range (0 ~ 2.55)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, fTmp.ToString("N02"));
                    }
                    catch (Exception eTS) {
                        MessageBox.Show((rIndex + 1) + " row's AVE's a:" + sTmp + " data not valid\n" + eTS.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[2]);
                if (sTmp.Length != 0) {
                    cIndex = 2;
                    try {
                        fTmp = Convert.ToSingle(sTmp);
                        if ((fTmp < 0) || (fTmp > 6553.5)) {
                            MessageBox.Show((rIndex + 1) + " rows' ACE's b:" + sTmp + " out of range (0 ~ 6553.5)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, fTmp.ToString("N01"));
                    }
                    catch (Exception eTS) {
                        MessageBox.Show((rIndex + 1) + " row's AVE's b:" + sTmp + " data not valid\n" + eTS.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[3]);
                if (sTmp.Length != 0) {
                    cIndex = 3;
                    try {
                        iTmp = Convert.ToInt32(sTmp);
                        if ((iTmp < 0) || (iTmp > 65535)) {
                            MessageBox.Show((rIndex + 1) + " rows' ACE's c:" + sTmp + " out of range (0 ~ 65535)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, iTmp.ToString());
                    }
                    catch (Exception eTI) {
                        MessageBox.Show((rIndex + 1) + " row's AVE's c:" + sTmp + " data not valid\n" + eTI.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[4]);
                if (sTmp.Length != 0) {
                    cIndex = 4;
                    try {
                        fTmp = Convert.ToSingle(sTmp);
                        if ((fTmp < 0) || (fTmp > 2.55)) {
                            MessageBox.Show((rIndex + 1) + " rows' MCE's a:" + sTmp + " out of range (0 ~ 2.55)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, fTmp.ToString("N02"));
                    }
                    catch (Exception eTS) {
                        MessageBox.Show((rIndex + 1) + " row's MCE's a:" + sTmp + " data not valid\n" + eTS.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[5]);
                if (sTmp.Length != 0) {
                    cIndex = 5;
                    try {
                        fTmp = Convert.ToSingle(sTmp);
                        if ((fTmp < 0) || (fTmp > 6553.5)) {
                            MessageBox.Show((rIndex + 1) + " rows' MCE's b:" + sTmp + " out of range (0 ~ 6553.5)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, fTmp.ToString("N01"));
                    }
                    catch (Exception eTS) {
                        MessageBox.Show((rIndex + 1) + " row's MCE's b:" + sTmp + " data not valid\n" + eTS.ToString());
                        goto cleanup;
                    }
                }

                sTmp = Convert.ToString(row.ItemArray[6]);
                if (sTmp.Length != 0) {
                    cIndex = 6;
                    try {
                        iTmp = Convert.ToInt32(sTmp);
                        if ((iTmp < 0) || (iTmp > 65535)) {
                            MessageBox.Show((rIndex + 1) + " rows' MCE's c:" + sTmp + " out of range (0 ~ 65535)");
                            goto cleanup;
                        }
                        else
                            row.SetField<String>(cIndex, iTmp.ToString());
                    }
                    catch (Exception eTI) {
                        MessageBox.Show((rIndex + 1) + " row's MCE's c:" + sTmp + " data not valid\n" + eTI.ToString());
                        goto cleanup;
                    }
                }
            }

            foreach (DataRow row1 in dtEquation.Rows) {
                foreach (DataRow row2 in dtEquation.Rows) {
                    if (row1 == row2)
                        continue;
                    if (Convert.ToString(row1.ItemArray[0]) == Convert.ToString(row2.ItemArray[0])) {
                        MessageBox.Show(dtEquation.Rows.IndexOf(row1) +
                            " row's percentage:" +
                            Convert.ToString(row1.ItemArray[0]) + " equation " +
                            dtEquation.Rows.IndexOf(row2) + " row");
                        dtEquation.Rows.RemoveAt(dtEquation.Rows.IndexOf(row2));
                        return -1;
                    }
                }
            }

            return 0;

        cleanup:
            dtEquation.Rows[rIndex].SetField<String>(cIndex, "");
            return -1;
        }

        private void _dgvEquationCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_EquationCellCheck() < 0)
                return;
        }

        private int _SetAverageCurrentWithTemperature()
        {
            float aCa, aCb;
            
            byte[] data = new byte[1];
            int aCc, temperature, averageCurrent, tmp;

            if (qsfpConnected == false) {
                if (_QsfpConnect() < 0)
                    return -1;
            }
            else if (cbTemperatureCorrected.Checked == false) {
                if (_AutoCorrectTemperatureOffset() < 0)
                    return 0;
            }
            else {
                if (_ReadTemperature() < 0)
                    return -1;
            }

            try {
                temperature = Convert.ToInt32(tbTemperature.Text);
            }
            catch (Exception eTI) {
                MessageBox.Show("Temperature format error!!\n" + eTI.ToString());
                return 0;
            }

            if (cbMonitorLinked.Checked == false) {
                if (_MonitorConnect() < 0)
                    return -1;
            }

            averageCurrent = 0;
            foreach (DataRow row in dtEquation.Rows) {
                if (!Convert.ToString(row.ItemArray[0]).Equals("100"))
                    continue;

                aCa = Convert.ToSingle(row.ItemArray[1]);
                aCb = Convert.ToSingle(row.ItemArray[2]);
                aCc = Convert.ToInt32(row.ItemArray[3]);

                averageCurrent = Convert.ToInt32(aCa * temperature * temperature * 1000 + aCb * temperature * 1000 + aCc * temperature * 1000);
            }

            if (averageCurrent == 0) {
                MessageBox.Show("Cannot calculate txPower Error!!");
                return 0;
            }

            tmp = averageCurrent * 6 / 125;
            data[0] = Convert.ToByte(tmp / 10);
            if ((tmp % 10) >= 5)
                data[0]++;

            if (i2cMasterQsfp.WriteApi(107, 13, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _ReadTxPowerFromMonitor()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbMonitorPower1.Text = tbMonitorPower2.Text = tbMonitorPower3.Text =
                tbMonitorPower4.Text = "";

            if (monitorConnected == false) {
                if (_MonitorConnect() < 0)
                    return -1;
            }

            if (i2cMasterMonitor.ReadApi(80, 34, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbMonitorPower1.Text = power.ToString("#0.0");

            if (i2cMasterMonitor.ReadApi(80, 36, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbMonitorPower2.Text = power.ToString("#0.0");

            if (i2cMasterMonitor.ReadApi(80, 38, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbMonitorPower3.Text = power.ToString("#0.0");

            if (i2cMasterMonitor.ReadApi(80, 40, 2, data) != 2)
                goto DeviceNoResponse;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbMonitorPower4.Text = power.ToString("#0.0");

            return 0;

        DeviceNoResponse:
            MessageBox.Show("Monitor no reponse!!");
            tbMonitorPower1.Text = tbMonitorPower2.Text = tbMonitorPower3.Text =
                tbMonitorPower4.Text = "";
            return -1;
        }

        private int _CalculateCEDPercentage()
        {
            int tmp;

            tmp = Convert.ToInt32(Convert.ToSingle(tbMonitorPower1.Text) / Convert.ToSingle(tbTxPower.Text) * 100);
            if ((tmp < 80) || (tmp > 120)) {
                MessageBox.Show("Channel 1 coupling efficiency different percentage:" + tmp + " out of rane (80 ~ 120) Error!!");
                return -1;
            }
            tbCEDPercentage1.Text = tmp.ToString();

            tmp = Convert.ToInt32(Convert.ToSingle(tbMonitorPower2.Text) / Convert.ToSingle(tbTxPower.Text) * 100);
            if ((tmp < 80) || (tmp > 120)) {
                MessageBox.Show("Channel 2 coupling efficiency different percentage:" + tmp + " out of rane (80 ~ 120) Error!!");
                return -1;
            }
            tbCEDPercentage2.Text = tmp.ToString();

            tmp = Convert.ToInt32(Convert.ToSingle(tbMonitorPower3.Text) / Convert.ToSingle(tbTxPower.Text) * 100);
            if ((tmp < 80) || (tmp > 120)) {
                MessageBox.Show("Channel 3 coupling efficiency different percentage:" + tmp + " out of rane (80 ~ 120) Error!!");
                return -1;
            }
            tbCEDPercentage3.Text = tmp.ToString();

            tmp = Convert.ToInt32(Convert.ToSingle(tbMonitorPower4.Text) / Convert.ToSingle(tbTxPower.Text) * 100);
            if ((tmp < 80) || (tmp > 120)) {
                MessageBox.Show("Channel 4 coupling efficiency different percentage:" + tmp + " out of rane (80 ~ 120) Error!!");
                return -1;
            }
            tbCEDPercentage4.Text = tmp.ToString();

            return 0;
        }

        private int _WriteCEDPercentage()
        {
            byte[] data = new byte[44];
            byte[] baTmp;
            int result, index, i;
            UInt16 u16Tmp;
            byte bChecksum;

            data[0] = 2;
            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto device_no_response;

            result = index = 0;
            foreach (DataRow row in dtEquation.Rows) {
                if (!Convert.ToString(row.ItemArray[0]).Equals(tbCEDPercentage1.Text))
                    continue;

                data[index] = Convert.ToByte(Convert.ToSingle(row.ItemArray[1]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[2]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 1] = baTmp[1];
                data[index + 2] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[3]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 3] = baTmp[1];
                data[index + 4] = baTmp[0];

                data[index + 22] = Convert.ToByte(Convert.ToSingle(row.ItemArray[4]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[5]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 23] = baTmp[1];
                data[index + 24] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[6]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 25] = baTmp[1];
                data[index + 26] = baTmp[0];

                result |= 1;
            }

            if ((result & 1) == 0) {
                MessageBox.Show("Cannot find channel 1 equation Error!!");
                    return -1;
            }

            index += 5;
            foreach (DataRow row in dtEquation.Rows) {
                if (!Convert.ToString(row.ItemArray[0]).Equals(tbCEDPercentage2.Text))
                    continue;

                data[index] = Convert.ToByte(Convert.ToSingle(row.ItemArray[1]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[2]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 1] = baTmp[1];
                data[index + 2] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[3]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 3] = baTmp[1];
                data[index + 4] = baTmp[0];

                data[index + 22] = Convert.ToByte(Convert.ToSingle(row.ItemArray[4]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[5]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 23] = baTmp[1];
                data[index + 24] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[6]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 25] = baTmp[1];
                data[index + 26] = baTmp[0];

                result |= 2;
            }

            if ((result & 2) == 0) {
                MessageBox.Show("Cannot find channel 2 equation Error!!");
                return -1;
            }

            index += 5;
            foreach (DataRow row in dtEquation.Rows) {
                if (!Convert.ToString(row.ItemArray[0]).Equals(tbCEDPercentage3.Text))
                    continue;

                data[index] = Convert.ToByte(Convert.ToSingle(row.ItemArray[1]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[2]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 1] = baTmp[1];
                data[index + 2] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[3]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 3] = baTmp[1];
                data[index + 4] = baTmp[0];

                data[index + 22] = Convert.ToByte(Convert.ToSingle(row.ItemArray[4]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[5]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 23] = baTmp[1];
                data[index + 24] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[6]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 25] = baTmp[1];
                data[index + 26] = baTmp[0];

                result |= 4;
            }

            if ((result & 4) == 0) {
                MessageBox.Show("Cannot find channel 3 equation Error!!");
                return -1;
            }

            index += 5;
            foreach (DataRow row in dtEquation.Rows) {
                if (!Convert.ToString(row.ItemArray[0]).Equals(tbCEDPercentage4.Text))
                    continue;

                data[index] = Convert.ToByte(Convert.ToSingle(row.ItemArray[1]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[2]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 1] = baTmp[1];
                data[index + 2] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[3]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 3] = baTmp[1];
                data[index + 4] = baTmp[0];

                data[index + 22] = Convert.ToByte(Convert.ToSingle(row.ItemArray[4]) * 100);
                u16Tmp = Convert.ToUInt16(Convert.ToSingle(row.ItemArray[5]) * 10);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 23] = baTmp[1];
                data[index + 24] = baTmp[0];
                u16Tmp = Convert.ToUInt16(row.ItemArray[6]);
                baTmp = BitConverter.GetBytes(u16Tmp);
                data[index + 25] = baTmp[1];
                data[index + 26] = baTmp[0];

                result |= 8;
            }

            if ((result & 8) == 0) {
                MessageBox.Show("Cannot find channel 4 equation Error!!");
                return -1;
            }

            data[20] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMin.Text) / 0.04);
            data[21] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMax.Text) / 0.04);
            data[42] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMin.Text) / 0.04);
            data[43] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMax.Text) / 0.04);

            if (i2cMasterQsfp.WriteApi(80, 192, 44, data) < 0)
                goto device_no_response;

            for (i = 0, bChecksum = 0; i < 44; i++)
                bChecksum += data[i];

            data[0] = bChecksum;

            if (i2cMasterQsfp.WriteApi(80, 132, 1, data) < 0)
                goto device_no_response;

            return 0;

        device_no_response:
            MessageBox.Show("QSFP+ module no response Error!!");
            return -1;
        }

        private int _AutoCorrectAcMc()
        {
            if (_SetAverageCurrentWithTemperature() < 0)
                return -1;

            if (_ReadTxPowerFromMonitor() < 0)
                return -1;

            if (_CalculateCEDPercentage() < 0)
                return -1;

            cbAcMcCorrected.Checked = true;

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


            tbCEDPercentage1.Text = tbCEDPercentage2.Text = tbCEDPercentage3.Text =
                tbCEDPercentage4.Text = "";

            if (i2cMasterQsfp.WriteApi(80, 127, 1, data) < 0)
                goto device_no_response;

            data[0] = 0;
            if (i2cMasterQsfp.WriteApi(80, 192, 1, data) < 0)
                goto device_no_response;

            return 0;

        device_no_response:
            MessageBox.Show("QSFP+ module no response Error!!");
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
    }
}
