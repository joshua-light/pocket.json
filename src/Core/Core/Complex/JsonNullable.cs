using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonNullable<T> where T : struct
    {
        public static void Append(T? nullable, StringBuffer buffer)
        {
            if (nullable != null) Json<T>.Append(nullable.Value, buffer);
        }
        
        public static T? Unwrap(StringSpan json)
        {
            if (json.Length == 0)
                return null;

            return Json<T>.Unwrap(json);
        }
    }

    internal static class JsonNullable
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonNullable<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T)));
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonNullable<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T)));
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}