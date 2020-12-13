using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using ChineseCheckersCP.Data;
using ChineseCheckersCP.Logic;
namespace ChineseCheckersCP.ViewModels
{
    [InstanceGame]
    public class ChineseCheckersMainViewModel : SimpleBoardGameVM
    {
        private readonly GameBoardProcesses _gameBoard;
        public ChineseCheckersMainViewModel(CommandContainer commandContainer,
            ChineseCheckersMainGameClass mainGame,
            ChineseCheckersVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            GameBoardProcesses gameBoard
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver)
        {
            _gameBoard = gameBoard;
        }
        public override bool CanEndTurn()
        {
            return _gameBoard.WillContinueTurn();
        }
    }
}