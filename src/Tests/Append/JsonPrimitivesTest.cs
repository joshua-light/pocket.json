using System;
using Xunit;

namespace Castalia.Tests.Append
{
    public class JsonPrimitivesTest
    {
        [Fact]
        public void Bool()
        {
            Represents(false, "0");
            Represents(true, "1");

            Represents<bool?>(null, "");
            Represents<bool?>(false, "0");
            Represents<bool?>(true, "1");
        }

        [Fact]
        public void Byte()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
                Represents((byte) i, i.ToString());
        }

        [Fact]
        public void Char()
        {
            Represents('a', "a");
            Represents('\r', "\r");
            Represents('\n', "\n");
            Represents('Ş', "Ş");
            Represents('ğ', "ğ");

            Represents<char?>(null, "");
            Represents<char?>('a', "a");
        }

        [Fact]
        public void Double()
        {
            Represents(0.0, "0");
            Represents(0.1, "0.1");
            Represents(0.01, "0.01");
            Represents(0.001, "0.001");
            Represents(0.0001, "0.0001");
            Represents(0.00001, "0.00001");
            Represents(0.000001, "0.000001");
            Represents(0.0000001, "0.0000001");
            Represents(0.00000001, "0.00000001");
            Represents(0.000000001, "0.000000001");
            Represents(0.0000000001, "0.0000000001");
            Represents(0.00000000001, "0.00000000001");
            Represents(0.000000000001, "0.000000000001");
            Represents(0.0000000000001, "0.0000000000001");
            Represents(0.0000000000001, "0.0000000000001");
            Represents(0.00000000000001, "0.00000000000001");
            Represents(0.0000000000000001, "0");

            Represents(1.1, "1.1");
            Represents(1.12, "1.12");
            Represents(1.123, "1.123");
            Represents(1.1234, "1.1234");
            Represents(1.12345, "1.12345");
            Represents(1.123456, "1.123456");
            Represents(1.1234567, "1.1234567");
            Represents(1.12345678, "1.12345678");
            Represents(1.123456789, "1.123456789");
            Represents(1.1234567891, "1.1234567891");
            Represents(1.12345678912, "1.12345678912");
            Represents(1.123456789123, "1.123456789123");
            Represents(1.1234567891234, "1.1234567891234");
            Represents(1.12345678912345, "1.12345678912345");
            Represents(1.123456789123456, "1.12345678912346");
            Represents(1.1234567891234567, "1.12345678912346");
            Represents(1.12345678912345678, "1.12345678912346");
            Represents(1.123456789123456789, "1.12345678912346");
            Represents(1.1234567891234567891, "1.12345678912346");
            Represents(1.12345678912345678912, "1.12345678912346");
            Represents(1.123456789123456789123, "1.12345678912346");

            Represents(11.1, "11.1");
            Represents(11.12, "11.12");
            Represents(11.123, "11.123");
            Represents(11.1234, "11.1234");
            Represents(11.12345, "11.12345");
            Represents(11.123456, "11.123456");
            Represents(11.1234567, "11.1234567");
            Represents(11.12345678, "11.12345678");
            Represents(11.123456789, "11.123456789");
            Represents(11.1234567891, "11.1234567891");
            Represents(11.12345678912, "11.12345678912");
            Represents(11.123456789123, "11.123456789123");
            Represents(11.1234567891234, "11.1234567891234");
            Represents(11.12345678912345, "11.1234567891235");
            Represents(11.123456789123456, "11.1234567891235");
            Represents(11.1234567891234567, "11.1234567891235");
            Represents(11.12345678912345678, "11.1234567891235");
            Represents(11.123456789123456789, "11.1234567891235");
            Represents(11.1234567891234567891, "11.1234567891235");
            Represents(11.12345678912345678912, "11.1234567891235");
            Represents(11.123456789123456789123, "11.1234567891235");

            Represents(111.1, "111.1");
            Represents(111.12, "111.12");
            Represents(111.123, "111.123");
            Represents(111.1234, "111.1234");
            Represents(111.12345, "111.12345");
            Represents(111.123456, "111.123456");
            Represents(111.1234567, "111.1234567");
            Represents(111.12345678, "111.12345678");
            Represents(111.123456789, "111.123456789");
            Represents(111.1234567891, "111.1234567891");
            Represents(111.12345678912, "111.12345678912");
            Represents(111.123456789123, "111.123456789123");
            Represents(111.1234567891234, "111.123456789123");
            Represents(111.12345678912345, "111.123456789123");
            Represents(111.123456789123456, "111.123456789123");
            Represents(111.1234567891234567, "111.123456789123");
            Represents(111.12345678912345678, "111.123456789123");

            Represents(1111.1, "1111.1");
            Represents(1111.12, "1111.12");
            Represents(1111.123, "1111.123");
            Represents(1111.1234, "1111.1234");
            Represents(1111.12345, "1111.12345");
            Represents(1111.123456, "1111.123456");
            Represents(1111.1234567, "1111.1234567");
            Represents(1111.12345678, "1111.12345678");
            Represents(1111.123456789, "1111.123456789");
            Represents(1111.1234567891, "1111.1234567891");
            Represents(1111.12345678912, "1111.12345678912");
            Represents(1111.123456789123, "1111.12345678912");
            Represents(1111.1234567891234, "1111.12345678912");
            Represents(1111.12345678912345, "1111.12345678912");
            Represents(1111.123456789123456, "1111.12345678912");
            Represents(1111.1234567891234567, "1111.12345678912");

            Represents(11111.1, "11111.1");
            Represents(11111.12, "11111.12");
            Represents(11111.123, "11111.123");
            Represents(11111.1234, "11111.1234");
            Represents(11111.12345, "11111.12345");
            Represents(11111.123456, "11111.123456");
            Represents(11111.1234567, "11111.1234567");
            Represents(11111.12345678, "11111.12345678");
            Represents(11111.123456789, "11111.123456789");
            Represents(11111.1234567891, "11111.1234567891");
            Represents(11111.12345678912, "11111.1234567891");
            Represents(11111.123456789123, "11111.1234567891");
            Represents(11111.1234567891234, "11111.1234567891");
            Represents(11111.12345678912345, "11111.1234567891");
            Represents(11111.123456789123456, "11111.1234567891");

            Represents(111111.1, "111111.1");
            Represents(111111.12, "111111.12");
            Represents(111111.123, "111111.123");
            Represents(111111.1234, "111111.1234");
            Represents(111111.12345, "111111.12345");
            Represents(111111.123456, "111111.123456");
            Represents(111111.1234567, "111111.1234567");
            Represents(111111.12345678, "111111.12345678");
            Represents(111111.123456789, "111111.123456789");
            Represents(111111.1234567891, "111111.123456789");
            Represents(111111.12345678912, "111111.123456789");
            Represents(111111.123456789123, "111111.123456789");
            Represents(111111.1234567891234, "111111.123456789");
            Represents(111111.12345678912345, "111111.123456789");

            Represents(1111111.0, "1111111");
            Represents(1111111.1, "1111111.1");
            Represents(1111111.12, "1111111.12");
            Represents(1111111.123, "1111111.123");
            Represents(1111111.1234, "1111111.1234");
            Represents(1111111.12345, "1111111.12345");
            Represents(1111111.123456, "1111111.123456");
            Represents(1111111.1234567, "1111111.1234567");
            Represents(1111111.12345678, "1111111.12345678");
            Represents(1111111.123456789, "1111111.12345679");
            Represents(1111111.1234567891, "1111111.12345679");
            Represents(1111111.12345678912, "1111111.12345679");
            Represents(1111111.123456789123, "1111111.12345679");
            Represents(1111111.1234567891234, "1111111.12345679");

            Represents(11111111.0, "11111111");
            Represents(11111111.1, "11111111.1");
            Represents(11111111.12, "11111111.12");
            Represents(11111111.123, "11111111.123");
            Represents(11111111.1234, "11111111.1234");
            Represents(11111111.12345, "11111111.12345");
            Represents(11111111.123456, "11111111.123456");
            Represents(11111111.1234567, "11111111.1234567");
            Represents(11111111.12345678, "11111111.1234568");
            Represents(11111111.123456789, "11111111.1234568");
            Represents(11111111.1234567891, "11111111.1234568");
            Represents(11111111.12345678912, "11111111.1234568");
            Represents(11111111.123456789123, "11111111.1234568");

            Represents(111111111.0, "111111111");
            Represents(111111111.1, "111111111.1");
            Represents(111111111.12, "111111111.12");
            Represents(111111111.123, "111111111.123");
            Represents(111111111.1234, "111111111.1234");
            Represents(111111111.12345, "111111111.12345");
            Represents(111111111.123456, "111111111.123456");
            Represents(111111111.1234567, "111111111.123457");
            Represents(111111111.12345678, "111111111.123457");
            Represents(111111111.123456789, "111111111.123457");
            Represents(111111111.1234567891, "111111111.123457");
            Represents(111111111.12345678912, "111111111.123457");

            Represents(1111111111.0, "1111111111");
            Represents(1111111111.1, "1111111111.1");
            Represents(1111111111.12, "1111111111.12");
            Represents(1111111111.123, "1111111111.123");
            Represents(1111111111.1234, "1111111111.1234");
            Represents(1111111111.12345, "1111111111.12345");
            Represents(1111111111.123456, "1111111111.12346");
            Represents(1111111111.1234567, "1111111111.12346");
            Represents(1111111111.12345678, "1111111111.12346");
            Represents(1111111111.123456789, "1111111111.12346");
            Represents(1111111111.1234567891, "1111111111.12346");

            Represents(11111111111.0, "11111111111");
            Represents(11111111111.1, "11111111111.1");
            Represents(11111111111.12, "11111111111.12");
            Represents(11111111111.123, "11111111111.123");
            Represents(11111111111.1234, "11111111111.1234");
            Represents(11111111111.12345, "11111111111.1234");
            Represents(11111111111.123456, "11111111111.1235");
            Represents(11111111111.1234567, "11111111111.1235");
            Represents(11111111111.12345678, "11111111111.1235");
            Represents(11111111111.123456789, "11111111111.1235");

            Represents(111111111111.0, "111111111111");
            Represents(111111111111.1, "111111111111.1");
            Represents(111111111111.12, "111111111111.12");
            Represents(111111111111.123, "111111111111.123");
            Represents(111111111111.1234, "111111111111.123");
            Represents(111111111111.12345, "111111111111.123");
            Represents(111111111111.123456, "111111111111.123");
            Represents(111111111111.1234567, "111111111111.123");
            Represents(111111111111.12345678, "111111111111.123");

            Represents(1111111111111.0, "1111111111111");
            Represents(1111111111111.1, "1111111111111.1");
            Represents(1111111111111.12, "1111111111111.12");
            Represents(1111111111111.123, "1111111111111.12");
            Represents(1111111111111.1234, "1111111111111.12");
            Represents(1111111111111.12345, "1111111111111.12");
            Represents(1111111111111.123456, "1111111111111.12");
            Represents(1111111111111.1234567, "1111111111111.12");

            Represents(11111111111111.0, "11111111111111");
            Represents(11111111111111.1, "11111111111111.1");
            Represents(11111111111111.12, "11111111111111.1");
            Represents(11111111111111.123, "11111111111111.1");
            Represents(11111111111111.1234, "11111111111111.1");
            Represents(11111111111111.12345, "11111111111111.1");
            Represents(11111111111111.123456, "11111111111111.1");

            Represents(111111111111111.0, "111111111111111");
            Represents(111111111111111.1, "111111111111111");
            Represents(111111111111111.12, "111111111111111");
            Represents(111111111111111.123, "111111111111111");
            Represents(111111111111111.1234, "111111111111111");
            Represents(111111111111111.12345, "111111111111111");

            Represents(1111111111111111.0, "111111111111111E1");
            Represents(1111111111111111.1, "111111111111111E1");
            Represents(1111111111111111.12, "111111111111111E1");
            Represents(1111111111111111.123, "111111111111111E1");
            Represents(1111111111111111.1234, "111111111111111E1");

            Represents(11111111111111111.0, "111111111111111E2");
            Represents(11111111111111111.1, "111111111111111E2");
            Represents(11111111111111111.12, "111111111111111E2");
            Represents(11111111111111111.123, "111111111111111E2");

            Represents(111111111111111111.0, "111111111111111E3");
            Represents(111111111111111111.1, "111111111111111E3");
            Represents(111111111111111111.12, "111111111111111E3");

            Represents(1111111111111111111.0, "111111111111111E4");
            Represents(1111111111111111111.1, "111111111111111E4");

            Represents(11111111111111111111.0, "111111111111111E5");
            Represents(111111111111111111111.0, "111111111111111E6");
            Represents(1111111111111111111111.0, "111111111111111E7");
            Represents(11111111111111111111111.0, "111111111111111E8");
            Represents(111111111111111111111111.0, "111111111111111E9");
            Represents(1111111111111111111111111.0, "111111111111111E10");
            Represents(11111111111111111111111111.0, "111111111111111E11");
            Represents(111111111111111111111111111.0, "111111111111111E12");
            Represents(1111111111111111111111111111.0, "111111111111111E13");
            Represents(11111111111111111111111111111.0, "111111111111111E14");
            Represents(111111111111111111111111111111.0, "111111111111111E15");
            Represents(1111111111111111111111111111111.0, "111111111111111E16");

            Represents(0, "0");
            Represents(0.1, "0.1");
            Represents(0.02, "0.02");
            Represents(0.102, "0.102");
            Represents(0.1002, "0.1002");
            Represents(0.10203, "0.10203");
            Represents(0.102034, "0.102034");
            Represents(0.1020341, "0.1020341");
            Represents(0.10203411, "0.10203411");
            Represents(0.102034111, "0.102034111");
            Represents(0.1020341101, "0.1020341101");
            Represents(0.10203411001, "0.10203411001");
            Represents(0.102034110001, "0.102034110001");
            Represents(0.1020341100001, "0.1020341100001");
            Represents(0.10203411000001, "0.10203411000001");
            Represents(0.102034110000001, "0.102034110000001");
            Represents(0.1020341100000001, "0.10203411");

            Represents(0.523154234575693, "0.523154234575693");
        }

        [Fact]
        public void Float()
        {
            Represents(0f, "0");
            Represents(0.0f, "0");
            Represents(0.1f, "0.1");
            Represents(0.01f, "0.01");
            Represents(0.001f, "0.001");
            Represents(0.0001f, "0.0001");
            Represents(0.00001f, "0.00001");
            Represents(0.000001f, "0.000001");
            Represents(0.0000001f, "0.0000001");
            Represents(0.00000001f, "0");

            Represents(1.1f, "1.1");
            Represents(1.12f, "1.12");
            Represents(1.123f, "1.123");
            Represents(1.1234f, "1.1234");
            Represents(1.12345f, "1.12345");
            Represents(1.123456f, "1.123456");
            Represents(1.1234567f, "1.123457");
            Represents(1.12345678f, "1.123457");

            Represents(11.1f, "11.1");
            Represents(11.12f, "11.12");
            Represents(11.123f, "11.123");
            Represents(11.1234f, "11.1234");
            Represents(11.12345f, "11.12345");
            Represents(11.123456f, "11.12346");
            Represents(11.1234567f, "11.12346");
            Represents(11.12345678f, "11.12346");

            Represents(111.1f, "111.1");
            Represents(111.12f, "111.12");
            Represents(111.123f, "111.123");
            Represents(111.1234f, "111.1234");
            Represents(111.12345f, "111.1235");
            Represents(111.123456f, "111.1235");
            Represents(111.1234567f, "111.1235");

            Represents(1111.1f, "1111.1");
            Represents(1111.12f, "1111.12");
            Represents(1111.123f, "1111.123");
            Represents(1111.1234f, "1111.123");
            Represents(1111.12345f, "1111.123");
            Represents(1111.123456f, "1111.123");

            Represents(11111.1f, "11111.1");
            Represents(11111.12f, "11111.12");
            Represents(11111.123f, "11111.12");
            Represents(11111.1234f, "11111.12");
            Represents(11111.12345f, "11111.12");

            Represents(111111.1f, "111111.1");
            Represents(111111.12f, "111111.1");
            Represents(111111.123f, "111111.1");
            Represents(111111.1234f, "111111.1");

            Represents(1111111f, "1111111");
            Represents(1111111.1f, "1111111");
            Represents(1111111.12f, "1111111");
            Represents(1111111.123f, "1111111");

            Represents(11111111f, "1111111E1");
            Represents(111111111f, "1111111E2");
            Represents(1111111111f, "1111111E3");
            Represents(11111111111f, "1111111E4");
            Represents(111111111111f, "1111111E5");
            Represents(1111111111111f, "1111111E6");
            Represents(11111111111111f, "1111111E7");
            Represents(111111111111111f, "1111111E8");
            Represents(1111111111111111f, "1111111E9");
            Represents(11111111111111111f, "1111111E10");
            Represents(111111111111111111f, "1111111E11");
            Represents(1111111111111111111f, "1111111E12");
            Represents(11111111111111111111f, "1111111E13");
            Represents(111111111111111111111f, "1111111E14");
            Represents(1111111111111111111111f, "1111111E15");
            Represents(11111111111111111111111f, "1111111E16");
            Represents(111111111111111111111111f, "1111111E17");
            Represents(1111111111111111111111111f, "1111111E18");

            Represents(0f, "0");
            Represents(0.1f, "0.1");
            Represents(0.02f, "0.02");
            Represents(0.102f, "0.102");
            Represents(0.1002f, "0.1002");
            Represents(0.10203f, "0.10203");
            Represents(0.102034f, "0.102034");
        }

        [Fact]
        public void Integer()
        {
            Represents(1, "1");
            Represents(11, "11");
            Represents(111, "111");
            Represents(1111, "1111");
            Represents(11111, "11111");
            Represents(111111, "111111");
            Represents(1111111, "1111111");
            Represents(11111111, "11111111");
            Represents(111111111, "111111111");

            Assert.Equal(int.MaxValue.ToString(), int.MaxValue.AsJson());
            Assert.Equal(int.MinValue.ToString(), int.MinValue.AsJson());

            Assert.Equal((int.MaxValue - 1).ToString(), (int.MaxValue - 1).AsJson());
            Assert.Equal((int.MinValue + 1).ToString(), (int.MinValue + 1).AsJson());

            var rand = new Random(1111);
            for (var i = 0; i < 1000; i++)
            {
                var number = rand.Next();
                Represents(number, number.ToString());
            }
        }

        [Fact]
        public void Long()
        {
            Represents(1L, "1");
            Represents(11L, "11");
            Represents(111L, "111");
            Represents(1111L, "1111");
            Represents(11111L, "11111");
            Represents(111111L, "111111");
            Represents(1111111L, "1111111");
            Represents(11111111L, "11111111");
            Represents(111111111L, "111111111");
            Represents(1111111111L, "1111111111");
            Represents(11111111111L, "11111111111");
            Represents(111111111111L, "111111111111");
            Represents(1111111111111L, "1111111111111");
            Represents(11111111111111L, "11111111111111");
            Represents(111111111111111L, "111111111111111");
            Represents(1111111111111111L, "1111111111111111");
            Represents(11111111111111111L, "11111111111111111");
            Represents(111111111111111111L, "111111111111111111");
            Represents(1111111111111111111L, "1111111111111111111");

            Assert.Equal(long.MaxValue.ToString(), long.MaxValue.AsJson());
            Assert.Equal(long.MinValue.ToString(), long.MinValue.AsJson());
        }

        [Fact]
        public void String()
        {
            Represents("Hello", "\"Hello\"");
            Represents("Josh", "\"Josh\"");
        }
        
        private static void Represents(int item, string representation)
        {
            Assert.Equal(representation, item.AsJson());

            if (item != 0)
                Assert.Equal("-" + representation, (-item).AsJson());
        }

        private static void Represents(long item, string representation)
        {
            Assert.Equal(representation, item.AsJson());

            if (item != 0)
                Assert.Equal("-" + representation, (-item).AsJson());
        }

        private static void Represents(float item, string representation)
        {
            Assert.Equal(representation, item.AsJson());

            if (Math.Abs(item) > 0.00000000000000000000001)
                Assert.Equal("-" + representation, (-item).AsJson());
        }

        private static void Represents(double item, string representation)
        {
            Assert.Equal(representation, item.AsJson());

            if (Math.Abs(item) > 0.00000000000000000000001)
                Assert.Equal("-" + representation, (-item).AsJson());
        }

        private static void Represents<T>(T item, string representation)
        {
            Assert.Equal(representation, item.AsJson());
        }
    }
}