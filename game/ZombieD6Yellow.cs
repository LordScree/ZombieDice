namespace ZombieDice;
public class ZombieD6Yellow : IZombieD6
{
    public DieType GetDieType()
    {
        return DieType.Yellow;
    }
    public DieFace[] GetFaces()
    {
        return [DieFace.Brain, DieFace.Brain, DieFace.Runner, DieFace.Runner, DieFace.Shotgun, DieFace.Shotgun];
    }
}