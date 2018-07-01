using System;
using Xunit;

namespace Pocket.Json.Tests.Unwrap
{
    public class JsonPrimitivesTest
    {
        [Fact]
        public void Float()
        {
            Unwraps("0").To(0f);
            Unwraps("0.0").To(0f);
            Unwraps("0.1").To(0.1f);
            Unwraps("0.01").To(0.01f);
            Unwraps("0.001").To(0.001f);
            Unwraps("0.0001").To(0.0001f);
            Unwraps("0.00001").To(0.00001f);
            Unwraps("0.000001").To(0.000001f);
            Unwraps("0.0000001").To(0.0f);
            Unwraps("0.00000001").To(0.0f);
            Unwraps("0.000000001").To(0.0f);
            Unwraps("0.0000000001").To(0.0f);
            Unwraps("0.00000000001").To(0.0f);

            Unwraps("1.1").To(1.1f);
            Unwraps("1.12").To(1.12f);
            Unwraps("1.123").To(1.123f);
            Unwraps("1.1234").To(1.1234f);
            Unwraps("1.12345").To(1.12345f);
            Unwraps("1.123456").To(1.123456f);
            Unwraps("1.123456").To(1.123456f);

            Unwraps("11.1").To(11.1f);
            Unwraps("11.12").To(11.12f);
            Unwraps("11.123").To(11.123f);
            Unwraps("11.1234").To(11.1234f);
            Unwraps("11.12345").To(11.12345f);
            Unwraps("11.12345").To(11.12345f);
            Unwraps("11.12345").To(11.12345f);

            Unwraps("111.1").To(111.1f);
            Unwraps("111.12").To(111.12f);
            Unwraps("111.123").To(111.123f);
            Unwraps("111.1234").To(111.1234f);
            Unwraps("111.1234").To(111.1234f);
            Unwraps("111.1234").To(111.1234f);

            Unwraps("1111.1").To(1111.1f);
            Unwraps("1111.12").To(1111.12f);
            Unwraps("1111.123").To(1111.123f);
            Unwraps("1111.123").To(1111.123f);
            Unwraps("1111.123").To(1111.123f);

            Unwraps("11111.1").To(11111.1f);
            Unwraps("11111.12").To(11111.12f);
            Unwraps("11111.12").To(11111.12f);
            Unwraps("11111.12").To(11111.12f);

            Unwraps("111111.1").To(111111.1f);
            Unwraps("111111.1").To(111111.1f);
            Unwraps("111111.1").To(111111.1f);
            Unwraps("1111111.1").To(1111111.0f);

            Unwraps("1111111").To(1111111f);
            Unwraps("1111111E1").To(11111110f);
            Unwraps("1111111E2").To(111111100f);
            Unwraps("1111111E3").To(1111111000f);
            Unwraps("1111111E4").To(11111110000f);
            Unwraps("1111111E5").To(111111100000f);
            Unwraps("1111111E6").To(1111111000000f);
            Unwraps("1111111E7").To(11111110000000f);
            Unwraps("1111111E8").To(111111100000000f);
            Unwraps("1111111E9").To(1111111000000000f);
            Unwraps("1111111E10").To(11111110000000000f);
            Unwraps("1111111E11").To(111111100000000000f);
            Unwraps("1111111E12").To(1111111000000000000f);

            Unwraps("0.102").To(0.102f);
            Unwraps("0.1002").To(0.1002f);
            Unwraps("0.10203").To(0.10203f);
            Unwraps("0.102034").To(0.102034f);
            Unwraps("0.1020034").To(0.102003f);
            Unwraps("0.10200034").To(0.102f);
        }

        [Fact]
        public void Integer()
        {
            Unwraps("1").To(1);
            Unwraps("11").To(11);
            Unwraps("111").To(111);
            Unwraps("1111").To(1111);
            Unwraps("11111").To(11111);
            Unwraps("111111").To(111111);
            Unwraps("1111111").To(1111111);
            Unwraps("11111111").To(11111111);
            Unwraps("111111111").To(111111111);
            Unwraps("1111111111").To(1111111111);

            Assert.Equal(int.MaxValue - 1, (int.MaxValue - 1).ToString().AsJson<int>());
            Assert.Equal(int.MinValue + 1, (int.MinValue + 1).ToString().AsJson<int>());

            Assert.Equal(int.MaxValue, int.MaxValue.ToString().AsJson<int>());
            Assert.Equal(int.MinValue, int.MinValue.ToString().AsJson<int>());

            var rand = new Random(1111);
            for (var i = 0; i < 1000; i++)
            {
                var number = rand.Next();
                Assert.Equal(number, number.ToString().AsJson<int>());
            }
        }

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