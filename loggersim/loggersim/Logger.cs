using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace loggersim
{
    public class Logger
    {
        Int32 port = 13000;
        private TcpClient tcpClient;

        public Logger()
        {
        }

        public void createListener()
        {
            TcpListener tcpListener = null;
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
           
                tcpListener = new TcpListener(ipAddress, port);
                tcpClient = default(TcpClient);

                tcpListener.Start();

                Console.WriteLine("Waiting for a connection ...");

            while (true)
            {
                Thread.Sleep(10);
                // Create a TCP socket.
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                // Read the data stream from the client.
                byte[] bytes = new byte[256];
                NetworkStream stream = tcpClient.GetStream();
                int bytesRead = stream.Read(bytes, 0, bytes.Length);

                string dataReceived = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                Console.WriteLine("Received : " + bytesRead);

                //Write back text to client
                Console.WriteLine("Sending back : " + dataReceived);
                stream.Write(bytes, 0, bytesRead);
                tcpClient.Close();
                tcpListener.Stop();
                Console.ReadLine();

            }
        }

    }
}
