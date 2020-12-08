using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public abstract class PossibleDummyTrickObservable<SU, T, P, SA> : BasicTrickAreaObservable<SU, T>, IMultiplayerTrick<SU, T, P>
        , ITrickPlay
        where SU : struct, Enum
        where T : class, ITrickCard<SU>, new()
        where P : class, IPlayerTrick<SU, T>, new()
        where SA : BasicSavedTrickGamesClass<SU, T, P>, new()
    {
        private readonly TrickGameContainer<T, P, SA, SU> _gameContainer;
        public PossibleDummyTrickObservable(TrickGameContainer<T, P, SA, SU> gameContainer) : base(gameContainer.Command, gameContainer.Aggregator)
        {
            _gameContainer = gameContainer;
        }
        protected abstract bool UseDummy { get; set; }
        public CustomBasicList<TrickCoordinate>? ViewList { get; set; }
        protected abstract int GetCardIndex(); // this is different too.
        protected DeckRegularDict<T> OrderList => _gameContainer.SaveRoot!.TrickList;
        protected abstract void PopulateNewCard(T oldCard, ref T newCard);
        protected abstract void PopulateOldCard(T oldCard);
        protected virtual int GetMaxCount()
        {
            if (UseDummy == true)
            {
                return 3;
            }
            return 4;
        }
        public async Task PlayCardAsync(int deck)
        {
            T thisCard = _gameContainer.GetSpecificCardFromDeck(deck);
            thisCard.Player = _gameContainer.WhoTurn;
            PopulateOldCard(thisCard); // sometimes, its needed.  othertimes its not needed.
            int index;
            index = GetCardIndex();
            if (index == -1)
            {
                throw new BasicBlankException("Index cannot be -1");
            }
            T newCard;
            newCard = _gameContainer.GetBrandNewCard(deck);
            newCard.Player = _gameContainer.WhoTurn;
            newCard.Visible = true;
            PopulateOldCard(newCard); // i think
            OrderList.Add(newCard);
            TradeCard(index, newCard); // try this
            int nums;
            nums = GetMaxCount();
            _gameContainer.Command.UpdateAll();
            if (OrderList.Count == nums)
            {
                await _gameContainer.EndTrickAsync!.Invoke();
            }
            else
            {
                await _gameContainer.ContinueTrickAsync!.Invoke();
            }
        }
        protected virtual string FirstHumanText()
        {
            return "Yours";
        }
        protected virtual string FirstOpponentText()
        {
            return "Opponents";
        }
        protected virtual string DummyHumanText()
        {
            return "Dummy";
        }
        protected virtual string DummyOpponentText()
        {
            return "Dummy";
        }
        protected CustomBasicList<TrickCoordinate> GetCoordinateList()
        {
            CustomBasicList<TrickCoordinate> output = new CustomBasicList<TrickCoordinate>();
            int howManyPlayers = _gameContainer.PlayerList.Count();
            TrickCoordinate thisPlayer;
            if (howManyPlayers == 2)
            {
                if (UseDummy == false)
                {
                    throw new BasicBlankException("Must use dummy if 2 players.  If no dummy is used, try using TwoPlayerTrickViewModel");
                }
                thisPlayer = new TrickCoordinate();
                thisPlayer.IsSelf = true;
                thisPlayer.Row = 2;
                thisPlayer.Column = 1; // 1 based
                thisPlayer.Player = _gameContainer.SelfPlayer;
                thisPlayer.Text = FirstHumanText();
                output.Add(thisPlayer);
                thisPlayer = new TrickCoordinate();
                thisPlayer.Column = 1;
                thisPlayer.Row = 1;
                thisPlayer.Text = FirstOpponentText();
                if (_gameContainer.SelfPlayer == 1)
                {
                    thisPlayer.Player = 2;
                }
                else
                {
                    thisPlayer.Player = 1;
                }
                int oldPlayer;
                oldPlayer = thisPlayer.Player;
                output.Add(thisPlayer);
                thisPlayer = new TrickCoordinate();
                thisPlayer.Player = oldPlayer;
                thisPlayer.PossibleDummy = true;
                thisPlayer.Column = 2;
                thisPlayer.Row = 1;
                thisPlayer.Text = DummyOpponentText();
                output.Add(thisPlayer);
                thisPlayer = new TrickCoordinate();
                thisPlayer.Player = _gameContainer.SelfPlayer;
                thisPlayer.PossibleDummy = true;
                thisPlayer.Column = 2;
                thisPlayer.Row = 2;
                thisPlayer.Text = DummyHumanText();
                output.Add(thisPlayer);
                return output;
            }
            if (howManyPlayers == 3)
            {
                throw new BasicBlankException("Currently, can't support 3 players because even the old version was too buggy.  Therefore, only 2 players are supported for now");
            }
            // now focus on bottom.
            int howManyBottom;
            int howManyTop;
            if (howManyPlayers <= 4)
            {
                howManyBottom = 2;
            }
            else if (howManyPlayers <= 6)
            {
                howManyBottom = 3;
            }
            else
            {
                howManyBottom = 4;
            }
            howManyTop = howManyPlayers - howManyBottom;
            if (howManyTop > howManyBottom)
            {
                throw new Exception("Top can never have more than bottom");
            }
            int x;
            int y;
            y = _gameContainer.SelfPlayer;
            var loopTo = howManyBottom;
            for (x = 1; x <= loopTo; x++)
            {
                thisPlayer = new TrickCoordinate();
                if (x == 1)
                {
                    thisPlayer.IsSelf = true;
                }
                thisPlayer.Row = 2;
                thisPlayer.Column = x;
                thisPlayer.Player = y;
                y += 1;
                if (y > howManyPlayers)
                {
                    y = 1;
                }
                output.Add(thisPlayer);
            }

            var loopTo1 = howManyTop;
            for (x = 1; x <= loopTo1; x++)
            {
                thisPlayer = new TrickCoordinate();
                thisPlayer.Column = x;
                thisPlayer.Row = 1;
                thisPlayer.Player = y;
                y += 1;
                if (y > howManyPlayers)
                {
                    y = 1;
                }
                output.Add(thisPlayer);
            }
            return output;
        }
        public void FirstLoad()
        {
            if (_gameContainer.PlayerList.Count() == 0)
            {
                throw new BasicBlankException("Playerlist Has Not Been Initialized Yet");
            }
            ViewList = GetCoordinateList();
            if (ViewList.First().IsSelf == false)
            {
                throw new BasicBlankException("First must be self");
            }
            int x;
            MaxPlayers = 4; //i think
            CardList.Clear();
            for (x = 1; x <= 4; x++)
            {
                T newCard = new T();
                newCard.Populate(x);
                newCard.IsUnknown = true;
                newCard.Deck = x + 1000; //try this way.  to at least make it work.
                CardList.Add(newCard); //i think
            }
        }
        public P GetSpecificPlayer(int id)
        {
            return _gameContainer.PlayerList![id];
        }
    }
}