using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;
public struct PlayedDie
{
    public PlayedDie(ZombieDieType type, ZombieDieFace face)
    {
        Type = type;
        Face = face;
    }

    public ZombieDieType Type { get; set; }
    public ZombieDieFace Face { get; set; }
}

public interface IZombieDiceGame
{

}