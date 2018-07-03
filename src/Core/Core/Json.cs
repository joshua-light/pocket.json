using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pocket.Json
{
    internal delegate void Append<T>(T value, StringBuffer buffer);

    internal delegate T Unwrap<T>(StringSpan json);

    internal static class Json<T>
    {
        private static readonly Append<T> _append = (Append<T>) NewAppend();
        private static readonly Unwrap<T> _unwrap = (Unwrap<T>) NewUnwrap();

        public static void Append(T value, StringBuffer buffer)
        {
            _append(value, buffer);
        }

        public static T Unwrap(StringSpan json)
        {
            return _unwrap(json);
        }

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
                return JsonNullable.GenerateAppend<T>();

            if (type.IsArray)
                return JsonArray.GenerateAppend<T>();

            if (type.IsGeneric(typeof(List<>)))
                return JsonList.GenerateAppend<T>();
            
            if (type.IsGeneric(typeof(Dictionary<,>)))
                return JsonDictionary.GenerateAppend<T>();

            if (type.IsGeneric(typeof(IEnumerable<>))
                || type.GetTypeInfo().ImplementedInterfaces.Any(x => x.IsGeneric(typeof(IEnumerable<>))))
                return JsonEnumerable.GenerateAppend<T>();

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
            
            if (type.IsArray)
                return JsonArray.GenerateUnwrap<T>();
            
            if (type.IsGeneric(typeof(List<>)))
                return JsonList.GenerateUnwrap<T>();
            
            if (type.IsGeneric(typeof(HashSet<>)))
                return JsonHashSet.GenerateUnwrap<T>();
            
            if (type.IsGeneric(typeof(Dictionary<,>)))
                return JsonDictionary.GenerateUnwrap<T>();

            return (Unwrap<T>) JsonObject<T>.Unwrap;
        }
    }
}