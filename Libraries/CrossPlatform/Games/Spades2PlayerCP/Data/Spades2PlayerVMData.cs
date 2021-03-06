using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Spades2PlayerCP.Cards;
namespace Spades2PlayerCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class Spades2PlayerVMData : ObservableObject, ITrickCardGamesData<Spades2PlayerCardInformation, EnumSuitList>
    {
        public Spades2PlayerVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, Spades2PlayerCardInformation> trickArea1,
            IGamePackageResolver resolver
            )
        {
            Deck1 = new DeckObservablePile<Spades2PlayerCardInformation>(command);
            Pile1 = new SingleObservablePile<Spades2PlayerCardInformation>(command);
            PlayerHand1 = new HandObservable<Spades2PlayerCardInformation>(command);
            TrickArea1 = trickArea1;
            OtherPile = new SingleObservablePile<Spades2PlayerCardInformation>(command);
            Bid1 = new NumberPicker(command, resolver);
            Bid1.LoadNormalNumberRangeValues(0, 13);
            OtherPile.Text = "Current";
            OtherPile.Visible = false;
        }
        public NumberPicker Bid1;
        public BasicTrickAreaObservable<EnumSuitList, Spades2PlayerCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<Spades2PlayerCardInformation> Deck1 { get; set; }
        public SingleObservablePile<Spades2PlayerCardInformation> Pile1 { get; set; }
        public HandObservable<Spades2PlayerCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<Spades2PlayerCardInformation>? OtherPile { get; set; }
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
        private EnumSuitList _trumpSuit;
        [VM]
        public EnumSuitList TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
            }
        }

        private int _roundNumber;
        [VM]
        public int RoundNumber
        {
            get { return _roundNumber; }
            set
            {
                if (SetProperty(ref _roundNumber, value))
                {
                    
                }
            }
        }
        private EnumGameStatus _gameStatus;
        [VM]
        public EnumGameStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    
                }
            }
        }
        public int BidAmount { get; set; } = -1;
    }
}