using System.Runtime.CompilerServices;

namespace Pocket.Json
{
    public static class Bitwise
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Equals(int a, int b)
        {
            // Found this as an answer to 4161656 StackOverflow question written by Fabian Giesen.
            
            // So, basically we just take the concept of difference between numbers.
            // It can be either 0 or some pair X and -X.
            // X | -X will result in some negative value (`-` bit will be transfered from one to another).
            // 0 | 0 will result in 0.
            var result = (a - b) | (b - a);
            
            // By using `>>` we reduce any negative value to just -1 or leaving 0 (if `result` is 0).
            result >>= 31;
            
            return result + 1;
        }
    }
}