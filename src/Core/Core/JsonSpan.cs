using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    internal class JsonSpan
    {
        public StringSpan Span;

        public JsonSpan(string span)
        {
            Span = new StringSpan(span);
        }

        public char Char => Span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Skip(int count) => Span.SkipMutable(count);
        
        public StringSpan NextName()
        {
            if (Span.Length <= 0)
                return StringSpan.Zero;

            Span.SkipMutable(1); // Skips '"'.
            
            var span = Span;
            
            var start = span.Offset;
            var source = span.Source;
            
            var i = start;
            
            LOOP:

            if (source[i] == '"')
            {
                span.Length = i  - start;
                Span.SkipMutable(i - start + 1);
                return span;
            }

            i++;
            
            goto LOOP;
        }
        
        public StringSpan NextString()
        {
            var span = Span;
            
            var start = span.Offset;
            var source = span.Source;
            
            var i = start + 1;
            
            LOOP:

            if (source[i] == '"')
            {
                span.Length = i + 1 - start;
                Span.SkipMutable(i + 1 - start);
                return span;
            }

            i++;
            
            goto LOOP;
        }

        public StringSpan NextPrimitive()
        {
            var span = Span;
            
            var start = span.Offset;
            var source = span.Source;
            var length = start + span.Length;

            for (var i = start; i < length; i++)
            {
                switch (source[i])
                {
                    case ',':
                    case ':':
                    case '}':
                    case ']':
                        span.Length = i - start;
                        Span.SkipMutable(i - start);
                        return span;
                }
            }
            
            return StringSpan.Zero;
        }
    }
}