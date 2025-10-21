using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Gn1190Corrector
{
    public partial class UcGn1190Corrector : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int PowerMeterReadCB(String[] data);

        private BackgroundWorker bwMonitor;
        private I2cReadCB qsfpI2cReadCB = null;
        private I2cWriteCB qsfpI2cWriteCB = null;
        private PowerMeterReadCB powerMeterReadCB = null;
        private String sAcConfig;
        private bool bContinuousRead = false;
        private bool bTriggerRead = false;

        public UcGn1190Corrector()
        {
            InitializeComponent();
            //ConfigUiByXmlApi("");
            cbCustomerPage.SelectedIndex = 1;

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            while (cbContinuousRead.Checked == true) {
                Thread.Sleep(100);
                if (bTriggerRead == false) {
                    bTriggerRead = true;
                    bwMonitor.ReportProgress(1, null);
                }
            }

            bwMonitor.ReportProgress(100, null);
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1) {
                if (_ReadVoltage() < 0) {
                    MessageBox.Show("_ReadVoltage() Fail!!");
                    goto error;
                }

                if (_ReadTemperature() < 0) {
                    MessageBox.Show("_ReadTemperature() Fail!!");
                    goto error;
                }

                if (_ReadRxPowerRate() < 0) {
                    MessageBox.Show("_ReadRxPowerRate() Fail!!");
                    goto error;
                }

                if (_ReadTxPowerRate() < 0) {
                    MessageBox.Show("_ReadTxPowerRate() Fail!!");
                    goto error;
                }

                if (_ReadAverageCurrentAndModulationCurrentCorrectData() < 0) {
                    MessageBox.Show("_ReadAverageCurrentAndModulationCurrentCorrectData() Fail!!");
                    goto error;
                }

                if (cbContinuousRead.Text == "連續讀取")
                    cbContinuousRead.Text = "連續讀取 -";
                else if (cbContinuousRead.Text == "連續讀取 -")
                    cbContinuousRead.Text = "連續讀取 \\";
                else if (cbContinuousRead.Text == "連續讀取 \\")
                    cbContinuousRead.Text = "連續讀取 /";
                else if (cbContinuousRead.Text == "連續讀取 /")
                    cbContinuousRead.Text = "連續讀取 -";

                bTriggerRead = false;

                if (cbContinuousRead.Checked == false)
                    goto error;

                return;
            }

            return;

        error:
            cbContinuousRead.Text = "連續讀取";
            bContinuousRead = false;
            cbContinuousRead.Checked = false;
        }

        private void _PaserUiConfigXml(XmlReader reader)
        {
            while (true) {
                if (reader.IsStartElement()) {
                    String attObject = reader.GetAttribute("Object");
                    String attVisible = reader.GetAttribute("Visible");
                    String attEnbaled = reader.GetAttribute("Enabled");
                    String attReadOnly = reader.GetAttribute("ReadOnly");

                    switch (attObject) {
                        case "TextBox":
                            TextBox tbTmp = (TextBox)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    tbTmp.Visible = true;
                                else
                                    tbTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    tbTmp.Enabled = true;
                                else
                                    tbTmp.Enabled = false;
                            }
                            if (attReadOnly != null) {
                                if (attReadOnly.Equals("True"))
                                    tbTmp.ReadOnly = true;
                                else
                                    tbTmp.ReadOnly = false;
                            }
                            reader.Read();
                            break;

                        case "Label":
                            Label lTmp = (Label)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    lTmp.Visible = true;
                                else
                                    lTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    lTmp.Enabled = true;
                                else
                                    lTmp.Enabled = false;
                            }
                            reader.Read();
                            break;

                        case "Button":
                            Button bTmp = (Button)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    bTmp.Visible = true;
                                else
                                    bTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    bTmp.Enabled = true;
                                else
                                    bTmp.Enabled = false;
                            }
                            reader.Read();
                            break;

                        case "GroupBox":
                            GroupBox gbTmp = (GroupBox)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    gbTmp.Visible = true;
                                else
                                    gbTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    gbTmp.Enabled = true;
                                else
                                    gbTmp.Enabled = false;
                            }
                            reader.Read();
                            break;

                        case "CheckBox":
                            CheckBox cbTmp = (CheckBox)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    cbTmp.Visible = true;
                                else
                                    cbTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    cbTmp.Enabled = true;
                                else
                                    cbTmp.Enabled = false;
                            }
                            reader.Read();
                            break;

                        case "ComboBox":
                            ComboBox cobTmp = (ComboBox)this.Controls.Find(reader.Name, true).FirstOrDefault();
                            if (attVisible != null) {
                                if (attVisible.Equals("True"))
                                    cobTmp.Visible = true;
                                else
                                    cobTmp.Visible = false;
                            }
                            if (attEnbaled != null) {
                                if (attEnbaled.Equals("True"))
                                    cobTmp.Enabled = true;
                                else
                                    cobTmp.Enabled = false;
                            }
                            reader.Read();
                            break;

                        default:
                            reader.Read();
                            break;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        public int ConfigUiByXmlApi(String configXml)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();
            XmlReader xrConfig;

            if (configXml.Length == 0) {
                ofdSelectFile.Title = "Select config file";
                ofdSelectFile.Filter = "xml files (*.xml)|*.xml";
                if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                    return -1;
                xrConfig = XmlReader.Create(ofdSelectFile.FileName);
            }
            else {
                xrConfig = XmlReader.Create(configXml);
            }

            
            while (xrConfig.Read()) {
                if (xrConfig.IsStartElement()) {
                    switch (xrConfig.Name) {
                        case "UiConfig":
                            xrConfig.Read();
                            _PaserUiConfigXml(xrConfig);
                            break;

                        default:
                            break;
                    }
                }
            }
            

            return 0;
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

        public int SetPowerMeterReadCBApi(PowerMeterReadCB cb)
        {
            if (cb == null)
                return -1;

            powerMeterReadCB = new PowerMeterReadCB(cb);

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
            tbRxPowerRate2.Text = sATmp[3];
            tbRxPowerRate3.Text = sATmp[4];
            tbRxPowerRate4.Text = sATmp[5];

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
                tbRxPowerRateMin.Text + "," + tbRxPowerRate1.Text + "," +
                tbRxPowerRate2.Text + "," + tbRxPowerRate3.Text + "," +
                tbRxPowerRate4.Text);

            swConfig.Close();
        }

        private int _ReadLutTemperature()
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;

            bLutTemperatureUpdate.Enabled = false;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            Thread.Sleep(1);

            if (qsfpI2cReadCB(80, 175, 2, data) != 2)
                goto exit;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbLutTemperature.Text = tmp.ToString("#0");
            return 0;
        exit:
            bLutTemperatureUpdate.Enabled = true;
            return -1;
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

            if (qsfpI2cReadCB(80, 22, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTxTemperature.Text = temperature.ToString("#0.0");
            tbTxTemperature.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 241, 1, data) != 1) {
                if (bContinuousRead == false)
                    return -1;
            }

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
            tbTemperatureOffset.Update();

            if (qsfpI2cReadCB(80, 242, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            tmp = BitConverter.ToInt16(data, 0);
            tbTemperatureSlope.Text = tmp.ToString();
            tbTemperatureSlope.Update();

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

            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 123, 4, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
                    return -1;
            }

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[1];

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                    return -1;
            }

            data[0] = mode;
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 164, 1, data, ref sAcConfig);
            else
            {
                if (qsfpI2cWriteCB(80, 164, 1, data) < 0)
                    return -1;
            }

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

            if (qsfpI2cReadCB(80, 26, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            voltage = tmp;
            voltage = voltage / 10000;
            tbTxVoltage.Text = voltage.ToString("#0.0000");
            tbTxVoltage.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 240, 1, data) != 1) {
                if (bContinuousRead == false)
                    return -1;
            }

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
            tbVoltageOffset.Update();

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

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else
                qsfpI2cWriteCB(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }
            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 240, 1, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x1\n";
                _I2cReadToString(80, 240, 1, data, ref sAcConfig);
            }
            else
                qsfpI2cWriteCB(80, 240, 1, data);

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

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else
                qsfpI2cWriteCB(80, 127, 1, data);

            try {
                Buffer.BlockCopy(tmp, 0, data, 0, 1);
            }
            catch (Exception e2) {
                MessageBox.Show(e2.ToString());
                return -1;
            }

            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 241, 1, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x1\n";
                _I2cReadToString(80, 241, 1, data, ref sAcConfig);
            }
            else
                qsfpI2cWriteCB(80, 241, 1, data);

            bATmp = BitConverter.GetBytes(tmpS16);
            data[0] = bATmp[0];
            data[1] = bATmp[1];

            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 242, 2, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x1\n";
                _I2cReadToString(80, 242, 2, data, ref sAcConfig);
            }
            else
                qsfpI2cWriteCB(80, 242, 2, data);

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

            if (qsfpI2cReadCB(80, 34, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower1.Text = power.ToString("#0.0");
            tbRxPower1.Update();

            if (qsfpI2cReadCB(80, 36, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower2.Text = power.ToString("#0.0");
            tbRxPower2.Update();

            if (qsfpI2cReadCB(80, 38, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower3.Text = power.ToString("#0.0");
            tbRxPower3.Update();

            if (qsfpI2cReadCB(80, 40, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbRxPower4.Text = power.ToString("#0.0");
            tbRxPower4.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;
            
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 128, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi1.Text = tmp.ToString();
            tbRssi1.Update();

            if (qsfpI2cReadCB(80, 130, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi2.Text = tmp.ToString();
            tbRssi2.Update();

            if (qsfpI2cReadCB(80, 132, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi3.Text = tmp.ToString();
            tbRssi3.Update();

            if (qsfpI2cReadCB(80, 134, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRssi4.Text = tmp.ToString();
            tbRssi4.Update();

            if (qsfpI2cReadCB(80, 163, 1, data) != 1) {
                if (bContinuousRead == false)
                    return -1;
            }

            tbRxPowerRateDefault.Text = data[0].ToString();
            tbRxPowerRateDefault.Update();

            if (tbRxPowerRateMax.Text.Length == 0) {
                tbRxPowerRateMax.Text = (data[0] + 15).ToString();
                tbRxPowerRateMax.Update();
            }

            if (tbRxPowerRateMin.Text.Length == 0) {
                tbRxPowerRateMin.Text = (data[0] - 12).ToString();
                tbRxPowerRateMin.Update();
            }

            data = new byte[4];
            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 244, 4, data) != 4) {
                if (bContinuousRead == false)
                    return -1;
            }

            tbRxPowerRate1.Text = data[0].ToString();
            tbRxPowerRate1.Update();
            tbRxPowerRate2.Text = data[1].ToString();
            tbRxPowerRate2.Update();
            tbRxPowerRate3.Text = data[2].ToString();
            tbRxPowerRate3.Update();
            tbRxPowerRate4.Text = data[3].ToString();
            tbRxPowerRate4.Update();

            return 0;
        }

        private void bRxPowerRateRead_Click(object sender, EventArgs e)
        {
            if (_ReadRxPowerRate() < 0)
                return;
        }

        private int _WriteRxPowerRate()
        {
            byte[] data = new byte[4];

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

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

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

            return 0;
        }

        private void bRxPowerRateWrite_Click(object sender, EventArgs e)
        {
            if (_WriteRxPowerRate() < 0)
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

            if (_WriteRxPowerRate() < 0)
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

            if (_WriteRxPowerRate() < 0)
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
            byte[] data = new byte[125];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            Single sTmp;
            int iTmp;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;
            
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 128, 44, data) != 44) {
                if (bContinuousRead == false)
                    return -1;
            }

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[0]) * 0.04);
            tbAverageCurrentMin.Text = sTmp.ToString("#0.00");
            tbAverageCurrentMin.Update();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[1]) * 0.1);
            tbAverageCurrentMax.Text = sTmp.ToString("#0.00");
            tbAverageCurrentMax.Update();

            Buffer.BlockCopy(data, 2, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationACh1.Text = sTmp.ToString("#0.00");
            tbAverageCurrentEquationACh1.Update();

            Array.Copy(data, 3, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationBCh1.Text = sTmp.ToString("#0.0");
            tbAverageCurrentEquationBCh1.Update();

            Array.Copy(data, 5, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationCCh1.Text = iTmp.ToString();
            tbAverageCurrentEquationCCh1.Update();

            Buffer.BlockCopy(data, 7, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationACh2.Text = sTmp.ToString("#0.00");
            tbAverageCurrentEquationACh2.Update();

            Array.Copy(data, 8, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationBCh2.Text = sTmp.ToString("#0.0");
            tbAverageCurrentEquationBCh2.Update();

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationCCh2.Text = iTmp.ToString();
            tbAverageCurrentEquationCCh2.Update();

            Buffer.BlockCopy(data, 12, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationACh3.Text = sTmp.ToString("#0.00");
            tbAverageCurrentEquationACh3.Update();

            Array.Copy(data, 13, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationBCh3.Text = sTmp.ToString("#0.0");
            tbAverageCurrentEquationBCh3.Update();

            Array.Copy(data, 15, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationCCh3.Text = iTmp.ToString();
            tbAverageCurrentEquationCCh3.Update();

            Buffer.BlockCopy(data, 17, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbAverageCurrentEquationACh4.Text = sTmp.ToString("#0.00");
            tbAverageCurrentEquationACh4.Update();

            Array.Copy(data, 18, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbAverageCurrentEquationBCh4.Text = sTmp.ToString("#0.0");
            tbAverageCurrentEquationBCh4.Update();

            Array.Copy(data, 20, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToUInt16(reverseData, 0);
            tbAverageCurrentEquationCCh4.Text = iTmp.ToString();
            tbAverageCurrentEquationCCh4.Update();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[22]) * 0.04);
            tbModulationCurrentMin.Text = sTmp.ToString("#0.00");
            tbModulationCurrentMin.Update();

            sTmp = Convert.ToSingle(Convert.ToUInt32(data[23]) * 0.04);
            tbModulationCurrentMax.Text = sTmp.ToString("#0.00");
            tbModulationCurrentMax.Update();

            Buffer.BlockCopy(data, 24, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationACh1.Text = sTmp.ToString("#0.00");
            tbModulationCurrentEquationACh1.Update();

            Array.Copy(data, 25, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationBCh1.Text = sTmp.ToString("#0.0");
            tbModulationCurrentEquationBCh1.Update();

            Array.Copy(data, 27, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationCCh1.Text = iTmp.ToString();
            tbModulationCurrentEquationCCh1.Update();

            Buffer.BlockCopy(data, 29, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationACh2.Text = sTmp.ToString("#0.00");
            tbModulationCurrentEquationACh2.Update();

            Array.Copy(data, 30, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationBCh2.Text = sTmp.ToString("#0.0");
            tbModulationCurrentEquationBCh2.Update();

            Array.Copy(data, 32, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationCCh2.Text = iTmp.ToString();
            tbModulationCurrentEquationCCh2.Update();

            Buffer.BlockCopy(data, 34, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationACh3.Text = sTmp.ToString("#0.00");
            tbModulationCurrentEquationACh3.Update();

            Array.Copy(data, 35, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationBCh3.Text = sTmp.ToString("#0.0");
            tbModulationCurrentEquationBCh3.Update();

            Array.Copy(data, 37, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationCCh3.Text = iTmp.ToString();
            tbModulationCurrentEquationCCh3.Update();

            Buffer.BlockCopy(data, 39, sData, 0, 1);
            sTmp = Convert.ToSingle(sData[0]) / 100;
            tbModulationCurrentEquationACh4.Text = sTmp.ToString("#0.00");
            tbModulationCurrentEquationACh4.Update();

            Array.Copy(data, 40, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10;
            tbModulationCurrentEquationBCh4.Text = sTmp.ToString("#0.0");
            tbModulationCurrentEquationBCh4.Update();

            Array.Copy(data, 42, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            tbModulationCurrentEquationCCh4.Text = iTmp.ToString();
            tbModulationCurrentEquationCCh4.Update();

            if (qsfpI2cReadCB(80, 252, 1, data) != 1) {
                if (bContinuousRead == false)
                    return -1;
            }

            if ((data[0] & 0x01) != 0)
                cbTemperatureCompensation.Checked = true;
            else
                cbTemperatureCompensation.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbBurnIn.Checked = true;
            else
                cbBurnIn.Checked = false;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0) {
                if (bContinuousRead == false)
                    return -1;
            }

            Thread.Sleep(1);

            if (qsfpI2cReadCB(80, 128, 96, data) != 96) {
                if (bContinuousRead == false)
                    return -1;
            }

            Array.Copy(data, 0, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompPropEquationACh1.Text = sTmp.ToString("#0.00000");
            tbVhfCompPropEquationACh1.Update();

            Array.Copy(data, 2, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationBCh1.Text = sTmp.ToString("#0.0000");
            tbVhfCompPropEquationBCh1.Update();

            Array.Copy(data, 4, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompPropEquationCCh1.Text = sTmp.ToString("#0.00");
            tbVhfCompPropEquationCCh1.Update();

            Array.Copy(data, 6, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompPropEquationACh2.Text = sTmp.ToString("#0.00000");
            tbVhfCompPropEquationACh2.Update();

            Array.Copy(data, 8, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationBCh2.Text = sTmp.ToString("#0.0000");
            tbVhfCompPropEquationBCh2.Update();

            Array.Copy(data, 10, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompPropEquationCCh2.Text = sTmp.ToString("#0.00");
            tbVhfCompPropEquationCCh2.Update();

            Array.Copy(data, 12, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompPropEquationACh3.Text = sTmp.ToString("#0.00000");
            tbVhfCompPropEquationACh3.Update();

            Array.Copy(data, 14, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationBCh3.Text = sTmp.ToString("#0.0000");
            tbVhfCompPropEquationBCh3.Update();

            Array.Copy(data, 16, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompPropEquationCCh3.Text = sTmp.ToString("#0.00");
            tbVhfCompPropEquationCCh3.Update();

            Array.Copy(data, 18, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompPropEquationACh4.Text = sTmp.ToString("#0.00000");
            tbVhfCompPropEquationACh4.Update();

            Array.Copy(data, 20, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompPropEquationBCh4.Text = sTmp.ToString("#0.0000");
            tbVhfCompPropEquationBCh4.Update();

            Array.Copy(data, 22, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompPropEquationCCh4.Text = sTmp.ToString("#0.00");
            tbVhfCompPropEquationCCh4.Update();

            Array.Copy(data, 24, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompConstEquationACh1.Text = sTmp.ToString("#0.00000");
            tbVhfCompConstEquationACh1.Update();

            Array.Copy(data, 26, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationBCh1.Text = sTmp.ToString("#0.0000");
            tbVhfCompConstEquationBCh1.Update();

            Array.Copy(data, 28, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationCCh1.Text = sTmp.ToString("#0.00");
            tbVhfCompConstEquationCCh1.Update();

            Array.Copy(data, 30, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompConstEquationACh2.Text = sTmp.ToString("#0.00000");
            tbVhfCompConstEquationACh2.Update();

            Array.Copy(data, 32, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationBCh2.Text = sTmp.ToString("#0.0000");
            tbVhfCompConstEquationBCh2.Update();

            Array.Copy(data, 34, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationCCh2.Text = sTmp.ToString("#0.00");
            tbVhfCompConstEquationCCh2.Update();

            Array.Copy(data, 36, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompConstEquationACh3.Text = sTmp.ToString("#0.00000");
            tbVhfCompConstEquationACh3.Update();

            Array.Copy(data, 38, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationBCh3.Text = sTmp.ToString("#0.0000");
            tbVhfCompConstEquationBCh3.Update();

            Array.Copy(data, 40, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationCCh3.Text = sTmp.ToString("#0.00");
            tbVhfCompConstEquationCCh3.Update();

            Array.Copy(data, 42, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbVhfCompConstEquationACh4.Text = sTmp.ToString("#0.00000");
            tbVhfCompConstEquationACh4.Update();

            Array.Copy(data, 44, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbVhfCompConstEquationBCh4.Text = sTmp.ToString("#0.0000");
            tbVhfCompConstEquationBCh4.Update();

            Array.Copy(data, 46, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbVhfCompConstEquationCCh4.Text = sTmp.ToString("#0.00");
            tbVhfCompConstEquationCCh4.Update();

            Array.Copy(data, 48, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakEnEquationACh1.Text = sTmp.ToString("#0.00000");
            tbPeakEnEquationACh1.Update();

            Array.Copy(data, 50, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakEnEquationBCh1.Text = sTmp.ToString("#0.0000");
            tbPeakEnEquationBCh1.Update();

            Array.Copy(data, 52, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakEnEquationCCh1.Text = sTmp.ToString("#0.00");
            tbPeakEnEquationCCh1.Update();

            Array.Copy(data, 54, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakEnEquationACh2.Text = sTmp.ToString("#0.00000");
            tbPeakEnEquationACh2.Update();

            Array.Copy(data, 56, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakEnEquationBCh2.Text = sTmp.ToString("#0.0000");
            tbPeakEnEquationBCh2.Update();

            Array.Copy(data, 58, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakEnEquationCCh2.Text = sTmp.ToString("#0.00");
            tbPeakEnEquationCCh2.Update();

            Array.Copy(data, 60, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakEnEquationACh3.Text = sTmp.ToString("#0.00000");
            tbPeakEnEquationACh3.Update();

            Array.Copy(data, 62, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakEnEquationBCh3.Text = sTmp.ToString("#0.0000");
            tbPeakEnEquationBCh3.Update();

            Array.Copy(data, 64, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakEnEquationCCh3.Text = sTmp.ToString("#0.00");
            tbPeakEnEquationCCh3.Update();

            Array.Copy(data, 66, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakEnEquationACh4.Text = sTmp.ToString("#0.00000");
            tbPeakEnEquationACh4.Update();

            Array.Copy(data, 68, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakEnEquationBCh4.Text = sTmp.ToString("#0.0000");
            tbPeakEnEquationBCh4.Update();

            Array.Copy(data, 70, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakEnEquationCCh4.Text = sTmp.ToString("#0.00");
            tbPeakEnEquationCCh4.Update();

            Array.Copy(data, 72, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakLenCtrlEquationACh1.Text = sTmp.ToString("#0.00000");
            tbPeakLenCtrlEquationACh1.Update();

            Array.Copy(data, 74, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakLenCtrlEquationBCh1.Text = sTmp.ToString("#0.0000");
            tbPeakLenCtrlEquationBCh1.Update();

            Array.Copy(data, 76, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakLenCtrlEquationCCh1.Text = sTmp.ToString("#0.00");
            tbPeakLenCtrlEquationCCh1.Update();

            Array.Copy(data, 78, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakLenCtrlEquationACh2.Text = sTmp.ToString("#0.00000");
            tbPeakLenCtrlEquationACh2.Update();

            Array.Copy(data, 80, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakLenCtrlEquationBCh2.Text = sTmp.ToString("#0.0000");
            tbPeakLenCtrlEquationBCh2.Update();

            Array.Copy(data, 82, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakLenCtrlEquationCCh2.Text = sTmp.ToString("#0.00");
            tbPeakLenCtrlEquationCCh2.Update();

            Array.Copy(data, 84, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakLenCtrlEquationACh3.Text = sTmp.ToString("#0.00000");
            tbPeakLenCtrlEquationACh3.Update();

            Array.Copy(data, 86, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakLenCtrlEquationBCh3.Text = sTmp.ToString("#0.0000");
            tbPeakLenCtrlEquationBCh3.Update();

            Array.Copy(data, 88, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakLenCtrlEquationCCh3.Text = sTmp.ToString("#0.00");
            tbPeakLenCtrlEquationCCh3.Update();

            Array.Copy(data, 90, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100000;
            tbPeakLenCtrlEquationACh4.Text = sTmp.ToString("#0.00000");
            tbPeakLenCtrlEquationACh4.Update();

            Array.Copy(data, 92, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 10000;
            tbPeakLenCtrlEquationBCh4.Text = sTmp.ToString("#0.0000");
            tbPeakLenCtrlEquationBCh4.Update();

            Array.Copy(data, 94, bATmp, 0, 2);
            reverseData = bATmp.Reverse().ToArray();
            sTmp = Convert.ToSingle(BitConverter.ToInt16(reverseData, 0)) / 100;
            tbPeakLenCtrlEquationCCh4.Text = sTmp.ToString("#0.00");
            tbPeakLenCtrlEquationCCh4.Update();

            return 0;
        }

        private void bAcMcRead_Click(object sender, EventArgs e)
        {
            bAcMcRead.Enabled = false;
            _ReadAverageCurrentAndModulationCurrentCorrectData();
            bAcMcRead.Enabled = true;
        }

        private int _I2cWriteToString(byte devAddr, byte regAddr, byte length, byte[] data, ref String sTmp)
        {
            int i;

            if (length == 1)
                sTmp += "Write,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" +
                    data[0].ToString("X2") + "\n";
            else {
                sTmp += "WriteMulti,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" + length.ToString("X2");
                for (i = 0; i < length; i++) {
                    sTmp += ",0x" + data[i].ToString("X2");
                }
                sTmp += "\n";
            }

            return 0;
        }

        private int _I2cReadToString(byte devAddr, byte regAddr, byte length, byte[] data, ref String sTmp)
        {
            int i;

            if (length == 1) {
                sTmp += "Read,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" +
                    data[0].ToString("X2") + "\n";
            }
            else {
                sTmp += "ReadMulti,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" +
                    length.ToString("X2");
                for (i = 0; i < length; i++)
                    sTmp += ",0x" + data[i].ToString("X2");
                sTmp += "\n";
            }

            return 0;
        }

        private int _WriteAcMcCorrectData()
        {

            byte[] data = new byte[125];
            byte[] bATmp;
            UInt16 u16Tmp;
            Int16 s16Tmp;
            sbyte[] sBATmp = new sbyte[1];

            if ((tbAverageCurrentMin.Text.Length == 0) ||
                (tbAverageCurrentMax.Text.Length == 0) ||
                (tbAverageCurrentEquationACh1.Text.Length == 0) ||
                (tbAverageCurrentEquationBCh1.Text.Length == 0) ||
                (tbAverageCurrentEquationCCh1.Text.Length == 0) ||
                (tbAverageCurrentEquationACh2.Text.Length == 0) ||
                (tbAverageCurrentEquationBCh2.Text.Length == 0) ||
                (tbAverageCurrentEquationCCh2.Text.Length == 0) ||
                (tbAverageCurrentEquationACh3.Text.Length == 0) ||
                (tbAverageCurrentEquationBCh3.Text.Length == 0) ||
                (tbAverageCurrentEquationCCh3.Text.Length == 0) ||
                (tbAverageCurrentEquationACh4.Text.Length == 0) ||
                (tbAverageCurrentEquationBCh4.Text.Length == 0) ||
                (tbAverageCurrentEquationCCh4.Text.Length == 0) ||
                (tbModulationCurrentMin.Text.Length == 0) ||
                (tbModulationCurrentMax.Text.Length == 0) ||
                (tbModulationCurrentEquationACh1.Text.Length == 0) ||
                (tbModulationCurrentEquationBCh1.Text.Length == 0) ||
                (tbModulationCurrentEquationCCh1.Text.Length == 0) ||
                (tbModulationCurrentEquationACh2.Text.Length == 0) ||
                (tbModulationCurrentEquationBCh2.Text.Length == 0) ||
                (tbModulationCurrentEquationCCh2.Text.Length == 0) ||
                (tbModulationCurrentEquationACh3.Text.Length == 0) ||
                (tbModulationCurrentEquationBCh3.Text.Length == 0) ||
                (tbModulationCurrentEquationCCh3.Text.Length == 0) ||
                (tbModulationCurrentEquationACh4.Text.Length == 0) ||
                (tbModulationCurrentEquationBCh4.Text.Length == 0) ||
                (tbModulationCurrentEquationCCh4.Text.Length == 0) ||
                (tbVhfCompPropEquationACh1.Text.Length == 0) ||
                (tbVhfCompPropEquationBCh1.Text.Length == 0) ||
                (tbVhfCompPropEquationCCh1.Text.Length == 0) ||
                (tbVhfCompPropEquationACh2.Text.Length == 0) ||
                (tbVhfCompPropEquationBCh2.Text.Length == 0) ||
                (tbVhfCompPropEquationCCh2.Text.Length == 0) ||
                (tbVhfCompPropEquationACh3.Text.Length == 0) ||
                (tbVhfCompPropEquationBCh3.Text.Length == 0) ||
                (tbVhfCompPropEquationCCh3.Text.Length == 0) ||
                (tbVhfCompPropEquationACh4.Text.Length == 0) ||
                (tbVhfCompPropEquationBCh4.Text.Length == 0) ||
                (tbVhfCompPropEquationCCh4.Text.Length == 0) ||
                (tbVhfCompConstEquationACh1.Text.Length == 0) ||
                (tbVhfCompConstEquationBCh1.Text.Length == 0) ||
                (tbVhfCompConstEquationCCh1.Text.Length == 0) ||
                (tbVhfCompConstEquationACh2.Text.Length == 0) ||
                (tbVhfCompConstEquationBCh2.Text.Length == 0) ||
                (tbVhfCompConstEquationCCh2.Text.Length == 0) ||
                (tbVhfCompConstEquationACh3.Text.Length == 0) ||
                (tbVhfCompConstEquationBCh3.Text.Length == 0) ||
                (tbVhfCompConstEquationCCh3.Text.Length == 0) ||
                (tbVhfCompConstEquationACh4.Text.Length == 0) ||
                (tbVhfCompConstEquationBCh4.Text.Length == 0) ||
                (tbVhfCompConstEquationCCh4.Text.Length == 0) ||
                (tbPeakEnEquationACh1.Text.Length == 0) ||
                (tbPeakEnEquationBCh1.Text.Length == 0) ||
                (tbPeakEnEquationCCh1.Text.Length == 0) ||
                (tbPeakEnEquationACh2.Text.Length == 0) ||
                (tbPeakEnEquationBCh2.Text.Length == 0) ||
                (tbPeakEnEquationCCh2.Text.Length == 0) ||
                (tbPeakEnEquationACh3.Text.Length == 0) ||
                (tbPeakEnEquationBCh3.Text.Length == 0) ||
                (tbPeakEnEquationCCh3.Text.Length == 0) ||
                (tbPeakEnEquationACh4.Text.Length == 0) ||
                (tbPeakEnEquationBCh4.Text.Length == 0) ||
                (tbPeakEnEquationCCh4.Text.Length == 0) ||
                (tbPeakLenCtrlEquationACh1.Text.Length == 0) ||
                (tbPeakLenCtrlEquationBCh1.Text.Length == 0) ||
                (tbPeakLenCtrlEquationCCh1.Text.Length == 0) ||
                (tbPeakLenCtrlEquationACh2.Text.Length == 0) ||
                (tbPeakLenCtrlEquationBCh2.Text.Length == 0) ||
                (tbPeakLenCtrlEquationCCh2.Text.Length == 0) ||
                (tbPeakLenCtrlEquationACh3.Text.Length == 0) ||
                (tbPeakLenCtrlEquationBCh3.Text.Length == 0) ||
                (tbPeakLenCtrlEquationCCh3.Text.Length == 0) ||
                (tbPeakLenCtrlEquationACh4.Text.Length == 0) ||
                (tbPeakLenCtrlEquationBCh4.Text.Length == 0) ||
                (tbPeakLenCtrlEquationCCh4.Text.Length == 0)) {
                MessageBox.Show("Please input value before write!!");
                return -1;
            }

            if (bStoreAcConfigToFile.Enabled == true) {
                if (_WritePassword() < 0)
                    return -1;

                if (_SetQsfpMode(0x4D) < 0)
                    return -1;
            }

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                    return -1;
            }

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
                Convert.ToSingle(tbAverageCurrentMax.Text) > 25.6) {
                MessageBox.Show("Average current max: " +
                    tbAverageCurrentMax.Text +
                    " out of range (0 ~ 25.6)!!");
            }
            try {
                data[1] = Convert.ToByte(Convert.ToSingle(tbAverageCurrentMax.Text) / 0.1);
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

            if ((Convert.ToSingle(tbAverageCurrentEquationACh2.Text) < -1.28) ||
                (Convert.ToSingle(tbAverageCurrentEquationACh2.Text) > 1.27)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationACh2.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[7] = (byte)Convert.ToSByte(Convert.ToSingle(tbAverageCurrentEquationACh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationBCh2.Text) < -3276.8) ||
                Convert.ToSingle(tbAverageCurrentEquationBCh2.Text) > 3276.7) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationBCh2.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbAverageCurrentEquationBCh2.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[8] = bATmp[1];
            data[9] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationCCh2.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[10] = bATmp[1];
            data[11] = bATmp[0];

            if ((Convert.ToSingle(tbAverageCurrentEquationACh3.Text) < -1.28) ||
                (Convert.ToSingle(tbAverageCurrentEquationACh3.Text) > 1.27)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationACh3.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[12] = (byte)Convert.ToSByte(Convert.ToSingle(tbAverageCurrentEquationACh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationBCh3.Text) < -3276.8) ||
                Convert.ToSingle(tbAverageCurrentEquationBCh3.Text) > 3276.7) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationBCh3.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbAverageCurrentEquationBCh3.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[13] = bATmp[1];
            data[14] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationCCh3.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[15] = bATmp[1];
            data[16] = bATmp[0];

            if ((Convert.ToSingle(tbAverageCurrentEquationACh4.Text) < -1.28) ||
                (Convert.ToSingle(tbAverageCurrentEquationACh4.Text) > 1.27)) {
                MessageBox.Show("Average current equation A: " +
                    tbAverageCurrentEquationACh4.Text +
                    " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[17] = (byte)Convert.ToSByte(Convert.ToSingle(tbAverageCurrentEquationACh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }

            if ((Convert.ToSingle(tbAverageCurrentEquationBCh4.Text) < -3276.8) ||
                Convert.ToSingle(tbAverageCurrentEquationBCh4.Text) > 3276.7) {
                MessageBox.Show("Average current equation B: " +
                    tbAverageCurrentEquationBCh4.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbAverageCurrentEquationBCh4.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[18] = bATmp[1];
            data[19] = bATmp[0];

            try {
                u16Tmp = Convert.ToUInt16(tbAverageCurrentEquationCCh4.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[20] = bATmp[1];
            data[21] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentMin.Text) < 0) ||
                Convert.ToSingle(tbModulationCurrentMin.Text) > 10.2) {
                MessageBox.Show("Modulation current min: " +
                    tbModulationCurrentMin.Text +
                    " out of range (0 ~ 10.2)!!");
            }
            try {
                data[22] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMin.Text) / 0.04);
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
                data[23] = Convert.ToByte(Convert.ToSingle(tbModulationCurrentMax.Text) / 0.04);
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
                data[24] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationACh1.Text) * 100);
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
            data[25] = bATmp[1];
            data[26] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationCCh1.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[27] = bATmp[1];
            data[28] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentEquationACh2.Text) < -1.28) ||
                (Convert.ToSingle(tbModulationCurrentEquationACh2.Text) > 1.27)) {
                MessageBox.Show("Modualtion current equation A: " +
                tbModulationCurrentEquationACh2.Text +
                " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[29] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationACh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbModulationCurrentEquationBCh2.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationBCh2.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationACh2.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationBCh2.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[30] = bATmp[1];
            data[31] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationCCh2.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[32] = bATmp[1];
            data[33] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentEquationACh3.Text) < -1.28) ||
                (Convert.ToSingle(tbModulationCurrentEquationACh3.Text) > 1.27)) {
                MessageBox.Show("Modualtion current equation A: " +
                tbModulationCurrentEquationACh3.Text +
                " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[34] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationACh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbModulationCurrentEquationBCh3.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationBCh3.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationACh3.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationBCh3.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[35] = bATmp[1];
            data[36] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationCCh3.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[37] = bATmp[1];
            data[38] = bATmp[0];

            if ((Convert.ToSingle(tbModulationCurrentEquationACh4.Text) < -1.28) ||
                (Convert.ToSingle(tbModulationCurrentEquationACh4.Text) > 1.27)) {
                MessageBox.Show("Modualtion current equation A: " +
                tbModulationCurrentEquationACh4.Text +
                " out of range (-1.28 ~ 1.27)!!");
                return -1;
            }
            try {
                data[39] = (byte)Convert.ToSByte(Convert.ToSingle(tbModulationCurrentEquationACh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            if (Convert.ToSingle(tbModulationCurrentEquationBCh4.Text) < -3276.8 ||
                Convert.ToSingle(tbModulationCurrentEquationBCh4.Text) > 3276.7) {
                MessageBox.Show("Modualtion current equation B: " +
                    tbModulationCurrentEquationACh4.Text +
                    " out of range (-3276.8 ~ 3276.7)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbModulationCurrentEquationBCh4.Text) * 10);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[40] = bATmp[1];
            data[41] = bATmp[0];
            try {
                u16Tmp = Convert.ToUInt16(tbModulationCurrentEquationCCh4.Text);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(u16Tmp);
            data[42] = bATmp[1];
            data[43] = bATmp[0];

            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 128, 44, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x14\n";
                _I2cReadToString(80, 128, 44, data, ref sAcConfig);
            }
            else
            {
                if (qsfpI2cWriteCB(80, 128, 44, data) < 0)
                    return -1;
            }

            Thread.Sleep(1);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                    return -1;
            }

            if ((Convert.ToSingle(tbVhfCompPropEquationACh1.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompPropEquationACh1.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Prop equation A: " +
                    tbVhfCompPropEquationACh1.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationACh1.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationBCh1.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompPropEquationBCh1.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Prop equation B: " +
                    tbVhfCompPropEquationBCh1.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationBCh1.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationCCh1.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompPropEquationCCh1.Text) > 327.67) {
                MessageBox.Show("VHF Comp Prop equation C: " +
                    tbVhfCompPropEquationCCh1.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationCCh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompPropEquationACh2.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompPropEquationACh2.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Prop equation A: " +
                    tbVhfCompPropEquationACh2.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationACh2.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];
            
            if (Convert.ToSingle(tbVhfCompPropEquationBCh2.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompPropEquationBCh2.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Prop equation B: " +
                    tbVhfCompPropEquationBCh2.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationBCh2.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[8] = bATmp[1];
            data[9] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationCCh2.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompPropEquationCCh2.Text) > 327.67) {
                MessageBox.Show("VHF Comp Prop equation C: " +
                    tbVhfCompPropEquationCCh2.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationCCh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[10] = bATmp[1];
            data[11] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompPropEquationACh3.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompPropEquationACh3.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Prop equation A: " +
                    tbVhfCompPropEquationACh3.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationACh3.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[12] = bATmp[1];
            data[13] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationBCh3.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompPropEquationBCh3.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Prop equation B: " +
                    tbVhfCompPropEquationBCh3.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationBCh3.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[14] = bATmp[1];
            data[15] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationCCh3.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompPropEquationCCh3.Text) > 327.67) {
                MessageBox.Show("VHF Comp Prop equation C: " +
                    tbVhfCompPropEquationCCh3.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationCCh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[16] = bATmp[1];
            data[17] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompPropEquationACh4.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompPropEquationACh4.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Prop equation A: " +
                    tbVhfCompPropEquationACh4.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationACh4.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[18] = bATmp[1];
            data[19] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationBCh4.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompPropEquationBCh4.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Prop equation B: " +
                    tbVhfCompPropEquationBCh4.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationBCh4.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[20] = bATmp[1];
            data[21] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompPropEquationCCh4.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompPropEquationCCh4.Text) > 327.67) {
                MessageBox.Show("VHF Comp Prop equation C: " +
                    tbVhfCompPropEquationCCh4.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompPropEquationCCh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[22] = bATmp[1];
            data[23] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompConstEquationACh1.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompConstEquationACh1.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Const equation A: " +
                    tbVhfCompConstEquationACh1.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationACh1.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[24] = bATmp[1];
            data[25] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationBCh1.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompConstEquationBCh1.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Const equation B: " +
                    tbVhfCompConstEquationBCh1.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationBCh1.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[26] = bATmp[1];
            data[27] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationCCh1.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompConstEquationCCh1.Text) > 327.67) {
                MessageBox.Show("VHF Comp Const equation C: " +
                    tbVhfCompConstEquationCCh1.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationCCh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[28] = bATmp[1];
            data[29] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompConstEquationACh2.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompConstEquationACh2.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Const equation A: " +
                    tbVhfCompConstEquationACh2.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationACh2.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[30] = bATmp[1];
            data[31] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationBCh2.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompConstEquationBCh2.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Const equation B: " +
                    tbVhfCompConstEquationBCh2.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationBCh2.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[32] = bATmp[1];
            data[33] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationCCh2.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompConstEquationCCh2.Text) > 327.67) {
                MessageBox.Show("VHF Comp Const equation C: " +
                    tbVhfCompConstEquationCCh2.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationCCh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[34] = bATmp[1];
            data[35] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompConstEquationACh3.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompConstEquationACh3.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Const equation A: " +
                    tbVhfCompConstEquationACh3.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationACh3.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[36] = bATmp[1];
            data[37] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationBCh3.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompConstEquationBCh3.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Const equation B: " +
                    tbVhfCompConstEquationBCh3.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationBCh3.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[38] = bATmp[1];
            data[39] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationCCh3.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompConstEquationCCh3.Text) > 327.67) {
                MessageBox.Show("VHF Comp Const equation C: " +
                    tbVhfCompConstEquationCCh3.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationCCh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[40] = bATmp[1];
            data[41] = bATmp[0];

            if ((Convert.ToSingle(tbVhfCompConstEquationACh4.Text) < -0.32768) ||
                (Convert.ToSingle(tbVhfCompConstEquationACh4.Text) > 0.32767)) {
                MessageBox.Show("VHF Comp Const equation A: " +
                    tbVhfCompConstEquationACh4.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationACh4.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[42] = bATmp[1];
            data[43] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationBCh4.Text) < -3.2768 ||
                Convert.ToSingle(tbVhfCompConstEquationBCh4.Text) > 3.2767) {
                MessageBox.Show("VHF Comp Const equation B: " +
                    tbVhfCompConstEquationBCh4.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationBCh4.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[44] = bATmp[1];
            data[45] = bATmp[0];

            if (Convert.ToSingle(tbVhfCompConstEquationCCh4.Text) < -327.68 ||
                Convert.ToSingle(tbVhfCompConstEquationCCh4.Text) > 327.67) {
                MessageBox.Show("VHF Comp Const equation C: " +
                    tbVhfCompConstEquationCCh4.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbVhfCompConstEquationCCh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[46] = bATmp[1];
            data[47] = bATmp[0];

            if ((Convert.ToSingle(tbPeakEnEquationACh1.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakEnEquationACh1.Text) > 0.32767)) {
                MessageBox.Show("Peak En equation A: " +
                    tbPeakEnEquationACh1.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationACh1.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[48] = bATmp[1];
            data[49] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationBCh1.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakEnEquationBCh1.Text) > 3.2767) {
                MessageBox.Show("Peak En equation B: " +
                    tbPeakEnEquationBCh1.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationBCh1.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[50] = bATmp[1];
            data[51] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationCCh1.Text) < -327.68 ||
                Convert.ToSingle(tbPeakEnEquationCCh1.Text) > 327.67) {
                MessageBox.Show("Peak En equation C: " +
                    tbPeakEnEquationCCh1.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationCCh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[52] = bATmp[1];
            data[53] = bATmp[0];

            if ((Convert.ToSingle(tbPeakEnEquationACh2.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakEnEquationACh2.Text) > 0.32767)) {
                MessageBox.Show("Peak En equation A: " +
                    tbPeakEnEquationACh2.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationACh2.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[54] = bATmp[1];
            data[55] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationBCh2.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakEnEquationBCh2.Text) > 3.2767) {
                MessageBox.Show("Peak En equation B: " +
                    tbPeakEnEquationBCh2.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationBCh2.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[56] = bATmp[1];
            data[57] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationCCh2.Text) < -327.68 ||
                Convert.ToSingle(tbPeakEnEquationCCh2.Text) > 327.67) {
                MessageBox.Show("Peak En equation C: " +
                    tbPeakEnEquationCCh2.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationCCh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[58] = bATmp[1];
            data[59] = bATmp[0];

            if ((Convert.ToSingle(tbPeakEnEquationACh3.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakEnEquationACh3.Text) > 0.32767)) {
                MessageBox.Show("Peak En equation A: " +
                    tbPeakEnEquationACh3.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationACh3.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[60] = bATmp[1];
            data[61] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationBCh3.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakEnEquationBCh3.Text) > 3.2767) {
                MessageBox.Show("Peak En equation B: " +
                    tbPeakEnEquationBCh3.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationBCh3.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[62] = bATmp[1];
            data[63] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationCCh3.Text) < -327.68 ||
                Convert.ToSingle(tbPeakEnEquationCCh3.Text) > 327.67) {
                MessageBox.Show("Peak En equation C: " +
                    tbPeakEnEquationCCh3.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationCCh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[64] = bATmp[1];
            data[65] = bATmp[0];

            if ((Convert.ToSingle(tbPeakEnEquationACh4.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakEnEquationACh4.Text) > 0.32767)) {
                MessageBox.Show("Peak En equation A: " +
                    tbPeakEnEquationACh4.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationACh4.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[66] = bATmp[1];
            data[67] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationBCh4.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakEnEquationBCh4.Text) > 3.2767) {
                MessageBox.Show("Peak En equation B: " +
                    tbPeakEnEquationBCh4.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationBCh4.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[68] = bATmp[1];
            data[69] = bATmp[0];

            if (Convert.ToSingle(tbPeakEnEquationCCh4.Text) < -327.68 ||
                Convert.ToSingle(tbPeakEnEquationCCh4.Text) > 327.67) {
                MessageBox.Show("Peak En equation C: " +
                    tbPeakEnEquationCCh4.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakEnEquationCCh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[70] = bATmp[1];
            data[71] = bATmp[0];

            if ((Convert.ToSingle(tbPeakLenCtrlEquationACh1.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakLenCtrlEquationACh1.Text) > 0.32767)) {
                MessageBox.Show("Peak Len Ctrl equation A: " +
                    tbPeakLenCtrlEquationACh1.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationACh1.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[72] = bATmp[1];
            data[73] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationBCh1.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationBCh1.Text) > 3.2767) {
                MessageBox.Show("Peak Len Ctrl equation B: " +
                    tbPeakLenCtrlEquationBCh1.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationBCh1.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[74] = bATmp[1];
            data[75] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationCCh1.Text) < -327.68 ||
                Convert.ToSingle(tbPeakLenCtrlEquationCCh1.Text) > 327.67) {
                MessageBox.Show("Peak Len Ctrl equation C: " +
                    tbPeakLenCtrlEquationCCh1.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationCCh1.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[76] = bATmp[1];
            data[77] = bATmp[0];

            if ((Convert.ToSingle(tbPeakLenCtrlEquationACh2.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakLenCtrlEquationACh2.Text) > 0.32767)) {
                MessageBox.Show("Peak Len Ctrl equation A: " +
                    tbPeakLenCtrlEquationACh2.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationACh2.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[78] = bATmp[1];
            data[79] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationBCh2.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationBCh2.Text) > 3.2767) {
                MessageBox.Show("Peak Len Ctrl equation B: " +
                    tbPeakLenCtrlEquationBCh2.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationBCh2.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[80] = bATmp[1];
            data[81] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationCCh2.Text) < -327.68 ||
                Convert.ToSingle(tbPeakLenCtrlEquationCCh2.Text) > 327.67) {
                MessageBox.Show("Peak Len Ctrl equation C: " +
                    tbPeakLenCtrlEquationCCh2.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationCCh2.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[82] = bATmp[1];
            data[83] = bATmp[0];

            if ((Convert.ToSingle(tbPeakLenCtrlEquationACh3.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakLenCtrlEquationACh3.Text) > 0.32767)) {
                MessageBox.Show("Peak Len Ctrl equation A: " +
                    tbPeakLenCtrlEquationACh3.Text +
                    " out of range (-0.32768 ~ 0.32767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationACh3.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[84] = bATmp[1];
            data[85] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationBCh3.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationBCh3.Text) > 3.2767) {
                MessageBox.Show("Peak Len Ctrl equation B: " +
                    tbPeakLenCtrlEquationBCh3.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationBCh3.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[86] = bATmp[1];
            data[87] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationCCh3.Text) < -327.68 ||
                Convert.ToSingle(tbPeakLenCtrlEquationCCh3.Text) > 327.67) {
                MessageBox.Show("Peak Len Ctrl equation C: " +
                    tbPeakLenCtrlEquationCCh3.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationCCh3.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[88] = bATmp[1];
            data[89] = bATmp[0];

            if ((Convert.ToSingle(tbPeakLenCtrlEquationACh4.Text) < -0.32768) ||
                (Convert.ToSingle(tbPeakLenCtrlEquationACh4.Text) > 0.32767)) {
                MessageBox.Show("Peak Len Ctrl equation A: " +
                    tbPeakLenCtrlEquationACh4.Text +
                    " out of range (-0.128 ~ 0.127)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationACh4.Text) * 100000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[90] = bATmp[1];
            data[91] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationBCh4.Text) < -3.2768 ||
                Convert.ToSingle(tbPeakLenCtrlEquationBCh4.Text) > 3.2767) {
                MessageBox.Show("Peak Len Ctrl equation B: " +
                    tbPeakLenCtrlEquationBCh4.Text +
                    " out of range (-3.2768 ~ 3.2767)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationBCh4.Text) * 10000);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[92] = bATmp[1];
            data[93] = bATmp[0];

            if (Convert.ToSingle(tbPeakLenCtrlEquationCCh4.Text) < -327.68 ||
                Convert.ToSingle(tbPeakLenCtrlEquationCCh4.Text) > 327.67) {
                MessageBox.Show("Peak Len Ctrl equation C: " +
                    tbPeakLenCtrlEquationCCh4.Text +
                    " out of range (-327.68 ~ 327.67)!!");
                return -1;
            }
            try {
                s16Tmp = Convert.ToInt16(Convert.ToSingle(tbPeakLenCtrlEquationCCh4.Text) * 100);
            }
            catch (Exception eC) {
                MessageBox.Show(eC.ToString());
                return -1;
            }
            bATmp = BitConverter.GetBytes(s16Tmp);
            data[94] = bATmp[1];
            data[95] = bATmp[0];

            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 128, 96, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x14\n";
                _I2cReadToString(80, 128, 96, data, ref sAcConfig);
            }
            else
            {
                if (qsfpI2cWriteCB(80, 128, 96, data) < 0)
                    return -1;
            }

            Thread.Sleep(1);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                    return -1;
            }

            if (cbTemperatureCompensation.Checked)
                data[0] = 0x01;
            else
                data[0] = 0x00;

            if (cbBurnIn.Checked)
                data[0] |= 0x02;

            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 252, 1, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x1\n";
                _I2cReadToString(80, 252, 1, data, ref sAcConfig);
            }
            else
            {
                if (qsfpI2cWriteCB(80, 252, 1, data) < 0)
                    return -1;
            }

            return 0;
        }

        private int _WriteBootLoaderIdentify()
        {
            byte[] data = new byte[2];

            if (bStoreAcConfigToFile.Enabled == true) {
                if (_WritePassword() < 0)
                    return -1;

                if (_SetQsfpMode(0x4D) < 0)
                    return -1;
            }

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else
                qsfpI2cWriteCB(80, 127, 1, data);

            data[0] = 0x19;
            data[1] = 0x3A;
            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 253, 2, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x1\n";
                _I2cReadToString(80, 253, 2, data, ref sAcConfig);
            }
            else
                qsfpI2cWriteCB(80, 253, 2, data);

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
                tbAverageCurrentEquationACh2.Text = 
                tbAverageCurrentEquationBCh2.Text =
                tbAverageCurrentEquationCCh2.Text =
                tbAverageCurrentEquationACh3.Text =
                tbAverageCurrentEquationBCh3.Text =
                tbAverageCurrentEquationCCh3.Text =
                tbAverageCurrentEquationACh4.Text =
                tbAverageCurrentEquationBCh4.Text =
                tbAverageCurrentEquationCCh4.Text =
                tbModulationCurrentMin.Text =
                tbModulationCurrentMax.Text =
                tbModulationCurrentEquationACh1.Text =
                tbModulationCurrentEquationBCh1.Text =
                tbModulationCurrentEquationCCh1.Text =
                tbModulationCurrentEquationACh2.Text =
                tbModulationCurrentEquationBCh2.Text =
                tbModulationCurrentEquationCCh2.Text =
                tbModulationCurrentEquationACh3.Text =
                tbModulationCurrentEquationBCh3.Text =
                tbModulationCurrentEquationCCh3.Text =
                tbModulationCurrentEquationACh4.Text =
                tbModulationCurrentEquationBCh4.Text =
                tbModulationCurrentEquationCCh4.Text =
                tbVhfCompPropEquationACh1.Text =
                tbVhfCompPropEquationBCh1.Text =
                tbVhfCompPropEquationCCh1.Text =
                tbVhfCompPropEquationACh2.Text =
                tbVhfCompPropEquationBCh2.Text =
                tbVhfCompPropEquationCCh2.Text =
                tbVhfCompPropEquationACh3.Text =
                tbVhfCompPropEquationBCh3.Text =
                tbVhfCompPropEquationCCh3.Text =
                tbVhfCompPropEquationACh4.Text =
                tbVhfCompPropEquationBCh4.Text =
                tbVhfCompPropEquationCCh4.Text =
                tbVhfCompConstEquationACh1.Text =
                tbVhfCompConstEquationBCh1.Text =
                tbVhfCompConstEquationCCh1.Text =
                tbVhfCompConstEquationACh2.Text =
                tbVhfCompConstEquationBCh2.Text =
                tbVhfCompConstEquationCCh2.Text =
                tbVhfCompConstEquationACh3.Text =
                tbVhfCompConstEquationBCh3.Text =
                tbVhfCompConstEquationCCh3.Text =
                tbVhfCompConstEquationACh4.Text =
                tbVhfCompConstEquationBCh4.Text =
                tbVhfCompConstEquationCCh4.Text =
                tbPeakEnEquationACh1.Text =
                tbPeakEnEquationBCh1.Text =
                tbPeakEnEquationCCh1.Text =
                tbPeakEnEquationACh2.Text =
                tbPeakEnEquationBCh2.Text =
                tbPeakEnEquationCCh2.Text =
                tbPeakEnEquationACh3.Text =
                tbPeakEnEquationBCh3.Text =
                tbPeakEnEquationCCh3.Text =
                tbPeakEnEquationACh4.Text =
                tbPeakEnEquationBCh4.Text =
                tbPeakEnEquationCCh4.Text =
                tbPeakLenCtrlEquationACh1.Text =
                tbPeakLenCtrlEquationBCh1.Text =
                tbPeakLenCtrlEquationCCh1.Text =
                tbPeakLenCtrlEquationACh2.Text =
                tbPeakLenCtrlEquationBCh2.Text =
                tbPeakLenCtrlEquationCCh2.Text =
                tbPeakLenCtrlEquationACh3.Text =
                tbPeakLenCtrlEquationBCh3.Text =
                tbPeakLenCtrlEquationCCh3.Text =
                tbPeakLenCtrlEquationACh4.Text =
                tbPeakLenCtrlEquationBCh4.Text =
                tbPeakLenCtrlEquationCCh4.Text = "0";

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

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;
            
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

        private int _EnableBurnIn()
        {
            byte[] data = new byte[1];

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 252, 1, data) != 1)
                return -1;

            data[0] |= 0x02;
            if (qsfpI2cWriteCB(80, 252, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _DisableBurnIn()
        {
            byte[] data = new byte[1];

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 252, 1, data) != 1)
                return -1;

            data[0] &= 0xFD;
            if (qsfpI2cWriteCB(80, 252, 1, data) < 0)
                return -1;

            return 0;
        }

        private void cbBurnIn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBurnIn.Checked == true) {
                if (_EnableBurnIn() < 0)
                    return;
            }
            else {
                if (_DisableBurnIn() < 0)
                    return;
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            bVoltageReset_Click(sender, e);
            bTemperatureReset_Click(sender, e);
            bRxPowerRateReset_Click(sender, e);
            bTxPowerRateReset_Click(sender, e);
        }

        private void bAutoCorrect_Click(object sender, EventArgs e)
        {
            bVoltageAutoCorrect_Click(sender, e);
            bTemperatureAutoCorrect_Click(sender, e);
            bRxPowerRateAutoCorrect_Click(sender, e);
            bTxPowerRateAutoCorrect_Click(sender, e);
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

        private int _SaveIntoFlash()
        {
            byte[] data = new byte[1];

            if (bStoreAcConfigToFile.Enabled == true) {
                if (_WritePassword() < 0)
                    goto exit;

                if (_SetQsfpMode(0x4D) < 0)
                    goto exit;
            }

            if (qsfpI2cWriteCB == null)
                goto exit;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;
            
            if (bStoreAcConfigToFile.Enabled == false)
                _I2cWriteToString(80, 127, 1, data, ref sAcConfig);
            else {
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                    goto exit;
            }

            data[0] = 0xAA;
            if (bStoreAcConfigToFile.Enabled == false) {
                _I2cWriteToString(80, 162, 1, data, ref sAcConfig);
                sAcConfig += "Delay10mSec,0x64\n"; //* Wait flash write done */
            }
            else {
                if (qsfpI2cWriteCB(80, 162, 1, data) < 0)
                    goto exit;
                Thread.Sleep(1000);
            }

        exit:
            return 0;
        }

        private void bSaveIntoFlash_Click(object sender, EventArgs e)
        {
            bStoreIntoFlash.Enabled = false;
            _SaveIntoFlash();
            bStoreIntoFlash.Enabled = true;
        }

        private void bLutTemperatureUpdate_Click(object sender, EventArgs e)
        {
            _ReadLutTemperature();
        }

        private int _ReadTxPowerRate()
        {
            String[] txPower = new String[4];
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbTxRssi1.Text = tbTxRssi2.Text = tbTxRssi3.Text = tbTxRssi4.Text = "";
            tbTxPowerRate1.Text = tbTxPowerRate2.Text = tbTxPowerRate3.Text = tbTxPowerRate4.Text = "";
            tbTxPower1.Text = tbTxPower2.Text = tbTxPower3.Text = tbTxPower4.Text = "";

            if (powerMeterReadCB == null)
                return -1;

            //if (powerMeterReadCB(txPower) < 0)
            //    return -1;
            txPower[0] = txPower[1] = txPower[2] = txPower[3] = "1200.0";

            if ((txPower[0].Equals("NA") &&
                txPower[1].Equals("NA") &&
                txPower[2].Equals("NA") &&
                txPower[3].Equals("NA"))) {
                MessageBox.Show("Tx output power read fail!!");
                return -1;
            }

            tbTxOutputPower1.Text = txPower[0];
            tbTxOutputPower1.Update();
            tbTxOutputPower2.Text = txPower[1];
            tbTxOutputPower2.Update();
            tbTxOutputPower3.Text = txPower[2];
            tbTxOutputPower3.Update();
            tbTxOutputPower4.Text = txPower[3];
            tbTxOutputPower4.Update();

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 50, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower1.Text = power.ToString("#0.0");
            tbTxPower1.Update();

            if (qsfpI2cReadCB(80, 52, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower2.Text = power.ToString("#0.0");
            tbTxPower2.Update();

            if (qsfpI2cReadCB(80, 54, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower3.Text = power.ToString("#0.0");
            tbTxPower3.Update();

            if (qsfpI2cReadCB(80, 56, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTxPower4.Text = power.ToString("#0.0");
            tbTxPower4.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;
            
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 136, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi1.Text = tmp.ToString();
            tbTxRssi1.Update();

            if (qsfpI2cReadCB(80, 138, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi2.Text = tmp.ToString();
            tbTxRssi2.Update();

            if (qsfpI2cReadCB(80, 140, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi3.Text = tmp.ToString();
            tbTxRssi3.Update();

            if (qsfpI2cReadCB(80, 142, 2, data) != 2) {
                if (bContinuousRead == false)
                    return -1;
            }

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbTxRssi4.Text = tmp.ToString();
            tbTxRssi4.Update();

            if (qsfpI2cReadCB(80, 163, 1, data) != 1) {
                if (bContinuousRead == false)
                    return -1;
            }

            tbTxPowerRateDefault.Text = data[0].ToString();
            tbTxPowerRateDefault.Update();

            if (tbTxPowerRateMax.Text.Length == 0) {
                tbTxPowerRateMax.Text = (data[0] + 15).ToString();
                tbTxPowerRateMax.Update();
            }

            if (tbTxPowerRateMin.Text.Length == 0) {
                tbTxPowerRateMin.Text = (data[0] - 12).ToString();
                tbTxPowerRateMin.Update();
            }
            
            data = new byte[4];
            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;
            
            qsfpI2cWriteCB(80, 127, 1, data);
            if (qsfpI2cReadCB(80, 248, 4, data) != 4) {
                if (bContinuousRead == false)
                    return -1;
            }

            tbTxPowerRate1.Text = data[0].ToString();
            tbTxPowerRate1.Update();
            tbTxPowerRate2.Text = data[1].ToString();
            tbTxPowerRate2.Update();
            tbTxPowerRate3.Text = data[2].ToString();
            tbTxPowerRate3.Update();
            tbTxPowerRate4.Text = data[3].ToString();
            tbTxPowerRate4.Update();

            return 0;
        }

        private void bTxPowerRateRead_Click(object sender, EventArgs e)
        {
            if (_ReadTxPowerRate() < 0)
                return;
        }

        private int _WriteTxPowerRate()
        {
            byte[] data = new byte[4];

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

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

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
                input = Convert.ToSingle(tbTxOutputPowerRssi1.Text);
                rssi = Convert.ToSingle(tbTxRssi1.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 10000 / input));
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
                input = Convert.ToSingle(tbTxOutputPowerRssi2.Text);
                rssi = Convert.ToSingle(tbTxRssi2.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 10000 / input));
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
                input = Convert.ToSingle(tbTxOutputPowerRssi3.Text);
                rssi = Convert.ToSingle(tbTxRssi3.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 10000 / input));
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
                input = Convert.ToSingle(tbTxOutputPowerRssi4.Text);
                rssi = Convert.ToSingle(tbTxRssi4.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 10000 / input));
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

        private void tbTxOutputPower1_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try
            {
                input = Convert.ToSingle(tbTxOutputPower1.Text);
                numerator = Convert.ToUInt32(tbTxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbTxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbTxOutputPowerRssi1.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbTxOutputPower2_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try
            {
                input = Convert.ToSingle(tbTxOutputPower2.Text);
                numerator = Convert.ToUInt32(tbTxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbTxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbTxOutputPowerRssi2.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbTxOutputPower3_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try
            {
                input = Convert.ToSingle(tbTxOutputPower3.Text);
                numerator = Convert.ToUInt32(tbTxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbTxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbTxOutputPowerRssi3.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbTxOutputPower4_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try
            {
                input = Convert.ToSingle(tbTxOutputPower4.Text);
                numerator = Convert.ToUInt32(tbTxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbTxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbTxOutputPowerRssi4.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT)
            {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void bStoreAcConfigToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();

            
            bStoreAcConfigToFile.Enabled = false;
            sAcConfig = "//Write,DevAddr,RegAddr,Value\n" + "//Read,DevAddr,RegAddr,Value\n" + 
                "//WriteMulti,DevAddr,RegAddr,Length,Value0,Value1...\n" +
                "//ReadMulti,DevAddr,RegAddr,Length,Value0,Value1...\n" +
                "//Delay10mSec,Time\n";
            _WriteVoltageCorrector();
            _WriteTemperatureCorrector();
            _WriteAcMcCorrectData();
            _WriteBootLoaderIdentify();
            _SaveIntoFlash();

            sfdSelectFile.Filter = "cfg files (*.cfg)|*.cfg";
            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                goto exit;
            System.IO.File.WriteAllText(sfdSelectFile.FileName, sAcConfig);
        exit:
            bStoreAcConfigToFile.Enabled = true;
        }

        private void cbContinuousRead_CheckedChanged(object sender, EventArgs e)
        {
            if (cbContinuousRead.Checked == false)
                return;

            if (bContinuousRead == true)
                return;

            bContinuousRead = true;
            bwMonitor.RunWorkerAsync();
        }
        
		public int ReadAll()
        {
            bLutTemperatureUpdate.Enabled = false;
            bAcMcRead.Enabled = false;
            bVoltageRead.Enabled = false;
            bTemperatureRead.Enabled = false;


            if (_ReadLutTemperature() < 0)
                goto exit;

            if (_ReadAverageCurrentAndModulationCurrentCorrectData() < 0)
                goto exit;

            if (_ReadVoltage() < 0)
                goto exit;

            if (_ReadTemperature() < 0)
                goto exit;


        
            bLutTemperatureUpdate.Enabled = true;
            bAcMcRead.Enabled = true;
            bVoltageRead.Enabled = true;
            bTemperatureRead.Enabled = true;
            return 0;

        exit:
            bLutTemperatureUpdate.Enabled = true;
            bAcMcRead.Enabled = true;
            bVoltageRead.Enabled = true;
            bTemperatureRead.Enabled = true;
            return -1;
        }
    }
}
