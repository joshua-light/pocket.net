using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pocket.Common.Tests.Extensions.Guards
{
    public class GuardEnumerableExtensionsTest
    {
        #region EnsureEmpty

        [Fact]
        public void EnsureEmpty_ShouldThrowArgumentException_IfEnumerableIsNotEmpty() =>
            Assert.Throws<ArgumentException>(() => Enumerable.Range(0, 1).EnsureEmpty());
        
        [Fact]
        public void EnsureEmpty_ShouldThrowArgumentException_IfArrayIsNotEmpty() =>
            Assert.Throws<ArgumentException>(() => "1".EnsureEmpty());
        
        [Fact]
        public void EnsureEmpty_ShouldThrowArgumentException_IfLinkedListIsNotEmpty() =>
            Assert.Throws<ArgumentException>(() => new LinkedList<int>(new []{ 1 }).EnsureEmpty());

        [Fact]
        public void EnsureEmpty_ShouldNotThrow_IfEnumerableIsEmpty() =>
            Enumerable.Empty<int>().EnsureEmpty();
        
        [Fact]
        public void EnsureEmpty_ShouldNotThrow_IfArrayIsEmpty() =>
            "".EnsureEmpty();
        
        [Fact]
        public void EnsureEmpty_ShouldNotThrow_IfLinkedListIsEmpty() =>
            new LinkedList<int>().EnsureEmpty();

        #endregion

        #region EnsureNotEmpty

        [Fact]
        public void EnsureNotEmpty_ShouldThrowArgumentException_IfEnumerableIsEmpty() =>
            Assert.Throws<ArgumentException>(() => Enumerable.Empty<int>().EnsureNotEmpty());
        
        [Fact]
        public void EnsureNotEmpty_ShouldThrowArgumentException_IfArrayIsEmpty() =>
            Assert.Throws<ArgumentException>(() => "".EnsureNotEmpty());
        
        [Fact]
        public void EnsureNotEmpty_ShouldThrowArgumentException_IfLinkedListIsEmpty() =>
            Assert.Throws<ArgumentException>(() => new LinkedList<int>().EnsureNotEmpty());

        [Fact]
        public void EnsureNotEmpty_ShouldNotThrow_IfEnumerableIsNotEmpty() =>
            Enumerable.Range(0, 1).EnsureNotEmpty();
        
        [Fact]
        public void EnsureNotEmpty_ShouldNotThrow_IfArrayIsNotEmpty() =>
            "1".EnsureNotEmpty();
        
        [Fact]
        public void EnsureNotEmpty_ShouldNotThrow_IfLinkedListIsNotEmpty() =>
            new LinkedList<int>(new []{ 1 }).EnsureNotEmpty();

        #endregion
    }
}