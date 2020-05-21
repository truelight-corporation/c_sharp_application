using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QsfpPlus40gSr4SerialNumberWiter
{
    public partial class UcSerialNumberWriter : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private BackgroundWorker bwMonitor;
        private DialogResult drAskOverwrite;
        private DataTable dtTable = new DataTable();
        private volatile String sYcSerialNumber, sXaSerialNumber, sNewSerialNumber;
        private String fileDirectory = "d:\\XaSerialNumberTable";
        private String lastLogFileName = "";
        private volatile bool monitorStart = false;
        private volatile bool writeSerialNumber = false;

        public UcSerialNumberWriter()
        {
            InitializeComponent();

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);

            dtTable.Columns.Add("XA序號", typeof(String));
            dtTable.Columns.Add("YC序號", typeof(String));

            dgvTable.DataSource = dtTable;
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

        public int StartMonitorApi()
        {
            if (monitorStart == true)
                return 0;

            monitorStart = true;
            bwMonitor.RunWorkerAsync();

            return 0;
        }

        public int StopMonitorApi()
        {
            if (monitorStart == false)
                return 0;

            monitorStart = false;

            return 0;
        }

        private int _GetModuleSerialValue()
        {
            byte[] data = new byte[16];

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 5;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            if (i2cReadCB(80, 220, 16, data) != 16)
                goto clearData;

            sYcSerialNumber = System.Text.Encoding.Default.GetString(data);

            if (i2cReadCB(80, 236, 16, data) != 16)
                goto clearData;

            sXaSerialNumber = System.Text.Encoding.Default.GetString(data);

            return 0;

        clearData:
            sYcSerialNumber = "";

            return 0;
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
                data[0] = (byte)Convert.ToInt32(tbPassword123.Text, 16);
                data[1] = (byte)Convert.ToInt32(tbPassword124.Text, 16);
                data[2] = (byte)Convert.ToInt32(tbPassword125.Text, 16);
                data[3] = (byte)Convert.ToInt32(tbPassword126.Text, 16);
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
                return -1;
            }

            if (i2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
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

        private void _StoreIntoFlash()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                goto exit;

            data[0] = 32;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                goto exit;

        exit:
            _SetQsfpMode(0);
        }

        private int _SetModuleYcSerialNumber()
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[16];
            byte[] baReadTmp = new byte[16];
            int i;

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 5;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            tmp = System.Text.Encoding.Default.GetBytes(tbYcSerialNumber.Text);

            for (i = 0; i < 16; i++)
            {
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteCB(80, 220, 16, data) < 0)
                return -1;

            if (i2cReadCB(80, 220, 16, baReadTmp) != 16)
                return -1;

            for (i = 0; i < 16; i++)
            {
                if (baReadTmp[i] != '\0')
                {
                    if (data[i] != baReadTmp[i])
                    {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("設定 YC serial number 失敗!! 讀(" +
                            sRead + ") != 寫(" + tbYcSerialNumber.Text + ")\n!!請重新記錄!!");
                        return -1;
                    }
                }
            }

            _StoreIntoFlash();

            return 0;
        }

        private int _SetModuleXaSerialNumber()
        {
            String sRead;
            byte[] tmp;
            byte[] data = new byte[16];
            byte[] baReadTmp = new byte[16];
            int i;

            if (_WritePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 5;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            tmp = System.Text.Encoding.Default.GetBytes(sNewSerialNumber);

            for (i = 0; i < 16; i++) {
                if (i < tmp.Length)
                    data[i] = tmp[i];
                else
                    data[i] = 0;
            }

            if (i2cWriteCB(80, 236, 16, data) < 0)
                return -1;
            
            if (i2cReadCB(80, 236, 16, baReadTmp) != 16)
                return -1;

            for (i = 0; i < 16; i++) {
                if (baReadTmp[i] != '\0') {
                    if (data[i] != baReadTmp[i]) {
                        sRead = System.Text.Encoding.Default.GetString(baReadTmp);
                        MessageBox.Show("設定 XA serial number 失敗!! 讀(" +
                            sRead + ") != 寫(" + tbNewSerialNumber.Text + ")\n!!請重新記錄!!");
                        return -1;
                    }
                }
            }

            _StoreIntoFlash();

            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;

            while (monitorStart) {
                bGetModuleMonitorValueError = false;

                if (writeSerialNumber == true) {
                    bwMonitor.ReportProgress(1, null);
                    if (tbYcSerialNumber.ReadOnly == false)
                        _SetModuleYcSerialNumber();
                    _SetModuleXaSerialNumber();
                    Thread.Sleep(1000); // Wait value stable
                    bwMonitor.ReportProgress(10, null);
                    while (writeSerialNumber == true) // Wait log done
                        Thread.Sleep(100);
                }
                else {
                    if (_GetModuleSerialValue() < 0)
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

        private int _CheckDuplicationEntry(String xaSerialNumber)
        {
            DataRow[] filteredRows = dtTable.Select("XA序號 = '" + xaSerialNumber + "'");

            if (filteredRows.Length == 0)
                return 0;

            drAskOverwrite = MessageBox.Show("是否覆蓋舊紀錄?", "發現重複紀錄", MessageBoxButtons.YesNo);
            if (drAskOverwrite == DialogResult.Yes)
                dtTable.Rows.RemoveAt(dtTable.Rows.IndexOf(filteredRows[0]));
            else
                return -1;

            return 0;
        }

        private void AddEntryIntoTable()
        {
            if (_CheckDuplicationEntry(sNewSerialNumber) < 0)
                return;

            dtTable.Rows.Add(sNewSerialNumber, tbYcSerialNumber.Text);
            dgvTable.FirstDisplayedScrollingRowIndex = 0;
        }

        private void bDelRecord_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvTable.SelectedRows) {
                dgvTable.Rows.RemoveAt(item.Index);
            }
        }

        private void _SaveTableToFile()
        {
            StreamWriter swLog;

            if (lastLogFileName == "")
                return;

            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(fileDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(fileDirectory);
                }
            }
            catch (Exception e) {
                if (e != null)
                    return;
                return;
            }

            swLog = new StreamWriter(tbTableSavePath.Text);
            swLog.WriteLine("XA Number,YC Number");
            foreach (DataRow row in dtTable.Rows) {
                swLog.WriteLine(row[0].ToString() + "," + row[1].ToString());
            }
            swLog.Close();
            lastLogFileName = "";
            tbTableSavePath.Text = fileDirectory;
            dtTable.Clear();
        }

        private void bSaveTable_Click(object sender, EventArgs e)
        {
            if (dtTable.Rows.Count > 0)
                _SaveTableToFile();
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage) {
                case 1:
                    lStatus.Text = "Set XA serial number ...";
                    lStatus.Update();
                    return;

                case 10:
                    lStatus.Text = "Write into table ...";
                    lStatus.Update();
                    break;

                default:
                    break;
            }

            if (writeSerialNumber == false) {
                if ((sYcSerialNumber.IndexOf("YC") == 0)) {
                    tbYcSerialNumber.ReadOnly = true;
                    tbYcSerialNumber.BackColor = System.Drawing.Color.DarkSeaGreen;
                    tbYcSerialNumber.Text = sYcSerialNumber;
                    tbYcSerialNumber.Update();
                    bWriteXaSerialNumber.Enabled = true;
                }
                else {
                    String tmp;
                    tbYcSerialNumber.ReadOnly = false;
                    tbYcSerialNumber.BackColor = System.Drawing.Color.YellowGreen;
                    if (tbYcSerialNumber.Text.Length != 15)
                        bWriteXaSerialNumber.Enabled = false;
                    else
                        bWriteXaSerialNumber.Enabled = true;
                    if (tbYcSerialNumber.Text.Length == 17) {
                        tmp = tbYcSerialNumber.Text;
                        tbYcSerialNumber.Text = tmp.Substring(0, 8) + tmp.Substring(9, 3) +
                            tmp.Substring(13, 4);
                        tbYcSerialNumber.Update();
                    }
                }
            }

            tbXaSerialNumber.Text = sXaSerialNumber;
            tbXaSerialNumber.Update();

            if ((writeSerialNumber == true) && (e.ProgressPercentage == 10)) {
                AddEntryIntoTable();
                sNewSerialNumber = "";
                writeSerialNumber = false;
                bWriteXaSerialNumber.Enabled = true;
                lStatus.Text = "Entry Added.";
                lStatus.Update();
                tbNewSerialNumber.Focus();
                tbNewSerialNumber.SelectAll();
            }
        }

        private void _OpenTableFile()
        {
            StreamReader srLog;
            String sLine;
            String[] saItems;
            bool swap = false;

            if (tbYcSerialNumber.Text.Length < 15)
                return;

            lastLogFileName = tbNewSerialNumber.Text.Substring(0, 8) + "-" + tbNewSerialNumber.Text.Substring(9, 3) + ".csv";
            tbTableSavePath.Text = fileDirectory + "\\" + lastLogFileName;
            tbTableSavePath.Update();

            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(fileDirectory)) {
                    DirectoryInfo di = Directory.CreateDirectory(fileDirectory);
                }
            }
            catch (Exception e) {
                if (e != null)
                    return;
                return;
            }

            try {
                srLog = new StreamReader(tbTableSavePath.Text);
            }
            catch (FileNotFoundException e) {
                if (e != null)
                    return;
                return;
            }

            if ((sLine = srLog.ReadLine()) == null)  //Header
                return;

            if (sLine.Length > 0) {
                if (sLine[0].Equals('Y'))
                    swap = true;
                else
                    swap = false;
            }

            while ((sLine = srLog.ReadLine()) != null)
            { //Record
                saItems = sLine.Split(',');
                if (saItems.Length < 2)
                    continue;

                if (swap == true)
                    dtTable.Rows.Add(saItems[1], saItems[0]);
                else
                    dtTable.Rows.Add(saItems[0], saItems[1]);
            }

            srLog.Close();
            dgvTable.Sort(dgvTable.Columns[0], ListSortDirection.Descending);
        }

        private void bWriteXaSerialNumber_Click(object sender, EventArgs e)
        {
            if ((tbNewSerialNumber.Text.Length != 17) ||
                (!tbNewSerialNumber.Text[0].Equals('X')) ||
                (!tbNewSerialNumber.Text[1].Equals('A')) ||
                (!tbNewSerialNumber.Text[8].Equals('-')) ||
                (!tbNewSerialNumber.Text[12].Equals('-'))) {
                MessageBox.Show("輸入序號格式錯誤 XAxxxxxx-xxx-xxxx !!");
                tbNewSerialNumber.Text = "";
                return;
            }

            if (tbYcSerialNumber.Text.Length < 15) {
                MessageBox.Show("模組YC序號長度異常!!");
                return;
            }

            if (lastLogFileName.Length != 0) {
                if (!lastLogFileName.Substring(0, 8).Equals(tbNewSerialNumber.Text.Substring(0, 8)) ||
                    !lastLogFileName.Substring(9, 3).Equals(tbNewSerialNumber.Text.Substring(9, 3)))
                    _SaveTableToFile();
            }
            else    
                _OpenTableFile();

            bWriteXaSerialNumber.Enabled = false;

            sNewSerialNumber = tbNewSerialNumber.Text.Substring(0, 8) + tbNewSerialNumber.Text.Substring(9, 3) + 
                tbNewSerialNumber.Text.Substring(13, 4);

            writeSerialNumber = true;
        }

        public void SaveFileCheckApi(object sender, EventArgs e)
        {
            bSaveTable_Click(sender, e);
        }

        private void tbYcSerialNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbXaSerialNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void l_Click(object sender, EventArgs e)
        {

        }

        private void tbPassword126_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPassword125_TextChanged(object sender, EventArgs e)
        {

        }

        public int SetFocusOnLogFilePathApi()
        {
            tbTableSavePath.Focus();
            return 0;
        }

        public int SpaceKeyDownApi(object sender, KeyEventArgs e)
        {
            if (bWriteXaSerialNumber.Enabled == false)
                return -1;

            bWriteXaSerialNumber_Click(sender, e);

            return 0;
        }
    }
}
