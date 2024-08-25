using LordScree.ZombieDice;

namespace LordScree.ZombieDice
{
    internal class GameState
    {
        public int TargetBrains { get; set; } = 13;
        public List<ZombieDicePlayer> Players { get; set; } = new List<ZombieDicePlayer>();

        public GameState(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(ZombieDicePlayer.GetPlayer());
            }
        }
    }
}
