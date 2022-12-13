using IHM_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace IHM_Client.Models
{
    public class ServerCommunication
    {
        private static Socket socket;
        private static IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];

        public static bool isConnect = false;

        public static Socket Connect()
        {
            if (isConnect) return socket;
            socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(ip, 11000));
            if (socket.Connected) isConnect = true;
            return socket;
        }

        public static void Send(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            socket.Send(buffer);
        }

        public static ObjectReceived? Receive()
        {
            if (!isConnect) return null;
            byte[] buffer = new byte[1024];
            int received = 0;
            try
            {
                received = socket.Receive(buffer);
            } catch (Exception) 
            { 
                Close();
                MessageBox.Show("Une erreur de connexion est survenue");
            }
            
            string a = Encoding.ASCII.GetString(buffer, 0, received);
            ObjectReceived? obj = null;
            try { obj = JsonSerializer.Deserialize<ObjectReceived>(a); }
            catch(Exception) {}
            return obj;
        }

        public static void Close()
        {
            isConnect = false;
            MainViewModel._isConnected = false;
            try
            {
                if (socket == null) return;
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception) { }
        }
    }
}
