using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace OldMaidCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class OldMaidVMData : ObservableObject, IBasicCardGamesData<RegularSimpleCard>
    {
        public OldMaidVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<RegularSimpleCard>(command);
            Pile1 = new SingleObservablePile<RegularSimpleCard>(command);
            PlayerHand1 = new HandObservable<RegularSimpleCard>(command);
            OpponentCards1 = new HandObservable<RegularSimpleCard>(command);
            OpponentCards1.Text = "Opponent Cards";
            OpponentCards1.AutoSelect = EnumHandAutoType.None;
        }
        public DeckObservablePile<RegularSimpleCard> Deck1 { get; set; }
        public SingleObservablePile<RegularSimpleCard> Pile1 { get; set; }
        public HandObservable<RegularSimpleCard> PlayerHand1 { get; set; }
        public SingleObservablePile<RegularSimpleCard>? OtherPile { get; set; }

        public HandObservable<RegularSimpleCard> OpponentCards1;

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