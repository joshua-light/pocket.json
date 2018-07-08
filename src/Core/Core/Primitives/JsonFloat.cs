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
            
            var span = json.NextPrimitive();
            
            var result = 0f;
            var dotIndex = -1;

            for (var i = 0; i < (span.Length > precision ? precision : span.Length); i++)
            {
                var ch = span[i];
                if (ch != '.')
                    continue;

                result += JsonInt.Unwrap(span.SubSpan(i));

                if (i == precision - 1)
                    return result;

                dotIndex = i;
                break;
            }

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
                        "Cannot deserialize " + span +
                        " because it looks like it's too long and must have the exponent part."
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