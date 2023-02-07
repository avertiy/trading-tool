namespace AVS.Trading.Tool.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSwitchExchange = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemPoloniex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPoloniex2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExmo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBinance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKuna = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOrderBook = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMarketTradeHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCandles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.myOrdersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTradeHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.walletToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loanOffersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPing = new System.Windows.Forms.ToolStripMenuItem();
            this.apiKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExecuteCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.myTradeHistoryControl1 = new AVS.Trading.Tool.Controls.TradingTools.MyTradeHistoryControl();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripSwitchExchange,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 977);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1926, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripSwitchExchange
            // 
            this.toolStripSwitchExchange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSwitchExchange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPoloniex,
            this.toolStripMenuItemPoloniex2,
            this.toolStripMenuItemExmo,
            this.toolStripMenuItemBinance,
            this.toolStripMenuItemKuna});
            this.toolStripSwitchExchange.Image = global::AVS.Trading.Tool.Properties.Resources.poloniex_logo_32x32;
            this.toolStripSwitchExchange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSwitchExchange.Name = "toolStripSwitchExchange";
            this.toolStripSwitchExchange.Size = new System.Drawing.Size(32, 24);
            this.toolStripSwitchExchange.Text = "Current exchange is Poloniex";
            // 
            // toolStripMenuItemPoloniex
            // 
            this.toolStripMenuItemPoloniex.Image = global::AVS.Trading.Tool.Properties.Resources.poloniex_logo_32x32;
            this.toolStripMenuItemPoloniex.Name = "toolStripMenuItemPoloniex";
            this.toolStripMenuItemPoloniex.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemPoloniex.Text = "Poloniex (1)";
            this.toolStripMenuItemPoloniex.Click += new System.EventHandler(this.toolStripMenuItemPoloniex_Click);
            // 
            // toolStripMenuItemPoloniex2
            // 
            this.toolStripMenuItemPoloniex2.Image = global::AVS.Trading.Tool.Properties.Resources.Poloniex_icon_2;
            this.toolStripMenuItemPoloniex2.Name = "toolStripMenuItemPoloniex2";
            this.toolStripMenuItemPoloniex2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemPoloniex2.Text = "Poloniex (2)";
            this.toolStripMenuItemPoloniex2.Click += new System.EventHandler(this.ToolStripMenuItemPoloniex2_Click);
            // 
            // toolStripMenuItemExmo
            // 
            this.toolStripMenuItemExmo.Image = global::AVS.Trading.Tool.Properties.Resources.exmo_logo_32x32;
            this.toolStripMenuItemExmo.Name = "toolStripMenuItemExmo";
            this.toolStripMenuItemExmo.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemExmo.Text = "Exmo";
            this.toolStripMenuItemExmo.Click += new System.EventHandler(this.toolStripMenuItemExmo_Click);
            // 
            // toolStripMenuItemBinance
            // 
            this.toolStripMenuItemBinance.Image = global::AVS.Trading.Tool.Properties.Resources.binance;
            this.toolStripMenuItemBinance.Name = "toolStripMenuItemBinance";
            this.toolStripMenuItemBinance.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemBinance.Text = "Binance";
            this.toolStripMenuItemBinance.Click += new System.EventHandler(this.toolStripMenuItemBinance_Click);
            // 
            // toolStripMenuItemKuna
            // 
            this.toolStripMenuItemKuna.Image = global::AVS.Trading.Tool.Properties.Resources.Kuna;
            this.toolStripMenuItemKuna.Name = "toolStripMenuItemKuna";
            this.toolStripMenuItemKuna.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemKuna.Text = "Kuna";
            this.toolStripMenuItemKuna.Click += new System.EventHandler(this.ToolStripMenuItemKuna_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(88, 21);
            this.statusLabel.Text = "status label";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.walletToolsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.menuItemExecuteCommand});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1926, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOrderBook,
            this.menuItemMarketTradeHistory,
            this.menuItemCandles});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(86, 20);
            this.toolStripMenuItem1.Text = "&Market Tools";
            // 
            // menuItemOrderBook
            // 
            this.menuItemOrderBook.Name = "menuItemOrderBook";
            this.menuItemOrderBook.Size = new System.Drawing.Size(183, 22);
            this.menuItemOrderBook.Text = "Order &Book";
            this.menuItemOrderBook.Click += new System.EventHandler(this.menuItemOpenOrders_Click);
            // 
            // menuItemMarketTradeHistory
            // 
            this.menuItemMarketTradeHistory.Name = "menuItemMarketTradeHistory";
            this.menuItemMarketTradeHistory.Size = new System.Drawing.Size(183, 22);
            this.menuItemMarketTradeHistory.Text = "Market Trade &History";
            this.menuItemMarketTradeHistory.Click += new System.EventHandler(this.menuItemMarketTradeHistory_Click);
            // 
            // menuItemCandles
            // 
            this.menuItemCandles.Name = "menuItemCandles";
            this.menuItemCandles.Size = new System.Drawing.Size(183, 22);
            this.menuItemCandles.Text = "&Candles";
            this.menuItemCandles.Click += new System.EventHandler(this.menuItemCandles_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myOrdersMenuItem,
            this.menuItemTradeHistory});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(88, 20);
            this.toolStripMenuItem3.Text = "&Trading Tools";
            // 
            // myOrdersMenuItem
            // 
            this.myOrdersMenuItem.Name = "myOrdersMenuItem";
            this.myOrdersMenuItem.Size = new System.Drawing.Size(163, 22);
            this.myOrdersMenuItem.Text = "&My Orders";
            this.myOrdersMenuItem.Click += new System.EventHandler(this.myOrdersMenuItem_Click);
            // 
            // menuItemTradeHistory
            // 
            this.menuItemTradeHistory.Name = "menuItemTradeHistory";
            this.menuItemTradeHistory.Size = new System.Drawing.Size(163, 22);
            this.menuItemTradeHistory.Text = "My Trade &History";
            this.menuItemTradeHistory.Click += new System.EventHandler(this.menuItemTradeHistory_Click);
            // 
            // walletToolsToolStripMenuItem
            // 
            this.walletToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loanOffersToolStripMenuItem});
            this.walletToolsToolStripMenuItem.Name = "walletToolsToolStripMenuItem";
            this.walletToolsToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.walletToolsToolStripMenuItem.Text = "&Wallet Tools";
            // 
            // loanOffersToolStripMenuItem
            // 
            this.loanOffersToolStripMenuItem.Name = "loanOffersToolStripMenuItem";
            this.loanOffersToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.loanOffersToolStripMenuItem.Text = "My &Loan offers";
            this.loanOffersToolStripMenuItem.Click += new System.EventHandler(this.loanOffersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPing,
            this.apiKeysToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem4.Text = "&Settings";
            // 
            // menuItemPing
            // 
            this.menuItemPing.Name = "menuItemPing";
            this.menuItemPing.Size = new System.Drawing.Size(147, 22);
            this.menuItemPing.Text = "&Ping Poloniex";
            this.menuItemPing.Click += new System.EventHandler(this.menuItemPing_Click);
            // 
            // apiKeysToolStripMenuItem
            // 
            this.apiKeysToolStripMenuItem.Name = "apiKeysToolStripMenuItem";
            this.apiKeysToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.apiKeysToolStripMenuItem.Text = "&Api keys";
            this.apiKeysToolStripMenuItem.Click += new System.EventHandler(this.apiKeysToolStripMenuItem_Click);
            // 
            // menuItemExecuteCommand
            // 
            this.menuItemExecuteCommand.Name = "menuItemExecuteCommand";
            this.menuItemExecuteCommand.Size = new System.Drawing.Size(118, 20);
            this.menuItemExecuteCommand.Text = "Execute &command";
            this.menuItemExecuteCommand.Click += new System.EventHandler(this.menuItemExecuteCommand_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.myTradeHistoryControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1926, 953);
            this.panel1.TabIndex = 2;
            // 
            // myTradeHistoryControl1
            // 
            this.myTradeHistoryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTradeHistoryControl1.Location = new System.Drawing.Point(0, 0);
            this.myTradeHistoryControl1.Margin = new System.Windows.Forms.Padding(5);
            this.myTradeHistoryControl1.Name = "myTradeHistoryControl1";
            this.myTradeHistoryControl1.Size = new System.Drawing.Size(1926, 953);
            this.myTradeHistoryControl1.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "AVS Trading";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1926, 1003);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormTitle = "AVS Trading Tool";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AVS Trading Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemOrderBook;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItemPing;
        private System.Windows.Forms.Panel panel1;
        private Controls.TradingTools.MyTradeHistoryControl myTradeHistoryControl1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTradeHistory;
        private System.Windows.Forms.ToolStripMenuItem menuItemExecuteCommand;
        private System.Windows.Forms.ToolStripMenuItem menuItemMarketTradeHistory;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem myOrdersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem walletToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loanOffersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSwitchExchange;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPoloniex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExmo;
        private System.Windows.Forms.ToolStripMenuItem apiKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemCandles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPoloniex2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKuna;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBinance;
    }
}