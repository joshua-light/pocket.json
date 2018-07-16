using System;
using System.Collections.Generic;
using System.Linq;
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

            var fields = typeof(T).GetTypeInfo().DeclaredFields
                .Where(x => x.IsPublic && !x.IsStatic)
                .ToArray();

            Fields = new JsonField[fields.Length];
            for (var i = 0; i < fields.Length; i++)
                Fields[i] = new JsonField(fields[i]);

            FieldByNameHashCode = new Dictionary<int, JsonField>(Fields.Length);
            for (var i = 0; i < Fields.Length; i++)
                FieldByNameHashCode[StringSpan.GetHashCode(Fields[i].Name)] = Fields[i];
        }
        
        private class JsonField
        {
            private readonly Append<object> _append;
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

                _append = Generate.Append(field.FieldType);
                _unwrap = Generate.Unwrap(field.FieldType);
            }

            public string Name { get; }

            public void Append(T value, StringBuffer buffer)
            {
                buffer.Append(_formattedFieldName);

                var fieldValue = _readField(value);
                _append(fieldValue, buffer);
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
                fields[i].Append(value, buffer);
                if (i != fields.Length - 1)
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