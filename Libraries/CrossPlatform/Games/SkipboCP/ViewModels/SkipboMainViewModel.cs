using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using SkipboCP.Cards;
using SkipboCP.Data;
using SkipboCP.Logic;
using System.Threading.Tasks;
namespace SkipboCP.ViewModels
{
    [InstanceGame]
    public class SkipboMainViewModel : BasicCardGamesVM<SkipboCardInformation>
    {
        private readonly SkipboMainGameClass _mainGame;
        private readonly SkipboVMData _model;
        private readonly IGamePackageResolver _resolver;
        private readonly SkipboGameContainer _gameContainer;
        private int _cardsToShuffle;
        [VM]
        public int CardsToShuffle
        {
            get { return _cardsToShuffle; }
            set
            {
                if (SetProperty(ref _cardsToShuffle, value))
                {
                    
                }
            }
        }
        public SkipboMainViewModel(CommandContainer commandContainer,
            SkipboMainGameClass mainGame,
            SkipboVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            SkipboGameContainer gameContainer
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _resolver = resolver;
            _gameContainer = gameContainer;
            _model.Deck1.NeverAutoDisable = true;
            _model.PlayerHand1.Maximum = 5;
            _gameContainer.LoadPlayerPilesAsync = LoadPlayerPilesAsync;
        }
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            await LoadPlayerPilesAsync();
        }
        private async Task LoadPlayerPilesAsync()
        {
            if (PlayerPilesScreen != null)
            {
                await CloseSpecificChildAsync(PlayerPilesScreen);
            }
            PlayerPilesScreen = _resolver.Resolve<PlayerPilesViewModel>();
            await LoadScreenAsync(PlayerPilesScreen);
        }
        public PlayerPilesViewModel? PlayerPilesScreen { get; set; }
        protected override bool CanEnableDeck()
        {
            return false;
        }
        protected override bool CanEnablePile1()
        {
            return false;
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            await Task.CompletedTask;
        }
        protected override async Task BeforeUnselectCardFromHandAsync()
        {
            _mainGame!.UnselectAllCards();
            await Task.CompletedTask;
        }
        public override bool CanEnableAlways()
        {
            return true;
        }
    }
}