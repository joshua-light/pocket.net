using System;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class ResultTest
    {
        public class VoidResult
        {
            public class Success
            {
                private readonly Result _result = Result.Ok();

                [Fact]
                public void Success_ShouldReturnTrue() =>
                    Assert.True(_result.IsOk);
                
                [Fact]
                public void Fail_ShouldReturnFalse() =>
                    Assert.False(_result.IsFail);
                
                [Fact]
                public void Error_ShouldReturnEmptyString() =>
                    Assert.Equal("", _result.Error);
            }

            public class Fail
            {
                private readonly Result _result = Result.Fail("Test");
                
                [Fact]
                public void Success_ShouldReturnFalse() =>
                    Assert.False(_result.IsOk);
                
                [Fact]
                public void Fail_ShouldReturnTrue() =>
                    Assert.True(_result.IsFail);
                
                [Fact]
                public void Error_ShouldReturnErrorMessage() =>
                    Assert.Equal("Test", _result.Error);
            }

            [Fact]
            public void When_ShouldFail_IfConditionIsFalse() =>
                Assert.True(Result.Ok(when: false).IsFail);
            [Fact]
            public void When_ShouldSucceed_IfConditionIsTrue() =>
                Assert.True(Result.Ok(when: true).IsOk);

            [Fact]
            public void Of_ShouldFail_IfFuncReturnsNull() =>
                Assert.True(Result.Of<string>(() => null).IsFail);
            [Fact]
            public void Of_ShouldSucceed_IfFuncReturnsValue() =>
                Assert.True(Result.Of(() => "").IsOk);
        }

        public class IntResult
        {
            public class Success
            {
                private readonly Result<int> _result = Result.Ok(10);
                
                [Fact]
                public void Success_ShouldReturnTrue() =>
                    Assert.True(_result.IsOk);
                
                [Fact]
                public void Fail_ShouldReturnFalse() =>
                    Assert.False(_result.IsFail);
                
                [Fact]
                public void Value_ShouldReturnCorrectValue() =>
                    Assert.Equal(10, _result.Value);
                
                [Fact]
                public void Error_ShouldReturnEmptyString() =>
                    Assert.Equal("", _result.Error);

                [Fact]
                public void As_ShouldCreateSuccessOfOtherType()
                {
                    var newResult = _result.As<object>();

                    newResult.GetType().ShouldBe(typeof(Result<object>));
                    newResult.IsOk.ShouldBeTrue();
                    newResult.Value.ShouldBe(_result.Value);
                }
            }
            
            public class Fail
            {
                private readonly Result<int> _result = Result.Fail<int>("Test");
                
                [Fact]
                public void Success_ShouldReturnFalse() =>
                    Assert.False(_result.IsOk);
                
                [Fact]
                public void Fail_ShouldReturnTrue() =>
                    Assert.True(_result.IsFail);
                
                [Fact]
                public void Error_ShouldReturnErrorMessage() =>
                    Assert.Equal("Test", _result.Error);

                [Fact]
                public void Value_ShouldThrowInvalidOperationException() =>
                    Assert.Throws<InvalidOperationException>(() => _result.Value);
                
                [Fact]
                public void As_ShouldCreateFailWithSameMessageOfOtherType()
                {
                    var newResult = _result.As<object>();

                    newResult.GetType().ShouldBe(typeof(Result<object>));
                    newResult.IsFail.ShouldBeTrue();
                    newResult.Error.ShouldBe(_result.Error);
                }
            }
        }
    }
}