using System;

namespace Pocket.Json
{
    internal static class JsonChar
    {
        public static void Append(char value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static char Unwrap(StringSpan json)
        {
            if (json.Length == 1) return json[0];

            throw new ArgumentException("Specified string \"" + json + "\" is not a single character.", nameof(json));
        }
    }
}