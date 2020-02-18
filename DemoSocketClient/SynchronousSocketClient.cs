using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DemoSocketClient
{
    public static class SynchronousSocketClient
    {
        public static void StartClient()
        {
            try
            {
                // Establish the remote endpoint for the socket.  
                // IP Address is TMM Service IP
                var ipAddress = IPAddress.Parse("192.168.132.181");
                var remoteEP = new IPEndPoint(ipAddress, 5434);

                // Create a TCP/IP  socket.  
                var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    // Send the data through the socket.  
                    var content = "測試資料";
                    var command = $"H1\t{content}\tT1\n";
                    int bytesSent = sender.Send(Encoding.UTF8.GetBytes(command));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}