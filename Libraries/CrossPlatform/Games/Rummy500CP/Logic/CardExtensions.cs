using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace Rummy500CP.Logic
{
    public static class CardExtensions
    {
        public static int NegativePoints(this RegularRummyCard card)
        {
            //i think it should just show the minus amounts.
            if (card.Value == EnumRegularCardValueList.HighAce)
            {
                return -15;
            }
            if (card.Value < EnumRegularCardValueList.Eight)
            {
                return -5;
            }
            return -10;
        }
    }
}