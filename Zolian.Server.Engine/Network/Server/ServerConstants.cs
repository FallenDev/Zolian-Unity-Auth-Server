using Zolian.Network.Server.Abstractions;

namespace Zolian.Network.Server;

public class ServerConstants : IServerConstants
{
    public int ConnectionCapacity { get; init; }
    public double PingInterval { get; init; }
    public int LOGIN_PORT { get; init; }
    public int LOBBY_PORT { get; init; }
    public string SERVER_TITLE { get; init; }
    public string[] DevModeExemptions { get; init; }
    public string ClientVersion { get; init; }
}