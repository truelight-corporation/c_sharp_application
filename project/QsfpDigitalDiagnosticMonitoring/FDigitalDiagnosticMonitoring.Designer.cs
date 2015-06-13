namespace QsfpDigitalDiagnosticMonitoring
{
    partial class FDigitalDiagnosticMonitoring
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.ucDigitalDiagnosticsMonitoring = new QsfpDigitalDiagnosticMonitoring.UCDigitalDiagnosticsMonitoring();
            this.tcDdmAndInformation = new System.Windows.Forms.TabControl();
            this.tpDigitalDiagnosticMonitoring = new System.Windows.Forms.TabPage();
            this.tpInformation = new System.Windows.Forms.TabPage();
            this.tcDdmAndInformation.SuspendLayout();
            this.tpDigitalDiagnosticMonitoring.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(738, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 1;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this._cbConnected_CheckedChanged);
            // 
            // ucDigitalDiagnosticsMonitoring
            // 
            this.ucDigitalDiagnosticsMonitoring.Location = new System.Drawing.Point(6, 4);
            this.ucDigitalDiagnosticsMonitoring.Name = "ucDigitalDiagnosticsMonitoring";
            this.ucDigitalDiagnosticsMonitoring.Size = new System.Drawing.Size(782, 482);
            this.ucDigitalDiagnosticsMonitoring.TabIndex = 0;
            // 
            // tcDdmAndInformation
            // 
            this.tcDdmAndInformation.Controls.Add(this.tpDigitalDiagnosticMonitoring);
            this.tcDdmAndInformation.Controls.Add(this.tpInformation);
            this.tcDdmAndInformation.Location = new System.Drawing.Point(12, 34);
            this.tcDdmAndInformation.Name = "tcDdmAndInformation";
            this.tcDdmAndInformation.SelectedIndex = 0;
            this.tcDdmAndInformation.Size = new System.Drawing.Size(800, 516);
            this.tcDdmAndInformation.TabIndex = 2;
            // 
            // tpDigitalDiagnosticMonitoring
            // 
            this.tpDigitalDiagnosticMonitoring.Controls.Add(this.ucDigitalDiagnosticsMonitoring);
            this.tpDigitalDiagnosticMonitoring.Location = new System.Drawing.Point(4, 22);
            this.tpDigitalDiagnosticMonitoring.Name = "tpDigitalDiagnosticMonitoring";
            this.tpDigitalDiagnosticMonitoring.Padding = new System.Windows.Forms.Padding(3);
            this.tpDigitalDiagnosticMonitoring.Size = new System.Drawing.Size(792, 490);
            this.tpDigitalDiagnosticMonitoring.TabIndex = 0;
            this.tpDigitalDiagnosticMonitoring.Text = "DDM";
            this.tpDigitalDiagnosticMonitoring.UseVisualStyleBackColor = true;
            // 
            // tpInformation
            // 
            this.tpInformation.Location = new System.Drawing.Point(4, 22);
            this.tpInformation.Name = "tpInformation";
            this.tpInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tpInformation.Size = new System.Drawing.Size(792, 490);
            this.tpInformation.TabIndex = 1;
            this.tpInformation.Text = "Information";
            this.tpInformation.UseVisualStyleBackColor = true;
            // 
            // FDigitalDiagnosticMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(823, 554);
            this.Controls.Add(this.tcDdmAndInformation);
            this.Controls.Add(this.cbConnected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FDigitalDiagnosticMonitoring";
            this.Text = "Digital Diagnostic Monitoring";
            this.tcDdmAndInformation.ResumeLayout(false);
            this.tpDigitalDiagnosticMonitoring.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCDigitalDiagnosticsMonitoring ucDigitalDiagnosticsMonitoring;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcDdmAndInformation;
        private System.Windows.Forms.TabPage tpDigitalDiagnosticMonitoring;
        private System.Windows.Forms.TabPage tpInformation;
    }
}

