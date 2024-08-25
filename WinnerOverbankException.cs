
namespace LordScree.ZombieDice
{
    [Serializable]
    internal class WinnerOverbankException : Exception
    {
        public WinnerOverbankException()
        {
        }

        public WinnerOverbankException(string? message) : base(message)
        {
        }

        public WinnerOverbankException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}