using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using PickelCardGameCP.Cards;
namespace PickelCardGameCP.Data
{
    public class PickelCardGamePlayerItem : PlayerTrick<EnumSuitList, PickelCardGameCardInformation>
    {
        private EnumSuitList _suitDesired;
        public EnumSuitList SuitDesired
        {
            get { return _suitDesired; }
            set
            {
                if (SetProperty(ref _suitDesired, value))
                {
                    
                }
            }
        }
        private int _bidAmount;

        public int BidAmount
        {
            get { return _bidAmount; }
            set
            {
                if (SetProperty(ref _bidAmount, value))
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
    }
}