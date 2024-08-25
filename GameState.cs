using LordScree.ZombieDice;

namespace LordScree.ZombieDice
{
    internal class GameState
    {
        private IZombieDiceTurnHandler TurnHandler { get; set; }
        public static int TargetBrains { get; set; } = 13;
        public List<ZombieDicePlayer> Players { get; set; } = [];

        private int CurrentPlayerIndex { get; set; } = 0;

        public bool SomeoneHasWon { get; private set; } = false;

        private bool GameHasEnded { get; set; } = false;

        private int[] WinningPlayerIndexes { get; set; } = [];

        public GameState(IZombieDiceTurnHandler turnHandler, int numberOfPlayers)
        {
            TurnHandler = turnHandler;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(ZombieDicePlayer.GetPlayer($"Player {i + 1}"));
            }
        }

        public GameState(IZombieDiceTurnHandler turnHandler, string[] playerNames)
        {
            TurnHandler = turnHandler;
            foreach (string playerName in playerNames)
            {
                Players.Add(ZombieDicePlayer.GetPlayer(playerName));
            }
        }

        public ZombieDicePlayer GetCurrentPlayer()
        {
            return Players[CurrentPlayerIndex];
        }

        public bool HasGameEnded()
        {
            CheckForWinner();
            return GameHasEnded;
        }

        public ZombieDicePlayer NextPlayer()
        {
            CheckForWinner();
            CurrentPlayerIndex = CurrentPlayerIndex == Players.Count - 1 ? CurrentPlayerIndex + 1 : 0;
            return Players[CurrentPlayerIndex];
        }

        private void CheckForWinner()
        {
            if (SomeoneHasWon)
            {
                CheckForGameEnd();
            }
            else if (Players[CurrentPlayerIndex].Winner)
            {
                SomeoneHasWon = true;
                CheckForGameEnd();
            }
        }

        private void CheckForGameEnd()
        {
            // The game ends at the end of the round in which someone has banked enough brains.
            if (SomeoneHasWon && CurrentPlayerIndex == Players.Count)
            {
                GameHasEnded = true;
                ResolveWinners();
            }
        }

        private void ResolveWinners()
        {
            var highScore = 0;
            List<int> currentWinnerIndexes = [];
            for(int i=0; i<Players.Count; i++)
            {
                if (Players[i].Winner && Players[i].BankedBrains >= highScore)
                {
                    highScore = Players[i].BankedBrains;
                    currentWinnerIndexes.Add(i);
                }
            }
            WinningPlayerIndexes = currentWinnerIndexes.ToArray();
        }

        public ZombieDicePlayer[] GetWinners()
        {
            if (!GameHasEnded || WinningPlayerIndexes.Length == 0) throw new ZombieDiceGameStateException("No winners yet!");

            List<ZombieDicePlayer> winners = [];
            foreach (int i in WinningPlayerIndexes)
            {
                winners.Add(Players[i]);
            }
            return winners.ToArray();
        }
    }
}

