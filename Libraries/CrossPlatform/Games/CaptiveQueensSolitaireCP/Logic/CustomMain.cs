using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.PileInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace CaptiveQueensSolitaireCP.Logic
{
    public class CustomMain : BasicMultiplePilesCP<SolitaireCard>, IMain
    {
        public int CardsNeededToBegin { get; set; }
        public bool IsRound { get; set; }

#pragma warning disable 0067

        public event MainPileClickedEventHandler? PileSelectedAsync;
#pragma warning restore 0067
        private readonly IScoreData _score;

        public CustomMain(IScoreData score, CommandContainer command) : base(command)
        {
            _score = score;
        }

        public void SetSavedScore(int previousScore)
        {
            _score.Score = previousScore;
        }

        public override void ClearBoard()
        {
            base.ClearBoard();
            _score.Score = 4;
        }

        public void AddCard(int pile, SolitaireCard thisCard)
        {
            AddCardToPile(pile, thisCard);
            _score.Score++;
        }

        public void FirstLoad(bool needToMatch, bool showNextNeeded)
        {
            Columns = 8;
            Rows = 1;
            HasFrame = true;
            HasText = false;
            Style = EnumMultiplePilesStyleList.HasList;
            LoadBoard();
            static bool GetAngle(int index)
            {
                if (index == 2 || index == 3 || index == 6 || index == 7)
                {
                    return true;
                }
                return false;
            }
            PileList!.ForEach(thisPile =>
            {
                int index = PileList.IndexOf(thisPile);
                thisPile.Rotated = GetAngle(index);
            });

        }
        public int HowManyPiles()
        {
            if (PileList!.Count != 8)
            {
                throw new BasicBlankException("There should have been 8 piles");
            }
            return PileList.Count;
        }
        protected override async Task OnPileClickedAsync(int Index, BasicPileInfo<SolitaireCard> ThisPile)
        {
            await PileSelectedAsync!(Index);
        }

        public bool CanAddCard(int pile, SolitaireCard thiscard)
        {
            if (HasCard(pile) == false)
            {
                return pile switch
                {
                    0 => thiscard.Value == EnumRegularCardValueList.Six && thiscard.Suit == EnumSuitList.Hearts,
                    1 => thiscard.Value == EnumRegularCardValueList.Five && thiscard.Suit == EnumSuitList.Hearts,
                    2 => thiscard.Value == EnumRegularCardValueList.Six && thiscard.Suit == EnumSuitList.Clubs,
                    3 => thiscard.Value == EnumRegularCardValueList.Five && thiscard.Suit == EnumSuitList.Clubs,
                    4 => thiscard.Value == EnumRegularCardValueList.Six && thiscard.Suit == EnumSuitList.Diamonds,
                    5 => thiscard.Value == EnumRegularCardValueList.Five && thiscard.Suit == EnumSuitList.Diamonds,
                    6 => thiscard.Value == EnumRegularCardValueList.Six && thiscard.Suit == EnumSuitList.Spades,
                    7 => thiscard.Value == EnumRegularCardValueList.Five && thiscard.Suit == EnumSuitList.Spades,
                    _ => throw new BasicBlankException("Only has 8 piles"),
                };
            }
            var firstCard = PileList![pile].ObjectList.First();
            var lastCard = GetLastCard(pile);
            if (lastCard.Suit != firstCard.Suit)
            {
                throw new BasicBlankException("Must match suit");
            }
            if (lastCard.Suit != thiscard.Suit)
            {
                return false;
            }
            if (lastCard.Value + 1 == thiscard.Value && firstCard.Value == EnumRegularCardValueList.Six)
            {
                return true;
            }
            if (lastCard.Value - 1 == thiscard.Value && firstCard.Value == EnumRegularCardValueList.Five)
            {
                return true;
            }
            return firstCard.Value == EnumRegularCardValueList.Five && lastCard.Value == EnumRegularCardValueList.LowAce && thiscard.Value == EnumRegularCardValueList.King;
        }
        public int StartNumber()
        {
            throw new NotImplementedException(); // until i decide what to do
        }
        public void ClearBoard(IDeckDict<SolitaireCard> thisList) //somehow was never done.
        {

        }
        public async Task LoadGameAsync(string data)
        {
            PileList = await js.DeserializeObjectAsync<CustomBasicList<BasicPileInfo<SolitaireCard>>>(data);
        }
        public async Task<string> GetSavedPilesAsync()
        {
            return await js.SerializeObjectAsync(PileList!);
        }

        public void AddCards(int Pile, IDeckDict<SolitaireCard> list)
        {

        }
        public void AddCards(IDeckDict<SolitaireCard> list)
        {

        }
    }
}