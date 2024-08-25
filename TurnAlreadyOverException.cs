namespace LordScree.ZombieDice;

[System.Serializable]
public class TurnAlreadyOverException : System.Exception
{
    public TurnAlreadyOverException() { }
    public TurnAlreadyOverException(string message) : base(message) { }
    public TurnAlreadyOverException(string message, System.Exception inner) : base(message, inner) { }
}