namespace AVS.Trading.Tool.Forms.MarketTools
{
    partial class TradeHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeHistoryForm));
            this.marketTradeHistoryControl1 = new AVS.Trading.Tool.Controls.MarketTools.MarketTradeHistoryControl();
            this.SuspendLayout();
            // 
            // marketTradeHistoryControl1
            // 
            this.marketTradeHistoryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marketTradeHistoryControl1.Location = new System.Drawing.Point(0, 0);
            
            this.marketTradeHistoryControl1.Name = "marketTradeHistoryControl1";
            this.marketTradeHistoryControl1.Size = new System.Drawing.Size(1324, 819);
            this.marketTradeHistoryControl1.TabIndex = 0;
            // 
            // TradeHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 819);
            this.Controls.Add(this.marketTradeHistoryControl1);
            this.FormTitle = "TradeHistoryForm";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradeHistoryForm";
            this.Text = "TradeHistoryForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MarketTools.MarketTradeHistoryControl marketTradeHistoryControl1;
    }
}