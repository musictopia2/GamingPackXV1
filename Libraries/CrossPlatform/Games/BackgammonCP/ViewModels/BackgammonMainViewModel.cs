using BackgammonCP.Data;
using BackgammonCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;
namespace BackgammonCP.ViewModels
{
    [InstanceGame]
    public class BackgammonMainViewModel : BoardDiceGameVM
    {
        private readonly BackgammonMainGameClass _mainGame;
        private readonly BackgammonVMData _model;
        private readonly GameBoardProcesses _gameBoard;
        public BackgammonMainViewModel(CommandContainer commandContainer,
            BackgammonMainGameClass mainGame,
            BackgammonVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller,
            GameBoardProcesses gameBoard,
            BackgammonGameContainer gameContainer
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = model;
            _gameBoard = gameBoard;
            gameContainer.MakeMoveAsync = MakeMoveAsync;
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        private async Task MakeMoveAsync(int space)
        {
            if (_gameBoard.IsValidMove(space) == false)
            {
                return;
            }
            if (_mainGame.BasicData.MultiPlayer)
            {
                await _mainGame.Network!.SendMoveAsync(_gameBoard.GetReversedID(space));
            }
            await _mainGame.MakeMoveAsync(space);
        }
        public override bool CanEndTurn()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumGameStatus.EndingTurn;
        }
        public override bool CanRollDice()
        {
            return false;
        }
        public override async Task RollDiceAsync()
        {
            await base.RollDiceAsync();
        }

        private int _movesMade;
        [VM]
        public int MovesMade
        {
            get { return _movesMade; }
            set
            {
                if (SetProperty(ref _movesMade, value))
                {
                    
                }
            }
        }
        private string _lastStatus = "";
        [VM]
        public string LastStatus
        {
            get { return _lastStatus; }
            set
            {
                if (SetProperty(ref _lastStatus, value))
                {
                    
                }
            }
        }
        public bool CanUndoMove
        {
            get
            {
                if (_mainGame.SaveRoot.SpaceHighlighted > -1)
                {
                    return false;
                }
                return _mainGame.SaveRoot.GameStatus == EnumGameStatus.EndingTurn || _mainGame.SaveRoot.MadeAtLeastOneMove;
            }
        }

        [Command(EnumCommandCategory.Game)]
        public async Task UndoMoveAsync()
        {
            if (_mainGame.BasicData.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("undomove");
            }
            await _gameBoard.UndoAllMovesAsync();
        }
    }
}