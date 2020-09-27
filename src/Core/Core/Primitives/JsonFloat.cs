using System;

namespace Pocket.Json
{
    internal static class JsonFloat
    {
        public static float Read(ref StringSpan json)
        {
            const int precision = 7 + 1; // Float precision and '.' symbol.
            
            var result = 0f;
            var dotIndex = -1;
            
            var span = json;
            var start = span.Start;
            var source = span.Source;

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
                        
                        result += JsonInt.Read(ref integralSpan);

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
                    return JsonInt.Read(ref span);

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
                var ePartValue = JsonByte.Read(ref ePart);
                if (ePartValue < PowerOfTen.FloatCount)
                    return integralPart * PowerOfTen.PositiveFloat[ePartValue];

                return integralPart * (float) Math.Pow(10, ePartValue);
            }

            if (length > precision)
            {
                span.End = start + precision;
                length = precision;
            }

            span.Start += dotIndex + 1;
            length -= dotIndex + 1;
            
            result += JsonInt.Read(ref span) * PowerOfTen.NegativeFloat[length];

            return result;
        }
        
        public static void Write(float value, StringBuffer buffer) =>
            buffer.Write(value);
    }
}