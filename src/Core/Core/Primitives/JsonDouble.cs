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

            var result = 0.0;
            var dotIndex = -1;
            
            var span = json.Span;
            var source = span.Source;
            var end = span.End;
            var start = span.Start;

            for (var i = start; i < span.End; i++)
            {
                var ch = source[i];
                switch (ch)
                {
                    case ',':
                    case ':':
                    case '}':
                    case ']':
                        span.End = i;
                        goto CYCLE_END;
                        
                    case '.':
                        var integralSpan = span;
                        integralSpan.End = i;
                        
                        result += JsonLong.Unwrap(integralSpan);

                        if (i == precision - 1)
                            return result;
                        
                        dotIndex = i - start;
                        break;
                }
            }

            CYCLE_END:

            var length = span.End - start;
            
            json.Span.Start += length;

            if (dotIndex == -1)
            {
                if (length < precision)
                    return JsonLong.Unwrap(span);

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

                var ePart = span.SubSpan(eIndex + 1, length - eIndex - 1);
                span.End = eIndex;

                var integralPart = JsonLong.Unwrap(span);
                var ePartUnwrapped = JsonByte.Unwrap(ePart); // TODO: Consider to use `JsonInt` here.

                if (ePartUnwrapped < PowerOfTen.DoubleCount)
                    return integralPart * PowerOfTen.PositiveDouble[ePartUnwrapped];

                return integralPart * Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (length > precision)
            {
                span.End = start + precision;
                length = precision;
            }

            span.Start += dotIndex + 1;
            length -= dotIndex + 1;
            
            result += JsonLong.Unwrap(span) * PowerOfTen.NegativeDouble[length];

            return result;
        }
    }
}