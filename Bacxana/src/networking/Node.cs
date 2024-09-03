using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Bacxana.Blocksystem;

namespace Bacxana.Networking
{
    public class Node
    {
        public string NodeId { get; private set; }
        public Ledger Blockchain { get; private set; }
        public List<Node> Peers { get; private set; }

        private TcpListener? _listener;

        public Node(string nodeId, Ledger blockchain)
        {
            NodeId = nodeId;
            Blockchain = blockchain;
            Peers = new List<Node>();
            _listener = null;
        }

        public void Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Console.WriteLine($"Node {NodeId} started on port {port}");

            Task.Run(() => AcceptClientsAsync());
        }

        private async Task AcceptClientsAsync()
        {
            while (true)
            {
                var client = await _listener!.AcceptTcpClientAsync();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            Task.Run(async () =>
            {
                using (var stream = client.GetStream())
                {
                    var buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        ProcessMessage(message);
                    }
                }
            });
        }

        private void ProcessMessage(string message)
        {
            Console.WriteLine($"Node {NodeId} received message: {message}");
        }

        public void ConnectToPeer(Node peer)
        {
            if (!Peers.Contains(peer))
            {
                Peers.Add(peer);
                Console.WriteLine($"Node {NodeId} connected to peer {peer.NodeId}");
            }
        }

        public void Broadcast(string message)
        {
            foreach (var peer in Peers)
            {
                SendMessage(peer, message);
            }
        }

        private void SendMessage(Node peer, string message)
        {
            Task.Run(() =>
            {
                try
                {
                    var client = new TcpClient(peer.NodeId, 9000);
                    using (var stream = client.GetStream())
                    {
                        var data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send message to {peer.NodeId}: {ex.Message}");
                }
            });
        }
    }
}
