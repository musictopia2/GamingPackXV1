using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using SorryCardGameCP.Cards;
namespace SorryCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class SorryCardGameVMData : ObservableObject, IBasicCardGamesData<SorryCardGameCardInformation>
    {
        public SorryCardGameVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<SorryCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<SorryCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<SorryCardGameCardInformation>(command);
            OtherPile = new SingleObservablePile<SorryCardGameCardInformation>(command);
            OtherPile.Text = "Play Pile";
            OtherPile.FirstLoad(new SorryCardGameCardInformation());
        }
        public DeckObservablePile<SorryCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<SorryCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<SorryCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<SorryCardGameCardInformation>? OtherPile { get; set; }
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
        private int _upTo;
        [VM]
        public int UpTo
        {
            get { return _upTo; }
            set
            {
                if (SetProperty(ref _upTo, value))
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