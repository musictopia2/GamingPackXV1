using System.Net.Sockets;
namespace BasicGameFrameworkLibrary.NetworkingClasses.SocketClasses
{
    internal class ClientInfo
    {
        public TcpClient? Socket { get; set; }
        public NetworkStream? ThisStream { get; set; }
    }
}