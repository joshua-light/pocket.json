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
        }
        
        private static void Represents<T>(string representation, T item)
        {
            Assert.Equal(representation.AsJson<T>(), item);
        }
    }
}