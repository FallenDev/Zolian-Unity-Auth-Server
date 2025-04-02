using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using System.Net.Sockets;

using Zolian.Enums;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets;
using Zolian.Packets.Abstractions;
using Zolian.Sprites.Entities;

using ILoginClient = Zolian.Network.Client.Abstractions.ILoginClient;

namespace Zolian.Network.Client;

[UsedImplicitly]
public class LoginClient([NotNull] ILoginServer<ILoginClient> server, [NotNull] Socket socket,
        [NotNull] IPacketSerializer packetSerializer,
        [NotNull] ILogger<LoginClient> logger)
    : LoginClientBase(socket, packetSerializer, logger), ILoginClient
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

    public void SendConnectionInfo(ushort port)
    {
        var args = new ConnectionInfoArgs
        {
            PortNumber = port
        };

        Send(args);
    }
    
    public void SendLoginMessage(PopupMessageType loginMessageType, string message = null)
    {
        var args = new LoginMessageArgs
        {
            LoginMessageType = loginMessageType,
            Message = message
        };

        Send(args);
    }

    public void SendAccountData(List<Player> players)
    {
        
        var args = new AccountListArgs
        {
            Players = []
        };

        foreach (var player in players)
        {
            args.Players.Add(new AccountListArgs.PlayerSelection
            {
                Serial = player.Serial,
                Disabled = player.Disabled,
                Name = player.UserName,
                Level = player.EntityLevel + player.JobLevel,
                BaseClass = ClassStrings.BaseClassValue(player.FirstClass),
                AdvClass = ClassStrings.BaseClassValue(player.SecondClass),
                Job = ClassStrings.JobValue(player.Job),
                Health = player.BaseHp,
                Mana = player.BaseMp,
                Race = player.Race,
                Sex = player.Gender,
                Hair = player.Hair,
                HairColor = player.HairColor,
                HairHighlightColor = player.HairHighlightColor,
                SkinColor = player.SkinColor,
                EyeColor = player.EyeColor,
                Beard = player.Beard,
                Mustache = player.Mustache,
                Bangs = player.Bangs,

            });
        }

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

    public void SendCharacterFinalized()
    {
        var args = new CharacterFinalizedArgs();
        Send(args);
    }
}