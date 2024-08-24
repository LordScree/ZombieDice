using ZombieDice.Dice;

namespace ZombieDice;

public class DiceRoller
{
    public ZombieDieFace Roll(IZombieDie die)
    {
        var rand = new Random();
        var faces = die.GetFaces();
        return faces[rand.Next(faces.Length)];
    }
}