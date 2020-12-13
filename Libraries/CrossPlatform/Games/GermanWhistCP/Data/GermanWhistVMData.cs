using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using GermanWhistCP.Cards;
namespace GermanWhistCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class GermanWhistVMData : ObservableObject, ITrickCardGamesData<GermanWhistCardInformation, EnumSuitList>
    {
        public GermanWhistVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, GermanWhistCardInformation> trickArea1
            )
        {
            Deck1 = new DeckObservablePile<GermanWhistCardInformation>(command);
            Pile1 = new SingleObservablePile<GermanWhistCardInformation>(command);
            PlayerHand1 = new HandObservable<GermanWhistCardInformation>(command);
            TrickArea1 = trickArea1;
        }
        public BasicTrickAreaObservable<EnumSuitList, GermanWhistCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<GermanWhistCardInformation> Deck1 { get; set; }
        public SingleObservablePile<GermanWhistCardInformation> Pile1 { get; set; }
        public HandObservable<GermanWhistCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<GermanWhistCardInformation>? OtherPile { get; set; }
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