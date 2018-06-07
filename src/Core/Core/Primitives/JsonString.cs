using System;

namespace Pocket.Json
{
    internal static class JsonString
    {
        public static void Append(string value, StringBuffer buffer)
        {
            buffer.Append('"').Append(value).Append('"');
        }

        public static string Unwrap(StringSpan json)
        {
            if (json.IsEmpty()) throw new ArgumentException("Cannot unwrap empty json to string", nameof(json));
            if (json.Length == 1)
                throw new ArgumentException("Cannot unwrap single character json to string", nameof(json));
            if (json[0] != '"' || json[json.Length - 1] != '"')
                throw new ArgumentException(
                    "Specified string \"" + json + "\" must have open and close quotes characters.", nameof(json));

            return json.SubSpan(1, json.Length - 2).ToString();
        }
    }
}