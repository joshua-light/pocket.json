using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    public static class StringSpanExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringSpan NextItem(this ref StringSpan self)
        {
            var result = self;
            
            var source = result.Source;
            var start = result.Start;
            var end = result.End;

            for (var i = start; i < end; i++)
            {
                var ch = source[i];
                if (ch == ',' || ch == ':' || ch == '}' || ch == ']')
                {
                    result.End = i;
                    self.Start = i;
                    
                    return result;
                }
            }
            
            self.SkipMutable(result.End - result.Start);
            
            return result;
        }
    }
}