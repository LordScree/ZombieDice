namespace ZombieDice;
public class ZombieD6Green : IZombieD6
{
    public DieType GetDieType()
    {
        return DieType.Green;
    }
    public DieFace[] GetFaces()
    {
        return [DieFace.Brain, DieFace.Brain, DieFace.Brain, DieFace.Runner, DieFace.Runner, DieFace.Shotgun];
    }
}