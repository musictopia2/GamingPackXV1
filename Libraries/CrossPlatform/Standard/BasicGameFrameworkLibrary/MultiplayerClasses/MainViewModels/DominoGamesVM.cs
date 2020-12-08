using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels
{
    public abstract class DominoGamesVM<D> : BasicMultiplayerMainVM
        where D : IDominoInfo, new()
    {
        private readonly IDominoGamesData<D> _model;
        private readonly IDominoDrawProcesses<D> _mainGame;
        public DominoGamesVM(CommandContainer commandContainer, IDominoDrawProcesses<D> mainGame, IDominoGamesData<D> viewModel, BasicData basicData, TestOptions test, IGamePackageResolver resolver) : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _model = viewModel;
            _mainGame = mainGame;
            _model.DrewDominoAsync = DrewDominoAsync;
            if (AlwaysEnableHand() == false)
            {
                _model.PlayerHand1.SendEnableProcesses(this, () =>
                {
                    return CanEnableHand();
                });
            }
            else
            {
                _model.PlayerHand1.SendAlwaysEnable(this);// will handle this part
            }
            _model.BoneYard.SendEnableProcesses(this, () =>
            {
                return CanEnableBoneYard();
            });
            _model.HandClickedAsync = HandClicked;
            _model.PlayerBoardClickedAsync = PlayerBoardClickedAsync;
        }
        public async Task DrewDominoAsync(D thisDomino)
        {
            await _mainGame.DrawDominoAsync(thisDomino);
        }
        protected virtual bool AlwaysEnableHand()
        {
            return true; // most of the time, you can enable hand.  if you can't then will be here
        }
        protected virtual bool CanEnableHand()
        {
            return true;
        }
        protected abstract bool CanEnableBoneYard();
        protected virtual Task PlayerBoardClickedAsync()
        {
            return Task.CompletedTask;
        }
        protected virtual Task HandClicked(D domino, int index)
        {
            return Task.CompletedTask;
        }
        public override bool CanEndTurn()
        {
            if (_model.BoneYard!.IsEnabled == true)
            {
                return false;
            }
            else
            {
                return base.CanEndTurn();
            }
        }
    }
}