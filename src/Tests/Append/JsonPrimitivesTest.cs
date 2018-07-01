using System;
using Xunit;

namespace Pocket.Json.Tests.Append
{
    public class JsonPrimitivesTest
    {
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