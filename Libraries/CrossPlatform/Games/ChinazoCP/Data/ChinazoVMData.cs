using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using ChinazoCP.Logic;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace ChinazoCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class ChinazoVMData : ObservableObject, IBasicCardGamesData<ChinazoCard>
    {
        public ChinazoVMData(IEventAggregator aggregator, CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<ChinazoCard>(command);
            Pile1 = new SingleObservablePile<ChinazoCard>(command);
            PlayerHand1 = new HandObservable<ChinazoCard>(command);
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, ChinazoCard>(command, resolver);
            MainSets = new MainSetsObservable<EnumSuitList, EnumRegularColorList, ChinazoCard, PhaseSet, SavedSet>(command);
            TempSets.HowManySets = 5;
        }
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, ChinazoCard> TempSets;
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, ChinazoCard, PhaseSet, SavedSet> MainSets;
        public DeckObservablePile<ChinazoCard> Deck1 { get; set; }
        public SingleObservablePile<ChinazoCard> Pile1 { get; set; }
        public HandObservable<ChinazoCard> PlayerHand1 { get; set; }
        public SingleObservablePile<ChinazoCard>? OtherPile { get; set; }
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