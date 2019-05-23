using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonIntTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(1).As("1");
            Appends(11).As("11");
            Appends(111).As("111");
            Appends(1111).As("1111");
            Appends(11111).As("11111");
            Appends(111111).As("111111");
            Appends(1111111).As("1111111");
            Appends(11111111).As("11111111");
            Appends(111111111).As("111111111");
            
            Assert.Equal(int.MaxValue.ToString(), int.MaxValue.AsJson());
            Assert.Equal(int.MinValue.ToString(), int.MinValue.AsJson());

            Assert.Equal((int.MaxValue - 1).ToString(), (int.MaxValue - 1).AsJson());
            Assert.Equal((int.MinValue + 1).ToString(), (int.MinValue + 1).AsJson());
        }

        [Fact]
        public void Unwrap_ShouldWorkCorrectly()
        {
            Unwraps("1").As(1);
            Unwraps("11").As(11);
            Unwraps("111").As(111);
            Unwraps("1111").As(1111);
            Unwraps("11111").As(11111);
            Unwraps("111111").As(111111);
            Unwraps("1111111").As(1111111);
            Unwraps("11111111").As(11111111);
            Unwraps("111111111").As(111111111);
            Unwraps("1111111111").As(1111111111);

            Assert.Equal(int.MaxValue - 1, (int.MaxValue - 1).ToString().OfJson<int>());
            Assert.Equal(int.MinValue + 1, (int.MinValue + 1).ToString().OfJson<int>());

            Assert.Equal(int.MaxValue, int.MaxValue.ToString().OfJson<int>());
            Assert.Equal(int.MinValue, int.MinValue.ToString().OfJson<int>());
        }
    }
}