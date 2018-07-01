using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonCharTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends('a').As("a");
            Appends('\r').As("\r");
            Appends('\n').As("\n");
            Appends('Ş').As("Ş");
            Appends('ğ').As("ğ");
        }

        [Fact]
        public void Unwrap_ShouldWorkCorrectly()
        {
            Unwraps("a").As('a');
            Unwraps("\r").As('\r');
            Unwraps("\n").As('\n');
            Unwraps("Ş").As('Ş');
            Unwraps("ğ").As('ğ');
        }
    }
}