using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mald38045Mata38044Config
{
    public partial class UcMissionMode : UserControl
    {
        public delegate int I2cReadCB(byte bank, byte page, byte regAddr, int length, byte[] data);
        public delegate int I2cWriteCB(byte bank, byte page, byte regAddr, int length, byte[] data);

        private byte regBank = 0x00;
        private byte regPage = 0xB0;
        private byte regStartAddr = 0x00;
        private I2cReadCB i2cReadCB;
        private I2cWriteCB i2cWriteCB;
        private bool reading = false;
        private bool txMaintenanceMode = false;
        private bool rxMaintenanceMode = false;

        private int I2cNotImplemented(byte bank, byte page, byte regAddr, int length, byte[] data)
        {
            throw new NotImplementedException();
        }

        private int I2cWrite(byte regAddr, int length, byte[] data)
        {
            int addr;
            byte page;

            page = regPage;
            addr = regStartAddr + regAddr;
            while (addr > 255)
            {
                page++;
                addr -= 128;
            }

            return i2cWriteCB(regBank, page, (byte)addr, length, data);
        }

        public UcMissionMode()
        {
            i2cReadCB = new I2cReadCB(I2cNotImplemented);
            i2cWriteCB = new I2cWriteCB(I2cNotImplemented);

            InitializeComponent();
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

        public int SetRegBankApi(byte bank)
        {
            regBank = bank;

            return 0;
        }

        public int SetRegPageApi(byte page)
        {
            regPage = page;

            return 0;
        }

        public int SetRegStartAddrApi(byte startAddr)
        {
            regStartAddr = startAddr;

            return 0;
        }

        private int _RxMaintenceModeControl()
        {
            byte[] data = new byte[2];
            int rv;

            data[0] = data[1] = 0;

            if (cbTa38044G1Ch1.Checked == false)
                data[0] |= 0x01;
            else
                data[1] |= 0x01;

            if (cbTa38044G1Ch2.Checked == false)
                data[0] |= 0x02;
            else
                data[1] |= 0x02;

            if (cbTa38044G1Ch3.Checked == false)
                data[0] |= 0x04;
            else
                data[1] |= 0x04;

            if (cbTa38044G1Ch4.Checked == false)
                data[0] |= 0x08;
            else
                data[1] |= 0x08;

            if (cbTa38044G2Ch5.Checked == false)
                data[0] |= 0x10;
            else
                data[1] |= 0x10;

            if (cbTa38044G2Ch6.Checked == false)
                data[0] |= 0x20;
            else
                data[1] |= 0x20;

            if (cbTa38044G2Ch7.Checked == false)
                data[0] |= 0x40;
            else
                data[1] |= 0x40;

            if (cbTa38044G2Ch8.Checked == false)
                data[0] |= 0x80;
            else
                data[1] |= 0x80;

            rv = I2cWrite(0, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void bRxModeSwitch_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0xA0;

            if (_RxMaintenceModeControl() < 0)
                return;
        }

        private int _TxMaintenceModeControl()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = data[1] = 0;

            if (cbLd38045G1Ch1.Checked == false)
                data[0] |= 0x01;
            else
                data[1] |= 0x01;

            if (cbLd38045G1Ch2.Checked == false)
                data[0] |= 0x02;
            else
                data[1] |= 0x02;

            if (cbLd38045G1Ch3.Checked == false)
                data[0] |= 0x04;
            else
                data[1] |= 0x04;

            if (cbLd38045G1Ch4.Checked == false)
                data[0] |= 0x08;
            else
                data[1] |= 0x08;

            if (cbLd38045G2Ch5.Checked == false)
                data[0] |= 0x10;
            else
                data[1] |= 0x10;

            if (cbLd38045G2Ch6.Checked == false)
                data[0] |= 0x20;
            else
                data[1] |= 0x20;

            if (cbLd38045G2Ch7.Checked == false)
                data[0] |= 0x40;
            else
                data[1] |= 0x40;

            if (cbLd38045G2Ch8.Checked == false)
                data[0] |= 0x80;
            else
                data[1] |= 0x80;

            rv = I2cWrite(0, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void bTxModeSwitch_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0x9E;

            if (_TxMaintenceModeControl() < 0)
                return;
        }

        private int _TxMaintenceModeCheck()
        {
            byte[] dataEnable = new byte[1];
            byte[] dataDisable = new byte[1];
            int rv;

            dataEnable[0] = dataDisable[0] = 0;

            if (rbLd38045EnableCh1.Checked == true)
                dataEnable[0] |= 0x01;
            else
                dataDisable[1] |= 0x01;

            if (rbLd38045EnableCh2.Checked == true)
                dataEnable[0] |= 0x02;
            else
                dataDisable[1] |= 0x02;

            if (rbLd38045EnableCh3.Checked == true)
                dataEnable[0] |= 0x04;
            else
                dataDisable[1] |= 0x04;

            if (rbLd38045EnableCh4.Checked == true)
                dataEnable[0] |= 0x08;
            else
                dataDisable[1] |= 0x08;

            if (rbLd38045EnableCh5.Checked == true)
                dataEnable[0] |= 0x10;
            else
                dataDisable[1] |= 0x10;

            if (rbLd38045EnableCh6.Checked == true)
                dataEnable[0] |= 0x20;
            else
                dataDisable[1] |= 0x20;

            if (rbLd38045EnableCh7.Checked == true)
                dataEnable[0] |= 0x40;
            else
                dataDisable[1] |= 0x40;

            if (rbLd38045EnableCh8.Checked == true)
                dataEnable[0] |= 0x80;
            else
                dataDisable[1] |= 0x80;

            if (txMaintenanceMode == true)
                rv = I2cWrite(1, 1, dataEnable);
            else
                rv = I2cWrite(0, 1, dataDisable);

            if (rv < 0)
                return -1;

            return 0;
        }

        private void bTxModeEnable_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0x9E;
            txMaintenanceMode = true;

            if (_TxMaintenceModeCheck() < 0)
                return;
        }

        private void bTxModeDisable_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0x9E;
            txMaintenanceMode = false;

            if (_TxMaintenceModeCheck() < 0)
                return;
        }

        private int _RxMaintenceModeCheck()
        {
            byte[] dataEnable = new byte[1];
            byte[] dataDisable = new byte[1];
            int rv;

            dataEnable[0] = dataDisable[0] = 0;

            if (rbTa38044EnableCh1.Checked == true)
                dataEnable[0] |= 0x01;
            else
                dataDisable[1] |= 0x01;

            if (rbTa38044EnableCh2.Checked == true)
                dataEnable[0] |= 0x02;
            else
                dataDisable[1] |= 0x02;

            if (rbTa38044EnableCh3.Checked == true)
                dataEnable[0] |= 0x04;
            else
                dataDisable[1] |= 0x04;

            if (rbTa38044EnableCh4.Checked == true)
                dataEnable[0] |= 0x08;
            else
                dataDisable[1] |= 0x08;

            if (rbTa38044EnableCh5.Checked == true)
                dataEnable[0] |= 0x10;
            else
                dataDisable[1] |= 0x10;

            if (rbTa38044EnableCh6.Checked == true)
                dataEnable[0] |= 0x20;
            else
                dataDisable[1] |= 0x20;

            if (rbTa38044EnableCh7.Checked == true)
                dataEnable[0] |= 0x40;
            else
                dataDisable[1] |= 0x40;

            if (rbTa38044EnableCh8.Checked == true)
                dataEnable[0] |= 0x80;
            else
                dataDisable[1] |= 0x80;

            if (rxMaintenanceMode == true)
                rv = I2cWrite(1, 1, dataEnable);
            else
                rv = I2cWrite(0, 1, dataDisable);

            if (rv < 0)
                return -1;

            return 0;
        }

        private void bRxModeEnable_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0xA0;
            rxMaintenanceMode = true;

            if (_RxMaintenceModeCheck() < 0)
                return;
        }

        private void bRxModeDisable_Click(object sender, EventArgs e)
        {
            regBank = 0x00;
            regPage = 0xBB;
            regStartAddr = 0xA0;
            rxMaintenanceMode = false;

            if (_RxMaintenceModeCheck() < 0)
                return;
        }

        private int _WriteBank03PageC4Addr80()
        {
            byte[] data = new byte[3];
            int rv;

            data[0] = data[1] = data[2] = 0x00;
            data[0] = Convert.ToByte(tbLdG1Page.Text);
            data[1] = Convert.ToByte(tbLdG1Addr.Text);
            data[2] = Convert.ToByte(tbLdG1Value.Text);
            rv = I2cWrite(0, 3, data);

            tbLdG1Status.Text = _CmisExtendAddrControlForRead();

            if (rv <= 0)
                return -1;

            return 0;
        }

        private int _WriteBank04PageC4Addr80()
        {

            byte[] data = new byte[3];
            int rv;

            data[0] = data[1] = data[2] = 0x00;
            data[0] = Convert.ToByte(tbLdG2Page.Text);
            data[1] = Convert.ToByte(tbLdG2Addr.Text);
            data[2] = Convert.ToByte(tbLdG2Value.Text);
            rv = I2cWrite(0, 3, data);

            tbLdG2Status.Text = _CmisExtendAddrControlForRead();

            if (rv <= 0)
                return -1;

            return 0;
        }

        private int _WriteBank05PageC4Addr80()
        {

            byte[] data = new byte[3];
            int rv;

            data[0] = data[1] = data[2] = 0x00;
            data[0] = Convert.ToByte(tbTaG1Page.Text);
            data[1] = Convert.ToByte(tbTaG1Addr.Text);
            data[2] = Convert.ToByte(tbTaG1Value.Text);
            rv = I2cWrite(0, 3, data);

            tbTaG1Status.Text = _CmisExtendAddrControlForRead();

            if (rv <= 0)
                return -1;

            return 0;
        }

        private int _WriteBank06PageC4Addr80()
        {
            byte[] data = new byte[3];
            int rv;

            data[0] = data[1] = data[2] = 0x00;
            data[0] = Convert.ToByte(tbTaG2Page.Text);
            data[1] = Convert.ToByte(tbTaG2Addr.Text);
            data[2] = Convert.ToByte(tbTaG2Value.Text);
            rv = I2cWrite(0, 3, data);

            tbTaG2Status.Text = _CmisExtendAddrControlForRead();

            if (rv <= 0)
                return -1;

            return 0;
        }

        private string _CmisExtendAddrControlForRead()
        {
            byte[] data = new byte[4];
            int i, rv;
            string x = "";

            if (reading == true)
                return x;

            if (i2cReadCB == null)
                return x;

            reading = true;
            rv = i2cReadCB(regBank, regPage, regStartAddr, 4, data);
            if (rv != 4)
                goto exit;

            return Convert.ToString(data[4]);

            exit:
                reading = false;
                return x;
        }

        private void textCharCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F'))
            {
                if((e.KeyChar >='a' && e.KeyChar <= 'f'))
                {
                    e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
                    e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }

        private void bTaG2Write_Click(object sender, EventArgs e)
        {
            bTaG2Write.Enabled = false;
            regBank = 6;
            regPage = 0xC4;
            regStartAddr = 0x80;
            _WriteBank06PageC4Addr80();
            bTaG2Write.Enabled = true;
        }

        private void bTaG1Write_Click(object sender, EventArgs e)
        {
            bTaG1Write.Enabled = false;
            regBank = 5;
            regPage = 0xC4;
            regStartAddr = 0x80;
            _WriteBank05PageC4Addr80();
            bTaG1Write.Enabled = true;
        }

        private void bLdG2Write_Click(object sender, EventArgs e)
        {
            bLdG2Write.Enabled = false;
            regBank = 4;
            regPage = 0xC4;
            regStartAddr = 0x80;
            _WriteBank04PageC4Addr80();
            bLdG2Write.Enabled = true;
        }

        private void bLdG1Write_Click(object sender, EventArgs e)
        {
            bLdG1Write.Enabled = false;
            regBank = 3;
            regPage = 0xC4;
            regStartAddr = 0x80;
            _WriteBank03PageC4Addr80();
            bLdG1Write.Enabled = true;
        }

        private void bTaG2Read_Click(object sender, EventArgs e)
        {
            bTaG2Read.Enabled = false;
            regBank = 6;
            regPage = 0xC4;
            regStartAddr = 0x80;
            tbTaG2Status.Text = _CmisExtendAddrControlForRead();
            bTaG2Read.Enabled = true;
        }

        private void bTaG1Read_Click(object sender, EventArgs e)
        {
            bTaG1Read.Enabled = false;
            regBank = 5;
            regPage = 0xC4;
            regStartAddr = 0x80;
            tbTaG1Status.Text = _CmisExtendAddrControlForRead();
            bTaG1Read.Enabled = true;
        }

        private void bLdG2Read_Click(object sender, EventArgs e)
        {
            bLdG2Read.Enabled = false;
            regBank = 4;
            regPage = 0xC4;
            regStartAddr = 0x80;
            tbLdG2Status.Text = _CmisExtendAddrControlForRead();
            bLdG2Read.Enabled = true;
        }

        private void bLdG1Read_Click(object sender, EventArgs e)
        {
            bLdG1Read.Enabled = false;
            regBank = 3;
            regPage = 0xC4;
            regStartAddr = 0x80;
            tbLdG1Status.Text = _CmisExtendAddrControlForRead();
            bLdG1Read.Enabled = true;
        }
    }
     
}
