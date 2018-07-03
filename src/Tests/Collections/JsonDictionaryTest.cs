using System.Collections.Generic;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonDictionaryTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertEmptyDictionaryToBrackets() =>
            Appends(new Dictionary<int, string>()).As("{}");

        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            }).As("{1:1,2:1,3:1}");

            Appends(new Dictionary<string, IntAndString>
            {
                { "1", new IntAndString { Item1 = 2, Item2 = "3" } },
                { "4", new IntAndString { Item1 = 5, Item2 = "6" } },
                { "7", new IntAndString { Item1 = 8, Item2 = "9" } },
            }).As(
                "{" +
                "\"1\":{\"Item1\":2,\"Item2\":\"3\"}," +
                "\"4\":{\"Item1\":5,\"Item2\":\"6\"}," +
                "\"7\":{\"Item1\":8,\"Item2\":\"9\"}" +
                "}");
        }
        
        [Fact]
        public void Unwraps_ShouldConvertBracketsToEmptyDictionary() =>
            Unwraps("{}").As(new Dictionary<int, string>());

        [Fact]
        public void Unwraps_ShouldWorkCorrectly()
        {
            Unwraps("{1:1,2:1,3:1}").As(new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            });
            
            Unwraps(
                "{" +
                "\"1\":{\"Item1\":2,\"Item2\":\"3\"}," +
                "\"4\":{\"Item1\":5,\"Item2\":\"6\"}," +
                "\"7\":{\"Item1\":8,\"Item2\":\"9\"}" +
                "}").As(
                new Dictionary<string, IntAndString>
                {
                    { "1", new IntAndString { Item1 = 2, Item2 = "3" } },
                    { "4", new IntAndString { Item1 = 5, Item2 = "6" } },
                    { "7", new IntAndString { Item1 = 8, Item2 = "9" } },
                });
        }
    }
}