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

        public static T Unwrap(JsonSpan json)
        {
            if (json.Span[0] == '{' && json.Span[1] == '}')
            {
                json.Skip(2);
                return Constructor();
            }
            
            var fieldByName = FieldByNameHashCode;
            var instance = Constructor();
            
            json.Skip(1); // Skip '{'.

            while (true)
            {
                var name = json.NextName();
                
                if (!fieldByName.ContainsKey(name.GetHashCode()))
                    throw new Exception($"Couldn't find {name} field.");
                
                var field = fieldByName[name.GetHashCode()];

                json.Skip(1); // Skip ':'.

                field.Write(instance, json);

                if (json.Char == '}')
                {
                    json.Skip(1);
                    break;
                }

                json.Skip(1); // Skip ','.
            }

            return instance;
        }
    }
}