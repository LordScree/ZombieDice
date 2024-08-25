using LordScree.InteractionHandlers;
using LordScree.ZombieDice;
using LordScree.ZombieDice.Dice;
using LordScree.ZombieDice.GameModes;
using System.Net.Security;
using System.Runtime;

var game = new StandardZombieDiceGame();
var roller = new DiceRoller();
var turnHandler = game.GetTurnHandler(roller);
var gameState = new GameState(turnHandler, ["Lizzie", "Tini", "Dard"]);
var player = gameState.GetCurrentPlayer();

var consoleHandler = new ConsoleInteractionHandler();
consoleHandler.PrintHelp();

Console.WriteLine($"Current Player: {player.PlayerName}");

string input = String.Empty;
PlayedDie[] playedDice = [];

bool done = false;

while (!done)
{
    // Check game state at the start of the loop.
    done = gameState.HasGameEnded();
    input = consoleHandler.ReadPlayerInput("What would you like to do?");
    if (consoleHandler.IsVerb(input))
    {
        switch (input.ToLower())
        {
            case "roll":
                consoleHandler.PrintRollResult(turnHandler.RollZombieDice(), turnHandler.HasTurnEnded());
                if (turnHandler.HasTurnEnded()) HandleEndTurn();
                break;
            case "inspect":
                consoleHandler.PrintDiceInspection(turnHandler);
                break;
            case "bank":
                consoleHandler.PrintBrainBank(turnHandler.BankBrains(player));
                HandleEndTurn();
                break;
            case "quit":
                if (consoleHandler.QuitConfirm())
                {
                    done = true;
                }
                break;
            case "help":
                consoleHandler.PrintHelp();
                break;
        }
    }
    else
    {
        consoleHandler.PrintInvalidInputMessage(input);
    }

    if (gameState.HasGameEnded()) { done = true; }
}

void HandleEndTurn()
{
    consoleHandler.PrintEndOfTurn(player);
    if (gameState.HasGameEnded())
    {
        consoleHandler.PrintGameEndSummary(gameState);
    }
    else
    {
        player = gameState.NextPlayer();
        consoleHandler.PrintNextPlayerIntro(player);
    }
}