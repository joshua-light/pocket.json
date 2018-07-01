using Xunit;

namespace Pocket.Json.Tests.Primitives
{
    public class JsonFloatTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(0f).As("0");
            Appends(0.0f).As("0");
            Appends(0.1f).As("0.1");
            Appends(0.01f).As("0.01");
            Appends(0.001f).As("0.001");
            Appends(0.0001f).As("0.0001");
            Appends(0.00001f).As("0.00001");
            Appends(0.000001f).As("0.000001");
            Appends(0.0000001f).As("0.0000001");
            Appends(0.00000001f).As("0");

            Appends(1.1f).As("1.1");
            Appends(1.12f).As("1.12");
            Appends(1.123f).As("1.123");
            Appends(1.1234f).As("1.1234");
            Appends(1.12345f).As("1.12345");
            Appends(1.123456f).As("1.123456");
            Appends(1.1234567f).As("1.123457");
            Appends(1.12345678f).As("1.123457");

            Appends(11.1f).As("11.1");
            Appends(11.12f).As("11.12");
            Appends(11.123f).As("11.123");
            Appends(11.1234f).As("11.1234");
            Appends(11.12345f).As("11.12345");
            Appends(11.123456f).As("11.12346");
            Appends(11.1234567f).As("11.12346");
            Appends(11.12345678f).As("11.12346");

            Appends(111.1f).As("111.1");
            Appends(111.12f).As("111.12");
            Appends(111.123f).As("111.123");
            Appends(111.1234f).As("111.1234");
            Appends(111.12345f).As("111.1235");
            Appends(111.123456f).As("111.1235");
            Appends(111.1234567f).As("111.1235");

            Appends(1111.1f).As("1111.1");
            Appends(1111.12f).As("1111.12");
            Appends(1111.123f).As("1111.123");
            Appends(1111.1234f).As("1111.123");
            Appends(1111.12345f).As("1111.123");
            Appends(1111.123456f).As("1111.123");

            Appends(11111.1f).As("11111.1");
            Appends(11111.12f).As("11111.12");
            Appends(11111.123f).As("11111.12");
            Appends(11111.1234f).As("11111.12");
            Appends(11111.12345f).As("11111.12");

            Appends(111111.1f).As("111111.1");
            Appends(111111.12f).As("111111.1");
            Appends(111111.123f).As("111111.1");
            Appends(111111.1234f).As("111111.1");

            Appends(1111111f).As("1111111");
            Appends(1111111.1f).As("1111111");
            Appends(1111111.12f).As("1111111");
            Appends(1111111.123f).As("1111111");

            Appends(11111111f).As("1111111E1");
            Appends(111111111f).As("1111111E2");
            Appends(1111111111f).As("1111111E3");
            Appends(11111111111f).As("1111111E4");
            Appends(111111111111f).As("1111111E5");
            Appends(1111111111111f).As("1111111E6");
            Appends(11111111111111f).As("1111111E7");
            Appends(111111111111111f).As("1111111E8");
            Appends(1111111111111111f).As("1111111E9");
            Appends(11111111111111111f).As("1111111E10");
            Appends(111111111111111111f).As("1111111E11");
            Appends(1111111111111111111f).As("1111111E12");
            Appends(11111111111111111111f).As("1111111E13");
            Appends(111111111111111111111f).As("1111111E14");
            Appends(1111111111111111111111f).As("1111111E15");
            Appends(11111111111111111111111f).As("1111111E16");
            Appends(111111111111111111111111f).As("1111111E17");
            Appends(1111111111111111111111111f).As("1111111E18");

            Appends(0f).As("0");
            Appends(0.1f).As("0.1");
            Appends(0.02f).As("0.02");
            Appends(0.102f).As("0.102");
            Appends(0.1002f).As("0.1002");
            Appends(0.10203f).As("0.10203");
            Appends(0.102034f).As("0.102034");
        }

        [Fact]
        public void Unwrap_ShouldWorkCorrectly()
        {
            Unwraps("0").As(0f);
            Unwraps("0.0").As(0f);
            Unwraps("0.1").As(0.1f);
            Unwraps("0.01").As(0.01f);
            Unwraps("0.001").As(0.001f);
            Unwraps("0.0001").As(0.0001f);
            Unwraps("0.00001").As(0.00001f);
            Unwraps("0.000001").As(0.000001f);
            Unwraps("0.0000001").As(0.0f);
            Unwraps("0.00000001").As(0.0f);
            Unwraps("0.000000001").As(0.0f);
            Unwraps("0.0000000001").As(0.0f);
            Unwraps("0.00000000001").As(0.0f);

            Unwraps("1.1").As(1.1f);
            Unwraps("1.12").As(1.12f);
            Unwraps("1.123").As(1.123f);
            Unwraps("1.1234").As(1.1234f);
            Unwraps("1.12345").As(1.12345f);
            Unwraps("1.123456").As(1.123456f);
            Unwraps("1.123456").As(1.123456f);

            Unwraps("11.1").As(11.1f);
            Unwraps("11.12").As(11.12f);
            Unwraps("11.123").As(11.123f);
            Unwraps("11.1234").As(11.1234f);
            Unwraps("11.12345").As(11.12345f);
            Unwraps("11.12345").As(11.12345f);
            Unwraps("11.12345").As(11.12345f);

            Unwraps("111.1").As(111.1f);
            Unwraps("111.12").As(111.12f);
            Unwraps("111.123").As(111.123f);
            Unwraps("111.1234").As(111.1234f);
            Unwraps("111.1234").As(111.1234f);
            Unwraps("111.1234").As(111.1234f);

            Unwraps("1111.1").As(1111.1f);
            Unwraps("1111.12").As(1111.12f);
            Unwraps("1111.123").As(1111.123f);
            Unwraps("1111.123").As(1111.123f);
            Unwraps("1111.123").As(1111.123f);

            Unwraps("11111.1").As(11111.1f);
            Unwraps("11111.12").As(11111.12f);
            Unwraps("11111.12").As(11111.12f);
            Unwraps("11111.12").As(11111.12f);

            Unwraps("111111.1").As(111111.1f);
            Unwraps("111111.1").As(111111.1f);
            Unwraps("111111.1").As(111111.1f);
            Unwraps("1111111.1").As(1111111.0f);

            Unwraps("1111111").As(1111111f);
            Unwraps("1111111E1").As(11111110f);
            Unwraps("1111111E2").As(111111100f);
            Unwraps("1111111E3").As(1111111000f);
            Unwraps("1111111E4").As(11111110000f);
            Unwraps("1111111E5").As(111111100000f);
            Unwraps("1111111E6").As(1111111000000f);
            Unwraps("1111111E7").As(11111110000000f);
            Unwraps("1111111E8").As(111111100000000f);
            Unwraps("1111111E9").As(1111111000000000f);
            Unwraps("1111111E10").As(11111110000000000f);
            Unwraps("1111111E11").As(111111100000000000f);
            Unwraps("1111111E12").As(1111111000000000000f);

            Unwraps("0.102").As(0.102f);
            Unwraps("0.1002").As(0.1002f);
            Unwraps("0.10203").As(0.10203f);
            Unwraps("0.102034").As(0.102034f);
            Unwraps("0.1020034").As(0.102003f);
            Unwraps("0.10200034").As(0.102f);
        }
    }
}