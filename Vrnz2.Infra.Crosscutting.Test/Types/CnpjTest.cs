using Vrnz2.Infra.CrossCutting.Types;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Types
{
    public class CnpjTest
    {
        [Theory]
        [InlineData("26.070.359/0001-29")]
        [InlineData("26070359000129")]
        [InlineData("85.980.128/0001-11")]
        [InlineData("85980128000111")]
        [InlineData("89.788.418/0001-19")]
        [InlineData("89788418000119")]
        [InlineData("74.654.751/0001-68")]
        [InlineData("74654751000168")]
        [InlineData("45.135.978/0001-07")]
        [InlineData("45135978000107")]
        [InlineData("89.385.770/0001-03")]
        [InlineData("89385770000103")]
        public void ValidCnpj_StringValue(string value)
        {
            Cnpj cnpj = value;

            Assert.True(cnpj.IsValid());
        }

        [Theory]
        [InlineData(26070359000129)]
        [InlineData(85980128000111)]
        [InlineData(89788418000119)]
        [InlineData(74654751000168)]
        [InlineData(45135978000107)]
        [InlineData(89385770000103)]
        public void ValidCnpj_LongValue(long value)
        {
            Cnpj cnpj = value;

            Assert.True(cnpj.IsValid());
        }

        [Theory]
        [InlineData("308.929.541-79")]
        [InlineData("30892954179")]
        [InlineData("954.044.851-69")]
        [InlineData("95404485169")]
        [InlineData("768.792.681-39")]
        [InlineData("76879268139")]
        [InlineData("870.737.391-28")]
        [InlineData("87073739128")]
        [InlineData("209.180.811-58")]
        [InlineData("20918081158")]
        [InlineData("257.371.681-58")]
        [InlineData("25737168158")]
        [InlineData("975.956.031-58")]
        [InlineData("97595603158")]
        [InlineData(null)]
        [InlineData("")]
        public void InvalidCnpj_StringValue(string value)
        {
            Cnpj cnpj = value;

            Assert.False(cnpj.IsValid());
        }

        [Theory]
        [InlineData(30892954179)]
        [InlineData(95404485169)]
        [InlineData(76879268139)]
        [InlineData(87073739128)]
        [InlineData(20918081158)]
        [InlineData(25737168158)]
        [InlineData(97595603158)]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidCnpj_LongValue(long value)
        {
            Cnpj cnpj = value;

            Assert.False(cnpj.IsValid());
        }
    }
}
