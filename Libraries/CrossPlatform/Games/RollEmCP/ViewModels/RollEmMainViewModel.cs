using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using RollEmCP.Data;
using RollEmCP.Logic;
using System.Threading.Tasks;
namespace RollEmCP.ViewModels
{
    [InstanceGame]
    public class RollEmMainViewModel : DiceGamesVM<SimpleDice>
    {
        private readonly RollEmMainGameClass _mainGame;
        private readonly RollEmVMData _model;
        private readonly GameBoardProcesses _gameBoard;
        private readonly BasicData _basicData;
        private readonly IEventAggregator _aggregator;
        public RollEmMainViewModel(CommandContainer commandContainer,
            RollEmMainGameClass mainGame,
            RollEmVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller,
            RollEmGameContainer gameContainer,
            IEventAggregator aggregator,
            GameBoardProcesses gameBoard
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _gameBoard = gameBoard;
            CommandContainer.ExecutingChanged += CommandContainer_ExecutingChanged;
            gameContainer.MakeMoveAsync = MakeMoveAsync;
            _basicData = basicData;
            _aggregator = aggregator;
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public PlayerCollection<RollEmPlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return false;
        }
        public override bool CanEndTurn()
        {
            return _mainGame!.SaveRoot!.GameStatus != EnumStatusList.NeedRoll;
        }
        public override bool CanRollDice()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumStatusList.NeedRoll;
        }

        private async Task MakeMoveAsync(int space)
        {
            if (_gameBoard.CanMakeMove(space) == false)
            {
                if (_gameBoard.HadRecent)
                {
                    if (_basicData.MultiPlayer)
                    {
                        await _mainGame.Network!.SendAllAsync("clearrecent");
                    }
                    await UIPlatform.ShowMessageAsync("Illegal Move");
                    _gameBoard.ClearRecent(true);
                    await _mainGame.ContinueTurnAsync();
                }
                return;
            }
            await _mainGame.MakeMoveAsync(space);
        }
        private void CommandContainer_ExecutingChanged()
        {
            _aggregator.RepaintBoard();
        }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        private int _round;
        [VM]
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value))
                {
                    
                }
            }
        }
    }
}