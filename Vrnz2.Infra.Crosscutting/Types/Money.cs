using System.Globalization;
using System.Threading;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Types
{
    public struct Money
    {
        #region Constants

        private const string PT_BR = "pt-BR";

        #endregion

        #region Atributes

        public readonly string IniValue { get; }
        public readonly decimal? Value { get; }
        public string StringValue { get; private set; }
        public string CurrencyStringValue { get { return $"{CurrencySymbol} {StringValue}"; } }

        public string CurrentLocaleName { get; private set; }
        public static string DecimalSeparator { get; private set; }
        public static string ThousandSeparator { get; private set; }
        public static string CurrencySymbol { get; private set; }

        #endregion

        #region Constructors

        public Money(decimal value) 
            : this(value, PT_BR) { }

        public Money(string value) 
            : this(value, PT_BR) { }

        public Money(decimal value, string localeName)
        : this()
        {
            var cultureInfo = SetupCultureInfo(localeName);

            this.CurrentLocaleName = localeName;
            this.Value = value;

            this.StringValue = string.Format(cultureInfo, "{0:#,0.00}", this.Value);
        }

        public Money(string value, string localeName)
            : this()
        {
            var old_culture = Thread.CurrentThread.CurrentCulture;

            var cultureInfo = SetupCultureInfo(localeName);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            this.CurrentLocaleName = localeName;
            this.IniValue = value;

            if (this.IsValid())
            {
                this.StringValue = ConvertAndFormat(this.IniValue, localeName);

                if (decimal.TryParse(value, out decimal valor))
                    this.Value = valor;
            }

            Thread.CurrentThread.CurrentCulture = old_culture;
            Thread.CurrentThread.CurrentUICulture = old_culture;
        }

        #endregion

        #region Methods

        #region Operator

        public static implicit operator Money(string value)
            => new Money(value);

        public static implicit operator Money(decimal value)
            => new Money(value);

        #endregion

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
                result = ValidateLenght(value);
                result = result && ValidateSeparator(value);
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

        private static string ConvertAndFormat(string value, string localeName = PT_BR)
        {
            var decValue = Convert(value);
            var cultureInfo = SetupCultureInfo(localeName);

            return string.Format(cultureInfo, "{0:#,0.00}", decValue);
        }

        private static CultureInfo SetupCultureInfo(string localeName = PT_BR)
        {
            var cultureInfo = new CultureInfo(localeName);

            DecimalSeparator = cultureInfo.NumberFormat.NumberDecimalSeparator;
            ThousandSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            CurrencySymbol = cultureInfo.NumberFormat.CurrencySymbol;

            return cultureInfo;
        }

        #endregion
    }
}
