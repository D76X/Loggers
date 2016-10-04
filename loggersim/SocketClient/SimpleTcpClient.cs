﻿
using System;
using System.Net;
using System.Net.Sockets;

namespace SimpleTcpClient
{
    class SimpleTcpClient
    {
        static public void usage()
        {
            Console.WriteLine("usage: ClientSocket.exe [-n server] [-p port] [-x size]");
            Console.WriteLine("Available options:");
            Console.WriteLine("     -n server       Name or address of server to connect to");
            Console.WriteLine("     -p port         Port number to connect to server on");
            Console.WriteLine("     -x size         Size of send and receive buffers");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string myHost = System.Net.Dns.GetHostName();
            string serverAddress = Dns.GetHostEntry(myHost).AddressList[0].ToString();
            ushort serverPort = 13000;
            int bufferSize = 1024;

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
                            case 'n':       // String name or address of server to connect to
                                serverAddress = args[++i];
                                break;
                            case 'p':       // Port number for the destination
                                serverPort = System.Convert.ToUInt16(args[++i]);
                                break;
                            case 'x':       // Size of the send and receive buffers
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

            TcpClient simpleTcp = null;
            NetworkStream tcpStream = null;
            byte[] sendBuffer = new byte[bufferSize], receiveBuffer = new byte[bufferSize], byteCount;
            int bytesToRead = 0, nextReadCount, rc;

            // Initialize the send buffer
            Console.WriteLine("TCP client: Initializing the send buffer...");
            for (int i = 0; i < sendBuffer.Length; i++)
                sendBuffer[i] = (byte)'Z';

            try
            {
                // Create the client and indicate the server to connect to
                Console.WriteLine("TCP client: Creating the client and indicate the server to connect to...");
                simpleTcp = new TcpClient(serverAddress, (int)serverPort);

                // Retrieve the NetworkStream so we can do Read and Write
                Console.WriteLine("TCP client: Retrieving the NetworkStream so we can do Read and Write...");
                tcpStream = simpleTcp.GetStream();

                // First send the number of bytes the client is sending
                Console.WriteLine("TCP client: Sending the number of bytes the client is sending...");
                byteCount = BitConverter.GetBytes(sendBuffer.Length);
                tcpStream.Write(byteCount, 0, byteCount.Length);

                // Send the actual data
                Console.WriteLine("TCP client: Sending the actual data...");
                tcpStream.Write(sendBuffer, 0, sendBuffer.Length);
                tcpStream.Read(byteCount, 0, byteCount.Length);

                // Read how many bytes the server is responding with
                Console.WriteLine("TCP client: Reading how many bytes the server is responding with...");
                bytesToRead = BitConverter.ToInt32(byteCount, 0);

                // Receive the data
                Console.WriteLine("TCP client: Receiving, reading & displaying the data...");
                while (bytesToRead > 0)
                {
                    // Make sure we don't read beyond what the first message indicates
                    //    This is important if the client is sending multiple "messages" --
                    //    but in this sample it sends only one
                    if (bytesToRead < receiveBuffer.Length)
                        nextReadCount = bytesToRead;
                    else
                        nextReadCount = receiveBuffer.Length;

                    // Read the data
                    rc = tcpStream.Read(receiveBuffer, 0, nextReadCount);

                    // Display what was read
                    string readText = System.Text.Encoding.ASCII.GetString(receiveBuffer, 0, rc);
                    Console.WriteLine("TCP client: Received: {0}", readText);
                    bytesToRead -= rc;
                }
            }
            catch (SocketException err)
            {
                // Exceptions on the TcpListener are caught here
                Console.WriteLine("TCP client: Socket error occurred: {0}", err.Message);
            }
            catch (System.IO.IOException err)
            {
                // Exceptions on the NetworkStream are caught here
                Console.WriteLine("TCP client: I/O error: {0}", err.Message);
            }
            finally
            {
                // Close any remaining open resources
                Console.WriteLine("TCP client: Closing all the opening resources...");
                if (tcpStream != null)
                    tcpStream.Close();
                if (simpleTcp != null)
                    simpleTcp.Close();
            }
        }

    }
    }

