using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Rummy500CP.Logic;
namespace Rummy500CP.Data
{
    [SingletonGame]
    [AutoReset]
    public class Rummy500VMData : ObservableObject, IBasicCardGamesData<RegularRummyCard>
    {
        public Rummy500VMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<RegularRummyCard>(command);
            Pile1 = new SingleObservablePile<RegularRummyCard>(command);
            PlayerHand1 = new HandObservable<RegularRummyCard>(command);
            MainSets1 = new MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, RummySet, SavedSet>(command);
            DiscardList1 = new DiscardListCP(command);
            Pile1.Visible = false;
        }
        public DiscardListCP DiscardList1;
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, RummySet, SavedSet> MainSets1;
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