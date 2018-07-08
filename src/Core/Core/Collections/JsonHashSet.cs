using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal class JsonHashSet<T>
    {
        public static HashSet<T> Unwrap(JsonSpan json)
        {
            var list = JsonList<T>.Unwrap(json);
            
            return new HashSet<T>(list);
        }
    }
    
    internal static class JsonHashSet
    {
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonHashSet<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}