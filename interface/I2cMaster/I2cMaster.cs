using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AardvarkAdapter;

namespace I2cMasterInterface
{
    public class I2cMaster
    {
        private delegate int AdapterConnectedCB(int handler);

        private FAdapterSelect fASelect;
        private int iHandler = -1;
        private int iBitrate = 10; //kbps
        private AdapterSelector.AdapterType as_atAdapterType = AdapterSelector.AdapterType.AS_AT_DUMMY;

        private int _AardvarkDisconnect()
        {
            if (iHandler <= 0)
                return 0;

            if (as_atAdapterType != AdapterSelector.AdapterType.AS_AT_AARDVARK)
                return -1;

            AardvarkApi.aa_close(iHandler);

            iHandler = -1;
            as_atAdapterType = AdapterSelector.AdapterType.AS_AT_DUMMY;

            return 0;
        }

        ~I2cMaster()
        {
            switch (as_atAdapterType) {
                case AdapterSelector.AdapterType.AS_AT_DUMMY:
                    break;

                case AdapterSelector.AdapterType.AS_AT_AARDVARK:
                    break;

                case AdapterSelector.AdapterType.AS_AT_UI051:
                    break;

                default:
                    break;
            }
        }

        private int _AardvarkConnect(int port)
        {
            int bitrate;

            if ((port < 0) || (port > 16))
                return -1;

            if (iHandler > 0)
                goto Exit;

            iHandler = AardvarkApi.aa_open(port);
            if (iHandler <= 0) {
                MessageBox.Show("Please check I2C adapter connect!!");
                goto Error;
            }

            // Ensure that the I2C subsystem is enabled
            AardvarkApi.aa_configure(iHandler, AardvarkConfig.AA_CONFIG_SPI_I2C);

            // Enable the I2C bus pullup resistors (2.2k resistors).
            // This command is only effective on v2.0 hardware or greater.
            // The pullup resistors on the v1.02 hardware are enabled by default.
            AardvarkApi.aa_i2c_pullup(iHandler, AardvarkApi.AA_I2C_PULLUP_BOTH);

            // Set the bitrate
            bitrate = AardvarkApi.aa_i2c_bitrate(iHandler, iBitrate);

        Exit:
            return iHandler;

        Error:
            return -1;
        }

        private int SelectAdapterCB(AdapterSelector.AdapterType type, int port)
        {
            switch (type) {
                case AdapterSelector.AdapterType.AS_AT_DUMMY:
                    iHandler = 0;
                    break;

                case AdapterSelector.AdapterType.AS_AT_AARDVARK:
                    if (_AardvarkConnect(port) < 0)
                        goto Error;
                    break;

                case AdapterSelector.AdapterType.AS_AT_UI051:
                    // TODO: handle ui051 type
                    break;

                default:
                    iHandler = -1;
                    goto Error;
            }

            as_atAdapterType = type; 
            
            return 0;

        Error:
            return -1;
        }

        public int DisconnectApi()
        {
            switch (as_atAdapterType) {
                case AdapterSelector.AdapterType.AS_AT_DUMMY:
                    break;

                case AdapterSelector.AdapterType.AS_AT_AARDVARK:
                    return _AardvarkDisconnect();

                case AdapterSelector.AdapterType.AS_AT_UI051:
                    break;

                default:
                    break;
            }
            
            return 0;
        }

        public int SetBitRateApi(int bitrate)
        {
            if ((bitrate < 10) || (bitrate > 400))
                return -1;

            iBitrate = bitrate;

            return iBitrate;
        }

        public int GetBitrateApi()
        {
            return iBitrate;
        }

        public int ConnectApi()
        {
            if (fASelect == null) {
                fASelect = new FAdapterSelect();
                fASelect.adapterSelector1.AdapterSelectorSetAdapterSelectedCBApi(SelectAdapterCB);
            }

            fASelect.Show();

            return 0;
        }

        public FAdapterSelect GetAdapterSelectFormApi()
        {
            return fASelect;
        }

        private int _AardvarkRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            byte[] reg = { regAddr };

            AardvarkApi.aa_i2c_write(iHandler, devAddr, AardvarkI2cFlags.AA_I2C_NO_STOP, 1, reg);

            return AardvarkApi.aa_i2c_read(iHandler, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, length, data);
        }

        public int ReadApi(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            if (iHandler <= 0) {
                if (ConnectApi() < 0)
                    MessageBox.Show("I2cMasterConnectApi() fail!!");
                return -1;
            }

            switch (as_atAdapterType) {
                case AdapterSelector.AdapterType.AS_AT_DUMMY:
                    break;

                case AdapterSelector.AdapterType.AS_AT_AARDVARK:
                    return _AardvarkRead(devAddr, regAddr, length, data);

                case AdapterSelector.AdapterType.AS_AT_UI051:
                    break;

                default:
                    break;
            }

            return 0;
        }

        private int _AardvarkWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            byte[] buf = new byte[length + 1];
            
            buf[0] = regAddr;
            Array.Copy(data, 0, buf, 1, length);

            AardvarkApi.aa_i2c_write(iHandler, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, Convert.ToByte(length + 1), data);

            return 0;
        }

        public int WriteApi(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            if (iHandler <= 0) {
                if (ConnectApi() < 0)
                    MessageBox.Show("I2cMasterConnectApi() fail!!");
                return -1;
            }

            switch (as_atAdapterType) {
                case AdapterSelector.AdapterType.AS_AT_DUMMY:
                    break;

                case AdapterSelector.AdapterType.AS_AT_AARDVARK:
                    
                    break;

                case AdapterSelector.AdapterType.AS_AT_UI051:
                    break;

                default:
                    break;
            }

            return 0;
        }
    }
}
