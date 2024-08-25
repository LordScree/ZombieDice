using LordScree.ZombieDice;
using LordScree.ZombieDice.Dice;
using LordScree.ZombieDice.GameModes.TurnHandlers;
using System.Text;

namespace LordScree.InteractionHandlers
{
    public class ConsoleInteractionHandler
    {
        private Dictionary<string, string> Actions { get; set; } = new Dictionary<string, string>() {
            {"Roll", "Reach into the bag and roll some zombie dice!"}, 
            {"Inspect", "View a summary of the dice you have already played this turn."},
            {"Bank", "Bank your brains and hand over to the next player."},
            {"Help", "Print this help."}, {"Quit", "Exit the game."}
         };

        private string GetHelp()
        {
            var help = new StringBuilder();
            help.AppendLine();
            help.AppendLine("**************************************************");
            help.AppendLine("     Welcome to LordScree's Zombie Dice game.     ");
            help.AppendLine("A .NET8 variant of the classic Steve Jackson game.");
            help.AppendLine("**************************************************");
            help.AppendLine();
            help.AppendLine("Rules");
            help.AppendLine("--------------------------------------------------");
            help.AppendLine("Zombie Dice is a simple dice-rolling game of chance and pushing your luck.");
            help.AppendLine("On your turn, you will pick three dice out of a bag and 'roll' them.");
            help.AppendLine("Each die has six sides, with chances of either a Brain (good) a Shotgun (bad) or a Runner (re-roll).");
            help.AppendLine("(Details of dice variants below)");
            help.AppendLine();
            help.AppendLine("You can 'bank' your brains at any point to end your turn and pass to the next player. Banked brains are safe and accumulate across multiple turns.");
            help.AppendLine("The game ends at the end of the round in which a player has banked 13 or more brains. The winner is then the player with the most banked brains. Ties are shared (zombies are used to sharing things).");
            help.AppendLine();
            help.AppendLine("If you ever have 3 Shotguns on your turn, you end your turn and play passes to the next player. You lose any unbanked brains.");
            help.AppendLine();
            help.AppendLine("You can 'roll' as many times as you like on your turn. Each time you 'roll', any previously-rolled Brains or Shotguns will be set aside and any Runners will be re-rolled.");
            help.AppendLine();
            help.AppendLine("In this way, the number of Shotguns and Brains accumulate on your turn, until you either bank your Brains or you are shot too many times.");
            help.AppendLine();
            help.AppendLine("Dice variants");
            help.AppendLine("--------------------------------------------------");
            help.AppendLine("There is a bag of 13 zombie dice: 6 green dice, 4 yellow dice and 3 red dice.");
            help.AppendLine("Green dice have 3 brains, 2 runners and 1 shotgun.");
            help.AppendLine("Yellow dice have 2 each of brains, runners and shotguns.");
            help.AppendLine("Red dice have 1 brain, 2 runners and 3 shotguns (these are bad!).");
            help.AppendLine();
            help.AppendLine("Actions (how to play in the console)");
            help.AppendLine("--------------------------------------------------");
            help.AppendLine("This is a console-based game. To play, you need to type an action and then press return.");
            help.AppendLine("(actions are case-insensitive)");
            help.AppendLine();
            help.AppendLine($"The following actions are supported: {string.Join(", ", Actions.Select(x => x.Key))}");

            foreach(var a in Actions)
            {
                help.AppendLine($"{a.Key}\t\t{a.Value}");
            }

            help.AppendLine();
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

            result.AppendLine($"{zombieDicePlayer.PlayerName} has ended their turn with {zombieDicePlayer.BankedBrains} banked brains.");

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
            return Actions.Keys.Contains(verb, StringComparer.OrdinalIgnoreCase);
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
