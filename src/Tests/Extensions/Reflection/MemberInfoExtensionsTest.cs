using System;
using System.Linq;
using Pocket.Extensions;
using Shouldly;
using Xunit;

namespace Pocket.Tests.Extensions.Reflection
{
    public class MemberInfoExtensionsTest
    {
        public class SomeAttribute : Attribute { }
        
        public class EmptyType { }
        public class SomeType
        {
            [Some] public int Field;
            [Some] public int Property => 0;
            [Some] public void Method() { }
        }
        
        [Fact]
        public void AttributesCountOfEmptyType_ShouldBe0() =>
            AttributesCount(typeof(EmptyType)).ShouldBe(0);

        [Fact]
        public void AttributesCountOfSomeType_ShouldBe3() =>
            AttributesCount(typeof(SomeType)).ShouldBe(3);

        private static int AttributesCount(Type type) =>
            type.Fields().Count(x => x.Has<SomeAttribute>()) +
            type.Properties().Count(x => x.Has<SomeAttribute>()) +
            type.Methods().Count(x => x.Has<SomeAttribute>());
    }
}