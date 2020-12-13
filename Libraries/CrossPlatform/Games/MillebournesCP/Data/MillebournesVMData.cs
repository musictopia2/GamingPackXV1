using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using MillebournesCP.Cards;
namespace MillebournesCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class MillebournesVMData : ObservableObject, IBasicCardGamesData<MillebournesCardInformation>
    {
        public MillebournesVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<MillebournesCardInformation>(command);
            Pile1 = new SingleObservablePile<MillebournesCardInformation>(command);
            PlayerHand1 = new HandObservable<MillebournesCardInformation>(command);
            OtherPile = Pile1;
            Pile2 = new SingleObservablePile<MillebournesCardInformation>(command);

            Stops = new CustomStopWatchCP();
            Stops.MaxTime = 3000;
        }
        public CustomStopWatchCP Stops;
        public DeckObservablePile<MillebournesCardInformation> Deck1 { get; set; }
        public SingleObservablePile<MillebournesCardInformation> Pile1 { get; set; }
        public SingleObservablePile<MillebournesCardInformation> Pile2 { get; set; }
        public HandObservable<MillebournesCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<MillebournesCardInformation>? OtherPile { get; set; }
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
        public bool CoupeVisible { get; set; }
    }
}