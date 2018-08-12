using System;
using System.Reflection;

namespace Pocket.Json
{
    internal class JsonEnum<T>
    {
        public static void Append(T value, StringBuffer buffer)
        {
            var type = Enum.GetUnderlyingType(typeof(T));
            Json.Append(type, value, buffer);
        }

        public static T Unwrap(JsonSpan span)
        {
            var type = Enum.GetUnderlyingType(typeof(T));
            return (T) Json.Unwrap(type, span);
        }
    }

    internal class JsonEnum
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonEnum<>).MakeGenericType(typeof(T));
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonEnum<>).MakeGenericType(typeof(T));
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}