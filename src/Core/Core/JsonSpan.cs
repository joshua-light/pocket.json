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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Skip(int count) => Span.SkipMutable(count);
        
        public StringSpan NextName()
        {
            if (Span.End == Span.Start)
                return StringSpan.Zero;

            Span.SkipMutable(1); // Skips '"'.
            
            var span = Span;
            
            var start = span.Start;
            var source = span.Source;
            var i = start;

            while (true)
            {
                if (source[i] == '"')
                {
                    span.End = i;
                    Span.Start = i + 1;
                    return span;
                }

                i++;
            }
        }
        
        public StringSpan NextString()
        {
            var span = Span;
            
            var start = span.Start;
            var source = span.Source;
            var i = start + 1;

            while (true)
            {
                if (source[i] == '"')
                {
                    span.End = i + 1;
                    Span.Start = i + 1;
                    return span;
                }

                i++;
            }
        }

        public StringSpan NextPrimitive() => NextPrimitive(ref Span);

        private static StringSpan NextPrimitive(ref StringSpan sourceSpan)
        {
            var span = sourceSpan;
            
            var source = span.Source;
            var start = span.Start;
            var end = span.End;

            for (var i = start; i < end; i++)
            {
                var ch = source[i];
                if (ch == ',' || ch == ':' || ch == '}' || ch == ']')
                {
                    span.End = i;
                    sourceSpan.Start = i;
                    return span;
                }
            }
            
            sourceSpan.SkipMutable(span.End - span.Start);
            return span;
        }
    }
}