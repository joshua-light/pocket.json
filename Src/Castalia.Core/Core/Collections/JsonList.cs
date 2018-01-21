using System.Collections.Generic;
using System.Reflection;

namespace Castalia
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
    }

    internal static class JsonList
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
    }
}