using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonList<T>
    {
        public static List<T> Read(ref StringSpan json)
        {
            if (json.CharAt(0) == '[' && json.CharAt(1) == ']')
            {
                json.Start += 2;
                return new List<T>();
            }

            json.Start++; // Skip '['.

            var result = new List<T>();

            while (true)
            {
                var item = Json<T>.Read(ref json);

                result.Add(item);

                if (json.CharAt(0) == ']')
                {
                    json.Start++;
                    break;
                }

                json.Start++; // Skip ','.
            }

            return result;
        }
        
        public static void Write(IList<T> items, StringBuffer buffer)
        {
            buffer.Write('[');

            for (var i = 0; i < items.Count; i++)
            {
                Json<T>.Write(items[i], buffer);
                if (i != items.Count - 1)
                    buffer.Write(',');
            }

            buffer.Write(']');
        }
    }

    internal static class JsonList
    {
        public static Read<T> GenerateRead<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
        
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}