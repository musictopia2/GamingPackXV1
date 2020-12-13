using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using HitTheDeckCP.Cards;
namespace HitTheDeckCP.Data
{
    public class HitTheDeckPlayerItem : PlayerSingleHand<HitTheDeckCardInformation>
    {
        private int _previousPoints;
        public int PreviousPoints
        {
            get { return _previousPoints; }
            set
            {
                if (SetProperty(ref _previousPoints, value))
                {
                    
                }
            }
        }
        private int _totalPoints;
        public int TotalPoints
        {
            get { return _totalPoints; }
            set
            {
                if (SetProperty(ref _totalPoints, value))
                {
                    
                }
            }
        }
    }
}