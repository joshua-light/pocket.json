namespace Castalia
{
    internal static class JsonByte
    {
        public static void Append(byte value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static byte Unwrap(StringSpan json)
        {
            if (json.Length == 1)
                return (byte) (json.Source[json.Offset + 0] - '0');

            if (json.Length == 2)
                return (byte) ((json.Source[json.Offset + 0] - '0') * 10
                    + (json.Source[json.Offset + 1] - '0'));

            return (byte) ((json.Source[json.Offset + 0] - '0') * 100
                + (json.Source[json.Offset + 1] - '0') * 10
                + (json.Source[json.Offset + 2] - '0'));
        }
    }
}