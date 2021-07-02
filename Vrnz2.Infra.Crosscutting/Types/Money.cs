using System.Globalization;
using System.Threading;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Types
{
    public struct Money
    {
        #region Variables

        private static CultureInfo _oldCulture;

        #endregion

        #region Atributes

        public string IniValue { get; }
        public decimal? Value { get; }
        public string StringValue { get; private set; }

        public static string DecimalSeparator { get; private set; }
        public static string ThousandSeparator { get; private set; }

        #endregion

        #region Constructors

        public Money(decimal value)
            : this()
        {
            var cultureInfo = SetupCultureInfo();
            var oldCultureName = _oldCulture.Name;

            this.Value = value;

            this.StringValue = string.Format(cultureInfo, "{0:#,0.00}", this.Value);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(oldCultureName);
        }

        public Money(string value)
            : this()
        {
            Thread.CurrentThread.CurrentCulture = SetupCultureInfo();
            var oldCultureName = _oldCulture.Name;

            this.IniValue = value;

            if (this.IsValid())
            {
                this.StringValue = ConvertAndFormat(this.IniValue);

                if (decimal.TryParse(value, out decimal valor))
                    this.Value = valor;
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(oldCultureName);
        }

        #endregion

        #region Operator

        public static implicit operator Money(string value)
            => new Money(value);

        public static implicit operator Money(decimal value)
            => new Money(value);

        #endregion

        #region Methods

        public bool IsEmpty()
            => 0.Equals(this.Value);

        public bool IsNull()
            => this.Value.IsNull();

        public bool IsValid()
            => IsValid(this.IniValue);

        #endregion

        #region Static methods

        public static bool IsValid(string value)
        {
            var result = true;

            if (!string.IsNullOrEmpty(value))
            {
                Thread.CurrentThread.CurrentCulture = SetupCultureInfo();
                var oldCultureName = _oldCulture.Name;

                result = ValidateLenght(value);
                result = result && ValidateSeparator(value);

                Thread.CurrentThread.CurrentCulture = new CultureInfo(oldCultureName);
            }            

            return result;
        }

        private static bool ValidateLenght(string value)
        {
            var result = false;

            result = (value.Length > 3) && (value.Length < 15);

            return result;
        }

        private static bool ValidateSeparator(string value)
        {
            var separadorAtual = value.Substring(value.Length - 3)
                                      .Substring(0, 1);

            return separadorAtual.Equals(DecimalSeparator);
        }

        private static decimal? Convert(string value)
        {
            decimal? result = null;

            if (!string.IsNullOrEmpty(value))
            {
                DecimalSeparator = value.Substring(value.Length - 3).Substring(0, 1);

                var valor_decimal = value.Substring(value.Length - 2);

                var valor_inteiro = value.Substring(0, value.Length - 3);

                valor_decimal = valor_decimal.OnlyNumeric();
                valor_inteiro = valor_inteiro.OnlyNumeric();

                result = decimal.Parse(valor_inteiro) + (decimal.Parse(valor_decimal) / 100);
            }

            return result;
        }

        private static string ConvertAndFormat(string value)
        {
            var decValue = Convert(value);
            var cultureInfo = SetupCultureInfo();

            return string.Format(cultureInfo, "{0:#,0.00}", decValue);
        }

        private static CultureInfo SetupCultureInfo()
        {
            _oldCulture = Thread.CurrentThread.CurrentCulture;

            var cultureInfo = new CultureInfo("en-US");

            DecimalSeparator = ".";
            ThousandSeparator = ",";

            return cultureInfo;
        }

        #endregion
    }
}
