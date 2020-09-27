namespace Pocket.Json.Benchmarks
{
    public class BigObject
    {
        public class Nested1
        {
            [Json] public Nested2 Nested = new Nested2();
        }

        public class Nested2
        {
            [Json] public Nested3 Nested01 = new Nested3();
            [Json] public Nested4 Nested02 = new Nested4();
        }

        public class Nested3
        {
            [Json] public TenFields.Bool Booleans = new TenFields.Bool();
            [Json] public TenFields.Byte Bytes = new TenFields.Byte();
            [Json] public TenFields.Int Ints = new TenFields.Int();
            [Json] public TenFields.Long Longs = new TenFields.Long();
            [Json] public TenFields.Float Floats = new TenFields.Float();
            [Json] public TenFields.Double Doubles = new TenFields.Double();
            [Json] public TenFields.String Strings = new TenFields.String();
            
            [Json] public Nested4 Nested01 = new Nested4();
            [Json] public Nested4 Nested02 = new Nested4();
            [Json] public Nested4 Nested03 = new Nested4();
            [Json] public Nested4 Nested04 = new Nested4();
        }

        public class Nested4
        {
            [Json] public TenFields.Bool Booleans = new TenFields.Bool();
            [Json] public TenFields.Byte Bytes = new TenFields.Byte();
            [Json] public TenFields.Int Ints = new TenFields.Int();
            [Json] public TenFields.Long Longs = new TenFields.Long();
            [Json] public TenFields.Float Floats = new TenFields.Float();
            [Json] public TenFields.Double Doubles = new TenFields.Double();
            [Json] public TenFields.String Strings = new TenFields.String();
            
            [Json] public Nested5 Nested = new Nested5();
        }
        
        public class Nested5
        {
            [Json] public TenFields.Bool Booleans = new TenFields.Bool();
            [Json] public TenFields.Byte Bytes = new TenFields.Byte();
            [Json] public TenFields.Int Ints = new TenFields.Int();
            [Json] public TenFields.Long Longs = new TenFields.Long();
            [Json] public TenFields.Float Floats = new TenFields.Float();
            [Json] public TenFields.Double Doubles = new TenFields.Double();
            [Json] public TenFields.String Strings = new TenFields.String();
        }
        
        [Json] public Nested1 Nested = new Nested1();
        
        [Json] public TenFields.Bool Booleans = new TenFields.Bool();
        [Json] public TenFields.Byte Bytes = new TenFields.Byte();
        [Json] public TenFields.Int Ints = new TenFields.Int();
        [Json] public TenFields.Long Longs = new TenFields.Long();
        [Json] public TenFields.Float Floats = new TenFields.Float();
        [Json] public TenFields.Double Doubles = new TenFields.Double();
        [Json] public TenFields.String Strings = new TenFields.String();
    }
}