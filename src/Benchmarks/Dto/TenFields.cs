using System.Linq;

namespace Pocket.Json.Benchmarks
{
    public class TenFields
    {
        public class Bool
        {
            [Json] public bool Data = true;
            [Json] public bool Data10 = false;
            [Json] public bool Data2 = false;
            [Json] public bool Data3 = true;
            [Json] public bool Data4 = false;
            [Json] public bool Data5 = true;
            [Json] public bool Data6 = false;
            [Json] public bool Data7 = false;
            [Json] public bool Data8 = false;
            [Json] public bool Data9 = true;
        }

        public class Byte
        {
            [Json] public byte Data = 0;
            [Json] public byte Data10 = 1;
            [Json] public byte Data2 = 12;
            [Json] public byte Data3 = 99;
            [Json] public byte Data4 = 99;
            [Json] public byte Data5 = 233;
            [Json] public byte Data6 = 231;
            [Json] public byte Data7 = 222;
            [Json] public byte Data8 = 222;
            [Json] public byte Data9 = 111;
        }

        public class Char
        {
            [Json] public char Data = '0';
            [Json] public char Data10 = '9';
            [Json] public char Data2 = '1';
            [Json] public char Data3 = '2';
            [Json] public char Data4 = '3';
            [Json] public char Data5 = '4';
            [Json] public char Data6 = '5';
            [Json] public char Data7 = '6';
            [Json] public char Data8 = '7';
            [Json] public char Data9 = '8';
        }

        public class Int
        {
            [Json] public int Data = 1;
            [Json] public int Data10 = 1234567891;
            [Json] public int Data2 = 12;
            [Json] public int Data3 = 123;
            [Json] public int Data4 = 1234;
            [Json] public int Data5 = 12345;
            [Json] public int Data6 = 1234567;
            [Json] public int Data7 = 12345678;
            [Json] public int Data8 = 123456789;
            [Json] public int Data9 = 1234567891;
        }

        public class Long
        {
            [Json] public long Data = 100000000000000000;
            [Json] public long Data10 = 1000000000000000;
            [Json] public long Data2 = 10000000000000000;
            [Json] public long Data3 = 10000000000000000;
            [Json] public long Data4 = 10000000000000000;
            [Json] public long Data5 = 10000000000000000;
            [Json] public long Data6 = 10000000000000000;
            [Json] public long Data7 = 10000000000000000;
            [Json] public long Data8 = 10000000000000000;
            [Json] public long Data9 = 10000000000000000;
        }

        public class Float
        {
            [Json] public float Data = 1.1f;
            [Json] public float Data10 = 1234567891.12345678912f;
            [Json] public float Data2 = 12.12f;
            [Json] public float Data3 = 123.123f;
            [Json] public float Data4 = 1234.1234f;
            [Json] public float Data5 = 12345.12345f;
            [Json] public float Data6 = 1234567.123456f;
            [Json] public float Data7 = 12345678.12345678f;
            [Json] public float Data8 = 123456789.123456789f;
            [Json] public float Data9 = 1234567891.1234567891f;
        }

        public class Double
        {
            [Json] public double Data = 1141512312412423.1231241241231;
            [Json] public double Data10 = 1234123567891.12341235678912;
            [Json] public double Data2 = 11123123123123232.11231233312;
            [Json] public double Data3 = 1211231231231231231232233.123;
            [Json] public double Data4 = 12123312312414.11231231234234;
            [Json] public double Data5 = 1231234114123445.123412312315;
            [Json] public double Data6 = 1231123133234567.121234123456;
            [Json] public double Data7 = 123412356123478.1212341235678;
            [Json] public double Data8 = 123412356789.1234123123456789;
            [Json] public double Data9 = 123423567891.1234123123567891;
        }

        public class String
        {
            [Json] public string Data = "123456789123456789123456789123456789123456789";
            [Json] public string Data2 = "123456789123456789123456789123456789123456789";
            [Json] public string Data3 = "123456789123456789123456789123456789123456789";
            [Json] public string Data4 = "123456789123456789123456789123456789123456789";
            [Json] public string Data5 = "123456789123456789123456789123456789123456789";
            [Json] public string Data6 = "123456789123456789123456789123456789123456789";
            [Json] public string Data7 = "123456789123456789123456789123456789123456789";
            [Json] public string Data8 = "123456789123456789123456789123456789123456789";
            [Json] public string Data9 = "123456789123456789123456789123456789123456789";
            [Json] public string Data10 = "123456789123456789123456789123456789123456789";
        }
    }
}