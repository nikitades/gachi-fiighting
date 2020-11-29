using System.Collections.Generic;

namespace GachiFighting
{
    public static class PlayerRegistry
    {
        private static List<string> connectedUsers = new List<string>();

        public static void Add(string playerName)
        {
            connectedUsers.Add(playerName);
        }

        public static void Remove(string playerName)
        {
            connectedUsers.Remove(playerName);
        }

        public static int Total()
        {
            return connectedUsers.Count;
        }
    }
}