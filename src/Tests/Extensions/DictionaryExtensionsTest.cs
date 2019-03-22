using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class DictionaryExtensionsTest
    {        
        #region One
        
        [Fact]
        public void OneOrDefault_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).OrDefault().ShouldBe("");
        
        [Fact]
        public void OneOr_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).Or("1").ShouldBe("");
        
        [Fact]
        public void OneOrFunc_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).Or(() => "1").ShouldBe("");
        
        [Fact]
        public void OneOrNew_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).OrNew("1").ShouldBe("");
        
        [Fact]
        public void OneOrNewFunc_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).OrNew(() => "1").ShouldBe("");
        
        [Fact]
        public void OneOrThrow_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).OrThrow().ShouldBe("");
        
        [Fact]
        public void OneOrThrowWithMessage_ShouldReturnObject_IfKeyExists() =>
            Dictionary(with: (1, "")).One(1).OrThrow(withMessage: "").ShouldBe("");

        [Fact]
        public void OneOrDefault_ShouldReturnDefault_IfKeyDoesNotExist() =>
            Empty<int, string>().One(1).OrDefault().ShouldBeNull();

        [Fact]
        public void OneOr_ShouldReturnValue_IfKeyDoesNotExist() =>
            Empty<int, string>().One(1).Or("").ShouldBe("");
        
        [Fact]
        public void OneOrFunc_ShouldReturnValue_IfKeyDoesNotExist() =>
            Empty<int, string>().One(1).Or(() => "").ShouldBe("");
        
        [Fact]
        public void OneOrNew_ShouldWriteNewValue_IfKeyDoesNotExist() =>
            Empty<int, string>()
                .Do(_ => _.One(1).OrNew(""))
                .One(1).OrDefault().ShouldBe("");
        
        [Fact]
        public void OneOrNewFunc_ShouldWriteNewValue_IfKeyDoesNotExist() =>
            Empty<int, string>()
                .Do(_ => _.One(1).OrNew(() => ""))
                .One(1).OrDefault().ShouldBe("");

        [Fact]
        public void OneOrThrow_ShouldThrow_IfKeyDoesNotExist() =>
            Assert.Throws<KeyNotFoundException>(() => Empty<int, string>().One(1).OrThrow())
                .Message.ShouldBe("Couldn't find value with [ 1 ] key.");
        
        [Fact]
        public void OneOrThrow_ShouldThrowWithSpecifiedMessage_IfKeyDoesNotExist() =>
            Assert.Throws<KeyNotFoundException>(() => Empty<int, string>().One(1).OrThrow(withMessage: ""))
                .Message.ShouldBe("");
        
        private static IDictionary<TKey, TValue> Empty<TKey, TValue>() =>
            new Dictionary<TKey, TValue>();
        private static IDictionary<TKey, TValue> Dictionary<TKey, TValue>((TKey Key, TValue Value) with) =>
            new Dictionary<TKey, TValue> { { with.Key, with.Value } };

        #endregion
    }
}