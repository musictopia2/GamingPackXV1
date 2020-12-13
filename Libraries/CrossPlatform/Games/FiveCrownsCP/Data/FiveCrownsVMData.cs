using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Logic;
namespace FiveCrownsCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class FiveCrownsVMData : ObservableObject, IBasicCardGamesData<FiveCrownsCardInformation>
    {
        public FiveCrownsVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<FiveCrownsCardInformation>(command);
            Pile1 = new SingleObservablePile<FiveCrownsCardInformation>(command);
            PlayerHand1 = new HandObservable<FiveCrownsCardInformation>(command);
            PlayerHand1.Maximum = 14;
            TempSets = new TempSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation>(command, resolver);
            MainSets = new MainSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation, PhaseSet, SavedSet>(command);
            TempSets.HowManySets = 6;
        }
        public TempSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation> TempSets;
        public MainSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation, PhaseSet, SavedSet> MainSets;
        public DeckObservablePile<FiveCrownsCardInformation> Deck1 { get; set; }
        public SingleObservablePile<FiveCrownsCardInformation> Pile1 { get; set; }
        public HandObservable<FiveCrownsCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<FiveCrownsCardInformation>? OtherPile { get; set; }
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
        private int _upTo;
        [VM]
        public int UpTo
        {
            get { return _upTo; }
            set
            {
                if (SetProperty(ref _upTo, value))
                {
                    
                }
            }
        }
    }
}