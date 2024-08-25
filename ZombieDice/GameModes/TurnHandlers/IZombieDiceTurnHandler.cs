using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice.GameModes.TurnHandlers
{
    public interface IZombieDiceTurnHandler
    {
        PlayedDie[] RollZombieDice();
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
