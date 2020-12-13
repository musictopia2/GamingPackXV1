using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace SinisterSixCP.Data
{
    [SingletonGame]
    public class SinisterSixVMData : ObservableObject, IBasicDiceGamesData<EightSidedDice>
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
        private int _rollNumber;
        private readonly IGamePackageResolver _resolver;
        private readonly CommandContainer _command;
        [VM]
        public int RollNumber
        {
            get { return _rollNumber; }
            set
            {
                if (SetProperty(ref _rollNumber, value))
                {
                    
                }
            }
        }
        public DiceCup<EightSidedDice>? Cup { get; set; }
        public SinisterSixVMData(IGamePackageResolver resolver, CommandContainer command)
        {
            _resolver = resolver;
            _command = command;
        }
        public void LoadCup(ISavedDiceList<EightSidedDice> saveRoot, bool autoResume)
        {
            if (Cup != null && autoResume)
            {
                return;
            }
            Cup = new DiceCup<EightSidedDice>(saveRoot.DiceList, _resolver, _command);
            if (autoResume == true)
            {
                Cup.CanShowDice = true;
            }
            Cup.HowManyDice = 6;
            Cup.Visible = true;
            Cup.ShowHold = false;
        }
        private int _maxRolls;
        [VM]
        public int MaxRolls
        {
            get { return _maxRolls; }
            set
            {
                if (SetProperty(ref _maxRolls, value))
                {
                    
                }
            }
        }
    }
}