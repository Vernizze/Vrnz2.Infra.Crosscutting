using FluentAssertions;
using System.ComponentModel;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public enum DataAnnotationEnumTest
    {
        [Description("Value #01")]
        Value01 = 1,
        [Description("Value #02")]
        Value02 = 2
    }

    [Description("Value #01")]
    public class DataAnnotationClassTest
    {
        [Description("Attribute #01")]
        public string Name { get; set; }
    }

    public class DataAnnotationsExtensionsTests
    {
        [Theory]
        [InlineData(DataAnnotationEnumTest.Value01, "Value #01")]
        [InlineData(DataAnnotationEnumTest.Value02, "Value #02")]
        public void DataAnnotationsExtensions_DescriptionFromEnum_When_GetDescription_Should_Success(DataAnnotationEnumTest value, string message)
        {
            // Arrange

            // Act
            var valueDescription = value.Description();

            // Assert
            valueDescription.Should().Be(message);
        }

        [Fact]
        public void DataAnnotationsExtensions_DescriptionFromType_When_GetTypeDescription_Should_Success()
        {
            // Arrange
            var type = typeof(DataAnnotationClassTest);

            // Act
            var valueDescription = type.Description();

            // Assert
            valueDescription.Should().Be("Value #01");
        }

        [Fact]
        public void DataAnnotationsExtensions_DescriptionFromType_When_GetAttributeDescription_Should_Success()
        {
            // Arrange
            var type = typeof(DataAnnotationClassTest);

            // Act
            var attributeValueDescription = type.AttributeDescription("Name");

            // Assert
            attributeValueDescription.Should().Be("Attribute #01");
        }
    }
}
