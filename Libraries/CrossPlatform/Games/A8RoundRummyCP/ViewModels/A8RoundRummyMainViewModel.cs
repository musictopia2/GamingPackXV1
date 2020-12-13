using A8RoundRummyCP.Cards;
using A8RoundRummyCP.Data;
using A8RoundRummyCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks;
namespace A8RoundRummyCP.ViewModels
{
    [InstanceGame]
    public class A8RoundRummyMainViewModel : BasicCardGamesVM<A8RoundRummyCardInformation>
    {
        private readonly A8RoundRummyMainGameClass _mainGame;
        private readonly A8RoundRummyVMData _model;
        private readonly A8RoundRummyGameContainer _gameContainer;
        public A8RoundRummyMainViewModel(CommandContainer commandContainer,
            A8RoundRummyMainGameClass mainGame,
            A8RoundRummyVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            A8RoundRummyGameContainer gameContainer
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _gameContainer = gameContainer;
            _model.Deck1.NeverAutoDisable = true;
        }
        protected override bool CanEnableDeck()
        {
            return !_gameContainer!.AlreadyDrew;
        }
        protected override bool CanEnablePile1()
        {
            return true;
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            if (_mainGame!.CanProcessDiscard(out bool pickUp, out int deck, out string message) == false)
            {
                ToastPlatform.ShowError(message);
                return;
            }
            if (pickUp == true)
            {
                await _mainGame.PickupFromDiscardAsync();
                return;
            }
            await _gameContainer.SendDiscardMessageAsync(deck);
            await _mainGame.DiscardAsync(deck);
        }
        public override bool CanEnableAlways()
        {
            return true;
        }

        private string _nextTurn = "";
        [VM]
        public string NextTurn
        {
            get { return _nextTurn; }
            set
            {
                if (SetProperty(ref _nextTurn, value))
                {
                    
                }
            }
        }
        public bool CanGoOut => _model.PlayerHand1.HandList.Count == 8; //if 8, you can go out since you have to draw first.
        [Command(EnumCommandCategory.Game)]
        public async Task GoOutAsync()
        {
            if (_mainGame!.CanGoOut() == false)
            {
                ToastPlatform.ShowError("Sorry; you cannot go out");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer == true)
            {
                MultiplayerOut thisOut = new MultiplayerOut();
                thisOut.Deck = _mainGame.CardForDiscard!.Deck;
                thisOut.WasGuaranteed = _mainGame.WasGuarantee;
                await _mainGame.Network!.SendAllAsync("goout", thisOut);
            }
            await _mainGame.GoOutAsync();
        }
    }
}