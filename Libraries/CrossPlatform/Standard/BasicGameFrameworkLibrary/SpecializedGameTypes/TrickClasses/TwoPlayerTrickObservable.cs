using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public class TwoPlayerTrickObservable<SU, T, P, SA> : BasicTrickAreaObservable<SU, T>,
        ITrickPlay, IAdvancedTrickProcesses
        where SU : struct, Enum
        where T : class, ITrickCard<SU>, new()
        where P : class, IPlayerTrick<SU, T>, new()
        where SA : BasicSavedTrickGamesClass<SU, T, P>, new()
    {
        private readonly TrickGameContainer<T, P, SA, SU> _gameContainer;
        public TwoPlayerTrickObservable(TrickGameContainer<T, P, SA, SU> gameContainer) : base(gameContainer.Command, gameContainer.Aggregator)
        {
            _gameContainer = gameContainer;
        }
        public void FirstLoad()
        {
            if (_gameContainer.PlayerList.Count() != 2)
            {
                throw new BasicBlankException("Must have 2 players in order to load");
            }
            int x;
            CardList.Clear(); //just in case.
            for (x = 1; x <= 2; x++)
            {
                T newCard = new T();
                newCard.Populate(x);
                newCard.Deck = x + 1000; //try to do it this way so at least it will work.
                newCard.IsUnknown = true;
                CardList.Add(newCard); //i think
            }
        }
        protected int GetCardIndex()
        {
            if (_gameContainer.SingleInfo!.PlayerCategory == EnumPlayerCategory.Self)
            {
                return 0;
            }
            return 1;
        }
        public bool IsLead => OrderList.Count == 0;
        public void ClearBoard()
        {
            DeckRegularDict<T> tempList = new DeckRegularDict<T>();
            int x = 0;
            foreach (var thisCard in CardList)
            {
                T tempCard = new T();
                x++;
                tempCard.Populate(x);
                tempCard.Deck += 1000; //try this way.  hopefully won't cause any other issues.
                tempCard.IsUnknown = true;
                tempCard.Visible = true;
                tempList.Add(tempCard);
            }
            OrderList.Clear();
            CardList.ReplaceRange(tempList); // hopefully its that simple.
            Visible = true; // now it is visible.
        }
        public virtual void LoadGame()
        {
            var tempList = OrderList.ToRegularDeckDict();
            ClearBoard();
            if (tempList.Count == 0)
            {
                return;
            }
            int index;
            int tempTurn;
            T lastCard;
            tempTurn = _gameContainer.WhoTurn;
            DeckRegularDict<T> otherList = new DeckRegularDict<T>();
            tempList.ForEach(thisCard =>
            {
                if (thisCard.Player == 0)
                {
                    throw new BasicBlankException("The Player Cannot Be 0");
                }
                _gameContainer.WhoTurn = thisCard.Player;
                _gameContainer.SingleInfo = _gameContainer.PlayerList!.GetWhoPlayer();
                index = GetCardIndex();
                lastCard = _gameContainer.GetBrandNewCard(thisCard.Deck);
                lastCard.Player = thisCard.Player;
                TradeCard(index, lastCard);
                otherList.Add(lastCard); //i think
            });
            OrderList.ReplaceRange(otherList); //i think we have to do it this way this tiem.
            _gameContainer.WhoTurn = tempTurn;
        }
        protected T GetWinningCard(int wins)
        {
            return OrderList.Single(items => items.Player == wins);
        }
        public async Task AnimateWinAsync(int wins)
        {
            var thisCard = GetWinningCard(wins);
            WinCard = thisCard;
            await AnimateWinAsync();
        }
        public async Task PlayCardAsync(int deck)
        {
            T thisCard = _gameContainer.GetSpecificCardFromDeck(deck);
            thisCard.Player = _gameContainer.WhoTurn;
            int index = GetCardIndex();
            T newCard = _gameContainer.GetBrandNewCard(deck);
            newCard.Player = _gameContainer.WhoTurn;
            newCard.Visible = true;
            OrderList.Add(newCard);
            TradeCard(index, newCard);
            _gameContainer.Command.UpdateAll();
            await AfterPlayCardAsync(thisCard);
        }
        protected virtual async Task AfterPlayCardAsync(T thisCard) //overrided versions may need this.
        {
            if (OrderList.Count == _gameContainer.PlayerList.Count())
            {
                await _gameContainer.EndTrickAsync!.Invoke();
            }
            else
            {
                await _gameContainer.ContinueTrickAsync!.Invoke();
            }
        }
        protected DeckRegularDict<T> OrderList => _gameContainer.SaveRoot!.TrickList;
        protected override async Task ProcessCardClickAsync(T thisCard)
        {
            if (CardList.IndexOf(thisCard) == 0)
                await _gameContainer.CardClickedAsync!.Invoke(); //because only human can do it for 2 player trick games.
        }       
    }
}