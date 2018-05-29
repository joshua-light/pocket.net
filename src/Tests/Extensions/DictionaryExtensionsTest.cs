using System.Collections.Generic;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class DictionaryExtensionsTest
    {
        [Fact]
        public void GetOrNew_ShouldReturnSameObject_IfCalledTwice()
        {
            var dictionary = new Dictionary<int, List<int>>();
                
            var list = dictionary.GetOrNew(1, () => new List<int>());
            var otherList = dictionary.GetOrNew(1, () => new List<int>());

            Assert.Same(list, otherList);
        }

        [Fact]
        public void GetOrDefault_ShouldReturnObject_IfKeyIsExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.GetOrDefault(1);

            Assert.Same(list, otherList);
        }

        [Fact]
        public void GetOrDefault_ShouldReturnDefault_IfKeyIsNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            
            Assert.Null(dictionary.GetOrDefault(1));
            Assert.Null(dictionary.GetOrDefault(2));
            Assert.Null(dictionary.GetOrDefault(3));
        }
    }
}