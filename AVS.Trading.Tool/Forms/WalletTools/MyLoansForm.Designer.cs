namespace AVS.Trading.Tool.Forms.WalletTools
{
    partial class MyLoansForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyLoansForm));
            this.myLoansControl1 = new AVS.Trading.Tool.Controls.WalletTools.MyActiveLoansControl();
            this.SuspendLayout();
            // 
            // myLoansControl1
            // 
            this.myLoansControl1.Currency = "DOGE";
            this.myLoansControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myLoansControl1.Location = new System.Drawing.Point(0, 0);
            this.myLoansControl1.MinimumSize = new System.Drawing.Size(964, 440);
            this.myLoansControl1.Name = "myLoansControl1";
            this.myLoansControl1.Size = new System.Drawing.Size(1076, 708);
            this.myLoansControl1.TabIndex = 0;
            // 
            // MyLoansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 708);
            this.Controls.Add(this.myLoansControl1);
            this.FormTitle = "My Loans";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(985, 460);
            this.Name = "MyLoansForm";
            this.Text = "My Loans";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WalletTools.MyActiveLoansControl myLoansControl1;
    }
}