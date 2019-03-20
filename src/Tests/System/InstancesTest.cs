using System.Linq;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System
{
  public class InstancesTest
  {
    [Fact]
    public void InstancesOf_IInterface_ShouldBeAllImplementations() =>
      Instances.Of<IInterface>().InCurrentAssembly().Select(x => x.GetType()).ShouldBe(new[]
      {
        typeof(Implementation1),
        typeof(Implementation2),
        typeof(Implementation3),
        typeof(Implementation4),
        typeof(Implementation5),
        typeof(ChildAndImplementation1),
        typeof(ChildAndImplementation2),
        typeof(ChildAndImplementation3),
        typeof(ChildAndImplementation4),
        typeof(ChildAndImplementation5),
      });
    
    [Fact]
    public void InstancesOf_Class_ShouldBeAllChilds() =>
      Instances.Of<Class>().InCurrentAssembly().Select(x => x.GetType()).ShouldBe(new[]
      {
        typeof(Child1),
        typeof(Child2),
        typeof(Child3),
        typeof(Child4),
        typeof(Child5),
        typeof(ChildAndImplementation1),
        typeof(ChildAndImplementation2),
        typeof(ChildAndImplementation3),
        typeof(ChildAndImplementation4),
        typeof(ChildAndImplementation5),
      });
    
    [Fact]
    public void InstancesOf_ConstructedGenericInterface_ShouldBeConstructed() =>
      Instances.Of<IGenericInterface<string>>().InCurrentAssembly().Select(x => x.GetType()).ShouldBe(new[]
      {
        typeof(GenericImplementation<string>),
        typeof(ConstructedGenericImplementation)
      });
    
    public interface IInterface { }
    public interface IGenericInterface<T> { }
    
    public abstract class Class { }
    public abstract class AbstractChild : Class { }
    
    public class Implementation1 : IInterface { }
    public class Implementation2 : IInterface { }
    public class Implementation3 : IInterface { }
    public class Implementation4 : IInterface { }
    public class Implementation5 : IInterface { }
    
    public class GenericImplementation<T> : IGenericInterface<T> { }
    public class ConstructedGenericImplementation : IGenericInterface<string> { }
    
    public class Child1 : Class  { }
    public class Child2 : Class  { }
    public class Child3 : Class  { }
    public class Child4 : Class  { }
    public class Child5 : Class  { }
    
    public class ChildAndImplementation1 : Class, IInterface { }
    public class ChildAndImplementation2 : Class, IInterface { }
    public class ChildAndImplementation3 : Class, IInterface { }
    public class ChildAndImplementation4 : Class, IInterface { }
    public class ChildAndImplementation5 : Class, IInterface { }
  }
}