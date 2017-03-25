<Query Kind="Statements">
  <Reference Relative="AsyncIO.dll">C:\GitHub\Loggers\Examples\ZeroMQ-Examples\ZeroMQ-Basic-0\AsyncIO.dll</Reference>
  <Reference Relative="NetMQ.dll">C:\GitHub\Loggers\Examples\ZeroMQ-Examples\ZeroMQ-Basic-0\NetMQ.dll</Reference>
  <Namespace>AsyncIO</Namespace>
  <Namespace>NetMQ</Namespace>
  <Namespace>NetMQ.Monitoring</Namespace>
  <Namespace>NetMQ.Sockets</Namespace>
</Query>

// Instructions

// Dowload the nuget packages  
// https://www.nuget.org/packages/NetMQ/
// https://www.nuget.org/packages/AsyncIO/

// Change the extensions .nupkg to .zip
// Unzip and find the binaries in the Lib\Net4 folders
// copy the binaries NetMQ.ddl and AsyncIO.dll in the same folder as the LINQPad files
// Reference the binaries in the LINQpad files via Query >> Properties >> Add References
// From the same Query Properties Window >> Additional Namespaces >> Pick From Assemblies

// For the examples refer to the guide
// https://netmq.readthedocs.io/en/latest/introduction/

// Set the Language dropdown box above to C# Satement(s)

// use the command \>netstat -aon | more and the the PID in TaskManager to see the listening process 
// better \>netstat -o -n -a | findstr 0.0:5555
// or \>netstat -na | findstr 5555

// to terminate teh process
// \>taskkill /F /pid <port number>

using (var server = new ResponseSocket())
{
    server.Bind("tcp://*:5555");

    while (true)
    {
        var message = server.ReceiveFrameString();

        Console.WriteLine("Received {0}", message);

        // processing the request
        Thread.Sleep(100);

        Console.WriteLine("Sending World");
        server.SendFrame("World");
    }
}
