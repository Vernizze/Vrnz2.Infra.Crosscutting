using FluentAssertions;
using System;
using Vrnz2.Infra.CrossCutting.Types;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Types
{
    public class TimeTests
    {
        [Fact]
        public void Time_When_PassingValidHoursAndMinutesAndSecondsFromDateTimeNowValues_Should_Be_Success()
        {
            //Arrange
            var now = DateTime.Now;

            // Act
            Time time = string.Concat(now.Hour.ToString("0#"), ":", now.Minute.ToString("0#"), ":", now.Second.ToString("0#"));

            // Assert
            time.Hour.Should().Be(now.Hour);
            time.Minute.Should().Be(now.Minute);
            time.Second.Should().Be(now.Second);
            time.Millisecond.Should().Be(0);
        }

        [Fact]
        public void Time_When_PassingValidHoursAndMinutesAndSecondsFromDateTimeUtcNowValues_Should_Be_Success()
        {
            //Arrange
            var now = DateTime.UtcNow;

            // Act
            Time time = string.Concat(now.Hour.ToString("0#"), ":", now.Minute.ToString("0#"), ":", now.Second.ToString("0#"));

            // Assert
            time.Hour.Should().Be(now.Hour);
            time.Minute.Should().Be(now.Minute);
            time.Second.Should().Be(now.Second);
            time.Millisecond.Should().Be(0);
        }

        [Theory]
        [InlineData("12", "15", "27")]
        public void Time_When_PassingValidHoursAndMinutesAndSecondsValues_Should_Be_Success(string hours, string minutes, string seconds)
        {
            //Arrange


            // Act
            Time time = string.Concat(hours, ":", minutes, ":", seconds);

            // Assert
            time.Hour.Should().Be(Convert.ToInt32(hours));
            time.Minute.Should().Be(Convert.ToInt32(minutes));
            time.Second.Should().Be(Convert.ToInt32(seconds));
            time.Millisecond.Should().Be(0);
        }

        [Theory]
        [InlineData("12", "15", "27", "123")]
        public void Time_When_PassingValidHoursAndMinutesAndSecondsAndMillisecondsValues_Should_Be_Success(string hours, string minutes, string seconds, string milliseconds)
        {
            //Arrange


            // Act
            Time time = string.Concat(hours, ":", minutes, ":", seconds, ".", milliseconds);

            // Assert
            time.Hour.Should().Be(Convert.ToInt32(hours));
            time.Minute.Should().Be(Convert.ToInt32(minutes));
            time.Second.Should().Be(Convert.ToInt32(seconds));
            time.Millisecond.Should().Be(Convert.ToInt32(milliseconds));
        }

        [Fact]
        public void ToDateTime_When_PassingValidHoursAndMinutesAndSecondsAndMillisecondsValues_Should_Be_Success()
        {
            //Arrange
            var now = new DateTime(1, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

            // Act
            Time time = (now.Hour, now.Minute, now.Second, now.Millisecond);

            // Assert
            time.ToDateTime().Should().Be(now);
        }

        [Fact]
        public void ToDateTime_When_PassingValidHoursAndMinutesAndSecondsValues_Should_Be_Success()
        {
            //Arrange
            var now = new DateTime(1, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

            // Act
            Time time = (now.Hour, now.Minute, now.Second);

            // Assert
            time.ToDateTime().Should().Be(now);
        }

        [Fact]
        public void ToDateTime_When_PassingValidHoursAndMinutesValues_Should_Be_Success()
        {
            //Arrange
            var now = new DateTime(1, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0, 0);

            // Act
            Time time = (now.Hour, now.Minute);

            // Assert
            time.ToDateTime().Should().Be(now);
        }

        [Fact]
        public void ToDateTime_When_PassingValidHoursValue_Should_Be_Success()
        {
            //Arrange
            var now = new DateTime(1, 1, 1, DateTime.Now.Hour, 0, 0, 0);

            // Act
            Time time = now.Hour;

            // Assert
            time.ToDateTime().Should().Be(now);
        }
    }
}
