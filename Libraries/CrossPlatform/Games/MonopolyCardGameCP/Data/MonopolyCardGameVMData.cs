using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using MonopolyCardGameCP.Cards;
using MonopolyCardGameCP.ViewModels;
namespace MonopolyCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class MonopolyCardGameVMData : ObservableObject, IBasicCardGamesData<MonopolyCardGameCardInformation>
    {
        public MonopolyCardGameVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<MonopolyCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<MonopolyCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<MonopolyCardGameCardInformation>(command);
            AdditionalInfo1 = new DetailCardViewModel();

        }
        public DetailCardViewModel AdditionalInfo1;

        public DeckObservablePile<MonopolyCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<MonopolyCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<MonopolyCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<MonopolyCardGameCardInformation>? OtherPile { get; set; }
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