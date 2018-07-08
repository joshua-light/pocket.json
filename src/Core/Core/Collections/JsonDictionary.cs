using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonDictionary<TKey, TValue>
    {
        public static void Append(IDictionary<TKey, TValue> items, StringBuffer buffer)
        {
            buffer.Append('{');

            var i = 0;
            foreach (var item in items)
            {
                Json<TKey>.Append(item.Key, buffer);
                buffer.Append(':');
                Json<TValue>.Append(item.Value, buffer);

                if (i++ != items.Count - 1)
                    buffer.Append(',');
            }

            buffer.Append('}');
        }

        public static Dictionary<TKey, TValue> Unwrap(JsonSpan json)
        {
            if (json.Span[0] == '{' && json.Span[1] == '}')
            {
                json.Skip(2);
                return new Dictionary<TKey, TValue>();
            }

            json.Skip(1); // Skip '{'.
            
            var result = new Dictionary<TKey, TValue>();

            while (true)
            {
                var key = Json<TKey>.Unwrap(json);
                json.Skip(1); // Skip ':'.
                var value = Json<TValue>.Unwrap(json);

                result.Add(key, value);

                if (json.Char == '}')
                {
                    json.Skip(1);
                    break;
                }

                json.Skip(1); // Skip ','.
            }

            return result;
        }
    }

    internal static class JsonDictionary
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}