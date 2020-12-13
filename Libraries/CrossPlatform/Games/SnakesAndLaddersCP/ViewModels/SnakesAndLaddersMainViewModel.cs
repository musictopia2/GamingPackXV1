using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using SnakesAndLaddersCP.Data;
using SnakesAndLaddersCP.Logic;
using System.Threading.Tasks;
namespace SnakesAndLaddersCP.ViewModels
{
    [InstanceGame]
    public class SnakesAndLaddersMainViewModel : BasicMultiplayerMainVM
    {
        private readonly SnakesAndLaddersMainGameClass _mainGame;
        private readonly SnakesAndLaddersVMData _viewModel;
        public SnakesAndLaddersMainViewModel(CommandContainer commandContainer,
            SnakesAndLaddersMainGameClass mainGame,
            SnakesAndLaddersVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _viewModel = viewModel;
        }
        public CustomBasicList<SnakesAndLaddersPlayerItem> GetPlayerList
        {
            get
            {
                CustomBasicList<SnakesAndLaddersPlayerItem> output = _mainGame.PlayerList.ToCustomBasicList();
                output.RemoveSpecificItem(_mainGame.SingleInfo!);
                output.Add(_mainGame.SingleInfo!);
                return output;
            }
        }
        public SnakesAndLaddersPlayerItem CurrentPlayer => _mainGame.SingleInfo!;
        public DiceCup<SimpleDice> GetCup => _viewModel.Cup!;
        public bool CanRollDice => !_mainGame.SaveRoot.HasRolled;
        [Command(EnumCommandCategory.Game)]
        public async Task RollDiceAsync()
        {
            await _mainGame.Roll.RollDiceAsync();
        }
        public bool CanMakeMove(int space)
        {
            if (space == 0)
            {
                return false;
            }
            return _mainGame.SaveRoot.HasRolled;
        }

        [Command(EnumCommandCategory.Game)]
        public async Task MakeMoveAsync(int space)
        {
            if (_mainGame.GameBoard1.IsValidMove(space) == false)
            {
                ToastPlatform.ShowError("Illegal Move");
                return;
            }
            await _mainGame.MakeMoveAsync(space);
        }
    }
}