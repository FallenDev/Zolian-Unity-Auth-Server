namespace Zolian.Network.Server.Abstractions;

public interface IServerConstants
{
    int ConnectionCapacity { get; }
    double PingInterval { get; }
    int LOGIN_PORT { get; }
    int LOBBY_PORT { get; }
    string SERVER_TITLE { get; }
    string[] DevModeExemptions { get; }
    string ClientVersion { get; }
}