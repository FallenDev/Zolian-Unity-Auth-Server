using System.Diagnostics;

using Zolian.Common;
using Zolian.Network.Server;
using Zolian.Networking.Abstractions.Definitions;

namespace Zolian.Network.Components;

public class EntityUpdateComponent(LoginServer server) : GameServerComponent(server)
{
    private const long GameSpeed = 30; // ms per tick
    private const float ViewRange = 180f;

    // Maps player serial to a set of currently visible entity serials
    private readonly Dictionary<Guid, HashSet<Guid>> VisibleEntities = [];

    protected internal override async Task Update()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var interval = GameSpeed;

        while (ServerSetup.Instance.Running)
        {
            if (stopwatch.Elapsed.TotalMilliseconds < interval)
            {
                await Task.Delay(10);
                continue;
            }

            UpdateAllPlayerVisibilityAndPosition();

            var delay = GameSpeed - stopwatch.ElapsedMilliseconds;
            interval = delay < 0 ? GameSpeed + delay : GameSpeed;

            await Task.Delay(Math.Max(0, (int)delay));
            stopwatch.Restart();
        }
    }

    private void UpdateAllPlayerVisibilityAndPosition()
    {
        //foreach (var playerKvp in Server.ActivePlayers)
        //{
        //    var player = playerKvp.Value;
        //    if (player?.Client == null)
        //    {
        //        Server.ActivePlayers.TryRemove(playerKvp.Key, out _);
        //        continue;
        //    }

        //    if (!VisibleEntities.TryGetValue(player.Serial, out var known))
        //        known = VisibleEntities[player.Serial] = [];

        //    var currentPosition = player.MovementState.Position;
        //    var nowVisible = new HashSet<Guid>();

        //    foreach (var entityKvp in Server.ActivePlayers)
        //    {
        //        if (entityKvp.Key == player.Serial) continue;
        //        var other = entityKvp.Value;
        //        if (other == null || other.Serial == player.Serial)
        //            continue;

        //        if (currentPosition.IsInRangeXZ(other.MovementState.Position, ViewRange))
        //        {
        //            nowVisible.Add(other.Serial);

        //            if (!known.Contains(other.Serial))
        //            {
        //                // Newly in view
        //                player.Client.SendEntityPlayerSpawn(other);
        //            }
        //            else
        //            {
        //                // Already known, send update
        //                player.Client.SendPlayerPositionUpdate(other);
        //            }
        //        }
        //    }

        //    // Removed entities
        //    foreach (var removed in known.Except(nowVisible))
        //    {
        //        player.Client.SendEntityDespawn(removed);
        //    }

        //    // Update known list
        //    VisibleEntities[player.Serial] = nowVisible;
        //}
    }
}
