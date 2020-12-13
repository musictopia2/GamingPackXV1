using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using Spades2PlayerCP.Cards;
using Spades2PlayerCP.Data;
using Spades2PlayerCP.Logic;
using System.Threading.Tasks;
namespace Spades2PlayerCP.ViewModels
{
    [InstanceGame]
    public class Spades2PlayerMainViewModel : TrickCardGamesVM<Spades2PlayerCardInformation, EnumSuitList>
    {
        private readonly Spades2PlayerMainGameClass _mainGame;
        private readonly Spades2PlayerVMData _model;
        private readonly IGamePackageResolver _resolver;
        private readonly Spades2PlayerGameContainer _gameContainer;
        public Spades2PlayerMainViewModel(CommandContainer commandContainer,
            Spades2PlayerMainGameClass mainGame,
            Spades2PlayerVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            Spades2PlayerGameContainer gameContainer
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _resolver = resolver;
            _gameContainer = gameContainer;
            _model.Deck1.NeverAutoDisable = true;
            GameStatus = _model.GameStatus;
        }
        public SpadesBeginningViewModel? BeginningScreen { get; set; }
        public SpadesBiddingViewModel? BiddingScreen { get; set; }
        protected override Task ActivateAsync()
        {
            ScreenChangeAsync();
            return base.ActivateAsync();
        }
        private async void ScreenChangeAsync()
        {
            if (_isClosed)
            {
                return;
            }
            if (_model == null)
            {
                return;
            }
            if (_processing)
            {
                return;
            }
            _processing = true;
            if (_mainGame.SaveRoot.GameStatus == EnumGameStatus.Normal)
            {
                await CloseBeginningScreenAsync();
                await CloseBiddingScreenAsync();
                _model.TrickArea1.Visible = true;
                _processing = false;
                return;
            }
            _model.TrickArea1.Visible = false;
            if (_mainGame.SaveRoot.GameStatus == EnumGameStatus.Bidding)
            {
                await CloseBeginningScreenAsync();
                await OpenBiddingAsync();
                _processing = false;
                return;
            }
            if (_mainGame.SaveRoot.GameStatus == EnumGameStatus.ChooseCards)
            {
                await CloseBiddingScreenAsync();
                await OpenBeginningAsync();
                _processing = false;
                return;
            }
            throw new BasicBlankException("Not supported.  Rethink");
        }
        private async Task CloseBeginningScreenAsync()
        {
            if (BeginningScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(BeginningScreen);
            BeginningScreen = null;
        }
        private async Task CloseBiddingScreenAsync()
        {
            if (BiddingScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(BiddingScreen);
            BiddingScreen = null;
        }
        private async Task OpenBeginningAsync()
        {
            if (BeginningScreen != null)
            {
                return;
            }
            BeginningScreen = _resolver.Resolve<SpadesBeginningViewModel>();
            await LoadScreenAsync(BeginningScreen);
        }
        private async Task OpenBiddingAsync()
        {
            if (BiddingScreen != null)
            {
                return;
            }
            BiddingScreen = _resolver.Resolve<SpadesBiddingViewModel>();
            await LoadScreenAsync(BiddingScreen);
        }
        protected override bool CanEnableDeck()
        {
            return false;
        }
        protected override bool CanEnablePile1()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumGameStatus.ChooseCards;
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            _model.OtherPile!.Visible = false;
            await _gameContainer!.SendDiscardMessageAsync(_model.OtherPile!.GetCardInfo().Deck);
            await _mainGame!.DiscardAsync(_model.OtherPile.GetCardInfo().Deck);
        }
        public override bool CanEnableAlways()
        {
            return true;
        }

        private int _roundNumber;
        [VM]
        public int RoundNumber
        {
            get { return _roundNumber; }
            set
            {
                if (SetProperty(ref _roundNumber, value))
                {
                    
                }
            }
        }
        private bool _processing;

        private EnumGameStatus _gameStatus;
        [VM]
        public EnumGameStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    ScreenChangeAsync();
                }
            }
        }
        private bool _isClosed = false;
        protected override Task TryCloseAsync()
        {
            _isClosed = true;
            return base.TryCloseAsync();
        }
    }
}