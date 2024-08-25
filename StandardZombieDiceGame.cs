using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;

public class StandardZombieDiceGame : IZombieDiceGame
{
    private List<IZombieDie> Dice { get; set; } = [];
    private DiceBag Bag { get; set; }

    public StandardZombieDiceGame()
    {
        Dice = [
            new GreenD6(),
            new GreenD6(),
            new GreenD6(),
            new GreenD6(),
            new GreenD6(),
            new GreenD6(),
            new YellowD6(),
            new YellowD6(),
            new YellowD6(),
            new YellowD6(),
            new RedD6(),
            new RedD6(),
            new RedD6()
        ];

        Bag = new DiceBag(Dice);
    }

    public void ResetDiceBag()
    {
        Bag = new DiceBag(Dice);
    }

    public List<IZombieDie> GrabZombieDice(int howMany)
    {
        return Bag.GrabZombieDice(howMany);
    }

    public IZombieDiceTurnHandler GetTurnHandler(DiceRoller roller)
    {
        return new StandardZombieDiceTurnHandler(this, roller);
    }

}