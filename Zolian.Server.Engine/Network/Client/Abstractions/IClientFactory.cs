using System.Net.Sockets;
using Zolian.Networking.Abstractions;

namespace Darkages.Network.Client.Abstractions
{
    public interface IClientFactory<out T> where T : SocketClientBase
    {
        T CreateClient(Socket socket);
    }
}
