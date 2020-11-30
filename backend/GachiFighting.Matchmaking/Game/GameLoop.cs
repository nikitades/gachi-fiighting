using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GachiFighting.Matchmaking.Game
{
    public class GameLoop
    {
        private const int delay = 1000 / 60 - 2;

        public bool Running { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public GameLoop(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void Stop()
        {
            this.Running = false;
        }

        public async Task Start()
        {
            Running = true;
            var elapsedTicks = (double)Environment.TickCount;
            var lastSeconds = new SortedDictionary<int, int>();
            while (Running)
            {
                if (Environment.TickCount >= elapsedTicks)
                {
                    Player1.Caller.SendAsync("Send", $"New game state! {DateTime.Now.ToString()}");
                    Player2.Caller.SendAsync("Send", $"New game state! {DateTime.Now.ToString()}");
                    var ticksLag = Environment.TickCount - elapsedTicks;
                    elapsedTicks = Environment.TickCount + 16.65 - ticksLag;
                }
            }
        }
    }
}