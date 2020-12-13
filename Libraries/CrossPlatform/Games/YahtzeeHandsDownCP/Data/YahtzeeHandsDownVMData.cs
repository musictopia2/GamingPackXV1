using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using YahtzeeHandsDownCP.Cards;
namespace YahtzeeHandsDownCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class YahtzeeHandsDownVMData : ObservableObject, IBasicCardGamesData<YahtzeeHandsDownCardInformation>
    {
        public YahtzeeHandsDownVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<YahtzeeHandsDownCardInformation>(command);
            Pile1 = new SingleObservablePile<YahtzeeHandsDownCardInformation>(command);
            PlayerHand1 = new HandObservable<YahtzeeHandsDownCardInformation>(command);
            ComboHandList = new HandObservable<ComboCardInfo>(command);
            ComboHandList.Text = "Category Cards";
            ChancePile = new SingleObservablePile<ChanceCardInfo>(command);
            ChancePile.Visible = false;
            ChancePile.CurrentOnly = true;
            ChancePile.Text = "Chance";
        }
        public HandObservable<ComboCardInfo>? ComboHandList;
        public SingleObservablePile<ChanceCardInfo>? ChancePile;
        public DeckObservablePile<YahtzeeHandsDownCardInformation> Deck1 { get; set; }
        public SingleObservablePile<YahtzeeHandsDownCardInformation> Pile1 { get; set; }
        public HandObservable<YahtzeeHandsDownCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<YahtzeeHandsDownCardInformation>? OtherPile { get; set; }
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