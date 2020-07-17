using static Pocket.Guard;

namespace Pocket
{
    public struct GuardBoolExpression
    {
        private readonly bool _this;

        public GuardBoolExpression(bool @this) =>
            _this = @this;

        public void True() =>
            True($"[ {_this} ] should be true.");
        public void True(string because) =>
            When(!_this, @throw: because);

        public void False() =>
            False($"[ {_this} ] should be false.");
        public void False(string because) =>
            When(_this, @throw: because);
    }
}