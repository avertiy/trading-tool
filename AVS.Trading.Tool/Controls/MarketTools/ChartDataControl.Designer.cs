namespace AVS.Trading.Tool.Controls.MarketTools
{
    partial class ChartDataControl
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.selectMarketPeriodControl1 = new AVS.Trading.Tool.Controls.Common.SelectMarketPeriodControl();
            this.selectMarketControl1 = new AVS.Trading.Tool.Controls.Common.SelectMarketControl();
            this.selectDateRangeControl1 = new AVS.Trading.Tool.Controls.Common.SelectDateRangeControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(625, 27);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // selectMarketPeriodControl1
            // 
            this.selectMarketPeriodControl1.Location = new System.Drawing.Point(158, 3);
            this.selectMarketPeriodControl1.Name = "selectMarketPeriodControl1";
            this.selectMarketPeriodControl1.SelectedPeriod = AVS.Trading.Core.Enums.MarketPeriod.M5;
            this.selectMarketPeriodControl1.Size = new System.Drawing.Size(79, 70);
            this.selectMarketPeriodControl1.TabIndex = 20;
            // 
            // selectMarketControl1
            // 
            this.selectMarketControl1.DataSource = null;
            this.selectMarketControl1.Location = new System.Drawing.Point(3, 3);
            this.selectMarketControl1.Market = "";
            this.selectMarketControl1.Name = "selectMarketControl1";
            this.selectMarketControl1.Size = new System.Drawing.Size(149, 70);
            this.selectMarketControl1.TabIndex = 19;
            // 
            // selectDateRangeControl1
            // 
            this.selectDateRangeControl1.Location = new System.Drawing.Point(243, 3);
            this.selectDateRangeControl1.Name = "selectDateRangeControl1";
            this.selectDateRangeControl1.Size = new System.Drawing.Size(366, 70);
            this.selectDateRangeControl1.TabIndex = 18;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(847, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ChartDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.selectMarketPeriodControl1);
            this.Controls.Add(this.selectMarketControl1);
            this.Controls.Add(this.selectDateRangeControl1);
            this.Name = "ChartDataControl";
            this.Size = new System.Drawing.Size(847, 492);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private Common.SelectMarketPeriodControl selectMarketPeriodControl1;
        private Common.SelectMarketControl selectMarketControl1;
        private Common.SelectDateRangeControl selectDateRangeControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
