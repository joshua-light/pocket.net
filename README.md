# Pocket.Common

[![Build status](https://ci.appveyor.com/api/projects/status/kwed1k33oxbs8a0j/branch/master?svg=true)](https://ci.appveyor.com/project/JoshuaLight/pocket-common/branch/master)
[![codecov](https://codecov.io/gh/JoshuaLight/Pocket.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/JoshuaLight/Pocket.Common)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Common.svg)](https://www.nuget.org/packages/Pocket.Common)

Common utilities and extensions that extend .NET Framework in declarative and fluent way.

## Extensions

### Collections

#### `AddRange`

Adds a range of items to the `ICollection<T>` instance (pretty common extension that is missing in standard library).

```cs
ICollection<int> numbersA = { 1, 2, 3 };
IEnumerable<int> numbersB = { 4, 5, 6 };

numbersA.AddRange(numbersB);

Console.WriteLine(numbersA.AsString()); // Prints "[1, 2, 3, 4, 5, 6]".
```

### Dictionary

#### `One`

Represents a consistent access to dictionary elements in four different ways using fluent sentences, starting with `One`. Each way represents a reaction to situation, when key is missing.

These reactions are:

a) throw a `KeyNotFoundException` with specified message:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var x = writers.One(withKey: "Thomas").OrThrow(withMessage: "Couldn't find `Thomas`.");
// `KeyNotFoundException` is thrown.
```

b) return `default(TValue)`:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var x = writers.One(withKey: "Thomas").OrDefault();
Console.WriteLine(x ?? "null"); // Prints `null`.
```

c) return specified value:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var x = writers.One(withKey: "Thomas").Or("");
var y = writers.One(withKey: "Thomas").Or(() => "");
Console.WriteLine(x); // Prints `""`.
Console.WriteLine(y); // Prints `""`.
```

d) return specified value and also write it to dictionary:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var x = writers.One(withKey: "Thomas").OrNew("Mann");
var y = writers["Thomas"];
Console.WriteLine(x); // Prints `"Mann"`.
Console.WriteLine(y); // Prints `"Mann"`.
```

### Enumerable

#### `OrEmpty`

Returns `Enumerable.Empty<T>` sequence if current is `null`. This method can be used
as a more fluent alternative to `x ?? Enumerable.Empty<T>`.

```cs
IEnumerable<int> x = null;

Console.WriteLine(x.OrEmpty()); // Prints [].
```

#### `Each`

Executes specified action on each item of the sequence. This can be useful when you want,
for example, to print items in the middle of LINQ.

``` cs
Enumerable
    .Range(1, 10)
    .Where(x => x % 2 == 0)
    .Each(Console.Write)
    .Select(x => x * x)
    .ToList(); // Prints "246810".
```

#### `ForEach`

Executes specified action on each item of the sequence, but doesn't return anything.

This method is similar to `ForEach` from `List`.

``` cs
Enumerable
    .Range(0, 10)
    .Where(x => x % 2 == 0)
    .ForEach(Console.Write); // Prints "246810".
```

#### `TakeMin`

Takes first object from the sequence that has minimum value, provided by specified selector function.

This method works in same way as LINQ `Min` but instead of value returns the object, not the minimum value it holds.

``` cs
struct Point
{
    public readonly int X;
    public readonly int Y;

    public Point(int x, int y) =>
        (X, Y) = (x, y)
}

// `point` holds not an `int`, but `Point`.
var point = Enumerable
    .Range(0, 10)
    .Select(x => new Point(x, x))
    .TakeMin(point => point.X)

Console.WriteLine($"{point.X}, {point.Y}") // Prints "0, 0".
```

#### `TakeMax`

Works in similar way as `TakeMin`, but returns an object with maximum value.

#### `IsNullOrEmpty`

Checks whether sequence is either `null` or contains no elements.

``` cs
IEnumerable<int> a;

a = Enumerable.Range(0, 10);
Console.WriteLine(numbers.IsNullOrEmpty()); // Prints "false".

a = null;
Console.WriteLine(numbers.IsNullOrEmpty()); // Prints "true".

a = Enumerable.Empty<int>();
Console.WriteLine(numbers.IsNullOrEmpty()); // Prints "true".
```

TODO

