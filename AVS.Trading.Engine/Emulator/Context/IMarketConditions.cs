using AVS.Trading.Core.Models;
using AVS.Trading.Pipeline.Models;

namespace AVS.Trading.Pipeline.TradingAlgorithms.Context
{
    public interface IMarketConditions { }
    
    /// <summary>
    /// Тренд - это тенденция движения цены. 
    /// Линия тренда строится по 2 точкам! 
    /// Если строить по трем или более точкам – надежность трендовой линии снижается (вероятен ее пробой).
    /// Не пытайтесь построить линию в любых условиях.
    /// Если не удается её начертить, скорее всего, тренда нет.
    /// Следовательно, данный инструмент не годится к использованию в текущих рыночных условиях.
    /// </summary>
    public interface ITrend : IMarketConditions
    {
        ////1. Тренд характеризуется плавным движением цены
        //double Smoothness { get; set; }

        ////2. Тренд, как правило, начинается с ценового импульса
        //IPriceImpulse Start { get; set; }


        //double[] Max { get; set; }
        //double[] Min { get; set; }

        //int Duration { get; set; }
    }

    public abstract class Trend : ITrend
    {
        ////1. Тренд характеризуется плавным движением цены
        public double Smoothness { get; set; }

        ////2. Тренд, как правило, начинается с ценового импульса
        public IPriceImpulse Start { get; set; }

        public double[] Max { get; set; }
        public double[] Min { get; set; }

        public int Duration { get; set; }

        public Trend Prev { get; set; }
    }

    //нисходящий тренд 
    public class Downtrend : Trend
    {
    }

    //боковик
    public class Flat : Trend
    {
        public PriceRange Range { get; set; }
    }

    //восходящий тренд
    public class Uptrend : Trend
    {
    }


    public interface ITrendVerifier
    {
        //Характеристики тренда
        //1. медленное развитие во времени и пространстве 
        //(то есть относительно плавное движение цены на всем промежутке тренда)
        void ValidateSmoothness();
        //2. Тренд, как правило, начинается с мощного ценового импульса и мощным импульсом заканчивается (но не всегда).
        void ValidateStartImpulse();
        //3. Откаты (движения против основной тенденции) являются незначительными (в % к трендовому движению) 
        //и, как правило, быстро отыгрываются.
        void ValidateKickbacks();
        //4. Для восходящего тренда каждый последующий максимум выше предыдущего, 
        //каждый последующий минимум тоже выше предыдущего. 
        //При этом величина коррекций в процентах против тренда невелика.
        //Для нисходящего тренда максимумы и минимумы соотвественно ниже предыдущих
        //ну это если классика
        void ValidatePeaks(ITrend trend);
    }


    public interface IPriceImpulse { }

    /// <summary>
    /// Откаты (движения против основной тенденции) являются незначительными 
    /// (в % к трендовому движению) и, как правило, быстро отыгрываются
    /// </summary>
    public interface ITrendRollback : IMarketConditions { }



    public interface ILocalTrend : ITrend
    {
        bool CounterTrend { get; set; }
    }


    public class Undefined : ITrend
    {
    }

    public abstract class Rollback : ITrend
    {

    }


    //timeline scale
    //trend/flat -> local trend/local flat ->


    

    public class LocalDowntrend : ITrend
    {
        public bool CounterTrend { get; set; }
    }

    




    public class Drop : ITrend
    {
    }
    public class DropBack : ITrend
    {
    }

    public abstract class Upwards : ITrend
    {

    }

    public abstract class Downwards : ITrend
    {

    }

    //импульс вверх
    public class JumpUp : Upwards
    {
    }

    //импульс вниз
    public class FallDown : Downwards
    {
    }

    //пробой тени свечи вверх
    public class BreakUp : Upwards
    {
    }

    //пробой тени свечи вниз
    public class BreakDown : Downwards
    {
    }
}