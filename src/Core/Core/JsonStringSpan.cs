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
            var start = json.Offset;
            var source = json.Source;

            var i = start;
            while (true)
            {
                var a = source[i];
                if (a == '"')
                {
                    json.Length = i - start;
                    span = json;
                    break;
                }
                
                i++;
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
            var source = json.Source;

            var i = start + 1;
            while (true)
            {
                var ch = source[i];
                if (ch == '{')
                    stack++;
                else if (ch == '}' && --stack == 0)
                {
                    json = json.SubSpan(i - start + 1);
                    break;
                }
                
                i++;
            }
        }
        
        public void NextArray(ref StringSpan json)
        {
            // We already know that first character is '['.
            var stack = 1;
            
            var start = json.Offset;
            var source = json.Source;

            var i = start + 1;
            while (true)
            {
                var ch = source[i];
                if (ch == '[')
                    stack++;
                else if (ch == ']' && --stack == 0)
                {
                    json = json.SubSpan(i - start + 1);
                    break;
                }
                
                i++;
            }
        }

        public void NextString(ref StringSpan json)
        {
            var i = 1;
            while (true)
            {
                if (json[i] == '"')
                {
                    json = json.SubSpan(i + 1);
                    break;
                }

                i++;
            }
        }

        public void NextPrimitive(ref StringSpan json)
        {
            var start = json.Offset;
            var source = json.Source;
            
            var i = start;
            while (true)
            {
                var a = source[i];
                if (a == ',' || a == ':')
                {
                    json.Length = i - start;
                    break;
                }
                
                i++;
            }
        }
    }
}