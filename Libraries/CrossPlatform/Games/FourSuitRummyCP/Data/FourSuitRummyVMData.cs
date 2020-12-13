using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace FourSuitRummyCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class FourSuitRummyVMData : ObservableObject, IBasicCardGamesData<RegularRummyCard>
    {
        public FourSuitRummyVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<RegularRummyCard>(command);
            Pile1 = new SingleObservablePile<RegularRummyCard>(command);
            PlayerHand1 = new HandObservable<RegularRummyCard>(command);
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard>(command, resolver);
            TempSets.HowManySets = 7;
        }
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard> TempSets;
        public DeckObservablePile<RegularRummyCard> Deck1 { get; set; }
        public SingleObservablePile<RegularRummyCard> Pile1 { get; set; }
        public HandObservable<RegularRummyCard> PlayerHand1 { get; set; }
        public SingleObservablePile<RegularRummyCard>? OtherPile { get; set; }
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
    }
}