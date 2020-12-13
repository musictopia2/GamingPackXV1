using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using LifeBoardGameCP.Cards;
using System.Threading.Tasks;
using static BasicGameFrameworkLibrary.ChooserClasses.ListViewPicker;
namespace LifeBoardGameCP.Data
{
    [SingletonGame]
    public class LifeBoardGameVMData : ObservableObject, ISimpleBoardGamesData, IEnableAlways
    {
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
        private int _currentSelected; //this is the space you selected so far when you have a choice between 2 spaces.
        [VM]
        public int CurrentSelected
        {
            get { return _currentSelected; }
            set
            {
                if (SetProperty(ref _currentSelected, value))
                {
                    
                }
            }
        }
        private EnumWhatStatus _gameStatus = EnumWhatStatus.None;
        [VM]
        public EnumWhatStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {

                }
            }
        }
        private string _gameDetails = "";
        [VM]
        public string GameDetails
        {
            get { return _gameDetails; }
            set
            {
                if (SetProperty(ref _gameDetails, value))
                {
                    
                }
            }
        }
        public HandObservable<LifeBaseCard> HandList;
        public SimpleEnumPickerVM<EnumGender> GenderChooser { get; set; }
        public string PlayerChosen { get; set; } = "";
        public ListViewPicker PlayerPicker { get; set; }
        public SingleObservablePile<LifeBaseCard> SinglePile { get; set; }
        public LifeBoardGameVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            HandList = new HandObservable<LifeBaseCard>(command);
            GenderChooser = new SimpleEnumPickerVM<EnumGender>(command, new ColorListChooser<EnumGender>());
            GenderChooser.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
            PlayerPicker = new ListViewPicker(command, resolver);
            PlayerPicker.IndexMethod = EnumIndexMethod.OneBased;
            PlayerPicker.ItemSelectedAsync += PlayerPicker_ItemSelectedAsync;
            SinglePile = new SingleObservablePile<LifeBaseCard>(command);
            SinglePile.SendAlwaysEnable(this); //hopefuly this simple.
            SinglePile.Text = "Card";
            SinglePile.CurrentOnly = true;
            HandList.Text = "None";
            HandList.IgnoreMaxRules = true;
        }
        private Task PlayerPicker_ItemSelectedAsync(int SelectedIndex, string SelectedText)
        {
            PlayerChosen = SelectedText;
            return Task.CompletedTask;
        }
        public int GetRandomCard => HandList.HandList.GetRandomItem().Deck;
        bool IEnableAlways.CanEnableAlways()
        {
            return false;
        }
    }
}