using System;
using System.Linq;

namespace Pocket.Json
{
    internal static class PowerOfTen
    {
        public const int LongCount = 19;
        public const int IntCount = 10;
        public const int FloatCount = 30;
        public const int DoubleCount = 30;
        
        public static long[] PositiveLong = Enumerable.Range(0, LongCount)
            .Select(x => (long) Math.Pow(10, x))
            .ToArray();
        public static int[] PositiveInt = Enumerable.Range(0, IntCount)
            .Select(x => (int) Math.Pow(10, x))
            .ToArray();
        
        public static float[] PositiveFloat = Enumerable.Range(0, FloatCount)
            .Select(x => (float) Math.Pow(10, x))
            .ToArray();
        public static float[] NegativeFloat = Enumerable.Range(0, FloatCount)
            .Select(x => (float) Math.Pow(10, -x))
            .ToArray();

        public static double[] PositiveDouble = Enumerable.Range(0, DoubleCount)
            .Select(x => Math.Pow(10, x))
            .ToArray();
        public static double[] NegativeDouble = Enumerable.Range(0, DoubleCount)
            .Select(x => Math.Pow(10, -x))
            .ToArray();
    }
}