using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsfpDigitalDiagnosticMonitoring
{
    public class TextBoxTextEventArgs : EventArgs
    {
        public string Text1 { get; private set; }
        public string Text2 { get; private set; }
        public string Text3 { get; private set; }
        public string Text4 { get; private set; }

        public TextBoxTextEventArgs(string text1, string text2, string text3, string text4)
        {
            Text1 = text1;
            Text2 = text2;
            Text3 = text3;
            Text4 = text4;
        }
    }
}
