using System;
using System.Reflection;

namespace Pocket.Json
{
    internal class JsonEnum<T>
    {
        public static T Read(ref StringSpan span)
        {
            var type = Enum.GetUnderlyingType(typeof(T));
            return (T) Json.Read(type, ref span);
        }
        
        public static void Write(T value, StringBuffer buffer)
        {
            var type = Enum.GetUnderlyingType(typeof(T));
            Json.Write(type, value, buffer);
        }
    }

    internal class JsonEnum
    {
        public static Read<T> Read<T>()
        {
            var type = typeof(JsonEnum<>).MakeGenericType(typeof(T));
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
        
        public static Write<T> Write<T>()
        {
            var type = typeof(JsonEnum<>).MakeGenericType(typeof(T));
            var method = type.GetTypeInfo().GetDeclaredMethod("Write");

            return (Write<T>) method.CreateDelegate(typeof(Write<T>));
        }
    }
}