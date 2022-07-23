using System;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Types
{
    public struct Time
    {
        #region Variables

        private readonly DateTime _dateTime;

        #endregion

        #region Atributes

        public string OriginalValue { get; private set; }
        public int Hour => _dateTime.Hour;
        public int Minute => _dateTime.Minute;
        public int Second => _dateTime.Second;
        public int Millisecond => _dateTime.Millisecond;
        public static Time Now => new Time(DateTime.Now.ToString("HH:mm:ss:fff"));
        public static Time UtcNow => new Time(DateTime.UtcNow.ToString("HH:mm:ss:fff"));

        #endregion

        #region Constructors

        public Time(string value)
            : this()
        {
            OriginalValue = value;

            if (IsValid(value))
                _dateTime = Parse(value);
        }

        #endregion

        #region Operators

        public static implicit operator Time(string value)
            => new Time(value);

        public static implicit operator Time((int Hours, int Minutes, int Seconds, int Milliseconds) value)
            => new Time(string.Concat(value.Hours.ToString("0#"), ":", value.Minutes.ToString("0#"), ":", value.Seconds.ToString("0#"), ".", value.Milliseconds.ToString("00#")));

        public static implicit operator Time((int Hours, int Minutes, int Seconds) value)
            => new Time(string.Concat(value.Hours.ToString("0#"), ":", value.Minutes.ToString("0#"), ":", value.Seconds.ToString("0#"), ".000"));

        public static implicit operator Time((int Hours, int Minutes) value)
            => new Time(string.Concat(value.Hours.ToString("0#"), ":", value.Minutes.ToString("0#"), ":00.000"));

        public static implicit operator Time(int hours)
            => new Time(string.Concat(hours.ToString("0#"), ":00:00.000"));

        #endregion

        #region Methods

        public Time AddHours(double value)
        {
            _dateTime.AddHours(value);

            return this;
        }

        public Time AddMinutes(double value)
        {
            _dateTime.AddMinutes(value);

            return this;
        }

        public Time AddSeconds(double value)
        {
            _dateTime.AddSeconds(value);

            return this;
        }

        public Time AddMilliseconds(double value)
        {
            _dateTime.AddMilliseconds(value);

            return this;
        }

        public override string ToString()
            => _dateTime.ToString("HH:mm:ss:fff");

        public DateTime ToDateTime()
            => new DateTime(1, 1, 1, _dateTime.Hour, _dateTime.Minute, _dateTime.Second, _dateTime.Millisecond);

        //Static
        public static bool IsValid(string value)
        {
            var result = !string.IsNullOrWhiteSpace(value);

            if (!result)
                return false;

            result = value.Length > 7 && value.Length < 13;

            if (!result)
                return false;

            var timeParts = value.Split(':', '.');

            result = (timeParts.Length > 2 && timeParts.Length < 5);

            if (!result)
                return false;

            for (int i = 0; i < timeParts.Length; i++)
                result &= timeParts[i].IsNumeric();

            if (!result)
                return false;

            result = DateTime.TryParse(string.Concat("0001-01-01T", value), out DateTime _);

            return result;
        }

        public static bool TryParse(string value, out Time result)
        {
            if (DateTime.TryParse(string.Concat("0001-01-01T", value), out DateTime dateTimeResult))
            {
                result = new Time(dateTimeResult.ToString("HH:mm:ss:fff"));

                return true;
            }

            result = new Time(DateTime.MinValue.ToString("HH:mm:ss:fff"));

            return false;
        }

        public static Time Parse(DateTime value)
            => new Time(value.ToString("HH:mm:ss:fff"));

        public static DateTime Parse(Time value)
        {
            if (DateTime.TryParse(string.Concat("0001-01-01T", value.ToString()), out DateTime result))
                return result;

            throw new FormatException($"Invalid Time format! Value => {string.Concat("0001-01-01T", value.IsNull() ? "NULL" : value.ToString())}");
        }

        public static DateTime Parse(string value)
        {
            if (DateTime.TryParse(string.Concat("0001-01-01T", value), out DateTime result))
                return result;

            throw new FormatException($"Invalid Time format! Value => {string.Concat("0001-01-01T", value)}");
        }

        #endregion
    }
}
