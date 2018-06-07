using System;
using System.Reflection;

namespace Castalia
{
    internal static class JsonNullable<T> where T : struct
    {
        public static void Append(T? nullable, StringBuffer buffer)
        {
            if (nullable != null) Json<T>.Append(nullable.Value, buffer);
        }
    }

    internal static class JsonNullable
    {
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonNullable<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T)));
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
    }
}