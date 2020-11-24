using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace GachiFighting.Matchmaking
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }

        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("Send", "zhopa");
        }
    }
}