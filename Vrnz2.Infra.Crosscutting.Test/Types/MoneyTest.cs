using Vrnz2.Infra.CrossCutting.Types;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Types
{
    public class MoneyTest
    {
        [Theory]
        [InlineData("10.00", 10)]
        [InlineData("10,000.00", 10000)]
        public void ValidMoney_StringValue(string value, decimal finalValue)
        {
            Money money = value;

            Assert.True(money.IsValid());
            Assert.True(Money.IsValid(value));
            Assert.Equal(money.Value, finalValue);
        }

        [Theory]
        [InlineData(10.00)]
        public void ValidCpf_LongValue(decimal value)
        {
            Money money = value;

            Assert.True(money.IsValid());
            Assert.Equal(money.Value, value);
        }

        [Theory]
        [InlineData(".0")]
        [InlineData("10.")]
        [InlineData("10.0")]
        [InlineData(",0")]
        [InlineData("10,")]
        [InlineData("10,0")]
        [InlineData("10,000")]
        [InlineData("10")]
        [InlineData("10.000,00")]
        [InlineData("10.000,000")]
        public void InvalidMoney_StringValue(string value)
        {
            Money money = value;

            Assert.False(money.IsValid());
            Assert.False(Money.IsValid(value));
        }

        [Theory]
        [InlineData(10)]
        public void InvalidCpf_LongValue(decimal value)
        {
            Money money = value;

            Assert.True(money.IsValid());
        }
    }
}
