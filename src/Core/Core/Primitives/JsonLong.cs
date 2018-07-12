namespace Pocket.Json
{
    internal static class JsonLong
    {
        public static void Append(long value, StringBuffer buffer)
        {
            buffer.Append(value);
        }
        
        public static long Unwrap(JsonSpan json) => Unwrap(json.NextPrimitive());

        public static long Unwrap(StringSpan json)
        {
            var source = json.Source;
            var offset = json.Offset;
            
            var multiplier = 1;
            if (source[offset + 0] == '-')
            {
                multiplier = -1;
                json.SkipMutable(1);
            }

            long value;
            switch (json.Length)
            {
                case 1:
                    value = source[offset + 0] - '0';
                    break;
                case 2:
                    value = (source[offset + 0] - '0') * 10
                        + (source[offset + 1] - '0');
                    break;
                case 3:
                    value = (source[offset + 0] - '0') * 100
                        + (source[offset + 1] - '0') * 10
                        + (source[offset + 2] - '0');
                    break;
                case 4:
                    value = (source[offset + 0] - '0') * 1000
                        + (source[offset + 1] - '0') * 100
                        + (source[offset + 2] - '0') * 10
                        + (source[offset + 3] - '0');
                    break;
                case 5:
                    value = (source[offset + 0] - '0') * 10000
                        + (source[offset + 1] - '0') * 1000
                        + (source[offset + 2] - '0') * 100
                        + (source[offset + 3] - '0') * 10
                        + (source[offset + 4] - '0');
                    break;
                case 6:
                    value = (source[offset + 0] - '0') * 100000
                        + (source[offset + 1] - '0') * 10000
                        + (source[offset + 2] - '0') * 1000
                        + (source[offset + 3] - '0') * 100
                        + (source[offset + 4] - '0') * 10
                        + (source[offset + 5] - '0');
                    break;
                case 7:
                    value = (source[offset + 0] - '0') * 1000000
                        + (source[offset + 1] - '0') * 100000
                        + (source[offset + 2] - '0') * 10000
                        + (source[offset + 3] - '0') * 1000
                        + (source[offset + 4] - '0') * 100
                        + (source[offset + 5] - '0') * 10
                        + (source[offset + 6] - '0');
                    break;
                case 8:
                    value = (source[offset + 0] - '0') * 10000000
                        + (source[offset + 1] - '0') * 1000000
                        + (source[offset + 2] - '0') * 100000
                        + (source[offset + 3] - '0') * 10000
                        + (source[offset + 4] - '0') * 1000
                        + (source[offset + 5] - '0') * 100
                        + (source[offset + 6] - '0') * 10
                        + (source[offset + 7] - '0');
                    break;
                case 9:
                    value = (source[offset + 0] - '0') * 100000000
                        + (source[offset + 1] - '0') * 10000000
                        + (source[offset + 2] - '0') * 1000000
                        + (source[offset + 3] - '0') * 100000
                        + (source[offset + 4] - '0') * 10000
                        + (source[offset + 5] - '0') * 1000
                        + (source[offset + 6] - '0') * 100
                        + (source[offset + 7] - '0') * 10
                        + (source[offset + 8] - '0');
                    break;
                case 10:
                    value = (source[offset + 0] - '0') * 1000000000L
                        + (source[offset + 1] - '0') * 100000000
                        + (source[offset + 2] - '0') * 10000000
                        + (source[offset + 3] - '0') * 1000000
                        + (source[offset + 4] - '0') * 100000
                        + (source[offset + 5] - '0') * 10000
                        + (source[offset + 6] - '0') * 1000
                        + (source[offset + 7] - '0') * 100
                        + (source[offset + 8] - '0') * 10
                        + (source[offset + 9] - '0');
                    break;
                case 11:
                    value = (source[offset + 0] - '0') * 10000000000L
                        + (source[offset + 1] - '0') * 1000000000L
                        + (source[offset + 2] - '0') * 100000000
                        + (source[offset + 3] - '0') * 10000000
                        + (source[offset + 4] - '0') * 1000000
                        + (source[offset + 5] - '0') * 100000
                        + (source[offset + 6] - '0') * 10000
                        + (source[offset + 7] - '0') * 1000
                        + (source[offset + 8] - '0') * 100
                        + (source[offset + 9] - '0') * 10
                        + (source[offset + 10] - '0');
                    break;
                case 12:
                    value = (source[offset + 0] - '0') * 100000000000L
                        + (source[offset + 1] - '0') * 10000000000L
                        + (source[offset + 2] - '0') * 1000000000L
                        + (source[offset + 3] - '0') * 100000000
                        + (source[offset + 4] - '0') * 10000000
                        + (source[offset + 5] - '0') * 1000000
                        + (source[offset + 6] - '0') * 100000
                        + (source[offset + 7] - '0') * 10000
                        + (source[offset + 8] - '0') * 1000
                        + (source[offset + 9] - '0') * 100
                        + (source[offset + 10] - '0') * 10
                        + (source[offset + 11] - '0');
                    break;
                case 13:
                    value = (source[offset + 0] - '0') * 1000000000000L
                        + (source[offset + 1] - '0') * 100000000000L
                        + (source[offset + 2] - '0') * 10000000000L
                        + (source[offset + 3] - '0') * 1000000000L
                        + (source[offset + 4] - '0') * 100000000
                        + (source[offset + 5] - '0') * 10000000
                        + (source[offset + 6] - '0') * 1000000
                        + (source[offset + 7] - '0') * 100000
                        + (source[offset + 8] - '0') * 10000
                        + (source[offset + 9] - '0') * 1000
                        + (source[offset + 10] - '0') * 100
                        + (source[offset + 11] - '0') * 10
                        + (source[offset + 12] - '0');
                    break;
                case 14:
                    value = (source[offset + 0] - '0') * 10000000000000L
                        + (source[offset + 1] - '0') * 1000000000000L
                        + (source[offset + 2] - '0') * 100000000000L
                        + (source[offset + 3] - '0') * 10000000000L
                        + (source[offset + 4] - '0') * 1000000000L
                        + (source[offset + 5] - '0') * 100000000
                        + (source[offset + 6] - '0') * 10000000
                        + (source[offset + 7] - '0') * 1000000
                        + (source[offset + 8] - '0') * 100000
                        + (source[offset + 9] - '0') * 10000
                        + (source[offset + 10] - '0') * 1000
                        + (source[offset + 11] - '0') * 100
                        + (source[offset + 12] - '0') * 10
                        + (source[offset + 13] - '0');
                    break;
                case 15:
                    value = (source[offset + 0] - '0') * 100000000000000L
                        + (source[offset + 1] - '0') * 10000000000000L
                        + (source[offset + 2] - '0') * 1000000000000L
                        + (source[offset + 3] - '0') * 100000000000L
                        + (source[offset + 4] - '0') * 10000000000L
                        + (source[offset + 5] - '0') * 1000000000L
                        + (source[offset + 6] - '0') * 100000000
                        + (source[offset + 7] - '0') * 10000000
                        + (source[offset + 8] - '0') * 1000000
                        + (source[offset + 9] - '0') * 100000
                        + (source[offset + 10] - '0') * 10000
                        + (source[offset + 11] - '0') * 1000
                        + (source[offset + 12] - '0') * 100
                        + (source[offset + 13] - '0') * 10
                        + (source[offset + 14] - '0');
                    break;
                case 16:
                    value = (source[offset + 0] - '0') * 1000000000000000L
                        + (source[offset + 1] - '0') * 100000000000000L
                        + (source[offset + 2] - '0') * 10000000000000L
                        + (source[offset + 3] - '0') * 1000000000000L
                        + (source[offset + 4] - '0') * 100000000000L
                        + (source[offset + 5] - '0') * 10000000000L
                        + (source[offset + 6] - '0') * 1000000000L
                        + (source[offset + 7] - '0') * 100000000
                        + (source[offset + 8] - '0') * 10000000
                        + (source[offset + 9] - '0') * 1000000
                        + (source[offset + 10] - '0') * 100000
                        + (source[offset + 11] - '0') * 10000
                        + (source[offset + 12] - '0') * 1000
                        + (source[offset + 13] - '0') * 100
                        + (source[offset + 14] - '0') * 10
                        + (source[offset + 15] - '0');
                    break;
                case 17:
                    value = (source[offset + 0] - '0') * 10000000000000000L
                        + (source[offset + 1] - '0') * 1000000000000000L
                        + (source[offset + 2] - '0') * 100000000000000L
                        + (source[offset + 3] - '0') * 10000000000000L
                        + (source[offset + 4] - '0') * 1000000000000L
                        + (source[offset + 5] - '0') * 100000000000L
                        + (source[offset + 6] - '0') * 10000000000L
                        + (source[offset + 7] - '0') * 1000000000L
                        + (source[offset + 8] - '0') * 100000000
                        + (source[offset + 9] - '0') * 10000000
                        + (source[offset + 10] - '0') * 1000000
                        + (source[offset + 11] - '0') * 100000
                        + (source[offset + 12] - '0') * 10000
                        + (source[offset + 13] - '0') * 1000
                        + (source[offset + 14] - '0') * 100
                        + (source[offset + 15] - '0') * 10
                        + (source[offset + 16] - '0');
                    break;
                case 18:
                    value = (source[offset + 0] - '0') * 100000000000000000L
                        + (source[offset + 1] - '0') * 10000000000000000L
                        + (source[offset + 2] - '0') * 1000000000000000L
                        + (source[offset + 3] - '0') * 100000000000000L
                        + (source[offset + 4] - '0') * 10000000000000L
                        + (source[offset + 5] - '0') * 1000000000000L
                        + (source[offset + 6] - '0') * 100000000000L
                        + (source[offset + 7] - '0') * 10000000000L
                        + (source[offset + 8] - '0') * 1000000000L
                        + (source[offset + 9] - '0') * 100000000
                        + (source[offset + 10] - '0') * 10000000
                        + (source[offset + 11] - '0') * 1000000
                        + (source[offset + 12] - '0') * 100000
                        + (source[offset + 13] - '0') * 10000
                        + (source[offset + 14] - '0') * 1000
                        + (source[offset + 15] - '0') * 100
                        + (source[offset + 16] - '0') * 10
                        + (source[offset + 17] - '0');
                    break;
                default:
                    value = (source[offset + 0] - '0') * 1000000000000000000L
                        + (source[offset + 1] - '0') * 100000000000000000L
                        + (source[offset + 2] - '0') * 10000000000000000L
                        + (source[offset + 3] - '0') * 1000000000000000L
                        + (source[offset + 4] - '0') * 100000000000000L
                        + (source[offset + 5] - '0') * 10000000000000L
                        + (source[offset + 6] - '0') * 1000000000000L
                        + (source[offset + 7] - '0') * 100000000000L
                        + (source[offset + 8] - '0') * 10000000000L
                        + (source[offset + 9] - '0') * 1000000000L
                        + (source[offset + 10] - '0') * 100000000
                        + (source[offset + 11] - '0') * 10000000
                        + (source[offset + 12] - '0') * 1000000
                        + (source[offset + 13] - '0') * 100000
                        + (source[offset + 14] - '0') * 10000
                        + (source[offset + 15] - '0') * 1000
                        + (source[offset + 16] - '0') * 100
                        + (source[offset + 17] - '0') * 10
                        + (source[offset + 18] - '0');
                    break;
            }

            return value * multiplier;
        }
    }
}