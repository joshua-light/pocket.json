using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Pocket.Common;

namespace Pocket.Json
{
    internal static class JsonObject<T>
    {
        private class JsonField
        {
            private readonly string _formattedFieldName;

            private readonly Func<T, object> _readField;
            private readonly Action<T, object> _writeField;
            
            private readonly Read<object> _read;

            public JsonField(FieldInfo field, JsonAttribute json) : this(field, json.Name ?? field.Name) { }
            public JsonField(FieldInfo field, string name)
            {
                _formattedFieldName = $"\"{name}\":";
                
                _readField = Emit.GetField<T>(field);
                _writeField = Emit.SetField<T>(field);

                _read = Generate.Read(field.FieldType);
                
                Name = name;
            }

            public string Name { get; }

            public object ValueOf(T instance) =>
                _readField(instance);

            public void Write(object fieldValue, StringBuffer buffer)
            {
                buffer.Write(_formattedFieldName);
                Json.Write(fieldValue.GetType(), fieldValue, buffer);
            }
            
            public void Write(T value, ref StringSpan json)
            {
                var field = _read(ref json);
                
                _writeField(value, field);
            }
        }

        private static readonly List<JsonField> Fields;
        private static readonly Func<T> New;
        private static readonly Dictionary<int, JsonField> FieldByNameHashCode;

        static JsonObject()
        {
            New = Emit.Ctor<T>();
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
        
        public static T Read(ref StringSpan json)
        {
            if (json.CharAt(0) == '{' && json.CharAt(1) == '}')
            {
                json.Start += 2;
                
                return New();
            }
            
            var fieldByName = FieldByNameHashCode;
            var instance = New();
            
            // Skip '{"' (start of object and field name).
            json.Start += 2;

            while (true)
            {
                var name = json;
            
                var start = name.Start;
                var source = name.Source;
                var i = start;

                while (true)
                {
                    if (source[i] == '"')
                    {
                        name.End = i;
                        
                        // Skip '":'.
                        json.Start = i + 2;
                        break;
                    }

                    i++;
                }

                if (fieldByName.TryGetValue(name.GetHashCode(), out var field))
                    field.Write(instance, ref json);
                else
                    SkipValue(ref json);

                if (json.CharAt(0) == '}')
                {
                    json.Start++;
                    break;
                }

                // Skip ',"'.
                json.Start += 2;
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
            var i = 1;
            var copy = span;
            
            span.Start++;

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

            span.Start += i;
        }

        private static void SkipString(ref StringSpan span) =>
            JsonString.Read(ref span);

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
        
        public static void Write(T instance, StringBuffer buffer)
        {
            buffer.Write('{');

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
                    buffer.Write(',');
                
                fields[i].Write(value, buffer);
                lastAppended = true;
            }

            buffer.Write('}');
        }
    }
}