namespace AVS.Trading.Tool.Forms.TradingTools
{
    partial class MyOrdersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyOrdersForm));
            this.postOrdersControl1 = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.PostOrdersControl();
            this.marketTickerControl1 = new AVS.Trading.Tool.Controls.MarketTools.ChildControls.MarketTickerControl();
            this.SuspendLayout();
            // 
            // postOrdersControl1
            // 
            this.postOrdersControl1.BackColor = System.Drawing.SystemColors.Control;
            this.postOrdersControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.postOrdersControl1.Location = new System.Drawing.Point(258, 0);
            this.postOrdersControl1.Pair = null;
            this.postOrdersControl1.Name = "postOrdersControl1";
            this.postOrdersControl1.Size = new System.Drawing.Size(708, 336);
            this.postOrdersControl1.TabIndex = 1;
            // 
            // marketTickerControl1
            // 
            this.marketTickerControl1.Change = "+0.0%";
            this.marketTickerControl1.HighestBid = "0.0000000";
            this.marketTickerControl1.Location = new System.Drawing.Point(0, -1);
            this.marketTickerControl1.LowestAsk = "0.0000000";
            this.marketTickerControl1.Name = "marketTickerControl1";
            this.marketTickerControl1.Price = "0.0000000";
            this.marketTickerControl1.Size = new System.Drawing.Size(255, 229);
            this.marketTickerControl1.TabIndex = 3;
            this.marketTickerControl1.Volume = "0.00";
            this.marketTickerControl1.MarketChanged += new System.EventHandler<AVS.Trading.Data.Domain.MarketTools.MarketData>(this.marketTickerControl1_MarketChanged);
            // 
            // MyOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 336);
            this.Controls.Add(this.marketTickerControl1);
            this.Controls.Add(this.postOrdersControl1);
            this.FormTitle = "My orders";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(982, 375);
            this.Name = "MyOrdersForm";
            this.Text = "My orders";
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.TradingTools.ChildControls.PostOrdersControl postOrdersControl1;
        private Controls.MarketTools.ChildControls.MarketTickerControl marketTickerControl1;
    }
}