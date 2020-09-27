namespace Pocket.Json
{
    internal static class JsonString
    {
        public static string Read(ref StringSpan json)
        {
            var span = Read(ref json, out var escapes);
            var source = span.Source;

            var chars = new char[span.End - span.Start - escapes - 2]; // 2 is '"' quotes.
            var i = 0;
            
            // TODO: Add support for more characters.
            for (var j = span.Start + 1; j < span.End - 1; j++)
                if (source[j] != '\\')
                    chars[i++] = source[j];

            return new string(chars);
        }

        public static StringSpan Read(ref StringSpan span, out int escapes)
        {
            escapes = 0;
            
            var source = span.Source;
            var result = span;
            var start = result.Start;
            var i = start + 1;

            while (true)
            {
                if (source[i] == '\\')
                {
                    escapes++;
                }
                else if (source[i] == '"' && source[i - 1] != '\\')
                {
                    result.End = i + 1;
                    span.Start = i + 1;
                    break;
                }

                i++;
            }

            return result;
        }
        
        public static void Write(string value, StringBuffer buffer) =>
            buffer.Write('"').WriteEscaped(value).Write('"');
    }
}