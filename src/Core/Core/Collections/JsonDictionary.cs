using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Json
{
    internal static class JsonDictionary<TKey, TValue>
    {
        public static void Append(IDictionary<TKey, TValue> items, StringBuffer buffer)
        {
            buffer.Append('{');

            var i = 0;
            foreach (var item in items)
            {
                Json<TKey>.Append(item.Key, buffer);
                buffer.Append(':');
                Json<TValue>.Append(item.Value, buffer);

                if (i++ != items.Count - 1)
                    buffer.Append(',');
            }

            buffer.Append('}');
        }
        
        #region Unwrap
        
        public static Dictionary<TKey, TValue> Unwrap(StringSpan json)
        {
            json = json.Cut(1, 1); // Cut '{' and '}'.

            var reader = JsonDictionary.JsonSpan;
            reader.Json = json;

            var length = 0;
            while (reader.NextValue().Length != 0)
                length++;
            
            reader.Json = json;

            var result = new Dictionary<TKey, TValue>(length);
            var key = StringSpan.Zero;
            
            while ((key = reader.NextValue()).Length != 0)
            {
                var value = reader.NextValue();
                
                result.Add(key.ToString().AsJson<TKey>(), value.ToString().AsJson<TValue>());
            }
            
            return result;
        }
        
        #endregion
    }

    internal static class JsonDictionary
    {
        public static readonly JsonStringSpan JsonSpan = new JsonStringSpan();
        
        public static Append<T> GenerateAppend<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Append");

            return (Append<T>) method.CreateDelegate(typeof(Append<T>));
        }
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonDictionary<,>).MakeGenericType(
                typeof(T).GetTypeInfo().GenericTypeArguments[0],
                typeof(T).GetTypeInfo().GenericTypeArguments[1]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}