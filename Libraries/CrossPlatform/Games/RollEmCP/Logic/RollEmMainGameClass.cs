using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using RollEmCP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace RollEmCP.Logic
{
    [SingletonGame]
    public class RollEmMainGameClass : DiceGameClass<SimpleDice, RollEmPlayerItem, RollEmSaveInfo>, IMiscDataNM, IMoveNM
    {
        private readonly RollEmVMData _model;
        private readonly RollEmGameContainer _gameContainer;
        private readonly StandardRollProcesses<SimpleDice, RollEmPlayerItem> _roller;
        private readonly GameBoardProcesses _gameBoard;
        public RollEmMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            RollEmVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            RollEmGameContainer gameContainer,
            StandardRollProcesses<SimpleDice, RollEmPlayerItem> roller,
            GameBoardProcesses gameBoard

            ) :
            base(mainContainer, aggregator, basicData, test, currentMod, state, delay, command, gameContainer, roller)
        {
            _model = currentMod;
            _gameContainer = gameContainer;
            _roller = roller;
            _gameBoard = gameBoard;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            AfterRestoreDice();
            SaveRoot!.LoadMod(_model);
            _gameBoard.LoadSavedGame();
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            LoadMod();
            GameBoardGraphicsCP.CreateNumberList(_gameContainer);
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            if (SaveRoot!.GameStatus == EnumStatusList.NeedRoll)
            {
                await _roller.RollDiceAsync();
                return;
            }
            var thisList = ComputerAI.NumberList(_gameBoard);
            if (thisList.Count == 0)
            {
                if (BasicData!.MultiPlayer == true)
                {
                    await Network!.SendEndTurnAsync();
                }
                await EndTurnAsync();
                return;
            }
            await thisList.ForEachAsync(async thisItem =>
            {
                await MakeMoveAsync(thisItem);
            });
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            SetUpDice();
            SaveRoot!.LoadMod(_model);
            SaveRoot.Round = 1;
            SaveRoot.GameStatus = EnumStatusList.NeedRoll;
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.ScoreGame = 0;
                thisPlayer.ScoreRound = 0;
            });
            await FinishUpAsync(isBeginning);
        }
        internal async Task MakeMoveAsync(int Space)
        {
            if (SingleInfo!.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendMoveAsync(Space);
            }
            await _gameBoard.MakeMoveAsync(Space);
            bool rets = _gameBoard.IsMoveFinished();
            if (rets == false)
            {
                if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Computer)
                {
                    if (BasicData!.MultiPlayer == false)
                    {
                        return;
                    }
                    if (BasicData.Client == true)
                    {
                        Check!.IsEnabled = true;
                        return;
                    }
                }
                await ContinueTurnAsync();
                return;
            }
            _gameBoard.FinishMove();
            SaveRoot!.GameStatus = EnumStatusList.NeedRoll;
            await ContinueTurnAsync();
        }
        public override Task PopulateSaveRootAsync()
        {
            _gameBoard.SaveGame();
            return base.PopulateSaveRootAsync();
        }
        private async Task GameOverAsync()
        {
            SingleInfo = PlayerList.OrderBy(x => x.ScoreGame).First();
            await ShowWinAsync();
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "clearrecent":
                    _gameBoard.ClearRecent(true);
                    await ContinueTurnAsync();
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            _gameBoard.ClearBoard();
            this.ShowTurn();
            SaveRoot!.GameStatus = EnumStatusList.NeedRoll;
            await ContinueTurnAsync();
        }
        protected override async Task ProtectedAfterRollingAsync()
        {
            SaveRoot!.GameStatus = EnumStatusList.ChooseNumbers;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            int score = _gameBoard.CalculateScore;
            SingleInfo!.ScoreRound = score;
            SingleInfo.ScoreGame += score;
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            if (WhoTurn == WhoStarts)
            {
                if (SaveRoot!.Round == 5 || Test!.ImmediatelyEndGame)
                {
                    _gameBoard.ClearBoard();
                    await GameOverAsync();
                    return;
                }
                SaveRoot.Round++;
            }
            await StartNewTurnAsync();
        }
        protected override bool ShowDiceUponAutoSave => SaveRoot!.GameStatus != EnumStatusList.NeedRoll;
        async Task IMoveNM.MoveReceivedAsync(string data)
        {
            await MakeMoveAsync(int.Parse(data));
        }
    }
}