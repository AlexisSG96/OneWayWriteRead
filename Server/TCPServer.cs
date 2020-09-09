using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class TCPServer
    {
        private static readonly string[] seperator = { "," };
        private static TcpListener server = null;
        private static TcpClient client = null;
        public static void Connect()
        {
            server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(IPAddress.Loopback, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        Decode(data);
                        // Process the data sent by the client.

                        // Dont write anything back
                        //data = data.ToUpper();
                        //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        //// Send back a response.
                        //stream.Write(msg, 0, msg.Length);
                        //Console.WriteLine("Sent: {0}", data);
                    }
                    //stream.Close();//Maybe?
                    // Shutdown and end connection
                    //client.Close();
                }
            }
            catch (Exception ex)
            {
                LogExceptions(ex.ToString());
            }
            //finally
            //{
            //    // stop listening for new clients.
            //    //server.Stop();
            //}

            //Console.WriteLine("\nHit enter to continue...");
            //Console.Read();
        }
        private static void Decode(string text)
        {
            string[] decodeArray;
            decodeArray = text.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            switch (decodeArray.Length)
            {
                case 2:
                    AppendToFile(decodeArray[0], $"{decodeArray[0]}: {decodeArray[1]}");
                    break;
                case 3:
                    AppendToFile(decodeArray[0], decodeArray[1], $"{decodeArray[1]}: {decodeArray[2]}");
                    break;
                default:
                    if (decodeArray.Length <= 1)
                    {
                        Console.WriteLine("Not enough arguments.");
                    }
                    else if (decodeArray.Length >= 4)
                    {
                        Console.WriteLine("To many arguments.");
                    }
                    break;
            }
        }
        private static void LogExceptions(string ex)
        {
            {
                const string datePatt = @"yyyy mm dd H:mm:ss";
                DateTime dateTimeUTC = DateTime.UtcNow;
                string dtString = "[" + dateTimeUTC.ToString(datePatt) + " UTC] " + ex;
                using (StreamWriter stream = File.AppendText("Exceptions.txt"))
                {
                    stream.WriteLine($"{dtString}");
                }
            }
        }
        private static void AppendToFile(string fileName, string attribute, string message)
        {
            const string datePatt = @"yyyy mm dd H:mm:ss";
            DateTime dateTimeUTC = DateTime.UtcNow;
            string dtString = "[" + dateTimeUTC.ToString(datePatt) + " UTC] " + message;
            using (StreamWriter stream = File.AppendText(fileName + ".txt"))
            {
                stream.WriteLine($"{dtString}");
            }
        }
        private static void AppendToFile(string attribute, string message)
        {
            const string datePatt = @"yyyy mm dd H:mm:ss";
            DateTime dateTimeUTC = DateTime.UtcNow;
            string dtString = "[" + dateTimeUTC.ToString(datePatt) + " UTC] " + message;
            using (StreamWriter stream = File.AppendText(attribute + ".txt")) 
            {
                stream.WriteLine($"{dtString}");
            }
        }
        ~TCPServer()
        {
            try
            {
                client.Close();
                server.Stop();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
