using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Pocket.Json
{
    internal static class Generate
    {
        public static Append<object> Append(Type type) =>
            (Append<object>) typeof(AppendGenerator)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);

        public static Unwrap<object> Unwrap(Type type) =>
            (Unwrap<object>) typeof(UnwrapGenerator)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);

        internal class AppendGenerator
        {
            public static Append<object> Generate<T>() => (x, buffer) => Json<T>.Append((T) x, buffer);
        }

        internal class UnwrapGenerator
        {
            public static Unwrap<object> Generate<T>() => x => Json<T>.Unwrap(x);
        }
    }
}