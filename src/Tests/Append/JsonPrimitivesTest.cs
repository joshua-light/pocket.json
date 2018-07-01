using System;
using Xunit;

namespace Pocket.Json.Tests.Append
{
    public class JsonPrimitivesTest
    {
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