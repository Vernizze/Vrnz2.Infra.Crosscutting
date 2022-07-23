using FluentAssertions;
using System;
using Vrnz2.Infra.CrossCutting.Types;
using Xunit;

namespace Vrnz2.Infra.CrossCutting.Test.Types
{
    public class CpfTest
    {
        [Theory]
        [InlineData("308.929.540-78")]
        [InlineData("30892954078")]
        [InlineData("954.044.850-60")]
        [InlineData("95404485060")]
        [InlineData("768.792.680-30")]
        [InlineData("76879268030")]
        [InlineData("870.737.390-29")]
        [InlineData("87073739029")]
        [InlineData("209.180.810-50")]
        [InlineData("20918081050")]
        [InlineData("257.371.680-50")]
        [InlineData("25737168050")]
        [InlineData("975.956.030-56")]
        [InlineData("97595603056")]
        public void ValidCpf_StringValue(string value)
        {
            //Arrange
            Cpf cpf = value;

            // Act

            // Assert
            cpf.IsValid().Should().BeTrue();
        }

        [Theory]
        [InlineData(30892954078)]
        [InlineData(95404485060)]
        [InlineData(76879268030)]
        [InlineData(87073739029)]
        [InlineData(20918081050)]
        [InlineData(25737168050)]
        [InlineData(97595603056)]
        public void ValidCpf_LongValue(long value)
        {
            //Arrange
            Cpf cpf = value;

            // Act

            // Assert
            cpf.IsValid().Should().BeTrue();
        }

        [Theory]
        [InlineData("308.929.541-78")]
        [InlineData("30892954178")]
        [InlineData("954.044.851-60")]
        [InlineData("95404485160")]
        [InlineData("768.792.681-30")]
        [InlineData("76879268130")]
        [InlineData("870.737.391-29")]
        [InlineData("87073739129")]
        [InlineData("209.180.811-50")]
        [InlineData("20918081150")]
        [InlineData("257.371.681-50")]
        [InlineData("25737168150")]
        [InlineData("975.956.031-56")]
        [InlineData("97595603156")]
        [InlineData(null)]
        [InlineData("")]
        public void InvalidCpf_StringValue(string value)
        {
            //Arrange
            Cpf cpf = value;

            // Act

            // Assert
            cpf.IsValid().Should().BeFalse();
        }

        [Theory]
        [InlineData(30892954178)]
        [InlineData(95404485160)]
        [InlineData(76879268130)]
        [InlineData(87073739129)]
        [InlineData(20918081150)]
        [InlineData(25737168150)]
        [InlineData(97595603156)]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidCpf_LongValue(long value)
        {
            //Arrange
            Cpf cpf = value;

            // Act

            // Assert
            cpf.IsValid().Should().BeFalse();
        }
    }
}