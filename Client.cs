using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace tcp_client
{
    class Client
    {
        /*
Create a new TcpClient object, which takes a server name and a port (no IPEndPoint necessary, nice).

Pull a NetworkStream out of the TcpClient by calling GetStream()

Convert your message into bytes using Encoding.ASCII.GetBytes(string)

Now you can send and receive data using the stream.Write and stream.Read methods, respectively. 
        The stream.Read method returns the number of bytes written to your receiving array, by the way.

Put the data back into human-readable format using Encoding.ASCII.GetString(byte array).

Clean up your mess before the network admins get mad by calling stream.Close() and client.Close().
        */
        public static void Main(String[] args)
        {
            TcpClient tcpClient = new TcpClient();

            // Resolves an IP or hostname to an IPHostEntry instance
            IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0]; // Gets the first address associated with that host
            // Creates a new instance of the IPEndPoint class with the IP resolved earlier with the port 11004
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 9999);
            // Connects the TCPClient
            tcpClient.Connect(ipEndPoint);

            NetworkStream ns = tcpClient.GetStream();

            while (true)
            {
                try
                {
                    Console.Write("> ");
                    string cmd = Console.ReadLine();

                    switch (cmd)
                    {
                        case "send":
                            send_message(ns);
                            break;

                        case "close":
                            close_connection(ns, tcpClient);
                            break;

                        case "-h":
                            short_help();
                            break;

                        case "-H":
                            long_help();
                            break;

                        default:
                            Console.WriteLine("Invalid syntax\nUse -h/H");
                            break;
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine("There as an error sending the message to the remote server" + Convert.ToString(e.Message));
                }
            }
            

            /*else
            {
                Console.WriteLine("Data cannot be written to this stream");
                tcpClient.Close();
                ns.Close();
            }*/



            byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
            ns.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

            string returnData = Encoding.Default.GetString(bytes);

            Console.WriteLine($"Response: {returnData}");


            /*else
            {
                Console.WriteLine("You cannot read from this stream");

                tcpClient.Close();

                ns.Close();
            }*/
        }

        private static void short_help()
        {
            Console.WriteLine(@"
TODO
");
        }

        private static void long_help()
        {
            Console.WriteLine(@"
TODO
");
        }

        public static void close_connection(NetworkStream ns, TcpClient tcpClient)
        {
            ns.Close();
            tcpClient.Close();
        }

        public static void send_message(NetworkStream ns)
        {
            if (ns.CanWrite)
            {
                // Don't ask me why I wrote the method like that, I don't know either
                //string mac_addr = Mac_addr_grab.mac_addr_find();

                Console.WriteLine("Enter a message:");
                string to_send = Console.ReadLine();
                Byte[] sendBytes = Encoding.Default.GetBytes(to_send);
                ns.Write(sendBytes, 0, sendBytes.Length);
            }
        }
    }
}