﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace GenuineOptoQsfpPlusSr4WithMpd
{
    public partial class fMain : Form
    {

        private I2cMaster i2cMaster = new I2cMaster();
        private DataTable dtValue = new DataTable();
        private String lastFileName = "";

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbConnected.Checked = false;

            return 0;
        }

        private int _I2cMasterConnect()
        {
            byte[] buf = new byte[5];

            if ((tbPassword123.Text.Length == 0) || (tbPassword124.Text.Length == 0) ||
                (tbPassword125.Text.Length == 0) || (tbPassword126.Text.Length == 0)) {
                MessageBox.Show("Before connect need input 4 int password!!");
                return -1;
            }

            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            if (_I2cRead(80, 0, 1, buf) != 1)
                goto Disconnect;

            buf[0] = Convert.ToByte(tbPassword123.Text);
            buf[1] = Convert.ToByte(tbPassword124.Text);
            buf[2] = Convert.ToByte(tbPassword125.Text);
            buf[3] = Convert.ToByte(tbPassword126.Text);
            buf[4] = 8;
            if (_I2cWrite(80, 123, 5, buf) < 0)
                goto Disconnect;

            cbConnected.Checked = true;

            return 0;

        Disconnect:
            _I2cMasterDisconnect();
            cbConnected.Checked = false;
            return -1;
        }

        public fMain()
        {
            InitializeComponent();
            dtValue.Columns.Add("Lable", typeof(String));
            dtValue.Columns.Add("Rx1", typeof(String));
            dtValue.Columns.Add("Rx2", typeof(String));
            dtValue.Columns.Add("Rx3", typeof(String));
            dtValue.Columns.Add("Rx4", typeof(String));
            dtValue.Columns.Add("Tx1", typeof(String));
            dtValue.Columns.Add("Tx2", typeof(String));
            dtValue.Columns.Add("Tx3", typeof(String));
            dtValue.Columns.Add("Tx4", typeof(String));
            dgvRecord.DataSource = dtValue;
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true)
                _I2cMasterConnect();
            else
                _I2cMasterDisconnect();
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[16];
            int tmp, rv;

            rv = _I2cRead(80, 140, 16, buf);
            if (rv != 16)
                goto Disconnect;

            tmp = buf[0] * 256 + buf[1];
            tbTx1.Text = tmp.ToString();
            tmp = buf[2] * 256 + buf[3];
            tbTx2.Text = tmp.ToString();
            tmp = buf[4] * 256 + buf[5];
            tbTx3.Text = tmp.ToString();
            tmp = buf[6] * 256 + buf[7];
            tbTx4.Text = tmp.ToString();

            tmp = buf[8] * 256 + buf[9];
            tbRx1.Text = tmp.ToString();
            tmp = buf[10] * 256 + buf[11];
            tbRx2.Text = tmp.ToString();
            tmp = buf[12] * 256 + buf[13];
            tbRx3.Text = tmp.ToString();
            tmp = buf[14] * 256 + buf[15];
            tbRx4.Text = tmp.ToString();

            return;

        Disconnect:
            _I2cMasterDisconnect();
            cbConnected.Checked = false;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();
            StreamWriter swConfig;

            sfdSelectFile.Filter = "txt files (*.txt)|*.txt";
            if (lastFileName.Length != 0)
                sfdSelectFile.FileName = lastFileName;

            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            lastFileName = sfdSelectFile.FileName;

            swConfig = new StreamWriter(lastFileName);
            swConfig.WriteLine("Lable\tRx1\tRx2\tRx3\tRx4\tTx1\tTx2\tTx3\tTx4");
            foreach (DataRow row in dtValue.Rows) {
                swConfig.WriteLine(row[0].ToString() + "\t" + row[1].ToString()
                    + "\t" + row[2].ToString() + "\t" + row[3].ToString() +
                    "\t" + row[4].ToString() + "\t" + row[5].ToString() + "\t" +
                    row[6].ToString() + "\t" + row[7].ToString() + "\t" + 
                    row[8].ToString());
            }
            
            swConfig.Close();
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            dtValue.Rows.Add(tbLogLable.Text, tbRx1.Text, tbRx2.Text,
                tbRx3.Text, tbRx4.Text, tbTx1.Text, tbTx2.Text, tbTx3.Text,
                tbTx4.Text);
        }

        private void bClearLog_Click(object sender, EventArgs e)
        {
            dtValue.Clear();
        }
    }
}
