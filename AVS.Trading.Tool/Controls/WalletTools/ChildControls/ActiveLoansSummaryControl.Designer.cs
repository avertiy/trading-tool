namespace AVS.Trading.Tool.Controls.WalletTools.ChildControls
{
    partial class ActiveLoansSummaryControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAvgRateLabel = new System.Windows.Forms.Label();
            this.lblAvgRate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total amount";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(134, 22);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(18, 20);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "0";
            // 
            // lblAvgRateLabel
            // 
            this.lblAvgRateLabel.AutoSize = true;
            this.lblAvgRateLabel.Location = new System.Drawing.Point(39, 42);
            this.lblAvgRateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgRateLabel.Name = "lblAvgRateLabel";
            this.lblAvgRateLabel.Size = new System.Drawing.Size(68, 20);
            this.lblAvgRateLabel.TabIndex = 2;
            this.lblAvgRateLabel.Text = "Avg rate";
            // 
            // lblAvgRate
            // 
            this.lblAvgRate.AutoSize = true;
            this.lblAvgRate.Location = new System.Drawing.Point(134, 42);
            this.lblAvgRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgRate.Name = "lblAvgRate";
            this.lblAvgRate.Size = new System.Drawing.Size(32, 20);
            this.lblAvgRate.TabIndex = 3;
            this.lblAvgRate.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total fees";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Location = new System.Drawing.Point(134, 62);
            this.lblTotalFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(18, 20);
            this.lblTotalFees.TabIndex = 5;
            this.lblTotalFees.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.lblTotalFees);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblAvgRateLabel);
            this.groupBox1.Controls.Add(this.lblAvgRate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 98);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            // 
            // ActiveLoansSummaryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ActiveLoansSummaryControl";
            this.Size = new System.Drawing.Size(262, 98);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblAvgRateLabel;
        private System.Windows.Forms.Label lblAvgRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
