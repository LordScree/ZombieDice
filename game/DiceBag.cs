namespace ZombieDice;

public class DiceBag
{
    List<IZombieD6> _dice;

    public DiceBag(List<IZombieD6> dice)
    {
        _dice = dice;
    }

    public List<IZombieD6> GrabZombieDice()
    {
        // take three at random from the list and return them.
        var rand = new Random();
        int index;
        List<IZombieD6> diceToReturn = new List<IZombieD6>();

        for (int i = 0; i <= 3; i++)
        {
            index = rand.Next(_dice.Count());
            diceToReturn.Add(_dice[index]);
            _dice.RemoveAt(index);
        }

        return diceToReturn;
    }
}