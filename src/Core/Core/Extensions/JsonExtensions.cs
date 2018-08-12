using System;

namespace Pocket.Json
{
    /// <summary>
    ///     Represents JSON serialization/deserialization extension-methods for all objects.
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
        public static string AsJson<T>(this T self)
        {
            if (_buffer == null)
                _buffer = new StringBuffer();

            var buffer = _buffer;
            buffer.Clear();

            Json<T>.Append(self, buffer);

            return buffer.AsString();
        }

        /// <summary>
        ///     Represents object as formatted JSON string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="type">Type of converted object.</param>
        /// <returns>JSON representation of object.</returns>
        public static string AsJson(this object self, Type type)
        {
            if (_buffer == null)
                _buffer = new StringBuffer();

            var buffer = _buffer;
            buffer.Clear();

            Json.Append(type, self, buffer);

            return buffer.AsString();
        }

        /// <summary>
        ///     Represents string as JSON object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of JSON object, to which string will be converted.</typeparam>
        /// <returns>Object created from string representation.</returns>
        public static T AsJson<T>(this string self) =>
            Json<T>.Unwrap(new JsonSpan(self));
        
        /// <summary>
        ///     Represents string as JSON object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="type">Type of JSON object, to which string will be converted.</param>
        /// <returns>Object created from string representation.</returns>
        public static object AsJson(this string self, Type type) =>
            Json.Unwrap(type, new JsonSpan(self));
    }
}