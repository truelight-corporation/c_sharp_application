using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QsfpDigitalDiagnosticMonitoring
{
    public partial class UCMemoryDump : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int WritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private WritePasswordCB writePasswordCB = null;

        private DataTable dtMemory = new DataTable();

        public UCMemoryDump()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 16; i++)
                dtMemory.Columns.Add(i.ToString("X2"), typeof(String));
            
            for (i = 0; i < 128; i++) {
                if (i % 16 == 0) {
                    dtMemory.Rows.Add("NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA");
                }
            }
            dgvMemory.DataSource = dtMemory;
            for (i = 0; i < dgvMemory.Rows.Count; i++)
                dgvMemory.Rows[i].HeaderCell.Value = (i * 16).ToString("X2");

            cbPageSelect.Items.Add("Low Page");
            cbPageSelect.Items.Add("Up 00h");
            cbPageSelect.Items.Add("Up 01h");
            cbPageSelect.Items.Add("Up 02h");
            cbPageSelect.Items.Add("Up 03h");
            cbPageSelect.Text = "Up 00h";
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

        public int SetWritePasswordCBApi(WritePasswordCB cb)
        {
            if (cb == null)
                return -1;

            writePasswordCB = new WritePasswordCB(cb);

            return 0;
        }

        private int _ChangePage()
        {
            byte[] pageData = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            switch (cbPageSelect.SelectedItem.ToString()) {
                case "Low Page":
                    return 0;

                case "Up 00h":
                    pageData[0] = 0x00;
                    break;

                case "Up 01h":
                    pageData[0] = 0x01;
                    break;

                case "Up 02h":
                    pageData[0] = 0x02;
                    break;

                case "Up 03h":
                    pageData[0] = 0x03;
                    break;

                default:
                    return -1;
            }

            if (i2cWriteCB(80, 127, 1, pageData) < 0)
                return -1;

            return 0;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[128];
            int i;
            byte starAddr;

            if (i2cReadCB == null)
                return;

            bRead.Enabled = false;

            if (cbPageSelect.SelectedItem.Equals("Low Page"))
                starAddr = 0;
            else {
                if (_ChangePage() < 0)
                    goto exit;
                starAddr = 128;
            }

            if (i2cReadCB(80, starAddr, 128, data) != 128)
                goto exit;

            for (i = 0; i < 128; i++)
                dtMemory.Rows[i / 16].SetField(i % 16, data[i].ToString("X2"));

            if (starAddr == 0)
                bWriteLowPage.Enabled = true;
            else
                bWriteLowPage.Enabled = false;

            exit:
            bRead.Enabled = true;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[128];
            int i;
            byte starAddr;

            if (writePasswordCB == null)
                return;

            if (i2cWriteCB == null)
                return;

            bWrite.Enabled = false;

            if (writePasswordCB() < 0)
                goto exit;

            if (cbPageSelect.SelectedItem.Equals("Low Page"))
                starAddr = 0;
            else {
                if (_ChangePage() < 0)
                    goto exit;
                starAddr = 128;
            }

            for (i = 0; i < 128; i++)
                data[i] = Convert.ToByte(dtMemory.Rows[i / 16].ItemArray[i % 16].ToString(), 16);

            if (i2cWriteCB(80, starAddr, 128, data) < 0)
                goto exit;

            exit:
            bWrite.Enabled = true;
        }

        private void bWriteLowPage_Click(object sender, EventArgs e)
        {
            byte[] data = {0xA9, 0x46, 0x50, 0x54};
            int i;

            if (i2cWriteCB == null)
                return;

            bWriteLowPage.Enabled = false;

            if (i2cWriteCB(80, 0x7B, 4, data) < 0)
                goto exit;

            data = new byte[123];

            for (i = 0; i < 123; i++)
                data[i] = Convert.ToByte(dtMemory.Rows[i / 16].ItemArray[i % 16].ToString(), 16);

            System.Threading.Thread.Sleep(1000);

            if (i2cWriteCB(80, 0, 123, data) < 0)
                goto exit;

            data[0] = 0x06;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0x80;
            if (i2cWriteCB(80, 0x82, 1, data) < 0)
                goto exit;

            System.Threading.Thread.Sleep(100);

            MessageBox.Show("Please re-plug QSFP28 module!!");

            exit:
            bWriteLowPage.Enabled = false;
        }
    }
}
