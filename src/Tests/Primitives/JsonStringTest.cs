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
            Appends("\"1\"2").As("\"\\\"1\\\"2\"");
        }

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            Unwraps("\"0\"").As("0");
            Unwraps("\"Hello\"").As("Hello");
            Unwraps("\"Hello, guys!\"").As("Hello, guys!");
            Unwraps("\"    ����          PTest123541234123Test123541234123Test123541234123Test123541234123Test123541234123//\"")
                .As("    ����          PTest123541234123Test123541234123Test123541234123Test123541234123Test123541234123//");
            Unwraps("\"\\\"1\\\"2\"").As("\"1\"2");
        }
    }
}