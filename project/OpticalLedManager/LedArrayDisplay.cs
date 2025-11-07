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
    public partial class LedArrayDisplay : UserControl
    {
        // --- 從 LedArrayController 貼過來的程式碼 ---
        private Image greenLedImage;
        private Image grayLedImage;

        public LedArrayDisplay()
        {
            InitializeComponent();

            // 呼叫載入圖片
            LoadLedImages();

            // 清除所有 LED 作為預設狀態
            ClearAllLeds();
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
            if (pictureBox !=null && greenLedImage !=null)
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
