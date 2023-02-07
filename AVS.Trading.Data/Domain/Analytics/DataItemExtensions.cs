namespace AVS.Trading.Data.Domain.Analytics
{
    public static class DataItemExtensions
    {
        public static double ChangePercentage(this IDataItem item) => item.Change / item.Open*100;
        public static double DiffPercentage(this IDataItem item) => item.Diff / item.Low * 100;
    }
}