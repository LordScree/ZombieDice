namespace ZombieDice.Dice;
public class ZombieD6Yellow : IZombieDie
{
    public ZombieDieType GetZombieDieType()
    {
        return ZombieDieType.Yellow;
    }
    public ZombieDieFace[] GetFaces()
    {
        return [ZombieDieFace.Brain, ZombieDieFace.Brain, ZombieDieFace.Runner, ZombieDieFace.Runner, ZombieDieFace.Shotgun, ZombieDieFace.Shotgun];
    }
}