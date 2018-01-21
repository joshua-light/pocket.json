using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Castalia
{
    internal static class JsonObject<T>
    {
        private static readonly JsonField[] Fields;
        private static readonly Func<T> Constructor;
        private static readonly Dictionary<int, JsonField> FieldByNameHashCode;

        static JsonObject()
        {
            Constructor = JsonCodeGen.NewObj<T>();

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

                _readField = JsonCodeGen.ReadField<T>(field);
                _writeField = JsonCodeGen.WriteField<T>(field);

                _append = JsonCodeGen.Append(field.FieldType);
                _unwrap = JsonCodeGen.Unwrap(field.FieldType);
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

        private class JsonReader
        {
            public StringSpan Json;

            public bool NextName(ref StringSpan span)
            {
                if (Json.Length <= 0)
                    return false;

                Json.SkipMutable(1); // Skips '"'.

                var json = Json;
                var remainder = json.Length % 2;
                var length = json.Length - remainder;

                for (var i = 0; i < length; i += 2)
                {
                    var a = json.Source[json.Offset + i];
                    if (a == '"')
                    {
                        json.Length = i;
                        span = json;
                        break;
                    }

                    var b = json.Source[json.Offset + i + 1];
                    if (b == '"')
                    {
                        json.Length = i + 1;
                        span = json;
                        break;
                    }
                }

                Json.SkipMutable(span.Length + 1);

                return true;
            }

            public StringSpan NextField()
            {
                var json = Json;
                json.SkipMutable(1); // Skips ':'.

                var span = json;
                var ch = span.Source[span.Offset];

                if (ch == '{')
                    NextObject(ref span);
                else if (ch == '"')
                    NextString(ref span);
                else
                    NextValue(ref span);

                json.SkipMutable(span.Length + 1); // Skip ','.

                Json = json;

                return span;
            }

            private static void NextObject(ref StringSpan json)
            {
                var stack = 0;

                for (var i = 0; i < json.Length; i++)
                    if (json[i] == '{')
                    {
                        stack++;
                    }
                    else if (json[i] == '}' && stack-- == 1)
                    {
                        json = json.SubSpan(i + 1);
                        break;
                    }
            }

            private static void NextString(ref StringSpan json)
            {
                for (var i = 1; i < json.Length; i++)
                {
                    if (json[i] != '"')
                        continue;

                    json = json.SubSpan(i + 1);
                    break;
                }
            }

            private static void NextValue(ref StringSpan json)
            {
                var remainder = json.Length % 2;
                var length = json.Length - remainder;

                for (var i = 0; i < length; i += 2)
                {
                    var a = json.Source[json.Offset + i];
                    if (a == ',')
                    {
                        json.Length = i;
                        break;
                    }

                    var b = json.Source[json.Offset + i + 1];
                    if (b == ',')
                    {
                        json.Length = i + 1;
                        break;
                    }
                }
            }
        }

        // Static instance that is used only in `Unwrap` method.
        private static readonly JsonReader Reader = new JsonReader();

        public static T Unwrap(StringSpan json)
        {
            if (json[0] != '{' || json[json.Length - 1] != '}')
                throw new ArgumentException(
                    "Specified json \"" + json + "\" must have open '{' and close '}' brackets.", nameof(json));

            var result = Constructor();

            json = json.Cut(1, 1); // Skip '{' and '}'.

            Reader.Json = json;

            var reader = Reader;
            var fieldByName = FieldByNameHashCode;

            var span = StringSpan.Zero;
            while (reader.NextName(ref span))
            {
                var hashCode = span.GetHashCode();
                var field = fieldByName[hashCode];
                var value = reader.NextField();

                field.Write(result, value);
            }

            return result;
        }

        #endregion
    }
}