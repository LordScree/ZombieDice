namespace ZombieDice.Dice;
public class ZombieD6Green : IZombieDie
{
    public ZombieDieType GetZombieDieType()
    {
        return ZombieDieType.Green;
    }
    public ZombieDieFace[] GetFaces()
    {
        return [ZombieDieFace.Brain, ZombieDieFace.Brain, ZombieDieFace.Brain, ZombieDieFace.Runner, ZombieDieFace.Runner, ZombieDieFace.Shotgun];
    }
}