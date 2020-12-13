using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace A21DiceGameCP.Data
{
    public class A21DiceGamePlayerItem : SimplePlayer
    {
        private bool _isFaceOff;

        public bool IsFaceOff
        {
            get { return _isFaceOff; }
            set
            {
                if (SetProperty(ref _isFaceOff, value))
                {
                    
                }

            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    
                }

            }
        }
        private int _numberOfRolls;
        public int NumberOfRolls
        {
            get { return _numberOfRolls; }
            set
            {
                if (SetProperty(ref _numberOfRolls, value))
                {
                    
                }
            }
        }
    }
}