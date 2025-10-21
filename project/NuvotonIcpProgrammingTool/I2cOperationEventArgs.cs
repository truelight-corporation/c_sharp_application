using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuvotonIcpTool
{
    public enum I2cOperationType
    {
        Connect,
        Disconnect,
        SetChannel
    }

    public class I2cOperationEventArgs: EventArgs
    {
        public I2cOperationType OperationType { get; private set; }
        public int Channel { get; private set; }

        public I2cOperationEventArgs(I2cOperationType operationType, int channel)
        {
            OperationType = operationType;
            Channel = channel;
        }
    }
}
