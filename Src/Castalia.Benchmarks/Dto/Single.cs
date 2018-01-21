using System.Collections.Generic;
using System.Linq;

namespace Castalia.Benchmarks
{
    public static class Single
    {
        public class WithBool
        {
            public class True
            {
                public bool Data = true;
            }

            public class False
            {
                public bool Data = false;
            }
        }

        public class WithByte
        {
            public class Zero
            {
                public byte Data = 0;
            }

            public class Short
            {
                public byte Data = 1;
            }

            public class Middle
            {
                public byte Data = 1;
            }

            public class Max
            {
                public byte Data = 255;
            }
        }

        public class WithChar
        {
            public class Zero
            {
                public char Data = (char) 0;
            }

            public class Letter
            {
                public char Data = 'a';
            }

            public class Digit
            {
                public char Data = '1';
            }
        }

        public class WithInt
        {
            public class Min
            {
                public int Data = int.MinValue + 10;
            }

            public class Zero
            {
                public int Data = 0;
            }

            public class Short
            {
                public int Data = 123;
            }

            public class Max
            {
                public int Data = int.MaxValue - 10;
            }
        }

        public class WithLong
        {
            public class Min
            {
                public long Data = long.MinValue + 10;
            }

            public class Zero
            {
                public long Data = 0L;
            }

            public class Short
            {
                public long Data = 123456L;
            }

            public class Max
            {
                public long Data = long.MaxValue - 10;
            }
        }

        public class WithFloat
        {
            public class Min
            {
                public float Data = float.MinValue;
            }

            public class Zero
            {
                public float Data = 0.0f;
            }

            public class OneDigit
            {
                public float Data = 1.1f;
            }

            public class SevenDigits
            {
                public float Data = 0.523159f;
            }

            public class Max
            {
                public float Data = float.MaxValue;
            }
        }

        public class WithDouble
        {
            public class Min
            {
                public double Data = double.MinValue;
            }

            public class Zero
            {
                public double Data = 0.0;
            }

            public class OneDigit
            {
                public double Data = 1.1;
            }

            public class FifteenDigits
            {
                public double Data = 0.523154234575693;
            }

            public class Max
            {
                public double Data = double.MaxValue;
            }
        }

        public class WithString
        {
            public class Null
            {
                public string Data;
            }

            public class Empty
            {
                public string Data = "";
            }

            public class Short
            {
                public string Data = "123456789";
            }

            public class Long
            {
                public string Data = Enumerable.Range(0, 1000).Aggregate("", (cur, next) => cur + next);
            }
        }

        public class WithArray
        {
            public int[] Items = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        }

        public class WithHashSet
        {
            public HashSet<int> Items =
                new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        }

        public class WithDictionary
        {
            public Dictionary<int, int> Items = Enumerable.Range(0, 100).ToDictionary(x => x, x => x);
        }
    }
}