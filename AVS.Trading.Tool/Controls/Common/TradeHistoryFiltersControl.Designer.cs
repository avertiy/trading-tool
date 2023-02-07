namespace AVS.Trading.Tool.Controls.Common
{
    partial class TradeHistoryFiltersControl
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbBuys = new System.Windows.Forms.CheckBox();
            this.cbSells = new System.Windows.Forms.CheckBox();
            this.tradeCategoryGroupBox = new System.Windows.Forms.GroupBox();
            this.cbSettlement = new System.Windows.Forms.CheckBox();
            this.cbExchange = new System.Windows.Forms.CheckBox();
            this.cbMargin = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAmount = new System.Windows.Forms.RadioButton();
            this.rbPrice = new System.Windows.Forms.RadioButton();
            this.txtAmountMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmountMin = new System.Windows.Forms.TextBox();
            this.cbReduce = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReduce = new System.Windows.Forms.TextBox();
            this.selectMarketControl1 = new AVS.Trading.Tool.Controls.Common.SelectMarketControl();
            this._selectDateRangeControl1 = new AVS.Trading.Tool.Controls.Common.SelectDateRangeControl();
            this.groupBox5.SuspendLayout();
            this.tradeCategoryGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbBuys);
            this.groupBox5.Controls.Add(this.cbSells);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(495, 79);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(148, 70);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Type";
            // 
            // cbBuys
            // 
            this.cbBuys.AutoSize = true;
            this.cbBuys.Checked = true;
            this.cbBuys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBuys.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBuys.ForeColor = System.Drawing.Color.ForestGreen;
            this.cbBuys.Location = new System.Drawing.Point(16, 22);
            this.cbBuys.Name = "cbBuys";
            this.cbBuys.Size = new System.Drawing.Size(63, 24);
            this.cbBuys.TabIndex = 31;
            this.cbBuys.Text = "Buys";
            this.cbBuys.UseVisualStyleBackColor = true;
            // 
            // cbSells
            // 
            this.cbSells.AutoSize = true;
            this.cbSells.Checked = true;
            this.cbSells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSells.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSells.ForeColor = System.Drawing.Color.DarkRed;
            this.cbSells.Location = new System.Drawing.Point(85, 21);
            this.cbSells.Name = "cbSells";
            this.cbSells.Size = new System.Drawing.Size(62, 24);
            this.cbSells.TabIndex = 32;
            this.cbSells.Text = "Sells";
            this.cbSells.UseVisualStyleBackColor = true;
            // 
            // tradeCategoryGroupBox
            // 
            this.tradeCategoryGroupBox.Controls.Add(this.cbSettlement);
            this.tradeCategoryGroupBox.Controls.Add(this.cbExchange);
            this.tradeCategoryGroupBox.Controls.Add(this.cbMargin);
            this.tradeCategoryGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tradeCategoryGroupBox.Location = new System.Drawing.Point(184, 79);
            this.tradeCategoryGroupBox.Name = "tradeCategoryGroupBox";
            this.tradeCategoryGroupBox.Size = new System.Drawing.Size(305, 70);
            this.tradeCategoryGroupBox.TabIndex = 35;
            this.tradeCategoryGroupBox.TabStop = false;
            this.tradeCategoryGroupBox.Text = "Category";
            // 
            // cbSettlement
            // 
            this.cbSettlement.AutoSize = true;
            this.cbSettlement.Checked = true;
            this.cbSettlement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSettlement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSettlement.Location = new System.Drawing.Point(193, 24);
            this.cbSettlement.Name = "cbSettlement";
            this.cbSettlement.Size = new System.Drawing.Size(106, 24);
            this.cbSettlement.TabIndex = 31;
            this.cbSettlement.Text = "Settlement";
            this.cbSettlement.UseVisualStyleBackColor = true;
            // 
            // cbExchange
            // 
            this.cbExchange.AutoSize = true;
            this.cbExchange.Checked = true;
            this.cbExchange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExchange.Location = new System.Drawing.Point(6, 24);
            this.cbExchange.Name = "cbExchange";
            this.cbExchange.Size = new System.Drawing.Size(99, 24);
            this.cbExchange.TabIndex = 29;
            this.cbExchange.Text = "Exchange";
            this.cbExchange.UseVisualStyleBackColor = true;
            // 
            // cbMargin
            // 
            this.cbMargin.AutoSize = true;
            this.cbMargin.Checked = true;
            this.cbMargin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMargin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMargin.Location = new System.Drawing.Point(111, 24);
            this.cbMargin.Name = "cbMargin";
            this.cbMargin.Size = new System.Drawing.Size(76, 24);
            this.cbMargin.TabIndex = 30;
            this.cbMargin.Text = "Margin";
            this.cbMargin.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAmount);
            this.groupBox2.Controls.Add(this.rbPrice);
            this.groupBox2.Controls.Add(this.txtAmountMax);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAmountMin);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 70);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // rbAmount
            // 
            this.rbAmount.AutoSize = true;
            this.rbAmount.Checked = true;
            this.rbAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAmount.Location = new System.Drawing.Point(8, 49);
            this.rbAmount.Name = "rbAmount";
            this.rbAmount.Size = new System.Drawing.Size(74, 17);
            this.rbAmount.TabIndex = 35;
            this.rbAmount.TabStop = true;
            this.rbAmount.Text = "by amount";
            this.rbAmount.UseVisualStyleBackColor = true;
            // 
            // rbPrice
            // 
            this.rbPrice.AutoSize = true;
            this.rbPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrice.Location = new System.Drawing.Point(104, 50);
            this.rbPrice.Name = "rbPrice";
            this.rbPrice.Size = new System.Drawing.Size(62, 17);
            this.rbPrice.TabIndex = 34;
            this.rbPrice.Text = "by price";
            this.rbPrice.UseVisualStyleBackColor = true;
            // 
            // txtAmountMax
            // 
            this.txtAmountMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountMax.Location = new System.Drawing.Point(102, 22);
            this.txtAmountMax.Name = "txtAmountMax";
            this.txtAmountMax.Size = new System.Drawing.Size(66, 26);
            this.txtAmountMax.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "-";
            // 
            // txtAmountMin
            // 
            this.txtAmountMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountMin.Location = new System.Drawing.Point(6, 22);
            this.txtAmountMin.Name = "txtAmountMin";
            this.txtAmountMin.Size = new System.Drawing.Size(70, 26);
            this.txtAmountMin.TabIndex = 27;
            // 
            // cbReduce
            // 
            this.cbReduce.AutoSize = true;
            this.cbReduce.Checked = true;
            this.cbReduce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReduce.Location = new System.Drawing.Point(10, 32);
            this.cbReduce.Name = "cbReduce";
            this.cbReduce.Size = new System.Drawing.Size(15, 14);
            this.cbReduce.TabIndex = 32;
            this.cbReduce.UseVisualStyleBackColor = true;
            this.cbReduce.CheckedChanged += new System.EventHandler(this.cbReduce_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.selectMarketControl1);
            this.flowLayoutPanel1.Controls.Add(this._selectDateRangeControl1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.tradeCategoryGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(654, 153);
            this.flowLayoutPanel1.TabIndex = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReduce);
            this.groupBox1.Controls.Add(this.cbReduce);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(531, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 70);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reduce";
            // 
            // txtReduce
            // 
            this.txtReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReduce.Location = new System.Drawing.Point(31, 25);
            this.txtReduce.Name = "txtReduce";
            this.txtReduce.Size = new System.Drawing.Size(70, 26);
            this.txtReduce.TabIndex = 36;
            this.txtReduce.Text = "1.0";
            // 
            // selectMarketControl1
            // 
            this.selectMarketControl1.Location = new System.Drawing.Point(3, 3);
            this.selectMarketControl1.Name = "selectMarketControl1";
            this.selectMarketControl1.Size = new System.Drawing.Size(149, 70);
            this.selectMarketControl1.TabIndex = 38;
            // 
            // selectPeriodControl1
            // 
            this._selectDateRangeControl1.Location = new System.Drawing.Point(158, 3);
            this._selectDateRangeControl1.Name = "_selectDateRangeControl1";
            this._selectDateRangeControl1.Size = new System.Drawing.Size(367, 70);
            this._selectDateRangeControl1.TabIndex = 39;
            // 
            // TradeHistoryFiltersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TradeHistoryFiltersControl";
            this.Size = new System.Drawing.Size(654, 153);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tradeCategoryGroupBox.ResumeLayout(false);
            this.tradeCategoryGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbBuys;
        private System.Windows.Forms.CheckBox cbSells;
        private System.Windows.Forms.GroupBox tradeCategoryGroupBox;
        private System.Windows.Forms.CheckBox cbExchange;
        private System.Windows.Forms.CheckBox cbMargin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAmountMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmountMin;
        private System.Windows.Forms.CheckBox cbSettlement;
        private Common.SelectMarketControl selectMarketControl1;
        private Common.SelectDateRangeControl _selectDateRangeControl1;
        private System.Windows.Forms.RadioButton rbPrice;
        private System.Windows.Forms.RadioButton rbAmount;
        private System.Windows.Forms.CheckBox cbReduce;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtReduce;
    }
}
