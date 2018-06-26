using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonList<T>
    {
        public static void Append(IList<T> items, StringBuffer buffer)
        {
            buffer.Append('[');

            for (var i = 0; i < items.Count; i++)
            {
                Json<T>.Append(items[i], buffer);
                if (i != items.Count - 1)
                    buffer.Append(',');
            }

            buffer.Append(']');
        }
        
        #region Unwrap
        
        public static List<T> Unwrap(StringSpan json)
        {
            json = json.Cut(1, 1); // Cut '[' and ']'.

            var reader = JsonList.Reader;
            reader.Json = json;

            var length = 0;
            while (reader.NextValue().Length != 0)
                length++;
            
            reader.Json = json;

            var result = new List<T>(length);
            var i = 0;
            
            var span = StringSpan.Zero;
            while ((span = reader.NextValue()).Length != 0)
                result.Add(span.ToString().AsJson<T>());
            
            return result;
        }
        
        #endregion
    }

    internal static class JsonList
    {
        public static readonly JsonReader Reader = new JsonReader();
        
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonList<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}