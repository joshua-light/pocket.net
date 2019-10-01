# Pocket.Common

[![Build status](https://ci.appveyor.com/api/projects/status/kwed1k33oxbs8a0j/branch/master?svg=true)](https://ci.appveyor.com/project/JoshuaLight/pocket-common/branch/master)
[![codecov](https://codecov.io/gh/JoshuaLight/Pocket.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/JoshuaLight/Pocket.Common)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Common.svg)](https://www.nuget.org/packages/Pocket.Common)

This repository contains a lot of common utilities and extensions, which can be useful in everyday development. They're all implemented using fluent and declarative style.

## Modules

### Extensions

`Dictionary.One`:
```cs
var x = new Dictionary<string, string>
{
    { "Hermann": "Hesse" },
    { "Oscar": "Wilde" },
    { "Vladimir": "Nabokov" }
}

var a = x.One(withKey: "Thomas").OrThrow(withMessage: "Couldn't find `Thomas`.");
// `KeyNotFoundException` is thrown.

var b = x.One(withKey: "Thomas").OrDefault();
Console.WriteLine(b ?? "null"); // Prints `null`.

var c = x.One(withKey: "Thomas").Or("");
var c = x.One(withKey: "Thomas").Or(() => "");
Console.WriteLine(c); // Prints `""`.

var d = x.One(withKey: "Thomas").OrNew("Mann");
var e = x["Thomas"];
Console.WriteLine(d); // Prints `"Mann"`.
Console.WriteLine(e); // Prints `"Mann"`.
```



