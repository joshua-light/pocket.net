using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;
using static Xunit.Assert;

namespace Pocket.Tests.Extensions.Collections
{
    public class CollectionExtensionsTest
    {
        [Fact] public void AddRange_ShouldThrow_IfSelfIsNull() =>
            Throws<ArgumentNullException>(() => Null().AddRange(Of(1, 2, 3)));
        [Fact] public void AddRange_ShouldThrow_IfOtherIsNull() =>
            Throws<ArgumentNullException>(() => New().AddRange(Null()));

        [Fact] public void AddRange_ShouldAddAllElementsToCollection() =>
            Add(Of(1, 2, 3), to: New()).ShouldBe(new[] { 1, 2, 3 });

        private static ICollection<int> Null() => null;
        private static ICollection<int> New() => new List<int>();
        private static IEnumerable<int> Of(params int[] args) => args;

        private static ICollection<int> Add(IEnumerable<int> items, ICollection<int> to) =>
            to.Do(x => x.AddRange(items));
    }
}