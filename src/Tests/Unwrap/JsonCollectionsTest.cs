using System;
using System.Collections.Generic;
using Xunit;

namespace Pocket.Json.Tests.Unwrap
{
    public class JsonCollectionsTest
    {
        [Fact]
        public void Array()
        {
            Represents("[]", new int[0]);
            Represents("[\"Hello\"]", new[] { "Hello" });
            Represents("[\"Hello\",\"Hello1\"]", new[] { "Hello", "Hello1" });
            Represents("[1,2,3,4,5]", new[] { 1, 2, 3, 4, 5 });
            Represents("[{\"Data1\":1,\"Data2\":2}]", new [] { new Item{ Data1 = 1, Data2 = 2 } });
        }
        
        [Fact]
        public void List()
        {
            Represents("[]", new List<int>());
            Represents("[\"Hello\"]", new List<string> { "Hello" });
            Represents("[1,2,3,4,5]", new List<int> { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void Dictionary()
        {
            Represents("{}", new Dictionary<int, int>());
            Represents("{1:1,2:1,3:1}", new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 }
            });
            Represents(
                "{\"1\":{\"Data1\":1,\"Data2\":2},\"2\":{\"Data1\":0,\"Data2\":20},\"3\":{\"Data1\":100,\"Data2\":0}}",
                new Dictionary<string, Item>
                {
                    { "1", new Item{ Data1 = 1, Data2 = 2 } },
                    { "2", new Item{            Data2 = 20 } },
                    { "3", new Item{ Data1 = 100 } },
                });
        }
        
        public class Item : IEquatable<Item>
        {
            public int Data1;
            public int Data2;

            public bool Equals(Item other) => Data1 == other.Data1 && Data2 == other.Data2;
        }
        
        private static void Represents<T>(string representation, T item)
        {
            Assert.Equal(representation.AsJson<T>(), item);
        }
    }
}