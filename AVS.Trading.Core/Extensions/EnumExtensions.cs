using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Extensions
{
    public static class EnumExtensions
    {
        public static bool CheckFlag(this TradeCategory category, TradeCategory flag)
        {
            return (category & flag) == flag;
        }
    }
}