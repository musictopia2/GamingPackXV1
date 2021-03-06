using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using FlinchCP.Cards;
using FlinchCP.Data;
using FlinchCP.Logic;
using System.Threading.Tasks;
namespace FlinchCP.ViewModels
{
    [InstanceGame]
    public class FlinchMainViewModel : BasicCardGamesVM<FlinchCardInformation>
    {
        private readonly FlinchMainGameClass _mainGame;
        private readonly FlinchVMData _model;
        private readonly IGamePackageResolver _resolver;
        private readonly FlinchGameContainer _gameContainer;
        public FlinchMainViewModel(CommandContainer commandContainer,
            FlinchMainGameClass mainGame,
            FlinchVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            FlinchGameContainer gameContainer
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
        private async Task LoadPlayerPilesAsync()
        {
            if (PlayerPilesScreen != null)
            {
                await CloseSpecificChildAsync(PlayerPilesScreen);
            }
            PlayerPilesScreen = _resolver.Resolve<PlayerPilesViewModel>();
            await LoadScreenAsync(PlayerPilesScreen);
        }
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            await LoadPlayerPilesAsync();
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
        public override bool CanEndTurn()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumStatusList.FirstOne && _mainGame.SaveRoot.PlayerFound == 0;
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
    }
}