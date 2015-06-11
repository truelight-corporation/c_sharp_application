using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AardvarkAdapter;

namespace I2cMasterInterface
{
    public partial class AdapterSelector : UserControl
    {
        public enum AdapterType : int {
            AS_AT_DUMMY = 0,
            AS_AT_AARDVARK = 1,
            AS_AT_UI051 = 2
        }

        public delegate int AdapterSelectedCB(AdapterType type, int port);

        private AdapterSelectedCB adapterSelectedCB;
        private BindingList<string> lAdapter = new BindingList<string>();
        private ushort[] aardvarkPorts = new ushort[16];
        private uint[] aardvarkUniqueIds = new uint[16];

        public int AdapterSelectorSetAdapterSelectedCBApi(AdapterSelectedCB cb)
        {
            if (cb == null)
                return -1;

            adapterSelectedCB = new AdapterSelectedCB(cb);

            return 0;
        }

        public AdapterSelector()
        {
            InitializeComponent();
            lbAardvark.DataSource = lAdapter;
        }

        public int UpdateAdapterApi()
        {
            string tmp;
            int numElem = 16;
            int i, count;

            lAdapter.Clear();

            // Find all the attached devices
            count = AardvarkApi.aa_find_devices_ext(numElem, aardvarkPorts, numElem, aardvarkUniqueIds);

            if (count > numElem) {
                MessageBox.Show("Find " + count + " device!!");
                count = numElem;
            }

            if (count == 0) {
                lAdapter.Add("Cannot find any adapter!!");
                return 0;
            }

            for (i = 0; i < count; i++) {
                if ((aardvarkPorts[i] & AardvarkApi.AA_PORT_NOT_FREE) != 0) {
                    aardvarkPorts[i] &= unchecked((ushort)~AardvarkApi.AA_PORT_NOT_FREE);
                    tmp = string.Format("{0,-3}: {1:d4}-{2:d6} {3}", aardvarkPorts[i], aardvarkUniqueIds[i] / 1000000, aardvarkUniqueIds[i] % 1000000, "(Busy)");
                }
                else
                    tmp = string.Format("{0,-3}: {1:d4}-{2:d6}", aardvarkPorts[i], aardvarkUniqueIds[i] / 1000000, aardvarkUniqueIds[i] % 1000000);

                lAdapter.Add(tmp);
            }

            return 0;
        }

        private void bLBAUpdate_Click(object sender, EventArgs e)
        {
            UpdateAdapterApi();
        }

        private void _lbAardvarkDoubleClick(object sender, EventArgs e)
        {
            adapterSelectedCB(AdapterType.AS_AT_AARDVARK, aardvarkPorts[lbAardvark.SelectedIndex]);
        }
    }
}
