using FluentAssertions;
using Vrnz2.Infra.CrossCutting.Utils;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Utils
{
    public class CodeGeneratorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(13)]
        [InlineData(99)]
        [InlineData(100)]
        public void CodeGenerator_ValidStringValue(int size)
        {
            //Arrange


            // Act
            var code = CodeGenerator.Generate(size);

            // Assert
            code.Should().NotBeNullOrWhiteSpace();
            code.Length.Should().Be(size);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CodeGenerator_InvalidStringValue(int size)
        {
            //Arrange


            // Act
            var code = CodeGenerator.Generate(size);

            // Assert
            code.Should().BeNullOrWhiteSpace();
        }
    }
}
