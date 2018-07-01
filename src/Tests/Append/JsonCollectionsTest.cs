using System.Collections.Generic;
using Xunit;

namespace Pocket.Json.Tests.Append
{
    public class JsonCollectionsTest
    {
        [Fact]
        public void HashSet()
        {
            Represents(new HashSet<int>(), "[]");
            Represents(new HashSet<string> { "Hello" }, "[\"Hello\"]");
            Represents(new HashSet<int> { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]");
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