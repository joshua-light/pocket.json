using System;
using Xunit;

namespace Pocket.Json.Tests
{
    public class JsonTest
    {
        protected class IntAndInt : IEquatable<IntAndInt>
        {
            public int Item1;
            public int Item2;

            public bool Equals(IntAndInt other) => Item1 == other.Item1 && Item2 == other.Item2;
        }

        protected class IntAndString
        {
            public int Item1;
            public string Item2;
        }

        protected class IntAndAnotherIntAndInt
        {
            public int Item1;
            public IntAndInt Item2;
        }
        
        #region Appends
        
        protected static AppendsFluent<T> Appends<T>(T instance) => new AppendsFluent<T>(instance);

        protected struct AppendsFluent<T>
        {
            private readonly T _instance;

            public AppendsFluent(T instance)
            {
                _instance = instance;
            }

            public void As(string json) => Assert.Equal(json, _instance.AsJson());
        }

        #endregion
        
        #region Unwraps
        
        protected static UnwrapsFluent Unwraps(string json)
        {
            return new UnwrapsFluent(json);
        }

        protected struct UnwrapsFluent
        {
            private readonly string _json;

            public UnwrapsFluent(string json)
            {
                _json = json;
            }

            public UnwrapsFluentWhere<T> As<T>()
            {
                return new UnwrapsFluentWhere<T>(_json.AsJson<T>());
            }
            
            public void As<T>(T value)
            {
                Assert.Equal(value, _json.AsJson<T>());
            }

            public void As(int value)
            {
                Assert.Equal(value, _json.AsJson<int>());
                Assert.Equal(-value, ("-" + _json).AsJson<int>());
            }

            public void As(long value)
            {
                Assert.Equal(value, _json.AsJson<long>());
                Assert.Equal(-value, ("-" + _json).AsJson<long>());
            }
        }

        protected struct UnwrapsFluentWhere<T>
        {
            private readonly T _item;

            public UnwrapsFluentWhere(T item)
            {
                _item = item;
            }

            public UnwrapsFluentIs<T, TData> Where<TData>(Func<T, TData> getData)
            {
                return new UnwrapsFluentIs<T, TData>(this, getData(_item));
            }
        }

        protected struct UnwrapsFluentIs<T, TData>
        {
            private readonly UnwrapsFluentWhere<T> _where;
            private readonly TData _actual;

            public UnwrapsFluentIs(UnwrapsFluentWhere<T> where, TData actual)
            {
                _where = where;
                _actual = actual;
            }

            public UnwrapsFluentWhere<T> Is(TData expected)
            {
                Assert.Equal(expected, _actual);
                return _where;
            }
        }
        
        #endregion
    }
}