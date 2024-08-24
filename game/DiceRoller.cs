namespace ZombieDice;

public class DiceRoller
{
    public DieFace Roll(IZombieD6 die)
    {
        var rand = new Random();
        var faces = die.GetFaces();
        return faces[rand.Next(faces.Length)];
    }
}