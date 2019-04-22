using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Mcp2221Adapter
{
    public class Mcp2221Api
    {
        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_GetLibraryVersion", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_GetLibraryVersion([MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder version);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_GetConnectedDevices", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_GetConnectedDevices(uint vid, uint pid, ref uint noOfDevs);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_GetSerialNumberDescriptor", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_GetSerialNumberDescriptor(IntPtr handle, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder serialNumber);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_GetFactorySerialNumber", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_GetFactorySerialNumber(IntPtr handle, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder serialNumber);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_OpenByIndex", CharSet = CharSet.Unicode)]
        private static extern IntPtr Mcp2221_OpenByIndex(uint VID, uint PID, uint index);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_OpenBySN", CharSet = CharSet.Unicode)]
        private static extern IntPtr Mcp2221_OpenBySN(uint VID, uint PID, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder serialNo);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_Close", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_Close(IntPtr handle);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_Reset", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_Reset(IntPtr handle);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_SetAdvancedCommParams", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_SetAdvancedCommParams(IntPtr handle, byte timeout, byte maxRetries);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_SetSpeed", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_SetSpeed(IntPtr handle, uint speed);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_I2cCancelCurrentTransfer", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_I2cCancelCurrentTransfer(IntPtr handle);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_I2cWrite", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_I2cWrite(IntPtr handle, uint bytesToWrite, byte slaveAddress, byte use7bitAddress, IntPtr i2cTxData);

        [DllImport("mcp2221_dll_um", EntryPoint = "Mcp2221_I2cRead", CharSet = CharSet.Unicode)]
        private static extern int Mcp2221_I2cRead(IntPtr handle, uint bytesToRead, byte slaveAddress, byte use7bitAddress, IntPtr i2cRxData);

        //[DllImport("mcp2221_dll_um_x64", EntryPoint = "Mcp2221_I2cReadRestart", CharSet = CharSet.Unicode)]
        //private static extern int Mcp2221_I2cReadRestart(IntPtr handle, uint bytesToRead, byte slaveAddress, byte use7bitAddress, IntPtr i2cRxData);

        /********************************
        Error codes
        *********************************/
        public const int E_NO_ERR = 0;
        public const int E_ERR_UNKOWN_ERROR = - 1;
        public const int E_ERR_CMD_FAILED = -2;
        public const int E_ERR_INVALID_HANDLE = -3;
        public const int E_ERR_INVALID_PARAMETER = -4;
        public const int E_ERR_INVALID_PASS = -5;
        public const int E_ERR_PASSWORD_LIMIT_REACHED = -6;
        public const int E_ERR_FLASH_WRITE_PROTECTED = -7;
        // null pointer received
        public const int E_ERR_NULL = -10;
        // destination string too small
        public const int E_ERR_DESTINATION_TOO_SMALL = -11;
        public const int E_ERR_INPUT_TOO_LARGE = -12;
        public const int E_ERR_FLASH_WRITE_FAILED = -13;
        public const int E_ERR_MALLOC = -14;

        //we tried to connect to a device with a non existent index
        public const int E_ERR_NO_SUCH_INDEX = -101;
        // no device matching the provided criteria was found
        public const int E_ERR_DEVICE_NOT_FOUND = -103;

        // one of the internal buffers of the function was too small
        public const int E_ERR_INTERNAL_BUFFER_TOO_SMALL = -104;
        // an error occurred when trying to get the device handle
        public const int E_ERR_OPEN_DEVICE_ERROR = -105;
        // connection already opened
        public const int E_ERR_CONNECTION_ALREADY_OPENED = -106;

        public const int E_ERR_CLOSE_FAILED = -107;

        /******* I2C errors *******/
        public const int E_ERR_INVALID_SPEED = -401;
        public const int E_ERR_SPEED_NOT_SET = -402;
        public const int E_ERR_INVALID_BYTE_NUMBER = -403;
        public const int E_ERR_INVALID_ADDRESS = -404;
        public const int E_ERR_I2C_BUSY = -405;
        //mcp2221 signaled an error during the i2c read operation
        public const int E_ERR_I2C_READ_ERROR = -406 ;
        public const int E_ERR_ADDRESS_NACK = -407;
        public const int E_ERR_TIMEOUT = -408;
        public const int E_ERR_TOO_MANY_RX_BYTES = -409;
        //could not copy the data received from the slave into the provided buffer;
        public const int E_ERR_COPY_RX_DATA_FAILED = -410;
        // failed to copy the data into the HID buffer
        public const int E_ERR_COPY_TX_DATA_FAILED = -412;
        // The i2c engine (inside mcp2221) was already idle. The cancellation command had no effect.
        public const int E_ERR_NO_EFFECT = -411;
        public const int E_ERR_INVALID_PEC = -413;
        // The slave sent a different value for the block size(byte count) than we expected
        public const int E_ERR_BLOCK_SIZE_MISMATCH = -414;

        public const int E_ERR_RAW_TX_TOO_LARGE = -301;
        public const int E_ERR_RAW_TX_COPYFAILED = -302;
        public const int E_ERR_RAW_RX_COPYFAILED = -303;
        
        public static string Mcp2221GetLibraryVersionApi()
        {
            var sbVersion = new StringBuilder(10);

            Mcp2221_GetLibraryVersion(sbVersion);

            return sbVersion.ToString();
        }

        public static int Mcp2221GetConnectedDevicesApi()
        {
            uint numberOfDevice = 0;
            int result;

            result = Mcp2221_GetConnectedDevices(0x4D8, 0xDD, ref numberOfDevice);
            if (result < 0)
                return result;

            return Convert.ToInt32(numberOfDevice);
        }

        public static int Mcp2221FindDevicesExtApi(int num_devices, ushort[] devices, int num_ids, string[] unique_ids)
        {
            IntPtr adapter;
            string serialNumber;
            uint i;
            int number;

            number = Mcp2221GetConnectedDevicesApi();

            if ((number > num_devices) || (number > num_ids))
                return E_ERR_INVALID_PARAMETER;

            for (i = 0; i < number; i++)
            {
                adapter = Mcp2221OpenByIndexApi(i);

                if (adapter == IntPtr.Zero)
                    continue;

                serialNumber = Mcp2221GetSerialNumberDescriptorApi(adapter);

                devices[i] = Convert.ToUInt16(i);
                unique_ids[i] = serialNumber;

                Mcp2221CloseApi(adapter);
            }
            
            return Convert.ToInt32(i);
        }

        public static IntPtr Mcp2221OpenByIndexApi(uint index)
        {
            return Mcp2221_OpenByIndex(0x4D8, 0xDD, index);
        }

        public static IntPtr Mcp2221OpenBySNApi(string serialNumber)
        {
            var sbSN = new StringBuilder(serialNumber);

            return Mcp2221_OpenBySN(0x4D8, 0xDD, sbSN);
        }

        public static int Mcp2221CloseApi(IntPtr adapter)
        {
            int result;

            if (adapter == IntPtr.Zero)
                return E_ERR_INVALID_HANDLE;

            result = Mcp2221_Close(adapter);
            if (result < 0)
                return result;

            return 0;
        }

        public static string Mcp2221GetSerialNumberDescriptorApi(IntPtr adapter)
        {
            var sbSN = new StringBuilder(30);

            Mcp2221_GetSerialNumberDescriptor(adapter , sbSN);

            return sbSN.ToString();
        }

        public static string Mcp2221GetFactorySerialNumberApi(IntPtr adapter)
        {
            var sbSN = new StringBuilder(30);

            Mcp2221_GetFactorySerialNumber(adapter, sbSN);

            return sbSN.ToString();
        }

        public static int Mcp2221SetAdvancedCommParamsApi(IntPtr adapter, byte timeout, byte maxRetries)
        {
            return Mcp2221_SetAdvancedCommParams(adapter, timeout, maxRetries);
        }

        public static int Mcp2221SetBitrateApi(IntPtr adapter, uint bitRate)
        {
            int result;

            if (adapter == IntPtr.Zero)
                return E_ERR_INVALID_HANDLE;

            result = Mcp2221_I2cCancelCurrentTransfer(adapter);
            if (result < 0)
                return result;
                

            result = Mcp2221_SetSpeed(adapter, bitRate);
            if (result < 0)
                return result;

            return 0;
        }

        public static int Mcp2221I2cWriteApi(IntPtr adapter, uint bytesToWrite, byte slaveAddress, byte use7bitAddress, byte[] txData)
        {
            IntPtr ipData;
            int result;

            result = Mcp2221_I2cCancelCurrentTransfer(adapter);
            if (result < 0)
                return result;

            ipData = Marshal.AllocHGlobal(Marshal.SizeOf(txData[0]) * Convert.ToInt32(bytesToWrite));
            Marshal.Copy(txData, 0, ipData, Convert.ToInt32(bytesToWrite));

            result = Mcp2221_I2cWrite(adapter, bytesToWrite, slaveAddress, use7bitAddress, ipData);
            if (result < 0)
                goto errorFree;

            Marshal.FreeHGlobal(ipData);

            return 0;

        errorFree:
            Marshal.FreeHGlobal(ipData);
            return result;
        }

        public static int Mcp2221I2cReadApi(IntPtr adapter, uint bytesToRead, byte slaveAddress, byte use7bitAddress, byte[] rxData)
        {
            IntPtr ipData;
            int result;

            result = Mcp2221_I2cCancelCurrentTransfer(adapter);
            if (result < 0)
                return result;

            ipData = Marshal.AllocHGlobal(Marshal.SizeOf(rxData[0]) * Convert.ToInt32(bytesToRead));

            result = Mcp2221_I2cRead(adapter, bytesToRead, slaveAddress, use7bitAddress, ipData);
            if (result < 0)
                goto errorFree;
            
            Marshal.Copy(ipData, rxData, 0, Convert.ToInt32(bytesToRead));

            Marshal.FreeHGlobal(ipData);
            return 0;

        errorFree:
            Marshal.FreeHGlobal(ipData);
            return result;
        }
    }
}
