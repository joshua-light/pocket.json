using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pocket.Common;
using Xunit;

namespace Pocket.Json.Tests
{
    public class JsonTest
    {
        public class Empty : IEquatable<Empty>
        {
            public bool Equals(Empty other) => true;
        }

        public class Int : IEquatable<Int>
        {
            [Json] public int Item1;
            
            public bool Equals(Int other) => Item1 == other.Item1;
        }
        
        public class IntAndInt : IEquatable<IntAndInt>
        {
            [Json] public int Item1;
            [Json] public int Item2;

            public bool Equals(IntAndInt other) => Item1 == other.Item1 && Item2 == other.Item2;
        }
        
        public class FloatAndDouble : IEquatable<FloatAndDouble>
        {
            [Json] public float Item1;
            [Json] public double Item2;

            public bool Equals(FloatAndDouble other) => Item1 == other.Item1 && Item2 == other.Item2;
        }

        public class IntAndString : IEquatable<IntAndString>
        {
            [Json] public int Item1;
            [Json] public string Item2;

            public bool Equals(IntAndString other) => Item1 == other.Item1 && Item2 == other.Item2;
        }
        
        public class StringAndInt : IEquatable<StringAndInt>
        {
            [Json] public string Item1;
            [Json] public int Item2;

            public bool Equals(StringAndInt other) => Item1 == other.Item1 && Item2 == other.Item2;
        }

        public class IntAndAnotherIntAndInt : IEquatable<IntAndAnotherIntAndInt>
        {
            [Json] public int Item1;
            [Json] public IntAndInt Item2;

            public bool Equals(IntAndAnotherIntAndInt other) =>
                Item1 == other.Item1 && (Item2?.Equals(other.Item2) ?? false);
        }

        public class IntArray : IEquatable<IntArray>
        {
            [Json] public int[] Items;

            public bool Equals(IntArray other) => Items.SequenceEqual(other.Items);
        }

        public class UnderscoredInt : IEquatable<UnderscoredInt>
        {
            [Json] public int Item_1;

            public bool Equals(UnderscoredInt other) => other != null && Item_1 == other.Item_1;
        }
        
        public class UnderscoredEmptyAndEmpty : IEquatable<UnderscoredEmptyAndEmpty>
        {            
            [Json] public Empty Item_1;
            [Json] public Empty Item_2;

            public bool Equals(UnderscoredEmptyAndEmpty other) =>
                other != null && Item_1.Equals(other.Item_1) && Item_2.Equals(other.Item_2);
        }

        public class UnderscoredNestedEmptyAndEmpty : IEquatable<UnderscoredNestedEmptyAndEmpty>
        {
            public class Nested : IEquatable<Nested>
            {
                [Json] public UnderscoredEmptyAndEmpty Item1;

                public bool Equals(Nested other) => Item1.Equals(other.Item1);
            }

            [Json] public Nested Item1;

            public bool Equals(UnderscoredNestedEmptyAndEmpty other) => Item1.Equals(other.Item1);
        }
        
        public class StrangeNestedWithUnderscore : IEquatable<StrangeNestedWithUnderscore>
        {
            public class Nested1 : IEquatable<Nested1>
            {
                [Json] public Nested2 Item1;

                public bool Equals(Nested1 other) => Item1.Equals(other.Item1);
            }
            
            public class Nested2 : IEquatable<Nested2>
            {
                // These fields have hashcode collision problem.
                [Json] public Nested3 Item_1;
                [Json] public Nested4 Item_2;

                public bool Equals(Nested2 other) =>
                    Item_1.Equals(other.Item_1) && Item_2.Equals(other.Item_2);
            }

            public class Nested3 : IEquatable<Nested3>
            {
                [Json] public Nested4 Item1;

                public bool Equals(Nested3 other) => Item1.Equals(other.Item1);
            }
            
            public class Nested4 : IEquatable<Nested4>
            {
                public bool Equals(Nested4 other) => true;
            }

            [Json] public Nested1 Item1;

            public bool Equals(StrangeNestedWithUnderscore other) => Item1.Equals(other.Item1);
        }

        public class WithObjectField : IEquatable<WithObjectField>
        {
            public class ActualType : IEquatable<ActualType>
            {
                [Json] public int Data;
                
                public bool Equals(ActualType other) => Data == other.Data;
            }
            
            [Json] public object Field;

            public bool Equals(WithObjectField other)
            {
                if (Field == null)
                    return other.Field == null;
                
                return (Field as ActualType).Equals(other.Field as ActualType);
            }  
        }

        public class JsonPacket : IEquatable<JsonPacket>
        {
            [Json] public int Code;
            [Json] public string Body;

            public bool Equals(JsonPacket other) =>
                Code == other.Code && Body == other.Body;
        }
        
        #region Writes
        
        protected static WritesFluent<T> Writes<T>(T instance) => new WritesFluent<T>(instance);

        protected struct WritesFluent<T>
        {
            private readonly T _instance;

            public WritesFluent(T instance)
            {
                _instance = instance;
            }

            public void As(string json)
            {
                Assert.Equal(json, _instance.ToJson());
                Assert.Equal(json, ((object) _instance).ToJson(typeof(T)));
                
                if (_instance != null)
                    Assert.Equal(json, ((object) _instance).ToJson());
            }
        }

        #endregion
        
        #region Reads
        
        protected static ReadsFluent Reads(string json)
        {
            return new ReadsFluent(json);
        }

        protected struct ReadsFluent
        {
            private readonly string _json;

            public ReadsFluent(string json)
            {
                _json = json;
            }
            
            public void As<T>(T value)
            {
                var json = _json;
                InvokeWithTryCatch(() =>
                {
                    var type = typeof(T);
                    if (type.Implements(typeof(IEnumerable<>)))
                    {
                        Assert.Equal((IEnumerable) value, (IEnumerable) json.FromJson<T>());
                        Assert.Equal((IEnumerable) value, (IEnumerable) json.FromJson(type));
                    }
                    else if (type == typeof(double))
                    {
                        Assert.Equal((double) (object) value, json.FromJson<double>(), 15);
                        Assert.Equal((double) (object) value, (double) json.FromJson(typeof(double)), 15);
                    }
                    else if (type == typeof(float))
                    {
                        Assert.Equal((float) (object) value, json.FromJson<float>(), 7);
                        Assert.Equal((float) (object) value, (float) json.FromJson(typeof(float)), 7);
                    }
                    else
                    {
                        Assert.Equal(value, json.FromJson<T>());
                        Assert.Equal(value, json.FromJson(type));
                    }
                });
            }

            public void As(int value)
            {
                var json = _json;
                InvokeWithTryCatch(() =>
                {
                    Assert.Equal(value, json.FromJson<int>());
                    Assert.Equal(value, json.FromJson(typeof(int)));
                    
                    Assert.Equal(-value, ("-" + json).FromJson<int>());
                    Assert.Equal(-value, ("-" + json).FromJson(typeof(int)));
                });
            }
            
            public void As(byte value)
            {
                var json = _json;
                InvokeWithTryCatch(() =>
                {
                    Assert.Equal(value, json.FromJson<byte>());
                    Assert.Equal(value, json.FromJson(typeof(byte)));
                    
                    Assert.Equal(-value, ("-" + json).FromJson<byte>());
                    Assert.Equal(-value, ("-" + json).FromJson(typeof(byte)));
                });
            }

            public void As(long value)
            {
                var json = _json;
                InvokeWithTryCatch(() =>
                {
                    Assert.Equal(value, json.FromJson<long>());
                    Assert.Equal(value, json.FromJson(typeof(long)));
                    
                    Assert.Equal(-value, ("-" + json).FromJson<long>());
                    Assert.Equal(-value, ("-" + json).FromJson(typeof(long)));
                });
            }

            private void InvokeWithTryCatch(Action assertion)
            {
                try
                {
                    assertion();
                }
                catch (Exception e)
                {
                    throw new Exception($"Exception occured while reading \"{_json}\": ", e);
                }
            }
        }
        
        #endregion
    }
}