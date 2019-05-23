using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Pocket.Common;

namespace Pocket.Json
{
    internal delegate void Append<T>(T value, StringBuffer buffer);
    internal delegate T Unwrap<T>(JsonSpan json);

    internal static class Json<T>
    {
        private static readonly Append<T> _append = (Append<T>) NewAppend();
        private static readonly Unwrap<T> _unwrap = (Unwrap<T>) NewUnwrap();

        public static void Append(T value, StringBuffer buffer) =>
            _append(value, buffer);

        public static T Unwrap(JsonSpan json) =>
            _unwrap(json);

        private static object NewAppend()
        {
            var type = typeof(T);

            if (type == typeof(bool))
                return (Append<bool>) JsonBool.Append;

            if (type == typeof(byte))
                return (Append<byte>) JsonByte.Append;

            if (type == typeof(char))
                return (Append<char>) JsonChar.Append;

            if (type == typeof(int))
                return (Append<int>) JsonInt.Append;

            if (type == typeof(long))
                return (Append<long>) JsonLong.Append;

            if (type == typeof(float))
                return (Append<float>) JsonFloat.Append;

            if (type == typeof(double))
                return (Append<double>) JsonDouble.Append;

            if (type == typeof(string))
                return (Append<string>) JsonString.Append;

            if (type.IsNullable())
                return JsonNullable.Append<T>();

            if (type.IsEnum)
                return JsonEnum.Append<T>();

            if (type.IsArray)
                return JsonArray.Append<T>();

            if (type.Is(typeof(List<>)))
                return JsonList.Append<T>();
            
            if (type.Is(typeof(Dictionary<,>)))
                return JsonDictionary.Append<T>();

            if (type.Is(typeof(IEnumerable<>)) || type.Implements(typeof(IEnumerable<>)))
                return JsonEnumerable.Append<T>();

            return (Append<T>) JsonObject<T>.Append;
        }

        private static object NewUnwrap()
        {
            var type = typeof(T);

            if (type == typeof(bool))
                return (Unwrap<bool>) JsonBool.Unwrap;

            if (type == typeof(byte))
                return (Unwrap<byte>) JsonByte.Unwrap;

            if (type == typeof(char))
                return (Unwrap<char>) JsonChar.Unwrap;

            if (type == typeof(int))
                return (Unwrap<int>) JsonInt.Unwrap;

            if (type == typeof(long))
                return (Unwrap<long>) JsonLong.Unwrap;

            if (type == typeof(float))
                return (Unwrap<float>) JsonFloat.Unwrap;

            if (type == typeof(double))
                return (Unwrap<double>) JsonDouble.Unwrap;

            if (type == typeof(string))
                return (Unwrap<string>) JsonString.Unwrap;
            
            if (type.IsNullable())
                return JsonNullable.GenerateUnwrap<T>();
            
            if (type.IsEnum)
                return JsonEnum.GenerateUnwrap<T>();
            
            if (type.IsArray)
                return JsonArray.GenerateUnwrap<T>();
            
            if (type.Is(typeof(List<>)))
                return JsonList.GenerateUnwrap<T>();
            
            if (type.Is(typeof(HashSet<>)))
                return JsonHashSet.GenerateUnwrap<T>();
            
            if (type.Is(typeof(Dictionary<,>)))
                return JsonDictionary.GenerateUnwrap<T>();

            return (Unwrap<T>) JsonObject<T>.Unwrap;
        }
    }

    internal static class Json
    {
        private static class Cache
        {
            // TODO: Probably need to cache this better somehow.
            private static readonly ConcurrentDictionary<Type, Append<object>> Appends = new ConcurrentDictionary<Type, Append<object>>();
            private static readonly ConcurrentDictionary<Type, Unwrap<object>> Unwraps = new ConcurrentDictionary<Type, Unwrap<object>>();
            
            public static Append<object> Append(Type type) =>
                Appends.One(type).OrNew(() => Generate.Append(type));

            public static Unwrap<object> Unwrap(Type type) =>
                Unwraps.One(type).OrNew(() => Generate.Unwrap(type));
        }

        public static void Append(Type type, object value, StringBuffer buffer) =>
            Cache.Append(type)(value, buffer);

        public static object Unwrap(Type type, JsonSpan json) =>
            Cache.Unwrap(type)(json);
    }
}