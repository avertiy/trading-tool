using System;
using System.Collections.Generic;
using System.Linq;

namespace AVS.Trading.Data.Domain.Analytics
{
    //public abstract class DataArray
    //{
    //    protected abstract IDataItem[] Values { get; }
    //public double[] OpenArr => Values.Select(v=>v.Open).ToArray();
    //public double[] CloseArr => Values.Select(v=>v.Close).ToArray();
    //public double[] ChangeArr => Values.Select(v=>v.Change).ToArray();
    //public double[] HighArr => Values.Select(v=>v.High).ToArray();
    //public double[] LowArr => Values.Select(v=>v.Low).ToArray();
    //public double[] DiffArr => Values.Select(v=>v.Diff).ToArray();
    //public double[] VolumeBaseArr => Values.Select(v=>v.VolumeBase).ToArray();
    //}
    /*
    public abstract class DataRow: IDataItem
    {
        protected abstract IDataItem[] Values { get; }
        
        #region IDataItem
        public double Open
        {
            get => Values[0].Open;
        }
        public double Close
        {
            get => Values.Last().Close;
        }
        public double Change => Close - Open;
        public double High => Values.Select(v => v.High).Max();
        public double Low => Values.Select(v => v.Low).Min();
        public double Diff => High - Low;
        public double VolumeBase => Values.Select(v => v.VolumeBase).Sum();
        public double VolumeQuote => Values.Select(v => v.VolumeQuote).Sum();
        #endregion
    }

    public class M5 : DataItem
    {

    }

    public class Quater: DataRow
    {
        public M5 M1 { get; set; }
        public M5 M2 { get; set; }
        public M5 M3 { get; set; }
        protected override IDataItem[] Values => new IDataItem[]{M1,M2,M3};
    }


    public class Hour : DataRow
    {
        public HalfHour HH1 { get; set; }
        public HalfHour HH2 { get; set; }
        protected override IDataItem[] Values => new IDataItem[] { HH1, HH2 };
        
    }

    */
    
}
