using System;

namespace Pocket.Json
{
    internal static class JsonDouble
    {
        public static double Read(ref StringSpan json)
        {
            const int precision = 15 + 1; // Double precision and '.' symbol.

            var result = 0.0;
            var dotIndex = -1;
            
            var span = json;
            var source = span.Source;
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
                        
                        result += JsonLong.Read(ref integralSpan);

                        if (i == precision - 1)
                            return result;
                        
                        dotIndex = i - start;
                        break;
                }
            }

            CYCLE_END:

            var length = span.End - start;
            
            json.Start += length;

            if (dotIndex == -1)
            {
                if (length < precision)
                    return JsonLong.Read(ref span);

                var eIndex = -1;
                for (var i = precision - 1; i < length; i++)
                {
                    var ch = source[span.Start + i];
                    if (ch != 'E')
                        continue;

                    eIndex = i;
                    break;
                }

                if (eIndex == -1)
                    throw new ArgumentException(
                        $"Cannot deserialize {span.ToString()} because it looks like it\'s too long and must have the exponent part.");

                var ePart = span;
                ePart.Start = start + eIndex + 1;
                ePart.End = start + length;
                span.End = eIndex;

                var integralPart = JsonLong.Read(ref span);
                var ePartReadped = JsonByte.Read(ref ePart); // TODO: Consider to use `JsonInt` here.

                if (ePartReadped < PowerOfTen.DoubleCount)
                    return integralPart * PowerOfTen.PositiveDouble[ePartReadped];

                return integralPart * Math.Pow(10, JsonByte.Read(ref ePart));
            }

            if (length > precision)
            {
                span.End = start + precision;
                length = precision;
            }

            span.Start += dotIndex + 1;
            length -= dotIndex + 1;
            
            result += JsonLong.Read(ref span) * PowerOfTen.NegativeDouble[length];

            return result;
        }
        
        public static void Write(double value, StringBuffer buffer) =>
            buffer.Write(value);
    }
}