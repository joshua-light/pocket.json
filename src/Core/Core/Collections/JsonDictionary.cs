using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonDictionary<TKey, TValue>
    {
        private static Dictionary<TKey, TValue> Read(ref StringSpan json)
        {
            if (json.CharAt(0) == '{' && json.CharAt(1) == '}')
            {
                json.Start += 2;
                return new Dictionary<TKey, TValue>();
            }

            json.Start++; // Skip '{'.
            
            var result = new Dictionary<TKey, TValue>();

            while (true)
            {
                var key = Json<TKey>.Read(ref json);
                json.Start++; // Skip ':'.
                var value = Json<TValue>.Read(ref json);

                result.Add(key, value);

                if (json.CharAt(0) == '}')
                {
                    json.Start++;
                    break;
                }

                json.Start++; // Skip ','.
            }

            return result;
        }
        
        public static void Write(IDictionary<TKey, TValue> items, StringBuffer buffer)
        {
            buffer.Write('{');

            var i = 0;
            foreach (var item in items)
            {
                Json<TKey>.Write(item.Key, buffer);
                buffer.Write(':');
                Json<TValue>.Write(item.Value, buffer);

                if (i++ != items.Count - 1)
                    buffer.Write(',');
            }

            buffer.Write('}');
        }
    }

    internal static class JsonDictionary
    {
        public static Read<T> Read<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
        
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}