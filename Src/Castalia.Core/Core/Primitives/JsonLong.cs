namespace Castalia
{
    internal static class JsonLong
    {
        public static void Append(long value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static long Unwrap(StringSpan json)
        {
            var multiplier = 1;

            if (json.Source[json.Offset + 0] == '-')
            {
                multiplier = -1;
                json.SkipMutable(1);
            }

            long value;
            switch (json.Length)
            {
                case 1:
                    value = json.Source[json.Offset + 0] - '0';
                    break;
                case 2:
                    value = (json.Source[json.Offset + 0] - '0') * 10
                        + (json.Source[json.Offset + 1] - '0');
                    break;
                case 3:
                    value = (json.Source[json.Offset + 0] - '0') * 100
                        + (json.Source[json.Offset + 1] - '0') * 10
                        + (json.Source[json.Offset + 2] - '0');
                    break;
                case 4:
                    value = (json.Source[json.Offset + 0] - '0') * 1000
                        + (json.Source[json.Offset + 1] - '0') * 100
                        + (json.Source[json.Offset + 2] - '0') * 10
                        + (json.Source[json.Offset + 3] - '0');
                    break;
                case 5:
                    value = (json.Source[json.Offset + 0] - '0') * 10000
                        + (json.Source[json.Offset + 1] - '0') * 1000
                        + (json.Source[json.Offset + 2] - '0') * 100
                        + (json.Source[json.Offset + 3] - '0') * 10
                        + (json.Source[json.Offset + 4] - '0');
                    break;
                case 6:
                    value = (json.Source[json.Offset + 0] - '0') * 100000
                        + (json.Source[json.Offset + 1] - '0') * 10000
                        + (json.Source[json.Offset + 2] - '0') * 1000
                        + (json.Source[json.Offset + 3] - '0') * 100
                        + (json.Source[json.Offset + 4] - '0') * 10
                        + (json.Source[json.Offset + 5] - '0');
                    break;
                case 7:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000
                        + (json.Source[json.Offset + 1] - '0') * 100000
                        + (json.Source[json.Offset + 2] - '0') * 10000
                        + (json.Source[json.Offset + 3] - '0') * 1000
                        + (json.Source[json.Offset + 4] - '0') * 100
                        + (json.Source[json.Offset + 5] - '0') * 10
                        + (json.Source[json.Offset + 6] - '0');
                    break;
                case 8:
                    value = (json.Source[json.Offset + 0] - '0') * 10000000
                        + (json.Source[json.Offset + 1] - '0') * 1000000
                        + (json.Source[json.Offset + 2] - '0') * 100000
                        + (json.Source[json.Offset + 3] - '0') * 10000
                        + (json.Source[json.Offset + 4] - '0') * 1000
                        + (json.Source[json.Offset + 5] - '0') * 100
                        + (json.Source[json.Offset + 6] - '0') * 10
                        + (json.Source[json.Offset + 7] - '0');
                    break;
                case 9:
                    value = (json.Source[json.Offset + 0] - '0') * 100000000
                        + (json.Source[json.Offset + 1] - '0') * 10000000
                        + (json.Source[json.Offset + 2] - '0') * 1000000
                        + (json.Source[json.Offset + 3] - '0') * 100000
                        + (json.Source[json.Offset + 4] - '0') * 10000
                        + (json.Source[json.Offset + 5] - '0') * 1000
                        + (json.Source[json.Offset + 6] - '0') * 100
                        + (json.Source[json.Offset + 7] - '0') * 10
                        + (json.Source[json.Offset + 8] - '0');
                    break;
                case 10:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000000L
                        + (json.Source[json.Offset + 1] - '0') * 100000000
                        + (json.Source[json.Offset + 2] - '0') * 10000000
                        + (json.Source[json.Offset + 3] - '0') * 1000000
                        + (json.Source[json.Offset + 4] - '0') * 100000
                        + (json.Source[json.Offset + 5] - '0') * 10000
                        + (json.Source[json.Offset + 6] - '0') * 1000
                        + (json.Source[json.Offset + 7] - '0') * 100
                        + (json.Source[json.Offset + 8] - '0') * 10
                        + (json.Source[json.Offset + 9] - '0');
                    break;
                case 11:
                    value = (json.Source[json.Offset + 0] - '0') * 10000000000L
                        + (json.Source[json.Offset + 1] - '0') * 1000000000L
                        + (json.Source[json.Offset + 2] - '0') * 100000000
                        + (json.Source[json.Offset + 3] - '0') * 10000000
                        + (json.Source[json.Offset + 4] - '0') * 1000000
                        + (json.Source[json.Offset + 5] - '0') * 100000
                        + (json.Source[json.Offset + 6] - '0') * 10000
                        + (json.Source[json.Offset + 7] - '0') * 1000
                        + (json.Source[json.Offset + 8] - '0') * 100
                        + (json.Source[json.Offset + 9] - '0') * 10
                        + (json.Source[json.Offset + 10] - '0');
                    break;
                case 12:
                    value = (json.Source[json.Offset + 0] - '0') * 100000000000L
                        + (json.Source[json.Offset + 1] - '0') * 10000000000L
                        + (json.Source[json.Offset + 2] - '0') * 1000000000L
                        + (json.Source[json.Offset + 3] - '0') * 100000000
                        + (json.Source[json.Offset + 4] - '0') * 10000000
                        + (json.Source[json.Offset + 5] - '0') * 1000000
                        + (json.Source[json.Offset + 6] - '0') * 100000
                        + (json.Source[json.Offset + 7] - '0') * 10000
                        + (json.Source[json.Offset + 8] - '0') * 1000
                        + (json.Source[json.Offset + 9] - '0') * 100
                        + (json.Source[json.Offset + 10] - '0') * 10
                        + (json.Source[json.Offset + 11] - '0');
                    break;
                case 13:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 100000000000L
                        + (json.Source[json.Offset + 2] - '0') * 10000000000L
                        + (json.Source[json.Offset + 3] - '0') * 1000000000L
                        + (json.Source[json.Offset + 4] - '0') * 100000000
                        + (json.Source[json.Offset + 5] - '0') * 10000000
                        + (json.Source[json.Offset + 6] - '0') * 1000000
                        + (json.Source[json.Offset + 7] - '0') * 100000
                        + (json.Source[json.Offset + 8] - '0') * 10000
                        + (json.Source[json.Offset + 9] - '0') * 1000
                        + (json.Source[json.Offset + 10] - '0') * 100
                        + (json.Source[json.Offset + 11] - '0') * 10
                        + (json.Source[json.Offset + 12] - '0');
                    break;
                case 14:
                    value = (json.Source[json.Offset + 0] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 100000000000L
                        + (json.Source[json.Offset + 3] - '0') * 10000000000L
                        + (json.Source[json.Offset + 4] - '0') * 1000000000L
                        + (json.Source[json.Offset + 5] - '0') * 100000000
                        + (json.Source[json.Offset + 6] - '0') * 10000000
                        + (json.Source[json.Offset + 7] - '0') * 1000000
                        + (json.Source[json.Offset + 8] - '0') * 100000
                        + (json.Source[json.Offset + 9] - '0') * 10000
                        + (json.Source[json.Offset + 10] - '0') * 1000
                        + (json.Source[json.Offset + 11] - '0') * 100
                        + (json.Source[json.Offset + 12] - '0') * 10
                        + (json.Source[json.Offset + 13] - '0');
                    break;
                case 15:
                    value = (json.Source[json.Offset + 0] - '0') * 100000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 3] - '0') * 100000000000L
                        + (json.Source[json.Offset + 4] - '0') * 10000000000L
                        + (json.Source[json.Offset + 5] - '0') * 1000000000L
                        + (json.Source[json.Offset + 6] - '0') * 100000000
                        + (json.Source[json.Offset + 7] - '0') * 10000000
                        + (json.Source[json.Offset + 8] - '0') * 1000000
                        + (json.Source[json.Offset + 9] - '0') * 100000
                        + (json.Source[json.Offset + 10] - '0') * 10000
                        + (json.Source[json.Offset + 11] - '0') * 1000
                        + (json.Source[json.Offset + 12] - '0') * 100
                        + (json.Source[json.Offset + 13] - '0') * 10
                        + (json.Source[json.Offset + 14] - '0');
                    break;
                case 16:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 100000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 3] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 4] - '0') * 100000000000L
                        + (json.Source[json.Offset + 5] - '0') * 10000000000L
                        + (json.Source[json.Offset + 6] - '0') * 1000000000L
                        + (json.Source[json.Offset + 7] - '0') * 100000000
                        + (json.Source[json.Offset + 8] - '0') * 10000000
                        + (json.Source[json.Offset + 9] - '0') * 1000000
                        + (json.Source[json.Offset + 10] - '0') * 100000
                        + (json.Source[json.Offset + 11] - '0') * 10000
                        + (json.Source[json.Offset + 12] - '0') * 1000
                        + (json.Source[json.Offset + 13] - '0') * 100
                        + (json.Source[json.Offset + 14] - '0') * 10
                        + (json.Source[json.Offset + 15] - '0');
                    break;
                case 17:
                    value = (json.Source[json.Offset + 0] - '0') * 10000000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 1000000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 100000000000000L
                        + (json.Source[json.Offset + 3] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 4] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 5] - '0') * 100000000000L
                        + (json.Source[json.Offset + 6] - '0') * 10000000000L
                        + (json.Source[json.Offset + 7] - '0') * 1000000000L
                        + (json.Source[json.Offset + 8] - '0') * 100000000
                        + (json.Source[json.Offset + 9] - '0') * 10000000
                        + (json.Source[json.Offset + 10] - '0') * 1000000
                        + (json.Source[json.Offset + 11] - '0') * 100000
                        + (json.Source[json.Offset + 12] - '0') * 10000
                        + (json.Source[json.Offset + 13] - '0') * 1000
                        + (json.Source[json.Offset + 14] - '0') * 100
                        + (json.Source[json.Offset + 15] - '0') * 10
                        + (json.Source[json.Offset + 16] - '0');
                    break;
                case 18:
                    value = (json.Source[json.Offset + 0] - '0') * 100000000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 10000000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 1000000000000000L
                        + (json.Source[json.Offset + 3] - '0') * 100000000000000L
                        + (json.Source[json.Offset + 4] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 5] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 6] - '0') * 100000000000L
                        + (json.Source[json.Offset + 7] - '0') * 10000000000L
                        + (json.Source[json.Offset + 8] - '0') * 1000000000L
                        + (json.Source[json.Offset + 9] - '0') * 100000000
                        + (json.Source[json.Offset + 10] - '0') * 10000000
                        + (json.Source[json.Offset + 11] - '0') * 1000000
                        + (json.Source[json.Offset + 12] - '0') * 100000
                        + (json.Source[json.Offset + 13] - '0') * 10000
                        + (json.Source[json.Offset + 14] - '0') * 1000
                        + (json.Source[json.Offset + 15] - '0') * 100
                        + (json.Source[json.Offset + 16] - '0') * 10
                        + (json.Source[json.Offset + 17] - '0');
                    break;
                default:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000000000000000L
                        + (json.Source[json.Offset + 1] - '0') * 100000000000000000L
                        + (json.Source[json.Offset + 2] - '0') * 10000000000000000L
                        + (json.Source[json.Offset + 3] - '0') * 1000000000000000L
                        + (json.Source[json.Offset + 4] - '0') * 100000000000000L
                        + (json.Source[json.Offset + 5] - '0') * 10000000000000L
                        + (json.Source[json.Offset + 6] - '0') * 1000000000000L
                        + (json.Source[json.Offset + 7] - '0') * 100000000000L
                        + (json.Source[json.Offset + 8] - '0') * 10000000000L
                        + (json.Source[json.Offset + 9] - '0') * 1000000000L
                        + (json.Source[json.Offset + 10] - '0') * 100000000
                        + (json.Source[json.Offset + 11] - '0') * 10000000
                        + (json.Source[json.Offset + 12] - '0') * 1000000
                        + (json.Source[json.Offset + 13] - '0') * 100000
                        + (json.Source[json.Offset + 14] - '0') * 10000
                        + (json.Source[json.Offset + 15] - '0') * 1000
                        + (json.Source[json.Offset + 16] - '0') * 100
                        + (json.Source[json.Offset + 17] - '0') * 10
                        + (json.Source[json.Offset + 18] - '0');
                    break;
            }

            return value * multiplier;
        }
    }
}