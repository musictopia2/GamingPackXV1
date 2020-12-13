using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace CousinRummyCP.Data
{
    public class CousinRummyPlayerItem : PlayerRummyHand<RegularRummyCard>
    {
        private int _tokensLeft;
        public int TokensLeft
        {
            get { return _tokensLeft; }
            set
            {
                if (SetProperty(ref _tokensLeft, value))
                {
                    
                }
            }
        }
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