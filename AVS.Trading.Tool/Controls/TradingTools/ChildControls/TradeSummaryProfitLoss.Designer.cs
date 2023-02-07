namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    partial class TradeSummaryProfitLoss
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSettlementTotal = new System.Windows.Forms.Label();
            this.lblSettlementAmount = new System.Windows.Forms.Label();
            this.lblSettlementLabel = new System.Windows.Forms.Label();
            this.lblPLTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPLAmount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDiff = new System.Windows.Forms.Label();
            this.lblPLSubTotal = new System.Windows.Forms.Label();
            this.pnlBuy = new System.Windows.Forms.Panel();
            this.lblBuyTotal = new System.Windows.Forms.Label();
            this.lblBuyVolume = new System.Windows.Forms.Label();
            this.lblAvgBuyPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblSellTotal = new System.Windows.Forms.Label();
            this.lblSellVolume = new System.Windows.Forms.Label();
            this.lblAvgSellPrice = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.pnlBuy.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblSettlementTotal);
            this.panel1.Controls.Add(this.lblSettlementAmount);
            this.panel1.Controls.Add(this.lblSettlementLabel);
            this.panel1.Controls.Add(this.lblPLTotal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblAction);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblPLAmount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblDiff);
            this.panel1.Controls.Add(this.lblPLSubTotal);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 92);
            this.panel1.TabIndex = 64;
            // 
            // lblSettlementTotal
            // 
            this.lblSettlementTotal.AutoSize = true;
            this.lblSettlementTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettlementTotal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSettlementTotal.Location = new System.Drawing.Point(200, 42);
            this.lblSettlementTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettlementTotal.Name = "lblSettlementTotal";
            this.lblSettlementTotal.Size = new System.Drawing.Size(112, 16);
            this.lblSettlementTotal.TabIndex = 84;
            this.lblSettlementTotal.Text = "0.00000000 (BTC)";
            // 
            // lblSettlementAmount
            // 
            this.lblSettlementAmount.AutoSize = true;
            this.lblSettlementAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettlementAmount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSettlementAmount.Location = new System.Drawing.Point(45, 42);
            this.lblSettlementAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettlementAmount.Name = "lblSettlementAmount";
            this.lblSettlementAmount.Size = new System.Drawing.Size(112, 16);
            this.lblSettlementAmount.TabIndex = 83;
            this.lblSettlementAmount.Text = "0.00000000 (BTS)";
            // 
            // lblSettlementLabel
            // 
            this.lblSettlementLabel.AutoSize = true;
            this.lblSettlementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettlementLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSettlementLabel.Location = new System.Drawing.Point(3, 45);
            this.lblSettlementLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettlementLabel.Name = "lblSettlementLabel";
            this.lblSettlementLabel.Size = new System.Drawing.Size(36, 13);
            this.lblSettlementLabel.TabIndex = 82;
            this.lblSettlementLabel.Text = "Settl-s";
            this.toolTip1.SetToolTip(this.lblSettlementLabel, "Settlements");
            // 
            // lblPLTotal
            // 
            this.lblPLTotal.AutoSize = true;
            this.lblPLTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLTotal.Location = new System.Drawing.Point(174, 63);
            this.lblPLTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPLTotal.Name = "lblPLTotal";
            this.lblPLTotal.Size = new System.Drawing.Size(141, 17);
            this.lblPLTotal.TabIndex = 81;
            this.lblPLTotal.Text = "0.00000000 (BTC)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(70, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 80;
            this.label3.Text = "P&&L TOTAL";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAction.Location = new System.Drawing.Point(200, 20);
            this.lblAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(57, 18);
            this.lblAction.TabIndex = 79;
            this.lblAction.Text = "buy/sell";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(3, 22);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 18);
            this.label11.TabIndex = 78;
            this.label11.Text = "Diff";
            // 
            // lblPLAmount
            // 
            this.lblPLAmount.AutoSize = true;
            this.lblPLAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLAmount.Location = new System.Drawing.Point(45, 2);
            this.lblPLAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPLAmount.Name = "lblPLAmount";
            this.lblPLAmount.Size = new System.Drawing.Size(125, 17);
            this.lblPLAmount.TabIndex = 77;
            this.lblPLAmount.Text = "0.00000000 (BTS)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 18);
            this.label8.TabIndex = 76;
            this.label8.Text = "P&&L";
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiff.Location = new System.Drawing.Point(45, 22);
            this.lblDiff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(125, 17);
            this.lblDiff.TabIndex = 74;
            this.lblDiff.Text = "0.00000000 (BTS)";
            // 
            // lblPLSubTotal
            // 
            this.lblPLSubTotal.AutoSize = true;
            this.lblPLSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLSubTotal.Location = new System.Drawing.Point(200, 2);
            this.lblPLSubTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPLSubTotal.Name = "lblPLSubTotal";
            this.lblPLSubTotal.Size = new System.Drawing.Size(125, 17);
            this.lblPLSubTotal.TabIndex = 75;
            this.lblPLSubTotal.Text = "0.00000000 (BTC)";
            // 
            // pnlBuy
            // 
            this.pnlBuy.Controls.Add(this.lblBuyTotal);
            this.pnlBuy.Controls.Add(this.lblBuyVolume);
            this.pnlBuy.Controls.Add(this.lblAvgBuyPrice);
            this.pnlBuy.Controls.Add(this.label2);
            this.pnlBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBuy.Location = new System.Drawing.Point(37, 1);
            this.pnlBuy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBuy.Name = "pnlBuy";
            this.pnlBuy.Size = new System.Drawing.Size(144, 97);
            this.pnlBuy.TabIndex = 62;
            // 
            // lblBuyTotal
            // 
            this.lblBuyTotal.AutoSize = true;
            this.lblBuyTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuyTotal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblBuyTotal.Location = new System.Drawing.Point(4, 73);
            this.lblBuyTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuyTotal.Name = "lblBuyTotal";
            this.lblBuyTotal.Size = new System.Drawing.Size(125, 17);
            this.lblBuyTotal.TabIndex = 40;
            this.lblBuyTotal.Text = "0.00000000 (BTC)";
            // 
            // lblBuyVolume
            // 
            this.lblBuyVolume.AutoSize = true;
            this.lblBuyVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuyVolume.Location = new System.Drawing.Point(4, 51);
            this.lblBuyVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuyVolume.Name = "lblBuyVolume";
            this.lblBuyVolume.Size = new System.Drawing.Size(125, 17);
            this.lblBuyVolume.TabIndex = 39;
            this.lblBuyVolume.Text = "0.00000000 (BTS)";
            // 
            // lblAvgBuyPrice
            // 
            this.lblAvgBuyPrice.AutoSize = true;
            this.lblAvgBuyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgBuyPrice.Location = new System.Drawing.Point(4, 30);
            this.lblAvgBuyPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgBuyPrice.Name = "lblAvgBuyPrice";
            this.lblAvgBuyPrice.Size = new System.Drawing.Size(125, 17);
            this.lblAvgBuyPrice.TabIndex = 38;
            this.lblAvgBuyPrice.Text = "0.00000000 (BTC)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(18, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Buys";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblSellTotal);
            this.panel5.Controls.Add(this.lblSellVolume);
            this.panel5.Controls.Add(this.lblAvgSellPrice);
            this.panel5.Controls.Add(this.label40);
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(181, 1);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(156, 97);
            this.panel5.TabIndex = 63;
            // 
            // lblSellTotal
            // 
            this.lblSellTotal.AutoSize = true;
            this.lblSellTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSellTotal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSellTotal.Location = new System.Drawing.Point(8, 73);
            this.lblSellTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellTotal.Name = "lblSellTotal";
            this.lblSellTotal.Size = new System.Drawing.Size(125, 17);
            this.lblSellTotal.TabIndex = 40;
            this.lblSellTotal.Text = "0.00000000 (BTC)";
            // 
            // lblSellVolume
            // 
            this.lblSellVolume.AutoSize = true;
            this.lblSellVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSellVolume.Location = new System.Drawing.Point(8, 51);
            this.lblSellVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellVolume.Name = "lblSellVolume";
            this.lblSellVolume.Size = new System.Drawing.Size(125, 17);
            this.lblSellVolume.TabIndex = 39;
            this.lblSellVolume.Text = "0.00000000 (BTS)";
            // 
            // lblAvgSellPrice
            // 
            this.lblAvgSellPrice.AutoSize = true;
            this.lblAvgSellPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgSellPrice.Location = new System.Drawing.Point(8, 30);
            this.lblAvgSellPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgSellPrice.Name = "lblAvgSellPrice";
            this.lblAvgSellPrice.Size = new System.Drawing.Size(125, 17);
            this.lblAvgSellPrice.TabIndex = 38;
            this.lblAvgSellPrice.Text = "0.00000000 (BTC)";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.DarkRed;
            this.label40.Location = new System.Drawing.Point(11, 3);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(45, 18);
            this.label40.TabIndex = 0;
            this.label40.Text = "Sells";
            // 
            // TradeSummaryProfitLoss
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlBuy);
            this.Controls.Add(this.panel5);
            this.Name = "TradeSummaryProfitLoss";
            this.Size = new System.Drawing.Size(339, 192);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBuy.ResumeLayout(false);
            this.pnlBuy.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPLTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblPLAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.Label lblPLSubTotal;
        private System.Windows.Forms.Panel pnlBuy;
        private System.Windows.Forms.Label lblBuyTotal;
        private System.Windows.Forms.Label lblBuyVolume;
        private System.Windows.Forms.Label lblAvgBuyPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblSellTotal;
        private System.Windows.Forms.Label lblSellVolume;
        private System.Windows.Forms.Label lblAvgSellPrice;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblSettlementTotal;
        private System.Windows.Forms.Label lblSettlementAmount;
        private System.Windows.Forms.Label lblSettlementLabel;
    }
}
