using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace HT32DualImageTool
{
    public partial class UCFirmwareTool : UserControl
    {
        enum IAP_CODE_Enum : ushort
        { 
            IAP_CMD_START_IAP = 0x8101,
            IAP_CMD_START_APP = 0x8102,
            IAP_CMD_ERASE = 0x8103,
            IAP_CMD_FLASH = 0x8104,
            IAP_CMD_CRC = 0x8105,

            IAP_RET_OK = 0x5A01,
            IAP_RET_FAIL = 0x5A02,
            IAP_RET_CMDERROR = 0x5A03,
            IAP_RET_TIMEOUT = 0x5A04,
        }

        [StructLayout(LayoutKind.Explicit)]
        struct IAP_Protocol_TypeDef
        {
            [FieldOffset(0)] public IAP_CODE_Enum Code; // 2 Bytes
            [FieldOffset(2)] public IAP_CODE_Enum Check; // 2 Bytes
            [FieldOffset(4)] public byte uLength; // 1 Bytes
            [FieldOffset(5)] public byte uPara; // 1 Bytes
            [FieldOffset(6)] public ushort uCRC; // 2 Bytes
            [FieldOffset(8)] public uint uStAddr; // 4 Bytes
            [FieldOffset(12)] public uint uEndAddr; // 4 Bytes
        }

        enum IAP_PARA_Enum : byte
        {
            IAP_PARA_PROGRAM = 0x00,
            IAP_PARA_VERIFY = 0x01,
        }

        enum IAP_Enum : uint
        {
            IAP_SKIP_VECTOR = 0x40
        }

        public delegate int I2cReadRawCB(byte devAddr, byte length, byte[] data);
        public delegate int I2cWriteRawCB(byte devAddr, byte length, byte[] data);

        private I2cReadRawCB i2cReadRawCB = null;
        private I2cWriteRawCB i2cWriteRawCB = null;

        public UCFirmwareTool()
        {
            InitializeComponent();
            cbFunctionSelect.Items.Add("Get Version");
            cbFunctionSelect.Items.Add("Reset");
            cbFunctionSelect.Items.Add("Updata Application");
            cbFunctionSelect.Text = "Get Version";
        }

        public int SetI2cReadCBApi(I2cReadRawCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadRawCB = new I2cReadRawCB(cb);

            return 0;
        }

        public int SetI2cWriteCBApi(I2cWriteRawCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteRawCB = new I2cWriteRawCB(cb);

            return 0;
        }

        private int _BufDumpLog(byte[] buf, int length)
        {
            int i;

            for (i = 0; i < length; i++) {
                if (i % 16 == 0)
                    tbLog.AppendText("\r\n" + i.ToString("d8") + ": ");
                tbLog.AppendText(buf[i].ToString("X2") + " ");
            }
            tbLog.AppendText("\r\n");

            return 0;
        }

        private ushort _CalcCRC16(byte[] data, int start, int end)
        {
            ushort crc = 0x0000;
            
            for (int i = start; i < end; i++) {
                crc ^= (ushort)(data[i] << 8);
                for (int j = 0; j < 8; j++) {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }

        private int _CheckDeviceReady() 
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            IntPtr ptr;
            byte[] buf;

            if (i2cReadRawCB == null)
                return -1;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];

            if (i2cReadRawCB(0x70, 4, buf) < 0)
                return -1;

            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.Copy(buf, 0, ptr, sizeCmd);
            cmd = (IAP_Protocol_TypeDef)Marshal.PtrToStructure(ptr, typeof(IAP_Protocol_TypeDef));
            Marshal.FreeHGlobal(ptr);

            if ((ushort)cmd.Code + (ushort)cmd.Check == 0xFFFF) {
                if (cmd.Code != IAP_CODE_Enum.IAP_RET_OK) {
                    tbLog.AppendText("Rx buf[" + 4 + "]");
                    _BufDumpLog(buf, 4);
                }
                switch (cmd.Code) {
                    case IAP_CODE_Enum.IAP_RET_OK:
                        //tbLog.AppendText("[OK][" + cmd.Code.ToString("X") + "]\r\n");
                        break;

                    case IAP_CODE_Enum.IAP_RET_FAIL:
                        tbLog.AppendText("[FAIL][" + cmd.Code.ToString("X") + "]\r\n");
                        break;

                    case IAP_CODE_Enum.IAP_RET_CMDERROR:
                        tbLog.AppendText("[CMDERR][" + cmd.Code.ToString("X") + "]\r\n");
                        break;

                    default:
                        tbLog.AppendText("[RETFAIL][" + cmd.Code.ToString("X") + "]\r\n");
                        break;
                }
            }
            else {
                tbLog.AppendText("[RETFAIL][" + cmd.Code.ToString("X") + "]\r\n");
                tbLog.AppendText("Rx buf[" + 4 + "]");
                _BufDumpLog(buf, 4);
            }

            return 0;
        }

        private int _FlashRead()
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            byte[] buf;
            IntPtr ptr;

            if (i2cWriteRawCB == null)
                return -1;

            tbLog.AppendText("Get Version ... ");

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_FLASH;
            cmd.uLength = 12;
            cmd.uPara = 2;
            cmd.uStAddr = 0x10;
            cmd.uEndAddr = 0x10 + 12 - 1;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, false);
            Marshal.Copy(ptr, buf, 0, sizeCmd);

            cmd.uCRC = _CalcCRC16(buf, 4, sizeCmd);

            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, (byte)sizeCmd, buf) < 0)
                return -1;

            //tbLog.AppendText("\r\nTx buf[" + sizeCmd + "]");
            //_BufDumpLog(buf, sizeCmd);

            Thread.Sleep(2);

            _CheckDeviceReady();

            Thread.Sleep(10);
            tbLog.AppendText("Done.\r\n");

            return 0;
        }

        private int _StartAp()
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            byte[] buf;
            IntPtr ptr;

            if (i2cWriteRawCB == null)
                return -1;

            tbLog.AppendText("Reset ... ");

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_START_APP;
            cmd.uLength = 0;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, 4, buf) < 0)
                return -1;

            //tbLog.AppendText("\r\nTx buf[4]");
            //_BufDumpLog(buf, sizeCmd);

            Thread.Sleep(2);

            _CheckDeviceReady();

            Thread.Sleep(10);

            tbLog.AppendText("Done.\r\n");

            return 0;
        }

        private int _Erase()
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            byte[] buf;
            IntPtr ptr;

            if (i2cWriteRawCB == null)
                return -1;

            tbLog.AppendText("Erase ... ");

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_ERASE;
            cmd.uLength = 12;
            cmd.uPara = 0;
            cmd.uStAddr = 0;
            cmd.uEndAddr = (1024 * 29) - 1;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, false);
            Marshal.Copy(ptr, buf, 0, sizeCmd);

            cmd.uCRC = _CalcCRC16(buf, 4, sizeCmd);

            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, (byte)sizeCmd, buf) < 0)
                return -1;

            //tbLog.AppendText("\r\nTx buf[" + sizeCmd + "]");
            //_BufDumpLog(buf, sizeCmd);

            Thread.Sleep(50);

            _CheckDeviceReady();

            Thread.Sleep(10);

            tbLog.AppendText("Done.\r\n");

            return 0;
        }

        private int _EraseOb()
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            byte[] buf;
            IntPtr ptr;

            if (i2cWriteRawCB == null)
                return -1;

            tbLog.AppendText("Erase Option ... ");

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_ERASE;
            cmd.uLength = 12;
            cmd.uPara = 0;
            cmd.uStAddr = 0x1FF00000;
            cmd.uEndAddr = cmd.uStAddr + 1024 - 1;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, false);
            Marshal.Copy(ptr, buf, 0, sizeCmd);

            cmd.uCRC = _CalcCRC16(buf, 4, sizeCmd);

            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, (byte)sizeCmd, buf) < 0)
                return -1;

            tbLog.AppendText("\r\nTx buf[" + sizeCmd + "]");
            _BufDumpLog(buf, sizeCmd);

            Thread.Sleep(2);

            _CheckDeviceReady();

            Thread.Sleep(10);

            tbLog.AppendText("Done.\r\n");

            return 0;
        }

        private int _FlashAp(IAP_PARA_Enum type)
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd, index;
            byte[] buf, fileBuf, tmp;
            IntPtr ptr;
            ushort crc16;

            if (i2cWriteRawCB == null)
                return -1;

            if (tbApFilePath.Text.Length == 0) {
                tbLog.AppendText("Please select AP file!!!\r\n");
                return -2;
            }

            if (type == IAP_PARA_Enum.IAP_PARA_PROGRAM)
                tbLog.AppendText("Program AP ... ");
            else
                tbLog.AppendText("Verify AP ... ");

            fileBuf = File.ReadAllBytes(tbApFilePath.Text);

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_FLASH;
            cmd.uLength = 12;
            cmd.uPara = (byte)type;
            cmd.uStAddr = 0;
            cmd.uEndAddr = (uint)fileBuf.Length - 1;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, false);
            Marshal.Copy(ptr, buf, 0, sizeCmd);

            cmd.uCRC = _CalcCRC16(buf, 4, sizeCmd);

            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, (byte)sizeCmd, buf) < 0)
                return -1;

            //tbLog.AppendText("Tx buf[" + sizeCmd + "]");
            //_BufDumpLog(buf, sizeCmd);

            Thread.Sleep(2);

            _CheckDeviceReady();

            buf = new byte[130];
            index = 0;

            tbLog.AppendText(fileBuf.Length.ToString() + "\r\n");
            while (index < fileBuf.Length) {
                tbLog.AppendText(index.ToString() + " ");
                if (index + 128 < fileBuf.Length) {
                    Buffer.BlockCopy(fileBuf, index, buf, 0, 128);
                    index += 128;
                }
                else {
                    Buffer.BlockCopy(fileBuf, index, buf, 0, fileBuf.Length - index);
                    index += fileBuf.Length - index;
                }
                crc16 = _CalcCRC16(buf, 0, 128);
                tmp = BitConverter.GetBytes(crc16);
                Buffer.BlockCopy(tmp, 0, buf, 128, 2);
                if (i2cWriteRawCB(0x70, 130, buf) < 0)
                    return -1;

                //tbLog.AppendText("Tx buf[" + 130 + "]");
                //_BufDumpLog(buf, 130);
                Thread.Sleep(2);

                _CheckDeviceReady();

                Thread.Sleep(2);
            }
            tbLog.AppendText("Done.\r\n");


            Thread.Sleep(10);

            return 0;
        }

        private int _CrcAp()
        {
            IAP_Protocol_TypeDef cmd;
            int sizeCmd;
            byte[] buf, fileBuf;
            IntPtr ptr;
            ushort crc16;

            if (i2cWriteRawCB == null)
                return -1;

            if (tbApFilePath.Text.Length == 0) {
                tbLog.AppendText("Please select AP file!!!\r\n");
                return -2;
            }

            tbLog.AppendText("CRC AP ... ");

            fileBuf = File.ReadAllBytes(tbApFilePath.Text);

            cmd = new IAP_Protocol_TypeDef();
            cmd.Code = IAP_CODE_Enum.IAP_CMD_CRC;
            cmd.uLength = 12;
            cmd.uPara = 0;
            cmd.uStAddr = (uint)IAP_Enum.IAP_SKIP_VECTOR;
            cmd.uEndAddr = (uint)fileBuf.Length - 1;
            cmd.Check = ~(cmd.Code);
            cmd.uCRC = 0;

            sizeCmd = Marshal.SizeOf(typeof(IAP_Protocol_TypeDef));
            buf = new byte[sizeCmd];
            ptr = Marshal.AllocHGlobal(sizeCmd);
            Marshal.StructureToPtr(cmd, ptr, false);
            Marshal.Copy(ptr, buf, 0, sizeCmd);

            cmd.uCRC = _CalcCRC16(buf, 4, sizeCmd);

            Marshal.StructureToPtr(cmd, ptr, true);
            Marshal.Copy(ptr, buf, 0, sizeCmd);
            Marshal.FreeHGlobal(ptr);

            if (i2cWriteRawCB(0x70, (byte)sizeCmd, buf) < 0)
                return -1;

            //tbLog.AppendText("\r\nTx buf[" + sizeCmd + "]");
            //_BufDumpLog(buf, sizeCmd);

            Thread.Sleep(2);

            _CheckDeviceReady();

            crc16 = _CalcCRC16(fileBuf, (int)IAP_Enum.IAP_SKIP_VECTOR, fileBuf.Length);
            buf = BitConverter.GetBytes(crc16);
            if (i2cWriteRawCB(0x70, 2, buf) < 0)
                return -1;

            //tbLog.AppendText("Tx buf[" + 2 + "]");
            //_BufDumpLog(buf, 2);

            Thread.Sleep(2);

            _CheckDeviceReady();

            Thread.Sleep(10);

            tbLog.AppendText("Done.\r\n");

            return 0;
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            switch (cbFunctionSelect.SelectedItem) {
                case "Get Version":
                    _FlashRead();
                    break;

                case "Reset":
                    _StartAp();
                    break;

                case "Updata Application":
                    _Erase();
                    if (tbApFilePath.Text.Length == 0)
                        bSelectApFile_Click(sender, e);
                    _FlashAp(IAP_PARA_Enum.IAP_PARA_PROGRAM);
                    _FlashAp(IAP_PARA_Enum.IAP_PARA_VERIFY);
                    _CrcAp();
                    _StartAp();
                    break;

                default:
                    break;
            }

            
            
            return;
        }

        private void bClearLog_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
        }

        private void bSelectApFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Title = "Select Ap file";
            ofdSelectFile.Filter = "bin files (*.bin)|*.bin";

            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbApFilePath.Text = ofdSelectFile.FileName;
            tbApFilePath.SelectionStart = tbApFilePath.Text.Length;
            tbApFilePath.ScrollToCaret();
        }
    }
}
