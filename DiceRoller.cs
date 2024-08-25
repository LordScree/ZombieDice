using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;

public class DiceRoller
{
    public ZombieDieFace Roll(IZombieDie die)
    {
        var rand = new Random();
        var faces = die.GetFaces();
        die.LastFace = faces[rand.Next(faces.Length)];
        return die.LastFace;
    }
}