using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    internal class JsonSpan
    {
        public StringSpan Span;

        public JsonSpan(StringSpan span)
        {
            Span = span;
        }

        public char Char => Span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Skip(int count)
        {
            Span.SkipMutable(count);
        }
        
        public StringSpan NextName()
        {
            if (Span.Length <= 0)
                return StringSpan.Zero;

            Span.SkipMutable(1); // Skips '"'.

            var span = StringSpan.Zero;
            var json = Span;
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
            
            Span.SkipMutable(span.Length + 1);

            return span;
        }
        
        public StringSpan NextString()
        {
            var span = Span;
            
            var start = Span.Offset;
            var source = Span.Source;
            var i = start + 1;
            
            while (i < start + Span.Length)
            {
                if (source[i] == '"')
                {
                    span = span.SubSpan(i + 1 - start);
                    break;
                }

                i++;
            }

            Span.SkipMutable(span.Length);
            
            return span;
        }

        public StringSpan NextPrimitive()
        {
            var span = Span;
            
            var start = Span.Offset;
            var source = Span.Source;
            var i = start;
            
            while (i < start + Span.Length)
            {
                var a = source[i];
                if (a == ',' || a == ':' || a == '}' || a == ']')
                {
                    span.Length = i - start;
                    break;
                }
                
                i++;
            }
            
            Span.SkipMutable(span.Length);

            return span;
        }
    }
}