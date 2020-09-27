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
        public void ByteEnum_ShouldSerializeCorrectly() => Writes(ByteEnum.Value).As("100");
        [Fact]
        public void ByteEnum_ShouldDeserializeCorrectly() => Reads("100").As(ByteEnum.Value);
        
        [Fact]
        public void IntEnum_ShouldSerializeCorrectly() => Writes(IntEnum.Value).As("100");
        [Fact]
        public void IntEnum_ShouldDeserializeCorrectly() => Reads("100").As(IntEnum.Value);
        
        [Fact]
        public void LongEnum_ShouldSerializeCorrectly() => Writes(LongEnum.Value).As("100");
        [Fact]
        public void LongEnum_ShouldDeserializeCorrectly() => Reads("100").As(LongEnum.Value);
    }
}