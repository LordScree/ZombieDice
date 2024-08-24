using ZombieDice.Dice;

namespace ZombieDice;

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
            new ZombieD6Green(),
            new ZombieD6Green(),
            new ZombieD6Green(),
            new ZombieD6Green(),
            new ZombieD6Green(),
            new ZombieD6Green(),
            new ZombieD6Yellow(),
            new ZombieD6Yellow(),
            new ZombieD6Yellow(),
            new ZombieD6Yellow(),
            new ZombieD6Red(),
            new ZombieD6Red(),
            new ZombieD6Red()
        ];

        _bag = new DiceBag(_zombieDice);
    }

    public void ResetDiceBag()
    {
        _bag = new DiceBag(_zombieDice);
    }

    public void StartTurn()
    {
        EndTurn = false;
        ResetDiceBag();
        PlayedDice = new List<PlayedDie>();
        _currentHand = _bag.GrabZombieDice(3);
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

    public ZombieDieFace[] RollHand()
    {
        if (EndTurn) throw new TurnAlreadyOverException();

        var result = new List<ZombieDieFace>();

        foreach (var die in _currentHand)
        {
            result.Add(ResolveRoll(die));
        }

        return result.ToArray();
    }

    private ZombieDieFace ResolveRoll(IZombieDie die)
    {
        var currentFace = _roller.Roll(die);

        switch (currentFace)
        {
            case ZombieDieFace.Brain:
                Brains++;
                _currentHand.Remove(die);
                break;
            case ZombieDieFace.Runner:
                break;
            case ZombieDieFace.Shotgun:
                Shotguns++;
                CheckForEndTurnCondition();
                _currentHand.Remove(die);
                break;
        }

        PlayedDice.Add(new PlayedDie(die.GetZombieDieType(), currentFace));

        return currentFace;
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