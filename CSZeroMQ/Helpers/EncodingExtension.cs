using System.Text;

namespace CSZeroMQ.Helpers;

public static class EncodingExtensions
{
    /// <summary>
    /// Creates a null-terminated string with the given encoding.
    /// </summary>
    /// <param name="enc">The encoding to use</param>
    /// <param name="str">The string to use</param>
    /// <returns></returns>
    public static byte[] GetBytesNT(this Encoding enc, string str)
    {
        return enc.GetBytes(str + char.MinValue);
    }
}