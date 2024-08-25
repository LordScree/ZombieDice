using LordScree.ZombieDice.Dice;
using LordScree.ZombieDice.GameModes.TurnHandlers;

namespace LordScree.ZombieDice.GameModes;

public interface IZombieDiceGame
{
    void ResetDiceBag();
    List<IZombieDie> GrabZombieDice(int howMany);

    IZombieDiceTurnHandler GetTurnHandler(DiceRoller roller);
}