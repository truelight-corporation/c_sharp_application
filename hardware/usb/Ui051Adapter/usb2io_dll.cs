using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace Ui051Adapter
{
    class usb2io_dll
    {
        /***********设备操作函数;以下函数中的USB2IO_hdl是指USB2IO_Open返回的句柄*******/
        //打开端口获取句柄;  //Nbr是端口号，从1开始，依次为2/3/4...，最大126
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_Open", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Usb2ioOpen(int Nbr);
        //关闭端口；在程序退出前再关闭端口; 返回 0: 成功；!0: 失败
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_Close", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioClose(IntPtr USB2IO_hdl);
        //获取设备序列号
        //     参数:
        //         dwp_LenResponse: 设备序列号的长度，取值范围0~256。(单位: 字节)
        //         ucp_Response: 设备序列号buf(buf由调用该API的应用程序分配)
        //      返回 0: 成功；!0: 失败
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_GetDevSn", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioGetDevSn(IntPtr USB2IO_hdl, ref int dwp_LenResponse, ref byte ucp_Response);

        /***********IO操作函数;以下函数中的USB2IO_hdl是指USB2IO_Open返回的句柄*******/
        /** 以下所有接口函数                                                       **/
        /**  IoNbr: IO口(1~10)                                                     **/
        /**  返回值 0: 成功；!0: 失败                                              **/
        //设置IO输出方式：IoCfg:0=输入; IoCfg:2=OD+上拉, 3=PushPull推挽输出;
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetIoCfg", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetIoCfg(IntPtr USB2IO_hdl, int IoNbr, int IoCfg);
        //设置IO输出值：  IoOut:1=输出高, 0=输出低
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetIoOut", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetIoOut(IntPtr USB2IO_hdl, int IoNbr, int IoOut);
        //读取IO输出配置, 读取值存放在IoCfg中
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_RdIoCfg", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioRdIoCfg(IntPtr USB2IO_hdl, int IoNbr, ref int IoCfg);
        //读取IO输出值(这里读取到的是上位机用USB2IO_SetIoOut的设置值), 读取值存放在IoOut中
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_RdIoOut", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioRdIoOut(IntPtr USB2IO_hdl, int IoNbr, ref int IoOut);
        //读取IO引脚值(这里读取到的是实际引脚的电平值), 读取值存放在PinValue中
        //注意：IO输出值是设置IO输出寄存器；IO引脚值是芯片引脚上的实际电平值。
        //      两者一般情况下是相等的，但如果该IO受到外围MCU或其他外设影响，两者有可能不相等，此时需要仔细查看外围电路是否有问题
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_RdPin", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioRdPin(IntPtr USB2IO_hdl, int IoNbr, ref int PinValue);
        //读取ClkOut输出： ClkOutCfg: 1=输出Clk, 0=关闭Clk
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetClkOut", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetClkOut(IntPtr USB2IO_hdl, int ClkOutCfg);
        //读取ClkOut的输出配置, 读取值存放在ClkOutCfg中
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_RdClkOutCfg", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioRdClkOutCfg(IntPtr USB2IO_hdl, ref int ClkOutCfg);

        /***********SPI操作函数;以下函数中的USB2IO_hdl是指USB2IO_Open返回的句柄******/
        /** 以下所有接口函数                                                       **/
        /**  返回值 0: 成功；!0: 失败                                              **/
        /*********标准的SPI MODE 定义如下********************************************/
        /**    MODE0 CPOL=0 CPHA=0 SPI_CLK空闲时是低电平; 上升沿采样               **/
        /**    MODE1 CPOL=0 CPHA=1 SPI_CLK空闲时是低电平; 下降沿采样               **/
        /**    MODE2 CPOL=1 CPHA=0 SPI_CLK空闲时是高电平; 下降沿采样               **/
        /**    MODE3 CPOL=1 CPHA=1 SPI_CLK空闲时是高电平; 上升沿采样               **/
        /****************************************************************************/
        //使能SPI接口，SPI接口的相关IO配置将自动改变
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_EnableSpi", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioEnableSpi(IntPtr USB2IO_hdl);
        //SPI写: DataLength:数据长度(1~136字节); DataBuf:数据buf
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SpiWrite", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSpiWrite(IntPtr USB2IO_hdl, int DataLength, ref byte DataBuf);
        //SPI读: DataLength:期望读的数据长度(1~136字节); ; DataBuf:数据buf(大小至少为256字节)
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SpiRead", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSpiRead(IntPtr USB2IO_hdl, int DataLength, ref byte DataBuf);
        //设置SPI MODE: Mode0~3
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetSpiMode", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetSpiMode(IntPtr USB2IO_hdl, int SpiMode);
        //读取SPI MODE: Mode0~3
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_GetSpiMode", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioGetSpiMode(IntPtr USB2IO_hdl, ref int SpiMode);

        /***********I2C操作函数;以下函数中的USB2IO_hdl是指USB2IO_Open返回的句柄******/
        /** 以下所有接口函数                                                       **/
        /**  返回值 0: 成功；!0: 失败                                              **/
        //使能I2C接口，I2C接口的相关IO配置将自动改变
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_EnableI2c", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioEnableI2c(IntPtr USB2IO_hdl);
        //I2C写: 
        //I2cAddr:I2C设备地址; 
        //CmdRegAddr: 命令或寄存器地址, 只有当CmdRegAddrExistFlag!=0时有效; 
        //CmdRegAddrByteCnt: CmdRegAddr的长度(0~4)(单位:字节)  0: 不存在CmdRegAddr
        //DataLength:数据长度(1~136字节); 
        //DataBuf:数据buf
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_I2cWrite", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioI2cWrite(IntPtr USB2IO_hdl, int I2cAddr, int CmdRegAddr, int CmdRegAddrByteCnt, int DataLength, ref byte DataBuf);
        //I2C读: 
        //I2cAddr:I2C设备地址; 
        //CmdRegAddr: 命令或寄存器地址, 只有当CmdRegAddrByteCnt!=0时有效; 
        //CmdRegAddrByteCnt: CmdRegAddr的长度(0~4)(单位:字节)  0: 不存在CmdRegAddr
        //DataLength:期望读的数据长度(1~136字节); 
        //DataBuf:返回数据的数据buf(大小至少为256字节)
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_I2cRead", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioI2cRead(IntPtr USB2IO_hdl, int I2cAddr, int CmdRegAddr, int CmdRegAddrByteCnt, int DataLength, ref byte DataBuf);
        //设置I2C速度: 0:80k 1:160k
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetI2cSpeed", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetI2cSpeed(IntPtr USB2IO_hdl, int I2cSpeed);
        //读取I2C速度: 0:80k 1:160k
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_GetI2cSpeed", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioGetI2cSpeed(IntPtr USB2IO_hdl, ref int I2cSpeed);

        /***********UART操作函数;以下函数中的USB2IO_hdl是指USB2IO_Open返回的句柄******/
        /**  以下所有接口函数                                                       **/
        /**  返回值 0: 成功；!0: 失败                                               **/
        //使能UART接口，UART接口的相关IO配置将自动改变
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_EnableUart", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioEnableUart(IntPtr USB2IO_hdl);
        //UART发送接收
        //返回值：0:成功; 1:接收超时，接收到的实际数据长度为ActualReadDataLength; -1:失败
        //WrLen: 待发送的数据长度(取值范围0~136;0表示无数据发送)
        //WrBuf: 待发送的数据buf
        //ExpRdLen: 期望接收的数据长度(取值范围0~136;0表示无需接收数据)
        //ActRdLen: 实际接收到的数据长度(取值范围0~136;0表示无需接收数据)
        //RdBuf: 返回数据的数据buf(大小至少为256字节)
        //TimeOut:接收数据才需要；等待数据的时间(单位: s)。取值范围: 3~600s(也就是10分钟)
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_UartWrRd", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioUartWrRd(IntPtr USB2IO_hdl, int WrLen, ref byte WrBuf, int ExpRdLen, ref int ActRdLen, ref byte RdBuf, int TimeOut);
        //设置波特率: UartBr=115200或9600
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_SetUartBr", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioSetUartBr(IntPtr USB2IO_hdl, int UartBr);
        //读取波特率 115200或9600
        [DllImport("USB2IO.dll", EntryPoint = "USB2IO_GetUartBr", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int Usb2ioGetUartBr(IntPtr USB2IO_hdl, ref int UartBr);
    }
}

