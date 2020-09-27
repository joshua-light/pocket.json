using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonLongTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes(1L).As("1");
            Writes(11L).As("11");
            Writes(111L).As("111");
            Writes(1111L).As("1111");
            Writes(11111L).As("11111");
            Writes(111111L).As("111111");
            Writes(1111111L).As("1111111");
            Writes(11111111L).As("11111111");
            Writes(111111111L).As("111111111");
            Writes(1111111111L).As("1111111111");
            Writes(11111111111L).As("11111111111");
            Writes(111111111111L).As("111111111111");
            Writes(1111111111111L).As("1111111111111");
            Writes(11111111111111L).As("11111111111111");
            Writes(111111111111111L).As("111111111111111");
            Writes(1111111111111111L).As("1111111111111111");
            Writes(11111111111111111L).As("11111111111111111");
            Writes(111111111111111111L).As("111111111111111111");
            Writes(1111111111111111111L).As("1111111111111111111");

            Assert.Equal(long.MaxValue.ToString(), long.MaxValue.ToJson());
            Assert.Equal(long.MinValue.ToString(), long.MinValue.ToJson());
            
            Assert.Equal((long.MaxValue - 1).ToString(), (long.MaxValue - 1).ToJson());
            Assert.Equal((long.MinValue + 1).ToString(), (long.MinValue + 1).ToJson());
        }

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            Reads("1").As(1L);
            Reads("11").As(11L);
            Reads("111").As(111L);
            Reads("1111").As(1111L);
            Reads("11111").As(11111L);
            Reads("111111").As(111111L);
            Reads("1111111").As(1111111L);
            Reads("11111111").As(11111111L);
            Reads("111111111").As(111111111L);
            Reads("1111111111").As(1111111111L);
            Reads("11111111111").As(11111111111L);
            Reads("111111111111").As(111111111111L);
            Reads("1111111111111").As(1111111111111L);
            Reads("11111111111111").As(11111111111111L);
            Reads("111111111111111").As(111111111111111L);
            Reads("1111111111111111").As(1111111111111111L);
            Reads("11111111111111111").As(11111111111111111L);
            Reads("111111111111111111").As(111111111111111111L);
            Reads("1111111111111111111").As(1111111111111111111L);

            Assert.Equal(long.MaxValue - 1, (long.MaxValue - 1).ToString().FromJson<long>());
            Assert.Equal(long.MinValue + 1, (long.MinValue + 1).ToString().FromJson<long>());

            Assert.Equal(long.MaxValue, long.MaxValue.ToString().FromJson<long>());
            Assert.Equal(long.MinValue, long.MinValue.ToString().FromJson<long>());
        }
    }
}