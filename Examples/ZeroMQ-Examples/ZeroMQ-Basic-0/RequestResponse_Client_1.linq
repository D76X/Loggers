<Query Kind="Statements">
  <Reference Relative="AsyncIO.dll">C:\GitHub\Loggers\Examples\ZeroMQ-Examples\ZeroMQ-Basic-0\AsyncIO.dll</Reference>
  <Reference Relative="NetMQ.dll">C:\GitHub\Loggers\Examples\ZeroMQ-Examples\ZeroMQ-Basic-0\NetMQ.dll</Reference>
  <Namespace>AsyncIO</Namespace>
  <Namespace>NetMQ</Namespace>
  <Namespace>NetMQ.Monitoring</Namespace>
  <Namespace>NetMQ.Sockets</Namespace>
</Query>

// Set the Language dropdown box above to C# Satement(s)
using (var client = new RequestSocket())
{
    client.Connect("tcp://localhost:5555");

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine("Sending Hello");
        client.SendFrame("Hello");

        var message = client.ReceiveFrameString();
        Console.WriteLine("Received {0}", message);
    }
}