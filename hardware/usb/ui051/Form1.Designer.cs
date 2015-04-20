namespace ui051
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.functionTestPanel1 = new ui051.FunctionTestPanel();
            this.SuspendLayout();
            // 
            // functionTestPanel1
            // 
            this.functionTestPanel1.Location = new System.Drawing.Point(-2, 0);
            this.functionTestPanel1.MaximumSize = new System.Drawing.Size(294, 455);
            this.functionTestPanel1.MinimumSize = new System.Drawing.Size(294, 455);
            this.functionTestPanel1.Name = "functionTestPanel1";
            this.functionTestPanel1.Size = new System.Drawing.Size(294, 455);
            this.functionTestPanel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.functionTestPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private FunctionTestPanel functionTestPanel1;
    }
}

