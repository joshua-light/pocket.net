using System.Linq;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
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
    }
}