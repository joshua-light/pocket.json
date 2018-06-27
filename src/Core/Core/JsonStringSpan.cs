namespace Pocket.Json
{
    internal class JsonStringSpan
    {
        public StringSpan Json;

        public bool NextName(ref StringSpan span)
        {
            if (Json.Length <= 0)
                return false;

            Json.SkipMutable(1); // Skips '"'.

            var json = Json;
            var remainder = json.Length % 2;
            var length = json.Length - remainder;

            for (var i = 0; i < length; i += 2)
            {
                var a = json.Source[json.Offset + i];
                if (a == '"')
                {
                    json.Length = i;
                    span = json;
                    break;
                }

                var b = json.Source[json.Offset + i + 1];
                if (b == '"')
                {
                    json.Length = i + 1;
                    span = json;
                    break;
                }
            }

            Json.SkipMutable(span.Length + 1);

            return true;
        }

        public StringSpan NextValue()
        {
            if (Json.Length <= 0)
                return StringSpan.Zero;
            
            var json = Json;
            var span = json;
            var ch = span.Source[span.Offset];

            if (ch == '{')
                NextObject(ref span);
            else if (ch == '"')
                NextString(ref span);
            else
                NextPrimitive(ref span);

            json.SkipMutable(span.Length + 1); // Skip ','.

            Json = json;

            return span;
        }

        public bool NextNameAndValue(out StringSpan name, out StringSpan value)
        {
            name = StringSpan.Zero;
            value = StringSpan.Zero;

            if (!NextName(ref name))
                return false;
            
            Json.SkipMutable(1); // Skip ':'.

            value = NextValue();

            return true;
        }

        private static void NextObject(ref StringSpan json)
        {
            var stack = 0;

            for (var i = 0; i < json.Length; i++)
                if (json[i] == '{')
                {
                    stack++;
                }
                else if (json[i] == '}' && stack-- == 1)
                {
                    json = json.SubSpan(i + 1);
                    break;
                }
        }

        private static void NextString(ref StringSpan json)
        {
            for (var i = 1; i < json.Length; i++)
            {
                if (json[i] != '"')
                    continue;

                json = json.SubSpan(i + 1);
                break;
            }
        }

        private static void NextPrimitive(ref StringSpan json)
        {
            var remainder = json.Length % 2;
            var length = json.Length - remainder;

            for (var i = 0; i < length; i += 2)
            {
                var a = json.Source[json.Offset + i];
                if (a == ',')
                {
                    json.Length = i;
                    break;
                }

                var b = json.Source[json.Offset + i + 1];
                if (b == ',')
                {
                    json.Length = i + 1;
                    break;
                }
            }
        }
    }
}