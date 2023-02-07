using AVS.Trading.Tool.Controls.Common;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    partial class MarketTradeHistoryControl
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
            this.btnLoadTradeHistory = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new AVS.CoreLib.WinForms.Grid.GridControl();
            this.gridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewSummaryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeHistoryFiltersControl1 = new AVS.Trading.Tool.Controls.Common.TradeHistoryFiltersControl();
            this.tradeTotals = new AVS.Trading.Tool.Controls.Common.TradeTotalsControl();
            this.selectedCellsTradeTotals = new AVS.Trading.Tool.Controls.Common.TradeTotalsControl();
            this.MarketColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.gridContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadTradeHistory
            // 
            this.btnLoadTradeHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadTradeHistory.Location = new System.Drawing.Point(797, 160);
            this.btnLoadTradeHistory.Name = "btnLoadTradeHistory";
            this.btnLoadTradeHistory.Size = new System.Drawing.Size(197, 29);
            this.btnLoadTradeHistory.TabIndex = 5;
            this.btnLoadTradeHistory.Text = "Load Trade History";
            this.btnLoadTradeHistory.UseVisualStyleBackColor = true;
            this.btnLoadTradeHistory.Click += new System.EventHandler(this.btnLoadTradeHistory_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 847);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trade history";
            // 
            // gridControl1
            // 
            this.gridControl1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MarketColumn,
            this.TypeColumn,
            this.AmountColumn,
            this.PriceColumn,
            this.TotalColumn,
            this.DateColumn});
            this.gridControl1.ContextMenuStrip = this.gridContextMenu;
            this.gridControl1.Controller = null;
            this.gridControl1.DataSource = null;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.GridSummaryText = "";
            this.gridControl1.Location = new System.Drawing.Point(3, 22);
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.gridControl1.Size = new System.Drawing.Size(782, 822);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.LoadDataCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.gridControl1_LoadDataCompleted);
            // 
            // gridContextMenu
            // 
            this.gridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSummaryMenuItem});
            this.gridContextMenu.Name = "gridContextMenu";
            this.gridContextMenu.Size = new System.Drawing.Size(154, 26);
            // 
            // viewSummaryMenuItem
            // 
            this.viewSummaryMenuItem.Name = "viewSummaryMenuItem";
            this.viewSummaryMenuItem.Size = new System.Drawing.Size(153, 22);
            this.viewSummaryMenuItem.Text = "View &Summary";
            this.viewSummaryMenuItem.Click += new System.EventHandler(this.viewSummaryMenuItem_Click);
            // 
            // tradeHistoryFiltersControl1
            // 
            this.tradeHistoryFiltersControl1.DisplayTradeCategoryBox = false;
            this.tradeHistoryFiltersControl1.Location = new System.Drawing.Point(797, 3);
            this.tradeHistoryFiltersControl1.Name = "tradeHistoryFiltersControl1";
            this.tradeHistoryFiltersControl1.Size = new System.Drawing.Size(532, 151);
            this.tradeHistoryFiltersControl1.TabIndex = 13;
            // 
            // tradeTotals
            // 
            this.tradeTotals.AvgBuyPrice = "0 *";
            this.tradeTotals.AvgSellPrice = "0 *";
            this.tradeTotals.BuyTotal = "0 *";
            this.tradeTotals.BuyVolume = "0 *";
            this.tradeTotals.DataSource = null;
            this.tradeTotals.Location = new System.Drawing.Point(795, 244);
            this.tradeTotals.Name = "tradeTotals";
            this.tradeTotals.SellTotal = "0.00000000 (BTC)";
            this.tradeTotals.SellVolume = "0.00000000 (BTC)";
            this.tradeTotals.Size = new System.Drawing.Size(482, 124);
            this.tradeTotals.TabIndex = 14;
            this.tradeTotals.Total = "0.00000000 (BTC)";
            // 
            // selectedCellsTradeTotals
            // 
            this.selectedCellsTradeTotals.AvgBuyPrice = "0 *";
            this.selectedCellsTradeTotals.AvgSellPrice = "0 *";
            this.selectedCellsTradeTotals.BuyTotal = "0 *";
            this.selectedCellsTradeTotals.BuyVolume = "0 *";
            this.selectedCellsTradeTotals.DataSource = null;
            this.selectedCellsTradeTotals.Location = new System.Drawing.Point(795, 372);
            this.selectedCellsTradeTotals.Name = "selectedCellsTradeTotals";
            this.selectedCellsTradeTotals.SellTotal = "0.00000000 (BTC)";
            this.selectedCellsTradeTotals.SellVolume = "0.00000000 (BTC)";
            this.selectedCellsTradeTotals.Size = new System.Drawing.Size(482, 124);
            this.selectedCellsTradeTotals.TabIndex = 15;
            this.selectedCellsTradeTotals.Total = "0.00000000 (BTC)";
            this.selectedCellsTradeTotals.Visible = false;
            // 
            // MarketColumn
            // 
            this.MarketColumn.DataPropertyName = "Market";
            this.MarketColumn.HeaderText = "Market";
            this.MarketColumn.Name = "MarketColumn";
            this.MarketColumn.ReadOnly = true;
            // 
            // TypeColumn
            // 
            this.TypeColumn.DataPropertyName = "Type";
            this.TypeColumn.HeaderText = "Type";
            this.TypeColumn.Name = "TypeColumn";
            this.TypeColumn.ReadOnly = true;
            // 
            // AmountColumn
            // 
            this.AmountColumn.DataPropertyName = "AmountQuote";
            dataGridViewCellStyle1.Format = "N3";
            dataGridViewCellStyle1.NullValue = null;
            this.AmountColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.AmountColumn.HeaderText = "Amount";
            this.AmountColumn.MinimumWidth = 120;
            this.AmountColumn.Name = "AmountColumn";
            this.AmountColumn.ReadOnly = true;
            this.AmountColumn.Width = 120;
            // 
            // PriceColumn
            // 
            this.PriceColumn.DataPropertyName = "Price";
            dataGridViewCellStyle2.Format = "N8";
            dataGridViewCellStyle2.NullValue = null;
            this.PriceColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.PriceColumn.HeaderText = "Price";
            this.PriceColumn.MinimumWidth = 120;
            this.PriceColumn.Name = "PriceColumn";
            this.PriceColumn.ReadOnly = true;
            this.PriceColumn.Width = 120;
            // 
            // TotalColumn
            // 
            this.TotalColumn.DataPropertyName = "AmountBase";
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.TotalColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalColumn.HeaderText = "Total";
            this.TotalColumn.MinimumWidth = 120;
            this.TotalColumn.Name = "TotalColumn";
            this.TotalColumn.ReadOnly = true;
            this.TotalColumn.Width = 120;
            // 
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "DateUtc";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle4.NullValue = null;
            this.DateColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.DateColumn.HeaderText = "Date";
            this.DateColumn.MinimumWidth = 160;
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 170;
            // 
            // MarketTradeHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedCellsTradeTotals);
            this.Controls.Add(this.tradeTotals);
            this.Controls.Add(this.tradeHistoryFiltersControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLoadTradeHistory);
            this.Name = "MarketTradeHistoryControl";
            this.Size = new System.Drawing.Size(1331, 847);
            this.groupBox2.ResumeLayout(false);
            this.gridContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoadTradeHistory;
        private System.Windows.Forms.GroupBox groupBox2;
        private TradeHistoryFiltersControl tradeHistoryFiltersControl1;
        private AVS.CoreLib.WinForms.Grid.GridControl gridControl1;
        private Common.TradeTotalsControl tradeTotals;
        private Common.TradeTotalsControl selectedCellsTradeTotals;
        private System.Windows.Forms.ContextMenuStrip gridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem viewSummaryMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarketColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
    }
}
