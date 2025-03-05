using System;
using Darkages.Network.Server;
using Darkages.Network.Server.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServerOptions = Darkages.Models.ServerOptions;

namespace Zolian;

public interface IServer;

public class Server : IServer
{
    public Server(ILogger<ServerSetup> logger, IServerContext context, IServerConstants configConstants, IOptions<ServerOptions> serverOptions)
    {
        if (serverOptions.Value.Location == null) return;
        context.InitFromConfig(serverOptions.Value.Location, serverOptions.Value.ServerIp);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{configConstants.SERVER_TITLE} - IP: {serverOptions.Value.ServerIp} Server Start: {DateTime.Now}\n\n");
        context.Start(configConstants, logger);
    }
}