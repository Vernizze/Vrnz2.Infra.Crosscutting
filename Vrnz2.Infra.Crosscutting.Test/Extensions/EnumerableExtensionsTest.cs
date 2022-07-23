using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void SForEach_Sum()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            list = list.SForEach(s => s.Value = s.Value + 1).ToList();

            Assert.Equal(9, list.Sum(v => v.Value));
        }

        [Fact]
        public void SForEach_Empty()
        {
            var list = new List<ListTest>();

            list = list.SForEach(s => s.Value = s.Value + 1).ToList();

            Assert.Equal(0, list.Sum(v => v.Value));
        }

        [Fact]
        public void SForEach_Null()
        {
            IEnumerable<ListTest> list = null;

            list = list.SForEach(s => s.Value = s.Value + 1);

            Assert.Null(list);
        }

        [Fact]
        public void SFirstOrDefault_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SFirstOrDefault();

            Assert.NotNull(item);
            Assert.Equal(1, item.Value);
        }

        [Fact]
        public void SFirstOrDefault_NotNull_WithPredicate()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SFirstOrDefault(p => p.Value == 1);

            Assert.NotNull(item);
            Assert.Equal(1, item.Value);
        }

        [Fact]
        public void SFirstOrDefault_Null()
        {
            List<ListTest> list = null;

            var item = list.SFirstOrDefault();

            Assert.Null(item);
        }

        [Fact]
        public void SFirstOrDefault_Null_WithPredicate()
        {
            List<ListTest> list = null;

            var item = list.SFirstOrDefault(p => p.Value == 1);

            Assert.Null(item);
        }

        [Fact]
        public void SWhere_NotNull()
        {
            List<ListTest> list = new List<ListTest> { new ListTest { Value = 1 } };

            var item = list.SWhere(p => p.Value == 1);

            item.Should().NotBeNull();
            item.Should().ContainSingle();
        }

        [Fact]
        public void SWhere_Null()
        {
            List<ListTest> list = null;

            var item = list.SWhere(p => p.Value == 1);

            item.Should().NotBeNull();
            item.Should().BeEmpty();
        }

        [Fact]
        public void SToList_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SToList();

            Assert.NotNull(item);
            Assert.Equal(list, item);
        }

        [Fact]
        public void SToList_Null()
        {
            List<ListTest> list = null;

            var item = list.SToList();

            Assert.Null(item);
        }

        [Fact]
        public void SGroupBy_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 1 },
                new ListTest { Value = 3 }
            };

            var grouped = list.SGroupBy(g => g.Value);

            Assert.NotNull(grouped);
            Assert.Equal(2, grouped.Count());
            Assert.Equal(2, grouped.SFirstOrDefault().Count());
        }

        [Fact]
        public void SGroupBy_Null()
        {
            List<ListTest> list = null;

            var item = list.SGroupBy(g => g.Value);

            Assert.Null(item);
        }

        [Fact]
        public void SRemove_NotNull()
        {
            var value01 = new ListTest { Value = 1 };

            var list = new List<ListTest>
            {
                value01,
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SRemove(value01);

            Assert.True(item);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void SRemove_Null()
        {
            var value01 = new ListTest { Value = 1 };
            List<ListTest> list = null;

            var item = list.SRemove(value01);

            Assert.False(item);
        }

        [Fact]
        public void SRemove_NotNull_ParamNull()
        {
            ListTest value01 = null;

            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SRemove(value01);

            Assert.False(item);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void SExcept_NotNull()
        {
            var value01 = new ListTest { Value = 1 };

            var list = new List<ListTest>
            {
                value01,
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var removeList = new List<ListTest> { value01 };

            var item = list.SExcept(removeList);

            Assert.NotNull(item);
            Assert.Equal(2, item.Count());
        }

        [Fact]
        public void SExcept_Null()
        {
            var value01 = new ListTest { Value = 1 };
            List<ListTest> list = null;

            var removeList = new List<ListTest> { value01 };

            var item = list.SExcept(removeList);

            Assert.Null(item);
        }

        [Fact]
        public void SExcept_NotNull_ParamNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            List<ListTest> removeList = null;

            var item = list.SExcept(removeList);

            Assert.NotNull(item);
            Assert.Equal(3, item.Count());
        }

        [Fact]
        public void SOrderBy_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SOrderBy(o => o.Value);

            Assert.NotNull(item);
            Assert.Equal(1, item.SFirstOrDefault().Value);
            Assert.Equal(3, item.LastOrDefault().Value);
        }

        [Fact]
        public void SOrderBy_Null()
        {
            List<ListTest> list = null;

            var item = list.SOrderBy(o => o.Value);

            Assert.Null(item);
        }

        [Fact]
        public void SOrderByDescending_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var item = list.SOrderByDescending(o => o.Value);

            Assert.NotNull(item);
            Assert.Equal(3, item.SFirstOrDefault().Value);
            Assert.Equal(1, item.LastOrDefault().Value);
        }

        [Fact]
        public void SOrderByDescending_Null()
        {
            List<ListTest> list = null;

            var item = list.SOrderByDescending(o => o.Value);

            Assert.Null(item);
        }

        [Fact]
        public void SClear_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            list.SClear();

            Assert.Empty(list);
        }

        [Fact]
        public void SClear_Null()
        {
            List<ListTest> list = null;

            list.SClear();

            Assert.Null(list);
        }

        [Fact]
        public void SCount_NotNull()
        {
            var list = new List<ListTest>
            {
                new ListTest { Value = 1 },
                new ListTest { Value = 2 },
                new ListTest { Value = 3 }
            };

            var result = list.SCount();

            Assert.Equal(list.Count, result);
        }

        [Fact]
        public void SCount_Null()
        {
            List<ListTest> list = null;

            var result = list.SCount();

            Assert.Equal(0, result);
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNotNullAndValueIsNotNull_Should_Be_SuccessAdded()
        {
            // Arrange
            var value = new ListTest { Value = 1 };

            // Act
            var list = new List<ListTest>().AddIfNotNull(value);

            // Assert
            list.Single().Value.Should().Be(value.Value);
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNotNullAndValueIsNotNullAndCheckClauseIsFalse_Should_Be_DontAddedAndDontThrowException()
        {
            // Arrange
            var value = new ListTest { Value = 1 };

            // Act
            var list = new List<ListTest>().AddIfNotNull(value, () => value.Value > 1);

            // Assert
            list.Should().BeEmpty();
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNotNullAndValueIsNotNullAndCheckClauseIsTrue_Should_Be_SuccessAdded()
        {
            // Arrange
            var value = new ListTest { Value = 1 };

            // Act
            var list = new List<ListTest>().AddIfNotNull(value, () => value.Value > 0);

            // Assert
            list.Single().Value.Should().Be(value.Value);
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNullAndValueIsNotNull_Should_Be_DontThrowException()
        {
            // Arrange
            List<ListTest> list = null;
            var value = new ListTest { Value = 1 };

            // Act
            var result = list.AddIfNotNull(value);

            // Assert
            list.Should().BeNull();
            result.Should().BeNull();
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNotNullAndValueIsNull_Should_Be_DontAddedAndDontThrowException()
        {
            // Arrange
            ListTest value = null;

            // Act
            var list = new List<ListTest>().AddIfNotNull(value);

            // Assert
            list.Should().BeEmpty();
        }

        [Fact]
        public void AddIfNotNull_When_ListIsNullAndValueIsNull_Should_Be_DontThrowException()
        {
            // Arrange
            List<ListTest> list = null;
            ListTest value = null;

            // Act
            var result = list.AddIfNotNull(value);

            // Assert
            list.Should().BeNull();
            result.Should().BeNull();
        }
    }

    public class ListTest
    {
        public int Value { get; set; }
    }
}
