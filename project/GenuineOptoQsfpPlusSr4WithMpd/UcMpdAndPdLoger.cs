using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenuineOptoQsfpPlusSr4WithMpd
{
    public partial class UcMpdAndPdLoger : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;

        private DataTable dtValue = new DataTable();
        private String lastFileName = "";

        public UcMpdAndPdLoger()
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

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[16];
            int tmp, rv;

            rv = i2cReadCB(80, 140, 16, buf);
            if (rv != 16)
                return;

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
