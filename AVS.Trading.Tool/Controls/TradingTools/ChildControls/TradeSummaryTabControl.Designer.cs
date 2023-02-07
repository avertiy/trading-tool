namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    partial class TradeSummaryTabControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEx = new System.Windows.Forms.TabPage();
            this.plSummaryExSelectedCells = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.TradeSummaryProfitLoss();
            this.plSummaryEx = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.TradeSummaryProfitLoss();
            this.tabPageMargin = new System.Windows.Forms.TabPage();
            this.plSummaryMarginSelectedCells = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.TradeSummaryProfitLoss();
            this.plSummaryMargin = new AVS.Trading.Tool.Controls.TradingTools.ChildControls.TradeSummaryProfitLoss();
            this.tabControl1.SuspendLayout();
            this.tabPageEx.SuspendLayout();
            this.tabPageMargin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageEx);
            this.tabControl1.Controls.Add(this.tabPageMargin);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(699, 233);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageEx
            // 
            this.tabPageEx.Controls.Add(this.plSummaryExSelectedCells);
            this.tabPageEx.Controls.Add(this.plSummaryEx);
            this.tabPageEx.Location = new System.Drawing.Point(4, 27);
            this.tabPageEx.Name = "tabPageEx";
            this.tabPageEx.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEx.Size = new System.Drawing.Size(720, 202);
            this.tabPageEx.TabIndex = 0;
            this.tabPageEx.Text = "Exchange";
            this.tabPageEx.UseVisualStyleBackColor = true;
            // 
            // plSummaryExSelectedCells
            // 
            this.plSummaryExSelectedCells.BackColor = System.Drawing.SystemColors.Info;
            this.plSummaryExSelectedCells.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plSummaryExSelectedCells.Location = new System.Drawing.Point(343, 3);
            this.plSummaryExSelectedCells.Name = "plSummaryExSelectedCells";
            this.plSummaryExSelectedCells.Size = new System.Drawing.Size(341, 198);
            this.plSummaryExSelectedCells.TabIndex = 0;
            // 
            // plSummaryEx
            // 
            this.plSummaryEx.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plSummaryEx.Location = new System.Drawing.Point(3, 3);
            this.plSummaryEx.Name = "plSummaryEx";
            this.plSummaryEx.Size = new System.Drawing.Size(355, 195);
            this.plSummaryEx.TabIndex = 69;
            // 
            // tabPageMargin
            // 
            this.tabPageMargin.Controls.Add(this.plSummaryMarginSelectedCells);
            this.tabPageMargin.Controls.Add(this.plSummaryMargin);
            this.tabPageMargin.Location = new System.Drawing.Point(4, 27);
            this.tabPageMargin.Name = "tabPageMargin";
            this.tabPageMargin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMargin.Size = new System.Drawing.Size(691, 202);
            this.tabPageMargin.TabIndex = 1;
            this.tabPageMargin.Text = "Margin";
            this.tabPageMargin.UseVisualStyleBackColor = true;
            // 
            // plSummaryMarginSelectedCells
            // 
            this.plSummaryMarginSelectedCells.BackColor = System.Drawing.SystemColors.Info;
            this.plSummaryMarginSelectedCells.Location = new System.Drawing.Point(343, 3);
            this.plSummaryMarginSelectedCells.Name = "plSummaryMarginSelectedCells";
            this.plSummaryMarginSelectedCells.Size = new System.Drawing.Size(344, 195);
            this.plSummaryMarginSelectedCells.TabIndex = 0;
            // 
            // plSummaryMargin
            // 
            this.plSummaryMargin.Location = new System.Drawing.Point(3, 3);
            this.plSummaryMargin.Name = "plSummaryMargin";
            this.plSummaryMargin.Size = new System.Drawing.Size(355, 195);
            this.plSummaryMargin.TabIndex = 71;
            // 
            // TradeSummaryTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "TradeSummaryTabControl";
            this.Size = new System.Drawing.Size(699, 233);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEx.ResumeLayout(false);
            this.tabPageMargin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageEx;
        private System.Windows.Forms.TabPage tabPageMargin;
        private TradeSummaryProfitLoss plSummaryEx;
        private TradeSummaryProfitLoss plSummaryExSelectedCells;
        private TradeSummaryProfitLoss plSummaryMargin;
        private TradeSummaryProfitLoss plSummaryMarginSelectedCells;
    }
}
