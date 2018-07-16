namespace Pocket.Json
{
    internal static class JsonByte
    {
        public static void Append(byte value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static byte Unwrap(JsonSpan json) => Unwrap(json.NextPrimitive());

        public static byte Unwrap(StringSpan json)
        {
            var source = json.Source;
            var offset = json.Start;
            var length = json.End - offset;
            
            if (length == 1)
                return (byte) (source[offset] - '0');

            if (length == 2)
                return (byte) ((source[offset] - '0') * 10 + (source[offset + 1] - '0'));

            return (byte) ((source[offset] - '0') * 100
                + (source[offset + 1] - '0') * 10
                + (source[offset + 2] - '0'));
        }
    }
}