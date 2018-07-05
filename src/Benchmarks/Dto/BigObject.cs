namespace Pocket.Json.Benchmarks
{
    public class BigObject
    {
        public class Nested1
        {
            public Nested2 Nested = new Nested2();
        }

        public class Nested2
        {
            public Nested3 Nested01 = new Nested3();
            public Nested4 Nested02 = new Nested4();
        }

        public class Nested3
        {
            public TenFields.Bool Booleans = new TenFields.Bool();
            public TenFields.Byte Bytes = new TenFields.Byte();
            public TenFields.Int Ints = new TenFields.Int();
            public TenFields.Long Longs = new TenFields.Long();
            public TenFields.Float Floats = new TenFields.Float();
            public TenFields.Double Doubles = new TenFields.Double();
            public TenFields.String Strings = new TenFields.String();
            
            public Nested4 Nested01 = new Nested4();
            public Nested4 Nested02 = new Nested4();
            public Nested4 Nested03 = new Nested4();
            public Nested4 Nested04 = new Nested4();
        }

        public class Nested4
        {
            public TenFields.Bool Booleans = new TenFields.Bool();
            public TenFields.Byte Bytes = new TenFields.Byte();
            public TenFields.Int Ints = new TenFields.Int();
            public TenFields.Long Longs = new TenFields.Long();
            public TenFields.Float Floats = new TenFields.Float();
            public TenFields.Double Doubles = new TenFields.Double();
            public TenFields.String Strings = new TenFields.String();
            
            public Nested5 Nested = new Nested5();
        }
        
        public class Nested5
        {
            public TenFields.Bool Booleans = new TenFields.Bool();
            public TenFields.Byte Bytes = new TenFields.Byte();
            public TenFields.Int Ints = new TenFields.Int();
            public TenFields.Long Longs = new TenFields.Long();
            public TenFields.Float Floats = new TenFields.Float();
            public TenFields.Double Doubles = new TenFields.Double();
            public TenFields.String Strings = new TenFields.String();
        }
        
        public Nested1 Nested = new Nested1();
        
        public TenFields.Bool Booleans = new TenFields.Bool();
        public TenFields.Byte Bytes = new TenFields.Byte();
        public TenFields.Int Ints = new TenFields.Int();
        public TenFields.Long Longs = new TenFields.Long();
        public TenFields.Float Floats = new TenFields.Float();
        public TenFields.Double Doubles = new TenFields.Double();
        public TenFields.String Strings = new TenFields.String();
    }
}