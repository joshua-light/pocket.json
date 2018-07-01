using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonStringTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends("Hello").As("\"Hello\"");
            Appends("Josh").As("\"Josh\"");
        }

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            Unwraps("\"0\"").As("0");
            Unwraps("\"Hello\"").As("Hello");
            Unwraps("\"Hello, guys!\"").As("Hello, guys!");
        }
    }
}