namespace ZombieDice;
public class ZombieD6Red : IZombieD6
{
    public DieType GetDieType()
    {
        return DieType.Red;
    }
    public DieFace[] GetFaces()
    {
        return [DieFace.Brain, DieFace.Runner, DieFace.Runner, DieFace.Shotgun, DieFace.Shotgun, DieFace.Shotgun];
    }
}