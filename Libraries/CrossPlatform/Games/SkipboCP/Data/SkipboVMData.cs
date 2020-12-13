using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using SkipboCP.Cards;
using SkipboCP.Piles;
namespace SkipboCP.Data
{
    [SingletonGame]
    [AutoReset] 
    public class SkipboVMData : ObservableObject, IBasicCardGamesData<SkipboCardInformation>
    {
        public SkipboVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<SkipboCardInformation>(command);
            Pile1 = new SingleObservablePile<SkipboCardInformation>(command);
            PlayerHand1 = new HandObservable<SkipboCardInformation>(command);
            StockPile = new StockViewModel(command);
            PublicPiles = new PublicPilesViewModel(command);
        }
        public StockViewModel StockPile;
        public DiscardPilesVM<SkipboCardInformation>? DiscardPiles;
        public PublicPilesViewModel PublicPiles;
        public DeckObservablePile<SkipboCardInformation> Deck1 { get; set; }
        public SingleObservablePile<SkipboCardInformation> Pile1 { get; set; }
        public HandObservable<SkipboCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<SkipboCardInformation>? OtherPile { get; set; }
        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }

        private string _status = "";
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private int _cardsToShuffle;
        [VM]
        public int CardsToShuffle
        {
            get { return _cardsToShuffle; }
            set
            {
                if (SetProperty(ref _cardsToShuffle, value))
                {
                    
                }
            }
        }
    }
}