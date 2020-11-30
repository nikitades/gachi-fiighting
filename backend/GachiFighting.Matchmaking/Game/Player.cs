using Microsoft.AspNetCore.SignalR;

namespace GachiFighting.Matchmaking.Game
{
    public class Player
    {
        public string ConnectionId { get; }
        public IClientProxy Caller { get; }
        public string Name { get; }

        public Player(string playerName, string connectionId, IClientProxy caller)
        {
            Name = playerName;
            ConnectionId = connectionId;
            Caller = caller;
        }
    }
}