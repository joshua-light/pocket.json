using System;
using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    public unsafe struct StringSpan : IEquatable<StringSpan>
    {
        public static StringSpan Zero = new StringSpan();

        public string Source;
        public int Offset;
        public int Length;

        public StringSpan(string source)
        {
            Source = source;
            Offset = 0;
            Length = source.Length;
        }

        private StringSpan(string source, int offset, int length)
        {
            Source = source;
            Offset = offset;
            Length = length;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char LastCharAt(int i) => Source[Offset + Length - 1 - i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char CharAt(int i) => Source[Offset + i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty() => Length <= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => Source.Substring(Offset, Length);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringSpan SubSpan(int startIndex, int length) => new StringSpan(Source, Offset + startIndex, length);
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringSpan SubSpan(int length) => new StringSpan(Source, Offset, length);

        #region Mutable

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipMutable(int count)
        {
            Offset += count;
            Length -= count;
        }

        #endregion

        public bool Equals(StringSpan other)
        {
            if (Length != other.Length)
                return false;
            
            for (var i = 0; i < Length; i++)
                if (CharAt(i) != other.CharAt(i))
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            fixed (char* sourcePtr = Source)
            {
                return GetHashCode(sourcePtr + Offset, Length);
            }
        }

        public static int GetHashCode(string str)
        {
            fixed (char* strPtr = str)
            {
                return GetHashCode(strPtr, str.Length);
            }
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
    }
}