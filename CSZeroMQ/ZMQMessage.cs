using System.Runtime.InteropServices;
using System.Text;
using CSZeroMQ.Constants;
using CSZeroMQ.Native;
using static CSZeroMQ.Native.ZMQ;

namespace CSZeroMQ;

/// <summary>
/// A dynamic buffer used for storing ZeroMQ messages.
/// </summary>
public sealed unsafe class ZMQMessage
{
    public ZMQMessage()
    {
        fixed (zmq_msg_t* pMsg = &_msg)
        {
            zmq_msg_init(pMsg);
        }
    }

    public ZMQMessage(int size)
    {
        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be negative");
        fixed (zmq_msg_t* pMsg = &_msg)
        {
            zmq_msg_init_size(pMsg, (nuint) size);
        }
    }

    public ZMQMessage(Memory<byte> data) : this(data.Length)
    {
        fixed (zmq_msg_t* pMsg = &_msg)
        {
            void* msgData = zmq_msg_data(pMsg);
            data.Span.CopyTo(new Span<byte>(msgData, data.Length));
        }
    }

    public ZMQMessage(ZMQMessage other)
    {
        fixed (zmq_msg_t* pMsg = &_msg, pOtherMsg = &other._msg)
        {
            zmq_msg_copy(pMsg, pOtherMsg);
        }
    }

    public Span<byte> Span
    {
        get
        {
            fixed (zmq_msg_t* pMsg = &_msg)
            {
                nuint size = zmq_msg_size(pMsg);
                if (size > int.MaxValue)
                {
                    throw new InvalidOperationException("ZeroMQ span size too large!");
                }
                return new Span<byte>(zmq_msg_data(pMsg), (int) size);
            }
        }
    }

    public int GetProperty(MessageProperty prop)
    {
        fixed (zmq_msg_t* pMsg = &_msg)
        {
            int res = zmq_msg_get(pMsg, (int) prop);
            if (res == -1)
                throw new ZMQException();

            return res;
        }
    }

    public string GetMetadata(string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        fixed (zmq_msg_t* pMsg = &_msg)
        fixed (byte* pKey = keyBytes)
        {
            byte* res = zmq_msg_gets(pMsg, pKey);
            if (res == null)
                throw new ZMQException();

            long count = NativeUtils.StringLengthNT(res);
            if (count > int.MaxValue)
                throw new InvalidOperationException("ZeroMQ string too long!");
            return Encoding.UTF8.GetString(res, (int) count);
        }
    }
    
    

    #region Internal interface

    internal zmq_msg_t _msg;

    #endregion
}