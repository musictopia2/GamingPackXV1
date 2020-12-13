using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using LifeCardGameCP.Cards;
using LifeCardGameCP.Data;
using LifeCardGameCP.Logic;
using System.Threading.Tasks;
namespace LifeCardGameCP.ViewModels
{
    [InstanceGame]
    public class LifeCardGameMainViewModel : BasicCardGamesVM<LifeCardGameCardInformation>
    {
        private readonly LifeCardGameMainGameClass _mainGame;
        private readonly LifeCardGameVMData _model;
        private readonly IGamePackageResolver _resolver;
        private readonly LifeCardGameGameContainer _gameContainer;
        public LifeCardGameMainViewModel(CommandContainer commandContainer,
            LifeCardGameMainGameClass mainGame,
            LifeCardGameVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            LifeCardGameGameContainer gameContainer
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _resolver = resolver;
            _gameContainer = gameContainer;
            _model.Deck1.NeverAutoDisable = true;
            _model.PlayerHand1.Maximum = 5;
            _model.CurrentPile.SendEnableProcesses(this, () => false);
            CommandContainer!.ExecutingChanged += CommandContainer_ExecutingChanged;
            _gameContainer.LoadOtherScreenAsync = LoadOtherScreenAsync;
            _gameContainer.CloseOtherScreenAsync = CloseOtherScreenAsync;
        }
        public async Task LoadOtherScreenAsync()
        {
            if (OtherScreen != null)
            {
                return;
            }
            OtherScreen = _resolver.Resolve<OtherViewModel>();
            await LoadScreenAsync(OtherScreen);
        }
        public async Task CloseOtherScreenAsync()
        {
            if (OtherScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(OtherScreen);
            OtherScreen = null;
        }
        public OtherViewModel? OtherScreen { get; set; }
        protected override Task TryCloseAsync()
        {
            CommandContainer!.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        private void CommandContainer_ExecutingChanged()
        {
            _mainGame!.PlayerList!.EnableLifeStories(_mainGame, _model, !CommandContainer!.IsExecuting); //i think
        }
        protected override bool CanEnableDeck()
        {
            return false;
        }
        protected override bool CanEnablePile1()
        {
            return _model.CurrentPile!.PileEmpty();
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            int newDeck = _model.PlayerHand1!.ObjectSelected();
            if (newDeck == 0)
            {
                ToastPlatform.ShowError("Sorry, must select a card first");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("discard", newDeck);
            }
            await _mainGame!.DiscardAsync(newDeck);
        }
        public override bool CanEnableAlways()
        {
            return true;
        }
        [Command(EnumCommandCategory.Old)]
        public async Task YearsPassedAsync()
        {
            ToastPlatform.ShowError($"{_mainGame.SaveRoot!.YearsPassed()} passed.  Once it reaches 60; the game is over");
        }
        public bool CanPlayCard => _model.CurrentPile.PileEmpty();
        [Command(EnumCommandCategory.Game)]
        public async Task PlayCardAsync()
        {
            int decks = _model.PlayerHand1.ObjectSelected();
            if (decks == 0)
            {
                ToastPlatform.ShowError("Must choose a card to play");
                return;
            }
            var thisCard = _mainGame.SingleInfo!.MainHandList.GetSpecificItem(decks);
            if (_mainGame.CanPlayCard(thisCard) == false)
            {
                ToastPlatform.ShowError("Illegal Move");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("playcard", decks);
            }
            CommandContainer!.ManuelFinish = true;
            await _mainGame.PlayCardAsync(thisCard);
        }
    }
}