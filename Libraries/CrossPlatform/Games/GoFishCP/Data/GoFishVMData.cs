using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using GoFishCP.Logic;
namespace GoFishCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class GoFishVMData : ObservableObject, IBasicCardGamesData<RegularSimpleCard>
    {
        public GoFishVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<RegularSimpleCard>(command);
            Pile1 = new SingleObservablePile<RegularSimpleCard>(command);
            PlayerHand1 = new HandObservable<RegularSimpleCard>(command);
            AskList = new GoFishChooserCP(command);
            AskList.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent; //i think.
        }
        public DeckObservablePile<RegularSimpleCard> Deck1 { get; set; }
        public SingleObservablePile<RegularSimpleCard> Pile1 { get; set; }
        public HandObservable<RegularSimpleCard> PlayerHand1 { get; set; }
        public SingleObservablePile<RegularSimpleCard>? OtherPile { get; set; }

        public GoFishChooserCP AskList;
        public EnumRegularCardValueList CardYouAsked { get; set; } //decided to risk no property changed.  since we are using blazor now.

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