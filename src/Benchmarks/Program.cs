using System;
using System.Diagnostics;

namespace Pocket.Json.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BigObjectOnlyRun();
        }

        private static void DefaultRun()
        {
            Serialize<Single.WithBool.True>();
            Serialize<Single.WithBool.False>();

            Serialize<Single.WithByte.Zero>();
            Serialize<Single.WithByte.Short>();
            Serialize<Single.WithByte.Middle>();
            Serialize<Single.WithByte.Max>();

            Serialize<Single.WithChar.Zero>();
            Serialize<Single.WithChar.Letter>();
            Serialize<Single.WithChar.Digit>();
            
            Serialize<Single.WithInt.Zero>();
            Serialize<Single.WithInt.Short>();
            Serialize<Single.WithInt.Min>();
            Serialize<Single.WithInt.Max>();
            
            Serialize<Single.WithLong.Zero>();
            Serialize<Single.WithLong.Short>();
            Serialize<Single.WithLong.Min>();
            Serialize<Single.WithLong.Max>();
            
            Serialize<Single.WithFloat.Zero>();
            Serialize<Single.WithFloat.OneDigit>();
            Serialize<Single.WithFloat.SevenDigits>();
            Serialize<Single.WithFloat.Min>();
            Serialize<Single.WithFloat.Max>();

            Serialize<Single.WithDouble.Min>();
            Serialize<Single.WithDouble.Zero>();
            Serialize<Single.WithDouble.OneDigit>();
            Serialize<Single.WithDouble.FifteenDigits>();
            Serialize<Single.WithDouble.Max>();
            
            Serialize<Single.WithString.Null>();
            Serialize<Single.WithString.Empty>();
            Serialize<Single.WithString.Short>();
            Serialize<Single.WithString.Long>();
            
            Serialize<TenFields.Bool>();
            Serialize<TenFields.Char>();
            Serialize<TenFields.Byte>();
            Serialize<TenFields.Int>();
            Serialize<TenFields.Long>();
            Serialize<TenFields.Float>();
            Serialize<TenFields.Double>();

            Serialize<Single.WithArray>();
            Serialize<Single.WithHashSet>();
            Serialize<Single.WithDictionary>();
            Serialize<Single.WithList>();

            Deserialize<Single.WithBool.True>();
            Deserialize<Single.WithBool.False>();
            
            Deserialize<Single.WithByte.Zero>();
            Deserialize<Single.WithByte.Short>();
            Deserialize<Single.WithByte.Middle>();
            Deserialize<Single.WithByte.Max>();
            
            Deserialize<Single.WithChar.Zero>();
            Deserialize<Single.WithChar.Letter>();
            Deserialize<Single.WithChar.Digit>();
            
            Deserialize<Single.WithInt.Zero>();
            Deserialize<Single.WithInt.Short>();
            Deserialize<Single.WithInt.Min>();
            Deserialize<Single.WithInt.Max>();
            
            Deserialize<Single.WithLong.Zero>();
            Deserialize<Single.WithLong.Short>();
            Deserialize<Single.WithLong.Min>();
            Deserialize<Single.WithLong.Max>();
            
            Deserialize<Single.WithFloat.Zero>();
            Deserialize<Single.WithFloat.OneDigit>();
            Deserialize<Single.WithFloat.SevenDigits>();
            Deserialize<Single.WithFloat.Min>();
            Deserialize<Single.WithFloat.Max>();

            Deserialize<Single.WithDouble.Min>();
            Deserialize<Single.WithDouble.Zero>();
            Deserialize<Single.WithDouble.OneDigit>();
            Deserialize<Single.WithDouble.FifteenDigits>();
            Deserialize<Single.WithDouble.Max>();
            
            Deserialize<Single.WithString.Null>();
            Deserialize<Single.WithString.Empty>();
            Deserialize<Single.WithString.Short>();
            Deserialize<Single.WithString.Long>();

            Deserialize<TenFields.Bool>();
            Deserialize<TenFields.Char>();
            Deserialize<TenFields.Byte>();
            Deserialize<TenFields.Int>();
            Deserialize<TenFields.Long>();
            Deserialize<TenFields.Float>();
            Deserialize<TenFields.Double>();

            Deserialize<Single.WithArray>();
            Deserialize<Single.WithList>();
            Deserialize<Single.WithHashSet>();
            Deserialize<Single.WithDictionary>();
        }

        private static void BigObjectOnlyRun()
        {
            Serialize<BigObject>();
            Deserialize<BigObject>();
        }

        private static void Serialize<T>() where T : new()
        {
            var item = new T();

            Console.WriteLine("---------------------" + typeof(T).FullName);

            Run("Newtonsoft.Json", 1000, () => Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Run("Ut8Json", 1000, () => Utf8Json.JsonSerializer.ToJsonString(item));
            Run("Castalia", 1000, () => item.AsJson());

            Console.WriteLine("---------------------");
        }

        private static void Deserialize<T>() where T : new()
        {
            Deserialize(new T());
        }

        private static void Deserialize<T>(T item)
        {
            var newtonsoftJson = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var utf8Json = Utf8Json.JsonSerializer.Serialize(item);
            var json = item.AsJson();

            Console.WriteLine("---------------------" + typeof(T).FullName);

            Run("Newtonsoft.Json", 1000, () => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(newtonsoftJson));
            Run("Ut8Json", 1000, () => Utf8Json.JsonSerializer.Deserialize<T>(utf8Json));
            Run("Castalia", 1000, () => json.AsJson<T>());

            Console.WriteLine("---------------------");
        }

        private static void Run(string name, int times, Action action)
        {
            // Warmup.
            for (var i = 0; i < 10; i++)
                action();
            
            GC.Collect();

            var sw = Stopwatch.StartNew();

            for (var i = 0; i < times; i++)
                action();

            sw.Stop();

            Console.WriteLine($"{name} [{times}]: {sw.ElapsedMilliseconds} ms.");
            
            GC.Collect();
        }
    }
}