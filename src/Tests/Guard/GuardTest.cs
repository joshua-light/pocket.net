using System;
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
        
        private static string Null() => null;
        private static string NotNull() => "";
        
        private class Parent { }
        private class Child : Parent { }
    }
}