using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
using System.Drawing;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class RegularMultiTRCard : RegularSimpleCard, ITrickCard<EnumSuitList>, IRummmyObject<EnumSuitList, EnumRegularColorList>
    {
        public int Player { get; set; } //i don't think this needs binding.
        public virtual int GetPoints => Points; //different games can have different formulas for calculating points.
        public object CloneCard()
        {
            return MemberwiseClone(); //hopefully this simple (?)
        }
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

        public PointF Location { get; set; } //no need to be observable for this anymore.
    }
}