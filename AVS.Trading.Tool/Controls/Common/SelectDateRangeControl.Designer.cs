namespace AVS.Trading.Tool.Controls.Common
{
    partial class SelectDateRangeControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkLast2Weeks = new System.Windows.Forms.LinkLabel();
            this.lnk24h = new System.Windows.Forms.LinkLabel();
            this.lnkPrev = new System.Windows.Forms.LinkLabel();
            this.lnkNext = new System.Windows.Forms.LinkLabel();
            this.lnkLastWeek = new System.Windows.Forms.LinkLabel();
            this.lnkQuater = new System.Windows.Forms.LinkLabel();
            this.lnkLast3Days = new System.Windows.Forms.LinkLabel();
            this.lnkLastMonth = new System.Windows.Forms.LinkLabel();
            this.lnkLastYear = new System.Windows.Forms.LinkLabel();
            this.cb = new System.Windows.Forms.CheckBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lnk48h = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkLast2Weeks);
            this.groupBox1.Controls.Add(this.lnk24h);
            this.groupBox1.Controls.Add(this.lnkPrev);
            this.groupBox1.Controls.Add(this.lnkNext);
            this.groupBox1.Controls.Add(this.lnkLastWeek);
            this.groupBox1.Controls.Add(this.lnkQuater);
            this.groupBox1.Controls.Add(this.lnkLast3Days);
            this.groupBox1.Controls.Add(this.lnkLastMonth);
            this.groupBox1.Controls.Add(this.lnkLastYear);
            this.groupBox1.Controls.Add(this.cb);
            this.groupBox1.Controls.Add(this.dtFrom);
            this.groupBox1.Controls.Add(this.dtTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lnk48h);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 70);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period";
            // 
            // lnkLast2Weeks
            // 
            this.lnkLast2Weeks.AutoSize = true;
            this.lnkLast2Weeks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLast2Weeks.Location = new System.Drawing.Point(157, 49);
            this.lnkLast2Weeks.Name = "lnkLast2Weeks";
            this.lnkLast2Weeks.Size = new System.Drawing.Size(61, 16);
            this.lnkLast2Weeks.TabIndex = 46;
            this.lnkLast2Weeks.TabStop = true;
            this.lnkLast2Weeks.Text = "2 Weeks";
            this.lnkLast2Weeks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLast2Weeks_LinkClicked);
            // 
            // lnk24h
            // 
            this.lnk24h.AutoSize = true;
            this.lnk24h.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnk24h.Location = new System.Drawing.Point(6, 49);
            this.lnk24h.Name = "lnk24h";
            this.lnk24h.Size = new System.Drawing.Size(29, 16);
            this.lnk24h.TabIndex = 45;
            this.lnk24h.TabStop = true;
            this.lnk24h.Text = "24h";
            this.lnk24h.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk24h_LinkClicked);
            // 
            // lnkPrev
            // 
            this.lnkPrev.AutoSize = true;
            this.lnkPrev.Location = new System.Drawing.Point(23, 22);
            this.lnkPrev.Name = "lnkPrev";
            this.lnkPrev.Size = new System.Drawing.Size(27, 20);
            this.lnkPrev.TabIndex = 44;
            this.lnkPrev.TabStop = true;
            this.lnkPrev.Text = "<<";
            this.lnkPrev.Click += new System.EventHandler(this.lnkPrev_Click);
            // 
            // lnkNext
            // 
            this.lnkNext.AutoSize = true;
            this.lnkNext.Location = new System.Drawing.Point(334, 23);
            this.lnkNext.Name = "lnkNext";
            this.lnkNext.Size = new System.Drawing.Size(27, 20);
            this.lnkNext.TabIndex = 43;
            this.lnkNext.TabStop = true;
            this.lnkNext.Text = ">>";
            this.lnkNext.Click += new System.EventHandler(this.lnkNext_Click);
            // 
            // lnkLastWeek
            // 
            this.lnkLastWeek.AutoSize = true;
            this.lnkLastWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLastWeek.Location = new System.Drawing.Point(111, 49);
            this.lnkLastWeek.Name = "lnkLastWeek";
            this.lnkLastWeek.Size = new System.Drawing.Size(44, 16);
            this.lnkLastWeek.TabIndex = 37;
            this.lnkLastWeek.TabStop = true;
            this.lnkLastWeek.Text = "Week";
            this.lnkLastWeek.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLastWeek_LinkClicked);
            // 
            // lnkQuater
            // 
            this.lnkQuater.AutoSize = true;
            this.lnkQuater.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkQuater.Location = new System.Drawing.Point(274, 49);
            this.lnkQuater.Name = "lnkQuater";
            this.lnkQuater.Size = new System.Drawing.Size(48, 16);
            this.lnkQuater.TabIndex = 41;
            this.lnkQuater.TabStop = true;
            this.lnkQuater.Text = "Quater";
            this.lnkQuater.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuater_LinkClicked);
            // 
            // lnkLast3Days
            // 
            this.lnkLast3Days.AutoSize = true;
            this.lnkLast3Days.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLast3Days.Location = new System.Drawing.Point(76, 49);
            this.lnkLast3Days.Name = "lnkLast3Days";
            this.lnkLast3Days.Size = new System.Drawing.Size(29, 16);
            this.lnkLast3Days.TabIndex = 39;
            this.lnkLast3Days.TabStop = true;
            this.lnkLast3Days.Text = "72h";
            this.lnkLast3Days.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLast3Days_LinkClicked);
            // 
            // lnkLastMonth
            // 
            this.lnkLastMonth.AutoSize = true;
            this.lnkLastMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLastMonth.Location = new System.Drawing.Point(224, 49);
            this.lnkLastMonth.Name = "lnkLastMonth";
            this.lnkLastMonth.Size = new System.Drawing.Size(44, 16);
            this.lnkLastMonth.TabIndex = 38;
            this.lnkLastMonth.TabStop = true;
            this.lnkLastMonth.Text = "Month";
            this.lnkLastMonth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLastMonth_LinkClicked);
            // 
            // lnkLastYear
            // 
            this.lnkLastYear.AutoSize = true;
            this.lnkLastYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLastYear.Location = new System.Drawing.Point(321, 49);
            this.lnkLastYear.Name = "lnkLastYear";
            this.lnkLastYear.Size = new System.Drawing.Size(37, 16);
            this.lnkLastYear.TabIndex = 36;
            this.lnkLastYear.TabStop = true;
            this.lnkLastYear.Text = "Year";
            this.lnkLastYear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLastYear_LinkClicked);
            // 
            // cb
            // 
            this.cb.AutoSize = true;
            this.cb.Location = new System.Drawing.Point(6, 25);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(15, 14);
            this.cb.TabIndex = 35;
            this.cb.UseVisualStyleBackColor = true;
            this.cb.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // dtFrom
            // 
            this.dtFrom.Enabled = false;
            this.dtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(56, 20);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(122, 26);
            this.dtFrom.TabIndex = 15;
            this.dtFrom.Value = new System.DateTime(2018, 4, 29, 0, 0, 0, 0);
            // 
            // dtTo
            // 
            this.dtTo.Enabled = false;
            this.dtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(206, 20);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(122, 26);
            this.dtTo.TabIndex = 18;
            this.dtTo.Value = new System.DateTime(2018, 4, 29, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(185, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "-";
            // 
            // lnk48h
            // 
            this.lnk48h.AutoSize = true;
            this.lnk48h.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnk48h.Location = new System.Drawing.Point(41, 49);
            this.lnk48h.Name = "lnk48h";
            this.lnk48h.Size = new System.Drawing.Size(29, 16);
            this.lnk48h.TabIndex = 42;
            this.lnk48h.TabStop = true;
            this.lnk48h.Text = "48h";
            this.lnk48h.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk48h_LinkClicked);
            // 
            // SelectPeriodControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectDateRangeControl";
            this.Size = new System.Drawing.Size(366, 70);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkLast3Days;
        private System.Windows.Forms.LinkLabel lnkLastMonth;
        private System.Windows.Forms.LinkLabel lnkLastWeek;
        private System.Windows.Forms.LinkLabel lnkLastYear;
        private System.Windows.Forms.LinkLabel lnkQuater;
        private System.Windows.Forms.LinkLabel lnk48h;
        private System.Windows.Forms.LinkLabel lnk24h;
        private System.Windows.Forms.LinkLabel lnkPrev;
        private System.Windows.Forms.LinkLabel lnkNext;
        private System.Windows.Forms.LinkLabel lnkLast2Weeks;
    }
}
