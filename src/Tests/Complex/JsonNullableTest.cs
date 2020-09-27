using Xunit;

namespace Pocket.Json.Tests.Complex
{
    public class JsonNullableTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertNullToEmptyString() => Writes((int?) null).As("");

        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes((int?) 1).As("1");
            Writes((int?) 2).As("2");
            
            Writes((bool?) true).As("1");
            Writes((bool?) false).As("0");
        }
        
        [Fact]
        public void Read_ShouldConvertEmptyStringToNull() => Reads("").As((int?) null);

        [Fact]
        public void Read_ShouldWorkCorrectly()
        {
            Reads("1").As((int?) 1);
            Reads("2").As((int?) 2);
            
            Reads("1").As((bool?) true);
            Reads("0").As((bool?) false);
        }
    }
}