using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace SnakesAndLaddersCP.Data
{
    [SingletonGame]
    public class SnakesAndLaddersVMData : ObservableObject, IViewModelData, ICup<SimpleDice>, IBasicEnableProcess
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
        private readonly CommandContainer _command;
        private readonly IGamePackageResolver _resolver;
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
        public DiceCup<SimpleDice>? Cup { get; set; }
        public SnakesAndLaddersVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            _command = command;
            _resolver = resolver;
        }
        public void LoadCup(SnakesAndLaddersSaveInfo saveRoot, bool autoResume)
        {
            Cup = new DiceCup<SimpleDice>(saveRoot.DiceList, _resolver, _command);
            Cup.SendEnableProcesses(this, () =>
            {
                return false; //because you can't click the dice.
            });
            Cup.HowManyDice = 1;
            if (autoResume == true && saveRoot.HasRolled == true)
            {
                Cup.CanShowDice = true;
                Cup.Visible = true;
            }
        }
        bool IBasicEnableProcess.CanEnableBasics()
        {
            return false;
        }
    }
}