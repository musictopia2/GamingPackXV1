using AggravationCP.Data;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Threading.Tasks;
namespace AggravationCP.Logic
{
    [SingletonGame]
    public class AggravationMainGameClass
        : BoardDiceGameClass<AggravationPlayerItem, AggravationSaveInfo, EnumColorChoice, int>
    {
        public AggravationMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            AggravationVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            AggravationGameContainer container,
            StandardRollProcesses<SimpleDice, AggravationPlayerItem> roller,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container, roller)
        {
            _model = model;
            _command = command;
            _gameBoard = gameBoard;
            container.MakeMoveAsync = HumanMoveAsync;
        }
        private readonly AggravationVMData _model;
        private readonly CommandContainer _command;
        private readonly GameBoardProcesses _gameBoard;
        private async Task HumanMoveAsync(int space)
        {
            if (BasicData.MultiPlayer)
            {
                await Network!.SendMoveAsync(space);
            }
            await MakeMoveAsync(space);
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            AfterRestoreDice();
            _gameBoard.LoadSavedGame();
            BoardGameSaved();
            SaveRoot.LoadMod(_model);
            _model.Cup!.CanShowDice = SaveRoot.DiceNumber > 0;
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            await Task.CompletedTask;
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            SetUpDice();
            SaveRoot.LoadMod(_model);
            SaveRoot!.ImmediatelyStartTurn = true;
            await FinishUpAsync(isBeginning);
        }
        public override async Task StartNewTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                PrepStartTurn();
                _gameBoard.StartTurn();
            }
            await ContinueTurnAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                if (Test!.DoubleCheck)
                {
                    Test.DoubleCheck = false;
                    await _gameBoard.GetValidMovesAsync(); //so i can see what is wrong.
                    return;
                }
            }
            await base.ContinueTurnAsync();
        }
        public override async Task MakeMoveAsync(int space)
        {
            await _gameBoard.MakeMoveAsync(space);
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            _command.ManuelFinish = true;
            if (PlayerList.DidChooseColors())
            {
                _gameBoard.StartTurn();
            }
            await StartNewTurnAsync();
        }
        public override async Task AfterChoosingColorsAsync()
        {
            _gameBoard.ClearBoard();
            await EndTurnAsync();
        }
        public override async Task AfterRollingAsync()
        {
            SaveRoot.DiceNumber = _model.Cup!.ValueOfOnlyDice;
            await _gameBoard.GetValidMovesAsync();
        }
    }
}