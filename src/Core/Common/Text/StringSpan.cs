using System;
using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    public unsafe struct StringSpan : IEquatable<StringSpan>
    {
        public static StringSpan Zero = new StringSpan();

        public string Source;
        public int Start;
        public int End;

        public StringSpan(string source)
        {
            Source = source;
            Start = 0;
            End = source.Length;
        }

        private StringSpan(string source, int start, int length)
        {
            Source = source;
            Start = start;
            End = start + length;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char LastCharAt(int i) => Source[End - i - 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char CharAt(int i) => Source[Start + i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty() => End - Start <= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => Source.Substring(Start, End - Start);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringSpan SubSpan(int startIndex, int length) => new StringSpan(Source, Start + startIndex, length);
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringSpan SubSpan(int length) => new StringSpan(Source, Start, length);

        #region Mutable

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipMutable(int count)
        {
            Start += count;
        }

        #endregion

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

        public override int GetHashCode()
        {
            return GetHashCode(Source, Start, End - Start);
        }

        public static int GetHashCode(string str)
        {
            return GetHashCode(str, 0, str.Length);
        }

        private static int GetHashCode(char* sourcePtr, int length)
        {
            switch (length)
            {
                case 1: return sourcePtr[0];
                case 2: return sourcePtr[0] * 9733 ^ sourcePtr[1];
                case 3: return (sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2];
                case 4: return ((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3];
                case 5: return (((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4];
                case 6: return ((((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4]) * 9733 ^ sourcePtr[5];
                case 7: return (((((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4]) * 9733 ^ sourcePtr[5]) * 9733 ^ sourcePtr[6];
                case 8: return ((((((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4]) * 9733 ^ sourcePtr[5]) * 9733 ^ sourcePtr[6]) * 9733 ^ sourcePtr[7];
                case 9: return (((((((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4]) * 9733 ^ sourcePtr[5]) * 9733 ^ sourcePtr[6]) * 9733 ^ sourcePtr[7]) * 9733 ^ sourcePtr[8];
                case 10: return ((((((((sourcePtr[0] * 9733 ^ sourcePtr[1]) * 9733 ^ sourcePtr[2]) * 9733 ^ sourcePtr[3]) * 9733 ^ sourcePtr[4]) * 9733 ^ sourcePtr[5]) * 9733 ^ sourcePtr[6]) * 9733 ^ sourcePtr[7]) * 9733 ^ sourcePtr[8]) * 9733 ^ sourcePtr[9];
            }
            
            length *= 2;
            
            var ptr = (byte*) sourcePtr;
            var hash = (int) ptr[0];

            ptr += 2;

            for (var i = 2; i < length; i += 2)
            {
                hash = (hash * 9733) ^ ptr[0];
                
                ptr += 2;
            }
            
            return hash;
        }
        
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