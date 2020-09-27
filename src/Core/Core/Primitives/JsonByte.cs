namespace Pocket.Json
{
    internal static class JsonByte
    {
        public static byte Read(ref StringSpan json)
        {
            var span = json.NextItem();
            var source = span.Source;
            var offset = span.Start;
            var length = span.End - offset;
            
            if (length == 1)
                return (byte) (source[offset] - '0');

            if (length == 2)
                return (byte) ((source[offset] - '0') * 10 + (source[offset + 1] - '0'));

            return (byte) ((source[offset] - '0') * 100
                + (source[offset + 1] - '0') * 10
                + (source[offset + 2] - '0'));
        }

        public static void Write(byte value, StringBuffer buffer) =>
            buffer.Write(value);
    }
}