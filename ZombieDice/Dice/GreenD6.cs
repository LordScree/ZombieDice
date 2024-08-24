namespace ZombieDice.Dice;
public class GreenD6 : IZombieDie
{
    public ZombieDieType GetZombieDieType()
    {
        return ZombieDieType.Green;
    }
    public ZombieDieFace[] GetFaces()
    {
        return [ZombieDieFace.Brain, ZombieDieFace.Brain, ZombieDieFace.Brain, ZombieDieFace.Runner, ZombieDieFace.Runner, ZombieDieFace.Shotgun];
    }

    public ZombieDieFace LastFace { get; set; }
}