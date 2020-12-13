using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using DominosRegularCP.Data;
using DominosRegularCP.Logic;
namespace DominosRegularCP.ViewModels
{
    [InstanceGame]
    public class DominosRegularMainViewModel : DominoGamesVM<SimpleDominoInfo>
    {
        private readonly DominosRegularMainGameClass _mainGame;
        private readonly DominosRegularVMData _viewModel;
        public DominosRegularMainViewModel(
            CommandContainer commandContainer,
            DominosRegularMainGameClass mainGame,
            DominosRegularVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver) : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _viewModel = viewModel;
        }
        public HandObservable<SimpleDominoInfo> PlayerHand => _viewModel.PlayerHand1;
        public DominosBoneYardClass<SimpleDominoInfo> BoneYard => _viewModel.BoneYard;
        public PlayerCollection<DominosRegularPlayerItem> GetPlayerList => _mainGame.SaveRoot.PlayerList;
        protected override bool CanEnableBoneYard()
        {
            return true;
        }
        public override bool CanEndTurn()
        {
            if (_viewModel.BoneYard!.HasBone())
            {
                return _viewModel.BoneYard.HasDrawn();
            }
            return true;
        }
        public GameBoardCP GetBoard => _viewModel.GameBoard1;
    }
}