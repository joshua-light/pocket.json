using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonByteTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
                Appends(i).As(i.ToString());
        }

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
                Unwraps(i.ToString()).As(i);
        }
    }
}