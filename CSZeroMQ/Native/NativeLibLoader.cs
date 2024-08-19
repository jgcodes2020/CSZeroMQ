using Microsoft.Extensions.Logging;
using NativeLibraryManager;
using Serilog.Extensions.Logging;
using System.Reflection;


namespace CSZeroMQ.Native;

public static class NativeLibLoader
{
    private static readonly LibraryManager LibManager;
    private static readonly LoggerFactory Factory = new LoggerFactory(new[] {new SerilogLoggerProvider() });
    private static readonly ResourceAccessor Accessor = new ResourceAccessor(Assembly.GetExecutingAssembly());
    static NativeLibLoader()
    {
        try
        {
            LibManager = new LibraryManager(
                Factory,
                new LibraryItem(Platform.MacOs, Bitness.x64,
                    new LibraryFile("libzmq.dylib", Accessor.Binary("runtimes.osx_x64.native.libzmq.dylib"))),
                new LibraryItem(Platform.Windows, Bitness.x64,
                    new LibraryFile("libzmq.dll", Accessor.Binary("runtimes.win10_x64.native.libzmq-v143-mt-4_3_5.dll"))),
                new LibraryItem(Platform.Linux, Bitness.x64, 
                    new LibraryFile("libzmq.so", Accessor.Binary("runtimes.linux_x64.native.libzmq.so")))
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Loading Native Library Issue");
        }
    }

    public static void LoadLibrary()
    {
        try
        {
            LibManager.LoadNativeLibrary();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Loading Native Library Issue");
        }
    }
}