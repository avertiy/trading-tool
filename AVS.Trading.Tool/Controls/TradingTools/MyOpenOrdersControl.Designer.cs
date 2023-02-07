namespace AVS.Trading.Tool.Controls.TradingTools
{
    partial class MyOpenOrdersControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new AVS.CoreLib.WinForms.Grid.GridControl();
            this.OrderNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopLossColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TakeProfitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateUtcColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 502);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Open Orders";
            // 
            // gridControl1
            // 
            this.gridControl1.CellFormatter = null;
            this.gridControl1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNumberColumn,
            this.AccountColumn,
            this.TypeColumn,
            this.PriceColumn,
            this.AmountColumn,
            this.TotalColumn,
            this.StateColumn,
            this.StopLossColumn,
            this.TakeProfitColumn,
            this.CreatedColumn,
            this.NotesColumn,
            this.DateUtcColumn});
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Controller = null;
            this.gridControl1.DataSource = null;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.GridSummaryText = "";
            this.gridControl1.Location = new System.Drawing.Point(3, 22);
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.gridControl1.Size = new System.Drawing.Size(873, 477);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.LoadDataCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.gridControl1_LoadDataCompleted);
            // 
            // OrderNumberColumn
            // 
            this.OrderNumberColumn.DataPropertyName = "OrderNumber";
            this.OrderNumberColumn.HeaderText = "Ref #";
            this.OrderNumberColumn.Name = "OrderNumberColumn";
            // 
            // AccountColumn
            // 
            this.AccountColumn.DataPropertyName = "Account";
            this.AccountColumn.HeaderText = "Acc.";
            this.AccountColumn.Name = "AccountColumn";
            this.AccountColumn.ToolTipText = "Exchange / Margin";
            this.AccountColumn.Width = 40;
            // 
            // TypeColumn
            // 
            this.TypeColumn.DataPropertyName = "Type";
            this.TypeColumn.HeaderText = "Type";
            this.TypeColumn.Name = "TypeColumn";
            this.TypeColumn.Width = 60;
            // 
            // PriceColumn
            // 
            this.PriceColumn.DataPropertyName = "Price";
            this.PriceColumn.HeaderText = "Price";
            this.PriceColumn.Name = "PriceColumn";
            this.PriceColumn.Width = 90;
            // 
            // AmountColumn
            // 
            this.AmountColumn.DataPropertyName = "AmountQuote";
            this.AmountColumn.HeaderText = "Amount";
            this.AmountColumn.Name = "AmountColumn";
            this.AmountColumn.Width = 80;
            // 
            // TotalColumn
            // 
            this.TotalColumn.DataPropertyName = "AmountBase";
            this.TotalColumn.HeaderText = "Total";
            this.TotalColumn.Name = "TotalColumn";
            this.TotalColumn.Width = 90;
            // 
            // StateColumn
            // 
            this.StateColumn.DataPropertyName = "State";
            this.StateColumn.HeaderText = "State";
            this.StateColumn.Name = "StateColumn";
            this.StateColumn.Width = 80;
            // 
            // StopLossColumn
            // 
            this.StopLossColumn.DataPropertyName = "StopLoss";
            this.StopLossColumn.HeaderText = "Stop Loss";
            this.StopLossColumn.Name = "StopLossColumn";
            this.StopLossColumn.Width = 110;
            // 
            // TakeProfitColumn
            // 
            this.TakeProfitColumn.DataPropertyName = "TakeProfit";
            this.TakeProfitColumn.HeaderText = "Take Profit";
            this.TakeProfitColumn.Name = "TakeProfitColumn";
            this.TakeProfitColumn.Width = 110;
            // 
            // CreatedColumn
            // 
            this.CreatedColumn.DataPropertyName = "CreatedOnUtc";
            this.CreatedColumn.HeaderText = "Created";
            this.CreatedColumn.Name = "CreatedColumn";
            this.CreatedColumn.Width = 90;
            // 
            // NotesColumn
            // 
            this.NotesColumn.DataPropertyName = "Comment";
            this.NotesColumn.HeaderText = "Notes";
            this.NotesColumn.Name = "NotesColumn";
            this.NotesColumn.Visible = false;
            // 
            // DateUtcColumn
            // 
            this.DateUtcColumn.DataPropertyName = "DateUtc";
            this.DateUtcColumn.HeaderText = "Executed";
            this.DateUtcColumn.Name = "DateUtcColumn";
            this.DateUtcColumn.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Enabled = false;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newOrderToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // newOrderToolStripMenuItem
            // 
            this.newOrderToolStripMenuItem.Name = "newOrderToolStripMenuItem";
            this.newOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newOrderToolStripMenuItem.Text = "&New Order";
            this.newOrderToolStripMenuItem.Click += new System.EventHandler(this.newOrderToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cancelToolStripMenuItem.Text = "&Cancel Order";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // MyOpenOrdersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MyOpenOrdersControl";
            this.Size = new System.Drawing.Size(879, 502);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private CoreLib.WinForms.Grid.GridControl gridControl1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newOrderToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopLossColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TakeProfitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateUtcColumn;
    }
}
