using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Net.Sockets;
using System.Net;

// Used to discover clients on local network
namespace EasyFileTransferApp.Model
{
    class NetworkDiscovery
    {
        private int BroadcastPort = 8080; // Port used for broadcasting messages
        private UdpClient udpClient; // UDP client used to send and receive messages
        private bool isRunning; // Flag to indicate if the listener is running

        // Event that is triggered when a client is discovered
        public event Action<string, string> ClientDiscovered = delegate { };


        // Constructor
        public NetworkDiscovery()
        {
            udpClient = new UdpClient();
            udpClient.EnableBroadcast = true;
        }


        // Broadcast a message to all clients on the network
        public async Task StartBroadcastingAsync(string username)
        {
            while (true)
            {
                string message = $"DISCOVER:{username}";
                byte[] data = Encoding.UTF8.GetBytes(message); // Convert message to byte array
                await udpClient.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Broadcast, BroadcastPort)); // Broadcast message to all clients on the network
                await Task.Delay(5000);
            }
        }

        // Listen for incoming messages from clients
        public async Task StartListeningAsync()
        {
            isRunning = true;
            UdpClient listener = new UdpClient(BroadcastPort); // Create a UDP client to listen for incoming messages
            while (isRunning)
            {
                UdpReceiveResult result = await listener.ReceiveAsync(); // Receive incoming message
                string message = Encoding.UTF8.GetString(result.Buffer); // Convert message to string
                if (message.StartsWith("DISCOVER:"))
                {
                    string username = message.Substring(9);
                    ClientDiscovered(username, result.RemoteEndPoint.Address.ToString()); // Trigger event to notify that a client has been discovered
                }
            }
        }


        // Stop listening for incoming messages
        public void StopListening()
        {
            isRunning = false;
            udpClient.Close();
        }
    }
}
