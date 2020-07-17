using System;
using System.Collections.Generic;
using Pocket.Extensions;
using static Pocket.Guard;

namespace Pocket
{
    public struct GuardExpression<T>
    {
        private readonly T _this;

        public GuardExpression(T @this) =>
            _this = @this;

        public void NotNull() =>
            NotNull(because: "Specified value should be not null.");
        public void NotNull(string because) =>
            When(_this == null, @throw: () => new ArgumentNullException("", because));

        public void Null() =>
            Null(because: $"[ {_this} ] should be null.");
        public void Null(string because) =>
            When(_this != null, @throw: because);

        public void Is(T value) =>
            Is(value, $"[ {_this} ] should be [ {value} ].");
        public void Is(T value, string because) =>
            When(!Equals(_this, value), @throw: because);

        public void IsNot(T value) =>
            IsNot(value, $"[ {_this} ] should not be [ {value} ].");
        public void IsNot(T value, string because) =>
            When(Equals(_this, value), @throw: because);

        public void Has(Func<T, bool> predicate) =>
            Has(predicate, "Specified predicate should match.");
        public void Has(Func<T, bool> predicate, string because) =>
            When(!predicate(_this), @throw: because);

        public void Less(T than) =>
            Less(than, $"[ {_this} ] should be less than [ {than} ].");
        public void Less(T than, string because) =>
            When(Compared(_this, than) >= 0, @throw: because);

        public void LessOrEqual(T to) =>
            LessOrEqual(to, $"[ {_this} ] should be less or equal to [ {to} ].");
        public void LessOrEqual(T to, string because) =>
            When(Compared(_this, to) > 0, @throw: because);

        public void Greater(T than) =>
            Greater(than, $"[ {_this} ] should be greater than [ {than} ].");
        public void Greater(T than, string because) =>
            When(Compared(_this, than) <= 0, @throw: because);

        public void GreaterOrEqual(T to) =>
            GreaterOrEqual(to, $"[ {_this} ] should be greater or equal to [ {to} ].");
        public void GreaterOrEqual(T to, string because) =>
            When(Compared(_this, to) < 0, @throw: because);

        public void InRange(T from, T to) =>
            InRange(from, to, $"[ {_this} ] should be in range [ {from}, {to} ].");
        public void InRange(T from, T to, string because)
        {
            Ensure(from).LessOrEqual(from);

            When(Compared(_this, from) < 0 || Compared(_this, to) > 0, @throw: because);
        }

        private static bool Equals(T x, T y) => EqualityComparer<T>.Default.Equals(x, y);

        private static int Compared(T x, T y) => x is IComparable<T> c
            ? c.CompareTo(y)
            : throw new ArgumentException(
                $"[ {x.GetType().PrettyName()} ] should implement [ {typeof(IComparable<T>).PrettyName()} ].");
    }
}