using FluentAssertions;
using System;
using System.ComponentModel;
using System.Linq;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class EnumExtensionsTests
    {
        public enum TOperation
        {
            Undefined = 0,

            [Description("Transação")]
            Transacao = 1,
            [Description("Aprovação")]
            Aprovacao = 2,
        }

        public enum SOperation
        {
            Undefined = 0,

            [Description("Criado")]
            Criado = 1,
            [Description("Executando")]
            Executando = 2,
            [Description("Finalizado")]
            Finalizado = 3,
        }

        [Theory]
        [InlineData(TOperation.Undefined)]
        [InlineData(TOperation.Transacao)]
        [InlineData(TOperation.Aprovacao)]
        public void ParseExact_When_PassingValidEnumItem_Should_Be_Success(TOperation operation)
        {
            //Arrange

            //Act
            TOperation res = operation.ParseExact(operation.GetHashCode().ToString());

            //Assert
            res.Should().Be(operation);
        }

        [Theory]
        [InlineData(TOperation.Undefined)]
        [InlineData(TOperation.Transacao)]
        [InlineData(TOperation.Aprovacao)]
        public void ParseExact_When_PassingValidEnumItemWithDefaultValue_Should_Be_Success(TOperation operation)
        {
            //Arrange

            //Act
            TOperation res = operation.ParseExact("99", operation);

            //Assert
            res.Should().Be(operation);
        }

        [Fact]
        public void ParseExact_When_PassingValidEnumItemWithoutDefaultValue_Should_Be_Success()
        {
            //Arrange
            TOperation operation = TOperation.Transacao;

            //Act
            TOperation res = operation.ParseExact("99");

            //Assert
            res.Should().Be(TOperation.Undefined);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndNullStringValueAndNoDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = null;

            //Act
            var res = operation.ParseExact(null);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValiStringValueAndNoDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;

            //Act
            var res = operation.ParseExact("2");

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValidStringValueAndNullDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;

            //Act
            var res = operation.ParseExact("2", null);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidStringValueAndNullDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;

            //Act
            var res = operation.ParseExact("999", null);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidStringValueAndValidDefaultValue_Should_Be_ValidDefaultValue()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;

            //Act
            var res = operation.ParseExact("999", TOperation.Transacao);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Transacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndNullNumericValueAndNoDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = null;
            int? value = null;

            //Act
            var res = operation.ParseExact(value);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValiNumericValueAndNoDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;
            int value = 2;

            //Act
            var res = operation.ParseExact(value);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValidNumericValueAndNullDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;
            int value = 2;

            //Act
            var res = operation.ParseExact(value, null);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidNumericValueAndNullDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            int value = 999;

            //Act
            var res = operation.ParseExact(value, null);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidNumericValueAndValidDefaultValue_Should_Be_ValidDefaultValue()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            int value = 999;

            //Act
            var res = operation.ParseExact(value, TOperation.Transacao);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Transacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndNullEnumValueAndNoDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = null;
            SOperation? value = null;

            //Act
            var res = operation.ParseExact(value);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValiEnumValueAndNoDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;
            SOperation value = SOperation.Executando;

            //Act
            var res = operation.ParseExact(value);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndValidEnumValueAndNullDefaultValue_Should_Be_ValidEnumValue()
        {
            //Arrange
            TOperation? operation = null;
            SOperation value = SOperation.Executando;

            //Act
            var res = operation.ParseExact(value, null);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Aprovacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidEnumValueAndNullDefaultValue_Should_Be_NullResult()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            SOperation value = SOperation.Finalizado;

            //Act
            var res = operation.ParseExact(value, null);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndInvalidEnumValueAndValidDefaultValue_Should_Be_ValidDefaultValue()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            SOperation value = SOperation.Finalizado;

            //Act
            var res = operation.ParseExact(value, TOperation.Transacao);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Transacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndNonNumericOrEnumValueAndValidDefaultValue_Should_Be_ValidDefaultValue2()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            DateTime value = DateTime.Now;

            //Act
            var res = operation.ParseExact(value, TOperation.Transacao);

            //Assert
            res.Should().NotBeNull();
            res.Should().Be(TOperation.Transacao);
        }

        [Fact]
        public void ParseExact_When_PassingNullEnumAndNonNumericOrEnumValueAndValidDefaultValue_Should_Be_ValidDefaultValue3()
        {
            //Arrange
            TOperation? operation = TOperation.Undefined;
            DateTime value = DateTime.Now;

            //Act
            var res = operation.ParseExact(value, null);

            //Assert
            res.Should().BeNull();
        }

        [Fact]
        public void ToList_When_GetEnumElements_Should_Be_Success()
        {
            //Arrange
            TOperation operation = TOperation.Transacao;

            //Act
            var elements = operation.ToList();

            //Assert
            elements.Should().NotBeNull();
            elements.Should().HaveCount(3);
            elements[0].Should().Be(TOperation.Undefined);
            elements[1].Should().Be(TOperation.Transacao);
            elements[2].Should().Be(TOperation.Aprovacao);
        }
    }
}
