using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
namespace BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses
{
    public static class Extensions
    {
        public static DeckRegularDict<SolitaireCard> ListValidCardsSameSuit(this IDeckDict<SolitaireCard> givenList)
        {
            int x;
            int previousNumber = 0;
            DeckRegularDict<SolitaireCard> output = new DeckRegularDict<SolitaireCard>();
            EnumSuitList previousSuit = EnumSuitList.None;
            for (x = givenList.Count; x >= 1; x += -1)
            {
                var thisCard = givenList[x - 1];
                if (x == givenList.Count)
                {
                    previousSuit = thisCard.Suit;
                    previousNumber = (int)thisCard.Value;
                    output.Add(thisCard);
                }
                else
                {
                    if (previousNumber + 1 == (int)thisCard.Value && thisCard.Suit == previousSuit)
                    {
                        output.Add(thisCard);
                    }
                    else
                    {
                        return output;
                    }
                    previousNumber = (int)thisCard.Value;
                }
            }
            return output;
        }
        public static DeckRegularDict<SolitaireCard> ListValidCardsAlternateColors(this IDeckDict<SolitaireCard> givenList)
        {
            int x;
            int previousNumber = 0;
            DeckRegularDict<SolitaireCard> output = new DeckRegularDict<SolitaireCard>();
            EnumRegularColorList previousColor = EnumRegularColorList.None;
            for (x = givenList.Count; x >= 1; x += -1)
            {
                var thisCard = givenList[x - 1];
                if (x == givenList.Count)
                {
                    previousColor = thisCard.Color;
                    previousNumber = (int)thisCard.Value;
                    output.Add(thisCard);
                }
                else
                {

                    if (previousNumber + 1 == (int)thisCard.Value && thisCard.Color != previousColor)
                    {
                        output.Add(thisCard);
                    }
                    else
                    {
                        return output;
                    }
                    previousNumber = (int)thisCard.Value;
                    previousColor = thisCard.Color;
                }
            }
            return output;
        }
        public static bool CanMoveCardsAlternateColors(this IDeckDict<SolitaireCard> validList, SolitaireCard oldCard, ref int lastOne)
        {
            lastOne = -1; //i think.
            int x;
            for (x = validList.Count; x >= 1; x += -1)
            {
                var newCard = validList[x - 1];
                if (newCard.Value + 1 == oldCard.Value && newCard.Color != oldCard.Color)
                {
                    lastOne = x - 1;
                    return true;
                }
            }
            return false;
        }
        public static bool CanMoveCardsSameColor(this IDeckDict<SolitaireCard> validList, SolitaireCard oldCard, ref int lastOne)
        {
            lastOne = -1; //i think.
            int x;
            for (x = validList.Count; x >= 1; x += -1)
            {
                var newCard = validList[x - 1];
                if (newCard.Value + 1 == oldCard.Value && newCard.Color == oldCard.Color)
                {
                    lastOne = x - 1;
                    return true;
                }
            }
            return false;
        }
        public static bool CanMoveCardsRegardlessOfColorOrSuit(this IDeckDict<SolitaireCard> validList, SolitaireCard oldCard, ref int lastOne)
        {
            lastOne = -1; //i think.
            int x;
            for (x = validList.Count; x >= 1; x += -1)
            {
                var newCard = validList[x - 1];
                if (newCard.Value + 1 == oldCard.Value)
                {
                    lastOne = x - 1;
                    return true;
                }
            }
            return false;
        }
    }
}