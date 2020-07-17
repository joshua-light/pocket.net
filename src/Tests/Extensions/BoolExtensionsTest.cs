using Xunit;

namespace Pocket.Tests.Extensions
{
    public class LogicBoolExtensionsTest
    {
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void And_ShouldRepresentCorrectTruthTable(bool a, bool b, bool expected) =>
            Assert.Equal(expected, a.And(b));
        
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void Or_ShouldRepresentCorrectTruthTable(bool a, bool b, bool expected) =>
            Assert.Equal(expected, a.Or(b));
        
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, true)]
        [InlineData(false, false, true)]
        public void Implication_ShouldRepresentCorrectTruthTable(bool a, bool b, bool expected) =>
            Assert.Equal(expected, a.Implication(b));
    }
}