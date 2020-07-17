using System.Linq;
using Shouldly;
using Xunit;

namespace Pocket.Tests.Extensions
{
    public class ListExtensionsTest
    {
        [Theory]
        [InlineData(new string[0], "[]")]
        [InlineData(new[] { "1" }, "[ 1 ]")]
        [InlineData(new[] { "1", "2" }, "[ 1, 2 ]")]
        [InlineData(new[] { "1", "2", "3", "4", "5" }, "[ 1, 2, 3, 4, 5 ]")]
        [InlineData(new[] { "1", null, "3" }, "[ 1, null, 3 ]")]
        public void AsString_ShouldConvertListCorrectly(string[] array, string expected) =>
            array.ToList().AsString().ShouldBe(expected);

//        [Fact]
//        public void Permutations_ShouldReturnAllCombinationsOfItemsInList_If2Items() =>
//            Items("1", "2").Permutations().ShouldBe(new List<IList<string>>
//            {
//                Items("1", "2"),
//                Items("2", "1"),
//            });
//        
//        [Fact]
//        public void Permutations_ShouldReturnAllCombinationsOfItemsInList_If3Items() =>
//            Items("1", "2", "3").Permutations().ShouldBe(new List<IList<string>>
//            {
//                Items("1", "2", "3"),
//                Items("1", "3", "2"),
//                Items("2", "1", "3"),
//                Items("2", "3", "1"),
//                Items("3", "1", "2"),
//                Items("3", "2", "1"),
//            });
//        [Fact]
//        public void Permutations_ShouldReturnAllCombinationsOfItemsInList_If4Items() =>
//            Items("1", "2", "3", "4").Permutations().ShouldBe(new List<IList<string>>
//            {
//                Items("1", "2", "3", "4"),
//                Items("1", "2", "4", "3"),
//                Items("1", "3", "2", "4"),
//                Items("1", "3", "4", "2"),
//                Items("1", "4", "2", "3"),
//                Items("1", "4", "3", "2"),
//                
//                Items("2", "1", "3", "4"),
//                Items("2", "1", "4", "3"),
//                Items("2", "3", "1", "4"),
//                Items("2", "3", "4", "1"),
//                Items("2", "4", "1", "3"),
//                Items("2", "4", "3", "1"),
//                
//                Items("3", "1", "2", "4"),
//                Items("3", "1", "4", "2"),
//                Items("3", "2", "1", "4"),
//                Items("3", "2", "4", "1"),
//                Items("3", "4", "2", "1"),
//                Items("3", "4", "1", "2"),
//                
//                Items("4", "1", "2", "3"),
//                Items("4", "1", "3", "2"),
//                Items("4", "2", "3", "1"),
//                Items("4", "2", "1", "3"),
//                Items("4", "3", "2", "1"),
//                Items("4", "3", "1", "2"),
//            });
//        
//        private static List<T> Items<T>(params T[] items) => new List<T>(items);
    }
}