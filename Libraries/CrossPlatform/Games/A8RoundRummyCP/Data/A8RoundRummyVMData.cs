using A8RoundRummyCP.Cards;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace A8RoundRummyCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class A8RoundRummyVMData : ObservableObject, IBasicCardGamesData<A8RoundRummyCardInformation>
    {
        public A8RoundRummyVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<A8RoundRummyCardInformation>(command);
            Pile1 = new SingleObservablePile<A8RoundRummyCardInformation>(command);
            PlayerHand1 = new HandObservable<A8RoundRummyCardInformation>(command);
            PlayerHand1.Maximum = 8;
        }
        public DeckObservablePile<A8RoundRummyCardInformation> Deck1 { get; set; }
        public SingleObservablePile<A8RoundRummyCardInformation> Pile1 { get; set; }
        public HandObservable<A8RoundRummyCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<A8RoundRummyCardInformation>? OtherPile { get; set; }
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
        private string _nextTurn = "";
        [VM]
        public string NextTurn
        {
            get { return _nextTurn; }
            set
            {
                if (SetProperty(ref _nextTurn, value))
                {
                    
                }
            }
        }
    }
}