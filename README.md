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
    public int X; // Fields should be `public` aswell.
    public int Y;
}

var point = new Point{ X = 1, Y = 2 };

// Serialization.
var pointJson = point.AsJson(); // "{X:1,Y:2}" here.

// Deserialization.
var samePoint = pointJson.AsJson<Point>(); // { X = 1, Y = 2 } of type `Point` here.
```

## Benchmarks
Code for benchmarks can be found [here](https://github.com/JoshuaLight/Pocket.Json/blob/master/src/Benchmarks/Program.cs).
``` ini
BenchmarkDotNet=v0.10.14, OS=Windows 8.1 (6.3.9600.0)
Intel Core i5-4690 CPU 3.50GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
Frequency=3417974 Hz, Resolution=292.5710 ns, Timer=TSC
.NET Core SDK=2.1.200
  [Host]     : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
```
### Serialization
|        Method |      Mean |     Error |    StdDev |
|-------------- |----------:|----------:|----------:|
| NewtonsoftRun | 373.32 us | 1.8844 us | 1.7627 us |
|       Utf8Run | 110.72 us | 0.4506 us | 0.4215 us |
|     PocketRun |  89.35 us | 0.5339 us | 0.4994 us |
### Deserialization
|        Method |     Mean |     Error |    StdDev |
|-------------- |---------:|----------:|----------:|
| NewtonsoftRun | 515.5 us | 3.8278 us | 3.3932 us |
|       Utf8Run | 246.0 us | 0.7790 us | 0.7287 us |
|     PocketRun | 205.8 us | 1.5910 us | 1.4104 us |
### Deserialization (Object has field initializers)
|        Method |     Mean |    Error |   StdDev |
|-------------- |---------:|---------:|---------:|
| NewtonsoftRun | 518.3 us | 2.037 us | 1.905 us |
|       Utf8Run | 816.1 us | 3.007 us | 2.813 us |
|     PocketRun | 787.8 us | 3.784 us | 3.540 us |

## Architecture
In 2018 (or any year later, I don't want to update that number with grandchild on my knee) noone needs serialization libraries, so it may be helpful to some of you just to look (for learning purposes) how one can be created. This package have simple implementation, so it's a nice start.
1. `AsJson` — entry-point extension-method, which delegates work to internal class `Json`.
2. `Json` — responsible for resolving type of processed value into concrete serialization/deserialization implementation. For example, `int` is resolved to `JsonInt`, etc.
3. `Json*` — serializes/deserializes value of type `*` through `Append` (serialization) and `Unwrap` (deserialization) methods.
That's it!
Also there are some non-readable types, which serve for optimization purposes:
- `StringBuffer` — responsible for fast serialization, because it allocates its buffer in umanaged memory.
- `StringSpan` — a big mutable segment of text, that is used instead of `string` in deserialization.
