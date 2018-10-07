namespace Pocket.Json
{
    internal class JsonSpan
    {
        public StringSpan Span;

        public JsonSpan(string span)
        {
            Span = new StringSpan(span);
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