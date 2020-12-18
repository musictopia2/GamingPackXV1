using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using ConnectTheDotsCP.Data;
using System.Threading.Tasks;
namespace ConnectTheDotsCP.Logic
{
    [SingletonGame]
    public class ConnectTheDotsMainGameClass
        : SimpleBoardGameClass<ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo, EnumColorChoice, int>
    {
        public ConnectTheDotsMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            ConnectTheDotsVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            ConnectTheDotsGameContainer container,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container)
        {
            _command = command;
            _gameBoard = gameBoard;
            container.MakeMoveAsync = PrivateMoveAsync;
        }
        private async Task PrivateMoveAsync(int dot)
        {
            if (_gameBoard.IsValidMove(dot) == false)
            {
                ToastPlatform.ShowError("Illegal Move");
                _command.StopExecuting();
                return;
            }
            if (BasicData.MultiPlayer)
            {
                await Network!.SendMoveAsync(dot);
            }
            _command.StartExecuting();
            await _gameBoard.MakeMoveAsync(dot);
        }
        private readonly CommandContainer _command;
        private readonly GameBoardProcesses _gameBoard;
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            BoardGameSaved();
            if (PlayerList.DidChooseColors())
            {
                _gameBoard.LoadGame();
            }
            return Task.CompletedTask;
        }
        public override Task PopulateSaveRootAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                _gameBoard.SaveGame();
            }
            return base.PopulateSaveRootAsync();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
                return;

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
            SaveRoot!.ImmediatelyStartTurn = true;
            await FinishUpAsync(isBeginning);
        }
        public override async Task StartNewTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                PrepStartTurn();
            }
            await ContinueTurnAsync();
        }
        public override async Task MakeMoveAsync(int space)
        {
            await _gameBoard.MakeMoveAsync(space);
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            if (PlayerList.DidChooseColors())
            {
                _command.ManuelFinish = true;
            }
            await StartNewTurnAsync();
        }
        public override async Task AfterChoosingColorsAsync()
        {
            _gameBoard.ClearBoard();
            await EndTurnAsync();
        }
    }
}