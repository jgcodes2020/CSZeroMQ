namespace CSZeroMQ.Constants;

public enum SocketType : int
{
    Pair = 0,
    Pub = 1,
    Sub = 2,
    Req = 3,
    Rep = 4,
    Dealer = 5,
    Router = 6,
    Pull = 7,
    Push = 8,
    XPub = 9,
    XSub = 10,
    Stream = 11,
    Server = 12,
    Client = 13,
    Radio = 14,
    Gather = 15,
    DGram = 18,
    Peer = 19,
    Channel = 20
}

[Flags]
public enum SendFlags : int
{
    DontWait = 1,
    SendMore = 2
}

[Flags]
public enum ReceiveFlags : int
{
    DontWait = 1
}