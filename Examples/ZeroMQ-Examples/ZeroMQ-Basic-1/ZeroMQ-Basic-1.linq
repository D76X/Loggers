<Query Kind="Statements">
  <Reference Relative="..\..\..\packages\AsyncIO.0.1.26.0\lib\net40\AsyncIO.dll">C:\GitHub\Loggers\packages\AsyncIO.0.1.26.0\lib\net40\AsyncIO.dll</Reference>
  <Reference Relative="..\..\..\packages\NetMQ.4.0.0.1\lib\net40\NetMQ.dll">C:\GitHub\Loggers\packages\NetMQ.4.0.0.1\lib\net40\NetMQ.dll</Reference>
  <Namespace>NetMQ</Namespace>
  <Namespace>NetMQ.Sockets</Namespace>
</Query>

using (var server = new ResponseSocket("@tcp://localhost:5556")) // bind
using (var client = new RequestSocket(">tcp://localhost:5556"))  // connect
{
    // Send a message from the client socket
    client.SendFrame("Hello");

    // Receive the message from the server socket
    string m1 = server.ReceiveFrameString();
    Console.WriteLine("From Client: {0}", m1);

    // Send a response back from the server
    server.SendFrame("Hi Back");

    // Receive the response from the client socket
    string m2 = client.ReceiveFrameString();
    Console.WriteLine("From Server: {0}", m2);
}