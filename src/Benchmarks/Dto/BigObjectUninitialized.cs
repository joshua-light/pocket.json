namespace Pocket.Json.Benchmarks
{
    public class BigObjectUninitialized
    {
        public class Nested1
        {
            [Json] public Nested2 Nested;
        }

        public class Nested2
        {
            [Json] public Nested3 Nested01;
            [Json] public Nested4 Nested02;
        }

        public class Nested3
        {
            [Json] public TenFields.Bool Booleans;
            [Json] public TenFields.Byte Bytes;
            [Json] public TenFields.Int Ints;
            [Json] public TenFields.Long Longs;
            [Json] public TenFields.Float Floats;
            [Json] public TenFields.Double Doubles;
            [Json] public TenFields.String Strings;
            
            [Json] public Nested4 Nested01;
            [Json] public Nested4 Nested02;
            [Json] public Nested4 Nested03;
            [Json] public Nested4 Nested04;
        }

        public class Nested4
        {
            [Json] public TenFields.Bool Booleans;
            [Json] public TenFields.Byte Bytes;
            [Json] public TenFields.Int Ints;
            [Json] public TenFields.Long Longs;
            [Json] public TenFields.Float Floats;
            [Json] public TenFields.Double Doubles;
            [Json] public TenFields.String Strings;
            
            [Json] public Nested5 Nested;
        }
        
        public class Nested5
        {
            [Json] public TenFields.Bool Booleans;
            [Json] public TenFields.Byte Bytes;
            [Json] public TenFields.Int Ints;
            [Json] public TenFields.Long Longs;
            [Json] public TenFields.Float Floats;
            [Json] public TenFields.Double Doubles;
            [Json] public TenFields.String Strings;
        }
        
        [Json] public Nested1 Nested;
        
        [Json] public TenFields.Bool Booleans;
        [Json] public TenFields.Byte Bytes;
        [Json] public TenFields.Int Ints;
        [Json] public TenFields.Long Longs;
        [Json] public TenFields.Float Floats;
        [Json] public TenFields.Double Doubles;
        [Json] public TenFields.String Strings;
    }
}