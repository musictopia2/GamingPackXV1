using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
namespace CousinRummyCP.Data
{
    public class SavedSet
    {
        public DeckRegularDict<RegularRummyCard> CardList = new DeckRegularDict<RegularRummyCard>();
    }
}