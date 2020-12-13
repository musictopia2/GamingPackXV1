using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using FlinchCP.Cards;
using FlinchCP.Piles;
namespace FlinchCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class FlinchVMData : ObservableObject, IBasicCardGamesData<FlinchCardInformation>
    {
        public FlinchVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<FlinchCardInformation>(command);
            Pile1 = new SingleObservablePile<FlinchCardInformation>(command);
            PlayerHand1 = new HandObservable<FlinchCardInformation>(command);
            StockPile = new StockViewModel(command);
            PublicPiles = new PublicPilesViewModel(command);
        }
        public StockViewModel StockPile;
        public DiscardPilesVM<FlinchCardInformation>? DiscardPiles;
        public PublicPilesViewModel PublicPiles;
        public DeckObservablePile<FlinchCardInformation> Deck1 { get; set; }
        public SingleObservablePile<FlinchCardInformation> Pile1 { get; set; }
        public HandObservable<FlinchCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<FlinchCardInformation>? OtherPile { get; set; }
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