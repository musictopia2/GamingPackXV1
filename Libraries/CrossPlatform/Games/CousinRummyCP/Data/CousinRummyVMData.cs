using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using CousinRummyCP.Logic;
namespace CousinRummyCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class CousinRummyVMData : ObservableObject, IBasicCardGamesData<RegularRummyCard>
    {
        public CousinRummyVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<RegularRummyCard>(command);
            Pile1 = new SingleObservablePile<RegularRummyCard>(command);
            PlayerHand1 = new HandObservable<RegularRummyCard>(command);
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard>(command, resolver);
            MainSets = new MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, PhaseSet, SavedSet>(command);
            TempSets.HowManySets = 8;
            MainSets.Text = "Main Sets";
        }
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard> TempSets;
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, RegularRummyCard, PhaseSet, SavedSet> MainSets;
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
        private string _phaseData = "";
        [VM]
        public string PhaseData
        {
            get { return _phaseData; }
            set
            {
                if (SetProperty(ref _phaseData, value))
                {
                    
                }
            }
        }
        private string _otherLabel = "";
        [VM]
        public string OtherLabel
        {
            get { return _otherLabel; }
            set
            {
                if (SetProperty(ref _otherLabel, value))
                {
                    
                }
            }
        }
    }
}