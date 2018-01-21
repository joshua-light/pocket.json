using System;

namespace Castalia
{
    internal static class JsonBool
    {
        public static void Append(bool value, StringBuffer buffer)
        {
            buffer.Append(value ? '1' : '0');
        }

        public static bool Unwrap(StringSpan json)
        {
            var ch = json.Source[json.Offset];
            if (ch == '1') return true;
            if (ch == '0') return false;

            throw new ArgumentException("Specified string \"" + json + "\" must be either \"1\" or \"0\".",
                nameof(json));
        }
    }
}