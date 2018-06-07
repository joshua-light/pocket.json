using System;
using System.Reflection;

namespace Castalia
{
    internal static class TypeExtensions
    {
        public static bool IsNullable(this Type self) => self.IsGeneric(typeof(Nullable<>));

        public static bool IsGeneric(this Type self, Type genericType)
        {
            var type = self.GetTypeInfo();
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }
    }
}