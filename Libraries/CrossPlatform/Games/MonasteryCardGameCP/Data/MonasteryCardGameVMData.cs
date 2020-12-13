using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using MonasteryCardGameCP.Logic;
namespace MonasteryCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class MonasteryCardGameVMData : ObservableObject, IBasicCardGamesData<MonasteryCardInfo>
    {
        public MonasteryCardGameVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            Deck1 = new DeckObservablePile<MonasteryCardInfo>(command);
            Pile1 = new SingleObservablePile<MonasteryCardInfo>(command);
            PlayerHand1 = new HandObservable<MonasteryCardInfo>(command);
            PlayerHand1.Maximum = 10;
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, MonasteryCardInfo>(command, resolver);
            MainSets = new MainSetsObservable<EnumSuitList, EnumRegularColorList, MonasteryCardInfo, RummySet, SavedSet>(command);
            TempSets.HowManySets = 4;
        }
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, MonasteryCardInfo> TempSets;
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, MonasteryCardInfo, RummySet, SavedSet> MainSets;
        public DeckObservablePile<MonasteryCardInfo> Deck1 { get; set; }
        public SingleObservablePile<MonasteryCardInfo> Pile1 { get; set; }
        public HandObservable<MonasteryCardInfo> PlayerHand1 { get; set; }
        public SingleObservablePile<MonasteryCardInfo>? OtherPile { get; set; }

        public CustomBasicCollection<MissionList> CompleteMissions = new CustomBasicCollection<MissionList>();
        internal void PopulateMissions(CustomBasicList<MissionList> thisList)
        {
            MissionChosen = "";
            CompleteMissions.ReplaceRange(thisList);
        }
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
        private string _missionChosen = "";
        [VM]
        public string MissionChosen
        {
            get { return _missionChosen; }
            set
            {
                if (SetProperty(ref _missionChosen, value))
                {
                    
                }
            }
        }
    }
}