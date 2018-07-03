using System.Linq;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonEnumerableTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertEmptyEnumerableToBrackets() => Appends(Enumerable.Empty<int>()).As("[]");
    }
}