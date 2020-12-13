using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using SinisterSixCP.Data;
using SinisterSixCP.Logic;
using System.Linq;
using System.Threading.Tasks;
namespace SinisterSixCP.ViewModels
{
    [InstanceGame]
    public class SinisterSixMainViewModel : DiceGamesVM<EightSidedDice>
    {
        private readonly SinisterSixMainGameClass _mainGame;
        private readonly SinisterSixVMData _model;
        public SinisterSixMainViewModel(CommandContainer commandContainer,
            SinisterSixMainGameClass mainGame,
            SinisterSixVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = viewModel;
        }
        protected override bool NeedsRollIncrement => false;
        public DiceCup<EightSidedDice> GetCup => _model.Cup!;
        public PlayerCollection<SinisterSixPlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return true;
        }
        private bool CanRemoveSelectedDice()
        {
            var thisList = _model.Cup!.DiceList.GetSelectedItems();
            return thisList.Sum(Items => Items.Value) == 6;
        }
        public override bool CanEndTurn()
        {
            return RollNumber > 0;
        }
        public override bool CanRollDice()
        {
            return _mainGame!.SaveRoot!.RollNumber <= _mainGame.SaveRoot.MaxRolls;
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
        public bool CanRemoveDice => CanEndTurn();
        [Command(EnumCommandCategory.Game)]
        public async Task RemoveDiceAsync()
        {
            if (CanRemoveSelectedDice() == false)
            {
                await UIPlatform.ShowMessageAsync("Cannot remove dice that does not equal 6");
                return;
            };
            await _mainGame.RemoveSelectedDiceAsync();
        }
    }
}