using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AardvarkAdapter;
using Mcp2221Adapter;
using System.Runtime.Remoting.Channels;

namespace I2cMasterInterface
{
    public partial class AdapterSelector : UserControl
    {
        public enum AdapterType : int {
            AS_AT_DUMMY = 0,
            AS_AT_AARDVARK = 1,
            AS_AT_MCP2221 = 2
        }

        public delegate int AdapterSelectedCB(AdapterType type, int port);

        private AdapterSelectedCB adapterSelectedCB;
        private BindingList<string> lAardvarkAdapter = new BindingList<string>();
        private BindingList<string> lMcp2221Adapter = new BindingList<string>();
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
            lbAardvark.DataSource = lAardvarkAdapter;
            lbMcp2221.DataSource = lMcp2221Adapter;
        }

        public int UpdateAardvarkAdapterApi()
        {
            string tmp;
            int numElem = 16;
            int i, count;

            lAardvarkAdapter.Clear();

            // Find all the attached devices
            count = AardvarkApi.aa_find_devices_ext(numElem, aardvarkPorts, numElem, aardvarkUniqueIds);

            if (count > numElem) {
                MessageBox.Show("Find " + count + " device!!");
                count = numElem;
            }

            if (count == 0) {
                lAardvarkAdapter.Add("Cannot find any adapter!!");
                return 0;
            }

            for (i = 0; i < count; i++) {
                if ((aardvarkPorts[i] & AardvarkApi.AA_PORT_NOT_FREE) != 0) {
                    aardvarkPorts[i] &= unchecked((ushort)~AardvarkApi.AA_PORT_NOT_FREE);
                    tmp = string.Format("{0,-3}: {1:d4}-{2:d6} {3}", aardvarkPorts[i], aardvarkUniqueIds[i] / 1000000, aardvarkUniqueIds[i] % 1000000, "(Busy)");
                }
                else
                    tmp = string.Format("{0,-3}: {1:d4}-{2:d6}", aardvarkPorts[i], aardvarkUniqueIds[i] / 1000000, aardvarkUniqueIds[i] % 1000000);

                lAardvarkAdapter.Add(tmp);
            }

            if (lAardvarkAdapter.Count == 1)
            {
                lbAardvark.SelectedIndex = 0; // 自動選擇唯一的一筆
                if (adapterSelectedCB(AdapterType.AS_AT_AARDVARK, aardvarkPorts[0]) < 0)
                    return -1;
            }

            return 0;
        }

        private void bAardvarkUpdate_Click(object sender, EventArgs e)
        {
            UpdateAardvarkAdapterApi();
        }

        private void _lbAardvarkDoubleClick(object sender, EventArgs e)
        {
            adapterSelectedCB(AdapterType.AS_AT_AARDVARK, aardvarkPorts[lbAardvark.SelectedIndex]);
        }

        public int UpdateMcp2221AdapterApi()
        {
            ushort[] deviceIndex = new ushort[16];
            string[] deviceSerialNumber = new string[16];
            string tmp;
            int i, count;
            
            lMcp2221Adapter.Clear();

            // Find all the attached devices
            count = Mcp2221Api.Mcp2221FindDevicesExtApi(16, deviceIndex, 16, deviceSerialNumber);

            if (count == 0) {
                lMcp2221Adapter.Add("Cannot find any adapter!!");
                return 0;
            }

            for (i = 0; i < count; i++) {
                tmp = deviceIndex[i].ToString() + "  : " + deviceSerialNumber[i].ToString();
                lMcp2221Adapter.Add(tmp);
            }

            return 0;
        }

        private void bMcp2221Update_Click(object sender, EventArgs e)
        {
            UpdateMcp2221AdapterApi();
        }

        private void lbMcp2221_DoubleClick(object sender, EventArgs e)
        {
            adapterSelectedCB(AdapterType.AS_AT_MCP2221, lbMcp2221.SelectedIndex);
        }

        public int CheckAardvarkAdapterCountApi()
        {
            return lbAardvark.Items.Count;
        }

        public int ConnectFirstAardvarkAdapter()
        {
            if (adapterSelectedCB == null)
                goto error;

            if (adapterSelectedCB(AdapterSelector.AdapterType.AS_AT_AARDVARK, aardvarkPorts[0]) < 0)
                goto error;

            return 0;

        error:
            return -1;
        }
    }
}
