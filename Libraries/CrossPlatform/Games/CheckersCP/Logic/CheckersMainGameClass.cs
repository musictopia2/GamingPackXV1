using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
using CheckersCP.Data;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks;
namespace CheckersCP.Logic
{
    [SingletonGame]
    public class CheckersMainGameClass
        : SimpleBoardGameClass<CheckersPlayerItem, CheckersSaveInfo, EnumColorChoice, int>, IMiscDataNM
    {
        public CheckersMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            CheckersVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            CheckersGameContainer container,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container)
        {
            _model = model;
            _container = container;
            _gameBoard = gameBoard;
            CheckersChessDelegates.CanMove = CanMove;
            CheckersChessDelegates.MakeMoveAsync = PrivateMoveAsync;
        }
        private bool CanMove()
        {
            return SaveRoot.GameStatus == EnumGameStatus.None; //i think this simple this time.
        }
        private async Task PrivateMoveAsync(int space)
        {
            if (_gameBoard.IsValidMove(space) == false)
            {
                return;
            }
            if (BasicData.MultiPlayer)
            {
                await Network!.SendMoveAsync(GameBoardGraphicsCP.GetRealIndex(space, true));
            }
            _container.Command.ManuelFinish = true;
            await _gameBoard.MakeMoveAsync(space);
        }
        private readonly CheckersVMData _model;
        private readonly CheckersGameContainer _container;
        private readonly GameBoardProcesses _gameBoard;
        public override async Task FinishGetSavedAsync()
        {
            LoadControls();
            if (PlayerList.DidChooseColors())
            {
                await _gameBoard.LoadPreviousGameAsync();
            }
            BoardGameSaved();
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

        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "possibletie":
                    await ProcessTieAsync();
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
                await _gameBoard.StartNewTurnAsync();
                return;
            }
            await ContinueTurnAsync(); //most of the time, continue turn.  can change to what is needed
        }
        public override Task ContinueTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                if (SaveRoot.GameStatus == EnumGameStatus.PossibleTie)
                {
                    _model.Instructions = "Either Agree To Tie Or End Turn";
                }
                else if (SaveRoot.SpaceHighlighted == 0)
                {
                    _model.Instructions = "Make Move Or Initiate Tie";
                }
                else
                {
                    _model.Instructions = "Finish Move";
                }
            }
            return base.ContinueTurnAsync();
        }
        public override async Task MakeMoveAsync(int space)
        {
            await _gameBoard.MakeMoveAsync(space);
        }
        public override async Task EndTurnAsync()
        {
            PlayerList!.ForEach(thisPlayer => thisPlayer.PossibleTie = false);
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        public override async Task AfterChoosingColorsAsync()
        {
            SaveRoot!.GameStatus = EnumGameStatus.None;
            _gameBoard.ClearBoard();
            await EndTurnAsync();
        }
        public async Task ProcessTieAsync()
        {
            SingleInfo!.PossibleTie = true;
            if (PlayerList.Any(items => items.PossibleTie == false))
            {
                WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
                SaveRoot!.GameStatus = EnumGameStatus.PossibleTie;
                SingleInfo = PlayerList.GetWhoPlayer();
                PrepStartTurn();
                await ContinueTurnAsync();
                return;
            }
            await ShowTieAsync();
        }
    }
}