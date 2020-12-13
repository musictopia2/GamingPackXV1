using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using ClueBoardGameCP.Cards;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace ClueBoardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class ClueBoardGameVMData : ObservableObject, IDiceBoardGamesData
    {
        private readonly CommandContainer _command;
        private readonly IGamePackageResolver _resolver;
        public HandObservable<CardInfo> HandList;
        public SingleObservablePile<CardInfo> Pile;
        public ClueBoardGameVMData(CommandContainer command, IGamePackageResolver resolver, IEventAggregator aggregator)
        {
            _command = command;
            _resolver = resolver;
            HandList = new HandObservable<CardInfo>(command);
            HandList.AutoSelect = EnumHandAutoType.None;
            HandList.Maximum = 3;
            HandList.Text = "Your Cards";
            Pile = new SingleObservablePile<CardInfo>(command);
            Pile.CurrentOnly = true;
            Pile.Text = "Clue";
        }
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
        private int _leftToMove;
        [VM]
        public int LeftToMove
        {
            get { return _leftToMove; }
            set
            {
                if (SetProperty(ref _leftToMove, value))
                {
                    
                }
            }
        }
        private string _currentRoomName = "";
        [VM]
        public string CurrentRoomName
        {
            get { return _currentRoomName; }
            set
            {
                if (SetProperty(ref _currentRoomName, value))
                {
                    
                }
            }
        }
        private string _currentCharacterName = "";
        [VM]
        public string CurrentCharacterName
        {
            get { return _currentCharacterName; }
            set
            {
                if (SetProperty(ref _currentCharacterName, value))
                {
                    
                }
            }
        }
        private string _currentWeaponName = "";
        [VM]
        public string CurrentWeaponName
        {
            get { return _currentWeaponName; }
            set
            {
                if (SetProperty(ref _currentWeaponName, value))
                {

                }
            }
        }
        public DiceCup<SimpleDice>? Cup { get; set; }
        public void LoadCup(ISavedDiceList<SimpleDice> saveRoot, bool autoResume)
        {
            if (Cup != null && autoResume)
            {
                return;
            }
            Cup = new DiceCup<SimpleDice>(saveRoot.DiceList, _resolver, _command);
            if (autoResume == true)
            {
                Cup.CanShowDice = true;
            }
            Cup.HowManyDice = 1;
            Cup.Visible = true;
        }
    }
}