using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonIntTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes(1).As("1");
            Writes(11).As("11");
            Writes(111).As("111");
            Writes(1111).As("1111");
            Writes(11111).As("11111");
            Writes(111111).As("111111");
            Writes(1111111).As("1111111");
            Writes(11111111).As("11111111");
            Writes(111111111).As("111111111");
            
            Assert.Equal(int.MaxValue.ToString(), int.MaxValue.ToJson());
            Assert.Equal(int.MinValue.ToString(), int.MinValue.ToJson());

            Assert.Equal((int.MaxValue - 1).ToString(), (int.MaxValue - 1).ToJson());
            Assert.Equal((int.MinValue + 1).ToString(), (int.MinValue + 1).ToJson());
        }

        [Fact]
        public void Read_ShouldWorkCorrectly()
        {
            Reads("1").As(1);
            Reads("11").As(11);
            Reads("111").As(111);
            Reads("1111").As(1111);
            Reads("11111").As(11111);
            Reads("111111").As(111111);
            Reads("1111111").As(1111111);
            Reads("11111111").As(11111111);
            Reads("111111111").As(111111111);
            Reads("1111111111").As(1111111111);

            Assert.Equal(int.MaxValue - 1, (int.MaxValue - 1).ToString().FromJson<int>());
            Assert.Equal(int.MinValue + 1, (int.MinValue + 1).ToString().FromJson<int>());

            Assert.Equal(int.MaxValue, int.MaxValue.ToString().FromJson<int>());
            Assert.Equal(int.MinValue, int.MinValue.ToString().FromJson<int>());
        }
    }
}