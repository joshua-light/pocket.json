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

            switch (ch)
            {
                case '{': NextObject(ref span); break;
                case '[': NextArray(ref span); break;
                case '"': NextString(ref span); break;
                
                default: NextPrimitive(ref span); break;
            }

            json.SkipMutable(span.Length + 1); // Skip ','.

            Json = json;

            return span;
        }

        public void NextObject(ref StringSpan json)
        {
            // We already know that first character is '{'.
            var stack = 1;
            
            var start = json.Offset;
            var length = start + json.Length - 1;
            var source = json.Source;

            for (var i = start + 1; i < length; i++)
            {
                var ch = source[i];
                if (ch == '{')
                    stack++;
                else if (ch == '}' && --stack == 0)
                {
                    json = json.SubSpan(i - start + 1);
                    break;
                }
            }
        }
        
        public void NextArray(ref StringSpan json)
        {
            // We already know that first character is '['.
            var stack = 1;
            
            var start = json.Offset;
            var length = start + json.Length - 1;
            var source = json.Source;

            for (var i = start + 1; i < length; i++)
            {
                var ch = source[i];

                stack += Bitwise.Equals(ch, '[');
                stack -= Bitwise.Equals(ch, ']');
                
                if (stack == 0)
                {
                    json = json.SubSpan(i - start + 1);
                    break;
                }
            }
        }

        public void NextString(ref StringSpan json)
        {
            for (var i = 1; i < json.Length; i++)
            {
                if (json[i] != '"')
                    continue;

                json = json.SubSpan(i + 1);
                break;
            }
        }

        public void NextPrimitive(ref StringSpan json)
        {
            // In object there is ALWAYS something that comes after primitive value.
            // So we can unroll loop by the factor of 2 because we don't care if 1 character will be missed.
            var remainder = json.Length % 2;
            var length = json.Length - remainder;

            for (var i = 0; i < length; i += 2)
            {
                var a = json.Source[json.Offset + i];
                if (a == ',' || a == ':')
                {
                    json.Length = i;
                    break;
                }

                var b = json.Source[json.Offset + i + 1];
                if (b == ',' || b == ':')
                {
                    json.Length = i + 1;
                    break;
                }
            }
        }
    }
}