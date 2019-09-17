using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Pocket.Common
{
  public class FluentInitialization<T>
  {
    private class BoundedField
    {
      private readonly object _instance;
      private readonly FieldInfo _field;

      public BoundedField(object instance, FieldInfo field)
      {
        _instance = instance;
        _field = field;
      }

      public FieldInfo Info =>
        _field;
      
      public object Get() =>
        _field.GetValue(_instance);
      
      public void Set(object value) =>
        _field.SetValue(_instance, value);

      public void Initialize() => Initialize(Info.FieldType.New());
      public void Initialize(object value)
      {
        if (Get() == null || Info.FieldType.IsValueType)
          Set(value);
      }
    }

    private readonly T _value;

    public FluentInitialization(T value) =>
      _value = value;

    public FluentInitialization<T> Do(Action<T> action)
    {
      action(_value);
      return this;
    }

    public FluentInitialization<T> Has<TValue>(Expression<Func<T, TValue>> selector, Action<TValue> action) where TValue : new()
    {
      var body = selector.Body as MemberExpression;
      var fields = BoundedFieldsOf(body);
      var field = fields.Last();
      
      field.Initialize();
      action((TValue) field.Get());
      
      return this;
    }

    public FluentInitialization<T> Has<TValue>(Expression<Func<T, TValue>> selector) where TValue : new()
    {
      if (selector.Body is MethodCallExpression call)
        return Invoke(call);

      return Has(selector, new TValue());
    }

    public FluentInitialization<T> Has<TValue>(Expression<Func<T, TValue>> selector, TValue value)
    {
      var body = selector.Body as MemberExpression;
      var field = BoundedFieldsOf(body).Last();
      
      field.Initialize(value);

      return this;
    }

    private IEnumerable<BoundedField> BoundedFieldsOf(MemberExpression root)
    {
      object current = _value;
      foreach (var field in FieldsOf(root).Reverse())
      {
        var currentValue = field.GetValue(current);
        if (currentValue == null && field != FieldsOf(root).Reverse().Last())
        {
          currentValue = Activator.CreateInstance(field.FieldType);
          field.SetValue(current, currentValue);
        }

        yield return new BoundedField(current, field);
        current = currentValue;
      }
    }

    private static IEnumerable<FieldInfo> FieldsOf(MemberExpression root)
    {
      yield return root.Member as FieldInfo;

      var next = root;
      while ((next = next.Expression as MemberExpression) != null)
        yield return next.Member as FieldInfo;
    }

    private FluentInitialization<T> Invoke(MethodCallExpression call)
    {
      if (call.Object != null)
      {
        // Simple method call.
        var body = (MemberExpression) call.Object;
        var fields = BoundedFieldsOf(body);
        var field = fields.Last();
        
        field.Initialize();

        call.Method.Invoke(
          field.Get(),
          call.Arguments.Select(Evaluated).ToArray());
      }
      else
      {
        // Static method call.
        var body = (MemberExpression) call.Arguments[0];
        var fields = BoundedFieldsOf(body);
        var field = fields.Last();
        
        field.Initialize();

        call.Method.Invoke(
          null,
          new[] { field.Get() }.Concat(call.Arguments.Skip(1).Select(Evaluated)).ToArray());
      }

      return this;
    }

    private static object Evaluated(Expression expression)
    {
      switch (expression)
      {
        case ConstantExpression x: return Evaluated(x);
        case MemberExpression x: return Evaluated(x);
        case NewExpression x: return Evaluated(x);
        case MemberInitExpression x: return Evaluated(x);
        case UnaryExpression x: return Evaluated(x);

        default:
          throw new InvalidOperationException($"Expression '{expression.GetType()}' cannot be converted to plain value.");
      }
    }

    private static object Evaluated(ConstantExpression expression) => expression.Value;

    private static object Evaluated(MemberExpression expression)
    {
      var field = (FieldInfo) expression.Member;
      var parent = (ConstantExpression) expression.Expression;

      return field.GetValue(parent.Value);
    }

    private static object Evaluated(NewExpression expression)
    {
      var constructor = expression.Constructor;
      var arguments = expression.Arguments.Select(Evaluated).ToArray();

      return constructor.Invoke(arguments);
    }

    private static object Evaluated(MemberInitExpression expression)
    {
      var result = Evaluated(expression.NewExpression);

      foreach (var binding in expression.Bindings)
      {
        if (binding is MemberAssignment assignment)
        {
          var field = (FieldInfo) assignment.Member;
          var value = Evaluated(assignment.Expression);

          field.SetValue(result, value);
        }
        else
          throw new InvalidOperationException($"{binding.GetType().PrettyName()} binding is not supported.");
      }

      return result;
    }

    private static object Evaluated(UnaryExpression expression)
    {
      if (expression.NodeType == ExpressionType.Convert)
      {
        var operand = Evaluated(expression.Operand);
        var type = expression.Type;

        return Cast(operand, type);
      }

      throw new InvalidOperationException($"'UnaryExpression' with 'NodeType' of '{expression.NodeType}' is not supported.");
    }

    private static object Cast(object value, Type type)
    {
      try
      {
        return Convert.ChangeType(value, type);
      }
      catch (InvalidCastException)
      {
        var i = type.GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static);
        var e = type.GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static);

        if (i != null && Casts(i, from: value.GetType(), to: type))
          return i.Invoke(null, new[] { value });
        if (e != null && Casts(e, from: value.GetType(), to: type))
          return e.Invoke(null, new[] { value });

        throw new InvalidCastException($"'{value}' cannot be converted to type '{type}'");
      }
    }

    private static bool Casts(MethodInfo method, Type from, Type to)
    {
      if (method.ReturnType == to)
      {
        var parameters = method.GetParameters();
        if (parameters.Length == 1 && parameters[0].ParameterType == from)
          return true;
      }

      return false;
    }
  }
}