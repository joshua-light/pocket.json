using System.Collections.Generic;
using Xunit;

namespace Pocket.Json.Tests.Collections
{
    public class JsonDictionaryTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertEmptyDictionaryToBrackets() =>
            Writes(new Dictionary<int, string>()).As("{}");

        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes(new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            }).As("{1:1,2:1,3:1}");

            Writes(new Dictionary<string, IntAndString>
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
        public void Reads_ShouldConvertBracketsToEmptyDictionary() =>
            Reads("{}").As(new Dictionary<int, string>());

        [Fact]
        public void Reads_ShouldWorkCorrectly()
        {
            Reads("{1:1,2:1,3:1}").As(new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            });
            
            Reads(
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