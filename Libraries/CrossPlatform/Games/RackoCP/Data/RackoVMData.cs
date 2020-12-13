using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using RackoCP.Cards;
namespace RackoCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class RackoVMData : ObservableObject, IBasicCardGamesData<RackoCardInformation>
    {
        public RackoVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<RackoCardInformation>(command);
            Pile1 = new SingleObservablePile<RackoCardInformation>(command);
            PlayerHand1 = new HandObservable<RackoCardInformation>(command);
            OtherPile = new SingleObservablePile<RackoCardInformation>(command);
            OtherPile.Text = "Current Card";
            OtherPile.CurrentOnly = true;
            OtherPile.FirstLoad(new RackoCardInformation());
        }
        public DeckObservablePile<RackoCardInformation> Deck1 { get; set; }
        public SingleObservablePile<RackoCardInformation> Pile1 { get; set; }
        public HandObservable<RackoCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<RackoCardInformation>? OtherPile { get; set; }
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