using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace OpetongCP.Data
{
    public class OpetongPlayerItem : PlayerRummyHand<RegularRummyCard>
    {
        private int _setsPlayed;
        public int SetsPlayed
        {
            get { return _setsPlayed; }
            set
            {
                if (SetProperty(ref _setsPlayed, value))
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