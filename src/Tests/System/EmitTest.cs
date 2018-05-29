using Xunit;

namespace Pocket.Common.Tests.System
{
    public class EmitTest
    {
        #region Ctor
        
        [Fact]
        public void Ctor_ShouldCreateCorrectFunc_IfTypeHasEmptyCtor()
        {
            var func = Emit.Ctor<WithEmptyCtor>();

            Assert.NotNull(func());
        }

        [Fact]
        public void Ctor_ShouldCreateCorrectFunc_IfWasCalledManyTimes()
        {
            Emit.Ctor<WithEmptyCtor>();
            Emit.Ctor<WithEmptyCtor>();
            Emit.Ctor<WithEmptyCtor>();
            var func = Emit.Ctor<WithEmptyCtor>();
            
            Assert.NotNull(func());
        }
        
        [Fact]
        public void Ctor_ShouldCreateCorrectFunc_IfTypeHasManyCtors()
        {
            var func = Emit.Ctor<WithManyCtors>();

            Assert.NotNull(func());
        }
                
        public class WithEmptyCtor { }
        public class WithManyCtors
        {
            public WithManyCtors(int a) { }
            public WithManyCtors(int a, int b) { }
            public WithManyCtors(string a) { }
            public WithManyCtors() { }
        }
        
        #endregion
        
        #region Fields

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(30)]
        public void GetField_ShouldCreateCorrectFunc(int data)
        {
            var field = typeof(WithPublicField).GetField("Data");
            var get = Emit.GetField<WithPublicField>(field);
            
            Assert.Equal(data, get(new WithPublicField{ Data = data }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(30)]
        public void SetField_ShouldCreateCorrectAction(int data)
        {
            var field = typeof(WithPublicField).GetField("Data");
            var set = Emit.SetField<WithPublicField>(field);
            var instance = new WithPublicField();

            set(instance, data);
            
            Assert.Equal(data, instance.Data);
        }

        public class WithPublicField { public int Data; }
        
        #endregion
    }
}