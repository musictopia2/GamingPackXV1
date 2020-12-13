using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Phase10CP.Cards;
using Phase10CP.SetClasses;
namespace Phase10CP.Data
{
    [SingletonGame]
    [AutoReset]
    public class Phase10VMData : ObservableObject, IBasicCardGamesData<Phase10CardInformation>
    {
        public Phase10VMData(CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<Phase10CardInformation>(command);
            Pile1 = new SingleObservablePile<Phase10CardInformation>(command);
            PlayerHand1 = new HandObservable<Phase10CardInformation>(command);
            PlayerHand1.Maximum = 11;
            TempSets = new TempSetsObservable<EnumColorTypes, EnumColorTypes, Phase10CardInformation>(command, resolver);
            MainSets = new MainSetsObservable<EnumColorTypes, EnumColorTypes, Phase10CardInformation, PhaseSet, SavedSet>(command);
            TempSets.HowManySets = 5;

        }
        public TempSetsObservable<EnumColorTypes, EnumColorTypes, Phase10CardInformation> TempSets;
        public MainSetsObservable<EnumColorTypes, EnumColorTypes, Phase10CardInformation, PhaseSet, SavedSet> MainSets;
        public DeckObservablePile<Phase10CardInformation> Deck1 { get; set; }
        public SingleObservablePile<Phase10CardInformation> Pile1 { get; set; }
        public HandObservable<Phase10CardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<Phase10CardInformation>? OtherPile { get; set; }
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
        private string _currentPhase = "";
        [VM]
        public string CurrentPhase
        {
            get { return _currentPhase; }
            set
            {
                if (SetProperty(ref _currentPhase, value))
                {
                    
                }
            }
        }
    }
}