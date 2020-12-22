using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using SkipboCP.Cards;
using SkipboCP.Data;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace SkipboCP.Logic
{
    [SingletonGame]
    public class SkipboMainGameClass : CardGameClass<SkipboCardInformation, SkipboPlayerItem, SkipboSaveInfo>, IMiscDataNM
    {
        private readonly SkipboVMData _model;
        private readonly CommandContainer _command;
        private readonly SkipboGameContainer _gameContainer;
        private readonly SkipboComputerAI _ai;
        internal CustomBasicList<ComputerData> ComputerList { get; set; } = new CustomBasicList<ComputerData>(); //the computer ai needs it.
        public SkipboMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            SkipboVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<SkipboCardInformation> cardInfo,
            CommandContainer command,
            SkipboGameContainer gameContainer,
            SkipboComputerAI ai,
            SkipboDelegates delegates
            )
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
            _ai = ai;
            _model.PublicPiles.PileClickedAsync += PublicPiles_PileClickedAsync;
            _gameContainer.IsValidMove = IsValidMove;
            delegates.GetPlayerCount = (() => PlayerList.Count);
        }
        private async Task PublicPiles_PileClickedAsync(int index, BasicPileInfo<SkipboCardInformation> thisPile)
        {
            int decks = CardSelected(out EnumCardType types, out int discardNum);
            if (decks == 0 && types == EnumCardType.IsNone)
            {
                ToastPlatform.ShowError("Sorry, there was nothing selected");
                return;
            }
            if (decks == 0)
            {
                throw new BasicBlankException("Nothing selected but the type was not none");
            }
            bool rets = IsValidMove(index, decks);
            if (rets == false)
            {
                ToastPlatform.ShowError("Illegal Move");
                return;
            }
            await PlayOnPileAsync(index, decks, types, discardNum);
        }
        public override Task FinishGetSavedAsync()
        {
            SaveRoot!.LoadMod(_model!);
            LoadControls();
            _model.PublicPiles!.PileList!.ReplaceRange(SaveRoot.PublicPileList);
            if (SaveRoot.ImmediatelyStartTurn == false)
                PrepStartTurn();
            return base.FinishGetSavedAsync();
        }
        protected override Task MiddleReshuffleCardsAsync(IDeckDict<SkipboCardInformation> thisList, bool canSend)
        {
            if (thisList.Count != SaveRoot!.CardsToShuffle)
            {
                throw new BasicBlankException($"Must have {SaveRoot!.CardsToShuffle}, not {thisList.Count}");
            }
            var nextList = _model!.Pile1!.DiscardList();
            if (nextList.Count > 0)
            {
                throw new BasicBlankException("The discard list somehow did not get cleared out");
            }
            return base.MiddleReshuffleCardsAsync(thisList, canSend);
        }
        protected override void PrepStartTurn()
        {
            base.PrepStartTurn();
            ComputerList = new CustomBasicList<ComputerData>();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true;
        }
        private async Task ComputerDiscardAsync()
        {
            SkipboComputerAI.ComputerDiscardInfo thisDiscard;
            thisDiscard = _ai!.ComputerDiscard();
            await AddToDiscardAsync(thisDiscard.Pile, thisDiscard.Deck);
        }
        protected override async Task ComputerTurnAsync()
        {
            if (Test!.NoAnimations == false )
            {
                await Delay!.DelayMilli(200);
            }
            if (ComputerList.Count == 0)
            {
                ComputerList = _ai!.ComputerMoves();
                if (ComputerList.Count == 0)
                {
                    await ComputerDiscardAsync();
                    return;
                }
            }
            ComputerData thisItem = ComputerList.First();
            if (thisItem.WhichType == EnumCardType.IsNone)
            {
                throw new BasicBlankException("Needs to find a card type to play one");
            }
            await PlayOnPileAsync(thisItem.Pile, thisItem.ThisCard!.Deck, thisItem.WhichType, thisItem.Discard);
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            LoadControls();
            ComputerList.Clear();
            return base.StartSetUpAsync(isBeginning);
        }
        protected override Task LastPartOfSetUpBeforeBindingsAsync()
        {
            SaveRoot!.CardsToShuffle = 0;
            _model.PublicPiles!.ClearBoard();
            SaveRoot.LoadMod(_model!);
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.DiscardList.Clear();
                thisPlayer.StockList.ReplaceRange(thisPlayer.MainHandList);
                thisPlayer.MainHandList.Clear();
                thisPlayer.StockLeft = thisPlayer.StockList.Count;
                thisPlayer.InStock = thisPlayer.StockList.Last().Display;
                thisPlayer.Discard1 = "0";
                thisPlayer.Discard2 = "0";
                thisPlayer.Discard3 = "0";
                thisPlayer.Discard4 = "0";
                thisPlayer.Discard5 = "0";
            });
            return base.LastPartOfSetUpBeforeBindingsAsync();
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "discardnew":
                    SendDiscard ThisDiscard = await js.DeserializeObjectAsync<SendDiscard>(content);
                    await AddToDiscardAsync(ThisDiscard.Pile, ThisDiscard.Deck);
                    return;
                case "play":
                    SendPlay ThisPlay = await js.DeserializeObjectAsync<SendPlay>(content);
                    await PlayOnPileAsync(ThisPlay.Pile, ThisPlay.Deck, ThisPlay.WhichType, ThisPlay.Discard);
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            if (_gameContainer.LoadPlayerPilesAsync != null)
            {
                await _gameContainer.LoadPlayerPilesAsync.Invoke();
            }
            await StartDrawingAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.MainHandList.UnhighlightObjects();
            SingleInfo.StockList = _model.StockPile!.GetStockList();
            SingleInfo.DiscardList = _model.DiscardPiles!.PileList!.ToCustomBasicList();
            _command.ManuelFinish = true; //because it could be somebody else's turn.
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            this.ShowTurn();
            await SaveStateAsync();
            await StartNewTurnAsync();
        }
        public async override Task PopulateSaveRootAsync()
        {
            SaveRoot!.PublicPileList = _model.PublicPiles.PileList!.ToCustomBasicList();
            await base.PopulateSaveRootAsync();
        }

        public override async Task ContinueTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.MainHandList.Count == 0)
            {
                await SaveStateAsync();
                await StartDrawingAsync();
            }
            else
            {
                await base.ContinueTurnAsync();
            }
        }
        private bool _wasNew;
        private async Task StartDrawingAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.MainHandList.Count == 5)
            {
                await ContinueTurnAsync();
                return; // because you already have 5 cards now.
            }
            if (SingleInfo.MainHandList.Count == 0)
            {
                _wasNew = true;
            }
            else
            {
                _wasNew = false;
            }
            LeftToDraw = 5 - SingleInfo.MainHandList.Count;
            PlayerDraws = WhoTurn;
            if (LeftToDraw == 1)
            {
                LeftToDraw = 0;
                PlayerDraws = 0;
            }
            await DrawAsync();
        }
        public void UnselectAllCards()
        {
            _model.StockPile!.UnselectCard();
            _model.PublicPiles!.UnselectAllPiles();
            _model!.PlayerHand1!.UnselectAllObjects();
            _model.DiscardPiles!.UnselectAllCards();
        }
        private void UpdateDiscardData(SkipboPlayerItem thisPlayer, int thisPile)
        {
            string thisNum;
            if (_model.DiscardPiles!.HasCard(thisPile) == false)
            {
                thisNum = "0";
            }
            else
            {
                var card = _model.DiscardPiles.GetLastCard(thisPile);
                thisNum = card.Display; // i think needs to be value.  So can even be W
            }
            switch (thisPile)
            {
                case 0:
                    {
                        thisPlayer.Discard1 = thisNum;
                        break;
                    }

                case 1:
                    {
                        thisPlayer.Discard2 = thisNum;
                        break;
                    }

                case 2:
                    {
                        thisPlayer.Discard3 = thisNum;
                        break;
                    }

                case 3:
                    {
                        thisPlayer.Discard4 = thisNum;
                        break;
                    }
                case 4:
                    {
                        thisPlayer.Discard5 = thisNum;
                        break;
                    }
                default:
                    {
                        throw new BasicBlankException("Piles are only 0 to 3, not " + thisPile + ".  Remember, 0 based now");
                    }
            }
        }
        private void RemoveFromHand(int deck)
        {
            SingleInfo!.MainHandList.RemoveObjectByDeck(deck);
            if (SingleInfo.MainHandList.Count == 5)
            {
                throw new BasicBlankException($"After removing a card, must have less than 5 cards left for {SingleInfo.NickName}");
            }
            _command.UpdateAll();
        }
        protected override Task AfterDrawingAsync()
        {
            if (_wasNew == true)
            {
                SingleInfo!.MainHandList.UnhighlightObjects();
            }
            if (SingleInfo!.MainHandList.Any(items => items.IsUnknown))
            {
                throw new BasicBlankException("Should never have unknown cards");
            }
            return base.AfterDrawingAsync();
        }
        protected override Task AfterReshuffleAsync()
        {
            SaveRoot!.CardsToShuffle = 0;
            var firstList = _model!.Deck1!.DeckList();
            firstList.ForConditionalItems(items => items.IsWild && items.Number > 0, items => items.Number = 0);
            return base.AfterReshuffleAsync();
        }
        private void RemoveFromPile(int pile)
        {
            var thisCol = _model.PublicPiles!.EmptyPileList(pile);
            thisCol.ForEach(thisCard =>
            {
                SaveRoot!.CardsToShuffle++;
                _model!.Pile1!.AddCard(thisCard);
            });
        }
        public async Task AddToDiscardAsync(int pile, int deck)
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.CanSendMessage(BasicData!) == true)
            {
                SendDiscard thisDiscard = new SendDiscard();
                thisDiscard.Deck = deck;
                thisDiscard.Pile = pile;
                await Network!.SendAllAsync("discardnew", thisDiscard);
            }
            if (SingleInfo.MainHandList.Count > 5)
            {
                throw new BasicBlankException($"The hand must be 5 or less, not {SingleInfo.MainHandList.Count} for {SingleInfo.NickName}");
            }
            RemoveFromHand(deck);
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            if (thisCard.IsUnknown)
            {
                throw new BasicBlankException("Can not be unknown when playing on discard pile.  Rethink");
            }
            thisCard.Drew = false;
            thisCard.IsSelected = false;
            await AnimateDiscardAsync(thisCard, pile, EnumAnimcationDirection.StartUpToCard);
            _model.DiscardPiles!.AddCardToPile(pile, thisCard);
            _command.UpdateAll();
            UpdateDiscardData(SingleInfo, pile);
            _command.UpdateAll();
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(.5);
            }
            await EndTurnAsync();
        }
        public async Task AnimateDiscardAsync(SkipboCardInformation thisCard, int pile, EnumAnimcationDirection direction)
        {
            await Aggregator!.AnimateCardAsync(thisCard, direction, $"discard{SingleInfo!.NickName}", _model.DiscardPiles!.PileList![pile]);
        }
        public async Task AnimateStockAsync(SkipboCardInformation thisCard)
        {
            var thisPile = _model.StockPile!.StockFrame.PileList.Single();
            await Aggregator!.AnimateCardAsync(thisCard, EnumAnimcationDirection.StartCardToUp, $"stock{SingleInfo!.NickName}", thisPile);
        }
        public bool IsValidMove(int pile, int deck)
        {
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            if (thisCard.IsWild == true)
            {
                return true;
            }
            int nexts = _model.PublicPiles!.NextNumberNeeded(pile);
            return nexts == thisCard.Number;
        }
        public int CardSelected(out EnumCardType whatType, out int discardNum)
        {
            int thisNum = _model!.PlayerHand1!.ObjectSelected();
            if (thisNum > 0)
            {
                whatType = EnumCardType.MyCards;
                discardNum = -1;
                return thisNum;
            }
            thisNum = _model!.DiscardPiles!.CardSelected(out int thisPile);
            if (thisNum > 0)
            {
                whatType = EnumCardType.Discard;
                discardNum = thisPile;
                if (discardNum == -1)
                {
                    throw new BasicBlankException("Must be something from the discard pile");
                }
                return thisNum;
            }
            discardNum = -1;
            thisNum = _model.StockPile!.CardSelected();
            if (thisNum > 0)
            {
                whatType = EnumCardType.Stock;
                return thisNum;
            }
            whatType = EnumCardType.IsNone;
            return 0;
        }
        public async Task PlayOnPileAsync(int pile, int deck, EnumCardType whichType, int discardNum)
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (whichType == EnumCardType.IsNone)
            {
                throw new BasicBlankException("Must have a card type in order to play on a pile");
            }
            if (SingleInfo.CanSendMessage(BasicData!) == true)
            {
                SendPlay thisPlay = new SendPlay();
                thisPlay.Deck = deck;
                thisPlay.Pile = pile;
                thisPlay.WhichType = whichType;
                thisPlay.Discard = discardNum;
                await Network!.SendAllAsync("play", thisPlay);
            }
            int nexts = _model.PublicPiles!.NextNumberNeeded(pile);
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            if (thisCard.IsUnknown)
            {
                throw new BasicBlankException("Should not have been unknown.  Rethink");
            }
            thisCard.Drew = false;
            thisCard.IsSelected = false;
            if (whichType == EnumCardType.MyCards)
            {
                RemoveFromHand(deck);
            }
            else if (whichType == EnumCardType.Stock)
            {
                _model.StockPile!.RemoveCard();
                await AnimateStockAsync(thisCard);
                SingleInfo.StockLeft = _model.StockPile.CardsLeft();
                SingleInfo.InStock = _model.StockPile.NextCardInStock();
                _command.UpdateAll();
            }
            else
            {
                if (discardNum == -1)
                {
                    throw new BasicBlankException("The discard number cannot be -1");
                }
                if (_model.DiscardPiles!.PileList![discardNum].ObjectList.Count == 0)
                {
                    throw new BasicBlankException("The discard must have at least one item to play from");
                }
                _model.DiscardPiles.RemoveCard(discardNum, deck);
                UpdateDiscardData(SingleInfo, discardNum);
                await AnimateDiscardAsync(thisCard, discardNum, EnumAnimcationDirection.StartCardToUp);
                _model.DiscardPiles.UnselectAllCards();
                _command.UpdateAll();
            }
            var thisPile = _model.PublicPiles.PileList![pile];
            if (thisCard.IsWild == true)
            {
                thisCard.Number = thisPile.ObjectList.Count + 1;
            }
            await Aggregator.AnimateCardAsync(thisCard, EnumAnimcationDirection.StartDownToCard, "public", thisPile);
            _model.PublicPiles.AddCardToPile(pile, thisCard); //hopefully this is still okay.
            _command.UpdateAll();
            if (_model.PublicPiles.NeedToRemovePile(pile))
            {
                if (Test!.NoAnimations == false)
                {
                    await Delay!.DelaySeconds(1);
                }
                RemoveFromPile(pile);
            }
            if (whichType == EnumCardType.Stock)
            {
                if (_model.StockPile!.DidGoOut() == true)
                {
                    await ShowWinAsync();
                    return;
                }
            }
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Computer)
            {
                if (BasicData!.MultiPlayer == false || BasicData.Client == false)
                {
                    ComputerList.RemoveFirstItem();
                }
            }
            int manys = _model!.Pile1!.HowManyInDiscard();
            if (_model.Pile1.PileEmpty() == false)
            {
                manys++; //to account for the current card as well because of this part ReshuffleAllCardsFromDiscard
            }
            int expected = SaveRoot!.CardsToShuffle;
            //try here to see if it reconciles.  so if it does not, can hopefully get to the root of the problem each time.
            if (manys != expected)
            {
                throw new BasicBlankException("After playing on pile, does not reconcile for the discard pile.  Rethink further");
            }
            await ContinueTurnAsync(); //since this will check first to see if it has to draw 5 more cards.
        }
    }
}