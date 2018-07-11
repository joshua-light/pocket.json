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

            while (true)
            {
                if (source[i] == '"')
                {
                    span.Length = i - start;
                    Span.SkipMutable(i - start + 1);
                    return span;
                }

                i++;
            }
        }
        
        public StringSpan NextString()
        {
            var span = Span;
            
            var start = span.Offset;
            var source = span.Source;
            var i = start + 1;

            while (true)
            {
                if (source[i] == '"')
                {
                    span.Length = i - start + 1;
                    Span.SkipMutable(i - start + 1);
                    return span;
                }

                i++;
            }
        }

        public StringSpan NextPrimitive()
        {
            var span = Span;
            
            var start = span.Offset;
            var source = span.Source;
            var length = start + span.Length;

            for (var i = start; i < length; i++)
            {
                var ch = source[i];
                if (ch == ',' || ch == ':' || ch == '}' || ch == ']')
                {
                    span.Length = i - start;
                    Span.SkipMutable(i - start);
                    return span;
                }
                
//                switch (source[i])
//                {
//                    case ',':
//                    case ':':
//                    case '}':
//                    case ']':
//                        span.Length = i - start;
//                        Span.SkipMutable(i - start);
//                        return span;
//                }
            }
            
            Span.SkipMutable(span.Length);
            return span;
        }
    }
}