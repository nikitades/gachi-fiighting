using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace GachiFighting.Matchmaking.Game
{
    public class PlayerRegistry
    {
        private ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();

        public void Register(string connectionId, Player player)
        {
            players.TryAdd(connectionId, player);
        }

        public void Remove(string connectionId)
        {
            Player player;
            players.TryRemove(connectionId, out player);
        }

        public Player Get(string connectionId)
        {
            Player player;
            if (!players.TryGetValue(connectionId, out player))
            {
                return null;
            }

            return player;
        }

        public int Total()
        {
            return players.Count;
        }
    }
}