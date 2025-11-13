using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpticalLedManager
{
    public partial class arrayview : Form
    {

        public LedArrayController ledController;

        public arrayview()
        {
            InitializeComponent();

            ledController = new LedArrayController(this);

            ledController.ClearAllLeds();
        }

        private void picTX4_Load(object sender, EventArgs e)
        {

            ledController.SetTX4(true);   
            ledController.SetTX5(false);  
            ledController.SetRX1(true);   

            ledController.SetAllCameraSideLeds(
                tx4: true,   // TX4
                tx5: true,   // TX5
                tx6: false,  // TX6
                cmdR: true,  // Cmd_R
                cmdT: false, // Cmd_T
                tx1: true,   // TX1
                tx2: false,  // TX2
                tx3: true    // TX3
            );

            bool tx4IsOn = ledController.GetTX4State();
            if (tx4IsOn)
            {

            }

        }
            public LedArrayController LedController
        {
            get { return ledController; }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
