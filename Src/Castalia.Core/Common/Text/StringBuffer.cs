using System;
using System.Runtime.InteropServices;

namespace Castalia
{
    internal sealed class StringBuffer
    {
        private readonly char[] _numbersBuffer = new char[32];

        private readonly unsafe char* _sourcePtr;

        private int _length;

        public unsafe StringBuffer()
        {
            _sourcePtr = (char*) Marshal.AllocHGlobal(32768 * 2);
        }

        public void Clear()
        {
            _length = 0;
        }

        public unsafe StringBuffer Append(string value)
        {
            if (string.IsNullOrEmpty(value))
                return this;

            var length = value.Length;

            fixed (char* valuePtr = value)
            {
                Memory.Copy(valuePtr, _sourcePtr + _length, length);
            }

            _length += length;

            return this;
        }

        public unsafe StringBuffer Append(char value)
        {
            *(_sourcePtr + _length) = value;
            _length++;

            return this;
        }

        public StringBuffer Append(byte value)
        {
            // Here we just unroll everything, because byte value has either one or two or three digits.

            // One digit.
            if (value < 10)
                return Append((char) ('0' + value));

            // Two digits.
            if (value < 100)
            {
                var firstDigit = value / 10;
                var lastDigit = value - firstDigit * 10;

                Append((char) ('0' + firstDigit));
                Append((char) ('0' + lastDigit));

                return this;
            }

            // Three digits.
            var first = value / 100;

            var intValue = value - first * 100;
            var second = intValue / 10;
            var third = intValue - second * 10;

            Append((char) ('0' + first));
            Append((char) ('0' + second));
            Append((char) ('0' + third));

            return this;
        }

        public StringBuffer Append(int value)
        {
            if (value == 0)
            {
                Append('0');
                return this;
            }

            // This case is handled because there is no possible way
            // to represent integral part of int.MinValue as positive integer,
            // what we do for other integers.
            if (value == int.MinValue)
            {
                Append("-2147483648");
                return this;
            }

            // Negative values are represented as absolute values with '-' character prepended.
            if (value < 0)
            {
                Append('-');
                value = -value;
            }

            // Here we partially reuse unrolled append for bytes.
            if (value <= byte.MaxValue)
                return Append((byte) value);

            var digits = Digits.Count(value);
            var remainder = digits % 2;
            var unrolledDigits = digits - remainder;

            var chars = _numbersBuffer;

            // Unrolled loop performes faster than simple.
            // TODO: Check, whether this can be improved to 4 unrolled operations per cycle.
            for (var i = 0; i < unrolledDigits; i += 2)
            {
                var newValue = value / 100;
                var digitsPair = value - newValue * 100;

                var firstDigit = digitsPair / 10;
                var lastDigit = digitsPair - firstDigit * 10;

                chars[i] = (char) ('0' + lastDigit);
                chars[i + 1] = (char) ('0' + firstDigit);

                value = newValue;
            }

            if (remainder != 0)
                chars[digits - 1] = (char) ('0' + value);

            // Because all of the digits are appended in reversed order,
            // for example, 123 is transformed to [ '3', '2', 1' ],
            // we're looping from the end of array.
            // This loop is unrolled also.
            for (var i = 0; i < unrolledDigits; i += 2)
            {
                var index = digits - i;

                Append(chars[index - 1]);
                Append(chars[index - 2]);
            }

            if (remainder != 0)
                Append(chars[0]);

            return this;
        }

        public StringBuffer Append(long value)
        {
            if (value == 0)
            {
                Append('0');
                return this;
            }

            // Long values perform much more better with simple `ToString()` call,
            // so they're not manually serialized as integers.
            Append(value.ToString());
            return this;
        }

        public StringBuffer Append(float value)
        {
            // Here simple comparison is used because it's a *lot* faster, than more correct epsilon check.
            if (value == 0.0f)
                return Append('0');

            // Floating point serialization has some tricks.
            // First of all, it's using floating point precision digits,
            // and will not serialize properly after reaching that precision.
            // 
            // Some rules are applied:
            //     - Big numbers just appended by 'E*' suffix, where '*' - power of ten.
            //       For example: 10 000 000 000 is represented as "10000000E3".
            //
            //     - Short numbers (less than 1) are just cutted after 7th digit.
            //       For example: 123.45678 is represented as "123.4568" with some rounding applied.
            const int floatPrecision = 7;

            // Negative values are represented as absolute values with '-' character prepended.
            if (value < 0)
            {
                Append('-');
                value = -value;
            }

            var digits = 0;

            // Remove all the redundant calculations for numbers like 0.*.
            if (value > 1)
            {
                digits = Digits.Count(value);

                // Because `value` can be represented as number with up to 38 digits,
                // we must calculate the number of digits, that represent unprecise part of it.
                var unpreciseDigits = Math.Max(0, digits - floatPrecision);

                // We don't use precalculated power of ten here, because unprecise digits can be too big.
                // TODO: Check, whether it's worth to use hardcoded power of ten for small numbers.
                var integralPart = (int) (value / Math.Pow(10, unpreciseDigits));

                Append(integralPart);

                if (unpreciseDigits > 0)
                {
                    Append('E');
                    Append(unpreciseDigits);
                    return this;
                }
            }
            else
            {
                // If `value` is not greater than `1`, it has pattern 0.* or 1.*.
                Append((byte) value == 0 ? '0' : '1');
            }

            // Here we manually calculate the number of digits after floating point.
            // This is done mainly because it's much more faster to process a number like 0.1
            // and find, that it has only 1 digit precision, instead of working with big numbers all the time.
            var precision = 0;
            var maxPrecision = floatPrecision - digits;

            while (precision != maxPrecision)
            {
                var poweredValue = value * PowerOfTen.PositiveInt[precision];
                if (poweredValue == Math.Truncate(poweredValue))
                    break;

                precision++;
            }

            var pow = PowerOfTen.PositiveInt[precision];
            var roundedValue = value - Math.Truncate(value);

            // The rounding here prevents casting `value` to `decimal`,
            // that also allows converting small fractional parts without losing precision,
            // but in relativly slower way than current implementation.
            var fractionalPart = (int) Math.Round(roundedValue * pow, MidpointRounding.AwayFromZero);
            if (fractionalPart == 0)
                return this;

            Append('.');

            if (precision == 1)
                return Append((char) ('0' + fractionalPart));

            var chars = _numbersBuffer;

            // `nonZeroIndex` represents first non-zero digit index after the trailing ones.
            var nonZeroIndex = -1;

            for (var i = 0; i < precision; i++)
            {
                var newValue = fractionalPart / 10;
                var digit = (char) ('0' + fractionalPart - newValue * 10);

                if (nonZeroIndex == -1 && digit != '0')
                    nonZeroIndex = i;

                chars[i] = digit;
                fractionalPart = newValue;
            }

            for (int i = 0, length = precision - nonZeroIndex; i < length; i++)
                Append(chars[precision - 1 - i]);

            return this;
        }

        public StringBuffer Append(double value)
        {
            // Here simple comparison is used because it's a *lot* faster, than more correct epsilon check.
            if (value == 0.0)
                return Append('0');

            // The same considerations, explained in `Append( float value )`, are true for doubles.
            const int doublePrecision = 15;

            // Negative values are represented as absolute values with '-' character prepended.
            if (value < 0)
            {
                Append('-');
                value = -value;
            }

            var digits = 0;

            // Remove all the redundant calculations for numbers like 0.*.
            if (value > 1)
            {
                digits = Math.Max(0, 1 + (int) Math.Log10(value));

                // Because `value` can be represented as number with up to 308 digits,
                // we must calculate the number of digits, that represent unprecise part of it.
                var unpreciseDigits = Math.Max(0, digits - doublePrecision);

                // We don't use precalculated power of ten here, because unprecise digits can be too big.
                // TODO: Check, whether it's worth to use hardcoded power of ten for small numbers.
                var integralPart = (long) (value / Math.Pow(10, unpreciseDigits));

                Append(integralPart);

                if (unpreciseDigits > 0)
                {
                    Append('E');
                    Append(unpreciseDigits);
                    return this;
                }
            }
            else
            {
                // If `value` is not greater than `1`, it has pattern 0.* or 1.*.
                Append((byte) value == 0 ? '0' : '1');
            }

            // Here we manually calculate the number of digits after floating point.
            // This is done mainly because it's much more faster to process a number like 0.1
            // and find, that it has only 1 digit precision, instead of working with big numbers all the time.
            var precision = 0;
            var maxPrecision = doublePrecision - digits;

            while (precision != maxPrecision)
            {
                var poweredValue = value * PowerOfTen.PositiveLong[precision];
                if (poweredValue == Math.Truncate(poweredValue))
                    break;

                precision++;
            }

            var pow = PowerOfTen.PositiveLong[precision];
            var roundedValue = value - Math.Truncate(value);

            // The rounding here prevents casting `value` to `decimal`,
            // that also allows converting small fractional parts without losing precision,
            // but in relativly slower way than current implementation.
            var fractionalPart = (long) Math.Round(roundedValue * pow, MidpointRounding.AwayFromZero);
            if (fractionalPart == 0)
                return this;

            Append('.');

            var chars = _numbersBuffer;

            // `nonZeroIndex` represents first non-zero digit index after the trailing ones.
            var nonZeroIndex = -1;

            for (var i = 0; i < precision; i++)
            {
                var digit = (char) ('0' + fractionalPart % 10);
                chars[i] = digit;

                if (nonZeroIndex == -1 && digit != '0')
                    nonZeroIndex = i;

                fractionalPart = fractionalPart / 10;
            }

            for (int i = 0, length = precision - nonZeroIndex; i < length; i++)
                Append(chars[precision - 1 - i]);

            return this;
        }

        public unsafe string AsString()
        {
            var result = new string(_sourcePtr, 0, _length);
            return result;
        }
    }
}