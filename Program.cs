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

