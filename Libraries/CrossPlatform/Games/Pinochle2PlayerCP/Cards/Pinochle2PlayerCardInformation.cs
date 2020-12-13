using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Exceptions;
using Newtonsoft.Json;
namespace Pinochle2PlayerCP.Cards
{
    public class Pinochle2PlayerCardInformation : RegularMultiTRCard, IDeckObject
    {
        [JsonIgnore]
        public int PinochleCardValue
        {
            get
            {
                return Value switch
                {
                    EnumRegularCardValueList.Nine => 0,
                    EnumRegularCardValueList.Ten => 10,
                    EnumRegularCardValueList.Jack => 2,
                    EnumRegularCardValueList.Queen => 3,
                    EnumRegularCardValueList.King => 4,
                    EnumRegularCardValueList.HighAce => 11,
                    _ => throw new BasicBlankException("The first number must be greater than 8"),
                };
            }
        }
    }
}