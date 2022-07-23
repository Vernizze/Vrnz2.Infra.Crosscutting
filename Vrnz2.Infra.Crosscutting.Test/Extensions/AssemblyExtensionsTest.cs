using FluentAssertions;
using System.Reflection;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class AssemblyExtensionsTest
    {
        [Fact]
        public void AssemblyExtensionsTest_GetEmbeddedFileFromAssembly_When_GetEmbeddedFileContent_Should_Success()
        {
            // Arrange


            // Act
            var fileContent = Assembly
                .GetAssembly(typeof(AssemblyExtensionsTest))
                .GetEmbeddedFileFromAssembly("EmbeddedFileTest.txt");

            // Assert
            fileContent.Should().Be("Olar");
        }
    }
}
