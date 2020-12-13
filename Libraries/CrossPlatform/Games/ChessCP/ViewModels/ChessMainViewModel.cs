using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using ChessCP.Data;
using ChessCP.Logic;
using System.Threading.Tasks;
namespace ChessCP.ViewModels
{
    [InstanceGame]
    public class ChessMainViewModel : SimpleBoardGameVM
    {
        private readonly ChessMainGameClass _mainGame;
        private readonly BasicData _basicData;
        private readonly GameBoardProcesses _gameBoard;
        public ChessMainViewModel(CommandContainer commandContainer,
            ChessMainGameClass mainGame,
            ChessVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            GameBoardProcesses gameBoard
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _basicData = basicData;
            _gameBoard = gameBoard;
        }
        public bool CanTie
        {
            get
            {
                if (_mainGame.SaveRoot.SpaceHighlighted > 0)
                {
                    return false;
                }
                return _mainGame.SaveRoot.GameStatus != EnumGameStatus.EndingTurn;
            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task TieAsync()
        {
            if (_basicData.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("possibletie");
            }
            CommandContainer.ManuelFinish = true;
            await _mainGame.ProcessTieAsync();
        }
        public bool CanUndoMoves => _mainGame.SaveRoot.GameStatus == EnumGameStatus.EndingTurn;

        [Command(EnumCommandCategory.Game)]
        public async Task UndoMovesAsync()
        {
            if (_basicData.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("undomove");
            }
            await _gameBoard.UndoAllMovesAsync();
        }
        public override bool CanEndTurn()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumGameStatus.PossibleTie || _mainGame.SaveRoot.GameStatus == EnumGameStatus.EndingTurn;
        }
    }
}