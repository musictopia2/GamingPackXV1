using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using RoundsCardGameCP.Cards;
namespace RoundsCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class RoundsCardGameVMData : ObservableObject, ITrickCardGamesData<RoundsCardGameCardInformation, EnumSuitList>
    {
        public RoundsCardGameVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, RoundsCardGameCardInformation> trickArea1
            )
        {
            Deck1 = new DeckObservablePile<RoundsCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<RoundsCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<RoundsCardGameCardInformation>(command);
            TrickArea1 = trickArea1;
        }
        public BasicTrickAreaObservable<EnumSuitList, RoundsCardGameCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<RoundsCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<RoundsCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<RoundsCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<RoundsCardGameCardInformation>? OtherPile { get; set; }
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