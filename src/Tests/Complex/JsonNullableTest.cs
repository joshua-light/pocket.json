using Xunit;

namespace Pocket.Json.Tests.Complex
{
    public class JsonNullableTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertNullToEmptyString() => Appends((int?) null).As("");

        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends((int?) 1).As("1");
            Appends((int?) 2).As("2");
            
            Appends((bool?) true).As("1");
            Appends((bool?) false).As("0");
        }
        
        [Fact]
        public void Unwrap_ShouldConvertEmptyStringToNull() => Unwraps("").As((int?) null);

        [Fact]
        public void Unwrap_ShouldWorkCorrectly()
        {
            Unwraps("1").As((int?) 1);
            Unwraps("2").As((int?) 2);
            
            Unwraps("1").As((bool?) true);
            Unwraps("0").As((bool?) false);
        }
    }
}