using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace GachiFighting.Matchmaking.Game
{
    public class Player
    {
        public string ConnectionId { get; }
        public IClientProxy Caller { get; }
        public string Name { get; }
        public ConcurrentBag<string> Input { get; } = new ConcurrentBag<string>();

        public Player(string playerName, string connectionId, IClientProxy caller)
        {
            Name = playerName;
            ConnectionId = connectionId;
            Caller = caller;
        }

        public string[] GetInputClearing()
        {
            var i = Input.ToArray();
            Input.Clear();
            return i;
        }
    }
}