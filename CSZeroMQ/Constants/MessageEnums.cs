namespace CSZeroMQ.Constants;

/// <summary>
/// ZeroMQ message properties that return int.
/// </summary>
public enum MessageProperty : int
{
    More = 1,
    [Obsolete]
    SourceFD = 2,
    Shared = 3
}