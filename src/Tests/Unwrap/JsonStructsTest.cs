using System;
using Xunit;

namespace Pocket.Json.Tests.Unwrap
{
    public class JsonStructsTest
    {
        [Fact]
        public void OneField()
        {
            Unwraps("{\"Data\":5}")
                .As<WithOneField<int>>()
                .Where(x => x.Data).Is(5);

            Unwraps("{\"Data\":1}")
                .As<WithOneField<bool>>()
                .Where(x => x.Data).Is(true);

            Unwraps("{\"Data\":\"Hello\"}")
                .As<WithOneField<string>>()
                .Where(x => x.Data).Is("Hello");
        }

        [Fact]
        public void OneFieldNested()
        {
            Unwraps("{\"Data\":{\"Data\":1}}")
                .As<WithOneField<WithOneField<int>>>()
                .Where(x => x.Data.Data).Is(1);

            Unwraps("{\"Data\":{\"Data\":{\"Data\":\"1\"}}}")
                .As<WithOneField<WithOneField<WithOneField<string>>>>()
                .Where(x => x.Data.Data.Data).Is("1");
        }

        [Fact]
        public void ThreeFields()
        {
            Unwraps("{\"Data1\":1,\"Data2\":0,\"Data3\":0}")
                .As<WithThreeFields<bool, bool, bool>>()
                .Where(x => x.Data1).Is(true)
                .Where(x => x.Data2).Is(false)
                .Where(x => x.Data3).Is(false);

            Unwraps("{\"Data2\":1,\"Data1\":100,\"Data3\":99999}")
                .As<WithThreeFields<int, int, int>>()
                .Where(x => x.Data1).Is(100)
                .Where(x => x.Data2).Is(1)
                .Where(x => x.Data3).Is(99999);

            Unwraps("{\"Data1\":\"Hello\",\"Data2\":\"World!\",\"Data3\":\"I'm here\"}")
                .As<WithThreeFields<string, string, string>>()
                .Where(x => x.Data1).Is("Hello")
                .Where(x => x.Data2).Is("World!")
                .Where(x => x.Data3).Is("I'm here");
        }
        
        public class WithOneField<T>
        {
            public T Data;
        }

        public class WithThreeFields<T1, T2, T3>
        {
            public T1 Data1;
            public T2 Data2;
            public T3 Data3;
        }


        private static UnwrapsFluent Unwraps(string json)
        {
            return new UnwrapsFluent(json);
        }

        private struct UnwrapsFluent
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
        }

        private struct UnwrapsFluentWhere<T>
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

        private struct UnwrapsFluentIs<T, TData>
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
    }
}