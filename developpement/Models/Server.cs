using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Models
{
    public static class Server
    {
        private static Socket serverSocket;
        private static Socket clientSocket;
        private static readonly IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
        public static bool isSomeoneConnected = false;

        private static void StartServer()
        {
            serverSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, 11000));
            serverSocket.Listen(10);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            clientSocket = serverSocket.EndAccept(ar);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            isSomeoneConnected = true;
        }

        public static void Send(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            clientSocket.Send(buffer);
        }

        public static void SendObject(object obj)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(obj.ToString());
            clientSocket.Send(buffer);
        }

        public static void Receive()
        {
            byte[] buffer = new byte[1024];
            int received = clientSocket.Receive(buffer);
            byte[] data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
        }

        public static void Close()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            serverSocket.Close();
        }

        public static void Start()
        {
            StartServer();
        }
    }
}
