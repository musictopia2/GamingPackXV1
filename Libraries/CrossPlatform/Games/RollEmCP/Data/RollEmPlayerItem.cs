using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace RollEmCP.Data
{
    public class RollEmPlayerItem : SimplePlayer
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
    }
}