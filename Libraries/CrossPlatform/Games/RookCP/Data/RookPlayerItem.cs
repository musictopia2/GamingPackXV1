using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using RookCP.Cards;
namespace RookCP.Data
{
    public class RookPlayerItem : PlayerTrick<EnumColorTypes, RookCardInformation>
    {
        private bool _isDummy;
        public bool IsDummy
        {
            get { return _isDummy; }
            set
            {
                if (SetProperty(ref _isDummy, value))
                {
                    
                }
            }
        }
        private bool _pass;
        public bool Pass
        {
            get { return _pass; }
            set
            {
                if (SetProperty(ref _pass, value))
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