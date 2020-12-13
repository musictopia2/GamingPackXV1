using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using Newtonsoft.Json;
using SkuckCardGameCP.Cards;
namespace SkuckCardGameCP.Data
{
    public class SkuckCardGamePlayerItem : PlayerTrick<EnumSuitList, SkuckCardGameCardInformation>
    {
        private int _strengthHand;
        public int StrengthHand
        {
            get { return _strengthHand; }
            set
            {
                if (SetProperty(ref _strengthHand, value))
                {
                    
                }
            }
        }
        private string _tieBreaker = "";
        public string TieBreaker
        {
            get { return _tieBreaker; }
            set
            {
                if (SetProperty(ref _tieBreaker, value))
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
        private bool _bidVisible;
        public bool BidVisible
        {
            get { return _bidVisible; }
            set
            {
                if (SetProperty(ref _bidVisible, value))
                {
                    
                }
            }
        }
        private int _perfectRounds;
        public int PerfectRounds
        {
            get { return _perfectRounds; }
            set
            {
                if (SetProperty(ref _perfectRounds, value))
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
        [JsonIgnore]
        public PlayerBoardObservable<SkuckCardGameCardInformation>? TempHand;

        public DeckRegularDict<SkuckCardGameCardInformation> SavedTemp = new DeckRegularDict<SkuckCardGameCardInformation>();
    }
}