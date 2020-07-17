using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class MappedFlowTest
    {
        private readonly IFlow<int> _source = Substitute.For<IFlow<int>>();

        public MappedFlowTest()
        {
            _source.Current.Returns(10);

            _source
                .When(x => x.OnNext(Arg.Any<Action<int>>()))
                .Do(x => x.ArgAt<Action<int>>(0).Invoke(15));
        }
        
        [Fact]
        public void Current_ShouldReturnConvertedValue()
        {
            var flow = new MappedFlow<int, string>(_source, x => x.ToString() + "d");

            Assert.Equal("10d", flow.Current);
        }

        [Fact]
        public void OnNext_ShouldFireWithConvertedValue()
        {
            var flow = new MappedFlow<int, string>(_source, x => x.ToString() + "d");

            flow.OnNext(x => Assert.Equal("15d", x));
        }
        
        [Fact]
        public void Constructor_ShouldThrowGuardException_IfSourceIsNull() =>
            Assert.Throws<ArgumentNullException>(
                (Action) (() => new MappedFlow<int, string>(null, x => x.ToString() + "d")));
        
        [Fact]
        public void Constructor_ShouldThrowGuardException_IfFuncIsNull() =>
            Assert.Throws<ArgumentNullException>(
                (Action) (() => new MappedFlow<int, string>(_source, null)));

        [Fact]
        public void OnNext_ShouldThrowGuardException_IfActionIsNull() =>
            Assert.Throws<ArgumentNullException>(
                () => new MappedFlow<int, string>(_source, x => x.ToString() + "d").OnNext(null));
    }
}