using FluentAssertions;
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
            //Arrange
            Money money = value;

            // Act

            // Assert
            money.IsValid().Should().BeTrue();
            Money.IsValid(value).Should().BeTrue();
            money.Value.Should().Be(finalValue);
        }

        [Theory]
        [InlineData(10.00)]
        public void ValidMoney_LongValue(decimal value)
        {
            //Arrange
            Money money = value;

            // Act

            // Assert
            money.IsValid().Should().BeTrue();
            money.Value.Should().Be(value);
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
            //Arrange
            Money money = value;

            // Act

            // Assert
            money.IsValid().Should().BeFalse();
            Money.IsValid(value).Should().BeFalse();
        }
    }
}
