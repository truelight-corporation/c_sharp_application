using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedGuiV2
{
    public class ReadStateUpdateEventArgs : EventArgs
    {
        public string Message { get; set; }

        public ReadStateUpdateEventArgs(string message)
        {
            Message = message;
        }
    }
}
