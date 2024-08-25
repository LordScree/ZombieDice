using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;

public interface IZombieDiceGame
{
    void ResetDiceBag();
    List<IZombieDie> GrabZombieDice(int howMany);

    IZombieDiceTurnHandler GetTurnHandler(DiceRoller roller);
}