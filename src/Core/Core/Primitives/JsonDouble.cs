using System;

namespace Castalia
{
    internal static class JsonDouble
    {
        public static void Append(double value, StringBuffer buffer)
        {
            buffer.Append(value);
        }

        public static double Unwrap(StringSpan json)
        {
            const int precision = 15 + 1; // Double precision and '.' symbol.

            var result = 0.0;
            var dotIndex = -1;

            for (var i = 0; i < (json.Length > precision ? precision : json.Length); i++)
            {
                var ch = json[i];
                if (ch != '.')
                    continue;

                result += JsonLong.Unwrap(json.SubSpan(i));

                if (i == precision - 1)
                    return result;

                dotIndex = i;
                break;
            }

            if (dotIndex == -1)
            {
                if (json.Length < precision)
                    return JsonLong.Unwrap(json);

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

                var integralPart = JsonLong.Unwrap(json);

                return integralPart * Math.Pow(10, JsonByte.Unwrap(ePart));
            }

            if (json.Length > precision)
                json.Length = precision;

            var fractionalPart = json.SubSpan(dotIndex + 1, json.Length - dotIndex - 1);
            result += (double) (decimal) (JsonLong.Unwrap(fractionalPart) * Math.Pow(10, -fractionalPart.Length));

            return result;
        }
    }
}