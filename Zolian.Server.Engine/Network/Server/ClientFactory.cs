using Darkages.Network.Client.Abstractions;
using Microsoft.Extensions.DependencyInjection;

using System.Net.Sockets;
using Zolian.Networking.Abstractions;

namespace Darkages.Network.Server
{
    public class ClientFactory<T>(IServiceProvider service) : IClientFactory<T>
        where T : SocketClientBase
    {
        public T CreateClient(Socket socket)
        {
            return ActivatorUtilities.CreateInstance<T>(service, socket);
        }
    }
}
