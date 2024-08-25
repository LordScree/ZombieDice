using LordScree.ZombieDice.Dice;

namespace LordScree.ZombieDice
{
    public class StandardZombieDiceTurnHandler : IZombieDiceTurnHandler
    {
        private IZombieDiceGame _game;
        private DiceRoller _roller;

        private List<PlayedDie> PlayedDice { get; set; } = new List<PlayedDie>();

        private List<IZombieDie> _currentHand = new List<IZombieDie>();

        private int Brains { get; set; }

        private int Shotguns { get; set; }


        private bool EndTurn { get; set; } = false;


        public StandardZombieDiceTurnHandler(IZombieDiceGame game, DiceRoller roller)
        {
            _game = game;
            _roller = roller;
        }

        public int GetCurrentBrains()
        {
            return Brains;
        }

        public int GetCurrentShotguns()
        {
            return Shotguns;
        }

        public PlayedDie[] GetCurrentPlayedDice()
        {
            return PlayedDice.ToArray();
        }

        public PlayedDie[] StartTurn()
        {
            EndTurn = false;
            _game.ResetDiceBag();
            PlayedDice = new List<PlayedDie>();
            _currentHand = _game.GrabZombieDice(3);
            var rollResult = RollHand();
            CleanupCurrentHand();
            return rollResult;
        }

        public PlayedDie[] GoAgain()
        {
            if (EndTurn) throw new TurnAlreadyOverException();

            _currentHand.AddRange(_game.GrabZombieDice(_currentHand.Count - 3));
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

        public int BankBrains(ZombieDicePlayer player)
        {
            if (EndTurn) throw new TurnAlreadyOverException("You cannot bank brains if your turn is over.");

            return player.BankBrains(Brains);
        }

        public bool HasTurnEnded()
        {
            return EndTurn;
        }

        private PlayedDie[] RollHand()
        {
            if (EndTurn) throw new TurnAlreadyOverException("You cannot roll dice if your turn is over.");

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
}
