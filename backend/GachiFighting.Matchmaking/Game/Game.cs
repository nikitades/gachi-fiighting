using System;
using System.Threading.Tasks;
using GachiFighting.Matchmaking.Game.Exception;

namespace GachiFighting.Matchmaking.Game
{
    public class Game
    {
        public int GameId { get; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        private GameLoop gameLoop;

        public Game()
        {
            GameId = (new Random()).Next(100000000, 999999999);
        }

        public void AddPlayer(Player player)
        {
            var playerWasSet = false;
            if (Player1 == null)
            {
                Player1 = player;
                playerWasSet = true;
            }

            if (!playerWasSet && Player2 == null && Player1 != null)
            {
                Player2 = player;
                playerWasSet = true;
            }

            if (!playerWasSet && Player1 != null && Player2 != null)
            {
                throw new CantAddMorePlayersException("All slots are taken.");
            }

            if (Player1 != null && Player2 != null)
            {
                Run();
            }
        }

        public void RemovePlayer(string connectionId)
        {
            if (Player1 != null && Player1.ConnectionId == connectionId)
            {
                Player1 = null;
                gameLoop?.Stop();
                return;
            }

            if (Player2 != null && Player2.ConnectionId == connectionId)
            {
                Player2 = null;
                gameLoop?.Stop();
                return;
            }
        }

        private void Run()
        {
            gameLoop = new GameLoop(Player1, Player2);
            Task.Run(async () => await gameLoop.Start());
        }
    }
}