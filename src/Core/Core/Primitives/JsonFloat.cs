using System;

namespace Pocket.Json
{
    internal static class JsonFloat
    {
        public static void Append(float value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static float Unwrap(StringSpan json)
        {
            const int precision = 7 + 1; // Float precision and '.' symbol.

            var result = 0f;
            var dotIndex = -1;

            for (var i = 0; i < (json.Length > precision ? precision : json.Length); i++)
            {
                var ch = json[i];
                if (ch != '.')
                    continue;

                result += JsonInt.Unwrap(json.SubSpan(i));

                if (i == precision - 1)
                    return result;

                dotIndex = i;
                break;
            }

            if (dotIndex == -1)
            {
                if (json.Length < precision)
                    return JsonInt.Unwrap(json);

                var eIndex = -1;
                for (var i = precision - 1; i < json.Length; i++)
                {
                    var ch = json[i];
                    if (ch != 'E')
                        continue;

                    eIndex = i;
                    break;
                }

                if (eIndex == -1)
                    throw new ArgumentException(
                        "Cannot deserialize " + json +
                        " because it looks like it's too long and must have the exponent part."
                    );

                var ePart = json.SubSpan(eIndex + 1, json.Length - eIndex - 1);
                json.Length = eIndex;

                return JsonInt.Unwrap(json) * (float) Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (json.Length > precision)
                json.Length = precision;

            var fractionalPart = json.SubSpan(dotIndex + 1, json.Length - dotIndex - 1);
            result += (float) (decimal) (JsonInt.Unwrap(fractionalPart) *
                PowerOfTen.NegativeFloat[fractionalPart.Length]);

            return result;
        }
    }
}