using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.TradingTools.ChildControls;

namespace AVS.Trading.Tool.Controls.TradingTools
{
    partial class MyTradeHistoryControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuTradeHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSummaryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl = new AVS.CoreLib.WinForms.Grid.GridControl();
            this.ColumnMarket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpenOrderBook = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tradeHistoryFiltersControl1 = new AVS.Trading.Tool.Controls.Common.TradeHistoryFiltersControl();
            this.myOpenOrdersControl1 = new AVS.Trading.Tool.Controls.TradingTools.MyOpenOrdersControl();
            this.tradeSummaryTabControl1 = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.TradeSummaryTabControl();
            this.marketPriceControl1 = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.MarketPriceControl();
            this.contextMenuTradeHistory.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuTradeHistory
            // 
            this.contextMenuTradeHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCalc,
            this.viewSummaryMenuItem});
            this.contextMenuTradeHistory.Name = "contextMenuStrip1";
            this.contextMenuTradeHistory.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuTradeHistory.Size = new System.Drawing.Size(173, 48);
            // 
            // menuItemCalc
            // 
            this.menuItemCalc.Name = "menuItemCalc";
            this.menuItemCalc.Size = new System.Drawing.Size(172, 22);
            this.menuItemCalc.Text = "Trading &calculator ";
            this.menuItemCalc.ToolTipText = "Displays calculations for the next orders you might open";
            this.menuItemCalc.Click += new System.EventHandler(this.menuItemCalc_Click);
            // 
            // viewSummaryMenuItem
            // 
            this.viewSummaryMenuItem.Name = "viewSummaryMenuItem";
            this.viewSummaryMenuItem.Size = new System.Drawing.Size(172, 22);
            this.viewSummaryMenuItem.Text = "View &Summary";
            this.viewSummaryMenuItem.Click += new System.EventHandler(this.viewSummaryMenuItem_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(7, 157);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 29);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.CausesValidation = false;
            this.groupBox2.Controls.Add(this.gridControl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 917);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Your trade history";
            // 
            // gridControl
            // 
            this.gridControl.CellFormatter = null;
            this.gridControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMarket,
            this.ColumnCategory,
            this.ColumnType,
            this.ColumnAmount,
            this.ColumnPrice,
            this.ColumnTotal,
            this.ColumnDate});
            this.gridControl.ContextMenuStrip = this.contextMenuTradeHistory;
            this.gridControl.Controller = null;
            this.gridControl.DataSource = null;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl.GridSummaryText = "";
            this.gridControl.Location = new System.Drawing.Point(3, 22);
            this.gridControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl.Name = "gridControl";
            this.gridControl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.gridControl.Size = new System.Drawing.Size(625, 892);
            this.gridControl.TabIndex = 3;
            this.gridControl.LoadDataCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.gridControl1_LoadDataCompleted);
            // 
            // ColumnMarket
            // 
            this.ColumnMarket.DataPropertyName = "Pair";
            this.ColumnMarket.HeaderText = "Market";
            this.ColumnMarket.Name = "ColumnMarket";
            this.ColumnMarket.ReadOnly = true;
            this.ColumnMarket.Width = 116;
            // 
            // ColumnCategory
            // 
            this.ColumnCategory.DataPropertyName = "CategoryDisplayText";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCategory.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnCategory.HeaderText = "Acc.";
            this.ColumnCategory.Name = "ColumnCategory";
            this.ColumnCategory.ReadOnly = true;
            this.ColumnCategory.ToolTipText = "Exchange or Margin";
            this.ColumnCategory.Width = 40;
            // 
            // ColumnType
            // 
            this.ColumnType.DataPropertyName = "Type";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnType.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnType.HeaderText = "Type";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            this.ColumnType.Width = 48;
            // 
            // ColumnAmount
            // 
            this.ColumnAmount.DataPropertyName = "AmountQuote";
            this.ColumnAmount.HeaderText = "Amount";
            this.ColumnAmount.Name = "ColumnAmount";
            this.ColumnAmount.ReadOnly = true;
            this.ColumnAmount.Width = 95;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.DataPropertyName = "Price";
            dataGridViewCellStyle3.Format = "N8";
            dataGridViewCellStyle3.NullValue = null;
            this.ColumnPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnPrice.HeaderText = "Price";
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.ReadOnly = true;
            this.ColumnPrice.Width = 95;
            // 
            // ColumnTotal
            // 
            this.ColumnTotal.DataPropertyName = "AmountBase";
            this.ColumnTotal.HeaderText = "Total";
            this.ColumnTotal.Name = "ColumnTotal";
            this.ColumnTotal.ReadOnly = true;
            this.ColumnTotal.Width = 95;
            // 
            // ColumnDate
            // 
            this.ColumnDate.DataPropertyName = "DateUtc";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle4.NullValue = null;
            this.ColumnDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnDate.HeaderText = "Date";
            this.ColumnDate.MinimumWidth = 120;
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            this.ColumnDate.Width = 130;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenOrderBook});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(165, 26);
            // 
            // menuItemOpenOrderBook
            // 
            this.menuItemOpenOrderBook.Name = "menuItemOpenOrderBook";
            this.menuItemOpenOrderBook.Size = new System.Drawing.Size(164, 22);
            this.menuItemOpenOrderBook.Text = "Open order &book";
            this.menuItemOpenOrderBook.Click += new System.EventHandler(this.menuItemOpenOrderBook_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._tradeHistoryFiltersControl1);
            this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
            this.splitContainer1.Panel2.Controls.Add(this.myOpenOrdersControl1);
            this.splitContainer1.Panel2.Controls.Add(this.tradeSummaryTabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.marketPriceControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1525, 917);
            this.splitContainer1.SplitterDistance = 631;
            this.splitContainer1.TabIndex = 12;
            // 
            // _tradeHistoryFiltersControl1
            // 
            this._tradeHistoryFiltersControl1.DisplayTradeCategoryBox = true;
            this._tradeHistoryFiltersControl1.Location = new System.Drawing.Point(7, 3);
            this._tradeHistoryFiltersControl1.Market = "";
            this._tradeHistoryFiltersControl1.Name = "_tradeHistoryFiltersControl1";
            this._tradeHistoryFiltersControl1.Size = new System.Drawing.Size(707, 148);
            this._tradeHistoryFiltersControl1.TabIndex = 0;
            // 
            // myOpenOrdersControl1
            // 
            this.myOpenOrdersControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myOpenOrdersControl1.Location = new System.Drawing.Point(7, 448);
            this.myOpenOrdersControl1.Name = "myOpenOrdersControl1";
            this.myOpenOrdersControl1.Size = new System.Drawing.Size(880, 466);
            this.myOpenOrdersControl1.TabIndex = 8;
            this.myOpenOrdersControl1.ViewMode = AVS.Trading.Tool.Controls.Common.ViewModeEnum.Normal;
            this.myOpenOrdersControl1.LoadDataCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.myOpenOrdersControl1_LoadDataCompleted);
            // 
            // tradeSummaryTabControl1
            // 
            this.tradeSummaryTabControl1.DataSource = null;
            this.tradeSummaryTabControl1.Location = new System.Drawing.Point(7, 210);
            this.tradeSummaryTabControl1.Name = "tradeSummaryTabControl1";
            this.tradeSummaryTabControl1.Size = new System.Drawing.Size(757, 232);
            this.tradeSummaryTabControl1.TabIndex = 11;
            // 
            // marketPriceControl1
            // 
            this.marketPriceControl1.Location = new System.Drawing.Point(7, 188);
            this.marketPriceControl1.Market = null;
            this.marketPriceControl1.Name = "marketPriceControl1";
            this.marketPriceControl1.Size = new System.Drawing.Size(513, 23);
            this.marketPriceControl1.TabIndex = 10;
            this.marketPriceControl1.Visible = false;
            // 
            // MyTradeHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MyTradeHistoryControl";
            this.Size = new System.Drawing.Size(1525, 917);
            this.Load += new System.EventHandler(this.MyTradeHistoryControl_Load);
            this.contextMenuTradeHistory.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TradeHistoryFiltersControl _tradeHistoryFiltersControl1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuTradeHistory;
        private System.Windows.Forms.ToolStripMenuItem menuItemCalc;
        private System.Windows.Forms.ToolStripMenuItem viewSummaryMenuItem;
        private MyOpenOrdersControl myOpenOrdersControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenOrderBook;
        private AVS.CoreLib.WinForms.Grid.GridControl gridControl;
        private MarketPriceControl marketPriceControl1;
        private TradeSummaryTabControl tradeSummaryTabControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMarket;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
