using System.Text;
using CSZeroMQ;
using CSZeroMQ.Constants;

namespace CSZeroMQSubscriber
{
    internal static class SubProgram
    {
        private static void Main(string[] args)
        {
            const string uri = "ipc://ipx_example.ipc";
            ZMQContext context = new ZMQContext();
            ZMQSocket subSocket = new ZMQSocket(SocketType.Sub, context);

            subSocket.Connect(uri);
            subSocket.SetOption(SocketOptionString.Subscribe, ""); // Subscribe to all messages
            Console.WriteLine("Subscriber connected to " + uri);

            while (true)
            {
                var zmqMsg = subSocket.ReceiveMsg();
                if (zmqMsg != null)
                {
                    var decodedString = Encoding.UTF8.GetString(zmqMsg.Span);
                    Console.WriteLine("Received: " + decodedString);
                }
            }
        }
    }
}