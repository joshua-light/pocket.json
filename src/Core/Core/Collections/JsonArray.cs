using System;
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
            if (json[0] == '[' && json[1] == ']')
                return Array.Empty<T>();
            
            json = json.Cut(1, 1); // Cut '[' and ']'.

            var jsonSpan = JsonArray.JsonSpan;
            jsonSpan.Json = json;

            var length = 0;
            var ch = json[0];
            
            switch (ch)
            {
                case '{': length = LengthOfObjectsArray(jsonSpan); break;
                case '[': length = LengthOfArraysArray(jsonSpan); break;
                case '"': length = LengthOfStringsArray(jsonSpan); break;
                default: length = LengthOfPrimitivesArray(jsonSpan); break;
            }
            
            var result = new T[length];
            
            jsonSpan.Json = json;
            
            switch (ch)
            {
                case '{': UnwrapAsObjectsArray(jsonSpan, result); break;
                case '[': UnwrapAsArraysArray(jsonSpan, result); break;
                case '"': UnwrapAsStringsArray(jsonSpan, result); break;
                default: UnwrapAsPrimitivesArray(jsonSpan, result); break;
            }
            
            return result;
        }

        private static int LengthOfObjectsArray(JsonStringSpan jsonSpan)
        {
            var length = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                length++;
                
                if (json.Length <= 0)
                    break;
            }
            
            return length;
        }
        
        private static void UnwrapAsObjectsArray(JsonStringSpan jsonSpan, T[] array)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                array[i++] = span.AsJson<T>();
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static int LengthOfArraysArray(JsonStringSpan jsonSpan)
        {
            var length = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                length++;
                
                if (json.Length <= 0)
                    break;
            }
            
            return length;
        }
        
        private static void UnwrapAsArraysArray(JsonStringSpan jsonSpan, T[] array)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                array[i++] = span.AsJson<T>();
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        private static int LengthOfStringsArray(JsonStringSpan jsonSpan)
        {
            var length = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                length++;
                
                if (json.Length <= 0)
                    break;
            }
            
            return length;
        }
        
        private static void UnwrapAsStringsArray(JsonStringSpan jsonSpan, T[] array)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                array[i++] = span.AsJson<T>();
                
                if (json.Length <= 0)
                    break;
            }
        }

        private static int LengthOfPrimitivesArray(JsonStringSpan jsonSpan)
        {
            var length = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.

                length++;
                
                if (json.Length <= 0)
                    break;
            }
            
            return length;
        }
        
        private static void UnwrapAsPrimitivesArray(JsonStringSpan jsonSpan, T[] array)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                array[i++] = span.AsJson<T>();
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        #endregion
    }

    internal static class JsonArray
    {
        [ThreadStatic]
        private static JsonStringSpan _jsonSpan;
        public static JsonStringSpan JsonSpan => _jsonSpan ?? (_jsonSpan = new JsonStringSpan());
        
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