using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Assert.NotNull(item);
        }

        [Fact]
        public void SFirstOrDefault_Null_WithPredicate()
        {
            List<ListTest> list = null;

            var item = list.SFirstOrDefault(p => p.Value == 1);

            Assert.NotNull(item);
        }
    }

    public class ListTest 
    {
        public int Value { get; set; }
    }
}
