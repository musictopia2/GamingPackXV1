using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using ChineseCheckersCP.Data;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks;
namespace ChineseCheckersCP.Logic
{
    [SingletonGame]
    public class ChineseCheckersMainGameClass
        : SimpleBoardGameClass<ChineseCheckersPlayerItem, ChineseCheckersSaveInfo, EnumColorChoice, int>, IMiscDataNM
    {
        public ChineseCheckersMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            ChineseCheckersVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            ChineseCheckersGameContainer container,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container)
        {
            _gameContainer = container;
            _gameBoard = gameBoard;
            _gameContainer.Model = model;
            _gameContainer.MakeMoveAsync = PrivateMoveAsync;
            SaveRoot.Init(_gameContainer);
        }

        private void EnableControls()
        {
            _gameContainer.Command.StopExecuting(); //i think.
        }
        private async Task PrivateMoveAsync(int space)
        {
            if (SaveRoot!.PreviousSpace == space && _gameBoard.WillContinueTurn() == false)
            {
                SaveRoot.Instructions = "Choose a piece to move";
                if (BasicData!.MultiPlayer == true)
                {
                    await Network!.SendAllAsync("undomove");
                }
                await _gameBoard.UnselectPieceAsync();
                return;
            }
            else if (SaveRoot.PreviousSpace == space)
            {
                EnableControls();
                return;
            }
            if (_gameBoard!.IsValidMove(space) == false)
            {
                EnableControls();
                return;
            }
            if (SaveRoot.PreviousSpace == 0)
            {
                if (SingleInfo!.PieceList.Any(Items => Items == space) == false)
                {
                    EnableControls();
                    return;
                }
                SaveRoot.Instructions = "Select where to move to";
                if (BasicData!.MultiPlayer == true)
                {
                    await Network!.SendAllAsync("pieceselected", space);
                }
                await _gameBoard.HighlightItemAsync(space);
                return;
            }
            SaveRoot.Instructions = "Making Move";
            if (BasicData!.MultiPlayer == true)
            {
                await Network!.SendMoveAsync(space);
            }
            _gameContainer.Command.StartExecuting(); //i think.
            await MakeMoveAsync(space);
        }
        private readonly ChineseCheckersGameContainer _gameContainer;
        private readonly GameBoardProcesses _gameBoard;
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            BoardGameSaved();
            SaveRoot.Init(_gameContainer);
            _gameBoard.LoadSavedGame();
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
            SaveRoot!.ImmediatelyStartTurn = true;
            await FinishUpAsync(isBeginning);
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "undomove":
                    await _gameBoard.UnselectPieceAsync();
                    return;
                case "pieceselected":
                    await _gameBoard.HighlightItemAsync(int.Parse(content));
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                PrepStartTurn();
                _gameBoard.StartTurn();
                if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Self)
                {
                    SaveRoot!.Instructions = "Choose a piece to move";
                }
                else
                {
                    SaveRoot!.Instructions = $"Waitng for {SingleInfo.NickName} to take their turn";
                }
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
                _gameContainer.Command.ManuelFinish = true; //i think in this case, yes.
            }
            await StartNewTurnAsync();
        }

        public override async Task AfterChoosingColorsAsync()
        {
            _gameBoard.ClearBoard(); //i think here.
            await EndTurnAsync();
        }
    }
}