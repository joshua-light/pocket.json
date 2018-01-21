using System.Reflection;

namespace Castalia
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
    }

    internal static class JsonArray
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
    }
}