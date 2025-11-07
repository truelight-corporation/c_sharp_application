using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OpticalLedManager
{

    public class LedArrayController
    {
        private readonly Form parentForm;
        private Image greenLedImage;
        private Image grayLedImage;

        // Camera Side PictureBoxes
        private PictureBox pbTX4;
        private PictureBox pbTX5;
        private PictureBox pbTX6;
        private PictureBox pbCameraCmd_R;
        private PictureBox pbCameraCmd_T;
        private PictureBox pbTX1;
        private PictureBox pbTX2;
        private PictureBox pbTX3;

        // F-G Side PictureBoxes
        private PictureBox pbRX4;
        private PictureBox pbRX5;
        private PictureBox pbRX6;
        private PictureBox pbFGCmd_T;
        private PictureBox pbFGCmd_R;
        private PictureBox pbRX1;
        private PictureBox pbRX2;
        private PictureBox pbRX3;

        public LedArrayController(Form form)
        {
            parentForm = form;
            LoadLedImages();
            FindAllPictureBoxes();
        }

        private void LoadLedImages()
        {
            try
            {
                greenLedImage = Properties.Resources.greenled;
                grayLedImage = Properties.Resources.greyled;
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法載入 LED 圖片資源: " + ex.Message, "警告",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void FindAllPictureBoxes()
        {
            // Camera Side
            pbTX4 = FindControl<PictureBox>("pbTX4");
            pbTX5 = FindControl<PictureBox>("pbTX5");
            pbTX6 = FindControl<PictureBox>("pbTX6");
            pbCameraCmd_R = FindControl<PictureBox>("pbCameraCmd_R");
            pbCameraCmd_T = FindControl<PictureBox>("pbCameraCmd_T");
            pbTX1 = FindControl<PictureBox>("pbTX1");
            pbTX2 = FindControl<PictureBox>("pbTX2");
            pbTX3 = FindControl<PictureBox>("pbTX3");

            // F-G Side
            pbRX4 = FindControl<PictureBox>("pbRX4");
            pbRX5 = FindControl<PictureBox>("pbRX5");
            pbRX6 = FindControl<PictureBox>("pbRX6");
            pbFGCmd_T = FindControl<PictureBox>("pbFGCmd_T");
            pbFGCmd_R = FindControl<PictureBox>("pbFGCmd_R");
            pbRX1 = FindControl<PictureBox>("pbRX1");
            pbRX2 = FindControl<PictureBox>("pbRX2");
            pbRX3 = FindControl<PictureBox>("pbRX3");
        }
        private T FindControl<T>(string name) where T : Control
        {
            return FindControlRecursive<T>(parentForm, name);
        }

        private T FindControlRecursive<T>(Control parent, string name) where T : Control
        {
            if (parent.Name == name && parent is T)
                return parent as T;

            foreach (Control child in parent.Controls)
            {
                T result = FindControlRecursive<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        #region Camera Side LED 控制

        public void SetTX4(bool isOn)
        {
            SetLedState(pbTX4, isOn);
        }

        public void SetTX5(bool isOn)
        {
            SetLedState(pbTX5, isOn);
        }

        public void SetTX6(bool isOn)
        {
            SetLedState(pbTX6, isOn);
        }

        public void SetCameraCmd_R(bool isOn)
        {
            SetLedState(pbCameraCmd_R, isOn);
        }

        public void SetCameraCmd_T(bool isOn)
        {
            SetLedState(pbCameraCmd_T, isOn);
        }

        public void SetTX1(bool isOn)
        {
            SetLedState(pbTX1, isOn);
        }

        public void SetTX2(bool isOn)
        {
            SetLedState(pbTX2, isOn);
        }

        public void SetTX3(bool isOn)
        {
            SetLedState(pbTX3, isOn);
        }

        #endregion

        #region F-G Side LED 控制

        public void SetRX4(bool isOn)
        {
            SetLedState(pbRX4, isOn);
        }

        public void SetRX5(bool isOn)
        {
            SetLedState(pbRX5, isOn);
        }

        public void SetRX6(bool isOn)
        {
            SetLedState(pbRX6, isOn);
        }

        public void SetFGCmd_T(bool isOn)
        {
            SetLedState(pbFGCmd_T, isOn);
        }

        public void SetFGCmd_R(bool isOn)
        {
            SetLedState(pbFGCmd_R, isOn);
        }

        public void SetRX1(bool isOn)
        {
            SetLedState(pbRX1, isOn);
        }

        public void SetRX2(bool isOn)
        {
            SetLedState(pbRX2, isOn);
        }

        public void SetRX3(bool isOn)
        {
            SetLedState(pbRX3, isOn);
        }

        #endregion

        #region 批次控制方法

        public void SetAllCameraSideLeds(bool tx4, bool tx5, bool tx6, bool cmdR, bool cmdT, bool tx1, bool tx2, bool tx3)
        {
            SetTX4(tx4);
            SetTX5(tx5);
            SetTX6(tx6);
            SetCameraCmd_R(cmdR);
            SetCameraCmd_T(cmdT);
            SetTX1(tx1);
            SetTX2(tx2);
            SetTX3(tx3);
        }

        public void SetAllFGSideLeds(bool rx4, bool rx5, bool rx6, bool cmdT, bool cmdR, bool rx1, bool rx2, bool rx3)
        {
            SetRX4(rx4);
            SetRX5(rx5);
            SetRX6(rx6);
            SetFGCmd_T(cmdT);
            SetFGCmd_R(cmdR);
            SetRX1(rx1);
            SetRX2(rx2);
            SetRX3(rx3);
        }

        public void ClearAllLeds()
        {
            SetAllCameraSideLeds(false, false, false, false, false, false, false, false);
            SetAllFGSideLeds(false, false, false, false, false, false, false, false);
        }

        public void SetAllLedsOn()
        {
            SetAllCameraSideLeds(true, true, true, true, true, true, true, true);
            SetAllFGSideLeds(true, true, true, true, true, true, true, true);
        }

        #endregion

        #region 輔助方法

        private void SetLedState(PictureBox pictureBox, bool isOn)
        {
            if (pictureBox != null && greenLedImage != null && grayLedImage != null)
            {
                pictureBox.Image = isOn ? greenLedImage : grayLedImage;
            }
        }

        private bool GetLedState(PictureBox pictureBox)
        {
            if (pictureBox != null && greenLedImage != null)
            {
                return pictureBox.Image == greenLedImage;
            }
            return false;
        }

        #endregion

        #region 狀態讀取方法

        public bool GetTX4State()
        {
            return GetLedState(pbTX4);
        }

        public bool GetTX5State()
        {
            return GetLedState(pbTX5);
        }

        public bool GetTX6State()
        {
            return GetLedState(pbTX6);
        }

        public bool GetRX1State()
        {
            return GetLedState(pbRX1);
        }

        public bool GetRX2State()
        {
            return GetLedState(pbRX2);
        }

        public bool GetRX3State()
        {
            return GetLedState(pbRX3);
        }

        public bool GetCameraCmd_R_State()
        {
            return GetLedState(pbCameraCmd_R);
        }

        public bool GetCameraCmd_T_State()
        {
            return GetLedState(pbCameraCmd_T);
        }

        #endregion
    }
}
