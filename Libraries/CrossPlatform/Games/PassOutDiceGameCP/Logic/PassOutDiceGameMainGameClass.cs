using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using PassOutDiceGameCP.Data;
using System.Threading.Tasks;
namespace PassOutDiceGameCP.Logic
{
    [SingletonGame]
    public class PassOutDiceGameMainGameClass
        : BoardDiceGameClass<PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo, EnumColorChoice, int>
    {
        public PassOutDiceGameMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            PassOutDiceGameVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            PassOutDiceGameGameContainer container,
            StandardRollProcesses<SimpleDice, PassOutDiceGamePlayerItem> roller,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container, roller)
        {
            _model = model;
            _command = command;
            _gameBoard = gameBoard;
        }
        private readonly PassOutDiceGameVMData _model;
        private readonly CommandContainer _command;
        private readonly GameBoardProcesses _gameBoard;
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            AfterRestoreDice();
            BoardGameSaved();
            SaveRoot.LoadMod(_model);
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
        public override Task PopulateSaveRootAsync()
        {
            if (PlayerList.DidChooseColors() == true)
            {
                _gameBoard.SaveGame();
            }
            return base.PopulateSaveRootAsync();
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
                SaveRoot.DidRoll = false;
            }
            await ContinueTurnAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            if (PlayerList.DidChooseColors() == false)
            {
                await base.ContinueTurnAsync();
            }
            if (SaveRoot.DidRoll == false)
            {
                if (BasicData.MultiPlayer && SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
                {
                    Check!.IsEnabled = true;
                    return;
                }
                SaveRoot.DidRoll = true;
                await Roller.RollDiceAsync();
                return;
            }
            await base.ContinueTurnAsync();
        }
        public override async Task MakeMoveAsync(int space)
        {
            _gameBoard.MakeMove(space);
            int wons = _gameBoard.WhoWon;
            if (wons > 0)
            {
                SingleInfo = PlayerList[wons]; //i think should be whoever actually won.
                await ShowWinAsync();
                return;
            }
            await EndTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            _command.ManuelFinish = true;
            await StartNewTurnAsync();
        }
        public override async Task AfterChoosingColorsAsync()
        {
            _gameBoard.ClearBoard();
            await EndTurnAsync();
        }
        public override async Task AfterRollingAsync()
        {
            SaveRoot.DidRoll = true;
            await ContinueTurnAsync();
        }
    }
}