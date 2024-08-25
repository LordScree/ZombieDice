namespace LordScree.ZombieDice.Dice;
public class RedD6 : IZombieDie
{
    public ZombieDieType GetZombieDieType()
    {
        return ZombieDieType.Red;
    }
    public ZombieDieFace[] GetFaces()
    {
        return [ZombieDieFace.Brain, ZombieDieFace.Runner, ZombieDieFace.Runner, ZombieDieFace.Shotgun, ZombieDieFace.Shotgun, ZombieDieFace.Shotgun];
    }

    public ZombieDieFace LastFace { get; set; }
}