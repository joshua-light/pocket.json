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
            var start = span.Start;
            var end = span.End;

            if (start == end)
                throw new ArgumentException("Cannot unwrap empty json to string", nameof(span));
            if (end == 1)
                throw new ArgumentException("Cannot unwrap single character json to string", nameof(span));
            if (span.Source[start] != '"' || span.Source[end - 1] != '"')
                throw new ArgumentException($"Specified string \"{span}\" must have open and close quotes characters.", nameof(span));
            
            span.Start++;
            span.End--;
            
            return span.ToString();
        }
    }
}