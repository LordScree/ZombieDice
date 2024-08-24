namespace ZombieDice.Dice;
public class ZombieD6Red : IZombieDie
{
    public ZombieDieType GetZombieDieType()
    {
        return ZombieDieType.Red;
    }
    public ZombieDieFace[] GetFaces()
    {
        return [ZombieDieFace.Brain, ZombieDieFace.Runner, ZombieDieFace.Runner, ZombieDieFace.Shotgun, ZombieDieFace.Shotgun, ZombieDieFace.Shotgun];
    }
}