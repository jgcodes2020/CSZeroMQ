namespace CSZeroMQ.Native;

public static unsafe class NativeUtils
{
    public static long StringLengthNT(byte* str)
    {
        byte* p;
        for (p = str; *p != 0; p++) {}

        return p - str;
    }
}