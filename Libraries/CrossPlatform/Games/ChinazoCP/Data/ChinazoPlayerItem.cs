using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace ChinazoCP.Data
{
    public class ChinazoPlayerItem : PlayerRummyHand<ChinazoCard>
    {
        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }
            set
            {
                if (SetProperty(ref _currentScore, value))
                {
                    
                }
            }
        }
        private int _totalScore;

        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                if (SetProperty(ref _totalScore, value))
                {
                    
                }
            }
        }
        public bool LaidDown { get; set; }
    }
}