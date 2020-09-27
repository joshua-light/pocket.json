using System.Linq;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonEnumerableTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertEmptyEnumerableToBrackets() => Writes(Enumerable.Empty<int>()).As("[]");
    }
}