using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GachiFighting
{
    public static class GameLoop
    {
        public static async Task Loop(IHubCallerClients _clients)
        {
            while (PlayerRegistry.Total() == 2)
            {
                _clients.All.SendAsync("Send", $"There are currently {PlayerRegistry.Total()} players! ");
                await Task.Delay(1000 / 60);
            }
        }
    }
}