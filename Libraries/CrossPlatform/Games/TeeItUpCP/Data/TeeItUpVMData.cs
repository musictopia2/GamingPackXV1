using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using TeeItUpCP.Cards;
namespace TeeItUpCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class TeeItUpVMData : ObservableObject, IBasicCardGamesData<TeeItUpCardInformation>
    {
        public TeeItUpVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<TeeItUpCardInformation>(command);
            Pile1 = new SingleObservablePile<TeeItUpCardInformation>(command);
            PlayerHand1 = new HandObservable<TeeItUpCardInformation>(command);
            OtherPile = new SingleObservablePile<TeeItUpCardInformation>(command);
            OtherPile.CurrentOnly = true;
            OtherPile.Text = "Current Card";
            OtherPile.FirstLoad(new TeeItUpCardInformation());
            PlayerHand1.Visible = false; //try this too.
        }
        public DeckObservablePile<TeeItUpCardInformation> Deck1 { get; set; }
        public SingleObservablePile<TeeItUpCardInformation> Pile1 { get; set; }
        public HandObservable<TeeItUpCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<TeeItUpCardInformation>? OtherPile { get; set; }
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