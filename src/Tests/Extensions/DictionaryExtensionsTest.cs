using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class DictionaryExtensionsTest
    {        
        #region One

        [Fact]
        public void One_ShouldReturnObject_IfKeyExists()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.One(1);

            list.ShouldBeSameAs(otherList);
        }

        [Fact]
        public void One_ShouldReturnDefault_IfKeyDoesNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            
            dictionary.One(1).ShouldBeNull();
            dictionary.One(2).ShouldBeNull();
            dictionary.One(3).ShouldBeNull();
        }
        
        [Fact]
        public void OneWithFunc_ShouldReturnSameObject_IfCalledTwice()
        {
            var dictionary = new Dictionary<int, List<int>>();
                
            var list = dictionary.One(1, or: () => new List<int>());
            var otherList = dictionary.One(1, or: () => new List<int>());

            list.ShouldBeSameAs(otherList);
        }
        
        #endregion

        #region OneOrThrow

        [Fact]
        public void OneOrThrow_ShouldReturnObject_IfKeyExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.OneOrThrow(1);

            list.ShouldBeSameAs(otherList);
        }

        [Fact]
        public void OneOrThrow_ShouldThrowKeyNotFoundExceptionWithCorrectMessage_IfKeyDoesNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();

            var e = Assert.Throws<KeyNotFoundException>(() => dictionary.OneOrThrow(0));
            
            e.Message.ShouldBe("Couldn't find value by [ 0 ] key.");
        }

        #endregion
    }
}