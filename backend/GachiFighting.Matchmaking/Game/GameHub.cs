using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace GachiFighting.Matchmaking
{
    public class GameHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            PlayerRegistry.Add(getPlayerId());
            if (PlayerRegistry.Total() == 2)
            {
                GameLoop.Loop(Clients);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            PlayerRegistry.Remove(getPlayerId());
            return base.OnDisconnectedAsync(exception);
        }

        private string getPlayerId()
        {
            return Context.GetHttpContext().Request.Query["user"];
        }
    }
}