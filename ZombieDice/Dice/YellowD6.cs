namespace ZombieDice.Dice;
public class YellowD6 : IZombieDie
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