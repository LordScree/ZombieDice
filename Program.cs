using LordScree.ZombieDice;
using LordScree.ZombieDice.Dice;
using LordScree.ZombieDice.GameModes;

var game = new StandardZombieDiceGame();
var roller = new DiceRoller();
var turnHandler = game.GetTurnHandler(roller);
var gameState = new GameState(turnHandler, ["Scree", "Nilly"]);

var player = gameState.GetCurrentPlayer();
Console.WriteLine($"Current Player: {player.PlayerName}");
var playedDice = turnHandler.StartTurn();

PrintResult(playedDice, turnHandler.GetCurrentPlayedDice(), turnHandler.HasTurnEnded());

static void PrintResult(PlayedDie[] rollResult, PlayedDie[] allPlayedDice, bool endOfTurn)
{
    Console.WriteLine($"You rolled {rollResult.Length} zombie dice!");
    for (int i = 0; i < rollResult.Length; i++)
    {
        Console.WriteLine($"Die {i + 1} ({rollResult[i].Type}): {rollResult[i].Face}");
    }

    if (endOfTurn)
    {
        Console.WriteLine("You have been shot! It is the end of your turn.");
    }
    else
    {
        Console.WriteLine("You can continue to eat more brains if you wish!");
    }
}