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
        private DataTable dtBeforeTestConfig = new DataTable();
        private DataTable dtAfterTestConfig = new DataTable();
        private String fileDirectory = "D:\\DcTestLog";
        private String remoteFileDirectory = "\\\\nova\\vl1\\Public\\康博 SAS4 AOC Assembly\\DC Serial Number";
        private String lastLogFileName = "";
        private uint fileRecordNumber = 0;

        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int PowerMeterReadCB(String[] data);
        public delegate int WritePasswordCB();
        public delegate int SetPowerSupplyCB(Boolean powerOn);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private PowerMeterReadCB powerMeterReadCB = null;
        private SetPowerSupplyCB setPowerSupplyCB = null;

        private BackgroundWorker bwMonitor;
        private DialogResult drAskOverwrite;
        private volatile String[] txPower = new String[4];
        private volatile String[] mpdValue = new String[4];
        private volatile String[] rxRssiValue = new String[4];
        private volatile String[] rxPowerRate = new String[4];
        private volatile String[] rxPowerValue = new String[4];
        private volatile String[] txBiasValue = new String[4];
        private volatile String temperature;
        private volatile String voltage;
        private volatile String logModeSelect;
        private volatile String moduleSerialNumber, newModuleSerialNumber, customerSerialNumber;
        private volatile String lastNote;
        private volatile String messageBoxError = "";
        private volatile int statusNotifyCount;
        private volatile bool monitorStart = false;
        private volatile bool logValue = false;
        private volatile byte[] losStatus = new byte[1];
        private volatile byte[] txFault = new byte[1];
        private volatile int customerPage_selectIndex = 0;
        

        public UcQsfpPlus40gSr4DcTest()
        {
            int i;

            InitializeComponent();

            txPower[0] = txPower[1] = txPower[2] = txPower[3] = "0";
            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "0";
            mpdValue[0] = mpdValue[1] = mpdValue[2] = mpdValue[3] = "0";
            txBiasValue[0] = txBiasValue[1] = txBiasValue[2] = txBiasValue[3] = "0";
            losStatus[0] = 0;
            temperature = "0";
            voltage = "0";
            lastNote = moduleSerialNumber= "";

            Directory.CreateDirectory(fileDirectory);
            tbLogFilePath.Text = fileDirectory + "\\";

            dtValue.Columns.Add("Lable", typeof(String));
            dtValue.Columns.Add("Time", typeof(String));
            dtValue.Columns.Add("SN", typeof(String));
            dtValue.Columns.Add("CSN", typeof(String));

            dtValue.Columns.Add("Tx1", typeof(String));
            dtValue.Columns.Add("Tx2", typeof(String));
            dtValue.Columns.Add("Tx3", typeof(String));
            dtValue.Columns.Add("Tx4", typeof(String));

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

            dtValue.Columns.Add("Temp", typeof(String));
            dtValue.Columns.Add("Vcc", typeof(String));

            dtValue.Columns.Add("Operator", typeof(String));
            dtValue.Columns.Add("Grade", typeof(String));
            dtValue.Columns.Add("Note", typeof(String));

            dgvRecord.DataSource = dtValue;

            dtBeforeTestConfig.Columns.Add("Command", typeof(String));
            dtBeforeTestConfig.Columns.Add("P001", typeof(String));
            dtBeforeTestConfig.Columns.Add("P002", typeof(String));
            dtBeforeTestConfig.Columns.Add("P003", typeof(String));
            for (i = 0; i < 128; i++)
                dtBeforeTestConfig.Columns.Add("P" + (i + 4).ToString("D3"), typeof(String));

            dgvBeforeTestConfig.DataSource = dtBeforeTestConfig;

            dtAfterTestConfig.Columns.Add("Command", typeof(String));
            dtAfterTestConfig.Columns.Add("P001", typeof(String));
            dtAfterTestConfig.Columns.Add("P002", typeof(String));
            dtAfterTestConfig.Columns.Add("P003", typeof(String));
            for (i = 0; i < 128; i++)
                dtAfterTestConfig.Columns.Add("P" + (i + 4).ToString("D3"), typeof(String));

            dgvAfterTestConfig.DataSource = dtAfterTestConfig;

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);
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

        public int SetPowerSupplyCBApi(SetPowerSupplyCB cb)
        {
            if (cb == null)
                return -1;

            setPowerSupplyCB = new SetPowerSupplyCB(cb);

            return 0;
        }

        public Boolean GetPowerSupplyControlReverseApi()
        {
            return cbPowerSupplyControlReverse.Checked;
        }
        

        private int _CheckVoltageThreshold(String voltage)
        {
            float fMin, fMax, fVoltage;
            bool bPass = true;

            fMin = float.Parse(tbVoltageMinThreshold.Text);
            fMax = float.Parse(tbVoltageMaxThreshold.Text);
            fVoltage = float.Parse(voltage);

            if (fVoltage > fMax) {
                lastNote += "Voltage: " + voltage + " > " + tbVoltageMaxThreshold.Text;
                bPass = false;
            }
            else if (fVoltage < fMin) {
                lastNote += "Voltage: " + voltage + " < " + tbVoltageMinThreshold.Text;
                bPass = false;
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                lResult.Text = "NG (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.Red;
                lClassification.BackColor = System.Drawing.Color.White;
                lClassification.Text = "T";
            }

            return 0;
        }

        private int _CheckAfterBurnInPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] oldTxPower = new String[4];
            DataRow[] filteredRows;
            float fMin, fMax, fLimit;
            int i;
            bool bPass = true;
            bool bReBurnIn = false;

            switch (logModeSelect) {
                case "AfterBurnIn":
                    filteredRows = dtValue.Select("Lable = 'BeforeBurnIn' AND SN = '" + serialNumber + "'");
                    break;

                case "AocAfterGlue":
                    filteredRows = dtValue.Select("Lable = 'AocBeforeGlue' AND CSN = '" + serialNumber + "'");
                    break;

                default:
                    MessageBox.Show("Cannot find relation logModeSelect: " + logModeSelect + " !!");
                    return -1;
            }

            if (filteredRows.Length == 0) {
                MessageBox.Show("Cannot find: " + serialNumber + " before burn-in data!!");
                return -1;
            }

            oldTxPower[0] = filteredRows.ElementAt(0)["Tx1"].ToString();
            oldTxPower[1] = filteredRows.ElementAt(0)["Tx2"].ToString();
            oldTxPower[2] = filteredRows.ElementAt(0)["Tx3"].ToString();
            oldTxPower[3] = filteredRows.ElementAt(0)["Tx4"].ToString();

            

            if (oldTxPower[0].Equals("NA") || oldTxPower[1].Equals("NA") || oldTxPower[2].Equals("NA") ||
                oldTxPower[3].Equals("NA")) {
                bPass = false;
                for (i = 0; i < 4; i++)
                    lastNote += "Tx" + (i + 1).ToString() + "(B:" + oldTxPower[i] + " A:" + txPower[i] + "); ";
            }
            else {
                fLimit = float.Parse(tbAfterBurnInPowerDifferentLimit.Text) / 100;
                for (i = 0; i < 4; i++) {
                    fMin = (float)(float.Parse(oldTxPower[i]) * (1 - fLimit));
                    fMax = (float)(float.Parse(oldTxPower[i]) * (1 + fLimit));
                    if ((float.Parse(txPower[i]) < fMin) || (float.Parse(txPower[i]) > fMax)) {
                        bPass = false;
                        lastNote += "Tx" + (i + 1).ToString() + "(B:" + oldTxPower[i] + " A:" + txPower[i] + "); ";
                    }
                }

                if ((bPass == true) && (logModeSelect == "AfterBurnIn")) {
                    fLimit = float.Parse(tbAfterBurnInReBIPowerDifferentLimit.Text) / 100;
                    for (i = 0; i < 4; i++) {
                        fMin = (float)(float.Parse(oldTxPower[i]) * (1 - fLimit));
                        fMax = (float)(float.Parse(oldTxPower[i]) * (1 + fLimit));
                        if ((float.Parse(txPower[i]) < fMin) || (float.Parse(txPower[i]) > fMax)) {
                            bPass = false;
                            bReBurnIn = true;
                            lastNote += "Tx" + (i + 1).ToString() + "(B:" + oldTxPower[i] + " A:" + txPower[i] + "); ";
                        }
                    }
                }
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                if (logModeSelect == "AocAfterGlue")
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                if (bReBurnIn == true) {
                    lResult.ForeColor = System.Drawing.Color.OrangeRed;
                    lResult.Text = "Re-BurnIn (" + serialNumber + ")";
                    lClassification.ForeColor = System.Drawing.Color.OrangeRed;
                    lClassification.BackColor = System.Drawing.Color.Yellow;
                    lClassification.Text = "R";
                }
                else {
                    lResult.ForeColor = System.Drawing.Color.Red;
                    if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                        lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                    else
                        lResult.Text = "NG (" + serialNumber + ")";
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckTxPowerThreshold(String[] txPower)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

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
                if ((logModeSelect == "AocBeforeGlue")  || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + moduleSerialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckRxValueThreshold(String[] rxValue)
        {
            float fTmp, fThresholdMin, fThresholdMax;
            bool bPass = true;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

            try {
                fThresholdMin = float.Parse(tbRx1ThresholdMin.Text);
                fThresholdMax = float.Parse(tbRx1ThresholdMax.Text);
                fTmp = float.Parse(rxValue[0]);
                if (fThresholdMin > 0) {
                    if (fTmp < fThresholdMin) {
                        lastNote += "Rx1 value (" + rxValue[0] + ") < Threshold (" + tbRx1ThresholdMin.Text + "); ";
                        bPass = false;
                    }
                }
                if (fThresholdMax > 0) {
                    if (fTmp > fThresholdMax) {
                        lastNote += "Rx1 value (" + rxValue[0] + ") > Threshold (" + tbRx1ThresholdMax.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThresholdMin = float.Parse(tbRx2ThresholdMin.Text);
                fThresholdMax = float.Parse(tbRx2ThresholdMax.Text);
                fTmp = float.Parse(rxValue[1]);
                if (fThresholdMin > 0) {
                    if (fTmp < fThresholdMin) {
                        lastNote += "Rx2 value (" + rxValue[1] + ") < Threshold (" + tbRx2ThresholdMin.Text + "); ";
                        bPass = false;
                    }
                }
                if (fThresholdMax > 0) {
                    if (fTmp > fThresholdMax) {
                        lastNote += "Rx2 value (" + rxValue[1] + ") > Threshold (" + tbRx2ThresholdMax.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThresholdMin = float.Parse(tbRx3ThresholdMin.Text);
                fThresholdMax = float.Parse(tbRx3ThresholdMax.Text);
                fTmp = float.Parse(rxValue[2]);
                if (fThresholdMin > 0) {
                    if (fTmp < fThresholdMin) {
                        lastNote += "Rx3 value (" + rxValue[2] + ") < Threshold (" + tbRx3ThresholdMin.Text + "); ";
                        bPass = false;
                    }
                }
                if (fThresholdMin > 0) {
                    if (fTmp > fThresholdMax) {
                        lastNote += "Rx3 value (" + rxValue[2] + ") > Threshold (" + tbRx3ThresholdMax.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            try {
                fThresholdMin = float.Parse(tbRx4ThresholdMin.Text);
                fThresholdMax = float.Parse(tbRx4ThresholdMax.Text);
                fTmp = float.Parse(rxValue[3]);
                if (fThresholdMin > 0) {
                    if (fTmp < fThresholdMin) {
                        lastNote += "Rx4 value (" + rxValue[3] + ") < Threshold (" + tbRx4ThresholdMin.Text + "); ";
                        bPass = false;
                    }
                }
                if (fThresholdMin > 0) {
                    if (fTmp > fThresholdMax) {
                        lastNote += "Rx4 value (" + rxValue[3] + ") > Threshold (" + tbRx4ThresholdMax.Text + "); ";
                        bPass = false;
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("float.Parse() Error!!\n" + e.ToString());
                return -1;
            }

            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + moduleSerialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckMpdValueThreshold(String[] mpdValue)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (!lClassification.Text.Equals("A"))
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
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + moduleSerialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckLosStatus()
        {
            bool bPass = true;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

            if (cbIgnoreRxLos.Checked == false) {
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
            }

            if (cbIgnoreTxLos.Checked == false) {
                if ((losStatus[0] & 0x10) != 0) {
                    bPass = false;
                    lastNote += "Tx1 LOS; ";
                }

                if ((losStatus[0] & 0x20) != 0) {
                    bPass = false;
                    lastNote += "Tx2 LOS; ";
                }

                if ((losStatus[0] & 0x40) != 0) {
                    bPass = false;
                    lastNote += "Tx3 LOS; ";
                }

                if ((losStatus[0] & 0x80) != 0) {
                    bPass = false;
                    lastNote += "Tx4 LOS; ";
                }
            }

        
            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + moduleSerialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckTxFaultStatus()
        {
            bool bPass = true;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

            if (cbIgnoreTxFault.Checked == true)
                goto exit;

            switch (cbTxFaultBit.SelectedText) {
                case "N,N,N,N,4,3,2,1":
                    if ((txFault[0] & 0x01) != 0) {
                        bPass = false;
                        lastNote += "Tx1 Fault; ";
                    }

                    if ((txFault[0] & 0x02) != 0) {
                        bPass = false;
                        lastNote += "Tx2 Fault; ";
                    }

                    if ((txFault[0] & 0x04) != 0) {
                        bPass = false;
                        lastNote += "Tx3 Fault; ";
                    }

                    if ((txFault[0] & 0x08) != 0) {
                        bPass = false;
                        lastNote += "Tx4 Fault; ";
                    }
                    break;

                case "4,3,2,1,N,N,N,N":
                    if ((txFault[0] & 0x10) != 0) {
                        bPass = false;
                        lastNote += "Tx1 Fault; ";
                    }

                    if ((txFault[0] & 0x20) != 0) {
                        bPass = false;
                        lastNote += "Tx2 Fault; ";
                    }

                    if ((txFault[0] & 0x40) != 0) {
                        bPass = false;
                        lastNote += "Tx3 Fault; ";
                    }

                    if ((txFault[0] & 0x80) != 0) {
                        bPass = false;
                        lastNote += "Tx4 Fault; ";
                    }
                    break;

                default:
                    break;
            }

        exit:
            if (bPass == true) {
                lResult.ForeColor = System.Drawing.SystemColors.ControlText;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + moduleSerialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + moduleSerialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckLtPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] ltTxPower = new String[4];
            DataRow[] filteredRows = dtValue.Select("Lable = '-5' AND SN = '" + serialNumber + "'");
            double dLtPower, dRtPower, dMinThreshold, dMaxThreshold;
            int i;
            bool bPass = true;

            if ((tbLtMinThreshold.Text.Length == 0) &&
                (tbLtMaxThreshold.Text.Length == 0))
                return 1;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

            if (filteredRows.Length == 0) {
                MessageBox.Show("Cannot find: " + serialNumber + " -5 DegC Data!!");
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
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + serialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckHtPowerDifferent(String serialNumber, String[] txPower)
        {
            String[] htTxPower = new String[4];
            DataRow[] filteredRows = dtValue.Select("Lable = '70' AND SN = '" + serialNumber + "'");
            double dHtPower, dRtPower, dMinThreshold, dMaxThreshold;
            int i;
            bool bPass = true;

            if ((tbHtMinThreshold.Text.Length == 0) &&
                (tbHtMaxThreshold.Text.Length == 0))
                return 1;

            if (!lClassification.Text.Equals("A"))
                bPass = false;

            if (filteredRows.Length == 0) {
                MessageBox.Show("Cannot find: " + serialNumber + " 70 DegC data!!");
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
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "OK (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "OK (" + serialNumber + ")";
                lClassification.ForeColor = System.Drawing.Color.White;
                lClassification.BackColor = System.Drawing.Color.Green;
                lClassification.Text = "A";
            }
            else {
                lResult.ForeColor = System.Drawing.Color.Red;
                if ((logModeSelect == "AocBeforeGlue") || (logModeSelect == "AocAfterGlue"))
                    lResult.Text = "NG (" + tbSerialNumber.Text + ")";
                else
                    lResult.Text = "NG (" + serialNumber + ")";
                if (!lClassification.Text.Equals("R")) {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }
            }

            return 0;
        }

        private int _CheckDuplicationLog(String logLable, String serialNumber)
        {
            DataRow[] filteredRows;

            if ((logLable == "AocBeforeGlue") || (logLable == "AocAfterGlue")) {
                filteredRows = dtValue.Select("Lable = '" + logLable + "' AND CSN = '" + serialNumber + "'");
            }
            else {
                filteredRows = dtValue.Select("Lable = '" + logLable + "' AND SN = '" + serialNumber + "'");
            }

            if (filteredRows.Length == 0)
                return 0;

            drAskOverwrite = MessageBox.Show("Overwrite record?", "Find duplicate data", MessageBoxButtons.YesNo);
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
            String sTemperature, sVoltage;

            while (tbSerialNumber.Text[0] == ' ')
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);

            if (tbSerialNumber.Text.IndexOf(' ') > 0)
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(0, tbSerialNumber.Text.IndexOf(' '));

            if (_CheckDuplicationLog(tbLogLable.Text, tbSerialNumber.Text) < 0)
                return;

            saTxPower[0] = tbTx1PowerLog.Text;
            saTxPower[1] = tbTx2PowerLog.Text;
            saTxPower[2] = tbTx3PowerLog.Text;
            saTxPower[3] = tbTx4PowerLog.Text;
            saRxValue[0] = tbRx1Log.Text;
            saRxValue[1] = tbRx2Log.Text;
            saRxValue[2] = tbRx3Log.Text;
            saRxValue[3] = tbRx4Log.Text;
            saMpdValue[0] = tbMpd1Value.Text;
            saMpdValue[1] = tbMpd2Value.Text;
            saMpdValue[2] = tbMpd3Value.Text;
            saMpdValue[3] = tbMpd4Value.Text;
            saTxBiasValue[0] = tbTxBias1.Text;
            saTxBiasValue[1] = tbTxBias2.Text;
            saTxBiasValue[2] = tbTxBias3.Text;
            saTxBiasValue[3] = tbTxBias4.Text;
            sTemperature = tbTemperature.Text;
            sVoltage = tbVoltage.Text;
            lClassification.Text = "A";

            if (checkPowerDiff == true) {
                _CheckVoltageThreshold(sVoltage);
                _CheckAfterBurnInPowerDifferent(tbSerialNumber.Text, saTxPower);
            }

            _CheckTxPowerThreshold(saTxPower);
            _CheckRxValueThreshold(saRxValue);
            _CheckMpdValueThreshold(saMpdValue);
            _CheckLosStatus();
            _CheckTxFaultStatus();

            if ((cbAutoLogWithLableTemperature.Enabled == false) && (tbLogLable.Text.Equals("25"))) {
                _CheckLtPowerDifferent(tbSerialNumber.Text, saTxPower);
                _CheckHtPowerDifferent(tbSerialNumber.Text, saTxPower);
            }

            if ((cbLogMode.SelectedItem.ToString() == "AocBeforeGlue") || (cbLogMode.SelectedItem.ToString() == "AocAfterGlue")) {
                dtValue.Rows.Add(tbLogLable.Text, System.DateTime.Now.ToString("yy/MM/dd_HH:mm:ss"), tbModuleSerialNumber.Text,
                    tbCableSerialNumber.Text, saTxPower[0], saTxPower[1], saTxPower[2], saTxPower[3], saRxValue[0],
                    saRxValue[1], saRxValue[2], saRxValue[3], saMpdValue[0], saMpdValue[1], saMpdValue[2], saMpdValue[3],
                    saTxBiasValue[0], saTxBiasValue[1], saTxBiasValue[2], saTxBiasValue[3], sTemperature, sVoltage,
                    tbOperator.Text, lClassification.Text, lastNote);
            }
            else {
                dtValue.Rows.Add(tbLogLable.Text, System.DateTime.Now.ToString("yy/MM/dd_HH:mm:ss"), tbSerialNumber.Text,
                    tbCableSerialNumber.Text, saTxPower[0], saTxPower[1], saTxPower[2], saTxPower[3], saRxValue[0],
                    saRxValue[1], saRxValue[2], saRxValue[3], saMpdValue[0], saMpdValue[1], saMpdValue[2], saMpdValue[3],
                    saTxBiasValue[0], saTxBiasValue[1], saTxBiasValue[2], saTxBiasValue[3], sTemperature, sVoltage,
                    tbOperator.Text, lClassification.Text, lastNote);
            }
            if ((logModeSelect != "AfterBurnIn") ||
                (logModeSelect != "AocAfterGlue"))
                dgvRecord.FirstDisplayedScrollingRowIndex = 0;

            lClassification.Update();
            lastNote = "";
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[1];

            if (customerPage_selectIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _SetBias(int uA, bool burnIn)
        {
            byte[] data = new byte[1];
            byte[] baWritedata = new byte[22];
            byte[] baReadData = new byte[22];
            byte[] bATmp;

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (customerPage_selectIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            baWritedata[0] = Convert.ToByte(4 / 0.04); //Min
            baWritedata[1] = Convert.ToByte(10.2 / 0.04); //Max

            baWritedata[2] = (byte)Convert.ToSByte(0 * 100); //Ch1 EqA
            bATmp = BitConverter.GetBytes(Convert.ToInt16(0 * 10)); //Ch1 EqA
            baWritedata[3] = bATmp[1];
            baWritedata[4] = bATmp[0];
            bATmp = BitConverter.GetBytes(Convert.ToUInt16(uA)); //Ch1 EqC
            baWritedata[5] = bATmp[1];
            baWritedata[6] = bATmp[0];

            baWritedata[7] = (byte)Convert.ToSByte(0 * 100); //Ch2 EqA
            bATmp = BitConverter.GetBytes(Convert.ToInt16(0 * 10)); //Ch2 EqA
            baWritedata[8] = bATmp[1];
            baWritedata[9] = bATmp[0];
            bATmp = BitConverter.GetBytes(Convert.ToUInt16(uA)); //Ch2 EqC
            baWritedata[10] = bATmp[1];
            baWritedata[11] = bATmp[0];

            baWritedata[12] = (byte)Convert.ToSByte(0 * 100); //Ch3 EqA
            bATmp = BitConverter.GetBytes(Convert.ToInt16(0 * 10)); //Ch3 EqA
            baWritedata[13] = bATmp[1];
            baWritedata[14] = bATmp[0];
            bATmp = BitConverter.GetBytes(Convert.ToUInt16(uA)); //Ch3 EqC
            baWritedata[15] = bATmp[1];
            baWritedata[16] = bATmp[0];

            baWritedata[17] = (byte)Convert.ToSByte(0 * 100); //Ch4 EqA
            bATmp = BitConverter.GetBytes(Convert.ToInt16(0 * 10)); //Ch4 EqA
            baWritedata[18] = bATmp[1];
            baWritedata[19] = bATmp[0];
            bATmp = BitConverter.GetBytes(Convert.ToUInt16(uA)); //Ch4 EqC
            baWritedata[20] = bATmp[1];
            baWritedata[21] = bATmp[0];

            if (i2cWriteCB(80, 128, 22, baWritedata) < 0)
                return -1;

            //Write enable temperature compensate
            data[0] = 1;
            if (burnIn == true) //enable burn-in
                data[0] |= 0x02;
            if (i2cWriteCB(80, 252, 1, data) < 0)
                return -1;

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            //Write 223 to enable temperature compensate
            data[0] = 0;
            if (i2cWriteCB(80, 223, 1, data) < 0)
                return -1;

            if (customerPage_selectIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(100); //Wait change page

            if (i2cReadCB(80, 128, 22, baReadData) != 22)
                return -1;

            if (baReadData[0] != baWritedata[0])
                MessageBox.Show("Set Max bias Fail!! Read(" + baReadData[0].ToString("X2") +
                    ") != Write(" + baWritedata[0].ToString("X2") + ")\n!!Please re-log!!");

            if (baReadData[1] != baWritedata[1])
                MessageBox.Show("Set Min bias fail!! Read(" + baReadData[1].ToString("X2") +
                    ") != Write(" + baWritedata[1].ToString("X2") + ")\n!!Please re-log!!");

            if (baReadData[2] != baWritedata[2])
                MessageBox.Show("Set Ch1 EqA fail!! Read(" + baReadData[2].ToString("X2") +
                    ") != Write(" + baWritedata[2].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[3] != baWritedata[3]) || (baReadData[4] != baWritedata[4]))
                MessageBox.Show("Set Ch1 EqB fail!! Read(" + baReadData[3].ToString("X2") +
                    baReadData[4].ToString("X2") + ") != Write(" + baWritedata[3].ToString("X2") +
                    baWritedata[4].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[5] != baWritedata[5]) || (baReadData[6] != baWritedata[6]))
                MessageBox.Show("Set Ch1 EqC fail!! Read(" + baReadData[5].ToString("X2") +
                    baReadData[6].ToString("X2") + ") != Write(" + baWritedata[5].ToString("X2") +
                    baWritedata[6].ToString("X2") + ")\n!!Please re-log!!");

            if (baReadData[7] != baWritedata[7])
                MessageBox.Show("Set Ch2 EqA fail!! Read(" + baReadData[7].ToString("X2") +
                    ") != Write(" + baWritedata[7].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[8] != baWritedata[8]) || (baReadData[9] != baWritedata[9]))
                MessageBox.Show("Set Ch2 EqB fail!! Read(" + baReadData[8].ToString("X2") +
                    baReadData[9].ToString("X2") + ") != Write(" + baWritedata[8].ToString("X2") +
                    baWritedata[9].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[10] != baWritedata[10]) || (baReadData[11] != baWritedata[11]))
                MessageBox.Show("Set Ch2 EqC fail!! Read(" + baReadData[10].ToString("X2") +
                    baReadData[11].ToString("X2") + ") != Write(" + baWritedata[10].ToString("X2") +
                    baWritedata[11].ToString("X2") + ")\n!!Please re-log!!");

            if (baReadData[12] != baWritedata[12])
                MessageBox.Show("Set Ch3 EqA fail!! Read(" + baReadData[12].ToString("X2") +
                    ") != Write(" + baWritedata[12].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[13] != baWritedata[13]) || (baReadData[14] != baWritedata[14]))
                MessageBox.Show("Set Ch3 EqB fail!! Read(" + baReadData[13].ToString("X2") +
                    baReadData[14].ToString("X2") + ") != Write(" + baWritedata[13].ToString("X2") +
                    baWritedata[14].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[15] != baWritedata[15]) || (baReadData[16] != baWritedata[16]))
                MessageBox.Show("Set Ch3 EqC fail!! Read(" + baReadData[15].ToString("X2") +
                    baReadData[16].ToString("X2") + ") != Write(" + baWritedata[15].ToString("X2") +
                    baWritedata[16].ToString("X2") + ")\n!!Please re-log!!");

            if (baReadData[17] != baWritedata[17])
                MessageBox.Show("Set Ch4 EqA fail!! Read(" + baReadData[7].ToString("X2") +
                    ") != Write(" + baWritedata[7].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[18] != baWritedata[18]) || (baReadData[19] != baWritedata[19]))
                MessageBox.Show("Set Ch4 EqB fail!! Read(" + baReadData[18].ToString("X2") +
                    baReadData[19].ToString("X2") + ") != Write(" + baWritedata[18].ToString("X2") +
                    baWritedata[19].ToString("X2") + ")\n!!Please re-log!!");

            if ((baReadData[20] != baWritedata[20]) || (baReadData[21] != baWritedata[21]))
                MessageBox.Show("Set Ch4 EqC fail!! Read(" + baReadData[20].ToString("X2") +
                    baReadData[21].ToString("X2") + ") != Write(" + baWritedata[20].ToString("X2") +
                    baWritedata[21].ToString("X2") + ")\n!!Please re-log!!");

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

            if (customerPage_selectIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                return -1;
            tmp = System.Text.Encoding.Default.GetBytes(newModuleSerialNumber);
            if (tmp.Length > 16) {
                MessageBox.Show("newSerialNumber length:" + tmp.Length + " > 16 Error!!");
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
            
            Thread.Sleep(100); // Wait command proecss

            if (i2cReadCB((byte)devAddr, 220, 16, baReadTmp) != 16)
                return -1;
            data = System.Text.Encoding.Default.GetBytes(newModuleSerialNumber);
            for (i = 0; i < 16; i++) {
                if (baReadTmp[i] != '\0') {
                    if (data[i] != baReadTmp[i]) {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("Set serial number fail!! Read(" +
                            sRead + ") != Write(" + newModuleSerialNumber + ")\n!!Please re-log!!");
                        return -1;
                    }
                }
            }

            return 0;
        }

        private int _SetCustomerSerialNumber()
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

            if (customerPage_selectIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                return -1;
            tmp = System.Text.Encoding.Default.GetBytes(tbSerialNumber.Text);
            if (tmp.Length > 16) {
                MessageBox.Show("Serial Number length:" + tmp.Length + " > 16 Error!!");
                return -1;
            }
            for (i = 0; i < data.Length; i++) {
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteCB((byte)devAddr, 236, 16, data) < 0)
                return -1;

            Thread.Sleep(100); // Wait command proecss

            if (i2cReadCB((byte)devAddr, 236, 16, baReadTmp) != 16)
                return -1;
            data = System.Text.Encoding.Default.GetBytes(tbSerialNumber.Text);
            for (i = 0; i < 16; i++) {
                if (baReadTmp[i] != '\0') {
                    if (data[i] != baReadTmp[i]) {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("Set serial number fail!! Read(" +
                            sRead + ") != Write(" + tbSerialNumber.Text + ")\n!!Please re-log!!");
                        return -1;
                    }
                }
            }

            return 0;
        }

        private int _StoreConfigIntoFlash()
        {
            byte[] data = new byte[1];

            if (customerPage_selectIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _LogCheckInput()
        {
            if (tbOperator.Text.Length == 0) {
                MessageBox.Show("Please input operator!!");
                return -1;
            }

            switch (cbLogMode.SelectedItem.ToString()) {
                case "BeforeBurnIn":
                case "AfterBurnIn":
                case "AfterBurnIn(QC)":
                case "AocBeforeGlue":
                case "AocAfterGlue":
                case "AocAfterGlue(QC)":
                case "TxOnly":
                case "RxOnly":
                case "Log(w/o Config)":
                case "ConfigOnly":
                case "BeforeTestConfig":
                case "AfterTestConfig":
                    if (lastLogFileName.Length == 0) {
                        MessageBox.Show("Please input lot and sub-lot!!");
                        return -1;
                    }
                    if (tbSerialNumber.Text.Length < 1) {
                        MessageBox.Show("Please input SN!!");
                        return -1;
                    }
                    break;

                case "B-HDMI(QC)":
                    if (tbSerialNumber.Text.Length < 1) {
                        MessageBox.Show("Please input SN!!");
                        return -1;
                    }
                    break;

                default:
                    return -1;
            }

            return 0;
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            logModeSelect = cbLogMode.SelectedItem.ToString();

            if (_LogCheckInput() < 0)    
                goto Error;
            
            if ((logModeSelect == "BeforeBurnIn") ||
                (logModeSelect == "AfterBurnIn") ||
                (logModeSelect == "AfterBurnIn(QC)") ||
                (logModeSelect == "AocBeforeGlue") ||
                (logModeSelect == "AocAfterGlue") ||
                (logModeSelect == "AocAfterGlue(QC)") ||
                (logModeSelect == "TxOnly") ||
                (logModeSelect == "RxOnly") ||
                (logModeSelect == "Log(w/o Config)")) {
                if ((tbSerialNumber.Text.Length == 16) || (tbSerialNumber.Text.Length == 17)) {
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
                        if (lastLogFileName.Length == 0)
                            _OpenLogFile();
                    }

                    if (saTmp.Length >= 3) {
                        int.TryParse(saTmp[2], out iTmp);
                        tbSerialNumber.Text = iTmp.ToString("0000");
                    }
                }
            }
            else if (logModeSelect == "B-HDMI(QC)") {
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
                    if (lastLogFileName.Length == 0)
                        _OpenLogFile();
                    
                    tbSerialNumber.Text = tbSerialNumber.Text.Substring(5, 4);
                }
            }

            if (!((tbRx1ThresholdMin.Text.Equals("0")) && (tbRx2ThresholdMin.Text.Equals("0")) &&
                (tbRx3ThresholdMin.Text.Equals("0")) && (tbRx4ThresholdMin.Text.Equals("0"))) && 
                ((logModeSelect != "TxOnly"))) {
                if ((tbRx1.Text == "0") && (tbRx2.Text == "0") && (tbRx3.Text == "0") && (tbRx4.Text == "0")) {
                    DialogResult drRxZero = MessageBox.Show("Rx value wrong. Please check be measure item!!\n(or select No ignore)", "Please select Yes or No",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drRxZero == DialogResult.Yes)
                        return;
                }
            }

            bLog.Enabled = false;
            tbSerialNumber.Enabled = false;
            lAction.Text = "Start log...";
            lResult.Text = "";
            lClassification.Text = "";
            lClassification.BackColor = System.Drawing.SystemColors.Control;
            customerPage_selectIndex = cbCustomerPage.SelectedIndex;

            if ((logModeSelect == "BeforeBurnIn") ||
                (logModeSelect == "TxOnly") || 
                (logModeSelect == "RxOnly")) {
                while (tbSerialNumber.Text[0] == ' ')
                    tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);
                if (((tbLotNumber.Text[0] == 'Y') || (tbLotNumber.Text[0] == 'y')) && tbLotNumber.Text.Length == 8) {
                    if (tbSerialNumber.Text.Length != 4) {
                        int.TryParse(tbSerialNumber.Text, out iTmp);
                        tbSerialNumber.Text = iTmp.ToString("0000");
                    }   
                }
                newModuleSerialNumber = tbLotNumber.Text + tbSubLotNumber.Text + tbSerialNumber.Text;
            }
            else if ((logModeSelect == "AfterBurnIn") ||
                (logModeSelect == "AfterBurnIn(QC)")) {
                if (tbModuleSerialNumber.Text.Length == 16) {
                    if ((tbLotNumber.Text != tbModuleSerialNumber.Text.Substring(0, 8)) ||
                        (tbSubLotNumber.Text != tbModuleSerialNumber.Text.Substring(8, 3))) {
                        _SaveLogFile();
                        tbLotNumber.Text = tbModuleSerialNumber.Text.Substring(0, 8);
                        tbSubLotNumber.Text = tbModuleSerialNumber.Text.Substring(8, 3);
                        if (lastLogFileName.Length == 0)
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
            tbSerialNumber.Enabled = true;
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
            temperature = fTmp.ToString("#0.00");

            return 0;

        clearData:
            temperature = "0";

            return 0;
        }
        
        private int _ReadVoltageValue()
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

            if (i2cReadCB((byte)devAddr, 26, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = fTmp / 10000;
            voltage = fTmp.ToString("#0.000");

            return 0;

        clearData:
            voltage = "0";

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
            byte[] data = data = new byte[] { 0, 0, 0, 0 };
            byte[] reverseData;
            int tmp;
            float power;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

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
            String sTmp;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (customerPage_selectIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadCB((byte)devAddr, 220, 16, data) != 16)
                goto clearData;

            sTmp = System.Text.Encoding.Default.GetString(data);
            if (sTmp.IndexOf('\0') >= 0)
                moduleSerialNumber = sTmp.Substring(0, sTmp.IndexOf('\0'));
            else
                moduleSerialNumber = sTmp;

            if (i2cReadCB((byte)devAddr, 236, 16, data) != 16)
                goto clearData;

            sTmp = System.Text.Encoding.Default.GetString(data);
            if (sTmp.IndexOf('\0') >= 0)
                customerSerialNumber = sTmp.Substring(0, sTmp.IndexOf('\0'));
            else
                customerSerialNumber = sTmp;


            return 0;

        clearData:
            moduleSerialNumber = "";
            customerSerialNumber = "";
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

        private int _ReadTxFaultValue()
        {
            byte[] data = new byte[1];
            int devAddr, page, regAddr;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cTxFaultDevAddr.Text, out devAddr);
            int.TryParse(tbI2cTxFaultRegisterPage.Text, out page);
            int.TryParse(tbI2cTxFaultRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (i2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (i2cReadCB((byte)devAddr, (byte)regAddr, 1, txFault) != 1)
                goto clearData;

            return 0;

        clearData:
            txFault[0] = 0;
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

        private int _ReadRxIcRssi()
        {
            byte[] data = new byte[16];
            int devAddr, delay, tmp;

            delay = 100;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadCB((byte)devAddr, 0, 1, data) != 1)
                goto clearData;

            if (data[0] != 0x77)
                return -1;

            data[0] = 0x0F;
            if (i2cWriteCB((byte)devAddr, 0x16, 1, data) < 0)
                goto clearData;

            data[0] = 0xF0;
            if (i2cWriteCB((byte)devAddr, 0x40, 1, data) < 0)
                goto clearData;

            //RSSI0
            data[0] = 0xC0;
            if (i2cWriteCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxRssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxRssiValue[0] = tmp.ToString();
            }

            //RSSI1
            data[0] = 0xD0;
            if (i2cWriteCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;
            
            if (data[0] == 0)
                rxRssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxRssiValue[1] = tmp.ToString();
            }

            //RSSI2
            data[0] = 0xE0;
            if (i2cWriteCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxRssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxRssiValue[2] = tmp.ToString();
            }

            //RSSI3
            data[0] = 0xF0;
            if (i2cWriteCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxRssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxRssiValue[3] = tmp.ToString();
            }

            return 0;

        clearData:
            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "0";

            return 0;
        }

        private int _GetModuleMonitorValue()
        {
            bool rxValueReadError, txPowerReadError;

            rxValueReadError = txPowerReadError = false;
            switch (logModeSelect) {
                case "TxOnly":
                    if (_ReadTxPower() < 0) {
                        if (!(txPower[0].Equals("NA") &&
                            txPower[1].Equals("NA") &&
                            txPower[2].Equals("NA") &&
                            txPower[3].Equals("NA")))
                            txPowerReadError = true;
                    }
                    break;

                case "RxOnly":
                    if (_ReadRxIcRssi() < 0)
                        rxValueReadError = true;
                    break;

                default:
                    if (_ReadTemperatureValue() < 0)
                        rxValueReadError = true;

                    if (_ReadVoltageValue() < 0)
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

                    if (_ReadTxFaultValue() < 0)
                        rxValueReadError = true;

                    if ((String.Compare(rxRssiValue[0], "65535") == 0) &&
                        (String.Compare(rxRssiValue[1], "65535") == 0) &&
                        (String.Compare(rxRssiValue[2], "65535") == 0) &&
                        (String.Compare(rxRssiValue[3], "65535") == 0) &&
                        (String.Compare(mpdValue[0], "65535") == 0) &&
                        (String.Compare(mpdValue[1], "65535") == 0) &&
                        (String.Compare(mpdValue[2], "65535") == 0) &&
                        (String.Compare(mpdValue[3], "65535") == 0) &&
                        (String.Compare(temperature, "0") != 0) &&
                        (String.Compare(voltage, "0") != 0))
                        _WritePassword();

                    if (_ReadTxPower() < 0) {
                        if (!(txPower[0].Equals("NA") &&
                            txPower[1].Equals("NA") &&
                            txPower[2].Equals("NA") &&
                            txPower[3].Equals("NA")))
                            txPowerReadError = true;
                    }
                    break;
            }

            if (rxValueReadError || txPowerReadError)
                return -1;
            
            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;

            while (monitorStart) {
                bGetModuleMonitorValueError = false;

                if (logValue == true) {
                    switch (logModeSelect) {
                        case "TxOnly":
                        case "RxOnly":
                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "BeforeBurnIn":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();
                            bwMonitor.ReportProgress(4, null);
                            if (_SetModuleSerialNumber() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 3;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AocBeforeGlue":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();
                            bwMonitor.ReportProgress(4, null);
                            if (_SetCustomerSerialNumber() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();
                            
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 3;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_ReadSerialNumberValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AfterBurnIn":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            bwMonitor.ReportProgress(11, null);
                            if (_AutoCorrectTemperature() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();

                            bwMonitor.ReportProgress(13, null);
                            if (_AutoCorrectRxPowerRate() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(12, null);
                            if (_AutoCorrectVoltage() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 3;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_ReadVoltageValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AfterBurnIn(QC)":
                            _WritePassword();
                            _SetQsfpMode(0x4D);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 3;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            if (setPowerSupplyCB(false) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(7, null);
                            Thread.Sleep(1000);
                            if (setPowerSupplyCB(true) < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(8, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_ReadVoltageValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AocAfterGlue":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            bwMonitor.ReportProgress(11, null);
                            if (_AutoCorrectTemperature() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(13, null);
                            if (_AutoCorrectRxPowerRate() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(12, null);
                            if (_AutoCorrectVoltage() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();
                            
                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AocAfterGlue(QC)":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();

                            statusNotifyCount = 2;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable
                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "ConfigOnly":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "BeforeTestConfig":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(5, null);
                            _SetBeforeTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AfterTestConfig":
                            _WritePassword();
                            _SetQsfpMode(0x4D);
                            statusNotifyCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterTestConfig();

                            statusNotifyCount = 1;
                            bwMonitor.ReportProgress(1, null);
                            if (_StoreConfigIntoFlash() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            Thread.Sleep(1000); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        default:
                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }
                            bwMonitor.ReportProgress(9, null);

                            bwMonitor.ReportProgress(10, null);

                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;
                    }
                }
                else {
                    _WritePassword();
                    _SetQsfpMode(0x4D);
                    if (_GetModuleMonitorValue() < 0)
                        bGetModuleMonitorValueError = true;
                    else
                        bwMonitor.ReportProgress(0, null);
                }

                if (bGetModuleMonitorValueError == false)
                    Thread.Sleep(100);
                else {

                    Thread.Sleep(500);
                }
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
            byte[] data = new byte[1];

            /* thread@wood_20230728: different thread object
            lAction.Text = "Sotre Rx power rate...";
            lAction.Update();
            */

            if (customerPage_selectIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

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

            data = new byte[4];

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

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

        private int _SetBeforeTestConfig()
        {
            byte[] data;
            byte devAddr, regAddr, length;
            int delayTime, i;

            if (dtBeforeTestConfig.Rows.Count == 0)
                return 0;

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            foreach (DataRow row in dtBeforeTestConfig.Rows)
            {
                statusNotifyCount++;
                switch (row[0].ToString())
                {
                    case "Delay10mSec":
                        delayTime = int.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        bwMonitor.ReportProgress(6, null);
                        Thread.Sleep(delayTime);
                        break;

                    case "Write":
                        data = new byte[1];
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data[0] = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        i2cWriteCB(devAddr, regAddr, 1, data);
                        break;

                    case "WriteMulti":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data = new byte[length];
                        for (i = 0; i < length; i++) 
                            data[i] = byte.Parse(row[4 + i].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);

                        i2cWriteCB(devAddr, regAddr, length, data);
                        break;

                    case "Read":
                        data = new byte[1];
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

                    case "ReadMulti":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data = new byte[length];
                        while (i2cReadCB(devAddr, regAddr, length, data) != length) {
                            MessageBox.Show("i2cReadCB() fail!!");
                            Thread.Sleep(100);
                        }
                        for (i = 0; i < length; i++) {
                            if (data[i] != byte.Parse(row[4 + i].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber)) {
                                MessageBox.Show("DevAddr:0x" + devAddr.ToString("X2") + "RegAddr:0x" + regAddr.ToString("X2") +
                                    "Value:0x" + data[i].ToString("X2") + " != " + row[4 + i].ToString());
                                return -1;
                            }
                        }
                        break;


                    default:
                        break;
                }

                if (statusNotifyCount % 5 == 0)
                {
                    bwMonitor.ReportProgress(5, null);
                    Thread.Sleep(1);
                }
            }

            return 0;
        }

        private int _SetAfterTestConfig()
        {
            byte[] data = new byte[1];
            byte devAddr, regAddr, length;
            int delayTime, i;

            if (dtAfterTestConfig.Rows.Count == 0)
                return 0;

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            foreach (DataRow row in dtAfterTestConfig.Rows) {
                statusNotifyCount++;
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

                    case "WriteMulti":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data = new byte[length];
                        for (i = 0; i < length; i++)
                            data[i] = byte.Parse(row[4 + i].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);

                        i2cWriteCB(devAddr, regAddr, length, data);
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

                    case "ReadMulti":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data = new byte[length];
                        while (i2cReadCB(devAddr, regAddr, length, data) != length) {
                            MessageBox.Show("i2cReadCB() fail!!");
                            Thread.Sleep(100);
                        }
                        for (i = 0; i < length; i++) {
                            if (data[i] != byte.Parse(row[4 + i].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber)) {
                                MessageBox.Show("DevAddr:0x" + devAddr.ToString("X2") + "RegAddr:0x" + regAddr.ToString("X2") +
                                    "Value:0x" + data[i].ToString("X2") + " != " + row[4 + i].ToString());
                                return -1;
                            }
                        }
                        break;

                    default:
                        break;
                }

                if (statusNotifyCount % 5 == 0) {
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

            /* thread@wood_20230728: different thread object
            lAction.Text = "Correct Rx power rate...";
            lAction.Update();
            */

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

        private int _AutoCorrectTemperature()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float fTemperature, fOffset, fTarget;
            int devAddr, oldOffset;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadCB((byte)devAddr, 22, 2, data) != 2)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                messageBoxError = eBC.ToString();
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fTemperature = BitConverter.ToUInt16(reverseData, 0);
            fTemperature = fTemperature / 256;

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 241, 1, data) != 1)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, sData, 0, 1);
            }
            catch (Exception eBC) {
                messageBoxError = eBC.ToString();
                return -1;
            }

            oldOffset = sData[0];

            try {
                fTarget = Convert.ToSingle(tbTemperatureSet.Text);
            }
            catch (Exception eTSTemperature) {
                messageBoxError = eTSTemperature.ToString();
                return -1;
            }

            fOffset = (float)(((fTarget - fTemperature) * 2) + oldOffset);

            if ((fOffset > 127) || (fOffset < -128)) {
                messageBoxError = "Voltage offset out of range: " + fOffset + " (-128 ~ 127)!!";
                return -1;
            }

            try {
                sData[0] = Convert.ToSByte(fOffset);
            }
            catch (Exception eTSB) {
                messageBoxError = eTSB.ToString();
                return -1;
            }

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            i2cWriteCB(80, 127, 1, data);
            try {
                Buffer.BlockCopy(sData, 0, data, 0, 1);
            }
            catch (Exception e2) {
                messageBoxError = e2.ToString();
                return -1;
            }
            i2cWriteCB(80, 241, 1, data);

            return 0;
        }

        private int _AutoCorrectVoltage()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            sbyte[] sData = new sbyte[1];
            float fVoltage, fOffset, fTarget;
            int devAddr, oldOffset;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadCB((byte)devAddr, 26, 2, data) != 2)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                messageBoxError = eBC.ToString();
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fVoltage = BitConverter.ToUInt16(reverseData, 0);
            fVoltage = fVoltage / 10000;

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 240, 1, data) != 1)
                return -1;
            
            try {
                Buffer.BlockCopy(data, 0, sData, 0, 1);
            }
            catch (Exception eBC) {
                messageBoxError = eBC.ToString();
                return -1;
            }

            oldOffset = sData[0];

            try {
                fTarget = Convert.ToSingle(tbVoltageSet.Text);
            }
            catch (Exception eTSVoltage) {
                messageBoxError = eTSVoltage.ToString();
                return -1;
            }

            fOffset = ((fTarget - fVoltage) * 1000 / 25) + oldOffset;

            if ((fOffset > 127) || (fOffset < -128)) {
                messageBoxError = "Voltage offset out of range: " + fOffset + " (-128 ~ 127)!!";
                return -1;
            }

            try {
                sData[0] = Convert.ToSByte(fOffset);
            }
            catch (Exception eTSB) {
                messageBoxError = eTSB.ToString();
                return -1;
            }

            if (customerPage_selectIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            i2cWriteCB(80, 127, 1, data);
            try {
                Buffer.BlockCopy(sData, 0, data, 0, 1);
            }
            catch (Exception e2) {
                messageBoxError = e2.ToString();
                return -1;
            }
            i2cWriteCB(80, 240, 1, data);

            return 0;
        }

        private int _UpdateTxPowerLogGui()
        {
            float fTmp, fThreshold;

            fTmp = fThreshold = 0;
            if (!txPower[0].Equals("NA")) {
                float.TryParse(tbTx1Threshold.Text, out fThreshold);
                float.TryParse(txPower[0], out fTmp);
                if (fTmp < fThreshold)
                    tbTx1PowerLog.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx1PowerLog.ForeColor = SystemColors.ControlText;
            }
            tbTx1PowerLog.Text = txPower[0];
            tbTx1PowerLog.Update();

            if (!txPower[1].Equals("NA")) {
                float.TryParse(tbTx2Threshold.Text, out fThreshold);
                float.TryParse(txPower[1], out fTmp);
                if (fTmp < fThreshold)
                    tbTx2PowerLog.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx2PowerLog.ForeColor = SystemColors.ControlText;
            }
            tbTx2PowerLog.Text = txPower[1];
            tbTx2PowerLog.Update();

            if (!txPower[2].Equals("NA")) {
                float.TryParse(tbTx3Threshold.Text, out fThreshold);
                float.TryParse(txPower[2], out fTmp);
                if (fTmp < fThreshold)
                    tbTx3PowerLog.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx3PowerLog.ForeColor = SystemColors.ControlText;
            }
            tbTx3PowerLog.Text = txPower[2];
            tbTx3PowerLog.Update();

            if (!txPower[2].Equals("NA")) {
                float.TryParse(tbTx4Threshold.Text, out fThreshold);
                float.TryParse(txPower[3], out fTmp);
                if (fTmp < fThreshold)
                    tbTx4PowerLog.ForeColor = System.Drawing.Color.Red;
                else
                    tbTx4PowerLog.ForeColor = SystemColors.ControlText;
            }
            tbTx4PowerLog.Text = txPower[3];
            tbTx4PowerLog.Update();

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

        private int _UpdateRxRssiValueLogGui()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRx1ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[0]);
            if (fTmp < fThreshold)
                tbRx1Log.ForeColor = System.Drawing.Color.Red;
            else
                tbRx1Log.ForeColor = SystemColors.ControlText;
            tbRx1Log.Text = rxRssiValue[0];
            tbRx1Log.Update();

            fThreshold = float.Parse(tbRx2ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[1]);
            if (fTmp < fThreshold)
                tbRx2Log.ForeColor = System.Drawing.Color.Red;
            else
                tbRx2Log.ForeColor = SystemColors.ControlText;
            tbRx2Log.Text = rxRssiValue[1];
            tbRx2Log.Update();

            fThreshold = float.Parse(tbRx3ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[2]);
            if (fTmp < fThreshold)
                tbRx3Log.ForeColor = System.Drawing.Color.Red;
            else
                tbRx3Log.ForeColor = SystemColors.ControlText;
            tbRx3Log.Text = rxRssiValue[2];
            tbRx3Log.Update();

            fThreshold = float.Parse(tbRx4ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[3]);
            if (fTmp < fThreshold)
                tbRx4Log.ForeColor = System.Drawing.Color.Red;
            else
                tbRx4Log.ForeColor = SystemColors.ControlText;
            tbRx4Log.Text = rxRssiValue[3];
            tbRx4Log.Update();

            return 0;
        }

        private int _UpdateRxRssiValueGui()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRx1ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[0]);
            if (fTmp < fThreshold)
                tbRx1.ForeColor = System.Drawing.Color.Red;
            else
                tbRx1.ForeColor = SystemColors.ControlText;
            tbRx1.Text = rxRssiValue[0];
            tbRx1.Update();

            fThreshold = float.Parse(tbRx2ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[1]);
            if (fTmp < fThreshold)
                tbRx2.ForeColor = System.Drawing.Color.Red;
            else
                tbRx2.ForeColor = SystemColors.ControlText;
            tbRx2.Text = rxRssiValue[1];
            tbRx2.Update();

            fThreshold = float.Parse(tbRx3ThresholdMin.Text);
            fTmp = float.Parse(rxRssiValue[2]);
            if (fTmp < fThreshold)
                tbRx3.ForeColor = System.Drawing.Color.Red;
            else
                tbRx3.ForeColor = SystemColors.ControlText;
            tbRx3.Text = rxRssiValue[2];
            tbRx3.Update();

            fThreshold = float.Parse(tbRx4ThresholdMin.Text);
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
            tbModuleSerialNumber.Text = moduleSerialNumber;
            tbModuleSerialNumber.Update();

            if ((tbLogLable.Text.IndexOf("AfterBurnIn") == 0) ||
                (tbLogLable.Text.IndexOf("AfterBurnIn(QC)") == 0)) {
                if ((moduleSerialNumber.Length == 15) && (moduleSerialNumber.IndexOf("YC") == 0))
                    if (tbSerialNumber.Text != moduleSerialNumber.Substring(11))
                        tbSerialNumber.Text = moduleSerialNumber.Substring(11);
            }

            return 0;
        }

        private int _UpdateCableSerailNumberValueGui()
        {
            tbCableSerialNumber.Text = customerSerialNumber;
            tbCableSerialNumber.Update();

            if ((tbLogLable.Text.IndexOf("AocAfterGlue") == 0) ||
                (tbLogLable.Text.IndexOf("AocAfterGlue(QC") == 0)) {
                if ((tbLotNumber.Text.IndexOf("XA") == 0) || tbLotNumber.Text.IndexOf("xa") == 0)
                    if ((customerSerialNumber.Length > 0) && (tbSerialNumber.Text != customerSerialNumber))
                        tbSerialNumber.Text = customerSerialNumber;
            }

            return 0;
        }

        private int _UpdateLosStatusGui()
        {
            if ((losStatus[0] & 0x01) == 0) {
                cbRx1Los.Checked = false;
                cbRx1Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbRx1Los.Checked = true;
                cbRx1Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x02) == 0) {
                cbRx2Los.Checked = false;
                cbRx2Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbRx2Los.Checked = true;
                cbRx2Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x04) == 0) {
                cbRx3Los.Checked = false;
                cbRx3Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbRx3Los.Checked = true;
                cbRx3Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x08) == 0) {
                cbRx4Los.Checked = false;
                cbRx4Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbRx4Los.Checked = true;
                cbRx4Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x10) == 0) {
                cbTx1Los.Checked = false;
                cbTx1Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbTx1Los.Checked = true;
                cbTx1Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x20) == 0) {
                cbTx2Los.Checked = false;
                cbTx2Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbTx2Los.Checked = true;
                cbTx2Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x40) == 0) {
                cbTx3Los.Checked = false;
                cbTx3Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbTx3Los.Checked = true;
                cbTx3Los.ForeColor = System.Drawing.Color.Red;
            }

            if ((losStatus[0] & 0x80) == 0) {
                cbTx4Los.Checked = false;
                cbTx4Los.ForeColor = SystemColors.ControlText;
            }
            else {
                cbTx4Los.Checked = true;
                cbTx4Los.ForeColor = System.Drawing.Color.Red;
            }

            return 0;
        }

        private int _UpdateTxFaultStatusGui()
        {
            switch (cbTxFaultBit.SelectedItem) {
                case "N,N,N,N,4,3,2,1":
                    if ((txFault[0] & 0x01) == 0) {
                        cbTx1Fault.Checked = false;
                        cbTx1Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx1Fault.Checked = true;
                        cbTx1Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x02) == 0) {
                        cbTx2Fault.Checked = false;
                        cbTx2Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx2Fault.Checked = true;
                        cbTx2Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x04) == 0) {
                        cbTx3Fault.Checked = false;
                        cbTx3Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx3Fault.Checked = true;
                        cbTx3Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x08) == 0) {
                        cbTx4Fault.Checked = false;
                        cbTx4Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx4Fault.Checked = true;
                        cbTx4Fault.ForeColor = System.Drawing.Color.Red;
                    }
                    break;

                case "4,3,2,1,N,N,N,N":
                    if ((txFault[0] & 0x10) == 0) {
                        cbTx1Fault.Checked = false;
                        cbTx1Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx1Fault.Checked = true;
                        cbTx1Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x20) == 0) {
                        cbTx2Fault.Checked = false;
                        cbTx2Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx2Fault.Checked = true;
                        cbTx2Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x40) == 0) {
                        cbTx3Fault.Checked = false;
                        cbTx3Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx3Fault.Checked = true;
                        cbTx3Fault.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((txFault[0] & 0x80) == 0) {
                        cbTx4Fault.Checked = false;
                        cbTx4Fault.ForeColor = SystemColors.ControlText;
                    }
                    else {
                        cbTx4Fault.Checked = true;
                        cbTx4Fault.ForeColor = System.Drawing.Color.Red;
                    }
                    break;

                default:
                    return -1;
            }

            cbTx1Fault.Update();
            cbTx2Fault.Update();
            cbTx3Fault.Update();
            cbTx4Fault.Update();

            return 0;
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            float fTemperatureRead;

            switch (e.ProgressPercentage) {
                case 1:
                    lAction.Text = "Store into flash ... " + statusNotifyCount;
                    lAction.Update();
                    return;

                case 2:
                    lAction.Text = "Wait value stable ... " + statusNotifyCount;
                    lAction.Update();
                    return;

                case 3:
                    lAction.Text = "Get value ...";
                    lAction.Update();
                    break;

                case 4:
                    lAction.Text = "Set SN ...";
                    lAction.Update();
                    return;

                case 5:
                    lAction.Text = "Set before test config " + statusNotifyCount + "/" + dtBeforeTestConfig.Rows.Count + " ...";
                    lAction.Update();
                    return;

                case 6:
                    lAction.Text = "Set after test config " + statusNotifyCount + "/" + dtAfterTestConfig.Rows.Count + " ...";
                    lAction.Update();
                    return;

                case 7:
                    lAction.Text = "Module power OFF ...";
                    lAction.Update();
                    return;

                case 8:
                    lAction.Text = "Module power ON ...";
                    lAction.Update();
                    return;

                case 9:
                    lAction.Text = "Get value done.";
                    lAction.Update();
                    _UpdateTxPowerLogGui();
                    _UpdateRxRssiValueLogGui();
                    return;

                case 11:
                    lAction.Text = "Temperature correct ...";
                    lAction.Update();
                    return;

                case 12:
                    lAction.Text = "Voltage correct ...";
                    lAction.Update();
                    return;

                case 13:
                    lAction.Text = "Rx correct ...";
                    lAction.Update();
                    return;

                case 99:
                    lAction.Text = "Wait log ...";
                    lAction.Update();
                    break;

                case 100:
                    lAction.Text = "bwMonitor STOP ERROR!!";
                    lAction.Update();
                    break;

                default:
                    if (messageBoxError.Length != 0) {
                        MessageBox.Show(messageBoxError);
                        messageBoxError = "";
                    }
                    break;
            }
            
            _UpdateTxPowerGui();
            tbTemperature.Text = temperature;
            tbTemperature.Update();
            tbVoltage.Text = voltage;
            tbVoltage.Update();
            _UpdateTxBiasValueGui();
            _UpdateRxRssiValueGui();
            _UpdateMpdValueGui();
            _UpdateModuleSerailNumberValueGui();
            _UpdateCableSerailNumberValueGui();
            _UpdateLosStatusGui();
            _UpdateTxFaultStatusGui();

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
                    case "AocAfterGlue":
                        _AddLogValue(true);
                        lAction.Text = "Log Added.";
                        lAction.Update();
                        tbSerialNumber.SelectAll();
                        tbTx1PowerLog.Text = tbTx2PowerLog.Text = tbTx3PowerLog.Text = tbTx4PowerLog.Text =
                            tbRx1Log.Text = tbRx2Log.Text = tbRx3Log.Text = tbRx4Log.Text = "";
                        tbTx1PowerLog.ForeColor = tbTx2PowerLog.ForeColor = tbTx3PowerLog.ForeColor = tbTx4PowerLog.ForeColor =
                            tbRx1Log.ForeColor = tbRx2Log.ForeColor = tbRx3Log.ForeColor = tbRx4Log.ForeColor =
                            SystemColors.ControlText;
                        break;

                    case "ConfigOnly":
                        lAction.Text = "AC Config Done.";
                        lAction.Update();
                        break;

                    case "BeforeBurnIn":
                    case "AocBeforeGlue":
                    case "AfterBurnIn(QC)":
                    case "AocAfterGlue(QC)":
                    default:
                        _AddLogValue(false);
                        lAction.Text = "Log Added.";
                        lAction.Update();
                        tbSerialNumber.SelectAll();
                        tbTx1PowerLog.Text = tbTx2PowerLog.Text = tbTx3PowerLog.Text = tbTx4PowerLog.Text =
                            tbRx1Log.Text = tbRx2Log.Text = tbRx3Log.Text = tbRx4Log.Text = "";
                        tbTx1PowerLog.ForeColor = tbTx2PowerLog.ForeColor = tbTx3PowerLog.ForeColor = tbTx4PowerLog.ForeColor =
                            tbRx1Log.ForeColor = tbRx2Log.ForeColor = tbRx3Log.ForeColor = tbRx4Log.ForeColor =
                            SystemColors.ControlText;
                        break;
                }
                logValue = false;
                bLog.Enabled = true;
                tbSerialNumber.Enabled = true;
                tbSerialNumber.Focus();
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
                        lAction.Text = "Temperature trigger onlay support: 70, 25, -5 DegC!!";
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
            SaveFileDialog sfdSelectFile;
            StreamWriter swLog;
            String directoryPath;
            Boolean logSaved = false;

            if (dtValue.Rows.Count == 0)
                goto clear;
            
            if ((lastLogFileName == "") || fileRecordNumber > dtValue.Rows.Count) {
                sfdSelectFile = new SaveFileDialog();
                sfdSelectFile.Filter = "csv files (*.csv)|*.csv";
                sfdSelectFile.InitialDirectory = fileDirectory;
                if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                    return;
                tbLogFilePath.Text = sfdSelectFile.FileName;
                tbLogFilePath.Update();
            }

            do {
                try {
                    swLog = new StreamWriter(tbLogFilePath.Text);
                    swLog.WriteLine("Lable,Time,SN,CSN,Tx1(uW),Tx2(uW),Tx3(uW),Tx4(uW),Rx1,Rx2,Rx3,Rx4,MPD1,MPD2,MPD3,MPD4,Bias1,Bias2,Bias3,Bias4,Temperature,Voltage,Operator,Grade,Note");
                    foreach (DataRow row in dtValue.Rows) {
                        swLog.WriteLine(row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                            row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString() + "," +
                            row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString() + "," +
                            row[9].ToString() + "," + row[10].ToString() + "," + row[11].ToString() + "," +
                            row[12].ToString() + "," + row[13].ToString() + "," + row[14].ToString() + "," +
                            row[15].ToString() + "," + row[16].ToString() + "," + row[17].ToString() + "," +
                            row[18].ToString() + "," + row[19].ToString() + "," + row[20].ToString() + "," +
                            row[21].ToString() + "," + row[22].ToString() + "," + row[23].ToString() + "," +
                            row[24].ToString());
                    }
                    swLog.Close();
                    logSaved = true;
                }
                catch (Exception e) {
                    MessageBox.Show(e.ToString());
                }
            } while (logSaved == false);
            dtValue.Clear();

            if (tbLogLable.Text.Equals("AocAfterGlue")) {
                directoryPath = remoteFileDirectory + "\\" + tbLotNumber.Text;
                if (!Directory.Exists(directoryPath)) {
                    try {
                        Directory.CreateDirectory(directoryPath);
                    }
                    catch (Exception e) {
                        MessageBox.Show(e.ToString());
                    }
                }
                try {
                    File.Copy(tbLogFilePath.Text, Path.Combine(directoryPath, lastLogFileName) ,true);
                } catch (Exception e) {
                    MessageBox.Show(e.ToString());
                }
            }

        clear:
            tbLotNumber.Text = "";
            tbSubLotNumber.Text = "";
            lastLogFileName = "";
            tbLogFilePath.Text = fileDirectory;
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
            StreamReader srLog = null;
            String sLine;
            String[] saItems;
            Boolean logOpened = false;

            lastLogFileName = tbLotNumber.Text + "-" + tbSubLotNumber.Text + ".csv";
            tbLogFilePath.Text = fileDirectory + "\\" + lastLogFileName;
            tbLogFilePath.Update();
            fileRecordNumber = 0;

            do {
                try {
                    srLog = new StreamReader(tbLogFilePath.Text);
                    logOpened = true;
                }
                catch (FileNotFoundException e1) {
                    if (e1 != null)
                        return;
                    return;
                }
                catch (Exception e2) {
                    MessageBox.Show(e2.ToString());
                }
            } while (logOpened == false);

            if (srLog == null)
                return;

            if ((sLine = srLog.ReadLine()) == null) //Header
                return;

            while ((sLine = srLog.ReadLine()) != null) { //Record
                saItems = sLine.Split(',');
                if (saItems.Length < 17)
                    continue;

                if (saItems.Length == 17) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], "", saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], "", "", "", "", "", "", "", saItems[15], "",
                        saItems[16]);
                    fileRecordNumber++;
                }
                else if (saItems.Length == 22) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], "", saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], "", saItems[20], "", saItems[21]);
                    fileRecordNumber++;
                }
                else if (saItems.Length == 23) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], "", saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], saItems[20], saItems[21], "", saItems[22]);
                    fileRecordNumber++;
                }
                else if (saItems.Length == 24) {
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], saItems[20], saItems[21], saItems[22], "", saItems[23]);
                    fileRecordNumber++;
                }
                else if (saItems.Length == 25) {
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], saItems[20], saItems[21], saItems[22], saItems[23], saItems[24]);
                    fileRecordNumber++;
                }
            }

            srLog.Close();

            bLog.Enabled = true;
        }

        private void tbLotNumber_Leave(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbLotNumber.Text.Length == 12) {
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

            if (lastLogFileName.Length == 0)
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

            _SaveLogFile();
        }

        private void tbSubLotNumber_Enter(object sender, EventArgs e)
        {
            String lotNumber;

            if (lastLogFileName.Length == 0)
                return;

            lotNumber = tbLotNumber.Text;
            _SaveLogFile();
            tbLotNumber.Text = lotNumber;
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
                case "TxOnly":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "TxOnly";
                    break;

                case "RxOnly":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "RxOnly";
                    break;

                case "BeforeBurnIn":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "BeforeBurnIn";
                    break;

                case "AfterBurnIn":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AfterBurnIn";
                    break;

                case "AfterBurnIn(QC)":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AfterBurnIn(QC)";
                    break;

                case "AocBeforeGlue":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AocBeforeGlue";
                    break;

                case "AocAfterGlue":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AocAfterGlue";
                    break;

                case "AocAfterGlue(QC)":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AocAfterGlue(QC)";
                    break;

                case "ConfigOnly":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "ConfigOnly";
                    break;

                case "BeforeTestConfig":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "BeforeTestConfig";
                    break;

                case "AfterTestConfig":
                    tbLogLable.Enabled = false;
                    tbLogLable.Text = "AfterTestConfig";
                    break;
                    
                default:
                    tbLogLable.Text = "";
                    tbLogLable.Enabled = true;
                    break;
            }
        }

        private void bSaveFile_Click(object sender, EventArgs e)
        {
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
            int rowIndex, i, length;

            sfdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            using (XmlWriter xwConfig = XmlWriter.Create(sfdSelectFile.FileName)) {
                xwConfig.WriteStartDocument();
                xwConfig.WriteStartElement("DcTestConfig");
                {
                    xwConfig.WriteElementString("AfterBurnInPowerDifferentPercentage", tbAfterBurnInPowerDifferentLimit.Text);
                    xwConfig.WriteElementString("AfterBurnInReBIPowerDifferentPercentage", tbAfterBurnInReBIPowerDifferentLimit.Text);
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
                        xwConfig.WriteElementString("I2cTxFaultDevAddr", tbI2cTxFaultDevAddr.Text);
                        xwConfig.WriteElementString("I2cTxFaultRegisterPage", tbI2cTxFaultRegisterPage.Text);
                        xwConfig.WriteElementString("I2cTxFaultRegisterAddr", tbI2cTxFaultRegisterAddr.Text);
                    }
                    xwConfig.WriteEndElement(); //I2cConfig

                    xwConfig.WriteStartElement("TemperatureCorrectAndThresholdConfig");
                    {
                        xwConfig.WriteElementString("TemperatureSet", tbTemperatureSet.Text);
                        xwConfig.WriteElementString("TemperatureMaxThreshold", tbTemperatureMaxThreshold.Text);
                        xwConfig.WriteElementString("TemperatureMinThreshold", tbTemperatureMinThreshold.Text);
                    }
                    xwConfig.WriteEndElement(); //VoltageCorrectAndThresholdConfig

                    xwConfig.WriteStartElement("VoltageCorrectAndThresholdConfig");
                    {
                        xwConfig.WriteElementString("VoltageSet", tbVoltageSet.Text);
                        xwConfig.WriteElementString("VoltageMaxThreshold", tbVoltageMaxThreshold.Text);
                        xwConfig.WriteElementString("VoltageMinThreshold", tbVoltageMinThreshold.Text);
                    }
                    xwConfig.WriteEndElement(); //VoltageCorrectAndThresholdConfig

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
                        xwConfig.WriteElementString("Rx1ThresholdMin", tbRx1ThresholdMin.Text);
                        xwConfig.WriteElementString("Rx2ThresholdMin", tbRx2ThresholdMin.Text);
                        xwConfig.WriteElementString("Rx3ThresholdMin", tbRx3ThresholdMin.Text);
                        xwConfig.WriteElementString("Rx4ThresholdMin", tbRx4ThresholdMin.Text);
                        xwConfig.WriteElementString("Rx1ThresholdMax", tbRx1ThresholdMax.Text);
                        xwConfig.WriteElementString("Rx2ThresholdMax", tbRx2ThresholdMax.Text);
                        xwConfig.WriteElementString("Rx3ThresholdMax", tbRx3ThresholdMax.Text);
                        xwConfig.WriteElementString("Rx4ThresholdMax", tbRx4ThresholdMax.Text);
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

                    xwConfig.WriteStartElement("MiscConfig");
                    {
                        xwConfig.WriteElementString("CustomerPage", cbCustomerPage.Text);
                        xwConfig.WriteElementString("IgnoreRxLos", cbIgnoreRxLos.Checked.ToString());
                        xwConfig.WriteElementString("IgnoreTxLos", cbIgnoreTxLos.Checked.ToString());
                        xwConfig.WriteElementString("IgnoreTxFault", cbIgnoreTxFault.Checked.ToString());
                        xwConfig.WriteElementString("PowerSupplyControlReverse", cbPowerSupplyControlReverse.Checked.ToString());
                    }
                    xwConfig.WriteEndElement(); //CustomerPageConfig

                    sTmp = "";
                    rowIndex = 0;
                    foreach (DataRow row in dtBeforeTestConfig.Rows)
                    {
                        switch (row[0].ToString())
                        {
                            case "Delay10mSec":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                                break;

                            case "Write":
                            case "Read":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                                    row[3].ToString() + "\n");
                                break;

                            case "WriteMulti":
                            case "ReadMulti":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                                    row[3].ToString());
                                length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                                for (i = 0; i < length; i++) {
                                    if (row[i + 4].ToString().Length == 0) {
                                        MessageBox.Show("row[" + rowIndex + "]:" + row[0].ToString() + " length:" + length + " but data[" + i + "](P" + (i + 4).ToString("D3") + ") Empty!!");
                                        i = length;
                                    }
                                    else
                                        sTmp += "," + row[i + 4].ToString();
                                }
                                sTmp += "\n";
                                break;

                            default:
                                break;
                        }
                        rowIndex++;
                    }
                    xwConfig.WriteElementString("BeforeTestConfig", sTmp);

                    sTmp = "";
                    rowIndex = 0;
                    foreach (DataRow row in dtAfterTestConfig.Rows) {
                        switch (row[0].ToString()) {
                            case "Delay10mSec":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                                break;

                            case "Write":
                            case "Read":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                                    row[3].ToString() + "\n");
                                break;

                            case "WriteMulti":
                            case "ReadMulti":
                                sTmp += (row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                                    row[3].ToString());
                                length = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                                for (i = 0; i < byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber); i++) {
                                    if (row[i + 4].ToString().Length == 0) {
                                        MessageBox.Show("row[" + rowIndex + "]:" + row[0].ToString() + " length:" + length + " but data[" + i + "](P" + (i + 4).ToString("D3") + ") Empty!!");
                                        i = length;
                                    }
                                    else
                                        sTmp += "," + row[i + 4].ToString();
                                }
                                sTmp += "\n";
                                break;

                            default:
                                break;
                        }
                        rowIndex++;
                    }
                    xwConfig.WriteElementString("AfterTestConfig", sTmp);
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
                            break;

                        case "I2cTxFaultDevAddr":
                            tbI2cTxFaultDevAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cTxFaultRegisterPage":
                            tbI2cTxFaultRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "I2cTxFaultRegisterAddr":
                            tbI2cTxFaultRegisterAddr.Text = reader.ReadElementContentAsString();
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

        
        private void _PaserTemperatureCorrectAndThresholdConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "TemperatureSet":
                            tbTemperatureSet.Text = reader.ReadElementContentAsString();
                            break;

                        case "TemperatureMaxThreshold":
                            tbTemperatureMaxThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "TemperatureMinThreshold":
                            tbTemperatureMinThreshold.Text = reader.ReadElementContentAsString();
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

        private void _PaserVoltageCorrectAndThresholdConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "VoltageSet":
                            tbVoltageSet.Text = reader.ReadElementContentAsString();
                            break;

                        case "VoltageMaxThreshold":
                            tbVoltageMaxThreshold.Text = reader.ReadElementContentAsString();
                            break;

                        case "VoltageMinThreshold":
                            tbVoltageMinThreshold.Text = reader.ReadElementContentAsString();
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
                        case "Rx1ThresholdMin":
                            tbRx1ThresholdMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2Threshold":
                        case "Rx2ThresholdMin":
                            tbRx2ThresholdMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3Threshold":
                        case "Rx3ThresholdMin":
                            tbRx3ThresholdMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4Threshold":
                        case "Rx4ThresholdMin":
                            tbRx4ThresholdMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx1ThresholdMax":
                            tbRx1ThresholdMax.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2ThresholdMax":
                            tbRx2ThresholdMax.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3ThresholdMax":
                            tbRx3ThresholdMax.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4ThresholdMax":
                            tbRx4ThresholdMax.Text = reader.ReadElementContentAsString();
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

        private void _PaserMiscConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "CustomerPage":
                            if (reader.ReadElementContentAsString().Equals("Old: 4, 5, 32"))
                                cbCustomerPage.SelectedIndex = 0;
                            else
                                cbCustomerPage.SelectedIndex = 1;
                            break;

                        case "IgnoreRxLos":
                            if (reader.ReadElementContentAsString().Equals("False"))
                                cbIgnoreRxLos.Checked = false;
                            else
                                cbIgnoreRxLos.Checked = true;
                            break;

                        case "IgnoreTxLos":
                            if (reader.ReadElementContentAsString().Equals("False"))
                                cbIgnoreTxLos.Checked = false;
                            else
                                cbIgnoreTxLos.Checked = true;
                            break;

                        case "IgnoreTxFault":
                            if (reader.ReadElementContentAsString().Equals("False"))
                                cbIgnoreTxFault.Checked = false;
                            else
                                cbIgnoreTxFault.Checked = true;
                            break;

                        case "PowerSupplyControlReverse":
                            if (reader.ReadElementContentAsString().Equals("False"))
                                cbPowerSupplyControlReverse.Checked = false;
                            else
                                cbPowerSupplyControlReverse.Checked = true;
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

        private void _PaserBeforeTestConfig(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null)
            {
                saItems = line.Split(',');
                if (saItems.Length > 131) {
                    MessageBox.Show("saItems.Length:" + saItems.Length + " > 131 Error!!");
                    return;
                }
                dtBeforeTestConfig.Rows.Add(saItems);
            }
        }

        private void _PaserAfterTestConfig(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null) {
                saItems = line.Split(',');
                if (saItems.Length > 131) {
                    MessageBox.Show("saItems.Length:" + saItems.Length + " > 131 Error!!");
                    return;
                }
                dtAfterTestConfig.Rows.Add(saItems);
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

                        case "AfterBurnInReBIPowerDifferentPercentage":
                            tbAfterBurnInReBIPowerDifferentLimit.Text = reader.ReadElementContentAsString();
                            break;

                        case "DcTestModifyBiasCurrent":
                        case "BeforeAndAfterBurnInDcTestBiasCurrent":
                        case "BurnInBiasCurrent":
                            reader.ReadElementContentAsString();
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

                        case "TemperatureCorrectAndThresholdConfig":
                            _PaserTemperatureCorrectAndThresholdConfigXml(reader);
                            reader.Read();
                            break;

                        case "VoltageCorrectAndThresholdConfig":
                            _PaserVoltageCorrectAndThresholdConfigXml(reader);
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

                        case "MiscConfig":
                            _PaserMiscConfigXml(reader);
                            reader.Read();
                            break;

                        case "BeforeTestConfig":
                            _PaserBeforeTestConfig(reader.ReadElementContentAsString());
                            break;

                        case "AfterTestConfig":
                            _PaserAfterTestConfig(reader.ReadElementContentAsString());
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

        private void bOpenConfigFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Title = "Select config file";
            ofdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbConfigFilePath.Text = ofdSelectFile.FileName;
            tbConfigFilePath.SelectionStart = tbConfigFilePath.Text.Length;
            tbConfigFilePath.ScrollToCaret();
            dtBeforeTestConfig.Clear();
            dtAfterTestConfig.Clear();

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

            customerPage_selectIndex = cbCustomerPage.SelectedIndex;
        }

        public int SpaceKeyDownApi(object sender, KeyEventArgs e)
        {
            if (bLog.Enabled == false)
                return -1;

            bLog_Click(sender, e);

            return 0;
        }

        public int EnterKeyDownApi(object sender, KeyEventArgs e)
        {
            if (tbLotNumber.Focused == true) {
                tbLotNumber_Leave(sender, e);
            }
            else {
                if (bLog.Enabled == false)
                    return -1;

                bLog_Click(sender, e);
            }
            return 0;
        }

        public int RightKeyDownApi(object sender, KeyEventArgs e)
        {
            int iSerialNumber;

            if (bLog.Enabled == false)
                return 0;

            if (cbLogMode.Text.IndexOf("Aoc") < 0) {
                int.TryParse(tbSerialNumber.Text, out iSerialNumber);
                if (iSerialNumber + 1 < 10000)
                    tbSerialNumber.Text = (++iSerialNumber).ToString("0000");
                tbSerialNumber.Select(tbSerialNumber.Text.Length - 1, tbSerialNumber.Text.Length - 1);
            }

            return 0;
        }

        public int LeftKeyDownApi(object sender, KeyEventArgs e)
        {
            int iSerialNumber;

            if (bLog.Enabled == false)
                return 0;
            
            if (cbLogMode.Text.IndexOf("Aoc") < 0) {
                int.TryParse(tbSerialNumber.Text, out iSerialNumber);
                if (iSerialNumber - 1 >= 0)
                    tbSerialNumber.Text = (--iSerialNumber).ToString("0000");
                tbSerialNumber.Select(0, 0);
            }

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

        private void cbIgnoreRxLos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIgnoreRxLos.Checked == true) {
                cbRx1Los.Enabled = false;
                cbRx2Los.Enabled = false;
                cbRx3Los.Enabled = false;
                cbRx4Los.Enabled = false;
            }
            else {
                cbRx1Los.Enabled = true;
                cbRx2Los.Enabled = true;
                cbRx3Los.Enabled = true;
                cbRx4Los.Enabled = true;
            }
        }

        private void cbIgnoreTxLos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIgnoreTxLos.Checked == true) {
                cbTx1Los.Enabled = false;
                cbTx2Los.Enabled = false;
                cbTx3Los.Enabled = false;
                cbTx4Los.Enabled = false;
            }
            else {
                cbTx1Los.Enabled = true;
                cbTx2Los.Enabled = true;
                cbTx3Los.Enabled = true;
                cbTx4Los.Enabled = true;
            }
        }

        private void cbIgnoreTxFault_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIgnoreTxFault.Checked == true) {
                cbTx1Fault.Enabled = false;
                cbTx2Fault.Enabled = false;
                cbTx3Fault.Enabled = false;
                cbTx4Fault.Enabled = false;
            }
            else {
                cbTx1Fault.Enabled = true;
                cbTx2Fault.Enabled = true;
                cbTx3Fault.Enabled = true;
                cbTx4Fault.Enabled = true;
            }
        }

        private void tbSerialNumberNumber_Leave(object sender, EventArgs e)
        {
            String[] saTmp;
            int iTmp;

            if (tbSerialNumber.Text.Length == 17) {
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
                    if (lastLogFileName.Length == 0)
                        _OpenLogFile();
                }

                if (saTmp.Length >= 3) {
                    int.TryParse(saTmp[2], out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
            }
            else if (tbLotNumber.Text.Length > 0){
                if ((tbLotNumber.Text[0] == 'Y') || (tbLotNumber.Text[0] == 'y')) {
                    int.TryParse(tbSerialNumber.Text, out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
            }
        }

#if false //wood@20240627: Replace by before test config
        private void cbDcTestModifyBiasCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDcTestModifyBiasCurrent.Checked == true) {
                tbBeforeAndAfterBurnInDcTestBiasCurrent.Enabled = true;
                tbBurnInBiasCurrent.Enabled = true;
            }
            else {
                tbBeforeAndAfterBurnInDcTestBiasCurrent.Enabled = false;
                tbBurnInBiasCurrent.Enabled = false;
            }
        }
#endif

        private void tbSerialNumber_TextChanged(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";

            if (bLog.Enabled == false)
                return;

            if (cbLogMode.SelectedItem.ToString().Equals("AfterBurnIn") ||
                cbLogMode.SelectedItem.ToString().Equals("AocAfterGlue")) {
                dgvRecord.Sort(dgvRecord.Columns[2], ListSortDirection.Descending);
                dgvRecord.ClearSelection();
                foreach (DataGridViewRow row in dgvRecord.Rows) {
                    if (row.Index > (dgvRecord.Rows.Count - 2))
                        break;

                    if (row.Cells[0].Value.ToString().Equals("BeforeBurnIn") && row.Cells[2].Value.ToString().Equals(tbSerialNumber.Text)) {
                        if (row.Index > 0)
                            dgvRecord.FirstDisplayedScrollingRowIndex = row.Index -1;
                        else
                            dgvRecord.FirstDisplayedScrollingRowIndex = row.Index;
                        dgvRecord.Rows[row.Index].Selected = true;
                    }
                }
            }
            else 
                dgvRecord.Sort(dgvRecord.Columns[1], ListSortDirection.Descending);    
        }
    }
}
