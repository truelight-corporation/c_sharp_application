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


namespace QsfpPlus40gSr4DcTest
{
    public partial class UcQsfpPlus40gSr4DcTest : UserControl
    {
        private DataTable dtValue = new DataTable();
        private DataTable dtAfterBurnInConfig = new DataTable();
        private String fileDirectory = "d:\\DcTestLog";
        private String lastLogFileName = "";

        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int PowerMeterReadCB(String[] data);
        public delegate int WritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private PowerMeterReadCB powerMeterReadCB = null;

        private BackgroundWorker bwMonitor;
        private DialogResult drAskOverwrite;
        private volatile String[] txPower = new String[4];
        private volatile String[] mpdValue = new String[4];
        private volatile String[] rxRssiValue = new String[4];
        private volatile String[] rxPowerRate = new String[4];
        private volatile String[] rxPowerValue = new String[4];
        private volatile String[] txBiasValue = new String[4];
        private volatile String temperature;
        private volatile String logModeSelect;
        private volatile String serialNumber, newSerialNumber;
        private volatile String lastNote;
        private volatile int acConfigRowCount;
        private volatile bool monitorStart = false;
        private volatile bool logValue = false;
        private volatile byte[] losStatus = new byte[1];

        public UcQsfpPlus40gSr4DcTest()
        {
            InitializeComponent();

            txPower[0] = txPower[1] = txPower[2] = txPower[3] = "0";
            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "0";
            mpdValue[0] = mpdValue[1] = mpdValue[2] = mpdValue[3] = "0";
            txBiasValue[0] = txBiasValue[1] = txBiasValue[2] = txBiasValue[3] = "0";
            losStatus[0] = 0;
            temperature = "0";
            lastNote = serialNumber= "";

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

            dtValue.Columns.Add("Tx1(uW)", typeof(String));
            dtValue.Columns.Add("Tx2(uW)", typeof(String));
            dtValue.Columns.Add("Tx3(uW)", typeof(String));
            dtValue.Columns.Add("Tx4(uW)", typeof(String));

            dtValue.Columns.Add("Rx1", typeof(String));
            dtValue.Columns.Add("Rx2", typeof(String));
            dtValue.Columns.Add("Rx3", typeof(String));
            dtValue.Columns.Add("Rx4", typeof(String));

            dtValue.Columns.Add("MPD1", typeof(String));
            dtValue.Columns.Add("MPD2", typeof(String));
            dtValue.Columns.Add("MPD3", typeof(String));
            dtValue.Columns.Add("MPD4", typeof(String));

            dtValue.Columns.Add("Bias1", typeof(String));
            dtValue.Columns.Add("Bias2", typeof(String));
            dtValue.Columns.Add("Bias3", typeof(String));
            dtValue.Columns.Add("Bias4", typeof(String));

            dtValue.Columns.Add("溫度", typeof(String));

            dtValue.Columns.Add("操作員", typeof(String));
            dtValue.Columns.Add("Note", typeof(String));

            dgvRecord.DataSource = dtValue;

            dtAfterBurnInConfig.Columns.Add("Command", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter1", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter2", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter3", typeof(String));

            dgvAfterBurnInConfig.DataSource = dtAfterBurnInConfig;
        }

        public int SetI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        public int SetPowerMeterReadCBApi(PowerMeterReadCB cb)
        {
            if (cb == null)
                return -1;

            powerMeterReadCB = new PowerMeterReadCB(cb);

            return 0;
        }

        private int _CheckAfterBurnInPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] oldTxPower = new String[4];
            DataRow[] filteredRows = dtValue.Select("標籤 = 'BeforeBurnIn' AND 序號 = '" + serialNumber + "'");
            float fMin, fMax, fLimit;
            int i;
            bool bPass = true;
            
            if (filteredRows.Length == 0) {
                MessageBox.Show("找不到序號: " + serialNumber + " burn-in 前資料!!");
                return -1;
            }

            oldTxPower[0] = filteredRows.ElementAt(0)["Tx1(uW)"].ToString();
            oldTxPower[1] = filteredRows.ElementAt(0)["Tx2(uW)"].ToString();
            oldTxPower[2] = filteredRows.ElementAt(0)["Tx3(uW)"].ToString();
            oldTxPower[3] = filteredRows.ElementAt(0)["Tx4(uW)"].ToString();

            if (oldTxPower[0].Equals("NA"))
                return 0;

            fLimit = float.Parse(tbAfterBurnInPowerDifferentLimit.Text) / 100;
        
            for (i = 0; i < 4; i++) {
                fMin = (float)(float.Parse(oldTxPower[i]) * (1 - fLimit));
                fMax = (float)(float.Parse(oldTxPower[i]) * (1 + fLimit));
                if ((float.Parse(txPower[i]) < fMin) || (float.Parse(txPower[i]) > fMax)) {
                    bPass = false;
                    lastNote += "Tx" + (i + 1).ToString() + "(B:" + oldTxPower[i] + " A:" + txPower[i] + "); ";
                }
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
            }

            return 0;
        }

        private int _CheckTxPowerThreshold(String[] txPower)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (txPower[0].Equals("NA"))
                return 0;

            try {
                fThreshold = float.Parse(tbTx1Threshold.Text);
                fTmp = float.Parse(txPower[0]);
                if (fTmp < fThreshold) {
                    lastNote += "Tx1 power (" + txPower[0] + "uW) < Threshold (" + tbTx1Threshold.Text + "uW); ";
                    bPass = false;
                }
            } catch(Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbTx2Threshold.Text);
                fTmp = float.Parse(txPower[1]);
                if (fTmp < fThreshold) {
                    lastNote += "Tx2 power (" + txPower[1] + "uW) < Threshold (" + tbTx2Threshold.Text + "uW); ";
                    bPass = false;
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try { 
                fThreshold = float.Parse(tbTx3Threshold.Text);
                fTmp = float.Parse(txPower[2]);
                if (fTmp < fThreshold) {
                    lastNote += "Tx3 power (" + txPower[2] + "uW) < Threshold (" + tbTx3Threshold.Text + "uW); ";
                    bPass = false;
                }
            } catch(Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbTx4Threshold.Text);
                fTmp = float.Parse(txPower[3]);
                if (fTmp < fThreshold) {
                    lastNote += "Tx4 power (" + txPower[3] + "uW) < Threshold (" + tbTx4Threshold.Text + "uW); ";
                    bPass = false;
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckRxValueThreshold(String[] rxValue)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            try {
                fThreshold = float.Parse(tbRx1Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(rxValue[0]);
                    if (fTmp < fThreshold) {
                        lastNote += "Rx1 value (" + rxValue[0] + ") < Threshold (" + tbRx1Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbRx2Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(rxValue[1]);
                    if (fTmp < fThreshold) {
                        lastNote += "Rx2 value (" + rxValue[1] + ") < Threshold (" + tbRx2Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbRx3Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(rxValue[2]);
                    if (fTmp < fThreshold) {
                        lastNote += "Rx3 value (" + rxValue[2] + ") < Threshold (" + tbRx3Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbRx4Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(rxValue[3]);
                    if (fTmp < fThreshold) {
                        lastNote += "Rx4 value (" + rxValue[3] + ") < Threshold (" + tbRx4Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckMpdValueThreshold(String[] mpdValue)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            try {
                fThreshold = float.Parse(tbMpd1Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(mpdValue[0]);
                    if (fTmp < fThreshold) {
                        lastNote += "MPD1 value (" + mpdValue[0] + ") < Threshold (" + tbMpd1Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbMpd2Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(mpdValue[1]);
                    if (fTmp < fThreshold) {
                        lastNote += "MPD2 value (" + mpdValue[1] + ") < Threshold (" + tbMpd2Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbMpd3Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(mpdValue[2]);
                    if (fTmp < fThreshold) {
                        lastNote += "MPD3 value (" + mpdValue[2] + ") < Threshold (" + tbMpd3Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThreshold = float.Parse(tbMpd4Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(mpdValue[3]);
                    if (fTmp < fThreshold) {
                        lastNote += "MPD4 value (" + mpdValue[3] + ") < Threshold (" + tbMpd4Threshold.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckLosStatus()
        {
            bool bPass = true;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            if ((losStatus[0] & 0x01) != 0) {
                bPass = false;
                lastNote += "Rx1 LOS; ";
            }

            if ((losStatus[0] & 0x02) != 0) {
                bPass = false;
                lastNote += "Rx2 LOS; ";
            }

            if ((losStatus[0] & 0x04) != 0) {
                bPass = false;
                lastNote += "Rx3 LOS; ";
            }

            if ((losStatus[0] & 0x08) != 0) {
                bPass = false;
                lastNote += "Rx4 LOS; ";
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckLtPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] ltTxPower = new String[4];
            DataRow[] filteredRows = dtValue.Select("標籤 = '-5' AND 序號 = '" + serialNumber + "'");
            double dLtPower, dRtPower, dMinThreshold, dMaxThreshold;
            int i;
            bool bPass = true;

            if ((tbLtMinThreshold.Text.Length == 0) &&
                (tbLtMaxThreshold.Text.Length == 0))
                return 1;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            if (filteredRows.Length == 0) {
                MessageBox.Show("找不到序號: " + serialNumber + " -5 度資料!!");
                return -1;
            }

            ltTxPower[0] = filteredRows.ElementAt(0)["Tx1(uW)"].ToString();
            ltTxPower[1] = filteredRows.ElementAt(0)["Tx2(uW)"].ToString();
            ltTxPower[2] = filteredRows.ElementAt(0)["Tx3(uW)"].ToString();
            ltTxPower[3] = filteredRows.ElementAt(0)["Tx4(uW)"].ToString();

            dMinThreshold = double.Parse(tbLtMinThreshold.Text);
            dMaxThreshold = double.Parse(tbLtMaxThreshold.Text);

            for (i = 0; i < 4; i++) {
                dLtPower = 10 * Math.Log10(float.Parse(ltTxPower[i]) / 1000);
                dRtPower = 10 * Math.Log10(float.Parse(txPower[i]) / 1000);
                if (dLtPower - dRtPower < dMinThreshold) {
                    bPass = false;
                    lastNote += "Tx" + (i + 1).ToString() + " < Min (LT:" + ltTxPower[i] + " RT:" + txPower[i] + "); ";
                }
                else if (dLtPower - dRtPower > dMaxThreshold) {
                    bPass = false;
                    lastNote += "Tx" + (i + 1).ToString() + " > Max (LT:" + ltTxPower[i] + " RT:" + txPower[i] + "); ";
                }
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckHtPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] htTxPower = new String[4];
            DataRow[] filteredRows = dtValue.Select("標籤 = '70' AND 序號 = '" + serialNumber + "'");
            double dHtPower, dRtPower, dMinThreshold, dMaxThreshold;
            int i;
            bool bPass = true;

            if ((tbHtMinThreshold.Text.Length == 0) &&
                (tbHtMaxThreshold.Text.Length == 0))
                return 1;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            if (filteredRows.Length == 0) {
                MessageBox.Show("找不到序號: " + serialNumber + " 70 度資料!!");
                return -1;
            }

            htTxPower[0] = filteredRows.ElementAt(0)["Tx1(uW)"].ToString();
            htTxPower[1] = filteredRows.ElementAt(0)["Tx2(uW)"].ToString();
            htTxPower[2] = filteredRows.ElementAt(0)["Tx3(uW)"].ToString();
            htTxPower[3] = filteredRows.ElementAt(0)["Tx4(uW)"].ToString();

            dMinThreshold = double.Parse(tbHtMinThreshold.Text);
            dMaxThreshold = double.Parse(tbHtMaxThreshold.Text);

            for (i = 0; i < 4; i++) {
                dHtPower = 10 * Math.Log10(float.Parse(htTxPower[i]) / 1000);
                dRtPower = 10 * Math.Log10(float.Parse(txPower[i]) / 1000);
                if (dHtPower - dRtPower < dMinThreshold) {
                    bPass = false;
                    lastNote += "Tx" + (i + 1).ToString() + " < Min (HT:" + htTxPower[i] + " RT:" + txPower[i] + "); ";
                }
                else if (dHtPower - dRtPower > dMaxThreshold) {
                    bPass = false;
                    lastNote += "Tx" + (i + 1).ToString() + " > Max (HT:" + htTxPower[i] + " RT:" + txPower[i] + "); ";
                }
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

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

        private void _AddLogValue(bool checkPowerDiff)
        {
            String[] saTxPower = new String[4];
            String[] saRxValue = new String[4];
            String[] saMpdValue = new String[4];
            String[] saTxBiasValue = new String[4];
            String sTemperature;

            while (tbSerialNumber.Text[0] == ' ')
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);

            if (tbSerialNumber.Text.IndexOf(' ') > 0)
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(0, tbSerialNumber.Text.IndexOf(' '));

            if (_CheckDuplicationLog(tbLogLable.Text, tbSerialNumber.Text) < 0)
                return;

            saTxPower[0] = tbTx1Power.Text;
            saTxPower[1] = tbTx2Power.Text;
            saTxPower[2] = tbTx3Power.Text;
            saTxPower[3] = tbTx4Power.Text;
            saRxValue[0] = tbRx1.Text;
            saRxValue[1] = tbRx2.Text;
            saRxValue[2] = tbRx3.Text;
            saRxValue[3] = tbRx4.Text;
            saMpdValue[0] = tbMpd1Value.Text;
            saMpdValue[1] = tbMpd2Value.Text;
            saMpdValue[2] = tbMpd3Value.Text;
            saMpdValue[3] = tbMpd4Value.Text;
            saTxBiasValue[0] = tbTxBias1.Text;
            saTxBiasValue[1] = tbTxBias2.Text;
            saTxBiasValue[2] = tbTxBias3.Text;
            saTxBiasValue[3] = tbTxBias4.Text;
            sTemperature = tbTemperature.Text;
     
            if (checkPowerDiff == true)
                _CheckAfterBurnInPowerDifferent(tbSerialNumber.Text, saTxPower);

            _CheckTxPowerThreshold(saTxPower);
            _CheckRxValueThreshold(saRxValue);
            _CheckMpdValueThreshold(saMpdValue);
            _CheckLosStatus();

            if ((cbAutoLogWithLableTemperature.Enabled == false) && (tbLogLable.Text.Equals("25"))) {
                _CheckLtPowerDifferent(tbSerialNumber.Text, saTxPower);
                _CheckHtPowerDifferent(tbSerialNumber.Text, saTxPower);
            }

            dtValue.Rows.Add(tbLogLable.Text, System.DateTime.Now.ToString("yy/MM/dd_HH:mm:ss"), tbSerialNumber.Text,
                saTxPower[0], saTxPower[1], saTxPower[2], saTxPower[3], saRxValue[0], saRxValue[1], saRxValue[2],
                saRxValue[3], saMpdValue[0], saMpdValue[1], saMpdValue[2], saMpdValue[3], saTxBiasValue[0],
                saTxBiasValue[1], saTxBiasValue[2], saTxBiasValue[3], sTemperature, tbOperator.Text, lastNote);
            dgvRecord.FirstDisplayedScrollingRowIndex = 0;
            lastNote = "";
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _SetBias(int uA)
        {
            byte[] data = new byte[1];
            byte[] baWritedata = new byte[2];
            byte[] baReadData = new byte[2];
            Int16 i16Tmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            i16Tmp = (Int16)(uA / 8);
            i16Tmp <<= 5;
            baWritedata = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(baWritedata);

            if (i2cWriteCB(84, 66, 2, baWritedata) < 0)
                return -1;

            if (i2cWriteCB(84, 82, 2, baWritedata) < 0)
                return -1;

            if (i2cWriteCB(84, 98, 2, baWritedata) < 0)
                return -1;

            if (i2cWriteCB(84, 114, 2, baWritedata) < 0)
                return -1;

            if (i2cReadCB(84, 66, 2, baReadData) != 2)
                return -1;

            if ((baReadData[0] != baWritedata[0]) || (baReadData[1] != baWritedata[1]))
                MessageBox.Show("設定 Tx1 bias 失敗!! 讀(" + baReadData[0].ToString("X2") +
                    baReadData[1].ToString("X2") + ") != 寫(" + baWritedata[0].ToString("X2") +
                    baWritedata[1].ToString("X2") + ")\n!!請重新記錄!!");

            if (i2cReadCB(84, 82, 2, baReadData) != 2)
                return -1;

            if ((baReadData[0] != baWritedata[0]) || (baReadData[1] != baWritedata[1]))
                MessageBox.Show("設定 Tx2 bias 失敗!! 讀(" + baReadData[0].ToString("X2") +
                    baReadData[1].ToString("X2") + ") != 寫(" + baWritedata[0].ToString("X2") +
                    baWritedata[1].ToString("X2") + ")\n!!請重新記錄!!");

            if (i2cReadCB(84, 98, 2, baReadData) != 2)
                return -1;

            if ((baReadData[0] != baWritedata[0]) || (baReadData[1] != baWritedata[1]))
                MessageBox.Show("設定 Tx3 bias 失敗!! 讀(" + baReadData[0].ToString("X2") +
                    baReadData[1].ToString("X2") + ") != 寫(" + baWritedata[0].ToString("X2") +
                    baWritedata[1].ToString("X2") + ")\n!!請重新記錄!!");

            if (i2cReadCB(84, 114, 2, baReadData) != 2)
                return -1;

            if ((baReadData[0] != baWritedata[0]) || (baReadData[1] != baWritedata[1]))
                MessageBox.Show("設定 Tx4 bias 失敗!! 讀(" + baReadData[0].ToString("X2") +
                    baReadData[1].ToString("X2") + ") != 寫(" + baWritedata[0].ToString("X2") +
                    baWritedata[1].ToString("X2") + ")\n!!請重新記錄!!");

            return 0;
        }

        private int _SetModuleSerialNumber()
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[16];
            byte[] baReadTmp = new byte[16];
            int devAddr, i;

            if (_WritePassword() < 0)
                return -1;
            
            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            data[0] = 5;
            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                return -1;
            tmp = System.Text.Encoding.Default.GetBytes(newSerialNumber);
            if (tmp.Length > 16) {
                MessageBox.Show("newSerialNumber length:" + data.Length + " > 16 Error!!");
                return -1;
            }
            for (i = 0; i < data.Length; i++) {
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteCB((byte)devAddr, 220, 16, data) < 0)
                return -1;

            /* After write up page 5 addr 223 will set functionFlag(addr 252) so need clear */
            data[0] = 0;
            if (i2cWriteCB((byte)devAddr, 252, 1, data) < 0)
                return -1;

            data[0] = 32;
            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                return -1;
            data[0] = 0xAA;
            if (i2cWriteCB((byte)devAddr, 162, 1, data) < 0)
                return -1;
            
            data[0] = 5;
            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(100); // Wait change page

            if (i2cReadCB((byte)devAddr, 220, 16, baReadTmp) != 16)
                return -1;
            data = System.Text.Encoding.Default.GetBytes(newSerialNumber);
            for (i = 0; i < 16; i++) {
                if (baReadTmp[i] != '\0') {
                    if (data[i] != baReadTmp[i]) {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("設定 serial number 失敗!! 讀(" +
                            sRead + ") != 寫(" + newSerialNumber + ")\n!!請重新記錄!!");
                        return -1;
                    }
                }
            }

            return 0;
        }

        private int _StoreBiasConfig()
        {
            byte[] data = { 0x55 };

            if (i2cWriteCB(84, 170, 1, data) < 0)
                return -1;

            return 0;
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbOperator.Text.Length == 0) {
                MessageBox.Show("請輸入工號!!");
                return;
            }
            logModeSelect = cbLogMode.SelectedItem.ToString();

            if ((logModeSelect == "BeforeBurnIn") || (logModeSelect == "Log"))
            {
                if (lastLogFileName.Length == 0)
                {
                    MessageBox.Show("請輸入批號&子批號!!");
                    return;
                }
                if (tbSerialNumber.Text.Length < 1)
                {
                    MessageBox.Show("請輸入序號!!");
                    return;
                }
                if (tbSerialNumber.Text.Length > 4)
                {
                    saTmp = tbSerialNumber.Text.Split('-');

                    if ((lastLogFileName.Length != 0) && !tbLotNumber.Text.Equals(saTmp[0]) && (dtValue.Rows.Count > 0))
                        bSaveFile_Click(sender, e);

                    tbLotNumber.Text = saTmp[0];
                    tbLotNumber.Update();

                    if (saTmp.Length >= 2)
                    {
                        if (!tbSubLotNumber.Text.Equals(saTmp[1]))
                        {
                            if (int.TryParse(saTmp[1], out iTmp))
                                tbSubLotNumber.Text = iTmp.ToString("000");
                            else
                                tbSubLotNumber.Text = saTmp[1];
                            tbSubLotNumber.Update();
                        }
                        _OpenLogFile();
                    }

                    if (saTmp.Length >= 3)
                    {
                        int.TryParse(saTmp[2], out iTmp);
                        tbSerialNumber.Text = iTmp.ToString("0000");
                    }
                }
            }
            else if (logModeSelect == "B-HDMI(QC)")
            {
                if (tbSerialNumber.Text.Length < 1) {
                    MessageBox.Show("請輸入序號!!");
                    return;
                }
                if (tbSerialNumber.Text.Length == 9) {
                    if ((lastLogFileName.Length != 0) &&
                        (!tbLotNumber.Text.Substring(2, 2).Equals(tbSerialNumber.Text.Substring(0, 2)) ||
                        (!tbLotNumber.Text.Substring(5, 3).Equals(tbSerialNumber.Text.Substring(2, 3)))) &&
                        (dtValue.Rows.Count > 0)) 
                        bSaveFile_Click(sender, e);

                    tbLotNumber.Text = "YC" + tbSerialNumber.Text.Substring(0, 2) + "0" + tbSerialNumber.Text.Substring(2, 3);
                    tbLotNumber.Update();
                    tbSubLotNumber.Text = "QC";
                    tbSubLotNumber.Update();
                    _OpenLogFile();
                    
                    tbSerialNumber.Text = tbSerialNumber.Text.Substring(5, 4);
                }
            }

            if (!((tbRx1Threshold.Text.Equals("0")) && (tbRx2Threshold.Text.Equals("0")) &&
                (tbRx3Threshold.Text.Equals("0")) && (tbRx4Threshold.Text.Equals("0")))) {
                if ((tbRx1.Text == "0") && (tbRx2.Text == "0") && (tbRx3.Text == "0") && (tbRx4.Text == "0")) {
                    DialogResult drRxZero = MessageBox.Show("Rx無讀值異常, 請檢查待測物!!\n(或按No忽略)", "請選擇Yes或No",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drRxZero == DialogResult.Yes)
                        return;
                }
            }

            bLog.Enabled = false;
            lAction.Text = "Start log...";
            lResult.Text = "";
            lClassification.Text = "";
            lClassification.BackColor = System.Drawing.SystemColors.Control;

            if (logModeSelect == "BeforeBurnIn") {
                while (tbSerialNumber.Text[0] == ' ')
                    tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);
                if (tbLotNumber.Text.Length == 8)
                    newSerialNumber = tbLotNumber.Text + tbSubLotNumber.Text + tbSerialNumber.Text;
                else {
                    lAction.Text = "LOT number wrong!!";
                    MessageBox.Show("LOT編號錯誤!!");
                    goto Error;
                }
            }
            else if (logModeSelect == "AfterBurnIn") {
                if (tbModuleSerialNumber.Text.Length >= 15) {
                    if ((tbLotNumber.Text != tbModuleSerialNumber.Text.Substring(0, 8)) ||
                        (tbSubLotNumber.Text != tbModuleSerialNumber.Text.Substring(8, 3))) {
                        if (dtValue.Rows.Count > 0)
                            _SaveLogFile();
                        tbLotNumber.Text = tbModuleSerialNumber.Text.Substring(0, 8);
                        tbSubLotNumber.Text = tbModuleSerialNumber.Text.Substring(8, 3);
                        _OpenLogFile();
                    }
                    int.TryParse(tbModuleSerialNumber.Text.Substring(11, 4), out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
            }
            cbAutoLogWithLableTemperature.Enabled = false;
            cbAutoLogWithLableTemperature.Checked = false;

            logValue = true;

            return;

        Error:
            bLog.Enabled = true;
            lAction.Text = "";
            return;
        }

        private int _ReadTemperatureValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fTmp;
            int devAddr;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadCB((byte)devAddr, 22, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = fTmp / 256;
            temperature = fTmp.ToString();

            return 0;

        clearData:
            temperature = "0";

            return 0;
        }

        private int _ReadTxBiasValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fBias;
            int devAddr;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadCB((byte)devAddr, 42, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[0] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[1] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[2] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[3] = fBias.ToString();

            return 0;

        clearData:
            txBiasValue[0] = txBiasValue[1] = txBiasValue[2] = txBiasValue[3] = "0";

            return 0;
        }

        private int _ReadRxRssiValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);
            int.TryParse(tbI2cRxRegisterPage.Text, out page);
            int.TryParse(tbI2cRxRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (i2cReadCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[0] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[1] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[2] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[3] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "0";

            return 0;
        }

        private int _ReadRxPowerValue()
        {
            byte[] data = data = new byte[] { 4, 0, 0, 0 };
            byte[] reverseData;
            int tmp;
            float power;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadCB(80, 34, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxPowerValue[0] = power.ToString("#0.0");

            if (i2cReadCB(80, 36, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxPowerValue[1] = power.ToString("#0.0");

            if (i2cReadCB(80, 38, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxPowerValue[2] = power.ToString("#0.0");

            if (i2cReadCB(80, 40, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxPowerValue[3] = power.ToString("#0.0");

            if (i2cReadCB(80, 244, 4, data) != 4)
                goto clearData;

            rxPowerRate[0] = data[0].ToString();
            rxPowerRate[1] = data[1].ToString();
            rxPowerRate[2] = data[2].ToString();
            rxPowerRate[3] = data[3].ToString();

            return 0;

        clearData:
            rxPowerValue[0] = rxPowerValue[1] = rxPowerValue[2] = rxPowerValue[3] = "0";
            rxPowerRate[0] = rxPowerRate[1] = rxPowerRate[2] = rxPowerRate[3] = "0";
            return 0;
        }

        private int _ReadMpdValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cMpdDevAddr.Text, out devAddr);
            int.TryParse(tbI2cMpdRegisterPage.Text, out page);
            int.TryParse(tbI2cMpdRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (i2cReadCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            mpdValue[0] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            mpdValue[1] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            mpdValue[2] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            mpdValue[3] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            mpdValue[0] = mpdValue[1] = mpdValue[2] = mpdValue[3] = "0";

            return 0;
        }

        private int _ReadSerialNumberValue()
        {
            byte[] data = new byte[16];
            int devAddr;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            data[0] = 5;
            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadCB((byte)devAddr, 220, 16, data) != 16)
                goto clearData;

            serialNumber = System.Text.Encoding.Default.GetString(data);

            return 0;

        clearData:
            serialNumber = "";

            return 0;
        }

        private int _ReadLosValue()
        {
            byte[] data = new byte[1];
            int devAddr;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);
            
            if (i2cReadCB((byte)devAddr, 3, 1, losStatus) != 1)
                goto clearData;

        clearData:
            return 0;
        }

        private int _ReadTxPower()
        {
            if (powerMeterReadCB == null)
                return -1;

            if (powerMeterReadCB(txPower) < 0)
                return -1;

            return 0;
        }

        private int _GetModuleMonitorValue()
        {
            bool rxValueReadError, txPowerReadError;

            rxValueReadError = txPowerReadError = false;

            if (_ReadTemperatureValue() < 0)
                rxValueReadError = true;

            if (_ReadTxBiasValue() < 0)
                rxValueReadError = true;

            if (_ReadRxRssiValue() < 0)
                rxValueReadError = true;
                
            if (_ReadRxPowerValue() < 0)
                rxValueReadError = true;

            if (_ReadMpdValue() < 0)
                rxValueReadError = true;

            if (_ReadSerialNumberValue() < 0)
                rxValueReadError = true;

            if (_ReadLosValue() < 0)
                rxValueReadError = true;

            if (_ReadTxPower() < 0) {
                if (!(txPower[0].Equals("NA") &&
                    txPower[1].Equals("NA") &&
                    txPower[2].Equals("NA") &&
                    txPower[3].Equals("NA")))
                    txPowerReadError = true;
            }

            if ((String.Compare(rxRssiValue[0], "65535") == 0) &&
                (String.Compare(rxRssiValue[1], "65535") == 0) &&
                (String.Compare(rxRssiValue[2], "65535") == 0) &&
                (String.Compare(rxRssiValue[3], "65535") == 0) &&
                (String.Compare(mpdValue[0], "65535") == 0) &&
                (String.Compare(mpdValue[1], "65535") == 0) &&
                (String.Compare(mpdValue[2], "65535") == 0) &&
                (String.Compare(mpdValue[3], "65535") == 0) &&
                (String.Compare(temperature, "0") != 0))
                _WritePassword();

            if (rxValueReadError || txPowerReadError)
                return -1;
            
            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;
            int iTmp;
            int.TryParse(tbBeforeAndAfterBurnInDcTestBiasCurrent.Text, out iTmp);

            while (monitorStart) {
                bGetModuleMonitorValueError = false;

                if (logValue == true) {
                    switch (logModeSelect) {
                        case "BeforeBurnIn":
                            bwMonitor.ReportProgress(1, null);
                      
                            if (_SetBias(iTmp) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(100); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            
                            bwMonitor.ReportProgress(4, null);
                            if ((_SetModuleSerialNumber() < 0) || (_SetBias(10000) < 0) ||
                                (_StoreBiasConfig() < 0)) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AfterBurnIn":
                            bwMonitor.ReportProgress(5, null);
                            if ((_SetBias(iTmp) < 0) || (_StoreBiasConfig() < 0)) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(100); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            acConfigRowCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterBurnInAcConfig();

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AcConfig":
                            acConfigRowCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterBurnInAcConfig();

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        default:
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);

                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;
                    }
                }
                else {
                    if (_GetModuleMonitorValue() < 0)
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

        private int _WritePassword()
        {
            byte[] data = new byte[4];

            if (i2cWriteCB == null)
                return -1;

            if ((tbPassword123.Text.Length == 0) || (tbPassword124.Text.Length == 0) ||
                (tbPassword125.Text.Length == 0) || (tbPassword126.Text.Length == 0)) {
                MessageBox.Show("Please input 4 hex value password before write!!");
                return -1;
            }

            try {
                data[0] = (byte)Convert.ToInt32(tbPassword123.Text, 10);
                data[1] = (byte)Convert.ToInt32(tbPassword124.Text, 10);
                data[2] = (byte)Convert.ToInt32(tbPassword125.Text, 10);
                data[3] = (byte)Convert.ToInt32(tbPassword126.Text, 10);
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
                return -1;
            }

            if (i2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _StorePowerRateConfig()
        {
            byte[] data = { 32 };

            lAction.Text = "Sotre Rx power rate...";
            lAction.Update();

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WritePowerRate()
        {
            byte[] data;

            if ((tbRx1PowerRate.Text.Length == 0) || (tbRx2PowerRate.Text.Length == 0) ||
                (tbRx3PowerRate.Text.Length == 0) || (tbRx4PowerRate.Text.Length == 0)) {
                MessageBox.Show("Please input Rx power rate!!");
                return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (_WritePassword() < 0)
                return -1;
            
            if (i2cWriteCB == null)
                return -1;

            data = new byte[] { 4, 0, 0, 0 }; ;
            i2cWriteCB(80, 127, 1, data);

            try {
                data[0] = Convert.ToByte(tbRx1PowerRate.Text);
                data[1] = Convert.ToByte(tbRx2PowerRate.Text);
                data[2] = Convert.ToByte(tbRx3PowerRate.Text);
                data[3] = Convert.ToByte(tbRx4PowerRate.Text);
            }
            catch (Exception eTB) {
                MessageBox.Show("Rx power rate out of range (0 ~ 255)!!\n" + eTB.ToString());
                return -1;
            }

            i2cWriteCB(80, 244, 4, data);

            _StorePowerRateConfig();

            Thread.Sleep(1000);

            return 0;
        }

        private int _SetAfterBurnInAcConfig()
        {
            byte[] data = new byte[1];
            byte devAddr, regAddr;
            int delayTime;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (_WritePassword() < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            foreach (DataRow row in dtAfterBurnInConfig.Rows) {
                acConfigRowCount++;
                switch (row[0].ToString()) {
                    case "Delay10mSec":
                        delayTime = int.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        bwMonitor.ReportProgress(6, null);
                        Thread.Sleep(delayTime);
                        break;

                    case "Write":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data[0] = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        i2cWriteCB(devAddr, regAddr, 1, data);
                        break;

                    case "Read":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        while (i2cReadCB(devAddr, regAddr, 1, data) != 1) {
                            MessageBox.Show("i2cReadCB() fail!!");
                            Thread.Sleep(100);
                        }
                        if (data[0] != byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber)) {
                            MessageBox.Show("DevAddr:0x" + devAddr.ToString("X2") + "RegAddr:0x" + regAddr.ToString("X2") +
                                "Value:0x" + data[0].ToString("X2") + " != " + row[3].ToString());
                            return -1;
                        }
                        break;

                    default:
                        break;
                }

                if (acConfigRowCount % 5 == 0) {
                    bwMonitor.ReportProgress(6, null);
                    Thread.Sleep(1);
                }
            }

            return 0;
        }

        private int _AutoCorrectRxPowerRate()
        {
            float input, rssi;
            int rate;

            lAction.Text = "Correct Rx power rate...";
            lAction.Update();

            rate = 0;

            if ((tbRx1InputPower.Text.Length == 0) || (tbRx2InputPower.Text.Length == 0) ||
                (tbRx3InputPower.Text.Length == 0) || (tbRx4InputPower.Text.Length == 0)) {
                MessageBox.Show("Input power empty!!");
                return -1;
            }

            try {
                input = Convert.ToSingle(tbRx1InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbRx1.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx1PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx2InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbRx2.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx2PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx3InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbRx3.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx3PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx4InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbRx4.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx4PowerRate.Text = rate.ToString();

            if (_WritePowerRate() < 0)
                return -1;

            return 0;
        }

        private int _UpdateTxPowerGui()
        {
            float fTmp, fThreshold;

            fTmp = fThreshold = 0;
            if (!txPower[0].Equals("NA")) {
                float.TryParse(tbTx1Threshold.Text, out fThreshold);
                float.TryParse(txPower[0], out fTmp);
                if (fTmp < fThreshold)
                    tbTx1Power.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx1Power.ForeColor = SystemColors.ControlText;
            }
            tbTx1Power.Text = txPower[0];
            tbTx1Power.Update();

            if (!txPower[1].Equals("NA")) {
                float.TryParse(tbTx2Threshold.Text, out fThreshold);
                float.TryParse(txPower[1], out fTmp);
                if (fTmp < fThreshold)
                    tbTx2Power.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx2Power.ForeColor = SystemColors.ControlText;
            }
            tbTx2Power.Text = txPower[1];
            tbTx2Power.Update();

            if (!txPower[2].Equals("NA")) {
                float.TryParse(tbTx3Threshold.Text, out fThreshold);
                float.TryParse(txPower[2], out fTmp);
                if (fTmp < fThreshold)
                    tbTx3Power.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx3Power.ForeColor = SystemColors.ControlText;
            }
            tbTx3Power.Text = txPower[2];
            tbTx3Power.Update();

            if (!txPower[2].Equals("NA")) {
                float.TryParse(tbTx4Threshold.Text, out fThreshold);
                float.TryParse(txPower[3], out fTmp);
                if (fTmp < fThreshold)
                    tbTx4Power.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx4Power.ForeColor = SystemColors.ControlText;
            }
            tbTx4Power.Text = txPower[3];
            tbTx4Power.Update();

            return 0;
        }

        private int _UpdateTxBiasValueGui()
        {
            tbTxBias1.Text = txBiasValue[0];
            tbTxBias1.Update();

            tbTxBias2.Text = txBiasValue[1];
            tbTxBias2.Update();

            tbTxBias3.Text = txBiasValue[2];
            tbTxBias3.Update();

            tbTxBias4.Text = txBiasValue[3];
            tbTxBias4.Update();

            return 0;
        }

        private int _UpdateRxRssiValueGui()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRx1Threshold.Text);
            fTmp = float.Parse(rxRssiValue[0]);
            if (fTmp < fThreshold)
                tbRx1.ForeColor = System.Drawing.Color.Red;
            else
                tbRx1.ForeColor = SystemColors.ControlText;
            tbRx1.Text = rxRssiValue[0];
            tbRx1.Update();

            fThreshold = float.Parse(tbRx2Threshold.Text);
            fTmp = float.Parse(rxRssiValue[1]);
            if (fTmp < fThreshold)
                tbRx2.ForeColor = System.Drawing.Color.Red;
            else
                tbRx2.ForeColor = SystemColors.ControlText;
            tbRx2.Text = rxRssiValue[1];
            tbRx2.Update();

            fThreshold = float.Parse(tbRx3Threshold.Text);
            fTmp = float.Parse(rxRssiValue[2]);
            if (fTmp < fThreshold)
                tbRx3.ForeColor = System.Drawing.Color.Red;
            else
                tbRx3.ForeColor = SystemColors.ControlText;
            tbRx3.Text = rxRssiValue[2];
            tbRx3.Update();

            fThreshold = float.Parse(tbRx4Threshold.Text);
            fTmp = float.Parse(rxRssiValue[3]);
            if (fTmp < fThreshold)
                tbRx4.ForeColor = System.Drawing.Color.Red;
            else
                tbRx4.ForeColor = SystemColors.ControlText;
            tbRx4.Text = rxRssiValue[3];
            tbRx4.Update();

            return 0;
        }

        private int _UpdateMpdValueGui()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbMpd1Threshold.Text);
            fTmp = float.Parse(mpdValue[0]);
            if (fTmp < fThreshold)
                tbMpd1Value.ForeColor = System.Drawing.Color.Red;
            else
                tbMpd1Value.ForeColor = SystemColors.ControlText;
            tbMpd1Value.Text = mpdValue[0];
            tbMpd1Value.Update();

            fThreshold = float.Parse(tbMpd2Threshold.Text);
            fTmp = float.Parse(mpdValue[1]);
            if (fTmp < fThreshold)
                tbMpd2Value.ForeColor = System.Drawing.Color.Red;
            else
                tbMpd2Value.ForeColor = SystemColors.ControlText;
            tbMpd2Value.Text = mpdValue[1];
            tbMpd2Value.Update();

            fThreshold = float.Parse(tbMpd3Threshold.Text);
            fTmp = float.Parse(mpdValue[2]);
            if (fTmp < fThreshold)
                tbMpd3Value.ForeColor = System.Drawing.Color.Red;
            else
                tbMpd3Value.ForeColor = SystemColors.ControlText;
            tbMpd3Value.Text = mpdValue[2];
            tbMpd3Value.Update();

            fThreshold = float.Parse(tbMpd4Threshold.Text);
            fTmp = float.Parse(mpdValue[3]);
            if (fTmp < fThreshold)
                tbMpd4Value.ForeColor = System.Drawing.Color.Red;
            else
                tbMpd4Value.ForeColor = SystemColors.ControlText;
            tbMpd4Value.Text = mpdValue[3];
            tbMpd4Value.Update();

            return 0;
        }

        private int _UpdateModuleSerailNumberValueGui()
        {
            tbModuleSerialNumber.Text = serialNumber;
            tbModuleSerialNumber.Update();

            return 0;
        }

        private int _UpdateLosStatusGui()
        {
            if ((losStatus[0] & 0x01) == 0)
                cbRx1Los.Checked = false;
            else
                cbRx1Los.Checked = true;

            if ((losStatus[0] & 0x02) == 0)
                cbRx2Los.Checked = false;
            else
                cbRx2Los.Checked = true;

            if ((losStatus[0] & 0x04) == 0)
                cbRx3Los.Checked = false;
            else
                cbRx3Los.Checked = true;

            if ((losStatus[0] & 0x08) == 0)
                cbRx4Los.Checked = false;
            else
                cbRx4Los.Checked = true;

            if ((losStatus[0] & 0x10) == 0)
                cbTx1Los.Checked = false;
            else
                cbTx1Los.Checked = true;

            if ((losStatus[0] & 0x20) == 0)
                cbTx2Los.Checked = false;
            else
                cbTx2Los.Checked = true;

            if ((losStatus[0] & 0x40) == 0)
                cbTx3Los.Checked = false;
            else
                cbTx3Los.Checked = true;

            if ((losStatus[0] & 0x80) == 0)
                cbTx4Los.Checked = false;
            else
                cbTx4Los.Checked = true;

            return 0;
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            float fTmp;
            float fTemperatureRead;

            float.TryParse(tbBeforeAndAfterBurnInDcTestBiasCurrent.Text, out fTmp);
            fTmp /= 1000;

            switch (e.ProgressPercentage) {
                case 1:
                    lAction.Text = "Set bias " + fTmp.ToString() + "mA ...";
                    lAction.Update();
                    return;

                case 2:
                    lAction.Text = "Wait value stable ...";
                    lAction.Update();
                    return;

                case 3:
                    lAction.Text = "Get value ...";
                    lAction.Update();
                    break;

                case 4:
                    lAction.Text = "Set bias 10mA and store ...";
                    lAction.Update();
                    return;

                case 5:
                    lAction.Text = "Set bias " + fTmp.ToString() + "mA and store ...";
                    lAction.Update();
                    return;

                case 6:
                    lAction.Text = "Set AC Config " + acConfigRowCount + "/" + dtAfterBurnInConfig.Rows.Count + " ...";
                    lAction.Update();
                    return;

                case 10:
                    lAction.Text = "Wait log ...";
                    lAction.Update();
                    break;

                default:
                    break;
            }
            
            _UpdateTxPowerGui();
            tbTemperature.Text = temperature;
            tbTemperature.Update();
            _UpdateTxBiasValueGui();
            _UpdateRxRssiValueGui();
            _UpdateMpdValueGui();
            _UpdateModuleSerailNumberValueGui();
            _UpdateLosStatusGui();

            tbRx1Power.Text = rxPowerValue[0];
            tbRx1Power.Update();
            tbRx2Power.Text = rxPowerValue[1];
            tbRx2Power.Update();
            tbRx3Power.Text = rxPowerValue[2];
            tbRx3Power.Update();
            tbRx4Power.Text = rxPowerValue[3];
            tbRx4Power.Update();

            tbRx1PowerRate.Text = rxPowerRate[0];
            tbRx1PowerRate.Update();
            tbRx2PowerRate.Text = rxPowerRate[1];
            tbRx2PowerRate.Update();
            tbRx3PowerRate.Text = rxPowerRate[2];
            tbRx3PowerRate.Update();
            tbRx4PowerRate.Text = rxPowerRate[3];
            tbRx4PowerRate.Update();

            if ((logValue == true) && (e.ProgressPercentage == 10)) {
                switch (cbLogMode.SelectedItem.ToString()) {
                    case "AfterBurnIn":
                        _AddLogValue(true);
                        lAction.Text = "Log Added.";
                        lAction.Update();
                        break;

                    case "AcConfig":
                        lAction.Text = "AC Config Done.";
                        lAction.Update();
                        break;

                    case "BeforeBurnIn":
                    default:
                        _AddLogValue(false);
                        lAction.Text = "Log Added.";
                        lAction.Update();
                        tbSerialNumber.SelectAll();
                        break;
                }
                logValue = false;
                bLog.Enabled = true;
                cbAutoLogWithLableTemperature.Enabled = true;
            }

            if (cbAutoLogWithLableTemperature.Checked == true) {                
                float.TryParse(tbTemperature.Text, out fTemperatureRead);

                switch (tbLogLable.Text) {
                    case "70":
                        if (fTemperatureRead < 70)
                            goto error_exit;
                        break;

                    case "25":
                        if ((fTemperatureRead >= 26) || (fTemperatureRead < 25))
                            goto error_exit;
                        break;

                    case "-5":
                        if (fTemperatureRead > -5)
                            goto error_exit;
                        break;

                    default:
                        lAction.Text = "溫度觸發只支援: 70, 25, -5度!!";
                        cbAutoLogWithLableTemperature.Checked = false;
                        goto error_exit;
                }

                bLog_Click(sender, e);
                return;

            error_exit:
                return;
            }
        }

        private void _SaveLogFile()
        {
            StreamWriter swLog;

            if (lastLogFileName == "")
                return;

            swLog = new StreamWriter(tbLogFilePath.Text);
            swLog.WriteLine("Lable,Time,SN,Tx1(uW),Tx2(uW),Tx3(uW),Tx4(uW),Rx1,Rx2,Rx3,Rx4,MPD1,MPD2,MPD3,MPD4,Bias1,Bias2,Bias3,Bias4,Temperature,Operator,Note");
            foreach (DataRow row in dtValue.Rows) {
                swLog.WriteLine(row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                    row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString() + "," +
                    row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString() + "," +
                    row[9].ToString() + "," + row[10].ToString() + "," + row[11].ToString() + "," +
                    row[12].ToString() + "," + row[13].ToString() + "," + row[14].ToString() + "," +
                    row[15].ToString() + "," + row[16].ToString() + "," + row[17].ToString() + "," +
                    row[18].ToString() + "," + row[19].ToString() + "," + row[20].ToString() + "," +
                    row[21].ToString());
            }
            swLog.Close();
            tbLotNumber.Text = "";
            tbSubLotNumber.Text = "";
            lastLogFileName = "";
            tbLogFilePath.Text = fileDirectory;
            dtValue.Clear();
        }

        public int GetPassword(byte[] buffer)
        {
            buffer[0] = Convert.ToByte(tbPassword123.Text);
            buffer[1] = Convert.ToByte(tbPassword124.Text);
            buffer[2] = Convert.ToByte(tbPassword125.Text);
            buffer[3] = Convert.ToByte(tbPassword126.Text);

            return 0;
        }

        private void tbRx1InputPower_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRx1InputPower.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRx1InputPowerRssi.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRx2InputPower_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRx2InputPower.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRx2InputPowerRssi.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRx3InputPower_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRx3InputPower.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRx3InputPowerRssi.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRx4InputPower_TextChanged(object sender, EventArgs e)
        {
            float input, rssi;
            uint numerator, denominator;

            try {
                input = Convert.ToSingle(tbRx4InputPower.Text);
                numerator = Convert.ToUInt32(tbRxRssiRateNumerator.Text);
                denominator = Convert.ToUInt32(tbRxRssiRateDenominator.Text);
                rssi = input * numerator / denominator;
                tbRx4InputPowerRssi.Text = rssi.ToString("#0.0");
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
        }

        private void tbRxRssiRateNumerator_TextChanged(object sender, EventArgs e)
        {
            tbRx1InputPower_TextChanged(sender, e);
            tbRx2InputPower_TextChanged(sender, e);
            tbRx3InputPower_TextChanged(sender, e);
            tbRx4InputPower_TextChanged(sender, e);
        }

        private void tbRxRssiRateDenominator_TextChanged(object sender, EventArgs e)
        {
            tbRx1InputPower_TextChanged(sender, e);
            tbRx2InputPower_TextChanged(sender, e);
            tbRx3InputPower_TextChanged(sender, e);
            tbRx4InputPower_TextChanged(sender, e);
        }

        private void _OpenLogFile()
        {
            StreamReader srLog;
            String sLine;
            String[] saItems;

            lastLogFileName = tbLotNumber.Text + "-" + tbSubLotNumber.Text + ".csv";
            tbLogFilePath.Text = fileDirectory + "\\" + lastLogFileName;
            tbLogFilePath.Update();

            try {
                srLog = new StreamReader(tbLogFilePath.Text);
            }
            catch (FileNotFoundException e) {
                if (e != null)
                    return;
                return;
            }

            if ((sLine = srLog.ReadLine()) == null) //Header
                return;

            while ((sLine = srLog.ReadLine()) != null) { //Record
                saItems = sLine.Split(',');
                if (saItems.Length < 17)
                    continue;

                if (saItems.Length == 17) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], "", "", "", "", "", saItems[15], saItems[16]);
                }
                else if (saItems.Length == 22) {
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], saItems[20], saItems[21]);
                }
            }

            srLog.Close();
            dgvRecord.Sort(dgvRecord.Columns[1], ListSortDirection.Descending);

            bLog.Enabled = true;
        }

        private void tbLotNumber_Leave(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbLotNumber.Text.Length > 8) {
                saTmp = tbLotNumber.Text.Split('-');
                tbLotNumber.Text = saTmp[0];
                if (saTmp.Length >= 2) {
                    if (int.TryParse(saTmp[1], out iTmp))
                        tbSubLotNumber.Text = iTmp.ToString("000");
                    else
                        tbSubLotNumber.Text = saTmp[1];
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
            foreach (FileInfo file in files) {
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

            if (int.TryParse(tbSubLotNumber.Text, out iTmp))
                tbSubLotNumber.Text = iTmp.ToString("000");

            _OpenLogFile();
        }

        private void cbLogMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLogMode.SelectedItem.ToString()) {
                case "BeforeBurnIn":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "BeforeBurnIn";
                    break;

                case "AfterBurnIn":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AfterBurnIn";
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

        private void bSaveConfig_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();
            string sTmp;

            sfdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            using (XmlWriter xwConfig = XmlWriter.Create(sfdSelectFile.FileName)) {
                xwConfig.WriteStartDocument();
                xwConfig.WriteStartElement("DcTestConfig");
                {
                    xwConfig.WriteElementString("AfterBurnInPowerDifferentPercentage", tbAfterBurnInPowerDifferentLimit.Text);
                    xwConfig.WriteElementString("BeforeAndAfterBurnInDcTestBiasCurrent", tbBeforeAndAfterBurnInDcTestBiasCurrent.Text);
                    xwConfig.WriteElementString("ModulePassword123", tbPassword123.Text);
                    xwConfig.WriteElementString("ModulePassword124", tbPassword124.Text);
                    xwConfig.WriteElementString("ModulePassword125", tbPassword125.Text);
                    xwConfig.WriteElementString("ModulePassword126", tbPassword126.Text);

                    xwConfig.WriteStartElement("I2cConfig");
                    {
                        xwConfig.WriteElementString("I2cRxDevAddr", tbI2cRxDevAddr.Text);
                        xwConfig.WriteElementString("I2cRxRegisterPage", tbI2cRxRegisterPage.Text);
                        xwConfig.WriteElementString("I2cRxRegisterAddr", tbI2cRxRegisterAddr.Text);
                        xwConfig.WriteElementString("I2cMpdDevAddr", tbI2cMpdDevAddr.Text);
                        xwConfig.WriteElementString("I2cMpdRegisterPage", tbI2cMpdRegisterPage.Text);
                        xwConfig.WriteElementString("I2cMpdRegisterAddr", tbI2cMpdRegisterAddr.Text);
                    }
                    xwConfig.WriteEndElement(); //I2cConfig

                    xwConfig.WriteStartElement("MonitorThresholdConfig");
                    {
                        xwConfig.WriteElementString("Tx1Threshold", tbTx1Threshold.Text);
                        xwConfig.WriteElementString("Tx2Threshold", tbTx2Threshold.Text);
                        xwConfig.WriteElementString("Tx3Threshold", tbTx3Threshold.Text);
                        xwConfig.WriteElementString("Tx4Threshold", tbTx4Threshold.Text);
                        xwConfig.WriteElementString("LtMinThreshold", tbLtMinThreshold.Text);
                        xwConfig.WriteElementString("LtMaxThreshold", tbLtMaxThreshold.Text);
                        xwConfig.WriteElementString("HtMinThreshold", tbHtMinThreshold.Text);
                        xwConfig.WriteElementString("HtMaxThreshold", tbHtMaxThreshold.Text);
                        xwConfig.WriteElementString("Rx1Threshold", tbRx1Threshold.Text);
                        xwConfig.WriteElementString("Rx2Threshold", tbRx2Threshold.Text);
                        xwConfig.WriteElementString("Rx3Threshold", tbRx3Threshold.Text);
                        xwConfig.WriteElementString("Rx4Threshold", tbRx4Threshold.Text);
                        xwConfig.WriteElementString("Mpd1Threshold", tbMpd1Threshold.Text);
                        xwConfig.WriteElementString("Mpd2Threshold", tbMpd2Threshold.Text);
                        xwConfig.WriteElementString("Mpd3Threshold", tbMpd3Threshold.Text);
                        xwConfig.WriteElementString("Mpd4Threshold", tbMpd4Threshold.Text);
                    }
                    xwConfig.WriteEndElement(); //MonitorThresholdConfig

                    xwConfig.WriteStartElement("RxPowerRateConfig");
                    {
                        xwConfig.WriteElementString("Rx1InputPower", tbRx1InputPower.Text);
                        xwConfig.WriteElementString("Rx2InputPower", tbRx2InputPower.Text);
                        xwConfig.WriteElementString("Rx3InputPower", tbRx3InputPower.Text);
                        xwConfig.WriteElementString("Rx4InputPower", tbRx4InputPower.Text);
                        xwConfig.WriteElementString("RxRssiRateNumerator", tbRxRssiRateNumerator.Text);
                        xwConfig.WriteElementString("tbRxRssiRateDenominator", tbRxRssiRateDenominator.Text);

                    }
                    xwConfig.WriteEndElement(); //RxPowerRateConfig

                    sTmp = "";
                    foreach (DataRow row in dtAfterBurnInConfig.Rows) {
                        switch (row[0].ToString()) {
                            case "Delay10mSec":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                                break;

                            case "Write":
                            case "Read":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                                    row[3].ToString() + "\n");
                                break;

                            default:
                                break;
                        }
                        
                    }
                    xwConfig.WriteElementString("AfterBurnInAcConfig", sTmp);
                }
                xwConfig.WriteEndElement(); //DcTestConfig
                xwConfig.WriteEndDocument();
            }
        }

        private void _PaserI2cConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "I2cRxDevAddr":
                            tbI2cRxDevAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cRxRegisterPage":
                            tbI2cRxRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cRxRegisterAddr":
                            tbI2cRxRegisterAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cMpdDevAddr":
                            tbI2cMpdDevAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cMpdRegisterPage":
                            tbI2cMpdRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cMpdRegisterAddr":
                            tbI2cMpdRegisterAddr.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            return;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserMonitorThresholdConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "Tx1Threshold":
                            tbTx1Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Tx2Threshold":
                            tbTx2Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Tx3Threshold":
                            tbTx3Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Tx4Threshold":
                            tbTx4Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "LtMinThreshold":
                            tbLtMinThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "LtMaxThreshold":
                            tbLtMaxThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "HtMinThreshold":
                            tbHtMinThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "HtMaxThreshold":
                            tbHtMaxThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx1Threshold":
                            tbRx1Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2Threshold":
                            tbRx2Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3Threshold":
                            tbRx3Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4Threshold":
                            tbRx4Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Mpd1Threshold":
                            tbMpd1Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Mpd2Threshold":
                            tbMpd2Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Mpd3Threshold":
                            tbMpd3Threshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "Mpd4Threshold":
                            tbMpd4Threshold.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            return;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserRxPowerRateConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "Rx1InputPower":
                            tbRx1InputPower.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2InputPower":
                            tbRx2InputPower.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3InputPower":
                            tbRx3InputPower.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4InputPower":
                            tbRx4InputPower.Text = reader.ReadElementContentAsString();
                            break;

                        case "RxRssiRateNumerator":
                            tbRxRssiRateNumerator.Text = reader.ReadElementContentAsString();
                            break;

                        case "tbRxRssiRateDenominator":
                            tbRxRssiRateDenominator.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            return;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserAfterBurnInAcConfig(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null) {
                saItems = line.Split(',');
                switch (saItems[0]) {
                    case "Delay10mSec":
                        dtAfterBurnInConfig.Rows.Add(saItems[0], saItems[1]);
                        break;

                    case "Write":
                    case "Read":
                        dtAfterBurnInConfig.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3]);
                        break;

                    default:
                        break;
                }
            }
        }

        private void _PaserDcTestConfigXml(XmlReader reader)
        {
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "AfterBurnInPowerDifferentPercentage":
                            tbAfterBurnInPowerDifferentLimit.Text = reader.ReadElementContentAsString();
                            break;

                        case "BeforeAndAfterBurnInDcTestBiasCurrent":
                            tbBeforeAndAfterBurnInDcTestBiasCurrent.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword123":
                            tbPassword123.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword124":
                            tbPassword124.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword125":
                            tbPassword125.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword126":
                            tbPassword126.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cConfig":
                            _PaserI2cConfigXml(reader);
                            reader.Read();
                            break;

                        case "MonitorThresholdConfig":
                            _PaserMonitorThresholdConfigXml(reader);
                            reader.Read();
                            break;

                        case "RxPowerRateConfig":
                            _PaserRxPowerRateConfigXml(reader);
                            reader.Read();
                            break;

                        case "AfterBurnInAcConfig":
                            _PaserAfterBurnInAcConfig(reader.ReadElementContentAsString());
                            break;

                        default:
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
            dtAfterBurnInConfig.Clear();

            using (XmlReader xrConfig = XmlReader.Create(ofdSelectFile.FileName)) {
                while (xrConfig.Read()) {
                    if (xrConfig.IsStartElement()) {
                        switch (xrConfig.Name) {
                            case "DcTestConfig":
                                xrConfig.Read();
                                _PaserDcTestConfigXml(xrConfig);
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
                tbSerialNumber.Text = (++iSerialNumber).ToString("0000");
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
                tbSerialNumber.Text = (--iSerialNumber).ToString("0000");
            tbSerialNumber.Select(0, 0);

            return 0;
        }

        public int SetFocusOnLogFilePathApi()
        {
            tbLogFilePath.Focus();
            return 0;
        }

        private void bDelRecord_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvRecord.SelectedRows) {
                dgvRecord.Rows.RemoveAt(item.Index);
            }
        }

        private void tbSerialNumber_Enter(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";
        }

        private void tbSerialNumberNumber_Leave(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbSerialNumber.Text.Length > 4) {
                saTmp = tbSerialNumber.Text.Split('-');

                if ((lastLogFileName.Length != 0) && !tbLotNumber.Text.Equals(saTmp[0]) && (dtValue.Rows.Count > 0))
                    bSaveFile_Click(sender, e);

                tbLotNumber.Text = saTmp[0];
                tbLotNumber.Update();

                if (saTmp.Length >= 2) {
                    if (!tbSubLotNumber.Text.Equals(saTmp[1])) {
                        if (int.TryParse(saTmp[1], out iTmp))
                            tbSubLotNumber.Text = iTmp.ToString("000");
                        else
                            tbSubLotNumber.Text = saTmp[1];
                        tbSubLotNumber.Update();
                    }
                    _OpenLogFile();
                }

                if (saTmp.Length >= 3) {
                    int.TryParse(saTmp[2], out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
            }
            else {
                int.TryParse(tbSerialNumber.Text, out iTmp);
                tbSerialNumber.Text = iTmp.ToString("0000");
            }
        }

        private void tbSerialNumber_TextChanged(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";
        }
    }
}
