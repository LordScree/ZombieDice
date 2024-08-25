using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice
{
    public interface IZombieDiceTurnHandler
    {
        PlayedDie[] StartTurn();
        PlayedDie[] GoAgain();
        ZombieDieType[] InspectHand();
        ZombieDieType[] InspectPlayedDice();
        int GetCurrentBrains();
        int GetCurrentShotguns();
        PlayedDie[] GetCurrentPlayedDice();
        int BankBrains(ZombieDicePlayer player);
        bool HasTurnEnded();
    }
}
