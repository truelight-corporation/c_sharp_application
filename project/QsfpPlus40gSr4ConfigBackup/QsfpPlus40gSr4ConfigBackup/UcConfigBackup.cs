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
    public partial class UcConfigBackup : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private BackgroundWorker bwBackup;
        private volatile String sYcSerialNumber, sXaSerialNumber, sNewSerialNumber;
        private String fileDirectory = "d:\\XaConfigBackup";
        private byte[] configBackupLowPage = new byte[128];
        private byte[] configBackupUpPage0 = new byte[128];
        private byte[] configBackupUpPage2 = new byte[128];
        private byte[] configBackupUpPage3 = new byte[128];
        private byte[] configBackupUpPage4 = new byte[128];
        private byte[] configBackupUpPage5 = new byte[128];
        private volatile bool backupStart = false;
        private volatile bool writeXaSerialNumber = false;
        private volatile bool backupConfigToFile = false;
        private volatile bool restoreConfigFromFile = false;

        public UcConfigBackup()
        {
            InitializeComponent();

            bwBackup = new BackgroundWorker();
            bwBackup.WorkerReportsProgress = true;
            bwBackup.WorkerSupportsCancellation = false;
            bwBackup.DoWork += new DoWorkEventHandler(BackupConfigApi);
            bwBackup.ProgressChanged += new ProgressChangedEventHandler(BackupConfigProgressChangedApi);
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

        public int StartBackupApi()
        {
            if (backupStart == true)
                return 0;

            backupStart = true;
            bwBackup.RunWorkerAsync();

            return 0;
        }

        public int StopBackupApi()
        {
            if (backupStart == false)
                return 0;

            backupStart = false;

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

        private int _ReadLowPage()
        {
            int i;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 0, 128, configBackupLowPage) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupLowPage[i] = 0;

            return -1;
        }

        private int _ReadUpPage0()
        {
            byte[] data = new byte[1];
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 0;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cReadCB(80, 128, 128, configBackupUpPage0) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupUpPage0[i] = 0;

            return -1;
        }

        private int _ReadUpPage2()
        {
            byte[] data = new byte[1];
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 2;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cReadCB(80, 128, 128, configBackupUpPage2) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupUpPage2[i] = 0;

            return -1;
        }

        private int _ReadUpPage3()
        {
            byte[] data = new byte[1];
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cReadCB(80, 128, 128, configBackupUpPage3) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupUpPage3[i] = 0;

            return -1;
        }

        private int _ReadUpPage4()
        {
            byte[] data = new byte[1];
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 4;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cReadCB(80, 128, 128, configBackupUpPage4) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupUpPage4[i] = 0;

            return -1;
        }

        private int _ReadUpPage5()
        {
            byte[] data = new byte[1];
            int i;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 5;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cReadCB(80, 128, 128, configBackupUpPage5) != 128)
                goto clearData;

            return 0;

        clearData:
            for (i = 0; i < 128; i++)
                configBackupUpPage5[i] = 0;

            return -1;
        }

        private int _WriteLowPage()
        {
            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(80, 0, 119, configBackupLowPage) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        private int _WriteUpPage0()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cWriteCB(80, 128, 128, configBackupUpPage0) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        private int _WriteUpPage2()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 2;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cWriteCB(80, 128, 128, configBackupUpPage2) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        private int _WriteUpPage3()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cWriteCB(80, 128, 128, configBackupUpPage3) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        private int _WriteUpPage4()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 4;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cWriteCB(80, 128, 128, configBackupUpPage4) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        private int _WriteUpPage5()
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 5;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); // Wait chage page

            if (i2cWriteCB(80, 128, 127, configBackupUpPage5) < 0)
                return -1;

            Thread.Sleep(100); // Wait command prcoess

            return 0;
        }

        public void BackupConfigApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;

            while (backupStart) {
                bGetModuleMonitorValueError = false;

                if (backupConfigToFile == true)
                {
                    if (writeXaSerialNumber == true)
                    {
                        bwBackup.ReportProgress(1, null);
                        _SetModuleXaSerialNumber();
                        Thread.Sleep(1000); // Wait value stable
                        writeXaSerialNumber = false;
                    }

                    bwBackup.ReportProgress(2, null);
                    _ReadLowPage();

                    bwBackup.ReportProgress(3, null);
                    _ReadUpPage0();

                    bwBackup.ReportProgress(4, null);
                    _ReadUpPage2();

                    bwBackup.ReportProgress(5, null);
                    _ReadUpPage3();

                    bwBackup.ReportProgress(6, null);
                    _ReadUpPage4();

                    bwBackup.ReportProgress(7, null);
                    _ReadUpPage5();

                    bwBackup.ReportProgress(10, null);
                    while (backupConfigToFile == true) // Wait backup done
                        Thread.Sleep(100);
                }
                else if (restoreConfigFromFile == true) {
                    bwBackup.ReportProgress(11, null);
                    _WritePassword();
                    _SetQsfpMode(0x4D);  
                    
                    bwBackup.ReportProgress(12, null);
                    _WriteLowPage();

                    bwBackup.ReportProgress(13, null);
                    _WriteUpPage0();

                    bwBackup.ReportProgress(14, null);
                    _WriteUpPage2();

                    bwBackup.ReportProgress(15, null);
                    _WriteUpPage3();

                    bwBackup.ReportProgress(16, null);
                    _WriteUpPage4();

                    bwBackup.ReportProgress(17, null);
                    _WriteUpPage5();

                    bwBackup.ReportProgress(18, null);
                    _StoreIntoFlash();
                    Thread.Sleep(1000);

                    bwBackup.ReportProgress(20, null);

                    while (restoreConfigFromFile == true) // Wait restore done
                        Thread.Sleep(100);
                }
                else {
                    if (_GetModuleSerialValue() < 0)
                        bGetModuleMonitorValueError = true;
                    else
                        bwBackup.ReportProgress(0, null);
                }

                if (bGetModuleMonitorValueError == false)
                    Thread.Sleep(100);
                else
                    Thread.Sleep(500);
            }

            bwBackup.ReportProgress(100, null);
        }

        private int _WriteConfigToFileApi()
        {
            String filePath;
            Stream file;

            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(fileDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(fileDirectory);
                }
            }
            catch (Exception e)
            {
                if (e != null)
                    return -1;
                return -1;
            }

            filePath = fileDirectory + "\\" + tbNewSerialNumber.Text + ".bak";
            tbTableSavePath.Text = filePath;
            file = File.OpenWrite(filePath);

            try
            {
                file.Write(configBackupLowPage, 0, 128);
                file.Write(configBackupUpPage0, 0, 128);
                file.Write(configBackupUpPage2, 0, 128);
                file.Write(configBackupUpPage3, 0, 128);
                file.Write(configBackupUpPage4, 0, 128);
                file.Write(configBackupUpPage5, 0, 128);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return -1;
            }
            file.Close();

            return 0;
        }

        public void BackupConfigProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage) {
                case 1:
                    lStatus.Text = "Set XA serial number ...";
                    lStatus.Update();
                    return;

                case 2:
                    lStatus.Text = "Read low page ...";
                    lStatus.Update();
                    return;

                case 3:
                    lStatus.Text = "Read up page0 ...";
                    lStatus.Update();
                    return;

                case 4:
                    lStatus.Text = "Read up page2 ...";
                    lStatus.Update();
                    return;

                case 5:
                    lStatus.Text = "Read up page3 ...";
                    lStatus.Update();
                    return;

                case 6:
                    lStatus.Text = "Read up page4 ...";
                    lStatus.Update();
                    return;

                case 7:
                    lStatus.Text = "Read up page5 ...";
                    lStatus.Update();
                    return;

                case 10:
                    lStatus.Text = "Write into file ...";
                    lStatus.Update();
                    break;

                case 11:
                    lStatus.Text = "Write password ...";
                    lStatus.Update();
                    return;

                case 12:
                    lStatus.Text = "Restore low page ...";
                    lStatus.Update();
                    return;

                case 13:
                    lStatus.Text = "Restore up page0 ...";
                    lStatus.Update();
                    return;

                case 14:
                    lStatus.Text = "Restore up page2 ...";
                    lStatus.Update();
                    return;

                case 15:
                    lStatus.Text = "Restore up page3 ...";
                    lStatus.Update();
                    return;

                case 16:
                    lStatus.Text = "Restore up page4 ...";
                    lStatus.Update();
                    return;

                case 17:
                    lStatus.Text = "Restore up page5 ...";
                    lStatus.Update();
                    return;

                case 18:
                    lStatus.Text = "Save into flash ...";
                    lStatus.Update();
                    return;

                case 20:
                    lStatus.Text = "Wait restore ...";
                    lStatus.Update();
                    break;

                default:
                    break;
            }

            tbYcSerialNumber.Text = sYcSerialNumber;
            tbYcSerialNumber.Update();
            tbXaSerialNumber.Text = sXaSerialNumber;
            tbXaSerialNumber.Update();

            if ((backupConfigToFile == true) && (e.ProgressPercentage == 10)) {
                _WriteConfigToFileApi();
                sNewSerialNumber = "";
                backupConfigToFile = false;
                bBackupConfigToFile.Enabled = true;
                lStatus.Text = "Config backuped.";
                lStatus.Update();
                tbNewSerialNumber.Focus();
                tbNewSerialNumber.SelectAll();
            }

            if ((restoreConfigFromFile == true) && (e.ProgressPercentage == 20))
            {
                sNewSerialNumber = "";
                restoreConfigFromFile = false;
                bRestoreConfigFromFile.Enabled = true;
                lStatus.Text = "Restore Finish.";
                lStatus.Update();
                tbNewSerialNumber.Focus();
                tbNewSerialNumber.SelectAll();
            }
        }

        private int _CheckSerialNumberApi()
        {
            if (tbXaSerialNumber.Text.Length < 15)
            {
                if ((tbNewSerialNumber.Text.Length != 17) ||
                    (!tbNewSerialNumber.Text[0].Equals('X')) ||
                    (!tbNewSerialNumber.Text[1].Equals('A')) ||
                    (!tbNewSerialNumber.Text[8].Equals('-')) ||
                    (!tbNewSerialNumber.Text[12].Equals('-')))
                {
                    MessageBox.Show("輸入序號格式錯誤 XAxxxxxx-xxx-xxxx !!");
                    tbNewSerialNumber.Text = "";
                    return -1;
                }
                writeXaSerialNumber = true;
            }
            else
            {
                writeXaSerialNumber = false;
                tbNewSerialNumber.Text = tbXaSerialNumber.Text.Substring(0, 8) + "-" +
                    tbXaSerialNumber.Text.Substring(8, 3) + "-" + tbXaSerialNumber.Text.Substring(11, 4);
            }

            if (tbYcSerialNumber.Text.Length < 15)
            {
                MessageBox.Show("模組YC序號長度異常!!");
                return -1;
            }

            sNewSerialNumber = tbNewSerialNumber.Text.Substring(0, 8) + tbNewSerialNumber.Text.Substring(9, 3) +
                tbNewSerialNumber.Text.Substring(13, 4);

            return 0;
        }

        private void bBackupConfigToFile_Click(object sender, EventArgs e)
        {
            if (_CheckSerialNumberApi() < 0)
                return;

            bBackupConfigToFile.Enabled = false;
            backupConfigToFile = true;
        }

        private int _LoadConfigFile()
        {
            Stream file;
            String filePath;

            filePath = fileDirectory + '\\' + tbNewSerialNumber.Text + ".bak";
            tbTableSavePath.Text = filePath;

            try {
                file = File.OpenRead(filePath);
            }
            catch (Exception e) {
                MessageBox.Show(filePath + "設定檔開啟錯誤!!\n" + e.ToString());
                return -1;
            }

            try {
                file.Read(configBackupLowPage, 0, 128);
                file.Read(configBackupUpPage0, 0, 128);
                file.Read(configBackupUpPage2, 0, 128);
                file.Read(configBackupUpPage3, 0, 128);
                file.Read(configBackupUpPage4, 0, 128);
                file.Read(configBackupUpPage5, 0, 128);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return -1;
            }
            file.Close();

            return 0;
        }

        private void bRestoreConfigFromFile_Click(object sender, EventArgs e)
        {
          
            if (tbXaSerialNumber.Text.Length < 15)
            {
                if ((tbNewSerialNumber.Text.Length != 17) ||
                    (!tbNewSerialNumber.Text[0].Equals('X')) ||
                    (!tbNewSerialNumber.Text[1].Equals('A')) ||
                    (!tbNewSerialNumber.Text[8].Equals('-')) ||
                    (!tbNewSerialNumber.Text[12].Equals('-')))
                {
                    MessageBox.Show("輸入序號格式錯誤 XAxxxxxx-xxx-xxxx !!");
                    tbNewSerialNumber.Text = "";
                    return ;
                }
                writeXaSerialNumber = true;
            }
            else
            {
                writeXaSerialNumber = false;
                tbNewSerialNumber.Text = tbXaSerialNumber.Text.Substring(0, 8) + "-" +
                    tbXaSerialNumber.Text.Substring(8, 3) + "-" + tbXaSerialNumber.Text.Substring(11, 4);
            }

            sNewSerialNumber = tbNewSerialNumber.Text.Substring(0, 8) + tbNewSerialNumber.Text.Substring(9, 3) +
                tbNewSerialNumber.Text.Substring(13, 4);

  
            if (_LoadConfigFile() < 0)
                goto clear;

            bRestoreConfigFromFile.Enabled = false;
            restoreConfigFromFile = true;

            return;

        clear:
            sNewSerialNumber = "";
        }

        private void cbBackupOrRestoreSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBackupOrRestoreSelect.Checked == true) {
                bBackupConfigToFile.Enabled = true;
                bRestoreConfigFromFile.Enabled = false;
            }
            else
            {
                bBackupConfigToFile.Enabled = false;
                bRestoreConfigFromFile.Enabled = true;
            }
        }

        public int SpaceKeyDownApi(object sender, KeyEventArgs e)
        {
            if ((bBackupConfigToFile.Enabled == false) || (bRestoreConfigFromFile.Enabled == false))
                return -1;

            if (bBackupConfigToFile.Enabled == true)
                bBackupConfigToFile_Click(sender, e);
            else
                bRestoreConfigFromFile_Click(sender, e);

            return 0;
        }
    }
}
