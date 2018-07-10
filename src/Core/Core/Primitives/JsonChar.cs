using System;

namespace Pocket.Json
{
    internal static class JsonChar
    {
        public static void Append(char value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static char Unwrap(JsonSpan json)
        {
            var span = json.NextPrimitive();
            if (span.Length == 1)
                return span[0];

            throw new ArgumentException($"Specified string \"{span}\" is not a single character.", nameof(span));
        }
    }
}