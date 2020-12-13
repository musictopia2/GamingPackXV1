using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
using HuseHeartsCP.Cards;
namespace HuseHeartsCP.Data
{
    public class HuseHeartsPlayerItem : PlayerTrick<EnumSuitList, HuseHeartsCardInformation>
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
        private int _previousScore;
        public int PreviousScore
        {
            get { return _previousScore; }
            set
            {
                if (SetProperty(ref _previousScore, value))
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
        private bool _hadPoints;
        public bool HadPoints
        {
            get { return _hadPoints; }
            set
            {
                if (SetProperty(ref _hadPoints, value))
                {
                    
                }
            }
        }
        public CustomBasicList<int> CardsPassed { get; set; } = new CustomBasicList<int>();
    }
}