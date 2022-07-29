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


namespace MiniSasHd4Dot0DcTest
{
    public partial class UcMiniSsaHd4Dot0DcTest : UserControl
    {
        private DataTable dtValue = new DataTable();
        private DataTable dtAfterBurnInConfig = new DataTable();
        private String fileDirectory = "c:\\DcTestLog";
        private String lastLogFileName = "";       

        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);        
        public delegate int WritePasswordCB();

        private I2cReadCB i2cReadACB = null;
        private I2cWriteCB i2cWriteACB = null;

        private I2cReadCB i2cReadBCB = null;
        private I2cWriteCB i2cWriteBCB = null;

        private BackgroundWorker bwMonitor;
        private DialogResult drAskOverwrite;
        private volatile String[] rxARssiValue = new String[4];
        private volatile String[] rxBRssiValue = new string[4];
        private volatile String[] rxAPowerRate = new String[4];
        private volatile String[] rxAPowerValue = new String[4];
        private volatile String[] rxBPowerRate = new String[4];
        private volatile String[] rxBPowerValue = new String[4];
        private volatile String temperatureA;
        private volatile String temperatureB;
        private volatile String logModeSelect;
        private volatile String serialNumber, serialNumberA, serialNumberB, newSerialNumberA, newSerialNumberB;
        private volatile String lastNote;
        private volatile int acConfigRowCount;
        private volatile bool monitorStart = false;
        private volatile bool logValue = false;
        private volatile byte[] losStatusA = new byte[1];
        private volatile byte[] losStatusB = new byte[1];

        public UcMiniSsaHd4Dot0DcTest()
        {
            InitializeComponent();

            initialConfigFile();
            rxARssiValue[0] = rxARssiValue[1] = rxARssiValue[2] = rxARssiValue[3] = "0";
            rxBRssiValue[0] = rxBRssiValue[1] = rxBRssiValue[2] = rxBRssiValue[3] = "0";

            losStatusA[0] = losStatusB[0] = 0xFF;            
            temperatureA = "0";
            temperatureB = "0";
            lastNote = serialNumber = serialNumberA = serialNumberB = "";

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);

            Directory.CreateDirectory(fileDirectory);
            tbLogFilePath.Text = fileDirectory + "\\";

            dtValue.Columns.Add("Lable", typeof(String));
            dtValue.Columns.Add("Time", typeof(String));
            dtValue.Columns.Add("SN", typeof(String));

            dtValue.Columns.Add("ARx1", typeof(String));
            dtValue.Columns.Add("ARx2", typeof(String));
            dtValue.Columns.Add("ARx3", typeof(String));
            dtValue.Columns.Add("ARx4", typeof(String));

            dtValue.Columns.Add("BRx1", typeof(String));
            dtValue.Columns.Add("BRx2", typeof(String));
            dtValue.Columns.Add("BRx3", typeof(String));
            dtValue.Columns.Add("BRx4", typeof(String));

            dtValue.Columns.Add("TempA", typeof(String));
            dtValue.Columns.Add("TempB", typeof(String));

            dtValue.Columns.Add("Operator", typeof(String));
            dtValue.Columns.Add("Grade", typeof(String));
            dtValue.Columns.Add("Note", typeof(String));

            dgvRecord.DataSource = dtValue;

            dtAfterBurnInConfig.Columns.Add("Command", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter1", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter2", typeof(String));
            dtAfterBurnInConfig.Columns.Add("Parameter3", typeof(String));

            dgvAfterBurnInConfig.DataSource = dtAfterBurnInConfig;
        }

        public int SetI2cReadACBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadACB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteACBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteACB = new I2cWriteCB(cb);

            return 0;
        }

        public int SetI2cReadBCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadBCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteBCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteBCB = new I2cWriteCB(cb);

            return 0;
        }

        private int _CheckRxValueThreshold(String[] rxValueA, String[] rxValueB)
        {
            float fTmp, fThreshold;
            bool bPass = true;

            if (lClassification.Text.Equals("T"))
                bPass = false;

            try {
                fThreshold = float.Parse(tbRx1Threshold.Text);
                if (fThreshold > 0) {
                    fTmp = float.Parse(rxValueA[0]);
                    if (fTmp < fThreshold) {
                        lastNote += "ARx1 value (" + rxValueA[0] + ") < Threshold (" + tbRx1Threshold.Text + "); ";
                        bPass = false;
                    }
                    
                    fTmp = float.Parse(rxValueB[0]);
                    if (fTmp < fThreshold)
                    {
                        lastNote += "BRx1 value (" + rxValueB[0] + ") < Threshold (" + tbRx1Threshold.Text + "); ";
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
                    fTmp = float.Parse(rxValueA[1]);
                    if (fTmp < fThreshold) {
                        lastNote += "ARx2 value (" + rxValueA[1] + ") < Threshold (" + tbRx2Threshold.Text + "); ";
                        bPass = false;
                    }

                    fTmp = float.Parse(rxValueB[1]);
                    if (fTmp < fThreshold)
                    {
                        lastNote += "BRx2 value (" + rxValueB[1] + ") < Threshold (" + tbRx2Threshold.Text + "); ";
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
                    fTmp = float.Parse(rxValueA[2]);
                    if (fTmp < fThreshold) {
                        lastNote += "ARx3 value (" + rxValueA[2] + ") < Threshold (" + tbRx3Threshold.Text + "); ";
                        bPass = false;
                    }

                    fTmp = float.Parse(rxValueB[2]);
                    if (fTmp < fThreshold)
                    {
                        lastNote += "BRx3 value (" + rxValueB[2] + ") < Threshold (" + tbRx3Threshold.Text + "); ";
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
                    fTmp = float.Parse(rxValueA[3]);
                    if (fTmp < fThreshold) {
                        lastNote += "ARx4 value (" + rxValueA[3] + ") < Threshold (" + tbRx4Threshold.Text + "); ";
                        bPass = false;
                    }

                    fTmp = float.Parse(rxValueB[3]);
                    if (fTmp < fThreshold)
                    {
                        lastNote += "BRx4 value (" + rxValueB[3] + ") < Threshold (" + tbRx4Threshold.Text + "); ";
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

            if (cbIgnoreRxLos.Checked == true)
                goto exit;

            if ((losStatusA[0] & 0x01) != 0) {
                bPass = false;
                lastNote += "ARx1 LOS; ";
            }

            if ((losStatusA[0] & 0x02) != 0) {
                bPass = false;
                lastNote += "ARx2 LOS; ";
            }

            if ((losStatusA[0] & 0x04) != 0) {
                bPass = false;
                lastNote += "ARx3 LOS; ";
            }

            if ((losStatusA[0] & 0x08) != 0) {
                bPass = false;
                lastNote += "ARx4 LOS; ";
            }

            if ((losStatusB[0] & 0x01) != 0)
            {
                bPass = false;
                lastNote += "BRx1 LOS; ";
            }

            if ((losStatusB[0] & 0x02) != 0)
            {
                bPass = false;
                lastNote += "BRx2 LOS; ";
            }

            if ((losStatusB[0] & 0x04) != 0)
            {
                bPass = false;
                lastNote += "BRx3 LOS; ";
            }

            if ((losStatusB[0] & 0x08) != 0)
            {
                bPass = false;
                lastNote += "BRx4 LOS; ";
            }

        exit:
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
            DataRow[] filteredRows = dtValue.Select("Lable = '" + logLable + "' AND SN = '" + serialNumber + "'");
            
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
            String[] saARxValue = new String[4];
            String[] saBRxValue = new String[4];

            String sTemperatureA ,sTemperatureB, grade;

            while (tbSerialNumber.Text[0] == ' ')
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);

            if (tbSerialNumber.Text.IndexOf(' ') > 0)
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(0, tbSerialNumber.Text.IndexOf(' '));

            if (_CheckDuplicationLog(tbLogLable.Text, tbSerialNumber.Text) < 0)
                return;

            saARxValue[0] = tbARx1.Text;
            saARxValue[1] = tbARx2.Text;
            saARxValue[2] = tbARx3.Text;
            saARxValue[3] = tbARx4.Text;
            saBRxValue[0] = tbBRx1.Text;
            saBRxValue[1] = tbBRx2.Text;
            saBRxValue[2] = tbBRx3.Text;
            saBRxValue[3] = tbBRx4.Text;

            sTemperatureA = tbTemperatureA.Text;
            sTemperatureB = tbTemperatureB.Text;

            _CheckRxValueThreshold(saARxValue, saBRxValue);
            grade = lClassification.Text;

            _CheckLosStatus();

            dtValue.Rows.Add(tbLogLable.Text, System.DateTime.Now.ToString("yy/MM/dd_HH:mm:ss"), tbSerialNumber.Text,
                saARxValue[0], saARxValue[1], saARxValue[2], saARxValue[3],
                saBRxValue[0], saBRxValue[1], saBRxValue[2], saBRxValue[3],
                sTemperatureA, sTemperatureB, tbOperator.Text, grade, lastNote);
            dgvRecord.FirstDisplayedScrollingRowIndex = 0;
            lastNote = "";
            _UpdateDataListStatus();

        }

        private void _UpdateDataListStatus()
        {
            int  nRows;
            float yield, numberOfData, countGradeT;
            String GradeT = "T";

            numberOfData = countGradeT = 0;

            nRows = dgvRecord.Rows.Count - 1;
            for (int i = 0; i < nRows; i++)
            {
                if (dgvRecord.Rows[i].Cells[14].Value.ToString() == GradeT)
                    countGradeT++;
            }

            numberOfData = dgvRecord.Rows.Count - 1;
            yield = (numberOfData - countGradeT) / numberOfData *100;            
            lDataNumber.Text = "" + numberOfData + " pcs";
            lGradeT.Text = "" + countGradeT + " pcs";
            lYield.Text = yield.ToString("0.0") + " %";
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[1];

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteACB(80, 164, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 164, 1, data) < 0)
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

            if (i2cWriteACB == null)
                return -1;
            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;
            if (i2cReadBCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 127, 1, data) < 0)
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

            if (i2cWriteACB(80, 128, 22, baWritedata) < 0)
                return -1;
            if (i2cWriteBCB(80, 128, 22, baWritedata) < 0)
                return -1;

            //Write enable temperature compensate
            data[0] = 1;
            if (burnIn == true) //enable burn-in
                data[0] |= 0x02;
            if (i2cWriteACB(80, 252, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 252, 1, data) < 0)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 127, 1, data) < 0)
                return -1;

            //Write 223 to enable temperature compensate
            data[0] = 0;
            if (i2cWriteACB(80, 223, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 223, 1, data) < 0)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(100); //Wait change page

            if (i2cReadACB(80, 128, 22, baReadData) != 22)
                return -1;
            if (i2cReadBCB(80, 128, 22, baReadData) != 22)
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

        private int _SetModuleSerialNumberA()
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[17];
            byte[] baReadTmp = new byte[17];
            int devAddr, i;            

            if (_WritePassword() < 0)
                return -1;
            
            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteACB((byte)devAddr, 127, 1, data) < 0)
                return -1;
                        
            tmp = System.Text.Encoding.Default.GetBytes(newSerialNumberA);
            if (tmp.Length > 17){
                MessageBox.Show("newSerialNumber length:" + tmp.Length + " > 17 Error!!");
                return -1;
            }
            for (i = 0; i < data.Length; i++){
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteACB((byte)devAddr, 220, 17, data) < 0)
                return -1;
            
            Thread.Sleep(100); // Wait command proecss

            if (i2cReadACB((byte)devAddr, 220, 17, baReadTmp) != 17)
                return -1;
            data = System.Text.Encoding.Default.GetBytes(newSerialNumberA);
            for (i = 0; i < 17; i++) {
                if (baReadTmp[i] != '\0') {
                    if (data[i] != baReadTmp[i]) {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("Set serial number fail!! Read(" +
                            sRead + ") != Write(" + newSerialNumberA + ")\n!!Please re-log!!");
                        return -1;
                    }
                }
            }

            return 0;
        }

        private int _SetModuleSerialNumberB()
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[17];
            byte[] baReadTmp = new byte[17];
            int devAddr, i;            

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteBCB((byte)devAddr, 127, 1, data) < 0)
                return -1;
                        
            tmp = System.Text.Encoding.Default.GetBytes(newSerialNumberB);
            
            if (tmp.Length > 17){
                MessageBox.Show("newSerialNumber length:" + tmp.Length + " > 17 Error!!");
                return -1;
            }
            for (i = 0; i < data.Length; i++){
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteBCB((byte)devAddr, 220, 17, data) < 0)
                return -1;

            Thread.Sleep(100); // Wait command proecss

            if (i2cReadBCB((byte)devAddr, 220, 17, baReadTmp) != 17)
                return -1;
            data = System.Text.Encoding.Default.GetBytes(newSerialNumberB);
            for (i = 0; i < 17; i++)
            {
                if (baReadTmp[i] != '\0')
                {
                    if (data[i] != baReadTmp[i])
                    {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("Set serial number fail!! Read(" +
                            sRead + ") != Write(" + newSerialNumberB + ")\n!!Please re-log!!");
                        return -1;
                    }
                }
            }

            return 0;
        }

        private int _StoreConfigIntoFlashA()
        {
            byte[] data = new byte[1];

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            if (i2cWriteACB(80, 162, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _StoreConfigIntoFlashB()
        {
            byte[] data = new byte[1];

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteBCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            if (i2cWriteBCB(80, 162, 1, data) < 0)
                return -1;

            return 0;
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            String[] saTmp;
            String sPartA = "A";
            String sPartB = "B";
            int iTmp;

            if (tbOperator.Text.Length == 0) {
                MessageBox.Show("Please input operator!!");
                return;
            }
            logModeSelect = cbLogMode.SelectedItem.ToString();

            if ((logModeSelect == "BeforeBurnIn") ||
                (logModeSelect == "Log"))
            {
                if (lastLogFileName.Length == 0) {
                    MessageBox.Show("Please input lot and sub-lot!!");
                    return;
                }
                if (tbSerialNumber.Text.Length < 1) {
                    MessageBox.Show("Please input SN!!");
                    return;
                }
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
            }
            
            if (!((tbRx1Threshold.Text.Equals("0")) && (tbRx2Threshold.Text.Equals("0")) &&
                (tbRx3Threshold.Text.Equals("0")) && (tbRx4Threshold.Text.Equals("0")))) {
                if ((tbARx1.Text == "0") && (tbARx2.Text == "0") && (tbARx3.Text == "0") && (tbARx4.Text == "0")) {
                    DialogResult drRxZero = MessageBox.Show("PartA-Rx value wrong. Please check be measure item!!\n(or select No ignore)", "Please select Yes or No",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drRxZero == DialogResult.Yes)
                        return;
                }

                if ((tbBRx1.Text == "0") && (tbBRx2.Text == "0") && (tbBRx3.Text == "0") && (tbBRx4.Text == "0"))
                {
                    DialogResult drRxZero = MessageBox.Show("PartB-Rx value wrong. Please check be measure item!!\n(or select No ignore)", "Please select Yes or No",
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

            while (tbSerialNumber.Text[0] == ' ')
                tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);
            if (tbSerialNumber.Text.Length != 4)
            {
                int.TryParse(tbSerialNumber.Text, out iTmp);
                tbSerialNumber.Text = iTmp.ToString("0000");
            }
            if (tbLotNumber.Text.Length == 8)
            {
                newSerialNumberA = tbLotNumber.Text + tbSubLotNumber.Text + tbSerialNumber.Text + sPartA;
                newSerialNumberB = tbLotNumber.Text + tbSubLotNumber.Text + tbSerialNumber.Text + sPartB;
            }
            else
            {
                lAction.Text = "LOT number wrong!!";
                MessageBox.Show("LOT number wrong!!");
                goto Error;
            }

            /*
            if (logModeSelect == "BeforeBurnIn") {
                while (tbSerialNumber.Text[0] == ' ')
                    tbSerialNumber.Text = tbSerialNumber.Text.Substring(1, tbSerialNumber.Text.Length - 1);
                if (tbSerialNumber.Text.Length != 4) {
                    int.TryParse(tbSerialNumber.Text, out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
                if (tbLotNumber.Text.Length == 8)
                    newSerialNumber = tbLotNumber.Text + tbSubLotNumber.Text + tbSerialNumber.Text;
                else {
                    lAction.Text = "LOT number wrong!!";
                    MessageBox.Show("LOT number wrong!!");
                    goto Error;
                }
            }
            else if (logModeSelect == "AfterBurnIn") {
                if (tbModuleSerialNumberA.Text.Length >= 15) {
                    if ((tbLotNumber.Text != tbModuleSerialNumberA.Text.Substring(0, 8)) ||
                        (tbSubLotNumber.Text != tbModuleSerialNumberA.Text.Substring(8, 3))) {
                        if (dtValue.Rows.Count > 0)
                            _SaveLogFile();
                        tbLotNumber.Text = tbModuleSerialNumberA.Text.Substring(0, 8);
                        tbSubLotNumber.Text = tbModuleSerialNumberA.Text.Substring(8, 3);
                        _OpenLogFile();
                    }
                    int.TryParse(tbModuleSerialNumberA.Text.Substring(11, 4), out iTmp);
                    tbSerialNumber.Text = iTmp.ToString("0000");
                }
            }
            */

            logValue = true;

            return;

        Error:
            bLog.Enabled = true;
            lAction.Text = "";
            return;
        }

        private int _ReadTemperatureValueA()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fTmp;
            int devAddr;

            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadACB((byte)devAddr, 22, 2, data) != 2)
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
            temperatureA = fTmp.ToString("0.0");

            return 0;

        clearData:
            temperatureA = "0";

            return 0;
        }

        private int _ReadTemperatureValueB()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fTmp;
            int devAddr;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadBCB((byte)devAddr, 22, 2, data) != 2)
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
            fTmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = fTmp / 256;
            temperatureB = fTmp.ToString("0.0");

            return 0;

        clearData:
            temperatureB = "0";

            return 0;
        }

        private int _ReadRxRssiValueA()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);
            int.TryParse(tbI2cRxRegisterPage.Text, out page);
            int.TryParse(tbI2cRxRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (i2cWriteACB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (i2cReadACB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxARssiValue[0] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxARssiValue[1] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxARssiValue[2] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxARssiValue[3] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            rxARssiValue[0] = rxARssiValue[1] = rxARssiValue[2] = rxARssiValue[3] = "0";

            return 0;
        }

        private int _ReadRxRssiValueB()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);
            int.TryParse(tbI2cRxRegisterPage.Text, out page);
            int.TryParse(tbI2cRxRegisterAddr.Text, out regAddr);

            if (page > 0)
            {
                data[0] = (byte)page;

                if (i2cWriteBCB((byte)devAddr, 127, 1, data) < 0)
                    goto clearData;
            }

            if (i2cReadBCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
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
            rxBRssiValue[0] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try
            {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxBRssiValue[1] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try
            {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxBRssiValue[2] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try
            {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            rxBRssiValue[3] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            rxBRssiValue[0] = rxBRssiValue[1] = rxBRssiValue[2] = rxBRssiValue[3] = "0";

            return 0;
        }

        private int _ReadRxPowerValueA()
        {
            byte[] data = data = new byte[] { 0, 0, 0, 0 };
            byte[] reverseData;
            int tmp;
            float power;

            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadACB(80, 34, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxAPowerValue[0] = power.ToString("#0.0");

            if (i2cReadACB(80, 36, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxAPowerValue[1] = power.ToString("#0.0");

            if (i2cReadACB(80, 38, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxAPowerValue[2] = power.ToString("#0.0");

            if (i2cReadACB(80, 40, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxAPowerValue[3] = power.ToString("#0.0");

            if (i2cReadACB(80, 244, 4, data) != 4)
                goto clearData;

            rxAPowerRate[0] = data[0].ToString();
            rxAPowerRate[1] = data[1].ToString();
            rxAPowerRate[2] = data[2].ToString();
            rxAPowerRate[3] = data[3].ToString();

            return 0;

        clearData:
            rxAPowerValue[0] = rxAPowerValue[1] = rxAPowerValue[2] = rxAPowerValue[3] = "0";
            rxAPowerRate[0] = rxAPowerRate[1] = rxAPowerRate[2] = rxAPowerRate[3] = "0";
            return 0;
        }

        private int _ReadRxPowerValueB()
        {
            byte[] data = data = new byte[] { 0, 0, 0, 0 };
            byte[] reverseData;
            int tmp;
            float power;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            if (i2cWriteBCB(80, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadBCB(80, 34, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxBPowerValue[0] = power.ToString("#0.0");

            if (i2cReadBCB(80, 36, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxBPowerValue[1] = power.ToString("#0.0");

            if (i2cReadBCB(80, 38, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxBPowerValue[2] = power.ToString("#0.0");

            if (i2cReadBCB(80, 40, 2, data) != 2)
                goto clearData;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            rxBPowerValue[3] = power.ToString("#0.0");

            if (i2cReadBCB(80, 244, 4, data) != 4)
                goto clearData;

            rxBPowerRate[0] = data[0].ToString();
            rxBPowerRate[1] = data[1].ToString();
            rxBPowerRate[2] = data[2].ToString();
            rxBPowerRate[3] = data[3].ToString();

            return 0;

        clearData:
            rxBPowerValue[0] = rxBPowerValue[1] = rxBPowerValue[2] = rxBPowerValue[3] = "0";
            rxBPowerRate[0] = rxBPowerRate[1] = rxBPowerRate[2] = rxBPowerRate[3] = "0";
            return 0;
        }

        private int _ReadSerialNumberValueA()
        {
            byte[] data = new byte[16];
            int devAddr;

            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteACB((byte)devAddr, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadACB((byte)devAddr, 220, 16, data) != 16)
                goto clearData;

            serialNumberA = System.Text.Encoding.Default.GetString(data);

            return 0;

        clearData:
            serialNumberA = "";

            return 0;
        }

        private int _ReadSerialNumberValueB()
        {
            byte[] data = new byte[16];
            int devAddr;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            if (i2cWriteBCB((byte)devAddr, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadBCB((byte)devAddr, 220, 16, data) != 16)
                goto clearData;

            serialNumberB = System.Text.Encoding.Default.GetString(data);

            return 0;

        clearData:
            serialNumberB = "";

            return 0;
        }

        private int _ReadLosValueA()
        {
            byte[] data = new byte[1];
            int devAddr;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);
            
            if (i2cReadACB((byte)devAddr, 3, 1, losStatusA) != 1)
                goto clearData;            

            clearData:
            return 0;
        }

        private int _ReadLosValueB()
        {
            byte[] data = new byte[1];
            int devAddr;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadBCB((byte)devAddr, 3, 1, losStatusB) != 1)
                goto clearData;

            clearData:
            return 0;
        }

        private int _ReadRxIcRssiA()
        {
            byte[] data = new byte[16];
            int devAddr, delay, tmp;

            delay = 100;

            if (i2cWriteACB == null)
                return -1;

            if (i2cReadACB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadACB((byte)devAddr, 0, 1, data) != 1)
                goto clearData;

            if (data[0] != 0x77)
                return -1;

            data[0] = 0x0F;
            if (i2cWriteACB((byte)devAddr, 0x16, 1, data) < 0)
                goto clearData;

            data[0] = 0xF0;
            if (i2cWriteACB((byte)devAddr, 0x40, 1, data) < 0)
                goto clearData;

            //RSSI0
            data[0] = 0xC0;
            if (i2cWriteACB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadACB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxARssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxARssiValue[0] = tmp.ToString();
            }

            //RSSI1
            data[0] = 0xD0;
            if (i2cWriteACB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadACB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;
            
            if (data[0] == 0)
                rxARssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxARssiValue[1] = tmp.ToString();
            }

            //RSSI2
            data[0] = 0xE0;
            if (i2cWriteACB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadACB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxARssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxARssiValue[2] = tmp.ToString();
            }

            //RSSI3
            data[0] = 0xF0;
            if (i2cWriteACB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadACB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxARssiValue[0] = "0";
            else {
                tmp = 4 + (8 * data[0]);
                rxARssiValue[3] = tmp.ToString();
            }

            return 0;

        clearData:
            rxARssiValue[0] = rxARssiValue[1] = rxARssiValue[2] = rxARssiValue[3] = "0";

            return 0;
        }

        private int _ReadRxIcRssiB()
        {
            byte[] data = new byte[16];
            int devAddr, delay, tmp;

            delay = 100;

            if (i2cWriteBCB == null)
                return -1;

            if (i2cReadBCB == null)
                return -1;

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (i2cReadBCB((byte)devAddr, 0, 1, data) != 1)
                goto clearData;

            if (data[0] != 0x77)
                return -1;

            data[0] = 0x0F;
            if (i2cWriteBCB((byte)devAddr, 0x16, 1, data) < 0)
                goto clearData;

            data[0] = 0xF0;
            if (i2cWriteBCB((byte)devAddr, 0x40, 1, data) < 0)
                goto clearData;

            //RSSI0
            data[0] = 0xC0;
            if (i2cWriteBCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadBCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxBRssiValue[0] = "0";
            else
            {
                tmp = 4 + (8 * data[0]);
                rxBRssiValue[0] = tmp.ToString();
            }

            //RSSI1
            data[0] = 0xD0;
            if (i2cWriteBCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadBCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxBRssiValue[0] = "0";
            else
            {
                tmp = 4 + (8 * data[0]);
                rxBRssiValue[1] = tmp.ToString();
            }

            //RSSI2
            data[0] = 0xE0;
            if (i2cWriteBCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadBCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxBRssiValue[0] = "0";
            else
            {
                tmp = 4 + (8 * data[0]);
                rxBRssiValue[2] = tmp.ToString();
            }

            //RSSI3
            data[0] = 0xF0;
            if (i2cWriteBCB((byte)devAddr, 0x03, 1, data) < 0)
                goto clearData;

            Thread.Sleep(delay);

            if (i2cReadBCB((byte)devAddr, 0x1A, 1, data) != 1)
                goto clearData;

            if (data[0] == 0)
                rxBRssiValue[0] = "0";
            else
            {
                tmp = 4 + (8 * data[0]);
                rxBRssiValue[3] = tmp.ToString();
            }

            return 0;

        clearData:
            rxBRssiValue[0] = rxBRssiValue[1] = rxBRssiValue[2] = rxBRssiValue[3] = "0";

            return 0;
        }

        private int _GetModuleMonitorValue()
        {
            bool rxValueReadError, txPowerReadError;

            rxValueReadError = txPowerReadError = false;
            switch (logModeSelect) {
                
                default:
                    if (_ReadTemperatureValueA() < 0)
                        rxValueReadError = true;
                    if (_ReadTemperatureValueB() < 0)
                        rxValueReadError = true;

                    if (_ReadRxRssiValueA() < 0)
                        rxValueReadError = true;
                    if (_ReadRxRssiValueB() < 0)
                        rxValueReadError = true;

                    if (_ReadRxPowerValueA() < 0)
                        rxValueReadError = true;
                    if (_ReadRxPowerValueB() < 0)
                        rxValueReadError = true;

                    if (_ReadSerialNumberValueA() < 0)
                        rxValueReadError = true;
                    if (_ReadSerialNumberValueB() < 0)
                        rxValueReadError = true;

                    if (_ReadLosValueA() < 0)
                        rxValueReadError = true;
                    if (_ReadLosValueB() < 0)
                        rxValueReadError = true;

                    if ((String.Compare(rxARssiValue[0], "65535") == 0) &&
                        (String.Compare(rxARssiValue[1], "65535") == 0) &&
                        (String.Compare(rxARssiValue[2], "65535") == 0) &&
                        (String.Compare(rxARssiValue[3], "65535") == 0) &&
                        (String.Compare(temperatureA, "0") != 0))

                    if ((String.Compare(rxBRssiValue[0], "65535") == 0) &&
                        (String.Compare(rxBRssiValue[1], "65535") == 0) &&
                        (String.Compare(rxBRssiValue[2], "65535") == 0) &&
                        (String.Compare(rxBRssiValue[3], "65535") == 0) &&
                        (String.Compare(temperatureB, "0") != 0))

                        _WritePassword();
                    break;
            }

            if (rxValueReadError || txPowerReadError)
                return -1;
            
            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;
            int iDcBias, iBurnInCurrent;

            while (monitorStart) {
                bGetModuleMonitorValueError = false;

                if (logValue == true) {
                    int.TryParse(tbBeforeAndAfterBurnInDcTestBiasCurrent.Text, out iDcBias);
                    int.TryParse(tbBurnInBiasCurrent.Text, out iBurnInCurrent);
                    switch (logModeSelect) {
                        
                        case "BeforeBurnIn":
                            bwMonitor.ReportProgress(1, null);
                      
                            if (_SetBias(iDcBias, false) < 0) {
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
                            if ((_SetModuleSerialNumberA() < 0) || (_SetBias(iBurnInCurrent, true) < 0) ||
                                (_StoreConfigIntoFlashA() < 0)) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AfterBurnIn":
                            bwMonitor.ReportProgress(5, null);
                            if ((_SetBias(iDcBias, false) < 0) || (_StoreConfigIntoFlashA() < 0)) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(2, null);
                            Thread.Sleep(200); // Wait value stable

                            bwMonitor.ReportProgress(3, null);
                            if (_GetModuleMonitorValue() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            acConfigRowCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterBurnInAcConfigA();
                            _SetAfterBurnInAcConfigB();

                            if (_AutoCorrectRxPowerRate() < 0) {
                                bGetModuleMonitorValueError = true;
                                break;
                            }

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        case "AcConfig":
                            acConfigRowCount = 0;
                            bwMonitor.ReportProgress(6, null);
                            _SetAfterBurnInAcConfigA();
                            _SetAfterBurnInAcConfigB();

                            bwMonitor.ReportProgress(10, null);
                            while (logValue == true) // Wait log done
                                Thread.Sleep(100);
                            break;

                        default:
                            if (!((tbARx1.Text == "0") && (tbARx2.Text == "0") && (tbARx3.Text == "0") && (tbARx4.Text == "0")
                                && (tbTemperatureA.Text == "0")))
                            {
                                if ((_SetModuleSerialNumberA() < 0) || (_StoreConfigIntoFlashA() < 0) ||
                                (_GetModuleMonitorValue() < 0)) {
                                    bGetModuleMonitorValueError = true;
                                    break;
                                }
                            }

                            if (!((tbBRx1.Text == "0") && (tbBRx2.Text == "0") && (tbBRx3.Text == "0") && (tbBRx4.Text == "0")
                                && (tbTemperatureB.Text == "0")))
                            {
                                if ((_SetModuleSerialNumberB() < 0) || (_StoreConfigIntoFlashB() < 0) ||
                                (_GetModuleMonitorValue() < 0))
                                {
                                    bGetModuleMonitorValueError = true;
                                    break;
                                }

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

            if (i2cWriteACB == null)
                return -1;
            if (i2cWriteBCB == null)
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

            if (i2cWriteACB(80, 123, 4, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }        

        private int _StorePowerRateConfig()
        {
            byte[] data = new byte[1];

            lAction.Text = "Sotre Rx power rate...";
            lAction.Update();

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x20;
            else
                data[0] = 0xAA;

            if (i2cWriteACB(80, 127, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            if (i2cWriteACB(80, 162, 1, data) < 0)
                return -1;
            if (i2cWriteBCB(80, 162, 1, data) < 0)
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
            
            if (i2cWriteACB == null)
                return -1;
            if (i2cWriteBCB == null)
                return -1;

            data = new byte[4];

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x04;
            else
                data[0] = 0x80;

            i2cWriteACB(80, 127, 1, data);
            i2cWriteBCB(80, 127, 1, data);

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

            i2cWriteACB(80, 244, 4, data);
            i2cWriteBCB(80, 244, 4, data);

            _StorePowerRateConfig(); 

            Thread.Sleep(1000);

            return 0;
        }

        private int _SetAfterBurnInAcConfigA()
        {
            byte[] data = new byte[1];
            byte devAddr, regAddr;
            int delayTime;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (_WritePassword() < 0)
                return -1;

            if (i2cWriteACB == null)
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
                        i2cWriteACB(devAddr, regAddr, 1, data);
                        break;

                    case "Read":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        while (i2cReadACB(devAddr, regAddr, 1, data) != 1) {
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

        private int _SetAfterBurnInAcConfigB()
        {
            byte[] data = new byte[1];
            byte devAddr, regAddr;
            int delayTime;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (_WritePassword() < 0)
                return -1;

            if (i2cWriteBCB == null)
                return -1;

            foreach (DataRow row in dtAfterBurnInConfig.Rows)
            {
                acConfigRowCount++;
                switch (row[0].ToString())
                {
                    case "Delay10mSec":
                        delayTime = int.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        bwMonitor.ReportProgress(6, null);
                        Thread.Sleep(delayTime);
                        break;

                    case "Write":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data[0] = byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        i2cWriteBCB(devAddr, regAddr, 1, data);
                        break;

                    case "Read":
                        devAddr = byte.Parse(row[1].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(row[2].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber);
                        while (i2cReadBCB(devAddr, regAddr, 1, data) != 1)
                        {
                            MessageBox.Show("i2cReadCB() fail!!");
                            Thread.Sleep(100);
                        }
                        if (data[0] != byte.Parse(row[3].ToString().Substring(2), System.Globalization.NumberStyles.HexNumber))
                        {
                            MessageBox.Show("DevAddr:0x" + devAddr.ToString("X2") + "RegAddr:0x" + regAddr.ToString("X2") +
                                "Value:0x" + data[0].ToString("X2") + " != " + row[3].ToString());
                            return -1;
                        }
                        break;

                    default:
                        break;
                }

                if (acConfigRowCount % 5 == 0)
                {
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
                rssi = Convert.ToSingle(tbARx1.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx1PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx2InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbARx2.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx2PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx3InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbARx3.Text);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                MessageBox.Show(eCT.ToString());
            }
            tbRx3PowerRate.Text = rate.ToString();

            try {
                input = Convert.ToSingle(tbRx4InputPowerRssi.Text);
                rssi = Convert.ToSingle(tbARx4.Text);
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
                
        private int _UpdateRxRssiValueGuiA()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRx1Threshold.Text);
            fTmp = float.Parse(rxARssiValue[0]);
            if (fTmp < fThreshold)
            {
                tbARx1.ForeColor = System.Drawing.Color.Red;
                tbARx1.BackColor = System.Drawing.Color.Pink;
             }
            else
            {
                tbARx1.ForeColor = SystemColors.ControlText;
                tbARx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(178)))), ((int)(((byte)(137)))));
            }
            tbARx1.Text = rxARssiValue[0];
            tbARx1.Update();


            fThreshold = float.Parse(tbRx2Threshold.Text);
            fTmp = float.Parse(rxARssiValue[1]);
            if (fTmp < fThreshold)
            {
                tbARx2.ForeColor = System.Drawing.Color.Red;
                tbARx2.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbARx2.ForeColor = SystemColors.ControlText;
                tbARx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(178)))), ((int)(((byte)(137)))));
            }
            tbARx2.Text = rxARssiValue[1];
            tbARx2.Update();
 
            fThreshold = float.Parse(tbRx3Threshold.Text);
            fTmp = float.Parse(rxARssiValue[2]);
            if (fTmp < fThreshold)
            {
                tbARx3.ForeColor = System.Drawing.Color.Red;
                tbARx3.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbARx3.ForeColor = SystemColors.ControlText;
                tbARx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(178)))), ((int)(((byte)(137)))));
            }
            tbARx3.Text = rxARssiValue[2];
            tbARx3.Update();

            fThreshold = float.Parse(tbRx4Threshold.Text);
            fTmp = float.Parse(rxARssiValue[3]);
            if (fTmp < fThreshold)
            {
                tbARx4.ForeColor = System.Drawing.Color.Red;
                tbARx4.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbARx4.ForeColor = SystemColors.ControlText;
                tbARx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(178)))), ((int)(((byte)(137)))));
            }
            tbARx4.Text = rxARssiValue[3];
            tbARx4.Update();
 
            return 0;
        }

        private int _UpdateRxRssiValueGuiB()
        {
            float fTmp, fThreshold;

            fThreshold = float.Parse(tbRx1Threshold.Text);
            fTmp = float.Parse(rxBRssiValue[0]);
            if (fTmp < fThreshold)
            {
                tbBRx1.ForeColor = System.Drawing.Color.Red;
                tbBRx1.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbBRx1.ForeColor = SystemColors.ControlText;
                tbBRx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(218)))), ((int)(((byte)(177)))));
            }
            tbBRx1.Text = rxBRssiValue[0];
            tbBRx1.Update();

            fThreshold = float.Parse(tbRx2Threshold.Text);
            fTmp = float.Parse(rxBRssiValue[1]);
            if (fTmp < fThreshold)
            {
                tbBRx2.ForeColor = System.Drawing.Color.Red;
                tbBRx2.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbBRx2.ForeColor = SystemColors.ControlText;
                tbBRx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(218)))), ((int)(((byte)(177)))));
            }
            tbBRx2.Text = rxBRssiValue[1];
            tbBRx2.Update();

            fThreshold = float.Parse(tbRx3Threshold.Text);
            fTmp = float.Parse(rxBRssiValue[2]);
            if (fTmp < fThreshold)
            {
                tbBRx3.ForeColor = System.Drawing.Color.Red;
                tbBRx3.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbBRx3.ForeColor = SystemColors.ControlText;
                tbBRx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(218)))), ((int)(((byte)(177)))));
            }
            tbBRx3.Text = rxBRssiValue[2];
            tbBRx3.Update();

            fThreshold = float.Parse(tbRx4Threshold.Text);
            fTmp = float.Parse(rxBRssiValue[3]);
            if (fTmp < fThreshold)
            {
                tbBRx4.ForeColor = System.Drawing.Color.Red;
                tbBRx4.BackColor = System.Drawing.Color.Pink;
            }
            else
            {
                tbBRx4.ForeColor = SystemColors.ControlText;
                tbBRx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(218)))), ((int)(((byte)(177)))));
            }
            tbBRx4.Text = rxBRssiValue[3];
            tbBRx4.Update();

            return 0;
        }


        private int _UpdateModuleSerailNumberValueGui()
        {
            tbModuleSerialNumberA.Text = serialNumberA;
            tbModuleSerialNumberA.Update();
            
            tbModuleSerialNumberB.Text = serialNumberB;
            tbModuleSerialNumberB.Update();
            

            return 0;
        }

        private int _UpdateLosStatusGui()
        {
            if ((losStatusA[0] & 0x01) == 0)                
                tbARx1Los.BackColor = Color.LightGreen;            
            else                
                tbARx1Los.BackColor = Color.Pink;
            
            if ((losStatusA[0] & 0x02) == 0)
                tbARx2Los.BackColor = Color.LightGreen;
            else
                tbARx2Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x04) == 0)
                tbARx3Los.BackColor = Color.LightGreen;
            else
                tbARx3Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x08) == 0)
                tbARx4Los.BackColor = Color.LightGreen;
            else
                tbARx4Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x10) == 0)
                tbATx1Los.BackColor = Color.LightGreen;
            else
                tbATx1Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x20) == 0)
                tbATx2Los.BackColor = Color.LightGreen;
            else
                tbATx2Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x40) == 0)
                tbATx3Los.BackColor = Color.LightGreen;
            else
                tbATx3Los.BackColor = Color.Pink;

            if ((losStatusA[0] & 0x80) == 0)
                tbATx4Los.BackColor = Color.LightGreen;
            else
                tbATx4Los.BackColor = Color.Pink;


            if ((losStatusB[0] & 0x01) == 0)
                tbBRx1Los.BackColor = Color.LightGreen;
            else
                tbBRx1Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x02) == 0)
                tbBRx2Los.BackColor = Color.LightGreen;
            else
                tbBRx2Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x04) == 0)
                tbBRx3Los.BackColor = Color.LightGreen;
            else
                tbBRx3Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x08) == 0)
                tbBRx4Los.BackColor = Color.LightGreen;
            else
                tbBRx4Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x10) == 0)
                tbBTx1Los.BackColor = Color.LightGreen;
            else
                tbBTx1Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x20) == 0)
                tbBTx2Los.BackColor = Color.LightGreen;
            else
                tbBTx2Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x40) == 0)
                tbBTx3Los.BackColor = Color.LightGreen;
            else
                tbBTx3Los.BackColor = Color.Pink;

            if ((losStatusB[0] & 0x80) == 0)
                tbBTx4Los.BackColor = Color.LightGreen;
            else
                tbBTx4Los.BackColor = Color.Pink;


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
                    lAction.Text = "Set burn-in bias and store ...";
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
                        
            tbTemperatureA.Text = temperatureA;
            tbTemperatureA.Update();
            tbTemperatureB.Text = temperatureB;
            tbTemperatureB.Update();
            _UpdateRxRssiValueGuiA();
            _UpdateRxRssiValueGuiB();
            _UpdateModuleSerailNumberValueGui();
            _UpdateLosStatusGui();

            tbRx1Power.Text = rxAPowerValue[0];
            tbRx1Power.Update();
            tbRx2Power.Text = rxAPowerValue[1];
            tbRx2Power.Update();
            tbRx3Power.Text = rxAPowerValue[2];
            tbRx3Power.Update();
            tbRx4Power.Text = rxAPowerValue[3];
            tbRx4Power.Update();

            tbRx1PowerRate.Text = rxAPowerRate[0];
            tbRx1PowerRate.Update();
            tbRx2PowerRate.Text = rxAPowerRate[1];
            tbRx2PowerRate.Update();
            tbRx3PowerRate.Text = rxAPowerRate[2];
            tbRx3PowerRate.Update();
            tbRx4PowerRate.Text = rxAPowerRate[3];
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
            }
            
        }

        private void _SaveLogFile()
        {
            StreamWriter swLog;

            if (lastLogFileName == "")
                return;

            swLog = new StreamWriter(tbLogFilePath.Text);
            swLog.WriteLine("Lable,Time,SN,ARx1,ARx2,ARx3,ARx4,BRx1,BRx2,BRx3,BRx4,TemperatureA,TemperatureB,Operator,Grade,Note");
            foreach (DataRow row in dtValue.Rows) {
                swLog.WriteLine(row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," +
                    row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString() + "," +
                    row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString() + "," +
                    row[9].ToString() + "," + row[10].ToString() + "," + row[11].ToString() + "," +
                    row[12].ToString() + "," + row[13].ToString() + "," + row[14].ToString() + "," + 
                    row[15].ToString());
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
                if (saItems.Length < 16)
                    continue;

                if (saItems.Length == 16){
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                                    saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], 
                                    saItems[10], saItems[11], saItems[12], saItems[13], saItems[14], saItems[15]);
                }
/*
                if (saItems.Length == 17) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], "", "", "", "", "", saItems[15], saItems[16]);
                }
                else if (saItems.Length == 22) { //Old version log
                    dtValue.Rows.Add(saItems[0], saItems[1], saItems[2], saItems[3], saItems[4],
                        saItems[5], saItems[6], saItems[7], saItems[8], saItems[9], saItems[10],
                        saItems[11], saItems[12], saItems[13], saItems[14], saItems[15], saItems[16], saItems[17],
                        saItems[18], saItems[19], saItems[20], saItems[21]);
                }
*/
            }

            srLog.Close();
            dgvRecord.Sort(dgvRecord.Columns[1], ListSortDirection.Descending);
            bLog.Enabled = true;
            _UpdateDataListStatus();
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
            logModeSelect = cbLogMode.SelectedItem.ToString();
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
                    xwConfig.WriteElementString("BurnInBiasCurrent", tbBurnInBiasCurrent.Text);
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

                    xwConfig.WriteStartElement("MiscConfig");
                    {
                        xwConfig.WriteElementString("CustomerPage", cbCustomerPage.Text);
                        xwConfig.WriteElementString("IgnoreRxLos", cbIgnoreRxLos.Checked.ToString());
                        xwConfig.WriteElementString("IgnoreTxLos", cbIgnoreTxLos.Checked.ToString());
                    }
                    xwConfig.WriteEndElement(); //CustomerPageConfig

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

                        case "BurnInBiasCurrent":
                            tbBurnInBiasCurrent.Text = reader.ReadElementContentAsString();
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

                        case "MiscConfig":
                            _PaserMiscConfigXml(reader);
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

            ofdSelectFile.Title = "Select config file";
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

        private void initialConfigFile()
        {
            String initialFilePath = "C:\\DcTestLog\\Default.xml";

            tbConfigFilePath.Text = initialFilePath;
            tbConfigFilePath.SelectionStart = tbConfigFilePath.Text.Length;
            tbConfigFilePath.ScrollToCaret();

            using (XmlReader xrConfig = XmlReader.Create(initialFilePath))
            {
                while (xrConfig.Read())
                {
                    if (xrConfig.IsStartElement())
                    {
                        switch (xrConfig.Name)
                        {
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

        private void clearSerialNumberFromFlash(object sender, EventArgs e)
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[17];
            byte[] baReadTmp = new byte[17];
            int devAddr, i;

            bClear.Enabled = false;
            lAction.Text = "";
            lAction.Text = "Processing...";

            _WritePassword();
            _SetQsfpMode(0x4D);

            int.TryParse(tbI2cRxDevAddr.Text, out devAddr);

            if (cbCustomerPage.SelectedIndex == 0)
                data[0] = 0x05;
            else
                data[0] = 0x81;

            lAction.Text = "";
            if (tbModuleSerialNumberA.Text != "")
            {
                i2cWriteACB((byte)devAddr, 127, 1, data);
                newSerialNumberA = "";
                tmp = System.Text.Encoding.Default.GetBytes(newSerialNumberA);
                for (i = 0; i < data.Length; i++)
                {
                    if (i < tmp.Length)
                        data[i] = tmp[i];
                    else
                        data[i] = 0;
                }

                i2cWriteACB((byte)devAddr, 220, 17, data);
                Thread.Sleep(100); // Wait command proecss
                tbModuleSerialNumberA.Update();
            }
            else
                lAction.Text = "Part A is missing!!\n";

            if (tbModuleSerialNumberA.Text != "")
            {
                i2cWriteBCB((byte)devAddr, 127, 1, data);
                newSerialNumberB = "";
                tmp = System.Text.Encoding.Default.GetBytes(newSerialNumberB);
                for (i = 0; i < data.Length; i++)
                {
                    if (i < tmp.Length)
                        data[i] = tmp[i];
                    else
                        data[i] = 0;
                }

                i2cWriteBCB((byte)devAddr, 220, 17, data);
                Thread.Sleep(100); // Wait command proecss
                tbModuleSerialNumberB.Update();
            }
            else
                lAction.Text += "Part B is missing!!\n";

            bClear.Enabled = true;
            lAction.Text += "Clear done of the serial number";
            return ;

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

            if (tbSerialNumber.Text.Length > 4){
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

        private void lTemperature_Click(object sender, EventArgs e)
        {

        }

        private void tbTemperature_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbAutoLogWithLableTemperature_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbLogFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbInformation_Enter(object sender, EventArgs e)
        {

        }

        private void lTemperatureA_Click(object sender, EventArgs e)
        {

        }

        private void tbTemperatureA_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSerialNumber_TextChanged(object sender, EventArgs e)
        {
            lClassification.Text = "";
            lResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lResult.Text = "";
        }
    }
}
