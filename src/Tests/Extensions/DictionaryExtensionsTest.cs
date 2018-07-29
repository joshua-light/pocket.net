using System.Collections.Generic;
using Shouldly;
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

            list.ShouldBe(otherList);
        }

        [Fact]
        public void GetOrDefault_ShouldReturnObject_IfKeyIsExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            var list = new List<int>();
            dictionary[1] = list;

            var otherList = dictionary.GetOrDefault(1);

            list.ShouldBe(otherList);
        }

        [Fact]
        public void GetOrDefault_ShouldReturnDefault_IfKeyIsNotExist()
        {
            var dictionary = new Dictionary<int, List<int>>();
            
            dictionary.GetOrDefault(1).ShouldBeNull();
            dictionary.GetOrDefault(2).ShouldBeNull();
            dictionary.GetOrDefault(3).ShouldBeNull();
        }
    }
}