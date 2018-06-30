using System.Collections.Generic;
using Xunit;

namespace Pocket.Json.Tests.Append
{
    public class JsonCollectionsTest
    {
        [Fact]
        public void Array()
        {
            Represents(new int[0], "[]");
            Represents(new[] { "Hello" }, "[\"Hello\"]");
            Represents(new[] { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]");
        }

        [Fact]
        public void Dictionary()
        {
            Represents(new Dictionary<int, int>(), "{}");
            Represents(new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            }, "{1:1,2:1,3:1}");
            Represents(new Dictionary<string, Item>
            {
                { "1", new Item{ Data1 = 1, Data2 = 2 } },
                { "2", new Item{            Data2 = 20 } },
                { "3", new Item{ Data1 = 100 } },
            }, "{\"1\":{\"Data1\":1,\"Data2\":2},\"2\":{\"Data1\":0,\"Data2\":20},\"3\":{\"Data1\":100,\"Data2\":0}}");
        }

        [Fact]
        public void HashSet()
        {
            Represents(new HashSet<int>(), "[]");
            Represents(new HashSet<string> { "Hello" }, "[\"Hello\"]");
            Represents(new HashSet<int> { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]");
        }

        [Fact]
        public void List()
        {
            Represents(new List<int>(), "[]");
            Represents(new List<string> { "Hello" }, "[\"Hello\"]");
            Represents(new List<int> { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]");
        }

        #region Helpers

        public class Item
        {
            public int Data1;
            public int Data2;
        }

        private static void Represents<T>(T item, string representation)
        {
            Assert.Equal(representation, item.AsJson());
        }

        #endregion
    }
}