using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonHashSetTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertEmptyHashSetToBrackets() => Writes(new HashSet<int>()).As("[]");
        
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes(new HashSet<int> { 1, 2, 3 }).As("[1,2,3]");
            Writes(new HashSet<string> { "1", "2", "3" }).As("[\"1\",\"2\",\"3\"]");
            
            Writes(
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
        
        [Fact]
        public void Reads_ShouldConvertBracketsToEmptyHashSet() => Reads("[]").As(new HashSet<int>());

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            Reads("[1,2,3]").As(new HashSet<int> { 1, 2, 3 });
            Reads("[\"1\",\"2\",\"3\"]").As(new HashSet<string> { "1", "2", "3" });
            
            Reads(
                    "[" +
                    "{\"Item1\":1,\"Item2\":2}," +
                    "{\"Item1\":3,\"Item2\":4}," +
                    "{\"Item1\":5,\"Item2\":6}," +
                    "{\"Item1\":7,\"Item2\":8}" +
                    "]")
                .As(new HashSet<IntAndInt>
                {
                    new IntAndInt{ Item1 = 1, Item2 = 2 },
                    new IntAndInt{ Item1 = 3, Item2 = 4 },
                    new IntAndInt{ Item1 = 5, Item2 = 6 },
                    new IntAndInt{ Item1 = 7, Item2 = 8 },
                });
        }
    }
}