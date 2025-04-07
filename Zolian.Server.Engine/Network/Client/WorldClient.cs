using System.Diagnostics;
using System.Net.Sockets;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using IWorldClient = Zolian.Network.Client.Abstractions.IWorldClient;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;
using Zolian.Packets;
using Zolian.Networking.Entities.Server;
using Zolian.Sprites.Entities;

namespace Zolian.Network.Client;

[UsedImplicitly]
public class WorldClient([NotNull] IWorldServer<IWorldClient> server, [NotNull] Socket socket,
    [NotNull] IPacketSerializer packetSerializer,
    [NotNull] ILogger<WorldClient> logger)
    : WorldClientBase(socket, packetSerializer, logger), IWorldClient
{
    protected override ValueTask HandlePacketAsync(Span<byte> span)
    {
        try
        {
            // Fully parse the Packet from the span
            var packet = new Packet(ref span);

            if (packet.Payload.Length == 0)
            {
                Logger.LogWarning("Received packet with empty payload. OpCode={OpCode}", packet.OpCode);
            }

            // Pass the packet to the server for further handling
            return server.HandlePacketAsync(this, in packet);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error parsing packet from span: {RawBuffer}", BitConverter.ToString(span.ToArray()));
            return default;
        }
    }

    public Stopwatch Latency { get; set; }

    public void SendLoginMessage(PopupMessageType loginMessageType, string message = null)
    {
        var args = new LoginMessageArgs
        {
            LoginMessageType = loginMessageType,
            Message = message
        };

        Send(args);
    }

    public void SendCharacterData(Player player, UpdateType type)
    {
        var args = new CharacterDataArgs
        {
            Type = type,
            Serial = player.Serial,
            Disabled = player.Disabled,
            UserName = player.UserName,
            Stage = player.Stage.ToString(),
            Job = player.Job.ToString(),
            FirstClass = player.FirstClass.ToString(),
            SecondClass = player.SecondClass.ToString(),
            EntityLevel = player.EntityLevel,
            JobLevel = player.JobLevel,
            GameMaster = player.GameMaster,
            Position = player.Position,
            CameraYaw = player.CameraYaw,
            CurrentHealth = player.CurrentHp,
            MaxHealth = player.CalculatedMaxHp,
            CurrentMana = player.CurrentMp,
            MaxMana = player.CalculatedMaxMp,
            CurrentStamina = player.CurrentStamina,
            MaxStamina = player.CalculatedMaxStamina,
            CurrentRage = player.CurrentRage,
            MaxRage = player.CalculatedMaxRage,
            Regen = player.Regen,
            Dmg = player.Dmg,
            Reflex = player.Reflex,
            Fortitude = player.Fortitude,
            Will = player.Will,
            ArmorClass = player.ArmorClass,
            OffenseElement = player.OffenseElement.ToString(),
            DefenseElement = player.DefenseElement.ToString(),
            SecondaryOffenseElement = player.SecondaryOffensiveElement.ToString(),
            SecondaryDefenseElement = player.SecondaryDefensiveElement.ToString(),
            Str = player.Str,
            Int = player.Int,
            Wis = player.Wis,
            Con = player.Con,
            Dex = player.Dex,
            Luck = player.Luck,
            Race = player.Race,
            Sex = player.Gender,
            Hair = player.Hair,
            HairColor = player.HairColor,
            HairHighlightColor = player.HairHighlightColor,
            SkinColor = player.SkinColor,
            EyeColor = player.EyeColor,
            Beard = player.Beard,
            Mustache = player.Mustache,
            Bangs = player.Bangs
        };

        Send(args);
    }

    public void SendConfirmExit()
    {
        throw new NotImplementedException();
    }

    public void SendServerMessage(PopupMessageType serverMessageType, string message)
    {
        throw new NotImplementedException();
    }

    public void SendSound(byte sound, bool isMusic)
    {
        throw new NotImplementedException();
    }

    public WorldClient SystemMessage(string message)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save()
    {
        throw new NotImplementedException();
    }

    public WorldClient LoggedIn(bool state)
    {
        throw new NotImplementedException();
    }
}