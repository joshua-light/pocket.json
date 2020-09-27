using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal class JsonHashSet<T>
    {
        public static HashSet<T> Read(ref StringSpan json)
        {
            var list = JsonList<T>.Read(ref json);
            
            return new HashSet<T>(list);
        }
    }
    
    internal static class JsonHashSet
    {
        public static Read<T> Read<T>()
        {
            var type = typeof(JsonHashSet<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Read");

            return (Read<T>) method.CreateDelegate(typeof(Read<T>));
        }
    }
}