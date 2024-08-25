namespace LordScree.ZombieDice.Dice
{
    public class PlayedDie
    {
        public PlayedDie(ZombieDieType type, ZombieDieFace face)
        {
            Type = type;
            Face = face;
        }

        public ZombieDieType Type { get; set; }
        public ZombieDieFace Face { get; set; }

    }
}
