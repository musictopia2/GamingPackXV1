using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using GolfCardGameCP.CustomPiles;
namespace GolfCardGameCP.Data
{
    [SingletonGame]
    [AutoReset] 
    public class GolfCardGameVMData : ObservableObject, IBasicCardGamesData<RegularSimpleCard>
    {
        public GolfCardGameVMData(CommandContainer command, GolfCardGameGameContainer gameContainer)
        {
            Deck1 = new DeckObservablePile<RegularSimpleCard>(command);
            Pile1 = new SingleObservablePile<RegularSimpleCard>(command);
            PlayerHand1 = new HandObservable<RegularSimpleCard>(command);
            OtherPile = new SingleObservablePile<RegularSimpleCard>(command);
            OtherPile.CurrentOnly = true;
            OtherPile.Text = "Current";
            HiddenCards1 = new HiddenCards(gameContainer);
            Beginnings1 = new Beginnings(command);
            GolfHand1 = new GolfHand(gameContainer);
        }
        public HiddenCards HiddenCards1;
        public Beginnings Beginnings1;
        public GolfHand GolfHand1;
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
        private int _round;
        [VM]
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value))
                {
                    
                }
            }
        }
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    
                }
            }
        }
    }
}