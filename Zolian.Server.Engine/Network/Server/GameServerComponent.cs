namespace Zolian.Network.Server;

public abstract class GameServerComponent
{
    protected GameServerComponent(LoginServer server) => Server = ServerSetup.Instance.LoginServer;
    protected static LoginServer Server { get; private set; }
    protected internal abstract Task Update();
}
