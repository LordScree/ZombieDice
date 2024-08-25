
namespace LordScree.ZombieDice
{
    [Serializable]
    internal class ZombieDiceGameStateException : Exception
    {
        public ZombieDiceGameStateException()
        {
        }

        public ZombieDiceGameStateException(string? message) : base(message)
        {
        }

        public ZombieDiceGameStateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}