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
        private const double delay = 16.66666667;

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
            double elapsedTicks = Environment.TickCount;
            while (Running)
            {
                if (Environment.TickCount < elapsedTicks) continue;
                Player1.Caller.SendAsync("Send", $"New game state! {DateTime.Now.ToString()}");
                Player2.Caller.SendAsync("Send", $"New game state! {DateTime.Now.ToString()}");
                Thread.Sleep(getTimeToSleep(elapsedTicks)); //leave 1msec to catch exactly 60fps
                elapsedTicks += GameLoop.delay;
            }
        }

        private int getTimeToSleep(double elapsedTicks)
        {
            var operationTime = Environment.TickCount - elapsedTicks;
            var timeToSleep = GameLoop.delay - operationTime + getElapsedTimeModulo(elapsedTicks);
            if (timeToSleep > 0)
            {
                return (int)Math.Round(timeToSleep);
            }

            return 0;
        }

        private double getElapsedTimeModulo(double elapsedTime)
        {
            return Math.Round(elapsedTime) % 1;
        }
    }
}