using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SfpDigitalDiagnosticMonitoring
{
    public partial class UcMemoryDump : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private I2cWritePasswordCB i2cWritePasswordCB = null;
        private DataTable dtMemory = new DataTable();

        public UcMemoryDump()
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

            cbAddrSelect.Items.Add("A0h");
            cbAddrSelect.Items.Add("A2h");
            cbAddrSelect.Text = "A0h";

            cbPageSelect.Items.Add("Low Page");
            cbPageSelect.Items.Add("Up 00h");
            cbPageSelect.Items.Add("Up 01h");
            cbPageSelect.Items.Add("Up 02h");
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

        public int SetI2cWritePasswordCBApi(I2cWritePasswordCB cb)
        {
            if (cb == null)
                return -1;

            i2cWritePasswordCB = new I2cWritePasswordCB(cb);

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

                default:
                    return -1;
            }

            if (i2cWriteCB(81, 127, 1, pageData) < 0)
                return -1;

            return 0;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[128];
            int i;
            byte devAddr, starAddr;

            if (i2cReadCB == null)
                return;

            switch (cbAddrSelect.SelectedItem.ToString()) {
                case "A0h":
                    devAddr = 80;
                    break;

                case "A2h":
                    devAddr = 81;
                    break;

                default:
                    return;
            }

            bRead.Enabled = false;

            if (devAddr == 81) {
                if (cbPageSelect.SelectedItem.Equals("Low Page"))
                    starAddr = 0;
                else {
                    if (_ChangePage() < 0)
                        goto exit;
                    starAddr = 128;
                }
            }
            else
                starAddr = 0;

            if (i2cReadCB(devAddr, starAddr, 128, data) != 128)
                goto exit;

            for (i = 0; i < 128; i++)
                dtMemory.Rows[i / 16].SetField(i % 16, data[i].ToString("X2"));

            exit:
            bRead.Enabled = true;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[128];
            int i;
            byte addr;

            if (i2cWritePasswordCB == null)
                return;

            if (i2cWriteCB == null)
                return;

            switch (cbAddrSelect.SelectedItem.ToString()) {
                case "A0h":
                    addr = 80;
                    break;

                case "A2h":
                    addr = 81;
                    break;

                default:
                    return;
            }

            bWrite.Enabled = false;

            if (i2cWritePasswordCB() < 0)
                goto exit;

            for (i = 0; i < 128; i++)
                data[i] = Convert.ToByte(dtMemory.Rows[i / 16].ItemArray[i % 16].ToString(), 16);

            if (i2cWriteCB(addr, 0, 128, data) < 0)
                goto exit;

            exit:
            bWrite.Enabled = true;
        }

        private void cbAddrSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAddrSelect.SelectedItem.Equals("A2h")) {
                cbPageSelect.Enabled = true;
                cbPageSelect.Text = "Low Page";
            }
            else {
                cbPageSelect.Enabled = false;
                cbPageSelect.Text = "";
            }
        }
    }
}
