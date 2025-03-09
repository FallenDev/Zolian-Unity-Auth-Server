using Zolian.Network.Client;

namespace Zolian.Sprites.Abstractions;

public interface IPlayer
{
    WorldClient Client { get; set; }
}