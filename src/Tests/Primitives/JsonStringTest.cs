using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonStringTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes("Hello").As("\"Hello\"");
            Writes("Josh").As("\"Josh\"");
        }

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            Reads("\"0\"").As("0");
            Reads("\"Hello\"").As("Hello");
            Reads("\"Hello, guys!\"").As("Hello, guys!");
            Reads("\"    ����          PTest123541234123Test123541234123Test123541234123Test123541234123Test123541234123\"")
                .As("    ����          PTest123541234123Test123541234123Test123541234123Test123541234123Test123541234123");
        }
    }
}