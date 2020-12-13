using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using FiveCrownsCP.Cards;
namespace FiveCrownsCP.Data
{
    public class FiveCrownsPlayerItem : PlayerRummyHand<FiveCrownsCardInformation>
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