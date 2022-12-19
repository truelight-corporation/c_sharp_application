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
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbTa38044G1Ch1.Checked == true)
                data[0] |= 0x01;

            if (cbTa38044G1Ch2.Checked == true)
                data[0] |= 0x02;

            if (cbTa38044G1Ch3.Checked == true)
                data[0] |= 0x04;

            if (cbTa38044G1Ch4.Checked == true)
                data[0] |= 0x08;

            if (cbTa38044G2Ch5.Checked == true)
                data[0] |= 0x10;

            if (cbTa38044G2Ch6.Checked == true)
                data[0] |= 0x20;

            if (cbTa38044G2Ch7.Checked == true)
                data[0] |= 0x40;

            if (cbTa38044G2Ch8.Checked == true)
                data[0] |= 0x80;

            rv = I2cWrite(0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void bRxModeSwitch_Click(object sender, EventArgs e)
        {
            if (_RxMaintenceModeControl() < 0)
                return;
        }
    }
     
}
