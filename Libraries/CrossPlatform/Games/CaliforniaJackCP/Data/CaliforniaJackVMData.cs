using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CaliforniaJackCP.Cards;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace CaliforniaJackCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class CaliforniaJackVMData : ObservableObject, ITrickCardGamesData<CaliforniaJackCardInformation, EnumSuitList>
    {
        public CaliforniaJackVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, CaliforniaJackCardInformation> trickArea1
            )
        {
            Deck1 = new DeckObservablePile<CaliforniaJackCardInformation>(command);
            Pile1 = new SingleObservablePile<CaliforniaJackCardInformation>(command);
            PlayerHand1 = new HandObservable<CaliforniaJackCardInformation>(command);
            TrickArea1 = trickArea1;
        }
        public BasicTrickAreaObservable<EnumSuitList, CaliforniaJackCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<CaliforniaJackCardInformation> Deck1 { get; set; }
        public SingleObservablePile<CaliforniaJackCardInformation> Pile1 { get; set; }
        public HandObservable<CaliforniaJackCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<CaliforniaJackCardInformation>? OtherPile { get; set; }
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
    }
}