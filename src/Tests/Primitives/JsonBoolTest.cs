using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonBoolTest : JsonTest
    {
        [Fact]
        public void Append_ShouldConvertTrueTo1() => Appends(true).As("1");
        
        [Fact]
        public void Append_ShouldConvertFalseTo0() => Appends(false).As("0");

        [Fact]
        public void Unwrap_ShouldConvert1ToTrue() => Unwraps("1").As(true);
        
        [Fact]
        public void Unwrap_ShouldConvert0ToFalse() => Unwraps("0").As(false);
    }
}