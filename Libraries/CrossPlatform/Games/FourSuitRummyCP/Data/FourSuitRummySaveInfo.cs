using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace FourSuitRummyCP.Data
{
    [SingletonGame]
    public class FourSuitRummySaveInfo : BasicSavedCardClass<FourSuitRummyPlayerItem, RegularRummyCard>
    {
        public int TimesReshuffled { get; set; }
    }
}