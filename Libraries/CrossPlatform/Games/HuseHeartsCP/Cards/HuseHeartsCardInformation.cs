using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace HuseHeartsCP.Cards
{
    public class HuseHeartsCardInformation : RegularTrickCard, IDeckObject
    {
        public int HeartPoints
        {
            get
            {
                if (Suit == EnumSuitList.Hearts)
                {
                    return 1;
                }
                if (Suit == EnumSuitList.Diamonds && Value == EnumRegularCardValueList.Jack)
                {
                    return -10;
                }
                if (Suit == EnumSuitList.Spades && Value == EnumRegularCardValueList.Queen)
                {
                    return 13;
                }
                return 0;
            }
        }
        public bool ContainPoints
        {
            get
            {
                if (Suit == EnumSuitList.Hearts)
                {
                    return true;
                }
                if (Suit == EnumSuitList.Spades && Value == EnumRegularCardValueList.Queen)
                {
                    return true;
                }
                return false;
            }
        }
    }
}