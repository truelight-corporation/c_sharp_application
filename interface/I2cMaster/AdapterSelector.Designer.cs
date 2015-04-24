namespace I2cMasterInterface
{
    partial class AdapterSelector
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
            if (disposing && (components != null)) {
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
            this.tcAdapterType = new System.Windows.Forms.TabControl();
            this.tpAardvark = new System.Windows.Forms.TabPage();
            this.bLBAUpdate = new System.Windows.Forms.Button();
            this.lbAardvark = new System.Windows.Forms.ListBox();
            this.tpUi051 = new System.Windows.Forms.TabPage();
            this.tcAdapterType.SuspendLayout();
            this.tpAardvark.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcAdapterType
            // 
            this.tcAdapterType.Controls.Add(this.tpAardvark);
            this.tcAdapterType.Controls.Add(this.tpUi051);
            this.tcAdapterType.Location = new System.Drawing.Point(3, 3);
            this.tcAdapterType.Name = "tcAdapterType";
            this.tcAdapterType.SelectedIndex = 0;
            this.tcAdapterType.Size = new System.Drawing.Size(314, 234);
            this.tcAdapterType.TabIndex = 0;
            // 
            // tpAardvark
            // 
            this.tpAardvark.Controls.Add(this.bLBAUpdate);
            this.tpAardvark.Controls.Add(this.lbAardvark);
            this.tpAardvark.Location = new System.Drawing.Point(4, 22);
            this.tpAardvark.Name = "tpAardvark";
            this.tpAardvark.Padding = new System.Windows.Forms.Padding(3);
            this.tpAardvark.Size = new System.Drawing.Size(306, 208);
            this.tpAardvark.TabIndex = 0;
            this.tpAardvark.Text = "Aardvark";
            this.tpAardvark.UseVisualStyleBackColor = true;
            // 
            // bLBAUpdate
            // 
            this.bLBAUpdate.Location = new System.Drawing.Point(225, 6);
            this.bLBAUpdate.Name = "bLBAUpdate";
            this.bLBAUpdate.Size = new System.Drawing.Size(75, 23);
            this.bLBAUpdate.TabIndex = 1;
            this.bLBAUpdate.Text = "Update";
            this.bLBAUpdate.UseVisualStyleBackColor = true;
            this.bLBAUpdate.Click += new System.EventHandler(this.bLBAUpdate_Click);
            // 
            // lbAardvark
            // 
            this.lbAardvark.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbAardvark.FormattingEnabled = true;
            this.lbAardvark.ItemHeight = 16;
            this.lbAardvark.Location = new System.Drawing.Point(6, 6);
            this.lbAardvark.Name = "lbAardvark";
            this.lbAardvark.Size = new System.Drawing.Size(213, 196);
            this.lbAardvark.TabIndex = 0;
            this.lbAardvark.DoubleClick += new System.EventHandler(this._lbAardvarkDoubleClick);
            // 
            // tpUi051
            // 
            this.tpUi051.Location = new System.Drawing.Point(4, 22);
            this.tpUi051.Name = "tpUi051";
            this.tpUi051.Padding = new System.Windows.Forms.Padding(3);
            this.tpUi051.Size = new System.Drawing.Size(306, 208);
            this.tpUi051.TabIndex = 1;
            this.tpUi051.Text = "UI051";
            this.tpUi051.UseVisualStyleBackColor = true;
            // 
            // AdapterSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcAdapterType);
            this.Name = "AdapterSelector";
            this.Size = new System.Drawing.Size(320, 240);
            this.tcAdapterType.ResumeLayout(false);
            this.tpAardvark.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcAdapterType;
        private System.Windows.Forms.TabPage tpAardvark;
        private System.Windows.Forms.TabPage tpUi051;
        private System.Windows.Forms.Button bLBAUpdate;
        private System.Windows.Forms.ListBox lbAardvark;

    }
}
