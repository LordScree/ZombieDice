using ZombieDice;
using ZombieDice.Dice;

// var greenDie = new GreenD6();
// var yellowDie = new YellowD6();
// var redDie = new RedD6();

// var roller = new DiceRoller();

// Console.WriteLine($"Green: {roller.Roll(greenDie)}");
// Console.WriteLine($"Yellow: {roller.Roll(yellowDie)}");
// Console.WriteLine($"Red: {roller.Roll(redDie)}");

var roller = new DiceRoller();
var g = new StandardZombieDiceGame(roller);

var result = g.StartTurn();
PrintResult(result, g.PlayedDice, g.EndTurn);

void PrintResult(PlayedDie[] rollResult, List<PlayedDie> allPlayedDice, bool endTurn)
{
    Console.WriteLine($"You rolled {rollResult.Length} zombie dice!");
    for (int i = 0; i < rollResult.Length; i++)
    {
        Console.WriteLine($"Die {i + 1} ({rollResult[i].Type}): {rollResult[i].Face}");
    }

    if (endTurn)
    {
        Console.WriteLine("You have been shot! It is the end of your turn.");
    }
    else
    {
        Console.WriteLine("You can continue to eat more brains if you wish!");
    }
}