using System.Text;
using CSZeroMQ;
using CSZeroMQ.Constants;

namespace CSZeroMQPublisher
{
    internal static class PubProgram
    {
        private static void Main(string[] args)
        {
            const string uri = "ipc://ipx_example.ipc";
            ZMQContext context = new ZMQContext();
            ZMQSocket pubSocket = new ZMQSocket(SocketType.Pub, context);

            pubSocket.Bind(uri);
            Console.WriteLine("Publisher bound to " + uri);

            while (true)
            {
                ReadOnlySpan<byte> msg = Encoding.UTF8.GetBytes("Hello world");
                pubSocket.Send(msg);
                Console.WriteLine("Sent: Hello world");
                Thread.Sleep(1000);  // Send a message every second
            }
        }
    }
}