using System;
using System.Collections.Generic;
using System.Reflection;
using Pocket.Common;

namespace Pocket.Json
{
    internal static class JsonObject<T>
    {
        private static readonly List<JsonField> Fields;
        private static readonly Func<T> Constructor;
        private static readonly Dictionary<int, JsonField> FieldByNameHashCode;

        static JsonObject()
        {
            Constructor = Emit.Ctor<T>();
            Fields = FieldsOf(typeof(T));
            FieldByNameHashCode = new Dictionary<int, JsonField>(Fields.Count);
            
            for (var i = 0; i < Fields.Count; i++)
                FieldByNameHashCode[StringSpan.GetHashCode(Fields[i].Name)] = Fields[i];
        }

        private static List<JsonField> FieldsOf(Type type)
        {
            var all = new List<JsonField>();
            
            foreach (var field in type.Fields(_ => _.AllInstance()))
            {
                var attribute = field.Attribute<JsonAttribute>();
                if (attribute == null)
                    continue;

                var jsonField = new JsonField(field, attribute);

                all.Add(jsonField);
            }

            return all;
        }
        
        private class JsonField
        {
            private readonly string _formattedFieldName;

            private readonly Func<T, object> _readField;
            private readonly Action<T, object> _writeField;
            
            private readonly Unwrap<object> _unwrap;

            public JsonField(FieldInfo field, JsonAttribute json) : this(field, json.Name ?? field.Name) { }
            public JsonField(FieldInfo field, string name)
            {
                _formattedFieldName = $"\"{name}\":";
                
                _readField = Emit.GetField<T>(field);
                _writeField = Emit.SetField<T>(field);

                _unwrap = Generate.Unwrap(field.FieldType);
                
                Name = name;
            }

            public string Name { get; }

            public object ValueOf(T instance) =>
                _readField(instance);

            public void Append(object fieldValue, StringBuffer buffer)
            {
                buffer.Append(_formattedFieldName);
                Json.Append(fieldValue.GetType(), fieldValue, buffer);
            }
            
            public void Write(T value, JsonSpan json)
            {
                var field = _unwrap(json);
                
                _writeField(value, field);
            }
        }

        public static void Append(T instance, StringBuffer buffer)
        {
            buffer.Append('{');

            var lastAppended = true;
            var fields = Fields;
            
            for (int i = 0, length = fields.Count; i < length; i++)
            {
                var value = fields[i].ValueOf(instance);
                if (value == null)
                {
                    lastAppended = false;
                    continue;
                }
                
                if (lastAppended && i != 0)
                    buffer.Append(',');
                
                fields[i].Append(value, buffer);
                lastAppended = true;
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
                
                var field = fieldByName.One(name.GetHashCode());
                if (field != null)
                    field.Write(instance, json);
                else
                    SkipValue(ref span);

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

        private static void SkipValue(ref StringSpan span)
        {
            var ch = span.CharAt(0);
            if (ch == '{')
                SkipObject(ref span);
            else if (ch == '"')
                SkipString(ref span);
            else
                SkipPrimitive(ref span);
        }

        private static void SkipObject(ref StringSpan span)
        {
            var brackets = 1;
            var i = 0;
            var copy = span;

            copy.Start++;

            while (true)
            {
                var ch = copy.CharAt(i);
                if (ch == '}')
                    brackets--;
                else if (ch == '{')
                    brackets++;

                if (brackets == 0)
                    break;

                i++;
            }

            span.Start = i;
        }

        private static void SkipString(ref StringSpan span) =>
            JsonString.Read(ref span, out var _);

        private static void SkipPrimitive(ref StringSpan span)
        {
            var copy = span;
            var i = 0;

            while (true)
            {
                var ch = copy.CharAt(i);
                if (ch == ',' || ch == '}')
                    break;

                i++;
            }

            span.Start += i;
        }
    }
}