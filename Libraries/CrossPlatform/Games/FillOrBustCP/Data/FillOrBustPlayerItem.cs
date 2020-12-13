using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using FillOrBustCP.Cards;
namespace FillOrBustCP.Data
{
    public class FillOrBustPlayerItem : PlayerSingleHand<FillOrBustCardInformation>
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
    }
}