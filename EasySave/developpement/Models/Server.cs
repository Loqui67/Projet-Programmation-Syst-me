using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EasySave.developpement.Models;

namespace EasySave.developpement.Models
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
        }

        public static void AcceptConnection()
        {
            serverSocket.Listen(10);
            clientSocket = serverSocket.Accept();
            isSomeoneConnected = true;
        }

        public static void Send(string? state, string? fileNumber, float? progress)
        {
            if (clientSocket == null || !isSomeoneConnected) return;
            ObjectReceived data = new()
            {
                State = state,
                FileNumber = fileNumber,
                Progress = progress
            };
            
            string json = JsonSerializer.Serialize(data);
            byte[] buffer = Encoding.ASCII.GetBytes(json);
            clientSocket.Send(buffer);
        }

        public static string Receive()
        {
            byte[] buffer = new byte[1024];
            int received = clientSocket.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, received);
        }

        public static void Close()
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            } catch (Exception) { }
            isSomeoneConnected = false;
        }

        public static void Start()
        {
            StartServer();
        }
    }
}
