using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;

public class StandardZombieDiceGame : IZombieDiceGame
{
    private List<IZombieDie> _zombieDice;
    private DiceBag _bag;

    public List<PlayedDie> PlayedDice { get; private set; } = new List<PlayedDie>();

    private List<IZombieDie> _currentHand = new List<IZombieDie>();

    public int Brains { get; private set; }

    public int Shotguns { get; private set; }

    private DiceRoller _roller;

    public bool EndTurn { get; set; } = false;

    public StandardZombieDiceGame(DiceRoller roller)
    {
        _roller = roller;

        _zombieDice = [
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

        _bag = new DiceBag(_zombieDice);
    }

    public void ResetDiceBag()
    {
        _bag = new DiceBag(_zombieDice);
    }

    public PlayedDie[] StartTurn()
    {
        EndTurn = false;
        ResetDiceBag();
        PlayedDice = new List<PlayedDie>();
        _currentHand = _bag.GrabZombieDice(3);
        var rollResult = RollHand();
        CleanupCurrentHand();
        return rollResult;
    }

    public PlayedDie[] GoAgain()
    {
        if (EndTurn) throw new TurnAlreadyOverException();

        _currentHand.AddRange(_bag.GrabZombieDice(_currentHand.Count() - 3));
        var rollResult = RollHand();
        CleanupCurrentHand();
        return rollResult;
    }

    public ZombieDieType[] InspectHand()
    {
        var result = new List<ZombieDieType>();
        foreach (var die in _currentHand)
        {
            result.Add(die.GetZombieDieType());
        }
        return result.ToArray();
    }

    public ZombieDieType[] InspectPlayedDice()
    {
        var result = new List<ZombieDieType>();
        foreach (var die in PlayedDice)
        {
            result.Add(die.Type);
        }
        return result.ToArray();
    }

    private PlayedDie[] RollHand()
    {
        if (EndTurn) throw new TurnAlreadyOverException();

        var result = new List<PlayedDie>();

        foreach (var die in _currentHand)
        {
            result.Add(ResolveRoll(die));
        }

        return result.ToArray();
    }

    private PlayedDie ResolveRoll(IZombieDie die)
    {
        var currentFace = _roller.Roll(die);

        switch (currentFace)
        {
            case ZombieDieFace.Brain:
                Brains++;
                break;
            case ZombieDieFace.Runner:
                break;
            case ZombieDieFace.Shotgun:
                Shotguns++;
                CheckForEndTurnCondition();
                break;
        }

        var result = new PlayedDie(die.GetZombieDieType(), currentFace);
        PlayedDice.Add(result);

        return result;
    }

    private void CleanupCurrentHand()
    {
        _currentHand.RemoveAll(x => x.LastFace == ZombieDieFace.Brain || x.LastFace == ZombieDieFace.Shotgun);
    }

    private void CheckForEndTurnCondition()
    {
        if (Shotguns >= 3)
        {
            Brains = 0;
            EndTurn = true;
        }
    }
}