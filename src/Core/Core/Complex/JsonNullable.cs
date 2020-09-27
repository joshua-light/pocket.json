using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonNullable<T> where T : struct
    {
        public static T? Read(ref StringSpan json)
        {
            if (json.Start == json.End)
                return null;

            return Json<T>.Read(ref json);
        }
        
        public static void Write(T? nullable, StringBuffer buffer)
        {
            if (nullable != null)
                Json<T>.Write(nullable.Value, buffer);
        }
    }

    internal static class JsonNullable
    {
        public static Read<T> Read<T>()
        {
            var type = typeof(JsonNullable<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T)));
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
        
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonNullable<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T)));
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}