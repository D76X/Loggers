using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace loggersim
{
    public class Client
    {
        public Client()
        {
        }

        static void Connect(string loggerIP, string message)
        {
            string output = "";
            try
            {
                Int32 port = 13;
                TcpClient client = new TcpClient(loggerIP, port);

                // Translate the passed message into ASCII and store it as a byte array.
                Byte[] data = new Byte[256];
                data = System.Text.Encoding.ASCII.GetBytes(message);

                //client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                //send message to the connected TCPServer
                stream.Write(data, 0, data.Length);
                output = "Sent: " + message;
                Console.WriteLine(output);

                //Buffer to storeresponse
                data =new Byte[256];

                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                output = "Received: " + responseData;
                Console.WriteLine( output);

                // Close everything.
                stream.Close();
                client.Close();
            }

            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                Console.WriteLine(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                Console.WriteLine(output);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // In this code example, use a hard-coded
            // IP address and message.
            string serverIP = "localhost";
            string message = "Hello";
            Connect(serverIP, message);
        }

    }
    }


  

