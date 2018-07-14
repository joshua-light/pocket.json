using System;

namespace Pocket.Json
{
    internal static class JsonString
    {
        public static void Append(string value, StringBuffer buffer)
        {
            buffer.Append('"').Append(value).Append('"');
        }

        public static string Unwrap(JsonSpan json)
        {
            var span = json.NextString();

            if (span.IsEmpty()) throw new ArgumentException("Cannot unwrap empty json to string", nameof(span));
            if (span.Length == 1)
                throw new ArgumentException("Cannot unwrap single character json to string", nameof(span));
            if (span.CharAt(0) != '"' || span.LastCharAt(0) != '"')
                throw new ArgumentException(
                    $"Specified string \"{span}\" must have open and close quotes characters.", nameof(span));
            
            return span.SubSpan(1, span.Length - 2).ToString();
        }
    }
}