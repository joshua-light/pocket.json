using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonList<T>
    {
        public static void Append(IList<T> items, StringBuffer buffer)
        {
            buffer.Append('[');

            for (var i = 0; i < items.Count; i++)
            {
                Json<T>.Append(items[i], buffer);
                if (i != items.Count - 1)
                    buffer.Append(',');
            }

            buffer.Append(']');
        }

        public static List<T> Unwrap(JsonSpan json)
        {
            if (json.Span.CharAt(0) == '[' && json.Span.CharAt(1) == ']')
            {
                json.Skip(2);
                return new List<T>();
            }

            json.Skip(1); // Skip '['.

            var result = new List<T>();

            while (true)
            {
                var item = Json<T>.Unwrap(json);

                result.Add(item);

                if (json.Span.CharAt(0) == ']')
                {
                    json.Skip(1);
                    break;
                }

                json.Skip(1); // Skip ','.
            }

            return result;
        }
    }

    internal static class JsonList
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}