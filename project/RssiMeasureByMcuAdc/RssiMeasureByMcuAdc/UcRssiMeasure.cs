using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RssiMeasureByMcuAdc
{
    public partial class UcRssiMeasure : UserControl
    {
        private DataTable dtValue = new DataTable();
        private String fileDirectory = "d:\\RssiMeasureLog";
        private String lastLogFileName = "";

        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB lightSourceI2cReadCB = null;
        private I2cWriteCB lightSourceI2cWriteCB = null;
        private I2cReadCB rssiMeasureI2cReadCB = null;
        private I2cWriteCB rssiMeasureI2cWriteCB = null;

        private BackgroundWorker bwMonitor;
        private DialogResult drAskOverwrite;
        private volatile String[] rxRssiValue = new String[4];
        private volatile String logModeSelect;
        private volatile String serialNumber;
        private volatile String lastNote;
        private volatile bool monitorStart = false;
        private volatile bool logValue = false;
        

        public UcRssiMeasure()
        {
            InitializeComponent();

            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "0";
            lastNote = serialNumber = "";

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);

            Directory.CreateDirectory(fileDirectory);
            tbLogFilePath.Text = fileDirectory + "\\";

            dtValue.Columns.Add("標籤", typeof(String));
            dtValue.Columns.Add("時間", typeof(String));
            dtValue.Columns.Add("序號", typeof(String));

            dtValue.Columns.Add("Rssi1", typeof(String));
            dtValue.Columns.Add("Rssi2", typeof(String));
            dtValue.Columns.Add("Rssi3", typeof(String));
            dtValue.Columns.Add("Rssi4", typeof(String));

            dtValue.Columns.Add("操作員", typeof(String));
            dtValue.Columns.Add("Note", typeof(String));

            dgvRecord.DataSource = dtValue;
        }

        public int SetLightSourceI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            lightSourceI2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetLightSourceI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            lightSourceI2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        public int SetRssiMeasureI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            rssiMeasureI2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetRssiMeasureI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            rssiMeasureI2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        private int _SetLightSouceTxDisable(byte txDisable)
        {
            byte[] baWritedata = new byte[1];
            byte[] baReadData = new byte[1];
            int devAddr, page, regAddr;
            int rv;

            if (lightSourceI2cWriteCB == null)
                return -1;

            if (lightSourceI2cReadCB == null)
                return -1;

            int.TryParse(tbI2cLightSourseDevAddr.Text, out devAddr);
            int.TryParse(tbI2cLightSourseRegisterPage.Text, out page);
            int.TryParse(tbI2cLightSourseRegisterAddr.Text, out regAddr);

            baWritedata[0] = (byte)page;

            if (lightSourceI2cWriteCB((byte)devAddr, 127, 1, baWritedata) < 0)
                return -1;

            baWritedata[0] = txDisable;

            if (lightSourceI2cWriteCB((byte)devAddr, (byte)regAddr, 1, baWritedata) < 0)
                return -1;

            Thread.Sleep(100); // Wait value stable

            rv = lightSourceI2cReadCB((byte)devAddr, (byte)regAddr, 1, baReadData);
            if (rv != 1)
                return -1;

            if (baReadData[0] != baWritedata[0])
                MessageBox.Show("設定 TxDisable 失敗!! 讀(" + baReadData[0].ToString("X2") +
                    ") != 寫(" + baWritedata[0].ToString("X2") + ")\n!!請重新記錄!!");

            return 0;
        }

        private int _ReadRssiValue(int channel)
        {
            byte[] data = new byte[2];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            if (rssiMeasureI2cWriteCB == null)
                return -1;

            if (rssiMeasureI2cReadCB == null)
                return -1;

            if ((channel < 0) || (channel > 4))
                return -1;

            int.TryParse(tbI2cRssiDevAddr.Text, out devAddr);
            int.TryParse(tbI2cRssiRegisterPage.Text, out page);
            int.TryParse(tbI2cRssiRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (rssiMeasureI2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (rssiMeasureI2cReadCB((byte)devAddr, (byte)regAddr, 2, data) != 2)
                goto clearData;

            try
            {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[channel] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            rxRssiValue[channel] = "0";

            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            int waitValueStableDelay = 200;
            bool bGetModuleMonitorValueError;

            while (monitorStart)
            {
                bGetModuleMonitorValueError = false;

                if (logValue == true)
                {
                    switch (logModeSelect)
                    {
                        case "4 Channel":
                            bwMonitor.ReportProgress(1, null);
                            if (_SetLightSouceTxDisable(0x0E) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(2, null);
                            if (_ReadRssiValue(0) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(3, null);
                            if (_SetLightSouceTxDisable(0x0D) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(waitValueStableDelay); // Wait value stable
                            bwMonitor.ReportProgress(4, null);
                            if (_ReadRssiValue(1) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(5, null);
                            if (_SetLightSouceTxDisable(0x0B) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(waitValueStableDelay); // Wait value stable
                            bwMonitor.ReportProgress(6, null);
                            if (_ReadRssiValue(2) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(7, null);
                            if (_SetLightSouceTxDisable(0x07) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(waitValueStableDelay); // Wait value stable
                            bwMonitor.ReportProgress(8, null);
                            if (_ReadRssiValue(3) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            if (_SetLightSouceTxDisable(0x0E) < 0)
                            {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);

                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "1 Channel":
                            bwMonitor.ReportProgress(1, null);
                            if (_SetLightSouceTxDisable(0x0E) < 0)
                            {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(2, null);
                            if (_ReadRssiValue(0) < 0)
                            {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(10, null);

                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        default:
                            bwMonitor.ReportProgress(1, null);
                            if (_SetLightSouceTxDisable(0x0E) < 0)
                            {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(500); // Wait value stable
                            if (_ReadRssiValue(0) < 0)
                            {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            break;
                    }
                }
                else
                {
                    if (_ReadRssiValue(0) < 0)
                        bGetModuleMonitorValueError = true;
                    else
                        bwMonitor.ReportProgress(0, null);
                }

                if (bGetModuleMonitorValueError == false)
                    Thread.Sleep(100);
                else
                    Thread.Sleep(500);
            }

            bwMonitor.ReportProgress(100, null);
        }

        public int StartMonitorApi()
        {
            if (monitorStart == true)
                return 0;

            monitorStart = true;
            bwMonitor.RunWorkerAsync();

            if (tbConfigFilePath.Text.Length != 0)
                bLog.Enabled = true;
            else
                bLog.Enabled = false;

            return 0;
        }

        public int StopMonitorApi()
        {
            if (monitorStart == false)
                return 0;

            monitorStart = false;

            return 0;
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            if (tbOperator.Text.Length == 0)
            {
                MessageBox.Show("請輸入工號!!");
                return;
            }
            if (tbSerialNumber.Text.Length < 1)
            {
                MessageBox.Show("請輸入序號!!");
                return;
            }
            if (tbRssi1.Text == "0")
            {
                DialogResult drRxZero = MessageBox.Show("RSSI無讀值異常, 請檢查待測物!!\n(或按No忽略)", "請選擇Yes或No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drRxZero == DialogResult.Yes)
                    return;
            }

            bLog.Enabled = false;
            lAction.Text = "Start log...";
            lResult.Text = "";
            lClassification.Text = "";
            lClassification.BackColor = System.Drawing.SystemColors.Control;
            logModeSelect = cbLogMode.SelectedItem.ToString();
            serialNumber = tbLotNumber.Text.Substring(2, 6) + tbSubLotNumber.Text + tbSerialNumber.Text;

            logValue = true;

            return;
        }

        private int _UpdateRxRssiValueGui()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRssi1Threshold.Text);
            fTmp = float.Parse(rxRssiValue[0]);
            if (fTmp < fThreshold)
                tbRssi1.ForeColor = System.Drawing.Color.Red;
            else
                tbRssi1.ForeColor = SystemColors.ControlText;
            tbRssi1.Text = rxRssiValue[0];
            tbRssi1.Update();

            fThreshold = float.Parse(tbRssi2Threshold.Text);
            fTmp = float.Parse(rxRssiValue[1]);
            if (fTmp < fThreshold)
                tbRssi2.ForeColor = System.Drawing.Color.Red;
            else
                tbRssi2.ForeColor = SystemColors.ControlText;
            tbRssi2.Text = rxRssiValue[1];
            tbRssi2.Update();

            fThreshold = float.Parse(tbRssi3Threshold.Text);
            fTmp = float.Parse(rxRssiValue[2]);
            if (fTmp < fThreshold)
                tbRssi3.ForeColor = System.Drawing.Color.Red;
            else
                tbRssi3.ForeColor = SystemColors.ControlText;
            tbRssi3.Text = rxRssiValue[2];
            tbRssi3.Update();

            fThreshold = float.Parse(tbRssi4Threshold.Text);
            fTmp = float.Parse(rxRssiValue[3]);
            if (fTmp < fThreshold)
                tbRssi4.ForeColor = System.Drawing.Color.Red;
            else
                tbRssi4.ForeColor = SystemColors.ControlText;
            tbRssi4.Text = rxRssiValue[3];
            tbRssi4.Update();

            return 0;
        }

        private int _CheckDuplicationLog(String logLable, String serialNumber)
        {
            DataRow[] filteredRows = dtValue.Select("標籤 = '" + logLable + "' AND 序號 = '" + serialNumber + "'");

            if (filteredRows.Length == 0)
                return 0;

            drAskOverwrite = MessageBox.Show("是否覆蓋舊紀錄?", "發現重複紀錄", MessageBoxButtons.YesNo);
            if (drAskOverwrite == DialogResult.Yes)
                dtValue.Rows.RemoveAt(dtValue.Rows.IndexOf(filteredRows[0]));
            else
                return -1;

            return 0;
        }

        private int _CheckRxValueThreshold(String[] rssiValue)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            fThreshold = float.Parse(tbRssi1Threshold.Text);
            fTmp = float.Parse(rssiValue[0]);
            if (fTmp < fThreshold)
            {
                lastNote += "Rssi1 value (" + rssiValue[0] + ") < Threshold (" + tbRssi1Threshold.Text + "); ";
                bPass = false;
            }

            fThreshold = float.Parse(tbRssi2Threshold.Text);
            fTmp = float.Parse(rssiValue[1]);
            if (fTmp < fThreshold)
            {
                lastNote += "Rssi2 value (" + rssiValue[1] + ") < Threshold (" + tbRssi2Threshold.Text + "); ";
                bPass = false;
            }

            fThreshold = float.Parse(tbRssi3Threshold.Text);
            fTmp = float.Parse(rssiValue[2]);
            if (fTmp < fThreshold)
            {
                lastNote += "Rssi3 value (" + rssiValue[2] + ") < Threshold (" + tbRssi3Threshold.Text + "); ";
                bPass = false;
            }

            fThreshold = float.Parse(tbRssi4Threshold.Text);
            fTmp = float.Parse(rssiValue[3]);
            if (fTmp < fThreshold)
            {
                lastNote += "Rssi4 value (" + rssiValue[3] + ") < Threshold (" + tbRssi4Threshold.Text + "); ";
                bPass = false;
            }

            if (bPass == true)
            {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else
            {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private void _AddLogValue()
        {
            String[] saRssiValue = new String[4];

            while (tbSerialNumber.Text[0] == ' ')
            {
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(tbSerialNumber.Text.IndexOf(' ') + 1,
                    tbSerialNumber.Text.Length - 1);
            }

            if (tbSerialNumber.Text.IndexOf(' ') > 0)
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(0, tbSerialNumber.Text.IndexOf(' '));

            if (_CheckDuplicationLog(tbLogLable.Text, tbSerialNumber.Text) < 0)
                return;

            saRssiValue[0] = tbRssi1.Text;
            saRssiValue[1] = tbRssi2.Text;
            saRssiValue[2] = tbRssi3.Text;
            saRssiValue[3] = tbRssi4.Text;

            _CheckRxValueThreshold(saRssiValue);

            dtValue.Rows.Add(tbLogLable.Text, System.DateTime.Now.ToString("yy/MM/dd_HH:mm:ss"), tbSerialNumber.Text,
                saRssiValue[0], saRssiValue[1], saRssiValue[2], saRssiValue[3], tbOperator.Text, lastNote);
            dgvRecord.FirstDisplayedScrollingRowIndex = 0;
            lastNote = "";
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    lAction.Text = "Enable light source channel 1 ...";
                    lAction.Update();
                    return;

                case 2:
                    lAction.Text = "Wait RSSI1 value stable ...";
                    lAction.Update();
                    return;

                case 3:
                    lAction.Text = "Enable light source channel 2 ...";
                    lAction.Update();
                    return;

                case 4:
                    lAction.Text = "Wait RSSI2 value stable ...";
                    lAction.Update();
                    return;

                case 5:
                    lAction.Text = "Enable light source channel 3 ...";
                    lAction.Update();
                    return;

                case 6:
                    lAction.Text = "Wait RSSI3 value stable ...";
                    lAction.Update();
                    return;

                case 7:
                    lAction.Text = "Enable light source channel 4 ...";
                    lAction.Update();
                    return;

                case 8:
                    lAction.Text = "Wait RSSI4 value stable ...";
                    lAction.Update();
                    return;

                case 10:
                    lAction.Text = "Get value ...";
                    lAction.Update();
                    break;

                default:
                    break;
            }

            _UpdateRxRssiValueGui();
            
            if ((logValue == true) && (e.ProgressPercentage == 10))
            {
                _AddLogValue();
                logValue = false;
                bLog.Enabled = true;
                lAction.Text = "Log Added.";
                lAction.Update();
            }
        }

        private void _SaveLogFile()
        {
            StreamWriter swLog;

            if (lastLogFileName == "")
                return;

            swLog = new StreamWriter(tbLogFilePath.Text);
            swLog.WriteLine("Lable,Time,SN,RSSI1,RSSI2,RSSI3,RSSI4,Operator,Note");
            foreach (DataRow row in dtValue.Rows)
            {
                swLog.WriteLine(row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                    row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString() + "," +
                    row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString());
            }
            swLog.Close();
            tbLotNumber.Text = "";
            tbSubLotNumber.Text = "";
            lastLogFileName = "";
            tbLogFilePath.Text = fileDirectory;
            dtValue.Clear();
        }

        private void _OpenLogFile()
        {
            StreamReader srLog;
            String sLine;
            String[] saItems;
            int iSerialNumber;

            lastLogFileName = tbLotNumber.Text + "-" + tbSubLotNumber.Text + ".csv";
            tbLogFilePath.Text = fileDirectory + "\\" + lastLogFileName;
            tbLogFilePath.Update();

            try
            {
                srLog = new StreamReader(tbLogFilePath.Text);
            }
            catch (FileNotFoundException e)
            {
                if (e != null)
                    return;
                return;
            }

            if ((sLine = srLog.ReadLine()) == null) //Header
                return;

            while ((sLine = srLog.ReadLine()) != null)
            { //Record
                saItems = sLine.Split(',');
                if (saItems.Length < 9)
                    continue;

                int.TryParse(saItems[2], out iSerialNumber);

                if (saItems.Length == 9)
                { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], iSerialNumber.ToString(), saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8]);
                }
            }

            srLog.Close();
            dgvRecord.Sort(dgvRecord.Columns[1], ListSortDirection.Descending);
        }

        private void tbLotNumber_Leave(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbLotNumber.Text.Length > 8)
            {
                saTmp = tbLotNumber.Text.Split('-');
                tbLotNumber.Text = saTmp[0];
                if (saTmp.Length >= 2)
                {
                    int.TryParse(saTmp[1], out iTmp);
                    tbSubLotNumber.Text = iTmp.ToString("000");
                }
                if (saTmp.Length >= 3)
                {
                    int.TryParse(saTmp[2], out iTmp);
                    tbSerialNumber.Text = iTmp.ToString();
                }
            }

            if ((tbLotNumber.Text.Length < 8) || (tbSubLotNumber.Text.Length < 1))
                return;

            _OpenLogFile();
        }

        private void tbLotNumber_Enter(object sender, EventArgs e)
        {
            DirectoryInfo diLogFiles = new DirectoryInfo(fileDirectory);
            FileInfo[] files = diLogFiles.GetFiles("*.csv");
            AutoCompleteStringCollection acc = new AutoCompleteStringCollection();
            int index;
            foreach (FileInfo file in files)
            {
                index = file.Name.IndexOf(".csv");
                if (index > 0)
                    acc.Add(file.Name.Substring(0, index));
            }
            tbLotNumber.AutoCompleteCustomSource = acc;

            if (lastLogFileName.Length == 0)
                return;

            if (dtValue.Rows.Count > 0)
                bSaveFile_Click(sender, e);
        }

        private void tbSubLotNumber_Enter(object sender, EventArgs e)
        {
            if (lastLogFileName.Length == 0)
                return;

            if (dtValue.Rows.Count > 0)
                bSaveFile_Click(sender, e);
        }

        private void tbSubLotNumber_Leave(object sender, EventArgs e)
        {
            int iTmp;

            if ((tbLotNumber.Text.Length < 8) || (tbSubLotNumber.Text.Length < 1))
                return;

            int.TryParse(tbSubLotNumber.Text, out iTmp);
            tbSubLotNumber.Text = iTmp.ToString("000");

            _OpenLogFile();
        }

        private void cbLogMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLogMode.SelectedItem.ToString())
            {
                case "4 Channel":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "4 Channel";
                    break;

                case "1 Channel":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "1 Channel";
                    break;

                default:
                    tbLogLable.Text = "";
                    tbLogLable.Enabled = true;
                    break;
            }
        }

        private void bSaveFile_Click(object sender, EventArgs e)
        {
            if (dtValue.Rows.Count > 0)
                _SaveLogFile();
        }

        public void SaveFileCheckApi(object sender, EventArgs e)
        {
            bSaveFile_Click(sender, e);
        }

        private void bSaveConfig_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();

            sfdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            using (XmlWriter xwConfig = XmlWriter.Create(sfdSelectFile.FileName))
            {
                xwConfig.WriteStartDocument();
                xwConfig.WriteStartElement("RssiMeasureConfig");
                {
                    xwConfig.WriteStartElement("I2cConfig");
                    {
                        xwConfig.WriteElementString("I2cLightSourceRxDevAddr", tbI2cLightSourseDevAddr.Text);
                        xwConfig.WriteElementString("I2cLightSourceRxRegisterPage", tbI2cLightSourseRegisterPage.Text);
                        xwConfig.WriteElementString("I2cLightSourceRxRegisterAddr", tbI2cLightSourseRegisterAddr.Text);
                        xwConfig.WriteElementString("I2cRssiDevAddr", tbI2cRssiDevAddr.Text);
                        xwConfig.WriteElementString("I2cRssiRegisterPage", tbI2cRssiRegisterPage.Text);
                        xwConfig.WriteElementString("I2cRssiRegisterAddr", tbI2cRssiRegisterAddr.Text);
                    }
                    xwConfig.WriteEndElement(); //I2cConfig

                    xwConfig.WriteStartElement("MonitorThresholdConfig");
                    {
                        xwConfig.WriteElementString("Rssi1Threshold", tbRssi1Threshold.Text);
                        xwConfig.WriteElementString("Rssi2Threshold", tbRssi2Threshold.Text);
                        xwConfig.WriteElementString("Rssi3Threshold", tbRssi3Threshold.Text);
                        xwConfig.WriteElementString("Rssi4Threshold", tbRssi4Threshold.Text);
                    }
                    xwConfig.WriteEndElement(); //MonitorThresholdConfig
                }
                xwConfig.WriteEndElement(); //RssiMeasureConfig
                xwConfig.WriteEndDocument();
            }
        }

        private void _PaserI2cConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "I2cLightSourceRxDevAddr":
                            tbI2cLightSourseDevAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cLightSourceRxRegisterPage":
                            tbI2cLightSourseRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cLightSourceRxRegisterAddr":
                            tbI2cLightSourseRegisterAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cRssiDevAddr":
                            tbI2cRssiDevAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cRssiRegisterPage":
                            tbI2cRssiRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cRssiRegisterAddr":
                            tbI2cRssiRegisterAddr.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            return;
                    }
                }
                else
                {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserMonitorThresholdConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Rssi1Threshold":
                            tbRssi1Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rssi2Threshold":
                            tbRssi2Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rssi3Threshold":
                            tbRssi3Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rssi4Threshold":
                            tbRssi4Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        default:
                            return;
                    }
                }
                else
                {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserRssiMeasureConfigXml(XmlReader reader)
        {
            do
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "I2cConfig":
                            _PaserI2cConfigXml(reader);
                            break;

                        case "MonitorThresholdConfig":
                            _PaserMonitorThresholdConfigXml(reader);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            } while (reader.Read());
        }

        private void bOpenConfigFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Title = "選擇設定檔";
            ofdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbConfigFilePath.Text = ofdSelectFile.FileName;
            tbConfigFilePath.SelectionStart = tbConfigFilePath.Text.Length;
            tbConfigFilePath.ScrollToCaret();

            using (XmlReader xrConfig = XmlReader.Create(ofdSelectFile.FileName))
            {
                while (xrConfig.Read())
                {
                    if (xrConfig.IsStartElement())
                    {
                        switch (xrConfig.Name)
                        {
                            case "RssiMeasureConfig":
                                xrConfig.Read();
                                _PaserRssiMeasureConfigXml(xrConfig);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            if (monitorStart == true)
                bLog.Enabled = true;
            else
                bLog.Enabled = false;
        }

        public int SpaceKeyDownApi(object sender, KeyEventArgs e)
        {
            if (bLog.Enabled == false)
                return -1;

            bLog_Click(sender, e);

            return 0;
        }

        public int RightKeyDownApi(object sender, KeyEventArgs e)
        {
            int iSerialNumber;

            if (bLog.Enabled == false)
                return 0;

            int.TryParse(tbSerialNumber.Text, out iSerialNumber);
            if (iSerialNumber + 1 < 10000)
                tbSerialNumber.Text = (++iSerialNumber).ToString();
            tbSerialNumber.Select(tbSerialNumber.Text.Length - 1, tbSerialNumber.Text.Length - 1);

            return 0;
        }

        public int LeftKeyDownApi(object sender, KeyEventArgs e)
        {
            int iSerialNumber;

            if (bLog.Enabled == false)
                return 0;

            int.TryParse(tbSerialNumber.Text, out iSerialNumber);
            if (iSerialNumber - 1 >= 0)
                tbSerialNumber.Text = (--iSerialNumber).ToString();
            tbSerialNumber.Select(0, 0);

            return 0;
        }

        private void tbSerialNumber_Leave(object sender, EventArgs e)
        {
            int iSerialNumber;

            int.TryParse(tbSerialNumber.Text, out iSerialNumber);
            tbSerialNumber.Text = iSerialNumber.ToString();
        }

        public int SetFocusOnLogFilePathApi()
        {
            tbLogFilePath.Focus();
            return 0;
        }

        private void bDelRecord_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvRecord.SelectedRows)
            {
                dgvRecord.Rows.RemoveAt(item.Index);
            }
        }

        private void tbSerialNumber_Enter(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";
        }

        private void tbSerialNumber_TextChanged(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";
        }
    }
}
