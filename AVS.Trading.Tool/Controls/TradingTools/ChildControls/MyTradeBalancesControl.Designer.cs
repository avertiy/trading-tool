namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    partial class MyTradeBalancesControl
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblQuoteValue = new System.Windows.Forms.Label();
            this.lblBaseValue = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblQuoteValueMargin = new System.Windows.Forms.Label();
            this.lblBaseValueMargin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have:";
            // 
            // lblQuoteValue
            // 
            this.lblQuoteValue.AutoSize = true;
            this.lblQuoteValue.ForeColor = System.Drawing.Color.Blue;
            this.lblQuoteValue.Location = new System.Drawing.Point(89, 0);
            this.lblQuoteValue.Name = "lblQuoteValue";
            this.lblQuoteValue.Size = new System.Drawing.Size(127, 20);
            this.lblQuoteValue.TabIndex = 1;
            this.lblQuoteValue.Text = "0.00000000 LTC";
            // 
            // lblBaseValue
            // 
            this.lblBaseValue.AutoSize = true;
            this.lblBaseValue.ForeColor = System.Drawing.Color.Blue;
            this.lblBaseValue.Location = new System.Drawing.Point(238, 0);
            this.lblBaseValue.Name = "lblBaseValue";
            this.lblBaseValue.Size = new System.Drawing.Size(129, 20);
            this.lblBaseValue.TabIndex = 2;
            this.lblBaseValue.Text = "0.00000000 BTC";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Amount on orders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tradeable balance:";
            // 
            // lblQuoteValueMargin
            // 
            this.lblQuoteValueMargin.AutoSize = true;
            this.lblQuoteValueMargin.ForeColor = System.Drawing.Color.DarkRed;
            this.lblQuoteValueMargin.Location = new System.Drawing.Point(525, 0);
            this.lblQuoteValueMargin.Name = "lblQuoteValueMargin";
            this.lblQuoteValueMargin.Size = new System.Drawing.Size(127, 20);
            this.lblQuoteValueMargin.TabIndex = 4;
            this.lblQuoteValueMargin.Text = "0.00000000 LTC";
            // 
            // lblBaseValueMargin
            // 
            this.lblBaseValueMargin.AutoSize = true;
            this.lblBaseValueMargin.ForeColor = System.Drawing.Color.DarkRed;
            this.lblBaseValueMargin.Location = new System.Drawing.Point(685, 0);
            this.lblBaseValueMargin.Name = "lblBaseValueMargin";
            this.lblBaseValueMargin.Size = new System.Drawing.Size(129, 20);
            this.lblBaseValueMargin.TabIndex = 5;
            this.lblBaseValueMargin.Text = "0.00000000 BTC";
            // 
            // MyTradeBalancesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBaseValueMargin);
            this.Controls.Add(this.lblQuoteValueMargin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBaseValue);
            this.Controls.Add(this.lblQuoteValue);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MyTradeBalancesControl";
            this.Size = new System.Drawing.Size(823, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQuoteValue;
        private System.Windows.Forms.Label lblBaseValue;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQuoteValueMargin;
        private System.Windows.Forms.Label lblBaseValueMargin;
    }
}
