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

        public static Dictionary<TKey, TValue> Unwrap(JsonSpan json) => UnwrapOptimized(json, ref json.Span);

        private static Dictionary<TKey, TValue> UnwrapOptimized(JsonSpan json, ref StringSpan span)
        {
            if (span.CharAt(0) == '{' && span.CharAt(1) == '}')
            {
                span.Start += 2;
                return new Dictionary<TKey, TValue>();
            }

            span.Start++; // Skip '{'.
            
            var result = new Dictionary<TKey, TValue>();

            while (true)
            {
                var key = Json<TKey>.Unwrap(json);
                span.Start++; // Skip ':'.
                var value = Json<TValue>.Unwrap(json);

                result.Add(key, value);

                if (span.CharAt(0) == '}')
                {
                    span.Start++;
                    break;
                }

                span.Start++; // Skip ','.
            }

            return result;
        }
    }

    internal static class JsonDictionary
    {
        public static Append<T> Append<T>()
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