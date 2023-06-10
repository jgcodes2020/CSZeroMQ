using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CSZeroMQ.Constants;
using CSZeroMQ.Helpers;
using CSZeroMQ.Native;
using static CSZeroMQ.Native.ZMQ;

namespace CSZeroMQ;

public unsafe class ZMQSocket : IDisposable
{
    public ZMQSocket(SocketType type, ZMQContext? parent = null)
    {
        Context = parent ?? ZMQContext.Global;
        NativeHandle = zmq_socket(Context.NativeHandle, (int) type);
        if (NativeHandle == null)
            throw new ZMQException();
        _disposeHandle = Context.DisposeOnCleanup(this);
    }

    /// <summary>
    /// Binds this socket to a local endpoint, where it can await a connection.
    /// </summary>
    /// <param name="uri">The URI to bind to. (see ZeroMQ docs)</param>
    /// <exception cref="ZMQException">If the underlying <code>zmq_bind()</code> fails</exception>
    public void Bind(string uri)
    {
        byte[] uriArray = Encoding.UTF8.GetBytesNT(uri);
        fixed (byte* pUriArray = uriArray)
        {
            if (zmq_bind(NativeHandle, pUriArray) != 0)
                throw new ZMQException();
        }
    }

    /// <summary>
    /// Connects this socket to an existing endpoint.
    /// </summary>
    /// <param name="uri">The URI to bind to. (see ZeroMQ docs)</param>
    /// <exception cref="ZMQException">If the underlying <code>zmq_bind()</code> fails</exception>
    public void Connect(string uri)
    {
        byte[] uriArray = Encoding.UTF8.GetBytesNT(uri);
        fixed (byte* pUriArray = uriArray)
        {
            if (zmq_connect(NativeHandle, pUriArray) != 0)
                throw new ZMQException();
        }
    }

    /// <summary>
    /// Sends a ZeroMQ message containing plain data.
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="flags">Flags controlling the send operation</param>
    /// <returns>The number of bytes sent, or null if <see cref="SendFlags.DontWait"/> is set and the message could not be sent</returns>
    /// <exception cref="ZMQException">If the underlying <code>zmq_send_const()</code> fails.</exception>
    public int? Send(ReadOnlySpan<byte> data, SendFlags flags = 0)
    {
        fixed (byte* pData = data)
        {
            int res = zmq_send_const(NativeHandle, pData, (nuint) data.Length, (int) flags);
            if (res != -1) return res;

            // check the error value
            int err = zmq_errno();
            if (err == Errno.EAGAIN)
                return null;
            throw new ZMQException(err);
        }
    }

    /// <summary>
    /// Receives a ZeroMQ message containing plain data.
    /// </summary>
    /// <param name="data">Buffer to receive the data</param>
    /// <param name="flags">Flags controlling the receive operation</param>
    /// <returns>The number of bytes sent, or null if <see cref="ReceiveFlags.DontWait"/> is set and the message could not be sent</returns>
    /// <exception cref="ZMQException">If the underlying <code>zmq_recv()</code> fails.</exception>
    public int? Receive(Span<byte> data, ReceiveFlags flags = 0)
    {
        fixed (byte* pData = data)
        {
            int res = zmq_recv(NativeHandle, pData, (nuint) data.Length, (int) flags);
            if (res != -1) return res;

            // check the error value
            int err = zmq_errno();
            if (err == Errno.EAGAIN)
                return null;
            throw new ZMQException(err);
        }
    }

    /// <summary>
    /// Receives a ZeroMQ message into a new <see cref="ZMQMessage"/>
    /// </summary>
    /// <param name="flags">Flags controlling the receive operation</param>
    /// <returns>A <see cref="ZMQMessage"/> containing the data</returns>
    /// <exception cref="ZMQException">If the underlying <code>zmq_recvmsg()</code> fails.</exception>
    public ZMQMessage? ReceiveMsg(ReceiveFlags flags = 0)
    {
        ZMQMessage res = new ZMQMessage();
        fixed (zmq_msg_t* pMsg = &res._msg)
        {
            int rc = zmq_msg_recv(pMsg, NativeHandle, (int) flags);
            if (rc != -1) return res;

            int err = ZMQException.GetErrno();
            if (err == Errno.EAGAIN)
                return null;
            throw new ZMQException(err);
        }
    }

    /// <summary>
    /// Sets a socket option of integral type.
    /// </summary>
    /// <param name="opt">The option to set.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="T">The type of the returned option. Should match that specified by the option itself.</typeparam>
    /// <exception cref="ArgumentException">If the provided option does not match the expected type.</exception>
    /// <exception cref="ZMQException"></exception>
    public void SetOption<T>(SocketOptionInt opt, T value) where T: unmanaged, IBinaryInteger<T>
    {
        var expectType = opt.FindAttribute<IntSockOptDescriptorAttribute>()!.Type;
        if (expectType != typeof(T))
        {
            throw new ArgumentException($"Option {opt} expects type {expectType}", nameof(T));
        }

        var res = zmq_setsockopt(NativeHandle, (int) opt, &value, (nuint) sizeof(T));
        if (res == -1)
            throw new ZMQException();
    }

    /// <summary>
    /// Sets a socket option of string type.
    /// </summary>
    /// <param name="opt">The option to set.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="ZMQException"></exception>
    public void SetOption(SocketOptionString opt, string value)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(value);
        fixed (byte* pValue = buffer)
        {
            var res = zmq_setsockopt(NativeHandle, (int) opt, pValue, (nuint) (buffer.Length * sizeof(byte)));
            if (res == -1)
                throw new ZMQException();
        }
    }
    
    /// <summary>
    /// Sets a socket option of binary data type.
    /// </summary>
    /// <param name="opt">The option to set.</param>
    /// <param name="value">The data to copy in.</param>
    /// <exception cref="ZMQException"></exception>
    public void SetOption(SocketOptionBin opt, ReadOnlySpan<byte> value)
    {
        fixed (byte* pValue = value)
        {
            var res = zmq_setsockopt(NativeHandle, (int) opt, pValue, (nuint) (value.Length * sizeof(byte)));
            if (res == -1)
                throw new ZMQException();
        }
    }

    /// <summary>
    /// Gets a socket option of integral type.
    /// </summary>
    /// <param name="opt">The option to get.</param>
    /// <param name="value">Will contain the value of the option.</param>
    /// <typeparam name="T">The type of the returned option. Should match that specified by the option itself.</typeparam>
    /// <exception cref="ArgumentException">If the provided option does not match the expected type.</exception>
    /// <exception cref="ZMQException">If the underlying <code>zmq_getsockopt()</code> fails.</exception>
    public void GetOption<T>(SocketOptionInt opt, out T value) where T : unmanaged, IBinaryInteger<T>
    {
        var expectType = opt.FindAttribute<IntSockOptDescriptorAttribute>()!.Type;
        if (expectType != typeof(T))
        {
            throw new ArgumentException($"Option {opt} expects type {expectType}", nameof(T));
        }

        fixed (T* pValue = &value)
        {
            var size = (nuint) sizeof(T);
            var res = zmq_getsockopt(NativeHandle, (int) opt, pValue, &size);
            if (res == -1)
                throw new ZMQException();
            Debug.Assert(size == (nuint) sizeof(T));
        }
    }

    /// <summary>
    /// Gets a socket option of string type.
    /// </summary>
    /// <param name="opt">The option to get.</param>
    /// <param name="value">Will contain the value of the option.</param>
    /// <param name="allocSize">Default number of bytes to allocate for the result string.</param>
    /// <exception cref="ZMQException">If the underlying <code>zmq_getsockopt()</code> fails.</exception>
    public void GetOption(SocketOptionString opt, out string value, int allocSize = 1024)
    {
        byte[] buffer = new byte[allocSize];
        fixed (byte* pValue = buffer)
        {
            var size = (nuint) allocSize;
            var res = zmq_getsockopt(NativeHandle, (int) opt, pValue, &size);
            if (res == -1)
                throw new ZMQException();

            if (opt.FindAttribute<StringSockOptDescriptorAttribute>()!.NullTerminated)
            {
                Debug.Assert(pValue[size - 1] == '\0');
                size--;
            }

            value = Encoding.UTF8.GetString(pValue, (int) size);
        }
    }

    /// <summary>
    /// Gets a socket option of binary data type.
    /// </summary>
    /// <param name="opt">An option.</param>
    /// <param name="value">A span to copy the data into.</param>
    /// <returns>The number of bytes copied.</returns>
    /// <exception cref="ZMQException">If the underlying <code>zmq_getsockopt()</code> fails.</exception>
    public int GetOption(SocketOptionBin opt, Span<byte> value)
    {
        fixed (byte* pValue = value)
        {
            
            var size = (nuint) (value.Length * sizeof(byte));
            var res = zmq_getsockopt(NativeHandle, (int) opt, pValue, &size);
            if (res == -1)
                throw new ZMQException();
            return (int) size;
        }
    }

    /// <summary>
    /// Gets a socket option of binary data type. This overload is more convenient but incurs an extra copy.
    /// </summary>
    /// <param name="opt">An option.</param>
    /// <param name="value">Will contain the value of the option.</param>
    /// <param name="allocSize">The amount of bytes to allocate temporarily. By default, allocates an option-specific maximum.</param>
    /// <exception cref="ZMQException">If the underlying <code>zmq_getsockopt()</code> fails.</exception>
    public void GetOption(SocketOptionBin opt, out byte[] value, int? allocSize = null)
    {
        int realAllocSize = allocSize ?? opt.FindAttribute<BinSockOptDescriptorAttribute>()!.MaxSize;
        byte[] allocBuf = new byte[realAllocSize];
        var size = GetOption(opt, allocBuf);
        value = allocBuf[..size];
    }

    #region Internal interface

    public ZMQContext Context { get; }

    /// <summary>
    /// Returns the underlying native handle.
    /// </summary>
    internal void* NativeHandle { get; }

    private readonly Action _disposeHandle;

    #endregion

    #region Dispose pattern

    private void ReleaseUnmanagedResources()
    {
        zmq_close(NativeHandle);
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            Context.OnCleanup -= _disposeHandle;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ZMQSocket()
    {
        Dispose(false);
    }

    #endregion
}