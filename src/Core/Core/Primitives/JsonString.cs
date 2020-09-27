using System;

namespace Pocket.Json
{
    internal static class JsonString
    {
        public static string Read(ref StringSpan json)
        {
            var span = json;
            var source = span.Source;
            var i = span.Start + 1;

            while (true)
            {
                if (source[i] == '"')
                {
                    span.End = i;
                    json.Start = i + 1;
                    
                    break;
                }

                i++;
            }

            // Skip `"`;
            span.Start += 1;
            
            var x = new string(span.Source.AsSpan().Slice(span.Start, span.End - span.Start));

            return x;
        }
        
        public static void Write(string value, StringBuffer buffer) =>
            buffer.Write('"').Write(value).Write('"');
    }
}