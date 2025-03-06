using System.Net;
using Zolian.Networking.Abstractions;

namespace Zolian.Networking.Options;

/// <summary>
///     Represents the information needed to connect to a server
/// </summary>
public record ConnectionInfo : IConnectionInfo
{
    /// <inheritdoc />
    public IPAddress Address { get; set; } = null!;

    /// <inheritdoc />
    public virtual string HostName { get; set; } = null!;

    /// <inheritdoc />
    public int Port { get; set; }
}