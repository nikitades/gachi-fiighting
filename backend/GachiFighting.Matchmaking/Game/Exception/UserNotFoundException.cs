namespace GachiFighting
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException(string err) : base(err) { }
    }
}