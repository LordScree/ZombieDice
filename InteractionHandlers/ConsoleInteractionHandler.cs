using LordScree.ZombieDice;
using LordScree.ZombieDice.Dice;
using LordScree.ZombieDice.GameModes.TurnHandlers;
using System.Text;

namespace LordScree.InteractionHandlers
{
    public class ConsoleInteractionHandler
    {
        private string[] Verbs { get; set; } = ["Start", "Inspect", "Again", "Bank", "End", "Help", "Quit"];
        public string[] GetSupportedVerbs()
        {
            return Verbs;
        }

        private string GetHelp()
        {
            var help = new StringBuilder();
            help.AppendLine("Welcome to LordScree's Zombie Dice game.");
            help.AppendLine("A .NET8 variant of the classic Steve Jackson game.");
            help.AppendLine();
            help.AppendLine($"The following verbs are supported: {string.Join(", ", Verbs)}");
            help.AppendLine("Start: Start your turn.");
            help.AppendLine("Inspect: Inspect the dice you have already played.");
            help.AppendLine("Again: Go again (reach back into the bag and roll more dice!)");
            help.AppendLine("Bank: Bank your brains.");
            help.AppendLine("End: End your turn and hand the bag to the next player.");
            help.AppendLine("Help: Print this help.");
            help.AppendLine("Quit: Exit the game.");
            return help.ToString();
        }

        private string GetInspectionString(IZombieDiceTurnHandler turnHandler)
        {
            var playedDiceTypes = turnHandler.InspectPlayedDice();
            var result = new StringBuilder();

            result.AppendLine($"Dice played this turn: {string.Join(", ", playedDiceTypes)}");
            result.AppendLine("Summary:");
            result.AppendLine($"Red:\t{playedDiceTypes.Count(x => x == ZombieDice.Dice.ZombieDieType.Red)}");
            result.AppendLine($"Yellow:\t{playedDiceTypes.Count(x => x == ZombieDice.Dice.ZombieDieType.Yellow)}");
            result.AppendLine($"Green:\t{playedDiceTypes.Count(x => x == ZombieDice.Dice.ZombieDieType.Green)}");
            result.AppendLine($"Dice remaining in the bag: {turnHandler.GetTotalDiceCount() - playedDiceTypes.Length}");

            return result.ToString();
        }

        private string GetRollResultString(PlayedDie[] rollResult, bool endOfTurn)
        {
            var result = new StringBuilder();

            result.AppendLine($"You rolled {rollResult.Length} zombie dice!");

            for (int i = 0; i < rollResult.Length; i++)
            {
                result.AppendLine($"Die {i + 1} ({rollResult[i].Type}):\t{rollResult[i].Face}");
            }

            if (endOfTurn)
            {
                result.AppendLine("You have been shot too many times! It is the end of your turn.");
            }
            else
            {
                result.AppendLine("You can continue to eat more brains if you wish!");
            }

            return result.ToString();
        }

        private string GetBrainBankingString(int playerTotalBankedBrains)
        {
            var result = new StringBuilder();

            result.AppendLine($"You have now banked {playerTotalBankedBrains} brains.");

            return result.ToString();
        }

        private string GetEndTurnString(ZombieDicePlayer zombieDicePlayer)
        {
            var result = new StringBuilder();

            result.AppendLine($"You have ended your turn with {zombieDicePlayer.BankedBrains} banked brains.");

            if (zombieDicePlayer.Winner)
            {
                result.AppendLine($"You are potentially a winner. We will need to finish the round to find out!");
            }

            return result.ToString();
        }

        private string GetNewTurnString(ZombieDicePlayer nextPlayer)
        {
            var result = new StringBuilder();

            result.AppendLine($"It is now {nextPlayer.PlayerName}'s turn.");
            result.AppendLine($"{nextPlayer.PlayerName} starts their turn with {nextPlayer.BankedBrains} banked brains.");

            return result.ToString();
        }

        private string GetGameEndSummary(GameState gameState)
        {
            var winners = gameState.GetWinners();
            var winnerStr = winners.Length == 1 ? "winner is" : "winners are";
            var winnersStr = string.Join(" and ", winners.Select(x => x.PlayerName));

            var result = new StringBuilder();
            result.AppendLine($"The game has ended! And the {winnerStr}...");
            result.AppendLine($"{winnersStr}!");
            
            return result.ToString();
        }

        public ConsoleInteractionHandler() { }

        public void PrintHelp()
        {
            Console.WriteLine(GetHelp());
        }

        public void PrintDiceInspection(IZombieDiceTurnHandler turnHandler)
        {
            Console.WriteLine(GetInspectionString(turnHandler));
        }

        public void PrintRollResult(PlayedDie[] rollResult, bool endOfTurn)
        {
            Console.WriteLine(GetRollResultString(rollResult, endOfTurn));
        }

        public string ReadPlayerInput(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            input = String.IsNullOrWhiteSpace(input) ? String.Empty : input.Trim();
            return input;
        }

        public bool IsVerb(string verb)
        {
            return Verbs.Contains(verb, StringComparer.OrdinalIgnoreCase);
        }

        internal void PrintBrainBank(int playerTotalBankedBrains)
        {
            Console.WriteLine(GetBrainBankingString(playerTotalBankedBrains));
        }

        internal void PrintEndOfTurn(ZombieDicePlayer zombieDicePlayer)
        {
            Console.WriteLine(GetEndTurnString(zombieDicePlayer));
        }

        internal void PrintGameEndSummary(GameState gameState)
        {
            Console.WriteLine(GetGameEndSummary(gameState));
        }

        internal void PrintNextPlayerIntro(ZombieDicePlayer zombieDicePlayer)
        {
            Console.WriteLine(GetNewTurnString(zombieDicePlayer));
        }

        internal void PrintInvalidInputMessage(string invalidInput)
        {
            Console.WriteLine($"{invalidInput} is not one of the valid verbs. For help, type \"Help\".");
        }

        internal bool QuitConfirm()
        {
            Console.WriteLine("Are you sure you wish to quit? (y/n)");
            var conf = Console.ReadKey();
            Console.WriteLine();
            return conf.KeyChar == 'y';
        }
    }
}
