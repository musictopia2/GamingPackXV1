using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.BasicVMInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using BasicGameFrameworkLibrary.CommandClasses;
namespace BasicGameFrameworkLibrary.SolitaireClasses.MainClasses
{
    public abstract class SolitaireGameClass<S> : IAggregatorContainer where S : SolitaireSavedClass, new()
    {
        protected IBasicSolitaireVM? _thisMod;
        public RegularCardsBasicShuffler<SolitaireCard> DeckList;
        private readonly ISaveSinglePlayerClass _thisState;
        private readonly IScoreData _score;
        private bool _didAutoPlay;
        public int AutoMoveToMainPiles { get; set; }
        public S SaveRoot;
        protected DeckRegularDict<SolitaireCard>? CardList;
        public bool HadOneDeal { get; set; }
        protected int HowManyCards;
        public int DealsRemaining { get; set; }
        public bool NoCardsToShuffle { get; set; }
        public IEventAggregator Aggregator { get; }
        public CommandContainer Command { get; }

        protected bool HadWaste;
        protected ISolitaireData SolitaireData1;
        internal bool GameGoing { get; set; }
        public async Task OpenSavedGameAsync()
        {
            _opened = true;
            DeckList.OrderedObjects(); //i think
            SaveRoot = await _thisState.RetrieveSinglePlayerGameAsync<S>();
            S tempRoot = SaveRoot.AutoMap<S>();
            _score.Score = SaveRoot.Score; //i think.
            SaveRoot = tempRoot; //hopefully this simple so more efficient.
            await _thisMod!.MainPiles1!.LoadGameAsync(SaveRoot.MainPileData);
            _thisMod.MainDiscardPile!.SavedDiscardPiles(SaveRoot.Discard);
            await _thisMod.WastePiles1!.LoadGameAsync(SaveRoot.WasteData);
            if (SaveRoot.IntDeckList.Count > 0)
            {
                var newList = SaveRoot.IntDeckList.GetNewObjectListFromDeckList(DeckList);
                _thisMod.DeckPile!.OriginalList(newList);
            }
            _thisMod.MainPiles1.SetSavedScore(SaveRoot.Score); //i think.
            CardList = DeckList.ToRegularDeckDict();
            NoCardsToShuffle = false;
            HowManyCards = CardList.Count;
            await ContinueOpenSavedAsync(); //sometimes you need something else.
            AfterInit();
        }
        protected virtual Task ContinueOpenSavedAsync() { return Task.CompletedTask; }
        public async Task SaveGameAsync()
        {
            SaveRoot.MainPileData = await _thisMod!.MainPiles1!.GetSavedPilesAsync();
            SaveRoot.WasteData = await _thisMod.WastePiles1!.GetSavedGameAsync();
            SaveRoot.Discard = _thisMod.MainDiscardPile!.GetSavedPile();
            SaveRoot.IntDeckList = _thisMod.DeckPile!.GetCardIntegers();
            await FinishSaveAsync(); //this means the ones i previously could not save i now can.  has to look at the vb code possibly though in those cases.
            await _thisState.SaveSimpleSinglePlayerGameAsync(SaveRoot); //i think
        }
        protected virtual Task FinishSaveAsync() { return Task.CompletedTask; }
        private bool _opened = false;
        public virtual async Task NewGameAsync() //so for eight off, can do other things like clear cards.
        {
            if (await _thisState.CanOpenSavedSinglePlayerGameAsync() && _opened == false)
            {
                await OpenSavedGameAsync();
                return;
            }
            ShuffleCards();
        }
        public SolitaireGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            ) //you need the main view model loaded first or will have overflow errors.
        {
            DeckList = new RegularCardsBasicShuffler<SolitaireCard>(); //i think this should be fine.
            _thisState = thisState;
            Aggregator = aggregator;
            _score = score;
            Command = command;
            SaveRoot = new S(); //i think.
            SolitaireData1 = solitaireData1;
        }
        internal void LinkData(SolitaireMainViewModel<S> model)
        {
            if (SolitaireData1.MainRound == true && SolitaireData1.CardsNeededMainBegin != 1)
            {
                throw new BasicBlankException("If the main is round; then must have at least one card to begin with");
            }
            model.MainPiles1!.IsRound = SolitaireData1.MainRound;
            model.WastePiles1!.HowManyPiles = SolitaireData1.WastePiles;
            model.MainPiles1.Rows = SolitaireData1.Rows;
            model.MainPiles1.Columns = SolitaireData1.Columns;
            if (SolitaireData1.WasteColumns > 0 && SolitaireData1.WasteRows > 0)
            {
                if (SolitaireData1.MoveColumns == EnumSolitaireMoveType.MoveColumn)
                {
                    throw new BasicBlankException("Cannot move a column because there are discard piles instead");
                }
                model.WastePiles1.FirstLoad(SolitaireData1.WasteRows, SolitaireData1.WasteColumns);
            }
            else if (SolitaireData1.WasteColumns == 0 && SolitaireData1.WasteRows == 0)
            {
                model.WastePiles1.FirstLoad(SolitaireData1.IsKlondike, DeckList.ToRegularDeckDict());
            }
            else
            {
                throw new BasicBlankException("Either has to be 0 columns and 0 rows for the waste.  Or both has to be greater than 0");
            }
            model.WastePiles1.CardsNeededToBegin = SolitaireData1.CardsNeededWasteBegin;
            model.MainPiles1.CardsNeededToBegin = SolitaireData1.CardsNeededMainBegin;
            model.MainPiles1.FirstLoad(SolitaireData1.SuitsNeedToMatchForMainPile, SolitaireData1.ShowNextNeededOnMain);
        }
        private bool _didInit;
        public async Task InitAsync(IBasicSolitaireVM mod) //another option open.
        {
            _thisMod = mod;
            //eventually we can have it go from one game to another (not ready yet).
            //if (await _thisState.CanOpenSavedSinglePlayerGameAsync())
            //{
            //    await OpenSavedGameAsync();
            //    return;
            //}
            if (_didInit)
            {
                await NewGameAsync();
                return;
            }
            _didInit = true;
            if (_thisMod.CanStartNewGameImmediately)
            {
                await NewGameAsync(); //this will automatically check to see if it can save state
                return;
            }
            _thisMod.MainPiles1!.FirstLoad(SolitaireData1.SuitsNeedToMatchForMainPile, SolitaireData1.ShowNextNeededOnMain);
            _thisMod.WastePiles1!.FirstLoad(SolitaireData1.IsKlondike, new DeckRegularDict<SolitaireCard>());
        }
        protected SolitaireCard FindCardBySuitValue(EnumRegularCardValueList value, EnumSuitList suit)
        {
            return DeckList.First(items => items.Value == value && items.Suit == suit);
        }
        private void ShuffleCards()
        {
            if (_thisMod!.CanStartNewGameImmediately == false)
            {
                DeckList = new RegularCardsBasicShuffler<SolitaireCard>(); //try to create new one each time if you can't start immediately.
            }
            DeckList.ShuffleObjects(); //i think
            CardList = DeckList.ToRegularDeckDict();
            _thisMod.DeckPile!.ClearCards();
            _thisMod.MainDiscardPile!.ClearCards();
            HadOneDeal = false;
            NoCardsToShuffle = false;
            HowManyCards = CardList.Count;
            AfterShuffleCards();
        }
        abstract protected void AfterShuffleCards(); //i think
        protected void AfterShuffle()
        {
            var newCol = CardList!.GetRange(0, SolitaireData1.CardsNeededWasteBegin).ToRegularDeckDict();
            _thisMod!.WastePiles1!.ClearBoard(newCol);
            CardList.RemoveRange(0, SolitaireData1.CardsNeededWasteBegin);
            if (CardList.Count > 0)
            {
                PopulateDeck(CardList);
            }
            AfterInit();
        }
        protected void AfterShuffle(IDeckDict<SolitaireCard> mainList)
        {
            _thisMod!.MainPiles1!.ClearBoard(mainList);
            AfterShuffle();
        }
        private void AfterInit()
        {
            GameGoing = true;
            DealsRemaining = SolitaireData1.Deals - 1; //minus 1 because this is the first deal.  if only one is allowed; then will be 0
            _thisMod!.WastePiles1!.GetUnknowns();
            _thisMod.DeckPile.IsEnabled = true;
            _thisMod.MainDiscardPile.IsEnabled = true;
        }
        protected DeckRegularDict<SolitaireCard> GetAceList()
        {
            var output = CardList!.Where(items => items.Value == EnumRegularCardValueList.LowAce || items.Value == EnumRegularCardValueList.HighAce).ToRegularDeckDict();
            CardList!.RemoveGivenList(output);
            return output;
        }
        protected virtual void PopulateDeck(IEnumerableDeck<SolitaireCard> leftOverList) => _thisMod!.DeckPile!.OriginalList(leftOverList);
        protected virtual void AddToScore() { }
        protected virtual Task<bool> HasOtherAsync(int pile)
        {
            return Task.FromResult(false);
        }
        protected virtual void AfterMoveSingleCard() { } //hopefully no need for isbusy
        public virtual Task AfterMovingCardsAsync(int whichOne) { return Task.CompletedTask; }
        protected virtual bool HasWon(int scores)
        {
            return scores == HowManyCards;
        }
        protected virtual void RemoveFromMiscPiles(SolitaireCard thisCard) { }
        protected async Task FinishAddingToMainPileAsync(int pile, SolitaireCard thisCard)
        {
            if (thisCard.Deck > 0)
            {
                _thisMod!.MainPiles1!.AddCard(pile, thisCard);
            }
            if (_thisMod!.MainDiscardPile!.PileEmpty() == false && _thisMod.MainDiscardPile.GetCardInfo().Equals(thisCard))
            {
                _thisMod.MainDiscardPile.RemoveFromPile();
            }
            else if (_thisMod.DeckPile!.DeckStyle == EnumDeckPileStyle.AlwaysKnown
                && _thisMod.DeckPile.IsEndOfDeck() == false &&
                _thisMod.DeckPile.RevealCard().Equals(thisCard))
            {
                _thisMod.DeckPile.DrawCard();
            }
            else if (HadWaste)
            {
                _thisMod.WastePiles1!.RemoveSingleCard();
            }
            else
            {
                RemoveFromMiscPiles(thisCard); //used for games like freecell, eight off solitaire
            }
            AddToScore();
            _thisMod.WastePiles1!.UnselectAllColumns();
            _thisMod.DeckPile!.IsSelected = false;
            if (_didAutoPlay == false)
            {
                await FinishedAsync();
            }
            else
            {
                Command.UpdateAll(); //this makes it work with blazor.
            }
        }
        protected async Task FinishAddingToMainPileAsync() => await FinishAddingToMainPileAsync(0, new SolitaireCard());
        private async Task FinishedAsync()
        {
            int scores = _score!.Score;
            if (HasWon(scores))
            {
                await ShowWinAsync();
                return;
            }
        }
        protected async Task ShowWinAsync()
        {
            ToastPlatform.ShowSuccess("Congratulations, you won");
            await Task.Delay(2000);
            GameGoing = false;
            await this.SendGameOverAsync();
        }
        protected int ValidMainColumn(SolitaireCard thisCard) //grandfathers clock has to be different
        {
            int piles = _thisMod!.MainPiles1!.HowManyPiles();
            for (int x = 1; x <= piles; x++)
            {
                if (_thisMod.MainPiles1.CanAddCard(x - 1, thisCard))
                {
                    return x - 1;
                }
            }
            return -1;
        }
        public virtual async Task DiscardToMainAsync()
        {
            if (_thisMod!.MainDiscardPile!.PileEmpty() == false && _thisMod.DeckPile!.DeckStyle == EnumDeckPileStyle.Unknown)
            {
                return;
            }
            if (_thisMod.DeckPile!.IsEndOfDeck() && _thisMod.DeckPile.DeckStyle == EnumDeckPileStyle.AlwaysKnown)
            {
                return;
            }
            SolitaireCard thisCard;
            if (_thisMod.DeckPile.DeckStyle == EnumDeckPileStyle.Unknown)
            {
                thisCard = _thisMod.MainDiscardPile.GetCardInfo();
            }
            else if (_thisMod.DeckPile.IsEndOfDeck() == false)
            {
                thisCard = _thisMod.DeckPile.RevealCard();
            }
            else
            {
                thisCard = new SolitaireCard();
            }
            int index = ValidMainColumn(thisCard);
            if (index == -1)
            {
                _thisMod.MainDiscardPile.UnselectCard();
                return;
            }
            HadWaste = false;
            await FinishAddingToMainPileAsync(index, thisCard);
        }
        protected virtual int WhichAutoMoveIsValid()
        {
            int piles = _thisMod!.WastePiles1!.HowManyPiles;
            for (int x = 0; x < piles; x++)
            {
                if (_thisMod.WastePiles1.HasCard(x))
                {
                    var thisCard = _thisMod.WastePiles1.GetCard(x);
                    if (ValidMainColumn(thisCard) > -1)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }
        public async Task MakeAutoMovesToMainPilesAsync()
        {
            _thisMod!.WastePiles1!.UnselectAllColumns();
            int z = 0;
            do
            {
                z++;
                _didAutoPlay = true;
                int x = WhichAutoMoveIsValid();
                if (x == -1)
                {
                    _didAutoPlay = false;
                    await FinishedAsync();
                    return;
                }
                HadWaste = true;
                var thisCard = _thisMod.WastePiles1.GetCard(x);
                _thisMod.WastePiles1.SelectUnselectPile(x);
                int y = ValidMainColumn(thisCard);
                if (z > 1)
                {
                    await Task.Delay(150); //hopefully i don't regret this.
                }
                await FinishAddingToMainPileAsync(y, thisCard);
            } while (true);
        }
        public async Task WasteToMainAsync(int whichOne)
        {
            _didAutoPlay = false;
            _thisMod!.WastePiles1!.DoubleClickColumn(whichOne);
            if (_thisMod.WastePiles1.OneSelected() == -1)
            {
                return;
            }
            if (_thisMod.WastePiles1.HasCard(whichOne) == false)
            {
                _thisMod.WastePiles1.UnselectAllColumns();
                return;
            }
            var thisCard = _thisMod.WastePiles1.GetCard();
            int index = ValidMainColumn(thisCard);
            if (index == -1)
            {
                _thisMod.WastePiles1.UnselectAllColumns();
                return;
            }
            HadWaste = true;
            await FinishAddingToMainPileAsync(index, thisCard);
        }
        private void RedealCards()
        {
            var thisCol = _thisMod!.MainDiscardPile!.DiscardList();
            if (_thisMod.MainDiscardPile.PileEmpty() == false)
                thisCol.Add(_thisMod.MainDiscardPile.GetCardInfo());
            HadOneDeal = true;
            _thisMod.MainDiscardPile.ClearCards();
            _thisMod.DeckPile!.OriginalList(thisCol); //this should be fine.
            DealsRemaining--;
        }
        public virtual void DrawCard() //different for spider solitaire
        {
            if (_thisMod!.DeckPile!.IsEndOfDeck())
            {
                RedealCards();
                if (_thisMod.DeckPile.IsEndOfDeck())
                {
                    NoCardsToShuffle = true;
                    return;
                }
            }
            int howManyToDo;
            if (_thisMod.DeckPile.CardsLeft() <= SolitaireData1.CardsToDraw)
            {
                howManyToDo = 1;
            }
            else
            {
                howManyToDo = SolitaireData1.CardsToDraw; //i think that if near the end, should just draw one at a time.
            }
            for (int x = 0; x < howManyToDo; x++)
            {
                if (_thisMod.DeckPile.IsEndOfDeck())
                {
                    break;
                }
                var thisCard = _thisMod.DeckPile.DrawCard();
                _thisMod.MainDiscardPile!.AddCard(thisCard);
            }
        }
        public void SelectDiscard()
        {
            if (_thisMod!.WastePiles1!.OneSelected() > -1 || _thisMod.MainDiscardPile!.PileEmpty())
            {
                return;
            }
            if (_thisMod.MainDiscardPile.CardSelected() == 0)
            {
                _thisMod.MainDiscardPile.IsSelected(true);
            }
            else
            {
                _thisMod.MainDiscardPile.IsSelected(false);
            }
        }
        protected virtual SolitaireCard CardPlayed()
        {
            int prev = _thisMod!.WastePiles1!.OneSelected();
            HadWaste = false;
            if (prev > -1)
            {
                if (_thisMod.WastePiles1.HasCard(prev) == false)
                {
                    return new SolitaireCard();
                }
                var thisCard = _thisMod.WastePiles1.GetCard();
                HadWaste = true;
                return thisCard;
            }
            if (_thisMod.DeckPile!.DeckStyle == EnumDeckPileStyle.AlwaysKnown)
            {
                if (_thisMod.DeckPile.IsEndOfDeck())
                {
                    return new SolitaireCard();
                }
                return _thisMod.DeckPile.RevealCard();
            }
            if (_thisMod.MainDiscardPile!.Visible == false)
            {
                return new SolitaireCard(); //because there is no discard pile.
            }
            if (_thisMod.MainDiscardPile.PileEmpty() || _thisMod.MainDiscardPile.CardSelected() == 0)
            {
                return new SolitaireCard();
            }
            return _thisMod.MainDiscardPile.GetCardInfo();
        }
        public async Task MainPileSelectedAsync(int pile) //hopefully no need for isbusy
        {
            var thisCard = CardPlayed();
            if (thisCard.Deck == 0)
            {
                return;
            }
            if (_thisMod!.MainPiles1!.CanAddCard(pile, thisCard) == false)
            {
                return;
            }
            await FinishAddingToMainPileAsync(pile, thisCard);
        }
        public async Task WasteSelectedAsync(int whichOne)
        {
            bool ots = await HasOtherAsync(whichOne);
            if (ots)
            {
                return;
            }
            int ones = _thisMod!.WastePiles1!.OneSelected();
            if (_thisMod.DeckPile!.DeckStyle == EnumDeckPileStyle.AlwaysKnown)
            {
                if (_thisMod.DeckPile.IsSelected)
                {
                    _thisMod.WastePiles1.AddSingleCard(whichOne, _thisMod.DeckPile.DrawCard());
                    _thisMod.DeckPile.IsSelected = false;
                    return;
                }
                if (ones != whichOne && ones > 0)
                {
                    return;
                }
                if (_thisMod.WastePiles1.HasCard(whichOne) == false)
                {
                    return;
                }
                _thisMod.WastePiles1.SelectUnselectPile(whichOne);
                return;
            }
            if (_thisMod.MainDiscardPile!.CardSelected() == 0 && ones == -1 || ones == whichOne)
            {
                _thisMod.WastePiles1.SelectUnselectPile(whichOne);
                return;
            }
            if (_thisMod.MainDiscardPile.CardSelected() > 0)
            {
                if (_thisMod.WastePiles1.CanAddSingleCard(whichOne, _thisMod.MainDiscardPile.GetCardInfo()) == false)
                {
                    return;
                }
                _thisMod.WastePiles1.AddSingleCard(whichOne, _thisMod.MainDiscardPile.GetCardInfo());
                _thisMod.MainDiscardPile.RemoveFromPile();
                return;
            }
            if (SolitaireData1.MoveColumns == EnumSolitaireMoveType.CantMove)
            {
                return;
            }
            if (SolitaireData1.MoveColumns == EnumSolitaireMoveType.MoveColumn)
            {
                if (_thisMod.WastePiles1.CanMoveCards(whichOne, out int lasts) == false)
                {
                    return;
                }
                _thisMod.WastePiles1.MoveCards(whichOne, lasts);
                await AfterMovingCardsAsync(whichOne);
                return;
            }
            if (_thisMod.WastePiles1.CanMoveToAnotherPile(whichOne) == false)
            {
                return;
            }
            _thisMod.WastePiles1.MoveSingleCard(whichOne);
            AfterMoveSingleCard();
        }
    }
}