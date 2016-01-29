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

            if (cbLightSourceConnected.Checked == true)
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

            if (ucLensAlignment.SetLightSourceI2cReadCBApi(_IM_LightSourceRead) < 0) {
                MessageBox.Show("ucHxr6104aConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucLensAlignment.SetLightSourceI2cWriteCBApi(_IM_LightSourceWrite) < 0) {
                MessageBox.Show("ucHxr6104aConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }

            if (ucLensAlignment.SetBeAlignmentI2cReadCBApi(_IM_BeAlignmentRead) < 0) {
                MessageBox.Show("ucHxr6104aConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucLensAlignment.SetBeAlignmentI2cWriteCBApi(_IM_BeAlignmentWrite) < 0) {
                MessageBox.Show("ucHxr6104aConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void _bLog_Click(object sender, EventArgs e)
        {
            dtValue.Rows.Add(tbLogLable.Text, ucLensAlignment.GetLightSourceRx1ValueApi().ToString(),
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
            swLog.WriteLine("Lable\tTx1\tTx2\tTx3\tTx4\tRx1\tRx2\tRx3\tRx4\tMpd1\tMpd2\tMpd3\tMpd4");
            foreach (DataRow row in dtValue.Rows) {
                swLog.WriteLine(row[0].ToString() + "\t" + row[1].ToString() +
                    "\t" + row[2].ToString() + "\t" + row[3].ToString() + "\t" +
                    row[4].ToString() + "\t" + row[5].ToString() + "\t" +
                    row[6].ToString() + "\t" + row[7].ToString() + "\t" +
                    row[8].ToString() + "\t" + row[9].ToString() + "\t" +
                    row[10].ToString() + "\t" + row[11].ToString() + "\t" +
                    row[12].ToString());
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
    }
}
