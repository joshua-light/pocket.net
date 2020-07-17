using System;
using System.Collections.Generic;
using System.Linq;
using Pocket.Extensions;
using static Pocket.Guard;

namespace Pocket
{
    public struct GuardEnumerableExpression<T>
    {
      public IEnumerable<T> _this;

      public GuardEnumerableExpression(IEnumerable<T> @this) =>
        _this = @this;
      
      public void NotNull() =>
        Common(_this).NotNull();
      public void NotNull(string because) =>
        Common(_this).NotNull(because);
      
      public void Null() =>
        Common(_this).Null();
      public void Null(string because) =>
        Common(_this).Null(because);
      
      public void Has(Func<IEnumerable<T>, bool> predicate) =>
        Common(_this).Has(predicate);
      public void Has(Func<IEnumerable<T>, bool> predicate, string because) =>
        Common(_this).Has(predicate, because);

      public void Empty() =>
        When(_this, x => !x.IsNullOrEmpty(), @throw: x => $"{x.ToList().AsString()} should be empty.");
      public void Empty(string because) =>
        When(!_this.IsNullOrEmpty(), @throw: because);

      public void NotEmpty() =>
        NotEmpty($"Specified value should be not empty.");
      public void NotEmpty(string because) =>
        When(_this.IsNullOrEmpty(), @throw: because);

      private static GuardExpression<IEnumerable<T>> Common(IEnumerable<T> x) => new GuardExpression<IEnumerable<T>>(x);
    }
}