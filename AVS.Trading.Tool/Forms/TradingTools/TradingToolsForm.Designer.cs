namespace AVS.Poloniex
{
    partial class TradingToolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingToolsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTradeHistory = new System.Windows.Forms.TabPage();
            this.myTradeHistoryControl1 = new AVS.Trading.Tool.Controls.TradingTools.MyTradeHistoryControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSwitchExchangeBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemPoloniex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExmo = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabTradeHistory.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTradeHistory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1762, 859);
            this.tabControl1.TabIndex = 0;
            // 
            // tabTradeHistory
            // 
            this.tabTradeHistory.Controls.Add(this.myTradeHistoryControl1);
            this.tabTradeHistory.Controls.Add(this.statusStrip1);
            this.tabTradeHistory.Location = new System.Drawing.Point(4, 22);
            this.tabTradeHistory.Name = "tabTradeHistory";
            this.tabTradeHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabTradeHistory.Size = new System.Drawing.Size(1754, 833);
            this.tabTradeHistory.TabIndex = 0;
            this.tabTradeHistory.Text = "My trade history";
            this.tabTradeHistory.UseVisualStyleBackColor = true;
            // 
            // myTradeHistoryControl1
            // 
            this.myTradeHistoryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTradeHistoryControl1.Location = new System.Drawing.Point(3, 3);
            this.myTradeHistoryControl1.Name = "myTradeHistoryControl1";
            this.myTradeHistoryControl1.Size = new System.Drawing.Size(1748, 805);
            this.myTradeHistoryControl1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripSwitchExchangeBtn});
            this.statusStrip1.Location = new System.Drawing.Point(3, 808);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1748, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripSwitchExchangeBtn
            // 
            this.toolStripSwitchExchangeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSwitchExchangeBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPoloniex,
            this.toolStripMenuItemExmo});
            this.toolStripSwitchExchangeBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSwitchExchangeBtn.Image")));
            this.toolStripSwitchExchangeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSwitchExchangeBtn.Name = "toolStripSwitchExchangeBtn";
            this.toolStripSwitchExchangeBtn.Size = new System.Drawing.Size(32, 20);
            this.toolStripSwitchExchangeBtn.Text = "toolStripSplitButton1";
            this.toolStripSwitchExchangeBtn.ToolTipText = "Current exchange is Poloniex";
            // 
            // toolStripMenuItemPoloniex
            // 
            this.toolStripMenuItemPoloniex.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemPoloniex.Image")));
            this.toolStripMenuItemPoloniex.Name = "toolStripMenuItemPoloniex";
            this.toolStripMenuItemPoloniex.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItemPoloniex.Text = "Poloniex exchange";
            this.toolStripMenuItemPoloniex.ToolTipText = "Click to switch exchange";
            this.toolStripMenuItemPoloniex.Click += new System.EventHandler(this.toolStripMenuItemPoloniex_Click);
            // 
            // toolStripMenuItemExmo
            // 
            this.toolStripMenuItemExmo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemExmo.Image")));
            this.toolStripMenuItemExmo.Name = "toolStripMenuItemExmo";
            this.toolStripMenuItemExmo.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItemExmo.Text = "Exmo exchange";
            this.toolStripMenuItemExmo.ToolTipText = "Click to switch exchange";
            this.toolStripMenuItemExmo.Click += new System.EventHandler(this.toolStripMenuItemExmo_Click);
            // 
            // TradingToolsForm
            // 
            this.ClientSize = new System.Drawing.Size(1762, 859);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradingToolsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabControl1.ResumeLayout(false);
            this.tabTradeHistory.ResumeLayout(false);
            this.tabTradeHistory.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTradeHistory;
        private AVS.Trading.Tool.Controls.TradingTools.MyTradeHistoryControl myTradeHistoryControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSwitchExchangeBtn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPoloniex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExmo;
    }
}
