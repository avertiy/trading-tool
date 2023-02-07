using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class SelectDateRangeControl : UserControl
    {
        public SelectDateRangeControl()
        {
            InitializeComponent();
            dtFrom.Value = DateTime.Today.AddDays(-1);
            dtTo.Value = DateTime.Now;
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            dtFrom.Enabled = cb.Checked;
            dtTo.Enabled = cb.Checked;
        }

        [Browsable(false)]
        public DateRange Range
        {
            get
            {
                if(cb.Checked)
                    return new DateRange(dtFrom.Value, dtTo.Value);
                return null;
            }
            set
            {
                if (value == null)
                {
                    cb.Checked = false;
                }
                else
                {
                    cb.Checked = true;
                    dtFrom.Value = value.From;
                    dtTo.Value = value.To;
                }
            }
        }

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public DateTime? From
        //{
        //    get => cb.Checked ? dtFrom.Value : (DateTime?) null;
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            dtFrom.Value = value.Value;
        //        }
        //        else
        //            cb.Checked = false;
        //    }
        //}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public DateTime? To
        //{
        //    get => cb.Checked ? dtTo.Value : (DateTime?) null;
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            dtTo.Value = value.Value;
        //        }
        //        else
        //            cb.Checked = false;
        //    }
        //}

        private double interval = 1;

        protected void SetPeriod(DateTime? from, DateTime? to)
        {
            if (from.HasValue && to.HasValue)
            {
                cb.Checked = true;
                dtFrom.Value = from.Value;
                dtTo.Value = to.Value;
            }
            else
            {
                cb.Checked = false;
            }
        }

        protected void SetPeriod(double addDaysInterval, DateTime endDate)
        {
            this.interval = addDaysInterval;
            if(addDaysInterval < 0)
                SetPeriod(endDate.AddDays(addDaysInterval), endDate);
        }

        private void lnk24h_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-1, DateTime.Now);
        }

        private void lnk48h_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-2, DateTime.Now);
        }

        private void lnkLast3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-3, DateTime.Now);
        }

        private void lnkLastWeek_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-7, DateTime.Now);
        }

        private void lnkLast2Weeks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-14, DateTime.Now);
        }

        private void lnkLastMonth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-30, DateTime.Now);
        }

        private void lnkQuater_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-91, DateTime.Now);
        }

        private void lnkLastYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetPeriod(-365, DateTime.Now);
        }

        private void lnkPrev_Click(object sender, EventArgs e)
        {
            SetPeriod(interval, dtFrom.Value);
        }

        private void lnkNext_Click(object sender, EventArgs e)
        {
            SetPeriod(interval, dtTo.Value.AddDays(-interval));
        }

        
    }
}
