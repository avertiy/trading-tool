namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    partial class PostOrdersControl
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
            this.sellControl = new AVS.Trading.Tool.Controls.Common.LimitOrderControl();
            this.buyControl = new AVS.Trading.Tool.Controls.Common.LimitOrderControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlBalances = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBalanceQuote = new System.Windows.Forms.Label();
            this.lblBalanceBase = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTradableBalanceQuote = new System.Windows.Forms.Label();
            this.lblTradableBalanceBase = new System.Windows.Forms.Label();
            this.marginBuyControl = new AVS.Trading.Tool.Controls.Common.LimitOrderControl();
            this.marginSellControl = new AVS.Trading.Tool.Controls.Common.LimitOrderControl();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMarginStatus = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlBalances.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sellControl
            // 
            this.sellControl.Amount = 0D;
            this.sellControl.ButtonText = "Sell";
            this.sellControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellControl.Header = "SELL ORDER";
            this.sellControl.LoanRate = 0D;
            this.sellControl.Location = new System.Drawing.Point(377, 53);
            this.sellControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellControl.Name = "sellControl";
            this.sellControl.OrderType = AVS.Trading.Core.Enums.OrderType.Sell;
            this.sellControl.Price = 0D;
            this.sellControl.Size = new System.Drawing.Size(310, 221);
            this.sellControl.TabIndex = 1;
            this.sellControl.Total = 0D;
            this.sellControl.Type = AVS.Trading.Core.Enums.TradingAccount.Exchange;
            this.sellControl.ButtonClick += new System.EventHandler(this.sellControl_ButtonClick);
            // 
            // buyControl
            // 
            this.buyControl.Amount = 0D;
            this.buyControl.ButtonText = "Buy";
            this.buyControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buyControl.Header = "BUY ORDER";
            this.buyControl.LoanRate = 0D;
            this.buyControl.Location = new System.Drawing.Point(17, 53);
            this.buyControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buyControl.Name = "buyControl";
            this.buyControl.OrderType = AVS.Trading.Core.Enums.OrderType.Buy;
            this.buyControl.Price = 0D;
            this.buyControl.Size = new System.Drawing.Size(335, 221);
            this.buyControl.TabIndex = 0;
            this.buyControl.Total = 0D;
            this.buyControl.Type = AVS.Trading.Core.Enums.TradingAccount.Exchange;
            this.buyControl.ButtonClick += new System.EventHandler(this.buyControl_ButtonClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 335);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.pnlBalances);
            this.tabPage1.Controls.Add(this.buyControl);
            this.tabPage1.Controls.Add(this.sellControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "EXCHANGE";
            // 
            // pnlBalances
            // 
            this.pnlBalances.Controls.Add(this.label2);
            this.pnlBalances.Controls.Add(this.label1);
            this.pnlBalances.Controls.Add(this.lblBalanceQuote);
            this.pnlBalances.Controls.Add(this.lblBalanceBase);
            this.pnlBalances.Location = new System.Drawing.Point(17, 9);
            this.pnlBalances.Name = "pnlBalances";
            this.pnlBalances.Size = new System.Drawing.Size(670, 30);
            this.pnlBalances.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "You have:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "You have:";
            // 
            // lblBalanceQuote
            // 
            this.lblBalanceQuote.AutoSize = true;
            this.lblBalanceQuote.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBalanceQuote.Location = new System.Drawing.Point(509, 4);
            this.lblBalanceQuote.Name = "lblBalanceQuote";
            this.lblBalanceQuote.Size = new System.Drawing.Size(127, 20);
            this.lblBalanceQuote.TabIndex = 4;
            this.lblBalanceQuote.Text = "0.00000001 LTC";
            // 
            // lblBalanceBase
            // 
            this.lblBalanceBase.AutoSize = true;
            this.lblBalanceBase.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBalanceBase.Location = new System.Drawing.Point(164, 4);
            this.lblBalanceBase.Name = "lblBalanceBase";
            this.lblBalanceBase.Size = new System.Drawing.Size(129, 20);
            this.lblBalanceBase.TabIndex = 3;
            this.lblBalanceBase.Text = "0.00000001 BTC";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Ivory;
            this.tabPage2.Controls.Add(this.lblMarginStatus);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.marginBuyControl);
            this.tabPage2.Controls.Add(this.marginSellControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(703, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MARGIN";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblTradableBalanceQuote);
            this.panel1.Controls.Add(this.lblTradableBalanceBase);
            this.panel1.Location = new System.Drawing.Point(17, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 30);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tradable balance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tradeable balance:";
            // 
            // lblTradableBalanceQuote
            // 
            this.lblTradableBalanceQuote.AutoSize = true;
            this.lblTradableBalanceQuote.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTradableBalanceQuote.Location = new System.Drawing.Point(509, 4);
            this.lblTradableBalanceQuote.Name = "lblTradableBalanceQuote";
            this.lblTradableBalanceQuote.Size = new System.Drawing.Size(127, 20);
            this.lblTradableBalanceQuote.TabIndex = 4;
            this.lblTradableBalanceQuote.Text = "0.00000001 LTC";
            // 
            // lblTradableBalanceBase
            // 
            this.lblTradableBalanceBase.AutoSize = true;
            this.lblTradableBalanceBase.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTradableBalanceBase.Location = new System.Drawing.Point(164, 4);
            this.lblTradableBalanceBase.Name = "lblTradableBalanceBase";
            this.lblTradableBalanceBase.Size = new System.Drawing.Size(129, 20);
            this.lblTradableBalanceBase.TabIndex = 3;
            this.lblTradableBalanceBase.Text = "0.00000001 BTC";
            // 
            // marginBuyControl
            // 
            this.marginBuyControl.Amount = 0D;
            this.marginBuyControl.ButtonText = "Buy";
            this.marginBuyControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marginBuyControl.Header = "BUY ORDER";
            this.marginBuyControl.LoanRate = 0.004D;
            this.marginBuyControl.Location = new System.Drawing.Point(17, 53);
            this.marginBuyControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.marginBuyControl.Name = "marginBuyControl";
            this.marginBuyControl.OrderType = AVS.Trading.Core.Enums.OrderType.Buy;
            this.marginBuyControl.Price = 0D;
            this.marginBuyControl.Size = new System.Drawing.Size(335, 221);
            this.marginBuyControl.TabIndex = 2;
            this.marginBuyControl.Total = 0D;
            this.marginBuyControl.Type = AVS.Trading.Core.Enums.TradingAccount.Margin;
            this.marginBuyControl.ButtonClick += new System.EventHandler(this.maginBuyControl_ButtonClick);
            // 
            // marginSellControl
            // 
            this.marginSellControl.Amount = 0D;
            this.marginSellControl.ButtonText = "Sell";
            this.marginSellControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marginSellControl.Header = "SELL ORDER";
            this.marginSellControl.LoanRate = 0.004D;
            this.marginSellControl.Location = new System.Drawing.Point(377, 53);
            this.marginSellControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.marginSellControl.Name = "marginSellControl";
            this.marginSellControl.OrderType = AVS.Trading.Core.Enums.OrderType.Sell;
            this.marginSellControl.Price = 0D;
            this.marginSellControl.Size = new System.Drawing.Size(310, 221);
            this.marginSellControl.TabIndex = 3;
            this.marginSellControl.Total = 0D;
            this.marginSellControl.Type = AVS.Trading.Core.Enums.TradingAccount.Margin;
            this.marginSellControl.ButtonClick += new System.EventHandler(this.marginSellControl_ButtonClick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lblStatus.Location = new System.Drawing.Point(19, 275);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 6;
            // 
            // lblMarginStatus
            // 
            this.lblMarginStatus.AutoSize = true;
            this.lblMarginStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMarginStatus.Location = new System.Drawing.Point(20, 275);
            this.lblMarginStatus.Name = "lblMarginStatus";
            this.lblMarginStatus.Size = new System.Drawing.Size(0, 20);
            this.lblMarginStatus.TabIndex = 7;
            // 
            // PostOrdersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "PostOrdersControl";
            this.Size = new System.Drawing.Size(711, 335);
            this.Load += new System.EventHandler(this.PostOrdersControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlBalances.ResumeLayout(false);
            this.pnlBalances.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.LimitOrderControl buyControl;
        private Common.LimitOrderControl sellControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Common.LimitOrderControl marginBuyControl;
        private Common.LimitOrderControl marginSellControl;
        private System.Windows.Forms.Panel pnlBalances;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBalanceQuote;
        private System.Windows.Forms.Label lblBalanceBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTradableBalanceQuote;
        private System.Windows.Forms.Label lblTradableBalanceBase;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMarginStatus;
    }
}
