using AVS.Trading.Tool.Controls.MarketTools.ChildControls;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    partial class MarketOrderBook
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoadOpenOrders = new System.Windows.Forms.Button();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridSellOrders = new System.Windows.Forms.DataGridView();
            this.SellPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellQuoteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellBaseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellSumQuoteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellSumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceSellOrders = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridBuyOrders = new System.Windows.Forms.DataGridView();
            this.BuyPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyQuoteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyBaseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuySumQuoteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuySumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceBuyOrders = new System.Windows.Forms.BindingSource(this.components);
            this.comboFilterAmount = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbHighlightWalls = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbThrottleUpdates = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.marketTickerControl1 = new AVS.Trading.Tool.Controls.MarketTools.ChildControls.MarketTickerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.orderBookSummary1 = new AVS.Trading.Tool.Controls.MarketTools.ChildControls.OrderBookSummary();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSellOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSellOrders)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBuyOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBuyOrders)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadOpenOrders
            // 
            this.btnLoadOpenOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadOpenOrders.Location = new System.Drawing.Point(921, 24);
            this.btnLoadOpenOrders.Name = "btnLoadOpenOrders";
            this.btnLoadOpenOrders.Size = new System.Drawing.Size(121, 30);
            this.btnLoadOpenOrders.TabIndex = 6;
            this.btnLoadOpenOrders.Text = "Load";
            this.btnLoadOpenOrders.UseVisualStyleBackColor = true;
            this.btnLoadOpenOrders.Click += new System.EventHandler(this.btnLoadOpenOrders_Click);
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Checked = true;
            this.cbFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.Location = new System.Drawing.Point(12, 26);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(15, 14);
            this.cbFilter.TabIndex = 2;
            this.cbFilter.UseVisualStyleBackColor = true;
            this.cbFilter.CheckedChanged += new System.EventHandler(this.cbFilter_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridSellOrders);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(495, 540);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 540);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sell orders";
            // 
            // gridSellOrders
            // 
            this.gridSellOrders.AllowUserToAddRows = false;
            this.gridSellOrders.AllowUserToDeleteRows = false;
            this.gridSellOrders.AllowUserToOrderColumns = true;
            this.gridSellOrders.AutoGenerateColumns = false;
            this.gridSellOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridSellOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSellOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SellPriceColumn,
            this.SellQuoteColumn,
            this.SellBaseColumn,
            this.SellSumQuoteColumn,
            this.SellSumColumn});
            this.gridSellOrders.DataSource = this.bindingSourceSellOrders;
            this.gridSellOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSellOrders.Location = new System.Drawing.Point(3, 22);
            this.gridSellOrders.Name = "gridSellOrders";
            this.gridSellOrders.RowHeadersWidth = 20;
            this.gridSellOrders.Size = new System.Drawing.Size(586, 515);
            this.gridSellOrders.TabIndex = 7;
            // 
            // SellPriceColumn
            // 
            this.SellPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SellPriceColumn.DataPropertyName = "Price";
            dataGridViewCellStyle9.Format = "N8";
            this.SellPriceColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.SellPriceColumn.HeaderText = "Price";
            this.SellPriceColumn.MinimumWidth = 100;
            this.SellPriceColumn.Name = "SellPriceColumn";
            // 
            // SellQuoteColumn
            // 
            this.SellQuoteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SellQuoteColumn.DataPropertyName = "AmountQuote";
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.SellQuoteColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.SellQuoteColumn.HeaderText = "Quote";
            this.SellQuoteColumn.MinimumWidth = 100;
            this.SellQuoteColumn.Name = "SellQuoteColumn";
            // 
            // SellBaseColumn
            // 
            this.SellBaseColumn.DataPropertyName = "AmountBase";
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.SellBaseColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.SellBaseColumn.HeaderText = "Base";
            this.SellBaseColumn.MinimumWidth = 100;
            this.SellBaseColumn.Name = "SellBaseColumn";
            // 
            // SellSumQuoteColumn
            // 
            this.SellSumQuoteColumn.DataPropertyName = "SumQuote";
            this.SellSumQuoteColumn.HeaderText = "Sum Quote ";
            this.SellSumQuoteColumn.Name = "SellSumQuoteColumn";
            this.SellSumQuoteColumn.Width = 119;
            // 
            // SellSumColumn
            // 
            this.SellSumColumn.DataPropertyName = "Sum";
            dataGridViewCellStyle12.Format = "N2";
            this.SellSumColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.SellSumColumn.HeaderText = "Sum";
            this.SellSumColumn.MinimumWidth = 100;
            this.SellSumColumn.Name = "SellSumColumn";
            // 
            // bindingSourceSellOrders
            // 
            this.bindingSourceSellOrders.AllowNew = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridBuyOrders);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(611, 0);
            this.groupBox2.MinimumSize = new System.Drawing.Size(495, 540);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(581, 540);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buy orders";
            // 
            // gridBuyOrders
            // 
            this.gridBuyOrders.AllowUserToAddRows = false;
            this.gridBuyOrders.AllowUserToDeleteRows = false;
            this.gridBuyOrders.AllowUserToOrderColumns = true;
            this.gridBuyOrders.AutoGenerateColumns = false;
            this.gridBuyOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridBuyOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBuyOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BuyPriceColumn,
            this.BuyQuoteColumn,
            this.BuyBaseColumn,
            this.BuySumQuoteColumn,
            this.BuySumColumn});
            this.gridBuyOrders.DataSource = this.bindingSourceBuyOrders;
            this.gridBuyOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBuyOrders.Location = new System.Drawing.Point(3, 22);
            this.gridBuyOrders.Name = "gridBuyOrders";
            this.gridBuyOrders.RowHeadersWidth = 20;
            this.gridBuyOrders.Size = new System.Drawing.Size(575, 515);
            this.gridBuyOrders.TabIndex = 8;
            // 
            // BuyPriceColumn
            // 
            this.BuyPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BuyPriceColumn.DataPropertyName = "Price";
            dataGridViewCellStyle13.Format = "N8";
            this.BuyPriceColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.BuyPriceColumn.HeaderText = "Price";
            this.BuyPriceColumn.MinimumWidth = 100;
            this.BuyPriceColumn.Name = "BuyPriceColumn";
            // 
            // BuyQuoteColumn
            // 
            this.BuyQuoteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BuyQuoteColumn.DataPropertyName = "AmountQuote";
            dataGridViewCellStyle14.Format = "N0";
            this.BuyQuoteColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.BuyQuoteColumn.HeaderText = "Quote";
            this.BuyQuoteColumn.MinimumWidth = 100;
            this.BuyQuoteColumn.Name = "BuyQuoteColumn";
            // 
            // BuyBaseColumn
            // 
            this.BuyBaseColumn.DataPropertyName = "AmountBase";
            dataGridViewCellStyle15.Format = "N2";
            this.BuyBaseColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.BuyBaseColumn.HeaderText = "Base";
            this.BuyBaseColumn.MinimumWidth = 100;
            this.BuyBaseColumn.Name = "BuyBaseColumn";
            // 
            // BuySumQuoteColumn
            // 
            this.BuySumQuoteColumn.DataPropertyName = "SumQuote";
            this.BuySumQuoteColumn.HeaderText = "Sum Quote";
            this.BuySumQuoteColumn.Name = "BuySumQuoteColumn";
            this.BuySumQuoteColumn.Width = 115;
            // 
            // BuySumColumn
            // 
            this.BuySumColumn.DataPropertyName = "Sum";
            dataGridViewCellStyle16.Format = "N2";
            this.BuySumColumn.DefaultCellStyle = dataGridViewCellStyle16;
            this.BuySumColumn.HeaderText = "Sum";
            this.BuySumColumn.MinimumWidth = 100;
            this.BuySumColumn.Name = "BuySumColumn";
            // 
            // comboFilterAmount
            // 
            this.comboFilterAmount.FormattingEnabled = true;
            this.comboFilterAmount.Location = new System.Drawing.Point(33, 19);
            this.comboFilterAmount.Name = "comboFilterAmount";
            this.comboFilterAmount.Size = new System.Drawing.Size(97, 26);
            this.comboFilterAmount.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbFilter);
            this.groupBox4.Controls.Add(this.comboFilterAmount);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(165, 59);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter out small orders";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbHighlightWalls);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(172, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 59);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Highlight walls";
            // 
            // cbHighlightWalls
            // 
            this.cbHighlightWalls.AutoSize = true;
            this.cbHighlightWalls.Checked = true;
            this.cbHighlightWalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHighlightWalls.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHighlightWalls.Location = new System.Drawing.Point(52, 26);
            this.cbHighlightWalls.Name = "cbHighlightWalls";
            this.cbHighlightWalls.Size = new System.Drawing.Size(15, 14);
            this.cbHighlightWalls.TabIndex = 4;
            this.cbHighlightWalls.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.LargeChange = 3;
            this.trackBar1.Location = new System.Drawing.Point(3, 20);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(180, 36);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 10;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.trackBar1);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(299, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(186, 59);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Look up price range (%)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(27, 21);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(62, 24);
            this.numericUpDown1.TabIndex = 23;
            this.numericUpDown1.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.cbThrottleUpdates);
            this.groupBox6.Controls.Add(this.numericUpDown1);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(489, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(139, 59);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Throttle updates";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "sec.";
            // 
            // cbThrottleUpdates
            // 
            this.cbThrottleUpdates.AutoSize = true;
            this.cbThrottleUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbThrottleUpdates.Location = new System.Drawing.Point(6, 27);
            this.cbThrottleUpdates.Name = "cbThrottleUpdates";
            this.cbThrottleUpdates.Size = new System.Drawing.Size(15, 14);
            this.cbThrottleUpdates.TabIndex = 4;
            this.cbThrottleUpdates.UseVisualStyleBackColor = true;
            this.cbThrottleUpdates.CheckedChanged += new System.EventHandler(this.cbThrottleUpdates_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // marketTickerControl1
            // 
            this.marketTickerControl1.Change = "+0.0%";
            this.marketTickerControl1.HighestBid = "0.0000000";
            this.marketTickerControl1.Location = new System.Drawing.Point(3, 0);
            this.marketTickerControl1.LowestAsk = "0.0000000";
            this.marketTickerControl1.Name = "marketTickerControl1";
            this.marketTickerControl1.Price = "0.000000";
            this.marketTickerControl1.Size = new System.Drawing.Size(239, 223);
            this.marketTickerControl1.TabIndex = 24;
            this.marketTickerControl1.Volume = "0.00";
            this.marketTickerControl1.MarketChanged += new System.EventHandler<AVS.Trading.Data.Domain.MarketTools.MarketData>(this.marketTickerControl1_MarketChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Location = new System.Drawing.Point(259, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 68);
            this.panel1.TabIndex = 25;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 738);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 238);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1192, 500);
            this.panel2.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(592, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(19, 500);
            this.panel4.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.orderBookSummary1);
            this.panel3.Controls.Add(this.btnLoadOpenOrders);
            this.panel3.Controls.Add(this.marketTickerControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1192, 238);
            this.panel3.TabIndex = 28;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // orderBookSummary1
            // 
            this.orderBookSummary1.Location = new System.Drawing.Point(259, 74);
            this.orderBookSummary1.Name = "orderBookSummary1";
            this.orderBookSummary1.Size = new System.Drawing.Size(783, 149);
            this.orderBookSummary1.TabIndex = 29;
            // 
            // MarketOrderBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel3);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MarketOrderBook";
            this.Size = new System.Drawing.Size(1192, 760);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSellOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSellOrders)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBuyOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBuyOrders)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLoadOpenOrders;
        private System.Windows.Forms.BindingSource bindingSourceSellOrders;
        private System.Windows.Forms.BindingSource bindingSourceBuyOrders;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridSellOrders;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboFilterAmount;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbHighlightWalls;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbThrottleUpdates;
        private System.Windows.Forms.Timer timer1;
        private MarketTickerControl marketTickerControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView gridBuyOrders;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private OrderBookSummary orderBookSummary1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellQuoteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellBaseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellSumQuoteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellSumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyQuoteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyBaseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuySumQuoteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuySumColumn;
    }
}
