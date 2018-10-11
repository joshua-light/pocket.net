using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class DictionaryExtensionsTest
    {
        #region GetOrNew
        
        [Fact]
        public void GetOrNew_ShouldReturnSameObject_IfCalledTwice()
        {
            var dictionary = new Dictionary<int, List<int>>();
                
            var list = dictionary.GetOrNew(1, () => new List<int>());
            var otherList = dictionary.GetOrNew(1, () => new List<int>());

            list.ShouldBeSameAs(otherList);
        }
        
        #endregion
        
        #region GetOrDefault

        [Fact]
        public void GetOrDefault_ShouldReturnObject_IfKeyExists()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.GetOrDefault(1);

            list.ShouldBeSameAs(otherList);
        }

        [Fact]
        public void GetOrDefault_ShouldReturnDefault_IfKeyDoesNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            
            dictionary.GetOrDefault(1).ShouldBeNull();
            dictionary.GetOrDefault(2).ShouldBeNull();
            dictionary.GetOrDefault(3).ShouldBeNull();
        }
        
        #endregion

        #region GetOrThrow

        [Fact]
        public void GetOrThrow_ShouldReturnObject_IfKeyExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.GetOrThrow(1);

            list.ShouldBeSameAs(otherList);
        }

        [Fact]
        public void GetOrThrow_ShouldThrowKeyNotFoundExceptionWithCorrectMessage_IfKeyDoesNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();

            var e = Assert.Throws<KeyNotFoundException>(() => dictionary.GetOrThrow(0));
            
            e.Message.ShouldBe("Couldn't find value by [ 0 ] key.");
        }

        #endregion
    }
}