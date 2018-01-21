using System.Collections.Generic;
using Xunit;

namespace Castalia.Tests.Core.Append
{
    public class JsonCollectionsTest
    {
        private static void Represents<T>(T item, string representation)
        {
            Assert.Equal(representation, item.AsJson());
        }

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
    }
}