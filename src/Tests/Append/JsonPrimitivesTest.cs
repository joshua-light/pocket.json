using System;
using Xunit;

namespace Pocket.Json.Tests.Append
{
    public class JsonPrimitivesTest
    {
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