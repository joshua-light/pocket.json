using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonEnumerable<T>
    {
        public static void Append(IEnumerable<T> items, StringBuffer buffer)
        {
            buffer.Append('[');

            var isFirst = true;
            foreach (var item in items)
            {
                if (isFirst)
                {
                    Json<T>.Append(item, buffer);
                    isFirst = false;
                    continue;
                }

                buffer.Append(',');
                Json<T>.Append(item, buffer);
            }

            buffer.Append(']');
        }
    }

    internal static class JsonEnumerable
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonEnumerable<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
    }
}