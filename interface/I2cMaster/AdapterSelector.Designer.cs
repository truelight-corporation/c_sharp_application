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
            this.bAardvarkUpdate = new System.Windows.Forms.Button();
            this.lbAardvark = new System.Windows.Forms.ListBox();
            this.tpMcp2221 = new System.Windows.Forms.TabPage();
            this.bMcp2221Update = new System.Windows.Forms.Button();
            this.lbMcp2221 = new System.Windows.Forms.ListBox();
            this.tcAdapterType.SuspendLayout();
            this.tpAardvark.SuspendLayout();
            this.tpMcp2221.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcAdapterType
            // 
            this.tcAdapterType.Controls.Add(this.tpAardvark);
            this.tcAdapterType.Controls.Add(this.tpMcp2221);
            this.tcAdapterType.Location = new System.Drawing.Point(3, 3);
            this.tcAdapterType.Name = "tcAdapterType";
            this.tcAdapterType.SelectedIndex = 0;
            this.tcAdapterType.Size = new System.Drawing.Size(314, 234);
            this.tcAdapterType.TabIndex = 0;
            // 
            // tpAardvark
            // 
            this.tpAardvark.Controls.Add(this.bAardvarkUpdate);
            this.tpAardvark.Controls.Add(this.lbAardvark);
            this.tpAardvark.Location = new System.Drawing.Point(4, 22);
            this.tpAardvark.Name = "tpAardvark";
            this.tpAardvark.Padding = new System.Windows.Forms.Padding(3);
            this.tpAardvark.Size = new System.Drawing.Size(306, 208);
            this.tpAardvark.TabIndex = 0;
            this.tpAardvark.Text = "Aardvark";
            this.tpAardvark.UseVisualStyleBackColor = true;
            // 
            // bAardvarkUpdate
            // 
            this.bAardvarkUpdate.Location = new System.Drawing.Point(225, 6);
            this.bAardvarkUpdate.Name = "bAardvarkUpdate";
            this.bAardvarkUpdate.Size = new System.Drawing.Size(75, 23);
            this.bAardvarkUpdate.TabIndex = 1;
            this.bAardvarkUpdate.Text = "Update";
            this.bAardvarkUpdate.UseVisualStyleBackColor = true;
            this.bAardvarkUpdate.Click += new System.EventHandler(this.bAardvarkUpdate_Click);
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
            // tpMcp2221
            // 
            this.tpMcp2221.Controls.Add(this.bMcp2221Update);
            this.tpMcp2221.Controls.Add(this.lbMcp2221);
            this.tpMcp2221.Location = new System.Drawing.Point(4, 22);
            this.tpMcp2221.Name = "tpMcp2221";
            this.tpMcp2221.Padding = new System.Windows.Forms.Padding(3);
            this.tpMcp2221.Size = new System.Drawing.Size(306, 208);
            this.tpMcp2221.TabIndex = 1;
            this.tpMcp2221.Text = "MCP2221";
            this.tpMcp2221.UseVisualStyleBackColor = true;
            // 
            // bMcp2221Update
            // 
            this.bMcp2221Update.Location = new System.Drawing.Point(225, 6);
            this.bMcp2221Update.Name = "bMcp2221Update";
            this.bMcp2221Update.Size = new System.Drawing.Size(75, 23);
            this.bMcp2221Update.TabIndex = 3;
            this.bMcp2221Update.Text = "Update";
            this.bMcp2221Update.UseVisualStyleBackColor = true;
            this.bMcp2221Update.Click += new System.EventHandler(this.bMcp2221Update_Click);
            // 
            // lbMcp2221
            // 
            this.lbMcp2221.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbMcp2221.FormattingEnabled = true;
            this.lbMcp2221.ItemHeight = 16;
            this.lbMcp2221.Location = new System.Drawing.Point(6, 6);
            this.lbMcp2221.Name = "lbMcp2221";
            this.lbMcp2221.Size = new System.Drawing.Size(213, 196);
            this.lbMcp2221.TabIndex = 2;
            this.lbMcp2221.DoubleClick += new System.EventHandler(this.lbMcp2221_DoubleClick);
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
            this.tpMcp2221.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcAdapterType;
        private System.Windows.Forms.TabPage tpAardvark;
        private System.Windows.Forms.TabPage tpMcp2221;
        private System.Windows.Forms.Button bAardvarkUpdate;
        private System.Windows.Forms.ListBox lbAardvark;
        private System.Windows.Forms.Button bMcp2221Update;
        private System.Windows.Forms.ListBox lbMcp2221;
    }
}
