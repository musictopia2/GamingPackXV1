using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace PokerCP.Data
{
    public class PokerCardInfo : RegularSimpleCard
    {
        public EnumRegularCardValueList SecondNumber //since i use low ace, here, use there too.
        {
            get
            {
                if (Value != EnumRegularCardValueList.HighAce)
                {
                    return Value;
                }
                return EnumRegularCardValueList.LowAce; //second seemed to lean towards low.
            }
        }
    }
}