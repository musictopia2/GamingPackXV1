using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using MilkRunCP.Cards;
namespace MilkRunCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class MilkRunVMData : ObservableObject, IBasicCardGamesData<MilkRunCardInformation>
    {
        public MilkRunVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<MilkRunCardInformation>(command);
            Pile1 = new SingleObservablePile<MilkRunCardInformation>(command);
            PlayerHand1 = new HandObservable<MilkRunCardInformation>(command);
        }
        public DeckObservablePile<MilkRunCardInformation> Deck1 { get; set; }
        public SingleObservablePile<MilkRunCardInformation> Pile1 { get; set; }
        public HandObservable<MilkRunCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<MilkRunCardInformation>? OtherPile { get; set; }
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