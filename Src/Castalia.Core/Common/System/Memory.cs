namespace Castalia
{
    internal static class Memory
    {
        public static unsafe void Copy(char* sourcePtr, char* targetPtr, int length)
        {
            if (length <= 0)
                return;

            if (((int) targetPtr & 2) != 0)
            {
                *targetPtr++ = *sourcePtr++;

                length--;
            }

            while (length >= 8)
            {
                *(int*) targetPtr = (int) *(uint*) sourcePtr;
                *(int*) (targetPtr + 2) = (int) *(uint*) (sourcePtr + 2);
                *(int*) (targetPtr + 4) = (int) *(uint*) (sourcePtr + 4);
                *(int*) (targetPtr + 6) = (int) *(uint*) (sourcePtr + 6);

                targetPtr += 8;
                sourcePtr += 8;
                length -= 8;
            }

            if ((length & 4) != 0)
            {
                *(int*) targetPtr = (int) *(uint*) sourcePtr;
                *(int*) (targetPtr + 2) = (int) *(uint*) (sourcePtr + 2);

                targetPtr += 4;
                sourcePtr += 4;
            }

            if ((length & 2) != 0)
            {
                *(int*) targetPtr = (int) *(uint*) sourcePtr;

                targetPtr += 2;
                sourcePtr += 2;
            }

            if ((length & 1) == 0)
                return;

            *targetPtr = *sourcePtr;
        }

        public static unsafe bool Equal(char* sourcePtr, char* targetPtr, int length)
        {
            if (length <= 0)
                return false;

            var sourcePtrLocal = sourcePtr;
            var targetPtrLocal = targetPtr;

            while (length >= 8
                && *(int*) sourcePtrLocal == *(int*) targetPtrLocal
                && *(int*) (sourcePtrLocal + 2) == *(int*) (targetPtrLocal + 2)
                && *(int*) (sourcePtrLocal + 4) == *(int*) (targetPtrLocal + 4)
                && *(int*) (sourcePtrLocal + 6) == *(int*) (targetPtrLocal + 6))
            {
                sourcePtrLocal += 8;
                targetPtrLocal += 8;

                length -= 8;
            }

            while (length >= 2
                && *(int*) sourcePtrLocal == *(int*) targetPtrLocal
            )
            {
                sourcePtrLocal += 2;
                targetPtrLocal += 2;

                length -= 2;
            }

            if (length == 1 && *sourcePtrLocal == *targetPtrLocal)
                return true;

            return false;
        }
    }
}