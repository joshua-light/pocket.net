using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class EnumerableExtensionsTest
    {
        #region OrEmpty

        [Fact]
        public void OrEmpty_ShouldReturnEmptyEnumerable_IfSelfIsNull() =>
            ((IEnumerable<string>) null).OrEmpty().ShouldBeEmpty();

        [Fact]
        public void OrEmpty_ShouldReturnSelf_IfSelfIsNotNull()
        {
            var enumerable = Enumerable.Range(0, 5);
            enumerable.ShouldBeSameAs(enumerable.OrEmpty());
        }

        #endregion
        
        #region Each

        [Fact]
        public void Each_ShouldThrow_IfSelfIsNull()
        {
            IEnumerable<int> numbers = null;
            Should.Throw<ArgumentNullException>(() => numbers.Each(x => { }));
        }

        [Fact]
        public void Each_ShouldInvokeActionOnElements_IfEnumerableIsCollapsed()
        {
            var sideNumbers = new List<int>();

            var numbers = Enumerable
                .Range(0, 10)
                .Each(x => sideNumbers.Add(x))
                .ToList();

            Assert.Equal(numbers, sideNumbers);
        }

        [Fact]
        public void Each_ShouldInvokeActionOnElements_IfEnumerableIsCollapsedAndFiltered()
        {
            var sideNumbers = new List<int>();

            var numbers = Enumerable
                .Range(0, 10)
                .Where(x => x > 5)
                .Each(x => sideNumbers.Add(x))
                .ToList();

            Assert.Equal(numbers, sideNumbers);
        }

        [Fact]
        public void Each_ShouldInvokeActionOnElements_IfEnumerableIsCollapsedButFilteredLater()
        {
            var sideNumbers = new List<int>();

            var numbers = Enumerable
                .Range(0, 10)
                .Each(x => sideNumbers.Add(x))
                .Where(x => x > 5)
                .ToList();

            Assert.NotEqual(numbers, sideNumbers);
            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, sideNumbers);
        }
        
        [Fact]
        public void Each_ShouldNotInvokeActionOnElements_IfEnumerableIsNotCollapsed()
        {
            var sideNumbers = new List<int>();

            Enumerable
                .Range(0, 10)
                .Each(x => sideNumbers.Add(x))
                .Where(x => x > 5);

            Assert.Empty(sideNumbers);
        }

        #endregion

        #region ForEach
        
        [Fact]
        public void ForEach_ShouldThrow_IfSelfIsNull()
        {
            IEnumerable<int> numbers = null;
            Assert.Throws<ArgumentNullException>(() => numbers.ForEach(x => { }));
        }

        [Fact]
        public void ForEach_ShouldInvokeActionOnElements()
        {
            var sideNumbers = new List<int>();
            
            Enumerable
                .Range(0, 10)
                .ForEach(x => sideNumbers.Add(x));

            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, sideNumbers);
        }

        [Fact]
        public void ForEach_ShouldInvokeActionOnElements_IfEnumerableIsFiltered()
        {
            var sideNumbers = new List<int>();

            Enumerable
                .Range(0, 10)
                .Where(x => x > 5)
                .ForEach(x => sideNumbers.Add(x));

            Assert.Equal(new[] { 6, 7, 8, 9 }, sideNumbers);
        }

        #endregion

        #region TakeMin

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

        #endregion

        #region TakeMax

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
        public void TakeMax_ShouldReturnCorrectReference()
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

        #endregion

        #region IsEmpty

        [Fact]
        public void IsEmpty_ShouldBeTrue_IfEnumerableIsEmpty() =>
            Enumerable.Empty<int>().IsEmpty().ShouldBeTrue();
        
        [Fact]
        public void IsEmpty_ShouldBeTrue_IfArrayIsEmpty() =>
            "".IsEmpty().ShouldBeTrue();
        
        [Fact]
        public void IsEmpty_ShouldBeTrue_IfLinkedListIsEmpty() =>
            new LinkedList<int>().IsEmpty().ShouldBeTrue();
        
        [Fact]
        public void IsEmpty_ShouldBeFalse_IfEnumerableIsNotEmpty() =>
            Enumerable.Range(0, 1).IsEmpty().ShouldBeFalse();
        
        [Fact]
        public void IsEmpty_ShouldBeFalse_IfArrayIsNotEmpty() =>
            "1".IsEmpty().ShouldBeFalse();
        
        [Fact]
        public void IsEmpty_ShouldBeFalse_IfLinkedListIsNotEmpty() =>
            new LinkedList<int>(new []{ 1 }).IsEmpty().ShouldBeFalse();

        #endregion

        #region Except

        [Fact]
        public void ExceptWithFunc_ShouldExcludeItemsUsingFunc()
        {
            var a = Enumerable.Range(0, 10);
            var b = Enumerable.Range(5, 10);

            a.Except(b, (x, y) => x == y).ShouldBe(Enumerable.Range(0, 5));
        }

        #endregion

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