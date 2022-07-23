using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class ObjectExtensionsTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Diff_When_PassinNotEqualObjects_Should_DifferencesBetweenThem()
        {
            // Arrange
            var type01 = new DiffTestStub
            {
                Code = _fixture.Create<string>(),
                Value = _fixture.Create<int>(),
                Items = new List<DiffItemTestStub>
                {
                    new DiffItemTestStub
                    {
                        Value = _fixture.Create<decimal>(),
                        SubItems = new List<DiffSubItemTestStub>
                        {
                            new DiffSubItemTestStub
                            {
                                Date = _fixture.Create<DateTime>(),
                                Values =_fixture.Create<bool?[]>()
                            }
                        }
                    }
                }
            };

            var type02 = new DiffTestStub
            {
                Code = _fixture.Create<string>(),
                Value = null,
                Items = new List<DiffItemTestStub>
                {
                    new DiffItemTestStub
                    {
                        Value = null,
                        SubItems = new List<DiffSubItemTestStub>
                        {
                            new DiffSubItemTestStub
                            {
                                Date = null,
                                Values = new bool?[] { null }
                            }
                        }
                    }
                }
            };

            // Act
            var diff = ObjectExtensions.Diff(type01, type02);

            // Assert
            diff.Should().NotBeNull();
        }
    }

    public class DiffTestStub
    {
        public string Code { get; set; }
        public int? Value { get; set; }
        public List<DiffItemTestStub> Items { get; set; }
    }

    public class DiffItemTestStub
    {
        public decimal? Value { get; set; }
        public List<DiffSubItemTestStub> SubItems { get; set; }
    }

    public class DiffSubItemTestStub
    {
        public DateTime? Date { get; set; }
        public bool?[] Values { get; set; }
    }
}
