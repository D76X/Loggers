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
            Console.WriteLine("     -s size      Buffer size");
           
        }

    private static void OnFail()
        {
            Console.WriteLine("Sorry something went wrong...");
            Console.ReadLine();
            Environment.Exit(-1);
        }

        private static byte[] SampleGenerator()
        {
            byte[] samples = new byte[99];
            Random rnd = new Random();

            for (int i = 1; i <samples.Length; i++)
            {
                samples[i] = (byte) rnd.Next(0, 254);
            }

            return samples;
        }

        static void Main(string[] args)
        {
            Options options = new Options();
            string myHost = System.Net.Dns.GetHostName();
            IPAddress myIP = Dns.GetHostEntry(myHost).AddressList[0];
          
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
                            case 'b':       // Size of the send and receive buffers
                                options.Buffer = System.Convert.ToInt32(args[++i]);
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

            //Initialize the buffer
            byte[] sendBuffer = new byte[options.Buffer];

            byte[] receiveBuffer = new byte[options.Buffer], byteCount;
            int bytesToRead = 0, nextReadCount, rc;

            sendBuffer = SampleGenerator();

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
                
                while ((bytesToRead = tcpStream.Read(receiveBuffer, 0, nextReadCount)) != 0)
                {
                    byteCount = BitConverter.GetBytes(bytesToRead);
                    Console.WriteLine("Received: {0}", byteCount);


                    // First send the number of bytes the server is responding with
                    //Console.WriteLine("LOGGER: Sending the number of bytes the server is responding with...");


                    byteCount = BitConverter.GetBytes(sendBuffer.Length);
                    tcpStream.Write(byteCount, 0, byteCount.Length);
                    

                    // Send the actual data
                    Console.WriteLine("LOGGER: Sending logger's data..." + byteCount);

                    tcpStream.Write(sendBuffer, 0, sendBuffer.Length);
                }
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
            
        }
    }
    
}
