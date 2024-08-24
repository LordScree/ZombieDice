using ZombieDice.Dice;

namespace ZombieDice;

public class StandardZombieDiceGame : IZombieDiceGame
{
    List<IZombieDie> _zombieDice;
    DiceBag _bag;

    public StandardZombieDiceGame()
    {
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
}