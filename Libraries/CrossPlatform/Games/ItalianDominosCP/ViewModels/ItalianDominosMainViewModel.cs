using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using ItalianDominosCP.Data;
using ItalianDominosCP.Logic;
using System.Threading.Tasks;
namespace ItalianDominosCP.ViewModels
{
    [InstanceGame]
    public class ItalianDominosMainViewModel : DominoGamesVM<SimpleDominoInfo>
    {
        private readonly ItalianDominosMainGameClass _mainGame;
        private readonly IDominoGamesData<SimpleDominoInfo> _viewModel;
        public ItalianDominosMainViewModel(
            CommandContainer commandContainer,
            ItalianDominosMainGameClass mainGame,
            IDominoGamesData<SimpleDominoInfo> viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver) : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _viewModel = viewModel;
            viewModel.PlayerHand1.Maximum = 8;
            viewModel.PlayerHand1.AutoSelect = EnumHandAutoType.SelectOneOnly;
        }
        public HandObservable<SimpleDominoInfo> PlayerHand => _viewModel.PlayerHand1;
        public DominosBoneYardClass<SimpleDominoInfo> BoneYard => _viewModel.BoneYard;
        public PlayerCollection<ItalianDominosPlayerItem> GetPlayerList => _mainGame.SaveRoot.PlayerList;
        protected override bool CanEnableBoneYard()
        {
            return !_mainGame.SingleInfo!.DrewYet;
        }
        private int _upTo;
        [VM]
        public int UpTo
        {
            get { return _upTo; }
            set
            {
                if (SetProperty(ref _upTo, value))
                {
                    
                }
            }
        }
        private int _nextNumber;
        [VM]
        public int NextNumber
        {
            get { return _nextNumber; }
            set
            {
                if (SetProperty(ref _nextNumber, value))
                {
                    
                }
            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task PlayAsync()
        {
            int deck = _viewModel.PlayerHand1.ObjectSelected();
            if (deck == 0)
            {
                await UIPlatform.ShowMessageAsync("You must choose one domino to play");
                return;
            }
            await _mainGame.PlayDominoAsync(deck);
        }
    }
}