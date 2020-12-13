using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace GolfCardGameCP.Data
{
    public class GolfCardGamePlayerItem : PlayerSingleHand<RegularSimpleCard>
    {
        private bool _knocked;

        public bool Knocked
        {
            get { return _knocked; }
            set
            {
                if (SetProperty(ref _knocked, value))
                {
                    
                }
            }
        }
        private bool _firstChanged;
        public bool FirstChanged
        {
            get { return _firstChanged; }
            set
            {
                if (SetProperty(ref _firstChanged, value))
                {
                    
                }
            }
        }
        private bool _secondChanged;
        public bool SecondChanged
        {
            get { return _secondChanged; }
            set
            {
                if (SetProperty(ref _secondChanged, value))
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
        private bool _finishedChoosing;
        public bool FinishedChoosing
        {
            get { return _finishedChoosing; }
            set
            {
                if (SetProperty(ref _finishedChoosing, value))
                {
                    
                }
            }
        }
        public DeckRegularDict<RegularSimpleCard> TempSets { get; set; } = new DeckRegularDict<RegularSimpleCard>();
    }
}