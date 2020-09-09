using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace Client
{
    class TCPClient
    {
        private static long messagesInterchanged = 1;
        private static TcpClient client = null;
        private static NetworkStream stream = null;
        public static void Connect(String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                String server = IPAddress.Loopback.ToString();
                client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                /*No more reading
                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
                */

                // Close everything.
                stream.Close();
                Console.WriteLine($"Messages Interchanged: {messagesInterchanged}");
                messagesInterchanged++;
                //client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Close();
                }
                if (client != null)
                {
                    client.Close();
                }
            }
            //Console.WriteLine("\n Press Enter to continue...");
            //Console.Read();
        }
        ~TCPClient()
        {
            stream.Close();
            client.Close();
        }
    }
}
