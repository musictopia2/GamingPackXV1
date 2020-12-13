using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using GalaxyCardGameCP.Cards;
namespace GalaxyCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class GalaxyCardGameVMData : ObservableObject, ITrickCardGamesData<GalaxyCardGameCardInformation, EnumSuitList>
    {
        public GalaxyCardGameVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, GalaxyCardGameCardInformation> trickArea1
            )
        {
            Deck1 = new DeckObservablePile<GalaxyCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<GalaxyCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<GalaxyCardGameCardInformation>(command);
            PlayerHand1.Maximum = 9;
            TrickArea1 = trickArea1;
        }
        public BasicTrickAreaObservable<EnumSuitList, GalaxyCardGameCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<GalaxyCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<GalaxyCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<GalaxyCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<GalaxyCardGameCardInformation>? OtherPile { get; set; }
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