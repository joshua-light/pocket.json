namespace Pocket.Json
{
    internal static class JsonInt
    {
        public static void Append(int value, StringBuffer buffer)
        {
            buffer.Append(value);
        }
        
        public static int Unwrap(JsonSpan json) => Unwrap(json.NextPrimitive());

        public static int Unwrap(StringSpan json)
        {            
            var source = json.Source;
            var multiplier = 1;
            
            if (source[json.Start] == '-')
            {
                multiplier = -1;
                json.Start++;
            }

            var offset = json.Start;
            
            int value;
            switch (json.End - offset)
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
                default:
                    value = (source[offset + 0] - '0') * 1000000000
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
            }

            return value * multiplier;
        }
    }
}