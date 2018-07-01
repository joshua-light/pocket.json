using System;
using Xunit;

namespace Pocket.Json.Tests.Unwrap
{
    public class JsonPrimitivesTest
    {
        [Fact]
        public void Long()
        {
            Unwraps("1").To(1L);
            Unwraps("11").To(11L);
            Unwraps("111").To(111L);
            Unwraps("1111").To(1111L);
            Unwraps("11111").To(11111L);
            Unwraps("111111").To(111111L);
            Unwraps("1111111").To(1111111L);
            Unwraps("11111111").To(11111111L);
            Unwraps("111111111").To(111111111L);
            Unwraps("1111111111").To(1111111111L);
            Unwraps("11111111111").To(11111111111L);
            Unwraps("111111111111").To(111111111111L);
            Unwraps("1111111111111").To(1111111111111L);
            Unwraps("11111111111111").To(11111111111111L);
            Unwraps("111111111111111").To(111111111111111L);
            Unwraps("1111111111111111").To(1111111111111111L);
            Unwraps("11111111111111111").To(11111111111111111L);
            Unwraps("111111111111111111").To(111111111111111111L);
            Unwraps("1111111111111111111").To(1111111111111111111L);

            Assert.Equal(long.MaxValue - 1, (long.MaxValue - 1).ToString().AsJson<long>());
            Assert.Equal(long.MinValue + 1, (long.MinValue + 1).ToString().AsJson<long>());

            Assert.Equal(long.MaxValue, long.MaxValue.ToString().AsJson<long>());
            Assert.Equal(long.MinValue, long.MinValue.ToString().AsJson<long>());
        }

        [Fact]
        public void String()
        {
            Unwraps("\"0\"").To("0");
            Unwraps("\"Hello\"").To("Hello");
            Unwraps("\"Hello, guys!\"").To("Hello, guys!");
        }
        
        private static UnwrapsFluent Unwraps(string json)
        {
            return new UnwrapsFluent(json);
        }

        private struct UnwrapsFluent
        {
            private readonly string _json;

            public UnwrapsFluent(string json)
            {
                _json = json;
            }

            public void To<T>(T value)
            {
                Assert.Equal(value, _json.AsJson<T>());
            }

            public void To(int value)
            {
                Assert.Equal(value, _json.AsJson<int>());
                Assert.Equal(-value, ("-" + _json).AsJson<int>());
            }

            public void To(long value)
            {
                Assert.Equal(value, _json.AsJson<long>());
                Assert.Equal(-value, ("-" + _json).AsJson<long>());
            }
        }
    }
}