using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using YahtzeeHandsDownCP.Cards;
namespace YahtzeeHandsDownCP.Data
{
    public class YahtzeeHandsDownPlayerItem : PlayerSingleHand<YahtzeeHandsDownCardInformation>
    {
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
        private string _wonLastRound = "";
        public string WonLastRound
        {
            get { return _wonLastRound; }
            set
            {
                if (SetProperty(ref _wonLastRound, value))
                {
                    
                }
            }
        }
        public YahtzeeResults Results { get; set; } = new YahtzeeResults();
    }
}