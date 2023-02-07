namespace AVS.Trading.Data.Domain.Analytics
{
    public class DataItem : IDataItem
    {
        public double Open { get; set; }
        public double Close { get; set; }
        public double Change => Close - Open;

        public double High { get; set; }
        public double Low { get; set; }
        public double Diff => High - Low;

        public double VolumeBase { get; set; }
        public double VolumeQuote { get; set; }
        //public double WeightedAverage { get; set; }
    }

    /*
    input data 
    Open  [1.52;1.51;1.48;1.49] =>1.52
    Close [1.51;1.58;1.49;1.46] =>1.46     
    Change [..]                 =>-0.06  
    High  [1.525;1.52;1.5;1.495]
    Low   [..]
    Volume [..]

    */
    public interface IDataItem
    {
        double Open { get; }
        double Close { get; }
        double Change { get; }

        double High { get; }
        double Low { get; }
        double Diff { get; }

        double VolumeBase { get; }
        double VolumeQuote { get; }
    }
}