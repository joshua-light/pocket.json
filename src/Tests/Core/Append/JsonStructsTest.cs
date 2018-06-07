using Xunit;

namespace Castalia.Tests.Core.Append
{
    public class JsonStructsTest
    {
        [Fact]
        public void NestedItems()
        {
            Represents(
                new BothItems
                {
                    Data = true,
                    Item = new Item { Data = 5 },
                    Item2 = new Item2 { Data = -1000, Data2 = 1000 }
                },
                "{\"Data\":1,\"Item\":{\"Data\":5},\"Item2\":{\"Data\":-1000,\"Data2\":1000}}"
            );
        }

        [Fact]
        public void OneField()
        {
            Represents(new Item { Data = 5 }, "{\"Data\":5}");
            Represents(new Item { Data = 1 }, "{\"Data\":1}");
            Represents(new Item { Data = -15 }, "{\"Data\":-15}");
        }

        [Fact]
        public void TwoFields()
        {
            Represents(new Item2 { Data = 5, Data2 = 6 }, "{\"Data\":5,\"Data2\":6}");
            Represents(new Item2 { Data = 1, Data2 = 2 }, "{\"Data\":1,\"Data2\":2}");
            Represents(new Item2 { Data = -15, Data2 = 155 }, "{\"Data\":-15,\"Data2\":155}");
        }
        
        private static void Represents<T>(T item, string representation)
        {
            Assert.Equal(representation, item.AsJson());
        }
        
        public class Item
        {
            public int Data;
        }

        public class Item2
        {
            public int Data;
            public int Data2;
        }

        public class BothItems
        {
            public bool Data;

            public Item Item;
            public Item2 Item2;
        }
    }
}