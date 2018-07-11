using System;

namespace Pocket.Json
{
    internal static class JsonFloat
    {
        public static void Append(float value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static float Unwrap(JsonSpan json)
        {
            const int precision = 7 + 1; // Float precision and '.' symbol.
            
            var span = json.Span;
            
            var result = 0f;
            var dotIndex = -1;
            var cycleLength = span.Length;

            for (var i = 0; i < cycleLength; i++)
            {
                var ch = span[i];
                switch (ch)
                {
                    case ',':
                    case ':':
                    case '}':
                    case ']':
                        span = span.SubSpan(i);
                        goto CYCLE_END;
                        
                    case '.':
                        result += JsonInt.Unwrap(span.SubSpan(i));

                        if (i == precision - 1)
                            return result;
                        
                        dotIndex = i;
                        break;
                        
                    default:
                        if (i == cycleLength - 1)
                        {
                            span = span.SubSpan(i + 1);
                            goto CYCLE_END;
                        }
                        continue;
                }
            }
            
            CYCLE_END:
            
            json.Skip(span.Length);

            if (dotIndex == -1)
            {
                if (span.Length < precision)
                    return JsonInt.Unwrap(span);

                var eIndex = -1;
                for (var i = precision - 1; i < span.Length; i++)
                {
                    var ch = span[i];
                    if (ch != 'E')
                        continue;

                    eIndex = i;
                    break;
                }

                if (eIndex == -1)
                    throw new ArgumentException(
                        $"Cannot deserialize {span} because it looks like it\'s too long and must have the exponent part."
                    );

                var ePart = span.SubSpan(eIndex + 1, span.Length - eIndex - 1);
                span.Length = eIndex;

                return JsonInt.Unwrap(span) * (float) Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (span.Length > precision)
                span.Length = precision;

            var fractionalPart = span.SubSpan(dotIndex + 1, span.Length - dotIndex - 1);
            result += (float) (decimal) (JsonInt.Unwrap(fractionalPart) *
                PowerOfTen.NegativeFloat[fractionalPart.Length]);

            return result;
        }
    }
}