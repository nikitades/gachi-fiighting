using System.Collections.Generic;

namespace GachiFighting.Matchmaking.Game
{
    public class GameRegistry
    {
        private Dictionary<int, Game> games = new Dictionary<int, Game>();
        private Game tempGame = new Game();

        public Game New()
        {
            var game = new Game();
            games.Add(game.GameId, game);

            return game;
        }

        public Game GetTempGame()
        {
            return tempGame;
        }
    }
}