using System;

namespace Pocket.Json
{
    internal static class JsonDouble
    {
        public static void Append(double value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static double Unwrap(JsonSpan json)
        {
            const int precision = 15 + 1; // Double precision and '.' symbol.

            var span = json.Span;
            
            var result = 0.0;
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
                        result += JsonLong.Unwrap(span.SubSpan(i));

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
                    return JsonLong.Unwrap(span);

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

                var integralPart = JsonLong.Unwrap(span);

                return integralPart * Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (span.Length > precision)
                span.Length = precision;

            var fractionalPart = span.SubSpan(dotIndex + 1, span.Length - dotIndex - 1);
            result += (double) (decimal) (JsonLong.Unwrap(fractionalPart) * Math.Pow(10, -fractionalPart.Length));

            return result;
        }
    }
}