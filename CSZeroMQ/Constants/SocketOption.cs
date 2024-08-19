namespace CSZeroMQ.Constants;

[AttributeUsage(AttributeTargets.Field)]
internal class IntSockOptDescriptorAttribute : Attribute
{
    public IntSockOptDescriptorAttribute(Type baseType)
    {
        BaseType = baseType;
    }

    public Type Type
    {
        get
        {
            if (OperatingSystem.IsWindows() && WindowsType != null)
                return WindowsType;
            else
            {
                if (OperatingSystem.IsLinux() && LinuxType != null)
                    return LinuxType;
                if (OperatingSystem.IsMacOS() && MacOSType != null)
                    return MacOSType;
            }

            return BaseType;
        }
    }
    public Type BaseType { get; init;  }
    public Type? WindowsType { get; init; } = null;
    public Type? MacOSType { get; init; } = null;
    public Type? LinuxType { get; init; } = null;
}

internal class StringSockOptDescriptorAttribute : Attribute
{
    public StringSockOptDescriptorAttribute(bool nullTerminated)
    {
        NullTerminated = nullTerminated;
    }
    
    public bool NullTerminated { get; init; }
}

[AttributeUsage(AttributeTargets.Field)]
internal class BinSockOptDescriptorAttribute : Attribute
{
    public BinSockOptDescriptorAttribute(int exactSize)
    {
        MinSize = MaxSize = exactSize;
    }

    public BinSockOptDescriptorAttribute(int minSize, int maxSize)
    {
        MinSize = minSize;
        MaxSize = maxSize;
    }
    public int MinSize { get; init; }
    public int MaxSize { get; init; }
}

public enum SocketOptionInt : int
{
    [IntSockOptDescriptor(typeof(ulong))]
    Affinity = 4,
    [IntSockOptDescriptor(typeof(int))]
    Backlog = 19,
    [IntSockOptDescriptor(typeof(int))]
    ConnectTimeout = 79,
    [IntSockOptDescriptor(typeof(int))]
    Events = 15,
    [IntSockOptDescriptor(typeof(int), WindowsType = typeof(nuint))]
    FD = 14,
    [IntSockOptDescriptor(typeof(int))]
    GssapiPlaintext = 65,
    [IntSockOptDescriptor(typeof(int))]
    GssapiServer = 62,
    [IntSockOptDescriptor(typeof(int))]
    GssapiServicePrincipalNameType = 91,
    [IntSockOptDescriptor(typeof(int))]
    GssapiPrincipalNameType = 90,
    [IntSockOptDescriptor(typeof(int))]
    HandshakeInterval = 66,
    [IntSockOptDescriptor(typeof(int))]
    Identity = 5,
    [IntSockOptDescriptor(typeof(int))]
    InvertMatching = 74,
    [IntSockOptDescriptor(typeof(int))]
    Ipv4Only = 31,
    [IntSockOptDescriptor(typeof(int))]
    Ipv6 = 42,
    [IntSockOptDescriptor(typeof(int))]
    Linger = 17,
    [IntSockOptDescriptor(typeof(long))]
    MaxMsgSize = 22,
    [IntSockOptDescriptor(typeof(int))]
    Mechanism = 43,
    [IntSockOptDescriptor(typeof(int))]
    MulticastHops = 25,
    [IntSockOptDescriptor(typeof(int))]
    MulticastMaxTpdu = 84,
    [IntSockOptDescriptor(typeof(int))]
    PlainServer = 44,
    [IntSockOptDescriptor(typeof(int))]
    UseFD = 89,
    [IntSockOptDescriptor(typeof(int))]
    Priority = 112,
    [IntSockOptDescriptor(typeof(int))]
    Rate = 8,
    [IntSockOptDescriptor(typeof(int))]
    ReceiveBufferSize = 12,
    [IntSockOptDescriptor(typeof(int))]
    ReceiveHwm = 24,
    [IntSockOptDescriptor(typeof(int))]
    ReceiveMore = 13,
    [IntSockOptDescriptor(typeof(int))]
    ReceiveTimeout = 27,
    [IntSockOptDescriptor(typeof(int))]
    ReconnectInterval = 18,
    [IntSockOptDescriptor(typeof(int))]
    ReconnectIntervalMax = 21,
    [IntSockOptDescriptor(typeof(int))]
    ReconnectStop = 109,
    [IntSockOptDescriptor(typeof(int))]
    RecoveryInterval = 9,
    [IntSockOptDescriptor(typeof(int))]
    SendBufferSize = 11,
    [IntSockOptDescriptor(typeof(int))]
    SendHwm = 23,
    [IntSockOptDescriptor(typeof(int))]
    SendTimeout = 28,
    [IntSockOptDescriptor(typeof(int))]
    TcpKeepalive = 34,
    [IntSockOptDescriptor(typeof(int))]
    TcpKeepaliveCount = 35,
    [IntSockOptDescriptor(typeof(int))]
    TcpKeepaliveIdle = 36,
    [IntSockOptDescriptor(typeof(int))]
    TcpKeepaliveInterval = 37,
    [IntSockOptDescriptor(typeof(int))]
    TcpMaxRt = 80,
    [IntSockOptDescriptor(typeof(int))]
    ThreadSafe = 81,
    [IntSockOptDescriptor(typeof(int))]
    Tos = 57,
    [IntSockOptDescriptor(typeof(int))]
    Type = 16,
    [IntSockOptDescriptor(typeof(int))]
    ZapEnforceDomain = 93,
    [IntSockOptDescriptor(typeof(ulong))]
    VmciBufferSize = 85,
    [IntSockOptDescriptor(typeof(ulong))]
    VmciBufferMinSize = 86,
    [IntSockOptDescriptor(typeof(ulong))]
    VmciBufferMaxSize = 87,
    [IntSockOptDescriptor(typeof(int))]
    VmciConnectTimeout = 88,
    [IntSockOptDescriptor(typeof(int))]
    MulticastLoop = 96,
    [IntSockOptDescriptor(typeof(int))]
    RouterNotify = 97,
    [IntSockOptDescriptor(typeof(int))]
    InBatchSize = 101,
    [IntSockOptDescriptor(typeof(int))]
    OutBatchSize = 102,
}

public enum SocketOptionString : int
{
    [StringSockOptDescriptor(false)]
    BindToDevice = 92,
    [StringSockOptDescriptor(true)]
    GssapiPrincipal = 63,
    [StringSockOptDescriptor(true)]
    GssapiServicePrincipal = 64,
    [StringSockOptDescriptor(true)]
    LastEndpoint = 32,
    [StringSockOptDescriptor(true)]
    PlainPassword = 46,
    [StringSockOptDescriptor(true)]
    PlainUsername = 45,
    [StringSockOptDescriptor(true)]
    SocksProxy = 68,
    [StringSockOptDescriptor(false)]
    ZapDomain = 55,
    [StringSockOptDescriptor(true)]
    Subscribe = 6,
}

public enum SocketOptionBin : int
{
    [BinSockOptDescriptor(32)]
    CurvePublicKey = 48,
    [BinSockOptDescriptor(32)]
    CurveSecretKey = 49,
    [BinSockOptDescriptor(32)]
    CurveServerKey = 50,
    [BinSockOptDescriptor(1, 255)]
    RoutingId = 5,
}