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

        #region Append

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

        #endregion

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

            public void Write(T value, StringSpan json)
            {
                var field = _unwrap(json);
                _writeField(value, field);
            }
        }

        #region Unwrap

        // Static instance that is used only in `Unwrap` method.
        private static readonly JsonStringSpan JsonSpan = new JsonStringSpan();

        public static T Unwrap(StringSpan json)
        {
            if (json[0] != '{' || json[json.Length - 1] != '}')
                throw new ArgumentException($"Specified json \"{json}\" must have open {'{'} and close {'}'} brackets.", nameof(json));

            var result = Constructor();

            json = json.Cut(1, 1); // Skip '{' and '}'.

            JsonSpan.Json = json;

            var reader = JsonSpan;
            var fieldByName = FieldByNameHashCode;

            var span = StringSpan.Zero;
            while (reader.NextName(ref span))
            {
                var hashCode = span.GetHashCode();
                var field = fieldByName[hashCode];
                
                reader.Json.SkipMutable(1); // Skip ':'.
                var value = reader.NextValue();

                field.Write(result, value);
            }

            return result;
        }

        #endregion
    }
}