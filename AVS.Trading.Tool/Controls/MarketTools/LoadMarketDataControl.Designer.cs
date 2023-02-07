namespace AVS.Trading.Tool.Controls.MarketTools
{
    partial class LoadMarketDataControl
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
            this.txtTargetCurrencies = new System.Windows.Forms.TextBox();
            this.txtCurrencyPair = new System.Windows.Forms.TextBox();
            this.btnLoadChartData = new System.Windows.Forms.Button();
            this.btnLoadMarketSummary = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTargetCurrencies
            // 
            this.txtTargetCurrencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetCurrencies.Location = new System.Drawing.Point(24, 46);
            this.txtTargetCurrencies.Multiline = true;
            this.txtTargetCurrencies.Name = "txtTargetCurrencies";
            this.txtTargetCurrencies.Size = new System.Drawing.Size(224, 89);
            this.txtTargetCurrencies.TabIndex = 16;
            this.txtTargetCurrencies.Text = "BTC, LTC, XMR, DASH, ETH, ETC, EMC2, RIC, STR";
            // 
            // txtCurrencyPair
            // 
            this.txtCurrencyPair.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrencyPair.Location = new System.Drawing.Point(148, 8);
            this.txtCurrencyPair.Name = "txtCurrencyPair";
            this.txtCurrencyPair.Size = new System.Drawing.Size(100, 26);
            this.txtCurrencyPair.TabIndex = 15;
            this.txtCurrencyPair.Text = "LTC/BTC";
            // 
            // btnLoadChartData
            // 
            this.btnLoadChartData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadChartData.Location = new System.Drawing.Point(304, 46);
            this.btnLoadChartData.Name = "btnLoadChartData";
            this.btnLoadChartData.Size = new System.Drawing.Size(184, 32);
            this.btnLoadChartData.TabIndex = 14;
            this.btnLoadChartData.Text = "Load ChartData";
            this.btnLoadChartData.UseVisualStyleBackColor = true;
            this.btnLoadChartData.Click += new System.EventHandler(this.btnLoadChartData_Click);
            // 
            // btnLoadMarketSummary
            // 
            this.btnLoadMarketSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadMarketSummary.Location = new System.Drawing.Point(304, 5);
            this.btnLoadMarketSummary.Name = "btnLoadMarketSummary";
            this.btnLoadMarketSummary.Size = new System.Drawing.Size(184, 32);
            this.btnLoadMarketSummary.TabIndex = 12;
            this.btnLoadMarketSummary.Text = "Load market summary";
            this.btnLoadMarketSummary.UseVisualStyleBackColor = true;
            this.btnLoadMarketSummary.Click += new System.EventHandler(this.btnLoadMarketSummary_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Market";
            // 
            // LoadMarketDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTargetCurrencies);
            this.Controls.Add(this.txtCurrencyPair);
            this.Controls.Add(this.btnLoadChartData);
            this.Controls.Add(this.btnLoadMarketSummary);
            this.Name = "LoadMarketDataControl";
            this.Size = new System.Drawing.Size(619, 163);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTargetCurrencies;
        private System.Windows.Forms.TextBox txtCurrencyPair;
        private System.Windows.Forms.Button btnLoadChartData;
        private System.Windows.Forms.Button btnLoadMarketSummary;
        private System.Windows.Forms.Label label1;
    }
}
