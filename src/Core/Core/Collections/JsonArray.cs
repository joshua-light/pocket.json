using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonArray<T>
    {
        public static void Append(T[] items, StringBuffer buffer)
        {
            buffer.Append('[');

            for (var i = 0; i < items.Length; i++)
            {
                Json<T>.Append(items[i], buffer);
                if (i != items.Length - 1)
                    buffer.Append(',');
            }

            buffer.Append(']');
        }
        
        #region Unwrap
        
        public static T[] Unwrap(StringSpan json)
        {           
            json = json.Cut(1, 1); // Cut '[' and ']'.

            var reader = JsonArray.JsonSpan;
            reader.Json = json;

            var length = 0;
            while (reader.NextValue().Length != 0)
                length++;
            
            reader.Json = json;
            
            var result = new T[length];
            var i = 0;
            
            var span = StringSpan.Zero;
            while ((span = reader.NextValue()).Length != 0)
                result[i++] = span.ToString().AsJson<T>();
            
            return result;
        }
        
        #endregion
    }

    internal static class JsonArray
    {
        public static readonly JsonStringSpan JsonSpan = new JsonStringSpan();
        
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonArray<>).MakeGenericType(typeof(T).GetElementType());
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}