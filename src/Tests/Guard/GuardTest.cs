using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;
using static Pocket.Common.Guard;
using static Pocket.Common.Tests.Core.Static;

namespace Pocket.Common.Tests.Guard
{
    public class GuardTest
    {
        [Fact]
        public void EnsureNotNull_ShouldThrow_IfValueIsNull() =>
            Call(() => Ensure(Null()).NotNull()).ShouldThrow(typeof(ArgumentNullException));
        [Fact]
        public void EnsureNotNull_ShouldNotThrow_IfValueIsNotNull() =>
            Call(() => Ensure(NotNull()).NotNull()).ShouldNotThrow();

        [Fact]
        public void EnsureNull_ShouldThrow_IfValueIsNotNull() =>
            Call(() => Ensure(NotNull()).Null()).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureNull_ShouldNotThrow_IfValueIsNull() =>
            Call(() => Ensure(Null()).Null()).ShouldNotThrow();
        
        [Fact]
        public void EnsureTrue_ShouldThrow_IfValueIsFalse() =>
            Call(() => Ensure(false).True()).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureTrue_ShouldNotThrow_IfValueIsTrue() =>
            Call(() => Ensure(true).True()).ShouldNotThrow();
        
        [Fact]
        public void EnsureFalse_ShouldThrow_IfValueIsTrue() =>
            Call(() => Ensure(true).False()).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureFalse_ShouldNotThrow_IfValueIsFalse() =>
            Call(() => Ensure(false).False()).ShouldNotThrow();

        [Fact]
        public void EnsureIsParent_ShouldThrow_IfValueIsChild() =>
            Call(() => Ensure(typeof(Child)).Is(typeof(Parent))).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureIsChild_ShouldNotThrow_IfValueIsChild() =>
            Call(() => Ensure(typeof(Child)).Is(typeof(Child))).ShouldNotThrow();

        [Fact]
        public void EnsureDerivesParent_ShouldThrow_IfValueIsParent() =>
            Call(() => Ensure(typeof(Parent)).Derives(typeof(Parent))).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureDerivesParent_ShouldThrow_IfValueIsString() =>
            Call(() => Ensure(typeof(string)).Derives(typeof(Parent))).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureDerivesParent_ShouldNotThrow_IfValueIsChild() =>
            Call(() => Ensure(typeof(Child)).Derives(typeof(Parent))).ShouldNotThrow();
        
        [Fact]
        public void EnsureIsOrDerivesParent_ShouldThrow_IfValueIsString() =>
            Call(() => Ensure(typeof(string)).IsOrDerives(typeof(Parent))).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureIsOrDerivesParent_ShouldNotThrow_IfValueIsParent() =>
            Call(() => Ensure(typeof(Parent)).IsOrDerives(typeof(Parent))).ShouldNotThrow();
        [Fact]
        public void EnsureIsOrDerivesParent_ShouldNotThrow_IfValueIsChild() =>
            Call(() => Ensure(typeof(Child)).IsOrDerives(typeof(Parent))).ShouldNotThrow();

        [Fact]
        public void EnsureIs0_ShouldThrow_IfValueIs1() =>
            Call(() => Ensure(1).Is(0)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureIs0_ShouldNotThrow_IfValueIs0() =>
            Call(() => Ensure(0).Is(0)).ShouldNotThrow();
        
        [Fact]
        public void EnsureIsNot0_ShouldThrow_IfValueIs0() =>
            Call(() => Ensure(0).IsNot(0)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureIsNot0_ShouldNotThrow_IfValueIs1() =>
            Call(() => Ensure(1).IsNot(0)).ShouldNotThrow();

        [Fact]
        public void EnsureLessThan1_ShouldThrow_IfValueIs1() =>
            Call(() => Ensure(1).Less(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureLessThan1_ShouldThrow_IfValueIs2() =>
            Call(() => Ensure(2).Less(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureLessThan1_ShouldNotThrow_IfValueIs0() =>
            Call(() => Ensure(0).Less(1)).ShouldNotThrow();
        
        [Fact]
        public void EnsureLessOrEqualTo1_ShouldThrow_IfValueIs2() =>
            Call(() => Ensure(2).LessOrEqual(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureLessOrEqualTo1_ShouldNotThrow_IfValueIs1() =>
            Call(() => Ensure(1).LessOrEqual(1)).ShouldNotThrow();
        [Fact]
        public void EnsureLessOrEqualTo1_ShouldNotThrow_IfValueIs0() =>
            Call(() => Ensure(0).LessOrEqual(1)).ShouldNotThrow();
        
        [Fact]
        public void EnsureGreaterThan1_ShouldThrow_IfValueIs1() =>
            Call(() => Ensure(1).Greater(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureGreaterThan1_ShouldThrow_IfValueIs0() =>
            Call(() => Ensure(0).Greater(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureGreaterThan1_ShouldNotThrow_IfValueIs2() =>
            Call(() => Ensure(2).Greater(1)).ShouldNotThrow();
        
        [Fact]
        public void EnsureGreaterOrEqualTo1_ShouldThrow_IfValueIs0() =>
            Call(() => Ensure(0).GreaterOrEqual(1)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureGreaterOrEqualTo1_ShouldNotThrow_IfValueIs1() =>
            Call(() => Ensure(1).GreaterOrEqual(1)).ShouldNotThrow();
        [Fact]
        public void EnsureGreaterOrEqualTo1_ShouldNotThrow_IfValueIs2() =>
            Call(() => Ensure(2).GreaterOrEqual(1)).ShouldNotThrow();

        [Fact]
        public void EnsureInRangeFrom1To10_ShouldThrow_IfValueIs0() =>
            Call(() => Ensure(0).InRange(1, 10)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureInRangeFrom1To10_ShouldThrow_IfValueIs11() =>
            Call(() => Ensure(11).InRange(1, 10)).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureInRangeFrom1To10_ShouldNotThrow_IfValueIs1() =>
            Call(() => Ensure(1).InRange(1, 10)).ShouldNotThrow();
        [Fact]
        public void EnsureInRangeFrom1To10_ShouldNotThrow_IfValueIs5() =>
            Call(() => Ensure(5).InRange(1, 10)).ShouldNotThrow();
        [Fact]
        public void EnsureInRangeFrom1To10_ShouldNotThrow_IfValueIs10() =>
            Call(() => Ensure(10).InRange(1, 10)).ShouldNotThrow();

        [Fact]
        public void EnsureEmpty_ShouldThrow_IfValueIsNotEmpty() =>
            Call(() => Ensure(NotEmpty()).Empty()).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureEmpty_ShouldNotThrow_IfValueIsEmpty() =>
            Call(() => Ensure(Empty()).Empty()).ShouldNotThrow();

        [Fact]
        public void EnsureNotEmpty_ShouldThrow_IfValueIsEmpty() =>
            Call(() => Ensure(Empty()).NotEmpty()).ShouldThrow(typeof(ArgumentException));
        [Fact]
        public void EnsureNotEmpty_ShouldNotThrow_IfValueIsNotEmpty() =>
            Call(() => Ensure(NotEmpty()).NotEmpty()).ShouldNotThrow();
        
        private static string Null() => null;
        private static string NotNull() => "";

        private static IEnumerable<int> Empty() => Enumerable.Empty<int>();
        private static IEnumerable<int> NotEmpty() => new [] { 1 };
        
        private class Parent { }
        private class Child : Parent { }
    }
}