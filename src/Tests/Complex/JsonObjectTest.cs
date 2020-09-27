using System;
using Xunit;

namespace Pocket.Json.Tests.Complex
{
    public class JsonObjectTest : JsonTest
    {
        [Fact]
        public void Write_ShouldWorkCorrectly()
        {
            Writes(new IntAndInt())
                .As("{\"Item1\":0,\"Item2\":0}");
            
            Writes(new IntAndInt{ Item1 = 1 })
                .As("{\"Item1\":1,\"Item2\":0}");
            
            Writes(new IntAndInt{ Item1 = 1, Item2 = 2 })
                .As("{\"Item1\":1,\"Item2\":2}");
            
            Writes(new IntArray{ Items = new [] { 1, 2, 3, 4, 5 } })
                .As("{\"Items\":[1,2,3,4,5]}");
            
            Writes(new UnderscoredInt{ Item_1 = 1 })
                .As("{\"Item_1\":1}");
            
            Writes(new StringAndInt
                {
                    Item1 = null,
                    Item2 = 1,
                })
                .As("{\"Item2\":1}");
            
            Writes(new UnderscoredEmptyAndEmpty
            {
                Item_1 = new Empty(),
                Item_2 = new Empty()
            })
                .As("{\"Item_1\":{},\"Item_2\":{}}");
            
            Writes(new UnderscoredEmptyAndEmpty
                {
                    Item_1 = new Empty(),
                    Item_2 = null
                })
                .As("{\"Item_1\":{}}");
            
            Writes(new UnderscoredEmptyAndEmpty
                {
                    Item_1 = null,
                    Item_2 = new Empty()
                })
                .As("{\"Item_2\":{}}");
            
            Writes(new WithObjectField())
                .As("{}");
            
            Writes(new WithObjectField{ Field = new WithObjectField.ActualType{ Data = 10 } })
                .As("{\"Field\":{\"Data\":10}}");

            Writes(new B { Field1 = 1, Field2 = 2 })
                .As("{\"Field2\":2,\"Field1\":1}");
            
            Writes(new WithAttribute{ Field1 = 1 })
                .As("{\"field_1\":1}");
        }

        [Fact]
        public void Read_ShouldWorkCorrectly()
        {
            Reads("{\"Item1\":0,\"Item2\":0}")
                .As(new Int());
            
            Reads("{\"Item1\":0,\"Item2\":0}")
                .As(new IntAndInt());
            
            Reads("{\"Item1\":1,\"Item2\":0}")
                .As(new IntAndInt{ Item1 = 1 });
            
            Reads("{\"Item1\":1,\"Item2\":2}")
                .As(new IntAndInt{ Item1 = 1, Item2 = 2 });
            
            Reads("{\"Item1\":1.1234,\"Item2\":2.12345678}")
                .As(new FloatAndDouble{ Item1 = 1.1234f, Item2 = 2.12345678 });
            
            Reads("{\"Items\":[1,2,3,4,5]}")
                .As(new IntArray{ Items = new [] { 1, 2, 3, 4, 5 } });
            
            Reads("{\"Items\":[]}")
                .As(new IntArray{ Items = new int[0] });
            
            Reads("{\"Item_1\":1}")
                .As(new UnderscoredInt{ Item_1 = 1 });
            
            Reads("{\"Item_1\":{},\"Item_2\":{}}")
                .As(new UnderscoredEmptyAndEmpty
                {
                    Item_1 = new Empty(),
                    Item_2 = new Empty()
                });
            
            Reads("{\"Item1\":{\"Item1\":{\"Item_1\":{},\"Item_2\":{}}}}")
                .As(new UnderscoredNestedEmptyAndEmpty{ 
                    Item1 = new UnderscoredNestedEmptyAndEmpty.Nested
                    {
                        Item1 = new UnderscoredEmptyAndEmpty
                        {
                            Item_1 = new Empty(),
                            Item_2 = new Empty()
                        }
                    } });
            
            Reads("{\"Item1\":{\"Item1\":{\"Item_1\":{\"Item1\":{}},\"Item_2\":{}}}}")
                .As(new StrangeNestedWithUnderscore { 
                    Item1 = new StrangeNestedWithUnderscore.Nested1
                    {
                        Item1 = new StrangeNestedWithUnderscore.Nested2
                        {
                            Item_1 = new StrangeNestedWithUnderscore.Nested3{ Item1 = new StrangeNestedWithUnderscore.Nested4() },
                            Item_2 = new StrangeNestedWithUnderscore.Nested4()
                        }
                    } });
            
            Reads("{}")
                .As(new WithObjectField());

            Reads("{\"Field1\":1,\"Field2\":2}")
                .As(new B { Field1 = 1, Field2 = 2 });
            
            Reads("{\"field_1\":1}")
                .As(new WithAttribute{ Field1 = 1 });
            
            Reads("{\"Item3\":{\"Hello\":{12}},\"Item2\":{},\"Item1\":1}")
                .As(new Int{ Item1 = 1 });
        }

        #region Inner Classes

        public class A
        {
            [Json] public int Field1;
        }

        public class B : A, IEquatable<B>
        {
            [Json] public int Field2;

            public bool Equals(B other) =>
                Field1 == other.Field1 && Field2 == other.Field2;
        }

        public class WithAttribute : IEquatable<WithAttribute>
        {
            [Json("field_1")] public int Field1;

            public bool Equals(WithAttribute other) =>
                Field1 == other.Field1;
        }

        #endregion
    }
}