using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Exceptions;
using Newtonsoft.Json;
namespace SixtySix2PlayerCP.Cards
{
    public class SixtySix2PlayerCardInformation : RegularTrickCard, IDeckObject
    {
        [JsonIgnore]
        public int PinochleCardValue
        {
            get
            {
                switch (Value)
                {
                    case EnumRegularCardValueList.Nine:
                        return 0;
                    case EnumRegularCardValueList.Ten:
                        return 10;
                    case EnumRegularCardValueList.Jack:
                        return 2;
                    case EnumRegularCardValueList.Queen:
                        return 3;
                    case EnumRegularCardValueList.King:
                        return 4;
                    case EnumRegularCardValueList.HighAce:
                        return 11;
                    default:
                        throw new BasicBlankException("The first number must be greater than 8");
                }
            }
        }
    }
}