using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class Generate
    {
        public static Write<object> Write(Type type) =>
            (Write<object>) typeof(Writes)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);

        public static Read<object> Read(Type type) =>
            (Read<object>) typeof(Reads)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);

        internal class Writes
        {
            public static Write<object> Generate<T>() =>
                (x, buffer) => Json<T>.Write((T) x, buffer);
        }

        internal class Reads
        {
            public static Read<object> Generate<T>() =>
                (ref StringSpan x) => Json<T>.Read(ref x);
        }
    }
}