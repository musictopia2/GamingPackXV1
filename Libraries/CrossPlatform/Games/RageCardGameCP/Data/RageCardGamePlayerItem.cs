using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using RageCardGameCP.Cards;
namespace RageCardGameCP.Data
{
    public class RageCardGamePlayerItem : PlayerTrick<EnumColor, RageCardGameCardInformation>
    {
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
        private bool _revealBid;
        public bool RevealBid
        {
            get { return _revealBid; }
            set
            {
                if (SetProperty(ref _revealBid, value))
                {
                    
                }
            }
        }
        private int _correctlyBidded;
        public int CorrectlyBidded
        {
            get { return _correctlyBidded; }
            set
            {
                if (SetProperty(ref _correctlyBidded, value))
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
        private int _scoreGame;
        public int ScoreGame
        {
            get { return _scoreGame; }
            set
            {
                if (SetProperty(ref _scoreGame, value))
                {
                    
                }
            }
        }
    }
}