using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using OpetongCP.Logic;
namespace OpetongCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class OpetongVMData : ObservableObject, IBasicCardGamesData<RegularRummyCard>
    {
        public OpetongVMData(IEventAggregator aggregator, CommandContainer command, IGamePackageResolver resolver, OpetongGameContainer gameContainer)
        {
            Deck1 = new DeckObservablePile<RegularRummyCard>(command);
            Pile1 = new SingleObservablePile<RegularRummyCard>(command);
            PlayerHand1 = new HandObservable<RegularRummyCard>(command);
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard>(command, resolver);
            TempSets.HowManySets = 3;
            MainSets = new MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, RummySet, SavedSet>(command);
            Pool1 = new CardPool(gameContainer);
        }
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard> TempSets;
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, RummySet, SavedSet> MainSets;
        public CardPool Pool1;
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
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    
                }
            }
        }
    }
}