using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonArrayTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertEmptyArrayToBrackets() => Appends(new int[0]).As("[]");

        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
           Appends(new[] { 1, 2, 3 }).As("[1,2,3]");
           Appends(new[] { "1", "2", "3" }).As("[\"1\",\"2\",\"3\"]");
            
            Appends(
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
        public void Unwraps_ShouldConvertBracketsToEmptyArray() => Unwraps("[]").As(new int[0]);

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            Unwraps("[1,2,3]").As(new[] { 1, 2, 3 });
            Unwraps("[\"1\",\"2\",\"3\"]").As(new[] { "1", "2", "3" });
            
            Unwraps(
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