using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuvotonIcpTool
{
    public class MessageEventArgs: EventArgs
    {
        public string Message { get; }
        public int ProgressValue { get; }

        public MessageEventArgs(string message, int progressValue)
        {
            Message = message;
            ProgressValue = progressValue;
        }
    }
}
