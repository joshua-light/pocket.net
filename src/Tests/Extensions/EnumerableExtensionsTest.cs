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

        #region IsNullOrEmpty

        [Fact]
        public void IsNullOrEmpty_ShouldBeTrue_IfEnumerableIsNull() =>
            ((IEnumerable<int>) null).IsNullOrEmpty().ShouldBeTrue();

        [Fact]
        public void IsNullOrEmpty_ShouldBeTrue_IfEnumerableIsEmpty() =>
            Enumerable.Empty<int>().IsNullOrEmpty().ShouldBeTrue();
        
        [Fact]
        public void IsNullOrEmpty_ShouldBeFalse_IfEnumerableIsNotEmpty() =>
            new [] { 1, 2, 3 }.IsNullOrEmpty().ShouldBeFalse();
        
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

        #region Distinct

        [Fact]
        public void DitinctWithFunc_ShouldFilterItemsAsLinq()
        {
            var items = new[] { 1, 2, 3, 4 };
            
            items.Distinct((x, y) => x == y).ShouldBe(items.Distinct());
        }

        #endregion

        #region One

        [Fact]
        public void One_ShouldReturnItemThatMatchesPredicate_IfIemExists()
        {
            var items = new[] { 1, 2, 3 };
            var item = items.One(x => x == 1);

            item.ShouldBe(1);
        }
        
        [Fact]
        public void One_ShouldReturnDefault_IfItemDoesNotExist()
        {
            var items = new[] { "1", "2", "3" };

            items.One(x => x == "0").ShouldBeNull();
        }
        
        [Fact]
        public void One_ShouldThrowInvalidOperationExceptionWithSpecifiedMessage_IfItemDoesNotExist()
        {
            const string message = "Message";
            var items = new[] { "1", "2", "3" };

            var e = Assert.Throws<InvalidOperationException>(() => items.One(x => x == "0", message));
            
            e.Message.ShouldBe(message);
        }

        #endregion

        #region Second

        [Fact]
        public void Second_ShouldThrowInvalidOperationException_IfEnumerableIsEmpty() =>
            Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Second());
        
        [Fact]
        public void Second_ShouldThrowInvalidOperationException_IfEnumerableHasOneElement() =>
            Assert.Throws<InvalidOperationException>(() => new []{ 1 }.Second());

        [Fact]
        public void Second_ShouldReturnSecondElementOfEnumerable() =>
            new[] { 100, 200, 300 }.Second().ShouldBe(200);

        #endregion
        
        #region PreviousTo
        
        [Fact]
        public void PreviousTo_ShouldReturnNull_IfFirstIsSpecified() =>
            new[] { "1", "2", "3" }.PreviousTo("1").ShouldBe(null);

        [Fact]
        public void PreviousTo_ShouldReturnFirstItem_IfSecondIsSpecified() =>
            new[] { "1", "2", "3" }.PreviousTo("2").ShouldBe("1");
        
        [Fact]
        public void PreviousTo_ShouldReturnSecondItem_IfThirdIsSpecified() =>
            new[] { "1", "2", "3" }.PreviousTo("3").ShouldBe("2");
        
        #endregion

        #region NextTo

        [Fact]
        public void NextTo_ShouldReturnSecondItem_IfFirstIsSpecified() =>
            new[] { "1", "2", "3" }.NextTo("1").ShouldBe("2");
        
        [Fact]
        public void NextTo_ShouldReturnThirdItem_IfSecondIsSpecified() =>
            new[] { "1", "2", "3" }.NextTo("2").ShouldBe("3");
        
        [Fact]
        public void NextTo_ShouldReturnNull_IfThirdIsSpecified() =>
            new[] { "1", "2", "3" }.NextTo("3").ShouldBe(null);

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