using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonArray<T>
    {
        public static T[] Read(ref StringSpan json)
        {
            if (json.CharAt(0) == '[' && json.CharAt(1) == ']')
            {
                json.Start += 2;
                return Array.Empty<T>();
            }

            return JsonList<T>.Read(ref json).ToArray();
        }
        
        public static void Write(T[] items, StringBuffer buffer)
        {
            buffer.Write('[');

            for (var i = 0; i < items.Length; i++)
            {
                Json<T>.Write(items[i], buffer);
                
                if (i != items.Length - 1)
                    buffer.Write(',');
            }

            buffer.Write(']');
        }
    }

    internal static class JsonArray
    {
        public static Read<T> GenerateRead<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
        
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}