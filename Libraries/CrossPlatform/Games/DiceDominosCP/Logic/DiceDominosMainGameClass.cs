using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using DiceDominosCP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace DiceDominosCP.Logic
{
    [SingletonGame]
    public class DiceDominosMainGameClass : DiceGameClass<SimpleDice, DiceDominosPlayerItem, DiceDominosSaveInfo>, IMoveNM
    {
        private readonly DiceDominosVMData _model;
        private readonly StandardRollProcesses<SimpleDice, DiceDominosPlayerItem> _roller;
        private readonly DiceDominosComputerAI _computerAI;
        private readonly GameBoardCP _gameBoard;
        private readonly DiceDominosGameContainer _gameContainer;
        public DiceDominosMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            DiceDominosVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            DiceDominosGameContainer gameContainer,
            StandardRollProcesses<SimpleDice, DiceDominosPlayerItem> roller,
            DiceDominosComputerAI computerAI,
            GameBoardCP gameBoard
            ) :
            base(mainContainer, aggregator, basicData, test, currentMod, state, delay, command, gameContainer, roller)
        {
            _model = currentMod;
            _roller = roller;
            _computerAI = computerAI;
            _gameBoard = gameBoard;
            _gameBoard.Text = "Dominos";
            _gameContainer = gameContainer;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            _gameBoard!.LoadSavedGame(SaveRoot!.BoardDice!);
            _gameContainer.DominosShuffler.ClearObjects();
            _gameContainer.DominosShuffler.OrderedObjects();
            AfterRestoreDice();
            if (SaveRoot.DidHold == true || SaveRoot.HasRolled == true)
            {
                _model.Cup!.CanShowDice = true;
            }
            else
            {
                _model.Cup!.CanShowDice = false;
            }
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
                return;
            LoadMod();
            IsLoaded = true;
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            PlayerList!.ForEach(items =>
            {
                items.DominosWon = 0;
            });
            SetUpDice();
            SaveRoot!.ImmediatelyStartTurn = true;
            _gameContainer.DominosShuffler.ClearObjects();
            _gameContainer.DominosShuffler.ShuffleObjects();
            _gameBoard.ClearPieces();
            await FinishUpAsync(isBeginning);
        }
        public override Task PopulateSaveRootAsync()
        {
            SaveRoot!.BoardDice = _gameBoard.ObjectList.ToRegularDeckDict();
            return Task.CompletedTask;
        }
        public override async Task StartNewTurnAsync()
        {
            PrepStartTurn();
            _model.Cup!.UnholdDice();
            SaveRoot!.HasRolled = false;
            SaveRoot.DidHold = false;
            _model.Cup.CanShowDice = false;
            await ContinueTurnAsync();
        }
        public override Task AfterHoldUnholdDiceAsync()
        {
            SaveRoot!.DidHold = true;
            SaveRoot.HasRolled = false;
            return base.AfterHoldUnholdDiceAsync();
        }
        protected override async Task ProtectedAfterRollingAsync()
        {
            SaveRoot!.HasRolled = true;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        public async Task MakeMoveAsync(int deck)
        {
            if (SingleInfo!.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendMoveAsync(deck);
            }
            SingleInfo!.DominosWon++;
            _gameBoard!.MakeMove(deck);
            if (IsGameOver(SingleInfo.DominosWon) == true)
            {
                await ShowWinAsync();
                return;
            }
            await EndTurnAsync();
        }
        public async Task MoveReceivedAsync(string Data)
        {
            await MakeMoveAsync(int.Parse(Data));
        }
        protected override async Task ComputerTurnAsync()
        {
            if (SaveRoot!.HasRolled == false)
            {
                await _roller!.RollDiceAsync();
                return;
            }
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(.5);
            }
            int nums = _computerAI.Move();
            if (nums == 0 && SaveRoot.DidHold == true)
            {
                if (BasicData!.MultiPlayer == true)
                {
                    await Network!.SendEndTurnAsync();
                }
                await EndTurnAsync();
                return;
            }
            if (nums == 0 && SaveRoot.DidHold == false)
            {
                int ThisNum = _computerAI.DominoToHold();
                await HoldUnholdDiceAsync(ThisNum);
                return;
            }
            await MakeMoveAsync(nums);
        }
        private bool IsGameOver(int score)
        {
            if (Test!.ImmediatelyEndGame)
            {
                return true;
            }
            if (score == 13 && PlayerList.Count() == 2)
            {
                return true;
            }
            if (score == 9 && PlayerList.Count() == 3)
            {
                return true;
            }
            if (score == 7 && PlayerList.Count() == 4)
            {
                return true;
            }
            if (score == 6 && PlayerList.Count() == 5)
            {
                return true;
            }
            if (score == 5 && PlayerList.Count() == 6)
            {
                return true;
            }
            return false;
        }
    }
}