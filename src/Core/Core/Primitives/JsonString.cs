namespace Pocket.Json
{
    internal static class JsonString
    {
        public static void Append(string value, StringBuffer buffer)
        {
            buffer.Append('"').AppendEscaped(value).Append('"');
        }

        public static string Unwrap(JsonSpan json)
        {
            var escapesCount = 0;
            var span = json.Span;
            var start = span.Start;
            var source = span.Source;
            var i = start + 1;

            while (true)
            {
                if (source[i] == '\\')
                {
                    escapesCount++;
                }
                else if (source[i] == '"' && source[i - 1] != '\\')
                {
                    span.End = i + 1;
                    json.Span.Start = i + 1;
                    break;
                }

                i++;
            }

            i = 0;
            var chars = new char[span.End - span.Start - escapesCount - 2]; // 2 is '"' quotes.
            
            // TODO: Add support for more characters.
            for (var j = span.Start + 1; j < span.End - 1; j++)
                if (source[j] != '\\')
                    chars[i++] = source[j];

            return new string(chars);
        }
    }
}