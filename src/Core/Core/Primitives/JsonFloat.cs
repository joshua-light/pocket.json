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
            
            var result = 0f;
            var dotIndex = -1;
            
            var span = json.Span;
            var start = span.Start;
            var length = span.End - start;

            for (var i = 0; i < length; i++)
            {
                var ch = span.CharAt(i);
                switch (ch)
                {
                    case ',':
                    case ':':
                    case '}':
                    case ']':
                        span.End = start + i;
                        goto CYCLE_END;
                        
                    case '.':
                        var integralSpan = span;
                        integralSpan.End = start + i;
                        
                        result += JsonInt.Unwrap(integralSpan);

                        if (i == precision - 1)
                            return result;
                        
                        dotIndex = i;
                        break;
                        
                    default:
                        if (i == length - 1)
                        {
                            span.End = start + i + 1;
                            goto CYCLE_END;
                        }
                        continue;
                }
            }
            
            CYCLE_END:
            
            length = span.End - start;
            
            json.Span.Start += length;

            if (dotIndex == -1)
            {
                if (length < precision)
                    return JsonInt.Unwrap(span);

                var eIndex = -1;
                for (var i = precision - 1; i < length; i++)
                {
                    var ch = span.CharAt(i);
                    if (ch != 'E')
                        continue;

                    eIndex = i;
                    break;
                }

                if (eIndex == -1)
                    throw new ArgumentException(
                        $"Cannot deserialize {span} because it looks like it\'s too long and must have the exponent part.");

                var ePart = span.SubSpan(eIndex + 1, span.End - eIndex - 1);
                span.End = eIndex;

                var integralPart = JsonLong.Unwrap(span);
                var ePartUnwrapped = JsonByte.Unwrap(ePart);

                if (ePartUnwrapped < PowerOfTen.FloatCount)
                    return integralPart * PowerOfTen.PositiveFloat[ePartUnwrapped];

                return integralPart * (float) Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (length > precision)
            {
                span.End = start + precision;
                length = precision;
            }

            span.Start += dotIndex + 1;
            length -= dotIndex + 1;
            
            result += JsonInt.Unwrap(span) * PowerOfTen.NegativeFloat[length];

            return result;
        }
    }
}