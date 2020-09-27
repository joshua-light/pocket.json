# Pocket.Json

![Build](https://github.com/JoshuaLight/pocket.json/workflows/Build/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Json.svg)](https://www.nuget.org/packages/Pocket.Json)


_A simple JSON serialization/deserialization library that is pretty fast._
It doesn't match 100% of JSON spec, and was made just for learning purposes.

## Usage
### Data Format
```c#
// Class should be `public` due to code generation.
public class Point
{
    // Fields should be raw and `public`.
    [Json] public int X;
    [Json] public int Y;
}
```

### Serialize
```c#
var json = new Point { X = 1, Y = 2 }.ToJson();
```

### Deserialize
```c#
var point = json.FromJson<Point>();
```

## Benchmarks (outdated)
Code for benchmarks can be found [here](https://github.com/JoshuaLight/Pocket.Json/blob/master/src/Benchmarks/Program.cs).
```
BenchmarkDotNet=v0.12.1, OS=elementary 5.1.5
Intel Core i9-9900KF CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
```

### Serialization
|         Method |     Mean |   Error |  StdDev |
|--------------- |---------:|--------:|--------:|
| NewtonsoftJson | 278.7 μs | 0.78 μs | 0.73 μs |
|       Utf8Json | 101.5 μs | 0.24 μs | 0.23 μs |
|     PocketJson | 121.4 μs | 1.37 μs | 1.28 μs |

### Deserialization
|         Method |      Mean |    Error |   StdDev |
|--------------- |----------:|---------:|---------:|
| NewtonsoftJson | 421.39 μs | 6.494 μs | 6.075 μs |
|       Utf8Json | 130.93 μs | 0.298 μs | 0.248 μs |
|     PocketJson |  90.99 μs | 0.386 μs | 0.322 μs |
