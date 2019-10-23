# Pocket.Common

[![Build status](https://ci.appveyor.com/api/projects/status/kwed1k33oxbs8a0j/branch/master?svg=true)](https://ci.appveyor.com/project/JoshuaLight/pocket-common/branch/master)
[![codecov](https://codecov.io/gh/JoshuaLight/Pocket.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/JoshuaLight/Pocket.Common)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Common.svg)](https://www.nuget.org/packages/Pocket.Common)

This repository contains a lot of common utilities and extensions, which can be useful in everyday development. They're all implemented using fluent and declarative style.

## Extensions

### Collections

#### `Dictionary.One`

Represents a consistent access to dictionary elements in four different ways using fluent sentences, starting with `One`. Each way represents a reaction to situation, when key is missing.

These reactions are:

a) throw a `KeyNotFoundException` with specified message:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var a = writers.One(withKey: "Thomas").OrThrow(withMessage: "Couldn't find `Thomas`.");
// `KeyNotFoundException` is thrown.
```

b) return `default(TValue)`:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var b = writers.One(withKey: "Thomas").OrDefault();
Console.WriteLine(b ?? "null"); // Prints `null`.
```

c) return specified value:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var c = x.One(withKey: "Thomas").Or("");
var d = x.One(withKey: "Thomas").Or(() => "");
Console.WriteLine(c); // Prints `""`.
Console.WriteLine(d); // Prints `""`.
```

d) return specified value and also write it to dictionary:
```cs
var writers = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
};

var e = x.One(withKey: "Thomas").OrNew("Mann");
var f = x["Thomas"];
Console.WriteLine(e); // Prints `"Mann"`.
Console.WriteLine(f); // Prints `"Mann"`.
```

```
