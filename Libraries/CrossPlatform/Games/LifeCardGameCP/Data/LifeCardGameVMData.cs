using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using LifeCardGameCP.Cards;
namespace LifeCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class LifeCardGameVMData : ObservableObject, IBasicCardGamesData<LifeCardGameCardInformation>
    {
        public LifeCardGameVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<LifeCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<LifeCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<LifeCardGameCardInformation>(command);
            CurrentPile = new SingleObservablePile<LifeCardGameCardInformation>(command);
            CurrentPile.Text = "Current Card";
            OtherPile = CurrentPile;
        }
        public SingleObservablePile<LifeCardGameCardInformation> CurrentPile;
        public DeckObservablePile<LifeCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<LifeCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<LifeCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<LifeCardGameCardInformation>? OtherPile { get; set; }
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
        public string OtherText { get; set; } = "";
    }
}