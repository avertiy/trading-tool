namespace AVS.Trading.Tool.Forms.MarketTools
{
    partial class OrderBookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderBookForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.marketOrderBook1 = new AVS.Trading.Tool.Controls.MarketTools.MarketOrderBook();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // 
            // marketOrderBook1
            // 
            this.marketOrderBook1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marketOrderBook1.Location = new System.Drawing.Point(0, 0);
            this.marketOrderBook1.MinimumSize = new System.Drawing.Size(1000, 700);
            this.marketOrderBook1.Name = "marketOrderBook1";
            this.marketOrderBook1.Size = new System.Drawing.Size(1565, 749);
            this.marketOrderBook1.StatusText = " ";
            this.marketOrderBook1.TabIndex = 0;

            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.marketOrderBook1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1565, 749);
            this.panel1.TabIndex = 0;
            
            // 
            // OrderBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 749);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrderBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order book";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.MarketTools.MarketOrderBook marketOrderBook1;
    }
}