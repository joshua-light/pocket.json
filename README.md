# Pocket.Json

[![Build status](https://ci.appveyor.com/api/projects/status/lwgemdqwe3gepq5b?svg=true)](https://ci.appveyor.com/project/JoshuaLight/castalia)
[![codecov](https://codecov.io/gh/JoshuaLight/Pocket.Json/branch/master/graph/badge.svg)](https://codecov.io/gh/JoshuaLight/Pocket.Json)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Json.svg)](https://www.nuget.org/packages/Pocket.Json)

This small package contains `JSON` serialization/deserialization library, which is mostly focusing on speed and simplicity.

## Usage
For converting objects you should use only one extension-method: `AsJson` (which sounds what it actually do — represents data as either serialized or deserialized JSON).
```c#
public class Point // It is important that type is `public` due to code-generation.
{
    public int X; // Fields should be public aswell.
    public int Y;
}

var point = new Point{ X = 1, Y = 2 };

// Serialization.
var pointJson = point.AsJson(); // "{X:1,Y:2}" here.

// Deserialization.
var samePoint = pointJson.AsJson<Point>(); // { X = 1, Y = 2 } of type `Point` here.
```

## Architecture
In 2018 (or any year later, I don't want to update that number with grandchild on my knee) noone needs serialization libraries, so it may be helpful to some of you just to look how one can be created for learning purposes. This package have simple implementation, so it's a nice start.
1. `AsJson` — entry-point extension-method, which delegates work to internal class `Json`.
2. `Json` — responsible for resolving type of processed value into concrete serialization/deserialization implementation. For example, `int` is resolved to `JsonInt`, etc.
3. `Json*` — serializes/deserializes value of type `*` through `Append` (serialization) and `Unwrap` (deserialization) methods.
That's it!
Also there are some non-readable types, which serve for optimization purposes:
- `StringBuffer` — responsible for fast serialization, because it allocates its buffer in umanaged memory.
- `StringSpan` — a big mutable segment of text, that is used instead of `string` in deserialization.
