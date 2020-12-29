using System;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value) 
            => new DateTime(value.Year, value.Month, 1);

        public static DateTime LastDayOfMonth(this DateTime value)
            => FirstDayOfMonth(value).AddMonths(1).AddDays(-1);

        public static DateTime? FirstDayOfMonth(this string value)
        {
            DateTime? result = null;

            if (value.IsDate())
                result = FirstDayOfMonth(DateTime.Parse(value));

            return result;
        }

        public static DateTime? LastDayOfMonth(this string value)
        {
            DateTime? result = null;

            if (value.IsDate())
                result = LastDayOfMonth(DateTime.Parse(value));

            return result;
        }

        public static int DiffInYears(this DateTime baseDate, DateTime compareDate)
            => new DateTime(Math.Abs(compareDate.Date.Subtract(baseDate.Date).Ticks)).Year - 1;
    }
}
