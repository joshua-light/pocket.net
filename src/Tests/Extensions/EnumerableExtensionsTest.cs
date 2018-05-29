using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void OrEmpty_ShouldReturnEmptyEnumerable_IfSelfIsNull() =>
            Assert.Same(Enumerable.Empty<string>(), ((IEnumerable<string>) null).OrEmpty());

        [Fact]
        public void OrEmpty_ShouldReturnSelf_IfSelfIsNotNull()
        {
            var enumerable = Enumerable.Range(0, 5);
            Assert.Same(enumerable, enumerable.OrEmpty());
        }

        public class TakeMin
        {
            [Fact]
            public void TakeMin_ShouldThrowException() =>
                Assert.Throws<ArgumentNullException>(() => ((IEnumerable<Item>) null).TakeMin(x => x.Number));
            
            [Fact]
            public void TakeMin_ShouldThrowException_IfFuncIsNull() =>
                Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<int>().TakeMin<int, Item>(null));

            [Fact]
            public void TakeMin_ShouldReturnMinObject()
            {
                var items = new List<Item>(Enumerable.Range(0, 10).Select(x => new Item(x)));

                var minValue = items.Min(x => x.Number);
                var min = items.TakeMin(x => x.Number);

                Assert.Equal(minValue, min.Number);
            }

            [Fact]
            public void TakeMin_ShouldReturnMinObject_IfIComparable()
            {
                var items = new List<Item>(Enumerable.Range(0, 10).Select(x => new Item(x)));

                var a = items.Min(x => x);
                var b = items.TakeMin(x => x);

                Assert.Same(a, b);
            }

            [Fact]
            public void TakeMin_ShouldReturnCorrectReference()
            {
                var secondItem = new Item(2);
                var items = new List<Item>
                {
                    new Item(3),
                    new Item(2),
                    secondItem
                };

                Assert.NotSame(secondItem, items.TakeMin(x => x.Number));
            }
        }

        public class TakeMax
        {
            [Fact]
            public void TakeMax_ShouldThrowException() =>
                Assert.Throws<ArgumentNullException>(() => ((IEnumerable<Item>) null).TakeMax(x => x.Number));
            
            [Fact]
            public void TakeMax_ShouldThrowException_IfFuncIsNull() =>
                Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<int>().TakeMax<int, Item>(null));

            [Fact]
            public void TakeMax_ShouldReturnMaxObject()
            {
                var items = new List<Item>(Enumerable.Range(0, 10).Select(x => new Item(x)));

                var minValue = items.Max(x => x.Number);
                var min = items.TakeMax(x => x.Number);

                Assert.Equal(minValue, min.Number);
            }

            [Fact]
            public void TakeMax_ShouldReturnMaxObject_IfIComparable()
            {
                var items = new List<Item>(Enumerable.Range(0, 10).Select(x => new Item(x)));

                var a = items.Max(x => x);
                var b = items.TakeMax(x => x);

                Assert.Same(a, b);
            }

            [Fact]
            public void TakeMin_ShouldReturnCorrectReference()
            {
                var secondItem = new Item(3);
                var items = new List<Item>
                {
                    new Item(3),
                    new Item(2),
                    secondItem
                };

                Assert.NotSame(secondItem, items.TakeMax(x => x.Number));
            }
        }

        #region Inner Classes

        private class Item : IComparable<Item>
        {
            public readonly int Number;

            public Item(int number)
            {
                Number = number;
            }

            public int CompareTo(Item other)
            {
                if (ReferenceEquals(this, other))
                    return 0;
                if (ReferenceEquals(null, other))
                    return 1;

                return Number.CompareTo(other.Number);
            }
        }

        #endregion
    }
}