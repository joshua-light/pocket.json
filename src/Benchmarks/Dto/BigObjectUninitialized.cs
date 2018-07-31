namespace Pocket.Json.Benchmarks
{
    public class BigObjectUninitialized
    {
        public class Nested1
        {
            public Nested2 Nested;
        }

        public class Nested2
        {
            public Nested3 Nested01;
            public Nested4 Nested02;
        }

        public class Nested3
        {
            public TenFields.Bool Booleans;
            public TenFields.Byte Bytes;
            public TenFields.Int Ints;
            public TenFields.Long Longs;
            public TenFields.Float Floats;
            public TenFields.Double Doubles;
            public TenFields.String Strings;
            
            public Nested4 Nested01;
            public Nested4 Nested02;
            public Nested4 Nested03;
            public Nested4 Nested04;
        }

        public class Nested4
        {
            public TenFields.Bool Booleans;
            public TenFields.Byte Bytes;
            public TenFields.Int Ints;
            public TenFields.Long Longs;
            public TenFields.Float Floats;
            public TenFields.Double Doubles;
            public TenFields.String Strings;
            
            public Nested5 Nested;
        }
        
        public class Nested5
        {
            public TenFields.Bool Booleans;
            public TenFields.Byte Bytes;
            public TenFields.Int Ints;
            public TenFields.Long Longs;
            public TenFields.Float Floats;
            public TenFields.Double Doubles;
            public TenFields.String Strings;
        }
        
        public Nested1 Nested;
        
        public TenFields.Bool Booleans;
        public TenFields.Byte Bytes;
        public TenFields.Int Ints;
        public TenFields.Long Longs;
        public TenFields.Float Floats;
        public TenFields.Double Doubles;
        public TenFields.String Strings;
    }
}