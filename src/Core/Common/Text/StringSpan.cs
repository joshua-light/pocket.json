using System;

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

        public char this[int i] => Source[Offset + i];

        public bool IsEmpty()
        {
            return Length <= 0;
        }

        public override string ToString()
        {
            return Source.Substring(Offset, Length);
        }

        public StringSpan SubSpan(int startIndex, int length)
        {
            return new StringSpan(Source, Offset + startIndex, length);
        }

        public StringSpan SubSpan(int length)
        {
            return new StringSpan(Source, Offset, length);
        }

        public StringSpan Cut(int start, int end)
        {
            return new StringSpan(Source, Offset + start, Length - start - end);
        }

        public int IndexOf(char c)
        {
            for (var i = 0; i < Length; i++)
                if (this[i] == c)
                    return i;
            
            return -1;
        }

        #region Mutable

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
                if (this[i] != other[i])
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
            var num1 = 352654597;
            var num2 = num1;
            var numPtr = (int*) sourcePtr;
//            
//            for (var i = length; i > 0; i -= 4)
//            {
//                num1 = ((num1 << 5) + num1 + (num1 >> 27)) ^ numPtr[0];
//                
//                if (i <= 2)
//                    break;
//                
//                num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ numPtr[1];
//                numPtr += 2;
//            }

            while (length > 0)
            {
                var val = (char) *numPtr;
                num1 = ((num1 << 5) + num1 + (num1 >> 27)) ^ val;
                if (length > 2)
                {
                    val = (char) numPtr[1];
                    num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ val;
                    numPtr += 2;
                    length -= 4;
                }
                else
                {
                    break;
                }
            }

            return num1 + num2 * 1566083941;
        }
    }
}