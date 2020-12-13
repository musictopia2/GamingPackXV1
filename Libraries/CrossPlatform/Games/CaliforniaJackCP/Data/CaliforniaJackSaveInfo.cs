using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CaliforniaJackCP.Cards;
namespace CaliforniaJackCP.Data
{
    [SingletonGame]
    public class CaliforniaJackSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, CaliforniaJackCardInformation, CaliforniaJackPlayerItem>
    {
        public DeckRegularDict<CaliforniaJackCardInformation> CardList = new DeckRegularDict<CaliforniaJackCardInformation>();
    }
}