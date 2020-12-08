using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class RegularRummyCard : RegularSimpleCard, IRummmyObject<EnumSuitList, EnumRegularColorList>
    {
        int IRummmyObject<EnumSuitList, EnumRegularColorList>.GetSecondNumber => (int)SecondNumber; //decided that even for rummy games, it will lean towards low.  if i am wrong, rethink.  for cases there is a choice, lean towards low.
        bool IIgnoreObject.IsObjectIgnored => false; //i can't think of a single game where we can ignore a card.
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
        public int Player { get; set; } //most rummy games require the player of the one who owns it or gets credit for it.
    }
}