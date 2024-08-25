using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice;

public class DiceBag
{
    private List<IZombieDie> _dice;

    public DiceBag(List<IZombieDie> dice) => _dice = dice;

    public List<IZombieDie> GrabZombieDice(int howMany = 3)
    {
        List<IZombieDie> diceToReturn = [];

        // If we have three or fewer left, return those.
        if (_dice.Count <= howMany)
        {
            diceToReturn.AddRange(_dice);
            _dice.RemoveRange(0, _dice.Count - 1);
            return diceToReturn;
        }

        // Take three at random from the list and return them.
        var rand = new Random();
        int index;

        for (int i = 0; i < howMany; i++)
        {
            index = rand.Next(_dice.Count);
            diceToReturn.Add(_dice[index]);
            _dice.RemoveAt(index);
        }

        return diceToReturn;
    }
}