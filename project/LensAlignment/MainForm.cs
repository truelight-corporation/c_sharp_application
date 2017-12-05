using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace LensAlignment
{
    public partial class fLensAlignment : Form
    {
        private I2cMaster imLightSoutce = new I2cMaster();
        private I2cMaster imBeAlignment = new I2cMaster();
        private DataTable dtValue = new DataTable();
        private String lastFileName = "";

        private int _IM_LightSourceConnect()
        {
            if (imLightSoutce.ConnectApi(100) < 0)
                return -1;

            imLightSoutce.SetTimeoutApi(50);

            if (imLightSoutce.connected == false) {
                cbLightSourceConnected.Checked = false;
                return -1;
            }

            cbLightSourceConnected.Checked = true;

            if (cbBeAlignmentConnected.Checked == true)
                cbStartMonitor.Enabled = true;

            return 0;
        }

        private int _IM_LightSourceDisconnect()
        {
            if (imLightSoutce.DisconnectApi() < 0)
                return -1;

            cbLightSourceConnected.Checked = false;
            cbStartMonitor.Enabled = false;
            cbStartMonitor.Checked = false;
            return 0;
        }

        private bool _IM_LightSourceCheckConnected()
        {
            return cbLightSourceConnected.Checked;
        }

        private int _IM_LightSourceRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (imLightSoutce.connected == false) {
                if (_IM_LightSourceConnect() < 0)
                    return -1;
            }

            rv = imLightSoutce.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _IM_LightSourceDisconnect();
            }

            return rv;
        }

        private int _IM_LightSourceWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (imLightSoutce.connected == false) {
                if (_IM_LightSourceConnect() < 0)
                    return -1;
            }

            rv = imLightSoutce.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _IM_LightSourceDisconnect();
            }

            return rv;
        }

        private int _IM_BeAlignmentConnect()
        {
            byte[] buf = new byte[5];
            int devAddr;

            if ((tbBeAlignmentPassword123.Text.Length == 0) ||
                (tbBeAlignmentPassword124.Text.Length == 0) ||
                (tbBeAlignmentPassword125.Text.Length == 0) ||
                (tbBeAlignmentPassword126.Text.Length == 0)) {
                MessageBox.Show("Before connect need input 4 int password!!");
                return -1;
            }

            if (imBeAlignment.ConnectApi(100) < 0)
                return -1;

            imBeAlignment.SetTimeoutApi(50);

            if (imBeAlignment.connected == false) {
                cbBeAlignmentConnected.Checked = false;
                return -1;
            }

            buf[0] = Convert.ToByte(tbBeAlignmentPassword123.Text);
            buf[1] = Convert.ToByte(tbBeAlignmentPassword124.Text);
            buf[2] = Convert.ToByte(tbBeAlignmentPassword125.Text);
            buf[3] = Convert.ToByte(tbBeAlignmentPassword126.Text);
            buf[4] = 8;

            devAddr = ucLensAlignment.GetBeAlignmentDeviceAddrApi();
            if (devAddr < 0)
                goto Disconnect;

            if (_IM_BeAlignmentWrite((byte)devAddr, 123, 5, buf) < 0)
                goto Disconnect;
                
            cbBeAlignmentConnected.Checked = true;

            //if (cbLightSourceConnected.Checked == true)
                cbStartMonitor.Enabled = true;

            return 0;

        Disconnect:
            _IM_BeAlignmentDisconnect();
            return -1;
        }

        private int _IM_BeAlignmentDisconnect()
        {
            if (imBeAlignment.DisconnectApi() < 0)
                return -1;

            cbBeAlignmentConnected.Checked = false;
            cbStartMonitor.Enabled = false;
            cbStartMonitor.Checked = false;
            return 0;
        }

        private bool _IM_BeAlignmentCheckConnected()
        {
            return cbBeAlignmentConnected.Checked;
        }

        private int _IM_BeAlignmentRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (imBeAlignment.connected == false) {
                if (_IM_BeAlignmentConnect() < 0)
                    return -1;
            }

            rv = imBeAlignment.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _IM_BeAlignmentDisconnect();
            }

            return rv;
        }

        private int _IM_BeAlignmentWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (imBeAlignment.connected == false) {
                if (_IM_BeAlignmentConnect() < 0)
                    return -1;
            }

            rv = imBeAlignment.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _IM_BeAlignmentDisconnect();
            }

            return rv;
        }

        private void _OnClosingHandler(object sender, EventArgs e)
        {
            ucLensAlignment.MonitorUpdateStopApi();
            _IM_LightSourceDisconnect();
            _IM_BeAlignmentDisconnect();
            if (dtValue.Rows.Count > 0)
                _bSave_Click(sender, e);
        }

        public fLensAlignment()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(_OnClosingHandler);

            dtValue.Columns.Add("Lable", typeof(String));
            dtValue.Columns.Add("SN", typeof(String));
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
            dgvRecord.DataSource = dtValue;

            if (ucLensAlignment.SetLightSourceI2cCheckConnectedCBApi(_IM_LightSourceCheckConnected) < 0) {
                MessageBox.Show("ucLensAlignment.SetLightSourceI2cCheckConnectedCBApi() faile Error!!");
                return;
            }

            if (ucLensAlignment.SetLightSourceI2cReadCBApi(_IM_LightSourceRead) < 0) {
                MessageBox.Show("ucLensAlignment.SetLightSourceI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucLensAlignment.SetLightSourceI2cWriteCBApi(_IM_LightSourceWrite) < 0) {
                MessageBox.Show("ucLensAlignment.SetLightSourceI2cWriteCBApi() faile Error!!");
                return;
            }

            if (ucLensAlignment.SetBeAlignmentI2cCheckConnectedCBApi(_IM_BeAlignmentCheckConnected) < 0) {
                MessageBox.Show("ucLensAlignment.SetBeAlignmentI2cCheckConnectedCBApi() faile Error!!");
                return;
            }
            
            if (ucLensAlignment.SetBeAlignmentI2cReadCBApi(_IM_BeAlignmentRead) < 0) {
                MessageBox.Show("ucLensAlignment.SetBeAlignmentI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucLensAlignment.SetBeAlignmentI2cWriteCBApi(_IM_BeAlignmentWrite) < 0) {
                MessageBox.Show("ucLensAlignment.SetBeAlignmentI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void _bLog_Click(object sender, EventArgs e)
        {
            dtValue.Rows.Add(tbLogLable.Text, tbSerialNumber.Text,
                ucLensAlignment.GetLightSourceRx1ValueApi().ToString(),
                ucLensAlignment.GetLightSourceRx2ValueApi().ToString(),
                ucLensAlignment.GetLightSourceRx3ValueApi().ToString(),
                ucLensAlignment.GetLightSourceRx4ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentRx1ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentRx2ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentRx3ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentRx4ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentMpd1ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentMpd2ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentMpd3ValueApi().ToString(),
                ucLensAlignment.GetBeAlignmentMpd4ValueApi().ToString());
        }

        private void _bClearLog_Click(object sender, EventArgs e)
        {
            dtValue.Clear();
        }

        private void _bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();
            StreamWriter swLog;

            sfdSelectFile.Filter = "txt files (*.txt)|*.txt";
            if (lastFileName.Length != 0)
                sfdSelectFile.FileName = lastFileName;

            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            lastFileName = sfdSelectFile.FileName;

            swLog = new StreamWriter(lastFileName);
            swLog.WriteLine("Lable\tSn\tTx1\tTx2\tTx3\tTx4\tRx1\tRx2\tRx3\tRx4\tMpd1\tMpd2\tMpd3\tMpd4");
            foreach (DataRow row in dtValue.Rows) {
                swLog.WriteLine(row[0].ToString() + "\t" + row[1].ToString() +
                    "\t" + row[2].ToString() + "\t" + row[3].ToString() + "\t" +
                    row[4].ToString() + "\t" + row[5].ToString() + "\t" +
                    row[6].ToString() + "\t" + row[7].ToString() + "\t" +
                    row[8].ToString() + "\t" + row[9].ToString() + "\t" +
                    row[10].ToString() + "\t" + row[11].ToString() + "\t" +
                    row[12].ToString() + "\t" + row[13].ToString());
            }
            swLog.Close();
            dtValue.Clear();
        }

        private void _cbLightSourceConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLightSourceConnected.Checked == true) {
                _IM_LightSourceConnect();
            }
            else {
                _IM_LightSourceDisconnect();
            }
        }

        private void _cbBeAlignmentConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBeAlignmentConnected.Checked == true) {
                _IM_BeAlignmentConnect();
            }
            else {
                _IM_BeAlignmentDisconnect();
            }
        }

        private void _cbStartMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStartMonitor.Checked == true)
                ucLensAlignment.MonitorUpdateStartApi();
            else
                ucLensAlignment.MonitorUpdateStopApi();
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            tbRx1Rssi.Text = tbRx2Rssi.Text = tbRx3Rssi.Text = tbRx4Rssi.Text = "";
            tbRx1Rate.Text = tbRx2Rate.Text = tbRx3Rate.Text = tbRx4Rate.Text = "";
            tbTx1RxPower.Text = tbTx2RxPower.Text = tbTx3RxPower.Text = tbTx4RxPower.Text = "";

            if (_IM_LightSourceRead(80, 108, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRx1Rssi.Text = tmp.ToString();

            if (_IM_LightSourceRead(80, 110, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRx2Rssi.Text = tmp.ToString();

            if (_IM_LightSourceRead(80, 112, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRx3Rssi.Text = tmp.ToString();

            if (_IM_LightSourceRead(80, 114, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            tbRx4Rssi.Text = tmp.ToString();

            if (_IM_LightSourceRead(80, 34, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTx1RxPower.Text = power.ToString("#0.0");

            if (_IM_LightSourceRead(80, 36, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTx2RxPower.Text = power.ToString("#0.0");

            if (_IM_LightSourceRead(80, 38, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTx3RxPower.Text = power.ToString("#0.0");

            if (_IM_LightSourceRead(80, 40, 2, data) != 2)
                return;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            tbTx4RxPower.Text = power.ToString("#0.0");

            data = new byte[] { 32, 0, 0, 0 };
            _IM_LightSourceWrite(80, 127, 1, data);
            if (_IM_LightSourceRead(80, 163, 1, data) != 1)
                return;

            tbRxRateDefault.Text = data[0].ToString();

            if (tbRxRateMax.Text.Length == 0)
                tbRxRateMax.Text = (data[0] + 15).ToString();

            if (tbRxRateMin.Text.Length == 0)
                tbRxRateMin.Text = (data[0] - 12).ToString();

            data = new byte[] { 2, 0, 0, 0 };
            _IM_LightSourceWrite(80, 127, 1, data);
            if (_IM_LightSourceRead(80, 133, 4, data) != 4)
                return;

            tbRx1Rate.Text = data[0].ToString();
            tbRx2Rate.Text = data[1].ToString();
            tbRx3Rate.Text = data[2].ToString();
            tbRx4Rate.Text = data[3].ToString();

            return;
        }

        private int _WriteLightSourcePassword()
        {
            byte[] data = new byte[4];

            if ((tbLightSourcePassword123.Text.Length != 2) ||
                (tbLightSourcePassword124.Text.Length != 2) ||
                (tbLightSourcePassword125.Text.Length != 2) ||
                (tbLightSourcePassword126.Text.Length != 2)) {
                MessageBox.Show("Please input 4 ASCII value password before write!!");
                return -1;
            }

            try {
                data[0] = Convert.ToByte(tbLightSourcePassword123.Text);
                data[1] = Convert.ToByte(tbLightSourcePassword124.Text);
                data[2] = Convert.ToByte(tbLightSourcePassword125.Text);
                data[3] = Convert.ToByte(tbLightSourcePassword126.Text);
            }
            catch (Exception eTB) {
                MessageBox.Show("Password to byte Error!!\n" + eTB.ToString());
                return -1;
            }

            if (_IM_LightSourceWrite(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _ClearLightSourcePassword()
        {
            byte[] data = new byte[4];

            data[0] = data[1] = data[2] = data[3] = 0;

            if (_IM_LightSourceWrite(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _SetLightSourceQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };


            if (_IM_LightSourceWrite(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (_IM_LightSourceWrite(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 2, 0, 0, 0 }; ;

            if ((tbRx1Rate.Text.Length == 0) || (tbRx2Rate.Text.Length == 0) ||
                (tbRx3Rate.Text.Length == 0) || (tbRx4Rate.Text.Length == 0)) {
                MessageBox.Show("Please input Rx power rate!!");
                return;
            }

            if (_WriteLightSourcePassword() < 0)
                return;

            if (_SetLightSourceQsfpMode(0x4D) < 0)
                return;

            _IM_LightSourceWrite(80, 127, 1, data);

            try {
                data[0] = Convert.ToByte(tbRx1Rate.Text);
                data[1] = Convert.ToByte(tbRx2Rate.Text);
                data[2] = Convert.ToByte(tbRx3Rate.Text);
                data[3] = Convert.ToByte(tbRx4Rate.Text);
            }
            catch (Exception eTB) {
                MessageBox.Show("Rx power rate out of range (0 ~ 255)!!\n" + eTB.ToString());
                return;
            }

            _IM_LightSourceWrite(80, 133, 4, data);

            _ClearLightSourcePassword();
            _SetLightSourceQsfpMode(0);

            return;
        }
    }
}
