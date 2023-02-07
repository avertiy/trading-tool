namespace AVS.Poloniex.Controls.WalletTools
{
    partial class MyOpenLoansControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gridControl1 = new AVS.CoreLib.WinForms.Grid.GridControl();
            this.activeLoansSummaryControl1 = new AVS.Poloniex.Controls.WalletTools.ChildControls.ActiveLoansSummaryControl();
            this.selectCurrencyControl2 = new AVS.Poloniex.Controls.Common.SelectCurrencyControl();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(896, 21);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CurrencyColumn1,
            this.Amount,
            this.Rate,
            this.Duration,
            this.Fees,
            this.Date});
            this.gridControl1.Controller = null;
            this.gridControl1.DataSource = null;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControl1.GridSummaryText = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MinimumSize = new System.Drawing.Size(650, 537);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.gridControl1.Size = new System.Drawing.Size(719, 627);
            this.gridControl1.TabIndex = 4;
            // 
            // activeLoansSummaryControl1
            // 
            this.activeLoansSummaryControl1.Currency = null;
            this.activeLoansSummaryControl1.DataSource = null;
            this.activeLoansSummaryControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeLoansSummaryControl1.Location = new System.Drawing.Point(741, 64);
            this.activeLoansSummaryControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.activeLoansSummaryControl1.Name = "activeLoansSummaryControl1";
            this.activeLoansSummaryControl1.Size = new System.Drawing.Size(277, 85);
            this.activeLoansSummaryControl1.TabIndex = 10;
            // 
            // selectCurrencyControl2
            // 
            this.selectCurrencyControl2.Currency = "BTC";
            this.selectCurrencyControl2.DataSource = new string[] {
        "BTC",
        "BTS",
        "DASH",
        "DOGE",
        "ETH",
        "FCT",
        "LTC",
        "MAID",
        "STR",
        "XMR",
        "XRP"};
            this.selectCurrencyControl2.Location = new System.Drawing.Point(741, 0);
            this.selectCurrencyControl2.Name = "selectCurrencyControl2";
            this.selectCurrencyControl2.Size = new System.Drawing.Size(149, 56);
            this.selectCurrencyControl2.TabIndex = 9;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 70;
            // 
            // CurrencyColumn1
            // 
            this.CurrencyColumn1.DataPropertyName = "Currency";
            this.CurrencyColumn1.HeaderText = "Currency";
            this.CurrencyColumn1.Name = "CurrencyColumn1";
            this.CurrencyColumn1.Width = 90;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 120;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle1.Format = "P3";
            dataGridViewCellStyle1.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle1;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            // 
            // Duration
            // 
            this.Duration.DataPropertyName = "Duration";
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.Width = 80;
            // 
            // Fees
            // 
            this.Fees.DataPropertyName = "Fees";
            this.Fees.HeaderText = "Fees";
            this.Fees.Name = "Fees";
            // 
            // Date
            // 
            this.Date.DataPropertyName = "DateUtc";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 120;
            // 
            // MyLoansControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.activeLoansSummaryControl1);
            this.Controls.Add(this.selectCurrencyControl2);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.gridControl1);
            this.MinimumSize = new System.Drawing.Size(964, 440);
            this.Name = "MyLoansControl";
            this.Size = new System.Drawing.Size(1042, 627);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private CoreLib.WinForms.Grid.GridControl gridControl1;
        private Common.SelectCurrencyControl selectCurrencyControl2;
        private ChildControls.ActiveLoansSummaryControl activeLoansSummaryControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrencyColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fees;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
    }
}
