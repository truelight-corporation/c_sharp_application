namespace FirmwareFlashLead
{
    partial class UcFirmwareFlashLead
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lLotNumber = new System.Windows.Forms.Label();
            this.tbLotNumber = new System.Windows.Forms.TextBox();
            this.lModuleNumber = new System.Windows.Forms.Label();
            this.tbModelNumber = new System.Windows.Forms.TextBox();
            this.tbFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lFirmwareVersion = new System.Windows.Forms.Label();
            this.bClear = new System.Windows.Forms.Button();
            this.bConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lLotNumber
            // 
            this.lLotNumber.AutoSize = true;
            this.lLotNumber.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lLotNumber.Location = new System.Drawing.Point(3, 6);
            this.lLotNumber.Name = "lLotNumber";
            this.lLotNumber.Size = new System.Drawing.Size(190, 64);
            this.lLotNumber.TabIndex = 0;
            this.lLotNumber.Text = "批號 :";
            // 
            // tbLotNumber
            // 
            this.tbLotNumber.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbLotNumber.Location = new System.Drawing.Point(199, 3);
            this.tbLotNumber.Name = "tbLotNumber";
            this.tbLotNumber.Size = new System.Drawing.Size(450, 84);
            this.tbLotNumber.TabIndex = 1;
            this.tbLotNumber.TextChanged += new System.EventHandler(this.tbLotNumber_TextChanged);
            // 
            // lModuleNumber
            // 
            this.lModuleNumber.AutoSize = true;
            this.lModuleNumber.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lModuleNumber.Location = new System.Drawing.Point(3, 96);
            this.lModuleNumber.Name = "lModuleNumber";
            this.lModuleNumber.Size = new System.Drawing.Size(190, 64);
            this.lModuleNumber.TabIndex = 2;
            this.lModuleNumber.Text = "型號 :";
            // 
            // tbModelNumber
            // 
            this.tbModelNumber.Enabled = false;
            this.tbModelNumber.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbModelNumber.Location = new System.Drawing.Point(199, 93);
            this.tbModelNumber.Name = "tbModelNumber";
            this.tbModelNumber.Size = new System.Drawing.Size(450, 84);
            this.tbModelNumber.TabIndex = 3;
            // 
            // tbFirmwareVersion
            // 
            this.tbFirmwareVersion.Enabled = false;
            this.tbFirmwareVersion.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbFirmwareVersion.Location = new System.Drawing.Point(199, 183);
            this.tbFirmwareVersion.Name = "tbFirmwareVersion";
            this.tbFirmwareVersion.Size = new System.Drawing.Size(450, 84);
            this.tbFirmwareVersion.TabIndex = 5;
            // 
            // lFirmwareVersion
            // 
            this.lFirmwareVersion.AutoSize = true;
            this.lFirmwareVersion.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lFirmwareVersion.Location = new System.Drawing.Point(3, 186);
            this.lFirmwareVersion.Name = "lFirmwareVersion";
            this.lFirmwareVersion.Size = new System.Drawing.Size(190, 64);
            this.lFirmwareVersion.TabIndex = 4;
            this.lFirmwareVersion.Text = "版本 :";
            // 
            // bClear
            // 
            this.bClear.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bClear.Location = new System.Drawing.Point(3, 273);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(300, 80);
            this.bClear.TabIndex = 6;
            this.bClear.Text = "清除";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bConfirm
            // 
            this.bConfirm.Enabled = false;
            this.bConfirm.Font = new System.Drawing.Font("PMingLiU", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bConfirm.Location = new System.Drawing.Point(349, 273);
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.Size = new System.Drawing.Size(300, 80);
            this.bConfirm.TabIndex = 7;
            this.bConfirm.Text = "確認";
            this.bConfirm.UseVisualStyleBackColor = true;
            this.bConfirm.Click += new System.EventHandler(this.bConfirm_Click);
            // 
            // UcFirmwareFlashLead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.tbFirmwareVersion);
            this.Controls.Add(this.lFirmwareVersion);
            this.Controls.Add(this.tbModelNumber);
            this.Controls.Add(this.lModuleNumber);
            this.Controls.Add(this.tbLotNumber);
            this.Controls.Add(this.lLotNumber);
            this.Name = "UcFirmwareFlashLead";
            this.Size = new System.Drawing.Size(653, 356);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lLotNumber;
        private System.Windows.Forms.TextBox tbLotNumber;
        private System.Windows.Forms.Label lModuleNumber;
        private System.Windows.Forms.TextBox tbModelNumber;
        private System.Windows.Forms.TextBox tbFirmwareVersion;
        private System.Windows.Forms.Label lFirmwareVersion;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bConfirm;
    }
}
