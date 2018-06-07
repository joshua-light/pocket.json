namespace Castalia
{
    internal static class JsonInt
    {
        public static void Append(int value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static int Unwrap(StringSpan json)
        {
            var multiplier = 1;

            if (json.Source[json.Offset + 0] == '-')
            {
                multiplier = -1;
                json.SkipMutable(1);
            }

            int value;
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
                default:
                    value = (json.Source[json.Offset + 0] - '0') * 1000000000
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
            }

            return value * multiplier;
        }
    }
}