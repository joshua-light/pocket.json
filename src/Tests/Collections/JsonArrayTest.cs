using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonArrayTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertEmptyArrayToBrackets() => Writes(new int[0]).As("[]");

        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
           Writes(new[] { 1, 2, 3 }).As("[1,2,3]");
           Writes(new[] { "1", "2", "3" }).As("[\"1\",\"2\",\"3\"]");
            
            Writes(
                new[]
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
        public void Reads_ShouldConvertBracketsToEmptyArray() => Reads("[]").As(new int[0]);

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            Reads("[1,2,3]").As(new[] { 1, 2, 3 });
            Reads("[\"1\",\"2\",\"3\"]").As(new[] { "1", "2", "3" });
            
            Reads(
                "[" +
                "{\"Item1\":1,\"Item2\":2}," +
                "{\"Item1\":3,\"Item2\":4}," +
                "{\"Item1\":5,\"Item2\":6}," +
                "{\"Item1\":7,\"Item2\":8}" +
                "]")
                .As(new[]
                {
                    new IntAndInt{ Item1 = 1, Item2 = 2 },
                    new IntAndInt{ Item1 = 3, Item2 = 4 },
                    new IntAndInt{ Item1 = 5, Item2 = 6 },
                    new IntAndInt{ Item1 = 7, Item2 = 8 },
                });
        }
    }
}