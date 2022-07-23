using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Types
{
    public struct Cnpj
    {
        #region Atributes

        public string FormatedValue { get; }
        public string OriginalValue { get; }
        public long NumericValue { get; }
        public string Value { get; }

        #endregion

        #region Constructors

        public Cnpj(long value)
            : this()
        {
            this.OriginalValue = value.ToString();

            if (this.IsValid())
            {
                this.Value = this.OriginalValue;

                this.FormatedValue = long.Parse(this.Value).ToString(@"00\.000\.000\\0000-00");

                this.NumericValue = value;
            }
        }

        public Cnpj(string value)
            : this()
        {
            this.OriginalValue = value;

            if (this.IsValid())
            {
                this.Value = this.OriginalValue.OnlyNumeric();

                this.FormatedValue = long.Parse(this.Value).ToString(@"00\.000\.000\\0000-00");

                this.NumericValue = long.Parse(this.Value);
            }
        }

        #endregion

        #region Operators

        public static implicit operator Cnpj(string value)
            => new Cnpj(value);

        public static implicit operator Cnpj(long value)
            => new Cnpj(value);

        #endregion

        #region Methods

        public bool IsEmpty()
            => 0.Equals(this.Value);

        public bool IsNull()
            => string.IsNullOrEmpty(this.Value);
        public bool IsValid()
            => IsValid(this.OriginalValue);

        #endregion

        #region Static methods

        public static bool IsValid(string value)
            => string.IsNullOrWhiteSpace(value) ? false : Validation(value);

        private static bool Validation(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        #endregion
    }
}
