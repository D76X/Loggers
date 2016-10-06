using CommandLine;
using CommandLine.Text;
using System;
using System.Net;
using System.Net.Sockets;
 

namespace SimpleTcpListener
{
    class SimpleTcpListener
    {
        static public void usage()
        {
            Console.WriteLine("Usage: loggersim.exe [-h help] [-p port] [-s samples]");
            Console.WriteLine("Available options:");
            Console.WriteLine("     -h help      Help message");
            Console.WriteLine("     -p port      Local port for TCP server to listen on");
            Console.WriteLine("     -s samples   How many samples");
           
        }
                        
            private static void OnFail()
        {
            Console.WriteLine("Sorry something went wrong...");
            Console.ReadLine();
            Environment.Exit(-1);
        }
       
        static void Main(string[] args)
        {
            Options options = new Options();
            string myHost = System.Net.Dns.GetHostName();
            IPAddress myIP = Dns.GetHostEntry(myHost).AddressList[0];

            //ushort listenPort = 13000; sub with option.InputPort
            int bufferSize = 500;

            usage();

            // Parse the command line
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    if ((args[i][0] == '-') || (args[i][0] == '/'))
                    {
                        switch (Char.ToLower(args[i][1]))
                        {
                            case 'h':       // Local interface server should listen on
                                myIP = IPAddress.Parse(args[++i]);
                                break;
                            case 'p':       // Port number for the destination
                                options.InputPort = System.Convert.ToUInt16(args[++i]);
                                break;
                            case 's':       // Size of the send and receive buffers
                                bufferSize = System.Convert.ToInt32(args[++i]);
                                break;
                            default:
                                usage();
                                return;
                        }
                    }
                }
                catch
                {
                    usage();
                    return;
                }
            }

            TcpListener tcpServer = null;
            TcpClient tcpClient = null;
            NetworkStream tcpStream = null;
            byte[] sendBuffer = new byte[bufferSize], receiveBuffer = new byte[bufferSize], byteCount;
            int bytesToRead = 0, nextReadCount, rc;

            // Initialize the send buffer
            for (int i = 0; i < sendBuffer.Length; i++)
                sendBuffer[i] = (byte)'X';

            try
            {
                // Create the TCP server
                Console.WriteLine("TCP Listener: Creating the TCP server...");
                tcpServer = new TcpListener(myIP, (int)options.InputPort);
                Console.WriteLine("TCP Listener: TcpListener created on address {0} and port {1}",
                    myIP.ToString(),
                    options.InputPort
                    );

                // Start listening for connections
                Console.WriteLine("TCP Listener: Start listening for connections...");
                tcpServer.Start();

                // Wait for a client connection
                Console.WriteLine("TCP Listener: Waiting for a client connection...");
                tcpClient = tcpServer.AcceptTcpClient();

                // Get the NetworkStream so we can do Read and Write on the client connection
                Console.WriteLine("TCP Listener: Getting the NetworkStream for reading/writing client connection...");
                tcpStream = tcpClient.GetStream();

                byteCount = BitConverter.GetBytes(bytesToRead);

                // First read the number of bytes the client is sending
                Console.WriteLine("TCP Listener: Reading the number of bytes client sent...");
                tcpStream.Read(byteCount, 0, byteCount.Length);

                bytesToRead = BitConverter.ToInt32(byteCount, 0);

                // Receive the data
                Console.WriteLine("TCP Listener: Receiving, reading & displaying the data...");
                while (bytesToRead > 0)
                {
                    // Make sure we don't read beyond what the first message indicates
                    //    This is important if the client is sending multiple "messages" --
                    //    but in this sample it sends only one
                    if (bytesToRead < receiveBuffer.Length)
                        nextReadCount = bytesToRead;
                    else
                        nextReadCount = receiveBuffer.Length;

                    // Read some data
                    rc = tcpStream.Read(receiveBuffer, 0, nextReadCount);

                    // Display what we read
                    string readText = System.Text.Encoding.ASCII.GetString(receiveBuffer, 0, rc);
                    Console.WriteLine("TCP Listener: Received: {0}", readText);
                    bytesToRead -= rc;
                }

                // First send the number of bytes the server is responding with
                Console.WriteLine("TCP Listener: Sending the number of bytes the server is responding with...");
                byteCount = BitConverter.GetBytes(sendBuffer.Length);
                tcpStream.Write(byteCount, 0, byteCount.Length);

                // Send the actual data
                Console.WriteLine("TCP Listener: Sending the actual data...");
                tcpStream.Write(sendBuffer, 0, sendBuffer.Length);

                // Close up the client
                Console.WriteLine("TCP Listener: Closing client tcp stream...");
                tcpStream.Close();
                tcpStream = null;
                tcpClient.Close();
                tcpClient = null;
            }
            catch (SocketException err)
            {
                // Exceptions on the TcpListener are caught here
                Console.WriteLine("TCP Listener: Socket error occurred: {0}", err.Message);
            }
            catch (System.IO.IOException err)
            {
                // Exceptions on the NetworkStream are caught here
                Console.WriteLine("TCP Listener: I/O error: {0}", err.Message);
            }
            finally
            {
                // Close any remaining open resources
                if (tcpServer != null)
                    tcpServer.Stop();
                if (tcpStream != null)
                    tcpStream.Close();
                if (tcpClient != null)
                    tcpClient.Close();
            }




            //OPEN TCP/IP END POINT 

            //CREATE STREAM OF DATA

            //TRANSMIT DATA OVER THAT PORT

            //CRATE A TCP/IP FOR CONSUMER

            //CONSUMER MUST OBTAIN A TCP/IP ENDPOINT WHERE DATA IS PUBLISHED

            //CONSUMER MUST READ THE INCOMING DATA STREAM

            //SAVE THE DATA TO A FILE

        }

        
    }
    
}
