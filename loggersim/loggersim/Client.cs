using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace loggersim
{
    public class Client
    {
        public Client()
        {
        }

       public void Connect()
        {
            string message = "this is the message from the client..";
            IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = iphostInfo.AddressList[0];
            IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 13000);

            TcpClient client = new TcpClient(localEndpoint);

            // Get a client stream for reading and writing.               
            NetworkStream stream = client.GetStream();

            // Translate the passed message into ASCII and store it as a byte array.
            Byte[] data = new Byte[256];
            data = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the connected TcpServer. 
            Console.WriteLine("Sending the messagge :" + message);
            stream.Write(data, 0, data.Length);

            // Buffer to store the response bytes.
            byte[] bytesToRead = new Byte[client.ReceiveBufferSize];

            // Read the first batch of the TcpServer response bytes.
            Int32 bytesRead = stream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            client.Close();
        }
    }
    }


  

