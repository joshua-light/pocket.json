using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonByteTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
                Writes(i).As(i.ToString());
        }

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
                Reads(i.ToString()).As<byte>((byte) i);
        }
    }
}