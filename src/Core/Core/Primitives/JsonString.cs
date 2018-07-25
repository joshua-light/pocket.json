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
            span.Start++;
            span.End--;
            
            return span.ToString();
        }
    }
}