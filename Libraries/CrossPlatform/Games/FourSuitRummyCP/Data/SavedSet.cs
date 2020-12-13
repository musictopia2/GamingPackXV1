using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace FourSuitRummyCP.Data
{
    public class SavedSet
    {
        public DeckRegularDict<RegularRummyCard> CardList = new DeckRegularDict<RegularRummyCard>();
    }
}