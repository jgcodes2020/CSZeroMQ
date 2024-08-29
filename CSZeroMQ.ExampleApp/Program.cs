// See https://aka.ms/new-console-template for more information

using CSZeroMQ;
using CSZeroMQ.Constants;

using var sock1 = new ZMQSocket(SocketType.Pair).Bind("inproc://test-socket");
using var sock2 = new ZMQSocket(SocketType.Pair).Connect("inproc://test-socket");

byte[] data = BitConverter.GetBytes(0xDEADBEEFu);
byte[] data2 = new byte[4];

sock1.Send(data);
sock2.Receive(data2);

int val = BitConverter.ToInt32(data2);
Console.WriteLine($"0x{val:X8}");