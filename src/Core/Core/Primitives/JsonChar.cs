using System;

namespace Pocket.Json
{
    internal static class JsonChar
    {
        public static char Read(ref StringSpan json)
        {
            var span = json.NextItem();
            if (span.End - span.Start == 1)
                return span.CharAt(0);

            throw new ArgumentException($"Specified string \"{span.ToString()}\" is not a single character.", nameof(span));
        }
        
        public static void Write(char value, StringBuffer buffer) => 
            buffer.Write(value);
    }
}