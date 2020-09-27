# Pocket.Json

![Build](https://github.com/JoshuaLight/pocket.json/workflows/Build/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Pocket.Json.svg)](https://www.nuget.org/packages/Pocket.Json)


_A simple JSON serialization/deserialization library that is pretty fast._

_(It doesn't match 100% of JSON spec, and was made just for learning purposes.)_

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

## Benchmarks
Code for benchmarks can be found [here](https://github.com/JoshuaLight/Pocket.Json/blob/master/src/Benchmarks/Program.cs).
```
BenchmarkDotNet=v0.12.1, OS=elementary 5.1.5
Intel Core i9-9900KF CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
```

### Serialization
|         Method |      Mean |    Error |   StdDev |
|--------------- |----------:|---------:|---------:|
| NewtonsoftJson | 274.29 μs | 1.478 μs | 1.310 μs |
|       Utf8Json |  99.78 μs | 0.537 μs | 0.476 μs |
|     PocketJson | 108.07 μs | 0.434 μs | 0.362 μs |

### Deserialization
|         Method |      Mean |    Error |   StdDev |
|--------------- |----------:|---------:|---------:|
| NewtonsoftJson | 401.33 μs | 3.472 μs | 3.077 μs |
|       Utf8Json | 130.03 μs | 1.492 μs | 1.395 μs |
|     PocketJson |  79.60 μs | 0.286 μs | 0.267 μs |
