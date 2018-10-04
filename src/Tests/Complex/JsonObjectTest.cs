using System;
using Xunit;

namespace Pocket.Json.Tests.Complex
{
    public class JsonObjectTest : JsonTest
    {
        [Fact]
        public void Append_ShouldWorkCorrectly()
        {
            Appends(new IntAndInt())
                .As("{\"Item1\":0,\"Item2\":0}");
            
            Appends(new IntAndInt{ Item1 = 1 })
                .As("{\"Item1\":1,\"Item2\":0}");
            
            Appends(new IntAndInt{ Item1 = 1, Item2 = 2 })
                .As("{\"Item1\":1,\"Item2\":2}");
            
            Appends(new IntArray{ Items = new [] { 1, 2, 3, 4, 5 } })
                .As("{\"Items\":[1,2,3,4,5]}");
            
            Appends(new UnderscoredInt{ Item_1 = 1 })
                .As("{\"Item_1\":1}");
            
            Appends(new UnderscoredEmptyAndEmpty
            {
                Item_1 = new Empty(),
                Item_2 = new Empty()
            })
                .As("{\"Item_1\":{},\"Item_2\":{}}");
            
            Appends(new WithObjectField())
                .As("{}");
            
            Appends(new WithObjectField{ Field = new WithObjectField.ActualType{ Data = 10 } })
                .As("{\"Field\":{\"Data\":10}}");

            Appends(new B { Field1 = 1, Field2 = 2 })
                .As("{\"Field2\":2,\"Field1\":1}");
        }

        [Fact]
        public void Unwrap_ShouldWorkCorrectly()
        {
            Unwraps("{\"Item1\":0,\"Item2\":0}")
                .As(new IntAndInt());
            
            Unwraps("{\"Item1\":1,\"Item2\":0}")
                .As(new IntAndInt{ Item1 = 1 });
            
            Unwraps("{\"Item1\":1,\"Item2\":2}")
                .As(new IntAndInt{ Item1 = 1, Item2 = 2 });
            
            Unwraps("{\"Item1\":1.1234,\"Item2\":2.12345678}")
                .As(new FloatAndDouble{ Item1 = 1.1234f, Item2 = 2.12345678 });
            
            Unwraps("{\"Items\":[1,2,3,4,5]}")
                .As(new IntArray{ Items = new [] { 1, 2, 3, 4, 5 } });
            
            Unwraps("{\"Items\":[]}")
                .As(new IntArray{ Items = new int[0] });
            
            Unwraps("{\"Item_1\":1}")
                .As(new UnderscoredInt{ Item_1 = 1 });
            
            Unwraps("{\"Item_1\":{},\"Item_2\":{}}")
                .As(new UnderscoredEmptyAndEmpty
                {
                    Item_1 = new Empty(),
                    Item_2 = new Empty()
                });
            
            Unwraps("{\"Item1\":{\"Item1\":{\"Item_1\":{},\"Item_2\":{}}}}")
                .As(new UnderscoredNestedEmptyAndEmpty{ 
                    Item1 = new UnderscoredNestedEmptyAndEmpty.Nested
                    {
                        Item1 = new UnderscoredEmptyAndEmpty
                        {
                            Item_1 = new Empty(),
                            Item_2 = new Empty()
                        }
                    } });
            Unwraps("{\"Item1\":{\"Item1\":{\"Item_1\":{\"Item1\":{}},\"Item_2\":{}}}}")
                .As(new StrangeNestedWithUnderscore { 
                    Item1 = new StrangeNestedWithUnderscore.Nested1
                    {
                        Item1 = new StrangeNestedWithUnderscore.Nested2
                        {
                            Item_1 = new StrangeNestedWithUnderscore.Nested3{ Item1 = new StrangeNestedWithUnderscore.Nested4() },
                            Item_2 = new StrangeNestedWithUnderscore.Nested4()
                        }
                    } });
            
            Unwraps("{}")
                .As(new WithObjectField());

            Unwraps("{\"Field1\":1,\"Field2\":2}")
                .As(new B { Field1 = 1, Field2 = 2 });
        }

        #region Inner Classes

        public class A
        {
            public int Field1;
        }

        public class B : A, IEquatable<B>
        {
            public int Field2;

            public bool Equals(B other) =>
                Field1 == other.Field1 && Field2 == other.Field2;
        }

        #endregion
    }
}