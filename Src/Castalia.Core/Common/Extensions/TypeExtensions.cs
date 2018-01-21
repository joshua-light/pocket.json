using System;
using System.Reflection;

namespace Castalia
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type self)
        {
            return self.GetTypeInfo().IsGenericType && self.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}