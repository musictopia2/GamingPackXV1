using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace GoFishCP.Data
{
    public class GoFishPlayerItem : PlayerSingleHand<RegularSimpleCard>
    {
        private int _pairs;
        public int Pairs
        {
            get { return _pairs; }
            set
            {
                if (SetProperty(ref _pairs, value))
                {
                    
                }
            }
        }
    }
}