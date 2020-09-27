using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonCharTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes('a').As("a");
            Writes('\r').As("\r");
            Writes('\n').As("\n");
            Writes('Ş').As("Ş");
            Writes('ğ').As("ğ");
        }

        [Fact]
        public void Read_ShouldWorkCorrectly()
        {
            Reads("a").As('a');
            Reads("\r").As('\r');
            Reads("\n").As('\n');
            Reads("Ş").As('Ş');
            Reads("ğ").As('ğ');
        }
    }
}