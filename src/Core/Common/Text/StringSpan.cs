using System;
using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    public struct StringSpan : IEquatable<StringSpan>
    {
        public static readonly StringSpan Zero = new StringSpan();

        public string Source;
        public int Start;
        public int End;

        public StringSpan(string source)
        {
            Source = source;
            Start = 0;
            End = source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char CharAt(int i) => Source[Start + i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipMutable(int count) => Start += count;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => Source.Substring(Start, End - Start);

        public bool Equals(StringSpan other)
        {
            var length = End - Start;
            if (length != other.End - other.Start)
                return false;
            
            for (var i = 0; i < length; i++)
                if (CharAt(i) != other.CharAt(i))
                    return false;

            return true;
        }

        public override int GetHashCode() =>
            GetHashCode(Source, Start, End - Start);

        public static int GetHashCode(string str) =>
            GetHashCode(str, 0, str.Length);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(string source, int offset, int length)
        {
            switch (length)
            {
                case 1: return source[offset];
                case 2: return source[offset] * 9733 ^ source[offset + 1];
                case 3: return (source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2];
                case 4: return ((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3];
                case 5: return (((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4];
                case 6: return ((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5];
                case 7: return (((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6];
                case 8: return ((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7];
                case 9: return (((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7]) * 9733 ^ source[offset + 8];
                case 10: return ((((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7]) * 9733 ^ source[offset + 8]) * 9733 ^ source[offset + 9];
            }
            
            var hash = (int) source[offset];
            var i = offset + 1;
            length--;

            while (length > 2)
            {
                hash = (hash * 9733) ^ source[i++];
                hash = (hash * 9733) ^ source[i++];
                length -= 2;
            }

            if (length != 0)
                hash = (hash * 9733) ^ source[i];

            return hash;
        }
    }
}