using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Pocket.Common;

namespace Pocket.Json
{
    internal delegate void Write<T>(T value, StringBuffer buffer);
    internal delegate T Read<T>(ref StringSpan json);

    internal static class Json<T>
    {
        public static readonly Read<T> Read = (Read<T>) NewRead();
        public static readonly Write<T> Write = (Write<T>) NewWrite();

        private static object NewRead()
        {
            var type = typeof(T);

            if (type == typeof(bool))
                return (Read<bool>) JsonBool.Read;

            if (type == typeof(byte))
                return (Read<byte>) JsonByte.Read;

            if (type == typeof(char))
                return (Read<char>) JsonChar.Read;

            if (type == typeof(int))
                return (Read<int>) JsonInt.Read;

            if (type == typeof(long))
                return (Read<long>) JsonLong.Read;

            if (type == typeof(float))
                return (Read<float>) JsonFloat.Read;

            if (type == typeof(double))
                return (Read<double>) JsonDouble.Read;

            if (type == typeof(string))
                return (Read<string>) JsonString.Read;

            if (type.IsNullable())
                return JsonNullable.Read<T>();

            if (type.IsEnum)
                return JsonEnum.Read<T>();

            if (type.IsArray)
                return JsonArray.GenerateRead<T>();

            if (type.Is(typeof(List<>)))
                return JsonList.GenerateRead<T>();

            if (type.Is(typeof(HashSet<>)))
                return JsonHashSet.Read<T>();

            if (type.Is(typeof(Dictionary<,>)))
                return JsonDictionary.Read<T>();

            return (Read<T>) JsonObject<T>.Read;
        }

        private static object NewWrite()
        {
            var type = typeof(T);

            if (type == typeof(bool))
                return (Write<bool>) JsonBool.Write;

            if (type == typeof(byte))
                return (Write<byte>) JsonByte.Write;

            if (type == typeof(char))
                return (Write<char>) JsonChar.Write;

            if (type == typeof(int))
                return (Write<int>) JsonInt.Write;

            if (type == typeof(long))
                return (Write<long>) JsonLong.Write;

            if (type == typeof(float))
                return (Write<float>) JsonFloat.Write;

            if (type == typeof(double))
                return (Write<double>) JsonDouble.Write;

            if (type == typeof(string))
                return (Write<string>) JsonString.Write;

            if (type.IsNullable())
                return JsonNullable.Write<T>();

            if (type.IsEnum)
                return JsonEnum.Write<T>();

            if (type.IsArray)
                return JsonArray.Write<T>();

            if (type.Is(typeof(List<>)))
                return JsonList.Write<T>();
            
            if (type.Is(typeof(Dictionary<,>)))
                return JsonDictionary.Write<T>();

            if (type.Is(typeof(IEnumerable<>)) || type.Implements(typeof(IEnumerable<>)))
                return JsonEnumerable.Write<T>();

            return (Write<T>) JsonObject<T>.Write;
        }
    }

    internal static class Json
    {
        private static class Cache
        {
            private static readonly ConcurrentDictionary<Type, Write<object>> Writes =
                new ConcurrentDictionary<Type, Write<object>>();
            
            private static readonly ConcurrentDictionary<Type, Read<object>> Reads =
                new ConcurrentDictionary<Type, Read<object>>();

            public static Write<object> Write(Type type) =>
                Writes.TryGetValue(type, out var write) ? write : Writes[type] = Generate.Write(type);

            public static Read<object> Read(Type type) =>
                Reads.TryGetValue(type, out var read) ? read : Reads[type] = Generate.Read(type);
        }

        public static void Write(Type type, object value, StringBuffer buffer) =>
            Cache.Write(type)(value, buffer);

        public static object Read(Type type, ref StringSpan json) =>
            Cache.Read(type)(ref json);
    }
}