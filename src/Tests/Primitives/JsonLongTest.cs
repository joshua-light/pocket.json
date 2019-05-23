using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonLongTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(1L).As("1");
            Appends(11L).As("11");
            Appends(111L).As("111");
            Appends(1111L).As("1111");
            Appends(11111L).As("11111");
            Appends(111111L).As("111111");
            Appends(1111111L).As("1111111");
            Appends(11111111L).As("11111111");
            Appends(111111111L).As("111111111");
            Appends(1111111111L).As("1111111111");
            Appends(11111111111L).As("11111111111");
            Appends(111111111111L).As("111111111111");
            Appends(1111111111111L).As("1111111111111");
            Appends(11111111111111L).As("11111111111111");
            Appends(111111111111111L).As("111111111111111");
            Appends(1111111111111111L).As("1111111111111111");
            Appends(11111111111111111L).As("11111111111111111");
            Appends(111111111111111111L).As("111111111111111111");
            Appends(1111111111111111111L).As("1111111111111111111");

            Assert.Equal(long.MaxValue.ToString(), long.MaxValue.AsJson());
            Assert.Equal(long.MinValue.ToString(), long.MinValue.AsJson());
            
            Assert.Equal((long.MaxValue - 1).ToString(), (long.MaxValue - 1).AsJson());
            Assert.Equal((long.MinValue + 1).ToString(), (long.MinValue + 1).AsJson());
        }

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            Unwraps("1").As(1L);
            Unwraps("11").As(11L);
            Unwraps("111").As(111L);
            Unwraps("1111").As(1111L);
            Unwraps("11111").As(11111L);
            Unwraps("111111").As(111111L);
            Unwraps("1111111").As(1111111L);
            Unwraps("11111111").As(11111111L);
            Unwraps("111111111").As(111111111L);
            Unwraps("1111111111").As(1111111111L);
            Unwraps("11111111111").As(11111111111L);
            Unwraps("111111111111").As(111111111111L);
            Unwraps("1111111111111").As(1111111111111L);
            Unwraps("11111111111111").As(11111111111111L);
            Unwraps("111111111111111").As(111111111111111L);
            Unwraps("1111111111111111").As(1111111111111111L);
            Unwraps("11111111111111111").As(11111111111111111L);
            Unwraps("111111111111111111").As(111111111111111111L);
            Unwraps("1111111111111111111").As(1111111111111111111L);

            Assert.Equal(long.MaxValue - 1, (long.MaxValue - 1).ToString().OfJson<long>());
            Assert.Equal(long.MinValue + 1, (long.MinValue + 1).ToString().OfJson<long>());

            Assert.Equal(long.MaxValue, long.MaxValue.ToString().OfJson<long>());
            Assert.Equal(long.MinValue, long.MinValue.ToString().OfJson<long>());
        }
    }
}