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
    public partial class UcMald38045Config : UserControl
    {
        public delegate int I2cReadCB(byte bank ,byte page, byte regAddr, int length, byte[] data);
        public delegate int I2cWriteCB(byte bank ,byte page, byte regAddr, int length, byte[] data);

        private DataTable dtCmisReg = new DataTable();
        private byte regBank = 0x00;
        private byte regPage = 0xB0;
        private byte regStartAddr = 0x00;
        private I2cReadCB i2cReadCB;
        private I2cWriteCB i2cWriteCB;
        private bool reading = false;

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
            while (addr > 255) {
                page++;
                addr -= 128;
            }

            return i2cWriteCB(regBank, page, (byte)addr, length, data);
        }

        public UcMald38045Config()
        {
            ComboboxItem item;
            double dTmp;
            int i;

            i2cReadCB = new I2cReadCB(I2cNotImplemented);
            i2cWriteCB = new I2cWriteCB(I2cNotImplemented);

            InitializeComponent();

            item = new ComboboxItem();
            item.Text = "0x00:Normal";
            item.Value = 0x00;
            cbReset.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0xAA:Reset w/o OTP";
            item.Value = 0xAA;
            cbReset.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0xAB:Reset w/ OTP";
            item.Value = 0xAB;
            cbReset.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Default";
            item.Value = 0x00;
            cbDeviceIdProgCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Prog mode";
            item.Value = 0x01;
            cbDeviceIdProgCode.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Not programmable";
            item.Value = 0x00;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Programmed through DIPC";
            item.Value = 0x01;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:programmable";
            item.Value = 0x02;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Overide LSBs";
            item.Value = 0x03;
            cbDeviceIdProgMode.Items.Add(item);

            for (i = 0; i <= 0x3F; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbDeviceIdValue.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Both buf turned off";
            item.Value = 0x00;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Buf1 on (full rate)";
            item.Value = 0x01;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Buf2 on (half rate)";
            item.Value = 0x02;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Both buf turned on";
            item.Value = 0x03;
            cbPllPdbClkbuf.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1/2";
            item.Value = 0x00;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1/8";
            item.Value = 0x01;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:1/32";
            item.Value = 0x02;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:1/40";
            item.Value = 0x03;
            cbPllDivSel.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1.5dB";
            item.Value = 0x00;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1.8dB";
            item.Value = 0x01;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.6dB";
            item.Value = 0x02;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:3.0dB";
            item.Value = 0x03;
            cbLosHystCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1.5dB";
            item.Value = 0x00;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1.8dB";
            item.Value = 0x01;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.6dB";
            item.Value = 0x02;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:3.0dB";
            item.Value = 0x03;
            cbLosHystCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1.5dB";
            item.Value = 0x00;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1.8dB";
            item.Value = 0x01;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.6dB";
            item.Value = 0x02;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:3.0dB";
            item.Value = 0x03;
            cbLosHystCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1.5dB";
            item.Value = 0x00;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1.8dB";
            item.Value = 0x01;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.6dB";
            item.Value = 0x02;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:3.0dB";
            item.Value = 0x03;
            cbLosHystCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1.5dB";
            item.Value = 0x00;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1.8dB";
            item.Value = 0x01;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.6dB";
            item.Value = 0x02;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:3.0dB";
            item.Value = 0x03;
            cbLosHystChAll.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:40mVppd";
            item.Value = 0x00;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50mVppd";
            item.Value = 0x01;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:55mVppd";
            item.Value = 0x02;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62mVppd";
            item.Value = 0x03;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:70mVppd";
            item.Value = 0x04;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:80mVppd";
            item.Value = 0x05;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:87mVppd";
            item.Value = 0x06;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:97mVppd";
            item.Value = 0x07;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:128mVppd";
            item.Value = 0x08;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:143mVppd";
            item.Value = 0x09;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:158mVppd";
            item.Value = 0x0A;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:175mVppd";
            item.Value = 0x0B;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:190mVppd";
            item.Value = 0x0C;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:230mVppd";
            item.Value = 0x0D;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:250mVppd";
            item.Value = 0x0E;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:275mVppd";
            item.Value = 0x0F;
            cbLosVthCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:40mVppd";
            item.Value = 0x00;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50mVppd";
            item.Value = 0x01;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:55mVppd";
            item.Value = 0x02;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62mVppd";
            item.Value = 0x03;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:70mVppd";
            item.Value = 0x04;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:80mVppd";
            item.Value = 0x05;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:87mVppd";
            item.Value = 0x06;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:97mVppd";
            item.Value = 0x07;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:128mVppd";
            item.Value = 0x08;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:143mVppd";
            item.Value = 0x09;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:158mVppd";
            item.Value = 0x0A;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:175mVppd";
            item.Value = 0x0B;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:190mVppd";
            item.Value = 0x0C;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:230mVppd";
            item.Value = 0x0D;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:250mVppd";
            item.Value = 0x0E;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:275mVppd";
            item.Value = 0x0F;
            cbLosVthCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:40mVppd";
            item.Value = 0x00;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50mVppd";
            item.Value = 0x01;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:55mVppd";
            item.Value = 0x02;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62mVppd";
            item.Value = 0x03;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:70mVppd";
            item.Value = 0x04;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:80mVppd";
            item.Value = 0x05;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:87mVppd";
            item.Value = 0x06;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:97mVppd";
            item.Value = 0x07;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:128mVppd";
            item.Value = 0x08;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:143mVppd";
            item.Value = 0x09;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:158mVppd";
            item.Value = 0x0A;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:175mVppd";
            item.Value = 0x0B;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:190mVppd";
            item.Value = 0x0C;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:230mVppd";
            item.Value = 0x0D;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:250mVppd";
            item.Value = 0x0E;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:275mVppd";
            item.Value = 0x0F;
            cbLosVthCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:40mVppd";
            item.Value = 0x00;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50mVppd";
            item.Value = 0x01;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:55mVppd";
            item.Value = 0x02;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62mVppd";
            item.Value = 0x03;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:70mVppd";
            item.Value = 0x04;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:80mVppd";
            item.Value = 0x05;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:87mVppd";
            item.Value = 0x06;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:97mVppd";
            item.Value = 0x07;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:128mVppd";
            item.Value = 0x08;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:143mVppd";
            item.Value = 0x09;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:158mVppd";
            item.Value = 0x0A;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:175mVppd";
            item.Value = 0x0B;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:190mVppd";
            item.Value = 0x0C;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:230mVppd";
            item.Value = 0x0D;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:250mVppd";
            item.Value = 0x0E;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:275mVppd";
            item.Value = 0x0F;
            cbLosVthCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:40mVppd";
            item.Value = 0x00;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50mVppd";
            item.Value = 0x01;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:55mVppd";
            item.Value = 0x02;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62mVppd";
            item.Value = 0x03;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:70mVppd";
            item.Value = 0x04;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:80mVppd";
            item.Value = 0x05;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:87mVppd";
            item.Value = 0x06;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:97mVppd";
            item.Value = 0x07;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:128mVppd";
            item.Value = 0x08;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:143mVppd";
            item.Value = 0x09;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:158mVppd";
            item.Value = 0x0A;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:175mVppd";
            item.Value = 0x0B;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:190mVppd";
            item.Value = 0x0C;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:230mVppd";
            item.Value = 0x0D;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:250mVppd";
            item.Value = 0x0E;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:275mVppd";
            item.Value = 0x0F;
            cbLosVthChAll.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:53Ohm";
            item.Value = 0x00;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:51Ohm";
            item.Value = 0x01;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:48Ohm";
            item.Value = 0x02;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:46Ohm";
            item.Value = 0x03;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:44Ohm";
            item.Value = 0x04;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:42Ohm";
            item.Value = 0x05;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:40Ohm";
            item.Value = 0x06;
            cbAfeRinCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:39Ohm";
            item.Value = 0x07;
            cbAfeRinCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:53Ohm";
            item.Value = 0x00;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:51Ohm";
            item.Value = 0x01;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:48Ohm";
            item.Value = 0x02;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:46Ohm";
            item.Value = 0x03;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:44Ohm";
            item.Value = 0x04;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:42Ohm";
            item.Value = 0x05;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:40Ohm";
            item.Value = 0x06;
            cbAfeRinCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:39Ohm";
            item.Value = 0x07;
            cbAfeRinCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:53Ohm";
            item.Value = 0x00;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:51Ohm";
            item.Value = 0x01;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:48Ohm";
            item.Value = 0x02;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:46Ohm";
            item.Value = 0x03;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:44Ohm";
            item.Value = 0x04;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:42Ohm";
            item.Value = 0x05;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:40Ohm";
            item.Value = 0x06;
            cbAfeRinCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:39Ohm";
            item.Value = 0x07;
            cbAfeRinCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:53Ohm";
            item.Value = 0x00;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:51Ohm";
            item.Value = 0x01;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:48Ohm";
            item.Value = 0x02;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:46Ohm";
            item.Value = 0x03;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:44Ohm";
            item.Value = 0x04;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:42Ohm";
            item.Value = 0x05;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:40Ohm";
            item.Value = 0x06;
            cbAfeRinCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:39Ohm";
            item.Value = 0x07;
            cbAfeRinCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:53Ohm";
            item.Value = 0x00;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:51Ohm";
            item.Value = 0x01;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:48Ohm";
            item.Value = 0x02;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:46Ohm";
            item.Value = 0x03;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:44Ohm";
            item.Value = 0x04;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:42Ohm";
            item.Value = 0x05;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:40Ohm";
            item.Value = 0x06;
            cbAfeRinChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:39Ohm";
            item.Value = 0x07;
            cbAfeRinChAll.Items.Add(item);

            for (i = 0, dTmp = 10; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbAfeBoostCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbAfeBoostCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbAfeBoostCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbAfeBoostCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbAfeBoostChAll.Items.Add(item);
            }

            for (i = 0, dTmp = -90; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 90; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = -105; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 105; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * (63 - i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = (209 - 105); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = (314 - 209); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = (418 - 314); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = -(209 - 105); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = -(314 - 209); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = -(418 - 314); i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjChAll.Items.Add(item);

            for (i = 0, dTmp = 0.1; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayChAll.Items.Add(item);
            }
            for (i = 0, dTmp = 0.1; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute control";
            item.Value = 0x00;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Force mute on";
            item.Value = 0x01;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Force driver on";
            item.Value = 0x02;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS status";
            item.Value = 0x03;
            cbMuteCntlCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute control";
            item.Value = 0x00;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Force mute on";
            item.Value = 0x01;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Force driver on";
            item.Value = 0x02;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS status";
            item.Value = 0x03;
            cbMuteCntlCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute control";
            item.Value = 0x00;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Force mute on";
            item.Value = 0x01;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Force driver on";
            item.Value = 0x02;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS status";
            item.Value = 0x03;
            cbMuteCntlCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute control";
            item.Value = 0x00;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Force mute on";
            item.Value = 0x01;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Force driver on";
            item.Value = 0x02;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS status";
            item.Value = 0x03;
            cbMuteCntlCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute control";
            item.Value = 0x00;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Force mute on";
            item.Value = 0x01;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Force driver on";
            item.Value = 0x02;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS status";
            item.Value = 0x03;
            cbMuteCntlChAll.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Normally";
            item.Value = 0x00;
            cbTxFaultStateCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Channel has no load";
            item.Value = 0x01;
            cbTxFaultStateCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Output is shorted to VCC33";
            item.Value = 0x02;
            cbTxFaultStateCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Output is shorted to GND";
            item.Value = 0x03;
            cbTxFaultStateCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Normally";
            item.Value = 0x00;
            cbTxFaultStateCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Channel has no load";
            item.Value = 0x01;
            cbTxFaultStateCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Output is shorted to VCC33";
            item.Value = 0x02;
            cbTxFaultStateCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Output is shorted to GND";
            item.Value = 0x03;
            cbTxFaultStateCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Normally";
            item.Value = 0x00;
            cbTxFaultStateCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Channel has no load";
            item.Value = 0x01;
            cbTxFaultStateCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Output is shorted to VCC33";
            item.Value = 0x02;
            cbTxFaultStateCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Output is shorted to GND";
            item.Value = 0x03;
            cbTxFaultStateCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Normally";
            item.Value = 0x00;
            cbTxFaultStateCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Channel has no load";
            item.Value = 0x01;
            cbTxFaultStateCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Output is shorted to VCC33";
            item.Value = 0x02;
            cbTxFaultStateCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Output is shorted to GND";
            item.Value = 0x03;
            cbTxFaultStateCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:50Ohm";
            item.Value = 0x00;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:55Ohm";
            item.Value = 0x01;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:58Ohm";
            item.Value = 0x02;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62.5Ohm";
            item.Value = 0x03;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:66.6Ohm";
            item.Value = 0x04;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:71Ohm";
            item.Value = 0x05;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:76Ohm";
            item.Value = 0x06;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:82Ohm";
            item.Value = 0x07;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:88Ohm";
            item.Value = 0x08;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:95Ohm";
            item.Value = 0x09;
            cbTxRvcselCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:100Ohm";
            item.Value = 0x0A;
            cbTxRvcselCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:50Ohm";
            item.Value = 0x00;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:55Ohm";
            item.Value = 0x01;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:58Ohm";
            item.Value = 0x02;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62.5Ohm";
            item.Value = 0x03;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:66.6Ohm";
            item.Value = 0x04;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:71Ohm";
            item.Value = 0x05;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:76Ohm";
            item.Value = 0x06;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:82Ohm";
            item.Value = 0x07;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:88Ohm";
            item.Value = 0x08;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:95Ohm";
            item.Value = 0x09;
            cbTxRvcselCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:100Ohm";
            item.Value = 0x0A;
            cbTxRvcselCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:50Ohm";
            item.Value = 0x00;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:55Ohm";
            item.Value = 0x01;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:58Ohm";
            item.Value = 0x02;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62.5Ohm";
            item.Value = 0x03;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:66.6Ohm";
            item.Value = 0x04;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:71Ohm";
            item.Value = 0x05;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:76Ohm";
            item.Value = 0x06;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:82Ohm";
            item.Value = 0x07;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:88Ohm";
            item.Value = 0x08;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:95Ohm";
            item.Value = 0x09;
            cbTxRvcselCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:100Ohm";
            item.Value = 0x0A;
            cbTxRvcselCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:50Ohm";
            item.Value = 0x00;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:55Ohm";
            item.Value = 0x01;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:58Ohm";
            item.Value = 0x02;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62.5Ohm";
            item.Value = 0x03;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:66.6Ohm";
            item.Value = 0x04;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:71Ohm";
            item.Value = 0x05;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:76Ohm";
            item.Value = 0x06;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:82Ohm";
            item.Value = 0x07;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:88Ohm";
            item.Value = 0x08;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:95Ohm";
            item.Value = 0x09;
            cbTxRvcselCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:100Ohm";
            item.Value = 0x0A;
            cbTxRvcselCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:50Ohm";
            item.Value = 0x00;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:55Ohm";
            item.Value = 0x01;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:58Ohm";
            item.Value = 0x02;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:62.5Ohm";
            item.Value = 0x03;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:66.6Ohm";
            item.Value = 0x04;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:71Ohm";
            item.Value = 0x05;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:76Ohm";
            item.Value = 0x06;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:82Ohm";
            item.Value = 0x07;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:88Ohm";
            item.Value = 0x08;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:95Ohm";
            item.Value = 0x09;
            cbTxRvcselChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:100Ohm";
            item.Value = 0x0A;
            cbTxRvcselChAll.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:0uA";
            item.Value = 0x00;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:11.5uA";
            item.Value = 0x01;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:23.0uA";
            item.Value = 0x02;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:34.5uA";
            item.Value = 0x03;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:46.0uA";
            item.Value = 0x04;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:57.5uA";
            item.Value = 0x05;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:69.0uA";
            item.Value = 0x06;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:80.0uA";
            item.Value = 0x07;
            cbTxIbiasFinetuneCtrlCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:0uA";
            item.Value = 0x00;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:11.5uA";
            item.Value = 0x01;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:23.0uA";
            item.Value = 0x02;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:34.5uA";
            item.Value = 0x03;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:46.0uA";
            item.Value = 0x04;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:57.5uA";
            item.Value = 0x05;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:69.0uA";
            item.Value = 0x06;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:80.0uA";
            item.Value = 0x07;
            cbTxIbiasFinetuneCtrlCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:0uA";
            item.Value = 0x00;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:11.5uA";
            item.Value = 0x01;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:23.0uA";
            item.Value = 0x02;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:34.5uA";
            item.Value = 0x03;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:46.0uA";
            item.Value = 0x04;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:57.5uA";
            item.Value = 0x05;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:69.0uA";
            item.Value = 0x06;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:80.0uA";
            item.Value = 0x07;
            cbTxIbiasFinetuneCtrlCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:0uA";
            item.Value = 0x00;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:11.5uA";
            item.Value = 0x01;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:23.0uA";
            item.Value = 0x02;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:34.5uA";
            item.Value = 0x03;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:46.0uA";
            item.Value = 0x04;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:57.5uA";
            item.Value = 0x05;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:69.0uA";
            item.Value = 0x06;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:80.0uA";
            item.Value = 0x07;
            cbTxIbiasFinetuneCtrlCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:0uA";
            item.Value = 0x00;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:11.5uA";
            item.Value = 0x01;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:23.0uA";
            item.Value = 0x02;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:34.5uA";
            item.Value = 0x03;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:46.0uA";
            item.Value = 0x04;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:57.5uA";
            item.Value = 0x05;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:69.0uA";
            item.Value = 0x06;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:80.0uA";
            item.Value = 0x07;
            cbTxIbiasFinetuneCtrlChAll.Items.Add(item);

            for (i = 0, dTmp = 0.1; i < 128; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (0.2 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIbiasCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (0.2 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIbiasCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (0.2 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIbiasCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (0.2 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIbiasCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (0.2 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIbiasChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (2.4 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIburninCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (2.4 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIburninCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (2.4 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIburninCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (2.4 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIburninCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (2.4 + dTmp * i).ToString("F1") + "mA";
                item.Value = i;
                cbTxIburninChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 10; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 12.75; i < 256; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbDacTxDacMain8bTopChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbTxDesiredImodCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbTxDesiredImodCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbTxDesiredImodCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbTxDesiredImodCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 255 * i).ToString("F2") + "mA";
                item.Value = i;
                cbTxDesiredImodChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 6; i < 128; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPreTopChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 127 * i).ToString("F2") + "dB";
                item.Value = i;
                cbDacTxDacPostTopChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Slower R&F time";
            item.Value = 0x00;
            cbTxBotPostPolinvCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Faster R&F time";
            item.Value = 0x01;
            cbTxBotPostPolinvCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Slower R&F time";
            item.Value = 0x00;
            cbTxBotPostPolinvCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Faster R&F time";
            item.Value = 0x01;
            cbTxBotPostPolinvCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Slower R&F time";
            item.Value = 0x00;
            cbTxBotPostPolinvCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Faster R&F time";
            item.Value = 0x01;
            cbTxBotPostPolinvCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Slower R&F time";
            item.Value = 0x00;
            cbTxBotPostPolinvCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Faster R&F time";
            item.Value = 0x01;
            cbTxBotPostPolinvCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Slower R&F time";
            item.Value = 0x00;
            cbTxBotPostPolinvChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Faster R&F time";
            item.Value = 0x01;
            cbTxBotPostPolinvChAll.Items.Add(item);

            dtCmisReg.Columns.Add("# (Dec)", typeof(String));
            dtCmisReg.Columns.Add("# (Hex)", typeof(String));
            dtCmisReg.Columns.Add("Data", typeof(String));

            dgvCmisReg.DataSource = dtCmisReg;
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

        private void _ParsePage00Addr00(byte data)
        {
            tbChipId.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr01(byte data)
        {
            tbRevId.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr02(byte data)
        {
            foreach (ComboboxItem item in cbReset.Items) {
                if (item.Value == data)
                    cbReset.SelectedItem = item;
            }
        }

        private void _ParsePage00Addr03(byte data)
        {
            if ((data & 0x80) == 0)
                cbIntrptPadPolarity.Checked = false;
            else
                cbIntrptPadPolarity.Checked = true;

            if ((data & 0x40) == 0)
                cbIntrptOen.Checked = false;
            else
                cbIntrptOen.Checked = true;

            if ((data & 0x20) == 0)
                cbIntrptOut.Checked = false;
            else
                cbIntrptOut.Checked = true;

            if ((data & 0x10) == 0)
                cbI2cAddressMode.Checked = false;
            else
                cbI2cAddressMode.Checked = true;

            if ((data & 0x08) == 0)
                cbForceTxfault.Checked = false;
            else
                cbForceTxfault.Checked = true;

            if ((data & 0x04) == 0)
                cbForceTxdis.Checked = false;
            else
                cbForceTxdis.Checked = true;
        }

        private void _ParsePage00Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbLpmodePadPolarity.Checked = false;
            else
                cbLpmodePadPolarity.Checked = true;

            if ((data & 0x04) == 0)
                cbStatusLpmode.Checked = false;
            else
                cbStatusLpmode.Checked = true;

            if ((data & 0x02) == 0)
                cbForceLpmodePin.Checked = false;
            else
                cbForceLpmodePin.Checked = true;

            if ((data & 0x01) == 0)
                cbSetLpmode.Checked = false;
            else
                cbSetLpmode.Checked = true;
        }

        private void _ParsePage00Addr06(byte data)
        {
            foreach (ComboboxItem item in cbDeviceIdProgCode.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDeviceIdProgCode.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDeviceIdProgMode.Items) {
                if (item.Value == (data & 0x03))
                    cbDeviceIdProgMode.SelectedItem = item;
            }
        }

        private void _ParsePage00Addr07(byte data)
        {
            foreach (ComboboxItem item in cbDeviceIdValue.Items) {
                if (item.Value == ((data & 0xFE) >> 1))
                    cbDeviceIdValue.SelectedItem = item;
            }
        }

        private void _ParsePage00AddrFF(byte data)
        {
            tbRegPage.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr08(byte data)
        {
            if ((data & 0x01) == 0)
                cbTsDisable.Checked = false;
            else
                cbTsDisable.Checked = true;
        }

        private void _ParsePage10Addr30(byte data)
        {
            foreach (ComboboxItem item in cbPllPdbClkbuf.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbPllPdbClkbuf.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbPllPdbMonclk.Checked = false;
            else
                cbPllPdbMonclk.Checked = true;

            if ((data & 0x01) == 0)
                cbPllPdb.Checked = false;
            else
                cbPllPdb.Checked = true;
        }

        private void _ParsePage10Addr31(byte data)
        {
            foreach (ComboboxItem item in cbPllDivSel.Items) {
                if (item.Value == (data & 0x03))
                    cbPllDivSel.SelectedItem = item;
            }
        }

        private void _ParsePage10Addr42(byte data)
        {
            if ((data & 0x02) == 0)
                cbPrbsReset.Checked = false;
            else
                cbPrbsReset.Checked = true;

            if ((data & 0x01) == 0)
                cbPrbsEnable.Checked = false;
            else
                cbPrbsEnable.Checked = true;
        }

        private void _ParsePage10Addr44(byte data)
        {
            if ((data & 0x20) == 0)
                cbPrbsInsErrB.Checked = false;
            else
                cbPrbsInsErrB.Checked = true;

            if ((data & 0x10) == 0)
                cbPrbsInsErrA.Checked = false;
            else
                cbPrbsInsErrA.Checked = true;

            if ((data & 0x08) == 0)
                cbPrbsForcePolflipHighB.Checked = false;
            else
                cbPrbsForcePolflipHighB.Checked = true;

            if ((data & 0x04) == 0)
                cbPrbsForcePolflipLowB.Checked = false;
            else
                cbPrbsForcePolflipLowB.Checked = true;

            if ((data & 0x02) == 0)
                cbPrbsForcePolflipHighA.Checked = false;
            else
                cbPrbsForcePolflipHighA.Checked = true;

            if ((data & 0x01) == 0)
                cbPrbsForcePolflipLowA.Checked = false;
            else
                cbPrbsForcePolflipLowA.Checked = true;
        }

        private void _ParsePage10Addr45(byte data)
        {
            if ((data & 0x20) == 0)
                cbPrbsEnNrz.Checked = false;
            else
                cbPrbsEnNrz.Checked = true;
        }

        private void _ParsePage10Addr46(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbsSelClk.Checked = false;
            else
                cbPrbsSelClk.Checked = true;
        }

        private void _ParsePage10Addr47(byte data)
        {
            if ((data & 0x08) == 0)
                cbChkFreezeCntr.Checked = false;
            else
                cbChkFreezeCntr.Checked = true;

            if ((data & 0x04) == 0)
                cbChkReset.Checked = false;
            else
                cbChkReset.Checked = true;

            if ((data & 0x02) == 0)
                cbChkClosedLoop.Checked = false;
            else
                cbChkClosedLoop.Checked = true;

            if ((data & 0x01) == 0)
                cbChkEnable.Checked = false;
            else
                cbChkEnable.Checked = true;
        }

        private void _ParsePage10Addr48(byte data)
        {
            if ((data & 0x02) == 0)
                cbChkPolflipStrmB.Checked = false;
            else
                cbChkPolflipStrmB.Checked = true;

            if ((data & 0x01) == 0)
                cbChkPolflipStrmA.Checked = false;
            else
                cbChkPolflipStrmA.Checked = true;
        }

        private void _ParsePage10Addr4A(byte data)
        {
            tbChkErrCnt1A.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4B(byte data)
        {
            tbChkErrCnt1B.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4C(byte data)
        {
            tbChkErrCnt2A.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4D(byte data)
        {
            tbChkErrCnt2B.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage11Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbDdmiReadRequest.Checked = false;
            else
                cbDdmiReadRequest.Checked = true;
        }

        private void _ParsePage11Addr40_41(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV3p3Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 3.73;
            tbDdmiOutV3p3Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr42_43(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1monReg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 3.73;
            tbDdmiOutV1monValue.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr46_47(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1p8Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 2;
            tbDdmiOutV1p8Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr48_49(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1p2Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 2;
            tbDdmiOutV1p2Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr60_61(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutImon0Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutImon0Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr62_63(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutImon1Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutImon1Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr64_65(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutImon2Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutImon2Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr66_67(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutImon3Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutImon3Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr68_69(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutTempSenseReg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = 0.128 * (dTmp - 2210) + 22;
            tbDdmiOutTempSenseValue.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr6A_6B(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutOffsetTrimAdcIbiasReg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 1);
            tbDdmiOutOffsetTrimAdcIbiasValue.Text = dTmp.ToString("F2");
        }

        private void _ParsePage30Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnalogCh0.Checked = false;
            else
                cbLosAnalogCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh0.Checked = false;
            else
                cbLolOrLosCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh0.Checked = false;
            else
                cbLolCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh0.Checked = false;
            else
                cbLosAlarmStickyCh0.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh0.Checked = false;
            else
                cbLolAlarmStickyCh0.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPowerDownCh0.Checked = false;
            else
                cbLosPowerDownCh0.Checked = true;
        }

        private void _ParsePage31Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnalogCh1.Checked = false;
            else
                cbLosAnalogCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh1.Checked = false;
            else
                cbLolOrLosCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh1.Checked = false;
            else
                cbLolCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh1.Checked = false;
            else
                cbLosAlarmStickyCh1.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh1.Checked = false;
            else
                cbLolAlarmStickyCh1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPowerDownCh1.Checked = false;
            else
                cbLosPowerDownCh1.Checked = true;
        }

        private void _ParsePage32Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnalogCh2.Checked = false;
            else
                cbLosAnalogCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh2.Checked = false;
            else
                cbLolOrLosCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh2.Checked = false;
            else
                cbLolCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh2.Checked = false;
            else
                cbLosAlarmStickyCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh2.Checked = false;
            else
                cbLolAlarmStickyCh2.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPowerDownCh2.Checked = false;
            else
                cbLosPowerDownCh2.Checked = true;
        }

        private void _ParsePage33Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnalogCh3.Checked = false;
            else
                cbLosAnalogCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh3.Checked = false;
            else
                cbLolOrLosCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh3.Checked = false;
            else
                cbLolCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh3.Checked = false;
            else
                cbLosAlarmStickyCh3.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh3.Checked = false;
            else
                cbLolAlarmStickyCh3.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPowerDownCh3.Checked = false;
            else
                cbLosPowerDownCh3.Checked = true;
        }

        private void _ParsePage30Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh0.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh1.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh2.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh3.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh0.Checked = false;
            else
                cbLosForceLosvalCh0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh0.Checked = false;
            else
                cbLosForceLosCh0.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh0.Checked = false;
            else
                cbLolMaskCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh0.Checked = false;
            else
                cbLosMaskCh0.Checked = true;
        }

        private void _ParsePage31Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh1.Checked = false;
            else
                cbLosForceLosvalCh1.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh1.Checked = false;
            else
                cbLosForceLosCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh1.Checked = false;
            else
                cbLolMaskCh1.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh1.Checked = false;
            else
                cbLosMaskCh1.Checked = true;
        }

        private void _ParsePage32Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh2.Checked = false;
            else
                cbLosForceLosvalCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh2.Checked = false;
            else
                cbLosForceLosCh2.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh2.Checked = false;
            else
                cbLolMaskCh2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh2.Checked = false;
            else
                cbLosMaskCh2.Checked = true;
        }

        private void _ParsePage33Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh3.Checked = false;
            else
                cbLosForceLosvalCh3.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh3.Checked = false;
            else
                cbLosForceLosCh3.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh3.Checked = false;
            else
                cbLolMaskCh3.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh3.Checked = false;
            else
                cbLosMaskCh3.Checked = true;
        }

        private void _ParsePage30Addr11(byte data)
        {
            foreach (ComboboxItem item in cbAfeRinCh0.Items) {
                if (item.Value == ((data & 0x70) >> 4))
                    cbAfeRinCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr11(byte data)
        {
            foreach (ComboboxItem item in cbAfeRinCh1.Items) {
                if (item.Value == ((data & 0x70) >> 4))
                    cbAfeRinCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr11(byte data)
        {
            foreach (ComboboxItem item in cbAfeRinCh2.Items) {
                if (item.Value == ((data & 0x70) >> 4))
                    cbAfeRinCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr11(byte data)
        {
            foreach (ComboboxItem item in cbAfeRinCh3.Items) {
                if (item.Value == ((data & 0x70) >> 4))
                    cbAfeRinCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr14(byte data)
        {
            foreach (ComboboxItem item in cbAfeBoostCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbAfeBoostCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr14(byte data)
        {
            foreach (ComboboxItem item in cbAfeBoostCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbAfeBoostCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr14(byte data)
        {
            foreach (ComboboxItem item in cbAfeBoostCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbAfeBoostCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr14(byte data)
        {
            foreach (ComboboxItem item in cbAfeBoostCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbAfeBoostCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh0.Checked = false;
            else
                cbThrsMid0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh0.Checked = false;
            else
                cbDacEqDac7baPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh1.Checked = false;
            else
                cbThrsMid0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh1.Checked = false;
            else
                cbDacEqDac7baPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh2.Checked = false;
            else
                cbThrsMid0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh2.Checked = false;
            else
                cbDacEqDac7baPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh3.Checked = false;
            else
                cbThrsMid0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh3.Checked = false;
            else
                cbDacEqDac7baPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh0.Checked = false;
            else
                cbThrsMid1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh1.Checked = false;
            else
                cbThrsMid1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh2.Checked = false;
            else
                cbThrsMid1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh3.Checked = false;
            else
                cbThrsMid1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh0.Checked = false;
            else
                cbThrsTop0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh0.Checked = false;
            else
                cbDacEqDac8baPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh1.Checked = false;
            else
                cbThrsTop0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh1.Checked = false;
            else
                cbDacEqDac8baPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh2.Checked = false;
            else
                cbThrsTop0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh2.Checked = false;
            else
                cbDacEqDac8baPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh3.Checked = false;
            else
                cbThrsTop0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh3.Checked = false;
            else
                cbDacEqDac8baPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh0.Checked = false;
            else
                cbThrsTop1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh1.Checked = false;
            else
                cbThrsTop1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh2.Checked = false;
            else
                cbThrsTop1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh3.Checked = false;
            else
                cbThrsTop1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh0.Checked = false;
            else
                cbThrsTop2ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh1.Checked = false;
            else
                cbThrsTop2ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh2.Checked = false;
            else
                cbThrsTop2ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh3.Checked = false;
            else
                cbThrsTop2ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh0.Checked = false;
            else
                cbThrsTop3ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh1.Checked = false;
            else
                cbThrsTop3ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh2.Checked = false;
            else
                cbThrsTop3ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh3.Checked = false;
            else
                cbThrsTop3ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh0.Checked = false;
            else
                cbThrsTop4ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh1.Checked = false;
            else
                cbThrsTop4ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh2.Checked = false;
            else
                cbThrsTop4ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh3.Checked = false;
            else
                cbThrsTop4ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh0.Checked = false;
            else
                cbThrsBot0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh0.Checked = false;
            else
                cbDacEqDac8bbPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh1.Checked = false;
            else
                cbThrsBot0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh1.Checked = false;
            else
                cbDacEqDac8bbPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh2.Checked = false;
            else
                cbThrsBot0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh2.Checked = false;
            else
                cbDacEqDac8bbPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh3.Checked = false;
            else
                cbThrsBot0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh3.Checked = false;
            else
                cbDacEqDac8bbPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh0.Checked = false;
            else
                cbThrsBot1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh1.Checked = false;
            else
                cbThrsBot1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh2.Checked = false;
            else
                cbThrsBot1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh3.Checked = false;
            else
                cbThrsBot1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh0.Checked = false;
            else
                cbThrsBot2ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh1.Checked = false;
            else
                cbThrsBot2ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh2.Checked = false;
            else
                cbThrsBot2ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh3.Checked = false;
            else
                cbThrsBot2ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh0.Checked = false;
            else
                cbThrsBot3ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh1.Checked = false;
            else
                cbThrsBot3ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh2.Checked = false;
            else
                cbThrsBot3ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh3.Checked = false;
            else
                cbThrsBot3ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh0.Checked = false;
            else
                cbThrsBot4ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh1.Checked = false;
            else
                cbThrsBot4ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh2.Checked = false;
            else
                cbThrsBot4ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh3.Checked = false;
            else
                cbThrsBot4ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh0.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh1.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh2.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh3.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh0.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh1.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh2.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh3.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr6D_6E(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 2) | ((data1 >> 6) & 0x03);

            tbEmAdcOutCh0.Text = "0x" + iTmp.ToString("X4");
        }

        private void _ParsePage31Addr6D_6E(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 2) | ((data1 >> 6) & 0x03);

            tbEmAdcOutCh1.Text = "0x" + iTmp.ToString("X4");
        }

        private void _ParsePage32Addr6D_6E(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 2) | ((data1 >> 6) & 0x03);

            tbEmAdcOutCh2.Text = "0x" + iTmp.ToString("X4");
        }

        private void _ParsePage33Addr6D_6E(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 2) | ((data1 >> 6) & 0x03);

            tbEmAdcOutCh3.Text = "0x" + iTmp.ToString("X4");
        }

        private void _ParsePage40Addr00(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxDisableCh0.Checked = false;
            else
                cbTxDisableCh0.Checked = true;

            foreach (ComboboxItem item in cbMuteCntlCh0.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr00(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxDisableCh1.Checked = false;
            else
                cbTxDisableCh1.Checked = true;

            foreach (ComboboxItem item in cbMuteCntlCh1.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr00(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxDisableCh2.Checked = false;
            else
                cbTxDisableCh2.Checked = true;

            foreach (ComboboxItem item in cbMuteCntlCh2.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr00(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxDisableCh3.Checked = false;
            else
                cbTxDisableCh3.Checked = true;

            foreach (ComboboxItem item in cbMuteCntlCh3.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr02(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxIgnoreFaultCh0.Checked = false;
            else
                cbTxIgnoreFaultCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbTxFaultAlarmCh0.Checked = false;
            else
                cbTxFaultAlarmCh0.Checked = true;

            foreach (ComboboxItem item in cbTxFaultStateCh0.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbTxFaultStateCh0.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbTxFaultCh0.Checked = false;
            else
                cbTxFaultCh0.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultMaskCh0.Checked = false;
            else
                cbTxFaultMaskCh0.Checked = true;
        }

        private void _ParsePage41Addr02(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxIgnoreFaultCh1.Checked = false;
            else
                cbTxIgnoreFaultCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbTxFaultAlarmCh1.Checked = false;
            else
                cbTxFaultAlarmCh1.Checked = true;

            foreach (ComboboxItem item in cbTxFaultStateCh1.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbTxFaultStateCh1.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbTxFaultCh1.Checked = false;
            else
                cbTxFaultCh1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultMaskCh1.Checked = false;
            else
                cbTxFaultMaskCh1.Checked = true;
        }

        private void _ParsePage42Addr02(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxIgnoreFaultCh2.Checked = false;
            else
                cbTxIgnoreFaultCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbTxFaultAlarmCh2.Checked = false;
            else
                cbTxFaultAlarmCh2.Checked = true;

            foreach (ComboboxItem item in cbTxFaultStateCh2.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbTxFaultStateCh2.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbTxFaultCh2.Checked = false;
            else
                cbTxFaultCh2.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultMaskCh2.Checked = false;
            else
                cbTxFaultMaskCh2.Checked = true;
        }

        private void _ParsePage43Addr02(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxIgnoreFaultCh3.Checked = false;
            else
                cbTxIgnoreFaultCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbTxFaultAlarmCh3.Checked = false;
            else
                cbTxFaultAlarmCh3.Checked = true;

            foreach (ComboboxItem item in cbTxFaultStateCh3.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbTxFaultStateCh3.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbTxFaultCh3.Checked = false;
            else
                cbTxFaultCh3.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultMaskCh3.Checked = false;
            else
                cbTxFaultMaskCh3.Checked = true;
        }

        private void _ParsePage40Addr04(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxAlarmClearInterruptCh0.Checked = false;
            else
                cbTxAlarmClearInterruptCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbTxAlarmClearPinCh0.Checked = false;
            else
                cbTxAlarmClearPinCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbTxAlarmClearRegCh0.Checked = false;
            else
                cbTxAlarmClearRegCh0.Checked = true;
        }

        private void _ParsePage41Addr04(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxAlarmClearInterruptCh1.Checked = false;
            else
                cbTxAlarmClearInterruptCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbTxAlarmClearPinCh1.Checked = false;
            else
                cbTxAlarmClearPinCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbTxAlarmClearRegCh1.Checked = false;
            else
                cbTxAlarmClearRegCh1.Checked = true;
        }

        private void _ParsePage42Addr04(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxAlarmClearInterruptCh2.Checked = false;
            else
                cbTxAlarmClearInterruptCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbTxAlarmClearPinCh2.Checked = false;
            else
                cbTxAlarmClearPinCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbTxAlarmClearRegCh2.Checked = false;
            else
                cbTxAlarmClearRegCh2.Checked = true;
        }

        private void _ParsePage43Addr04(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxAlarmClearInterruptCh3.Checked = false;
            else
                cbTxAlarmClearInterruptCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbTxAlarmClearPinCh3.Checked = false;
            else
                cbTxAlarmClearPinCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbTxAlarmClearRegCh3.Checked = false;
            else
                cbTxAlarmClearRegCh3.Checked = true;
        }

        private void _ParsePage40Addr05(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxPolinvCh0.Checked = false;
            else
                cbTxPolinvCh0.Checked = true;
        }

        private void _ParsePage41Addr05(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxPolinvCh1.Checked = false;
            else
                cbTxPolinvCh1.Checked = true;
        }

        private void _ParsePage42Addr05(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxPolinvCh2.Checked = false;
            else
                cbTxPolinvCh2.Checked = true;
        }

        private void _ParsePage43Addr05(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxPolinvCh3.Checked = false;
            else
                cbTxPolinvCh3.Checked = true;
        }

        private void _ParsePage40Addr06(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxRvcselAutodetectEnableCh0.Checked = false;
            else
                cbTxRvcselAutodetectEnableCh0.Checked = true;
            
            foreach (ComboboxItem item in cbTxRvcselCh0.Items) {
                if (item.Value == ((data & 0x78) >> 3))
                    cbTxRvcselCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxIbiasFinetuneCtrlCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbTxIbiasFinetuneCtrlCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr06(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxRvcselAutodetectEnableCh1.Checked = false;
            else
                cbTxRvcselAutodetectEnableCh1.Checked = true;

            foreach (ComboboxItem item in cbTxRvcselCh1.Items) {
                if (item.Value == ((data & 0x78) >> 3))
                    cbTxRvcselCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxIbiasFinetuneCtrlCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbTxIbiasFinetuneCtrlCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr06(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxRvcselAutodetectEnableCh2.Checked = false;
            else
                cbTxRvcselAutodetectEnableCh2.Checked = true;

            foreach (ComboboxItem item in cbTxRvcselCh2.Items) {
                if (item.Value == ((data & 0x78) >> 3))
                    cbTxRvcselCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxIbiasFinetuneCtrlCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbTxIbiasFinetuneCtrlCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr06(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxRvcselAutodetectEnableCh3.Checked = false;
            else
                cbTxRvcselAutodetectEnableCh3.Checked = true;

            foreach (ComboboxItem item in cbTxRvcselCh3.Items) {
                if (item.Value == ((data & 0x78) >> 3))
                    cbTxRvcselCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxIbiasFinetuneCtrlCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbTxIbiasFinetuneCtrlCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr07(byte data)
        {
            foreach (ComboboxItem item in cbTxIbiasCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIbiasCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr07(byte data)
        {
            foreach (ComboboxItem item in cbTxIbiasCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIbiasCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr07(byte data)
        {
            foreach (ComboboxItem item in cbTxIbiasCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIbiasCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr07(byte data)
        {
            foreach (ComboboxItem item in cbTxIbiasCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIbiasCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxBurninEnCh0.Checked = false;
            else
                cbTxBurninEnCh0.Checked = true;

            foreach (ComboboxItem item in cbTxIburninCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIburninCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxBurninEnCh1.Checked = false;
            else
                cbTxBurninEnCh1.Checked = true;

            foreach (ComboboxItem item in cbTxIburninCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIburninCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxBurninEnCh2.Checked = false;
            else
                cbTxBurninEnCh2.Checked = true;

            foreach (ComboboxItem item in cbTxIburninCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIburninCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxBurninEnCh3.Checked = false;
            else
                cbTxBurninEnCh3.Checked = true;

            foreach (ComboboxItem item in cbTxIburninCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbTxIburninCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnBotCh0.Checked = false;
            else
                cbTxIdoubleEnBotCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbTxBotDcdDacPolflipCh0.Checked = false;
            else
                cbTxBotDcdDacPolflipCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdBotPdbCh0.Checked = false;
            else
                cbDacTxDacDcdBotPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnBotCh1.Checked = false;
            else
                cbTxIdoubleEnBotCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbTxBotDcdDacPolflipCh1.Checked = false;
            else
                cbTxBotDcdDacPolflipCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdBotPdbCh1.Checked = false;
            else
                cbDacTxDacDcdBotPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnBotCh2.Checked = false;
            else
                cbTxIdoubleEnBotCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbTxBotDcdDacPolflipCh2.Checked = false;
            else
                cbTxBotDcdDacPolflipCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdBotPdbCh2.Checked = false;
            else
                cbDacTxDacDcdBotPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnBotCh3.Checked = false;
            else
                cbTxIdoubleEnBotCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbTxBotDcdDacPolflipCh3.Checked = false;
            else
                cbTxBotDcdDacPolflipCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdBotPdbCh3.Checked = false;
            else
                cbDacTxDacDcdBotPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnMidCh0.Checked = false;
            else
                cbTxIdoubleEnMidCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbTxMidDcdDacPolflipCh0.Checked = false;
            else
                cbTxMidDcdDacPolflipCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdMidPdbCh0.Checked = false;
            else
                cbDacTxDacDcdMidPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnMidCh1.Checked = false;
            else
                cbTxIdoubleEnMidCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbTxMidDcdDacPolflipCh1.Checked = false;
            else
                cbTxMidDcdDacPolflipCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdMidPdbCh1.Checked = false;
            else
                cbDacTxDacDcdMidPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnMidCh2.Checked = false;
            else
                cbTxIdoubleEnMidCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbTxMidDcdDacPolflipCh2.Checked = false;
            else
                cbTxMidDcdDacPolflipCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdMidPdbCh2.Checked = false;
            else
                cbDacTxDacDcdMidPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnMidCh3.Checked = false;
            else
                cbTxIdoubleEnMidCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbTxMidDcdDacPolflipCh3.Checked = false;
            else
                cbTxMidDcdDacPolflipCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdMidPdbCh3.Checked = false;
            else
                cbDacTxDacDcdMidPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnTopCh0.Checked = false;
            else
                cbTxIdoubleEnTopCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbTxTopDcdDacPolflipCh0.Checked = false;
            else
                cbTxTopDcdDacPolflipCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdTopPdbCh0.Checked = false;
            else
                cbDacTxDacDcdTopPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnTopCh1.Checked = false;
            else
                cbTxIdoubleEnTopCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbTxTopDcdDacPolflipCh1.Checked = false;
            else
                cbTxTopDcdDacPolflipCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdTopPdbCh1.Checked = false;
            else
                cbDacTxDacDcdTopPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnTopCh2.Checked = false;
            else
                cbTxIdoubleEnTopCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbTxTopDcdDacPolflipCh2.Checked = false;
            else
                cbTxTopDcdDacPolflipCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdTopPdbCh2.Checked = false;
            else
                cbDacTxDacDcdTopPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbTxIdoubleEnTopCh3.Checked = false;
            else
                cbTxIdoubleEnTopCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbTxTopDcdDacPolflipCh3.Checked = false;
            else
                cbTxTopDcdDacPolflipCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdTopPdbCh3.Checked = false;
            else
                cbDacTxDacDcdTopPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0E(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bBotCh0.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0E(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bBotCh1.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0E(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bBotCh2.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0E(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bBotCh3.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0F(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bMidCh0.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0F(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bMidCh1.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0F(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bMidCh2.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0F(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bMidCh3.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr10(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bTopCh0.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr10(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bTopCh1.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr10(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bTopCh2.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr10(byte data)
        {
            foreach (ComboboxItem item in cbDacTxDacMain8bTopCh3.Items) {
                if (item.Value == data)
                    cbDacTxDacMain8bTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr11(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreBotPdbCh0.Checked = false;
            else
                cbDacTxDacPreBotPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr11(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreBotPdbCh1.Checked = false;
            else
                cbDacTxDacPreBotPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr11(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreBotPdbCh2.Checked = false;
            else
                cbDacTxDacPreBotPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr11(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreBotPdbCh3.Checked = false;
            else
                cbDacTxDacPreBotPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr12(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreMidPdbCh0.Checked = false;
            else
                cbDacTxDacPreMidPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr12(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreMidPdbCh1.Checked = false;
            else
                cbDacTxDacPreMidPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr12(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreMidPdbCh2.Checked = false;
            else
                cbDacTxDacPreMidPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr12(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreMidPdbCh3.Checked = false;
            else
                cbDacTxDacPreMidPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr13(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreTopPdbCh0.Checked = false;
            else
                cbDacTxDacPreTopPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr13(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreTopPdbCh1.Checked = false;
            else
                cbDacTxDacPreTopPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr13(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreTopPdbCh2.Checked = false;
            else
                cbDacTxDacPreTopPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr13(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPreTopPdbCh3.Checked = false;
            else
                cbDacTxDacPreTopPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPreTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr14(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostBotPdbCh0.Checked = false;
            else
                cbDacTxDacPostBotPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr14(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostBotPdbCh1.Checked = false;
            else
                cbDacTxDacPostBotPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr14(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostBotPdbCh2.Checked = false;
            else
                cbDacTxDacPostBotPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr14(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostBotPdbCh3.Checked = false;
            else
                cbDacTxDacPostBotPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostMidPdbCh0.Checked = false;
            else
                cbDacTxDacPostMidPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostMidPdbCh1.Checked = false;
            else
                cbDacTxDacPostMidPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostMidPdbCh2.Checked = false;
            else
                cbDacTxDacPostMidPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostMidPdbCh3.Checked = false;
            else
                cbDacTxDacPostMidPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostTopPdbCh0.Checked = false;
            else
                cbDacTxDacPostTopPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh0.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostTopPdbCh1.Checked = false;
            else
                cbDacTxDacPostTopPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh1.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostTopPdbCh2.Checked = false;
            else
                cbDacTxDacPostTopPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh2.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbDacTxDacPostTopPdbCh3.Checked = false;
            else
                cbDacTxDacPostTopPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh3.Items) {
                if (item.Value == (data & 0x7F))
                    cbDacTxDacPostTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr17(byte data)
        {
            foreach (ComboboxItem item in cbTxBotPostPolinvCh0.Items) {
                if (item.Value == (data & 0x03))
                    cbTxBotPostPolinvCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr17(byte data)
        {
            foreach (ComboboxItem item in cbTxBotPostPolinvCh1.Items) {
                if (item.Value == (data & 0x03))
                    cbTxBotPostPolinvCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr17(byte data)
        {
            foreach (ComboboxItem item in cbTxBotPostPolinvCh2.Items) {
                if (item.Value == (data & 0x03))
                    cbTxBotPostPolinvCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr17(byte data)
        {
            foreach (ComboboxItem item in cbTxBotPostPolinvCh3.Items) {
                if (item.Value == (data & 0x03))
                    cbTxBotPostPolinvCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr18(byte data)
        {
            foreach (ComboboxItem item in cbTxMidPostPolinvCh0.Items) {
                if (item.Value == (data & 0x03))
                    cbTxMidPostPolinvCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr18(byte data)
        {
            foreach (ComboboxItem item in cbTxMidPostPolinvCh1.Items) {
                if (item.Value == (data & 0x03))
                    cbTxMidPostPolinvCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr18(byte data)
        {
            foreach (ComboboxItem item in cbTxMidPostPolinvCh2.Items) {
                if (item.Value == (data & 0x03))
                    cbTxMidPostPolinvCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr18(byte data)
        {
            foreach (ComboboxItem item in cbTxMidPostPolinvCh3.Items) {
                if (item.Value == (data & 0x03))
                    cbTxMidPostPolinvCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr19(byte data)
        {
            foreach (ComboboxItem item in cbTxTopPostPolinvCh0.Items) {
                if (item.Value == (data & 0x03))
                    cbTxTopPostPolinvCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr19(byte data)
        {
            foreach (ComboboxItem item in cbTxTopPostPolinvCh1.Items) {
                if (item.Value == (data & 0x03))
                    cbTxTopPostPolinvCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr19(byte data)
        {
            foreach (ComboboxItem item in cbTxTopPostPolinvCh2.Items) {
                if (item.Value == (data & 0x03))
                    cbTxTopPostPolinvCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr19(byte data)
        {
            foreach (ComboboxItem item in cbTxTopPostPolinvCh3.Items) {
                if (item.Value == (data & 0x03))
                    cbTxTopPostPolinvCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr21(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh0.Checked = false;
            else
                cbTxPdbLbkOutCh0.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh0.Checked = false;
            else
                cbTxPdbLbkInCh0.Checked = true;
        }

        private void _ParsePage41Addr21(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh1.Checked = false;
            else
                cbTxPdbLbkOutCh1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh1.Checked = false;
            else
                cbTxPdbLbkInCh1.Checked = true;
        }

        private void _ParsePage42Addr21(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh2.Checked = false;
            else
                cbTxPdbLbkOutCh2.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh2.Checked = false;
            else
                cbTxPdbLbkInCh2.Checked = true;
        }

        private void _ParsePage43Addr21(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh3.Checked = false;
            else
                cbTxPdbLbkOutCh3.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh3.Checked = false;
            else
                cbTxPdbLbkInCh3.Checked = true;
        }

        private void _ParsePage40Addr23(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxEnableAutoImodCh0.Checked = false;
            else
                cbTxEnableAutoImodCh0.Checked = true;
        }

        private void _ParsePage41Addr23(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxEnableAutoImodCh1.Checked = false;
            else
                cbTxEnableAutoImodCh1.Checked = true;
        }

        private void _ParsePage42Addr23(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxEnableAutoImodCh2.Checked = false;
            else
                cbTxEnableAutoImodCh2.Checked = true;
        }

        private void _ParsePage43Addr23(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxEnableAutoImodCh3.Checked = false;
            else
                cbTxEnableAutoImodCh3.Checked = true;
        }

        private void _ParsePage40Addr24(byte data)
        {
            foreach (ComboboxItem item in cbTxDesiredImodCh0.Items) {
                if (item.Value == data)
                    cbTxDesiredImodCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr24(byte data)
        {
            foreach (ComboboxItem item in cbTxDesiredImodCh1.Items) {
                if (item.Value == data)
                    cbTxDesiredImodCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr24(byte data)
        {
            foreach (ComboboxItem item in cbTxDesiredImodCh2.Items) {
                if (item.Value == data)
                    cbTxDesiredImodCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr24(byte data)
        {
            foreach (ComboboxItem item in cbTxDesiredImodCh3.Items) {
                if (item.Value == data)
                    cbTxDesiredImodCh3.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr26(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxIbiasAdcSelectCh0.Checked = false;
            else
                cbTxIbiasAdcSelectCh0.Checked = true;
        }

        private void _ParsePage41Addr26(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxIbiasAdcSelectCh1.Checked = false;
            else
                cbTxIbiasAdcSelectCh1.Checked = true;
        }

        private void _ParsePage42Addr26(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxIbiasAdcSelectCh2.Checked = false;
            else
                cbTxIbiasAdcSelectCh2.Checked = true;
        }

        private void _ParsePage43Addr26(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxIbiasAdcSelectCh3.Checked = false;
            else
                cbTxIbiasAdcSelectCh3.Checked = true;
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[256];
            int i, rv;

            if (reading == true)
                return;

            if (i2cReadCB == null)
                return;

            bReadAll.Enabled = false;
            bReadAll.Text = "Reading...";
            reading = true;

            rv = i2cReadCB(regBank, regPage, regStartAddr, 238, data);
            if (rv != 238)
                goto exit;

            for (i = 0; i < rv; i++)
                dtCmisReg.Rows.Add(i.ToString("D3"), "0x" + i.ToString("X2"), "0x" + data[i].ToString("X2"));

            _ParsePage00Addr00(data[0]);
            _ParsePage00Addr01(data[1]);
            _ParsePage00Addr02(data[2]);
            _ParsePage00Addr03(data[3]);
            _ParsePage00Addr04(data[4]);
            _ParsePage00Addr06(data[5]);
            _ParsePage00Addr07(data[6]);
            _ParsePage00AddrFF(data[7]);

            _ParsePage10Addr08(data[8]);
            _ParsePage10Addr30(data[9]);
            _ParsePage10Addr31(data[10]);
            _ParsePage10Addr42(data[11]);
            _ParsePage10Addr44(data[12]);
            _ParsePage10Addr45(data[13]);
            _ParsePage10Addr46(data[14]);
            _ParsePage10Addr47(data[15]);
            _ParsePage10Addr48(data[16]);
            _ParsePage10Addr4A(data[17]);
            _ParsePage10Addr4B(data[18]);
            _ParsePage10Addr4C(data[19]);
            _ParsePage10Addr4D(data[20]);

            _ParsePage11Addr1A(data[21]);
            _ParsePage11Addr40_41(data[22], data[23]);
            _ParsePage11Addr42_43(data[24], data[25]);
            _ParsePage11Addr46_47(data[26], data[27]);
            _ParsePage11Addr48_49(data[28], data[29]);
            _ParsePage11Addr60_61(data[30], data[31]);
            _ParsePage11Addr62_63(data[32], data[33]);
            _ParsePage11Addr64_65(data[34], data[35]);
            _ParsePage11Addr66_67(data[36], data[37]);
            _ParsePage11Addr68_69(data[38], data[39]);
            _ParsePage11Addr6A_6B(data[40], data[41]);

            _ParsePage30Addr08(data[42]);
            _ParsePage30Addr09(data[43]);
            _ParsePage30Addr0C(data[44]);
            _ParsePage30Addr11(data[45]);
            _ParsePage30Addr14(data[46]);
            _ParsePage30Addr15(data[47]);
            _ParsePage30Addr16(data[48]);
            _ParsePage30Addr17(data[49]);
            _ParsePage30Addr18(data[50]);
            _ParsePage30Addr19(data[51]);
            _ParsePage30Addr1A(data[52]);
            _ParsePage30Addr1B(data[53]);
            _ParsePage30Addr1C(data[54]);
            _ParsePage30Addr1D(data[55]);
            _ParsePage30Addr1E(data[56]);
            _ParsePage30Addr1F(data[57]);
            _ParsePage30Addr20(data[58]);
            _ParsePage30Addr41(data[59]);
            _ParsePage30Addr46(data[60]);
            _ParsePage30Addr47(data[61]);
            _ParsePage30Addr48(data[62]);
            _ParsePage30Addr6D_6E(data[63], data[64]);

            _ParsePage31Addr08(data[65]);
            _ParsePage31Addr09(data[66]);
            _ParsePage31Addr0C(data[67]);
            _ParsePage31Addr11(data[68]);
            _ParsePage31Addr14(data[69]);
            _ParsePage31Addr15(data[70]);
            _ParsePage31Addr16(data[71]);
            _ParsePage31Addr17(data[72]);
            _ParsePage31Addr18(data[73]);
            _ParsePage31Addr19(data[74]);
            _ParsePage31Addr1A(data[75]);
            _ParsePage31Addr1B(data[76]);
            _ParsePage31Addr1C(data[77]);
            _ParsePage31Addr1D(data[78]);
            _ParsePage31Addr1E(data[79]);
            _ParsePage31Addr1F(data[80]);
            _ParsePage31Addr20(data[81]);
            _ParsePage31Addr41(data[82]);
            _ParsePage31Addr46(data[83]);
            _ParsePage31Addr47(data[84]);
            _ParsePage31Addr48(data[85]);
            _ParsePage31Addr6D_6E(data[86], data[87]);

            _ParsePage32Addr08(data[88]);
            _ParsePage32Addr09(data[89]);
            _ParsePage32Addr0C(data[90]);
            _ParsePage32Addr11(data[91]);
            _ParsePage32Addr14(data[92]);
            _ParsePage32Addr15(data[93]);
            _ParsePage32Addr16(data[94]);
            _ParsePage32Addr17(data[95]);
            _ParsePage32Addr18(data[96]);
            _ParsePage32Addr19(data[97]);
            _ParsePage32Addr1A(data[98]);
            _ParsePage32Addr1B(data[99]);
            _ParsePage32Addr1C(data[100]);
            _ParsePage32Addr1D(data[101]);
            _ParsePage32Addr1E(data[102]);
            _ParsePage32Addr1F(data[103]);
            _ParsePage32Addr20(data[104]);
            _ParsePage32Addr41(data[105]);
            _ParsePage32Addr46(data[106]);
            _ParsePage32Addr47(data[107]);
            _ParsePage32Addr48(data[108]);
            _ParsePage32Addr6D_6E(data[109], data[110]);

            _ParsePage33Addr08(data[111]);
            _ParsePage33Addr09(data[112]);
            _ParsePage33Addr0C(data[113]);
            _ParsePage33Addr11(data[114]);
            _ParsePage33Addr14(data[115]);
            _ParsePage33Addr15(data[116]);
            _ParsePage33Addr16(data[117]);
            _ParsePage33Addr17(data[118]);
            _ParsePage33Addr18(data[119]);
            _ParsePage33Addr19(data[120]);
            _ParsePage33Addr1A(data[121]);
            _ParsePage33Addr1B(data[122]);
            _ParsePage33Addr1C(data[123]);
            _ParsePage33Addr1D(data[124]);
            _ParsePage33Addr1E(data[125]);
            _ParsePage33Addr1F(data[126]);
            _ParsePage33Addr20(data[127]);
            _ParsePage33Addr41(data[128]);
            _ParsePage33Addr46(data[129]);
            _ParsePage33Addr47(data[130]);
            _ParsePage33Addr48(data[131]);
            _ParsePage33Addr6D_6E(data[132], data[133]);

            _ParsePage40Addr00(data[134]);
            _ParsePage40Addr02(data[135]);
            _ParsePage40Addr04(data[136]);
            _ParsePage40Addr05(data[137]);
            _ParsePage40Addr06(data[138]);
            _ParsePage40Addr07(data[139]);
            _ParsePage40Addr08(data[140]);
            _ParsePage40Addr09(data[141]);
            _ParsePage40Addr0A(data[142]);
            _ParsePage40Addr0B(data[143]);
            _ParsePage40Addr0E(data[144]);
            _ParsePage40Addr0F(data[145]);
            _ParsePage40Addr10(data[146]);
            _ParsePage40Addr11(data[147]);
            _ParsePage40Addr12(data[148]);
            _ParsePage40Addr13(data[149]);
            _ParsePage40Addr14(data[150]);
            _ParsePage40Addr15(data[151]);
            _ParsePage40Addr16(data[152]);
            _ParsePage40Addr17(data[153]);
            _ParsePage40Addr18(data[154]);
            _ParsePage40Addr19(data[155]);
            _ParsePage40Addr21(data[156]);
            _ParsePage40Addr23(data[157]);
            _ParsePage40Addr24(data[158]);
            _ParsePage40Addr26(data[159]);

            _ParsePage41Addr00(data[160]);
            _ParsePage41Addr02(data[161]);
            _ParsePage41Addr04(data[162]);
            _ParsePage41Addr05(data[163]);
            _ParsePage41Addr06(data[164]);
            _ParsePage41Addr07(data[165]);
            _ParsePage41Addr08(data[166]);
            _ParsePage41Addr09(data[167]);
            _ParsePage41Addr0A(data[168]);
            _ParsePage41Addr0B(data[169]);
            _ParsePage41Addr0E(data[170]);
            _ParsePage41Addr0F(data[171]);
            _ParsePage41Addr10(data[172]);
            _ParsePage41Addr11(data[173]);
            _ParsePage41Addr12(data[174]);
            _ParsePage41Addr13(data[175]);
            _ParsePage41Addr14(data[176]);
            _ParsePage41Addr15(data[177]);
            _ParsePage41Addr16(data[178]);
            _ParsePage41Addr17(data[179]);
            _ParsePage41Addr18(data[180]);
            _ParsePage41Addr19(data[181]);
            _ParsePage41Addr21(data[182]);
            _ParsePage41Addr23(data[183]);
            _ParsePage41Addr24(data[184]);
            _ParsePage41Addr26(data[185]);

            _ParsePage42Addr00(data[186]);
            _ParsePage42Addr02(data[187]);
            _ParsePage42Addr04(data[188]);
            _ParsePage42Addr05(data[189]);
            _ParsePage42Addr06(data[190]);
            _ParsePage42Addr07(data[191]);
            _ParsePage42Addr08(data[192]);
            _ParsePage42Addr09(data[193]);
            _ParsePage42Addr0A(data[194]);
            _ParsePage42Addr0B(data[195]);
            _ParsePage42Addr0E(data[196]);
            _ParsePage42Addr0F(data[197]);
            _ParsePage42Addr10(data[198]);
            _ParsePage42Addr11(data[199]);
            _ParsePage42Addr12(data[200]);
            _ParsePage42Addr13(data[201]);
            _ParsePage42Addr14(data[202]);
            _ParsePage42Addr15(data[203]);
            _ParsePage42Addr16(data[204]);
            _ParsePage42Addr17(data[205]);
            _ParsePage42Addr18(data[206]);
            _ParsePage42Addr19(data[207]);
            _ParsePage42Addr21(data[208]);
            _ParsePage42Addr23(data[209]);
            _ParsePage42Addr24(data[210]);
            _ParsePage42Addr26(data[211]);

            _ParsePage43Addr00(data[212]);
            _ParsePage43Addr02(data[213]);
            _ParsePage43Addr04(data[214]);
            _ParsePage43Addr05(data[215]);
            _ParsePage43Addr06(data[216]);
            _ParsePage43Addr07(data[217]);
            _ParsePage43Addr08(data[218]);
            _ParsePage43Addr09(data[219]);
            _ParsePage43Addr0A(data[220]);
            _ParsePage43Addr0B(data[221]);
            _ParsePage43Addr0E(data[222]);
            _ParsePage43Addr0F(data[223]);
            _ParsePage43Addr10(data[224]);
            _ParsePage43Addr11(data[225]);
            _ParsePage43Addr12(data[226]);
            _ParsePage43Addr13(data[227]);
            _ParsePage43Addr14(data[228]);
            _ParsePage43Addr15(data[229]);
            _ParsePage43Addr16(data[230]);
            _ParsePage43Addr17(data[231]);
            _ParsePage43Addr18(data[232]);
            _ParsePage43Addr19(data[233]);
            _ParsePage43Addr21(data[234]);
            _ParsePage43Addr23(data[235]);
            _ParsePage43Addr24(data[236]);
            _ParsePage43Addr26(data[237]);

        exit:
            reading = false;
            bReadAll.Text = "Read All";
            bReadAll.Enabled = true;
        }

        private int _WritePage00Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbReset.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbReset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr02() < 0)
                return;
        }

        private int _WritePage00Addr03()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbIntrptPadPolarity.Checked == true)
                data[0] |= 0x80;

            if (cbIntrptOen.Checked == true)
                data[0] |= 0x40;

            if (cbI2cAddressMode.Checked == true)
                data[0] |= 0x10;

            if (cbForceTxfault.Checked == true)
                data[0] |= 0x08;

            if (cbForceTxdis.Checked == true)
                data[0] |= 0x04;

            rv = I2cWrite(3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbIntrptPadPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbIntrptOen_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbI2cAddressMode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbForceTxfault_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbForceTxdis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private int _WritePage00Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbLpmodePadPolarity.Checked == true)
                data[0] |= 0x08;

            if (cbForceLpmodePin.Checked == true)
                data[0] |= 0x02;

            if (cbSetLpmode.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLpmodePadPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private void cbForceLpmodePin_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private void cbSetLpmode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private int _WritePage00Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbDeviceIdProgCode.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDeviceIdProgMode.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDeviceIdProgCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr05() < 0)
                return;
        }

        private void cbDeviceIdProgMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr05() < 0)
                return;
        }

        private int _WritePage00Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbDeviceIdValue.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = I2cWrite(6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDeviceIdValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr06() < 0)
                return;
        }

        private int _WritePage10Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x20; //Default

            if (cbTsDisable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTsDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr08() < 0)
                return;
        }

        private int _WritePage10Addr30()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPllPdbClkbuf.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbPllPdbMonclk.Checked == true)
                data[0] |= 0x02;

            if (cbPllPdb.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPllPdbClkbuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private void cbPllPdbMonclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private void cbPllPdb_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private int _WritePage10Addr31()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbPllDivSel.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(10, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPllDivSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr31() < 0)
                return;
        }

        private int _WritePage10Addr42()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsReset.Checked == true)
                data[0] |= 0x02;

            if (cbPrbsEnable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(11, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr42() < 0)
                return;
        }

        private void cbPrbsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr42() < 0)
                return;
        }

        private int _WritePage10Addr44()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsInsErrB.Checked == true)
                data[0] |= 0x20;

            if (cbPrbsInsErrA.Checked == true)
                data[0] |= 0x10;

            if (cbPrbsForcePolflipHighB.Checked == true)
                data[0] |= 0x08;

            if (cbPrbsForcePolflipLowB.Checked == true)
                data[0] |= 0x04;

            if (cbPrbsForcePolflipHighA.Checked == true)
                data[0] |= 0x02;

            if (cbPrbsForcePolflipLowA.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsInsErrB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsInsErrA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipHighB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipLowB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipHighA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipLowA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private int _WritePage10Addr45()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsEnNrz.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(13, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsEnNrz_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr45() < 0)
                return;
        }

        private int _WritePage10Addr46()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsSelClk.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsSelClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr46() < 0)
                return;
        }

        private int _WritePage10Addr47()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbChkFreezeCntr.Checked == true)
                data[0] |= 0x08;

            if (cbChkReset.Checked == true)
                data[0] |= 0x04;

            if (cbChkClosedLoop.Checked == true)
                data[0] |= 0x02;

            if (cbChkEnable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbChkFreezeCntr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkClosedLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private int _WritePage10Addr48()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbChkPolflipStrmB.Checked == true)
                data[0] |= 0x02;

            if (cbChkPolflipStrmA.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(16, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbChkPolflipStrmB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr48() < 0)
                return;
        }

        private void cbChkPolflipStrmA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr48() < 0)
                return;
        }

        private int _WritePage30Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosAlarmStickyCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh0.Checked == true)
                data[0] |= 0x08;

            if (cbLosPowerDownCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(42, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosAlarmStickyCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private void cbLosPowerDownCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private int _WritePage31Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosAlarmStickyCh1.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh1.Checked == true)
                data[0] |= 0x08;

            if (cbLosPowerDownCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosAlarmStickyCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private void cbLosPowerDownCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private int _WritePage32Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosAlarmStickyCh2.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh2.Checked == true)
                data[0] |= 0x08;

            if (cbLosPowerDownCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(88, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosAlarmStickyCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private void cbLosPowerDownCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private int _WritePage33Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosAlarmStickyCh3.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLosPowerDownCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(111, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosAlarmStickyCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLosPowerDownCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLosAlarmStickyChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;

            if (_WritePage31Addr08() < 0)
                return;

            if (_WritePage32Addr08() < 0)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;

            if (_WritePage31Addr08() < 0)
                return;

            if (_WritePage32Addr08() < 0)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private int _WritePage2FAddr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosAlarmStickyChAll.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyChAll.Checked == true)
                data[0] |= 0x08;

            if (cbLosPowerDownChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(42, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(65, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(88, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(111, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosPowerDownChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr08() < 0)
                return;
        }

        private int _WritePage30Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbLosHystCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(43, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr09() < 0)
                return;
        }

        private void cbLosVthCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr09() < 0)
                return;
        }

        private int _WritePage31Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbLosHystCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(66, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr09() < 0)
                return;
        }

        private void cbLosVthCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr09() < 0)
                return;
        }

        private int _WritePage32Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbLosHystCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(89, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr09() < 0)
                return;
        }

        private void cbLosVthCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr09() < 0)
                return;
        }

        private int _WritePage33Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbLosHystCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr09() < 0)
                return;
        }

        private void cbLosVthCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr09() < 0)
                return;
        }

        private int _WritePage2FAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            if (cbLosHystChAll.SelectedIndex < 0)
                return -1;
            bTmp = Convert.ToByte(cbLosHystChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbLosVthChAll.SelectedIndex < 0)
                return -1;
            bTmp = Convert.ToByte(cbLosVthChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(43, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(66, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(89, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr09() < 0)
                return;
        }

        private void cbLosVthChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr09() < 0)
                return;
        }

        private int _WritePage30Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosForceLosvalCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh0.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh0.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh0.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(44, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private int _WritePage31Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosForceLosvalCh1.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh1.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh1.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh1.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private int _WritePage32Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosForceLosvalCh2.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh2.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh2.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(90, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private int _WritePage33Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosForceLosvalCh3.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh3.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh3.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private int _WritePage2FAddr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbLosForceLosvalChAll.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosChAll.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskChAll.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskChAll.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(46, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(69, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(92, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLosForceLosChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLolMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLosMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private int _WritePage30Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbAfeRinCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = I2cWrite(45, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeRinCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr11() < 0)
                return;
        }

        private int _WritePage31Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbAfeRinCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = I2cWrite(68, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeRinCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr11() < 0)
                return;
        }

        private int _WritePage32Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbAfeRinCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = I2cWrite(91, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeRinCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr11() < 0)
                return;
        }

        private int _WritePage33Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbAfeRinCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = I2cWrite(114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeRinCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr11() < 0)
                return;
        }

        private int _WritePage2FAddr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbAfeRinChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = I2cWrite(45, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(68, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(91, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeRinChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr11() < 0)
                return;
        }

        private int _WritePage30Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbAfeBoostCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(46, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeBoostCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr14() < 0)
                return;
        }

        private int _WritePage31Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbAfeBoostCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(69, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeBoostCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr14() < 0)
                return;
        }

        private int _WritePage32Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbAfeBoostCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(92, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeBoostCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr14() < 0)
                return;
        }

        private int _WritePage33Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbAfeBoostCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(115, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeBoostCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr14() < 0)
                return;
        }

        private int _WritePage2FAddr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbAfeBoostChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(46, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(69, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(92, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(115, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAfeBoostChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr14() < 0)
                return;
        }

        private int _WritePage30Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(47, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private int _WritePage31Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(70, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private int _WritePage32Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(93, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private int _WritePage33Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(116, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private int _WritePage2FAddr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(47, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(70, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(93, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(116, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private int _WritePage30Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(48, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr16() < 0)
                return;
        }

        private int _WritePage31Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(71, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr16() < 0)
                return;
        }

        private int _WritePage32Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(94, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr16() < 0)
                return;
        }

        private int _WritePage33Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(117, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr16() < 0)
                return;
        }

        private int _WritePage2FAddr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(48, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(71, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(94, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(117, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr16() < 0)
                return;
        }

        private int _WritePage30Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(49, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private int _WritePage31Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(72, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private int _WritePage32Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(95, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private int _WritePage33Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(118, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private int _WritePage2FAddr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(49, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(72, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(95, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(118, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private int _WritePage30Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(50, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr18() < 0)
                return;
        }

        private int _WritePage31Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(73, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr18() < 0)
                return;
        }

        private int _WritePage32Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(96, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr18() < 0)
                return;
        }

        private int _WritePage33Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(119, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr18() < 0)
                return;
        }

        private int _WritePage2FAddr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(50, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(73, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(96, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(119, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr18() < 0)
                return;
        }

        private int _WritePage30Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(51, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr19() < 0)
                return;
        }

        private int _WritePage31Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(74, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr19() < 0)
                return;
        }

        private int _WritePage32Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(97, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr19() < 0)
                return;
        }

        private int _WritePage33Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(120, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr19() < 0)
                return;
        }

        private int _WritePage2FAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(51, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(74, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(97, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(120, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr19() < 0)
                return;
        }

        private int _WritePage30Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(52, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1A() < 0)
                return;
        }

        private int _WritePage31Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(75, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1A() < 0)
                return;
        }

        private int _WritePage32Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(98, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1A() < 0)
                return;
        }

        private int _WritePage33Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(121, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1A() < 0)
                return;
        }

        private int _WritePage2FAddr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(52, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(75, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(98, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(121, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1A() < 0)
                return;
        }

        private int _WritePage30Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(53, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1B() < 0)
                return;
        }

        private int _WritePage31Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(76, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1B() < 0)
                return;
        }

        private int _WritePage32Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(99, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1B() < 0)
                return;
        }

        private int _WritePage33Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(122, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1B() < 0)
                return;
        }

        private int _WritePage2FAddr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(53, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(76, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(99, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(122, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1B() < 0)
                return;
        }

        private int _WritePage30Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(54, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private int _WritePage31Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(77, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private int _WritePage32Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(100, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private int _WritePage33Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(123, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private int _WritePage2FAddr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(54, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(77, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(100, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(123, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private int _WritePage30Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(55, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1D() < 0)
                return;
        }

        private int _WritePage31Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(78, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1D() < 0)
                return;
        }

        private int _WritePage32Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(101, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1D() < 0)
                return;
        }

        private int _WritePage33Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(124, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1D() < 0)
                return;
        }

        private int _WritePage2FAddr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(55, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(78, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(101, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(124, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1D() < 0)
                return;
        }

        private int _WritePage30Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(56, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1E() < 0)
                return;
        }

        private int _WritePage31Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(79, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1E() < 0)
                return;
        }

        private int _WritePage32Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(102, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1E() < 0)
                return;
        }

        private int _WritePage33Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1E() < 0)
                return;
        }

        private int _WritePage2FAddr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(56, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(79, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(102, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(125, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1E() < 0)
                return;
        }

        private int _WritePage30Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(57, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private int _WritePage31Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(80, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private int _WritePage32Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(103, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private int _WritePage33Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private int _WritePage2FAddr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(57, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(80, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(103, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private int _WritePage30Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(58, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private int _WritePage31Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(81, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private int _WritePage32Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(104, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private int _WritePage33Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private int _WritePage2FAddr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(58, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(81, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(104, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private int _WritePage30Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbCdrLbwAdjCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(59, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr41() < 0)
                return;
        }

        private int _WritePage31Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbCdrLbwAdjCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr41() < 0)
                return;
        }

        private int _WritePage32Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbCdrLbwAdjCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(105, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr41() < 0)
                return;
        }

        private int _WritePage33Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbCdrLbwAdjCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(128, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr41() < 0)
                return;
        }

        private int _WritePage2FAddr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbCdrLbwAdjChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(59, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(82, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(105, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(128, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr41() < 0)
                return;
        }

        private int _WritePage30Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(60, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr46() < 0)
                return;
        }

        private int _WritePage31Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(83, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr46() < 0)
                return;
        }

        private int _WritePage32Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(106, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr46() < 0)
                return;
        }

        private int _WritePage33Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(129, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr46() < 0)
                return;
        }

        private int _WritePage2FAddr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(60, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(83, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(106, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(129, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr46() < 0)
                return;
        }

        private int _WritePage30Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(61, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr47() < 0)
                return;
        }

        private int _WritePage31Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(84, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr47() < 0)
                return;
        }

        private int _WritePage32Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(107, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr47() < 0)
                return;
        }

        private int _WritePage33Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(130, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr47() < 0)
                return;
        }

        private int _WritePage2FAddr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(61, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(84, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(107, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(130, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr47() < 0)
                return;
        }

        private int _WritePage30Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(62, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr48() < 0)
                return;
        }

        private int _WritePage31Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(85, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr48() < 0)
                return;
        }

        private int _WritePage32Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(108, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr48() < 0)
                return;
        }

        private int _WritePage33Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(131, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr48() < 0)
                return;
        }

        private int _WritePage2FAddr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(62, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(85, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(108, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(131, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr48() < 0)
                return;
        }

        private int _WritePage40Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default

            if (cbTxDisableCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbMuteCntlCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(134, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDisableCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr00() < 0)
                return;
        }

        private void cbMuteCntlCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr00() < 0)
                return;
        }

        private int _WritePage41Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default

            if (cbTxDisableCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbMuteCntlCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(160, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDisableCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr00() < 0)
                return;
        }

        private void cbMuteCntlCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr00() < 0)
                return;
        }

        private int _WritePage42Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default

            if (cbTxDisableCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbMuteCntlCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(186, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDisableCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr00() < 0)
                return;
        }

        private void cbMuteCntlCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr00() < 0)
                return;
        }

        private int _WritePage43Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default

            if (cbTxDisableCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbMuteCntlCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(212, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDisableCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr00() < 0)
                return;
        }

        private void cbMuteCntlCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr00() < 0)
                return;
        }

        private int _WritePage3FAddr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default

            if (cbTxDisableChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbMuteCntlChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(134, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(160, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(186, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(212, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        
        private void cbTxDisableChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr00() < 0)
                return;
        }

        private void cbMuteCntlChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr00() < 0)
                return;
        }

        private int _WritePage40Addr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x40; //Default

            if (cbTxIgnoreFaultCh0.Checked == true)
                data[0] |= 0x20;

            if (cbTxFaultAlarmCh0.Checked == true)
                data[0] |= 0x10;

            if (cbTxFaultMaskCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(135, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIgnoreFaultCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private void cbTxFaultAlarmCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private void cbTxFaultMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private int _WritePage41Addr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x40; //Default

            if (cbTxIgnoreFaultCh1.Checked == true)
                data[0] |= 0x20;

            if (cbTxFaultAlarmCh1.Checked == true)
                data[0] |= 0x10;

            if (cbTxFaultMaskCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(161, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIgnoreFaultCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private void cbTxFaultAlarmCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private void cbTxFaultMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private int _WritePage42Addr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x40; //Default

            if (cbTxIgnoreFaultCh2.Checked == true)
                data[0] |= 0x20;

            if (cbTxFaultAlarmCh2.Checked == true)
                data[0] |= 0x10;

            if (cbTxFaultMaskCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(187, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIgnoreFaultCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private void cbTxFaultAlarmCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private void cbTxFaultMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private int _WritePage43Addr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x40; //Default

            if (cbTxIgnoreFaultCh3.Checked == true)
                data[0] |= 0x20;

            if (cbTxFaultAlarmCh3.Checked == true)
                data[0] |= 0x10;

            if (cbTxFaultMaskCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(213, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIgnoreFaultCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private void cbTxFaultAlarmCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private void cbTxFaultMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private int _WritePage3FAddr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x40; //Default

            if (cbTxIgnoreFaultChAll.Checked == true)
                data[0] |= 0x20;

            if (cbTxFaultAlarmChAll.Checked == true)
                data[0] |= 0x10;

            if (cbTxFaultMaskChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(135, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(161, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(187, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(213, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIgnoreFaultChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private void cbTxFaultAlarmChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private void cbTxFaultMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private int _WritePage40Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x08; //Default

            if (cbTxAlarmClearInterruptCh0.Checked == true)
                data[0] |= 0x40;

            if (cbTxAlarmClearPinCh0.Checked == true)
                data[0] |= 0x20;

            if (cbTxAlarmClearRegCh0.Checked == true)
                data[0] |= 0x10;

            rv = I2cWrite(136, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxAlarmClearInterruptCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearPinCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearRegCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr04() < 0)
                return;
        }

        private int _WritePage41Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x08; //Default

            if (cbTxAlarmClearInterruptCh1.Checked == true)
                data[0] |= 0x40;

            if (cbTxAlarmClearPinCh1.Checked == true)
                data[0] |= 0x20;

            if (cbTxAlarmClearRegCh1.Checked == true)
                data[0] |= 0x10;

            rv = I2cWrite(162, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxAlarmClearInterruptCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearPinCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearRegCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr04() < 0)
                return;
        }

        private int _WritePage42Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x08; //Default

            if (cbTxAlarmClearInterruptCh2.Checked == true)
                data[0] |= 0x40;

            if (cbTxAlarmClearPinCh2.Checked == true)
                data[0] |= 0x20;

            if (cbTxAlarmClearRegCh2.Checked == true)
                data[0] |= 0x10;

            rv = I2cWrite(188, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxAlarmClearInterruptCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearPinCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearRegCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr04() < 0)
                return;
        }

        private int _WritePage43Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x08; //Default

            if (cbTxAlarmClearInterruptCh3.Checked == true)
                data[0] |= 0x40;

            if (cbTxAlarmClearPinCh3.Checked == true)
                data[0] |= 0x20;

            if (cbTxAlarmClearRegCh3.Checked == true)
                data[0] |= 0x10;

            rv = I2cWrite(214, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxAlarmClearInterruptCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearPinCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr04() < 0)
                return;
        }

        private void cbTxAlarmClearRegCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr04() < 0)
                return;
        }

        private int _WritePage3FAddr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x08; //Default

            if (cbTxAlarmClearInterruptChAll.Checked == true)
                data[0] |= 0x40;

            if (cbTxAlarmClearPinChAll.Checked == true)
                data[0] |= 0x20;

            if (cbTxAlarmClearRegChAll.Checked == true)
                data[0] |= 0x10;

            rv = I2cWrite(136, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(162, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(188, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(214, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxAlarmClearInterruptChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr04() < 0)
                return;
        }

        private void cbTxAlarmClearPinChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr04() < 0)
                return;
        }

        private void cbTxAlarmClearRegChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr04() < 0)
                return;
        }

        private int _WritePage40Addr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPolinvCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(137, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr05() < 0)
                return;
        }

        private int _WritePage41Addr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPolinvCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(163, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr05() < 0)
                return;
        }

        private int _WritePage42Addr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPolinvCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(189, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr05() < 0)
                return;
        }

        private int _WritePage43Addr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPolinvCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(215, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr05() < 0)
                return;
        }

        private int _WritePage3FAddr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPolinvChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(137, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(163, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(189, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(215, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr05() < 0)
                return;
        }

        private int _WritePage40Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxRvcselAutodetectEnableCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxRvcselCh0.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxIbiasFinetuneCtrlCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(138, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxRvcselAutodetectEnableCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private void cbTxRvcselCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private void cbTxIbiasFinetuneCtrlCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private int _WritePage41Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxRvcselAutodetectEnableCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxRvcselCh1.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxIbiasFinetuneCtrlCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(164, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxRvcselAutodetectEnableCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private void cbTxRvcselCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private void cbTxIbiasFinetuneCtrlCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private int _WritePage42Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxRvcselAutodetectEnableCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxRvcselCh2.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxIbiasFinetuneCtrlCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(190, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxRvcselAutodetectEnableCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private void cbTxRvcselCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private void cbTxIbiasFinetuneCtrlCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private int _WritePage43Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxRvcselAutodetectEnableCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxRvcselCh3.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxIbiasFinetuneCtrlCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(216, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxRvcselAutodetectEnableCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private void cbTxRvcselCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private void cbTxIbiasFinetuneCtrlCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private int _WritePage3FAddr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxRvcselAutodetectEnableChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxRvcselChAll.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxIbiasFinetuneCtrlChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(138, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(164, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(190, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(216, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxRvcselAutodetectEnableChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private void cbTxRvcselChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private void cbTxIbiasFinetuneCtrlChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private int _WritePage40Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default
            bTmp = Convert.ToByte(cbTxIbiasCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(139, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr07() < 0)
                return;
        }

        private int _WritePage41Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default
            bTmp = Convert.ToByte(cbTxIbiasCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(165, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr07() < 0)
                return;
        }

        private int _WritePage42Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default
            bTmp = Convert.ToByte(cbTxIbiasCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(191, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr07() < 0)
                return;
        }

        private int _WritePage43Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default
            bTmp = Convert.ToByte(cbTxIbiasCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(217, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr07() < 0)
                return;
        }

        private int _WritePage3FAddr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x80; //Default
            bTmp = Convert.ToByte(cbTxIbiasChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(139, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(165, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(191, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(217, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr07() < 0)
                return;
        }

        private int _WritePage40Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxBurninEnCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxIburninCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(140, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBurninEnCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr08() < 0)
                return;
        }

        private void cbTxIburninCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr08() < 0)
                return;
        }

        private int _WritePage41Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxBurninEnCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxIburninCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(166, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBurninEnCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr08() < 0)
                return;
        }

        private void cbTxIburninCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr08() < 0)
                return;
        }

        private int _WritePage42Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxBurninEnCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxIburninCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(192, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBurninEnCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr08() < 0)
                return;
        }

        private void cbTxIburninCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr08() < 0)
                return;
        }

        private int _WritePage43Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxBurninEnCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxIburninCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(218, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBurninEnCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr08() < 0)
                return;
        }

        private void cbTxIburninCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr08() < 0)
                return;
        }

        private int _WritePage3FAddr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxBurninEnChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxIburninChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(140, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(166, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(192, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(218, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBurninEnChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr08() < 0)
                return;
        }

        private void cbTxIburninChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr08() < 0)
                return;
        }

        private int _WritePage40Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnBotCh0.Checked == true)
                data[0] |= 0x40;

            if (cbTxBotDcdDacPolflipCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdBotPdbCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(141, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private void cbTxBotDcdDacPolflipCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private int _WritePage41Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnBotCh1.Checked == true)
                data[0] |= 0x40;

            if (cbTxBotDcdDacPolflipCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdBotPdbCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(167, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private void cbTxBotDcdDacPolflipCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private int _WritePage42Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnBotCh2.Checked == true)
                data[0] |= 0x40;

            if (cbTxBotDcdDacPolflipCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdBotPdbCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(193, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private void cbTxBotDcdDacPolflipCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private int _WritePage43Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnBotCh3.Checked == true)
                data[0] |= 0x40;

            if (cbTxBotDcdDacPolflipCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdBotPdbCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(219, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private void cbTxBotDcdDacPolflipCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private int _WritePage3FAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnBotChAll.Checked == true)
                data[0] |= 0x40;

            if (cbTxBotDcdDacPolflipChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdBotPdbChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(141, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(167, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(193, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(219, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private void cbTxBotDcdDacPolflipChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private void cbDacTxDacDcdBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private int _WritePage40Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnMidCh0.Checked == true)
                data[0] |= 0x40;

            if (cbTxMidDcdDacPolflipCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdMidPdbCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(142, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private void cbTxMidDcdDacPolflipCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private int _WritePage41Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnMidCh1.Checked == true)
                data[0] |= 0x40;

            if (cbTxMidDcdDacPolflipCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdMidPdbCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(168, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private void cbTxMidDcdDacPolflipCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private int _WritePage42Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnMidCh2.Checked == true)
                data[0] |= 0x40;

            if (cbTxMidDcdDacPolflipCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdMidPdbCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(194, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private void cbTxMidDcdDacPolflipCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private int _WritePage43Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnMidCh3.Checked == true)
                data[0] |= 0x40;

            if (cbTxMidDcdDacPolflipCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdMidPdbCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(220, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private void cbTxMidDcdDacPolflipCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private int _WritePage3FAddr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnMidChAll.Checked == true)
                data[0] |= 0x40;

            if (cbTxMidDcdDacPolflipChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdMidPdbChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(142, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(168, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(194, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(220, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private void cbTxMidDcdDacPolflipChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private void cbDacTxDacDcdMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private int _WritePage40Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnTopCh0.Checked == true)
                data[0] |= 0x40;

            if (cbTxTopDcdDacPolflipCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdTopPdbCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(143, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private void cbTxTopDcdDacPolflipCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private int _WritePage41Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnTopCh1.Checked == true)
                data[0] |= 0x40;

            if (cbTxTopDcdDacPolflipCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdTopPdbCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(169, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private void cbTxTopDcdDacPolflipCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private int _WritePage42Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnTopCh2.Checked == true)
                data[0] |= 0x40;

            if (cbTxTopDcdDacPolflipCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdTopPdbCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(195, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private void cbTxTopDcdDacPolflipCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private int _WritePage43Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnTopCh3.Checked == true)
                data[0] |= 0x40;

            if (cbTxTopDcdDacPolflipCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdTopPdbCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(221, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private void cbTxTopDcdDacPolflipCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private int _WritePage3FAddr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxIdoubleEnTopChAll.Checked == true)
                data[0] |= 0x40;

            if (cbTxTopDcdDacPolflipChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdTopPdbChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(143, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(169, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(195, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(221, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIdoubleEnTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private void cbTxTopDcdDacPolflipChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private void cbDacTxDacDcdTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private int _WritePage40Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(144, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0E() < 0)
                return;
        }

        private int _WritePage41Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(170, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0E() < 0)
                return;
        }

        private int _WritePage42Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(196, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0E() < 0)
                return;
        }

        private int _WritePage43Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(222, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0E() < 0)
                return;
        }

        private int _WritePage3FAddr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(144, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(170, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(196, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(222, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0E() < 0)
                return;
        }

        private int _WritePage40Addr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(145, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0F() < 0)
                return;
        }

        private int _WritePage41Addr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(171, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0F() < 0)
                return;
        }

        private int _WritePage42Addr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(197, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0F() < 0)
                return;
        }

        private int _WritePage43Addr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(223, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0F() < 0)
                return;
        }

        private int _WritePage3FAddr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(145, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(171, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(197, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(223, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0F() < 0)
                return;
        }

        private int _WritePage40Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(146, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr10() < 0)
                return;
        }

        private int _WritePage41Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(172, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr10() < 0)
                return;
        }

        private int _WritePage42Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(198, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr10() < 0)
                return;
        }

        private int _WritePage43Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(224, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr10() < 0)
                return;
        }

        private int _WritePage3FAddr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbDacTxDacMain8bTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(146, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(172, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(198, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(224, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain8bTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr10() < 0)
                return;
        }

        private int _WritePage40Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(147, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreBotPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr11() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr11() < 0)
                return;
        }

        private int _WritePage41Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(173, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreBotPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr11() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr11() < 0)
                return;
        }

        private int _WritePage42Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(199, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreBotPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr11() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr11() < 0)
                return;
        }

        private int _WritePage43Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(225, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreBotPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr11() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr11() < 0)
                return;
        }

        private int _WritePage3FAddr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(147, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(173, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(199, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(225, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreBotPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr11() < 0)
                return;
        }

        private void cbDacTxDacPreBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr11() < 0)
                return;
        }

        private int _WritePage40Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreMidPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(148, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreMidPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr12() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr12() < 0)
                return;
        }

        private int _WritePage41Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreMidPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(174, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreMidPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr12() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr12() < 0)
                return;
        }

        private int _WritePage42Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreMidPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(200, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreMidPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr12() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr12() < 0)
                return;
        }

        private int _WritePage43Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreMidPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(226, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreMidPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr12() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr12() < 0)
                return;
        }

        private int _WritePage3FAddr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreBotPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(148, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(174, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(200, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(226, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreMidPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr12() < 0)
                return;
        }

        private void cbDacTxDacPreMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr12() < 0)
                return;
        }

        private int _WritePage40Addr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreTopPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(149, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreTopPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr13() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr13() < 0)
                return;
        }

        private int _WritePage41Addr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreTopPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(175, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreTopPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr13() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr13() < 0)
                return;
        }

        private int _WritePage42Addr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreTopPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(201, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreTopPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr13() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr13() < 0)
                return;
        }

        private int _WritePage43Addr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreTopPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(227, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreTopPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr13() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr13() < 0)
                return;
        }

        private int _WritePage3FAddr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPreTopPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPreTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(149, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(175, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(201, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(227, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPreTopPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr13() < 0)
                return;
        }

        private void cbDacTxDacPreTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr13() < 0)
                return;
        }

        private int _WritePage40Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(150, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostBotPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr14() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr14() < 0)
                return;
        }

        private int _WritePage41Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(176, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostBotPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr14() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr14() < 0)
                return;
        }

        private int _WritePage42Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(202, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostBotPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr14() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr14() < 0)
                return;
        }

        private int _WritePage43Addr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(228, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostBotPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr14() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr14() < 0)
                return;
        }

        private int _WritePage3FAddr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(150, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(176, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(202, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(228, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostBotPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr14() < 0)
                return;
        }

        private void cbDacTxDacPostBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr14() < 0)
                return;
        }

        private int _WritePage40Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostMidPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(151, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostMidPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr15() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr15() < 0)
                return;
        }

        private int _WritePage41Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostMidPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(177, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostMidPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr15() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr15() < 0)
                return;
        }

        private int _WritePage42Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostMidPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(203, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostMidPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr15() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr15() < 0)
                return;
        }

        private int _WritePage43Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostMidPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(229, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostMidPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr15() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr15() < 0)
                return;
        }

        private int _WritePage3FAddr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostBotPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(151, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(177, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(203, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(229, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostMidPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr15() < 0)
                return;
        }

        private void cbDacTxDacPostMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr15() < 0)
                return;
        }

        private int _WritePage40Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostTopPdbCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(152, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostTopPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr16() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr16() < 0)
                return;
        }

        private int _WritePage41Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostTopPdbCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(178, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostTopPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr16() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr16() < 0)
                return;
        }

        private int _WritePage42Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostTopPdbCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(204, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostTopPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr16() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr16() < 0)
                return;
        }

        private int _WritePage43Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostTopPdbCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(230, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostTopPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr16() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr16() < 0)
                return;
        }

        private int _WritePage3FAddr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPostTopPdbChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacTxDacPostTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(152, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(178, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(204, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(230, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPostTopPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr16() < 0)
                return;
        }

        private void cbDacTxDacPostTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr16() < 0)
                return;
        }

        private int _WritePage40Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxBotPostPolinvCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(153, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBotPostPolinvCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr17() < 0)
                return;
        }

        private int _WritePage41Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxBotPostPolinvCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(179, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBotPostPolinvCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr17() < 0)
                return;
        }

        private int _WritePage42Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxBotPostPolinvCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(205, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBotPostPolinvCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr17() < 0)
                return;
        }

        private int _WritePage43Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxBotPostPolinvCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(231, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBotPostPolinvCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr17() < 0)
                return;
        }

        private int _WritePage3FAddr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxBotPostPolinvChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(153, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(179, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(205, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(231, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxBotPostPolinvChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr17() < 0)
                return;
        }

        private int _WritePage40Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxMidPostPolinvCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(154, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxMidPostPolinvCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr18() < 0)
                return;
        }

        private int _WritePage41Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxMidPostPolinvCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(180, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxMidPostPolinvCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr18() < 0)
                return;
        }

        private int _WritePage42Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxMidPostPolinvCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(206, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxMidPostPolinvCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr18() < 0)
                return;
        }

        private int _WritePage43Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxMidPostPolinvCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(232, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxMidPostPolinvCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr18() < 0)
                return;
        }

        private int _WritePage3FAddr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxMidPostPolinvChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(154, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(180, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(206, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(232, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxMidPostPolinvChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr18() < 0)
                return;
        }

        private int _WritePage40Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxTopPostPolinvCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(155, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxTopPostPolinvCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr19() < 0)
                return;
        }

        private int _WritePage41Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxTopPostPolinvCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(181, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxTopPostPolinvCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr19() < 0)
                return;
        }

        private int _WritePage42Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxTopPostPolinvCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(207, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxTopPostPolinvCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr19() < 0)
                return;
        }

        private int _WritePage43Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxTopPostPolinvCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(233, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxTopPostPolinvCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr19() < 0)
                return;
        }

        private int _WritePage3FAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxTopPostPolinvChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(155, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(181, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(207, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(233, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        
        private void cbTxTopPostPolinvChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr19() < 0)
                return;
        }

        private int _WritePage40Addr21()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh0.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(156, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr21() < 0)
                return;
        }

        private void cbTxPdbLbkInCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr21() < 0)
                return;
        }

        private int _WritePage41Addr21()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh1.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(182, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr21() < 0)
                return;
        }

        private void cbTxPdbLbkInCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr21() < 0)
                return;
        }

        private int _WritePage42Addr21()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh2.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(208, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr21() < 0)
                return;
        }

        private void cbTxPdbLbkInCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr21() < 0)
                return;
        }

        private int _WritePage43Addr21()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh3.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(234, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr21() < 0)
                return;
        }

        private void cbTxPdbLbkInCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr21() < 0)
                return;
        }

        private int _WritePage3FAddr21()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutChAll.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(156, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(182, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(208, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(234, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr21() < 0)
                return;
        }

        private void cbTxPdbLbkInChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr21() < 0)
                return;
        }

        private int _WritePage40Addr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxEnableAutoImodCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(157, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnableAutoImodCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr23() < 0)
                return;
        }

        private int _WritePage41Addr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxEnableAutoImodCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(183, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnableAutoImodCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr23() < 0)
                return;
        }

        private int _WritePage42Addr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxEnableAutoImodCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(209, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnableAutoImodCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr23() < 0)
                return;
        }

        private int _WritePage43Addr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxEnableAutoImodCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(235, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnableAutoImodCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr23() < 0)
                return;
        }

        private int _WritePage3FAddr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxEnableAutoImodChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(157, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(183, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(209, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(235, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnableAutoImodChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr23() < 0)
                return;
        }

        private int _WritePage40Addr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxDesiredImodCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(158, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDesiredImodCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr24() < 0)
                return;
        }

        private int _WritePage41Addr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxDesiredImodCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(184, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDesiredImodCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr24() < 0)
                return;
        }

        private int _WritePage42Addr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxDesiredImodCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(210, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDesiredImodCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr24() < 0)
                return;
        }

        private int _WritePage43Addr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxDesiredImodCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(236, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDesiredImodCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr24() < 0)
                return;
        }

        private int _WritePage3FAddr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTxDesiredImodChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(158, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(184, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(210, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(236, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDesiredImodChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr24() < 0)
                return;
        }

        private int _WritePage40Addr26()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxIbiasAdcSelectCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(159, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasAdcSelectCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr26() < 0)
                return;
        }

        private int _WritePage41Addr26()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxIbiasAdcSelectCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(185, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasAdcSelectCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr26() < 0)
                return;
        }

        private int _WritePage42Addr26()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxIbiasAdcSelectCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(211, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasAdcSelectCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr26() < 0)
                return;
        }

        private int _WritePage43Addr26()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxIbiasAdcSelectCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(237, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasAdcSelectCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr26() < 0)
                return;
        }

        private int _WritePage3FAddr26()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxIbiasAdcSelectChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(159, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(185, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(211, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(237, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxIbiasAdcSelectChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr26() < 0)
                return;
        }
    }
}
