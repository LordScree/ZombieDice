using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice.GameModes.TurnHandlers
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
        int GetTotalDiceCount();
    }
}
