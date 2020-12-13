using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BackgammonCP.Data
{
    [SingletonGame]
    public class BackgammonVMData : ObservableObject, IDiceBoardGamesData
    {
        private readonly CommandContainer _command;
        private readonly IGamePackageResolver _resolver;
        public BackgammonVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            _command = command;
            _resolver = resolver;
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
        private int _movesMade;
        [VM]
        public int MovesMade
        {
            get { return _movesMade; }
            set
            {
                if (SetProperty(ref _movesMade, value))
                {
                    
                }
            }
        }
        private string _lastStatus = "";
        [VM]
        public string LastStatus
        {
            get { return _lastStatus; }
            set
            {
                if (SetProperty(ref _lastStatus, value))
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
            Cup.HowManyDice = 2;
            Cup.Visible = true;
        }
    }
}