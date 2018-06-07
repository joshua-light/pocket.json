using System;
using System.Linq;

namespace Pocket.Json
{
    internal static class PowerOfTen
    {
        public static long[] PositiveLong = Enumerable.Range(0, 20).Select(x => (long) Math.Pow(10, x)).ToArray();
        public static int[] PositiveInt = Enumerable.Range(0, 10).Select(x => (int) Math.Pow(10, x)).ToArray();
        public static float[] NegativeFloat = Enumerable.Range(0, 8).Select(x => (float) Math.Pow(10, -x)).ToArray();
        public static double[] NegativeDouble = Enumerable.Range(0, 20).Select(x => Math.Pow(10, -x)).ToArray();
    }
}