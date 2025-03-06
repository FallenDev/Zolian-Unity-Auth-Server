using Microsoft.Extensions.Logging;

namespace Zolian.Network.Server.Abstractions;

public interface IServerContext
{
    void InitFromConfig(string storagePath, string serverIp);
    void Start(IServerConstants config, ILogger<ServerSetup> logger);
}