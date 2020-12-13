using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using DiceDominosCP.Data;
using DiceDominosCP.Logic;
using System.Threading.Tasks;
namespace DiceDominosCP.ViewModels
{
    [InstanceGame]
    public class DiceDominosMainViewModel : DiceGamesVM<SimpleDice>
    {
        private readonly DiceDominosMainGameClass _mainGame;
        private readonly DiceDominosVMData _model;
        public DiceDominosMainViewModel(CommandContainer commandContainer,
            DiceDominosMainGameClass mainGame,
            DiceDominosVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller,
            DiceDominosGameContainer gameContainer,
            GameBoardCP gameBoard
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            GameBoard = gameBoard;
            _model = viewModel;
            gameContainer.DominoClickedAsync = DominoClickedAsync;
            gameBoard.SendEnableProcesses(this, (() => _mainGame.SaveRoot.HasRolled));
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public GameBoardCP GameBoard { get; }
        public PlayerCollection<DiceDominosPlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            if (_mainGame!.SaveRoot!.HasRolled == false || _mainGame.SaveRoot.DidHold == true)
            {
                return false;
            }
            return true;
        }
        public override bool CanEndTurn()
        {
            return _mainGame!.SaveRoot!.HasRolled;
        }
        public override bool CanRollDice()
        {
            return !_mainGame!.SaveRoot!.HasRolled;
        }
        private async Task DominoClickedAsync(SimpleDominoInfo thisDomino)
        {
            if (GameBoard.IsValidMove(thisDomino.Deck) == false)
            {
                await UIPlatform.ShowMessageAsync("Illegal Move");
                return;
            }
            await _mainGame.MakeMoveAsync(thisDomino.Deck);
        }
    }
}