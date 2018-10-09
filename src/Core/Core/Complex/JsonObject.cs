using System;
using System.Collections.Generic;
using System.Reflection;
using Pocket.Common;

namespace Pocket.Json
{
    internal static class JsonObject<T>
    {
        private static readonly JsonField[] Fields;
        private static readonly Func<T> Constructor;
        private static readonly Dictionary<int, JsonField> FieldByNameHashCode;

        static JsonObject()
        {
            Constructor = Emit.Ctor<T>();

            var fields = new List<FieldInfo>();
            GatherFields(typeof(T).GetTypeInfo(), fields);

            Fields = new JsonField[fields.Count];
            for (var i = 0; i < fields.Count; i++)
                Fields[i] = new JsonField(fields[i]);

            FieldByNameHashCode = new Dictionary<int, JsonField>(Fields.Length);
            for (var i = 0; i < Fields.Length; i++)
                FieldByNameHashCode[StringSpan.GetHashCode(Fields[i].Name)] = Fields[i];
        }

        private static void GatherFields(TypeInfo type, List<FieldInfo> fields)
        {
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                fields.Add(field);
        }
        
        private class JsonField
        {
            private readonly string _formattedFieldName;

            private readonly Func<T, object> _readField;
            private readonly Unwrap<object> _unwrap;
            private readonly Action<T, object> _writeField;

            public JsonField(FieldInfo field)
            {
                Name = field.Name;
                
                _formattedFieldName = $"\"{field.Name}\":";
                
                _readField = Emit.GetField<T>(field);
                _writeField = Emit.SetField<T>(field);

                _unwrap = Generate.Unwrap(field.FieldType);
            }

            public string Name { get; }

            public bool Append(T value, StringBuffer buffer)
            {
                var fieldValue = _readField(value);
                if (fieldValue == null)
                    return false;
                
                buffer.Append(_formattedFieldName);
                Json.Append(fieldValue.GetType(), fieldValue, buffer);

                return true;
            }
            
            public void Write(T value, JsonSpan json)
            {
                var field = _unwrap(json);
                
                _writeField(value, field);
            }
        }

        public static void Append(T value, StringBuffer buffer)
        {
            buffer.Append('{');

            var fields = Fields;
            for (int i = 0, length = fields.Length; i < length; i++)
            {
                var appended = fields[i].Append(value, buffer);
                if (appended && i != fields.Length - 1)
                    buffer.Append(',');
            }

            buffer.Append('}');
        }

        public static T Unwrap(JsonSpan json) => Unwrap(json, ref json.Span);

        private static T Unwrap(JsonSpan json, ref StringSpan span)
        {
            if (span.CharAt(0) == '{' && span.CharAt(1) == '}')
            {
                span.Start += 2;
                return Constructor();
            }
            
            var fieldByName = FieldByNameHashCode;
            var instance = Constructor();
            
            // Skip '{"' (start of object and field name).
            span.Start += 2;

            while (true)
            {
                // This is manually unrolled `JsonSpan.NextName()` method call.
                var name = json.Span;
            
                var start = name.Start;
                var source = name.Source;
                var i = start;

                while (true)
                {
                    if (source[i] == '"')
                    {
                        name.End = i;
                        
                        // Skip '":'.
                        span.Start = i + 2;
                        break;
                    }

                    i++;
                }
                
                #if DEBUG
                
                if (!fieldByName.ContainsKey(name.GetHashCode()))
                    throw new Exception($"Couldn't find \"{name}\" field.");
                
                #endif
                
                var field = fieldByName[name.GetHashCode()];

                field.Write(instance, json);

                if (span.CharAt(0) == '}')
                {
                    span.Start++;
                    break;
                }

                // Skip ',"'.
                span.Start += 2;
            }

            return instance;
        }
    }
}