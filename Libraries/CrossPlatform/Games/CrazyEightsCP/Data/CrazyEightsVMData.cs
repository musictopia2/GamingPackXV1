using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace CrazyEightsCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class CrazyEightsVMData : ObservableObject, IBasicCardGamesData<RegularSimpleCard>
    {
        public SimpleEnumPickerVM<EnumSuitList> SuitChooser;
        public CrazyEightsVMData(IEventAggregator aggregator, CommandContainer command)
        {
            Deck1 = new DeckObservablePile<RegularSimpleCard>(command);
            Pile1 = new SingleObservablePile<RegularSimpleCard>(command);
            PlayerHand1 = new HandObservable<RegularSimpleCard>(command);
            SuitChooser = new SimpleEnumPickerVM<EnumSuitList>(command, new SuitListChooser());
            SuitChooser.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
        }
        public DeckObservablePile<RegularSimpleCard> Deck1 { get; set; }
        public SingleObservablePile<RegularSimpleCard> Pile1 { get; set; }
        public HandObservable<RegularSimpleCard> PlayerHand1 { get; set; }
        public SingleObservablePile<RegularSimpleCard>? OtherPile { get; set; }
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
        private bool _chooseSuit;
        [VM]
        public bool ChooseSuit
        {
            get { return _chooseSuit; }
            set
            {
                if (SetProperty(ref _chooseSuit, value))
                {
                    
                }
            }
        }
    }
}