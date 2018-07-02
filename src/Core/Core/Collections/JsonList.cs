using System;
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
            if (json[0] == '[' && json[1] == ']')
                return new List<T>();
            
            json = json.Cut(1, 1); // Cut '[' and ']'.

            var jsonSpan = JsonList.JsonSpan;
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

            var result = new List<T>(length);
            
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
        
        private static void UnwrapAsObjectsArray(JsonStringSpan jsonSpan, List<T> list)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextObject(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                list.Add(span.AsJson<T>());
                
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
        
        private static void UnwrapAsArraysArray(JsonStringSpan jsonSpan, List<T> list)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextArray(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                list.Add(span.AsJson<T>());
                
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
        
        private static void UnwrapAsStringsArray(JsonStringSpan jsonSpan, List<T> list)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextString(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                list.Add(span.AsJson<T>());
                
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
        
        private static void UnwrapAsPrimitivesArray(JsonStringSpan jsonSpan, List<T> list)
        {
            var i = 0;
            var json = jsonSpan.Json;

            while (true)
            {
                var span = json;
                
                jsonSpan.NextPrimitive(ref span);
                json.SkipMutable(span.Length + 1); // Skip ','.
                
                list.Add(span.AsJson<T>());
                
                if (json.Length <= 0)
                    break;
            }
        }
        
        #endregion
    }

    internal static class JsonList
    {
        [ThreadStatic]
        private static JsonStringSpan _jsonSpan;
        public static JsonStringSpan JsonSpan => _jsonSpan ?? (_jsonSpan = new JsonStringSpan());
        
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