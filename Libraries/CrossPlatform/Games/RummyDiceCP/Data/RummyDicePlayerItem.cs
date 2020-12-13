using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace RummyDiceCP.Data
{
    public class RummyDicePlayerItem : SimplePlayer
    {
        private int _scoreRound;
        public int ScoreRound
        {
            get { return _scoreRound; }
            set
            {
                if (SetProperty(ref _scoreRound, value))
                {
                    
                }
            }
        }
        private int _scoreGame;
        public int ScoreGame
        {
            get { return _scoreGame; }
            set
            {
                if (SetProperty(ref _scoreGame, value))
                {
                    
                }
            }
        }
        private int _phase;
        public int Phase
        {
            get { return _phase; }
            set
            {
                if (SetProperty(ref _phase, value))
                {
                    
                }
            }
        }
        public int HowManyRepeats { get; set; }
    }
}