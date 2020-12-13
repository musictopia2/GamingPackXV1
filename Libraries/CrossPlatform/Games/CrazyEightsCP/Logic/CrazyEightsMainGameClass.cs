using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using CrazyEightsCP.Data;
using System.Threading.Tasks;
namespace CrazyEightsCP.Logic
{
    [SingletonGame]
    public class CrazyEightsMainGameClass : CardGameClass<RegularSimpleCard, CrazyEightsPlayerItem, CrazyEightsSaveInfo>
    {
        private readonly CrazyEightsComputerAI _aI = new CrazyEightsComputerAI();
        private readonly CrazyEightsVMData _model;
        private readonly CommandContainer _command;
        private readonly CrazyEightsGameContainer _gameContainer;
        private readonly ISuitProcesses _processes;
        public CrazyEightsMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            CrazyEightsVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<RegularSimpleCard> cardInfo,
            CommandContainer command,
            CrazyEightsGameContainer gameContainer,
            ISuitProcesses processes
            )
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
            _processes = processes;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            SaveRoot.LoadMod(_model);
            return base.FinishGetSavedAsync();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            if (SingleInfo!.PlayerCategory != EnumPlayerCategory.Computer)
            {
                throw new BasicBlankException("The computer player can't go for anybody else on this game");
            }
            if (Test!.ComputerEndsTurn == true)
            {
                throw new BasicBlankException("The computer player was suppposed to end turn.  Rethink");
            }
            await Delay!.DelayMilli(200);
            if (SaveRoot!.ChooseSuit == true)
            {
                EnumSuitList thisSuit = _aI.SuitToChoose(SingleInfo);
                await _processes.SuitChosenAsync(thisSuit);
                return;
            }
            await Delay.DelaySeconds(.5);
            int Nums = _aI.CardToPlay(SaveRoot);
            if (Nums > 0)
            {
                await PlayCardAsync(Nums);
                return;
            }
            if (BasicData!.MultiPlayer == true)
            {
                await _gameContainer.SendDrawMessageAsync();
            }
            await DrawAsync();
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            LoadControls();
            return base.StartSetUpAsync(isBeginning);
        }
        protected override Task LastPartOfSetUpBeforeBindingsAsync()
        {
            var tempCard = _model!.Pile1!.GetCardInfo();
            SaveRoot!.CurrentNumber = tempCard.Value;
            if (tempCard.DisplaySuit == EnumSuitList.None)
            {
                SaveRoot.CurrentSuit = tempCard.Suit;
            }
            else
            {
                SaveRoot.CurrentSuit = tempCard.DisplaySuit;
            }
            SaveRoot.CurrentCard = tempCard.Deck;
            SaveRoot.LoadMod(_model);
            return Task.CompletedTask;
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            SaveRoot!.ChooseSuit = false;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.MainHandList.UnhighlightObjects();
            if ((SingleInfo.MainHandList.Count == 0 && PlayerCanWin() == true) || Test!.ImmediatelyEndGame)
            {
                await ShowWinAsync();
                return;
            }
            _command.ManuelFinish = true;
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        protected async override Task AfterDrawingAsync()
        {
            _gameContainer.AlreadyDrew = false;
            PlayerDraws = 0;
            await base.AfterDrawingAsync();
        }
        public bool IsValidMove(int deck)
        {
            var card = _gameContainer.DeckList!.GetSpecificItem(deck);
            if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Self && Test!.AllowAnyMove == true)
            {
                return true;
            }
            if (card.Value == EnumRegularCardValueList.Eight)
            {
                return true;
            }
            if (card.Value == SaveRoot!.CurrentNumber)
            {
                return true;
            }
            if (card.Suit == SaveRoot.CurrentSuit)
            {
                return true;
            }
            return false;
        }
        public async Task PlayCardAsync(int deck)
        {
            var card = _gameContainer.DeckList!.GetSpecificItem(deck);
            card.Drew = false;
            card.IsSelected = false;
            await _gameContainer.SendDiscardMessageAsync(deck);
            await DiscardAsync(card);
        }
        public override async Task DiscardAsync(RegularSimpleCard thisCard)
        {
            int firstCount;
            firstCount = SingleInfo!.MainHandList.Count;
            SingleInfo.MainHandList.RemoveSpecificItem(thisCard);
            _gameContainer.Command.UpdateAll();
            int secondCount = SingleInfo.MainHandList.Count;
            if (secondCount + 1 != firstCount)
            {
                throw new BasicBlankException("Warning, second count was " + secondCount + " and first count was " + firstCount + ".  Something is not right");
            }
            await AnimatePlayAsync(thisCard);
            SaveRoot!.CurrentNumber = thisCard.Value;
            if (SingleInfo.ObjectCount != SingleInfo.MainHandList.Count)
            {
                throw new BasicBlankException("Failed to update count.  Rethink");
            }
            if (thisCard.Value == EnumRegularCardValueList.Eight && SingleInfo.MainHandList.Count > 0)
            {
                SaveRoot.ChooseSuit = true;
                await ContinueTurnAsync();
                return;
            }
            SaveRoot.CurrentSuit = thisCard.Suit;
            var tempCard = _model!.Pile1!.GetCardInfo();
            if (tempCard.Deck != thisCard.Deck)
            {
                await UIPlatform.ShowMessageAsync("Failed to add to discard pile.  Rethink");
                return;
            }
            await EndTurnAsync();
        }
    }
}