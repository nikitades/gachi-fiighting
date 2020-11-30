namespace GachiFighting.Matchmaking.Game.Exception
{
    public class CantAddMorePlayersException : System.Exception
    {
        public CantAddMorePlayersException(string err) : base(err) {}
    }
}