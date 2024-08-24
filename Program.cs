using ZombieDice;

var greenDie = new ZombieD6Green();
var yellowDie = new ZombieD6Yellow();
var redDie = new ZombieD6Red();

var roller = new DiceRoller();

Console.WriteLine($"Green: {roller.Roll(greenDie)}");
Console.WriteLine($"Yellow: {roller.Roll(yellowDie)}");
Console.WriteLine($"Red: {roller.Roll(redDie)}");
