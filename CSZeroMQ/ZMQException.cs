using System.Runtime.InteropServices;
using CSZeroMQ.Native;

namespace CSZeroMQ;

/// <summary>
/// Represents an error caused by ZeroMQ.
/// </summary>
public unsafe class ZMQException : ApplicationException
{
    public ZMQException(int errno) : base(GetErrString(errno))
    {
        Errno = errno;
    }
    
    public ZMQException() : this(GetErrno()) {}

    private static string GetErrString(int errno)
    {
        var errPtr = (IntPtr) ZMQ.zmq_strerror(errno);
        return Marshal.PtrToStringAnsi(errPtr) ?? "Unknown error";
    }

    public int Errno { get; }

    public static int GetErrno()
    {
        return OperatingSystem.IsWindows() ? ZMQ.zmq_errno() : Marshal.GetLastPInvokeError();
    }
}