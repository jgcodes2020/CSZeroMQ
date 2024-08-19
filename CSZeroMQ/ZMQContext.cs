using CSZeroMQ.Native;
using static CSZeroMQ.Native.ZMQ;

namespace CSZeroMQ;

public unsafe class ZMQContext : IDisposable
{
    public ZMQContext()
    {
        NativeHandle = zmq_ctx_new();
        // No errors are defined
        if (NativeHandle == null)
            throw new ZMQException();
    }

    #region IDisposable impl

    private void ReleaseUnmanagedResources()
    {
        OnCleanup();
        zmq_ctx_destroy(NativeHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ZMQContext()
    {
        ReleaseUnmanagedResources();
    }

    #endregion

    #region Internal interface
    
    /// <summary>
    /// Returns the underlying pointer.
    /// </summary>
    public void* NativeHandle { get; }

    /// <summary>
    /// Executed when disposing this ZMQContext.
    /// </summary>
    internal event Action OnCleanup = () => {};

    /// <summary>
    /// Helper function to dispose an object when cleaning up.
    /// </summary>
    /// <param name="obj">the object to dispose</param>
    /// <returns>the handler</returns>
    internal Action DisposeOnCleanup(IDisposable obj)
    {
        var handler = obj.Dispose;
        OnCleanup += handler;
        return handler;
    }

    #endregion
    
    #region Global context (for convenience)

    private static ZMQContext? _globalContext;
    
    /// <summary>
    /// Global ZeroMQ context provided for convenience.
    /// </summary>
    public static ZMQContext Global
    {
        get
        {
            if (_globalContext != null)
                return _globalContext;
            
            _globalContext = new ZMQContext();
            AppDomain.CurrentDomain.DomainUnload += (_, _) => _globalContext.Dispose();
            return _globalContext;
        }
    }

    #endregion
}