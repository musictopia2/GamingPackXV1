using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using UnoCP.Cards;
namespace UnoCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class UnoVMData : ObservableObject, IBasicCardGamesData<UnoCardInformation>
    {
        public UnoVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<UnoCardInformation>(command);
            Pile1 = new SingleObservablePile<UnoCardInformation>(command);
            PlayerHand1 = new HandObservable<UnoCardInformation>(command);
            PlayerHand1.Text = "Your Cards";
            ColorPicker = new SimpleEnumPickerVM<EnumColorTypes>(command, new ColorListChooser<EnumColorTypes>());
            Stops = new CustomStopWatchCP();
            Stops.MaxTime = 3000;
            ColorPicker.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
        }
        public DeckObservablePile<UnoCardInformation> Deck1 { get; set; }
        public SingleObservablePile<UnoCardInformation> Pile1 { get; set; }
        public HandObservable<UnoCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<UnoCardInformation>? OtherPile { get; set; }

        public SimpleEnumPickerVM<EnumColorTypes> ColorPicker;
        public CustomStopWatchCP Stops;

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
        private string _nextPlayer = "";
        [VM]
        public string NextPlayer
        {
            get
            {
                return _nextPlayer;
            }
            set
            {
                if (SetProperty(ref _nextPlayer, value) == true)
                {
                }
            }
        }
    }
}