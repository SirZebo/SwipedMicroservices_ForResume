using BiddingNotification.API.Models;
using System.Collections.Concurrent;

namespace BiddingNotification.API.Data;

public class ConnectionDb
{
    private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
    public ConcurrentDictionary<string, UserConnection> connections => _connections;
}
