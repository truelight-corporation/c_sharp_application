using NuvotonIcpTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedGuiV2
{
    internal class WaitFormFunc
    {
        LoadingForm wait;
        Thread loadthread;

        public void Show()
        {
            loadthread = new Thread(new ThreadStart(LoadingProcess));
            loadthread.Start();
        }

        public void Show(Form parent)
        {
            loadthread = new Thread(new ParameterizedThreadStart(LoadingProcess));
            loadthread.Start(parent);
        }

        public void Close()
        {
            if (wait != null)
            {
                wait.BeginInvoke(new ThreadStart(wait.CloseWaitForm));
                wait = null;
                loadthread = null;
            }
        }

        private void LoadingProcess()
        {
            wait = new LoadingForm();
            wait.ShowDialog();
        }

        private void LoadingProcess(object parent)
        {
            Form parent1 = parent as Form;
            wait = new LoadingForm(parent1);
            wait.ShowDialog();
        }

        internal void OnPluginWaiting()
        {
            if (wait != null && wait.InvokeRequired) {
                wait.Invoke(new MethodInvoker(() => wait.OnPluginWattingState()));
            }
            else {
                wait.OnPluginWattingState();
            }
        }

        internal void PluginDetected()
        {
            if (wait != null && wait.InvokeRequired) {
                wait.Invoke(new MethodInvoker(() => wait.InitialState()));
            }
            else {
                wait.InitialState();
            }
        }
    }
}
