using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonEnumTest : JsonTest
    {
        public enum ByteEnum : byte
        {
            Value = 100,
        }
        
        public enum IntEnum
        {
            Value = 100,
        }
        
        public enum LongEnum : long
        {
            Value = 100,
        }

        [Fact]
        public void ByteEnum_ShouldSerializeCorrectly() => Appends(ByteEnum.Value).As("100");
        [Fact]
        public void ByteEnum_ShouldDeserializeCorrectly() => Unwraps("100").As(ByteEnum.Value);
        
        [Fact]
        public void IntEnum_ShouldSerializeCorrectly() => Appends(IntEnum.Value).As("100");
        [Fact]
        public void IntEnum_ShouldDeserializeCorrectly() => Unwraps("100").As(IntEnum.Value);
        
        [Fact]
        public void LongEnum_ShouldSerializeCorrectly() => Appends(LongEnum.Value).As("100");
        [Fact]
        public void LongEnum_ShouldDeserializeCorrectly() => Unwraps("100").As(LongEnum.Value);
    }
}