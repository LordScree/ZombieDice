
namespace LordScree.ZombieDice
{
    public class ZombieDicePlayer
    {
        public int CurrentBrains { get; set; }

        internal static ZombieDicePlayer GetPlayer()
        {
            return new ZombieDicePlayer();
        }
    }
}