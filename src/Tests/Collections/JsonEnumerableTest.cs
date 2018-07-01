using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonEnumerableTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertEmptyEnumerableToBrackets() =>
            Appends(Enumerable.Empty<int>()).As("[]");
        
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(new HashSet<int> { 1, 2, 3 }).As("[1,2,3]");
            Appends(new HashSet<string> { "1", "2", "3" }).As("[\"1\",\"2\",\"3\"]");
            
            Appends(
                    new HashSet<IntAndInt>
                    {
                        new IntAndInt{ Item1 = 1, Item2 = 2 },
                        new IntAndInt{ Item1 = 3, Item2 = 4 },
                        new IntAndInt{ Item1 = 5, Item2 = 6 },
                        new IntAndInt{ Item1 = 7, Item2 = 8 },
                    })
                .As("[" +
                    "{\"Item1\":1,\"Item2\":2}," +
                    "{\"Item1\":3,\"Item2\":4}," +
                    "{\"Item1\":5,\"Item2\":6}," +
                    "{\"Item1\":7,\"Item2\":8}" +
                    "]");
        }
    }
}