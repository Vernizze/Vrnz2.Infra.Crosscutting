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

        public static DateTime AddWorkingDays(this DateTime date, double value)
        {
            var daysUntilFriday = (int)DayOfWeek.Friday - (int)date.DayOfWeek;

            if (daysUntilFriday > 0 && value > daysUntilFriday)
                date = AddWorkingDays(date.AddDays(daysUntilFriday + 2), value - daysUntilFriday);
            else
                date = date.AddDays(value > 0 ? value : 0);

            return date;
        }

        public static int DiffInYears(this DateTime baseDate, DateTime compareDate)
            => new DateTime(Math.Abs(compareDate.Date.Subtract(baseDate.Date).Ticks)).Year - 1;

        public static (int Year, int Month, int Day, int Hours, int Minutes, int Seconds, int Milliseconds) DateTimeDeconstructor(this DateTime value)
            => (value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);

        public static (int Hours, int Minutes, int Seconds, int Milliseconds) TimeDeconstructor(this DateTime value)
            => (value.Hour, value.Minute, value.Second, value.Millisecond);
    }
}
