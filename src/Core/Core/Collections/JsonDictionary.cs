using System;
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
            if (json[0] == '{' && json[1] == '}')
                return new Dictionary<TKey, TValue>();
            
            json = json.Cut(1, 1); // Cut '{' and '}'.

            var jsonSpan = JsonDictionary.JsonSpan;
            jsonSpan.Json = json;
            
            var result = new Dictionary<TKey, TValue>();
            
            var keyCh = json[0];
            jsonSpan.NextValue();
            
            var valueCh = jsonSpan.Json[0];
            jsonSpan.Json = json;

            switch (keyCh)
            {
                case '{':
                    switch (valueCh)
                    {
                        case '{': AsDictionaryWithObjectKeysAndObjectValues(jsonSpan, result); break;
                        case '[': AsDictionaryWithObjectKeysAndArrayValues(jsonSpan, result); break;
                        case '"': AsDictionaryWithObjectKeysAndStringValues(jsonSpan, result); break;
                        default: AsDictionaryWithObjectKeysAndPrimitiveValues(jsonSpan, result); break;
                    } break;
                
                case '"': switch (valueCh)
                {
                    case '{': AsDictionaryWithStringKeysAndObjectValues(jsonSpan, result); break;
                    case '[': AsDictionaryWithStringKeysAndArrayValues(jsonSpan, result); break;
                    case '"': AsDictionaryWithStringKeysAndStringValues(jsonSpan, result); break;
                    default: AsDictionaryWithStringKeysAndPrimitiveValues(jsonSpan, result); break;
                } break;
                
                default: switch (valueCh)
                {
                    case '{': AsDictionaryWithPrimitiveKeysAndObjectValues(jsonSpan, result); break;
                    case '[': AsDictionaryWithPrimitiveKeysAndArrayValues(jsonSpan, result); break;
                    case '"': AsDictionaryWithPrimitiveKeysAndStringValues(jsonSpan, result); break;
                    default: AsDictionaryWithPrimitiveKeysAndPrimitiveValues(jsonSpan, result); break;
                } break;
            }
            
            return result;
        }
                
        private static void AsDictionaryWithObjectKeysAndObjectValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }

        private static void AsDictionaryWithObjectKeysAndStringValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithObjectKeysAndArrayValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithObjectKeysAndPrimitiveValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithStringKeysAndObjectValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithStringKeysAndStringValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithStringKeysAndArrayValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithStringKeysAndPrimitiveValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithPrimitiveKeysAndObjectValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithPrimitiveKeysAndStringValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithPrimitiveKeysAndArrayValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static void AsDictionaryWithPrimitiveKeysAndPrimitiveValues(JsonStringSpan jsonSpan, Dictionary<TKey, TValue> dictionary)
        {
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                var key = Json<TKey>.Unwrap(span);
                
                span = json;
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                var value = Json<TValue>.Unwrap(span);
                
                dictionary.Add(key, value);
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        #endregion
    }

    internal static class JsonDictionary
    {
        [ThreadStatic]
        private static JsonStringSpan _jsonSpan;
        public static JsonStringSpan JsonSpan => _jsonSpan ?? (_jsonSpan = new JsonStringSpan());
        
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