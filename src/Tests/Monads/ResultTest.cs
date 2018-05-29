using System;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class ResultTest
    {
        public class VoidResult
        {
            public class Success
            {
                private readonly Result _result = Result.Succeded();

                [Fact]
                public void Success_ShouldReturnTrue() => Assert.True(_result.Success);
                
                [Fact]
                public void Fail_ShouldReturnFalse() => Assert.False(_result.Fail);
                
                [Fact]
                public void Error_ShouldReturnEmptyString() => Assert.Equal("", _result.Error);
            }

            public class Fail
            {
                private readonly Result _result = Result.Failed("Test");
                
                [Fact]
                public void Success_ShouldReturnFalse() => Assert.False(_result.Success);
                
                [Fact]
                public void Fail_ShouldReturnTrue() => Assert.True(_result.Fail);
                
                [Fact]
                public void Error_ShouldReturnErrorMessage() => Assert.Equal("Test", _result.Error);
            }

            [Fact]
            public void When_ShouldFail_IfConditionIsFalse() => Assert.True(Result.When(false).Fail);
            [Fact]
            public void When_ShouldSucceed_IfConditionIsTrue() => Assert.True(Result.When(true).Success);

            [Fact]
            public void Of_ShouldFail_IfFuncReturnsNull() => Assert.True(Result.Of<string>(() => null).Fail);
            [Fact]
            public void Of_ShouldSucceed_IfFuncReturnsValue() => Assert.True(Result.Of(() => "").Success);
        }

        public class IntResult
        {
            public class Success
            {
                private readonly Result<int> _result = Result.Succeded(10);
                
                [Fact]
                public void Success_ShouldReturnTrue() => Assert.True(_result.Success);
                
                [Fact]
                public void Fail_ShouldReturnFalse() => Assert.False(_result.Fail);
                
                [Fact]
                public void Value_ShouldReturnCorrectValue() => Assert.Equal(10, _result.Value);
                
                [Fact]
                public void Error_ShouldReturnEmptyString() => Assert.Equal("", _result.Error);
            }
            
            public class Fail
            {
                private readonly Result<int> _result = Result.Failed<int>("Test");
                
                [Fact]
                public void Success_ShouldReturnFalse() => Assert.False(_result.Success);
                
                [Fact]
                public void Fail_ShouldReturnTrue() => Assert.True(_result.Fail);
                
                [Fact]
                public void Error_ShouldReturnErrorMessage() => Assert.Equal("Test", _result.Error);

                [Fact]
                public void Value_ShouldThrowInvalidOperationException() =>
                    Assert.Throws<InvalidOperationException>(() => _result.Value);
            }
        }
    }
}