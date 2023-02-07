namespace AVS.Trading.Tool.Controls.Common
{
    partial class LimitOrderControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numLoanRate = new AVS.Trading.Tool.Controls.Common.NumControl();
            this.lblLoanRate = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.numTotal = new AVS.Trading.Tool.Controls.Common.NumControl();
            this.numAmount = new AVS.Trading.Tool.Controls.Common.NumControl();
            this.numPrice = new AVS.Trading.Tool.Controls.Common.NumControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numLoanRate);
            this.groupBox1.Controls.Add(this.lblLoanRate);
            this.groupBox1.Controls.Add(this.button);
            this.groupBox1.Controls.Add(this.numTotal);
            this.groupBox1.Controls.Add(this.numAmount);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(328, 218);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BUY ORDER";
            // 
            // numLoanRate
            // 
            this.numLoanRate.Currency = "%";
            this.numLoanRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLoanRate.ForeColor = System.Drawing.Color.DarkRed;
            this.numLoanRate.Format = "0.00000000";
            this.numLoanRate.Location = new System.Drawing.Point(89, 93);
            this.numLoanRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numLoanRate.Name = "numLoanRate";
            this.numLoanRate.Size = new System.Drawing.Size(190, 26);
            this.numLoanRate.TabIndex = 15;
            this.numLoanRate.Visible = false;
            // 
            // lblLoanRate
            // 
            this.lblLoanRate.AutoSize = true;
            this.lblLoanRate.ForeColor = System.Drawing.Color.DarkRed;
            this.lblLoanRate.Location = new System.Drawing.Point(4, 99);
            this.lblLoanRate.Name = "lblLoanRate";
            this.lblLoanRate.Size = new System.Drawing.Size(81, 20);
            this.lblLoanRate.TabIndex = 14;
            this.lblLoanRate.Text = "Loan rate:";
            this.lblLoanRate.Visible = false;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(220, 181);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 29);
            this.button.TabIndex = 12;
            this.button.Text = "Buy";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // numTotal
            // 
            this.numTotal.Currency = "BTC";
            this.numTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.ForeColor = System.Drawing.SystemColors.Highlight;
            this.numTotal.Format = "0.00000000";
            this.numTotal.Location = new System.Drawing.Point(89, 138);
            this.numTotal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numTotal.Name = "numTotal";
            this.numTotal.Size = new System.Drawing.Size(219, 26);
            this.numTotal.TabIndex = 11;
            // 
            // numAmount
            // 
            this.numAmount.Currency = "DASH";
            this.numAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAmount.Format = "0.00000000";
            this.numAmount.Location = new System.Drawing.Point(89, 61);
            this.numAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(219, 26);
            this.numAmount.TabIndex = 10;
            this.numAmount.ValueChanged += new System.EventHandler<double>(this.numAmount_ValueChanged);
            // 
            // numPrice
            // 
            this.numPrice.Currency = "BTC";
            this.numPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrice.Format = "0.00000000";
            this.numPrice.Location = new System.Drawing.Point(89, 31);
            this.numPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(206, 26);
            this.numPrice.TabIndex = 9;
            this.numPrice.ValueChanged += new System.EventHandler<double>(this.numPrice_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(28, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Price:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = ".......................................................................";
            // 
            // LimitOrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LimitOrderControl";
            this.Size = new System.Drawing.Size(328, 218);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button;
        private NumControl numTotal;
        private NumControl numAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private NumControl numPrice;
        private NumControl numLoanRate;
        private System.Windows.Forms.Label lblLoanRate;
    }
}
