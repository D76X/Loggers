using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace loggersim
{
    public class Logger
    {
        static string output = "";
        int port = 456;
        public Logger()
        {
        }

        public void createListener()
        {
            
            TcpListener tcpListener = null;
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            try
            {
                tcpListener = new TcpListener(ipAddress, port);
                tcpListener.Start();
                output = "Waiting for a connection...";
            }
            catch (Exception e)
            {
                output = "Error:  " + e.ToString();
                Console.WriteLine(output);
            }
            while (true)
            {
                Thread.Sleep(10);
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                //Read data from Client
                byte[] bytes = new byte[256];
                NetworkStream stream = tcpClient.GetStream();
                stream.Read(bytes, 0, bytes.Length);
                SocketHelper helper = new SocketHelper();
                helper.processMsg(tcpClient, stream, bytes);

            }

         }

    }
}
