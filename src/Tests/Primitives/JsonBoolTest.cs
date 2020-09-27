using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonBoolTest : JsonTest
    {
        [Fact]
        public void Write_ShouldConvertTrueTo1() => Writes(true).As("1");
        
        [Fact]
        public void Write_ShouldConvertFalseTo0() => Writes(false).As("0");

        [Fact]
        public void Read_ShouldConvert1ToTrue() => Reads("1").As(true);
        
        [Fact]
        public void Read_ShouldConvert0ToFalse() => Reads("0").As(false);
    }
}