using System;

namespace Pocket.Tests.Core.Extensions
{
    public static class ResultAssertExtensions
    {
        public static Result ShouldSucceed(this Result self)
        {
            if (self.IsOk) return self;
            
            throw new InvalidOperationException(self.Error);
        }
        
        public static Result ShouldFail(this Result self)
        {
            if (self.IsFail) return self;
            
            throw new InvalidOperationException("Specified result should be failed, but it's not.");
        }
        
        public static Result<T> ShouldSucceed<T>(this Result<T> self)
        {
            if (self.IsOk) return self;
            
            throw new InvalidOperationException(self.Error);
        }
        
        public static Result<T> ShouldFail<T>(this Result<T> self)
        {
            if (self.IsFail) return self;
            
            throw new InvalidOperationException();
        }
    }
}