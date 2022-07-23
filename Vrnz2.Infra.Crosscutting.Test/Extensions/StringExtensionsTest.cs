using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("2020", 2, "20")]
        [InlineData("20", 2, "20")]
        [InlineData("2", 2, "")]
        [InlineData("", 2, "")]
        [InlineData("2020", -1, "")]
        public void TakeString_ReturnAllFounded_False_StringValue(string value, int tamanho, string resultado)
        {
            var result = value.TakeString(tamanho);

            Assert.Equal(result, resultado);
        }

        [Theory]
        [InlineData("2020", 2, "20")]
        [InlineData("20", 2, "20")]
        [InlineData("2", 2, "2")]
        [InlineData("", 2, "")]
        [InlineData("2020", -1, "")]
        public void TakeString_ReturnAllFounded_True_StringValue(string value, int tamanho, string resultado)
        {
            var result = value.TakeString(tamanho, true);

            Assert.Equal(result, resultado);
        }

        [Theory]
        [InlineData("2020", 1, 2, "02")]
        [InlineData("2020", 2, 2, "20")]
        [InlineData("2020", 3, 2, "")]
        [InlineData("2020", 4, 2, "")]
        [InlineData("20", 1, 2, "")]
        [InlineData("2", 1, 2, "")]
        [InlineData("", 1, 2, "")]
        [InlineData("2020", 1, -1, "")]
        public void TakeString_ReturnAllFounded_False_IniPosition_StringValue(string value, int iniPosition, int tamanho, string resultado)
        {
            var result = value.TakeString(iniPosition, tamanho);

            Assert.Equal(result, resultado);
        }

        [Theory]
        [InlineData("2020", 1, 2, "02")]
        [InlineData("2020", 2, 2, "20")]
        [InlineData("2020", 3, 2, "0")]
        [InlineData("2020", 4, 2, "")]
        [InlineData("20", 1, 2, "0")]
        [InlineData("2", 1, 2, "")]
        [InlineData("", 1, 2, "")]
        [InlineData("2020", 1, -1, "")]
        public void TakeString_ReturnAllFounded_True_IniPosition_StringValue(string value, int iniPosition, int tamanho, string resultado)
        {
            var result = value.TakeString(iniPosition, tamanho, true);

            Assert.Equal(result, resultado);
        }
    }
}
