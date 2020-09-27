using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonEnumerable<T>
    {
        public static void Write(IEnumerable<T> items, StringBuffer buffer)
        {
            buffer.Write('[');

            var isFirst = true;
            foreach (var item in items)
            {
                if (isFirst)
                {
                    Json<T>.Write(item, buffer);
                    isFirst = false;
                    continue;
                }

                buffer.Write(',');
                Json<T>.Write(item, buffer);
            }

            buffer.Write(']');
        }
    }

    internal static class JsonEnumerable
    {
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonEnumerable<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}