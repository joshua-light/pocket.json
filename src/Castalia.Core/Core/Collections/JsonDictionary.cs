using System.Collections.Generic;
using System.Reflection;

namespace Castalia
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
    }
}