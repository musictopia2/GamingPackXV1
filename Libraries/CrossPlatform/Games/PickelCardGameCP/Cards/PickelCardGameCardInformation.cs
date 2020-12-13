using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using System;
namespace PickelCardGameCP.Cards
{
    public class PickelCardGameCardInformation : RegularTrickCard, IDeckObject, IComparable<PickelCardGameCardInformation>
    {
        int IComparable<PickelCardGameCardInformation>.CompareTo(PickelCardGameCardInformation other)
        {
            if (CardType != other.CardType)
            {
                return CardType.CompareTo(other.CardType);
            }
            if (Suit != other.Suit)
            {
                return Suit.CompareTo(other.Suit);
            }
            return Value.CompareTo(other.Value);
        }
    }
}