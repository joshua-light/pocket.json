using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    internal class JsonHashSet<T>
    {
        #region Unwrap
        
        public static HashSet<T> Unwrap(StringSpan json)
        {
            var list = JsonList<T>.Unwrap(json);
            return new HashSet<T>(list);
            
            if (json[0] == '[' && json[1] == ']')
                return new HashSet<T>();
            
            json = json.Cut(1, 1); // Cut '[' and ']'.

            var jsonSpan = JsonHashSet.JsonSpan;
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

            var result = new HashSet<T>();
            
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UnwrapAsObjectsArray(JsonStringSpan jsonSpan, HashSet<T> list)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UnwrapAsArraysArray(JsonStringSpan jsonSpan, HashSet<T> list)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UnwrapAsStringsArray(JsonStringSpan jsonSpan, HashSet<T> list)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UnwrapAsPrimitivesArray(JsonStringSpan jsonSpan, HashSet<T> list)
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
    
    internal static class JsonHashSet
    {
        [ThreadStatic]
        private static JsonStringSpan _jsonSpan;
        public static JsonStringSpan JsonSpan => _jsonSpan ?? (_jsonSpan = new JsonStringSpan());
        
        public static Unwrap<T> GenerateUnwrap<T>()
        {
            var type = typeof(JsonHashSet<>).MakeGenericType(typeof(T).GetTypeInfo().GenericTypeArguments[0]);
            var method = type.GetTypeInfo().GetDeclaredMethod("Unwrap");

            return (Unwrap<T>) method.CreateDelegate(typeof(Unwrap<T>));
        }
    }
}