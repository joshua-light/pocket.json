using System;

namespace Pocket.Json
{
    /// <summary>
    ///     JSON serialization/deserialization extension-methods for all objects.
    /// </summary>
    public static class JsonExtensions
    {
        [ThreadStatic] private static StringBuffer _buffer;

        /// <summary>
        ///     Represents object as formatted JSON string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of converted object.</typeparam>
        /// <returns>JSON representation of object.</returns>
        public static string ToJson<T>(this T self)
        {
            var buffer = _buffer ??= new StringBuffer();
            
            buffer.Clear();

            Json<T>.Write(self, buffer);

            return buffer.AsString();
        }

        /// <summary>
        ///     Represents object as formatted JSON string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>JSON representation of object.</returns>
        public static string ToJson(this object self) =>
            self.ToJson(self.GetType());
        
        /// <summary>
        ///     Represents object as formatted JSON string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="of">Type of converted object.</param>
        /// <returns>JSON representation of object.</returns>
        public static string ToJson(this object self, Type of)
        {
            var buffer = _buffer ??= new StringBuffer();
            
            buffer.Clear();

            Json.Write(of, self, buffer);

            return buffer.AsString();
        }

        /// <summary>
        ///     Deserializes a JSON string into C# object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of JSON object, to which string will be converted.</typeparam>
        /// <returns>Object created from string representation.</returns>
        public static T FromJson<T>(this string self)
        {
            var span = new StringSpan(self);
            
            return Json<T>.Read(ref span);
        }

        /// <summary>
        ///     Deserializes a JSON string into C# object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="type">Type of JSON object, to which string will be converted.</param>
        /// <returns>Object created from string representation.</returns>
        public static object FromJson(this string self, Type type)
        {
            var span = new StringSpan(self);
            
            return Json.Read(type, ref span);
        }
    }
}