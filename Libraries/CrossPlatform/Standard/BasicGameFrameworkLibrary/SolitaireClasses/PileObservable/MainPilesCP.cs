using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.PileInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.SolitaireClasses.PileObservable
{
    public class MainPilesCP : IMain
    {
        public int CardsNeededToBegin { get; set; } //the bad news is if part of interface, forced to be public when in c#.
        public bool IsRound { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private bool _suitsNeedToMatch;
        internal bool ShowNextNeeded { get; set; }
        protected int Increments = 1; // increase by 1.  however; some games may increment by a different amount
        public event MainPileClickedEventHandler? PileSelectedAsync;
        public BasicMultiplePilesCP<SolitaireCard> Piles;
        protected IRegularDeckInfo DeckContents;
        private readonly IScoreData _score;
        public MainPilesCP(IScoreData thisMod, CommandContainer command, IGamePackageResolver resolver)
        {
            _score = thisMod;
            DeckContents = resolver.Resolve<IRegularDeckInfo>(); //this is even better.
            Piles = new BasicMultiplePilesCP<SolitaireCard>(command);
            Piles.PileClickedAsync += Piles_PileClickedAsync;
        }
        private async Task Piles_PileClickedAsync(int Index, BasicPileInfo<SolitaireCard> thisPile)
        {
            if (PileSelectedAsync == null)
            {
                return;
            }
            await PileSelectedAsync.Invoke(Index);
        }
        public void SetSavedScore(int score) //hopefully this way still works.  may rethink but not sure yet.
        {
            _score.Score = score; //try this way.
        }
        public virtual void ClearBoard(IDeckDict<SolitaireCard> thisList)
        {
            if (thisList.Count != CardsNeededToBegin)
            {
                throw new BasicBlankException($"Needs {CardsNeededToBegin} not {thisList.Count}");
            }
            _score.Score = thisList.Count;
            if (CardsNeededToBegin != Rows * Columns && CardsNeededToBegin != 0 && IsRound == false)
            {
                throw new BasicBlankException($"Since there are no 0 cards needed; then needs {Rows * Columns}, not {CardsNeededToBegin}");
            }
            else if (IsRound && CardsNeededToBegin != 1)
            {
                throw new BasicBlankException("When its round suits; needs only one card to begin with");
            }
            else if (IsRound && thisList.Count != 1)
            {
                throw new BasicBlankException("When its round; needs to have exactly one card to begin with");
            }
            int y = 2;
            Piles.PileList!.ForEach(thisPile =>
            {
                if (ShowNextNeeded)
                {
                    thisPile.Text = y.ToString();
                }
                y += 2;
            });
            Piles.ClearBoard();
            if (thisList.Count == 0)
            {
                return;
            }
            int x = 0;
            foreach (var thisPile in Piles.PileList)
            {
                thisPile.ObjectList.Add(thisList[x]);
                x++;
                if (IsRound)
                {
                    return;
                }
            }
        }
        public void ClearBoard()
        {
            if (CardsNeededToBegin != 0)
            {
                throw new BasicBlankException("Must send a collection since this game requires cards to begin with");
            }
            DeckRegularDict<SolitaireCard> tempList = new DeckRegularDict<SolitaireCard>();
            ClearBoard(tempList);
        }
        protected int FindNextNeeded(int pile, int oldNumber)
        {
            if (oldNumber == 13)
            {
                return 0; //cannot go further than a king.
            }
            int diffs = 0;
            do
            {
                oldNumber++;
                if (oldNumber > 13)
                {
                    oldNumber = 1;
                }
                if (diffs == pile)
                {
                    return oldNumber;
                }
                diffs++; //set afterwards because its 0 based.
            } while (true);
        }
        public void AddCard(int pile, SolitaireCard thisCard)
        {
            if (thisCard.Suit == EnumSuitList.None)
            {
                throw new BasicBlankException("The suit cannot be none.  Hint:  May be a problem with cloning.  Find out what happened");
            }
            if (ShowNextNeeded)
            {
                var thisPile = Piles.PileList![pile];
                thisPile.Text = FindNextNeeded(pile, (int)thisCard.Value).ToString();
                if (thisPile.Text == "0")
                {
                    thisPile.IsEnabled = false; //i think this should be fine.  if i am wrong, rethink
                }
            }
            Piles.AddCardToPile(pile, thisCard);
            _score.Score++;
        }
        public void FirstLoad(bool needToMatch, bool _showNextNeeded)
        {
            ShowNextNeeded = _showNextNeeded;
            Piles.Rows = Rows;
            Piles.Columns = Columns;
            Piles.HasFrame = true;
            Piles.HasText = ShowNextNeeded;
            Piles.Style = EnumMultiplePilesStyleList.HasList;
            _suitsNeedToMatch = needToMatch;
            Piles.LoadBoard();
        }
        public void AddCards(int pile, IDeckDict<SolitaireCard> list)
        {
            list.ForEach(thisCard =>
            {
                Piles.AddCardToPile(pile, thisCard);
            });
            _score.Score += list.Count;
        }
        public void AddCards(IDeckDict<SolitaireCard> list)
        {
            for (int x = 0; x < list.Count; x++)
                if (Piles.HasCard(x) == false)
                {
                    AddCards(x, list);
                    return;
                }
            throw new BasicBlankException("There was no empty columns");
        }
        public int HowManyPiles()
        {
            return Piles.PileList!.Count;
        }
        public bool HasCard(int pile)
        {
            return Piles.HasCard(pile);
        }
        public int StartNumber()
        {
            if (IsRound == false)
            {
                return 0;
            }
            return (int)Piles.PileList.First().ObjectList.First().Value;
        }
        public virtual bool CanAddCard(int pile, SolitaireCard thisCard)
        {
            if (Piles.HasCard(pile) == false)
            {
                if ((int)thisCard.Value == Increments && IsRound == false)
                {
                    return true;
                }
                int starts = StartNumber();
                return (int)thisCard.Value == starts && IsRound;
            }
            var thisPile = Piles.PileList![pile];
            if (ShowNextNeeded)
            {
                return int.Parse(thisPile.Text) == (int)thisCard.Value;
            }
            var oldCard = Piles.GetLastCard(pile);
            if (_suitsNeedToMatch)
            {
                if (oldCard.Suit != thisCard.Suit)
                {
                    return false;
                }
            }
            if (thisPile.ObjectList.Count == 1 && Increments == 1 && DeckContents.LowestNumber > 2 && IsRound == false)
            {
                return (int)thisCard.Value == DeckContents.LowestNumber;
            }
            if ((int)oldCard.Value + Increments == (int)thisCard.Value)
            {
                return true;
            }
            if (IsRound && Increments == 1 && oldCard.Value == EnumRegularCardValueList.King && thisCard.Value == EnumRegularCardValueList.LowAce)
            {
                return true;
            }
            if ((int)oldCard.Value + Increments <= 13)
            {
                return false;
            }
            if (Increments == 1)
            {
                return false; //because already maxed out;
            }
            int diffs = 0;
            int olds = (int)oldCard.Value;
            do
            {
                olds++;
                diffs++;
                if (olds > 13)
                {
                    olds = 1;
                }
                if ((int)thisCard.Value == olds)
                {
                    break;
                }
                if (diffs > 13)
                {
                    throw new BasicBlankException("The difference cannot be more than 13.  Find out what happened");
                }
            } while (true);
            return diffs == Increments;
        }
        public async Task LoadGameAsync(string data)
        {
            CustomBasicList<BasicPileInfo<SolitaireCard>> tempList = await js.DeserializeObjectAsync<CustomBasicList<BasicPileInfo<SolitaireCard>>>(data);
            Piles.PileList = tempList; //hopefully this simple.
        }
        public async Task<string> GetSavedPilesAsync() //has to be string or is locked to a type
        {
            return await js.SerializeObjectAsync(Piles.PileList!); //hopefully this simple.
        }
    }
}