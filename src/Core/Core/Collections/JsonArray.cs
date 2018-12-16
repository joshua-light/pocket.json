using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonArray<T>
    {
        public static void Append(T[] items, StringBuffer buffer)
        {
            buffer.Append('[');

            for (var i = 0; i < items.Length; i++)
            {
                Json<T>.Append(items[i], buffer);
                
                if (i != items.Length - 1)
                    buffer.Append(',');
            }

            buffer.Append(']');
        }

        public static T[] Unwrap(JsonSpan json)
        {
            if (json.Span.CharAt(0) == '[' && json.Span.CharAt(1) == ']')
            {
                json.Span.Start += 2;
                return Array.Empty<T>();
            }

            return JsonList<T>.Unwrap(json).ToArray();
        }
    }

    internal static class JsonArray
    {
        public static Append<T> Append<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}