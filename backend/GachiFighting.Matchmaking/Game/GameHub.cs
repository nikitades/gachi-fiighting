using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace GachiFighting.Matchmaking.Game
{
    public class GameHub : Hub
    {
        public GameRegistry GameRegistry { get; set; }
        public PlayerRegistry PlayerRegistry { get; set; }

        public GameHub(GameRegistry gameRegistry, PlayerRegistry playerRegistry) : base()
        {
            GameRegistry = gameRegistry;
            PlayerRegistry = playerRegistry;
        }

        public override async Task OnConnectedAsync()
        {
            var player = new Player(getPlayerNameFromQuery(), Context.ConnectionId, Clients.Caller);
            PlayerRegistry.Register(Context.ConnectionId, player);
            GameRegistry.GetTempGame().AddPlayer(player);
            Console.WriteLine($"Player connected: {player.Name}");
        }

        public override Task OnDisconnectedAsync(System.Exception? exception)
        {
            var playerToRemove = PlayerRegistry.Get(Context.ConnectionId);
            PlayerRegistry.Remove(Context.ConnectionId);
            GameRegistry.GetTempGame().RemovePlayer(Context.ConnectionId);
            Console.WriteLine($"Player removed: {playerToRemove.Name}");
            return base.OnDisconnectedAsync(exception);
        }

        private string getPlayerNameFromQuery()
        {
            return Context.GetHttpContext().Request.Query["playerName"];
        }
    }
}