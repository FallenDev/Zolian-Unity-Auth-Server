using System.Net.Sockets;
using Zolian.Networking.Abstractions;

namespace Zolian.Network.Client.Abstractions
{
    public interface IClientFactory<out T> where T : SocketClientBase
    {
        T CreateClient(Socket socket);
    }
}
